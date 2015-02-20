using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportXlSalesPur : System.Web.UI.Page
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
            //if (optionrate.SelectedItem.Text == "Item Wise")
            //{
            bindData();
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    
    public void bindData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DateTime startDate, endDate;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);

        string pret = "NO";
        string itrans = "NO";
        string denot = "NO";

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        ds = objBL.GetTotalsalesdate(startDate, endDate, pret, itrans, denot);

        dt.Columns.Add(new DataColumn("Date"));
        dt.Columns.Add(new DataColumn("ProductName"));
        dt.Columns.Add(new DataColumn("Brand"));
        dt.Columns.Add(new DataColumn("Model"));
        dt.Columns.Add(new DataColumn("Sales"));
        dt.Columns.Add(new DataColumn("Purchase"));

        DataRow dr_export1 = dt.NewRow();
        dt.Rows.Add(dr_export1);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataRow dr_export = dt.NewRow();

                string aa = dr["BillDate"].ToString().ToUpper().Trim();
                string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                dr_export["Date"] = dtaa;

                dr_export["ProductName"] = dr["ProductName"];
                dr_export["Brand"] =  dr["Productdesc"];
                dr_export["Model"] = dr["Model"];
                dr_export["Sales"] = dr["Qty"];
                dr_export["Purchase"] = dr["Qty"];

                dt.Rows.Add(dr_export);
            }

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




    public void bindDataSubTotBrand(string cond)
    {
        DateTime startDate, endDate, Transdt;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        string condtion = "";
        condtion = getCond();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
        //DataSet dsGird = GenerateGridColumns();
        DataSet dst = new DataSet();
        DataSet dts = new DataSet();
        DataSet dstt = new DataSet();
        DataSet dsGir = new DataSet();
        DataSet dsG = new DataSet();
        dst = objBL.gettotalbrandsales(startDate, endDate);

        dts = objBL.gettotalbrandsalesdate(startDate, endDate);

        dstt = objBL.ListAllBrands();

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
                string ledger = dstt.Tables[0].Rows[i]["ProductDesc"].ToString();
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


        foreach (DataRow dre in dts.Tables[0].Rows)
        {
            DataRow dr_final12 = dtt.NewRow();
            Transdt = Convert.ToDateTime(dre["BillDate"]);

            credit = 0.00;

            foreach (DataRow dr in dst.Tables[0].Rows)
            {
                Transd = Convert.ToDateTime(dr["BillDate"]);
                if (Transdt == Transd)
                {
                    string aa = dr["BillDate"].ToString().ToUpper().Trim();
                    string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                    dr_final12["Date"] = dtaa;

                    string ledgernam = dr["ProductDesc"].ToString().ToUpper().Trim();
                    for (int ii = 1; ii < dsGir.Tables[0].Columns.Count; ii++)
                    {
                        string ledgerna = dsGir.Tables[0].Columns[ii].ToString();
                        if (ledgernam == ledgerna)
                        {
                            dr_final12[ledgerna] = double.Parse(dr["Quantity"].ToString());
                            credit = credit + double.Parse(dr["Quantity"].ToString());
                            Tottot = Tottot + double.Parse(dr["Quantity"].ToString());
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

}
