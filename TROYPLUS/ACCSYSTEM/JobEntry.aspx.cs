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

public partial class JobEntry : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!IsPostBack)
            {
                //txtRefno.Focus();  
                //MyAccordion.Visible = true;
                //pnlDetails.Visible = false;


                gvJob.PageSize = 8;


                Reset();
                GetJobDetails("0");
                updatePnlPurchase.Update();
                ModalPopupJob.Hide();
                
                rvReturnDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                rvReturnDate.MaximumValue = System.DateTime.Now.ToShortDateString();
                rvAssignedDate.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                rvAssignedDate.MaximumValue = System.DateTime.Now.ToShortDateString();



                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "JOBENT"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New ";
                }

            }

            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            BusinessLogic objChk = new BusinessLogic();

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                lnkBtnAdd.Visible = false;
                gvJob.Columns[6].Visible = false;
                gvJob.Columns[7].Visible = false;
            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void GetJobDetails(string JobTitle)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListJobDetails(JobTitle);
        if (ds != null)
        {
            gvJob.DataSource = ds;
            gvJob.DataBind();
        }
        else
        {
            gvJob.DataSource = null;
            gvJob.DataBind();
        }
        
    }

    protected void cmdUpdateJob_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                pnlException.Visible = false;
                string strRef = string.Empty;
                string strJobTitle = string.Empty;
                string strJobDesc = string.Empty;
                int iAssgn = 0;
                double dQtyAss = 0;
                string strAssDate = string.Empty;
                string strExpRetDate = string.Empty;
                double dQtyRet = 0;

                strRef = txtRefno.Text.Trim();
                strJobTitle = txtJobTitle.Text.Trim();
                strJobDesc = txtDesc.Text.Trim();
                if (drpIncharge.SelectedIndex > 0)
                    iAssgn = Convert.ToInt32(drpIncharge.SelectedItem.Value);
                dQtyAss = Convert.ToDouble(txtAssQty.Text.Trim());
                strAssDate = txtAssignedDate.Text.Trim();
                strExpRetDate = txtExpRetDate.Text.Trim();
                dQtyRet = Convert.ToDouble(lblRetQty.Text);
                int jobID = Convert.ToInt32(hdJobID.Value);
                string isComp = "N";
                /* Insert into the Job Table */
                BusinessLogic bl = new BusinessLogic(sDataSource);
                int jobid = bl.UpdateJobDetails(strRef, strJobTitle, strJobDesc, iAssgn, strAssDate, strExpRetDate, dQtyAss, dQtyRet, jobID, isComp);

                ModalPopupJob.Hide();
                UpdatePnlMaster.Update();

                Reset();
                GetJobDetails("0");
                //pnlValidation.Visible = false ;
                //lnkBtnAdd.Visible = true;
                //pnlDetails.Visible = false;
                

                gvJob.Visible = true;
                tabs2.Visible = false;
                //TabPanel1.Visible = false;

                //MyAccordion.Visible = true;
                //gvJob.Visible = true;
            }
            else
            {
                //pnlValidation.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void cmdSaveJob_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                pnlException.Visible = false;
                string strRef = string.Empty;
                string strJobTitle = string.Empty;
                string strJobDesc = string.Empty;
                int iAssgn = 0;
                double dQtyAss = 0;
                string strAssDate = string.Empty;
                string strExpRetDate = string.Empty;
                double dQtyRet = 0;

                strRef = txtRefno.Text.Trim();
                strJobTitle = txtJobTitle.Text.Trim();
                strJobDesc = txtDesc.Text.Trim();
                if (drpIncharge.SelectedIndex > 0)
                    iAssgn = Convert.ToInt32(drpIncharge.SelectedItem.Value);
                dQtyAss = Convert.ToDouble(txtAssQty.Text.Trim());
                strAssDate = txtAssignedDate.Text.Trim();
                strExpRetDate = txtExpRetDate.Text.Trim();
                dQtyRet = Convert.ToDouble(lblRetQty.Text);
                string isComp = "N";
                /* Insert into the Job Table */
                BusinessLogic bl = new BusinessLogic(sDataSource);
                int jobid = bl.InsertJobDetails(strRef, strJobTitle, strJobDesc, iAssgn, strAssDate, strExpRetDate, dQtyAss, dQtyRet, isComp);
                Reset();
                
                //pnlValidation.Visible = false;
                //lnkBtnAdd.Visible = true;
                //pnlDetails.Visible = false;
                
                GetJobDetails("0");

                ModalPopupJob.Hide();
                gvJob.Visible = true;
                tabs2.Visible = false;
                UpdatePnlMaster.Update();

                //updatePnlPurchase.Update();
                //MyAccordion.Visible = true;
            }
            else
            {
                //pnlValidation.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void Reset()
    {
        txtRefno.Text = "";
        txtAssQty.Text = "";
        txtJobTitle.Text = "";
        txtDesc.Text = "";
        drpIncharge.SelectedIndex = 0;
        txtAssignedDate.Text = DateTime.Now.ToShortDateString();
        txtExpRetDate.Text = DateTime.Now.ToShortDateString();
        lblRetQty.Text = "0";
        //txtRefno.Focus();
        hdJobID.Value = "0";
        gvJobReturn.DataSource = null;
        gvJobReturn.DataBind();
        txtCRetDate.Text = "";
        txtCRetQty.Text = "";
        txtRemarks.Text = "";
        pnlException.Visible = false;
        cmdSaveJobReturn.Enabled = true;
        cmdSaveJob.Enabled = true;
        cmdUpdateJob.Enabled = false;
    }
    public void ResetReturn()
    {
        txtCRetDate.Text = "";
        txtCRetQty.Text = "";
        txtRemarks.Text = "";
    }

    protected void cmdReset_Click(object sender, EventArgs e)
    {
        try
        {
            pnlException.Visible = false;
            //cmdSaveJobReturn.Enabled = true;
            //cmdSaveJob.Enabled = true;
            //cmdUpdateJob.Enabled = false;
            Reset();
            ResetReturn();
            //MyAccordion.Visible = true;
            //pnlDetails.Visible = false;
            ModalPopupJob.Hide();
            //lnkBtnAdd.Visible = true;
            //gvJob.Visible = true;
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
            ModalPopupJob.Show();
            //pnlDetails.Visible = true;
            //lnkBtnAdd.Visible = false;
            cmdUpdateJob.Visible = false;
            cmdSaveJobReturn.Enabled = true;
            cmdSaveJob.Visible = true;
            cmdSaveJob.Enabled = true;
            //gvJob.Visible = false;
            txtRefno.Focus();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void cmdSaveJobReturn_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                pnlException.Visible = false;
                string strRemarks = string.Empty;
                string strRetDate = string.Empty;
                double dQtyRet = 0;
                int jobID = 0;
                int jobCompID = 0;
                double QtyAss = 0;
                strRemarks = txtRemarks.Text.Trim();
                strRetDate = txtCRetDate.Text.Trim();
                dQtyRet = Convert.ToDouble(txtCRetQty.Text);
                jobID = Convert.ToInt32(hdJobID.Value);
                if (txtAssQty.Text.Trim() != string.Empty)
                    QtyAss = Convert.ToDouble(txtAssQty.Text.Trim());
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (jobID > 0)
                {
                    jobCompID = bl.InsertJobReturnDetails(strRemarks, jobID, dQtyRet, strRetDate, QtyAss);
                    System.Threading.Thread.Sleep(1000);
                    if (jobCompID == -1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty. Returned Should not be greater than assigned');", true);
                        return;
                    }

                    DataSet ds = new DataSet();
                    ds = bl.ListJobReturn(jobID);
                    if (ds != null)
                    {
                        gvJobReturn.DataSource = ds;
                        gvJobReturn.DataBind();
                    }

                    double dRet = Convert.ToDouble(lblRetQty.Text);
                    dRet = dRet + dQtyRet;
                    lblRetQty.Text = dRet.ToString();
                    ResetReturn();
                    GetJobDetails("0");

                    gvJob.Visible = true;
                    tabs2.Visible = false;

                    //lnkBtnAdd.Visible = true;
                    //pnlDetails.Visible = false;
                    ModalPopupJob.Hide();
                    UpdatePnlMaster.Update();
                    //MyAccordion.Visible = true;
                    //gvJob.Visible = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty. Returned Successfully Saved');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select the job before you return the qty.');", true);
                }
                //pnlValidation.Visible = false;
            }
            else
            {
                //pnlValidation.Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvJob_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int jobID = (int)gvJob.DataKeys[e.RowIndex].Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            bl.DeleteJobDetails(jobID);
            GetJobDetails("0");
            Reset();
            pnlException.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvJobReturn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int jobCID = (int)gvJobReturn.DataKeys[e.RowIndex].Value;
            double retQty = Convert.ToDouble(gvJobReturn.Rows[e.RowIndex].Cells[0].Text);

            BusinessLogic bl = new BusinessLogic(sDataSource);

            int jobID = Convert.ToInt32(hdJobID.Value);

            bl.DeleteJobReturnDetails(jobCID, retQty, jobID);
            DataSet ds = new DataSet();

            ds = bl.ListJobReturn(jobID);
            if (ds != null)
            {
                gvJobReturn.DataSource = ds;
                gvJobReturn.DataBind();
            }
            else
            {
                gvJobReturn.DataSource = null;
                gvJobReturn.DataBind();
            }

            double dRet = Convert.ToDouble(lblRetQty.Text);
            dRet = dRet - retQty;
            if (dRet < 0)
                lblRetQty.Text = "0";
            else
                lblRetQty.Text = dRet.ToString();
            //GetJobDetails("0");
            pnlException.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvJob_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(gvJob, e.Row, this);
            }
            //errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvJob_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "JOBENT"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "JOBENT"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }
            }
            //errPanel.Visible = false;
            ErrMsg.Text = "";
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }



    protected void gvJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvJob.PageIndex = e.NewPageIndex;
            string strJobTitle = txtSJobTitle.Text.Trim();
            if (strJobTitle == "")
                strJobTitle = "0";

            GetJobDetails(strJobTitle);
            pnlException.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvJob_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //lnkBtnAdd.Visible = false;
            //pnlDetails.Visible = true;
            ModalPopupJob.Show();
            //MyAccordion.Visible = false;
            pnlException.Visible = false;
            //gvJob.Visible = false;
            DataKey key = gvJob.SelectedDataKey;
            int jobID = Convert.ToInt32(key.Value);
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = bl.ListJobDetails(jobID);
            string strCompleted = string.Empty;
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    txtRefno.Text = Convert.ToString(dr["Ref"]);
                    txtJobTitle.Text = Convert.ToString(dr["JobTitle"]);
                    txtDesc.Text = Convert.ToString(dr["JobDesc"]);
                    strCompleted = Convert.ToString(dr["IsCompleted"]);
                    //drpIncharge.SelectedItem.Value = Convert.ToString(dr["AssignedTo"]);
                    drpIncharge.DataBind();
                    drpIncharge.ClearSelection();
                    ListItem li = drpIncharge.Items.FindByValue(Convert.ToString(dr["AssignedTo"]).Trim());
                    if (li != null) li.Selected = true;

                    txtAssQty.Text = Convert.ToString(dr["Qty_Assigned"]);
                    txtAssignedDate.Text = Convert.ToDateTime(dr["AssignedDate"]).ToShortDateString();
                    txtExpRetDate.Text = Convert.ToDateTime(Convert.ToString(dr["ExpReturnDate"])).ToShortDateString();
                    lblRetQty.Text = Convert.ToString(dr["Qty_Returned"]);
                    hdJobID.Value = jobID.ToString();
                    if (strCompleted == "Y")
                    {
                        cmdSaveJob.Enabled = false;
                        cmdSaveJob.Visible = false;
                        cmdUpdateJob.Enabled = false;
                        cmdSaveJobReturn.Enabled = false;
                        gvJobReturn.Enabled = false;
                        gvJobReturn.Columns[3].Visible = false;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected Job is Completed you can just view the summary.');", true);
                    }
                    else
                    {
                        gvJobReturn.Enabled = true;
                        cmdSaveJobReturn.Enabled = true;
                        cmdSaveJob.Enabled = false;
                        cmdSaveJob.Visible = false;
                        cmdUpdateJob.Enabled = true;
                        cmdUpdateJob.Visible = true;
                    }
                    DataSet dsR = new DataSet();
                    dsR = bl.ListJobReturn(jobID);
                    if (dsR != null)
                    {
                        gvJobReturn.DataSource = dsR;
                        gvJobReturn.DataBind();
                    }
                    else
                    {
                        gvJobReturn.DataSource = null;
                        gvJobReturn.DataBind();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private DataTable GenerateJobDs(string qty, string returnDate, string remarks)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataRow dr;

        DataColumn dcQty = new DataColumn("Qty_Return");
        DataColumn dcRetDate = new DataColumn("ReturnedDate");
        DataColumn dcRemarks = new DataColumn("Remarks");

        dt.Columns.Add(dcQty);
        dt.Columns.Add(dcRetDate);
        dt.Columns.Add(dcRemarks);

        dr = dt.NewRow();

        dr["Qty_Return"] = qty;
        dr["ReturnedDate"] = returnDate;
        dr["Remarks"] = remarks;


        dt.Rows.Add(dr);

        //ds.Tables.Add(dt);
        //Session["roledata"] = ds;
        return dt;
    }
    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string strJobTitle = txtSJobTitle.Text.Trim();
            GetJobDetails(strJobTitle);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
