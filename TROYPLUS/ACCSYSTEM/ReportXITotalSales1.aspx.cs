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

public partial class ReportXITotalSales1 : System.Web.UI.Page
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
                lblHeading.Text = "Total Quantity Sales report";

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

            string option = Convert.ToString(Request.QueryString["option"].ToString());
            DateTime startdate = Convert.ToDateTime(Request.QueryString["startdate"].ToString());
            DateTime enddate = Convert.ToDateTime(Request.QueryString["enddate"].ToString());


            string cond = "";
            cond = getCond();
            if (option == "Item Wise")
            {
                bindDataSubTot(cond);
            }
            else if (option == "Brand Wise")
            {
                bindDataSubTotBrand(cond);
            }
            else if (option == "Category Wise")
            {
                bindDataSubTotCat(cond);
            }
            if (option == "Branch Wise")
            {
                bindDataSubTotBranch(cond);
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

        

        return cond;
    }

    public void bindDataSubTot(string cond)
    {
        DateTime startDate, endDate, Transdt;
       startDate = Convert.ToDateTime(Request.QueryString["startdate"].ToString());
         endDate = Convert.ToDateTime(Request.QueryString["enddate"].ToString());
        string condtion = "";
        condtion = getCond();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        //DataSet dsGird = GenerateGridColumns();
        DataSet dst = new DataSet();
        DataSet dts = new DataSet();
        DataSet dstt = new DataSet();
        DataSet dsGir = new DataSet();
        DataSet dsG = new DataSet();
        dst = objBL.gettotalproductsales(startDate, endDate);

        dts = objBL.gettotalproductsalesdate(startDate, endDate);

        dstt = objBL.ListAllProductName();

        if (dts.Tables[0].Rows.Count > 0)
        {
            DataTable dtt = new DataTable();
            DataColumn dc;
            if (dstt.Tables[0].Rows.Count > 0)
            {
                dc = new DataColumn("Date");
                dtt.Columns.Add(dc);
                for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                {
                    string ledger = dstt.Tables[0].Rows[i]["Productname"].ToString();
                    dc = new DataColumn(ledger);
                    dtt.Columns.Add(dc);
                }
                dc = new DataColumn("Total");
                dtt.Columns.Add(dc);
            }
            dsGir.Tables.Add(dtt);


            DataRow dr_final14 = dtt.NewRow();
            dtt.Rows.Add(dr_final14);

            double credit = 0.00;
            double Tottot = 0.00;

            DateTime Transd = Convert.ToDateTime(Request.QueryString["startdate"].ToString());


            foreach (DataRow dre in dts.Tables[0].Rows)
            {
                DataRow dr_final12 = dtt.NewRow();
                Transdt = Convert.ToDateTime(dre["BillDate"]);

                credit = 0.00;

                foreach (DataRow dr in dst.Tables[0].Rows)
                {
                    Transd = Convert.ToDateTime(dr["BillDate"]);
                    if (Transdt == Transd)
                    {
                        string aa = dr["BillDate"].ToString().ToUpper().Trim();
                        string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                        dr_final12["Date"] = dtaa;

                        string ledgernam = dr["Productname"].ToString().ToUpper().Trim();
                        for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                        {
                            string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                            if (ledgernam == ledgerna)
                            {
                                dr_final12[ledgerna] = double.Parse(dr["Quantity"].ToString());
                                credit = credit + double.Parse(dr["Quantity"].ToString());
                                Tottot = Tottot + double.Parse(dr["Quantity"].ToString());
                            }
                            else
                            {
                                if (dr_final12[ledgerna] == null)
                                {
                                    dr_final12[ledgerna] = "";
                                }
                            }
                        }
                        dr_final12["Total"] = credit;
                    }
                }
                dtt.Rows.Add(dr_final12);
            }

            DataRow dr_final11 = dtt.NewRow();
            dtt.Rows.Add(dr_final11);

            DataRow dr_final88 = dtt.NewRow();
            dr_final88["Date"] = "Total";
            dr_final88["Total"] = Tottot;
            dtt.Rows.Add(dr_final88);

           // ExportToExcel(dtt);

            Grdreport.Visible = true;
            Grdreport.DataSource = dtt;
            Grdreport.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }
    public void bindDataSubTotBrand(string cond)
    {
        DateTime startDate, endDate, Transdt;
         startDate = Convert.ToDateTime(Request.QueryString["startdate"].ToString());
         endDate = Convert.ToDateTime(Request.QueryString["enddate"].ToString());
        string condtion = "";
        condtion = getCond();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        //DataSet dsGird = GenerateGridColumns();
        DataSet dst = new DataSet();
        DataSet dts = new DataSet();
        DataSet dstt = new DataSet();
        DataSet dsGir = new DataSet();
        DataSet dsG = new DataSet();
        dst = objBL.gettotalbrandsales(startDate, endDate);

        dts = objBL.gettotalbrandsalesdate(startDate, endDate);

        dstt = objBL.ListAllBrands();

        if (dts.Tables[0].Rows.Count > 0)
        {
            DataTable dtt = new DataTable();
            DataColumn dc;
            if (dstt.Tables[0].Rows.Count > 0)
            {
                dc = new DataColumn("Date");
                dtt.Columns.Add(dc);
                for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                {
                    string ledger = dstt.Tables[0].Rows[i]["ProductDesc"].ToString();
                    dc = new DataColumn(ledger);
                    dtt.Columns.Add(dc);
                }
                dc = new DataColumn("Total");
                dtt.Columns.Add(dc);
            }
            dsGir.Tables.Add(dtt);


            DataRow dr_final14 = dtt.NewRow();
            dtt.Rows.Add(dr_final14);

            double credit = 0.00;
            double Tottot = 0.00;

            DateTime Transd = Convert.ToDateTime(Request.QueryString["startdate"].ToString());


            foreach (DataRow dre in dts.Tables[0].Rows)
            {
                DataRow dr_final12 = dtt.NewRow();
                Transdt = Convert.ToDateTime(dre["BillDate"]);

                credit = 0.00;

                foreach (DataRow dr in dst.Tables[0].Rows)
                {
                    Transd = Convert.ToDateTime(dr["BillDate"]);
                    if (Transdt == Transd)
                    {
                        string aa = dr["BillDate"].ToString().ToUpper().Trim();
                        string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                        dr_final12["Date"] = dtaa;

                        string ledgernam = dr["ProductDesc"].ToString();
                        for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                        {
                            string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                            if (ledgernam == ledgerna)
                            {
                                dr_final12[ledgerna] = double.Parse(dr["Quantity"].ToString());
                                credit = credit + double.Parse(dr["Quantity"].ToString());
                                Tottot = Tottot + double.Parse(dr["Quantity"].ToString());
                            }
                            else
                            {
                                if (dr_final12[ledgerna] == null)
                                {
                                    dr_final12[ledgerna] = "";
                                }
                            }
                        }
                        dr_final12["Total"] = credit;
                    }
                }
                dtt.Rows.Add(dr_final12);
            }

            DataRow dr_final11 = dtt.NewRow();
            dtt.Rows.Add(dr_final11);

            DataRow dr_final88 = dtt.NewRow();
            dr_final88["Date"] = "Total";
            dr_final88["Total"] = Tottot;
            dtt.Rows.Add(dr_final88);

            Grdreport.Visible = true;
            Grdreport.DataSource = dtt;
            Grdreport.DataBind();

           // ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataSubTotCat(string cond)
    {
        DateTime startDate, endDate, Transdt;
        startDate = Convert.ToDateTime(Request.QueryString["startdate"].ToString());
        endDate = Convert.ToDateTime(Request.QueryString["enddate"].ToString());
        string condtion = "";
        condtion = getCond();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        //DataSet dsGird = GenerateGridColumns();
        DataSet dst = new DataSet();
        DataSet dts = new DataSet();
        DataSet dstt = new DataSet();
        DataSet dsGir = new DataSet();
        DataSet dsG = new DataSet();
        dst = objBL.gettotalcatsales(startDate, endDate);

        dts = objBL.gettotalcatsalesdate(startDate, endDate);


        if (dts.Tables[0].Rows.Count > 0)
        {

            dstt = objBL.ListAllCategory();
            DataTable dtt = new DataTable();
            DataColumn dc;
            if (dstt.Tables[0].Rows.Count > 0)
            {
                dc = new DataColumn("Date");
                dtt.Columns.Add(dc);
                for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                {
                    string ledger = dstt.Tables[0].Rows[i]["Categoryname"].ToString();
                    dc = new DataColumn(ledger);
                    dtt.Columns.Add(dc);
                }
                dc = new DataColumn("Total");
                dtt.Columns.Add(dc);
            }
            dsGir.Tables.Add(dtt);


            DataRow dr_final14 = dtt.NewRow();
            dtt.Rows.Add(dr_final14);

            double credit = 0.00;
            double Tottot = 0.00;

            DateTime Transd = Convert.ToDateTime(Request.QueryString["startdate"].ToString());


            foreach (DataRow dre in dts.Tables[0].Rows)
            {
                DataRow dr_final12 = dtt.NewRow();
                Transdt = Convert.ToDateTime(dre["BillDate"]);

                credit = 0.00;

                foreach (DataRow dr in dst.Tables[0].Rows)
                {
                    Transd = Convert.ToDateTime(dr["BillDate"]);
                    if (Transdt == Transd)
                    {
                        string aa = dr["BillDate"].ToString().ToUpper().Trim();
                        string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                        dr_final12["Date"] = dtaa;

                        string ledgernam = dr["Categoryname"].ToString();
                        for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                        {
                            string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                            if (ledgernam == ledgerna)
                            {
                                dr_final12[ledgerna] = double.Parse(dr["Quantity"].ToString());
                                credit = credit + double.Parse(dr["Quantity"].ToString());
                                Tottot = Tottot + double.Parse(dr["Quantity"].ToString());
                            }
                            else
                            {
                                if (dr_final12[ledgerna] == null)
                                {
                                    dr_final12[ledgerna] = "";
                                }
                            }
                        }
                        dr_final12["Total"] = credit;
                    }
                }
                dtt.Rows.Add(dr_final12);
            }

            DataRow dr_final11 = dtt.NewRow();
            dtt.Rows.Add(dr_final11);

            DataRow dr_final88 = dtt.NewRow();
            dr_final88["Date"] = "Total";
            dr_final88["Total"] = Tottot;
            dtt.Rows.Add(dr_final88);

            Grdreport.Visible = true;
            Grdreport.DataSource = dtt;
            Grdreport.DataBind();

          //  ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataSubTotBranch(string cond)
    {
        DateTime startDate, endDate, Transdt;
        startDate = Convert.ToDateTime(Request.QueryString["startdate"].ToString());
        endDate = Convert.ToDateTime(Request.QueryString["enddate"].ToString());
        string condtion = "";
        condtion = getCond();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        //DataSet dsGird = GenerateGridColumns();
        DataSet dst = new DataSet();
        DataSet dts = new DataSet();
        DataSet dstt = new DataSet();
        DataSet dsGir = new DataSet();
        DataSet dsG = new DataSet();
        dst = objBL.gettotalBranchsales(startDate, endDate);

        dts = objBL.gettotalBranchsalesdate(startDate, endDate);


        if (dts.Tables[0].Rows.Count > 0)
        {

            dstt = objBL.ListBranch();
            DataTable dtt = new DataTable();
            DataColumn dc;
            if (dstt.Tables[0].Rows.Count > 0)
            {
                dc = new DataColumn("Date");
                dtt.Columns.Add(dc);
                for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                {
                    string ledger = dstt.Tables[0].Rows[i]["Branchcode"].ToString();
                    dc = new DataColumn(ledger);
                    dtt.Columns.Add(dc);
                }
                dc = new DataColumn("Total");
                dtt.Columns.Add(dc);
            }
            dsGir.Tables.Add(dtt);


            DataRow dr_final14 = dtt.NewRow();
            dtt.Rows.Add(dr_final14);

            double credit = 0.00;
            double Tottot = 0.00;

            DateTime Transd = Convert.ToDateTime(Request.QueryString["startdate"].ToString());


            foreach (DataRow dre in dts.Tables[0].Rows)
            {
                DataRow dr_final12 = dtt.NewRow();
                Transdt = Convert.ToDateTime(dre["BillDate"]);

                credit = 0.00;

                foreach (DataRow dr in dst.Tables[0].Rows)
                {
                    Transd = Convert.ToDateTime(dr["BillDate"]);
                    if (Transdt == Transd)
                    {
                        string aa = dr["BillDate"].ToString().ToUpper().Trim();
                        string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                        dr_final12["Date"] = dtaa;

                        string ledgernam = dr["Branchcode"].ToString().ToUpper().Trim();
                        for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                        {
                            string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                            if (ledgernam == ledgerna)
                            {
                                dr_final12[ledgerna] = double.Parse(dr["Quantity"].ToString());
                                credit = credit + double.Parse(dr["Quantity"].ToString());
                                Tottot = Tottot + double.Parse(dr["Quantity"].ToString());
                            }
                            else
                            {
                                if (dr_final12[ledgerna] == null)
                                {
                                    dr_final12[ledgerna] = "";
                                }
                            }
                        }
                        dr_final12["Total"] = credit;
                    }
                }
                dtt.Rows.Add(dr_final12);
            }

            DataRow dr_final11 = dtt.NewRow();
            dtt.Rows.Add(dr_final11);

            DataRow dr_final88 = dtt.NewRow();
            dr_final88["Date"] = "Total";
            dr_final88["Total"] = Tottot;
            dtt.Rows.Add(dr_final88);

            Grdreport.Visible = true;
            Grdreport.DataSource = dtt;
            Grdreport.DataBind();

           // ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
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
    string cond;
    string cond1;
    string cond2;
    string cond3;
    string cond4;
    string cond5;
    string cond6;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }



    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
}