﻿using System;
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
using System.Text;
using System.IO;
using System.Data;
using ClosedXML.Excel;


public partial class ReportXLBankRecon : System.Web.UI.Page
{
    int catid = 0;
    string brand = "";
    string model = "";
    string product = "";
    decimal Gtotal = 0;
    decimal Gttl = 0;
    decimal Pttls = 0;
    decimal Ittls = 0;
    decimal brandTotal = 0, catIDTotal = 0, modelTotal = 0;
    string grpBy = "", selCols = "", selLevels = "", field1 = "", field2 = "", ordrby = "", cond = "", Subordrby="";

    //DBClass objBL = new DBClass();
    BusinessLogic objBL = new BusinessLogic();
    private string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!Page.IsPostBack)
            {
                DateTime dtCurrent = DateTime.Now;
                DateTime dtNew = new DateTime(dtCurrent.Year, dtCurrent.Month, 1);
                //txtSrtDate.Text = DateTime.Now.ToShortDateString();

                fillCustbank();
                ddlbank.Enabled = true;
                ddlCustomer.Enabled = false;
                //txtSrtDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                //txtEdDate.Text = DateTime.Now.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

   protected void btnxls_Click(object sender, EventArgs e)
   {
        try
        {
           bindData(sDataSource);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }
   }

   protected void btnReport_Click(object sender, EventArgs e)
   {
       int iLedgerID = 0;
       DateTime startDate;

       DataSet ds = new DataSet();
       DataSet dsttt = new DataSet();
       BusinessLogic objBL = new BusinessLogic();

       string Types = string.Empty;

       
       if (opnbank.SelectedItem.Text == "Bank")
       {
           if (ddlbank.SelectedValue == "0")
           {
               ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select the Bank. It cannot be left blank');", true);
               return;
           }
           else
           {
               iLedgerID = Convert.ToInt32(ddlbank.SelectedItem.Value);
               Types = "Bank";
           }
       }
       
       
       else if (opnbank.SelectedItem.Text == "Customer")
       {
           if (ddlbank.SelectedValue == "0")
           {
               ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select the Bank. It cannot be left blank');", true);
               return;
           }
           else
           {

               iLedgerID = Convert.ToInt32(ddlCustomer.SelectedItem.Value);
               Types = "Customer";
           }
       }

       string list = btnlist.SelectedItem.Text;

       Response.Write("<script language='javascript'> window.open('ReportXLBankRecon1.aspx?ledger=" + iLedgerID + "&list=" + list + "&type=" + Types + "' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");

       //if (btnlist.SelectedValue == "All")
       //{
       //}
   }

   protected void opnbank_SelectedIndexChanged(object sender, EventArgs e)
   {
       try
       {
           RadioButtonList chk = (RadioButtonList)sender;

           if (chk.SelectedItem.Text == "Bank")
           {
               ddlbank.Enabled = true;
               ddlCustomer.Enabled = false;
           }
           else
           {
               ddlbank.Enabled = false;
               ddlCustomer.Enabled = true;
           }
       }
       catch (Exception ex)
       {
           TroyLiteExceptionManager.HandleException(ex);
       }
   }

   private void fillCustbank()
   {
       DataSet ds = new DataSet();
       BusinessLogic bl = new BusinessLogic(sDataSource);
       ds = bl.ListSundryDebtorsPaymentMade(sDataSource);

       DataSet dst = new DataSet();
       dst = bl.ListBanks();

       ddlCustomer.DataSource = ds;
       ddlCustomer.DataTextField = "LedgerName";
       ddlCustomer.DataValueField = "LedgerID";
       ddlCustomer.DataBind();
       //ddlCustomer.Items.Insert(0, "All");

       ddlbank.DataSource = dst;
       ddlbank.DataTextField = "LedgerName";
       ddlbank.DataValueField = "LedgerID";
       ddlbank.DataBind();
       //ddlbank.Items.Insert(0, "All");
   }

   public void bindData(string sDataSource)
   {
         int iLedgerID = 0;
         DateTime startDate;

         DataSet ds = new DataSet();
         DataSet dsttt = new DataSet();
         BusinessLogic objBL = new BusinessLogic();

         string Types = string.Empty;
         //if (opnbank.SelectedItem.Text == "Bank")
         //{
         //    iLedgerID = Convert.ToInt32(ddlbank.SelectedItem.Value);
         //    Types = "Bank";
         //}
         //else if (opnbank.SelectedItem.Text == "Customer")
         //{
         //    iLedgerID = Convert.ToInt32(ddlCustomer.SelectedItem.Value);
         //    Types = "Customer";
         //}


             if (opnbank.SelectedItem.Text == "Bank")
             {
                 if (ddlbank.SelectedValue == "0")
                 {
                     ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select the Bank. It cannot be left blank');", true);
                     return;
                 }
                 else
                 {
                     iLedgerID = Convert.ToInt32(ddlbank.SelectedItem.Value);
                     Types = "Bank";
                 }
             }


             else if (opnbank.SelectedItem.Text == "Customer")
             {
                 if (ddlbank.SelectedValue == "0")
                 {
                     ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select the Bank. It cannot be left blank');", true);
                     return;
                 }
                 else
                 {

                     iLedgerID = Convert.ToInt32(ddlCustomer.SelectedItem.Value);
                     Types = "Customer";
                 }
             }
         

         objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

         //startDate = Convert.ToDateTime(txtSrtDate.Text);

         string usernam = Request.Cookies["LoggedUserName"].Value;



         if (btnlist.SelectedValue == "All")
         {
             dsttt = objBL.checkbankreconciliation1(iLedgerID, sDataSource, usernam, Types);

             if (dsttt != null)
             {
                 if (dsttt.Tables[0].Rows.Count > 0)
                 {
                     dsttt = objBL.getbankreconciliation2(iLedgerID, sDataSource, usernam);
                     ds = objBL.getbankrecon1(iLedgerID, sDataSource, usernam, Types);

                     if (dsttt != null)
                     {
                         if (dsttt.Tables[0].Rows.Count > 0)
                             ds.Tables[0].Merge(dsttt.Tables[0]);
                     }
                 }
                 else
                 {
                     ds = objBL.getbankreconciliation3(iLedgerID, sDataSource, usernam);
                 }
             }
             else
             {
                 ds = objBL.getbankreconciliation3(iLedgerID, sDataSource, usernam);
             }

             if (ds != null)
             {
                 DataTable dt = new DataTable("bank ercon");
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     dt.Columns.Add(new DataColumn("TransNo"));
                     dt.Columns.Add(new DataColumn("TransDate"));
                     dt.Columns.Add(new DataColumn("Name"));
                     dt.Columns.Add(new DataColumn("Ledger Name"));
                     dt.Columns.Add(new DataColumn("Branch"));
                     dt.Columns.Add(new DataColumn("Amount"));
                     dt.Columns.Add(new DataColumn("Narration"));
                     dt.Columns.Add(new DataColumn("Voucher Type"));
                     dt.Columns.Add(new DataColumn("ChequeNo"));
                     dt.Columns.Add(new DataColumn("Reconcilated By"));
                     dt.Columns.Add(new DataColumn("Reconcilated Date"));
                     dt.Columns.Add(new DataColumn("Remarks"));

                     DataRow dr_final679 = dt.NewRow();
                     dt.Rows.Add(dr_final679);

                     foreach (DataRow dr in ds.Tables[0].Rows)
                     {
                         DataRow dr_final6 = dt.NewRow();
                         dr_final6["TransNo"] = dr["TransNo"];
                         dr_final6["TransDate"] = dr["TransDate"];
                         dr_final6["Name"] = dr["Debtor"];
                         dr_final6["Ledger Name"] = dr["Creditor"];
                         dr_final6["Branch"] = dr["BranchCode"];
                         dr_final6["Amount"] = dr["Amount"];
                         dr_final6["Narration"] = dr["Narration"];
                         dr_final6["ChequeNo"] = dr["ChequeNo"];
                         dr_final6["Voucher Type"] = dr["VoucherType"];
                         dr_final6["Reconcilated By"] = dr["ReconcilatedBy"];
                         dr_final6["Reconcilated Date"] = dr["Reconcilateddate"];
                         dr_final6["Remarks"] = dr["Result"];
                         dt.Rows.Add(dr_final6);
                     }
                     ExportToExcel(dt);
                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                 }
             }
             else
             {
                 ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
             }
         }
         else if (btnlist.SelectedValue == "Pending")
         {
             dsttt = objBL.getbankreconciliation2(iLedgerID, sDataSource, usernam);

             if (dsttt != null)
             {
                 DataTable dt = new DataTable("Bank recon");
                 if (dsttt.Tables[0].Rows.Count > 0)
                 {
                     dt.Columns.Add(new DataColumn("TransNo"));
                     dt.Columns.Add(new DataColumn("TransDate"));
                     dt.Columns.Add(new DataColumn("Name"));
                     dt.Columns.Add(new DataColumn("Ledger Name"));
                     dt.Columns.Add(new DataColumn("Branch"));
                     dt.Columns.Add(new DataColumn("Amount"));
                     dt.Columns.Add(new DataColumn("Narration"));
                     dt.Columns.Add(new DataColumn("Voucher Type"));
                     dt.Columns.Add(new DataColumn("ChequeNo"));
                     dt.Columns.Add(new DataColumn("Reconcilated By"));
                     dt.Columns.Add(new DataColumn("Reconcilated Date"));
                     dt.Columns.Add(new DataColumn("Remarks"));

                     DataRow dr_final679 = dt.NewRow();
                     dt.Rows.Add(dr_final679);

                     foreach (DataRow dr in dsttt.Tables[0].Rows)
                     {
                         DataRow dr_final6 = dt.NewRow();
                         dr_final6["TransNo"] = dr["TransNo"];
                         dr_final6["TransDate"] = dr["TransDate"];
                         dr_final6["Name"] = dr["Debtor"];
                         dr_final6["Ledger Name"] = dr["Creditor"];
                         dr_final6["Branch"] = dr["BranchCode"];
                         dr_final6["Amount"] = dr["Amount"];
                         dr_final6["Narration"] = dr["Narration"];
                         dr_final6["ChequeNo"] = dr["ChequeNo"];
                         dr_final6["Voucher Type"] = dr["VoucherType"];
                         dr_final6["Reconcilated By"] = dr["ReconcilatedBy"];
                         dr_final6["Reconcilated Date"] = dr["Reconcilateddate"];
                         dr_final6["Remarks"] = dr["Result"];
                         dt.Rows.Add(dr_final6);
                     }
                     ExportToExcel(dt);
                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                 }
             }
             else
             {
                 ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
             }
         }
         else if (btnlist.SelectedValue == "Reconciliated")
         {
             ds = objBL.getbankrecon1(iLedgerID, sDataSource, usernam, Types);

             if (ds != null)
             {
                 DataTable dt = new DataTable("Bank recon");
                 if (ds.Tables[0].Rows.Count > 0)
                 {
                     dt.Columns.Add(new DataColumn("TransNo"));
                     dt.Columns.Add(new DataColumn("TransDate"));
                     dt.Columns.Add(new DataColumn("Name"));
                     dt.Columns.Add(new DataColumn("Ledger Name"));
                     dt.Columns.Add(new DataColumn("BranchCode"));
                     dt.Columns.Add(new DataColumn("Amount"));
                     dt.Columns.Add(new DataColumn("Narration"));
                     dt.Columns.Add(new DataColumn("Voucher Type"));
                     dt.Columns.Add(new DataColumn("ChequeNo"));
                     dt.Columns.Add(new DataColumn("Reconcilated By"));
                     dt.Columns.Add(new DataColumn("Reconcilated Date"));
                     dt.Columns.Add(new DataColumn("Remarks"));

                     DataRow dr_final679 = dt.NewRow();
                     dt.Rows.Add(dr_final679);

                     foreach (DataRow dr in ds.Tables[0].Rows)
                     {
                         DataRow dr_final6 = dt.NewRow();
                         dr_final6["TransNo"] = dr["TransNo"];
                         dr_final6["TransDate"] = dr["TransDate"];
                         dr_final6["Name"] = dr["Debtor"];
                         dr_final6["Ledger Name"] = dr["Creditor"];
                         dr_final6["Branch"] = dr["BranchCode"];
                         dr_final6["Amount"] = dr["Amount"];
                         dr_final6["Narration"] = dr["Narration"];
                         dr_final6["ChequeNo"] = dr["ChequeNo"];
                         dr_final6["Voucher Type"] = dr["VoucherType"];
                         dr_final6["Reconcilated By"] = dr["ReconcilatedBy"];
                         dr_final6["Reconcilated Date"] = dr["Reconcilateddate"];
                         dr_final6["Remarks"] = dr["Result"];
                         dt.Rows.Add(dr_final6);
                     }
                     ExportToExcel(dt);
                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
                 }
             }
             else
             {
                 ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
             }
         }
     }

     protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
     {

     }

     public void ExportToExcel(DataTable dt)
     {

         if (dt.Rows.Count > 0)
         {
             using (XLWorkbook wb = new XLWorkbook())
             {
                 string filename = "Bank recon.xlsx";
                 wb.Worksheets.Add(dt);
                 Response.Clear();
                 Response.Buffer = true;
                 Response.Charset = "";
                 Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                 Response.AddHeader("content-disposition", "attachment;filename=" + filename + "");
                 using (MemoryStream MyMemoryStream = new MemoryStream())
                 {
                     wb.SaveAs(MyMemoryStream);
                     MyMemoryStream.WriteTo(Response.OutputStream);
                     Response.Flush();
                     Response.End();
                 }
             }
         }
     }
    
}