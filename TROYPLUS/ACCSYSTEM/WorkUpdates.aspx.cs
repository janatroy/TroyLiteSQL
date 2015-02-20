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

public partial class WorkUpdates : System.Web.UI.Page
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

                GrdWME.Columns[11].Visible = false;
                BindWME();
                loadEmp();
                DisableForOffline();
                rvASDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                rvASDate.MaximumValue = System.DateTime.Now.ToShortDateString();
                rvAEDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                rvAEDate.MaximumValue = System.DateTime.Now.ToShortDateString();


                GrdWME.PageSize = 8;


                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveAdd(usernam, "WRKUPD"))
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

    private void DisableForOffline()
    {
        string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
        dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

        BusinessLogic objChk = new BusinessLogic(sDataSource);

        if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
        {
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
            GrdWME.Columns[10].Visible = false;
            GrdWME.Columns[11].Visible = false;
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

        drpsIncharge.DataSource = ds;
        drpsIncharge.DataBind();
        drpsIncharge.DataTextField = "empFirstName";
        drpsIncharge.DataValueField = "empno";
    }

    protected void drpWorkStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpWorkStatus.SelectedValue == "Open")
            {
                lblheading.Text = "Comments";
            }
            else if (drpWorkStatus.SelectedValue == "On Process")
            {
                lblheading.Text = "Comments";
            }
            else if (drpWorkStatus.SelectedValue == "Resolved")
            {
                lblheading.Text = "Resolution Details";
            }
            UpdatePanel21.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindWME()
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        int WorkId = 0;
        int empNo = 0;
        string dExpStrdate = string.Empty;
        string dExpEnddate = string.Empty;
        string dCreatdate = string.Empty;
        string status = string.Empty;


        if (txtSworkId.Text.Trim() != string.Empty)
            WorkId = Convert.ToInt32(txtSworkId.Text.Trim());

        if (drpsIncharge.Text.Trim() != string.Empty)
            empNo = Convert.ToInt32(drpsIncharge.Text.Trim());


        if (txtStartDate.Text.Trim() != string.Empty)
            dExpStrdate = txtStartDate.Text.Trim();

        if (txtEndDate.Text.Trim() != string.Empty)
            dExpEnddate = txtEndDate.Text.Trim();

        if (txtsCreationDate.Text.Trim() != string.Empty)
            dCreatdate = txtsCreationDate.Text.Trim();

        if (drpsIncharge.Text.Trim() != string.Empty)
            status = drpsStatus.Text.Trim();



        DataSet ds = bl.SearchWME(WorkId, empNo, dExpStrdate, dExpEnddate, dCreatdate, status);
        GrdWME.DataSource = ds;
        GrdWME.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindWME();
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
            int WorkId = 0;
            string CreationDate = string.Empty;
            string EWStartDate = string.Empty;
            string EWEndDate = string.Empty;
            int empNO = 0;
            string Workdet = string.Empty;
            string ActStartDate = string.Empty;
            string ActEndDate = string.Empty;
            string ResDet = string.Empty;
            string status = string.Empty;

            if (Page.IsValid)
            {
                if (txtWorkID.Text.Trim() != string.Empty)
                    WorkId = Convert.ToInt32(txtWorkID.Text.Trim());
                if (txtCDate.Text.Trim() != string.Empty)
                    CreationDate = txtCDate.Text.Trim();
                if (txtEWstartDate.Text.Trim() != string.Empty)
                    EWStartDate = txtEWstartDate.Text.Trim();
                if (txtEWEndDate.Text.Trim() != string.Empty)
                    EWEndDate = txtEWEndDate.Text.Trim();
                if (drpIncharge.Text.Trim() != string.Empty)
                    empNO = Convert.ToInt32(drpIncharge.Text.Trim());
                if (txtWorkdet.Text.Trim() != string.Empty)
                    Workdet = txtWorkdet.Text.Trim();
                if (txtActStartDate.Text.Trim() != string.Empty)
                    ActStartDate = txtActStartDate.Text.Trim();
                if (txtActEnddate.Text.Trim() != string.Empty)
                    ActEndDate = txtActEnddate.Text.Trim();
                if (txtResDet.Text.Trim() != string.Empty)
                    ResDet = txtResDet.Text.Trim();
                if (drpWorkStatus.Text.Trim() != string.Empty)
                    status = drpWorkStatus.Text.Trim();

                DataSet checkemp = bl.SearchWME(WorkId, empNO, EWStartDate, EWEndDate, CreationDate, status);

                if (checkemp == null || checkemp.Tables[0].Rows.Count == 0)
                {
                    bl.InsertWMEDetails(WorkId, CreationDate, EWStartDate, EWEndDate, empNO, Workdet, ActStartDate, ActEndDate, ResDet, status);


                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Work Management Entry Details Saved Successfully Work ID " + WorkId + "');", true);
                    Reset();
                    ResetSearch();
                    BindWME();
                    //MyAccordion.Visible = true;
                    GrdWME.Visible = true;
                    tbMain.Visible = false;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Duplicate work Management Entry EmpNo " + empNO + " ');", true);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int WorkId = 0;
            string CreationDate = string.Empty;
            string EWStartDate = string.Empty;
            string EWEndDate = string.Empty;
            int empNO = 0;
            string Workdet = string.Empty;
            string ActStartDate = string.Empty;
            string ActEndDate = string.Empty;
            string ResDet = string.Empty;
            string status = string.Empty;

            if (Page.IsValid)
            {
                if (txtWorkID.Text.Trim() != string.Empty)
                    WorkId = Convert.ToInt32(txtWorkID.Text.Trim());
                if (txtCDate.Text.Trim() != string.Empty)
                    CreationDate = txtCDate.Text.Trim();
                if (txtEWstartDate.Text.Trim() != string.Empty)
                    EWStartDate = txtEWstartDate.Text.Trim();
                if (txtEWEndDate.Text.Trim() != string.Empty)
                    EWEndDate = txtEWEndDate.Text.Trim();
                if (drpIncharge.Text.Trim() != string.Empty)
                    empNO = Convert.ToInt32(drpIncharge.Text.Trim());
                if (txtWorkdet.Text.Trim() != string.Empty)
                    Workdet = txtWorkdet.Text.Trim();
                if (txtActStartDate.Text.Trim() != string.Empty)
                    ActStartDate = txtActStartDate.Text.Trim();
                if (txtActEnddate.Text.Trim() != string.Empty)
                    ActEndDate = txtActEnddate.Text.Trim();
                if (txtResDet.Text.Trim() != string.Empty)
                    ResDet = txtResDet.Text.Trim();
                if (drpWorkStatus.Text.Trim() != string.Empty)
                    status = drpWorkStatus.Text.Trim();


                bl.UpdateWMEDetails(WorkId, CreationDate, EWStartDate, EWEndDate, empNO, Workdet, ActStartDate, ActEndDate, ResDet, status);


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Work Updates  Details Updated Successfully WorkID " + WorkId + "');", true);
                Reset();
                ResetSearch();
                BindWME();
                //MyAccordion.Visible = true;
                GrdWME.Visible = true;
                tbMain.Visible = false;

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
            //MyAccordion.Visible = true;
            GrdWME.Visible = true;
            tbMain.Visible = false;
            Reset();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void ResetSearch()
    {
        drpsIncharge.SelectedIndex = 0;
        txtSworkId.Text = "";
        txtStartDate.Text = "";
        txtEndDate.Text = "";
        txtsCreationDate.Text = "";
        drpsStatus.SelectedIndex = 0;
    }
    public void Reset()
    {

        drpIncharge.SelectedIndex = 0;

        pnsSave.Visible = false;
        pnsWCE.Visible = false;
        pnsWMP2.Visible = false;
        //lnkBtnAdd.Visible = true;
        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        btnUpdate.Enabled = false;
        txtWorkID.Text = "";
        txtCDate.Text = "";
        txtEWstartDate.Text = "";
        txtEWEndDate.Text = "";
        txtWorkdet.Text = "";
        txtActStartDate.Text = "";
        txtActEnddate.Text = "";
        txtResDet.Text = "";
        drpWorkStatus.SelectedIndex = 0;


    }
    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrdWME.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindWME();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdWME_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdWME.PageIndex = e.NewPageIndex;
            BindWME();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdWME_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdWME, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdWME_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gridView = (GridView)sender;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "WRKUPD"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "WRKUPD"))
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

    protected void GrdWME_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            int WorkId = Convert.ToInt32(GrdWME.Rows[e.RowIndex].Cells[0].Text);

            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.DeleteWMEDetails(WorkId);
            BindWME();
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
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
            //MyAccordion.Visible = false;
            tbMain.Visible = true;
            GrdWME.Visible = false;
            txtCDate.Text = DateTime.Now.ToShortDateString();
            btnUpdate.Enabled = false;
            pnsSave.Visible = true;
            pnsWCE.Visible = true;
            pnsWMP2.Visible = true;
            //lnkBtnAdd.Visible = false;
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            pnsWMP2.Enabled = false;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            txtWorkID.Text = bl.getMaxWorkID().ToString();
            if (txtWorkID.Text.Trim() == string.Empty)
                txtWorkID.Text = "1";
            else
                txtWorkID.Text = Convert.ToString(Convert.ToInt32(txtWorkID.Text) + 1);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdWME_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            tbMain.Visible = true;

            GridViewRow row = GrdWME.SelectedRow;
            txtWorkID.Text = row.Cells[0].Text;
            txtCDate.Text = row.Cells[1].Text;
            txtEWstartDate.Text = row.Cells[2].Text;
            txtEWEndDate.Text = row.Cells[3].Text;
            drpIncharge.ClearSelection();
            ListItem li2 = drpIncharge.Items.FindByValue(row.Cells[4].Text);
            if (li2 != null) li2.Selected = true;

            if (row.Cells[5].Text.Trim() != "&nbsp;")
                txtWorkdet.Text = row.Cells[5].Text;
            else
                txtWorkdet.Text = "";

            if (row.Cells[6].Text.Trim() == "&nbsp;")
            {
                txtActStartDate.Text = "";
                txtActEnddate.Text = "";
            }
            else
            {
                txtActStartDate.Text = row.Cells[6].Text.Trim();
                txtActEnddate.Text = row.Cells[7].Text.Trim();
            }

            if (row.Cells[8].Text.Trim() != "&nbsp;")
                txtResDet.Text = row.Cells[8].Text.Trim();
            else
                txtResDet.Text = "";

            drpWorkStatus.ClearSelection();
            ListItem li3 = drpWorkStatus.Items.FindByValue(row.Cells[9].Text);
            if (li3 != null) li3.Selected = true;

            btnUpdate.Enabled = true;
            pnsSave.Visible = true;
            pnsWCE.Visible = true;
            pnsWMP2.Visible = true;
            //lnkBtnAdd.Visible = false;
            btnCancel.Enabled = true;
            btnSave.Enabled = false;
            pnsWMP2.Enabled = true;
            pnsWCE.Enabled = false;
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
