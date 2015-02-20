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
using System.Collections.Generic;
using System.Text;

public partial class PhysicalStockForm : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            datasrc.ConnectionString = sDataSource;
            DataSet ds = new DataSet();

            if (!IsPostBack)
            {
                //txtDate.Text = DateTime.Now.ToShortDateString();

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtDate.Text = dtaa;

                BindGrid();
                hiddenDate.Value = Convert.ToDateTime(txtDate.Text).ToString("MM/dd/yyyy");

                err.Text = "";
                //cmpVal.ValueToCompare = DateTime.Now.ToString();
                BusinessLogic objChk = new BusinessLogic(sDataSource);

                string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    cmdSave.Enabled = false;
                }

                var checkIfExists = objChk.GetClosingStock(txtDate.Text);

                if (checkIfExists == null || (checkIfExists != null && checkIfExists.Tables[0].Rows.Count == 0))
                {
                    err.Visible = true;
                    err.Text = "No Physical Stock stored for Today's Date";
                }
                else
                {
                    err.Visible = true;
                    err.Text = "Physical stock values are stored for Today's date";
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    private void BindGrid()
    {

        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        DataSet ds = new DataSet();

        BusinessLogic bl = new BusinessLogic(sDataSource);
        string sDate = string.Empty;
        sDate = txtDate.Text.Trim();
        ds = bl.GetStockItems(sDate);

        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            err.Visible = true;
            err.Text = "No Physical Stock stored for the Date";
        }
        else
        {
            err.Visible = false;

            var checkIfExists = bl.GetClosingStock(txtDate.Text);

            if (checkIfExists == null || (checkIfExists != null && checkIfExists.Tables[0].Rows.Count == 0))
            {
                err.Visible = true;
                err.Text = "No Physical Stock stored for the Date";
            }
            else
            {
                err.Visible = true;
                err.Text = "Physical stock values are stored for the Date";
            }
        }

        EditableGrid.DataSource = ds;
        EditableGrid.DataBind();



    }
    private void BindOpenGrid()
    {

        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        DataSet ds = new DataSet();

        BusinessLogic bl = new BusinessLogic(sDataSource);
        string sDate = string.Empty;
        sDate = txtDate.Text.Trim();
        ds = bl.GetStockOpeningItems(sDate);
        EditableGrid.DataSource = ds;
        EditableGrid.DataBind();



    }
    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                err.Text = "";
                //long diff = DateDiff(DateInterval.Day, Convert.ToDateTime(txtDate.Text), DateTime.Now);

                //if (diff<=0)
                //{
                //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Date should be less than or equal to today')", true);
                //    err.Text ="Date should be less than or equal to today";
                //    cmdSave.Enabled = false;
                //    return;
                //}

                if (EditableGrid.Rows.Count <= 0)
                {
                    return;
                }

                if (txtDate.Text.Trim() == "")
                {
                    err.Visible = true;
                    err.Text = "Date is Blank";
                    return;
                }
                else
                {
                    err.Text = "";
                }
                if (Convert.ToDateTime(txtDate.Text) > DateTime.Now)
                {
                    err.Visible = true;
                    err.Text = "Date should be less than or equal to today";
                    cmdSave.Enabled = false;
                    return;
                }
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                string itemCode = string.Empty;
                string closingDate = string.Empty;


                BusinessLogic bl = new BusinessLogic(sDataSource);
                bl.DeleteClosingStock(txtDate.Text);
                if (EditableGrid.Rows.Count > 0)
                {

                    foreach (GridViewRow gr in EditableGrid.Rows)
                    {
                        TextBox txtStock = (TextBox)gr.Cells[4].FindControl("txtStock");
                        itemCode = gr.Cells[0].Text.Replace("&quot;", "\"");

                        if (itemCode.Contains("&amp;"))
                        {
                            itemCode = itemCode.Replace("&amp;", "&");
                        }

                        closingDate = txtDate.Text;
                        if (txtStock.Text.Trim() != "")
                        {
                            bl.InsertClosingStock(@itemCode, closingDate, Convert.ToDouble(txtStock.Text));
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Check the stock value some might be not given or entered wrongly')", true);
                            err.Visible = true;
                            err.Text = "Check the stock value some might be not given or entered wrongly";
                            return;
                        }
                    }
                }

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Physical stock values are stored for the date:" + closingDate + "')", true);
                err.Visible = true;
                err.Text = "Physical stock values are stored for the date: " + closingDate;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void cmdShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text.Trim() == "")
            {
                err.Visible = true;
                err.Text = "Date is Blank";
                return;
            }
            else
            {
                err.Visible = false;
                err.Text = "";
            }

            BindGrid();
            cmdSave.Enabled = true;

            if (Convert.ToDateTime(txtDate.Text) > DateTime.Now)
            {
                err.Visible = true;
                err.Text = "Date should be less than or equal to today";
                cmdSave.Enabled = false;
                return;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void cmdOpen_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text.Trim() == "")
            {
                err.Visible = true;
                err.Text = "Date is Blank";
                return;
            }
            else
            {
                err.Visible = false;
                err.Text = "";
            }
            BindOpenGrid();
            cmdSave.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2)
    {
        return DateDiff(interval, dt1, dt2, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
    }
    public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2, DayOfWeek eFirstDayOfWeek)
    {
        if (interval == DateInterval.Year)
            return dt2.Year - dt1.Year;

        if (interval == DateInterval.Month)
            return (dt2.Month - dt1.Month) + (12 * (dt2.Year - dt1.Year));

        TimeSpan ts = dt2 - dt1;

        if (interval == DateInterval.Day || interval == DateInterval.DayOfYear)
            return Round(ts.TotalDays);

        if (interval == DateInterval.Hour)
            return Round(ts.TotalHours);

        if (interval == DateInterval.Minute)
            return Round(ts.TotalMinutes);

        if (interval == DateInterval.Second)
            return Round(ts.TotalSeconds);

        if (interval == DateInterval.Weekday)
        {
            return Round(ts.TotalDays / 7.0);
        }

        if (interval == DateInterval.WeekOfYear)
        {
            while (dt2.DayOfWeek != eFirstDayOfWeek)
                dt2 = dt2.AddDays(-1);
            while (dt1.DayOfWeek != eFirstDayOfWeek)
                dt1 = dt1.AddDays(-1);
            ts = dt2 - dt1;
            return Round(ts.TotalDays / 7.0);
        }

        if (interval == DateInterval.Quarter)
        {
            double d1Quarter = GetQuarter(dt1.Month);
            double d2Quarter = GetQuarter(dt2.Month);
            double d1 = d2Quarter - d1Quarter;
            double d2 = (4 * (dt2.Year - dt1.Year));
            return Round(d1 + d2);
        }

        return 0;

    }
    public enum DateInterval
    {
        Day,
        DayOfYear,
        Hour,
        Minute,
        Month,
        Quarter,
        Second,
        Weekday,
        WeekOfYear,
        Year
    }
    private static long Round(double dVal)
    {
        if (dVal >= 0)
            return (long)Math.Floor(dVal);
        return (long)Math.Ceiling(dVal);
    }

    private static int GetQuarter(int nMonth)
    {
        if (nMonth <= 3)
            return 1;
        if (nMonth <= 6)
            return 2;
        if (nMonth <= 9)
            return 3;
        return 4;
    }

}
