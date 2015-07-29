using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportExlStock : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    BusinessLogic objBL;
    string connection;
    string brncode;
    string usernam;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            connection = Request.Cookies["Company"].Value;
            if (!IsPostBack)
            {

                //if (Request.Cookies["Company"] != null)
                //    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();
                //BusinessLogic bl = new BusinessLogic(sDataSource);

                objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

                txtStartDate.Text = DateTime.Now.ToShortDateString();

                loadBranch();
                BranchEnable_Disable();
                loadPriceList();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        lstBranch.Items.Clear();

        brncode = Request.Cookies["Branch"].Value;
        if (brncode == "All")
        {
            ds = bl.ListBranch();
           // lstBranch.Items.Add(new ListItem("All", "0"));
        }
        else
        {
            ds = bl.ListDefaultBranch(brncode);
        }
        lstBranch.DataSource = ds;
        lstBranch.DataTextField = "BranchName";
        lstBranch.DataValueField = "Branchcode";
        lstBranch.DataBind();
    }

    private void BranchEnable_Disable()
    {
        string sCustomer = string.Empty;
        connection = Request.Cookies["Company"].Value;
        usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);

        sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["DefaultBranchCode"]);
        lstBranch.ClearSelection();
        ListItem li = lstBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        if (li != null) li.Selected = true;

    }

    private void loadPriceList()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        lstPricelist.Items.Clear();
       // lstPricelist.Items.Add(new ListItem("All", "0"));
        ds = bl.ListPriceList(connection);
        lstPricelist.DataSource = ds;
        lstPricelist.DataTextField = "PriceName";
        lstPricelist.DataValueField = "PriceName";
        lstPricelist.DataBind();
    }

    protected string getCond1()
    {
        string cond = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond += "BranchCode='" + listItem.Value + "' ,";
                }
            }
        }
        cond = cond.TrimEnd(',');
        cond = cond.Replace(",", " or ");
        return cond;
    }

    protected string getCond3()
    {
        string cond3 = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond3 += "S.BranchCode='" + listItem.Value + "' ,";
                }
            }
        }
        cond3 = cond3.TrimEnd(',');
        cond3 = cond3.Replace(",", " or ");
        return cond3;
    }


    protected string getCond4()
    {
        string cond4 = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond4 += "" + listItem.Value + " ,";
                }
            }
        }
        cond4 = cond4.TrimEnd(',');
        //cond = cond.Replace(",", " or ");
        return cond4;
    }

    protected string getCond5()
    {
        string cond5 = "";

        foreach (ListItem listItem in lstPricelist.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond5 += "" + listItem.Value + " ,";
                }
            }
        }
        cond5 = cond5.TrimEnd(',');
        //cond = cond.Replace(",", " or ");
        return cond5;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            //DateTime Date;
            string cond = "";
            cond = getCond1();

            string cond3 = "";
            cond3 = getCond3();

            string cond4 = "";
            cond4 = getCond4();

            string cond5 = "";
            cond5 = getCond5();
            //objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
            //DataSet ds = new DataSet();
            //DataTable dtt = new DataTable();

            //Date = Convert.ToDateTime(txtStartDate.Text);

            //ds = objBL.gethistoryallrates(sDataSource, Date);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    dtt = ds.Tables[0];
            //    ExportToExcel(dtt);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            //}


            if (opnradio.SelectedItem.Text == "Normal")
            {
                if ((chkoption.SelectedItem.Text == "Category Wise"))
                {
                    bindDataCatwise(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Brand Wise"))
                {
                    bindDataBrandwise(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Product Wise"))
                {
                    bindDataproductwise(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Brand / Product Wise"))
                {
                    bindDatabrandprodwise(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Brand / Product / Model Wise"))
                {
                    bindDatabrandprodmodelwise(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Category / Brand / Product Wise"))
                {
                    bindDataCategorybrandprodwise(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Category / Brand Wise"))
                {
                    bindDatacatbrandwise(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Category / Product Wise"))
                {
                    bindDatacatprodwise(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Model Wise"))
                {
                    bindDataprodmodelwise(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Brand / Model Wise"))
                {
                    bindDatabrandmodelwise(cond, cond3, cond5);
                }
            }
            else if (opnradio.SelectedItem.Text == "GroupBy")
            {
                if ((chkoption.SelectedItem.Text == "Category Wise"))
                {
                    bindDataCatwiseGroupBy(cond, cond3, cond4, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Brand Wise"))
                {
                    bindDataBrandwiseGroupBy(cond, cond3,cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Product Wise"))
                {
                    bindDataproductwiseGroupBy(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Brand / Product Wise"))
                {
                    bindDatabrandprodwiseGroupBy(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Brand / Model Wise"))
                {
                    bindDatabrandprodmodelwiseGroupBy(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Category / Brand / Product Wise"))
                {
                    bindDataCategorybrandprodwiseGroupBy(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Category / Brand Wise"))
                {
                    bindDatacatbrandwiseGroupBy(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Category / Product Wise"))
                {
                    bindDatacatprodwiseGroupBy(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Model Wise"))
                {
                    bindDataprodmodelwiseGroupBy(cond, cond3, cond5);
                }
                else if ((chkoption.SelectedItem.Text == "Brand / Model Wise"))
                {
                    bindDatabrandmodelwiseGroupBy(cond, cond3, cond5);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void bindDataBrandwise(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;

        string field1 = "";
        string field2 = "";
        //field1 = getfield();
        field2 = getfield2();
        double total = 0;
        double stktotal = 0;
        double stktotal1 = 0;
        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "Brandwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("ItemCode"));
        dtt.Columns.Add(new DataColumn("Model"));
        dtt.Columns.Add(new DataColumn("BranchCode"));
        dtt.Columns.Add(new DataColumn("pricename"));
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        //ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();

                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "Total : " + fLvlValue;
                    dr_final8["Category"] = "";
                    dr_final8["Product"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";
                    dr_final8["BranchCode"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;


                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Category"] = dr["Categoryname"];
                dr_final88["Product"] = dr["Productname"];
                dr_final88["ItemCode"] = dr["ItemCode"];
                dr_final88["Model"] = dr["Model"];
                dr_final88["BranchCode"] = dr["BranchCode"];
                dr_final88["pricename"] = dr["pricename"];
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                dtt.Rows.Add(dr_final88);
            }
        }
        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Brand"] = "Total : " + fLvlValue;
        dr_final87["Category"] = "";
        dr_final87["Product"] = "";
        dr_final87["ItemCode"] = "";
        dr_final87["Model"] = "";
        dr_final87["BranchCode"] = "";
        dr_final87["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Brand"] = "Grand Total";
        dr_final123["Category"] = "";
        dr_final123["Product"] = "";
        dr_final123["ItemCode"] = "";
        dr_final123["Model"] = "";
        dr_final123["BranchCode"] = "";
        dr_final123["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataBrandwiseGroupBy(string branchcode, string saBranchcode,string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;

        string field1 = "";
        string field2 = "";
        //field1 = getfield();
        field2 = getfield2();
        double total = 0;
        double stktotal = 0;
        double stktotal1 = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "Brandwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode, pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Brand"));
 
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        //ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();

                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = fLvlValue;

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = stktotal;

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal = 0;

                    dtt.Rows.Add(dr_final8);

                  
                }
                fLvlValue = fLvlValueTemp;


                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Brand"] = dr["Brand"];
                
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);

                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                
            }
        }

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Brand"] = fLvlValue;
  

        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = stktotal;

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Brand"] = "Grand Total";
 
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = stktotal1;

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataproductwise(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double producttotal = 0;

        string field1 = "";
        string field2 = "";
        //field1 = getfield();
        field2 = getfield2();
        double total = 0;
        double stktotal = 0;
        double stktotal1 = 0;
        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "productwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("ItemCode"));
        dtt.Columns.Add(new DataColumn("Model"));
        dtt.Columns.Add(new DataColumn("pricename"));
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["productname"].ToString().ToUpper().Trim();

                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Product"] = "Total : " + fLvlValue;
                    dr_final8["Category"] = "";
                    dr_final8["Brand"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttotal));

                    producttotal = 0;
                    stktotal = 0;

                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Product"] = dr["Productname"];
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Category"] = dr["Categoryname"];
                dr_final88["ItemCode"] = dr["ItemCode"];
                dr_final88["Model"] = dr["Model"];
                dr_final88["pricename"] = dr["pricename"];
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                producttotal = producttotal + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                dtt.Rows.Add(dr_final88);
            }
        }
        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Product"] = "Total : " + fLvlValue;
        dr_final87["Brand"] = "";
        dr_final87["Category"] = "";
        dr_final87["ItemCode"] = "";
        dr_final87["Model"] = "";
        dr_final87["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(producttotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Product"] = "Grand Total";
        dr_final123["Category"] = "";
        dr_final123["Brand"] = "";
        dr_final123["ItemCode"] = "";
        dr_final123["Model"] = "";
        dr_final123["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataproductwiseGroupBy(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double producttotal = 0;

        string field1 = "";
        string field2 = "";
        //field1 = getfield();
        field2 = getfield2();
        double total = 0;
        double stktotal = 0;
        double stktotal1 = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "productwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Product"));

        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["productname"].ToString().ToUpper().Trim();

                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Product"] = fLvlValue;
 
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttotal));

                    producttotal = 0;
                    stktotal = 0;
                    dtt.Rows.Add(dr_final8);

                    
                }
                fLvlValue = fLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Product"] = dr["Productname"];

                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                stktotal = stktotal + Convert.ToDouble(dr["stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["stock"]);
                producttotal = producttotal + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
            }
        }
        

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Product"] = fLvlValue;

        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal));

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(producttotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Product"] = "Grand Total";

        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal1));

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDatacatbrandwise(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;

        string field1 = "";
        string field2 = "";
        field2 = getfield2();
        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;
        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "catbrandwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("ItemCode"));
        dtt.Columns.Add(new DataColumn("Model"));
        dtt.Columns.Add(new DataColumn("BranchCode"));
        dtt.Columns.Add(new DataColumn("pricename"));
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        //ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final889 = dtt.NewRow();
                    dtt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Category"] = "";
                    dr_final8["Brand"] = "Total : " + sLvlValue;
                    dr_final8["Product"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";
                    dr_final8["BranchCode"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal = 0;

                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Category"] = "Total : " + fLvlValue;
                    dr_final8["Brand"] = "";
                    dr_final8["Product"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";
                    dr_final8["BranchCode"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal1);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal1 = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Category"] = dr["Categoryname"];
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Product"] = dr["ProductName"];
                dr_final88["ItemCode"] = dr["ItemCode"];
                dr_final88["Model"] = dr["Model"];
                dr_final88["BranchCode"] = dr["BranchCode"];
                dr_final88["pricename"] = dr["pricename"];
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                dtt.Rows.Add(dr_final88);
            }
        }

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Category"] = "";
        dr_final8889["Brand"] = "Total : " + sLvlValue;
        dr_final8889["Product"] = "";
        dr_final8889["ItemCode"] = "";
        dr_final8889["Model"] = "";
        dr_final8889["BranchCode"] = "";
        dr_final8889["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Category"] = "Total : " + fLvlValue;
        dr_final87["Brand"] = "";
        dr_final87["ItemCode"] = "";
        dr_final87["Model"] = "";
        dr_final87["BranchCode"] = "";
        dr_final87["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Category"] = "Grand Total";
        dr_final123["Brand"] = "";
        dr_final123["Product"] = "";
        dr_final123["ItemCode"] = "";
        dr_final123["Model"] = "";
        dr_final123["BranchCode"] = "";
        dr_final123["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal2);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDatacatbrandwiseGroupBy(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;

        string field1 = "";
        string field2 = "";
        field2 = getfield2();
        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;
        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "catbrandwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("Brand"));
        
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        //ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final889 = dtt.NewRow();
                    dtt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Category"] = fLvlValue;
                    dr_final8["Brand"] = sLvlValue;
                    
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal = 0;

                    dtt.Rows.Add(dr_final8);

                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {

                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Category"] = "Total : " + fLvlValue;
                    dr_final8["Brand"] = "";
                   
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal1);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal1 = 0;
                    dtt.Rows.Add(dr_final8);

                  
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Category"] = dr["Categoryname"];
                dr_final88["Brand"] = dr["Brand"];
                
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
            }
        }

       

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Category"] = fLvlValue;
        dr_final8889["Brand"] = sLvlValue;
      
        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Category"] = "Total : " + fLvlValue;
        

        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Category"] = "Grand Total";
        dr_final123["Brand"] = "";
       

        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal2);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    public void bindDatacatprodwise(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;

        string field1 = "";
        string field2 = "";
        field2 = getfield2();
        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;
        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "catproductwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("ItemCode"));
        dtt.Columns.Add(new DataColumn("Model"));

        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Categoryname"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final889 = dtt.NewRow();
                    dtt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Category"] = "";
                    dr_final8["Product"] = "Total : " + sLvlValue;
                    dr_final8["Brand"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Category"] = "Total : " + fLvlValue;
                    dr_final8["Product"] = "";
                    dr_final8["Brand"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal1);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal1 = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Category"] = dr["CategoryName"];
                dr_final88["Product"] = dr["Productname"];
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["ItemCode"] = dr["ItemCode"];
                dr_final88["Model"] = dr["Model"];

                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                dtt.Rows.Add(dr_final88);
            }
        }

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Category"] = "";
        dr_final8889["Product"] = "Total : " + sLvlValue;
        dr_final8889["Brand"] = "";
        dr_final8889["ItemCode"] = "";
        dr_final8889["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Category"] = "Total : " + fLvlValue;
        dr_final87["Brand"] = "";
        dr_final87["Product"] = "";
        dr_final87["ItemCode"] = "";
        dr_final87["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Category"] = "Grand Total";
        dr_final123["Brand"] = "";
        dr_final123["Product"] = "";
        dr_final123["ItemCode"] = "";
        dr_final123["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal2);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDatacatprodwiseGroupBy(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;

        string field1 = "";
        string field2 = "";
        field2 = getfield2();
        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;
        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "catproductwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("Product"));
        
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Categoryname"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Category"] = fLvlValue;
                    dr_final8["Product"] = sLvlValue;
                    

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal = 0;
                    dtt.Rows.Add(dr_final8);

                   
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final889 = dtt.NewRow();
                    dtt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Category"] = "Total : " + fLvlValue;
                    dr_final8["Product"] = "";
                    
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal1);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal1 = 0;
                    dtt.Rows.Add(dr_final8);

                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Category"] = dr["CategoryName"];
                dr_final88["Product"] = dr["Productname"];
                
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["MRPValue"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);

            }
        }

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Category"] = fLvlValue;
        dr_final8889["Product"] = sLvlValue;
        
        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Category"] = "Total : " + fLvlValue;        
        dr_final87["Product"] = "";
        
        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Category"] = "Grand Total";
        dr_final123["Product"] = "";
       
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal2);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    public void bindDatabrandprodwise(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;
        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;
        string field1 = "";
        string field2 = "";
        field2 = getfield2();
        
        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "brandprodwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("Category"));       
        dtt.Columns.Add(new DataColumn("ItemCode"));
        dtt.Columns.Add(new DataColumn("Model"));
        dtt.Columns.Add(new DataColumn("pricename"));
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final889 = dtt.NewRow();
                    dtt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "";
                    dr_final8["Product"] = "Total : " + sLvlValue;
                    dr_final8["Category"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal = 0;

                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "Total : " + fLvlValue;
                    dr_final8["Category"] = "";
                    dr_final8["Product"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal1);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal1 = 0;

                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Product"] = dr["Productname"];
                dr_final88["Category"] = dr["Categoryname"];
                dr_final88["ItemCode"] = dr["ItemCode"];
                dr_final88["Model"] = dr["Model"];
                dr_final88["pricename"] = dr["pricename"];
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                dtt.Rows.Add(dr_final88);
            }
        }

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Brand"] = "";
        dr_final8889["Product"] = "Total : " + sLvlValue;
        dr_final8889["Category"] = "";
        dr_final8889["ItemCode"] = "";
        dr_final8889["Model"] = "";
        dr_final8889["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Brand"] = "Total : " + fLvlValue;
        dr_final87["Category"] = "";
        dr_final87["Product"] = "";
        dr_final87["ItemCode"] = "";
        dr_final87["Model"] = "";
        dr_final87["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Brand"] = "Grand Total";
        dr_final123["Category"] = "";
        dr_final123["Product"] = "";
        dr_final123["ItemCode"] = "";
        dr_final123["Model"] = "";
        dr_final123["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal2);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDatabrandprodwiseGroupBy(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;
        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;
        string field1 = "";
        string field2 = "";
        field2 = getfield2();

        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "brandprodwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Product"));
        
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                  

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = fLvlValue;
                    dr_final8["Product"] = sLvlValue;
                    
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal = 0;

                    dtt.Rows.Add(dr_final8);

                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final888 = dt.NewRow();
                    dt.Rows.Add(dr_final888);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "Total : " + fLvlValue;
                    dr_final8["Category"] = "";
                    
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal1);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal1 = 0;

                    dtt.Rows.Add(dr_final8);

                    
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Product"] = dr["Productname"];
               
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                
            }
        }

        

        
        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Brand"] = fLvlValue;
        dr_final8889["Product"] = sLvlValue;
        
        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);


        DataRow dr_final88877 = dt.NewRow();
        dt.Rows.Add(dr_final88877);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Brand"] = "Total : " + fLvlValue;
        dr_final87["Product"] = "";
        
        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Brand"] = "Grand Total";
        dr_final123["Product"] = "";
        
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal2);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    public void bindDatabrandmodelwise(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;
        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;
        string field1 = "";
        string field2 = "";
        field2 = getfield2();

        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "brandmodelwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Model"));
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("ItemCode"));
        dtt.Columns.Add(new DataColumn("BranchCode"));
        dtt.Columns.Add(new DataColumn("pricename")); 
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        //ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final889 = dtt.NewRow();
                    dtt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "";
                    dr_final8["Product"] = "";
                    dr_final8["Category"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "Total : " + sLvlValue;
                    dr_final8["BranchCode"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal = 0;

                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "Total : " + fLvlValue;
                    dr_final8["Category"] = "";
                    dr_final8["Product"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";
                    dr_final8["BranchCode"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal1);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal1 = 0;

                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Product"] = dr["Productname"];
                dr_final88["Category"] = dr["Categoryname"];
                dr_final88["ItemCode"] = dr["ItemCode"];
                dr_final88["Model"] = dr["Model"];
                dr_final88["BranchCode"] = dr["BranchCode"];
                dr_final88["pricename"] = dr["pricename"];
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                dtt.Rows.Add(dr_final88);
            }
        }

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Brand"] = "";
        dr_final8889["Product"] = "";
        dr_final8889["Category"] = "";
        dr_final8889["ItemCode"] = "";
        dr_final8889["Model"] = "Total : " + sLvlValue;
        dr_final8889["BranchCode"] = "";
        dr_final8889["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Brand"] = "Total : " + fLvlValue;
        dr_final87["Category"] = "";
        dr_final87["Product"] = "";
        dr_final87["ItemCode"] = "";
        dr_final87["Model"] = "";
        dr_final87["BranchCode"] = "";
        dr_final87["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Brand"] = "Grand Total";
        dr_final123["Category"] = "";
        dr_final123["Product"] = "";
        dr_final123["ItemCode"] = "";
        dr_final123["Model"] = "";
        dr_final123["BranchCode"] = "";
        dr_final123["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal2);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDatabrandmodelwiseGroupBy(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;
        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;
        string field1 = "";
        string field2 = "";
        field2 = getfield2();

        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "brandmodelwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Model"));
        
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = fLvlValue;
                    
                    dr_final8["Model"] = sLvlValue;

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal = 0;

                    dtt.Rows.Add(dr_final8);

                    
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "Total : " + fLvlValue;
                    
                    dr_final8["Model"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal1);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal1 = 0;

                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Model"] = dr["Model"];

                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                dtt.Rows.Add(dr_final88);
            }
        }

        

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Brand"] = fLvlValue;
        dr_final8889["Model"] = sLvlValue;

        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Brand"] = "Total : " + fLvlValue;
        
        dr_final87["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Brand"] = "Grand Total";
        
        dr_final123["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal2);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    public void bindDataprodmodelwise(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;
        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;
        string field1 = "";
        string field2 = "";
        field2 = getfield2();

        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "prodmodelwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("Model"));
        dtt.Columns.Add(new DataColumn("Brand"));
        
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("ItemCode"));
        dtt.Columns.Add(new DataColumn("BranchCode"));
        dtt.Columns.Add(new DataColumn("pricename"));
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        //ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Productname"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["model"].ToString().ToUpper().Trim();


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final889 = dtt.NewRow();
                    dtt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "";
                    dr_final8["Model"] = "Total : " + sLvlValue;
                    dr_final8["Category"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Product"] = "";
                    dr_final8["BranchCode"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal = 0;

                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Product"] = "Total : " + fLvlValue;
                    dr_final8["Category"] = "";
                    dr_final8["Brand"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";
                    dr_final8["BranchCode"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal1);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal1 = 0;

                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Product"] = dr["Productname"];
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Model"] = dr["Model"];
                dr_final88["Category"] = dr["Categoryname"];
                dr_final88["ItemCode"] = dr["ItemCode"];
                dr_final88["BranchCode"] = dr["BranchCode"];
                dr_final88["pricename"] = dr["pricename"];
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                dtt.Rows.Add(dr_final88);
            }
        }

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Brand"] = "";
        dr_final8889["Model"] = "Total : " + sLvlValue;
        dr_final8889["Category"] = "";
        dr_final8889["ItemCode"] = "";
        dr_final8889["Product"] = "";
        dr_final8889["BranchCode"] = "";
        dr_final8889["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Product"] = "Total : " + fLvlValue;
        dr_final87["Category"] = "";
        dr_final87["Brand"] = "";
        dr_final87["ItemCode"] = "";
        dr_final87["Model"] = "";
        dr_final87["BranchCode"] = "";
        dr_final87["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Product"] = "Grand Total";
        dr_final123["Category"] = "";
        dr_final123["Brand"] = "";
        dr_final123["ItemCode"] = "";
        dr_final123["Model"] = "";
        dr_final123["BranchCode"] = "";
        dr_final123["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal2);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataprodmodelwiseGroupBy(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;
        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;
        string field1 = "";
        string field2 = "";
        field2 = getfield2();

        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "prodmodelwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("Model"));
        
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        //ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Productname"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["model"].ToString().ToUpper().Trim();


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {

                    DataRow dr_final889 = dtt.NewRow();
                    dtt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Product"] = fLvlValue;
                    dr_final8["Model"] = sLvlValue;
                    
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal = 0;

                    dtt.Rows.Add(dr_final8);

                    
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final889 = dtt.NewRow();
                    dtt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Product"] = "Total : " + fLvlValue;
                    
                    dr_final8["Model"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal1);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal1 = 0;

                    dtt.Rows.Add(dr_final8);

                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Product"] = dr["Productname"];
                dr_final88["Model"] = dr["Model"];
                
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);

            }
        }

        DataRow dr_final1122122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122122);

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Product"] = fLvlValue;
        dr_final8889["Model"] = sLvlValue;
        
        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Product"] = "Total : " + fLvlValue;
        
        dr_final87["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Product"] = "Grand Total";
        
        dr_final123["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal2);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    public void bindDatabrandprodmodelwise(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string tLvlValueTemp = string.Empty;
        string tLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;
        double modeltot = 0;

        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;

        double stktotal3 = 0;
        string field1 = "";
        string field2 = "";
        field2 = getfield2();

        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "brandprodmodelwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("Model"));
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("ItemCode"));
        dtt.Columns.Add(new DataColumn("pricename"));
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        //ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                tLvlValueTemp = dr["model"].ToString().ToUpper().Trim();

                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                {
                    DataRow dr_final889 = dtt.NewRow();
                    dtt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "";
                    dr_final8["Category"] = "";
                    dr_final8["Product"] = "";
                    dr_final8["Model"] = "Total : " + tLvlValue;
                    dr_final8["ItemCode"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));


                    modeltot = 0;
                    stktotal = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "";
                    dr_final8["Product"] = "Total : " + sLvlValue;
                    dr_final8["Category"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal1));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal1 = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "Total : " + fLvlValue;
                    dr_final8["Category"] = "";
                    dr_final8["Product"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";
                    dr_final8["pricename"] = "";
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal2));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal2 = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Product"] = dr["Productname"];
                dr_final88["Category"] = dr["Categoryname"];
                dr_final88["ItemCode"] = dr["ItemCode"];
                dr_final88["Model"] = dr["Model"];
                dr_final88["pricename"] = dr["pricename"];
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);

                stktotal3 = stktotal3 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                modeltot = modeltot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                dtt.Rows.Add(dr_final88);
            }
        }

        DataRow dr_final1133 = dtt.NewRow();
        dtt.Rows.Add(dr_final1133);

        DataRow dr_final88899 = dtt.NewRow();
        dr_final88899["Brand"] = "";
        dr_final88899["Product"] = "";
        dr_final88899["Category"] = "";
        dr_final88899["ItemCode"] = "";
        dr_final88899["pricename"] = "";
        dr_final88899["Model"] = "Total : " + tLvlValue;

        if (chkboxQty.Checked == true)
            dr_final88899["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal));

        if (chkMRP.Checked == true)
            dr_final88899["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final88899["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final88899["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final88899["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final88899["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

        dtt.Rows.Add(dr_final88899);

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Brand"] = "";
        dr_final8889["Product"] = "Total : " + sLvlValue;
        dr_final8889["Category"] = "";
        dr_final8889["ItemCode"] = "";
        dr_final8889["Model"] = "";
        dr_final8889["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal1));

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Brand"] = "Total : " + fLvlValue;
        dr_final87["Category"] = "";
        dr_final87["Product"] = "";
        dr_final87["ItemCode"] = "";
        dr_final87["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal2));

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Brand"] = "Grand Total";
        dr_final123["Category"] = "";
        dr_final123["Product"] = "";
        dr_final123["ItemCode"] = "";
        dr_final123["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal3));

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDatabrandprodmodelwiseGroupBy(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string tLvlValueTemp = string.Empty;
        string tLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;
        double modeltot = 0;

        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;

        double stktotal3 = 0;
        string field1 = "";
        string field2 = "";
        field2 = getfield2();

        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "brandprodmodelwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("Model"));
        
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        //ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                tLvlValueTemp = dr["model"].ToString().ToUpper().Trim();

                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                {

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = fLvlValue;
                    dr_final8["Product"] = sLvlValue;
                    dr_final8["Model"] = tLvlValue;
                    
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));


                    modeltot = 0;
                    stktotal = 0;
                    dtt.Rows.Add(dr_final8);
                }


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "";
                    dr_final8["Product"] = "Total : " + sLvlValue;
                    
                    dr_final8["Model"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal1));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal1 = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final312123 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312123);
                    
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "Total : " + fLvlValue;
                    dr_final8["Product"] = "";
                    dr_final8["Model"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal2));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal2 = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);

                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Product"] = dr["Productname"];
                dr_final88["Model"] = dr["Model"];

                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);

                stktotal3 = stktotal3 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                modeltot = modeltot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                
            }
        }

        DataRow dr_final1133 = dtt.NewRow();
        dtt.Rows.Add(dr_final1133);

        DataRow dr_final88899 = dtt.NewRow();
        dr_final88899["Brand"] = fLvlValue;
        dr_final88899["Product"] = sLvlValue;
        dr_final88899["Model"] = tLvlValue;

        if (chkboxQty.Checked == true)
            dr_final88899["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal));

        if (chkMRP.Checked == true)
            dr_final88899["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final88899["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final88899["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final88899["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final88899["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

        dtt.Rows.Add(dr_final88899);

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Brand"] = "";
        dr_final8889["Product"] = "Total : " + sLvlValue;
        
        
        dr_final8889["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal1));

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Brand"] = "Total : " + fLvlValue;
        
        dr_final87["Product"] = "";
        
        dr_final87["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal2));

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Brand"] = "Grand Total";
        
        dr_final123["Product"] = "";
        
        dr_final123["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal3));

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataCategorybrandprodwise(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string tLvlValueTemp = string.Empty;
        string tLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;
        double modeltot = 0;

        string field1 = "";
        string field2 = "";
        field2 = getfield2();

        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;

        double stktotal3 = 0;
        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "Categorybrandprodwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("Model"));
        dtt.Columns.Add(new DataColumn("ItemCode"));

        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();
                tLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                {
                    DataRow dr_final889 = dtt.NewRow();
                    dtt.Rows.Add(dr_final889);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "";
                    dr_final8["Category"] = "";
                    dr_final8["Product"] = "Total : " + tLvlValue;
                    dr_final8["Model"] = "";
                    dr_final8["ItemCode"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));


                    modeltot = 0;
                    stktotal = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "Total : " + sLvlValue;
                    dr_final8["Product"] = "";
                    dr_final8["Category"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal1));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal1 = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "";
                    dr_final8["Category"] = "Total : " + fLvlValue;
                    dr_final8["Product"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal2));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal2 = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Product"] = dr["Productname"];
                dr_final88["Category"] = dr["Categoryname"];
                dr_final88["ItemCode"] = dr["ItemCode"];
                dr_final88["Model"] = dr["Model"];

                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);
                stktotal3 = stktotal3 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                modeltot = modeltot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                dtt.Rows.Add(dr_final88);
            }
        }

        DataRow dr_final1133 = dtt.NewRow();
        dtt.Rows.Add(dr_final1133);

        DataRow dr_final88899 = dtt.NewRow();
        dr_final88899["Brand"] = "";
        dr_final88899["Product"] = "Total : " + tLvlValue;
        dr_final88899["Category"] = "";
        dr_final88899["ItemCode"] = "";
        dr_final88899["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final88899["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal));

        if (chkMRP.Checked == true)
            dr_final88899["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final88899["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final88899["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final88899["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final88899["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

        dtt.Rows.Add(dr_final88899);

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Brand"] = "Total : " + sLvlValue;
        dr_final8889["Product"] = "";
        dr_final8889["Category"] = "";
        dr_final8889["ItemCode"] = "";
        dr_final8889["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal1));

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Brand"] = "";
        dr_final87["Category"] = "Total : " + fLvlValue;
        dr_final87["Product"] = "";
        dr_final87["ItemCode"] = "";
        dr_final87["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal2));

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Brand"] = "";
        dr_final123["Category"] = "Grand Total";
        dr_final123["Product"] = "";
        dr_final123["ItemCode"] = "";
        dr_final123["Model"] = "";

        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal3));

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataCategorybrandprodwiseGroupBy(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        string sLvlValueTemp = string.Empty;
        string sLvlValue = string.Empty;
        string tLvlValueTemp = string.Empty;
        string tLvlValue = string.Empty;
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double brandtotal = 0;
        double producttot = 0;
        double modeltot = 0;

        string field1 = "";
        string field2 = "";
        field2 = getfield2();

        double stktotal = 0;
        double stktotal1 = 0;
        double stktotal2 = 0;

        double stktotal3 = 0;
        double total = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "Categorybrandprodwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Product"));
        
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));

        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                sLvlValueTemp = dr["Brand"].ToString().ToUpper().Trim();
                tLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                {
                   
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = sLvlValue;
                    dr_final8["Category"] = fLvlValue;
                    dr_final8["Product"] = tLvlValue;
                    

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));


                    modeltot = 0;
                    stktotal = 0;
                    dtt.Rows.Add(dr_final8);

                   
                }


                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                {
                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "Total : " + sLvlValue;
                    dr_final8["Product"] = "";
                    dr_final8["Category"] = "";
                    

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal1));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

                    producttot = 0;
                    stktotal1 = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);
                }


                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Brand"] = "";
                    dr_final8["Category"] = "Total : " + fLvlValue;
                    dr_final8["Product"] = "";
                    
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal2));

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

                    brandtotal = 0;
                    stktotal2 = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;

                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Product"] = dr["Productname"];
                dr_final88["Category"] = dr["Categoryname"];
                
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                    dr_final88["MRP"] = dr["MRP"];

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                if (chkboxVal.Checked == true)
                    dr_final88["MRP Value"] = dr["MRPValue"];

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                stktotal2 = stktotal2 + Convert.ToDouble(dr["Stock"]);

                stktotal3 = stktotal3 + Convert.ToDouble(dr["Stock"]);
                brandtotal = brandtotal + Convert.ToDouble(dr["Value"]);
                producttot = producttot + Convert.ToDouble(dr["Value"]);
                modeltot = modeltot + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
            }
        }

        //DataRow dr_final1133 = dtt.NewRow();
        //dtt.Rows.Add(dr_final1133);

        DataRow dr_final88899 = dtt.NewRow();
        dr_final88899["Brand"] = sLvlValue;
        dr_final88899["Product"] = tLvlValue;
        dr_final88899["Category"] = fLvlValue;
        
        if (chkboxQty.Checked == true)
            dr_final88899["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal));

        if (chkMRP.Checked == true)
            dr_final88899["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final88899["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final88899["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final88899["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final88899["Value"] = Convert.ToString(Convert.ToDecimal(modeltot));

        dtt.Rows.Add(dr_final88899);

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final8889 = dtt.NewRow();
        dr_final8889["Brand"] = "Total : " + sLvlValue;
        dr_final8889["Product"] = "";
        dr_final8889["Category"] = "";
        
        if (chkboxQty.Checked == true)
            dr_final8889["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal1));

        if (chkMRP.Checked == true)
            dr_final8889["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final8889["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final8889["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final8889["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final8889["Value"] = Convert.ToString(Convert.ToDecimal(producttot));

        dtt.Rows.Add(dr_final8889);

        DataRow dr_final1122 = dtt.NewRow();
        dtt.Rows.Add(dr_final1122);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Brand"] = "";
        dr_final87["Category"] = "Total : " + fLvlValue;
        dr_final87["Product"] = "";
        
        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal2));

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(brandtotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Brand"] = "";
        dr_final123["Category"] = "Grand Total";
        dr_final123["Product"] = "";
        
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(Convert.ToDecimal(stktotal3));

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    //protected string getfield()
    //{
    //    //string field1 = "";
    //    ////string field2="";
    //    //string field = "";
    //    //if (chkboxCategory.Checked)
    //    //{

    //    //    if (field1 == "")
    //    //    {

    //    //        field1 = "";
    //    //    }
    //    //    else
    //    //    {

    //    //        field1 += " , ";
    //    //    }

    //    //    field1 += "CategoryName";
    //    //}
    //    //if (chkboxBrand.Checked)
    //    //{

    //    //    if (field1 == "")
    //    //    {

    //    //        field1 = "";
    //    //    }
    //    //    else
    //    //    {

    //    //        field1 += " , ";
    //    //    }
    //    //    field1 += "Brand";
    //    //}
    //    //if (chkboxProduct.Checked)
    //    //{

    //    //    if (field1 == "")
    //    //    {

    //    //        field1 = "";
    //    //    }
    //    //    else
    //    //    {

    //    //        field1 += " , ";
    //    //    }
    //    //    field1 += "ProductName";

    //    //}
    //}

    protected string getfield2()
    {
        string field2 = "";
    
        field2 += "cat.CategoryName,";
        
        field2 += "(pm.productdesc) as Brand,";

        field2 += "pm.ProductName,";

        field2 += "pm.itemcode,";

        field2 += "pm.model,";

        field2 += "pm.stock,";

        field2 += "pm.branchcode,";

        field2 += "pricename,";

        // field2 += "pm.nlc,";

      //  field2 += "pm.dealerrate as dp,";

       // field2 += "pm.rate as mrp,";

        field2 += "(PM.Stock * tblProductPrices.Price) as Value ";

        return field2;
    }

    public void bindDataCatwiseGroupBy(string branchcode, string saBranchcode, string cond4, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        DataSet datas = new DataSet();
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double cattotal = 0;

        string field1 = "";
        string field2 = "";
        //field1 = getfield();
        field2 = getfield2();
        double total = 0;
        double stktotal = 0;
        double stktotal1 = 0;

        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "Catwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        string itemcode = string.Empty;

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Category"));
        
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));


        //char[] commaSeparator = new char[] { ',' };
        //string[] result;
        //result = cond4.Split(commaSeparator, StringSplitOptions.None);

        //foreach (string str in result)
        //{
        //    dtt.Columns.Add(new DataColumn(str));
        //}
      


        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

       // ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();

                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Category"] = fLvlValue;
                    
                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = stktotal;

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(cattotal));

                    cattotal = 0;
                    stktotal = 0;

                    dtt.Rows.Add(dr_final8);
                }
                fLvlValue = fLvlValueTemp;
                itemcode = dr["ItemCode"].ToString();
                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Category"] = dr["Categoryname"];
                
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                cattotal = cattotal + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
            }
        }

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Category"] = fLvlValue;

        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = stktotal;

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(cattotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Category"] = "Grand Total";
 
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = stktotal1;

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    public void bindDataCatwise(string branchcode, string saBranchcode, string pricename)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date;

        DataSet datas = new DataSet();
        string fLvlValueTemp = string.Empty;
        string fLvlValue = string.Empty;
        double cattotal = 0;

        string field1 = "";
        string field2 = "";
        //field1 = getfield();
        field2 = getfield2();
        double total = 0;
        double stktotal = 0;
        double stktotal1 = 0;
        Date = Convert.ToDateTime(txtStartDate.Text);

        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        string Types = "Catwise";
        ds = objBL.getstockreport(connection, Date, field1, field2, Types, branchcode, saBranchcode,pricename);

        string itemcode = string.Empty;

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Category"));
        dtt.Columns.Add(new DataColumn("Brand"));
        dtt.Columns.Add(new DataColumn("Product"));
        dtt.Columns.Add(new DataColumn("ItemCode"));
        dtt.Columns.Add(new DataColumn("Model"));
        dtt.Columns.Add(new DataColumn("BranchCode"));
        dtt.Columns.Add(new DataColumn("pricename"));
        if (chkboxQty.Checked == true)
            dtt.Columns.Add(new DataColumn("Stock"));

        if (chkMRP.Checked == true)
            dtt.Columns.Add(new DataColumn("MRP"));

        if (chkNLC.Checked == true)
            dtt.Columns.Add(new DataColumn("NLC"));
        
        if (chkDP.Checked == true)
            dtt.Columns.Add(new DataColumn("DP"));
        
        if (chkboxper.Checked == true)
            dtt.Columns.Add(new DataColumn("Percentage"));

        if (chkboxVal.Checked == true)
            dtt.Columns.Add(new DataColumn("Value"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        //ds = objBL.gethistoryrate(sDataSource, Date, ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();

                if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                {
                    DataRow dr_final312 = dtt.NewRow();
                    dtt.Rows.Add(dr_final312);

                    DataRow dr_final8 = dtt.NewRow();
                    dr_final8["Category"] = "Total : " + fLvlValue;
                    dr_final8["Brand"] = "";
                    dr_final8["Product"] = "";
                    dr_final8["ItemCode"] = "";
                    dr_final8["Model"] = "";
                    dr_final8["BranchCode"] = "";

                    if (chkboxQty.Checked == true)
                        dr_final8["Stock"] = Convert.ToString(stktotal);

                    if (chkMRP.Checked == true)
                        dr_final8["MRP"] = "";

                    if (chkNLC.Checked == true)
                        dr_final8["NLC"] = "";

                    if (chkDP.Checked == true)
                        dr_final8["DP"] = "";

                    if (chkboxper.Checked == true)
                        dr_final8["Percentage"] = "";

                    if (chkboxVal.Checked == true)
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(cattotal));            

                    cattotal = 0;
                    stktotal = 0;
                    dtt.Rows.Add(dr_final8);

                    DataRow dr_final888 = dtt.NewRow();
                    dtt.Rows.Add(dr_final888);
                }
                fLvlValue = fLvlValueTemp;
                itemcode = dr["ItemCode"].ToString();
                DataRow dr_final88 = dtt.NewRow();
                dr_final88["Category"] = dr["Categoryname"];
                dr_final88["Brand"] = dr["Brand"];
                dr_final88["Product"] = dr["Productname"];
                dr_final88["ItemCode"] = dr["ItemCode"];
                dr_final88["Model"] = dr["Model"];
                dr_final88["BranchCode"] = dr["BranchCode"];
                dr_final88["pricename"] = dr["pricename"];
                if (chkboxQty.Checked == true)
                    dr_final88["Stock"] = dr["Stock"];

                if (chkMRP.Checked == true)
                {
                    dr_final88["MRP"] = dr["MRP"];
                }

                if (chkNLC.Checked == true)
                    dr_final88["NLC"] = dr["NLC"];

                if (chkDP.Checked == true)
                    dr_final88["DP"] = dr["DP"];

                if (chkboxper.Checked == true)
                {
                    if (Convert.ToDouble(dr["Value"]) > 0)
                    {
                        dr_final88["Percentage"] = (100 / Convert.ToDouble(dr["Value"])) * 100;
                    }
                    else
                    {
                        dr_final88["Percentage"] = "0";
                    }
                }

                if (chkboxVal.Checked == true)
                    dr_final88["Value"] = dr["Value"];

                stktotal = stktotal + Convert.ToDouble(dr["Stock"]);
                stktotal1 = stktotal1 + Convert.ToDouble(dr["Stock"]);
                cattotal = cattotal + Convert.ToDouble(dr["Value"]);
                total = total + Convert.ToDouble(dr["Value"]);
                dtt.Rows.Add(dr_final88);
            }
        }
        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final87 = dtt.NewRow();
        dr_final87["Category"] = "Total : " + fLvlValue;
        dr_final87["Brand"] = "";
        dr_final87["Product"] = "";
        dr_final87["ItemCode"] = "";
        dr_final87["Model"] = "";
        dr_final87["BranchCode"] = "";
        dr_final87["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final87["Stock"] = Convert.ToString(stktotal);

        if (chkMRP.Checked == true)
            dr_final87["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final87["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final87["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final87["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final87["Value"] = Convert.ToString(Convert.ToDecimal(cattotal));

        dtt.Rows.Add(dr_final87);

        DataRow dr_final113 = dtt.NewRow();
        dtt.Rows.Add(dr_final113);

        DataRow dr_final123 = dtt.NewRow();
        dr_final123["Category"] = "Grand Total";
        dr_final123["Brand"] = "";
        dr_final123["Product"] = "";
        dr_final123["ItemCode"] = "";
        dr_final123["Model"] = "";
        dr_final123["BranchCode"] = "";
        dr_final123["pricename"] = "";
        if (chkboxQty.Checked == true)
            dr_final123["Stock"] = Convert.ToString(stktotal1);

        if (chkMRP.Checked == true)
            dr_final123["MRP"] = "";

        if (chkNLC.Checked == true)
            dr_final123["NLC"] = "";

        if (chkDP.Checked == true)
            dr_final123["DP"] = "";

        if (chkboxper.Checked == true)
            dr_final123["Percentage"] = "";

        if (chkboxVal.Checked == true)
            dr_final123["Value"] = Convert.ToString(Convert.ToDecimal(total));

        dtt.Rows.Add(dr_final123);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    //protected void CheckBox13_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (CheckBox13.Checked == true)
    //    {
    //        CheckBox1.Checked = true;
    //        CheckBox2.Checked = true;
    //        CheckBox3.Checked = true;
    //        CheckBox4.Checked = true;
    //        CheckBox5.Checked = true;
    //        CheckBox6.Checked = true;
    //        CheckBox7.Checked = true;
    //        CheckBox8.Checked = true;
    //        CheckBox9.Checked = true;
    //        CheckBox10.Checked = true;
    //        CheckBox11.Checked = true;
    //        CheckBox12.Checked = true;
    //    }
    //    else
    //    {
    //        CheckBox1.Checked = false;
    //        CheckBox2.Checked = false;
    //        CheckBox3.Checked = false;
    //        CheckBox4.Checked = false;
    //        CheckBox5.Checked = false;
    //        CheckBox6.Checked = false;
    //        CheckBox7.Checked = false;
    //        CheckBox8.Checked = false;
    //        CheckBox9.Checked = false;
    //        CheckBox10.Checked = false;
    //        CheckBox11.Checked = false;
    //        CheckBox12.Checked = false;
    //    }
    //}


    //protected string getfield()
    //{
    //    string field1 = "";
    //    string field = "";

    //    if (CheckBox1.Checked)
    //    {

    //        if (field1 == "")
    //        {

    //            field1 = "";
    //        }
    //        else
    //        {

    //            field1 += " , ";
    //        }

    //        field1 += "April";
    //    }

    //    if (CheckBox2.Checked)
    //    {

    //        if (field1 == "")
    //        {

    //            field1 = "";
    //        }
    //        else
    //        {

    //            field1 += " , ";
    //        }
    //        field1 += "May";
    //    }
    //    if (CheckBox3.Checked)
    //    {

    //        if (field1 == "")
    //        {

    //            field1 = "";
    //        }
    //        else
    //        {

    //            field1 += " , ";
    //        }
    //        field1 += "June";

    //    }
    //    if (CheckBox4.Checked)
    //    {

    //        if (field1 == "")
    //        {

    //            field1 = "";
    //        }
    //        else
    //        {

    //            field1 += " , ";
    //        }
    //        field1 += "July";

    //    }

    //    if (CheckBox5.Checked)
    //    {

    //        if (field1 == "")
    //        {

    //            field1 = "";
    //        }
    //        else
    //        {

    //            field1 += " , ";
    //        }

    //        field1 += "August";
    //    }
    //    if (CheckBox6.Checked)
    //    {

    //        if (field1 == "")
    //        {

    //            field1 = "";
    //        }
    //        else
    //        {

    //            field1 += " , ";
    //        }

    //        field1 += "September";
    //    }
    //    if (CheckBox7.Checked)
    //    {

    //        if (field1 == "")
    //        {

    //            field1 = "";
    //        }
    //        else
    //        {

    //            field1 += " , ";
    //        }

    //        field1 += "October";
    //    }
    //    if (CheckBox8.Checked)
    //    {

    //        if (field1 == "")
    //        {

    //            field1 = "";
    //        }
    //        else
    //        {

    //            field1 += " , ";
    //        }

    //        field1 += "November";
    //    }
    //    if (CheckBox9.Checked == true)
    //    {
    //        if (field1 == "")
    //        {
    //            field1 = "";

    //        }
    //        else
    //        {
    //            field1 += ",";
    //        }
    //        field1 += "December";
    //    }
    //    if (CheckBox10.Checked == true)
    //    {
    //        if (field1 == "")
    //        {
    //            field1 = "";

    //        }
    //        else
    //        {
    //            field1 += ",";
    //        }
    //        field1 += "January";
    //    }

    //    if (CheckBox11.Checked == true)
    //    {
    //        if (field1 == "")
    //        {
    //            field1 = "";

    //        }
    //        else
    //        {
    //            field1 += ",";
    //        }
    //        field1 += "February";
    //    }


    //    if (CheckBox1.Checked == true)
    //    {
    //        if (field1 == "")
    //        {
    //            field1 = "";

    //        }
    //        else
    //        {
    //            field1 += ",";
    //        }
    //        field1 += "March";
    //    }
        

    //    return field1;
    //}

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

    public void bindData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DateTime Date, endDate;
        DataSet dst = new DataSet();

        string pret = "NO";
        string itrans = "NO";
        string denot = "NO";

        Date = Convert.ToDateTime(txtStartDate.Text);
       
        objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

        //ds = objBL.getstockdate(sDataSource, Date, selCols, field2, cond, grpBy, ordrby);

        DataTable dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("Month"));
        dtt.Columns.Add(new DataColumn(" "));
        dtt.Columns.Add(new DataColumn("Amount"));

        DataRow dr_final14 = dtt.NewRow();
        dtt.Rows.Add(dr_final14);

        double credit = 0.00;
        double Tottot = 0.00;

        foreach (DataRow dr in dst.Tables[0].Rows)
        {

        }

        DataRow dr_final11 = dtt.NewRow();
        dtt.Rows.Add(dr_final11);

        DataRow dr_final88 = dtt.NewRow();
        dr_final88["Month"] = "Total";
        dr_final88[" "] = " ";
        dr_final88["Amount"] = Tottot;
        dtt.Rows.Add(dr_final88);

        if (dst.Tables[0].Rows.Count > 0)
        {
            ExportToExcel(dtt);
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
            string filename = "Stock.xls";
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

    protected void lstBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //foreach (ListItem li in lstBranch.Items)
        //{
        //    if (lstBranch.SelectedIndex == 0)
        //    {
        //        if (li.Text != "All")
        //        {
        //            li.Selected = true;
        //        }
        //    }
        //}

    }
    protected void lstPricelist_SelectedIndexChanged(object sender, EventArgs e)
    {
        //foreach (ListItem li in lstPricelist.Items)
        //{
        //    if (lstPricelist.SelectedIndex == 0)
        //    {
        //        if (li.Text != "All")
        //        {
        //            li.Selected = true;
        //        }
        //    }
        //}
    }
}
