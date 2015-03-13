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
using System.IO;
using System.Web.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Xml;
//using JRO;

public partial class DBYearEnd : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    private string sDtSource = string.Empty;
    //public const string AccessOleDbConnectionStringFormat ="Data Source={0};Provider=Microsoft.Jet.OLEDB.4.0;";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if(!IsPostBack)
        //SetDbName();

        loadYear();
    }

    public string GetCurrentDBName(string con)
    {
        string str = con; // "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\\DemoDev\\Accsys\\App_Data\\sairama.mdb;Persist Security Info=True;Jet OLEDB:Database Password=moonmoon";
        string str2 = string.Empty;
        if (str.Contains(".mdb"))
        {
            str2 = str.Substring(str.IndexOf("Data Source"), str.IndexOf("Persist", 0));
            str2 = str2.Substring(str2.LastIndexOf("\\") + 1, str2.IndexOf(";") - str2.LastIndexOf("\\"));
            if (str2.EndsWith(";"))
            {
                str2 = str2.Remove(str2.Length - 5, 5);
            }
        }
        return str2;
    }

    private void loadYear()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpYear.Items.Clear();
        drpYear.Items.Add(new ListItem("Select Year", "0"));
        ds = bl.ListYear(connection);
        
        drpYear.DataTextField = "YearName";
        drpYear.DataValueField = "YearName";
        drpYear.DataSource = ds;
        drpYear.DataBind();

    }

    protected void btnAccount_Click(object sender, EventArgs e)
    {
        string NewDB="";
        string fileName = "Reports//" + System.Guid.NewGuid().ToString() + ConfigurationManager.AppSettings["OutstandingFileName"].ToString();
        string sXmlPath = Server.MapPath(fileName);
        string path = string.Empty;
        string curr = string.Empty;
        string monthname = string.Empty;
        string DBcompany = string.Empty;
        string DBname = string.Empty;
        BusinessLogic bl = new BusinessLogic();
        string ddd = Request.Cookies["Company"].Value;
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        DBname = GetCurrentDBName(sDataSource);//ConfigurationSettings.AppSettings["DBName"].ToString();
        try
        {
            if (Page.IsValid)
            {

                //if (File.Exists(Server.MapPath("App_Data\\" + DBname + ".mdb")))
                //{
                //    lblMsg.Text = "New account has already been created. You are not allowed to create again.";
                //    return;
                //}

                if (Request.Cookies["Company"] != null)
                    DBcompany = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("~/Login.aspx");

                if (DateTime.Now.Month == 12)
                    monthname = "December";
                else if (DateTime.Now.Month == 11)
                    monthname = "November";
                else if (DateTime.Now.Month == 10)
                    monthname = "October";
                else if (DateTime.Now.Month == 9)
                    monthname = "September";
                else if (DateTime.Now.Month == 8)
                    monthname = "August";
                else if (DateTime.Now.Month == 7)
                    monthname = "July";
                else if (DateTime.Now.Month == 6)
                    monthname = "June";
                else if (DateTime.Now.Month == 5)
                    monthname = "May";
                else if (DateTime.Now.Month == 4)
                    monthname = "April";
                else if (DateTime.Now.Month == 3)
                    monthname = "March";
                else if (DateTime.Now.Month == 2)
                    monthname = "February";
                else
                    monthname = "January";

                //curr = monthname + "_" + DateTime.Now.Year;
                //path = Server.MapPath("OldYear\\"+ DBname + "_curr.mdb");
                //curr = txtName.Text.Trim();


                string a = string.Empty;
                string b = string.Empty;
                int val = 0;
                for (int i = 0; i < DBname.Length; i++)
                {
                    if (Char.IsDigit(DBname[i]))
                        b += DBname[i];
                    else
                        a += DBname[i];
                }
                if (b.Length > 0)
                {
                    val = int.Parse(b);
                    val = val + 1;
                }

                //path = Server.MapPath("App_Data\\" + DBname + ".mdb");
                
                bl.changeReconDate(sDataSource);

                //BusinessLogic objChk = new BusinessLogic();
                //sDtSource = ConfigurationManager.ConnectionStrings[connection].ToString();
                //string sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDtSource;

                using (SqlConnection con = new SqlConnection())
                {
                    SqlCommand command = new SqlCommand();
                    SqlTransaction transaction = null;
                    SqlDataAdapter adapter = null;

                    // Set the Connection to the new OleDbConnection.
                    command.Connection = con;

                    con.Open();

                    transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);

                    command.Connection = con;
                    command.Transaction = transaction;

                    string nowdate = "31/03/" + val;

                    command.CommandText = string.Format("Update last_recon Set recon_date=Format('" + nowdate + "', 'MM/dd/yyyy')");
                    command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Update tblLedger SET Debit={0},Credit={1},OpenBalanceDr={2},OpenBalanceCr={3},OpDueDate=Format('{4}', 'dd/mm/yyyy') ", 0, 0, 0, 0, nowdate);
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblAudit");
                    //command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditBrand");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditCategories");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditCreditDebitNote");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditDayBook");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditLedger");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditPayMent");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditPaymentAmount");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditPurchase");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditProductMaster");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditPurchaseItems");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditReceipt");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditReceivedAmount");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditSales");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditSalesItems");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditTaskTypes");
                    command.ExecuteNonQuery();

                    command.CommandText = string.Format("Delete from tblAuditTaskStatus");
                    command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblBankRec");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblBankRecon");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblCommission");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblCommissionItems");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblCompetitors");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblCreditDebitNote");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblDayBook");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblExecution");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblFormula");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblPurchaseItems");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblPayMent");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblPaymentAmount");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblProjects");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblPurchase");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblReceipt");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblReceivedAmount");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblSales");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblSalesItems");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblPurchase");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblTimeSheetEntry");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblOpeningDcSales");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblopeningDcSalesItems");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblPurchase");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblPurchase");
                    //command.ExecuteNonQuery();

                    //command.CommandText = string.Format("Delete from tblPurchase");
                    //command.ExecuteNonQuery();
                    //command.CommandText = string.Format("Update tblProductMaster SET Stock={0}", 0);
                    //command.ExecuteNonQuery();

                    transaction.Commit();
                    con.Close();
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + DBname + " is Refreshed Successfully.');", true);

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);        
            return;

            //if (File.Exists(path))
            //{
            //    File.Delete(Server.MapPath("App_Data\\" + DBname + ".mdb"));
            //    System.IO.File.Move(path, Server.MapPath("App_Data\\" + DBname + ".mdb"));
            //}
            //if (File.Exists(path))
            //    System.IO.File.Delete(path);

            //bool success = bl.CreateNewAccount(connection, fileName, sXmlPath, curr);
            //if (success)
            //{
            //    lblMsg.Text = "New Account is created successfully";
            //    //AddConnStr(Request.Cookies["Company"].Value + " " + curr, path);
            //    AddConnectionStrings(a + val, path); 
            //}
            //else
            //{
            //    if (File.Exists(Server.MapPath("App_Data\\" + DBname + ".mdb")))
            //    {
            //        File.Delete(Server.MapPath("App_Data\\" + DBname + ".mdb"));
            //        System.IO.File.Move(path, Server.MapPath("App_Data\\" + DBname + ".mdb"));
            //    }
            //    if (File.Exists(path))
            //        System.IO.File.Delete(path);
            //    lblMsg.Text = "Problem in creating the Account Please Contact the Administrator";
            //}
        }
    }

    protected void Button123_Click(object sender, EventArgs e)
    {
        string NewDB = "";
        string fileName = "Reports//" + System.Guid.NewGuid().ToString() + ConfigurationManager.AppSettings["OutstandingFileName"].ToString();
        string sXmlPath = Server.MapPath(fileName);
        string path = string.Empty;
        string curr = string.Empty;
        string monthname = string.Empty;
        string DBcompany = string.Empty;
        string DBname = string.Empty;
        BusinessLogic bl = new BusinessLogic();
        string ddd = Request.Cookies["Company"].Value;
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        DBname = GetCurrentDBName(sDataSource);//ConfigurationSettings.AppSettings["DBName"].ToString();
        try
        {
            if (Page.IsValid)
            {

                //if (File.Exists(Server.MapPath("App_Data\\" + DBname + ".mdb")))
                //{
                //    lblMsg.Text = "New account has already been created. You are not allowed to create again.";
                //    return;
                //}

                if (Request.Cookies["Company"] != null)
                    DBcompany = Request.Cookies["Company"].Value;
                else
                    Response.Redirect("~/Login.aspx");

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                //string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                if (indianStd.Month == 12)
                    monthname = "December";
                else if (indianStd.Month == 11)
                    monthname = "November";
                else if (indianStd.Month == 10)
                    monthname = "October";
                else if (indianStd.Month == 9)
                    monthname = "September";
                else if (indianStd.Month == 8)
                    monthname = "August";
                else if (indianStd.Month == 7)
                    monthname = "July";
                else if (indianStd.Month == 6)
                    monthname = "June";
                else if (indianStd.Month == 5)
                    monthname = "May";
                else if (indianStd.Month == 4)
                    monthname = "April";
                else if (indianStd.Month == 3)
                    monthname = "March";
                else if (indianStd.Month == 2)
                    monthname = "February";
                else
                    monthname = "January";

                if (monthname == "April")
                {
                    
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Year closing can be done only in April');", true);
                    //return;
                }

                //curr = monthname + "_" + DateTime.Now.Year;
                //path = Server.MapPath("OldYear\\"+ DBname + "_curr.mdb");
                //curr = txtName.Text.Trim();


                string a = string.Empty;
                string b = string.Empty;
                int val = 0;
                for (int i = 0; i < DBname.Length; i++)
                {
                    if (Char.IsDigit(DBname[i]))
                        b += DBname[i];
                    else
                        a += DBname[i];
                }
                if (b.Length > 0)
                {
                    val = int.Parse(b);
                    val = val + 1;
                }

                //path = Server.MapPath("App_Data\\" + DBname + ".mdb");

                //bl.changeReconDate(sDataSource);

                BusinessLogic objChk = new BusinessLogic();
                //sDtSource = ConfigurationManager.ConnectionStrings[connection].ToString();
                //string sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDtSource;

                using (SqlConnection con = new SqlConnection(objChk.CreateConnectionString(sDataSource)))
                {
                    SqlCommand command = new SqlCommand();
                    SqlTransaction transaction = null;
                    SqlDataAdapter adapter = null;

                    //command.CommandTimeout = 0;

                    try
                    {
                        // Set the Connection to the new OleDbConnection.
                        command.Connection = con;

                        con.Open();

                        transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);

                        command.Connection = con;
                        command.Transaction = transaction;

                        string nowdate = drpYear.SelectedValue + "-03-31";

                        command.CommandText = string.Format("Update last_recon Set recon_date = " + nowdate );
                        command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Update tblLedger SET Debit={0},Credit={1},OpenBalanceDr={2},OpenBalanceCr={3},OpDueDate=Format('{4}', 'dd/mm/yyyy') ", 0, 0, 0, 0, nowdate);
                        //command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndSales select * from tblSales");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndSalesitems select * from tblSalesitems");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndPurchase select * from tblPurchase");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndPurchaseitems select * from tblPurchaseitems");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndStock select * from tblStock");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndBankRec select * from tblBankRec");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndBankRecon select * from tblBankRecon");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndReceipt select * from tblReceipt");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndReceivedAmount select * from tblReceivedAmount");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndPayMent select * from tblPayMent");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndDayBook select * from tblDayBook");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndCreditDebitNote select * from tblCreditDebitNote");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndPaymentAmount select * from tblPaymentAmount");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndExecution select * from tblExecution");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndFormula select * from tblFormula");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndOpeningDcSales select * from tblOpeningDcSales");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndopeningDcSalesItems select * from tblopeningDcSalesItems");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndCompetitors select * from tblCompetitors");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndActivities select * from tblActivities");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndCompProduct select * from tblCompProduct");
                        command.ExecuteNonQuery();

                        //command.CommandText = string.Format("INSERT INTO tblYearEndCompetitors select * from tblCompetitors");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAudit");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditBrand");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditCategories");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditCreditDebitNote");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditDayBook");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditLedger");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditPayMent");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditPaymentAmount");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditPurchase");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditProductMaster");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditPurchaseItems");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditReceipt");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditReceivedAmount");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditSales");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditSalesItems");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditTaskTypes");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblAuditTaskStatus");
                        //command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblBankRec");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblBankRecon");
                        command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblCommission");
                        //command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblCommissionItems");
                        //command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblStock");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblCreditDebitNote");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblLeadHeader");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblProductInterest");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblActivities");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblCompetitors");
                        command.ExecuteNonQuery();


                        command.CommandText = string.Format("Delete from tblDayBook");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblExecution");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblFormula");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblPurchaseItems");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblPayMent");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblPaymentAmount");
                        command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblProjects");
                        //command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblPurchase");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblReceipt");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblReceivedAmount");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblSales");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblSalesItems");
                        command.ExecuteNonQuery();

                        //command.CommandText = string.Format("Delete from tblTimeSheetEntry");
                        //command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblOpeningDcSales");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblopeningDcSalesItems");
                        command.ExecuteNonQuery();

                        command.CommandText = string.Format("Delete from tblCompProduct");
                        command.ExecuteNonQuery();

                        string ItemCode = string.Empty;
                        double CurrentStock = 0;

                        DataSet dsddd = bl.GetAllMasters(sDataSource, "Product", "");
                        if (dsddd != null)
                        {
                            if (dsddd.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsddd.Tables[0].Rows)
                                {
                                    ItemCode = Convert.ToString(dr["ItemCode"]);
                                    CurrentStock = Convert.ToDouble(dr["Stock"]);

                                    command.CommandText = string.Format("Insert into tblStock(itemcode,openingstock,productname,model,productdesc,categoryid,DueDate,BranchCode) Values('{0}',{1},'{2}','{3}','{4}',{5},'{6}','{7}')", ItemCode, CurrentStock, Convert.ToString(dr["productname"]), Convert.ToString(dr["model"]), Convert.ToString(dr["productdesc"]), Convert.ToInt32(dr["categoryid"]), indianStd.ToString("yyyy-MM-dd"), Convert.ToString(dr["BranchCode"]));
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        //command.CommandText = string.Format("Update tblProductStock SET Stock={0}", 0);
                        //command.ExecuteNonQuery();

                        command.CommandText = string.Format("INSERT INTO tblYearEndLedger select * from tblLedger where openbalancedr > 0 or openbalancecr > 0");
                        command.ExecuteNonQuery();

                        int iGroupID = 0;
                        string sXmlNodeName = "Outstanding";
                        string sLedger = string.Empty;
                        double obD = 0;
                        double obC = 0;

                        //string connection = Request.Cookies["Company"].Value;

                        DataSet dsdata = bl.GetYearEndOutStandingReport(sDataSource, "", "");
                        if (dsdata != null)
                        {
                            if (dsdata.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in dsdata.Tables[0].Rows)
                                {
                                    obD = Convert.ToDouble(dr["Debit"]);
                                    obC = Convert.ToDouble(dr["Credit"]);
                                    sLedger = dr["LedgerName"].ToString();
                                    command.CommandText = string.Format("Update tblLedger SET Debit={0},Credit={1},OpenBalanceDr={2},OpenBalanceCr={3},opDueDate='{4}' Where LedgerName = '{5}'", 0, 0, obD, obC, indianStd.ToString("yyyy-MM-dd"), sLedger);
                                    command.ExecuteNonQuery();

                                }
                            }
                        }

                        transaction.Commit();
                        con.Close();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + drpYear.SelectedValue + " year is closed Successfully.');", true);

                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            
                            transaction.Rollback();
                            
                        }
                        catch (Exception ep)
                        {

                        }
                    }
                }

               

            }
        }
        catch (Exception ex)
        {
        
                TroyLiteExceptionManager.HandleException(ex);
                return;
   
        }
    }


    public static void AddConnectionStrings(string csName, string path)
    {

        // Get the count of the connection strings.
        int connStrCnt =
            ConfigurationManager.ConnectionStrings.Count;

        // Define the string name.
        //string csName = "ConnStr" +       connStrCnt.ToString();
        if (!ConfigurationManager.ConnectionStrings.IsReadOnly())
        {
            // Get the configuration file.
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);

            // Add the connection string.
            ConnectionStringsSection csSection =
                config.ConnectionStrings;
            csSection.ConnectionStrings.Add(
                new ConnectionStringSettings(csName,
                    "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=True;Jet OLEDB:Database Password=moonmoon", "System.Data.OleDb"));

            //css.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=True;Jet OLEDB:Database Password=moonmoon";
            //css.ProviderName = "System.Data.OleDb";

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);

            //Console.WriteLine("Connection string added.");
        }
    }

    private void AddUpdateConnectionString(string connection,string path1,string name)
    {
        bool isNew = false;
        string path = Server.MapPath("~/Web.Config");
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNodeList list = doc.DocumentElement.SelectNodes(string.Format("connectionStrings/add[@name='{0}']", name));
        XmlNode node;
        isNew = list.Count == 0;
        if (isNew)
        {
            node = doc.CreateNode(XmlNodeType.Element, "add", null);
            XmlAttribute attribute = doc.CreateAttribute("name");
            attribute.Value = connection;
            node.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("connectionString");
            attribute.Value = name;
            node.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("providerName");
            attribute.Value = connection;
            node.Attributes.Append(attribute);
        }
        else
        {
            node = list[0];
        }
        //string conString = node.Attributes["connectionString"].Value;
        //OleDbConnectionStringBuilder conStringBuilder = new OleDbConnectionStringBuilder(conString);
        //conStringBuilder.ConnectionString = name.ToString();
        //conStringBuilder.Values
        //conStringBuilder.DataSource = name;
        //conStringBuilder.Password = "moonmoon";
        //node.Attributes["connectionString"].Value = conStringBuilder.ConnectionString;
        if (isNew)
        {
            doc.DocumentElement.SelectNodes("connectionStrings")[0].AppendChild(node);
        }
        doc.Save(path);
    }

    public void AddConnStr(string csName, string path)
    {
        try
        {
            System.Configuration.Configuration webConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            ConnectionStringsSection dbConnString = webConfig.ConnectionStrings;
            dbConnString.ConnectionStrings.Add(new ConnectionStringSettings(csName, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=True;Jet OLEDB:Database Password=moonmoon", "System.Data.OleDb"));
            webConfig.Save();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }

    protected void btnOutstanding_Click(object sender, EventArgs e)
    {
        
    }

    protected void userupd_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic();
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            DataSet dsd = bl.GetYearEndUpdation(sDataSource);

            string connection = "";
            string path = "";
            string userupd = "";
            if (dsd != null)
            {
                if (dsd.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsd.Tables[0].Rows)
                    {
                        connection = Convert.ToString(dr["yearname"]);
                        path = Convert.ToString(dr["path"]);
                        userupd = Convert.ToString(dr["userupd"]);
                    }
                }
            }

            if (userupd == "Y")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Users has already been created.');", true);
                return;
            }

            //BusinessLogic objChk = new BusinessLogic();
            //sDtSource = ConfigurationManager.ConnectionStrings[connection].ToString();
            //string sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDtSource;

            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=True;Jet OLEDB:Database Password=moonmoon"))
            {
                OleDbCommand command = new OleDbCommand();
                OleDbTransaction transaction = null;
                OleDbDataAdapter adapter = null;

                // Set the Connection to the new OleDbConnection.
                command.Connection = con;

                con.Open();

                transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);

                command.Connection = con;
                command.Transaction = transaction;

                DataSet dsdata = bl.GetAllMasters(sDataSource, "Users", "");
                string UserName = "";
                string UserId = "";
                string Email = "";

                Int32 Count = 0;
                if (dsdata != null)
                {
                    if (dsdata.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsdata.Tables[0].Rows)
                        {
                            UserName = Convert.ToString(dr["UserName"]);
                            UserId = Convert.ToString(dr["UserId"]);
                            Email = Convert.ToString(dr["Email"]);
                            command.CommandText = string.Format("INSERT INTO tblUserInfo(UserId,UserName,Userpwd,UserGroup,Email,Locked,DateLock) Values('{0}','{1}','{2}','{3}','{4}',{5},{6})", UserId, UserName, Convert.ToString(dr["Userpwd"]), Convert.ToString(dr["UserGroup"]), Email, Convert.ToBoolean(dr["Locked"]), Convert.ToBoolean(dr["DateLock"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet ds = bl.GetAllMasters(sDataSource, "UserRole", "");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            command.CommandText = string.Format("INSERT INTO tblUserRole(UserName,Role) Values('{0}','{1}')", Convert.ToString(dr["UserName"]), Convert.ToString(dr["Role"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dst = bl.GetAllMasters(sDataSource, "UserOptions", "");
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dst.Tables[0].Rows)
                        {
                            //command.CommandText = string.Format("INSERT INTO tblUserOptions(UserName,Role,RoleDesc,Section,Area,OrderNo,Add,Edit,Delete,Views) Values('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9})", Convert.ToString(dr["UserName"]), Convert.ToString(dr["Role"]), Convert.ToString(dr["RoleDesc"]), Convert.ToString(dr["Section"]), Convert.ToString(dr["Area"]), Convert.ToString(dr["OrderNo"]), Convert.ToBoolean(dr["Add"]), Convert.ToBoolean(dr["Edit"]), Convert.ToBoolean(dr["Delete"]), Convert.ToBoolean(dr["Views"]));
                            command.CommandText = string.Format("INSERT INTO tblUserOptions Values('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9})", Convert.ToString(dr["UserName"]), Convert.ToString(dr["Role"]), Convert.ToString(dr["RoleDesc"]), Convert.ToString(dr["Section"]), Convert.ToString(dr["Area"]), Convert.ToString(dr["OrderNo"]), Convert.ToBoolean(dr["Add"]), Convert.ToBoolean(dr["Edit"]), Convert.ToBoolean(dr["Delete"]), Convert.ToBoolean(dr["Views"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                transaction.Commit();
                con.Close();

                bl.UpdateYearEndUpdation(sDataSource, path, connection, "", "User");

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Users are updated.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void masupd_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic();
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            DataSet dsd = bl.GetYearEndUpdation(sDataSource);

            string connection = "";
            string path = "";
            string mastupd = "";
            if (dsd != null)
            {
                if (dsd.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsd.Tables[0].Rows)
                    {
                        connection = Convert.ToString(dr["yearname"]);
                        path = Convert.ToString(dr["path"]);
                        mastupd = Convert.ToString(dr["mastupd"]);
                    }
                }
            }

            if (mastupd == "Y")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Masters has already been created.');", true);
                return;
            }

            //BusinessLogic objChk = new BusinessLogic();
            //sDtSource = ConfigurationManager.ConnectionStrings[connection].ToString();
            //string sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDtSource;

            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=True;Jet OLEDB:Database Password=moonmoon"))
            {
                OleDbCommand command = new OleDbCommand();
                OleDbTransaction transaction = null;
                OleDbDataAdapter adapter = null;

                // Set the Connection to the new OleDbConnection.
                command.Connection = con;

                con.Open();

                transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);

                command.Connection = con;
                command.Transaction = transaction;

                DataSet dsdata = bl.GetAllMasters(sDataSource, "Category", "");
                string CategoryName = "";
                Int32 Categorylevel = 0;
                Int32 CategoryId = 0;
                if (dsdata != null)
                {
                    if (dsdata.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsdata.Tables[0].Rows)
                        {
                            CategoryName = Convert.ToString(dr["CategoryName"]);
                            Categorylevel = Convert.ToInt32(dr["Categorylevel"]);
                            CategoryId = Convert.ToInt32(dr["CategoryId"]);
                            command.CommandText = string.Format("INSERT INTO tblCategories(CategoryId,CategoryName,Categorylevel) Values({0},'{1}',{2})", CategoryId, CategoryName, Categorylevel);
                            command.ExecuteNonQuery();
                        }
                    }
                }


                DataSet dsdd = bl.GetAllMasters(sDataSource, "Brand", "");
                string BrandName = "";
                Int32 Brandlevel = 0;
                Int32 BrandId = 0;
                Int32 Deviation = 0;
                if (dsdd != null)
                {
                    if (dsdd.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsdd.Tables[0].Rows)
                        {
                            BrandName = Convert.ToString(dr["BrandName"]);
                            Brandlevel = Convert.ToInt32(dr["Brandlevel"]);
                            BrandId = Convert.ToInt32(dr["BrandId"]);
                            Deviation = Convert.ToInt32(dr["Deviation"]);
                            command.CommandText = string.Format("INSERT INTO tblBrand(BrandName,Brandlevel,BrandId,Deviation) Values('{0}',{1},{2},{3})", BrandName, Brandlevel, BrandId, Deviation);
                            command.ExecuteNonQuery();
                        }
                    }
                }


                DataSet dsddd = bl.GetAllMasters(sDataSource, "Product", "");
                string ItemCode = string.Empty;
                string ProductName = string.Empty;
                string Model = string.Empty;
                Int32 CategoryID = 0;
                string ProductDesc = string.Empty;
                Int32 ROL = 0;
                double Rate = 0;
                Int32 Unitt = 0;
                double VAT = 0;
                Int32 Discount = 0;
                Int32 BuyUnit = 0;
                double BuyRate = 0;
                double BuyVAT = 0;
                Int32 BuyDiscount = 0;
                Int32 DealerUnit = 0;
                double DealerRate = 0;
                double DealerVAT = 0;
                Int32 DealerDiscount = 0;
                string Complex = string.Empty;
                string Measure_Unit = string.Empty;
                string Accept_Role = string.Empty;
                Double CST = 0;
                string Barcode = string.Empty;
                string CommodityCode = string.Empty;
                double NLC = 0;
                string block = string.Empty;
                Int32 productlevel = 0;
                DateTime MRPEffDate;
                DateTime DPEffDate;
                DateTime NLCEffDate;
                Int32 MinSales = 0;
                if (dsddd != null)
                {
                    if (dsddd.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsddd.Tables[0].Rows)
                        {
                            ItemCode = Convert.ToString(dr["ItemCode"]);
                            ProductName = Convert.ToString(dr["ProductName"]);
                            Model = Convert.ToString(dr["Model"]);
                            CategoryID = Convert.ToInt32(dr["CategoryID"]);
                            ProductDesc = Convert.ToString(dr["ProductDesc"]);
                            ROL = Convert.ToInt32(dr["ROL"]);
                            Rate = Convert.ToDouble(dr["Rate"]);
                            if ((Convert.ToString(dr["Unit"]) == "") || (Convert.ToString(dr["Unit"]) == null))
                            {
                                Unitt = 0;
                            }
                            else
                            {
                                Unitt = Convert.ToInt32(dr["Unit"]);
                            }
                            VAT = Convert.ToDouble(dr["VAT"]);
                            Discount = Convert.ToInt32(dr["Discount"]);
                            if ((Convert.ToString(dr["BuyUnit"]) == "") || (Convert.ToString(dr["BuyUnit"]) == null))
                            {
                                BuyUnit = 0;
                            }
                            else
                            {
                                BuyUnit = Convert.ToInt32(dr["BuyUnit"]);
                            }
                            BuyRate = Convert.ToDouble(dr["BuyRate"]);
                            BuyVAT = Convert.ToDouble(dr["BuyVAT"]);
                            BuyDiscount = Convert.ToInt32(dr["BuyDiscount"]);
                            if ((Convert.ToString(dr["DealerUnit"]) == "") || (Convert.ToString(dr["DealerUnit"]) == null))
                            {
                                DealerUnit = 0;
                            }
                            else
                            {
                                DealerUnit = Convert.ToInt32(dr["DealerUnit"]);
                            }
                            DealerRate = Convert.ToDouble(dr["DealerRate"]);
                            DealerVAT = Convert.ToDouble(dr["DealerVAT"]);
                            DealerDiscount = Convert.ToInt32(dr["DealerDiscount"]);
                            Complex = Convert.ToString(dr["Complex"]);
                            Measure_Unit = Convert.ToString(dr["Measure_Unit"]);
                            Accept_Role = Convert.ToString(dr["Accept_Role"]);
                            CST = Convert.ToDouble(dr["CST"]);
                            Barcode = Convert.ToString(dr["Barcode"]);
                            CommodityCode = Convert.ToString(dr["CommodityCode"]);
                            NLC = Convert.ToDouble(dr["NLC"]);
                            block = Convert.ToString(dr["block"]);
                            productlevel = Convert.ToInt32(dr["productlevel"]);
                            MRPEffDate = Convert.ToDateTime(System.DateTime.Now);
                            DPEffDate = Convert.ToDateTime(System.DateTime.Now);
                            NLCEffDate = Convert.ToDateTime(System.DateTime.Now);
                            MinSales = Convert.ToInt32(dr["MinSales"]);
                            command.CommandText = string.Format("INSERT INTO tblProductMaster VALUES('{0}','{1}', '{2}',{3},'{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},'{19}','{20}','{21}',{22},'{23}',{24},'{25}',{26},'{27}',{28},Format('{29}', 'dd/mm/yyyy'),Format('{30}', 'dd/mm/yyyy'),Format('{31}', 'dd/mm/yyyy'),Format('{32}', 'dd/mm/yyyy'),Format('{33}', 'dd/mm/yyyy'),Format('{34}', 'dd/mm/yyyy'),Format('{35}', 'dd/mm/yyyy'),Format('{36}', 'dd/mm/yyyy'),Format('{37}', 'dd/mm/yyyy'),{38})",
                                ItemCode, ProductName, Model, CategoryID, ProductDesc, 0, ROL, Rate, Unitt, VAT, Discount, BuyUnit, BuyRate, BuyVAT, BuyDiscount, DealerUnit, DealerRate, DealerVAT, DealerDiscount, Complex, Measure_Unit, Accept_Role, CST, Barcode, 0, CommodityCode, NLC, block, productlevel, MRPEffDate.ToShortDateString(), DPEffDate.ToShortDateString(), NLCEffDate.ToShortDateString(), MRPEffDate.ToShortDateString(), MRPEffDate.ToShortDateString(), DPEffDate.ToShortDateString(), DPEffDate.ToShortDateString(), NLCEffDate.ToShortDateString(), NLCEffDate.ToShortDateString(), MinSales);
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dsddtt = bl.GetAllMasters(sDataSource, "Expenses", "");
                string ledgername = string.Empty;
                Int32 groupid = 0;
                Int32 ledgerid = 0;
                string aliasname = string.Empty;
                if (dsddtt != null)
                {
                    if (dsddtt.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsddtt.Tables[0].Rows)
                        {
                            ledgername = Convert.ToString(dr["ledgername"]);
                            ledgerid = Convert.ToInt32(dr["ledgerid"]);
                            groupid = Convert.ToInt32(dr["groupid"]);
                            command.CommandText = string.Format("INSERT INTO tblLedger(LedgerID,LedgerName, AliasName,GroupID,OpenBalanceDR,OpenBalanceCR,Debit,Credit,ContactName,Add1,Add2,Add3,Phone,BelongsTo,LedgerCategory,ExecutiveIncharge,TinNumber,Mobile,Inttrans,Paymentmade,dc,ChequeName,unuse,SFNo) VALUES({0},'{1}','{2}',{3},{4},{5},{6},{7},'{8}','{9}','{10}','{11}','{12}',{13},'{14}',{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}')",
                                    ledgerid, ledgername, Convert.ToString(dr["AliasName"]), groupid, 0, 0, 0, 0, Convert.ToString(dr["ContactName"]), Convert.ToString(dr["Add1"]), Convert.ToString(dr["Add2"]), Convert.ToString(dr["Add3"]), Convert.ToString(dr["Phone"]), 0, Convert.ToString(dr["LedgerCategory"]), Convert.ToInt32(dr["ExecutiveIncharge"]), Convert.ToString(dr["TinNumber"]), Convert.ToString(dr["Mobile"]), Convert.ToString(dr["Inttrans"]), Convert.ToString(dr["paymentmade"]), Convert.ToString(dr["dc"]), Convert.ToString(dr["ChequeName"]), Convert.ToString(dr["unuse"]), Convert.ToString(dr["SFNo"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dsddt = bl.GetAllMasters(sDataSource, "Banks", "");
                if (dsddt != null)
                {
                    if (dsddt.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsddt.Tables[0].Rows)
                        {
                            command.CommandText = string.Format("INSERT INTO tblLedger(LedgerID,LedgerName, AliasName,GroupID,OpenBalanceDR,OpenBalanceCR,Debit,Credit,ContactName,Add1,Add2,Add3,Phone,BelongsTo,LedgerCategory,ExecutiveIncharge,TinNumber,Mobile,Inttrans,Paymentmade,dc,ChequeName,unuse,SFNo) VALUES({0},'{1}','{2}',{3},{4},{5},{6},{7},'{8}','{9}','{10}','{11}','{12}',{13},'{14}',{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}')",
                                    Convert.ToInt32(dr["ledgerid"]), Convert.ToString(dr["ledgername"]), Convert.ToString(dr["AliasName"]), Convert.ToInt32(dr["groupid"]), 0, 0, 0, 0, Convert.ToString(dr["ContactName"]), Convert.ToString(dr["Add1"]), Convert.ToString(dr["Add2"]), Convert.ToString(dr["Add3"]), Convert.ToString(dr["Phone"]), 0, Convert.ToString(dr["LedgerCategory"]), Convert.ToInt32(dr["ExecutiveIncharge"]), Convert.ToString(dr["TinNumber"]), Convert.ToString(dr["Mobile"]), Convert.ToString(dr["Inttrans"]), Convert.ToString(dr["paymentmade"]), Convert.ToString(dr["dc"]), Convert.ToString(dr["ChequeName"]), Convert.ToString(dr["unuse"]), Convert.ToString(dr["SFNo"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dsddthh = bl.GetAllMasters(sDataSource, "FixedAsetAndIndInc", "");
                if (dsddthh != null)
                {
                    if (dsddthh.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsddthh.Tables[0].Rows)
                        {
                            string led = Convert.ToString(dr["ledgername"]);
                            command.CommandText = string.Format("INSERT INTO tblLedger(LedgerID,LedgerName, AliasName,GroupID,OpenBalanceDR,OpenBalanceCR,Debit,Credit,ContactName,Add1,Add2,Add3,Phone,BelongsTo,LedgerCategory,ExecutiveIncharge,TinNumber,Mobile,Inttrans,Paymentmade,dc,ChequeName,unuse,SFNo) VALUES({0},'{1}','{2}',{3},{4},{5},{6},{7},'{8}','{9}','{10}','{11}','{12}',{13},'{14}',{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}')",
                                    Convert.ToInt32(dr["ledgerid"]), Convert.ToString(dr["ledgername"]), Convert.ToString(dr["AliasName"]), Convert.ToInt32(dr["groupid"]), 0, 0, 0, 0, Convert.ToString(dr["ContactName"]), Convert.ToString(dr["Add1"]), Convert.ToString(dr["Add2"]), Convert.ToString(dr["Add3"]), Convert.ToString(dr["Phone"]), 0, Convert.ToString(dr["LedgerCategory"]), Convert.ToInt32(dr["ExecutiveIncharge"]), Convert.ToString(dr["TinNumber"]), Convert.ToString(dr["Mobile"]), Convert.ToString(dr["Inttrans"]), Convert.ToString(dr["paymentmade"]), Convert.ToString(dr["dc"]), Convert.ToString(dr["ChequeName"]), Convert.ToString(dr["unuse"]), Convert.ToString(dr["SFNo"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dsddtd = bl.GetAllMasters(sDataSource, "Suppliers", "");
                if (dsddtd != null)
                {
                    if (dsddtd.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsddtd.Tables[0].Rows)
                        {
                            command.CommandText = string.Format("INSERT INTO tblLedger(LedgerID,LedgerName, AliasName,GroupID,OpenBalanceDR,OpenBalanceCR,Debit,Credit,ContactName,Add1,Add2,Add3,Phone,BelongsTo,LedgerCategory,ExecutiveIncharge,TinNumber,Mobile,Inttrans,Paymentmade,dc,ChequeName,unuse,SFNo) VALUES({0},'{1}','{2}',{3},{4},{5},{6},{7},'{8}','{9}','{10}','{11}','{12}',{13},'{14}',{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}')",
                                    Convert.ToInt32(dr["ledgerid"]), Convert.ToString(dr["ledgername"]), Convert.ToString(dr["AliasName"]), Convert.ToInt32(dr["groupid"]), 0, 0, 0, 0, Convert.ToString(dr["ContactName"]), Convert.ToString(dr["Add1"]), Convert.ToString(dr["Add2"]), Convert.ToString(dr["Add3"]), Convert.ToString(dr["Phone"]), 0, Convert.ToString(dr["LedgerCategory"]), Convert.ToInt32(dr["ExecutiveIncharge"]), Convert.ToString(dr["TinNumber"]), Convert.ToString(dr["Mobile"]), Convert.ToString(dr["Inttrans"]), Convert.ToString(dr["paymentmade"]), Convert.ToString(dr["dc"]), Convert.ToString(dr["ChequeName"]), Convert.ToString(dr["unuse"]), Convert.ToString(dr["SFNo"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dsddtddd = bl.GetAllMasters(sDataSource, "Ledgers", "");
                if (dsddtddd != null)
                {
                    if (dsddtddd.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsddtddd.Tables[0].Rows)
                        {
                            command.CommandText = string.Format("INSERT INTO tblLedger(LedgerID,LedgerName, AliasName,GroupID,OpenBalanceDR,OpenBalanceCR,Debit,Credit,ContactName,Add1,Add2,Add3,Phone,BelongsTo,LedgerCategory,ExecutiveIncharge,TinNumber,Mobile,Inttrans,Paymentmade,dc,ChequeName,unuse,SFNo) VALUES({0},'{1}','{2}',{3},{4},{5},{6},{7},'{8}','{9}','{10}','{11}','{12}',{13},'{14}',{15},'{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}')",
                                    Convert.ToInt32(dr["ledgerid"]), Convert.ToString(dr["ledgername"]), Convert.ToString(dr["AliasName"]), Convert.ToInt32(dr["groupid"]), 0, 0, 0, 0, Convert.ToString(dr["ContactName"]), Convert.ToString(dr["Add1"]), Convert.ToString(dr["Add2"]), Convert.ToString(dr["Add3"]), Convert.ToString(dr["Phone"]), 0, Convert.ToString(dr["LedgerCategory"]), Convert.ToInt32(dr["ExecutiveIncharge"]), Convert.ToString(dr["TinNumber"]), Convert.ToString(dr["Mobile"]), Convert.ToString(dr["Inttrans"]), Convert.ToString(dr["paymentmade"]), Convert.ToString(dr["dc"]), Convert.ToString(dr["ChequeName"]), Convert.ToString(dr["unuse"]), Convert.ToString(dr["SFNo"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dsddtdd = bl.GetAllMasters(sDataSource, "Lead", "");
                if (dsddtdd != null)
                {
                    if (dsddtdd.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsddtdd.Tables[0].Rows)
                        {
                            command.CommandText = string.Format("INSERT INTO tblLeadReferences(ID,Type,TextValue,TypeName) VALUES({0},'{1}','{2}','{3}')",
                                    Convert.ToInt32(dr["id"]), Convert.ToString(dr["Type"]), Convert.ToString(dr["TextValue"]), Convert.ToString(dr["TypeName"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dsdt = bl.GetAllMasters(sDataSource, "Company", "");
                if (dsdt != null)
                {
                    if (dsdt.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsdt.Tables[0].Rows)
                        {
                            command.CommandText = string.Format("INSERT INTO tblCompanyInfo(CompanyName,Address,City,state,Pincode,phone,Fax,eMail,TINno,GstNo) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                                Convert.ToString(dr["CompanyName"]), Convert.ToString(dr["Address"]), Convert.ToString(dr["City"]), Convert.ToString(dr["State"]), Convert.ToString(dr["Pincode"]), Convert.ToString(dr["Phone"]), Convert.ToString(dr["Fax"]), Convert.ToString(dr["Email"]), Convert.ToString(dr["Tinno"]), Convert.ToString(dr["GstNo"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dsdtt = bl.GetAllMasters(sDataSource, "Division", "");
                if (dsdtt != null)
                {
                    if (dsdtt.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsdtt.Tables[0].Rows)
                        {
                            command.CommandText = string.Format("INSERT INTO tblDivisions(DivisionName,Address,City,State,PinCode,Phone,Fax,eMail,TINNo,GSTNo,DivisionId) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10})",
                                Convert.ToString(dr["DivisionName"]), Convert.ToString(dr["Address"]), Convert.ToString(dr["City"]), Convert.ToString(dr["State"]), Convert.ToString(dr["Pincode"]), Convert.ToString(dr["Phone"]), Convert.ToString(dr["Fax"]), Convert.ToString(dr["Email"]), Convert.ToString(dr["TinNo"]), Convert.ToString(dr["GstNo"]), Convert.ToInt32(dr["DivisionId"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dsdttd = bl.GetAllMasters(sDataSource, "Employee", "");
                if (dsdttd != null)
                {
                    if (dsdttd.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsdttd.Tables[0].Rows)
                        {
                            command.CommandText = string.Format("INSERT INTO tblEmployee(empno,empTitle,empFirstName,empMiddleName,empSurName,empDOJ,empDOB,empDesig,empRemarks) VALUES({0},'{1}','{2}','{3}','{4}',Format('{5}', 'dd/mm/yyyy'),Format('{6}', 'dd/mm/yyyy'),'{7}','{8}')",
                                Convert.ToInt32(dr["empno"]), Convert.ToString(dr["empTitle"]), Convert.ToString(dr["empFirstName"]), Convert.ToString(dr["empMiddleName"]), Convert.ToString(dr["empSurName"]), Convert.ToDateTime(dr["empDOJ"]), Convert.ToDateTime(dr["empDOB"]), Convert.ToString(dr["empDesig"]), Convert.ToString(dr["empRemarks"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dstdtd = bl.GetAllMasters(sDataSource, "Settings", "");
                if (dstdtd != null)
                {
                    if (dstdtd.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dstdtd.Tables[0].Rows)
                        {
                            command.CommandText = string.Format("INSERT INTO tblSettings VALUES('{0}','{1}')", Convert.ToString(dr["Key"]), Convert.ToString(dr["KeyValue"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                DataSet dstdtdtt = bl.GetAllMasters(sDataSource, "Image", "");
                if (dstdtdtt != null)
                {
                    if (dstdtdtt.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dstdtdtt.Tables[0].Rows)
                        {
                            command.CommandText = string.Format("INSERT INTO imagespath(img_title,img_title,img_filename,img_id) VALUES('{0}','{1}','{2}',{3})", Convert.ToString(dr["img_title"]), Convert.ToString(dr["img_title"]), Convert.ToString(dr["img_filename"]), Convert.ToInt32(dr["img_id"]));
                            command.ExecuteNonQuery();
                        }
                    }
                }

                transaction.Commit();
                con.Close();

                bl.UpdateYearEndUpdation(sDataSource, path, connection, "", "Master");

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Masters are updated.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void stkupd_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic();
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            DataSet dsd = bl.GetYearEndUpdation(sDataSource);

            string a = string.Empty;
            string b = string.Empty;
            string path = string.Empty;

            string DBname = string.Empty;
            DBname = GetCurrentDBName(sDataSource);//ConfigurationSettings.AppSettings["DBName"].ToString();

            int val = 0;
            for (int i = 0; i < DBname.Length; i++)
            {
                if (Char.IsDigit(DBname[i]))
                    b += DBname[i];
                else
                    a += DBname[i];
            }
            if (b.Length > 0)
            {
                val = int.Parse(b);
            }

            //path = Server.MapPath("App_Data\\" + DBname + ".mdb");

            //BusinessLogic objChk = new BusinessLogic();
            //sDtSource = ConfigurationManager.ConnectionStrings[connection].ToString();
            //string sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDtSource;

            using (SqlConnection con = new SqlConnection())
            {
                SqlCommand command = new SqlCommand();
                SqlTransaction transaction = null;
                SqlDataAdapter adapter = null;

                // Set the Connection to the new OleDbConnection.
                command.Connection = con;

                con.Open();

                transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);

                command.Connection = con;
                command.Transaction = transaction;

                string ItemCode = string.Empty;
                double CurrentStock = 0;
          
                DataSet ds = new DataSet();
                DataSet dsa = new DataSet();
                DataSet dsddd = bl.GetAllMasters(sDataSource, "Product", "");
                if (dsddd != null)
                {
                    if (dsddd.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsddd.Tables[0].Rows)
                        {
                            ItemCode = Convert.ToString(dr["ItemCode"]);
                            CurrentStock = Convert.ToDouble(dr["Stock"]);

                            command.CommandText = string.Format("Insert into tblStock(itemcode,openingstock,productname,model,productdesc,categoryid,OpDueDate) Values('{0}',{1},'{2}','{3}','{4}',{5},'{6}')", ItemCode, CurrentStock, Convert.ToString(dr["productname"]), Convert.ToString(dr["model"]), Convert.ToString(dr["productdesc"]), Convert.ToInt32(dr["categoryid"]),DateTime.Now.ToString("yyyy-MM-dd"));
                            command.ExecuteNonQuery();

                            

                        }
                    }
                }

                transaction.Commit();
                con.Close();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stocks are updated.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void opbalupd_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = "Reports//" + System.Guid.NewGuid().ToString() + ConfigurationManager.AppSettings["OutstandingFileName"].ToString();
            string sXmlPath = Server.MapPath(fileName);
            BusinessLogic bl = new BusinessLogic();
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            string DBname = string.Empty;

            string a = string.Empty;
            string b = string.Empty;
            string path = string.Empty;
            DBname = GetCurrentDBName(sDataSource);//ConfigurationSettings.AppSettings["DBName"].ToString();

            int val = 0;
            for (int i = 0; i < DBname.Length; i++)
            {
                if (Char.IsDigit(DBname[i]))
                    b += DBname[i];
                else
                    a += DBname[i];
            }
            if (b.Length > 0)
            {
                val = int.Parse(b);
            }

            //path = Server.MapPath("App_Data\\" + DBname + ".mdb");
            string nowdate = "31/03/" + val;

            using (SqlConnection con = new SqlConnection())
            {
                SqlCommand command = new SqlCommand();
                SqlTransaction transaction = null;
                SqlDataAdapter adapter = null;

                // Set the Connection to the new OleDbConnection.
                command.Connection = con;

                con.Open();

                transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);

                command.Connection = con;
                command.Transaction = transaction;
                int iGroupID = 0;
                string sXmlNodeName = "Outstanding";
                string sLedger = string.Empty;
                double obD = 0;
                double obC = 0;

                DataSet dsdata = bl.YearEndOutStandingReport(iGroupID, sXmlNodeName, sDataSource, sXmlPath);
                if (dsdata != null)
                {
                    if (dsdata.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsdata.Tables[0].Rows)
                        {
                            obD = Convert.ToDouble(dr["Debit"]);
                            obC = Convert.ToDouble(dr["Credit"]);
                            sLedger = dr["LedgerName"].ToString();
                            command.CommandText = string.Format("Update tblLedger SET Debit={0},Credit={1},OpenBalanceDr={2},OpenBalanceCr={3},OpDueDate='{4}' Where LedgerName = '{5}'", 0, 0, obD, obC, DateTime.Now.ToString("yyyy-MM-dd"), sLedger);
                            command.ExecuteNonQuery();

                            command.CommandText = string.Format("INSERT INTO tblYearEndLedger select * from tblLedger");
                            command.ExecuteNonQuery();
                        }
                    }
                }

                transaction.Commit();
                con.Close();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Opening Balance are updated.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnStockUpd_Click(object sender, EventArgs e)
    {
        //using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=True;Jet OLEDB:Database Password=moonmoon"))
        //{
        //    OleDbCommand command = new OleDbCommand();
        //    OleDbTransaction transaction = null;
        //    OleDbDataAdapter adapter = null;

        //    // Set the Connection to the new OleDbConnection.
        //    command.Connection = con;

        //    con.Open();

        //    command.CommandText = string.Format("Delete From tblStock");
        //    command.ExecuteNonQuery();

        //    command.CommandText = string.Format("Insert into tblStock(itemcode,openingstock,productname,model,productdesc,categoryid) SELECT itemcode,stock,productname,model,productdesc,categoryid From tblProductMaster where stock > 0");
        //    command.ExecuteNonQuery();
        //}

    }

    protected void btnCompress_Click(object sender, EventArgs e)
    {
        try
        {
            ////////string filename = string.Empty;
            ////////string dbfileName = string.Empty;
            ////////string path = string.Empty;

            //////string DBname = string.Empty;

            string localpath = "C:\\MyStuff\\Dev\\TROYPLUS\\ACCSYSTEM\\App_Data\\KT2014.mdb";

            sDataSource = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\MyStuff\Dev\TROYPLUS\ACCSYSTEM\App_Data\KT2014.mdb;Persist Security Info=False;Jet OLEDB:Jet OLEDB:Engine Type=5; Password=moonmoon";

            CompactAccessDB(sDataSource, localpath);

            //////DBname = GetCurrentDBName(sDataSource);//ConfigurationSettings.AppSettings["DBName"].ToString();

            //////string a = string.Empty;
            //////string b = string.Empty;
            //////int val = 0;
            //////for (int i = 0; i < DBname.Length; i++)
            //////{
            //////    if (Char.IsDigit(DBname[i]))
            //////        b += DBname[i];
            //////    else
            //////        a += DBname[i];
            //////}
            //////if (b.Length > 0)
            //////{
            //////    val = int.Parse(b);
            //////    val = val + 1;
            //////}


            ////////string Jr As JRO.JetEngine;
            ////////Jr = New JRO.JetEngine();
            ////////Jr.CompactDatabase("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\nwind.mdb","Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\NewNwind.mdb;Jet OLEDB:Engine Type=5");
            ////////MsgBox("Finished Compacting Database!");


            //////////path = Server.MapPath("App_Data\\" + a + val + ".mdb");


            ////////string connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ConnectionString;

            ////////dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
            ////////dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            ////////filename = Server.MapPath(localpath + dbfileName);

            ////////if (File.Exists(filename))
            ////////{
            ////////    GZip objZip = new GZip(filename, filename + ".zip", Action.Zip);
            ////////}

            ////////ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('DB File Compressed successfully.');", true);


            //////JRO.JetEngineClass jro;
            //////string newConStr;
            //////string conStr;

            //////////path = Server.MapPath("App_Data\\" + a + val + ".mdb");
            //////string path = Server.MapPath("App_Data\\");
            //////string originalDb = a + val + ".mdb";
            //////string newDb = "C" + a + val + ".mdb";
            //////string oriDb = a + val ;
            //////string dpath = Server.MapPath("App_Data\\" + a + val + ".mdb");
            ////////CompactAccessDB("Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Engine Type=5;Data Source=" + path + originalDb + ";Persist Security Info=True;Jet OLEDB:Database Password=j9MBWTeB7meRuELFLwlkVltsY7xhiYEe15AcFgrljtk=", originalDb);

            ////////instantiate the Jet Replication Object
            //////jro = new JRO.JetEngineClass();

            ////////get our connection string for the original database (mine is stored in my web.config file)
            //////conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Engine Type=5;Data Source=" + path + originalDb + ";Persist Security Info=True;Jet OLEDB:Database Password=moonmoon";

            ////////create a connection string for the compacted and repaired database we are going to create
            //////newConStr = dpath;

            ////////now lets compact the original database to our new one
            //////jro.CompactDatabase(conStr, newConStr);

            ////////ok, now lets overwrite the original Database file with the compacted one
            //////File.Copy(path + newDb, path + originalDb, true);
            ////////finally lets delete the database we made earlier since it is no longer needed
            ////////File.Delete(path + newDb);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    
    public static void CompactAccessDB(string connectionString, string mdwfilename)
    {

        /// <summary>
        /// MBD compact method (c) 2004 Alexander Youmashev
        /// !!IMPORTANT!!
        /// !make sure there's no open connections
        ///    to your db before calling this method!
        /// !!IMPORTANT!!
        /// </summary>
        /// <param name="connectionString">connection string to your db</param>
        /// <param name="mdwfilename">FULL name
        ///     of an MDB file you want to compress.</param>
        ///     
        object[] oParams;

        //create an inctance of a Jet Replication Object
        object objJRO =
          Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));

        //filling Parameters array
        //cnahge "Jet OLEDB:Engine Type=5" to an appropriate value
        // or leave it as is if you db is JET4X format (access 2000,2002)
        //(yes, jetengine5 is for JET4X, no misprint here)
        oParams = new object[] {
        connectionString,
        "Provider=Microsoft.Jet.OLEDB.4.0;Data" + 
        " Source=C:\\MyStuff\\Dev\\TROYPLUS\\ACCSYSTEM\\App_Data\\KT2014.mdb;Jet OLEDB:Engine Type=5;Persist Security Info=False;Jet OLEDB:Password=moonmoon;User ID=admin"};

        //invoke a CompactDatabase method of a JRO object
        //pass Parameters array
        objJRO.GetType().InvokeMember("CompactDatabase",
            System.Reflection.BindingFlags.InvokeMethod,
            null,
            objJRO,
            oParams);

        //database is compacted now
        //to a new file C:\\tempdb.mdw
        //let's copy it over an old one and delete it

        System.IO.File.Delete(mdwfilename);
        System.IO.File.Move("C:\\MyStuff\\Dev\\TROYPLUS\\ACCSYSTEM\\App_Data\\KT2014.mdb", mdwfilename);

        //clean up (just in case)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(objJRO);
        objJRO = null;
    }

    protected void cash_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = "Reports//" + System.Guid.NewGuid().ToString() + ConfigurationManager.AppSettings["OutstandingFileName"].ToString();
            string sXmlPath = Server.MapPath(fileName);
            BusinessLogic bl = new BusinessLogic();
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            DataSet dsd = bl.GetYearEndUpdation(sDataSource);

            string connection = "";
            string path = "";
            if (dsd != null)
            {
                if (dsd.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsd.Tables[0].Rows)
                    {
                        connection = Convert.ToString(dr["yearname"]);
                        path = Convert.ToString(dr["path"]);
                    }
                }
            }

            //BusinessLogic objChk = new BusinessLogic();
            //sDtSource = ConfigurationManager.ConnectionStrings[connection].ToString();
            //string sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDtSource;

            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Persist Security Info=True;Jet OLEDB:Database Password=moonmoon"))
            {
                OleDbCommand command = new OleDbCommand();
                OleDbTransaction transaction = null;
                OleDbDataAdapter adapter = null;

                // Set the Connection to the new OleDbConnection.
                command.Connection = con;

                con.Open();

                transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);

                command.Connection = con;
                command.Transaction = transaction;
                int iGroupID = 0;
                string sXmlNodeName = "Outstanding";
                string sLedger = string.Empty;
                double obD = 0;
                double obC = 0;





                DataSet dsdata = bl.YearEndOutStandingReportNew(iGroupID, sXmlNodeName, sDataSource, sXmlPath);
                if (dsdata != null)
                {
                    if (dsdata.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsdata.Tables[0].Rows)
                        {
                            obD = Convert.ToDouble(dr["Debit"]);
                            obC = Convert.ToDouble(dr["Credit"]);
                            sLedger = dr["LedgerName"].ToString();
                            command.CommandText = string.Format("Update tblLedger SET Debit={0},Credit={1},OpenBalanceDr={2},OpenBalanceCr={3} Where LedgerName = '{4}'", 0, 0, obD, obC, sLedger);
                            command.ExecuteNonQuery();
                        }
                    }
                }



                transaction.Commit();
                con.Close();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Balance are updated.');", true);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //////public static bool CompactJetDatabase(string fileName, string originalDb)
    //////{
        //////// I use this function as part of an AJAX page, so rather
        //////// than throwing exceptions if errors are encountered, I
        //////// simply return false and allow the page to handle the
        //////// failure generically.
        //////try
        //////{
        //////    // Find the database on the web server
        //////    string oldFileName =
        //////     HttpContext.Current.Server.MapPath(originalDb);

        //////    // JET will not compact the database in place, so we
        //////    // need to create a temporary filename to use
        //////    string newFileName =
        //////     Path.Combine(Path.GetDirectoryName(oldFileName),
        //////     Guid.NewGuid().ToString("N") + ".mdb");

        //////    // Obtain a reference to the JET engine
        //////    JetEngine engine =
        //////     (JetEngine)HttpContext.Current.Server.CreateObject(
        //////     "JRO.JetEngine");

        //////    // Compact the database (saves the compacted version to
        //////    // newFileName)
        //////    engine.CompactDatabase(
        //////     String.Format(
        //////      AccessOleDbConnectionStringFormat, oldFileName),
        //////     String.Format(
        //////      AccessOleDbConnectionStringFormat, newFileName));

        //////    // Delete the original database
        //////    File.Delete(oldFileName);

        //////    // Move (rename) the temporary compacted database to
        //////    // the original filename
        //////    File.Move(newFileName, oldFileName);

        //////    // The operation was successful
        //////    return true;
        //////}
        //////catch
        //////{
        //////    // We encountered an error
        //////    return false;
        //////}
    //////}



}
