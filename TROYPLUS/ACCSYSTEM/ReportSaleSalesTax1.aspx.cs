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


public partial class ReportSaleSalesTax1 : System.Web.UI.Page
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
                    DataSet ds1 = bl.getImageInfo();
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                            {
                                Image1.ImageUrl = "App_Themes/NewTheme/images/" + ds1.Tables[0].Rows[i]["img_filename"];
                                Image1.Height = 95;
                                Image1.Width = 114;
                            }
                        }
                        else
                        {
                            Image1.Height = 95;
                            Image1.Width = 114;
                            Image1.ImageUrl = "App_Themes/NewTheme/images/TESTLogo.png";
                        }
                    }
                }
            }

            lblHeading.Text = "Purchase Report";

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


            string connection = Request.Cookies["Company"].Value;

            string branch = Convert.ToString(Request.QueryString["Branch"].ToString());
            DateTime startDate = Convert.ToDateTime(Request.QueryString["startdate"].ToString());
            DateTime endDate = Convert.ToDateTime(Request.QueryString["enddate"].ToString());

            condi = Request.QueryString["condi"].ToString();
            condi = Server.UrlDecode(condi);
            bindData();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    public void bindData()
    {
      //  DateTime startDate, endDate;
        DataSet ds = new DataSet();
        double total14 = 0;
        double total5 = 0;
        string intTrans = "";
        string salesRet = "";
        string delNote = "";

        string condi = "";
        DataSet dstt = new DataSet();

        intTrans = "NO";
        salesRet = "NO";
        delNote = "NO";
        string Branch = Convert.ToString(Request.QueryString["Branch"].ToString());
        DateTime startDate = Convert.ToDateTime(Request.QueryString["startdate"].ToString());
        DateTime endDate = Convert.ToDateTime(Request.QueryString["enddate"].ToString());

        condi = Request.QueryString["condi"].ToString();
        condi = Server.UrlDecode(condi);

        if (condi == "5%")
        {
            condi = " And tblPurchaseItems.vat = 5";
        }
        else if (condi == "14.5%")
        {
            condi = " And tblPurchaseItems.vat = 14.5";
        }
        if (condi == "All")
        {
            condi = "";
        }

       // startDate = Convert.ToDateTime(txtStartDate.Text.Trim());
       // endDate = Convert.ToDateTime(txtEndDate.Text.Trim());

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        ds = objBL.PurchaseAnnuxere(startDate, endDate, salesRet, intTrans, delNote, condi, Branch);
        Double serialno = 1;

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("SNo"));
                dt.Columns.Add(new DataColumn("Name of the Seller"));
                dt.Columns.Add(new DataColumn("Seller Tin"));
                dt.Columns.Add(new DataColumn("Commodity Code"));
                dt.Columns.Add(new DataColumn("Invoice No"));
                dt.Columns.Add(new DataColumn("Invoice Date"));
                dt.Columns.Add(new DataColumn("Purchase Value"));
                dt.Columns.Add(new DataColumn("Tax Rate"));
                dt.Columns.Add(new DataColumn("Vat CST Paid"));
                dt.Columns.Add(new DataColumn("Category"));
                dt.Columns.Add(new DataColumn("Branchcode"));

                DataRow dr_export1 = dt.NewRow();
                dt.Rows.Add(dr_export1);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["SNo"] = serialno;
                    dr_export["Name of the Seller"] = dr["LinkName"];
                    dr_export["Seller Tin"] = dr["Tinnumber"];
                    dr_export["Commodity Code"] = "";
                    dr_export["Invoice No"] = dr["BillNo"];
                    dr_export["Branchcode"] = dr["Branchcode"];

                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_export["Invoice Date"] = dtaa;

                    dr_export["Purchase Value"] = Convert.ToDouble(dr["NetPurchaseRate"]) - (Convert.ToDouble(dr["ActualDiscount"]) + Convert.ToDouble(dr["DiscAmt"]));

                    if (Convert.ToDouble(dr["vat"]) == 5)
                    {
                        total5 = total5 + Convert.ToDouble(dr["ActualVAT"]);
                    }
                    else if (Convert.ToDouble(dr["vat"]) == 14.5)
                    {
                        total14 = total14 + Convert.ToDouble(dr["ActualVAT"]);
                    }

                    dr_export["Tax Rate"] = dr["vat"];
                    dr_export["Vat CST Paid"] = dr["ActualVAT"];
                    dr_export["Category"] = "";
                    dt.Rows.Add(dr_export);

                    serialno = serialno + 1;
                }

                DataRow dr_export2 = dt.NewRow();
                dr_export2["SNo"] = "";
                dr_export2["Name of the Seller"] = "";
                dr_export2["Seller Tin"] = "";
                dr_export2["Commodity Code"] = "";
                dr_export2["Invoice No"] = "";
                dr_export2["Invoice Date"] = "";
                dr_export2["Purchase Value"] = "";
                dr_export2["Tax Rate"] = "";
                dr_export2["Vat CST Paid"] = "";
                dr_export2["Category"] = "";
                dt.Rows.Add(dr_export2);

                DataRow dr_export211 = dt.NewRow();
                dr_export211["SNo"] = "";
                dr_export211["Name of the Seller"] = "";
                dr_export211["Seller Tin"] = "";
                dr_export211["Commodity Code"] = "";
                dr_export211["Invoice No"] = "";
                dr_export211["Invoice Date"] = "";
                dr_export211["Purchase Value"] = "";
                dr_export211["Tax Rate"] = "Total 5% ";
                dr_export211["Vat CST Paid"] = total5;
                dr_export211["Category"] = "";
                dt.Rows.Add(dr_export211);

                DataRow dr_export213 = dt.NewRow();
                dr_export213["SNo"] = "";
                dr_export213["Name of the Seller"] = "";
                dr_export213["Seller Tin"] = "";
                dr_export213["Commodity Code"] = "";
                dr_export213["Invoice No"] = "";
                dr_export213["Invoice Date"] = "";
                dr_export213["Purchase Value"] = "";
                dr_export213["Tax Rate"] = "Total 14.5% ";
                dr_export213["Vat CST Paid"] = total14;
                dr_export213["Category"] = "";
                dt.Rows.Add(dr_export213);

                dstt.Tables.Add(dt);
               // ExportToExcel(dt);
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
        ReportsBL.ReportClass rptStock = new ReportsBL.ReportClass();
        // DataSet ds = new DataSet(sDataSource);
        //  DataSet ds = rptStock.getCategory(sDataSource);
        Grdreport.Visible = true;
        Grdreport.DataSource = dstt;
        Grdreport.DataBind();
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
        try
        {
           
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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
           
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    string condi;
    string cond1;
    string cond2;
    string cond3;
    string cond4;
    string cond5;
    string cond6;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           
                      
              
         }
        
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}