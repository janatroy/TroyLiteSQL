using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class InternalTransferApproval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindGridData();
            BindDropdowns();

            //GrdViewRequestes.PageSize = 2;
        }
    }
    private string GetConnectionString()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        return connStr;
    }

    private void BindGridData()
    {
        string connection = Request.Cookies["Company"].Value;
        IInternalTransferService bl = new BusinessLogic(connection);
      //  string branch = Request.Cookies["Branch"].Value;
        var dbData = bl.ListInternalRequests1(connection, txtSearch.Text, ddCriteria.SelectedValue);

        if (dbData != null)
        {
            GrdViewRequestes.DataSource = dbData;
            GrdViewRequestes.DataBind();
        }
    }

    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrdViewRequestes.PageIndex = ((DropDownList)sender).SelectedIndex;

           // ModalPopupExtender1.Show();
            BindGridData();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GrdViewRequestes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewRequestes.PageIndex = e.NewPageIndex;

            BindGridData();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewRequestes_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    GridViewRow Row = GrdViewRequestes.SelectedRow;
        //    string connection = Request.Cookies["Company"].Value;
        //    IInternalTransferService bl = new BusinessLogic(connection, connection);

        //    int ID = Convert.ToInt32(GrdViewRequestes.SelectedDataKey.Value);
        //    UpdateButton.Visible = true;
        //    InsertButton.Visible = false;
        //    contentPopUp.Visible = true;
        //    cmbStatus.Enabled = false;
        //    txtCompletedDate.Enabled = false;
        //    txtReason.Enabled = false;

        //    //GrdViewBranches.Visible = false;

        //    InternalTransferRequest requestDetail = bl.GetInternalTransferRequest(ID);

        //    if (requestDetail != null)
        //    {
        //        hdRequestID.Value = ID.ToString();

        //        txtRequestedDate.Text = requestDetail.RequestedDate.ToShortDateString();
        //        if (cmbProd.Items.FindByValue(requestDetail.ItemCode) != null)
        //            cmbProd.SelectedValue = requestDetail.ItemCode;

        //        if (cmbRequestedBranch.Items.FindByValue(requestDetail.RequestedBranch) != null)
        //            cmbRequestedBranch.SelectedValue = requestDetail.RequestedBranch;

        //        if (cmbBranchHasStock.Items.FindByValue(requestDetail.BranchHasStock) != null)
        //            cmbBranchHasStock.SelectedValue = requestDetail.BranchHasStock;

        //        if (cmbStatus.Items.FindByValue(requestDetail.Status) != null)
        //            cmbStatus.SelectedValue = requestDetail.Status;

        //        txtReason.Text = requestDetail.RejectedReason;

        //        txtQtyAdd.Text = requestDetail.Quantity.ToString();

        //        if (requestDetail.CompletedDate.HasValue)
        //           txtCompletedDate.Text = requestDetail.CompletedDate.ToString();

        //        ModalPopupExtender1.Show();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    TroyLiteExceptionManager.HandleException(ex);
        //}



        //frmViewAdd.Visible = true;
        //frmViewAdd.ChangeMode(FormViewMode.Edit);
        //GrdViewBranches.Columns[8].Visible = false;
        //lnkBtnAdd.Visible = false;
        modalPopupApproveReject.Show();

        //GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;

        GridViewRow row = GrdViewRequestes.SelectedRow;

        int rowIndex = row.RowIndex;

        hdRequestID.Value = GrdViewRequestes.DataKeys[rowIndex].Value.ToString();
        //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
        //    Accordion1.SelectedIndex = 1;
    }

    protected void GrdViewRequestes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (((System.Web.UI.WebControls.Image)(e.CommandSource as ImageButton)).ID == "btnApprove")
        //{
           
        //}
        //else if (((System.Web.UI.WebControls.Image)(e.CommandSource as ImageButton)).ID == "btnEdit")
        //{
        //    try
        //    {

        //        GridViewRow Row = GrdViewRequestes.SelectedRow;
        //        string connection = Request.Cookies["Company"].Value;
        //        IInternalTransferService bl = new BusinessLogic(connection);

        //        int ID = Convert.ToInt32(GrdViewRequestes.SelectedDataKey.Value);
        //        UpdateButton.Visible = true;
        //        InsertButton.Visible = false;
        //        contentPopUp.Visible = true;
        //        cmbStatus.Enabled = false;
        //        txtCompletedDate.Enabled = false;
        //        txtReason.Enabled = false;

        //        //GrdViewBranches.Visible = false;

        //        InternalTransferRequest requestDetail = bl.GetInternalTransferRequest(connection, ID);

        //        if (requestDetail != null)
        //        {
        //            hdRequestID.Value = ID.ToString();

        //            txtRequestedDate.Text = requestDetail.RequestedDate.ToShortDateString();
        //            if (cmbProd.Items.FindByValue(requestDetail.ItemCode) != null)
        //                cmbProd.SelectedValue = requestDetail.ItemCode;

        //            if (cmbRequestedBranch.Items.FindByValue(requestDetail.RequestedBranch) != null)
        //                cmbRequestedBranch.SelectedValue = requestDetail.RequestedBranch;

        //            if (cmbBranchHasStock.Items.FindByValue(requestDetail.BranchHasStock) != null)
        //                cmbBranchHasStock.SelectedValue = requestDetail.BranchHasStock;

        //            if (cmbStatus.Items.FindByValue(requestDetail.Status) != null)
        //                cmbStatus.SelectedValue = requestDetail.Status;

        //            txtReason.Text = requestDetail.RejectedReason;

        //            txtQtyAdd.Text = requestDetail.Quantity.ToString();

        //            if (requestDetail.CompletedDate.HasValue)
        //                txtCompletedDate.Text = requestDetail.CompletedDate.ToString();

        //            ModalPopupExtender1.Show();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TroyLiteExceptionManager.HandleException(ex);
        //    }
        //}
    }

    protected void GrdViewRequestes_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewRequestes, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewRequestes_RowDataBound(object sender, GridViewRowEventArgs e)
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

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[8].Text == "&nbsp;")
                {
                    ((ImageButton)e.Row.FindControl("btnApprove")).Visible = true;
                    ((ImageButton)e.Row.FindControl("btnApproveDisabled")).Visible = false;

                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = true;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = false;

                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = true;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = false;
                }
                else
                {
                    ((ImageButton)e.Row.FindControl("btnApprove")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnApproveDisabled")).Visible = true;


                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;

                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;

                }
                BusinessLogic bl1 = new BusinessLogic(GetConnectionString());


                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl1.CheckUserHaveEdit(usernam, "INTTRNSAPRL"))
                {

                    ((ImageButton)e.Row.FindControl("btnApprove")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnApproveDisabled")).Visible = true;
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
            //if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
            //    return;
            //}
            txtRequestedDate.Text = DateTime.Now.ToShortDateString();
            cmbStatus.Enabled = false;
            txtCompletedDate.Enabled = false;
            txtReason.Enabled = false;
            //GrdViewRequestes.Visible = false;
            ////MyAccordion.Visible = false;
            contentPopUp.Visible = true;
            UpdateButton.Visible = false;
            InsertButton.Visible = true;
            ModalPopupExtender1.Show();

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnCancelSaveComments_Click(object sender, EventArgs e)
    {
        try
        {
            modalPopupApproveReject.Hide();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            GrdViewRequestes.Visible = true;
            //MyAccordion.Visible = true;
            lnkBtnAdd.Visible = true;
            contentPopUp.Visible = false;
            //frmViewAdd.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                string connection = Request.Cookies["Company"].Value;
                string UserID = Request.Cookies["LoggedUserName"].Value;

                InternalTransferRequest request = new InternalTransferRequest();
                IInternalTransferService bl = new BusinessLogic(connection);

                request.RequestID = int.Parse(hdRequestID.Value);
                request.UserID = UserID;

                if (cmbProd.SelectedValue != "0")
                    request.ItemCode = cmbProd.SelectedValue;
                if (cmbRequestedBranch.SelectedValue != "0")
                    request.RequestedBranch = cmbRequestedBranch.SelectedValue;
                if (cmbBranchHasStock.SelectedValue != "0")
                    request.BranchHasStock = cmbBranchHasStock.SelectedValue;

                if (cmbStatus.SelectedValue != "0")
                    request.Status = cmbStatus.SelectedValue;

                if (!string.IsNullOrEmpty(txtRequestedDate.Text))
                    request.RequestedDate = Convert.ToDateTime(txtRequestedDate.Text);

                if (!string.IsNullOrEmpty(txtQtyAdd.Text))
                    request.Quantity = Convert.ToDecimal(txtQtyAdd.Text);

                bl.UpdateInternalRequest(connection, request);

                ModalPopupExtender1.Hide();

                BindGridData();

                contentPopUp.Visible = false;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Internal Transfer Request Saved Successfully.');", true);

            }
            catch (Exception ex)
            {
                TroyLiteExceptionManager.HandleException(ex);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Error occured while saving details. Please contact Administrator');", true);
            }
        }
    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            GrdViewRequestes.Visible = true;
            //frmViewAdd.Visible = false;
            lnkBtnAdd.Visible = true;
            //MyAccordion.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmbApproveReject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbApproveReject.SelectedValue == "Reject")
        {
            //if (txtComments.Text.Trim() == string.Empty)
            //{
            //    rvComments.Enabled = true;
            //    Page.Validate();
                modalPopupApproveReject.Show();
            //   // ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter rejected reason.it cannot be left blank.');", true);
               return;
            //}
        }
        else
        {
            modalPopupApproveReject.Show();
            rvComments.Enabled = false;
            Page.Validate();
        }
    }

    private string GetDiscType()
    {
        DataSet appSettings;
        string discType = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "DISCTYPE")
                {
                    discType = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["DISCTYPE"] = discType.Trim().ToUpper();
                }

            }
        }
        else if (Session["AppSettings"] == null)
        {
            BusinessLogic bl = new BusinessLogic();
            DataSet ds = bl.GetAppSettings(Request.Cookies["Company"].Value);

            if (ds != null)
                Session["AppSettings"] = ds;

            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "DISCTYPE")
                {
                    discType = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["DISCTYPE"] = discType.Trim().ToUpper();
                }

            }
        }

        return discType;

    }

    private DataSet GetProductDetails(string ItemCode, string BranchCode, decimal Quantity, string billingMethod, DataSet prodData)
    {
        string discType = GetDiscType();

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc = new DataColumn("Prd");
        dt.Columns.Add(dc);

        dc = new DataColumn("Emp");
        dt.Columns.Add(dc);

        dc = new DataColumn("Desc");
        dt.Columns.Add(dc);

        dc = new DataColumn("Rate");
        dt.Columns.Add(dc);

        dc = new DataColumn("TotPrice");
        dt.Columns.Add(dc);

        dc = new DataColumn("Stock");
        dt.Columns.Add(dc);

        dc = new DataColumn("Qty");
        dt.Columns.Add(dc);

        dc = new DataColumn("ExeComm");
        dt.Columns.Add(dc);

        dc = new DataColumn("DisPre");
        dt.Columns.Add(dc);

        dc = new DataColumn("VATPre");
        dt.Columns.Add(dc);

        dc = new DataColumn("NLP");
        dt.Columns.Add(dc);

        dc = new DataColumn("CSTPre");
        dt.Columns.Add(dc);

        dc = new DataColumn("PrBefVATAmt");
        dt.Columns.Add(dc);

        dc = new DataColumn("VATAmt");
        dt.Columns.Add(dc);

        dc = new DataColumn("RtVAT");
        dt.Columns.Add(dc);

        dc = new DataColumn("Tot");
        dt.Columns.Add(dc);

        dc = new DataColumn("Prdname");
        dt.Columns.Add(dc);

        dc = new DataColumn("MeasureUnit");
        dt.Columns.Add(dc);

        dc = new DataColumn("Roles");
        dt.Columns.Add(dc);

        dc = new DataColumn("IsRole");
        dt.Columns.Add(dc);

        dc = new DataColumn("Bundles");
        dt.Columns.Add(dc);

        dc = new DataColumn("Rods");
        dt.Columns.Add(dc);

        dc = new DataColumn("DiscAmt");
        dt.Columns.Add(dc);

        dc = new DataColumn("TotalMrp");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        DataRow drNew = dt.NewRow();

        if (prodData != null && prodData.Tables[0].Rows.Count > 0)
        {
            drNew["Prd"] = ItemCode;
            drNew["Emp"] = 0;
            drNew["Desc"] = prodData.Tables[0].Rows[0]["ProductDesc"].ToString();
            drNew["Rate"] = prodData.Tables[0].Rows[0]["Rate"].ToString();
            drNew["TotPrice"] = "0";
            drNew["Stock"] = prodData.Tables[0].Rows[0]["Stock"].ToString();
            drNew["Qty"] = Quantity;
            drNew["ExeComm"] = prodData.Tables[0].Rows[0]["ExecutiveCommission"].ToString();
            drNew["DisPre"] = prodData.Tables[0].Rows[0]["Discount"].ToString();


            //drNew["Roles"] = strRole;
            drNew["Prdname"] = prodData.Tables[0].Rows[0]["ProductName"].ToString();
            drNew["VATPre"] = prodData.Tables[0].Rows[0]["VAT"].ToString();
            drNew["VATAmt"] = "0.0";
            drNew["CSTPre"] = prodData.Tables[0].Rows[0]["CST"].ToString();
            drNew["PrBefVATAmt"] = "0.0";
            drNew["RtVAT"] = "0";
            drNew["Rods"] = "0";
            //drNew["ExecCharge"] = prodData.Tables[0].Rows[0]["ExecutiveCommission"].ToString();
            drNew["Totalmrp"] = prodData.Tables[0].Rows[0]["Rate"].ToString();
            drNew["MeasureUnit"] = prodData.Tables[0].Rows[0]["Measure_Unit"].ToString();
            drNew["Roles"] = "0";
            drNew["IsRole"] = "N";
            drNew["Bundles"] = "0";
            drNew["Roles"] = "0";
            drNew["Roles"] = "0";
            drNew["Tot"] = "0";
            drNew["NLP"] = "0";
            //drNew["TotalMrp"] = GetTotal(Convert.ToDouble(txtQtyAdd.Text.Trim()), Convert.ToDouble(prodData.Tables[0].Rows[0]["Rate"].ToString()), Convert.ToDouble(prodData.Tables[0].Rows[0]["Discount"].ToString()), Convert.ToDouble(prodData.Tables[0].Rows[0]["VAT"].ToString()), Convert.ToDouble(prodData.Tables[0].Rows[0]["CST"].ToString()), Convert.ToDouble("0.0"));

            double caldisamt = 0.0;

            if (billingMethod == "VAT INCLUSIVE")
            {
                string total = Convert.ToString(Convert.ToDouble(prodData.Tables[0].Rows[0]["Rate"].ToString()) * Convert.ToDouble(Quantity));
                if (discType == "PERCENTAGE")
                {
                    caldisamt = Convert.ToDouble(total) * Convert.ToDouble(prodData.Tables[0].Rows[0]["Discount"].ToString()) / 100;
                }
                else if (discType == "RUPEE")
                {
                    caldisamt = Convert.ToDouble(prodData.Tables[0].Rows[0]["Discount"].ToString());
                }

                drNew["DiscAmt"] = caldisamt.ToString();

                double calnet = Convert.ToDouble(total) - caldisamt;
                double vatper = Convert.ToDouble(prodData.Tables[0].Rows[0]["VAT"].ToString());
                double vatper1 = vatper + 100;
                double vatinclusiverate = calnet * vatper / vatper1;
                double sVatamount = calnet - vatinclusiverate;
                drNew["TotalMrp"] = calnet.ToString();
                drNew["TotPrice"] = calnet.ToString();
            }
            else if (billingMethod == "VAT EXCLUSIVE")
            {
                double vatinclusiverate = 0.0;
                var total = Convert.ToString(Convert.ToDouble(Convert.ToDouble(prodData.Tables[0].Rows[0]["Rate"].ToString()) * Convert.ToDouble(Quantity)));

                if (discType == "PERCENTAGE")
                {
                    vatinclusiverate = Convert.ToDouble(total) * Convert.ToDouble(prodData.Tables[0].Rows[0]["Discount"].ToString()) / 100;
                }
                else if (discType == "RUPEE")
                {
                    vatinclusiverate = Convert.ToDouble(prodData.Tables[0].Rows[0]["Discount"].ToString());
                }

                drNew["DiscAmt"] = vatinclusiverate.ToString();

                double vatinclusiverate3 = Convert.ToDouble(total) - vatinclusiverate;
                double vatinclusiverate1 = Convert.ToDouble(vatinclusiverate3) * Convert.ToDouble(prodData.Tables[0].Rows[0]["VAT"].ToString()) / 100;
                double vatinclusiverate2 = vatinclusiverate1 + vatinclusiverate3;
                drNew["TotalMrp"] = vatinclusiverate2.ToString();
                drNew["TotPrice"] = vatinclusiverate2.ToString();
            }

            ds.Tables[0].Rows.Add(drNew);
        }

        return ds;
    }

    public string GetTotal(double qty, double rate, double discount, double VAT, double CST, double discamt)
    {
        double dis = 0;
        double vat = 0;
        double cst = 0;
        double tot = 0;
        double disRate = 0;

        double vatat = 0;
        double cstat = 0;

        if ((discamt == 0) && (discount > 0))
        {
            tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));
        }
        else if ((discamt > 0) && (discount == 0))
        {
            tot = (qty * rate) - (discamt);
            vatat = (tot * (VAT / 100));
            cstat = (tot * (CST / 100));
            tot = tot + vatat + cstat;
        }
        else if ((discamt > 0) && (discount > 0))
        {
            tot = (qty * rate) - ((qty * rate) * (discount / 100)) - (discamt) + ((((qty * rate) - ((qty * rate) * (discount / 100))) - discamt) * (VAT / 100)) + ((((qty * rate) - ((qty * rate) * (discount / 100))) - discamt) * (CST / 100));
        }
        else if ((discamt == 0) && (discount == 0))
        {
            tot = (qty * rate) - ((qty * rate) * (discount / 100)) - (discamt) + ((((qty * rate) - ((qty * rate) * (discount / 100))) - discamt) * (VAT / 100)) + ((((qty * rate) - ((qty * rate) * (discount / 100))) - discamt) * (CST / 100));
        }

        // tot = (qty * rate) - ((qty * rate) * (discount / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (VAT / 100)) + (((qty * rate) - ((qty * rate) * (discount / 100))) * (CST / 100));

        if ((discamt == 0) && (discount > 0))
        {
            disRate = (qty * rate) - ((qty * rate) * (discount / 100));
        }
        else if ((discamt > 0) && (discount == 0))
        {
            disRate = (qty * rate) - (discamt);
        }
        else if ((discamt > 0) && (discount > 0))
        {
            disRate = (qty * rate) - ((qty * rate) * (discount / 100)) - (discamt);
        }
        else if ((discamt == 0) && (discount == 0))
        {
            disRate = (qty * rate) - ((qty * rate) * (discount / 100)) - (discamt);
        }

        if ((discamt == 0) && (discount > 0))
        {
            dis = ((qty * rate) * (discount / 100));
        }
        else if ((discamt > 0) && (discount == 0))
        {
            dis = ((qty * rate) * (discamt));
        }
        else if ((discamt > 0) && (discount > 0))
        {
            dis = ((qty * rate) * (discount / 100)) + (discamt);
        }
        else if ((discamt == 0) && (discount == 0))
        {
            dis = ((qty * rate) * (discount / 100)) + (discamt);
        }


        return tot.ToString("#0.00");
    }


    protected void SaveCommentsButton_Click(object sender, EventArgs e)
    {
        DataSet paymentdata = null;
       
      
        if (cmbApproveReject.SelectedValue == "Approve")
        {
           
            int iSupplier = 0;
            int iCustomer = 0;

            int iPaymode = 3;
            string sInvoiceno = string.Empty;
            string connection = Request.Cookies["Company"].Value;
            string UserID = Request.Cookies["LoggedUserName"].Value;

            string RequestID = hdRequestID.Value;
            IInternalTransferService transferService = new BusinessLogic(connection);


            InternalTransferRequest request = transferService.GetInternalTransferRequest(connection, int.Parse(RequestID));

            BusinessLogic branchHasStockService = new BusinessLogic(connection);
            BusinessLogic branchRequestedService = new BusinessLogic(connection);

            BusinessLogic bl = new BusinessLogic(connection);

            string BillingMethod = bl.getConfigInfoMethod();

            if (transferService.CheckIftheItemHasStock(connection, request.ItemCode, request.BranchHasStock, request.Quantity))
            {
                iCustomer = transferService.GetCustomerIDForBranchCode(connection, request.BranchHasStock);
                iSupplier = transferService.GetSupplierIDForBranchCode(connection, request.RequestedBranch);
                DataSet customer = transferService.GeBranchHasStockCustomerID(connection, request.BranchHasStock, iCustomer);
                DataSet executives = transferService.GeBranchHasStockExecutives(connection, request.BranchHasStock);
                DataSet supplier = transferService.GetRequestedBranchSupplierID(connection, request.BranchHasStock,iSupplier);

                if (customer != null && executives != null)
                {
                    string dispatchFrom = request.BranchHasStock + " Internal Trasfer";


                    //DataSet prodData = branchHasStockService.GetProductForId(connection, request.ItemCode);

                   // iCustomer = transferService.GetCustomerIDForBranchCode(connection, request.BranchHasStock);

                    DataSet customerInfo = bl.GetExecutive(iCustomer);

                    DataSet prodData = bl.ListSalesProductPriceDetails(request.ItemCode, customerInfo.Tables[0].Rows[0]["LedgerCategory"].ToString(), request.BranchHasStock);

                    DataSet ds = GetProductDetails(request.ItemCode, request.BranchHasStock, request.Quantity, BillingMethod, prodData);

                  

                    int billNo = branchHasStockService.InsertSalesNewSeries("", DateTime.Now.ToShortDateString(), iSupplier,
                        supplier.Tables[0].Rows[0]["LedgerName"].ToString(), "", "", 3, "", 0, 0.0, "NO", "", 0.0,
                        0.0, ds, "", "YES", null, "NO", "NO", "", "", executives.Tables[0].Rows[0]["empFirstName"].ToString(), dispatchFrom, 0, 0, 0.0, UserID, "NO",
                        "NO", "VAT EXCLUSIVE", "Internal Transfer", "N", "Y", "0", "Others", "PERCENTAGE", 0, request.BranchHasStock, connection, "NO", 0);

                  

                    branchRequestedService.InsertPurchase(billNo.ToString(), DateTime.Now, iCustomer, iPaymode, string.Empty, 0, 0, "NO", "", 0, 0, 0, "YES", ds, "NO", sInvoiceno, DateTime.Now, 0, 0, 0, 0, UserID, "Internal transfer", billNo, request.RequestedBranch, connection, "NO", paymentdata);

                    request.CompletedDate = DateTime.Now;
                    request.CompletedUser = UserID;
                    request.Status = "Completed";

                    transferService.ApproveInternalTrasfer(connection, request);
                    modalPopupApproveReject.Hide();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Internal Transfer Completed Successfully.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Insufficent stock, please check the Branch if it has sufficient stock available.');", true);
            }

            BindGridData();

        }
        else if (cmbApproveReject.SelectedValue == "Reject")
        {
            string connection1 = Request.Cookies["Company"].Value;
            

            string RequestID = hdRequestID.Value;
            IInternalTransferService transferService = new BusinessLogic(connection1);

            if ((txtComments.Text == "") || (txtComments.Text ==null))
            {
                modalPopupApproveReject.Show();
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter rejected reason.it cannot be left blank.');", true);
                return;
            }
            InternalTransferRequest request1 = transferService.GetInternalTransferRequest(connection1, int.Parse(RequestID));
          //  string connection1;
           // string RequestID = hdRequestID.Value;
           // IInternalTransferService transferService1 = new BusinessLogic(connection);
           // InternalTransferRequest request = transferService1.GetInternalTransferRequest(connection, int.Parse(RequestID));
            transferService.RejectInternalTrasfer(connection1, request1);
            modalPopupApproveReject.Hide();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Internal Transfer Rejected.Please contact administration');", true);
            BindGridData();

        }

    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                string connection = Request.Cookies["Company"].Value;
                string UserID = Request.Cookies["LoggedUserName"].Value;

                InternalTransferRequest request = new InternalTransferRequest();
                IInternalTransferService bl = new BusinessLogic(connection);

                if (cmbProd.SelectedValue != "0")
                    request.ItemCode = cmbProd.SelectedValue;
                if (cmbRequestedBranch.SelectedValue != "0")
                    request.RequestedBranch = cmbRequestedBranch.SelectedValue;
                if (cmbBranchHasStock.SelectedValue != "0")
                    request.BranchHasStock = cmbBranchHasStock.SelectedValue;

                if (cmbStatus.SelectedValue != "0")
                    request.Status = cmbStatus.SelectedValue;

                if (!string.IsNullOrEmpty(txtRequestedDate.Text))
                    request.RequestedDate = Convert.ToDateTime(txtRequestedDate.Text);

                if (!string.IsNullOrEmpty(txtQtyAdd.Text))
                    request.Quantity = Convert.ToDecimal(txtQtyAdd.Text);

                request.UserID = UserID;

                bl.RaiseInternalTrasfer(connection, request);

                ModalPopupExtender1.Hide();

                BindGridData();

                contentPopUp.Visible = false;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Internal Transfer Request Saved Successfully.');", true);

            }
            catch (Exception ex)
            {
                TroyLiteExceptionManager.HandleException(ex);
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            rvSearch.Enabled = true;
            //Page.Validate();

            //if (Page.IsValid)
            //{
                BindGridData();
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);

        }
    }

    private void BindDropdowns()
    {
        string connection = Request.Cookies["Company"].Value;
        IInternalTransferService bl = new BusinessLogic(connection);

        var prodData = bl.GetProductList(connection);

        if (prodData != null)
        {
            cmbProd.DataSource = prodData;
            cmbProd.DataBind();
        }

        var branchData = bl.GetBranches(connection);

        if (branchData != null)
        {
            cmbRequestedBranch.DataSource = branchData;
            cmbRequestedBranch.DataBind();

            cmbBranchHasStock.DataSource = branchData;
            cmbBranchHasStock.DataBind();
        }
    }

    protected void GrdViewRequestes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string connection = Request.Cookies["Company"].Value;

            int RequestId = Convert.ToInt32(GrdViewRequestes.Rows[e.RowIndex].Cells[0].Text);

            IInternalTransferService bl = new BusinessLogic(connection);
            bl.DeleteInternalRequest(connection, RequestId);
            BindGridData();

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}