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
using System.Text;
using System.IO;


public partial class ReportXLBankRec : System.Web.UI.Page
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
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();

                fillCustbank();
                //ddlbank.Enabled = true;
                //ddlCustomer.Enabled = false;
                //txtSrtDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
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
            if (optionmethod.SelectedValue == "ReconciliatedDate")
            {
                bindData(sDataSource);
            }
            else if (optionmethod.SelectedValue == "TransDate")
            {
                bindData1(sDataSource);
            }
           
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            return;
        }
   }

   protected void btngriddata_Click(object sender, EventArgs e)
   {
       //try
       //{
       //    DateTime startDate, endDate;
       //    startDate = Convert.ToDateTime(txtSrtDate.Text);
       //    endDate = Convert.ToDateTime(txtEdDate.Text);
       //    string itemcode;
       //    itemcode = "";

       //    DataSet ds = new DataSet();
       //    ds = objBL.getstockstatement(sDataSource, startDate, endDate, itemcode);

       //    gvLedger.DataSource = ds;
       //    gvLedger.DataBind();
       //}
       //catch (Exception ex)
       //{
       //    var error = ex.Message.Replace("'", "");
       //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Error Occured : " + @error + "');", true);
       //    return;
       //}
   }

   protected void opnbank_SelectedIndexChanged(object sender, EventArgs e)
   {
       //RadioButtonList chk = (RadioButtonList)sender;

       //if (chk.SelectedItem.Text == "Bank")
       //{
       //    ddlbank.Enabled = true;
       //    ddlCustomer.Enabled = false;
       //}
       //else
       //{
       //    ddlbank.Enabled = false;
       //    ddlCustomer.Enabled = true;
       //}

   }

   private void fillCustbank()
   {
       DataSet ds = new DataSet();
       BusinessLogic bl = new BusinessLogic(sDataSource);
       ds = bl.ListSundryDebtorsPaymentMade(sDataSource);

       DataSet dst = new DataSet();
       dst = bl.ListBanks();

       //ddlCustomer.DataSource = ds;
       //ddlCustomer.DataTextField = "LedgerName";
       //ddlCustomer.DataValueField = "LedgerID";
       //ddlCustomer.DataBind();
       ////ddlCustomer.Items.Insert(0, "All");

       //ddlbank.DataSource = dst;
       //ddlbank.DataTextField = "LedgerName";
       //ddlbank.DataValueField = "LedgerID";
       //ddlbank.DataBind();
       //ddlbank.Items.Insert(0, "All");
   }

   public void bindData(string sDataSource)
   {
         int iLedgerID = 0;
         DateTime startDate, EndDate;

         DataSet ds = new DataSet();
         DataSet dsttt = new DataSet();
         BusinessLogic objBL = new BusinessLogic();

         string Types = string.Empty;         
         objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
         Types = "ReconcilatedDate";
         startDate = Convert.ToDateTime(txtStartDate.Text);
         EndDate = Convert.ToDateTime(txtEndDate.Text);

         string usernam = Request.Cookies["LoggedUserName"].Value;

         ds = objBL.getbankrecon2(sDataSource, startDate, EndDate, usernam, Types);
 
        if (ds != null)
        {
            DataTable dt = new DataTable();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("Reconcilated Date"));
                dt.Columns.Add(new DataColumn("TransNo"));
                dt.Columns.Add(new DataColumn("TransDate"));
                dt.Columns.Add(new DataColumn("Name"));
                dt.Columns.Add(new DataColumn("Ledger Name"));
                dt.Columns.Add(new DataColumn("Amount"));
                dt.Columns.Add(new DataColumn("Narration"));
                dt.Columns.Add(new DataColumn("Voucher Type"));
                dt.Columns.Add(new DataColumn("ChequeNo"));
                dt.Columns.Add(new DataColumn("Reconcilated By"));
                
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

   public void bindData1(string sDataSource)
   {
       int iLedgerID = 0;
       DateTime startDate, EndDate;

       DataSet ds = new DataSet();
       DataSet dsttt = new DataSet();
       BusinessLogic objBL = new BusinessLogic();

       string Types = string.Empty;

       Types = "TransDate";
       objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

       startDate = Convert.ToDateTime(txtStartDate.Text);
       EndDate = Convert.ToDateTime(txtEndDate.Text);

       string usernam = Request.Cookies["LoggedUserName"].Value;

       ds = objBL.getbankrecon2(sDataSource, startDate, EndDate, usernam, Types);

       if (ds != null)
       {
           DataTable dt = new DataTable();
           if (ds.Tables[0].Rows.Count > 0)
           {                             
               dt.Columns.Add(new DataColumn("TransDate"));
               dt.Columns.Add(new DataColumn("TransNo"));
               dt.Columns.Add(new DataColumn("Name"));
               dt.Columns.Add(new DataColumn("Ledger Name"));
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

     protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
     {

     }

     public void ExportToExcel(DataTable dt)
     {

         if (dt.Rows.Count > 0)
         {
             //string filename = "Bank Reconciliation.xls";
             string filename = "Bank Reconciliation_" + DateTime.Now.ToString() + ".xls";
             System.IO.StringWriter tw = new System.IO.StringWriter();
             System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
             DataGrid dgGrid = new DataGrid();
             dgGrid.DataSource = dt;
             dgGrid.DataBind();
             dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
             dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
             dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
             dgGrid.HeaderStyle.Font.Bold = true;
             //Get the HTML for the control.
             dgGrid.RenderControl(hw);
             //Write the HTML back to the browser.
             Response.ContentType = "application/vnd.ms-excel";
             Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
             this.EnableViewState = false;
             Response.Write(tw.ToString());
             Response.End();
         }
     }
    
}