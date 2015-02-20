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
using System.Data.OleDb;

public partial class ManualClearance : System.Web.UI.Page
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

                loadSupplier("Sundry Debtors");

                GrdViewReceipt.PageSize = 6;

                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic();

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    //lnkBtnAdd.Visible = false;
                    //GrdViewReceipt.Columns[8].Visible = false;
                    //GrdViewReceipt.Columns[7].Visible = false;
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

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddoption.SelectedValue == "Customer")
            {
                loadSupplier("Sundry Debtors");
            }
            else
            {
                loadSupplier("Sundry Creditors");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddoption_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddoption.SelectedValue == "Customer")
            {
                loadSupplier("Sundry Debtors");
            }
            else
            {
                loadSupplier("Sundry Creditors");
            }
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

        ddReceivedFrom.Items.Clear();
        if (SundryType == "Sundry Debtors")
            ddReceivedFrom.Items.Add(new ListItem("Select Customer", "0"));
        else if (SundryType == "Sundry Creditors")
            ddReceivedFrom.Items.Add(new ListItem("Select Supplier", "0"));

        if (SundryType == "Sundry Creditors")
        {
            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            //ds = bl.ListSundryCreditorsSuppliers(sDataSource);
            if (ddCriteria.SelectedValue == "Cleared")
            {
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                var dsSales = bl.ListCreditPurchaseCleared(connStr.Trim());

                var receivedData = bl.GetSupplierReceivedAmount(connStr);

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
                                dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                                dsSales.Tables[0].Rows[i]["pay"] = val;
                                dsSales.Tables[0].Rows[i].EndEdit();

                                if (val != 0.0)
                                    dsSales.Tables[0].Rows[i].Delete();
                            }
                        }
                        dsSales.Tables[0].AcceptChanges();
                    }
                }

                string CustomerName = string.Empty;

                if (dsSales != null)
                {
                    for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                    {
                        if (CustomerName.Trim() == dsSales.Tables[0].Rows[i]["CustomerName"].ToString())
                        {
                            CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
                            dsSales.Tables[0].Rows[i].Delete();
                        }
                        else
                        {
                            CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
                        }
                        
                    }
                    dsSales.Tables[0].AcceptChanges();
                }

                if (dsSales != null)
                {
                    if (dsSales.Tables[0].Rows.Count > 0)
                    {
                        ddReceivedFrom.DataSource = dsSales;
                        ddReceivedFrom.DataBind();
                        ddReceivedFrom.DataTextField = "customerName";
                        ddReceivedFrom.DataValueField = "customerid";
                    }
                }
            }
            else if (ddCriteria.SelectedValue == "PartiallyCleared")
            {
                
            }          
            else if (ddCriteria.SelectedValue == "NotCleared")
            {
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                var dsSales = bl.ListCreditPurchaseCleared(connStr.Trim());

                var receivedData = bl.GetSupplierReceivedAmount(connStr);

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
                                dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                                dsSales.Tables[0].Rows[i]["pay"] = val;
                                dsSales.Tables[0].Rows[i].EndEdit();

                                if (val == 0.0)
                                    dsSales.Tables[0].Rows[i].Delete();
                            }
                        }
                        dsSales.Tables[0].AcceptChanges();
                    }
                }

                string CustomerName = string.Empty;

                if (dsSales != null)
                {
                    for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                    {
                        if (CustomerName.Trim() == dsSales.Tables[0].Rows[i]["CustomerName"].ToString())
                        {
                            CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
                            dsSales.Tables[0].Rows[i].Delete();
                        }
                        else
                        {
                            CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
                        }

                    }
                    dsSales.Tables[0].AcceptChanges();
                }

                

                var dsSales2 = bl.ListCreditPurchaseNotCleared(connStr.Trim());

                if (dsSales != null)
                {
                    if (dsSales2 != null)
                    {
                        dsSales.Tables[0].Merge(dsSales2.Tables[0]);
                    }
                }

                //string CustomerName2 = string.Empty;

                //if (dsSales2 != null)
                //{
                //    for (int i = 0; i < dsSales2.Tables[0].Rows.Count; i++)
                //    {
                //        if (CustomerName.Trim() == dsSales2.Tables[0].Rows[i]["CustomerName"].ToString())
                //        {
                //            CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
                //            dsSales.Tables[0].Rows[i].Delete();
                //        }
                //        else
                //        {
                //            CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
                //        }
                       
                //    }
                //    dsSales.Tables[0].AcceptChanges();
                //}

                if (dsSales != null)
                {
                    if (dsSales.Tables[0].Rows.Count > 0)
                    {
                        ddReceivedFrom.DataSource = dsSales;
                        ddReceivedFrom.DataBind();
                        ddReceivedFrom.DataTextField = "customerName";
                        ddReceivedFrom.DataValueField = "customerid";
                    }
                }
                else if (dsSales2 != null)
                {
                    if (dsSales2.Tables[0].Rows.Count > 0)
                    {
                        ddReceivedFrom.DataSource = dsSales2;
                        ddReceivedFrom.DataBind();
                        ddReceivedFrom.DataTextField = "customerName";
                        ddReceivedFrom.DataValueField = "customerid";
                    }
                }
            }
        }

        

        

        //if (SundryType == "Sundry Creditors")
        //{
        //    ddReceivedFrom.DataSource = ds;
        //    ddReceivedFrom.DataBind();
        //    ddReceivedFrom.DataTextField = "CustomerName";
        //    ddReceivedFrom.DataValueField = "CustomerID";
        //}


        if (SundryType == "Sundry Debtors")
        {
            string debtorID = ddReceivedFrom.SelectedValue;
            BusinessLogic objBus = new BusinessLogic();

            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            var customerID = ddReceivedFrom.SelectedValue.Trim();

            pnlPopup.Visible = true;
            Panel1.Visible = false;
            Div1.Visible = false;

            if (ddCriteria.SelectedValue == "Cleared")
            {
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                
                var dsSales = bl.ListCreditSalesCleared(connStr.Trim());

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
                                dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                                dsSales.Tables[0].Rows[i]["pay"] = val;
                                dsSales.Tables[0].Rows[i].EndEdit();

                                if (val != 0.0)
                                    dsSales.Tables[0].Rows[i].Delete();
                            }
                        }
                        dsSales.Tables[0].AcceptChanges();
                    }

                }
                string CustomerName = string.Empty;

                if (dsSales != null)
                {
                    for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                    {
                        if (CustomerName.Trim() == dsSales.Tables[0].Rows[i]["CustomerName"].ToString())
                        {
                            CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
                            dsSales.Tables[0].Rows[i].Delete();
                        }
                        else
                        {
                            CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
                        }
                        
                    }
                    dsSales.Tables[0].AcceptChanges();
                }

                if (dsSales != null)
                {
                    if (dsSales.Tables[0].Rows.Count > 0)
                    {
                        ddReceivedFrom.DataSource = dsSales;
                        ddReceivedFrom.DataBind();
                        ddReceivedFrom.DataTextField = "customerName";
                        ddReceivedFrom.DataValueField = "customerid";
                    }
                }
                
            }
            //else if (ddCriteria.SelectedValue == "PartiallyCleared")
            //{
            //    if (Request.Cookies["Company"] != null)
            //        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            //    else
            //        Response.Redirect("~/Login.aspx");

            //    var dsSales = bl.ListCreditSalesClearedCustomerOdr(connStr.Trim());

            //    var receivedData = bl.GetCustReceivedAmount(connStr);

            //    if (dsSales != null)
            //    {
            //        foreach (DataRow dr in receivedData.Tables[0].Rows)
            //        {
            //            var billNo = dr["BillNo"].ToString();
            //            var billAmount = dr["TotalAmount"].ToString();

            //            for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
            //            {
            //                if (billNo.Trim() == dsSales.Tables[0].Rows[i]["BillNo"].ToString())
            //                {
            //                    dsSales.Tables[0].Rows[i].BeginEdit();
            //                    double val = (double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
            //                    dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
            //                    dsSales.Tables[0].Rows[i]["pay"] = val;
            //                    dsSales.Tables[0].Rows[i].EndEdit();

            //                    if (val == 0.0)
            //                        dsSales.Tables[0].Rows[i].Delete();
            //                }
            //            }
            //            dsSales.Tables[0].AcceptChanges();
            //        }
            //    }

            //    string CustomerName = string.Empty;

            //    //DataTable dt = new DataTable();
            //    //dt.Columns.Add(new DataColumn("BillNo"));
            //    //dt.Columns.Add(new DataColumn("BillDate"));
            //    //dt.Columns.Add(new DataColumn("CustomerName"));
            //    //dt.Columns.Add(new DataColumn("CustomerID"));
            //    //dt.Columns.Add(new DataColumn("Amount"));
            //    //dt.Columns.Add(new DataColumn("Pay"));

            //    //if (dsSales != null)
            //    //{
            //    //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    //    {                        
            //    //        if (CustomerName.Trim() == dr["CustomerName"].ToString())
            //    //        {

            //    //        }
            //    //        else
            //    //        {
            //    //            DataRow dr_final1 = dt.NewRow();
            //    //            dr_final1["BillNo"] = dr["BillNo"];
            //    //            dr_final1["BillDate"] = dr["BillDate"];
            //    //            dr_final1["CustomerID"] = dr["CustomerID"];
            //    //            dr_final1["Amount"] = dr["Amount"];
            //    //            dr_final1["Pay"] = dr["Pay"];
            //    //            dt.Rows.Add(dr_final1);                                   
            //    //        }
            //    //        CustomerName = dr["CustomerName"].ToString();
            //    //    }
            //    //}
            //    //DataSet dsddd = new DataSet();
            //    ////dsddd = dt.tables[0];
            //    //dsddd.Tables.Add(dt);


            //    if (dsSales != null)
            //    {
            //        for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
            //        {                        
            //            if (CustomerName.Trim() == dsSales.Tables[0].Rows[i]["CustomerName"].ToString())
            //            {
            //                CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
            //                dsSales.Tables[0].Rows[i].Delete();
            //            }
            //            else
            //            {
            //                CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
            //            }
                        
            //        }
            //        dsSales.Tables[0].AcceptChanges();
            //    }

            //    if (dsSales != null)
            //    {
            //        if (dsSales.Tables[0].Rows.Count > 0)
            //        {
            //            ddReceivedFrom.DataSource = dsSales;
            //            ddReceivedFrom.DataBind();
            //            ddReceivedFrom.DataTextField = "customerName";
            //            ddReceivedFrom.DataValueField = "customerid";
            //        }
            //    }
            //}
            else if (ddCriteria.SelectedValue == "NotCleared")
            {
                var dsSales = bl.ListCreditSalesClearedCustomerOdr(connStr.Trim());

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
                                dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                                dsSales.Tables[0].Rows[i]["pay"] = val;
                                dsSales.Tables[0].Rows[i].EndEdit();

                                if (val == 0.0)
                                    dsSales.Tables[0].Rows[i].Delete();
                            }
                        }
                        dsSales.Tables[0].AcceptChanges();
                    }
                }

                var dsSalesd = bl.ListCreditSalesNotCleared(connStr.Trim());

                if (dsSales != null)
                {
                    if (dsSalesd != null)
                    {
                        dsSales.Tables[0].Merge(dsSalesd.Tables[0]);
                    }
                }
                string CustomerName = string.Empty;

                if (dsSales != null)
                {
                    for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                    {
                        if (CustomerName.Trim() == dsSales.Tables[0].Rows[i]["CustomerName"].ToString())
                        {
                            CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
                            dsSales.Tables[0].Rows[i].Delete();
                        }
                        else
                        {
                            CustomerName = dsSales.Tables[0].Rows[i]["CustomerName"].ToString();
                        }

                    }
                    dsSales.Tables[0].AcceptChanges();
                }
                else if (dsSalesd != null)
                {
                    for (int i = 0; i < dsSalesd.Tables[0].Rows.Count; i++)
                    {
                        if (CustomerName.Trim() == dsSalesd.Tables[0].Rows[i]["CustomerName"].ToString())
                        {
                            CustomerName = dsSalesd.Tables[0].Rows[i]["CustomerName"].ToString();
                            dsSalesd.Tables[0].Rows[i].Delete();
                        }
                        else
                        {
                            CustomerName = dsSalesd.Tables[0].Rows[i]["CustomerName"].ToString();
                        }

                    }
                    dsSalesd.Tables[0].AcceptChanges();
                }

                if (dsSales != null)
                {
                    if (dsSales.Tables[0].Rows.Count > 0)
                    {
                        ddReceivedFrom.DataSource = dsSales;
                        ddReceivedFrom.DataBind();
                        ddReceivedFrom.DataTextField = "customerName";
                        ddReceivedFrom.DataValueField = "customerid";
                    }
                }
                else if (dsSalesd != null)
                {
                    if (dsSalesd.Tables[0].Rows.Count > 0)
                    {
                        ddReceivedFrom.DataSource = dsSalesd;
                        ddReceivedFrom.DataBind();
                        ddReceivedFrom.DataTextField = "customerName";
                        ddReceivedFrom.DataValueField = "customerid";
                    }
                }
            }           
        }

        
    }

    private void ShowPendingPurBills()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        BusinessLogic bl = new BusinessLogic();
        var customerID = ddReceivedFrom.SelectedValue.Trim();

        var dsSales = bl.ListCreditPurchaseCleared(connStr.Trim(), customerID);

        var receivedData = bl.GetSupplierReceivedAmount(connStr);

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
                        dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                        dsSales.Tables[0].Rows[i]["pay"] = val;
                        dsSales.Tables[0].Rows[i].EndEdit();

                        if (val == 0.0)
                            dsSales.Tables[0].Rows[i].Delete();
                    }
                }
                dsSales.Tables[0].AcceptChanges();
            }
        }

        if (dsSales != null)
        {
            if (dsSales.Tables[0].Rows.Count > 0)
            {
                GrdViewSales.DataSource = dsSales;
                GrdViewSales.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Date Found')", true);
                ModalPopupExtender2.Hide();
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Date Found')", true);
            ModalPopupExtender2.Hide();
            return;
        }

    }

    private void ShowPendingBills()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        BusinessLogic bl = new BusinessLogic();
        int customerID = Convert.ToInt32(ddReceivedFrom.SelectedValue);

        string ledID = ddReceivedFrom.SelectedValue.Trim();
        var dsSales = bl.ListCreditSalesCleared(connStr.Trim(), ledID);

        var receivedDat = bl.GetCustReceivedAmount(connStr);

        if (dsSales != null)
        {
            foreach (DataRow dr in receivedDat.Tables[0].Rows)
            {
                var billNo = dr["BillNo"].ToString();
                var billAmount = dr["TotalAmount"].ToString();

                for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                {
                    if (billNo.Trim() == dsSales.Tables[0].Rows[i]["BillNo"].ToString())
                    {
                        dsSales.Tables[0].Rows[i].BeginEdit();
                        double val = (double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
                        dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                        dsSales.Tables[0].Rows[i]["pay"] = val;
                        dsSales.Tables[0].Rows[i].EndEdit();

                        if (val == 0.0)
                            dsSales.Tables[0].Rows[i].Delete();
                    }
                }
                dsSales.Tables[0].AcceptChanges();
            }
        }

        if (dsSales != null)
        {
            if (dsSales.Tables[0].Rows.Count > 0)
            {
                GrdCreditSales.DataSource = dsSales;
                GrdCreditSales.DataBind();
            }
            else
            {
                GrdCreditSales.DataSource = null;
                GrdCreditSales.DataBind();
            }
        }
        else
        {
            GrdCreditSales.DataSource = null;
            GrdCreditSales.DataBind();
        }


        var receivedData = bl.GetReceiptForLedger(connStr, customerID);
        if (receivedData != null)
        {
            if (receivedData.Tables[0].Rows.Count > 0)
            {
                GrdViewSales.DataSource = receivedData;
                GrdViewSales.DataBind();
            }
            else
            {
                GrdViewSales.DataSource = null;
                GrdViewSales.DataBind();
            }
        }
        else
        {
            GrdViewSales.DataSource = null;
            GrdViewSales.DataBind();
        }

    }

    private void ShowBills()
    {


        if (ddoption.SelectedValue == "Customer")
        {
            string debtorID = ddReceivedFrom.SelectedValue;
            BusinessLogic objBus = new BusinessLogic();

            string Mobile = objBus.GetLedgerMobileForId(Request.Cookies["Company"].Value, int.Parse(debtorID));

            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            BusinessLogic bl = new BusinessLogic();
            var customerID = ddReceivedFrom.SelectedValue.Trim();

            pnlPopup.Visible = true;
            Panel1.Visible = false;
            Div1.Visible = false;

            if (ddCriteria.SelectedValue == "FullyCleared")
            {
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                var dsSales = bl.ListCreditSalesCleared(connStr.Trim(), customerID);

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
                                dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                                dsSales.Tables[0].Rows[i]["pay"] = val;
                                dsSales.Tables[0].Rows[i].EndEdit();

                                if (val != 0.0)
                                    dsSales.Tables[0].Rows[i].Delete();
                            }
                        }
                        dsSales.Tables[0].AcceptChanges();
                    }
                }

                if (dsSales != null)
                {
                    if (dsSales.Tables[0].Rows.Count > 0)
                    {
                        DropDownList ddl = (DropDownList)GrdBills.FindControl("txtBillNo");
                        DropDownList ddlll = (DropDownList)GrdBills.FooterRow.FindControl("txtAddBillNo");
                        ddlll.DataSource = dsSales;
                        ddlll.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found')", true);
                        ModalPopupExtender2.Hide();
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found')", true);
                    ModalPopupExtender2.Hide();
                    return;
                }
            }
            else if (ddCriteria.SelectedValue == "PartiallyCleared")
            {
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                var dsSales = bl.ListCreditSalesCleared(connStr.Trim(), customerID);

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
                                dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                                dsSales.Tables[0].Rows[i]["pay"] = val;
                                dsSales.Tables[0].Rows[i].EndEdit();

                                if (val == 0.0)
                                    dsSales.Tables[0].Rows[i].Delete();
                            }
                        }
                        dsSales.Tables[0].AcceptChanges();
                    }
                }

                if (dsSales != null)
                {
                    if (dsSales.Tables[0].Rows.Count > 0)
                    {
                        DropDownList ddl = (DropDownList)GrdBills.FindControl("txtBillNo");
                        DropDownList ddlll = (DropDownList)GrdBills.FooterRow.FindControl("txtAddBillNo");
                        ddlll.DataSource = dsSales;
                        ddlll.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found')", true);
                        ModalPopupExtender2.Hide();
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found')", true);
                    ModalPopupExtender2.Hide();
                    return;
                }
            }
            else if (ddCriteria.SelectedValue == "NotCleared")
            {
                var dsSales = bl.ListCreditSalesNotCleared(connStr.Trim(), customerID);

                if (dsSales != null)
                {
                    if (dsSales.Tables[0].Rows.Count > 0)
                    {
                        DropDownList ddl = (DropDownList)GrdBills.FindControl("txtBillNo");
                        DropDownList ddlll = (DropDownList)GrdBills.FooterRow.FindControl("txtAddBillNo");
                        ddlll.DataSource = dsSales;
                        ddlll.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found')", true);
                        ModalPopupExtender2.Hide();
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Data Found')", true);
                    ModalPopupExtender2.Hide();
                    return;
                }
            }
        }
    }

    private void ShowFullBills()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        BusinessLogic bl = new BusinessLogic();
        var customerID = ddReceivedFrom.SelectedValue.Trim();

        int ledID = Convert.ToInt32(ddReceivedFrom.SelectedValue);

        var dsSales = bl.ListCreditSalesCleared(connStr.Trim(), customerID);

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
                        dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                        dsSales.Tables[0].Rows[i]["pay"] = val;
                        dsSales.Tables[0].Rows[i].EndEdit();

                        if (val != 0.0)
                            dsSales.Tables[0].Rows[i].Delete();
                    }
                }
                dsSales.Tables[0].AcceptChanges();
            }

        }
        if (dsSales != null)
        {
            if (dsSales.Tables[0].Rows.Count > 0)
            {
                GrdCreditSales.DataSource = dsSales;
                GrdCreditSales.DataBind();
            }
            else
            {
                GrdCreditSales.DataSource = null;
                GrdCreditSales.DataBind();             
            }
        }
        else
        {
            GrdCreditSales.DataSource = null;
            GrdCreditSales.DataBind();        
        }

        var received = bl.GetReceiptForLedger(connStr, ledID);
        if (received != null)
        {
            if (received.Tables[0].Rows.Count > 0)
            {
                GrdViewSales.DataSource = received;
                GrdViewSales.DataBind();
            }
            else
            {
                GrdViewSales.DataSource = null;
                GrdViewSales.DataBind();
            }
        }
        else
        {
            GrdViewSales.DataSource = null;
            GrdViewSales.DataBind();
        }

    }

    private void ShowFullPurBills()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        BusinessLogic bl = new BusinessLogic();
        var customerID = ddReceivedFrom.SelectedValue.Trim();

        var dsSales = bl.ListCreditPurchaseCleared(connStr.Trim(), customerID);

        var receivedData = bl.GetSupplierReceivedAmount(connStr);

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
                        dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                        dsSales.Tables[0].Rows[i]["pay"] = val;
                        dsSales.Tables[0].Rows[i].EndEdit();

                        if (val != 0.0)
                            dsSales.Tables[0].Rows[i].Delete();
                    }
                }
                dsSales.Tables[0].AcceptChanges();
            }
        }
        if (dsSales != null)
        {
            if (dsSales.Tables[0].Rows.Count > 0)
            {
                GrdViewSales.DataSource = dsSales;
                GrdViewSales.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Date Found')", true);
                ModalPopupExtender2.Hide();
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Date Found')", true);
            ModalPopupExtender2.Hide();
            return;
        }
    }

    private void loadBanks()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBanks();

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

    //protected override void OnInit(EventArgs e)
    //{
    //    base.OnInit(e);
    //    //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
    //    GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
    //    //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
    //    GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
    //    GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));
    //}

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
            GridViewRow Row = GrdViewReceipt.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();
            string recondate = Row.Cells[2].Text;
            Session["BillData"] = null;
            //hd.Value = Convert.ToString(GrdViewReceipt.SelectedDataKey.Value);

            if (!bl.IsValidDate(connection, Convert.ToDateTime(recondate)))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date is invalid')", true);
                return;
            }
            else
            {
                //pnlEdit.Visible = true;
                DataSet ds = bl.GetReceiptForId(connection, int.Parse(GrdViewReceipt.SelectedDataKey.Value.ToString()));
                if (ds != null)
                {
                    //txtRefNo.Text = ds.Tables[0].Rows[0]["RefNo"].ToString();
                    //txtTransDate.Text = DateTime.Parse(ds.Tables[0].Rows[0]["TransDate"].ToString()).ToShortDateString();

                    //ddReceivedFrom.SelectedValue = ds.Tables[0].Rows[0]["CreditorID"].ToString();
                    //txtAmount.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
                    //txtMobile.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    //chkPayTo.SelectedValue = ds.Tables[0].Rows[0]["paymode"].ToString();
                    //txtNarration.Text = ds.Tables[0].Rows[0]["Narration"].ToString();
                    //if (chkPayTo.SelectedItem != null)
                    //{
                    //    if (chkPayTo.SelectedItem.Text == "Cheque")
                    //    {
                    //        tblBank.Attributes.Add("class", "AdvancedSearch");
                    //    }
                    //    else
                    //    {
                    //        tblBank.Attributes.Add("class", "hidden");
                    //    }
                    //}
                    //else
                    //{
                    //    tblBank.Attributes.Add("class", "hidden");
                    //}

                    //txtChequeNo.Text = ds.Tables[0].Rows[0]["ChequeNo"].ToString();

                    //string creditorID = ds.Tables[0].Rows[0]["DebtorID"].ToString();

                    //ddBanks.ClearSelection();

                    //ListItem li = ddBanks.Items.FindByValue(creditorID);
                    //if (li != null) li.Selected = true;

                    DataSet billsData = bl.GetReceivedAmountId(connection, int.Parse(GrdViewReceipt.SelectedDataKey.Value.ToString()));

                    Session["BillData"] = billsData;

                    if (billsData.Tables[0].Rows[0]["BillNo"].ToString() == "0")
                    {
                        billsData = null;
                    }
                    GrdBills.DataSource = billsData;
                    GrdBills.DataBind();
                    Session["RMode"] = "Edit";
                    ShowPendingBills();
                    checkPendingBills(billsData);
                }

                //GrdViewReceipt.Visible = false;
                ////MyAccordion.Visible = false;
                //lnkBtnAdd.Visible = false;
                pnlEdit.Visible = true;
                UpdateButton.Visible = true;
                SaveButton.Visible = false;
                ModalPopupExtender2.Show();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdBillsCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GrdBills.EditIndex = -1;
        if (Session["BillData"] != null)
        {
            GrdBills.DataSource = (DataSet)Session["BillData"];
            GrdBills.DataBind();
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

            //if (ddReceivedFrom.SelectedValue == "0")
            //{
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select the Customer before Adding Bills')", true);
            //    //pnlEdit.Visible = true;
            //    txtAmount.Focus();
            //    return;
            //}

            if (GrdBills.Rows.Count == 0)
            {
                if (ddoption.SelectedValue == "Customer")
                {
                    var ds = bl.GetReceivedAmountId(conn, -1);
                    GrdBills.DataSource = ds;
                    GrdBills.DataBind();
                    GrdBills.Rows[0].Visible = false;
                    checkPendingBills(ds);
                    Session["BillData"] = null;
                }
                else
                {
                    var ds = bl.GetPayAmountId(conn, -1);
                    GrdBills.DataSource = ds;
                    GrdBills.DataBind();
                    GrdBills.Rows[0].Visible = false;
                    checkPendingBills(ds);
                    Session["BillData"] = null;
                }
            }
            pnlEdit.Visible = true;
            GrdBills.FooterRow.Visible = true;


            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            var customerID = ddReceivedFrom.SelectedValue.Trim();
            var dsSales = bl.ListCreditSalesNotCleared(connStr.Trim(), customerID);



            var dsd = bl.ListCreditSalesCleared(connStr.Trim(), customerID);
            var receivedData = bl.GetCustReceivedAmount(connStr);
            if (dsd != null)
            {
                foreach (DataRow dr in receivedData.Tables[0].Rows)
                {
                    var billNo = dr["BillNo"].ToString();
                    var billAmount = dr["TotalAmount"].ToString();

                    for (int i = 0; i < dsd.Tables[0].Rows.Count; i++)
                    {
                        if (billNo.Trim() == dsd.Tables[0].Rows[i]["BillNo"].ToString())
                        {
                            dsd.Tables[0].Rows[i].BeginEdit();
                            double val = (double.Parse(dsd.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
                            dsd.Tables[0].Rows[i]["Amount"] = double.Parse(dsd.Tables[0].Rows[i]["Amount"].ToString());
                            dsd.Tables[0].Rows[i]["pay"] = val;
                            dsd.Tables[0].Rows[i].EndEdit();

                            if (val == 0.0)
                                dsd.Tables[0].Rows[i].Delete();
                        }
                    }
                    dsd.Tables[0].AcceptChanges();
                }
            }


            if ((dsd != null) && (dsSales != null))
            {
                dsSales.Tables[0].Merge(dsd.Tables[0]);
            }
            else if ((dsd == null) && (dsSales != null))
            {

            }
            else if ((dsd != null) && (dsSales == null))
            {

            }

            if ((dsd != null) && (dsSales != null))
            {
                if (dsSales.Tables[0].Rows.Count > 0)
                {
                    DropDownList ddl = (DropDownList)GrdBills.FindControl("txtBillNo");
                    DropDownList ddlll = (DropDownList)GrdBills.FooterRow.FindControl("txtAddBillNo");
                    ddlll.DataSource = dsSales;
                    ddlll.DataBind();
                }
            }
            else if ((dsd == null) && (dsSales != null))
            {
                if (dsSales.Tables[0].Rows.Count > 0)
                {
                    DropDownList ddl = (DropDownList)GrdBills.FindControl("txtBillNo");
                    DropDownList ddlll = (DropDownList)GrdBills.FooterRow.FindControl("txtAddBillNo");
                    ddlll.DataSource = dsSales;
                    ddlll.DataBind();
                }
            }
            else if ((dsd != null) && (dsSales == null))
            {
                if (dsd.Tables[0].Rows.Count > 0)
                {
                    DropDownList ddl = (DropDownList)GrdBills.FindControl("txtBillNo");
                    DropDownList ddlll = (DropDownList)GrdBills.FooterRow.FindControl("txtAddBillNo");
                    ddlll.DataSource = dsd;
                    ddlll.DataBind();
                }
            }



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

    protected void ddlPageSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GrdViewSales.PageIndex = ((DropDownList)sender).SelectedIndex;

            ShowPendingBills();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
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

    protected void GrdViewSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }


    protected void GrdCreditSales_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    
    protected void GrdViewSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewSales.PageIndex = e.NewPageIndex;

            ShowPendingBills();
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

    //protected void lnkBtnAdd_Click(object sender, EventArgs e)
    //{

    //    if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
    //        return;
    //    }
    //    ModalPopupExtender2.Show();
    //    pnlEdit.Visible = true;
    //    //lnkBtnAdd.Visible = false;
    //    ////MyAccordion.Visible = false;
    //    //GrdViewReceipt.Visible = false;
    //    UpdateButton.Visible = false;
    //    SaveButton.Visible = true;
    //    Panel1.Visible = false;
    //    Div1.Visible = false;
    //    ClearPanel();
    //    ShowPendingBills();
    //    //txtTransDate.Text = DateTime.Now.ToShortDateString();
    //    //txtRefNo.Focus();
    //    //chkPayTo.SelectedValue = "Cash";

    //    //if (chkPayTo.SelectedItem != null)
    //    //{
    //    //    if (chkPayTo.SelectedItem.Text == "Cheque")
    //    //    {
    //    //        tblBank.Attributes.Add("class", "AdvancedSearch");
    //    //    }
    //    //    else
    //    //    {
    //    //        tblBank.Attributes.Add("class", "hidden");
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    if (tblBank != null)
    //    //        tblBank.Attributes.Add("class", "hidden");
    //    //}
    //}

    private void ClearPanel()
    {
        //txtRefNo.Text = "";
        //txtTransDate.Text = "";
        //txtNarration.Text = "";
        //txtChequeNo.Text = "";
        //txtAmount.Text = "";
        //ddReceivedFrom.SelectedValue = "0";
        //txtMobile.Text = "";
        //ddBanks.SelectedValue = "0";

        ddReceivedFrom.SelectedValue = "0";

        //GrdViewSales.DataSource = null;
        //GrdViewSales.DataBind();
        GrdBills.DataSource = null;
        GrdBills.DataBind();
        Session["BillData"] = null;
    }

    protected void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {

        

    }

    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupContact.Hide();
            ModalPopupExtender2.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewSales_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connStr = string.Empty;

            txtBillNo1.Items.Clear();
            //txtBillNo1.Items.Add("Select BillNo");
            txtBillNo1.Items.Add(new ListItem("Select BillNo", "0"));

            txtamount.Text = "";
            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            var customerID = ddReceivedFrom.SelectedValue.Trim();
            if (ddoption.SelectedValue == "Customer")
            {
                if (ddCriteria.SelectedValue == "Cleared")
                {
                    int ledID = Convert.ToInt32(ddReceivedFrom.SelectedValue);

                    var dsSales = bl.ListCreditSalesCleared(connStr.Trim(), customerID);

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
                                    dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                                    dsSales.Tables[0].Rows[i]["pay"] = val;
                                    dsSales.Tables[0].Rows[i].EndEdit();

                                    if (val != 0.0)
                                        dsSales.Tables[0].Rows[i].Delete();
                                }
                            }
                            dsSales.Tables[0].AcceptChanges();
                        }

                    }
                    if (dsSales != null)
                    {
                        if (dsSales.Tables[0].Rows.Count > 0)
                        {
                            txtBillNo1.DataSource = dsSales;
                            txtBillNo1.DataBind();
                        }
                        else
                        {
                            txtBillNo1.DataSource = null;
                            txtBillNo1.DataBind();
                        }
                    }
                    else
                    {
                        txtBillNo1.DataSource = null;
                        txtBillNo1.DataBind();
                    }
                }
                else if ((ddCriteria.SelectedValue == "PartiallyCleared") || (ddCriteria.SelectedValue == "NotCleared"))
                {
                    string ledID = ddReceivedFrom.SelectedValue.Trim();
                    var dsSales = bl.ListCreditSalesCleared(connStr.Trim(), ledID);

                    var receivedDat = bl.GetCustReceivedAmount(connStr);

                    if (dsSales != null)
                    {
                        foreach (DataRow dr in receivedDat.Tables[0].Rows)
                        {
                            var billNo = dr["BillNo"].ToString();
                            var billAmount = dr["TotalAmount"].ToString();

                            for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                            {
                                if (billNo.Trim() == dsSales.Tables[0].Rows[i]["BillNo"].ToString())
                                {
                                    dsSales.Tables[0].Rows[i].BeginEdit();
                                    double val = (double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
                                    dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                                    dsSales.Tables[0].Rows[i]["pay"] = val;
                                    dsSales.Tables[0].Rows[i].EndEdit();

                                    if (val == 0.0)
                                        dsSales.Tables[0].Rows[i].Delete();
                                }
                            }
                            dsSales.Tables[0].AcceptChanges();
                        }
                    }
                    var dsSal = bl.ListCreditSalesNotCleared(connStr.Trim(), customerID);
                    if (dsSales != null)
                    {
                        if (dsSal != null)
                        {
                            dsSales.Tables[0].Merge(dsSal.Tables[0]);
                        }
                        if (dsSales.Tables[0].Rows.Count > 0)
                        {
                            txtBillNo1.DataSource = dsSales;
                            txtBillNo1.DataBind();


                        }
                        else
                        {
                            txtBillNo1.DataSource = null;
                            txtBillNo1.DataBind();
                        }
                    }
                    else
                    {
                        if (dsSal != null)
                        {
                            if (dsSal.Tables[0].Rows.Count > 0)
                            {
                                txtBillNo1.DataSource = dsSal;
                                txtBillNo1.DataBind();
                            }
                            else
                            {
                                txtBillNo1.DataSource = null;
                                txtBillNo1.DataBind();
                            }
                        }
                        else
                        {
                            txtBillNo1.DataSource = null;
                            txtBillNo1.DataBind();
                        }
                    }
                }
            }
            else
            {
                var SupplierID = ddReceivedFrom.SelectedValue.Trim();
                pnlPopup.Visible = true;

                int ledgerID = Convert.ToInt32(ddReceivedFrom.SelectedValue);

                if (ddCriteria.SelectedValue == "Cleared")
                {
                    ShowFullPurBills();
                }
                //else if (ddCriteria.SelectedValue == "PartiallyCleared")
                //{
                //    ShowPendingPurBills();
                //}
                else if (ddCriteria.SelectedValue == "NotCleared")
                {

                    if (Request.Cookies["Company"] != null)
                        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                    else
                        Response.Redirect("~/Login.aspx");

                    var customerIDD = ddReceivedFrom.SelectedValue.Trim();

                    var dsSales = bl.ListCreditPurchaseCleared(connStr.Trim(), customerIDD);

                    var receivedData = bl.GetSupplierReceivedAmount(connStr);

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
                                    dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                                    dsSales.Tables[0].Rows[i]["pay"] = val;
                                    dsSales.Tables[0].Rows[i].EndEdit();

                                    if (val == 0.0)
                                        dsSales.Tables[0].Rows[i].Delete();
                                }
                            }
                            dsSales.Tables[0].AcceptChanges();
                        }
                    }

                    var dsSalesd = bl.ListCreditPurchaseNotCleared(connStr.Trim(), SupplierID);

                    if (dsSales != null)
                    {
                        if (dsSales.Tables[0].Rows.Count > 0)
                        {
                            if (dsSalesd != null)
                            {
                                dsSales.Tables[0].Merge(dsSalesd.Tables[0]);
                            }
                            txtBillNo1.DataSource = dsSales;
                            txtBillNo1.DataBind();
                        }
                        else
                        {
                            txtBillNo1.DataSource = null;
                            txtBillNo1.DataBind();
                        }
                    }
                    else
                    {
                        if (dsSalesd.Tables[0].Rows.Count > 0)
                        {
                            txtBillNo1.DataSource = dsSalesd;
                            txtBillNo1.DataBind();
                        }
                        else
                        {
                            txtBillNo1.DataSource = null;
                            txtBillNo1.DataBind();
                        }
                    }
                }
            }
            //else if (ddCriteria.SelectedValue == "NotCleared")
            //{
            //    var dsSales = bl.ListCreditSalesNotCleared(connStr.Trim(), customerID);
            //    if (dsSales != null)
            //    {
            //        if (dsSales.Tables[0].Rows.Count > 0)
            //        {
            //            txtBillNo1.DataSource = dsSales;
            //            txtBillNo1.DataBind();
            //        }
            //        else
            //        {
            //            txtBillNo1.DataSource = null;
            //            txtBillNo1.DataBind();
            //        }
            //    }
            //    else
            //    {
            //        txtBillNo1.DataSource = null;
            //        txtBillNo1.DataBind();
            //    }
            //}

            ModalPopupContact.Show();
            updatePnlContact.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void txtBillNo1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            int BillNo = Convert.ToInt32(txtBillNo1.SelectedValue);

            if (ddoption.SelectedValue == "Customer")
            {
                DataSet dbill = bl.GetBillDetail(connStr.Trim(), BillNo);

                if (dbill != null)
                {
                    foreach (DataRow dr in dbill.Tables[0].Rows)
                    {
                        TextBox1.Text = dr["Amount"].ToString();
                        TextBox2.Text = dr["pay"].ToString();
                    }
                }
            }
            else
            {
                DataSet dbill = bl.GetBillDetailPurchase(connStr.Trim(), BillNo);

                if (dbill != null)
                {
                    foreach (DataRow dr in dbill.Tables[0].Rows)
                    {
                        TextBox1.Text = dr["Amount"].ToString();
                        TextBox2.Text = dr["pay"].ToString();
                    }
                }
            }

            //var customerID = ddReceivedFrom.SelectedValue.Trim();

            //if (ddCriteria.SelectedValue == "FullyCleared")
            //{
            //    int ledID = Convert.ToInt32(ddReceivedFrom.SelectedValue);

            //    var dsSales = bl.ListCreditSalesCleared(connStr.Trim(), customerID);

            //    var receivedData = bl.GetCustReceivedAmount(connStr);

            //    if (dsSales != null)
            //    {
            //        foreach (DataRow dr in receivedData.Tables[0].Rows)
            //        {
            //            var billNo = dr["BillNo"].ToString();
            //            var billAmount = dr["TotalAmount"].ToString();

            //            for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
            //            {
            //                if (billNo.Trim() == dsSales.Tables[0].Rows[i]["BillNo"].ToString())
            //                {
            //                    dsSales.Tables[0].Rows[i].BeginEdit();
            //                    double val = (double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
            //                    dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
            //                    dsSales.Tables[0].Rows[i]["pay"] = val;
            //                    dsSales.Tables[0].Rows[i].EndEdit();

            //                    if (val != 0.0)
            //                        dsSales.Tables[0].Rows[i].Delete();
            //                }
            //            }
            //            dsSales.Tables[0].AcceptChanges();
            //        }

            //    }
            //    if (dsSales != null)
            //    {
            //        if (dsSales.Tables[0].Rows.Count > 0)
            //        {
            //            txtBillNo.DataSource = dsSales;
            //            txtBillNo.DataBind();
            //        }
            //        else
            //        {
            //            txtBillNo.DataSource = null;
            //            txtBillNo.DataBind();
            //        }
            //    }
            //    else
            //    {
            //        txtBillNo.DataSource = null;
            //        txtBillNo.DataBind();
            //    }
            //}
            //else if (ddCriteria.SelectedValue == "PartiallyCleared")
            //{
            //    string ledID = ddReceivedFrom.SelectedValue.Trim();
            //    var dsSales = bl.ListCreditSalesCleared(connStr.Trim(), ledID);

            //    var receivedDat = bl.GetCustReceivedAmount(connStr);

            //    if (dsSales != null)
            //    {
            //        foreach (DataRow dr in receivedDat.Tables[0].Rows)
            //        {
            //            var billNo = dr["BillNo"].ToString();
            //            var billAmount = dr["TotalAmount"].ToString();

            //            for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
            //            {
            //                if (billNo.Trim() == dsSales.Tables[0].Rows[i]["BillNo"].ToString())
            //                {
            //                    dsSales.Tables[0].Rows[i].BeginEdit();
            //                    double val = (double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
            //                    dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
            //                    dsSales.Tables[0].Rows[i]["pay"] = val;
            //                    dsSales.Tables[0].Rows[i].EndEdit();

            //                    if (val == 0.0)
            //                        dsSales.Tables[0].Rows[i].Delete();
            //                }
            //            }
            //            dsSales.Tables[0].AcceptChanges();
            //        }
            //    }

            //    if (dsSales != null)
            //    {
            //        if (dsSales.Tables[0].Rows.Count > 0)
            //        {
            //            txtBillNo.DataSource = dsSales;
            //            txtBillNo.DataBind();
            //        }
            //        else
            //        {
            //            txtBillNo.DataSource = null;
            //            txtBillNo.DataBind();
            //        }
            //    }
            //    else
            //    {
            //        txtBillNo.DataSource = null;
            //        txtBillNo.DataBind();
            //    }
            //}
            //else if (ddCriteria.SelectedValue == "NotCleared")
            //{
            //    var dsSales = bl.ListCreditSalesNotCleared(connStr.Trim(), customerID);
            //    if (dsSales != null)
            //    {
            //        if (dsSales.Tables[0].Rows.Count > 0)
            //        {
            //            txtBillNo.DataSource = dsSales;
            //            txtBillNo.DataBind();
            //        }
            //        else
            //        {
            //            txtBillNo.DataSource = null;
            //            txtBillNo.DataBind();
            //        }
            //    }
            //    else
            //    {
            //        txtBillNo.DataSource = null;
            //        txtBillNo.DataBind();
            //    }
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void InsertButton_Click(object sender, EventArgs e)
    {
        try
        {
            string con = Request.Cookies["Company"].Value;

            string itemCode = string.Empty;
            string dbQry = string.Empty;
            int Transno = int.Parse(GrdViewSales.SelectedDataKey.Value.ToString());

            DataSet ds = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            string username = Request.Cookies["LoggedUserName"].Value;
            int BillNo = Convert.ToInt32(txtBillNo1.SelectedValue);
            double Amount = Convert.ToDouble(txtamount.Text);

            if (ddoption.SelectedValue == "Customer")
            {
                bl.InsertReceivedAmt(con, Transno, BillNo, Amount, username);
            }
            else
            {
                bl.InsertPaymentAmt(con, Transno, BillNo, Amount, username);
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Bill No " + BillNo + " Adjusted Successfully');", true);
            ModalPopupContact.Hide();
            //UpdatePanelPage.Update();
            //btnSearch_Click(sender, e);
            BindGrid();
            UpdatePanel8.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected void GrdViewSales_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        BusinessLogic objChk = new BusinessLogic();

    //        //using (OleDbConnection connection = new OleDbConnection(objChk.CreateConnectionString(sDataSource)))
    //        //{
    //        //    OleDbCommand command = new OleDbCommand();
    //        //    OleDbTransaction transaction = null;
    //        //    OleDbDataAdapter adapter = null;

    //        //    // Set the Connection to the new OleDbConnection.
    //        //    command.Connection = connection;

    //        //    try
    //        //    {
    //        //        connection.Open();

    //        //        // Start a local transaction with ReadCommitted isolation level.
    //        //        transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

    //                string con = Request.Cookies["Company"].Value;

    //                // Assign transaction object for a pending local transaction.
    //                //command.Connection = connection;
    //                //command.Transaction = transaction;
    //                string itemCode = string.Empty;
    //                string dbQry = string.Empty;
    //                int Transno = int.Parse(GrdViewSales.DataKeys[e.RowIndex].Value.ToString());

    //                DataSet ds = new DataSet();
    //                BusinessLogic bl = new BusinessLogic(sDataSource);
                    
    //                string username = Request.Cookies["LoggedUserName"].Value;
                   
    //                bl.InsertReceivedAmt(con, Transno, username);


                    


    //                //DataSet ds = new DataSet();
    //                //BusinessLogic bl = new BusinessLogic(sDataSource);

    //                //ds = bl.GetProdOUTsForCompID(CompID);

    //                //foreach (DataRow dr in ds.Tables[0].Rows)
    //                //{
    //                //    stock = dr["Qty"].ToString();
    //                //    itemCode = dr["ItemCode"].ToString();

    //                //    if (stock.Trim() != "0")
    //                //    {
    //                //        command.CommandText = string.Format("UPDATE tblProductMaster SET tblProductMaster.Stock =  tblProductMaster.Stock + {0} WHERE ItemCode='{1}'", stock, itemCode);
    //                //        command.ExecuteNonQuery();
    //                //    }
    //                //}

    //                //transaction.Commit();
    //                //BindProductsGrid(txtStartDate.Text, txtEndDate.Text, rdoIsPros.Checked);
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Adjusted Successfully');", true);

    //            //}
    //            //catch (Exception ex)
    //            //{
    //            //    try
    //            //    {
    //            //        transaction.Rollback();
    //            //    }
    //            //    catch (Exception ep)
    //            //    {

    //            //    }
    //            //}

    //        //}
    //    }
    //}

    protected void GrdBills_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                BusinessLogic objChk = new BusinessLogic();

                string con = Request.Cookies["Company"].Value;
                string itemCode = string.Empty;
                string dbQry = string.Empty;
                int ID = int.Parse(GrdBills.DataKeys[e.RowIndex].Value.ToString());

                Label lblBillNo = null;
                lblBillNo = (Label)(GrdBills.Rows[e.RowIndex].FindControl("lblBillNo"));
                int BillNo = Convert.ToInt32(lblBillNo.Text);

                DataSet ds = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);

                string username = Request.Cookies["LoggedUserName"].Value;

                if (ddoption.SelectedValue == "Customer")
                {
                    bl.DeleteReceivedAmt(con, ID, BillNo, username);
                }
                else
                {
                    Label lblBillNoll = null;
                    lblBillNoll = (Label)(GrdBills.Rows[e.RowIndex].FindControl("lblBillNo"));
                    string BillNoll = lblBillNo.Text;

                    bl.DeletePaymentAmt(con, ID, BillNoll, username);
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Removed the Adjusted Bill Successfully');", true);

                //btnSearch_Click(sender, e);
                //UpdatePanelPage.Update();

                BindGrid();
                //UpdatePanel3.Update();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindGrid()
    {
        //if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
        //    return;
        //}
        ModalPopupExtender2.Show();
        pnlEdit.Visible = true;
        UpdateButton.Visible = false;
        SaveButton.Visible = false;
        //Panel1.Visible = false;
        //Div1.Visible = false;

        BusinessLogic bl = new BusinessLogic();
        string connStr = string.Empty;

        Session["BillData"] = null;

        if (ddoption.SelectedValue == "Customer")
        {
            string debtorID = ddReceivedFrom.SelectedValue;
            BusinessLogic objBus = new BusinessLogic();

            lblledger.Text = ddReceivedFrom.SelectedItem.Text;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            var customerID = ddReceivedFrom.SelectedValue.Trim();


            int ledgerID = Convert.ToInt32(ddReceivedFrom.SelectedValue);

            pnlPopup.Visible = true;
            Panel1.Visible = false;
            Div1.Visible = false;

            if (ddCriteria.SelectedValue == "Cleared")
                ShowFullBills();
            else if (ddCriteria.SelectedValue == "NotCleared")
            {
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                string ledID = ddReceivedFrom.SelectedValue.Trim();
                var dsSales = bl.ListCreditSalesCleared(connStr.Trim(), ledID);

                var receivedDat = bl.GetCustReceivedAmount(connStr);

                if (dsSales != null)
                {
                    foreach (DataRow dr in receivedDat.Tables[0].Rows)
                    {
                        var billNo = dr["BillNo"].ToString();
                        var billAmount = dr["TotalAmount"].ToString();

                        for (int i = 0; i < dsSales.Tables[0].Rows.Count; i++)
                        {
                            if (billNo.Trim() == dsSales.Tables[0].Rows[i]["BillNo"].ToString())
                            {
                                dsSales.Tables[0].Rows[i].BeginEdit();
                                double val = (double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
                                dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                                dsSales.Tables[0].Rows[i]["pay"] = val;
                                dsSales.Tables[0].Rows[i].EndEdit();

                                if (val == 0.0)
                                    dsSales.Tables[0].Rows[i].Delete();
                            }
                        }
                        dsSales.Tables[0].AcceptChanges();
                    }
                }

                var dsSalesd = bl.ListCreditSalesNotCleared(connStr.Trim(), customerID);


                if (dsSales != null)
                {
                    if (dsSales.Tables[0].Rows.Count > 0)
                    {
                        if (dsSalesd != null)
                        {
                            dsSales.Tables[0].Merge(dsSalesd.Tables[0]);
                        }

                        GrdCreditSales.DataSource = dsSales;
                        GrdCreditSales.DataBind();
                    }
                    else
                    {
                        if (dsSalesd != null)
                        {
                            if (dsSalesd.Tables[0].Rows.Count > 0)
                            {
                                GrdCreditSales.DataSource = dsSalesd;
                                GrdCreditSales.DataBind();
                            }
                        }
                        else
                        {
                            GrdCreditSales.DataSource = null;
                            GrdCreditSales.DataBind();
                        }
                    }
                }
                else if (dsSalesd != null)
                {
                    if (dsSalesd.Tables[0].Rows.Count > 0)
                    {
                        GrdCreditSales.DataSource = dsSalesd;
                        GrdCreditSales.DataBind();
                    }
                    else
                    {
                        GrdCreditSales.DataSource = null;
                        GrdCreditSales.DataBind();
                    }
                }
                else
                {
                    GrdCreditSales.DataSource = null;
                    GrdCreditSales.DataBind();
                }

                var receivedData = bl.GetReceiptForLedger(connStr, ledgerID);
                if (receivedData != null)
                {
                    if (receivedData.Tables[0].Rows.Count > 0)
                    {
                        GrdViewSales.DataSource = receivedData;
                        GrdViewSales.DataBind();
                    }
                    else
                    {
                        GrdViewSales.DataSource = null;
                        GrdViewSales.DataBind();
                    }
                }
                else
                {
                    GrdViewSales.DataSource = null;
                    GrdViewSales.DataBind();
                }
            }


            var received = bl.GetCustReceivedAmountCustomer(connStr, ledgerID);
            Session["BillData"] = received;
            Session["Data"] = received;

            if (received != null)
            {
                if (received.Tables[0].Rows.Count > 0)
                {
                    GrdBills.DataSource = received;
                    GrdBills.DataBind();

                    GridView1.DataSource = received;
                    GridView1.DataBind();
                }
                else
                {
                    GrdBills.DataSource = null;
                    GrdBills.DataBind();

                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();

                GridView1.DataSource = null;
                GridView1.DataBind();
            }


            Div1.Visible = true;
            Panel1.Visible = true;
            lnkAddBills.Visible = false;
        }
        else
        {
            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            var SupplierID = ddReceivedFrom.SelectedValue.Trim();
            pnlPopup.Visible = true;

            int ledgerID = Convert.ToInt32(ddReceivedFrom.SelectedValue);

            if (ddCriteria.SelectedValue == "Cleared")
            {
                ShowFullPurBills();
            }
            //else if (ddCriteria.SelectedValue == "PartiallyCleared")
            //{
            //    ShowPendingPurBills();
            //}
            else if (ddCriteria.SelectedValue == "NotCleared")
            {
 
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                var customerID = ddReceivedFrom.SelectedValue.Trim();

                var dsSales = bl.ListCreditPurchaseCleared(connStr.Trim(), customerID);

                var receivedData = bl.GetSupplierReceivedAmount(connStr);

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
                                dsSales.Tables[0].Rows[i]["Amount"] = double.Parse(dsSales.Tables[0].Rows[i]["Amount"].ToString());
                                dsSales.Tables[0].Rows[i]["pay"] = val;
                                dsSales.Tables[0].Rows[i].EndEdit();

                                if (val == 0.0)
                                    dsSales.Tables[0].Rows[i].Delete();
                            }
                        }
                        dsSales.Tables[0].AcceptChanges();
                    }
                }

                var dsSalesd = bl.ListCreditPurchaseNotCleared(connStr.Trim(), SupplierID);

                if (dsSales != null)
                {
                    if (dsSales.Tables[0].Rows.Count > 0)
                    {
                        if (dsSalesd != null)
                        {
                            dsSales.Tables[0].Merge(dsSalesd.Tables[0]);
                        }
                        GrdCreditSales.DataSource = dsSales;
                        GrdCreditSales.DataBind();
                    }
                    else
                    {
                        GrdCreditSales.DataSource = null;
                        GrdCreditSales.DataBind();
                    }
                }
                else
                {
                    if (dsSalesd.Tables[0].Rows.Count > 0)
                    {
                        GrdCreditSales.DataSource = dsSalesd;
                        GrdCreditSales.DataBind();
                    }
                    else
                    {
                        GrdCreditSales.DataSource = null;
                        GrdCreditSales.DataBind();
                    }
                }

                var receivedDatad = bl.GetPaymentForLedger(connStr, ledgerID);
                if (receivedDatad != null)
                {
                    if (receivedDatad.Tables[0].Rows.Count > 0)
                    {
                        GrdViewSales.DataSource = receivedDatad;
                        GrdViewSales.DataBind();
                    }
                    else
                    {
                        GrdViewSales.DataSource = null;
                        GrdViewSales.DataBind();
                    }
                }
                else
                {
                    GrdViewSales.DataSource = null;
                    GrdViewSales.DataBind();
                }

            }


                
            



            DataSet receivedDatahh = new DataSet();
            var receivedDatadd = bl.GetSuppPayAmountCustomer(connStr.Trim(), ledgerID);
            Session["BillData"] = receivedDatadd;
            Session["Data"] = receivedDatadd;

            //if (receivedDatadd.Tables[0].Rows[0]["BillNo"].ToString() == "0")
            //{
            //    receivedDatadd = null;
            //}

            if (receivedDatadd != null)
            {
                if (receivedDatadd.Tables[0].Rows.Count > 0)
                {
                    GrdBills.DataSource = receivedDatadd;
                    GrdBills.DataBind();

                    GridView1.DataSource = receivedDatadd;
                    GridView1.DataBind();

                }
                else
                {
                    GrdBills.DataSource = null;
                    GrdBills.DataBind();

                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }
            }
            else
            {
                GrdBills.DataSource = null;
                GrdBills.DataBind();

                GridView1.DataSource = null;
                GridView1.DataBind();
            }

            Div1.Visible = true;
            Panel1.Visible = true;
            lnkAddBills.Visible = false;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
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

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
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
        GrdBills.EditIndex = e.NewEditIndex;
        DataRow row = ((DataSet)Session["BillData"]).Tables[0].Rows[e.NewEditIndex];
        Session["EditedRow"] = e.NewEditIndex.ToString();
        Session["EditedAmount"] = row["Amount"].ToString();
        GrdBills.DataSource = (DataSet)Session["BillData"];
        GrdBills.DataBind();
        
    }

    protected void txtBillNo_Load(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            BusinessLogic bl = new BusinessLogic();

            var customerID = ddReceivedFrom.SelectedValue.Trim();
            var dsSales = bl.ListCreditSalesNotCleared(connStr.Trim(), customerID);



            var dsd = bl.ListCreditSalesCleared(connStr.Trim(), customerID);
            var receivedData = bl.GetCustReceivedAmount(connStr);
            if (dsd != null)
            {
                foreach (DataRow dr in receivedData.Tables[0].Rows)
                {
                    var billNo = dr["BillNo"].ToString();
                    var billAmount = dr["TotalAmount"].ToString();

                    for (int i = 0; i < dsd.Tables[0].Rows.Count; i++)
                    {
                        if (billNo.Trim() == dsd.Tables[0].Rows[i]["BillNo"].ToString())
                        {
                            dsd.Tables[0].Rows[i].BeginEdit();
                            double val = (double.Parse(dsd.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
                            dsd.Tables[0].Rows[i]["Amount"] = double.Parse(dsd.Tables[0].Rows[i]["Amount"].ToString());
                            dsd.Tables[0].Rows[i]["pay"] = val;
                            dsd.Tables[0].Rows[i].EndEdit();

                            if (val == 0.0)
                                dsd.Tables[0].Rows[i].Delete();
                        }
                    }
                    dsd.Tables[0].AcceptChanges();
                }
            }


            if ((dsSales != null) && (dsd != null))
            {
                dsSales.Tables[0].Merge(dsd.Tables[0]);
            }
            else if ((dsSales != null) && (dsd == null))
            {

            }
            else if ((dsSales == null) && (dsd != null))
            {

            }

            if ((dsSales != null) && (dsd != null))
            {
                if (dsSales.Tables[0].Rows.Count > 0)
                {
                    if (ddl != null)
                    {
                        ddl.DataSource = dsSales;
                        ddl.DataBind();
                        Session["BillData"] = dsSales;
                    }
                    Session["BillData"] = dsSales;
                }
            }
            else if ((dsSales != null) && (dsd == null))
            {
                if (dsSales.Tables[0].Rows.Count > 0)
                {
                    if (ddl != null)
                    {
                        ddl.DataSource = dsSales;
                        ddl.DataBind();
                        Session["BillData"] = dsSales;
                    }
                    Session["BillData"] = dsSales;
                }
            }
            else if ((dsSales == null) && (dsd != null))
            {
                if (dsd.Tables[0].Rows.Count > 0)
                {
                    if (ddl != null)
                    {
                        ddl.DataSource = dsd;
                        ddl.DataBind();
                        Session["BillData"] = dsd;
                    }
                    Session["BillData"] = dsd;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void calcSum()
    {
        var ds = (DataSet)GrdBills.DataSource;

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

    protected void GrdBills_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            GrdBills.FooterRow.Visible = false;
            var ds = (DataSet)Session["BillData"];
            GrdBills.EditIndex = -1;
            if (ds != null)
            {
                GrdBills.DataSource = ds;
            }
            GrdBills.DataBind();
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

                string billNo = ((DropDownList)GrdBills.FooterRow.FindControl("txtAddBillNo")).SelectedValue;
                string amount = ((TextBox)GrdBills.FooterRow.FindControl("txtAddBillAmount")).Text;
                string CustomerID = ddReceivedFrom.SelectedValue.ToString().Trim();
                string TransNo = string.Empty;

                //if (GrdViewReceipt.SelectedDataKey != null)
                //    TransNo = GrdViewReceipt.SelectedDataKey.Value.ToString();
                //else
                    TransNo = "";

                    if (ddoption.SelectedValue == "Customer")
                    {
                        if (bl.GetIfBillNoExists(int.Parse(billNo), CustomerID) == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('BillNo does not Exists. Please check BillNo.')", true);
                            //Error.Text = "BillNo does not Exists. Please check BillNo.";
                            pnlEdit.Visible = true;
                            ModalPopupExtender2.Show();
                            return;
                        }

                        double eligibleAmount = bl.GetSalesPendingAmount(int.Parse(billNo));

                        if (double.Parse(amount) > eligibleAmount)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The Amount you entered for BillNo:" + billNo + " is Greater than Pending Sales Amount of " + eligibleAmount.ToString() + ". Please check the Bill Amount')", true);
                            //Error.Text = "The Amount you entered for BillNo:" + billNo + " is Greater than Pending Sales Amount of " + eligibleAmount.ToString() + ". Please check the Bill Amount";
                            ModalPopupExtender2.Show();
                            return;
                        }

                    }
                    else
                    {
                        if (bl.GetIfBillNoExistsPayment(int.Parse(billNo), CustomerID) == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('BillNo does not Exists. Please check BillNo.')", true);
                            //Error.Text = "BillNo does not Exists. Please check BillNo.";
                            pnlEdit.Visible = true;
                            ModalPopupExtender2.Show();
                            return;
                        }

                        double eligibleAmount = bl.GetPurchasePendingAmount(int.Parse(billNo));

                        if (double.Parse(amount) > eligibleAmount)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('The Amount you entered for BillNo : " + billNo + " is Greater than Pending Purchase Amount of " + eligibleAmount.ToString() + ". Please check the Bill Amount')", true);
                            //Error.Text = "The Amount you entered for BillNo:" + billNo + " is Greater than Pending Sales Amount of " + eligibleAmount.ToString() + ". Please check the Bill Amount";
                            ModalPopupExtender2.Show();
                            return;
                        }
                    }
                var isBillExists = CheckIfBillExists(billNo);

                //if (isBillExists)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('BillNo already Exists')", true);
                //    //Error.Text = "BillNo already Exists";
                //    ModalPopupExtender2.Show();
                //    return;
                //}


                

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

                    //dc = new DataColumn("ReceiptNo");
                    //dt.Columns.Add(dc);

                    dc = new DataColumn("BillNo");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Amount");
                    dt.Columns.Add(dc);

                    ds.Tables.Add(dt);

                    drNew = dt.NewRow();

                    //drNew["ReceiptNo"] = TransNo;
                    drNew["BillNo"] = billNo;
                    drNew["Amount"] = amount;

                    ds.Tables[0].Rows.Add(drNew);

                    Session["BillData"] = ds;
                    //Session["Data"] = ds;

                    GrdBills.DataSource = ds;
                    GrdBills.DataBind();
                    GrdBills.EditIndex = -1;
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

                    //if (ds.Tables[0].Rows[0]["ReceiptNo"].ToString() == "0")
                    //{
                    //    ds.Tables[0].Rows[0].Delete();
                    //    ds.Tables[0].AcceptChanges();
                    //}

                    drNew = ds.Tables[0].NewRow();
                    //drNew["ReceiptNo"] = 0;
                    drNew["BillNo"] = billNo;
                    drNew["Amount"] = amount;

                    ds.Tables[0].Rows.Add(drNew);
                    Session["BillData"] = ds;
                    //Session["Data"] = ds;
                    //System.Threading.Thread.Sleep(1000);
                    GrdBills.DataSource = ds;
                    GrdBills.DataBind();
                    GrdBills.EditIndex = -1;
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

    protected void GrdBills_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(1000);
            GrdBills.DataBind();
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


    protected void GrdBills_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        //    string connStr = string.Empty;

        //    if (Request.Cookies["Company"] != null)
        //        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        //    else
        //        Response.Redirect("~/Login.aspx");

        //    BusinessLogic bl = new BusinessLogic();

        //    var customerID = ddReceivedFrom.SelectedValue.Trim();
        //    var dsSales = bl.ListCreditSalesNotCleared(connStr.Trim(), customerID);



        //    var dsd = bl.ListCreditSalesCleared(connStr.Trim(), customerID);
        //    var receivedData = bl.GetCustReceivedAmount(connStr);
        //    if (dsd != null)
        //    {
        //        foreach (DataRow dr in receivedData.Tables[0].Rows)
        //        {
        //            var billNo = dr["BillNo"].ToString();
        //            var billAmount = dr["TotalAmount"].ToString();

        //            for (int i = 0; i < dsd.Tables[0].Rows.Count; i++)
        //            {
        //                if (billNo.Trim() == dsd.Tables[0].Rows[i]["BillNo"].ToString())
        //                {
        //                    dsd.Tables[0].Rows[i].BeginEdit();
        //                    double val = (double.Parse(dsd.Tables[0].Rows[i]["Amount"].ToString()) - double.Parse(billAmount));
        //                    dsd.Tables[0].Rows[i]["Amount"] = double.Parse(dsd.Tables[0].Rows[i]["Amount"].ToString());
        //                    dsd.Tables[0].Rows[i]["pay"] = val;
        //                    dsd.Tables[0].Rows[i].EndEdit();

        //                    if (val == 0.0)
        //                        dsd.Tables[0].Rows[i].Delete();
        //                }
        //            }
        //            dsd.Tables[0].AcceptChanges();
        //        }
        //    }


        //    if ((dsSales != null) && (dsd != null))
        //    {
        //        dsSales.Tables[0].Merge(dsd.Tables[0]);
        //    }
        //    else if ((dsSales != null) && (dsd == null))
        //    {
               
        //    }
        //    else if ((dsSales == null) && (dsd != null))
        //    {
               
        //    }

        //    if ((dsSales != null) && (dsd != null))
        //    {
        //        if (dsSales.Tables[0].Rows.Count > 0)
        //        {
        //            DropDownList ddl = (e.Row.FindControl("txtBillNo") as DropDownList);
        //            //DropDownList ddl = GrdBills.FindControl("txtBillNo") as DropDownList;
        //            if (ddl != null)
        //            {
        //                ddl.DataSource = dsSales;
        //                ddl.DataBind();
        //                //Session["BillData"] = dsSales;
        //                DataRowView dr = e.Row.DataItem as DataRowView;
        //                ddl.SelectedValue =
        //                             dr["BillNo"].ToString();
        //            }
        //            //Session["BillData"] = dsSales;
        //        }
        //    }
        //    else if ((dsSales != null) && (dsd == null))
        //    {
        //        if (dsSales.Tables[0].Rows.Count > 0)
        //        {
        //            DropDownList ddl = (e.Row.FindControl("txtBillNo") as DropDownList);
        //            //DropDownList ddl = GrdBills.FindControl("txtBillNo") as DropDownList;
        //            if (ddl != null)
        //            {
        //                ddl.DataSource = dsSales;
        //                ddl.DataBind();
        //                //Session["BillData"] = dsSales;
        //                DataRowView dr = e.Row.DataItem as DataRowView;
        //                ddl.SelectedValue =
        //                             dr["BillNo"].ToString();
        //            }
        //            //Session["BillData"] = dsSales;
        //        }
        //    }
        //    else if ((dsSales == null) && (dsd != null))
        //    {
        //        if (dsd.Tables[0].Rows.Count > 0)
        //        {
        //            DropDownList ddl = (e.Row.FindControl("txtBillNo") as DropDownList);
        //            //DropDownList ddl = GrdBills.FindControl("txtBillNo") as DropDownList;
        //            if (ddl != null)
        //            {
        //                ddl.DataSource = dsd;
        //                ddl.DataBind();
        //                //Session["BillData"] = dsd;
        //                DataRowView dr = e.Row.DataItem as DataRowView;
        //                ddl.SelectedValue =
        //                             dr["BillNo"].ToString();
        //            }
        //            //Session["BillData"] = dsd;
        //        }
        //    }
        }
        //lnkAddBills.Visible = true;
    }

    //protected void txtBillNo_DataBound(object sender, EventArgs e)
    //{
    //    DropDownList ddl = (DropDownList)sender;
        
    //        string creditorID = ((DataRowView)frmV.DataItem)["CreditorID"].ToString();

    //        ddl.ClearSelection();

    //        ListItem li = ddl.Items.FindByValue(creditorID);
    //        if (li != null) li.Selected = true;

    //}

    //protected void GrdBills_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    DataSet ds;

    //    try
    //    {
    //        if (Session["BillData"] != null)
    //        {
    //            GridViewRow row = GrdBills.Rows[e.RowIndex];
    //            ds = (DataSet)Session["BillData"];
    //            ds.Tables[0].Rows[GrdBills.Rows[e.RowIndex].DataItemIndex].Delete();
    //            ds.Tables[0].AcceptChanges();
    //            GrdBills.DataSource = ds;
    //            GrdBills.DataBind();
    //            Session["BillData"] = ds;
    //            ModalPopupExtender2.Show();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected void GrdBills_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int curRow = 0;
            string billNo = ((DropDownList)GrdBills.Rows[e.RowIndex].FindControl("txtBillNo")).SelectedItem.Text;
            string amount = ((TextBox)GrdBills.Rows[e.RowIndex].FindControl("txtBillAmount")).Text;
            //string Id = GrdBills.DataKeys[e.RowIndex].Value.ToString();
            string CustomerID = ddReceivedFrom.SelectedValue.ToString().Trim();
            string TransNo = "0";
            ModalPopupExtender2.Show();

            if (GrdViewReceipt.SelectedDataKey != null)
                TransNo = GrdViewReceipt.SelectedDataKey.Value.ToString();


            DataSet ds = (DataSet)Session["BillData"];

            //if ((calcDatasetSum(ds) + double.Parse(amount) - double.Parse(Session["EditedAmount"].ToString())) > double.Parse(txtAmount.Text))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Total Bills amount is exceeding the Receipt Amount. Please check the Bill Amount')", true);
            //    //Error.Text = "Total Bills amount is exceeding the Receipt Amount. Please check the Bill Amount";
            //    return;
            //}

            BusinessLogic bl = new BusinessLogic(GetConnectionString());

            if (bl.GetIfBillNoExists(int.Parse(billNo), CustomerID) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('BillNo does not Exists. Please check BillNo.')", true);
                //Error.Text = "BillNo does not Exists. Please check BillNo.";
                pnlEdit.Visible = true;
                ModalPopupExtender2.Show();
                return;
            }

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
            //ds.Tables[0].Rows[curRow]["ReceiptNo"] = TransNo;

            var isBillExists = CheckNoOfBillExists(billNo);

            //if (isBillExists > 1)
            //{
            //    ds.Tables[0].Rows[curRow].RejectChanges();
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('BillNo already Exists')", true);
            //    //Error.Text = "BillNo already Exists";
            //    return;
            //}

            ds.Tables[0].Rows[curRow].EndEdit();

            ds.Tables[0].Rows[curRow].AcceptChanges();
            GrdBills.DataSource = ds;
            GrdBills.EditIndex = -1;
            GrdBills.DataBind();
            Session["BillData"] = ds;
            lnkAddBills.Visible = true;
            checkPendingBills(ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
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

    protected void GrdBills_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdBills, e.Row, this);
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

            Page.Validate();

            if (Page.IsValid)
            {

                //int CreditorID = int.Parse(ddReceivedFrom.SelectedValue);

                //string RefNo = txtRefNo.Text;

                //DateTime TransDate = DateTime.Parse(txtTransDate.Text);

                //int DebitorID = 0;
                //string Paymode = string.Empty;
                //double Amount = 0.0;
                //string Narration = string.Empty;
                //string VoucherType = string.Empty;
                //string ChequeNo = string.Empty;

                //if (chkPayTo.SelectedValue == "Cash")
                //{
                //    DebitorID = 1;
                //    Paymode = "Cash";
                //}
                //else if (chkPayTo.SelectedValue == "Cheque")
                //{
                //    DebitorID = int.Parse(ddBanks.SelectedValue);
                //    Paymode = "Cheque";
                //}

                //Amount = double.Parse(txtAmount.Text);
                //Narration = txtNarration.Text;
                //VoucherType = "Receipt";
                //ChequeNo = txtChequeNo.Text;

                //if (hdSMSRequired.Value == "YES")
                //{

                //    if (txtMobile.Text != "")
                //        hdMobile.Value = txtMobile.Text;

                //    hdText.Value = "Thank you for Payment of Rs." + txtAmount.Text;

                //}

                BusinessLogic bl = new BusinessLogic();
                string conn = GetConnectionString();
                int OutPut = 0;

                DataSet ds = (DataSet)Session["BillData"];
                DataSet dsold = new DataSet();
                DataTable dtt;
                DataRow drNew;

                DataColumn dc;

                dtt = new DataTable();

                dc = new DataColumn("BillNo");
                dtt.Columns.Add(dc);

                dc = new DataColumn("Amount");
                dtt.Columns.Add(dc);

                dc = new DataColumn("ReceiptNo");
                dtt.Columns.Add(dc);

                dsold.Tables.Add(dtt);

                for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                {
                    //Label txt1 = (Label)GridView1.Rows[vLoop].FindControl("BillNo");
                    //string text1 = txt1.Text;
                    //Label txt2 = (Label)GridView1.Rows[vLoop].FindControl("Amount");
                    //string text2 = txt2.Text;
                    drNew = dtt.NewRow();
                    drNew["BillNo"] = GridView1.Rows[vLoop].Cells[0].Text;
                    drNew["Amount"] = GridView1.Rows[vLoop].Cells[1].Text;
                    drNew["ReceiptNo"] = GridView1.Rows[vLoop].Cells[1].Text;
                    dsold.Tables[0].Rows.Add(drNew);
                }





                //DataSet dsd = new DataSet();
                //DataTable dtd;
                //DataRow drt;

                //DataColumn dcd;

                //dtd = new DataTable();

                //dcd = new DataColumn("BillNo");
                //dtd.Columns.Add(dcd);

                //dcd = new DataColumn("Amount");
                //dtd.Columns.Add(dcd);

                //dcd = new DataColumn("ReceiptNo");
                //dtd.Columns.Add(dcd);

                //dsd.Tables.Add(dtd);

                //for (int vLoop = 0; vLoop < GrdBills.Rows.Count; vLoop++)
                //{
                //    Label txt1 = (Label)GrdBills.Rows[vLoop].FindControl("BillNo");
                //    string text1 = txt1.Text;
                //    Label txt2 = (Label)GrdBills.Rows[vLoop].FindControl("Amount");
                //    string text2 = txt2.Text;
                //    Label txt3 = (Label)GrdBills.Rows[vLoop].FindControl("ReceiptNo");
                //    string text3 = txt3.Text;
                //    drt = dtd.NewRow();
                //    drt["BillNo"] = text1;
                //    drt["Amount"] = text2;
                //    drt["ReceiptNo"] = text3;
                //    dsd.Tables[0].Rows.Add(drt);
                //}

                //if (ds == null)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Not given the Bill Details')", true);

                //}

                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (ddoption.SelectedValue == "Customer")
                {
                    bl.InsertManualAdjustment(conn, ds, dsold, usernam);
                }
                else if (ddoption.SelectedValue == "Supplier")
                {
                    bl.InsertManualAdjustmentPur(out OutPut, conn, ds, dsold, usernam);
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Manual Adjustment Saved Successfully.');", true);

                Div1.Visible = false;
                ModalPopupExtender2.Hide();

                //if (hdSMS.Value == "YES")
                //{
                //    UtilitySMS utilSMS = new UtilitySMS(conn);
                //    string UserID = Page.User.Identity.Name;

                //    if (Session["Provider"] != null)
                //        utilSMS.SendSMS(Session["Provider"].ToString(), Session["Priority"].ToString(), Session["SenderID"].ToString(), Session["UserName"].ToString(), Session["Password"].ToString(), hdMobile.Value, hdText.Value, true, UserID);
                //    else
                //    {
                //        if (hdMobile.Value != "")
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('you are not configured to send SMS. Please contact Administrator.');", true);
                //    }
                //}

                //pnlEdit.Visible = false;
                //ModalPopupExtender2.Hide();
                //lnkBtnAdd.Visible = true;
                ////MyAccordion.Visible = true;
                //GrdViewReceipt.Visible = true;
                //GrdViewReceipt.DataBind();
                //ClearPanel();
                //UpdatePanelPage.Update();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected void ShowPendingSales_Click(object sender, EventArgs e)
    //{

        
    //}

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

            Page.Validate();

            //if (Page.IsValid)
            //{

            //    int CreditorID = int.Parse(ddReceivedFrom.SelectedValue);

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

            //    string usernam = Request.Cookies["LoggedUserName"].Value;

            //    bl.UpdateCustReceipt(out OutPut, conn, TransNo, RefNo, TransDate, DebitorID, CreditorID, Amount, Narration, VoucherType, ChequeNo, Paymode, ds, usernam);

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
