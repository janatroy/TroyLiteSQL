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
using System.Text;
using SMSLibrary;

public partial class BillAdjustment : System.Web.UI.Page
{
    Double sumAmt = 0.0;
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!Page.IsPostBack)
            {
                string connStr = string.Empty;
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                CheckSMSRequired();

                loadBanks();

                ddledger.DataBind();

                GrdViewReceipt.PageSize = 8;

                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic();

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAdd.Visible = false;
                    GrdViewReceipt.Columns[8].Visible = false;
                    GrdViewReceipt.Columns[7].Visible = false;
                }

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
                pnlEdit.Visible = false;
                myRangeValidator.MinimumValue = System.DateTime.Now.AddYears(-100).ToShortDateString();
                myRangeValidator.MaximumValue = System.DateTime.Now.ToShortDateString();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdCancelMet_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupMethod.Hide();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadSupplier(string SundryType)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ddledger.Items.Clear();

        if (SundryType == "Sundry Debtors")
        {
            ds = bl.ListSundryDebtors(sDataSource);
            ddledger.Items.Add(new ListItem("Select Customer", "0"));
        }
        if (SundryType == "Sundry Creditors")
        {
            ds = bl.ListSundryCreditors(sDataSource);
            ddledger.Items.Add(new ListItem("Select Supplier", "0"));
        }
        
        ddledger.DataSource = ds;
        ddledger.DataBind();
        ddledger.DataTextField = "LedgerName";
        ddledger.DataValueField = "LedgerID";
        
    }

    protected void GrdViewSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try

        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("GrdViewDetails") as GridView;

                String BillNo = GrdViewSales.DataKeys[e.Row.RowIndex].Value.ToString();

                string connStr = string.Empty;

                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                BusinessLogic bl = new BusinessLogic();

                DataSet receivedData = bl.GetCustReceivedAmount(connStr, BillNo);

                Session["BillData"] = receivedData;


                if (receivedData.Tables[0].Rows.Count > 0)
                {
                    GrdViewDetails.DataSource = receivedData;
                    GrdViewDetails.DataBind();
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

       

    }

    private void ShowPendingBills()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"]  != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        BusinessLogic bl = new BusinessLogic();
        var customerID = ddledger.SelectedValue.Trim();

        var dsSales = bl.ListCreditSales(connStr.Trim(), customerID);

        var receivedData = bl.GetCustReceivedAmount(connStr);

        if (dsSales != null)
        {

            foreach (DataRow dr in receivedData.Tables[0].Rows)
            {
                var billNo = dr["BillNo"].ToString();
                var billAmount = dr["TotalAmount"].ToString();

                for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                {
                    if (billNo.Trim() == dsSales.Tables[0].Rows[i]["BillNo"].ToString())
                    {
                        dsSales.Tables[0].Rows[i].BeginEdit();
                        double val = (double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
                        dsSales.Tables[0].Rows[i]["Amount"] = val;
                        dsSales.Tables[0].Rows[i].EndEdit();

                        if (val == 0.0)
                            dsSales.Tables[0].Rows[i].Delete();
                    }
                }
                dsSales.Tables[0].AcceptChanges();
            }

        }
        rowdetails.Visible = true;
        GrdViewSales.DataSource = dsSales;
        GrdViewSales.DataBind();

    }

    private void loadBanks()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBanks();
        //ddBanks.DataSource = ds;
        //ddBanks.DataTextField = "LedgerName";
        //ddBanks.DataValueField = "LedgerID";
        //ddBanks.DataBind();
    }

    private void checkPendingBills(DataSet ds)
    {
        foreach (GridViewRow tt in GrdViewSales.Rows)
        {
            if (tt.RowType == DataControlRowType.DataRow)
            {
                string billNo = tt.Cells[0].Text;

                bool exists = false;

                if (ds != null)
                {
                    foreach (DataRow d in ds.Tables[0].Rows)
                    {
                        string bNo = d[1].ToString();

                        if (bNo == billNo)
                        {
                            exists = true;
                        }

                    }
                }

                if (!exists)
                {
                    hdPendingCount.Value = "1";
                    UpdatePanelPage.Update();
                    return;
                }

            }
        }

        hdPendingCount.Value = "0";
        UpdatePanelPage.Update();
    }

    private void CheckSMSRequired()
    {
        DataSet appSettings;
        string smsRequired = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "SMSREQ")
                {
                    smsRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["SMSREQUIRED"] = smsRequired.Trim().ToUpper();
                }

            }
        }

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    }

    protected void GrdViewReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            /*//MyAccordion.Visible = false;
            frmViewAdd.Visible = true;
            frmViewAdd.DataBind();
            frmViewAdd.ChangeMode(FormViewMode.Edit);
            //GrdViewReceipt.Columns[7].Visible = false;
            lnkBtnAdd.Visible = false;
            GrdViewReceipt.Visible = false;
            //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
                //Accordion1.SelectedIndex = 1;*/
        }
    }

    protected void GrdViewReceipt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
        //GridViewRow Row = GrdViewReceipt.SelectedRow;
        //string connection = Request.Cookies["Company"].Value;
        //BusinessLogic bl = new BusinessLogic();
        //string recondate = Row.Cells[2].Text;
        //Session["BillData"] = null;
        ////hd.Value = Convert.ToString(GrdViewReceipt.SelectedDataKey.Value);

        //if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
        //    return;
        //}
        //else
        //{
        //    //pnlEdit.Visible = true;
        //    DataSet ds = bl.GetReceiptForId(connection, int.Parse(GrdViewReceipt.SelectedDataKey.Value.ToString()));
        //    if (ds != null)
        //    {
        //        txtRefNo.Text = ds.Tables[0].Rows[0]["RefNo"].ToString();
        //        txtTransDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["TransDate"].ToString()).ToShortDateString();

        //        ddledger.SelectedValue = ds.Tables[0].Rows[0]["CreditorID"].ToString();
        //        txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
        //        txtMobile.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
        //        chkPayTo.SelectedValue = ds.Tables[0].Rows[0]["paymode"].ToString();
        //        txtNarration.Text = ds.Tables[0].Rows[0]["Narration"].ToString();
        //        if (chkPayTo.SelectedItem != null)
        //        {
        //            if (chkPayTo.SelectedItem.Text == "Cheque")
        //            {
        //                tblBank.Attributes.Add("class", "AdvancedSearch");
        //            }
        //            else
        //            {
        //                tblBank.Attributes.Add("class", "hidden");
        //            }
        //        }
        //        else
        //        {
        //            tblBank.Attributes.Add("class", "hidden");
        //        }

        //        txtChequeNo.Text = ds.Tables[0].Rows[0]["ChequeNo"].ToString();

        //        string creditorID = ds.Tables[0].Rows[0]["DebtorID"].ToString();

        //        ddBanks.ClearSelection();

        //        ListItem li = ddBanks.Items.FindByValue(creditorID);
        //        if (li != null) li.Selected = true;

        //        DataSet billsData = bl.GetReceivedAmountId(connection, int.Parse(GrdViewReceipt.SelectedDataKey.Value.ToString()));

        //        Session["BillData"] = billsData;

        //        if (billsData.Tables[0].Rows[0]["BillNo"].ToString() == "0")
        //        {
        //            billsData = null;
        //        }
        //        GrdBills.DataSource = billsData;
        //        GrdBills.DataBind();
        //        Session["RMode"] = "Edit";
        //        ShowPendingBills();
        //        checkPendingBills(billsData);
        //    }

        //    //GrdViewReceipt.Visible = false;
        //    ////MyAccordion.Visible = false;
        //    //lnkBtnAdd.Visible = false;
        //    pnlEdit.Visible = true;
        //    UpdateButton.Visible = true;
        //    SaveButton.Visible = false;
        //    ModalPopupExtender2.Show();

        //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GrdViewSales_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewSales, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void GrdBillsCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdViewDetails.EditIndex = -1;
        if (Session["BillData"] != null)
        {
            GrdViewDetails.DataSource = (DataSet)Session["BillData"];
            GrdViewDetails.DataBind();
            checkPendingBills((DataSet)Session["BillData"]);
        }
    }

    protected void lnkAddBills_Click(object sender, EventArgs e)
    {
        try
        {
        pnlEdit.Visible = false;
        BusinessLogic bl = new BusinessLogic();
        string conn = GetConnectionString();
        ModalPopupExtender2.Show();
        pnlEdit.Visible = true;
        //if (txtAmount.Text == "")
        //{

        //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter the Receipt Amount before Adding BillNo')", true);
        //    //CnrfmDel.ConfirmText = "Please enter the Receipt Amount before Adding BillNo";
        //    //CnrfmDel.TargetControlID = "lnkAddBills";
        //    txtAmount.Focus();
        //    return;
        //}

        //if (ddledger.SelectedValue == "0")
        //{
        //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select the Customer before Adding Bills')", true);
        //    //pnlEdit.Visible = true;
        //    txtAmount.Focus();
        //    return;
        //}

        if (GrdViewDetails.Rows.Count == 0)
        {
            var ds = bl.GetReceivedAmountId(conn, 0);
            GrdViewDetails.DataSource = ds;
            GrdViewDetails.DataBind();
            GrdViewDetails.Rows[0].Visible = false;
            checkPendingBills(ds);
        }
        pnlEdit.Visible = true;
        GrdViewDetails.FooterRow.Visible = true;
        lnkAddBills.Visible = true;
        Session["RMode"] = "Add";
        //lnkBtnAdd.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //MyAccordion.Visible = true;
            pnlEdit.Visible = false;
            ModalPopupExtender2.Hide();
            //lnkBtnAdd.Visible = true;
            //lnkAddBills.Visible = true;
            GrdViewReceipt.Visible = true;
            GrdViewReceipt.Columns[8].Visible = true;
            ClearPanel();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void GrdViewReceipt_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewReceipt, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gridView = (GridView)sender;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                int cellIndex = -1;

                foreach (DataControlField field in gridView.Columns)
                {
                    if (field.SortExpression == gridView.SortExpression)
                    {
                        cellIndex = gridView.Columns.IndexOf(field);
                    }
                    else if (field.SortExpression != "")
                    {
                        e.Row.Cells[gridView.Columns.IndexOf(field)].CssClass = "headerstyle";
                    }

                }

                if (cellIndex > -1)
                {
                    //  this is a header row,
                    //  set the sort style
                    e.Row.Cells[cellIndex].CssClass =
                        gridView.SortDirection == SortDirection.Ascending
                        ? "sortascheaderstyle" : "sortdescheaderstyle";
                }
            }
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
            Session["Show"] = "No";
            ModalPopupMethod.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdMet_Click(object sender, EventArgs e)
    {
        try
        {
            Session["Show"] = "No";
            ModalPopupMethod.Show();
            if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
                return;
            }
            ModalPopupExtender2.Show();
            pnlEdit.Visible = true;
            UpdateButton.Visible = false;
            SaveButton.Visible = true;
            ClearPanel();

            txtTransDate.Text = DateTime.Now.ToShortDateString();

            if (optionmethod.SelectedItem.Text == "Customer")
            {
                loadSupplier("Sundry Debtors");
                Label2.Text = "Select Customer *";
            }
            else if (optionmethod.SelectedItem.Text == "Supplier")
            {
                loadSupplier("Sundry Creditors");
                Label2.Text = "Select Supplier *";
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void ClearPanel()
    {
        //txtRefNo.Text = "";
        //txtTransDate.Text = "";
        //txtNarration.Text = "";
        //txtChequeNo.Text = "";
        //txtAmount.Text = "";
        //ddledger.SelectedValue = "0";
        //txtMobile.Text = "";
        //ddBanks.SelectedValue = "0";
        GrdViewDetails.DataSource = null;
        GrdViewDetails.DataBind();
        Session["BillData"] = null;
    }

    protected void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string debtorID = ddledger.SelectedValue;

            //ReportsBL.ReportClass rptStock = new ReportsBL.ReportClass();
            //DataSet ds = rptStock.getCategory(sDataSource);
            //gvCategory.DataSource = ds;
            //gvCategory.DataBind();

            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            BusinessLogic bl = new BusinessLogic();
            var customerID = ddledger.SelectedValue.Trim();

            var dsSales = bl.ListCreditSales(connStr.Trim(), customerID);

            rowdetails.Visible = true;
            GrdViewSales.DataSource = dsSales;
            GrdViewSales.DataBind();

            //ShowPendingBills();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                GrdViewReceipt.DataBind();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewReceipt_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GrdViewReceipt.SelectedIndex = e.RowIndex;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (GrdViewReceipt.SelectedDataKey != null)
                e.InputParameters["TransNo"] = GrdViewReceipt.SelectedDataKey.Value;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewReceipt_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                GrdViewReceipt.DataBind();
            }
            else
            {
                if (e.Exception.InnerException != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('You are not allowed to delete the record. Please contact Administrator.');");

                    if (e.Exception.InnerException.Message.IndexOf("Invalid Date") > -1)
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                    e.ExceptionHandled = true;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void EditBill(object sender, GridViewEditEventArgs e)
    {
        GrdViewDetails.EditIndex = e.NewEditIndex;
        DataRow row = ((DataSet)Session["BillData"]).Tables[0].Rows[e.NewEditIndex];
        Session["EditedRow"] = e.NewEditIndex.ToString();
        Session["EditedAmount"] = row["Amount"].ToString();
        GrdViewDetails.DataSource = (DataSet)Session["BillData"];
        GrdViewDetails.DataBind();
    }

    private void calcSum()
    {
        var ds = (DataSet)GrdViewDetails.DataSource;

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Amount"] != null)
                    {
                        sumAmt = sumAmt + Convert.ToDouble(dr["Amount"].ToString());
                    }
                }
            }
        }
    }

    private double calcDatasetSum(DataSet ds)
    {
        double total = 0.0;

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Amount"] != null)
                    {
                        total = total + Convert.ToDouble(dr["Amount"].ToString());
                    }
                }
            }
        }

        return total;
    }

    protected void GrdViewDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Cancel")
            {
                GrdViewDetails.FooterRow.Visible = false;
                var ds = (DataSet)Session["BillData"];
                GrdViewDetails.EditIndex = -1;
                if (ds != null)
                {
                    GrdViewDetails.DataSource = ds;
                }
                GrdViewDetails.DataBind();
                lnkAddBills.Visible = true;
                ModalPopupExtender2.Show();
                pnlEdit.Visible = true;
                Error.Text = "";
            }
            else if (e.CommandName == "Edit")
            {
                ModalPopupExtender2.Show();
                lnkAddBills.Visible = false;
            }
            else if (e.CommandName == "Insert")
            {
                try
                {
                    ModalPopupExtender2.Show();
                    DataTable dt;
                    DataRow drNew;
                    DataColumn dc;
                    DataSet ds;
                    BusinessLogic bl = new BusinessLogic(GetConnectionString());

                    string billNo = ((TextBox)GrdViewDetails.FooterRow.FindControl("txtAddBillNo")).Text;
                    string amount = ((TextBox)GrdViewDetails.FooterRow.FindControl("txtAddBillAmount")).Text;
                    string CustomerID = ddledger.SelectedValue.ToString().Trim();
                    string TransNo = string.Empty;

                    //if (GrdViewReceipt.SelectedDataKey != null)
                    //    TransNo = GrdViewReceipt.SelectedDataKey.Value.ToString();
                    //else
                    TransNo = "";

                    //if (bl.GetIfBillNoExists(int.Parse(billNo), CustomerID) == 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('BillNo does not Exists. Please check BillNo.')", true);
                    //    //Error.Text = "BillNo does not Exists. Please check BillNo.";
                    //    pnlEdit.Visible = true;
                    //    ModalPopupExtender2.Show();
                    //    return;
                    //}

                    //var isBillExists = CheckIfBillExists(billNo);

                    //if (isBillExists)
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('BillNo already Exists')", true);
                    //    //Error.Text = "BillNo already Exists";
                    //    ModalPopupExtender2.Show();
                    //    return;
                    //}


                    double eligibleAmount = bl.GetSalesPendingAmount(int.Parse(billNo));

                    if (double.Parse(amount) > eligibleAmount)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The Amount you entered for BillNo:" + billNo + " is Greater than Pending Sales Amount of " + eligibleAmount.ToString() + ". Please check the Bill Amount')", true);
                        //Error.Text = "The Amount you entered for BillNo:" + billNo + " is Greater than Pending Sales Amount of " + eligibleAmount.ToString() + ". Please check the Bill Amount";
                        ModalPopupExtender2.Show();
                        return;
                    }

                    if ((Session["BillData"] == null) || (((DataSet)Session["BillData"]).Tables[0].Rows.Count == 0))
                    {

                        //if (double.Parse(amount) > double.Parse(txtAmount.Text))
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Bills amount is exceeding the Receipt Amount. Please check the Bill Amount')", true);
                        //    //Error.Text = "Total Bills amount is exceeding the Receipt Amount. Please check the Bill Amount";
                        //    ModalPopupExtender2.Show();
                        //    return;
                        //}

                        ds = new DataSet();
                        dt = new DataTable();

                        dc = new DataColumn("ReceiptNo");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("BillNo");
                        dt.Columns.Add(dc);

                        dc = new DataColumn("Amount");
                        dt.Columns.Add(dc);

                        ds.Tables.Add(dt);

                        drNew = dt.NewRow();

                        drNew["ReceiptNo"] = TransNo;
                        drNew["BillNo"] = billNo;
                        drNew["Amount"] = amount;

                        ds.Tables[0].Rows.Add(drNew);

                        Session["BillData"] = ds;
                        GrdViewDetails.DataSource = ds;
                        GrdViewDetails.DataBind();
                        GrdViewDetails.EditIndex = -1;
                        lnkAddBills.Visible = true;

                    }
                    else
                    {
                        ds = (DataSet)Session["BillData"];

                        //if ((calcDatasetSum(ds) + double.Parse(amount)) > double.Parse(txtAmount.Text))
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Bills amount is exceeding the Receipt Amount. Please check the Bill Amount')", true);
                        //    //Error.Text = "Total Bills amount is exceeding the Receipt Amount. Please check the Bill Amount";
                        //    ModalPopupExtender2.Show();
                        //    return;
                        //}

                        if (ds.Tables[0].Rows[0]["ReceiptNo"].ToString() == "0")
                        {
                            ds.Tables[0].Rows[0].Delete();
                            ds.Tables[0].AcceptChanges();
                        }

                        drNew = ds.Tables[0].NewRow();
                        drNew["ReceiptNo"] = TransNo;
                        drNew["BillNo"] = billNo;
                        drNew["Amount"] = amount;

                        ds.Tables[0].Rows.Add(drNew);
                        Session["BillData"] = ds;
                        //System.Threading.Thread.Sleep(1000);
                        GrdViewDetails.DataSource = ds;
                        GrdViewDetails.DataBind();
                        GrdViewDetails.EditIndex = -1;
                        lnkAddBills.Visible = true;
                        ModalPopupExtender2.Show();
                        checkPendingBills(ds);
                    }

                //}
                //catch (Exception ex)
                //{
                //    if (ex.InnerException != null)
                //    {
                //        StringBuilder script = new StringBuilder();
                //        script.Append("alert('Unit with this name already exists, Please try with a different name.');");

                //        if (ex.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);

                //        ModalPopupExtender2.Show();
                //        return;
                //    }
                //}
                }
                catch (Exception ex)
                {
                    TroyLiteExceptionManager.HandleException(ex);
                }
                finally
                {
                    //checkPendingBills();
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void GrdViewDetails_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(1000);
            GrdViewDetails.DataBind();
            lnkAddBills.Visible = true;
            //checkPendingBills();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private bool CheckIfBillExists(string billNo)
    {
        bool dupFlag = false;

        if (Session["BillData"] != null)
        {
            var checkDs = (DataSet)Session["BillData"];

            foreach (DataRow dR in checkDs.Tables[0].Rows)
            {
                if (dR["BillNo"] != null)
                {
                    if (dR["BillNo"].ToString().Trim() == billNo)
                    {
                        dupFlag = true;
                        break;
                    }
                }
            }
        }

        return dupFlag;
    }

    private int CheckNoOfBillExists(string billNo)
    {
        int count = 0;

        if (Session["BillData"] != null)
        {
            var checkDs = (DataSet)Session["BillData"];

            foreach (DataRow dR in checkDs.Tables[0].Rows)
            {
                if (dR["BillNo"] != null)
                {
                    if (dR["BillNo"].ToString().Trim() == billNo)
                    {
                        count = count + 1;
                    }
                }
            }
        }

        return count;
    }


    protected void GrdViewDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataSet ds;

        try
        {
            if (Session["BillData"] != null)
            {
                GridViewRow row = GrdViewDetails.Rows[e.RowIndex];
                ds = (DataSet)Session["BillData"];
                ds.Tables[0].Rows[GrdViewDetails.Rows[e.RowIndex].DataItemIndex].Delete();
                ds.Tables[0].AcceptChanges();
                GrdViewDetails.DataSource = ds;
                GrdViewDetails.DataBind();
                Session["BillData"] = ds;
                ModalPopupExtender2.Show();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int curRow = 0;
            string billNo = ((TextBox)GrdViewDetails.Rows[e.RowIndex].FindControl("txtBillNo")).Text;
            string amount = ((TextBox)GrdViewDetails.Rows[e.RowIndex].FindControl("txtBillAmount")).Text;
            //string Id = GrdBills.DataKeys[e.RowIndex].Value.ToString();
            string CustomerID = ddledger.SelectedValue.ToString().Trim();
            string TransNo = "0";
            ModalPopupExtender2.Show();

            //if (GrdViewReceipt.SelectedDataKey != null)
            //    TransNo = GrdViewReceipt.SelectedDataKey.Value.ToString();


            DataSet ds = (DataSet)Session["BillData"];

            //if ((calcDatasetSum(ds) + double.Parse(amount) - double.Parse(Session["EditedAmount"].ToString())) > double.Parse(txtAmount.Text))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Bills amount is exceeding the Receipt Amount. Please check the Bill Amount')", true);
            //    //Error.Text = "Total Bills amount is exceeding the Receipt Amount. Please check the Bill Amount";
            //    return;
            //}

            BusinessLogic bl = new BusinessLogic(GetConnectionString());

            //if (bl.GetIfBillNoExists(int.Parse(billNo), CustomerID) == 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('BillNo does not Exists. Please check BillNo.')", true);
            //    //Error.Text = "BillNo does not Exists. Please check BillNo.";
            //    pnlEdit.Visible = true;
            //    ModalPopupExtender2.Show();
            //    return;
            //}

            double eligibleAmount = bl.GetSalesPendingAmount(int.Parse(billNo));


            if ((double.Parse(amount) - double.Parse(Session["EditedAmount"].ToString())) > eligibleAmount)
            {
                var eliAmount = double.Parse(Session["EditedAmount"].ToString()) + eligibleAmount;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The Amount you entered for BillNo:" + billNo + " is Greater than Pending Sales Amount of " + eliAmount.ToString() + ". Please check the Bill Amount')", true);
                //Error.Text = "The Amount you entered for BillNo:" + billNo + " is Greater than Pending Sales Amount of " + eliAmount.ToString() + ". Please check the Bill Amount";
                return;
            }

            curRow = Convert.ToInt32(Session["EditedRow"].ToString());

            ds.Tables[0].Rows[curRow].BeginEdit();
            ds.Tables[0].Rows[curRow]["BillNo"] = billNo;
            ds.Tables[0].Rows[curRow]["Amount"] = amount;
            ds.Tables[0].Rows[curRow]["ReceiptNo"] = TransNo;

            var isBillExists = CheckNoOfBillExists(billNo);

            if (isBillExists > 1)
            {
                ds.Tables[0].Rows[curRow].RejectChanges();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('BillNo already Exists')", true);
                //Error.Text = "BillNo already Exists";
                return;
            }

            ds.Tables[0].Rows[curRow].EndEdit();

            ds.Tables[0].Rows[curRow].AcceptChanges();
            GrdViewDetails.DataSource = ds;
            GrdViewDetails.EditIndex = -1;
            GrdViewDetails.DataBind();
            Session["BillData"] = ds;
            lnkAddBills.Visible = true;
            checkPendingBills(ds);
            ModalPopupExtender2.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private string GetConnectionString()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"]  != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        return connStr;
    }

    protected void GrdViewDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewDetails, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsData = (DataSet)Session["BillData"];

            //if (calcDatasetSum(dsData) > double.Parse(txtAmount.Text))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Bills amount is exceeding the Receipt Amount. Please check the Bill Amount')", true);
            //    return;
            //}


            //if (chkPayTo.SelectedValue == "Cheque")
            //{
            //    cvBank.Enabled = true;
            //    rvChequeNo.Enabled = true;
            //}
            //else
            //{
            //    cvBank.Enabled = false;
            //    rvChequeNo.Enabled = false;
            //}

            //Page.Validate();

            //if (Page.IsValid)
            //{

            //    int CreditorID = int.Parse(ddledger.SelectedValue);

            //    string RefNo = txtRefNo.Text;

            //    DateTime TransDate = DateTime.Parse(txtTransDate.Text);

            //    int DebitorID = 0;
            //    string Paymode = string.Empty;
            //    double Amount = 0.0;
            //    string Narration = string.Empty;
            //    string VoucherType = string.Empty;
            //    string ChequeNo = string.Empty;

            //    if (chkPayTo.SelectedValue == "Cash")
            //    {
            //        DebitorID = 1;
            //        Paymode = "Cash";
            //    }
            //    else if (chkPayTo.SelectedValue == "Cheque")
            //    {
            //        DebitorID = int.Parse(ddBanks.SelectedValue);
            //        Paymode = "Cheque";
            //    }

            //    Amount = double.Parse(txtAmount.Text);
            //    Narration = txtNarration.Text;
            //    VoucherType = "Receipt";
            //    ChequeNo = txtChequeNo.Text;

            //    if (hdSMSRequired.Value == "YES")
            //    {

            //        if (txtMobile.Text != "")
            //            hdMobile.Value = txtMobile.Text;

            //        hdText.Value = "Thank you for Payment of Rs." + txtAmount.Text;

            //    }

            //    BusinessLogic bl = new BusinessLogic();
            //    string conn = GetConnectionString();
            //    int OutPut = 0;

            //    DataSet ds = (DataSet)Session["BillData"];


            //    if (ds == null)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter the Bill Details')", true);
            //        return;
            //    }


            //    bl.InsertCustReceipt(out OutPut, conn, RefNo, TransDate, DebitorID, CreditorID, Amount, Narration, VoucherType, ChequeNo, Paymode, ds);

            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Receipt Saved Successfully. Transaction No : " + OutPut.ToString() + "');", true);

            //    if (hdSMS.Value == "YES")
            //    {
            //        UtilitySMS utilSMS = new UtilitySMS(conn);
            //        string UserID = Page.User.Identity.Name;

            //        if (Session["Provider"] != null)
            //            utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), hdMobile.Value, hdText.Value, true, UserID);
            //        else
            //        {
            //            if (hdMobile.Value != "")
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
            //        }
            //    }

            //    pnlEdit.Visible = false;
            //    ModalPopupExtender2.Hide();
            //    lnkBtnAdd.Visible = true;
            //    //MyAccordion.Visible = true;
            //    GrdViewReceipt.Visible = true;
            //    GrdViewReceipt.DataBind();
            //    ClearPanel();
            //    UpdatePanelPage.Update();

            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ShowPendingSales_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender1.Show();
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsData = (DataSet)Session["BillData"];

            //if (calcDatasetSum(dsData) > double.Parse(txtAmount.Text))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Bills amount is exceeding the Receipt Amount. Please check the Bill Amount')", true);
            //    return;
            //}


            //if (chkPayTo.SelectedValue == "Cheque")
            //{
            //    cvBank.Enabled = true;
            //    rvChequeNo.Enabled = true;
            //}
            //else
            //{
            //    cvBank.Enabled = false;
            //    rvChequeNo.Enabled = false;
            //}

            //Page.Validate();

            //if (Page.IsValid)
            //{

            //    int CreditorID = int.Parse(ddledger.SelectedValue);

            //    string RefNo = txtRefNo.Text;

            //    DateTime TransDate = DateTime.Parse(txtTransDate.Text);

            //    int DebitorID = 0;
            //    string Paymode = string.Empty;
            //    double Amount = 0.0;
            //    string Narration = string.Empty;
            //    string VoucherType = string.Empty;
            //    string ChequeNo = string.Empty;
            //    int TransNo = 0;

            //    if (chkPayTo.SelectedValue == "Cash")
            //    {
            //        DebitorID = 1;
            //        Paymode = "Cash";
            //    }
            //    else if (chkPayTo.SelectedValue == "Cheque")
            //    {
            //        DebitorID = int.Parse(ddBanks.SelectedValue);
            //        Paymode = "Cheque";
            //    }

            //    Amount = double.Parse(txtAmount.Text);
            //    Narration = txtNarration.Text;
            //    VoucherType = "Receipt";
            //    ChequeNo = txtChequeNo.Text;
            //    TransNo = int.Parse(GrdViewReceipt.SelectedDataKey.Value.ToString());

            //    if (hdSMSRequired.Value == "YES")
            //    {

            //        if (txtMobile.Text != "")
            //            hdMobile.Value = txtMobile.Text;

            //        hdText.Value = "Thank you for Payment of Rs." + txtAmount.Text;

            //    }

            //    BusinessLogic bl = new BusinessLogic();
            //    string conn = GetConnectionString();
            //    int OutPut = 0;

            //    DataSet ds = (DataSet)Session["BillData"];

            //    bl.UpdateCustReceipt(out OutPut, conn, TransNo, RefNo, TransDate, DebitorID, CreditorID, Amount, Narration, VoucherType, ChequeNo, Paymode, ds);

            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Receipt Updated Successfully. Transaction No : " + OutPut.ToString() + "');", true);

            //    if (hdSMS.Value == "YES")
            //    {
            //        UtilitySMS utilSMS = new UtilitySMS(conn);
            //        string UserID = Page.User.Identity.Name;

            //        if (Session["Provider"] != null)
            //            utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), hdMobile.Value, hdText.Value, true, UserID);
            //        else
            //        {
            //            if (hdMobile.Value != "")
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
            //        }
            //    }

            //    pnlEdit.Visible = false;
            //    //lnkBtnAdd.Visible = true;
            //    ////MyAccordion.Visible = true;
            //    //GrdViewReceipt.Visible = true;
            //    //ModalPopupExtender2.Hide();
            //    //popUp.Visible = false;
            //    GrdViewReceipt.DataBind();
            //    ClearPanel();
            //    UpdatePanelPage.Update();

            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
