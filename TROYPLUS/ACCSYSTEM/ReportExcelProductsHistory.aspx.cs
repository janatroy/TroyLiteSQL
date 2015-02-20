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
public partial class ReportExcelProductsHistory : System.Web.UI.Page
{
    //DBClass objdb = new DBClass();
    BusinessLogic objBL;
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
            //loadCodes();
            //loadModels();
            if (!IsPostBack)
            {
                loadCategory();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridCust_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GridCust, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loaProducts();
            loadCodes();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddlproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadCodes();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            loadBrands();
            loaProducts();
            loadCodes();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadBrands()
    {
        string cat = string.Empty;
        cat = ddlCategory.SelectedValue;

        BusinessLogic bl = new BusinessLogic(sDataSource);
        var ds = bl.ListBrandsForCategoryID(cat, "");

        ddlBrand.DataSource = ds;
        ddlBrand.DataTextField = "ProductDesc";
        ddlBrand.DataValueField = "ProductDesc";
        ddlBrand.DataBind();

        //ddlBrand.Items.Insert(0, new ListItem("All", "All"));
    }

    private void loadCategory()
    {
        string connection = Request.Cookies["Company"].Value;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        var ds = bl.ListCategory(connection, "");

        ddlCategory.DataSource = ds;
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataValueField = "CategoryID";
        ddlCategory.DataBind();

        ddlCategory.Items.Insert(0, new ListItem("All", "All"));
    }

    private void loaProducts()
    {
        string brand = string.Empty;
        brand = ddlBrand.SelectedValue;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        var ds = bl.ListProdcutName(brand);

        ddlproduct.DataSource = ds;
        ddlproduct.DataTextField = "ProductName";
        ddlproduct.DataValueField = "ProductName";
        ddlproduct.DataBind();

        //ddlproduct.Items.Insert(0, new ListItem("All", "All"));
    }

    private void loadCodes()
    {
        string cat = string.Empty;
        cat = ddlCategory.SelectedValue;
        string prodName = ddlproduct.SelectedValue;
        string brand = ddlBrand.SelectedValue;
        BusinessLogic bl = new BusinessLogic(sDataSource);

        var ds = bl.ListProdcutsForHistory(brand,prodName, cat);
        cmbProdAdd.Items.Clear();
        cmbProdAdd.DataTextField = "ItemCode";
        cmbProdAdd.DataValueField = "ItemCode";
        cmbProdAdd.DataSource = ds;
        cmbProdAdd.DataBind();

        //ddlproduct.Items.Insert(0, new ListItem("All", "All"));
    }

    //private void loadModels()
    //{
    //    //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
    //    BusinessLogic bl = new BusinessLogic();
    //    DataSet ds = new DataSet();
    //    ds = bl.ListAllModels();
    //    cmbProdAdd.DataTextField = "Model";
    //    cmbProdAdd.DataValueField = "Model";
    //    cmbProdAdd.DataSource = ds;
    //    cmbProdAdd.DataBind();
    //}

    private void BindGrid()
    {
        CreditLimitTotal = 0;
        OpenBalanceDRTotal = 0;
        DataSet ds = new DataSet();

        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        BusinessLogic bl = new BusinessLogic();

        
        DataSet dsSales = bl.ListProductHistory(connStr.Trim(), "");

        if (dsSales != null)
        {
            if (dsSales.Tables[0].Rows.Count > 0)
            {
                GridCust.DataSource = dsSales;
                GridCust.DataBind();
            }
        }
    }



    protected void btnData_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridCust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridCust.PageIndex = e.NewPageIndex;

            BindGrid();
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
            GridCust.PageIndex = ((DropDownList)sender).SelectedIndex;
            BindGrid();
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
            DateTime date = Convert.ToDateTime("01-01-1900");
            DataSet ds = new DataSet();

            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            BusinessLogic bl = new BusinessLogic();

            string itemcode = cmbProdAdd.SelectedValue;
            if (itemcode == "0")
            {
                itemcode = "";
            }

            ds = bl.ListProductHistory(connStr.Trim(), itemcode);

            string itemc = string.Empty;
            string sdate = string.Empty;
            string edate = string.Empty;
            double ratee = 0;

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    //dt.Columns.Add(new DataColumn("ProductName"));
                    dt.Columns.Add(new DataColumn("ItemCode"));
                    //dt.Columns.Add(new DataColumn("Model"));
                    //dt.Columns.Add(new DataColumn("Brand"));
                    //dt.Columns.Add(new DataColumn("MRP"));
                    //dt.Columns.Add(new DataColumn("MRP Date"));
                    //dt.Columns.Add(new DataColumn("NLC"));
                    //dt.Columns.Add(new DataColumn("NLC Date"));
                    //dt.Columns.Add(new DataColumn("DP"));
                    //dt.Columns.Add(new DataColumn("DP Date"));

                    dt.Columns.Add(new DataColumn("Rate Type"));
                    dt.Columns.Add(new DataColumn("Start Date"));
                    dt.Columns.Add(new DataColumn("End date"));
                    dt.Columns.Add(new DataColumn("Rate"));

                    DataRow dr_final123 = dt.NewRow();
                    dt.Rows.Add(dr_final123);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_final1 = dt.NewRow();
                        //dr_final1["ProductName"] = dr["ProductName"];
                        dr_final1["ItemCode"] = dr["ItemCode"];
                        //dr_final1["Model"] = dr["Model"];
                        //dr_final1["Brand"] = dr["productdesc"];
                        //dr_final1["MRP"] = dr["MRP"];
                        //dr_final1["MRP Date"] = dr["MRPDate"];
                        //dr_final1["NLC"] = dr["NLC"];
                        //dr_final1["NLC Date"] = dr["NLCDate"];
                        //dr_final1["DP"] = dr["DP"];
                        //dr_final1["DP Date"] = dr["DPDate"];

                        string afa = dr["MrpStartdate"].ToString().ToUpper().Trim();
                        string dftaa = Convert.ToDateTime(afa).ToString("dd/MM/yyyy");

                        string afaa = dr["MrpEnddate"].ToString().ToUpper().Trim();
                        string dfta = Convert.ToDateTime(afaa).ToString("dd/MM/yyyy");

                        if ((dr["ItemCode"].ToString() == itemc) && (sdate == dftaa) && (edate == dfta))
                        {
                            //dr_final1["Rate Type"] = "MRP";

                            //string aa = dr["MrpStartdate"].ToString().ToUpper().Trim();
                            //string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                            //dr_final1["Start Date"] = dtaa;

                            //string aaa = dr["MrpEnddate"].ToString().ToUpper().Trim();
                            //string dta = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                            //dr_final1["End Date"] = dta;

                            //dr_final1["Rate"] = dr["mrp"];
                        }
                        else
                        {
                            dr_final1["Rate Type"] = "MRP";

                            string aa = dr["MrpStartdate"].ToString().ToUpper().Trim();
                            string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                            dr_final1["Start Date"] = dtaa;

                            string aaa = dr["MrpEnddate"].ToString().ToUpper().Trim();
                            string dta = Convert.ToDateTime(aaa).ToString("dd/MM/yyyy");
                            dr_final1["End Date"] = dta;

                            dr_final1["Rate"] = dr["mrp"];
                            dt.Rows.Add(dr_final1);
                        }


                        itemc = dr["ItemCode"].ToString();

                        string aal = dr["MrpStartdate"].ToString().ToUpper().Trim();
                        string dtaal = Convert.ToDateTime(aal).ToString("dd/MM/yyyy");
                        sdate = dtaal;

                        string aaal = dr["MrpEnddate"].ToString().ToUpper().Trim();
                        string dtal = Convert.ToDateTime(aaal).ToString("dd/MM/yyyy");
                        edate = dtal;

                        ratee = Convert.ToDouble(dr["mrp"]);

                    }

