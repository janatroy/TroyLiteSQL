using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using ACCSYSTEM;
using AjaxControlToolkit;
using System.IO;

public partial class PageMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["LoggedUserName"] != null)
            {
                //lblWelcome.Text = "Welcome";
                lblUser1.Text = " " + Request.Cookies["LoggedUserName"].Value + "";
                lblVer.Text = " " + "1.1.1";
            }

            NavBarBlocks blocks = Navbar2.Blocks;

            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            BusinessLogic objBus = new BusinessLogic(connStr);

            DataSet ds = objBus.getImageInfo();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Image1.ImageUrl = "App_Themes/NewTheme/images/" + ds.Tables[0].Rows[i]["img_filename"];
                        Image1.Height = 35;
                        //Image1.Width = 220;
                        //string flname = Server.MapPath("App_Themes/NewTheme/images/" + ds.Tables[0].Rows[i]["img_filename"]);
                        //Image1.ImageUrl = flname;
                    }
                }
                else
                {
                    //Image1.Height = 30;
                    //Image1.Width = 130;
                    //Image1.ImageUrl = "App_Themes/NewTheme/images/Sam.png";

                    Image1.Height = 35;
                    Image1.Width = 220;
                    Image1.ImageUrl = "App_Themes/NewTheme/images/TESTLogo.png";
                }
            }
            else
            {
                //Image1.Height = 30;
                //Image1.Width = 130;
                //Image1.ImageUrl = "App_Themes/NewTheme/images/Sam.png";

                Image1.Height = 35;
                Image1.Width = 220;
                Image1.ImageUrl = "App_Themes/NewTheme/images/TESTLogo.png";
            }


            foreach (NavBarBlock block in blocks)
            {
                NavBarItems items = block.Items;
                NavBarItems toRemove = new NavBarItems();

                foreach (NavBarItem item in items)
                {
                    if ((item.Text == "System Configuration") && (Helper.GetDecryptedKey("InstallationType") != "ONLINE-OFFLINE-CLIENT"))
                        toRemove.Add(item);

                    if ((item.Text == "Compress DB") && (Helper.GetDecryptedKey("InstallationType") != "ONLINE-OFFLINE-SERVER"))
                        toRemove.Add(item);

                }

                foreach (NavBarItem test in toRemove)
                {
                    items.Remove(test);
                }

            }

            if (!Page.IsPostBack)
            {
                uiDateTimeLabel.Text = DateTime.Now.ToString("dd MMM yyyy");
                lblCompCode.Text = " " + Request.Cookies["Company"].Value.ToLower();
                //lblCompCode.Text = " " + Request.Cookies["Branch"].Value.ToLower();
                //string strImageID = getImageID();
                //Image1.ImageUrl = "ImageHandler.ashx?ImageID=" + strImageID + "";

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd MMM yyyy");
                lbldatetimenew.Text = dtaa;

                MenuAuthorization();
            }

            DataSet appSettings;
            bool isItemTrackingRequired = false;

            if (Session["AppSettings"] != null)
            {
                appSettings = (DataSet)Session["AppSettings"];

                for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
                {
                    if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "QTYRETURN")
                    {
                        if (appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString() == "YES")
                            isItemTrackingRequired = true;
                    }
                }
            }


            if (Helper.GetDecryptedKey("InstallationType") != "ONLINE-OFFLINE-CLIENT")
            {
                foreach (NavBarBlock block in blocks)
                {
                    NavBarItems items = block.Items;
                    NavBarItems toRemove = new NavBarItems();

                    foreach (NavBarItem item in items)
                    {
                        if (item.Text == "Backup & Restore" || item.Text == "New Account - Financial Year" || item.Text == "Previous Year Data")
                            toRemove.Add(item);

                        if ((item.Text == "Item Tracking") && !isItemTrackingRequired)
                            toRemove.Add(item);

                        if ((item.Text == "System Configuration") && (Helper.GetDecryptedKey("InstallationType") != "ONLINE-OFFLINE-CLIENT"))
                            toRemove.Add(item);
                    }

                    foreach (NavBarItem test in toRemove)
                    {
                        items.Remove(test);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddYears(-34);
            Response.Cookies.Clear();
            Response.Redirect("~/Login.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (Request.Cookies["Company"] == null)
            Response.Redirect("~/Login.aspx");

    }
    protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    {
        if (e.Exception != null)
        {
            ScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
            throw new Exception(e.Exception.Message + e.Exception.StackTrace);
        }

        //ScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
    }

    protected string getImageID()
    {
        Double days = 0;
        DateTime StartDate = new DateTime((DateTime.Now.Year), 01, 01);
        TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - StartDate.Ticks);
        System.Random RandNum = new System.Random();
        int MyRandomNumber = RandNum.Next(0, 99);
        days = ts.Days + 1;
        int intSecondOfDay = 0;
        string strReturn = "";
        strReturn = days.ToString().PadLeft(3, '0');
        strReturn = strReturn + MyRandomNumber.ToString().PadLeft(2, '0');
        intSecondOfDay = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
        return strReturn + intSecondOfDay.ToString().PadLeft(5, '0');
    }

    //protected string imageURL(string img_id)
    //{

    //    string connStr = string.Empty;

    //    if (Request.Cookies["Company"] != null)
    //        connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
    //    else
    //        Response.Redirect("~/Login.aspx");

    //    BusinessLogic objBus = new BusinessLogic(connStr);

    //    DataSet ds = objBus.getImageInfo(img_id);
    //    if (ds != null)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            foreach (DataRow dRow in ds.Tables[0].Rows)
    //            {
    //                Response.ContentType = dRow["img_type"].ToString();
    //                byte[] imageContent = (byte[])((dRow["img_stream"]));
    //                Response.BinaryWrite(imageContent);
    //            }
    //        }
    //    }
    //    return img_id;

        
    //}

    //public string imageURL(string img_id)
    //{
    //    return ("retrieveImages.aspx?id=" + img_id);
    //    //string connStr = string.Empty;

    //    //if (Request.Cookies["Company"] != null)
    //    //    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
    //    //else
    //    //    Response.Redirect("~/Login.aspx");
    //    //BusinessLogic objBus = new BusinessLogic(connStr);
    //    //DataSet ds = objBus.getImageInfo(img_id);

    //    //if (ds != null)
    //    //{
    //    //    if (ds.Tables[0].Rows.Count > 0)
    //    //    {


    //    //        foreach (DataRow dRow in ds.Tables[0].Rows)
    //    //        {
    //    //            Response.ContentType = dRow["img_type"].ToString();
    //    //            byte[] imageContent = (byte[])((dRow["img_stream"]));
    //    //            Response.BinaryWrite(imageContent);
    //    //        }
    //    //    }
    //    //}
    //    //return img_id;
    //}

    protected void CmdSales_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("CustomerSales.aspx?myname=" + "NEWSAL");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void CmdPurchase_click(object sender,EventArgs e)
    {
        try
        {
            Response.Redirect("Purchase.aspx?myname=" + "NEWPUR");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    
    protected void CmdExp_click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ExpPayment.aspx?myname=" + "NEWEXP");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }



    protected void CmdProduct_click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ProdMaster.aspx?myname=" + "NEWPROD");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    //private void MenuAuthorization()
    //{

    //    if (!this.Page.User.IsInRole("CUSTINFO"))
    //    {
    //        lnkCustomerInfo.Visible = false;
    //    }
    //    if (!this.Page.User.IsInRole("CUSTPMT"))
    //    {
    //        lnkCustomerPayment.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("CUSTRCT"))
    //    {
    //        lnkCustomerReceipt.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("SALES"))
    //    {
    //        lnkCustomerSales.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("LDMNGT"))
    //    {
    //        lnkLeadManagement.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("COMMNGT"))
    //    {
    //        lnkCommissionMngmt.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("SUPPINFO"))
    //    {
    //        lnkSupplierInfo.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("SUPPPMT"))
    //    {
    //        lnkSupplierPayment.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("SUPPRCT"))
    //    {
    //        lnkSupplierReceipt.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("PURCHS"))
    //    {
    //        lnkSupplierPurchase.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("BANKINFO"))
    //    {
    //        lnkBankInfo.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("BNKPMT"))
    //    {
    //        lnkBankDeposit.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("BNKRCT"))
    //    {
    //        lnkBankReceipt.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("BNKREC"))
    //    {
    //        lnkBankRec.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("EXPINFO"))
    //    {
    //        lnkExpenseInfo.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("EXPPMT"))
    //    {
    //        lnkExpensePayment.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("CATMST"))
    //    {
    //        lnkCategoryMast.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("BRDMST"))
    //    {
    //        lnkBrandMast.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("PRDMST"))
    //    {
    //        lnkProductMast.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("STKMST"))
    //    {
    //        lnkTransferDefn.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("PHYSTK"))
    //    {
    //        lnkRPS.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("STKADJ"))
    //    {
    //        lnkTransferExecution.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("GRPMST"))
    //    {
    //        lnkGroupInfo.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("LDGMST"))
    //    {
    //        lnkLedgerInfo.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("JURNAL"))
    //    {
    //        lnkJournalInfo.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("CDNOTE"))
    //    {
    //        lnkCreditDebitNote.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("MULJOU"))
    //    {
    //        lnkMultipleInfo.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("TMENTRY"))
    //    {
    //        lnkTimeSheet.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("TMAPP"))
    //    {
    //        lnkTimeSheetApproval.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("WMENTRY"))
    //    {
    //        lnkWorkManagement.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("WRKUPD"))
    //    {
    //        lnkWorkUpdates.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("WRKCLO"))
    //    {
    //        lnkWorkClosure.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("SRVENT"))
    //    {
    //        lnkContracts.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("SRVMGMT"))
    //    {
    //        lnkVisits.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("EMPMST"))
    //    {
    //        lnkEmployeeMast.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("CMPMST"))
    //    {
    //        lnkCompanyinfo.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("SICMST"))
    //    {
    //        lnkInterestCalculation.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("SMSDRFT"))
    //    {
    //        lnkSmsText.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("ITMTRK"))
    //    {
    //        lnkQtyReturns.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("JOBENT"))
    //    {
    //        lnkJobEntry.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("BILTENT"))
    //    {
    //        lnkBilty.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("CHQMST"))
    //    {
    //        lnkChequeBook.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("REORDRPT"))
    //    {
    //        lnkInventoryReports.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("SLSRPT"))
    //    {
    //        lnkSalesReports.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("PRSUMRPT"))
    //    {
    //        lnkPurchaseReports.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("TRLBRPT"))
    //    {
    //        lnkAccReports.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("OUTRPT"))
    //    {
    //        lnkOutStandingReports.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("TMEREPORT"))
    //    {
    //        lnkResMgmReports.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("SERMNGT"))
    //    {
    //        lnkServiceReports.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("COMRPT"))
    //    {
    //        lnkComplaintReports.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("VATSUM"))
    //    {
    //        lnkVATReports.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("BUSTRNRPT"))
    //    {
    //        lnkOtherReports.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("SYSCONF"))
    //    {
    //        lnkConfiguration.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("RECDATE"))
    //    {
    //        lnkReconDate.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("MNGUSRS"))
    //    {
    //        lnkUserMaintenance.Attributes.Add("disabled", "disabled");
    //    }
    //    if (!this.Page.User.IsInRole("ULOCK"))
    //    {
    //        lnkuserlock.Attributes.Add("disabled", "disabled");
    //    }
    //}

    private void MenuAuthorization()
    {

        if (!this.Page.User.IsInRole("CUSTINFO"))
        {
            lnkCustomerInfo.Visible = false;
        }
        if (!this.Page.User.IsInRole("CUSTPMT"))
        {
            lnkCustomerPayment.Visible = false;
        }
        if (!this.Page.User.IsInRole("CUSTRCT"))
        {
            lnkCustomerReceipt.Visible = false;
        }
        if (!this.Page.User.IsInRole("SALES"))
        {
            lnkCustomerSales.Visible = false;
            CmdSales.Enabled = false;
            CmdSales.ToolTip = "You are not allowed to Add Sales";
            LinkButton2.Enabled = false;
            //LinkButton2.ForeColor = System.Drawing.Color.0099CC;
        }
        else
        {
            CmdSales.Enabled = true;
            LinkButton2.Enabled = true;
        }
        if (!this.Page.User.IsInRole("LDMNGT"))
        {
            lnkLeadManagement.Visible = false;
        }
        //if (!this.Page.User.IsInRole("COMMNGT"))
        //{
        //    lnkCommissionMngmt.Visible = false;
        //}
        if (!this.Page.User.IsInRole("HIPUR"))
        {
            lnkHirePurchaseNew.Visible = false;
        }
        if (!this.Page.User.IsInRole("SUPPINFO"))
        {
            lnkSupplierInfo.Visible = false;
        }
        if (!this.Page.User.IsInRole("SUPPPMT"))
        {
            lnkSupplierPayment.Visible = false;
        }
        if (!this.Page.User.IsInRole("SUPPRCT"))
        {
            lnkSupplierReceipt.Visible = false;
        }
        if (!this.Page.User.IsInRole("PURCHS"))
        {
            lnkSupplierPurchase.Visible = false;
            CmdPurchase.Enabled = false;
            CmdPurchase.ToolTip = "You are not allowed to Add Purchase";
        }
        else
        {
            CmdPurchase.Enabled = true;
        }
        if (!this.Page.User.IsInRole("BANKINFO"))
        {
            lnkBankInfo.Visible = false;
        }
        if (!this.Page.User.IsInRole("BNKPMT"))
        {
            lnkBankDeposit.Visible = false;
        }
        if (!this.Page.User.IsInRole("BNKRCT"))
        {
            lnkBankReceipt.Visible = false;
        }
        if (!this.Page.User.IsInRole("BNKREC"))
        {
            lnkBankRec.Visible = false;
        }
        if (!this.Page.User.IsInRole("EXPINFO"))
        {
            lnkExpenseInfo.Visible = false;
        }
        if (!this.Page.User.IsInRole("EXPPMT"))
        {
            lnkExpensePayment.Visible = false;
            CmdExp.Enabled = false;
            CmdExp.ToolTip = "You are not allowed to Add Expenses";
        }
        else
        {
            CmdExp.Enabled = true;
        }
        if (!this.Page.User.IsInRole("CATMST"))
        {
            lnkCategoryMast.Visible = false;
        }
        if (!this.Page.User.IsInRole("BRDMST"))
        {
            lnkBrandMast.Visible = false;
        }

        lnkResetPassword.Visible = false;

        if (!this.Page.User.IsInRole("PRDMST"))
        {
            lnkProductMast.Visible = false;
            CmdProduct.Enabled = false;
            CmdProduct.ToolTip = "You are not allowed to Add Product";
        }
        else
        {
            CmdProduct.Enabled = true;
        }
        if (!this.Page.User.IsInRole("OPNSTK"))
        {
            lnkOpeningStock.Visible = false;
        }
        if (!this.Page.User.IsInRole("STKMST"))
        {
            lnkTransferDefn.Visible = false;
        }
        if (!this.Page.User.IsInRole("PHYSTK"))
        {
            lnkRPS.Visible = false;
        }
        if (!this.Page.User.IsInRole("STKADJ"))
        {
            lnkTransferExecution.Visible = false;
        }
        if (!this.Page.User.IsInRole("GRPMST"))
        {
            lnkGroupInfo.Visible = false;
        }
        if (!this.Page.User.IsInRole("LDGMST"))
        {
            lnkLedgerInfo.Visible = false;
        }
        //if (!this.Page.User.IsInRole("JURNAL"))
        //{
        //    lnkJournalInfo.Visible = false;
        //}
        if (!this.Page.User.IsInRole("CDNOTE"))
        {
            lnkCreditDebitNote.Visible = false;
        }
        if (!this.Page.User.IsInRole("MULJOU"))
        {
            lnkMultipleInfo.Visible = false;
        }
        //if (!this.Page.User.IsInRole("MNL"))
        //{
        //    lnkBillAdjustment.Visible = false;
        //}
        if (!this.Page.User.IsInRole("TMENTRY"))
        {
            lnkTimeSheet.Visible = false;
        }
        //if (!this.Page.User.IsInRole("TMAPP"))
        //{
        //    lnkTimeSheetApproval.Visible = false;
        //}
        //if (!this.Page.User.IsInRole("WMENTRY"))
        //{
        //    lnkWorkManagement.Visible = false;
        //}
        //if (!this.Page.User.IsInRole("WRKUPD"))
        //{
        //    lnkWorkUpdates.Visible = false;
        //}
        //if (!this.Page.User.IsInRole("WRKCLO"))
        //{
        //    lnkWorkClosure.Visible = false;
        //}
        //if (!this.Page.User.IsInRole("SRVENT"))
        //{
        //    lnkContracts.Visible = false;
        //}
        //if (!this.Page.User.IsInRole("SRVMGMT"))
        //{
        //    lnkVisits.Visible = false;
        //}
        if (!this.Page.User.IsInRole("EMPMST"))
        {
            lnkEmployeeMast.Visible = false;
        }
        if (!this.Page.User.IsInRole("CMPMST"))
        {
            lnkCompanyinfo.Visible = false;
        }
        //if (!this.Page.User.IsInRole("SICMST"))
        //{
        //lnkInterestCalculation.Visible = false;
        //}
        //if (!this.Page.User.IsInRole("SMSDRFT"))
        //{
        //    lnkSmsText.Visible = false;
        //}
        //if (!this.Page.User.IsInRole("ITMTRK"))
        //{
        //    lnkQtyReturns.Visible = false;
        //}
        //if (!this.Page.User.IsInRole("JOBENT"))
        //{
        //    lnkJobEntry.Visible = false;
        //}
        //if (!this.Page.User.IsInRole("BILTENT"))
        //{
        //    lnkBilty.Visible = false;
        //}
        if (!this.Page.User.IsInRole("CHQMST"))
        {
            lnkChequeBook.Visible = false;
        }
        
        //if ((!this.Page.User.IsInRole("STKRPT")) && (!this.Page.User.IsInRole("STKLEDRPT")) && (!this.Page.User.IsInRole("STKRECRPT")) && (!this.Page.User.IsInRole("STKAGE")) && (!this.Page.User.IsInRole("STKCOM")) && (!this.Page.User.IsInRole("STKLVL")) && (!this.Page.User.IsInRole("OBSITEM")))
        //{
        //    lnkInventoryReports.Visible = false;
        //}
        //if ((!this.Page.User.IsInRole("SLSRPT")) && (!this.Page.User.IsInRole("SALCOM")) && (!this.Page.User.IsInRole("MISSDC")) && (!this.Page.User.IsInRole("TOTSAL")) && (!this.Page.User.IsInRole("ZERORS")) && (!this.Page.User.IsInRole("SALTURN")) && (!this.Page.User.IsInRole("SLSUMRPT")))
        //{
        //    lnkSalesReports.Visible = false;
        //}
        //if ((!this.Page.User.IsInRole("PRSUMRPT")) && (!this.Page.User.IsInRole("PURCOM")))
        //{
        //    lnkPurchaseReports.Visible = false;
        //}
        //if ((!this.Page.User.IsInRole("TRLBRPT")) && (!this.Page.User.IsInRole("SALTAX")) && (!this.Page.User.IsInRole("EXPRPT")) && (!this.Page.User.IsInRole("BALSHTRPT")) && (!this.Page.User.IsInRole("PLRPT")) && (!this.Page.User.IsInRole("BAKSTRPT")) && (!this.Page.User.IsInRole("CACCRPT")) && (!this.Page.User.IsInRole("LEDRPT")) && (!this.Page.User.IsInRole("DYBKRPT")))
        //{
        //    lnkAccReports.Visible = false;
        //}
        //if ((!this.Page.User.IsInRole("OUTRPT")) && (!this.Page.User.IsInRole("OUTAGE")))
        //{
        //    lnkOutStandingReports.Visible = false;
        //}
        //if ((!this.Page.User.IsInRole("TMEREPORT")) && (!this.Page.User.IsInRole("WKRRPT")))
        //{
        //    lnkResMgmReports.Visible = false;
        //}
        //if (!this.Page.User.IsInRole("SRVMGTRPT"))
        //{
        //    lnkServiceReports.Visible = false;
        //}
        //if (!this.Page.User.IsInRole("COMPRPT"))
        //{
        //lnkComplaintReports.Visible = false;
        //}

        //if ((!this.Page.User.IsInRole("VATSUMRPT")) && (!this.Page.User.IsInRole("VATRECON")))
        //{
        //    lnkVATReports.Visible = false;
        //}
        //if ((!this.Page.User.IsInRole("GPRPT")) && (!this.Page.User.IsInRole("CRTOWN")) && (!this.Page.User.IsInRole("CSTSMRPT")))
        //{
        //    lnkOtherReports.Visible = false;
        //}
        if (!this.Page.User.IsInRole("SYSCONF"))
        {
          //  lnkConfiguration.Visible = false;
        }
        if (!this.Page.User.IsInRole("RECDATE"))
        {
            lnkReconDate.Visible = false;
        }

        //if (!this.Page.User.IsInRole("MNGUSRS"))
        //{
        //    lnkUserMaintenance.Visible = false;
        //}
        if (!this.Page.User.IsInRole("ULOCK"))
        {
            lnkuserlock.Visible = false;
        }
        if (!this.Page.User.IsInRole("Treport"))
        {
            lnkprojectreport.Visible = false;
        }

        if (!this.Page.User.IsInRole("Mreport"))
        {
            lnkmanufacturereport.Visible = false;
        }
        //if (!this.Page.User.IsInRole("SALEREP"))
        //{
        //    A241.Visible = false;
        //}
        A10.Visible = false;
        A9.Visible = false;
        A11.Visible = false;
        A15.Visible = false;
        A23.Visible = false;
        A12.Visible = false;
        A13.Visible = false;
        A14.Visible = false;
        A21.Visible = false;
        A22.Visible = false;
        A16.Visible = false;
        lnkTimeSheet.Visible = false;
        

        //if (Helper.GetDecryptedKey("InstallationType") != "ONLINE-OFFLINE-CLIENT")
        //{
           // lnkConfiguration.Visible = true;
        //}
    }


}

