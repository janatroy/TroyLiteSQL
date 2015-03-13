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
using System.Data.OleDb;
using SMSLibrary;
using System.Data.SqlClient;

public partial class FormulaExecution : System.Web.UI.Page
{
    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "Showalert();", true);
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!Page.IsPostBack)
            {
                BindProductsGrid("", "", rdoIsPros.Checked);
                PanelTemplatesList.Visible = false;
                PanelTemplateGrids.Visible = false;
                PanelProductsList.Visible = true;
                // lblMsg.Visible = false;
                string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic();

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAdd.Visible = false;
                    //cmdUpdate.Enabled = false;
                    GridViewProducts.Columns[3].Visible = false;
                }


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "STKADJ"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New ";
                }

                GridViewProducts.PageSize = 8;
                GridViewTemplates.PageSize = 7;

                CheckSMSRequired();

                if (Session["SMSREQUIRED"] != null)
                {
                    if (Session["SMSREQUIRED"].ToString() == "NO")
                        hdSMSRequired.Value = "NO";
                    else
                        hdSMSRequired.Value = "YES";
                }
                else
                {
                    hdSMSRequired.Value = "NO";
                }

                if (Session["EMAILREQUIRED"] != null)
                {
                    if (Session["EMAILREQUIRED"].ToString() == "NO")
                        hdEmailRequired.Value = "NO";
                    else
                        hdEmailRequired.Value = "YES";
                }
                else
                {
                    hdEmailRequired.Value = "NO";
                }


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    private void CheckSMSRequired()
    {
        DataSet appSettings;
        string smsRequired = string.Empty;
        string emailRequired = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "SMSREQ")
                {
                    smsRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["SMSREQUIRED"] = smsRequired.Trim().ToUpper();
                }
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "EMAILREQ")
                {
                    emailRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["EMAILREQUIRED"] = emailRequired.Trim().ToUpper();
                }

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "OWNERMOB")
                {
                    Session["OWNERMOB"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }

            }
        }
        else
        {
            BusinessLogic bl = new BusinessLogic();
            DataSet ds = bl.GetAppSettings(Request.Cookies["Company"].Value);

            if (ds != null)
                Session["AppSettings"] = ds;

            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "SMSREQ")
                {
                    smsRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["SMSREQUIRED"] = smsRequired.Trim().ToUpper();
                }
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "EMAILREQ")
                {
                    emailRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["EMAILREQUIRED"] = emailRequired.Trim().ToUpper();
                }

                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "OWNERMOB")
                {
                    Session["OWNERMOB"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }

            }
        }
    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        txtEndDate.Text = "";
        txtStartDate.Text = "";
        rdoIsPros.Checked = false;
        BindProductsGrid("", "", rdoIsPros.Checked);

        //ddCriteria.SelectedIndex = 0;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindProductsGrid(txtStartDate.Text, txtEndDate.Text, rdoIsPros.Checked);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindProductsGrid(string startDate, string endDate, bool isProcessed)
    {
        DataSet ds = new DataSet();
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DateTime sDate = DateTime.Now;
        DateTime eDate = DateTime.Now;
        string branch = Request.Cookies["Branch"].Value;
        if (startDate != "")
            sDate = DateTime.Parse(startDate);
        else
            sDate = DateTime.Now.AddYears(-100);

        if (endDate != "")
            eDate = DateTime.Parse(endDate);
        else
            eDate = DateTime.Now.AddYears(100);

        ds = bl.listCompProducts(sDate, eDate, isProcessed,branch);


        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridViewProducts.DataSource = ds.Tables[0].DefaultView;
                GridViewProducts.DataBind();
            }
        }
        else
        {
            GridViewProducts.DataSource = null;
            GridViewProducts.DataBind();
        }
    }

    private void BindFormulasGrid(string strFormName)
    {

        DataSet ds = new DataSet();
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);

        ds = bl.GetFormulaForName(strFormName, "");


        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridViewTemplates.DataSource = ds.Tables[0].DefaultView;
                GridViewTemplates.DataBind();
                //PanelBill.Visible = true;
            }
        }
        else
        {
            GridViewTemplates.DataSource = null;
            GridViewTemplates.DataBind();
            //PanelBill.Visible = true;

        }

    }

    protected void grdIn_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(grdIn, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdOut_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(grdOut, e.Row, this);
            }
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
            GridViewTemplates.PageIndex = ((DropDownList)sender).SelectedIndex;
            String strBillno = string.Empty;
            string strTransNo = string.Empty;

            BindTemplatesGrid(string.Empty);
            ModalPopupExtender1.Show();
            PanelTemplatesList.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridViewTemplates_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridViewTemplates.PageIndex = e.NewPageIndex;
            BindTemplatesGrid(string.Empty);
            ModalPopupExtender1.Show();
            PanelTemplatesList.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GridViewTemplates_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadBranch();
            ModalPopupExtender1.Show();
            string Formula = GridViewTemplates.SelectedDataKey.Value.ToString();

            GridViewRow row = GridViewTemplates.SelectedRow;
            drpBranch.SelectedValue = row.Cells[2].Text;
            PanelCmd.Visible = true;
            //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            txtDate.Text = dtaa;

            txtComments.Text = "";
            PanelTemplateGrids.Visible = true;
            PanelTemplatesList.Visible = true;
            BindInGrid(Formula);
            BindOutGrid(Formula);
            //BindDetails(Formula);
            lblFormula.Text = Formula;
            cmdSave.Visible = true;
            cmdSave.Enabled = true;
            //MyAccordion.Visible = false;
            //cmdUpdate.Enabled = false;
            //cmdUpdate.Visible = false;
            GridViewTemplates.Visible = false;
            //  lblMsg.Text = "Please enter the Product Processing Details";
            // lblMsg.Visible = true;
            BtnDefnBack.Visible = false;
            //cmdRelease.Visible = false;
            rdComplete.Enabled = true;
            txtDate.Enabled = true;
            txtComments.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridViewProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                BusinessLogic objChk = new BusinessLogic();

                using (SqlConnection connection = new SqlConnection(objChk.CreateConnectionString(sDataSource)))
                {
                    SqlCommand command = new SqlCommand();
                    SqlTransaction transaction = null;
                    SqlDataAdapter adapter = null;

                    // Set the Connection to the new OleDbConnection.
                    command.Connection = connection;

                    try
                    {
                        connection.Open();

                        // Start a local transaction with ReadCommitted isolation level.
                        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                        // Assign transaction object for a pending local transaction.
                        command.Connection = connection;
                        command.Transaction = transaction;
                        string itemCode = string.Empty;
                        string date = txtDate.Text;
                        string formula = lblFormula.Text;
                        string dbQry = string.Empty;
                        string comments = txtComments.Text;
                        int CompID = int.Parse(GridViewProducts.DataKeys[e.RowIndex].Value.ToString());
                        GridViewRow row= GridViewProducts.SelectedRow;
                        string branch = row.Cells[3].Text;
                        string StockLimit = string.Empty;
                        string stockHold = string.Empty;
                        string stock = string.Empty;

                        if (hdStockHold.Value == "Y")
                            stockHold = "N";
                        else
                            stockHold = "Y";

                        command.CommandText = string.Format("Update tblCompProduct SET IsReleased='Y' Where CompID = {0}", CompID);
                        command.ExecuteNonQuery();

                        StockLimit = string.Empty;

                        DataSet ds = new DataSet();
                        BusinessLogic bl = new BusinessLogic(sDataSource);

                        ds = bl.GetProdOUTsForCompID(CompID);

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            stock = dr["Qty"].ToString();
                            itemCode = dr["ItemCode"].ToString();

                            if (stock.Trim() != "0")
                            {
                                command.CommandText = string.Format("UPDATE tblProductStock SET tblProductStock.Stock =  tblProductStock.Stock + {0} WHERE ItemCode='{1}' and BranchCode='{2}'", stock, itemCode, branch);
                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        BindProductsGrid(txtStartDate.Text, txtEndDate.Text, rdoIsPros.Checked);

                        string connection1 = Request.Cookies["Company"].Value;


                        string usernam = Request.Cookies["LoggedUserName"].Value;

                        string salestype = string.Empty;
                        int ScreenNo = 0;
                        string ScreenName = string.Empty;


                        salestype = "Formula Execution";
                        ScreenName = "Formula Execution";


                        bool mobile = false;
                        bool Email = false;
                        string emailsubject = string.Empty;

                        string emailcontent = string.Empty;
                        if (hdEmailRequired.Value == "YES")
                        {
                            var toAddress = "";
                            var toAdd = "";
                            Int32 ModeofContact = 0;
                            int ScreenType = 0;


                            DataSet dsdd = bl.GetDetailsForScreenNo(connection1, ScreenName, "");
                            if (dsdd != null)
                            {
                                if (dsdd.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dsdd.Tables[0].Rows)
                                    {
                                        ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                        mobile = Convert.ToBoolean(dr["mobile"]);
                                        Email = Convert.ToBoolean(dr["Email"]);
                                        emailsubject = Convert.ToString(dr["emailsubject"]);
                                        emailcontent = Convert.ToString(dr["emailcontent"]);

                                        if (ScreenType == 1)
                                        {

                                            toAddress = toAdd;

                                        }
                                        else
                                        {
                                            toAddress = dr["EmailId"].ToString();
                                        }
                                        if (Email == true)
                                        {

                                            string body = "\n";

                                            int index123 = emailcontent.IndexOf("@Branch");
                                            body = Request.Cookies["Company"].Value;
                                            if (index123 >= 0)
                                            {
                                                emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                            }

                                            int sno = 1;
                                            string prd = string.Empty;
                                            int index322 = emailcontent.IndexOf("@Product");
                                            foreach (DataRow drd in ds.Tables[0].Rows)
                                            {
                                                stock = drd["Qty"].ToString();
                                                itemCode = drd["ItemCode"].ToString();

                                                prd = prd + "\n";
                                                prd = prd + itemCode + "  " + stock;
                                                prd = prd + "\n";

                                            }
                                            if (index322 >= 0)
                                            {
                                                emailcontent = emailcontent.Remove(index322, 8).Insert(index322, prd);
                                            }

                                            int index312 = emailcontent.IndexOf("@User");
                                            body = usernam;
                                            if (index312 >= 0)
                                            {
                                                emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                            }

                                            int index2 = emailcontent.IndexOf("@FormulaName");
                                            body = formula;
                                            if (index2 >= 0)
                                            {
                                                emailcontent = emailcontent.Remove(index2, 12).Insert(index2, body);
                                            }

                                            string smtphostname = ConfigurationManager.AppSettings["SmtpHostName"].ToString();
                                            int smtpport = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPortNumber"]);
                                            var fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();

                                            string fromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();

                                            EmailLogic.SendEmail(smtphostname, smtpport, fromAddress, toAddress, emailsubject, emailcontent, fromPassword);

                                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email sent successfully')", true);

                                        }

                                    }
                                }
                            }
                        }

                        string conn = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                        UtilitySMS utilSMS = new UtilitySMS(conn);
                        string UserID = Page.User.Identity.Name;

                        string smscontent = string.Empty;
                        if (hdSMSRequired.Value == "YES")
                        {
                            var toAddress = "";
                            var toAdd = "";
                            Int32 ModeofContact = 0;
                            int ScreenType = 0;

                            DataSet dsdd = bl.GetDetailsForScreenNo(connection1, ScreenName, "");
                            if (dsdd != null)
                            {
                                if (dsdd.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dsdd.Tables[0].Rows)
                                    {
                                        ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                        mobile = Convert.ToBoolean(dr["mobile"]);
                                        smscontent = Convert.ToString(dr["smscontent"]);

                                        if (ScreenType == 1)
                                        {

                                            toAddress = toAdd;

                                        }
                                        else
                                        {
                                            toAddress = dr["mobile"].ToString();
                                        }
                                        if (mobile == true)
                                        {

                                            string body = "\n";

                                            int index123 = smscontent.IndexOf("@Branch");
                                            body = Request.Cookies["Company"].Value;
                                            if (index123 >= 0)
                                            {
                                                smscontent = smscontent.Remove(index123, 7).Insert(index123, body);
                                            }

                                            int sno = 1;
                                            string prd = string.Empty;
                                            int index322 = smscontent.IndexOf("@Product");
                                            foreach (DataRow drd in ds.Tables[0].Rows)
                                            {
                                                stock = drd["Qty"].ToString();
                                                itemCode = drd["ItemCode"].ToString();

                                                prd = prd + "\n";
                                                prd = prd + itemCode + "  " + stock;
                                                prd = prd + "\n";

                                            }
                                            if (index322 >= 0)
                                            {
                                                smscontent = smscontent.Remove(index322, 8).Insert(index322, prd);
                                            }

                                            int index312 = smscontent.IndexOf("@User");
                                            body = usernam;
                                            if (index312 >= 0)
                                            {
                                                smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                            }

                                            int index2 = smscontent.IndexOf("@FormulaName");
                                            body = formula;
                                            if (index2 >= 0)
                                            {
                                                smscontent = smscontent.Remove(index2, 12).Insert(index2, body);
                                            }

                                            if (Session["Provider"] != null)
                                            {
                                                utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), toAddress, smscontent, true, UserID);
                                            }


                                        }

                                    }
                                }
                            }
                        }



                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Released Successfully');", true);

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            // Attempt to roll back the transaction.
                            transaction.Rollback();
                            //errorDisplay.AddItem("Exception : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                        }
                        catch (Exception ext)
                        {
                            TroyLiteExceptionManager.HandleException(ex);
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

    protected void GridViewProducts_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender1.Show();
            loadBranch();
            int CompID = int.Parse(GridViewProducts.SelectedDataKey.Value.ToString());
            GridViewRow row = GridViewProducts.SelectedRow;
            drpBranch.SelectedValue = row.Cells[3].Text;
            GetExecutionDetails(CompID);
            PanelProductsList.Visible = false;
            PanelTemplateGrids.Visible = true;
            PanelTemplatesList.Visible = false;
            PanelCmd.Visible = true;
            cmdSave.Enabled = false;
            //cmdUpdate.Enabled = false;
            lnkBtnAdd.Visible = false;
            //MyAccordion.Visible = false;
            cmdSave.Visible = false;
            rdComplete.Enabled = false;
            txtDate.Enabled = false;
            txtComments.Enabled = false;


            //  .Visible = true;
            //cmdRelease.Visible = true;
            //cmdRelease.Enabled = !CheckIfCompReleased(CompID);
            /*if (cmdRelease.Enabled == false)
                cmdRelease.Visible = false;
            else
                cmdRelease.Visible = true;*/
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridViewTemplates_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GridViewTemplates, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridViewTemplates_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            PanelTemplatesList.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpBranch.Items.Clear();
        drpBranch.Items.Add(new ListItem("Select Branch", "0"));
        ds = bl.ListBranch();
        drpBranch.DataSource = ds;
        drpBranch.DataBind();
        drpBranch.DataTextField = "BranchName";
        drpBranch.DataValueField = "Branchcode";
    }

    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            PanelProductsList.Visible = false;
            PanelCmd.Visible = false;
            PanelTemplateGrids.Visible = false;
            BindTemplatesGrid(string.Empty);
            PanelTemplatesList.Visible = true;
            GridViewTemplates.Visible = true;
            lnkBtnAdd.Visible = false;
            //MyAccordion.Visible = false;
            // lblMsg.Visible = true;
            //  lblMsg.Text = "Please select one of the available Product Stocks.";
            BtnDefnBack.Visible = true;
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender1.Hide();
            PanelTemplatesList.Visible = false;
            PanelTemplateGrids.Visible = false;
            PanelProductsList.Visible = true;
            //  lblMsg.Text = string.Empty;
            lnkBtnAdd.Visible = true;
            //MyAccordion.Visible = true;
            //  lblMsg.Visible = false;
            BtnDefnBack.Visible = false;

            BusinessLogic objChk = new BusinessLogic();
            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                lnkBtnAdd.Visible = false;
                //cmdUpdate.Enabled = false;
                GridViewProducts.Columns[3].Visible = true;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdRelease_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                BusinessLogic objChk = new BusinessLogic();

                using (SqlConnection connection = new SqlConnection(objChk.CreateConnectionString(sDataSource)))
                {
                    SqlCommand command = new SqlCommand();
                    SqlTransaction transaction = null;
                    SqlDataAdapter adapter = null;

                    // Set the Connection to the new OleDbConnection.
                    command.Connection = connection;

                    try
                    {
                        connection.Open();

                        // Start a local transaction with ReadCommitted isolation level.
                        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                        // Assign transaction object for a pending local transaction.
                        command.Connection = connection;
                        command.Transaction = transaction;
                        string itemCode = string.Empty;
                        string date = txtDate.Text;
                        string formula = lblFormula.Text;
                        string dbQry = string.Empty;
                        string comments = txtComments.Text;
                        int CompID = int.Parse(GridViewProducts.SelectedDataKey.Value.ToString());
                        string StockLimit = string.Empty;
                        string stockHold = string.Empty;

                        if (hdStockHold.Value == "Y")
                            stockHold = "N";
                        else
                            stockHold = "Y";

                        command.CommandText = string.Format("Update tblCompProduct SET IsReleased='Y' Where CompID = {0}", CompID);
                        command.ExecuteNonQuery();

                        StockLimit = string.Empty;

                        foreach (GridViewRow gr in grdOut.Rows)
                        {
                            TextBox txtStock = (TextBox)gr.Cells[3].FindControl("txtQty");
                            itemCode = gr.Cells[0].Text;

                            if (txtStock.Text.Trim() != "0")
                            {
                                command.CommandText = string.Format("UPDATE tblProductMaster SET tblProductMaster.Stock =  tblProductMaster.Stock + {0} WHERE ItemCode='{1}'", txtStock.Text, itemCode);
                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        //BindProductsGrid(string.Empty);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Released Successfully');", true);

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            // Attempt to roll back the transaction.
                            transaction.Rollback();
                            //errorDisplay.AddItem("Exception : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                        }
                        catch (Exception ep)
                        {
                            // Do nothing here; transaction is not active.
                            //errorDisplay.AddItem("Exception : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
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



    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender1.Show();
            if (Page.IsValid)
            {
                BusinessLogic objChk = new BusinessLogic();

                using (SqlConnection connection = new SqlConnection(objChk.CreateConnectionString(sDataSource)))
                {
                    SqlCommand command = new SqlCommand();
                    SqlTransaction transaction = null;
                    SqlDataAdapter adapter = null;

                    // Set the Connection to the new OleDbConnection.
                    command.Connection = connection;

                    try
                    {
                        connection.Open();

                        // Start a local transaction with ReadCommitted isolation level.
                        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                        // Assign transaction object for a pending local transaction.
                        command.Connection = connection;
                        command.Transaction = transaction;
                        string itemCode = string.Empty;
                        DateTime date;
                        string formula = lblFormula.Text;
                        string inOut = "Raw Material";
                        string isAssembly = "Y";
                        string dbQry = string.Empty;
                        string comments = txtComments.Text;
                        string branch = drpBranch.SelectedValue;
                        int CompID = 0;
                        string StockLimit = string.Empty;
                        string stockHold = string.Empty;

                        date = Convert.ToDateTime(txtDate.Text.Trim().ToString());

                        if (rdComplete.SelectedValue == "N")
                            stockHold = "N";
                        else
                            stockHold = "Y";

                        command.CommandText = string.Format("Insert Into tblCompProduct(FormulaName,Comments,CDate,IsAssembly,IsReleased,BranchCode) Values('{0}','{1}','{2}','{3}','{4}','{5}')", formula, comments, date.ToString("yyyy-MM-dd"), isAssembly, stockHold, branch);
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Select Max(CompID) from tblCompProduct");
                        object compObj = command.ExecuteScalar();

                        if (compObj != null)
                        {
                            CompID = Convert.ToInt32(compObj.ToString());
                        }
                        else
                        {
                            return;
                        }

                        foreach (GridViewRow gr in grdIn.Rows)
                        {
                            TextBox txtStock = (TextBox)gr.Cells[3].FindControl("txtQty");
                            itemCode = gr.Cells[0].Text;
                            StockLimit = gr.Cells[4].Text;

                            if (int.Parse(txtStock.Text) <= 0)
                            {
                                transaction.Rollback();
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Quantity should be more than Zero.');", true);
                                Error.Text = "Product Quantity should be more than Zero.";
                                return;
                            }

                            if (double.Parse(StockLimit) < double.Parse(txtStock.Text))
                            {
                                transaction.Rollback();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Quantity you entered is more than the Product Quantity Limit.For Product ItemCode is " + itemCode + "');", true);
                                ///  Error.Text = "Stock you entered is more than the Product Quantity Limit. Product ItemCode is " + itemCode + "";
                                return;
                            }

                            if (txtStock.Text.Trim() != "0")
                            {
                                command.CommandText = string.Format("Insert Into tblExecution(CompID,FormulaName,ItemCode,Qty,InOut,BranchCode) Values({0},'{1}','{2}',{3},'{4}','{5}')", CompID, formula, itemCode, txtStock.Text, inOut,branch);
                                command.ExecuteNonQuery();
                                command.CommandText = string.Format("UPDATE tblProductStock SET tblProductStock.Stock =  tblProductStock.Stock - {0} WHERE ItemCode='{1}' and BranchCode='{2}'", txtStock.Text, itemCode, branch);
                                command.ExecuteNonQuery();
                            }
                        }

                        inOut = "Product";
                        StockLimit = string.Empty;

                        foreach (GridViewRow gr in grdOut.Rows)
                        {
                            TextBox txtStock = (TextBox)gr.Cells[3].FindControl("txtQty");
                            itemCode = gr.Cells[0].Text;
                            //StockLimit = gr.Cells[4].Text;

                            //if(double.Parse(StockLimit) < double.Parse(txtStock.Text))
                            //{
                            //    transaction.Rollback();
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock you entered is more than the Limit for ItemCode : "+ itemCode +"');", true);
                            //    return;
                            //}

                            if (txtStock.Text.Trim() != "0")
                            {
                                command.CommandText = string.Format("Insert Into tblExecution(CompID,FormulaName,ItemCode,Qty,InOut,BranchCode) Values({0},'{1}','{2}',{3},'{4}','{5}')", CompID, formula, itemCode, txtStock.Text, inOut, branch);
                                command.ExecuteNonQuery();

                                if (stockHold != "N")
                                {
                                    command.CommandText = string.Format("UPDATE tblProductStock SET tblProductStock.Stock =  tblProductStock.Stock + {0} WHERE ItemCode='{1}' and BranchCode='{2}'", txtStock.Text, itemCode, branch);
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                        PanelTemplatesList.Visible = false;
                        PanelTemplateGrids.Visible = false;
                        PanelCmd.Visible = false;
                        PanelProductsList.Visible = true;
                        //errorDisplay.AddItem("Added Sucesssfully.", DisplayIcons.GreenTick, false);
                        BindProductsGrid(string.Empty, string.Empty, rdoIsPros.Checked);
                        lnkBtnAdd.Visible = true;
                        // lblMsg.Text = string.Empty;
                        // lblMsg.Visible = false;
                        //MyAccordion.Visible = true;
                        ModalPopupExtender1.Hide();

                        if (stockHold == "N")
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Processed Successfully. However the Product is not Released.');", true);
                        else
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Processed and Released Successfully');", true);


                        BusinessLogic bl = new BusinessLogic(sDataSource);
                        string connection1 = Request.Cookies["Company"].Value;

                        if (stockHold == "Y")
                        {
                            string usernam = Request.Cookies["LoggedUserName"].Value;

                            string salestype = string.Empty;
                            int ScreenNo = 0;
                            string ScreenName = string.Empty;


                            salestype = "Formula Execution";
                            ScreenName = "Formula Execution";


                            bool mobile = false;
                            bool Email = false;
                            string emailsubject = string.Empty;

                            string emailcontent = string.Empty;
                            if (hdEmailRequired.Value == "YES")
                            {
                                var toAddress = "";
                                var toAdd = "";
                                Int32 ModeofContact = 0;
                                int ScreenType = 0;


                                DataSet dsdd = bl.GetDetailsForScreenNo(connection1, ScreenName, "");
                                if (dsdd != null)
                                {
                                    if (dsdd.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in dsdd.Tables[0].Rows)
                                        {
                                            ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                            mobile = Convert.ToBoolean(dr["mobile"]);
                                            Email = Convert.ToBoolean(dr["Email"]);
                                            emailsubject = Convert.ToString(dr["emailsubject"]);
                                            emailcontent = Convert.ToString(dr["emailcontent"]);

                                            if (ScreenType == 1)
                                            {

                                                toAddress = toAdd;

                                            }
                                            else
                                            {
                                                toAddress = dr["EmailId"].ToString();
                                            }
                                            if (Email == true)
                                            {

                                                string body = "\n";

                                                int index123 = emailcontent.IndexOf("@Branch");
                                                body = Request.Cookies["Company"].Value;
                                                if (index123 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index123, 7).Insert(index123, body);
                                                }

                                                int sno = 1;
                                                string prd = string.Empty;
                                                int index322 = emailcontent.IndexOf("@Product");
                                                foreach (GridViewRow gr in grdIn.Rows)
                                                {
                                                    TextBox txtStock = (TextBox)gr.Cells[3].FindControl("txtQty");
                                                    itemCode = gr.Cells[0].Text;

                                                    prd = prd + "\n";
                                                    prd = prd + itemCode + "  " + txtStock.Text;
                                                    prd = prd + "\n";


                                                }

                                                foreach (GridViewRow gr in grdOut.Rows)
                                                {
                                                    TextBox txtStock = (TextBox)gr.Cells[3].FindControl("txtQty");
                                                    itemCode = gr.Cells[0].Text;

                                                    prd = prd + "\n";
                                                    prd = prd + itemCode + "  " + txtStock.Text;
                                                    prd = prd + "\n";


                                                }
                                                if (index322 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index322, 8).Insert(index322, prd);
                                                }

                                                int index312 = emailcontent.IndexOf("@User");
                                                body = usernam;
                                                if (index312 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index312, 5).Insert(index312, body);
                                                }

                                                int index2 = emailcontent.IndexOf("@FormulaName");
                                                body = formula;
                                                if (index2 >= 0)
                                                {
                                                    emailcontent = emailcontent.Remove(index2, 12).Insert(index2, body);
                                                }

                                                string smtphostname = ConfigurationManager.AppSettings["SmtpHostName"].ToString();
                                                int smtpport = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPortNumber"]);
                                                var fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();

                                                string fromPassword = ConfigurationManager.AppSettings["FromPassword"].ToString();

                                                EmailLogic.SendEmail(smtphostname, smtpport, fromAddress, toAddress, emailsubject, emailcontent, fromPassword);

                                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email sent successfully')", true);

                                            }

                                        }
                                    }
                                }
                            }

                            string conn = bl.CreateConnectionString(Request.Cookies["Company"].Value);
                            UtilitySMS utilSMS = new UtilitySMS(conn);
                            string UserID = Page.User.Identity.Name;

                            string smscontent = string.Empty;
                            if (hdSMSRequired.Value == "YES")
                            {
                                var toAddress = "";
                                var toAdd = "";
                                Int32 ModeofContact = 0;
                                int ScreenType = 0;

                                DataSet dsdd = bl.GetDetailsForScreenNo(connection1, ScreenName, "");
                                if (dsdd != null)
                                {
                                    if (dsdd.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow dr in dsdd.Tables[0].Rows)
                                        {
                                            ScreenType = Convert.ToInt32(dr["ScreenType"]);
                                            mobile = Convert.ToBoolean(dr["mobile"]);
                                            smscontent = Convert.ToString(dr["smscontent"]);

                                            if (ScreenType == 1)
                                            {

                                                toAddress = toAdd;

                                            }
                                            else
                                            {
                                                toAddress = dr["mobile"].ToString();
                                            }
                                            if (mobile == true)
                                            {

                                                string body = "\n";

                                                int index123 = smscontent.IndexOf("@Branch");
                                                body = Request.Cookies["Company"].Value;
                                                if (index123 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index123, 7).Insert(index123, body);
                                                }

                                                int sno = 1;
                                                string prd = string.Empty;
                                                int index322 = smscontent.IndexOf("@Product");
                                                foreach (GridViewRow gr in grdIn.Rows)
                                                {
                                                    TextBox txtStock = (TextBox)gr.Cells[3].FindControl("txtQty");
                                                    itemCode = gr.Cells[0].Text;

                                                    prd = prd + "\n";
                                                    prd = prd + itemCode + "  " + txtStock.Text;
                                                    prd = prd + "\n";


                                                }

                                                foreach (GridViewRow gr in grdOut.Rows)
                                                {
                                                    TextBox txtStock = (TextBox)gr.Cells[3].FindControl("txtQty");
                                                    itemCode = gr.Cells[0].Text;

                                                    prd = prd + "\n";
                                                    prd = prd + itemCode + "  " + txtStock.Text;
                                                    prd = prd + "\n";


                                                }
                                                if (index322 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index322, 8).Insert(index322, prd);
                                                }

                                                int index312 = smscontent.IndexOf("@User");
                                                body = usernam;
                                                if (index312 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index312, 5).Insert(index312, body);
                                                }

                                                int index2 = smscontent.IndexOf("@FormulaName");
                                                body = formula;
                                                if (index2 >= 0)
                                                {
                                                    smscontent = smscontent.Remove(index2, 12).Insert(index2, body);
                                                }

                                                if (Session["Provider"] != null)
                                                {
                                                    utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), toAddress, smscontent, true, UserID);
                                                }


                                            }

                                        }
                                    }
                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            // Attempt to roll back the transaction.
                            transaction.Rollback();
                            //errorDisplay.AddItem("Exception : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                        }
                        catch (Exception ep)
                        {
                            // Do nothing here; transaction is not active.
                            //errorDisplay.AddItem("Exception : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
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


    protected void cmdUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                BusinessLogic objChk = new BusinessLogic();

                using (SqlConnection connection = new SqlConnection(objChk.CreateConnectionString(sDataSource)))
                {
                    SqlCommand command = new SqlCommand();
                    SqlTransaction transaction = null;
                    SqlDataAdapter adapter = null;

                    // Set the Connection to the new OleDbConnection.
                    command.Connection = connection;

                    try
                    {
                        connection.Open();

                        // Start a local transaction with ReadCommitted isolation level.
                        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                        // Assign transaction object for a pending local transaction.
                        command.Connection = connection;
                        command.Transaction = transaction;
                        string itemCode = string.Empty;
                        string date = txtDate.Text;
                        string CompID = GridViewProducts.SelectedDataKey.Value.ToString();
                        string inOut = "Raw Material";
                        string isAssembly = "Y";
                        string dbQry = string.Empty;
                        string comments = txtComments.Text;
                        //int CompID = 0;
                        string StockLimit = string.Empty;

                        command.CommandText = string.Format("Update tblCompProduct SET Comments = '{0}', IsAssembly = '{1}' Where CompID = {2}", comments, isAssembly, CompID);
                        command.ExecuteNonQuery();

                        foreach (GridViewRow gr in grdIn.Rows)
                        {
                            TextBox txtStock = (TextBox)gr.Cells[3].FindControl("txtQty");
                            HiddenField hdID = (HiddenField)gr.Cells[3].FindControl("lblID");
                            itemCode = gr.Cells[0].Text;
                            StockLimit = gr.Cells[4].Text;

                            if (double.Parse(StockLimit) < double.Parse(txtStock.Text))
                            {
                                transaction.Rollback();
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Quantity you entered is more than the Limit for ItemCode : " + itemCode + "');", true);
                                return;
                            }

                            command.CommandText = string.Format("Select Stock From tblProductMaster WHERE ItemCode='{0}'", itemCode);
                            double ProdStock = double.Parse(command.ExecuteScalar().ToString());

                            command.CommandText = string.Format("Select Qty From tblExecution WHERE ID={0}", hdID.Value);
                            double OldQty = double.Parse(command.ExecuteScalar().ToString());

                            double newStock = (ProdStock + OldQty) - double.Parse(txtStock.Text);

                            if (txtStock.Text.Trim() != "")
                            {
                                command.CommandText = string.Format("UPDATE tblExecution SET Qty={0} Where ID={1}", double.Parse(txtStock.Text), hdID.Value);
                                command.ExecuteNonQuery();

                                command.CommandText = string.Format("UPDATE tblProductMaster SET tblProductMaster.Stock =  {0} WHERE ItemCode='{1}'", newStock, itemCode);
                                command.ExecuteNonQuery();
                            }

                        }

                        inOut = "Product";
                        StockLimit = string.Empty;

                        foreach (GridViewRow gr in grdOut.Rows)
                        {
                            TextBox txtStock = (TextBox)gr.Cells[3].FindControl("txtQty");
                            HiddenField hdID = (HiddenField)gr.Cells[3].FindControl("lblOut");
                            itemCode = gr.Cells[0].Text;

                            //StockLimit = gr.Cells[4].Text;

                            //if(double.Parse(StockLimit) < double.Parse(txtStock.Text))
                            //{
                            //    transaction.Rollback();
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock you entered is more than the Limit for ItemCode : "+ itemCode +"');", true);
                            //    return;
                            //}

                            command.CommandText = string.Format("Select Stock From tblProductMaster WHERE ItemCode='{0}'", itemCode);
                            double ProdStock = double.Parse(command.ExecuteScalar().ToString());

                            command.CommandText = string.Format("Select Qty From tblExecution WHERE ID={0}", hdID.Value);
                            double OldQty = double.Parse(command.ExecuteScalar().ToString());

                            double newStock = (ProdStock - OldQty) + double.Parse(txtStock.Text);

                            if (txtStock.Text.Trim() != "")
                            {
                                command.CommandText = string.Format("UPDATE tblExecution SET Qty={0} Where ID={1}", double.Parse(txtStock.Text), hdID.Value);
                                command.ExecuteNonQuery();

                                command.CommandText = string.Format("UPDATE tblProductMaster SET tblProductMaster.Stock =  {0} WHERE ItemCode='{1}'", newStock, itemCode);
                                command.ExecuteNonQuery();
                            }
                        }

                        PanelTemplatesList.Visible = false;
                        PanelTemplateGrids.Visible = false;
                        PanelCmd.Visible = false;
                        PanelProductsList.Visible = true;
                        //MyAccordion.Visible = true;
                        transaction.Commit();
                        ModalPopupExtender1.Hide();
                        //errorDisplay.AddItem("Updated Sucesssfully.", DisplayIcons.GreenTick, false);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Updated Successfully.');", true);

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            // Attempt to roll back the transaction.
                            transaction.Rollback();
                            //errorDisplay.AddItem("Exception : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                        }
                        catch (Exception ep)
                        {
                            // Do nothing here; transaction is not active.
                            //errorDisplay.AddItem("Exception : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
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


    private void BindDetails(string strFormula)
    {
        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        ds = bl.GetFromulaDetails(strFormula);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblFormula.Text = ds.Tables[0].Rows[0]["FormulaName"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                //ddType.SelectedValue = ds.Tables[0].Rows[0]["IsAssembly"].ToString();
            }
        }
    }

    private void BindInGrid(string strFormula)
    {
        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        ds = bl.GetINsForFromula(strFormula,drpBranch.SelectedValue);


        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdIn.DataSource = ds.Tables[0].DefaultView;
                grdIn.DataBind();
            }
        }
        else
        {
            grdIn.DataSource = null;
            grdIn.DataBind();

        }
    }

    private void BindTemplatesGrid(string strFormula)
    {
        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string Branch = Request.Cookies["Branch"].Value;
        DataSet dstt = bl.GetFormulaForName(strFormula, Branch);

        if (dstt != null)
        {
            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();



            dct = new DataColumn("Row");
            dttt.Columns.Add(dct);

            dct = new DataColumn("FormulaName");
            dttt.Columns.Add(dct);

            dct = new DataColumn("BranchCode");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            int sno = 1;

            if (ds != null)
            {
                if (dstt.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                    {
                        drNew = dttt.NewRow();
                        drNew["Row"] = sno;
                        drNew["FormulaName"] = Convert.ToString(dstt.Tables[0].Rows[i]["FormulaName"]);
                        drNew["BranchCode"] = Convert.ToString(dstt.Tables[0].Rows[i]["BranchCode"]);
                        dstd.Tables[0].Rows.Add(drNew);
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    drNew = dttt.NewRow();
                        //    drNew["Row"] = sno;
                        sno = sno + 1;
                        //}
                    }
                }

                GridViewTemplates.DataSource = dstd.Tables[0].DefaultView;
                GridViewTemplates.DataBind();
            }
        }
        else
        {
            grdIn.DataSource = null;
            grdIn.DataBind();

        }
    }


    private void BindOutGrid(string strFormula)
    {
        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic(sDataSource);

        ds = bl.GetOUTsForFromula(strFormula,drpBranch.SelectedValue);


        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdOut.DataSource = ds.Tables[0].DefaultView;
                grdOut.DataBind();
            }
        }
        else
        {
            grdOut.DataSource = null;
            grdOut.DataBind();

        }
    }

    private bool CheckIfCompReleased(int CompID)
    {
        BusinessLogic objBus = new BusinessLogic(sDataSource);
        return objBus.CheckIfCompReleased(CompID);
    }

    private void GetExecutionDetails(int CompID)
    {
        BusinessLogic objBus = new BusinessLogic(sDataSource);
        using (SqlConnection connection = new SqlConnection(objBus.CreateConnectionString(sDataSource)))
        {
            SqlCommand command = new SqlCommand();
            SqlTransaction transaction = null;
            SqlDataAdapter adapter = null;

            // Set the Connection to the new OleDbConnection.
            command.Connection = connection;

            try
            {
                connection.Open();

                // Start a local transaction with ReadCommitted isolation level.
                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                // Assign transaction object for a pending local transaction.
                command.Connection = connection;

                DataSet dsIn = new DataSet();
                DataSet dsOut = new DataSet();
                DataSet dsDetails = new DataSet();
                command.Transaction = transaction;

                //command.CommandText = string.Format("SELECT E.ID,E.CompID,E.ItemCode,P.ProductDesc,P.ProductName,P.Stock,E.Qty FROM tblExecution E Inner Join tblProductMaster P ON P.ItemCode = E.ItemCode Where E.InOut='Raw Material' and E.CompID = {0}", CompID);
                command.CommandText=string.Format("SELECT E.ID,E.CompID,E.ItemCode,P.ProductDesc,P.ProductName,P.Stock,E.Qty,E.BranchCode " +
                                                  " FROM tblExecution E Inner Join tblProductStock P ON P.ItemCode = E.ItemCode and " +
                                                  " E.BranchCode = P.BranchCode" +
                                                  " Where E.InOut='Raw Material' and E.BranchCode='" + drpBranch.SelectedValue + "' and E.CompID = {0}", CompID);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dsIn);
                grdIn.DataSource = dsIn;
                grdIn.DataBind();

                //command.CommandText = string.Format("SELECT E.ID,E.CompID,E.ItemCode,P.ProductDesc,P.ProductName,P.Stock,E.Qty FROM tblExecution E Inner Join tblProductMaster P ON P.ItemCode = E.ItemCode Where E.InOut='Product' and E.CompID = {0}", CompID);
                command.CommandText=string.Format(" SELECT E.ID,E.CompID,E.ItemCode,P.ProductDesc,P.ProductName,P.Stock,E.Qty,E.BranchCode " +
                                                  " FROM tblExecution E Inner Join tblProductStock P ON P.ItemCode = E.ItemCode and " +
                                                  " E.BranchCode = P.BranchCode " +
                                                   " Where E.InOut='Product' and E.BranchCode='" + drpBranch.SelectedValue + "' and E.CompID = {0}", CompID);                                                
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dsOut);
                grdOut.DataSource = dsOut;
                grdOut.DataBind();

                command.CommandText = string.Format("SELECT CompID,FormulaName,Comments,CDate,IsAssembly FROM tblCompProduct Where CompID = {0}", CompID);
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dsDetails);

                if (dsDetails != null)
                {
                    if (dsDetails.Tables[0].Rows.Count == 1)
                    {
                        lblFormula.Text = dsDetails.Tables[0].Rows[0]["FormulaName"].ToString();
                        txtDate.Text = DateTime.Parse(dsDetails.Tables[0].Rows[0]["CDate"].ToString()).ToShortDateString();
                        txtComments.Text = dsDetails.Tables[0].Rows[0]["Comments"].ToString();
                        //ddType.SelectedValue = dsDetails.Tables[0].Rows[0]["IsAssembly"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                try
                {
                    // Attempt to roll back the transaction.
                    transaction.Rollback();
                    //errorDisplay.AddItem("Exception : " + ex.Message + ex.StackTrace, DisplayIcons.Error, false);
                }
                catch (Exception ep)
                {
                    // Do nothing here; transaction is not active.
                    //errorDisplay.AddItem("Exception : " + ep.Message + ep.StackTrace, DisplayIcons.Error, false);
                }
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }

    protected void GridViewProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridViewProducts.PageIndex = e.NewPageIndex;

            BindProductsGrid(txtStartDate.Text, txtEndDate.Text, rdoIsPros.Checked);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GridViewProducts_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GridViewProducts, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GridViewProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (CheckIfCompReleased(int.Parse(((HiddenField)e.Row.FindControl("hdcompID")).Value)))
                {
                    ((ImageButton)e.Row.FindControl("btnRelease")).Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdIn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (GridViewProducts.SelectedDataKey != null)
                {
                    int CompID = int.Parse(GridViewProducts.SelectedDataKey.Value.ToString());

                    if (CompID != 0)
                        ((TextBox)e.Row.FindControl("txtQty")).Enabled = false;

                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void grdOut_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (GridViewProducts.SelectedDataKey != null)
                {
                    int CompID = int.Parse(GridViewProducts.SelectedDataKey.Value.ToString());

                    if (CompID != 0)
                        ((TextBox)e.Row.FindControl("txtQty")).Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void grdIn_DataBound(object sender, EventArgs e)
    {

    }
}
