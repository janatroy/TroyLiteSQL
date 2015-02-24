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

public partial class PurchaseSummaryReport1 : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    double dSNetRate = 0;
    double dSVatRate = 0;

    double dSCSTRate = 0;
    double dSFrRate = 0;
    double dSLURate = 0;
    double dSGrandRate = 0;
    double dSDiscountRate = 0;

    double dSCDiscountRate = 0;
    double dSCNetRate = 0;
    double dSQty = 0;
    double dSCVatRate = 0;

    double dSCCSTRate = 0;
    double dSCFrRate = 0;
    double dSCLURate = 0;
    double dSCGrandRate = 0;
    int tempBillno = 0;
    string strBillno = string.Empty;
    int cnt = 0;

    BusinessLogic objBL;
    DateTime startDate, endDate;
    string empname = string.Empty;
    string area = string.Empty;
    string category = string.Empty;
    string actname = string.Empty;
    string nxtactname = string.Empty;
    string info3 = string.Empty;
    string info4 = string.Empty;
    string status = string.Empty;
    string condtion = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());


            if (!Page.IsPostBack)
            {
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                lblBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtEndDate.Text = dtaa;

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

                string connection = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connection = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("Login.aspx");



                DateTime stdt = Convert.ToDateTime(txtStartDate.Text);
                DateTime etdt = Convert.ToDateTime(txtEndDate.Text);

                if (Request.QueryString["startDate"] != null)
                {
                    stdt = Convert.ToDateTime(Request.QueryString["startDate"].ToString());
                }
                if (Request.QueryString["endDate"] != null)
                {
                    etdt = Convert.ToDateTime(Request.QueryString["endDate"].ToString());
                }
                if (Request.QueryString["status"] != null)
                {
                    status = Convert.ToString(Request.QueryString["status"].ToString());
                }
                if (Request.QueryString["empname"] != null)
                {
                    empname = Convert.ToString(Request.QueryString["empname"].ToString());
                }
                if (Request.QueryString["area"] != null)
                {
                    area = Convert.ToString(Request.QueryString["area"].ToString());
                }
                if (Request.QueryString["category"] != null)
                {
                    category = Convert.ToString(Request.QueryString["category"].ToString());
                }
                if (Request.QueryString["actname"] != null)
                {
                    actname = Convert.ToString(Request.QueryString["actname"].ToString());
                }
                if (Request.QueryString["nxtactname"] != null)
                {
                    nxtactname = Convert.ToString(Request.QueryString["nxtactname"].ToString());
                }
                if (Request.QueryString["info3"] != null)
                {
                    info3 = Convert.ToString(Request.QueryString["info3"].ToString());
                }
                if (Request.QueryString["info4"] != null)
                {
                    info4 = Convert.ToString(Request.QueryString["info4"].ToString());
                }

                startDate = Convert.ToDateTime(stdt);
                endDate = Convert.ToDateTime(etdt);

                string condtion = "";
                condtion = getCond();

                DataSet BillDs = new DataSet();

                BillDs = bl.GetLeadManagementListFilter(connection, condtion);
                
                gvMain.DataSource = BillDs;
                gvMain.DataBind();
                divPrint.Visible = true;
                divmain.Visible = true;
                lblErr.Text = "";

                div1.Visible = false;
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

        objBL.StartDate = Convert.ToString(startDate);

        objBL.StartDate = string.Format("{0:MM/dd/yyyy}", startDate.ToString());
        objBL.EndDate = Convert.ToString(endDate);
        objBL.EndDate = string.Format("{0:MM/dd/yyyy}", endDate.ToString());

        string aa = Convert.ToString(startDate);
        string dt = Convert.ToDateTime(aa).ToString("MM/dd/yyyy");

        string aaa = Convert.ToString(endDate);
        string dtt = Convert.ToDateTime(aaa).ToString("MM/dd/yyyy");

        cond = " Start_Date >= '" + dt + "' and Start_Date <= '" + dtt + "' ";

        if ((status != "Select Lead Status"))
        {
            cond += " and Doc_Status='" + status + "'";
        }
        if ((empname != "0"))
        {
            cond += " and Emp_Id=" + Convert.ToInt32(empname) + "";
        }
        if ((area != "0"))
        {
            cond += " and Area=" + Convert.ToInt32(area) + "";
        }
        if ((category != "0"))
        {
            cond += " and Category=" + Convert.ToInt32(category) + "";
        }
        if ((actname != "0"))
        {
            cond += " and Activity_Name_Id=" + Convert.ToInt32(actname) + "";
        }
        if ((nxtactname != "0"))
        {
            cond += " and Next_Activity_Id=" + Convert.ToInt32(nxtactname) + "";
        }
        if ((info3 != "0"))
        {
            cond += " and Information3=" + Convert.ToInt32(info3) + "";
        }
        if ((info4 != "0"))
        {
            cond += " and Information4=" + Convert.ToInt32(info4) + "";
        }
        return cond;
    }

    public string ProcessMyDataItem(object myValue)
    {
        string ss = Convert.ToString(myValue);
        if (ss == "01/01/2000")
        {
            return "";
        }

        return ss.ToString();
    }
}
