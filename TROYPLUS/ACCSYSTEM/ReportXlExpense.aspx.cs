using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportXlExpense : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    BusinessLogic objBL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!IsPostBack)
            {


                //if (Request.Cookies["Company"] != null)
                //    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();
                //BusinessLogic bl = new BusinessLogic(sDataSource);

                objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string cond = "";
            cond = getCond();

            bindDataSubTot(cond);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private bool isValidLevels()
    {

        if ((CheckBox1.Checked == false) && (CheckBox2.Checked == false) && (CheckBox3.Checked == false) && (CheckBox4.Checked == false) && (CheckBox5.Checked == false) && (CheckBox5.Checked == false) && (CheckBox6.Checked == false) && (CheckBox7.Checked == false) && (CheckBox8.Checked == false) && (CheckBox9.Checked == false) && (CheckBox10.Checked == false) && (CheckBox11.Checked == false) &&  (CheckBox12.Checked == false))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please Select Atleast Any One Month');", true);
            return false;
        }
        
        return true;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (!isValidLevels())
            {
                return;
            }

            if (optionrate.SelectedItem.Text == "Monthly Wise")
            {
                bindData();
            }
            else
            {
                if (CheckBox13.Checked == true)
                {
                    bindDataHeading();
                }
                else
                {
                    bindDataHeadingSelect();
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void CheckBox13_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (CheckBox13.Checked == true)
            {
                CheckBox1.Checked = true;
                CheckBox2.Checked = true;
                CheckBox3.Checked = true;
                CheckBox4.Checked = true;
                CheckBox5.Checked = true;
                CheckBox6.Checked = true;
                CheckBox7.Checked = true;
                CheckBox8.Checked = true;
                CheckBox9.Checked = true;
                CheckBox10.Checked = true;
                CheckBox11.Checked = true;
                CheckBox12.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
                CheckBox2.Checked = false;
                CheckBox3.Checked = false;
                CheckBox4.Checked = false;
                CheckBox5.Checked = false;
                CheckBox6.Checked = false;
                CheckBox7.Checked = false;
                CheckBox8.Checked = false;
                CheckBox9.Checked = false;
                CheckBox10.Checked = false;
                CheckBox11.Checked = false;
                CheckBox12.Checked = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected string getfield()
    {
        string field1 = "";
        string field = "";

        if (CheckBox1.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }

            field1 += "April";
        }

        if (CheckBox2.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }
            field1 += "May";
        }
        if (CheckBox3.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }
            field1 += "June";

        }
        if (CheckBox4.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }
            field1 += "July";

        }

        if (CheckBox5.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }

            field1 += "August";
        }
        if (CheckBox6.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }

            field1 += "September";
        }
        if (CheckBox7.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }

            field1 += "October";
        }
        if (CheckBox8.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }

            field1 += "November";
        }
        if (CheckBox9.Checked == true)
        {
            if (field1 == "")
            {
                field1 = "";

            }
            else
            {
                field1 += ",";
            }
            field1 += "December";
        }
        if (CheckBox10.Checked == true)
        {
            if (field1 == "")
            {
                field1 = "";

            }
            else
            {
                field1 += ",";
            }
            field1 += "January";
        }

        if (CheckBox11.Checked == true)
        {
            if (field1 == "")
            {
                field1 = "";

            }
            else
            {
                field1 += ",";
            }
            field1 += "February";
        }


        if (CheckBox1.Checked == true)
        {
            if (field1 == "")
            {
                field1 = "";

            }
            else
            {
                field1 += ",";
            }
            field1 += "March";
        }
        

        return field1;
    }

    public void bindDataHeadingSelect()
    {

        DataSet ds = new DataSet();
        DataSet dstt = new DataSet();
        DataSet dsGir = new DataSet();
        DataTable dt = new DataTable();

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string condtion = "";

        DataSet dsttt = new DataSet();
        dsttt = objBL.getmonthexpensemonth(condtion);

        ds = objBL.getmonthexpenseheading(condtion);


        if (ds.Tables[0].Rows.Count > 0)
        {

            dstt = objBL.getexpensetypes();
            DataTable dtt = new DataTable();
            DataColumn dc;
            if (dstt.Tables[0].Rows.Count > 0)
            {
                dc = new DataColumn("Month");
                dtt.Columns.Add(dc);
                for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                {
                    string ledger = dstt.Tables[0].Rows[i]["LedgerName"].ToString();
                    dc = new DataColumn(ledger);
                    dtt.Columns.Add(dc);
                }
                dc = new DataColumn("Total");
                dtt.Columns.Add(dc);
            }
            dsGir.Tables.Add(dtt);


            DataRow dr_final14 = dtt.NewRow();
            dtt.Rows.Add(dr_final14);

            double credit = 0.00;
            double Tottot = 0.00;
            
            foreach (DataRow drd in dsttt.Tables[0].Rows)
            {
                DataRow dr_final12 = dtt.NewRow();
                string hhh = drd["monthname"].ToString().ToUpper().Trim();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string aa = dr["monthname"].ToString().ToUpper().Trim();
                    if (hhh == aa)
                    {
                        if (aa == "1")
                        {
                            dr_final12["Month"] = "January";
                        }
                        else if (aa == "2")
                        {
                            dr_final12["Month"] = "February";
                        }
                        else if (aa == "3")
                        {
                            dr_final12["Month"] = "March";
                        }
                        else if (aa == "4")
                        {
                            dr_final12["Month"] = "April";
                        }
                        else if (aa == "5")
                        {
                            dr_final12["Month"] = "May";
                        }
                        else if (aa == "6")
                        {
                            dr_final12["Month"] = "June";
                        }
                        else if (aa == "7")
                        {
                            dr_final12["Month"] = "July";
                        }
                        else if (aa == "8")
                        {
                            dr_final12["Month"] = "August";
                        }
                        else if (aa == "9")
                        {
                            dr_final12["Month"] = "September";
                        }
                        else if (aa == "10")
                        {
                            dr_final12["Month"] = "October";
                        }
                        else if (aa == "11")
                        {
                            dr_final12["Month"] = "November";
                        }
                        else if (aa == "12")
                        {
                            dr_final12["Month"] = "December";
                        }

                        string ledgernam = dr["LedgerName"].ToString().ToUpper().Trim();
                        for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                        {
                            string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                            if (ledgernam == ledgerna)
                            {
                                dr_final12[ledgerna] = double.Parse(dr["Total"].ToString());
                                credit = credit + double.Parse(dr["Total"].ToString());
                                Tottot = Tottot + double.Parse(dr["Total"].ToString());
                            }
                            else
                            {
                                if (dr_final12[ledgerna] == null)
                                {
                                    dr_final12[ledgerna] = "";
                                }
                            }
                        }
                    }
                }
                dr_final12["Total"] = credit;
                credit = 0.00;

                string mmm = dr_final12["Month"].ToString().ToUpper().Trim();

                if (mmm=="JANUARY")
                {
                    if (CheckBox10.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
                else if (mmm == "FEBRUARY")
                {
                    if (CheckBox11.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
                else if (mmm == "MARCH")
                {
                    if (CheckBox12.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
                else if (mmm == "APRIL")
                {
                    if (CheckBox1.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
                else if (mmm == "MAY")
                {
                    if (CheckBox2.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
                else if (mmm == "JUNE")
                {
                    if (CheckBox3.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
                else if (mmm == "JULY")
                {
                    if (CheckBox4.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
                else if (mmm == "AUGUST")
                {
                    if (CheckBox5.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
                else if (mmm == "SEPTEMBER")
                {
                    if (CheckBox6.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
                else if (mmm == "OCTOBER")
                {
                    if (CheckBox7.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
                else if (mmm == "NOVEMBER")
                {
                    if (CheckBox8.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
                else if (mmm == "DECEMBER")
                {
                    if (CheckBox9.Checked == true)
                        dtt.Rows.Add(dr_final12);
                }
            }


            //DataRow dr_final13 = dt.NewRow();
            //Double gdd = 0.00;
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    dr_final13["Month"] = "";
            //    if (CheckBox1.Checked == true)
            //    {
            //        if (dr["monthname"] == "4")
            //        {
            //            dr_final13["April"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["April"] == null)
            //                dr_final13["April"] = "";
            //        }
            //    }
            //    if (CheckBox2.Checked == true)
            //    {
            //        if (dr["monthname"] == "5")
            //        {
            //            dr_final13["May"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["May"] == null)
            //                dr_final13["May"] = "";
            //        }
            //    }
            //    if (CheckBox3.Checked == true)
            //    {
            //        if (dr["monthname"] == "6")
            //        {
            //            dr_final13["June"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["June"] == null)
            //                dr_final13["June"] = "";
            //        }
            //    }
            //    if (CheckBox4.Checked == true)
            //    {
            //        if (dr["monthname"] == "7")
            //        {
            //            dr_final13["July"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["July"] == null)
            //                dr_final13["July"] = "";
            //        }
            //    }
            //    if (CheckBox5.Checked == true)
            //    {
            //        if (dr["monthname"] == "8")
            //        {
            //            dr_final13["August"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["August"] == null)
            //                dr_final13["August"] = "";
            //        }
            //    }
            //    if (CheckBox6.Checked == true)
            //    {
            //        if (dr["monthname"] == "9")
            //        {
            //            dr_final13["September"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["September"] == null)
            //                dr_final13["September"] = "";
            //        }
            //    }
            //    if (CheckBox7.Checked == true)
            //    {
            //        if (dr["monthname"] == "10")
            //        {
            //            dr_final13["October"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["October"] == null)
            //                dr_final13["October"] = "";
            //        }
            //    }
            //    if (CheckBox8.Checked == true)
            //    {
            //        if (dr["monthname"] == "11")
            //        {
            //            dr_final13["November"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["November"] == null)
            //                dr_final13["November"] = "";
            //        }
            //    }
            //    if (CheckBox9.Checked == true)
            //    {
            //        if (dr["monthname"].ToString() == "12")
            //        {
            //            dr_final13["December"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["December"] == null)
            //                dr_final13["December"] = "";
            //        }
            //    }
            //    if (CheckBox10.Checked == true)
            //    {
            //        if (dr["monthname"].ToString() == "1")
            //        {
            //            dr_final13["January"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["January"] == null)
            //                dr_final13["January"] = "";
            //        }
            //    }
            //    if (CheckBox11.Checked == true)
            //    {
            //        if (dr["monthname"] == "2")
            //        {
            //            dr_final13["February"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["February"] == null)
            //                dr_final13["February"] = "";
            //        }
            //    }
            //    if (CheckBox12.Checked == true)
            //    {
            //        if (dr["monthname"] == "3")
            //        {
            //            dr_final13["March"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["March"] == null)
            //                dr_final13["March"] = "";
            //        }
            //    }
            //}
            //dr_final13["Total"] = gdd;
            //dt.Rows.Add(dr_final13);

            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataHeading()
    {
        DataSet ds = new DataSet();
        DataSet dstt = new DataSet();
        DataSet dsGir = new DataSet();
        DataTable dt = new DataTable();

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string condtion = "";

        DataSet dsttt = new DataSet();
        dsttt = objBL.getmonthexpensemonth(condtion);

        ds = objBL.getmonthexpenseheading(condtion);


        if (ds.Tables[0].Rows.Count > 0)
        {

            dstt = objBL.getexpensetypes();
            DataTable dtt = new DataTable();
            DataColumn dc;
            if (dstt.Tables[0].Rows.Count > 0)
            {
                dc = new DataColumn("Month");
                dtt.Columns.Add(dc);
                for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                {
                    string ledger = dstt.Tables[0].Rows[i]["LedgerName"].ToString();
                    dc = new DataColumn(ledger);
                    dtt.Columns.Add(dc);
                }
                dc = new DataColumn("Total");
                dtt.Columns.Add(dc);
            }
            dsGir.Tables.Add(dtt);


            DataRow dr_final14 = dtt.NewRow();
            dtt.Rows.Add(dr_final14);

            double credit = 0.00;
            double Tottot = 0.00;


            foreach (DataRow drd in dsttt.Tables[0].Rows)
            {
                DataRow dr_final12 = dtt.NewRow();
                string hhh = drd["monthname"].ToString().ToUpper().Trim();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string aa = dr["monthname"].ToString().ToUpper().Trim();
                    if (hhh == aa)
                    {
                        if (aa == "1")
                        {
                            dr_final12["Month"] = "January";
                        }
                        else if (aa == "2")
                        {
                            dr_final12["Month"] = "February";
                        }
                        else if (aa == "3")
                        {
                            dr_final12["Month"] = "March";
                        }
                        else if (aa == "4")
                        {
                            dr_final12["Month"] = "April";
                        }
                        else if (aa == "5")
                        {
                            dr_final12["Month"] = "May";
                        }
                        else if (aa == "6")
                        {
                            dr_final12["Month"] = "June";
                        }
                        else if (aa == "7")
                        {
                            dr_final12["Month"] = "July";
                        }
                        else if (aa == "8")
                        {
                            dr_final12["Month"] = "August";
                        }
                        else if (aa == "9")
                        {
                            dr_final12["Month"] = "September";
                        }
                        else if (aa == "10")
                        {
                            dr_final12["Month"] = "October";
                        }
                        else if (aa == "11")
                        {
                            dr_final12["Month"] = "November";
                        }
                        else if (aa == "12")
                        {
                            dr_final12["Month"] = "December";
                        }

                        string ledgernam = dr["LedgerName"].ToString().ToUpper().Trim();
                        for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                        {
                            string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                            if (ledgernam == ledgerna)
                            {
                                dr_final12[ledgerna] = double.Parse(dr["Total"].ToString());
                                credit = credit + double.Parse(dr["Total"].ToString());
                                Tottot = Tottot + double.Parse(dr["Total"].ToString());
                            }
                            else
                            {
                                if (dr_final12[ledgerna] == null)
                                {
                                    dr_final12[ledgerna] = "";
                                }
                            }
                        }
                    }
                }
                dr_final12["Total"] = credit;
                credit=0.00;
                dtt.Rows.Add(dr_final12);
            }


            //DataRow dr_final13 = dt.NewRow();

            //Double gdd = 0.00;

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{

            //    dr_final13["Month"] = "";

            //    if (CheckBox1.Checked == true)
            //    {
            //        if (dr["monthname"] == "4")
            //        {
            //            dr_final13["April"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["April"] == null)
            //                dr_final13["April"] = "";
            //        }
            //    }
            //    if (CheckBox2.Checked == true)
            //    {
            //        if (dr["monthname"] == "5")
            //        {
            //            dr_final13["May"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["May"] == null)
            //                dr_final13["May"] = "";
            //        }
            //    }
            //    if (CheckBox3.Checked == true)
            //    {
            //        if (dr["monthname"] == "6")
            //        {
            //            dr_final13["June"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["June"] == null)
            //                dr_final13["June"] = "";
            //        }
            //    }
            //    if (CheckBox4.Checked == true)
            //    {
            //        if (dr["monthname"] == "7")
            //        {
            //            dr_final13["July"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["July"] == null)
            //                dr_final13["July"] = "";
            //        }
            //    }
            //    if (CheckBox5.Checked == true)
            //    {
            //        if (dr["monthname"] == "8")
            //        {
            //            dr_final13["August"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["August"] == null)
            //                dr_final13["August"] = "";
            //        }
            //    }
            //    if (CheckBox6.Checked == true)
            //    {
            //        if (dr["monthname"] == "9")
            //        {
            //            dr_final13["September"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["September"] == null)
            //                dr_final13["September"] = "";
            //        }
            //    }
            //    if (CheckBox7.Checked == true)
            //    {
            //        if (dr["monthname"] == "10")
            //        {
            //            dr_final13["October"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["October"] == null)
            //                dr_final13["October"] = "";
            //        }
            //    }
            //    if (CheckBox8.Checked == true)
            //    {
            //        if (dr["monthname"] == "11")
            //        {
            //            dr_final13["November"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["November"] == null)
            //                dr_final13["November"] = "";
            //        }
            //    }
            //    if (CheckBox9.Checked == true)
            //    {
            //        if (dr["monthname"].ToString() == "12")
            //        {
            //            dr_final13["December"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["December"] == null)
            //                dr_final13["December"] = "";
            //        }
            //    }
            //    if (CheckBox10.Checked == true)
            //    {
            //        if (dr["monthname"].ToString() == "1")
            //        {
            //            dr_final13["January"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["January"] == null)
            //                dr_final13["January"] = "";
            //        }
            //    }
            //    if (CheckBox11.Checked == true)
            //    {
            //        if (dr["monthname"] == "2")
            //        {
            //            dr_final13["February"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["February"] == null)
            //                dr_final13["February"] = "";
            //        }
            //    }
            //    if (CheckBox12.Checked == true)
            //    {
            //        if (dr["monthname"] == "3")
            //        {
            //            dr_final13["March"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["March"] == null)
            //                dr_final13["March"] = "";
            //        }
            //    }
            //}

            //dr_final13["Total"] = gdd;
            //dt.Rows.Add(dr_final13);

            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataHeading512013()
    {
        DataSet ds = new DataSet();
        DataSet dstt = new DataSet();
        DataSet dsGir = new DataSet();
        DataTable dt = new DataTable();

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string condtion = "";

        DataSet dsttt = new DataSet();
        dsttt = objBL.getmonthexpensemonth(condtion);
                
        ds = objBL.getmonthexpenseheading(condtion);


        if (ds.Tables[0].Rows.Count > 0)
        {

            dstt = objBL.getexpensetypes();
            DataTable dtt = new DataTable();
            DataColumn dc;
            if (dstt.Tables[0].Rows.Count > 0)
            {
                dc = new DataColumn("Month");
                dtt.Columns.Add(dc);
                for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
                {
                    string ledger = dstt.Tables[0].Rows[i]["LedgerName"].ToString();
                    dc = new DataColumn(ledger);
                    dtt.Columns.Add(dc);
                }
                dc = new DataColumn("Total");
                dtt.Columns.Add(dc);
            }
            dsGir.Tables.Add(dtt);
            

            DataRow dr_final14 = dtt.NewRow();
            dtt.Rows.Add(dr_final14);

            double credit = 0.00;
            double Tottot = 0.00;

            
            foreach (DataRow drd in dsttt.Tables[0].Rows)
            {
                DataRow dr_final12 = dtt.NewRow();
                string hhh = drd["monthname"].ToString().ToUpper().Trim();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string aa = dr["monthname"].ToString().ToUpper().Trim();
                    if (hhh == aa)
                    {
                        if (aa == "1")
                        {
                            dr_final12["Month"] = "January";
                        }
                        else if (aa == "2")
                        {
                            dr_final12["Month"] = "February";
                        }
                        else if (aa == "3")
                        {
                            dr_final12["Month"] = "March";
                        }
                        else if (aa == "4")
                        {
                            dr_final12["Month"] = "April";
                        }
                        else if (aa == "5")
                        {
                            dr_final12["Month"] = "May";
                        }
                        else if (aa == "6")
                        {
                            dr_final12["Month"] = "June";
                        }
                        else if (aa == "7")
                        {
                            dr_final12["Month"] = "July";
                        }
                        else if (aa == "8")
                        {
                            dr_final12["Month"] = "August";
                        }
                        else if (aa == "9")
                        {
                            dr_final12["Month"] = "September";
                        }
                        else if (aa == "10")
                        {
                            dr_final12["Month"] = "October";
                        }
                        else if (aa == "11")
                        {
                            dr_final12["Month"] = "November";
                        }
                        else if (aa == "12")
                        {
                            dr_final12["Month"] = "December";
                        }

                        string ledgernam = dr["LedgerName"].ToString().ToUpper().Trim();
                        for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                        {
                            string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                            if (ledgernam == ledgerna)
                            {
                                dr_final12[ledgerna] = double.Parse(dr["Total"].ToString());
                                credit = credit + double.Parse(dr["Total"].ToString());
                                Tottot = Tottot + double.Parse(dr["Total"].ToString());
                            }
                            else
                            {
                                if (dr_final12[ledgerna] == null)
                                {
                                    dr_final12[ledgerna] = "";
                                }
                            }
                        }
                    }
                }
                dtt.Rows.Add(dr_final12);
            }
            

            //DataRow dr_final13 = dt.NewRow();

            //Double gdd = 0.00;

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{

            //    dr_final13["Month"] = "";

            //    if (CheckBox1.Checked == true)
            //    {
            //        if (dr["monthname"] == "4")
            //        {
            //            dr_final13["April"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["April"] == null)
            //                dr_final13["April"] = "";
            //        }
            //    }
            //    if (CheckBox2.Checked == true)
            //    {
            //        if (dr["monthname"] == "5")
            //        {
            //            dr_final13["May"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["May"] == null)
            //                dr_final13["May"] = "";
            //        }
            //    }
            //    if (CheckBox3.Checked == true)
            //    {
            //        if (dr["monthname"] == "6")
            //        {
            //            dr_final13["June"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["June"] == null)
            //                dr_final13["June"] = "";
            //        }
            //    }
            //    if (CheckBox4.Checked == true)
            //    {
            //        if (dr["monthname"] == "7")
            //        {
            //            dr_final13["July"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["July"] == null)
            //                dr_final13["July"] = "";
            //        }
            //    }
            //    if (CheckBox5.Checked == true)
            //    {
            //        if (dr["monthname"] == "8")
            //        {
            //            dr_final13["August"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["August"] == null)
            //                dr_final13["August"] = "";
            //        }
            //    }
            //    if (CheckBox6.Checked == true)
            //    {
            //        if (dr["monthname"] == "9")
            //        {
            //            dr_final13["September"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["September"] == null)
            //                dr_final13["September"] = "";
            //        }
            //    }
            //    if (CheckBox7.Checked == true)
            //    {
            //        if (dr["monthname"] == "10")
            //        {
            //            dr_final13["October"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["October"] == null)
            //                dr_final13["October"] = "";
            //        }
            //    }
            //    if (CheckBox8.Checked == true)
            //    {
            //        if (dr["monthname"] == "11")
            //        {
            //            dr_final13["November"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["November"] == null)
            //                dr_final13["November"] = "";
            //        }
            //    }
            //    if (CheckBox9.Checked == true)
            //    {
            //        if (dr["monthname"].ToString() == "12")
            //        {
            //            dr_final13["December"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["December"] == null)
            //                dr_final13["December"] = "";
            //        }
            //    }
            //    if (CheckBox10.Checked == true)
            //    {
            //        if (dr["monthname"].ToString() == "1")
            //        {
            //            dr_final13["January"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["January"] == null)
            //                dr_final13["January"] = "";
            //        }
            //    }
            //    if (CheckBox11.Checked == true)
            //    {
            //        if (dr["monthname"] == "2")
            //        {
            //            dr_final13["February"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["February"] == null)
            //                dr_final13["February"] = "";
            //        }
            //    }
            //    if (CheckBox12.Checked == true)
            //    {
            //        if (dr["monthname"] == "3")
            //        {
            //            dr_final13["March"] = Convert.ToDouble(dr["Total"]);
            //            gdd = gdd + Convert.ToDouble(dr["Total"]);
            //        }
            //        else
            //        {
            //            if (dr_final13["March"] == null)
            //                dr_final13["March"] = "";
            //        }
            //    }
            //}

            //dr_final13["Total"] = gdd;
            //dt.Rows.Add(dr_final13);

            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        string condtion = "";

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        ds = objBL.getmonthexpensemonth(condtion);

        
        if (ds.Tables[0].Rows.Count > 0)
        {
            dt.Columns.Add(new DataColumn("Month"));

            if (CheckBox1.Checked == true)
            {
                dt.Columns.Add(new DataColumn("April"));
            }
            if (CheckBox2.Checked == true)
            {
                dt.Columns.Add(new DataColumn("May"));
            }
            if (CheckBox3.Checked == true)
            {
                dt.Columns.Add(new DataColumn("June"));
            }
            if (CheckBox4.Checked == true)
            {
                dt.Columns.Add(new DataColumn("July"));
            }
            if (CheckBox5.Checked == true)
            {
                dt.Columns.Add(new DataColumn("August"));
            }
            if (CheckBox6.Checked == true)
            {
                dt.Columns.Add(new DataColumn("September"));
            }
            if (CheckBox7.Checked == true)
            {
                dt.Columns.Add(new DataColumn("October"));
            }
            if (CheckBox8.Checked == true)
            {
                dt.Columns.Add(new DataColumn("November"));
            }
            if (CheckBox9.Checked == true)
            {
                dt.Columns.Add(new DataColumn("December"));
            }
            if (CheckBox10.Checked == true)
            {
                dt.Columns.Add(new DataColumn("January"));
            }
            if (CheckBox11.Checked == true)
            {
                dt.Columns.Add(new DataColumn("February"));
            }
            if (CheckBox12.Checked == true)
            {
                dt.Columns.Add(new DataColumn("March"));
            }

            dt.Columns.Add(new DataColumn("Total"));

            DataRow dr_final17 = dt.NewRow();
            dt.Rows.Add(dr_final17);

            DataRow dr_final13 = dt.NewRow();

            Double gdd = 0.00;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                
                dr_final13["Month"] = "";

                if (CheckBox1.Checked == true)
                {
                    if (dr["monthname"] == "4")
                    {
                        dr_final13["April"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["April"] == null)
                            dr_final13["April"] = "";
                    }
                }
                if (CheckBox2.Checked == true)
                {
                    if (dr["monthname"] == "5")
                    {
                        dr_final13["May"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["May"] == null)
                            dr_final13["May"] = "";
                    }
                }
                if (CheckBox3.Checked == true)
                {
                    if (dr["monthname"] == "6")
                    {
                        dr_final13["June"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["June"] == null)
                            dr_final13["June"] = "";
                    }
                }
                if (CheckBox4.Checked == true)
                {
                    if (dr["monthname"] == "7")
                    {
                        dr_final13["July"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["July"] == null)
                            dr_final13["July"] = "";
                    }
                }
                if (CheckBox5.Checked == true)
                {
                    if (dr["monthname"] == "8")
                    {
                        dr_final13["August"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["August"] == null)
                            dr_final13["August"] = "";
                    }
                }
                if (CheckBox6.Checked == true)
                {
                    if (dr["monthname"] == "9")
                    {
                        dr_final13["September"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["September"] == null)
                            dr_final13["September"] = "";
                    }
                }
                if (CheckBox7.Checked == true)
                {
                    if (dr["monthname"] == "10")
                    {
                        dr_final13["October"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["October"] == null)
                            dr_final13["October"] = "";
                    }
                }
                if (CheckBox8.Checked == true)
                {
                    if (dr["monthname"] == "11")
                    {
                        dr_final13["November"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["November"] == null)
                            dr_final13["November"] = "";
                    }
                }
                if (CheckBox9.Checked == true)
                {
                    if (dr["monthname"].ToString() == "12")
                    {
                        dr_final13["December"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["December"] == null)
                            dr_final13["December"] = "";
                    }
                }
                if (CheckBox10.Checked == true)
                {
                    if (dr["monthname"].ToString() == "1")
                    {
                        dr_final13["January"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["January"] == null)
                            dr_final13["January"] = "";
                    }
                }
                if (CheckBox11.Checked == true)
                {
                    if (dr["monthname"] == "2")
                    {
                        dr_final13["February"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["February"] == null)
                            dr_final13["February"] = "";
                    }
                }
                if (CheckBox12.Checked == true)
                {
                    if (dr["monthname"] == "3")
                    {
                        dr_final13["March"] = Convert.ToDouble(dr["Total"]);
                        gdd = gdd + Convert.ToDouble(dr["Total"]);
                    }
                    else
                    {
                        if (dr_final13["March"] == null)
                            dr_final13["March"] = "";
                    }
                }
            }

            dr_final13["Total"] = gdd;
            dt.Rows.Add(dr_final13);

            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    protected string getCond()
    {
        string cond = "";
        
        //if (txtStartDate.Text != "" && txtEndDate.Text != "")
        //{
        //    objBL.StartDate = txtStartDate.Text;

        //    objBL.StartDate = string.Format("{0:MM/dd/yyyy}", txtStartDate.Text);
        //    objBL.EndDate = txtEndDate.Text;
        //    objBL.EndDate = string.Format("{0:MM/dd/yyyy}", txtEndDate.Text);


        //    string aa = txtStartDate.Text;
        //    string dt = Convert.ToDateTime(aa).ToString("MM/dd/yyyy");

        //    string aaa = txtEndDate.Text;
        //    string dtt = Convert.ToDateTime(aaa).ToString("MM/dd/yyyy");
  
        //    cond = " BillDate >= #" + dt + "# and billdate <= #" + dtt + "# ";
        //}
        
        return cond;
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            string filename = "Expense.xls";
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


    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //    {
    //        string ledger = ds.Tables[0].Rows[i]["Ledger"].ToString();
    //        double credit = 0.00;
    //        double debit = 0.00;

    //        foreach (DataRow dr in dsData.Tables[0].Rows)
    //        {
    //            if (dr["Ledger"].ToString() == ledger)
    //            {
    //                credit = credit + double.Parse(dr["Credit"].ToString());
    //                debit = debit + double.Parse(dr["Debit"].ToString());
    //            }
    //        }

    //        ds.Tables[0].Rows[i]["Debit"] = debit;
    //        ds.Tables[0].Rows[i]["Credit"] = credit;

    //    }


    public DataSet GenerateGridColumns()
    {
        DataSet dstt = new DataSet();

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        dstt = objBL.getexpensetypes();


        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn dc;

        if (dstt.Tables[0].Rows.Count > 0)
        {
            dc = new DataColumn("Date");
            dt.Columns.Add(dc);
            for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
            {
                string ledger = dstt.Tables[0].Rows[i]["LedgerName"].ToString();
                dc = new DataColumn(ledger);
                dt.Columns.Add(dc);
             }
            dc = new DataColumn("Total");
            dt.Columns.Add(dc);
        }

        ds.Tables.Add(dt);
        return ds;

            //dt.Columns.Add(new DataColumn("DATE"));
            //dt.Columns.Add(new DataColumn("AD TV"));
            //dt.Columns.Add(new DataColumn("AD Flex"));
            //dt.Columns.Add(new DataColumn("AD Gift"));
            //dt.Columns.Add(new DataColumn("AD Dhinakaran"));
            //dt.Columns.Add(new DataColumn("AD Dinamalar"));
            //dt.Columns.Add(new DataColumn("AD Thanthi"));
            //dt.Columns.Add(new DataColumn("Bank Charges"));
            //dt.Columns.Add(new DataColumn("Bank Charges HDFC"));
            //dt.Columns.Add(new DataColumn("Bank Interest"));
            //dt.Columns.Add(new DataColumn("BBK"));
            //dt.Columns.Add(new DataColumn("Computer Maintenance"));
            //dt.Columns.Add(new DataColumn("Courier Exp"));
            //dt.Columns.Add(new DataColumn("Demo Salary"));
            //dt.Columns.Add(new DataColumn("Discount"));
            //dt.Columns.Add(new DataColumn("Exgratia Account"));
            //dt.Columns.Add(new DataColumn("Electricity"));
            //dt.Columns.Add(new DataColumn("Freight Inward"));
            //dt.Columns.Add(new DataColumn("Freight Outward"));
            //dt.Columns.Add(new DataColumn("Generator Exp"));
            //dt.Columns.Add(new DataColumn("Hospital"));
            //dt.Columns.Add(new DataColumn("HO Exp"));
            //dt.Columns.Add(new DataColumn("Income Tax Filing Fee"));
            //dt.Columns.Add(new DataColumn("Madurai Talk"));
            //dt.Columns.Add(new DataColumn("MISC-Exp"));
            //dt.Columns.Add(new DataColumn("Membership Fee"));
            //dt.Columns.Add(new DataColumn("Progress Fee"));
            //dt.Columns.Add(new DataColumn("Project"));
            //dt.Columns.Add(new DataColumn("Rent A/C"));
            //dt.Columns.Add(new DataColumn("Salary"));
            //dt.Columns.Add(new DataColumn("Sales Promotions"));
            //dt.Columns.Add(new DataColumn("Security Service"));
            //dt.Columns.Add(new DataColumn("ShowRoom Maintenance"));
            //dt.Columns.Add(new DataColumn("Staff Incentive"));
            //dt.Columns.Add(new DataColumn("Staff Welfare"));
            //dt.Columns.Add(new DataColumn("Stationary"));
            //dt.Columns.Add(new DataColumn("Subscriptios"));
            //dt.Columns.Add(new DataColumn("Telephone Exp"));
            //dt.Columns.Add(new DataColumn("Title"));
            //dt.Columns.Add(new DataColumn("Travelling And Conveyance"));
            //dt.Columns.Add(new DataColumn("Troyplus Exp"));
            //dt.Columns.Add(new DataColumn("VAT A/C"));
            //dt.Columns.Add(new DataColumn("Vehicle Maintanance"));
            //dt.Columns.Add(new DataColumn("Xerox And Printing"));
            //dt.Columns.Add(new DataColumn("Total"));
        
      
    }

    public DataSet UpdateColumnsData(DataSet dsGrid, DataSet debitData)
    {
        Double credit = 0.00;

        if (debitData != null)
        {

            if (true)
            {
                DataTable dt = debitData.Tables[0];

                DataView dv = dt.AsDataView();



                dt = dv.ToTable();
                int colIndex = 1;

                foreach (DataRow dr in dt.Rows)
                {
                    bool dupFlag = false;
                    string customer = dr["LedgerName"].ToString();
                    DateTime transDate = DateTime.Parse(dr["TransDate"].ToString());
                    int rowIndex = 0;

                    if (dupFlag)
                    {
                      
                        double currAmount = 0.0;

                        if (dsGrid.Tables[0].Rows[rowIndex][colIndex] != null)
                        {
                            if (dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString() != "")
                                currAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString());
                        }

                        double totAmount = 0.0;

                        totAmount = currAmount + double.Parse(dr["Amount"].ToString());

                        dsGrid.Tables[0].Rows[rowIndex][colIndex] = totAmount.ToString("#0");
                        dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                        dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                    }
                    else
                    {
                        DataRow gridRow = dsGrid.Tables[0].NewRow();

                        string ledgernam = dr["LedgerName"].ToString().ToUpper().Trim();
                        for (int ii = 1; ii < dsGrid.Tables[0].Columns.Count; ii++)
                        {
                            string ledgerna = dsGrid.Tables[0].Columns[ii].ToString();
                            if (ledgernam == ledgerna)
                            {
                                gridRow[colIndex] = double.Parse(dr["Amt"].ToString()).ToString("#0");
                                credit = credit + double.Parse(dr["Amt"].ToString());
                            }
                            else
                            {
                                gridRow[colIndex] = ("#0");
                            }
                        }

                        dsGrid.Tables[0].Rows.Add(gridRow);
                    }

                }
            }
        }

        return dsGrid;
    }


    public void bindDataSubTot(string cond)
    {
        DateTime startDate, endDate, Transdt;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        string condtion = "";
        condtion = getCond();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        DataSet dsGird = GenerateGridColumns();
        DataSet dst = new DataSet();
        DataSet dts = new DataSet();
        DataSet dstt = new DataSet();
        DataSet dsGir = new DataSet();
        DataSet dsG = new DataSet();
        dst = objBL.getexpensepayments(condtion, startDate, endDate);
        dsG = UpdateColumnsData(dsGird, dst);

        dts = objBL.getexpensepaymentsDate(condtion, startDate, endDate);
        if (dsG.Tables[0].Rows.Count > 0)
        {
            gvRepor.Visible = true;
            gvRepor.DataSource = dsG;
            gvRepor.DataBind();
        }

        dstt = objBL.getexpensetypes();


        if (dts.Tables[0].Rows.Count > 0)
        {
        DataTable dtt = new DataTable();
        DataColumn dc;
        if (dstt.Tables[0].Rows.Count > 0)
        {
            dc = new DataColumn("Date");
            dtt.Columns.Add(dc);
            for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
            {
                string ledger = dstt.Tables[0].Rows[i]["LedgerName"].ToString();
                dc = new DataColumn(ledger);
                dtt.Columns.Add(dc);
            }
            dc = new DataColumn("Total");
            dtt.Columns.Add(dc);
        }
        dsGir.Tables.Add(dtt);

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        double credit = 0.00;
        double Tottot = 0.00;

        DateTime Transd = Convert.ToDateTime(txtStartDate.Text);

        int gg = 1;

        foreach (DataRow dre in dts.Tables[0].Rows)
        {
            DataRow dr_final12 = dtt.NewRow();
            Transdt = Convert.ToDateTime(dre["Transdate"]);

            credit = 0.00;

            foreach (DataRow dr in dst.Tables[0].Rows)
            {
                Transd = Convert.ToDateTime(dr["Transdate"]);

                if (Transdt == Transd)
                {
                    string aa = dr["TransDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");

                    dr_final12["Date"] = dtaa;
                    
                    string ledgernam = dr["LedgerName"].ToString().ToUpper().Trim();
                    for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                    {
                        string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                        if (ledgernam == ledgerna)
                        {
                            dr_final12[ledgerna] = double.Parse(dr["Amt"].ToString());
                            credit = credit + double.Parse(dr["Amt"].ToString());
                            Tottot = Tottot + double.Parse(dr["Amt"].ToString());
                        }
                        else
                        {
                            if (dr_final12[ledgerna] == null)
                            {
                                dr_final12[ledgerna] = "";
                            }                    
                        }
                    }
                    dr_final12["Total"] = credit;                                    
                }               
            }
            dtt.Rows.Add(dr_final12);
        }

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final88 = dtt.NewRow();
        dr_final88["Date"] = "Total";
        dr_final88["Total"] = Tottot;
        dtt.Rows.Add(dr_final88);

       

            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataSubTot412013(string cond)
    {
        DateTime startDate, endDate, Transdt;
        

        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);

        bool dispLastTotal = false;
        string condtion = "";
        condtion = getCond();

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        DataSet dsGird = GenerateGridColumns();
        DataSet dst = new DataSet();
        DataSet dstt = new DataSet();
        DataSet dsGir = new DataSet();
        DataSet dsG = new DataSet();
        dst = objBL.getexpensepayments(condtion, startDate, endDate);
        dsG = UpdateColumnsData(dsGird, dst);

        if (dsG.Tables[0].Rows.Count > 0)
        {
            gvRepor.Visible = true;
            gvRepor.DataSource = dsG;
            gvRepor.DataBind();
            
        }

        dstt = objBL.getexpensetypes();

        DataTable dtt = new DataTable();
        DataColumn dc;
        if (dstt.Tables[0].Rows.Count > 0)
        {
            dc = new DataColumn("Date");
            dtt.Columns.Add(dc);
            for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
            {
                string ledger = dstt.Tables[0].Rows[i]["LedgerName"].ToString();
                dc = new DataColumn(ledger);
                dtt.Columns.Add(dc);
            }
            dc = new DataColumn("Total");
            dtt.Columns.Add(dc);
        }
        dsGir.Tables.Add(dtt);



        double credit = 0.00;
        DateTime Transd = Convert.ToDateTime(txtStartDate.Text);

        foreach (DataRow dr in dst.Tables[0].Rows)
        {
            Transdt = Convert.ToDateTime(dr["Transdate"]);
            DataRow dr_final12 = dtt.NewRow();

            if (Transdt == Transd)
            {
                string ledgernam = dr["LedgerName"].ToString().ToUpper().Trim();
                for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                {
                    string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                    if (ledgernam == ledgerna)
                    {
                        dr_final12[ledgerna] = double.Parse(dr["Amt"].ToString());
                        credit = credit + double.Parse(dr["Amt"].ToString());
                    }
                    else
                    {
                        dr_final12[ledgerna] = "";
                    }
                }
            }
            else
            {
                dr_final12["Date"] = DateTime.Parse(dr["TransDate"].ToString());
                credit = 0.00;
                string ledgernam = dr["LedgerName"].ToString().ToUpper().Trim();
                for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                {
                    string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                    if (ledgernam == ledgerna)
                    {
                        dr_final12[ledgerna] = double.Parse(dr["Amt"].ToString());
                        credit = credit + double.Parse(dr["Amt"].ToString());
                    }
                    else
                    {
                        dr_final12[ledgerna] = "";
                    }
                }
                dr_final12["Total"] = credit;
                Transd = Convert.ToDateTime(dr["Transdate"]);
                dtt.Rows.Add(dr_final12);
            }

            

            


        
        //for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
        //{
        //    DateTime Transd = Convert.ToDateTime(dst.Tables[0].Rows[i]["Transdate"]);

        //    //DataRow gridRow = dsGird.Tables[0].NewRow();
        //    //gridRow["Date"] = Convert.ToDateTime(dst.Tables[0].Rows[i]["Transdate"]);


        //    DataRow dr_final12 = dtt.NewRow();
        //    dr_final12["Date"] = Convert.ToDateTime(dst.Tables[0].Rows[i]["Transdate"]);

        //    string ledgernam = dst.Tables[0].Rows[i]["LedgerName"].ToString();
        //    for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
        //    {
        //        string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
        //        if (ledgernam == ledgerna)
        //        {
        //            //gridRow[colIndex] = dst.Tables[0].Rows[i]["Amt"].ToString();
        //            dr_final12[ledgernam] = dst.Tables[0].Rows[i]["Amt"].ToString();
        //        }
        //        else
        //        {
        //            dr_final12[ledgernam] = "";
        //        }
        //    }
        // dsGird.Tables[0].Rows.Add(gridRow);

            
        }



        if (dsGir.Tables[0].Rows.Count > 0)
        {
         
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataSubTotH(string cond)
    {
        DateTime startDate, endDate, Transdt;

        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);

        bool dispLastTotal = false;
        string condtion = "";
        condtion = getCond();

        DataSet ds = new DataSet();


        DataTable dt = new DataTable();

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string FLvlSub = "", SLvlSub = "", TLvlSub = "", FourLvlSub = "", FiveLvlSUb = "", sixlvlsub = "", sevenlvlsub = "", eightlvlsub = "";

        DataSet dsGird = GenerateGridColumns();
        DataSet dst = new DataSet();
        DataSet dsG = new DataSet();
        dst = objBL.getexpensepayments(condtion, startDate, endDate);
        //dsG = UpdateColumnsData(dsGird, dst);





        int colIndex = 1;
        for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
        {
            DateTime Transd = Convert.ToDateTime(dst.Tables[0].Rows[i]["Transdate"]);

            DataRow gridRow = dsGird.Tables[0].NewRow();
            gridRow["Date"] = Convert.ToDateTime(dst.Tables[0].Rows[i]["Transdate"]);

            //foreach (DataRow dr in debitData.Tables[0].Rows)
            //{
            //    if (Convert.ToDateTime(dr["Transdate"]) == Transdt)
            //    {



            string ledgernam = dst.Tables[0].Rows[i]["LedgerName"].ToString();
            for (int ii = 1; ii < dsGird.Tables[0].Columns.Count; ii++)
            {
                string ledgerna = dsGird.Tables[0].Columns[ii].ToString();
                if (ledgernam == ledgerna)
                {
                    gridRow[colIndex] = dst.Tables[0].Rows[i]["Amt"].ToString();
                    colIndex = colIndex + 1;
                }
            }

            dsGird.Tables[0].Rows.Add(gridRow);
        }

        //gvRepor.DataSource = dsGird;
        //gvRepor.DataBind();


        //DataTable dts = new DataTable();

        if (dsGird.Tables[0].Rows.Count > 0)
        {
            string fLvlValue = "", sLvlValue = "", tLvlValue = "", frthLvlValue = "", fifLvlValue = "", sixLvlValue = "", svthLvlValue = "", eightLvlValue = "", ninthLvlValue = "", tenthLvlValue = "", eleventhLvlValue = "", twelthLvlValue = "", thirteenLvlValue = "", fourteenLvlValue = "";
            string fLvlValueTemp = "", sLvlValueTemp = "", tLvlValueTemp = "", frthLvlValueTemp = "", fifLvlValueTemp = "", sixLvlValueTemp = "", svthLvlValueTemp = "", eightLvlValueTemp = "", ninthLvlValueTemp = "", tenthLvlValueTemp = "", eleventhLvlValueTemp = "", twelthLvlValueTemp = "", thirteenLvlValueTemp = "", fourteenLvlValueTemp = "";


            DataSet dstt = new DataSet();
            DataColumn dc;
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            dstt = objBL.getexpensetypes();

            int columnNo = dsGird.Tables[0].Columns.Count;
            int colDur = 0, nextDur = 0;

            DataRow dr_final13 = dt.NewRow();
            dt.Columns.Add(new DataColumn("Date"));
            for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
            {
                string ledger = dstt.Tables[0].Rows[i]["LedgerName"].ToString();
                dt.Columns.Add(new DataColumn(ledger));
            }
            dt.Columns.Add(new DataColumn("Total"));
            dt.Rows.Add(dr_final13);


            foreach (DataRow dr in dsGird.Tables[0].Rows)
            {
                DataRow dr_final12 = dt.NewRow();
                dr_final12["Date"] = dr["date"].ToString();

                colDur = 0;
                nextDur = 0;
                dt = new DataTable();

                for (int i = 1; i < columnNo; i++)
                {
                    //nextDur = nextDur + duration;
                    dr_final12[dstt.Tables[0].Rows[i]["LedgerName"].ToString()] = dr[dst.Tables[0].Rows[0][i].ToString()];
                    //colDur = nextDur + 1;
                }

                dt.Rows.Add(dr_final12);

            }
            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }

    public void bindDataSubTotHH(string cond)
    {
        DateTime startDate, endDate, Transdt;

        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);

        bool dispLastTotal = false;
        string condtion = "";
        condtion = getCond();
        
        DataSet ds = new DataSet();
        DataSet dst = new DataSet();

        DataTable dt = new DataTable();

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string FLvlSub = "", SLvlSub = "", TLvlSub = "", FourLvlSub = "", FiveLvlSUb = "", sixlvlsub = "", sevenlvlsub = "", eightlvlsub = "";

        ds = objBL.getexpensetypes();

        //ds = objBL.getexpensepayments(condtion);

        DataRow dr_final17 = dt.NewRow();
        dt.Rows.Add(dr_final17);

        if (ds.Tables[0].Rows.Count > 0)
        {
            
            //DataRow dr_final = dt.NewRow();
            //dt.Columns.Add(new DataColumn(""));
            //dt.Columns.Add(new DataColumn(""));
            //dt.Columns.Add(new DataColumn(""));
            //dt.Columns.Add(new DataColumn(""));
            //dt.Columns.Add(new DataColumn("DAILY EXPENSE"));
            //dt.Rows.Add(dr_final);

            //DataRow dr_final1 = dt.NewRow();
            //dt.Rows.Add(dr_final1);

            DataRow dr_final13 = dt.NewRow();
            dt.Columns.Add(new DataColumn("Date"));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string ledger = ds.Tables[0].Rows[i]["LedgerName"].ToString();
                dt.Columns.Add(new DataColumn(ledger));
            }
            dt.Columns.Add(new DataColumn("Total"));
            dt.Rows.Add(dr_final13);

             //dt.Columns.Add(new DataColumn("DATE"));
             //dt.Columns.Add(new DataColumn("AD TV"));
             //dt.Columns.Add(new DataColumn("AD Flex"));
             //dt.Columns.Add(new DataColumn("AD Gift"));
             //dt.Columns.Add(new DataColumn("AD Dhinakaran"));
             //dt.Columns.Add(new DataColumn("AD Dinamalar"));
             //dt.Columns.Add(new DataColumn("AD Thanthi"));
             //dt.Columns.Add(new DataColumn("Bank Charges"));
             //dt.Columns.Add(new DataColumn("Bank Charges HDFC"));
             //dt.Columns.Add(new DataColumn("Bank Interest"));
             //dt.Columns.Add(new DataColumn("BBK"));
             //dt.Columns.Add(new DataColumn("Computer Maintenance"));
             //dt.Columns.Add(new DataColumn("Courier Exp"));
             //dt.Columns.Add(new DataColumn("Demo Salary"));
             //dt.Columns.Add(new DataColumn("Discount"));
             //dt.Columns.Add(new DataColumn("Exgratia Account"));
             //dt.Columns.Add(new DataColumn("Electricity"));
             //dt.Columns.Add(new DataColumn("Freight Inward"));
             //dt.Columns.Add(new DataColumn("Freight Outward"));
             //dt.Columns.Add(new DataColumn("Generator Exp"));
             //dt.Columns.Add(new DataColumn("Hospital"));
             //dt.Columns.Add(new DataColumn("HO Exp"));
             //dt.Columns.Add(new DataColumn("Income Tax Filing Fee"));
             //dt.Columns.Add(new DataColumn("Madurai Talk"));
             //dt.Columns.Add(new DataColumn("MISC-Exp"));
             //dt.Columns.Add(new DataColumn("Membership Fee"));
             //dt.Columns.Add(new DataColumn("Progress Fee"));
             //dt.Columns.Add(new DataColumn("Project"));
             //dt.Columns.Add(new DataColumn("Rent A/C"));
             //dt.Columns.Add(new DataColumn("Salary"));
             //dt.Columns.Add(new DataColumn("Sales Promotions"));
             //dt.Columns.Add(new DataColumn("Security Service"));
             //dt.Columns.Add(new DataColumn("ShowRoom Maintenance"));
             //dt.Columns.Add(new DataColumn("Staff Incentive"));
             //dt.Columns.Add(new DataColumn("Staff Welfare"));
             //dt.Columns.Add(new DataColumn("Stationary"));
             //dt.Columns.Add(new DataColumn("Subscriptios"));
             //dt.Columns.Add(new DataColumn("Telephone Exp"));
             //dt.Columns.Add(new DataColumn("Title"));
             //dt.Columns.Add(new DataColumn("Travelling And Conveyance"));
             //dt.Columns.Add(new DataColumn("Troyplus Exp"));
             //dt.Columns.Add(new DataColumn("VAT A/C"));
             //dt.Columns.Add(new DataColumn("Vehicle Maintanance"));
             //dt.Columns.Add(new DataColumn("Xerox And Printing"));
             //dt.Columns.Add(new DataColumn("Total"));

            string fLvlValue = "", sLvlValue = "", tLvlValue = "", frthLvlValue = "", fifLvlValue = "", sixLvlValue = "", svthLvlValue = "", eightLvlValue = "", ninthLvlValue = "", tenthLvlValue = "", eleventhLvlValue = "", twelthLvlValue = "", thirteenLvlValue = "", fourteenLvlValue = "";
            string fLvlValueTemp = "", sLvlValueTemp = "", tLvlValueTemp = "", frthLvlValueTemp = "", fifLvlValueTemp = "", sixLvlValueTemp = "", svthLvlValueTemp = "", eightLvlValueTemp = "", ninthLvlValueTemp = "", tenthLvlValueTemp = "", eleventhLvlValueTemp = "", twelthLvlValueTemp = "", thirteenLvlValueTemp = "", fourteenLvlValueTemp = "";

            dst = objBL.getexpensepayments(condtion, startDate, endDate);

            for (int i = 0; i < dst.Tables[0].Rows.Count; i++)
            {
                Transdt = Convert.ToDateTime(dst.Tables[0].Rows[i]["Transdate"]);

                DataRow dr_final5 = dt.NewRow();

                foreach (DataRow dr in dst.Tables[0].Rows)
                {
                    if (Convert.ToDateTime(dr["Transdate"]) == Transdt)
                    {
                        dr_final5["Date"] = dr["Transdate"];

                        for (int ii = 0; ii < ds.Tables[0].Rows.Count; ii++)
                        {
                            string ledgernam = ds.Tables[0].Rows[ii]["LedgerName"].ToString();
                            if (ledgernam == dr["LedgerName"])
                            {
                                dr_final5[ledgernam] = dr["DrAmt"];
                            }

                        }
                    }
                    
                }
                dt.Rows.Add(dr_final5);
            }
            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    //private DataSet GenerateTempVisitTable()
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    DataColumn dc;

    //    dc = new DataColumn("ServiceID", typeof(Int32));
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("SlNo", typeof(Int32));
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("DueDate", typeof(DateTime));
    //    dt.Columns.Add(dc);

    //    ds.Tables.Add(dt);

    //    return ds;

    //}

    //private DataSet GenerateReportDataset()
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    DataColumn dc;

    //    dc = new DataColumn("ServiceID");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("RefNumber");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("Details");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("Customer");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("Frequency");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("StartDate");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("EndDate");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("Amount");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("DueDate", typeof(DateTime));
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("VisitDate");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("VisitDetails");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("AmountReceived");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("PayMode");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("DiffDays");
    //    dt.Columns.Add(dc);

    //    dc = new DataColumn("DiffAmount");
    //    dt.Columns.Add(dc);

    //    ds.Tables.Add(dt);

    //    return ds;

    //}

}
