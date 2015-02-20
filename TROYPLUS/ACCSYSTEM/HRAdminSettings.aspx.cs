using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_AdminSettings : System.Web.UI.Page
{
    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("Login.aspx");

        if (!IsPostBack)
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);

            DataSet ds = new DataSet();

            ds = bl.GetAdminSettings();

            if (ds != null)
            {
                SettingsID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                txtHolidayCount.Text = ds.Tables[0].Rows[0]["Yearly_Holiday_Count"].ToString();
                txtPermissionHr.Text = ds.Tables[0].Rows[0]["Daily_Permission_Thresold"].ToString();
                txtNumPermission.Text = ds.Tables[0].Rows[0]["Monthly_Permission_Count"].ToString();
                txtCompOff.Text = ds.Tables[0].Rows[0]["Compoff_Validity_Period"].ToString();
                txtWorkDays.Text = ds.Tables[0].Rows[0]["Weekly_WorkDays_Count"].ToString();
                if (ds.Tables[0].Rows[0]["isSupervisorOverwritable"].ToString() == "True")
                {
                    chkSupervisor.Checked = true;
                }
                else
                {
                    chkSupervisor.Checked = false;
                }
            }
            else
            {
                txtHolidayCount.Text = null;
                txtPermissionHr.Text = null;
                txtNumPermission.Text = null;
                txtCompOff.Text = null;
                txtWorkDays.Text = null;
                chkSupervisor.Checked = false;
            }
        }
    }


    protected void btnSettingsSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("Login.aspx");

                int settingsId = 0;
                int strHolidayCount = 0;
                int strPermissionHr = 0;
                int strNumPermission = 0;
                int strCompOff = 0;
                int strWorkdaysWeek = 0;
                bool boolSupervisorOverwrite = false;

                settingsId = Convert.ToInt32(SettingsID.Value);
                strHolidayCount = Convert.ToInt32(txtHolidayCount.Text);
                strPermissionHr = Convert.ToInt32(txtPermissionHr.Text);
                strNumPermission = Convert.ToInt32(txtNumPermission.Text);
                strCompOff = Convert.ToInt32(txtCompOff.Text);
                strWorkdaysWeek = Convert.ToInt32(txtWorkDays.Text);
                boolSupervisorOverwrite = chkSupervisor.Checked;

                if (strWorkdaysWeek > 7)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Work days per week is not valid number');", true);
                    //Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                }
                else
                {
                    BusinessLogic bl = new BusinessLogic(sDataSource);

                    int affectedRows = bl.InsertAdminSettingsInfo(settingsId, strHolidayCount, strPermissionHr, strNumPermission, strCompOff, strWorkdaysWeek, boolSupervisorOverwrite);

                    if (affectedRows == 1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Settings Information Successfully Stored. Please Logout and Login again to refelect the Changes. Thank You.');", true);
                        //Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSettingsCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DashBoard.aspx");
    }
}