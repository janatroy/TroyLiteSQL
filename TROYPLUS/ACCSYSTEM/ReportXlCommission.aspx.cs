using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportXlCommission : System.Web.UI.Page
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

                fillCustSupp();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected string getCond()
    {
        string cond = "";

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        if ((ddlCustomer.SelectedItem.Text != "All"))
        {
            objBL.Custids = Convert.ToInt32(ddlCustomer.SelectedItem.Value);
            cond += " and tblCommissionItems.CustomerID=" + Convert.ToInt32(ddlCustomer.SelectedItem.Value) + " ";
        }
        if ((ddlSupplier.SelectedItem.Text != "All"))
        {
            objBL.Suppids = Convert.ToInt32(ddlSupplier.SelectedItem.Value);
            cond += " and SupplierID=" + Convert.ToInt32(ddlSupplier.SelectedItem.Value) + " ";
        }
        return cond;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (chkboxAll.Checked == true)
            {
                bindDatac();
            }
            else
            {
                bindData();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        
    }

    private void fillCustSupp()
    {
        DataSet ds = new DataSet();
        ds = objBL.ListSundryDebtors(sDataSource);

        ddlCustomer.DataSource = ds;
        ddlCustomer.DataTextField = "LedgerName";
        ddlCustomer.DataValueField = "LedgerID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, "All");

        DataSet dst = new DataSet();
        dst = objBL.ListSundryCreditors(sDataSource);

        ddlSupplier.DataSource = dst;
        ddlSupplier.DataTextField = "LedgerName";
        ddlSupplier.DataValueField = "LedgerID";
        ddlSupplier.DataBind();
        ddlSupplier.Items.Insert(0, "All");
    }

    public void bindData()
    {
        DataSet ds = new DataSet();
        DataSet ddd = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        DataSet dst = new DataSet();
        string condtion = "";
        condtion = getCond();

        string method = string.Empty;
        string metho = string.Empty;
        method = ddlSummary.SelectedItem.Text;

        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);

        if ((ddlCustomer.SelectedItem.Text == "All"))
        {
            metho = "All";
        }
        else
        {
            metho = "N";
        }

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        int commno = 0;

        if (method == "No")
        {
            dst = objBL.getcommissiondetails(sDataSource, startDate, endDate, condtion, method, metho);

            if (dst != null)
            {
                if (dst.Tables[0].Rows.Count > 0)
                {
                    DataTable dtt = new DataTable();
                    dtt.Columns.Add(new DataColumn("Commission No"));
                    dtt.Columns.Add(new DataColumn("Date"));
                    //dtt.Columns.Add(new DataColumn("Customer Name"));
                    //dtt.Columns.Add(new DataColumn("Customer Paymode"));
                    dtt.Columns.Add(new DataColumn("Supplier Name"));
                    dtt.Columns.Add(new DataColumn("Supplier Paymode"));
                    dtt.Columns.Add(new DataColumn("Freight"));
                    dtt.Columns.Add(new DataColumn("Freight Paymode"));
                    dtt.Columns.Add(new DataColumn("Load/Unload"));
                    dtt.Columns.Add(new DataColumn("Load/Unload Paymode"));
                    dtt.Columns.Add(new DataColumn("Sales Value"));
                    dtt.Columns.Add(new DataColumn("Purchase Value"));
                    
                    dtt.Columns.Add(new DataColumn("Commission Value"));
                    dtt.Columns.Add(new DataColumn("Remarks"));

                    DataRow dr_final14 = dtt.NewRow();
                    dtt.Rows.Add(dr_final14);

                    foreach (DataRow dr in dst.Tables[0].Rows)
                    {
                        if (commno == Convert.ToInt32(dr["CommissionNo"]))
                        {
                            break;
                        }

                        commno = Convert.ToInt32(dr["CommissionNo"]);
                        
                        DataRow dr_final12 = dtt.NewRow();
                        dr_final12["Commission No"] = dr["CommissionNo"];
                        dr_final12["Date"] = dr["CommDate"];
                        //dr_final12["Customer Name"] = dr["CustomerName"].ToString();
                        //if (dr["SellingPayMode"].ToString() == "1")
                        //{
                        //    dr_final12["Customer Paymode"] = "Cash";
                        //}
                        //else if (dr["SellingPayMode"].ToString() == "2")
                        //{
                        //    dr_final12["Customer Paymode"] = "Bank / Credit Card";
                        //}
                        //else if (dr["SellingPayMode"].ToString() == "3")
                        //{
                        //    dr_final12["Customer Paymode"] = "Credit";
                        //}
                        dr_final12["Supplier Name"] = dr["subname"];
                        if (dr["SupplierPaymode"].ToString() == "1")
                        {
                            dr_final12["Supplier Paymode"] = "Cash";
                        }
                        else if (dr["SupplierPaymode"].ToString() == "2")
                        {
                            dr_final12["Supplier Paymode"] = "Bank / Credit Card";
                        }
                        else if (dr["SupplierPaymode"].ToString() == "3")
                        {
                            dr_final12["Supplier Paymode"] = "Credit";
                        }
                        dr_final12["Freight"] = Convert.ToDouble(dr["Freight"]);
                        if (dr["FreightPaymode"].ToString() == "1")
                        {
                            dr_final12["Freight Paymode"] = "Cash";
                        }
                        else if (dr["FreightPaymode"].ToString() == "2")
                        {
                            dr_final12["Freight Paymode"] = "Bank / Credit Card";
                        }
                        else if (dr["FreightPaymode"].ToString() == "3")
                        {
                            dr_final12["Freight Paymode"] = "Credit";
                        }
                        dr_final12["Load/Unload"] = Convert.ToDouble(dr["LoadUnload"]);
                        if (dr["LoadUnLoadPaymode"].ToString() == "1")
                        {
                            dr_final12["Load/Unload Paymode"] = "Cash";
                        }
                        else if (dr["LoadUnLoadPaymode"].ToString() == "2")
                        {
                            dr_final12["Load/Unload Paymode"] = "Bank / Credit Card";
                        }
                        else if (dr["LoadUnLoadPaymode"].ToString() == "3")
                        {
                            dr_final12["Load/Unload Paymode"] = "Credit";
                        }
                        dr_final12["Sales Value"] = Convert.ToDouble(dr["SalesValue"]);
                        dr_final12["Purchase Value"] = Convert.ToDouble(dr["PurchaseValue"]);
                        
                        dr_final12["Commission Value"] = Convert.ToDouble(dr["ComissionValue"]);
                        dr_final12["Remarks"] = dr["Remarks"].ToString();
                        dtt.Rows.Add(dr_final12);
                    }

                    DataRow dr_final11 = dtt.NewRow();
                    dtt.Rows.Add(dr_final11);

                    DataRow dr_final88 = dtt.NewRow();
                    dr_final88["Commission No"] = "";
                    dr_final88["Date"] = " ";
                    //dr_final88["Customer Name"] = " ";
                    //dr_final88["Customer Paymode"] = " ";
                    dr_final88["Supplier Name"] = " ";
                    dr_final88["Supplier Paymode"] = " ";
                    dr_final88["Freight"] = " ";
                    dr_final88["Freight Paymode"] = " ";
                    dr_final88["Load/Unload"] = " ";
                    dr_final88["Load/Unload Paymode"] = " ";
                    dr_final88["Sales Value"] = "";
                    dr_final88["Purchase Value"] = "";
                    dr_final88["Commission Value"] = " ";
                    dr_final88["Remarks"] = " ";
                    dtt.Rows.Add(dr_final88);

                    ExportToExcel(dtt);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            }
        }
        else if (method == "Yes")
        {
            int sno = 1;

            dst = objBL.getcommissiondetails(sDataSource, startDate, endDate, condtion, method, metho);
            
            if (dst != null)
            {
                if (dst.Tables[0].Rows.Count > 0)
                {
                    DataTable dtt = new DataTable();
                    dtt.Columns.Add(new DataColumn("Commission No"));
                    dtt.Columns.Add(new DataColumn("Date"));
                    dtt.Columns.Add(new DataColumn("Supplier Name"));
                    dtt.Columns.Add(new DataColumn("Supplier Paymode"));
                    dtt.Columns.Add(new DataColumn("Freight"));
                    dtt.Columns.Add(new DataColumn("Freight Paymode"));
                    dtt.Columns.Add(new DataColumn("Load/Unload"));
                    dtt.Columns.Add(new DataColumn("Load/Unload Paymode"));
                    dtt.Columns.Add(new DataColumn("Sales Value"));
                    dtt.Columns.Add(new DataColumn("Purchase Value"));
                    dtt.Columns.Add(new DataColumn("Commission Value"));
                    dtt.Columns.Add(new DataColumn("Remarks"));

                    dtt.Columns.Add(new DataColumn("Customer Name"));
                    dtt.Columns.Add(new DataColumn("Customer Paymode"));
                    dtt.Columns.Add(new DataColumn("Product Name"));
                    dtt.Columns.Add(new DataColumn("Model"));
                    dtt.Columns.Add(new DataColumn("Brand"));
                    dtt.Columns.Add(new DataColumn("Qty"));
                    dtt.Columns.Add(new DataColumn("Rate"));
                    dtt.Columns.Add(new DataColumn("Amount"));
                    
                    DataRow dr_final14 = dtt.NewRow();
                    dtt.Rows.Add(dr_final14);

                    foreach (DataRow dr in dst.Tables[0].Rows)
                    {
                        sno = 1;

                        DataRow dr_final12 = dtt.NewRow();
                        if (commno == Convert.ToInt32(dr["CommissionNo"]))
                        {
                            dr_final12["Commission No"] = "";
                            dr_final12["Date"] = ""; 
                            dr_final12["Supplier Name"] = ""; 
                            dr_final12["Supplier Paymode"] = "";
                            dr_final12["Freight"] = "";
                            dr_final12["Freight Paymode"] = "";
                            dr_final12["Load/Unload"] = "";
                            dr_final12["Load/Unload Paymode"] = "";
                            dr_final12["Sales Value"] = "";
                            dr_final12["Purchase Value"] = "";
                            dr_final12["Commission Value"] = "";
                            dr_final12["Remarks"] = "";
                        }
                        else
                        {
                            dr_final12["Commission No"] = dr["CommissionNo"];
                            dr_final12["Date"] = dr["CommDate"];
                            dr_final12["Supplier Name"] = dr["subname"];
                            if (dr["SupplierPaymode"].ToString() == "1")
                            {
                                dr_final12["Supplier Paymode"] = "Cash";
                            }
                            else if (dr["SupplierPaymode"].ToString() == "2")
                            {
                                dr_final12["Supplier Paymode"] = "Bank / Credit Card";
                            }
                            else if (dr["SupplierPaymode"].ToString() == "3")
                            {
                                dr_final12["Supplier Paymode"] = "Credit";
                            }
                            dr_final12["Freight"] = Convert.ToDouble(dr["Freight"]);
                            if (dr["FreightPaymode"].ToString() == "1")
                            {
                                dr_final12["Freight Paymode"] = "Cash";
                            }
                            else if (dr["FreightPaymode"].ToString() == "2")
                            {
                                dr_final12["Freight Paymode"] = "Bank / Credit Card";
                            }
                            else if (dr["FreightPaymode"].ToString() == "3")
                            {
                                dr_final12["Freight Paymode"] = "Credit";
                            }
                            dr_final12["Load/Unload"] = Convert.ToDouble(dr["LoadUnload"]);
                            if (dr["LoadUnLoadPaymode"].ToString() == "1")
                            {
                                dr_final12["Load/Unload Paymode"] = "Cash";
                            }
                            else if (dr["LoadUnLoadPaymode"].ToString() == "2")
                            {
                                dr_final12["Load/Unload Paymode"] = "Bank / Credit Card";
                            }
                            else if (dr["LoadUnLoadPaymode"].ToString() == "3")
                            {
                                dr_final12["Load/Unload Paymode"] = "Credit";
                            }

                            dr_final12["Sales Value"] = Convert.ToDouble(dr["SalesValue"]);
                            dr_final12["Purchase Value"] = Convert.ToDouble(dr["PurchaseValue"]);
                            dr_final12["Commission Value"] = Convert.ToDouble(dr["ComissionValue"]);
                            dr_final12["Remarks"] = dr["Remarks"].ToString();
                        }
                        

                        

                        if ((ddlCustomer.SelectedItem.Text == "All"))
                        {
                            ddd = objBL.GetCommissionItemsForId(Convert.ToInt32(dr["CommissionNo"]));
                        }
                        else
                        {
                            ddd = objBL.GetCommissionItemsForIdNo(Convert.ToInt32(dr["CommissionNo"]), Convert.ToInt32(ddlCustomer.SelectedItem.Value));
                        }
                        

                        
                        if (ddd.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dd in ddd.Tables[0].Rows)
                            {
                                if (commno == Convert.ToInt32(dr["CommissionNo"]))
                                {
                                    break;
                                }

                                if (sno == 1)
                                {
                                    dr_final12["Customer Name"] = dd["CustomerName"].ToString();
                                    dr_final12["Customer Paymode"] = dd["SellingPayMode"].ToString();
                                    dr_final12["Product Name"] = dd["ProductName"].ToString();
                                    dr_final12["Model"] = dd["Model"].ToString();
                                    dr_final12["Brand"] = dd["productdesc"].ToString();
                                    dr_final12["Qty"] = Convert.ToDouble(dd["Qty"]);
                                    dr_final12["Rate"] = Convert.ToDouble(dd["Rate"]);
                                    dr_final12["Amount"] = Convert.ToDouble(dd["Qty"]) * Convert.ToDouble(dd["Rate"]);
                                    dtt.Rows.Add(dr_final12);
                                }
                                else
                                {
                                    DataRow dr_final122 = dtt.NewRow();
                                    dr_final122["Customer Name"] = dd["CustomerName"].ToString();
                                    dr_final122["Customer Paymode"] = dd["SellingPayMode"].ToString();
                                    dr_final122["Product Name"] = dd["ProductName"].ToString();
                                    dr_final122["Model"] = dd["Model"].ToString();
                                    dr_final122["Brand"] = dd["productdesc"].ToString();
                                    dr_final122["Qty"] = Convert.ToDouble(dd["Qty"]);
                                    dr_final122["Rate"] = Convert.ToDouble(dd["Rate"]);
                                    dr_final122["Amount"] = Convert.ToDouble(dd["Qty"]) * Convert.ToDouble(dd["Rate"]);
                                    dtt.Rows.Add(dr_final122);
                                }
                                sno = 2;
                            }
                            
                        }

                        commno = Convert.ToInt32(dr["CommissionNo"]);

                        DataRow dr_final113 = dtt.NewRow();
                        dtt.Rows.Add(dr_final113);
                    }

                    DataRow dr_final11 = dtt.NewRow();
                    dtt.Rows.Add(dr_final11);

                    DataRow dr_final88 = dtt.NewRow();
                    dr_final88["Commission No"] = "";
                    dr_final88["Date"] = " ";
                    dr_final88["Supplier Name"] = " ";
                    dr_final88["Supplier Paymode"] = " ";
                    dr_final88["Freight"] = " ";
                    dr_final88["Freight Paymode"] = " ";
                    dr_final88["Load/Unload"] = " ";
                    dr_final88["Load/Unload Paymode"] = " ";
                    dr_final88["Sales Value"] = "";
                    dr_final88["Purchase Value"] = "";
                    dr_final88["Commission Value"] = " ";
                    dr_final88["Remarks"] = " ";
                    dtt.Rows.Add(dr_final88);

                    ExportToExcel(dtt);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            }
        }
    }

    public void bindDatac()
    {
        DataSet ds = new DataSet();
        DataSet ddd = new DataSet();
        DataTable dt = new DataTable();
        DateTime startDate, endDate;

        DataSet dst = new DataSet();
        string condtion = "";
        condtion = getCond();

        string method = string.Empty;
        string metho = string.Empty;
        method = ddlSummary.SelectedItem.Text;

        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);

        if ((ddlCustomer.SelectedItem.Text == "All"))
        {
            metho = "All";
        }
        else
        {
            metho = "N";
        }

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        int commno = 0;

        if (method == "No")
        {
            dst = objBL.getcommissiondetails(sDataSource, startDate, endDate, condtion, method, metho);

            if (dst != null)
            {
                if (dst.Tables[0].Rows.Count > 0)
                {
                    DataTable dtt = new DataTable();
                    dtt.Columns.Add(new DataColumn("Commission No"));
                    dtt.Columns.Add(new DataColumn("Date"));
                    

                    DataRow dr_final14 = dtt.NewRow();
                    dtt.Rows.Add(dr_final14);

                    foreach (DataRow dr in dst.Tables[0].Rows)
                    {
                        if (commno == Convert.ToInt32(dr["CommissionNo"]))
                        {
                            break;
                        }

                        commno = Convert.ToInt32(dr["CommissionNo"]);

                        DataRow dr_final12 = dtt.NewRow();
                        dr_final12["Commission No"] = dr["CommissionNo"];
                        dr_final12["Date"] = dr["CommDate"];
                       
                        dtt.Rows.Add(dr_final12);
                    }

                    DataRow dr_final11 = dtt.NewRow();
                    dtt.Rows.Add(dr_final11);

                    DataRow dr_final88 = dtt.NewRow();
                    dr_final88["Commission No"] = "";
                    dr_final88["Date"] = " ";
                   
                    dtt.Rows.Add(dr_final88);

                    ExportToExcel(dtt);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            }
        }
        else if (method == "Yes")
        {
            dst = objBL.getcommissiondetails(sDataSource, startDate, endDate, condtion, method, metho);

            if (dst != null)
            {
                if (dst.Tables[0].Rows.Count > 0)
                {
                    DataTable dtt = new DataTable();
                    dtt.Columns.Add(new DataColumn("Commission No"));
                    dtt.Columns.Add(new DataColumn("Date"));
                    dtt.Columns.Add(new DataColumn("Customer Name"));
                    dtt.Columns.Add(new DataColumn("Customer Paymode"));
                    dtt.Columns.Add(new DataColumn("Product Name"));
                    dtt.Columns.Add(new DataColumn("Model"));
                    dtt.Columns.Add(new DataColumn("Brand"));
                    dtt.Columns.Add(new DataColumn("Qty"));
                    dtt.Columns.Add(new DataColumn("Rate"));
                    dtt.Columns.Add(new DataColumn("Amount"));
                    
                    DataRow dr_final14 = dtt.NewRow();
                    dtt.Rows.Add(dr_final14);

                    foreach (DataRow dr in dst.Tables[0].Rows)
                    {
                        if (commno == Convert.ToInt32(dr["CommissionNo"]))
                        {
                            break;
                        }

                        commno = Convert.ToInt32(dr["CommissionNo"]);

                        DataRow dr_final12 = dtt.NewRow();
                        dr_final12["Commission No"] = dr["CommissionNo"];
                        dr_final12["Date"] = dr["CommDate"];
                        dr_final12["Customer Name"] = "";
                        dr_final12["Customer Paymode"] = "";
                        dr_final12["Product Name"] = "";
                        dr_final12["Model"] = "";
                        dr_final12["Brand"] = "";
                        dr_final12["Qty"] = "";
                        dr_final12["Rate"] = "";
                        dr_final12["Amount"] = "";
                        
                        dtt.Rows.Add(dr_final12);

                        if ((ddlCustomer.SelectedItem.Text == "All"))
                        {
                            ddd = objBL.GetCommissionItemsForId(Convert.ToInt32(dr["CommissionNo"]));
                        }
                        else
                        {
                            ddd = objBL.GetCommissionItemsForIdNo(Convert.ToInt32(dr["CommissionNo"]), Convert.ToInt32(ddlCustomer.SelectedItem.Value));
                        }

                        //DataRow dr_final144 = dtt.NewRow();
                        //dtt.Rows.Add(dr_final144);

                        if (ddd.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dd in ddd.Tables[0].Rows)
                            {
                                DataRow dr_final122 = dtt.NewRow();
                                dr_final122["Commission No"] = "";
                                dr_final122["Date"] = "";
                                dr_final122["Customer Name"] = dd["CustomerName"].ToString();
                                dr_final122["Customer Paymode"] = dd["SellingPayMode"].ToString();
                                dr_final122["Product Name"] = dd["ProductName"].ToString();
                                dr_final122["Model"] = dd["Model"].ToString();
                                dr_final122["Brand"] = dd["productdesc"].ToString();
                                dr_final122["Qty"] = Convert.ToDouble(dd["Qty"]);
                                dr_final122["Rate"] = Convert.ToDouble(dd["Rate"]);
                                dr_final122["Amount"] = Convert.ToDouble(dd["Qty"]) * Convert.ToDouble(dd["Rate"]);

                                dtt.Rows.Add(dr_final122);
                            }
                        }

                        DataRow dr_final113 = dtt.NewRow();
                        dtt.Rows.Add(dr_final113);
                    }

                    DataRow dr_final11 = dtt.NewRow();
                    dtt.Rows.Add(dr_final11);

                    DataRow dr_final88 = dtt.NewRow();
                    dr_final88["Commission No"] = "";
                    dr_final88["Date"] = " ";
                    dr_final88["Customer Name"] = "";
                    dr_final88["Customer Paymode"] = "";
                    dr_final88["Product Name"] = "";
                    dr_final88["Model"] = "";
                    dr_final88["Brand"] = "";
                    dr_final88["Qty"] = "";
                    dr_final88["Rate"] = "";
                    dr_final88["Amount"] = "";
                    dtt.Rows.Add(dr_final88);

                    ExportToExcel(dtt);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            }
        }
    }


    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            string filename = "Commission Report.xls";
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


    public void bindDataSubTotCat(string cond)
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
        dst = objBL.gettotalcatsales(startDate, endDate);

        dts = objBL.gettotalcatsalesdate(startDate, endDate);


        if (dts.Tables[0].Rows.Count > 0)
            {

        dstt = objBL.ListAllCategory();
        DataTable dtt = new DataTable();
        DataColumn dc;
        if (dstt.Tables[0].Rows.Count > 0)
        {
            dc = new DataColumn("Date");
            dtt.Columns.Add(dc);
            for (int i = 0; i < dstt.Tables[0].Rows.Count; i++)
            {
                string ledger = dstt.Tables[0].Rows[i]["Categoryname"].ToString();
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

                    string ledgernam = dr["Categoryname"].ToString().ToUpper().Trim();
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
        DataSet dsGird = GenerateGridColumns();
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
        dst = objBL.gettotalproductsales(startDate, endDate);

        dts = objBL.gettotalproductsalesdate(startDate, endDate);

        dstt = objBL.ListAllProductName();

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
                        string ledger = dstt.Tables[0].Rows[i]["Productname"].ToString();
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

                            string ledgernam = dr["Productname"].ToString().ToUpper().Trim();
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
