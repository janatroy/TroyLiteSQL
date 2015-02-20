using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class ComplaintReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!IsPostBack)
            {
                //AccessDataSource1.DataFile = sDataSource; 
                if (Request.Cookies["Company"] != null)
                {
                    if (Request.Cookies["Company"] != null)
                        hdDataSource.Value = Request.Cookies["Company"].Value;
                    else
                        Response.Redirect("~/Login.aspx");

                    DataSet companyInfo = new DataSet();
                    BusinessLogic bl = new BusinessLogic(sDataSource);
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);
                    lblBillDate.Text = DateTime.Now.ToShortDateString();

                    txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                    txtEndDate.Text = DateTime.Now.ToShortDateString();

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
            BusinessLogic bl = new BusinessLogic();

            var customer = drpCustomer.SelectedValue;
            var assignedTo = drpAssignedTo.SelectedValue;

            DateTime sDate = DateTime.Parse(txtStartDate.Text);
            DateTime eDate = DateTime.Parse(txtEndDate.Text);

            var status = drpStatus.SelectedValue;
            var isBilled = drpBilled.SelectedValue;

            DataSet ds = bl.GenerateComplaints(sDataSource, customer, assignedTo, status, sDate, eDate, isBilled);
            gvComplaint.DataSource = ds;
            gvComplaint.DataBind();
            compPanel.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