                    itemc = string.Empty;
                    ratee = 0;
                    sdate = string.Empty;
                    edate = string.Empty;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DataRow dr_final1 = dt.NewRow();
                            dr_final1["ItemCode"] = dr["ItemCode"];

                            string aka = dr["NLCStartdate"].ToString().ToUpper().Trim();
                            string dktaa = Convert.ToDateTime(aka).ToString("dd/MM/yyyy");

                            string akaa = dr["NLCEnddate"].ToString().ToUpper().Trim();
                            string dkta = Convert.ToDateTime(akaa).ToString("dd/MM/yyyy");

                            if ((dr["ItemCode"].ToString() == itemc) && (sdate == dktaa) && (edate == dkta))
                            {
                            }
                            else
                            {
                                dr_final1["Rate Type"] = "NLC";

                                string aa = dr["NLCStartdate"].ToString().ToUpper().Trim();
                                string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                                dr_final1["Start Date"] = dtaa;

                                string aaa = dr["NLCEnddate"].ToString().ToUpper().Trim();
                                string dta = Convert.ToDateTime(aaa).ToString("dd/MM/yyyy");
                                dr_final1["End Date"] = dta;

                                dr_final1["Rate"] = dr["NLC"];
                                dt.Rows.Add(dr_final1);
                            }
                            itemc = dr["ItemCode"].ToString();

                            string aal = dr["NLCStartdate"].ToString().ToUpper().Trim();
                            string dtaal = Convert.ToDateTime(aal).ToString("dd/MM/yyyy");
                            sdate = dtaal;

