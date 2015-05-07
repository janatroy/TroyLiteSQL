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
using System.Net.NetworkInformation;
using System.Management;

public partial class ReportXLSal2 : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    BusinessLogic objBL;
    string cond;
    string cond1;

    DateTime stdt;
    DateTime etdt;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            //printPreview();
            if (!IsPostBack)
            {

                //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                //Label2.Text = nics[0].GetPhysicalAddress().ToString();


                //Label1.Text = GetMacAddress().ToString();

                //Label3.Text = GetMacAdd().ToString();

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                lblBillDate.Text = DateTime.Now.ToShortDateString();
                SalesPanel.Visible = false;



                //txtEndDate.Text = DateTime.Now.ToShortDateString();

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



                DateTime startDate;
                DateTime endDate;

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                SalesPanel.Visible = true;
                string option = string.Empty;



                if (Request.QueryString["startDate"] != null)
                    stdt = Convert.ToDateTime(Request.QueryString["startDate"].ToString());
                if (Request.QueryString["endDate"] != null)
                    etdt = Convert.ToDateTime(Request.QueryString["endDate"].ToString());
                if (Request.QueryString["option"] != null)
                    option = Request.QueryString["option"].ToString();


                cond = Request.QueryString["cond"].ToString();
                cond = Server.UrlDecode(cond);

                cond1 = Request.QueryString["cond1"].ToString();
                cond1 = Server.UrlDecode(cond1);

                startDate = Convert.ToDateTime(stdt);
                endDate = Convert.ToDateTime(etdt);
                lblStartDate.Text = startDate.ToString("dd/MM/yyyy");
                lblEndDate.Text = endDate.ToString("dd/MM/yyyy");

                bindData(cond1);

                gvSales.Visible = true;
                // SalesPanel.Visible = true;
                // div1.Visible = false;


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void bindData(string cond1)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;
        DataSet dst = new DataSet();

        string pret = "NO";
        string itrans = "NO";
        string denot = "NO";

        startDate = Convert.ToDateTime(Request.QueryString["startDate"].ToString());
        endDate = Convert.ToDateTime(Request.QueryString["endDate"].ToString());

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        dst = objBL.getmonthsales(startDate, endDate, pret, itrans, denot, cond1);

        DataTable dtt = new DataTable("Sales Turnover report");
        dtt.Columns.Add(new DataColumn("Month"));
        dtt.Columns.Add(new DataColumn("BillNo"));
        dtt.Columns.Add(new DataColumn("BranchCode"));    
        dtt.Columns.Add(new DataColumn("Amount"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        double credit = 0.00;
        double Tottot = 0.00;

        foreach (DataRow dr in dst.Tables[0].Rows)
        {
            DataRow dr_final12 = dtt.NewRow();
            string aa = dr["monthname"].ToString().ToUpper().Trim();
            if (aa == "1")
            {
                //if (CheckBox10.Checked == true)
                    dr_final12["Month"] = "January";
                //else
                //    dr_final12["Month"] = " ";
            }
            else if (aa == "2")
            {
                //if (CheckBox11.Checked == true)
                    dr_final12["Month"] = "February";
                //else
                //    dr_final12["Month"] = " ";
            }
            else if (aa == "3")
            {
                //if (CheckBox12.Checked == true)
                    dr_final12["Month"] = "March";
                //else
                //    dr_final12["Month"] = " ";
            }
            else if (aa == "4")
            {
                //if (CheckBox1.Checked == true)
                    dr_final12["Month"] = "April";
                //else
                //    dr_final12["Month"] = " ";
            }
            else if (aa == "5")
            {
                //if (CheckBox2.Checked == true)
                    dr_final12["Month"] = "May";
                //else
                //    dr_final12["Month"] = " ";
            }
            else if (aa == "6")
            {
                //if (CheckBox3.Checked == true)
                    dr_final12["Month"] = "June";
                //else
                //    dr_final12["Month"] = " ";
            }
            else if (aa == "7")
            {
                //if (CheckBox4.Checked == true)
                    dr_final12["Month"] = "July";
                //else
                //    dr_final12["Month"] = " ";
            }
            else if (aa == "8")
            {
                //if (CheckBox5.Checked == true)
                    dr_final12["Month"] = "August";
                //else
                //    dr_final12["Month"] = " ";
            }
            else if (aa == "9")
            {
                //if (CheckBox6.Checked == true)
                    dr_final12["Month"] = "September";
                //else
                //    dr_final12["Month"] = " ";
            }
            else if (aa == "10")
            {
                //if (CheckBox7.Checked == true)
                    dr_final12["Month"] = "October";
                //else
                //    dr_final12["Month"] = " ";
            }
            else if (aa == "11")
            {
                //if (CheckBox8.Checked == true)
                    dr_final12["Month"] = "November";
                //else
                //    dr_final12["Month"] = " ";
            }
            else if (aa == "12")
            {
                //if (CheckBox9.Checked == true)
                    dr_final12["Month"] = "December";
                //else
                //    dr_final12["Month"] = " ";
            }

            dr_final12["BillNo"] = dr["BillNo"].ToString();
            dr_final12["BranchCode"] = dr["BranchCode"].ToString();

            if (dr_final12["Month"] == " ")
            {
            }
            else
            {             
               // credit = double.Parse(dr["SalesDiscount"].ToString()) + double.Parse(dr["ActualVAT"].ToString()) + double.Parse(dr["ActualCST"].ToString()) + double.Parse(dr["Loading"].ToString()) + double.Parse(dr["SumFreight"].ToString());
                credit = double.Parse(dr["SalesDiscount"].ToString()) + double.Parse(dr["ActualCST"].ToString()) + double.Parse(dr["Loading"].ToString()) + double.Parse(dr["SumFreight"].ToString());
                dr_final12["Amount"] = credit.ToString("#0.00");
                Tottot = Tottot + credit;
                credit = 0.00;
                dtt.Rows.Add(dr_final12);
            }
        }

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final88 = dtt.NewRow();
        dr_final88["Month"] = "Total";
        dr_final88["Amount"] = Tottot.ToString("#0.00");
        dtt.Rows.Add(dr_final88);

        if (dst.Tables[0].Rows.Count > 0)
        {
            DataSet dss = new DataSet();
            dss.Tables.Add(dtt);
            div1.Visible = true;
            gvSales.DataSource = dss;
            gvSales.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

}