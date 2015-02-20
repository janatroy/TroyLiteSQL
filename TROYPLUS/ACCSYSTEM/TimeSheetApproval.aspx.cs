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

public partial class TimeSheetApproval : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                GrdTse.PageSize = 8;

                ResetSearch();
                BindTse();
                loadEmp();
                DisableForOffline();




                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveAdd(usernam, "TMAPP"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New item ";
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtSEmpno.Text = "";
            //ddCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void DisableForOffline()
    {
        string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        BusinessLogic objChk = new BusinessLogic(sDataSource);

        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        {
            btnSave.Enabled = false;
            GrdTse.Columns[3].Visible = false;
            GrdTse.Columns[4].Visible = false;
        }
    }

    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();

        ds = bl.ListExecutive();
        drpIncharge.DataSource = ds;
        drpIncharge.DataBind();
        drpIncharge.DataTextField = "empFirstName";
        drpIncharge.DataValueField = "empno";
    }
    private void BindTse()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        int empNO = 0;
        string dTSE = string.Empty;
        string sApproved = string.Empty;



        if (txtSEmpno.Text.Trim() != string.Empty)
            empNO = Convert.ToInt32(txtSEmpno.Text.Trim());


        if (txtSDate.Text.Trim() != string.Empty)
            dTSE = txtSDate.Text.Trim();

        if (drpsApproved.Text.Trim() != string.Empty)
            sApproved = drpsApproved.Text.Trim();

        DataSet ds = bl.SearchTSE(empNO, dTSE, sApproved);
        GrdTse.DataSource = ds;
        GrdTse.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindTse();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int empNO = 0;
            string TSEDate = string.Empty;
            string Before8 = string.Empty;
            string _8to9 = string.Empty;
            string _9to10 = string.Empty;
            string _10to11 = string.Empty;
            string _11to12 = string.Empty;
            string _12to1 = string.Empty;
            string _1pmto2 = string.Empty;
            string _2pmto3 = string.Empty;
            string _3pmto4 = string.Empty;
            string _4pmto5 = string.Empty;
            string _5pmto6 = string.Empty;
            string _6pmto7 = string.Empty;
            string _7pmto8 = string.Empty;
            string _8pmto9 = string.Empty;
            string _9pmto10 = string.Empty;
            string After10 = string.Empty;
            string _Approved = string.Empty;

            if (Page.IsValid)
            {
                if (drpIncharge.Text.Trim() != string.Empty)
                    empNO = Convert.ToInt32(drpIncharge.Text.Trim());
                if (txtDate.Text.Trim() != string.Empty)
                    TSEDate = txtDate.Text.Trim();
                if (txtBefore8.Text.Trim() != string.Empty)
                    Before8 = txtBefore8.Text.Trim();
                if (txt8to9.Text.Trim() != string.Empty)
                    _8to9 = txt8to9.Text.Trim();
                if (txt9to10.Text.Trim() != string.Empty)
                    _9to10 = txt9to10.Text.Trim();
                if (txt10to11.Text.Trim() != string.Empty)
                    _10to11 = txt10to11.Text.Trim();
                if (txt11to12.Text.Trim() != string.Empty)
                    _11to12 = txt11to12.Text.Trim();
                if (txt12to1.Text.Trim() != string.Empty)
                    _12to1 = txt12to1.Text.Trim();
                if (txtPM1to2.Text.Trim() != string.Empty)
                    _1pmto2 = txtPM1to2.Text.Trim();
                if (txtPM2to3.Text.Trim() != string.Empty)
                    _2pmto3 = txtPM2to3.Text.Trim();
                if (txtPM3to4.Text.Trim() != string.Empty)
                    _3pmto4 = txtPM3to4.Text.Trim();
                if (txtPM4to5.Text.Trim() != string.Empty)
                    _4pmto5 = txtPM4to5.Text.Trim();
                if (txtPM5to6.Text.Trim() != string.Empty)
                    _5pmto6 = txtPM5to6.Text.Trim();
                if (txtPM6to7.Text.Trim() != string.Empty)
                    _6pmto7 = txtPM6to7.Text.Trim();
                if (txtPM7to8.Text.Trim() != string.Empty)
                    _7pmto8 = txtPM7to8.Text.Trim();
                if (txtPM8to9.Text.Trim() != string.Empty)
                    _8pmto9 = txtPM8to9.Text.Trim();
                if (txtPM9to10.Text.Trim() != string.Empty)
                    _9pmto10 = txtPM9to10.Text.Trim();
                if (txtPMafter10.Text.Trim() != string.Empty)
                    After10 = txtPMafter10.Text.Trim();
                if (drpapproved.Text.Trim() != string.Empty)
                    _Approved = drpapproved.Text.Trim();



                DataSet checkemp = bl.SearchTSE(empNO, TSEDate, "");
                if (checkemp == null || checkemp.Tables[0].Rows.Count == 0)
                {
                    bl.InsertTSEDetails(empNO, TSEDate, Before8, _8to9, _9to10, _10to11, _11to12, _12to1, _1pmto2, _2pmto3, _3pmto4, _4pmto5, _5pmto6, _6pmto7, _7pmto8, _8pmto9, _9pmto10, After10, _Approved);


                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Time Sheet Entry Details Saved Successfully. Employee No " + empNO + " Date=" + TSEDate + "');", true);
                    Reset();
                    ResetSearch();
                    BindTse();
                    GrdTse.Visible = true;
                    tbMain.Visible = false;
                    ModalPopupExtender1.Hide();
                    UpdatePanelPage.Update();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Duplicate Time Sheet Entry EmpNo " + empNO + " and Date " + TSEDate + "');", true);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void ResetSearch()
    {
        txtSDate.Text = "";
        txtSEmpno.Text = "";
        drpsApproved.SelectedIndex = 1;
    }
    public void Reset()
    {

        drpIncharge.SelectedIndex = 0;
        txtDate.Text = "";
        txtBefore8.Text = "";
        txt8to9.Text = "";
        txt9to10.Text = "";
        txt10to11.Text = "";
        txt11to12.Text = "";
        txt12to1.Text = "";
        txtPM1to2.Text = "";
        txtPM2to3.Text = "";
        txtPM3to4.Text = "";
        txtPM4to5.Text = "";
        txtPM5to6.Text = "";
        txtPM6to7.Text = "";
        txtPM7to8.Text = "";
        txtPM8to9.Text = "";
        txtPM9to10.Text = "";
        txtPMafter10.Text = "";
        drpapproved.SelectedIndex = 0;
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        btnCancel.Enabled = true;
        //lnkBtnAdd.Visible = true;
        pnsTime.Visible = false;
        pnsTse.Visible = false;
        pnsApprov.Visible = false;
        pnsSave.Visible = false;

    }

    private void EnableControls()
    {

        txtDate.Enabled = true;
        txtBefore8.Enabled = true;
        txt8to9.Enabled = true;
        txt9to10.Enabled = true;
        txt10to11.Enabled = true;
        txt11to12.Enabled = true;
        txt12to1.Enabled = true;
        txtPM1to2.Enabled = true;
        txtPM2to3.Enabled = true;
        txtPM3to4.Enabled = true;
        txtPM4to5.Enabled = true;
        txtPM5to6.Enabled = true;
        txtPM6to7.Enabled = true;
        txtPM7to8.Enabled = true;
        txtPM8to9.Enabled = true;
        txtPM9to10.Enabled = true;
        txtPMafter10.Enabled = true;

    }

    private void DisableControls()
    {

        txtDate.Enabled = false;
        txtBefore8.Enabled = false;
        txt8to9.Enabled = false;
        txt9to10.Enabled = false;
        txt10to11.Enabled = false;
        txt11to12.Enabled = false;
        txt12to1.Enabled = false;
        txtPM1to2.Enabled = false;
        txtPM2to3.Enabled = false;
        txtPM3to4.Enabled = false;
        txtPM4to5.Enabled = false;
        txtPM5to6.Enabled = false;
        txtPM6to7.Enabled = false;
        txtPM7to8.Enabled = false;
        txtPM8to9.Enabled = false;
        txtPM9to10.Enabled = false;
        txtPMafter10.Enabled = false;

    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int empNO = 0;
            string TSEDate = string.Empty;
            string Before8 = string.Empty;
            string _8to9 = string.Empty;
            string _9to10 = string.Empty;
            string _10to11 = string.Empty;
            string _11to12 = string.Empty;
            string _12to1 = string.Empty;
            string _1pmto2 = string.Empty;
            string _2pmto3 = string.Empty;
            string _3pmto4 = string.Empty;
            string _4pmto5 = string.Empty;
            string _5pmto6 = string.Empty;
            string _6pmto7 = string.Empty;
            string _7pmto8 = string.Empty;
            string _8pmto9 = string.Empty;
            string _9pmto10 = string.Empty;
            string After10 = string.Empty;
            string _Approved = string.Empty;

            if (Page.IsValid)
            {
                if (drpIncharge.Text.Trim() != string.Empty)
                    empNO = Convert.ToInt32(drpIncharge.Text.Trim());
                if (txtDate.Text.Trim() != string.Empty)
                    TSEDate = txtDate.Text.Trim();
                if (txtBefore8.Text.Trim() != string.Empty)
                    Before8 = txtBefore8.Text.Trim();
                if (txt8to9.Text.Trim() != string.Empty)
                    _8to9 = txt8to9.Text.Trim();
                if (txt9to10.Text.Trim() != string.Empty)
                    _9to10 = txt9to10.Text.Trim();
                if (txt10to11.Text.Trim() != string.Empty)
                    _10to11 = txt10to11.Text.Trim();
                if (txt11to12.Text.Trim() != string.Empty)
                    _11to12 = txt11to12.Text.Trim();
                if (txt12to1.Text.Trim() != string.Empty)
                    _12to1 = txt12to1.Text.Trim();
                if (txtPM1to2.Text.Trim() != string.Empty)
                    _1pmto2 = txtPM1to2.Text.Trim();
                if (txtPM2to3.Text.Trim() != string.Empty)
                    _2pmto3 = txtPM2to3.Text.Trim();
                if (txtPM3to4.Text.Trim() != string.Empty)
                    _3pmto4 = txtPM3to4.Text.Trim();
                if (txtPM4to5.Text.Trim() != string.Empty)
                    _4pmto5 = txtPM4to5.Text.Trim();
                if (txtPM5to6.Text.Trim() != string.Empty)
                    _5pmto6 = txtPM5to6.Text.Trim();
                if (txtPM6to7.Text.Trim() != string.Empty)
                    _6pmto7 = txtPM6to7.Text.Trim();
                if (txtPM7to8.Text.Trim() != string.Empty)
                    _7pmto8 = txtPM7to8.Text.Trim();
                if (txtPM8to9.Text.Trim() != string.Empty)
                    _8pmto9 = txtPM8to9.Text.Trim();
                if (txtPM9to10.Text.Trim() != string.Empty)
                    _9pmto10 = txtPM9to10.Text.Trim();
                if (txtPMafter10.Text.Trim() != string.Empty)
                    After10 = txtPMafter10.Text.Trim();
                if (drpapproved.Text.Trim() != string.Empty)
                    _Approved = drpapproved.Text.Trim();
                //_Approved = Convert.ToInt32(drpapproved.Text.Trim());



                bl.UpdateTSEDetails(empNO, TSEDate, Before8, _8to9, _9to10, _10to11, _11to12, _12to1, _1pmto2, _2pmto3, _3pmto4, _4pmto5, _5pmto6, _6pmto7, _7pmto8, _8pmto9, _9pmto10, After10, _Approved);
                GrdTse.Visible = true;
                tbMain.Visible = false;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Time Sheet Entry Details Updated Successfully. Employee No " + empNO + " TSEDate=" + TSEDate + "');", true);
                Reset();
                ResetSearch();
                BindTse();
                //MyAccordion.Visible = true;
                ModalPopupExtender1.Hide();
                UpdatePanelPage.Update();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Reset();
            tbMain.Visible = false;
            pnsApprov.Visible = false;
            pnsSave.Visible = false;
            pnsTse.Visible = false;
            pnsTime.Visible = false;
            //lnkBtnAdd.Visible = true;
            GrdTse.Visible = true;
            //MyAccordion.Visible = true;
            ModalPopupExtender1.Hide();
            UpdatePanelPage.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            tbMain.Visible = true;
            pnsApprov.Visible = true;
            pnsApprov.Enabled = false;
            pnsSave.Visible = true;

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            txtDate.Text = dtaa;

            //lnkBtnAdd.Visible = false;
            btnCancel.Enabled = true;
            btnSave.Visible = true;
            btnSave.Enabled = true;
            btnUpdate.Visible = false;
            GrdTse.Visible = false;
            pnsTime.Visible = true;
            pnsTse.Visible = true;
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdTse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = GrdTse.SelectedRow;
            string TseDate = string.Empty;
            TseDate = GrdTse.Rows[GrdTse.SelectedIndex].Cells[0].Text;
            int empNo = Convert.ToInt32(GrdTse.Rows[GrdTse.SelectedIndex].Cells[1].Text);
            txtDate.Text = TseDate;
            drpIncharge.ClearSelection();
            ListItem li2 = drpIncharge.Items.FindByValue(row.Cells[1].Text);
            if (li2 != null) li2.Selected = true;

            //MyAccordion.Visible = false;

            drpapproved.ClearSelection();
            ListItem li3 = drpapproved.Items.FindByValue(row.Cells[2].Text);
            if (li3 != null) li3.Selected = true;

            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = bl.GetTSEDet(empNo, TseDate, drpapproved.Text.ToString());

            txtBefore8.Text = ds.Tables[0].Rows[0].ItemArray[2].ToString();
            txt8to9.Text = ds.Tables[0].Rows[0].ItemArray[3].ToString();
            txt9to10.Text = ds.Tables[0].Rows[0].ItemArray[4].ToString();
            txt10to11.Text = ds.Tables[0].Rows[0].ItemArray[5].ToString();
            txt11to12.Text = ds.Tables[0].Rows[0].ItemArray[6].ToString();
            txt12to1.Text = ds.Tables[0].Rows[0].ItemArray[7].ToString();
            txtPM1to2.Text = ds.Tables[0].Rows[0].ItemArray[8].ToString();
            txtPM2to3.Text = ds.Tables[0].Rows[0].ItemArray[9].ToString();
            txtPM3to4.Text = ds.Tables[0].Rows[0].ItemArray[10].ToString();
            txtPM4to5.Text = ds.Tables[0].Rows[0].ItemArray[11].ToString();
            txtPM5to6.Text = ds.Tables[0].Rows[0].ItemArray[12].ToString();
            txtPM6to7.Text = ds.Tables[0].Rows[0].ItemArray[13].ToString();
            txtPM7to8.Text = ds.Tables[0].Rows[0].ItemArray[14].ToString();
            txtPM8to9.Text = ds.Tables[0].Rows[0].ItemArray[15].ToString();
            txtPM9to10.Text = ds.Tables[0].Rows[0].ItemArray[16].ToString();
            txtPMafter10.Text = ds.Tables[0].Rows[0].ItemArray[17].ToString();

            BindTse();
            btnUpdate.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = true;

            pnsTse.Visible = true;
            //pnsTse.Enabled = false;
            DisableControls();
            pnsApprov.Visible = true;
            //pnsApprov.Enabled = true;
            drpIncharge.Enabled = false;
            pnsSave.Visible = true;
            pnsTime.Visible = true;
            //pnsTime.Enabled = false;
            //GrdTse.Visible = false;
            tbMain.Visible = true;
            btnUpdate.Enabled = true;
            btnUpdate.Visible = true;
            btnCancel.Enabled = true;
            btnSave.Enabled = false;
            btnSave.Visible = false;
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrdTse.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindTse();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdTse_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdTse.PageIndex = e.NewPageIndex;
            BindTse();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdTse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gridView = (GridView)sender;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "TMAPP"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "TMAPP"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void GrdTse_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdTse, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdTse_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            string TseDate = string.Empty;
            TseDate = GrdTse.Rows[e.RowIndex].Cells[0].Text;
            int empno = Convert.ToInt32(GrdTse.Rows[e.RowIndex].Cells[1].Text);

            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.DeleteTSEDetails(TseDate, empno);
            BindTse();
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
