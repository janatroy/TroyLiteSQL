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
using System.IO;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Data.OleDb;
using System.IO;
using ClosedXML.Excel;

public partial class _DashBoard : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    string connection;
  //  string usernam;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
              //  Response.AddHeader("Refresh", 10 + "; URL=Dashboard.aspx");

               
                //if (Helper.IsTrialVersion())
                //{
                //imgTrial.Visible = true;
                //}
                //else
                //{
                //imgTrial.Visible = false;
                ////}

                //string connStr = string.Empty;

                //if (Request.Cookies["Company"] != null)
                //    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                //else
                //    Response.Redirect("~/Login.aspx");

                //string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                //dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

                ////BusinessLogic objChk = new BusinessLogic(); 

                ////if (!objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline"))) 
                ////{
                ////    divWarning.Visible = false;
                ////    tblWarning.Visible = false;
                ////}

                //string filePath = Server.MapPath("Offline\\" + dbfileName + ".offline");

                //if (File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-CLIENT"))
                //{
                //    divWarning.Visible = true;
                //    tblWarning.Visible = true;
                //}
                //else if (!File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-CLIENT"))
                //{
                //    divWarning.Visible = false;
                //    tblWarning.Visible = false;
                //}
                //else if (File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-SERVER"))
                //{
                //    divWarning.Visible = true;
                //    tblWarning.Visible = true;
                //}
                //else if (!File.Exists(filePath) && (ConfigurationManager.AppSettings["InstallationType"] == "ONLINE-OFFLINE-SERVER"))
                //{
                //    divWarning.Visible = false;
                //    tblWarning.Visible = false;
                //}
                //else
                //{
                //    divWarning.Visible = false;
                //    tblWarning.Visible = false;
                //}
                //BusinessLogic bl = new BusinessLogic(sDataSource);
                //connection = Request.Cookies["Company"].Value;
                //usernam = Request.Cookies["LoggedUserName"].Value;

                //if (bl.CheckUserHaveAdd(usernam, "TCreate"))
                //{
                //    lnkBtnAdd.Enabled = false;
                //    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                //}
                //else
                //{
                //    lnkBtnAdd.Enabled = true;
                //    lnkBtnAdd.ToolTip = "Click to Add New item ";
                //}
                string connection = Request.Cookies["Company"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveView(connection, usernam, "SALEREP"))
                {
                    GrdWME.Visible = false;
                    tblWarning.Visible = false;
                    dpshow.Visible = false;
                    // GrdWME.ToolTip = "You are not allow to see this report.";
                }
                else
                {
                    GrdWME.Visible = true;
                    tblWarning.Visible = true;
                }

                if (bl.CheckUserHaveView(connection, usernam, "SALEREP1"))
                {
                    GridViewdp.Visible = false;
                    tblWarning.Visible = false;
                   
                    // GrdWME.ToolTip = "You are not allow to see this report.";
                }
               else
                {
                    GridViewdp.Visible = true;
                    tblWarning.Visible = true;
                }

                if (bl.CheckUserHaveView(connection, usernam, "PURREP"))
                {
                    grdpurchase.Visible = false;
                    Table1.Visible = false;
                    // GrdWME.ToolTip = "You are not allow to see this report.";
                }
                else
                {
                    grdpurchase.Visible = true;
                    Table1.Visible = true;
                }

                if(grdpurchase.Visible==true &&  (GrdWME.Visible == true || GridViewdp.Visible==true))
                {
                    btnSearch.Visible = true;
                    if (GrdWME.Visible == true)
                    {
                        dpshow.Visible = true;
                    }                 
                    tblWarning.Visible = true;
                 //   lble1.Visible = true;
                   // lble1.Text = "Today's Sales - Real-time Summary";
                     
                }
                else
                {
                    if(grdpurchase.Visible==true)
                    {
                        Button1.Visible = true;
                    }
                    else
                    {
                        btnSearch.Visible = true;
                        dpshow.Visible = true;
                    }

                }          
                BindWME("", "");
                BindWME1("", "");
                BindWMEPUR("", "");


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
            GrdWME.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindWME("", "");

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
            
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void BindWME(string textSearch, string dropDown)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string branch = string.Empty;
        string total = string.Empty;
        string connection = Request.Cookies["Company"].Value;
        string sMonth = DateTime.Now.ToString("MM");
        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);
        var start = startDate.ToString("yyyy-MM-dd");
        var endDate = startDate.AddMonths(1).AddDays(-1);
        var end = endDate.ToString("yyyy-MM-dd");

        int year = DateTime.Now.Year;
        var firstDay = new DateTime(year, 1, 1);
        var firstday1 = firstDay.ToString("yyyy-MM-dd");
        var lastDay = new DateTime(year, 12, 31);
        var lastday1 = lastDay.ToString("yyyy-MM-dd");

        string usernam = Request.Cookies["LoggedUserName"].Value;
        double Tottot = 0.00;
        double Totgp = 0.00;
        double totgpmon = 0.00;
        double dailysalestot = 0.00;
        int dailyqty = 0;
        int monthlyqty = 0;


        string sCustomer = string.Empty;
        connection = Request.Cookies["Company"].Value;
        usernam = Request.Cookies["LoggedUserName"].Value;
        // BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);
        DataSet ds = new DataSet();

        sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["DefaultBranchCode"]);
        //  drpBranch.ClearSelection();
        //  ListItem li = drpBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        //  if (li != null) li.Selected = true;

        if (dsd.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
        {
            ds = bl.ListBranch(connection, usernam);
        }
        else
        {
            dpshow.Visible = false;
            ds = bl.ListBranch1(connection, usernam, sCustomer);
        }

      //  DataSet ds = bl.getdashboardreport(connection);

       // DataSet ds = bl.ListBranch(connection, usernam);

       // DataSet ds;
        DataTable dt;
        DataRow drNew;
        DataSet dstt = new DataSet();

        DataColumn dc;

       DataSet dst = new DataSet();

        dt = new DataTable();
        dt.Columns.Add(new DataColumn("BranchCode"));
        dt.Columns.Add(new DataColumn("Branchname"));
        dt.Columns.Add(new DataColumn("DailySales"));
        dt.Columns.Add(new DataColumn("MonthlySales"));
        dt.Columns.Add(new DataColumn("Todaysalesquantity"));
        dt.Columns.Add(new DataColumn("monthlysalesquantity"));
        dt.Columns.Add(new DataColumn("DailySalesforgp"));
        dt.Columns.Add(new DataColumn("montlySalesforgp"));
        dt.Columns.Add(new DataColumn("Amount"));

      

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_final113 = dt.NewRow();

                    dr_final113["BranchCode"] = dr["BranchCode"].ToString();
                    dr_final113["Branchname"] = dr["Branchname"].ToString();
                   string branch1 = dr["BranchCode"].ToString();

                    //BusinessLogic bl = new BusinessLogic(sDataSource);
                    DataSet db = bl.getdailysalesreport1(connection, dr["BranchCode"].ToString());
                    if (db != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drd in db.Tables[0].Rows)
                            {
                                dr_final113["DailySales"] = (Convert.ToDecimal(drd["DailySales"])).ToString("#0.00");
                                dr_final113["DailySalesforgp"] = (Convert.ToDecimal(drd["DailySales"])) - (Convert.ToDecimal(drd["DailySalesforgp"]));
                                dr_final113["Todaysalesquantity"] = drd["Todaysalesquantity"].ToString();
                            }
                        }
                        else
                        {
                            dr_final113["DailySales"] = 0;
                            dr_final113["DailySalesforgp"] = 0;
                            dr_final113["Todaysalesquantity"] = 0;
                        }
                    }

                    else
                    {
                        dr_final113["DailySales"] = 0;
                        dr_final113["DailySalesforgp"] = 0;
                        dr_final113["Todaysalesquantity"] = 0;
                    }

                    DataSet dm = bl.getmonthlysalesreport1(connection, dr["BranchCode"].ToString());
                    if (dm != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow drm in dm.Tables[0].Rows)
                            {
                                dr_final113["MonthlySales"] = (Convert.ToDecimal(drm["MonthlySales"])).ToString("#0.00");
                                dr_final113["montlySalesforgp"] = (Convert.ToDecimal(drm["MonthlySales"])) - (Convert.ToDecimal(drm["montlySalesforgp"]));
                                dr_final113["monthlysalesquantity"] = drm["monthlysalesquantity"].ToString();
                                
                            }
                        }
                        else
                        {
                            dr_final113["MonthlySales"] = 0;
                            dr_final113["montlySalesforgp"] = 0;
                            dr_final113["monthlysalesquantity"] = 0;
                        }
                    }
                    else
                    {
                        dr_final113["MonthlySales"] = 0;
                        dr_final113["montlySalesforgp"] = 0;
                        dr_final113["monthlysalesquantity"] = 0;
                    }

                    //if (ds.Tables[0].Rows[0]["totalsales"] != null)
                    //    total = (ds.Tables[0].Rows[0]["totalsales"]).ToString();
                    // GrdWME.DataSource = ds;
                    //GrdWME.DataBind();
                    Tottot = Tottot + Convert.ToDouble(dr_final113["MonthlySales"]);
                    dailysalestot = dailysalestot + Convert.ToDouble(dr_final113["DailySales"]);
                    dailyqty = dailyqty + Convert.ToInt32(dr_final113["Todaysalesquantity"]);
                    monthlyqty = monthlyqty + Convert.ToInt32(dr_final113["monthlysalesquantity"]);
                    Totgp = Totgp + (Convert.ToDouble(dr_final113["DailySalesforgp"]));
                    totgpmon = totgpmon + (Convert.ToDouble(dr_final113["montlySalesforgp"]));
                    dt.Rows.Add(dr_final113);   
        
                }
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                DataRow dr_final88 = dt.NewRow();
                dr_final88["Branchname"] = "Total";
                dr_final88["MonthlySales"] = Tottot.ToString("#0.00");
                dr_final88["DailySalesforgp"] = Totgp.ToString("#0.00");
                dr_final88["montlySalesforgp"] = totgpmon.ToString("#0.00");
                dr_final88["DailySales"] = dailysalestot.ToString("#0.00");
                dr_final88["Todaysalesquantity"] = dailyqty.ToString("#0");
                dr_final88["monthlysalesquantity"] = monthlyqty.ToString("#0");
                dt.Rows.Add(dr_final88);

                dst.Tables.Add(dt);
                GrdWME.DataSource = dst;
                GrdWME.DataBind();
            }
            else
            {
                GrdWME.DataSource = null;
                GrdWME.DataBind();
            }
        }
        else
        {
            GrdWME.DataSource = null;
            GrdWME.DataBind();
        }
    }
   

    private void BindWME1(string textSearch, string dropDown)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string branch = string.Empty;
        string total = string.Empty;
        string connection = Request.Cookies["Company"].Value;
        string sMonth = DateTime.Now.ToString("MM");
        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);
        var start = startDate.ToString("yyyy-MM-dd");
        var endDate = startDate.AddMonths(1).AddDays(-1);
        var end = endDate.ToString("yyyy-MM-dd");

        string usernam = Request.Cookies["LoggedUserName"].Value;
        double Tottot = 0.00;
        double Totgp = 0.00;
        double totgpmon = 0.00;
        double dailysalestot = 0.00;
        int dailyqty = 0;
        int monthlyqty = 0;

        string sCustomer = string.Empty;
        connection = Request.Cookies["Company"].Value;
         usernam = Request.Cookies["LoggedUserName"].Value;
       // BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);
        DataSet ds = new DataSet();

        sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["DefaultBranchCode"]);
        //  drpBranch.ClearSelection();
      //  ListItem li = drpBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
      //  if (li != null) li.Selected = true;

        if (dsd.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
        {
             ds = bl.ListBranch(connection, usernam);
        }
        else
        {
            dpshow.Visible = false;
            ds = bl.ListBranch1(connection, usernam,sCustomer);
        }

        //  DataSet ds = bl.getdashboardreport(connection);

      //  DataSet ds = bl.ListBranch(connection, usernam);

       
        DataTable dt;
        DataRow drNew;
        DataSet dstt = new DataSet();

        DataColumn dc;

        DataSet dst = new DataSet();

        dt = new DataTable();
        dt.Columns.Add(new DataColumn("BranchCode"));
        dt.Columns.Add(new DataColumn("Branchname"));
        dt.Columns.Add(new DataColumn("DailySales"));
        dt.Columns.Add(new DataColumn("MonthlySales"));
        dt.Columns.Add(new DataColumn("Todaysalesquantity"));
        dt.Columns.Add(new DataColumn("monthlysalesquantity"));
        dt.Columns.Add(new DataColumn("DailySalesforgp"));
        dt.Columns.Add(new DataColumn("montlySalesforgp"));
        dt.Columns.Add(new DataColumn("Amount"));



        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_final113 = dt.NewRow();

                    dr_final113["BranchCode"] = dr["BranchCode"].ToString();
                    dr_final113["Branchname"] = dr["Branchname"].ToString();
                    string branch1 = dr["BranchCode"].ToString();

                    //BusinessLogic bl = new BusinessLogic(sDataSource);
                    DataSet db = bl.getdailysalesreport(connection, dr["BranchCode"].ToString());
                    if (db != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drd in db.Tables[0].Rows)
                            {
                                dr_final113["DailySales"] = (Convert.ToDecimal(drd["DailySales"])).ToString("#0.00");
                                dr_final113["DailySalesforgp"] = (Convert.ToDecimal(drd["DailySales"]))-(Convert.ToDecimal(drd["DailySalesforgp"]));
                                dr_final113["Todaysalesquantity"] = drd["Todaysalesquantity"].ToString();
                            }
                        }
                        else
                        {
                            dr_final113["DailySales"] = 0;
                            dr_final113["DailySalesforgp"] = 0;
                            dr_final113["Todaysalesquantity"] = 0;
                        }
                    }
                    else
                    {
                        dr_final113["DailySales"] = 0;
                        dr_final113["DailySalesforgp"] = 0;
                        dr_final113["Todaysalesquantity"] = 0;
                    }

                    DataSet dm = bl.getmonthlysalesreport(connection, dr["BranchCode"].ToString());
                    if (dm != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow drm in dm.Tables[0].Rows)
                            {
                                dr_final113["MonthlySales"] = (Convert.ToDecimal(drm["MonthlySales"])).ToString("#0.00");
                                dr_final113["montlySalesforgp"] = (Convert.ToDecimal(drm["MonthlySales"])) - (Convert.ToDecimal(drm["montlySalesforgp"]));
                                dr_final113["monthlysalesquantity"] = drm["monthlysalesquantity"].ToString();

                            }
                        }
                        else
                        {
                            dr_final113["MonthlySales"] = 0;
                            dr_final113["montlySalesforgp"] = 0;
                            dr_final113["monthlysalesquantity"] = 0;
                        }
                    }
                    else
                    {
                        dr_final113["MonthlySales"] = 0;
                        dr_final113["montlySalesforgp"] = 0;
                        dr_final113["monthlysalesquantity"] = 0;
                    }

                    //if (ds.Tables[0].Rows[0]["totalsales"] != null)
                    //    total = (ds.Tables[0].Rows[0]["totalsales"]).ToString();
                    // GrdWME.DataSource = ds;
                    //GrdWME.DataBind();
                    Tottot = Tottot + Convert.ToDouble(dr_final113["MonthlySales"]);
                    dailysalestot = dailysalestot + Convert.ToDouble(dr_final113["DailySales"]);
                    dailyqty = dailyqty + Convert.ToInt32(dr_final113["Todaysalesquantity"]);
                    monthlyqty = monthlyqty + Convert.ToInt32(dr_final113["monthlysalesquantity"]);
                    Totgp = Totgp + (Convert.ToDouble(dr_final113["DailySalesforgp"]));
                     totgpmon = totgpmon + (Convert.ToDouble(dr_final113["montlySalesforgp"]));
                    dt.Rows.Add(dr_final113);

                }
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                DataRow dr_final88 = dt.NewRow();
                dr_final88["Branchname"] = "Total";
                dr_final88["MonthlySales"] = Tottot.ToString("#0.00");
                dr_final88["DailySalesforgp"] = Totgp.ToString("#0.00");
                dr_final88["montlySalesforgp"] = totgpmon.ToString("#0.00");
                dr_final88["DailySales"] = dailysalestot.ToString("#0.00");
                dr_final88["Todaysalesquantity"] = dailyqty.ToString("#0");
                dr_final88["monthlysalesquantity"] = monthlyqty.ToString("#0");
                dt.Rows.Add(dr_final88);

                dst.Tables.Add(dt);
                GridViewdp.DataSource = dst;
                GridViewdp.DataBind();
            }
            else
            {
                GridViewdp.DataSource = null;
                GridViewdp.DataBind();
            }
        }
        else
        {
            GridViewdp.DataSource = null;
            GridViewdp.DataBind();
        }
    }
    protected void GrdWME_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
           
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
    protected void GrdWME_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = GrdWME.SelectedRow;
            string branch1 = string.Empty;
            string todaydate = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
            string branchcode = row.Cells[0].Text;
          //  string branch1 = dr["BranchCode"].ToString();
           // clickpageevent(sender,e,branchcode);
            Response.Redirect("SalesdailyReport1.aspx?branchcode='" + branchcode +"' ");
           // Response.Write("<script language='javascript'> window.open('SalesdailyReport1.aspx?branchcode=" + branchcode +" ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void clickpageevent(object sender, EventArgs e,string branchcode)
    {
        Response.Write("<script language='javascript'> window.open('SalesdailyReport1.aspx?branchcode=" + branchcode + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
    }
    protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label label1 = (Label)e.Row.FindControl("label1");
               // ImageButton img1 = (ImageButton)e.Row.FindControl("lnkB");
               // ImageButton img2 = (ImageButton)e.Row.FindControl("lnkBDisabled");

                if (label1.Text == "Total" || label1.Text == "")
                {
                   // img1.Visible = false;
                 //   img2.Visible = false;
                    ((Image)e.Row.FindControl("lnkprint")).Visible = false;
                  //  ((Image)e.Row.FindControl("btnEditDisabled")).Visible = false;
                }
               
            }
            
        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void GridViewdp_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GridViewdp, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GridViewdp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = GrdWME.SelectedRow;
            string branch1 = string.Empty;
            string todaydate = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
            string branchcode = row.Cells[0].Text;
            //  string branch1 = dr["BranchCode"].ToString();
            // clickpageevent(sender,e,branchcode);
            Response.Redirect("SalesdailyReport2.aspx?branchcode='" + branchcode + "' ");
            // Response.Write("<script language='javascript'> window.open('SalesdailyReport1.aspx?branchcode=" + branchcode +" ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
   
    protected void GridViewdp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label label1 = (Label)e.Row.FindControl("label1");
                // ImageButton img1 = (ImageButton)e.Row.FindControl("lnkB");
                // ImageButton img2 = (ImageButton)e.Row.FindControl("lnkBDisabled");

                if (label1.Text == "Total" || label1.Text == "")
                {
                    // img1.Visible = false;
                    //   img2.Visible = false;
                    ((Image)e.Row.FindControl("lnkprint")).Visible = false;
                    //  ((Image)e.Row.FindControl("btnEditDisabled")).Visible = false;
                }

            }

        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void BindWMEPUR(string textSearch, string dropDown)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string branch = string.Empty;
        string total = string.Empty;
        string connection = Request.Cookies["Company"].Value;
        string sMonth = DateTime.Now.ToString("MM");
        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);
        var start = startDate.ToString("yyyy-MM-dd");
        var endDate = startDate.AddMonths(1).AddDays(-1);
        var end = endDate.ToString("yyyy-MM-dd");

        string usernam = Request.Cookies["LoggedUserName"].Value;
        double Tottot1 = 0.00;
        double dailypurstot = 0.00;
        int dailypurqty = 0;
        int monthlypurqty = 0;

        string sCustomer = string.Empty;
        connection = Request.Cookies["Company"].Value;
        usernam = Request.Cookies["LoggedUserName"].Value;
        // BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);
        DataSet ds = new DataSet();

        sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["DefaultBranchCode"]);
        //  drpBranch.ClearSelection();
        //  ListItem li = drpBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        //  if (li != null) li.Selected = true;

        if (dsd.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
        {
            ds = bl.ListBranch(connection, usernam);
        }
        else
        {
            dpshow.Visible = false;
            ds = bl.ListBranch1(connection, usernam, sCustomer);
        }

        //  DataSet ds = bl.getdashboardreport(connection);

       // DataSet ds = bl.ListBranch(connection, usernam);

        // DataSet ds;
        DataTable dt;
        DataRow drNew;
        DataSet dstt = new DataSet();

        DataColumn dc;

        DataSet dst = new DataSet();

        dt = new DataTable();
        dt.Columns.Add(new DataColumn("BranchCode"));
        dt.Columns.Add(new DataColumn("Branchname"));
        dt.Columns.Add(new DataColumn("DailyPuchase"));
        dt.Columns.Add(new DataColumn("MonthlyPuchase"));
        dt.Columns.Add(new DataColumn("TodayPuchasequantity"));
        dt.Columns.Add(new DataColumn("monthlyPuchasequantity"));
        dt.Columns.Add(new DataColumn("Amount"));



        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_final113 = dt.NewRow();

                    dr_final113["BranchCode"] = dr["BranchCode"].ToString();
                    dr_final113["Branchname"] = dr["Branchname"].ToString();
                    string branch1 = dr["BranchCode"].ToString();

                    //BusinessLogic bl = new BusinessLogic(sDataSource);
                    DataSet db = bl.getdailypurchasereport(connection, dr["BranchCode"].ToString());
                    if (db != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drd in db.Tables[0].Rows)
                            {
                                dr_final113["DailyPuchase"] = (Convert.ToDecimal(drd["DailyPuchase"])).ToString("#0.00");
                                dr_final113["TodayPuchasequantity"] = drd["TodayPuchasequantity"].ToString();
                            }
                        }
                        else
                        {
                            dr_final113["DailyPuchase"] = 0;
                            dr_final113["TodayPuchasequantity"] = 0;
                        }
                    }
                    else
                    {
                        dr_final113["DailyPuchase"] = 0;
                        dr_final113["TodayPuchasequantity"] = 0;
                    }

                    DataSet dm = bl.getmonthlypurchasereport(connection, dr["BranchCode"].ToString());
                    if (dm != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            foreach (DataRow drm in dm.Tables[0].Rows)
                            {
                                dr_final113["MonthlyPuchase"] = (Convert.ToDecimal(drm["MonthlyPuchase"])).ToString("#0.00");
                                dr_final113["monthlyPuchasequantity"] = drm["monthlyPuchasequantity"].ToString();

                            }
                        }
                        else
                        {
                            dr_final113["MonthlyPuchase"] = 0;
                            dr_final113["monthlyPuchasequantity"] = 0;
                        }
                    }
                    else
                    {
                        dr_final113["MonthlyPuchase"] = 0;
                        dr_final113["monthlyPuchasequantity"] = 0;
                    }

                    //if (ds.Tables[0].Rows[0]["totalsales"] != null)
                    //    total = (ds.Tables[0].Rows[0]["totalsales"]).ToString();
                    // GrdWME.DataSource = ds;
                    //GrdWME.DataBind();
                    Tottot1 = Tottot1 + Convert.ToDouble(dr_final113["MonthlyPuchase"]);
                    dailypurstot = dailypurstot + Convert.ToDouble(dr_final113["DailyPuchase"]);
                    dailypurqty = dailypurqty + Convert.ToInt32(dr_final113["TodayPuchasequantity"]);
                    monthlypurqty = monthlypurqty + Convert.ToInt32(dr_final113["monthlyPuchasequantity"]);
                    dt.Rows.Add(dr_final113);

                }
                DataRow dr_final11 = dt.NewRow();
                dt.Rows.Add(dr_final11);

                DataRow dr_final88 = dt.NewRow();
                dr_final88["Branchname"] = "Total";
                dr_final88["MonthlyPuchase"] = Tottot1.ToString("#0.00");
                dr_final88["DailyPuchase"] = dailypurstot.ToString("#0.00");
                dr_final88["TodayPuchasequantity"] = dailypurqty.ToString("#0");
                dr_final88["monthlyPuchasequantity"] = monthlypurqty.ToString("#0");
                dt.Rows.Add(dr_final88);

                dst.Tables.Add(dt);
                grdpurchase.DataSource = dst;
                grdpurchase.DataBind();
            }
            else
            {
                grdpurchase.DataSource = null;
                grdpurchase.DataBind();
            }
        }
        else
        {
            grdpurchase.DataSource = null;
            grdpurchase.DataBind();
        }
    }
    protected void grdpurchase_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void grdpurchase_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void grdpurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Label12 = (Label)e.Row.FindControl("label12");
                // ImageButton img1 = (ImageButton)e.Row.FindControl("lnkB");
                // ImageButton img2 = (ImageButton)e.Row.FindControl("lnkBDisabled");

                if (Label12.Text == "Total" || Label12.Text == "")
                {
                    // img1.Visible = false;
                    //   img2.Visible = false;
                    ((Image)e.Row.FindControl("lnkprint1")).Visible = false;
                  //  ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = false;
                }

            }
            
        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       // Page_Load(sender, e);
        BindWME("", "");
        BindWME1("","");
        BindWMEPUR("", "");

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindWME("", "");
        BindWME1("", "");
        BindWMEPUR("", "");
    }
    protected void dpshow_Click(object sender, EventArgs e)
    {
        if (GridViewdp.Visible == true)
        {
            GridViewdp.Visible = false;
        }
        else
        {
            GridViewdp.Visible = true;
            tblWarning.Visible = true;
        }
    }
}