                            string aaal = dr["NLCEnddate"].ToString().ToUpper().Trim();
                            string dtal = Convert.ToDateTime(aaal).ToString("dd/MM/yyyy");
                            edate = dtal;

                            ratee = Convert.ToDouble(dr["NLC"]);

                        }
                    }

                    itemc = string.Empty;
                    ratee = 0;
                    sdate = string.Empty;
                    edate = string.Empty;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DataRow dr_final1 = dt.NewRow();
                            dr_final1["ItemCode"] = dr["ItemCode"];

                            string ala = dr["DPStartdate"].ToString().ToUpper().Trim();
                            string dltaa = Convert.ToDateTime(ala).ToString("dd/MM/yyyy");

                            string alaa = dr["DPEnddate"].ToString().ToUpper().Trim();
                            string dlta = Convert.ToDateTime(alaa).ToString("dd/MM/yyyy");

                            if ((dr["ItemCode"].ToString() == itemc) && (sdate == dltaa) && (edate == dlta))
                            {
                            }
                            else
                            {
                                dr_final1["Rate Type"] = "DP";

                                string aa = dr["DPStartdate"].ToString().ToUpper().Trim();
                                string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                                dr_final1["Start Date"] = dtaa;

                                string aaa = dr["DPEnddate"].ToString().ToUpper().Trim();
                                string dta = Convert.ToDateTime(aaa).ToString("dd/MM/yyyy");
                                dr_final1["End Date"] = dta;

                                dr_final1["Rate"] = dr["DP"];
                                dt.Rows.Add(dr_final1);
                            }
                            itemc = dr["ItemCode"].ToString();

                            string aal = dr["DPStartdate"].ToString().ToUpper().Trim();
                            string dtaal = Convert.ToDateTime(aal).ToString("dd/MM/yyyy");
                            sdate = dtaal;

                            string aaal = dr["DPEnddate"].ToString().ToUpper().Trim();
                            string dtal = Convert.ToDateTime(aaal).ToString("dd/MM/yyyy");
                            edate = dtal;

                            ratee = Convert.ToDouble(dr["DP"]);
                        }
                    }


                    DataSet dstt = bl.ListProductDet(connStr.Trim(), itemcode);

                    if (dstt.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drd in dstt.Tables[0].Rows)
                        {
                            DataRow dr_final12312 = dt.NewRow();
                            dr_final12312["ItemCode"] = drd["ItemCode"];
                            dr_final12312["Rate Type"] = "MRP";
                            string aka = drd["MrpEffdate"].ToString().ToUpper().Trim();
                            string dktaa = Convert.ToDateTime(aka).ToString("dd/MM/yyyy");
                            dr_final12312["Start Date"] = dktaa;
                            //string akaa = drd["MrpEnddate"].ToString().ToUpper().Trim();
                            //string dkta = Convert.ToDateTime(akaa).ToString("dd/MM/yyyy");
                            dr_final12312["End Date"] = "";
                            dr_final12312["Rate"] = drd["mrp"];
                            dt.Rows.Add(dr_final12312);


                            DataRow dr_final123123 = dt.NewRow();
                            dr_final123123["ItemCode"] = drd["ItemCode"];
                            dr_final123123["Rate Type"] = "NLC";
                            string aa = drd["NLCEffdate"].ToString().ToUpper().Trim();
                            string dtaa = Convert.ToDateTime(aa).ToString("dd/MM/yyyy");
                            dr_final123123["Start Date"] = dtaa;
                            //string aaa = drd["NLCEnddate"].ToString().ToUpper().Trim();
                            //string dta = Convert.ToDateTime(aaa).ToString("dd/MM/yyyy");
                            dr_final123123["End Date"] = "";
                            dr_final123123["Rate"] = drd["NLC"];
                            dt.Rows.Add(dr_final123123);


                            DataRow dr_final1 = dt.NewRow();
                            dr_final1["ItemCode"] = drd["ItemCode"];
                            dr_final1["Rate Type"] = "DP";
                            string ata = drd["DPEffdate"].ToString().ToUpper().Trim();
                            string dttaa = Convert.ToDateTime(ata).ToString("dd/MM/yyyy");
                            dr_final1["Start Date"] = dttaa;
                            //string ataa = drd["DPEnddate"].ToString().ToUpper().Trim();
                            //string dtta = Convert.ToDateTime(ataa).ToString("dd/MM/yyyy");
                            dr_final1["End Date"] = "";
                            dr_final1["Rate"] = drd["DP"];
                            dt.Rows.Add(dr_final1);
                        }
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
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            string filename = "Product Rate History.xls";
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
    decimal CreditLimitTotal;
    decimal OpenBalanceDRTotal;

    protected void GridCust_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }
}
