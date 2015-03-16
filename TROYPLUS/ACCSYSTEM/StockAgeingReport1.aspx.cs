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

public partial class StockAgeingReport1 : System.Web.UI.Page
{
    decimal Gtotal = 0;
    decimal Gttl = 0;
    decimal Pttls = 0;
    decimal Pttl = 0;
    decimal brandTotal = 0, catIDTotal = 0, modelTotal = 0;

    string selLevels = "";
    string firstLevel = "";
    string secondLevel = "";
    string thirdLevel = "";
    string fourthLevel = "";

    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!Page.IsPostBack)
            {
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);
                    //companyInfo = bl.getCompanyInfo("");

                    if (companyInfo != null)
                    {
                        if (companyInfo.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in companyInfo.Tables[0].Rows)
                            {
                                lblTNGST.Text = Convert.ToString(dr["TINno"]);
                                lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                                lblPhone.Text = Convert.ToString(dr["Phone"]);
                                lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                                lblAddress.Text = Convert.ToString(dr["Address"]);
                                lblCity.Text = Convert.ToString(dr["city"]);
                                lblPinCode.Text = Convert.ToString(dr["Pincode"]);
                                lblState.Text = Convert.ToString(dr["state"]);

                            }
                        }
                        //}
                    }

                }
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                //txtEndDate.Text = DateTime.Now.ToShortDateString();

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                txtEndDate.Text = dtaa;

                txtDuration.Text = "7";
                txtColumns.Text = "4";
                loadCategories();
                //loadCustomer();
                loadModels();
                loadBrands();
                //loadEmp();
                loadProducts();

                fillDdl(ddlFirstLvl);
                fillDdl(ddlSecondLvl);
                fillDdl(ddlThirdLvl);
                fillDdl(ddlFourthLvl);



                if (Request.QueryString["firstLevel"] != null)
                {
                    firstLevel = Request.QueryString["firstLevel"].ToString();
                }
                if (Request.QueryString["secondLevel"] != null)
                {
                    secondLevel = Request.QueryString["secondLevel"].ToString();
                }
                if (Request.QueryString["thirdLevel"] != null)
                {
                    thirdLevel = Request.QueryString["thirdLevel"].ToString();
                }
                if (Request.QueryString["fourthLevel"] != null)
                {
                    fourthLevel = Request.QueryString["fourthLevel"].ToString();
                }
                DataSet ds = GenerateGridColumns();
                ds = UpdatePurchaseData(ds);
                ds = UpdateSalesData(ds);
                ds = ConsolidateData(ds);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    gvOuts.DataSource = ds;
                    gvOuts.DataBind();
                    gvOuts.Visible = true;
                }
                else
                {
                    gvOuts.DataSource = null;
                    gvOuts.DataBind();
                    gvOuts.Visible = true;
                }

                divPrint.Visible = true;

                divmain.Visible = true;
                div1.Visible = false;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void loadProducts()
    {
        BusinessLogic objBL = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = objBL.ListProdMdlItcd("ProductName");
        cmbProduct.DataSource = ds;
        cmbProduct.DataTextField = "ItemCode";
        cmbProduct.DataValueField = "ItemCode";
        cmbProduct.DataBind();
        cmbProduct.Items.Insert(0, "All");
    }

    private void loadCategories()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListCategory(sDataSource, "");

        cmbCategory.Items.Clear();

        cmbCategory.DataTextField = "CategoryName";
        cmbCategory.DataValueField = "CategoryID";
        cmbCategory.DataSource = ds;
        cmbCategory.DataBind();

        cmbCategory.Items.Insert(0, new ListItem("All Categories", "0"));

    }

    private void loadModels()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);

        var ds = bl.ListAllModels();

        cmbModel.Items.Clear();

        cmbModel.DataSource = ds;
        cmbModel.DataTextField = "Model";
        cmbModel.DataValueField = "Model";
        cmbModel.DataBind();

        cmbModel.Items.Insert(0, new ListItem("All Models", "0"));
    }

    private void loadBrands()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        var ds = bl.ListBrands();

        cmbBrand.DataSource = ds;
        cmbBrand.DataTextField = "ProductDesc";
        cmbBrand.DataValueField = "ProductDesc";
        cmbBrand.DataBind();

        cmbBrand.Items.Insert(0, new ListItem("All Brands", "0"));
    }

    private void fillDdl(DropDownList ddlist)
    {
        //DropDownList ddlList = new DropDownList();,,
        ddlist.Items.Insert(0, "None");
        ddlist.Items.Insert(1, "ProductDesc");
        ddlist.Items.Insert(2, "CategoryName");
        ddlist.Items.Insert(3, "Model");
        ddlist.Items.Insert(4, "ItemCode");
    }


    protected void btnstockageing_Click(object sender, EventArgs e)
    {
        try
        {
            if (!isValidLevels())
            {
                return;
            }
            firstLevel = ddlFirstLvl.SelectedValue;
            secondLevel = ddlSecondLvl.SelectedValue;
            thirdLevel = ddlThirdLvl.SelectedValue;
            fourthLevel = ddlFourthLvl.SelectedValue;

            DataSet ds = GenerateGridColumns();
            ds = UpdatePurchaseData(ds);
            ds = UpdateSalesData(ds);
            ds = ConsolidateData(ds);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvOuts.DataSource = ds;
                gvOuts.DataBind();
                gvOuts.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            }

            divPrint.Visible = true;

            divmain.Visible = true;
            div1.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btndet_Click(object sender, EventArgs e)
    {
        try
        {
            div1.Visible = true;
            divPrint.Visible = false;
            divmain.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private DataSet ConsolidateData(DataSet dsData)
    {
        for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
        {
            double qtyCount = 0.0;

            for (int j = dsData.Tables[0].Columns.Count - 1; j > 3; j--)
            {
                if (dsData.Tables[0].Rows[i][j] != null && dsData.Tables[0].Rows[i][j].ToString() != "")
                    qtyCount = qtyCount + double.Parse(dsData.Tables[0].Rows[i][j].ToString());
                else
                    dsData.Tables[0].Rows[i][j] = "0";
            }

            if (qtyCount < 1)
            {
                dsData.Tables[0].Rows[i].Delete();
            }
        }

        dsData.Tables[0].AcceptChanges();
        return dsData;
    }

    public DataSet GenerateGridColumns()
    {
        selLevels = "";

        int duration = int.Parse(txtDuration.Text);
        int noOfColumns = int.Parse(txtColumns.Text);

        
        if (Request.QueryString["duration"] != null)
        {
            duration = Convert.ToInt32(Request.QueryString["duration"].ToString());
        }
        if (Request.QueryString["noOfColumns"] != null)
        {
            noOfColumns = Convert.ToInt32(Request.QueryString["noOfColumns"].ToString());
        }


        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn dc;

        int ddl1 = 0;
        int ddl2 = 0;
        int ddl3 = 0;
        int ddl4 = 0;

        if (Request.QueryString["ddl1"] != null)
        {
            ddl1 = Convert.ToInt32(Request.QueryString["ddl1"].ToString());
        }
        if (Request.QueryString["ddl2"] != null)
        {
            ddl2 = Convert.ToInt32(Request.QueryString["ddl2"].ToString());
        }
        if (Request.QueryString["ddl3"] != null)
        {
            ddl3 = Convert.ToInt32(Request.QueryString["ddl3"].ToString());
        }
        if (Request.QueryString["ddl4"] != null)
        {
            ddl4 = Convert.ToInt32(Request.QueryString["ddl4"].ToString());
        }


        if (Request.QueryString["firstLevel"] != null)
        {
            firstLevel = Request.QueryString["firstLevel"].ToString();
        }
        if (Request.QueryString["secondLevel"] != null)
        {
            secondLevel = Request.QueryString["secondLevel"].ToString();
        }
        if (Request.QueryString["thirdLevel"] != null)
        {
            thirdLevel = Request.QueryString["thirdLevel"].ToString();
        }
        if (Request.QueryString["fourthLevel"] != null)
        {
            fourthLevel = Request.QueryString["fourthLevel"].ToString();
        }

        if (ddl1 > 0)
        {
            dc = new DataColumn(firstLevel);
            dt.Columns.Add(dc);
            selLevels += firstLevel;
        }

        if (ddl2 > 0)
        {
            dc = new DataColumn(secondLevel);
            dt.Columns.Add(dc);
            selLevels += secondLevel;
        }

        if (ddl3 > 0)
        {
            dc = new DataColumn(thirdLevel);
            dt.Columns.Add(dc);
            selLevels += thirdLevel;
        }

        if (ddl4 > 0)
        {
            dc = new DataColumn(fourthLevel);
            dt.Columns.Add(dc);
            selLevels += fourthLevel;
        }

        if (selLevels.IndexOf("ProductDesc") < 0)
            dt.Columns.Add(new DataColumn("ProductDesc"));
        if (selLevels.IndexOf("CategoryName") < 0)
            dt.Columns.Add(new DataColumn("CategoryName"));
        if (selLevels.IndexOf("Model") < 0)
            dt.Columns.Add(new DataColumn("Model"));
        if (selLevels.IndexOf("ItemCode") < 0)
            dt.Columns.Add(new DataColumn("ItemCode"));

        //dc = new DataColumn("ItemCode");
        //dt.Columns.Add(dc);
        //dc = new DataColumn("Description");
        //dt.Columns.Add(dc);
        int colDur = 1;
        int nextDur = duration;

        for (int i = 0; i < noOfColumns; i++)
        {
            //nextDur = nextDur + duration;
            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")");
            dt.Columns.Add(dc);
            colDur = nextDur + 1;
            nextDur = nextDur + duration;
        }

        dc = new DataColumn("Days(" + colDur + ") Above");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        return ds;

    }

    protected void gvOuts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes["align"] = "Left";
                e.Row.Cells[1].Attributes["align"] = "Left";
                int noofCols = int.Parse(txtColumns.Text) + 2;
                //for (int i = 2; i <= noofCols; i++)
                //{
                //    e.Row.Cells[i].Attributes["align"] = "Right";
                //}
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public DataSet UpdatePurchaseData(DataSet dsGrid)
    {
        BusinessLogic objBL = new BusinessLogic(sDataSource);

        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        //DataSet productData = rpt.GetProductData(sDataSource);
        DataSet productData = objBL.ListProdMdlItcd("ProductName");

        DateTime startDate ;
        DateTime endDate ;

        DateTime stdt = Convert.ToDateTime(txtStartDate.Text);
        DateTime etdt = Convert.ToDateTime(txtEndDate.Text);

        if (Request.QueryString["startDate"] != null)
        {
            stdt = Convert.ToDateTime(Request.QueryString["startDate"].ToString());
        }
        if (Request.QueryString["endDate"] != null)
        {
            etdt = Convert.ToDateTime(Request.QueryString["endDate"].ToString());
        }

        startDate = Convert.ToDateTime(stdt);
        endDate = Convert.ToDateTime(etdt);

        //DateTime refDate = DateTime.Parse("01/05/2011");
        string selecteditemCode = "0";
        string Product = "";
        string Category = "";
        string Model = "";
        string Brand = "";
        int duration = int.Parse(txtDuration.Text);
        int noOfColumns = int.Parse(txtColumns.Text);
        string itemCode = "0";
        string Productname = "";
        string Categoryval = "";

        if (Request.QueryString["Product"] != null)
        {
            Product = Request.QueryString["Product"].ToString();
        }
        if (Request.QueryString["Category"] != null)
        {
            Category = Request.QueryString["Category"].ToString();
        }
        if (Request.QueryString["Model"] != null)
        {
            Model = Request.QueryString["Model"].ToString();
        }
        if (Request.QueryString["Brand"] != null)
        {
            Brand = Request.QueryString["Brand"].ToString();
        }
        if (Request.QueryString["Productname"] != null)
        {
            Productname = Request.QueryString["Productname"].ToString();
        }
        if (Request.QueryString["Categoryval"] != null)
        {
            Categoryval = Request.QueryString["Categoryval"].ToString();
        }
        if (Request.QueryString["itemCode"] != null)
        {
            itemCode = Request.QueryString["itemCode"].ToString();
        }
        int productindex = 0;
        if (Request.QueryString["duration"] != null)
        {
            duration = Convert.ToInt32(Request.QueryString["duration"].ToString());
        }
        if (Request.QueryString["noOfColumns"] != null)
        {
            noOfColumns = Convert.ToInt32(Request.QueryString["noOfColumns"].ToString());
        }
        if (Request.QueryString["productindex"] != null)
        {
            productindex = Convert.ToInt32(Request.QueryString["productindex"].ToString());
        }

        if (productindex > 0)
            selecteditemCode = itemCode;

        string Brandval = "";
        string Modelval = "";

        if (Request.QueryString["Brandval"] != null)
        {
            Brandval = Request.QueryString["Brandval"].ToString();
        }

        if (Request.QueryString["Modelval"] != null)
        {
            Modelval = Request.QueryString["Modelval"].ToString();
        }

        string condition = "";
        if (Categoryval != "0")
            condition = " C.CategoryName='" + Category + "' ";

        if (Modelval != "0")
        {
            if (Categoryval != "0")
                condition = condition + " And ";

            condition = condition + " M.Model='" + Model + "' ";           
        }

        if (Brandval != "0")
        {
            if ((Categoryval != "0") || (Modelval != "0"))
                condition = condition + " And ";

            condition = condition + " M.ProductDesc='" + Brand + "' ";            
        }

        if (Product != "" && Productname != "All")
        {
            if ((Categoryval != "0") || (Modelval != "0") || (Brandval != "0"))
                condition = condition + " And ";

            condition = condition + " PI.itemcode='" + Product + "' ";         
        }

        //DataSet purchaseData = rpt.GetPurchaseData(sDataSource, selecteditemCode);
        DataSet purchaseData = objBL.GetPurchaseDataNew(sDataSource, selecteditemCode, condition);

        DataView dv = purchaseData.Tables[0].AsDataView();

        //////if (Categoryval != "0")
        //////    dv.RowFilter = "CategoryName=" + Category + "";

        //////if (Modelval != "0")
        //////    dv.RowFilter = "Model=" + Model + "";

        //////if (Brandval != "0")
        //////    dv.RowFilter = "ProductDesc=" + Brand + "";

        //////if (Product != "" && Productname != "All")
        //////    dv.RowFilter = "itemcode=" + Product + "";

        


        if (Request.QueryString["firstLevel"] != null)
        {
            firstLevel = Request.QueryString["firstLevel"].ToString();
        }
        if (Request.QueryString["secondLevel"] != null)
        {
            secondLevel = Request.QueryString["secondLevel"].ToString();
        }
        if (Request.QueryString["thirdLevel"] != null)
        {
            thirdLevel = Request.QueryString["thirdLevel"].ToString();
        }
        if (Request.QueryString["fourthLevel"] != null)
        {
            fourthLevel = Request.QueryString["fourthLevel"].ToString();
        }
        if (Request.QueryString["duration"] != null)
        {
            duration = Convert.ToInt32(Request.QueryString["duration"].ToString());
        }
        if (Request.QueryString["noOfColumns"] != null)
        {
            noOfColumns = Convert.ToInt32(Request.QueryString["noOfColumns"].ToString());
        }

        int maxColIndex = dsGrid.Tables[0].Columns.Count - 1;

        foreach (DataRow dr in dv.ToTable().Rows)
        {
            bool dupFlag = false;
            string fiLevel = "";
            string seLevel = "";
            string thLevel = "";
            string foLevel = "";
            if (firstLevel != "None")
            {
                fiLevel = dr[firstLevel].ToString();
            }
            if (secondLevel != "None")
            {
                seLevel = dr[secondLevel].ToString();
            }
            if (thirdLevel != "None")
            {
                thLevel = dr[thirdLevel].ToString();
            }
            if (fourthLevel != "None")
            {
                foLevel = dr[fourthLevel].ToString();
            }

            string itemCode1 = dr["ItemCode"].ToString();
            string Description = dr["ProductName"].ToString();
            DateTime purchaseDate = DateTime.Parse(dr["BillDate"].ToString());
            //DateTimeHelper.DateDifference dateHelper = new DateTimeHelper.DateDifference(refDate, purchaseDate);

            int diffDays = int.Parse((endDate - purchaseDate).TotalDays.ToString());
            int rowIndex = 0;

            foreach (DataRow dR in dsGrid.Tables[0].Rows)
            {
                if (dR["itemCode"] != null)
                {
                    if (dR["itemCode"].ToString().Trim() == itemCode1.Trim())
                    {
                        dupFlag = true;
                        break;
                    }
                    rowIndex++;
                }
            }
            int colDur = 1;
            int nextDur = duration;

            if (dupFlag)
            {
                int colIndex = diffDays / duration;
                colIndex = colIndex + 2;
                

                //if (colIndex >= maxColIndex)
                //{
                //    colIndex = maxColIndex;
                //}

                for (int i = 0; i < noOfColumns; i++)
                {
                    //nextDur = nextDur + duration;
                    //dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")");
                    //dt.Columns.Add(dc);

                    if ((diffDays >= colDur) && (diffDays <= nextDur))
                    {
                        colIndex = 4;
                        break;
                    }
                    else if ((diffDays >= colDur) && (diffDays <= nextDur))
                    {
                        colIndex = 5;
                        break;
                    }
                    else if ((diffDays >= colDur) && (diffDays <= nextDur))
                    {
                        colIndex = 6;
                        break;
                    }
                    else if ((diffDays >= colDur) && (diffDays <= nextDur))
                    {
                        colIndex = 7;
                        break;
                    }
                    else if (diffDays >= colDur)
                    {
                        colIndex = 8;
                        break;
                    }

                    colDur = nextDur + 1;
                    nextDur = nextDur + duration;
                }

                

                double currQty = 0;

                if (dsGrid.Tables[0].Rows[rowIndex] != null)
                {
                    if (dsGrid.Tables[0].Rows[rowIndex][colIndex] != null)
                    {
                        string tt = dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString();
                        if (dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString() != "")
                        {
                            if (colIndex > 4)
                                currQty = Convert.ToDouble(dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString());
                        }
                    }
                }

                double totQty = currQty + double.Parse(dr["Qty"].ToString());

                dsGrid.Tables[0].Rows[rowIndex][colIndex] = totQty;
                dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                dsGrid.Tables[0].AcceptChanges();
            }
            else
            {
                DataRow gridRow = dsGrid.Tables[0].NewRow();
                if (firstLevel != "None")
                    gridRow[firstLevel] = fiLevel;
                if (secondLevel != "None")
                    gridRow[secondLevel] = seLevel;
                if (thirdLevel != "None")
                    gridRow[thirdLevel] = thLevel;
                if (fourthLevel != "None")
                    gridRow[fourthLevel] = foLevel;

                if (selLevels.IndexOf("ProductDesc") < 0)
                    gridRow["ProductDesc"] = dr["ProductDesc"].ToString();
                if (selLevels.IndexOf("CategoryName") < 0)
                    gridRow["CategoryName"] = dr["CategoryName"].ToString();
                if (selLevels.IndexOf("Model") < 0)
                    gridRow["Model"] = dr["Model"].ToString();
                if (selLevels.IndexOf("ItemCode") < 0)
                    gridRow["ItemCode"] = dr["ItemCode"].ToString();

                int colIndex = diffDays / duration;
                colIndex = colIndex + 2;
                //if (colIndex >= maxColIndex)
                //{
                //    colIndex = maxColIndex;
                //}
                for (int i = 0; i < noOfColumns; i++)
                {
                    if ((diffDays >= colDur) && (diffDays <= nextDur))
                    {
                        colIndex = 4;
                        break;
                    }
                    else if ((diffDays >= colDur) && (diffDays <= nextDur))
                    {
                        colIndex = 5;
                        break;
                    }
                    else if ((diffDays >= colDur) && (diffDays <= nextDur))
                    {
                        colIndex = 6;
                        break;
                    }
                    else if ((diffDays >= colDur) && (diffDays <= nextDur))
                    {
                        colIndex = 7;
                        break;
                    }
                    else if (diffDays >= colDur)
                    {
                        colIndex = 8;
                        break;
                    }

                    colDur = nextDur + 1;
                    nextDur = nextDur + duration;
                }

                gridRow[colIndex] = dr["Qty"].ToString();

                dsGrid.Tables[0].Rows.Add(gridRow);
            }

        }

        return dsGrid;
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (!isValidLevels())
            {
                return;
            }
            bindData();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void bindData()
    {
        firstLevel = ddlFirstLvl.SelectedValue;
        secondLevel = ddlSecondLvl.SelectedValue;
        thirdLevel = ddlThirdLvl.SelectedValue;
        fourthLevel = ddlFourthLvl.SelectedValue;
        bool dispLastTotal = false;

        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet ds = GenerateGridColumns();
        ds = UpdatePurchaseData(ds);
        ds = UpdateSalesData(ds);
        ds = ConsolidateData(ds);
        DataTable dts = new DataTable();

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ddlFirstLvl.SelectedIndex > 0)
            {
                dts.Columns.Add(new DataColumn(ddlFirstLvl.SelectedItem.Text));
            }
            if (ddlSecondLvl.SelectedIndex > 0)
            {
                dts.Columns.Add(new DataColumn(ddlSecondLvl.SelectedItem.Text));
            }
            if (ddlThirdLvl.SelectedIndex > 0)
            {
                dts.Columns.Add(new DataColumn(ddlThirdLvl.SelectedItem.Text));
            }
            if (ddlFourthLvl.SelectedIndex > 0)
            {
                dts.Columns.Add(new DataColumn(ddlFourthLvl.SelectedItem.Text));
            }
            if (selLevels.IndexOf("ProductDesc") < 0)
                dts.Columns.Add(new DataColumn("ProductDesc"));
            if (selLevels.IndexOf("CategoryName") < 0)
                dts.Columns.Add(new DataColumn("CategoryName"));
            if (selLevels.IndexOf("Model") < 0)
                dts.Columns.Add(new DataColumn("Model"));
            if (selLevels.IndexOf("ItemCode") < 0)
                dts.Columns.Add(new DataColumn("ItemCode"));

            int columnNo = ds.Tables[0].Columns.Count - 5;
            int colDur = 0, nextDur = 0;
            int duration = int.Parse(txtDuration.Text);
            int noOfColumns = int.Parse(txtColumns.Text);
            DataTable dt = new DataTable();
            DataColumn dc;

            for (int i = 0; i < columnNo; i++)
            {
                nextDur = nextDur + duration;
                dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")");
                dts.Columns.Add(dc);
                colDur = nextDur + 1;
            }
            dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
            dts.Columns.Add(dc);
            //dts.Columns.Add(new DataColumn("Total"));

            //initialize column values for entire row
            string fLvlValue = "", sLvlValue = "", tLvlValue = "", frthLvlValue = "";
            string fLvlValueTemp = "", sLvlValueTemp = "", tLvlValueTemp = "", frthLvlValueTemp = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //initialize column values for entire row 
                if (ddlFirstLvl.SelectedIndex > 0)
                    fLvlValueTemp = dr[ddlFirstLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                if (ddlSecondLvl.SelectedIndex > 0)
                    sLvlValueTemp = dr[ddlSecondLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                if (ddlThirdLvl.SelectedIndex > 0)
                    tLvlValueTemp = dr[ddlThirdLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                if (ddlFourthLvl.SelectedIndex > 0)
                    frthLvlValueTemp = dr[ddlFourthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                dispLastTotal = true;
                if (ddlFourthLvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                        (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp))
                    {
                        DataRow dr_final7 = dts.NewRow();
                        if (ddlFirstLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFourthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlFourthLvl.SelectedItem.Text] = "Total:" + frthLvlValue;
                        }
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final7["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryName") < 0)
                            dr_final7["CategoryName"] = "";
                        if (selLevels.IndexOf("Model") < 0)
                            dr_final7["Model"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final7["ItemCode"] = "";

                        colDur = 0;
                        nextDur = 0;
                        dt = new DataTable();

                        for (int i = 0; i < columnNo; i++)
                        {
                            nextDur = nextDur + duration;
                            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                            dt.Columns.Add(dc);
                            colDur = nextDur + 1;
                        }
                        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                        dt.Columns.Add(dc);
                        ds.Tables.Add(dt);

                        // dr_final7["Total"] = Convert.ToString(Convert.ToDecimal(Pttls));
                        dts.Rows.Add(dr_final7);
                        Pttls = 0;
                    }
                }

                if (ddlThirdLvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final8 = dts.NewRow();
                        if (ddlFirstLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlThirdLvl.SelectedItem.Text] = "Total " + tLvlValue;
                        }
                        if (ddlFourthLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlFourthLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final8["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryName") < 0)
                            dr_final8["CategoryName"] = "";
                        if (selLevels.IndexOf("Model") < 0)
                            dr_final8["Model"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final8["ItemCode"] = "";

                        colDur = 0;
                        nextDur = 0;
                        dt = new DataTable();

                        for (int i = 0; i < columnNo; i++)
                        {
                            nextDur = nextDur + duration;
                            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                            dt.Columns.Add(dc);
                            colDur = nextDur + 1;
                        }
                        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                        dt.Columns.Add(dc);
                        ds.Tables.Add(dt);

                        //dr_final8["Total"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                        dts.Rows.Add(dr_final8);
                        modelTotal = 0;
                    }
                }

                if (ddlSecondLvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final8 = dts.NewRow();
                        if (ddlFirstLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlSecondLvl.SelectedItem.Text] = "Total " + sLvlValue;
                        }
                        if (ddlThirdLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFourthLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlFourthLvl.SelectedItem.Text] = "";
                        }

                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final8["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryName") < 0)
                            dr_final8["CategoryName"] = "";
                        if (selLevels.IndexOf("Model") < 0)
                            dr_final8["Model"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final8["ItemCode"] = "";

                        colDur = 0;
                        nextDur = 0;
                        dt = new DataTable();

                        for (int i = 0; i < columnNo; i++)
                        {
                            nextDur = nextDur + duration;
                            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                            dt.Columns.Add(dc);
                            colDur = nextDur + 1;
                        }
                        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                        dt.Columns.Add(dc);
                        ds.Tables.Add(dt);

                        //dr_final8["Total"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                        dts.Rows.Add(dr_final8);
                        catIDTotal = 0;
                    }
                }
                if (ddlFirstLvl.SelectedIndex > 0)
                {
                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dts.NewRow();
                        if (ddlFirstLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlFirstLvl.SelectedItem.Text] = "Total " + fLvlValue;
                        }
                        if (ddlSecondLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFourthLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlFourthLvl.SelectedItem.Text] = "";
                        }

                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final8["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryName") < 0)
                            dr_final8["CategoryName"] = "";
                        if (selLevels.IndexOf("Model") < 0)
                            dr_final8["Model"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final8["ItemCode"] = "";

                        colDur = 0;
                        nextDur = 0;
                        dt = new DataTable();

                        for (int i = 0; i < columnNo; i++)
                        {
                            nextDur = nextDur + duration;
                            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                            dt.Columns.Add(dc);
                            colDur = nextDur + 1;
                        }
                        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                        dt.Columns.Add(dc);
                        ds.Tables.Add(dt);

                        //dr_final8["Total"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                        dts.Rows.Add(dr_final8);
                        brandTotal = 0;
                    }
                }

                if (ddlFirstLvl.SelectedIndex > 0)
                {
                    if (fLvlValueTemp != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final1 = dts.NewRow();
                        if (ddlFirstLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlFirstLvl.SelectedItem.Text] = fLvlValueTemp;
                        }
                        if (ddlSecondLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFourthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlFourthLvl.SelectedItem.Text] = "";
                        }

                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final1["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryName") < 0)
                            dr_final1["CategoryName"] = "";
                        if (selLevels.IndexOf("Model") < 0)
                            dr_final1["Model"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final1["ItemCode"] = "";

                        colDur = 0;
                        nextDur = 0;
                        dt = new DataTable();

                        for (int i = 0; i < columnNo; i++)
                        {
                            nextDur = nextDur + duration;
                            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                            dt.Columns.Add(dc);
                            colDur = nextDur + 1;
                        }
                        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                        dt.Columns.Add(dc);
                        ds.Tables.Add(dt);

                        //dr_final1["Total"] = "";
                        dts.Rows.Add(dr_final1);
                    }
                }

                if (ddlSecondLvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final2 = dts.NewRow();
                        if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp))
                        {
                            if (ddlSecondLvl.SelectedIndex > 0)
                            {
                                dr_final2[ddlSecondLvl.SelectedItem.Text] = sLvlValueTemp;
                            }
                        }
                        if (ddlFirstLvl.SelectedIndex > 0)
                        {
                            dr_final2[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedIndex > 0)
                        {
                            dr_final2[ddlSecondLvl.SelectedItem.Text] = sLvlValueTemp;
                        }
                        if (ddlThirdLvl.SelectedIndex > 0)
                        {
                            dr_final2[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFourthLvl.SelectedIndex > 0)
                        {
                            dr_final2[ddlFourthLvl.SelectedItem.Text] = "";
                        }

                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final2["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryName") < 0)
                            dr_final2["CategoryName"] = "";
                        if (selLevels.IndexOf("Model") < 0)
                            dr_final2["Model"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final2["ItemCode"] = "";

                        colDur = 0;
                        nextDur = 0;
                        dt = new DataTable();

                        for (int i = 0; i < columnNo; i++)
                        {
                            nextDur = nextDur + duration;
                            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                            dt.Columns.Add(dc);
                            colDur = nextDur + 1;
                        }
                        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                        dt.Columns.Add(dc);
                        ds.Tables.Add(dt);

                        //dr_final2["Total"] = "";
                        dts.Rows.Add(dr_final2);
                    }
                }

                if (ddlThirdLvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final1 = dts.NewRow();
                        if (fLvlValueTemp != "" && fLvlValue != fLvlValueTemp)
                        {
                            if (ddlThirdLvl.SelectedIndex > 0)
                            {
                                dr_final1[ddlThirdLvl.SelectedItem.Text] = tLvlValueTemp;
                            }
                        }
                        //else
                        //{
                        if (ddlFirstLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlThirdLvl.SelectedItem.Text] = tLvlValueTemp;
                        }
                        if (ddlFourthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlFourthLvl.SelectedItem.Text] = "";
                        }

                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final1["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryName") < 0)
                            dr_final1["CategoryName"] = "";
                        if (selLevels.IndexOf("Model") < 0)
                            dr_final1["Model"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final1["ItemCode"] = "";

                        colDur = 0;
                        nextDur = 0;
                        dt = new DataTable();

                        for (int i = 0; i < columnNo; i++)
                        {
                            nextDur = nextDur + duration;
                            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                            dt.Columns.Add(dc);
                            colDur = nextDur + 1;
                        }
                        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                        dt.Columns.Add(dc);
                        ds.Tables.Add(dt);

                        //dr_final1["Total"] = "";
                        dts.Rows.Add(dr_final1);
                    }
                }

                if (ddlFourthLvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp) ||
                        (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp))
                    {
                        DataRow dr_final1 = dts.NewRow();
                        if (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp)
                        {
                            if (ddlFourthLvl.SelectedIndex > 0)
                            {
                                dr_final1[ddlFourthLvl.SelectedItem.Text] = frthLvlValueTemp;
                            }
                            else
                            {
                                if (ddlFirstLvl.SelectedIndex > 0)
                                {
                                    dr_final1[ddlFirstLvl.SelectedItem.Text] = "";
                                }
                                if (ddlSecondLvl.SelectedIndex > 0)
                                {
                                    dr_final1[ddlSecondLvl.SelectedItem.Text] = "";
                                }
                                if (ddlThirdLvl.SelectedIndex > 0)
                                {
                                    dr_final1[ddlThirdLvl.SelectedItem.Text] = "";
                                }
                                if (ddlFourthLvl.SelectedIndex > 0)
                                {
                                    dr_final1[ddlFourthLvl.SelectedItem.Text] = frthLvlValueTemp;
                                }
                            }
                        }


                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final1["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryName") < 0)
                            dr_final1["CategoryName"] = "";
                        if (selLevels.IndexOf("Model") < 0)
                            dr_final1["Model"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final1["ItemCode"] = "";

                        colDur = 0;
                        nextDur = 0;
                        dt = new DataTable();

                        for (int i = 0; i < columnNo; i++)
                        {
                            nextDur = nextDur + duration;
                            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                            dt.Columns.Add(dc);
                            colDur = nextDur + 1;
                        }
                        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                        dt.Columns.Add(dc);
                        ds.Tables.Add(dt);

                        //dr_final1["Total"] = "";
                        dts.Rows.Add(dr_final1);
                    }
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;
                frthLvlValue = frthLvlValueTemp;
                DataRow dr_final5 = dts.NewRow();
                if (ddlFirstLvl.SelectedIndex > 0)
                {

                    dr_final5[ddlFirstLvl.SelectedItem.Text] = "";
                }

                if (ddlSecondLvl.SelectedIndex > 0)
                {
                    dr_final5[ddlSecondLvl.SelectedItem.Text] = "";
                }
                if (ddlThirdLvl.SelectedIndex > 0)
                {
                    dr_final5[ddlThirdLvl.SelectedItem.Text] = "";
                }
                if (ddlFourthLvl.SelectedIndex > 0)
                {
                    dr_final5[ddlFourthLvl.SelectedItem.Text] = "";
                }
                if (selLevels.IndexOf("ProductDesc") < 0)
                    dr_final5["ProductDesc"] = dr["ProductDesc"].ToString();
                if (selLevels.IndexOf("CategoryName") < 0)
                    dr_final5["CategoryName"] = dr["CategoryName"].ToString();
                if (selLevels.IndexOf("Model") < 0)
                    dr_final5["Model"] = dr["Model"].ToString();
                if (selLevels.IndexOf("ItemCode") < 0)
                    dr_final5["ItemCode"] = dr["ItemCode"].ToString();

                colDur = 0;
                nextDur = 0;
                dt = new DataTable();

                for (int i = 0; i < columnNo; i++)
                {
                    nextDur = nextDur + duration;
                    dr_final5["Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")"] = dr["Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")"].ToString();
                    colDur = nextDur + 1;
                }
                dr_final5["Days(" + nextDur.ToString() + ") Above"] = dr[("Days(" + nextDur.ToString() + ") Above")];

                //dr_final5["Total"] = dr["Total"];
                dts.Rows.Add(dr_final5);
                //Gtotal = Gtotal + Convert.ToDecimal(dr["Total"]);
                //modelTotal = modelTotal + Convert.ToDecimal(dr["Total"]);
                //catIDTotal = catIDTotal + Convert.ToDecimal(dr["Total"]);
                //Pttls = Pttls + Convert.ToDecimal(dr["Total"]);
                //brandTotal = brandTotal + Convert.ToDecimal(dr["Total"]);
            }
            //Display the last Total and Grand Total
            if (dispLastTotal)
            {
                if (ddlFourthLvl.SelectedIndex > 0)
                {
                    DataRow dr_final7 = dts.NewRow();
                    if (ddlFirstLvl.SelectedIndex > 0)
                    {
                        dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    }
                    if (ddlSecondLvl.SelectedIndex > 0)
                    {
                        dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    }
                    if (ddlThirdLvl.SelectedIndex > 0)
                    {
                        dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                    }
                    dr_final7[ddlFourthLvl.SelectedItem.Text] = "Total:" + frthLvlValueTemp;

                    if (selLevels.IndexOf("ProductDesc") < 0)
                        dr_final7["ProductDesc"] = "";
                    if (selLevels.IndexOf("CategoryName") < 0)
                        dr_final7["CategoryName"] = "";
                    if (selLevels.IndexOf("Model") < 0)
                        dr_final7["Model"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final7["ItemCode"] = "";

                    colDur = 0;
                    nextDur = 0;
                    dt = new DataTable();

                    for (int i = 0; i < columnNo; i++)
                    {
                        nextDur = nextDur + duration;
                        dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                        dt.Columns.Add(dc);
                        colDur = nextDur + 1;
                    }
                    dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                    dt.Columns.Add(dc);
                    ds.Tables.Add(dt);

                    //dr_final7["Total"] = Convert.ToString(Convert.ToDecimal(Pttls));
                    dts.Rows.Add(dr_final7);
                    Pttls = 0;
                }

                if (ddlThirdLvl.SelectedIndex > 0)
                {
                    DataRow dr_final8 = dts.NewRow();
                    if (ddlFirstLvl.SelectedIndex > 0)
                    {
                        dr_final8[ddlFirstLvl.SelectedItem.Text] = "";
                    }
                    if (ddlSecondLvl.SelectedIndex > 0)
                    {
                        dr_final8[ddlSecondLvl.SelectedItem.Text] = "";
                    }
                    if (ddlThirdLvl.SelectedIndex > 0)
                    {
                        dr_final8[ddlThirdLvl.SelectedItem.Text] = "Total: " + tLvlValueTemp;
                    }
                    if (ddlFourthLvl.SelectedIndex > 0)
                    {
                        dr_final8[ddlFourthLvl.SelectedItem.Text] = "";
                    }

                    if (selLevels.IndexOf("ProductDesc") < 0)
                        dr_final8["ProductDesc"] = "";
                    if (selLevels.IndexOf("CategoryName") < 0)
                        dr_final8["CategoryName"] = "";
                    if (selLevels.IndexOf("Model") < 0)
                        dr_final8["Model"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final8["ItemCode"] = "";

                    colDur = 0;
                    nextDur = 0;
                    dt = new DataTable();

                    for (int i = 0; i < columnNo; i++)
                    {
                        nextDur = nextDur + duration;
                        dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                        dt.Columns.Add(dc);
                        colDur = nextDur + 1;
                    }
                    dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                    dt.Columns.Add(dc);
                    ds.Tables.Add(dt);

                    //dr_final8["Total"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                    dts.Rows.Add(dr_final8);
                    modelTotal = 0;
                }

                if (ddlSecondLvl.SelectedIndex > 0)
                {
                    DataRow dr_final9 = dts.NewRow();
                    if (ddlFirstLvl.SelectedIndex > 0)
                    {
                        dr_final9[ddlFirstLvl.SelectedItem.Text] = "";
                    }
                    if (ddlSecondLvl.SelectedIndex > 0)
                    {
                        dr_final9[ddlSecondLvl.SelectedItem.Text] = "Total: " + sLvlValueTemp;
                    }
                    if (ddlThirdLvl.SelectedIndex > 0)
                    {
                        dr_final9[ddlThirdLvl.SelectedItem.Text] = "";
                    }
                    if (ddlFourthLvl.SelectedIndex > 0)
                    {
                        dr_final9[ddlFourthLvl.SelectedItem.Text] = "";
                    }

                    if (selLevels.IndexOf("ProductDesc") < 0)
                        dr_final9["ProductDesc"] = "";
                    if (selLevels.IndexOf("CategoryName") < 0)
                        dr_final9["CategoryName"] = "";
                    if (selLevels.IndexOf("Model") < 0)
                        dr_final9["Model"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final9["ItemCode"] = "";

                    colDur = 0;
                    nextDur = 0;
                    dt = new DataTable();

                    for (int i = 0; i < columnNo; i++)
                    {
                        nextDur = nextDur + duration;
                        dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                        dt.Columns.Add(dc);
                        colDur = nextDur + 1;
                    }
                    dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                    dt.Columns.Add(dc);
                    ds.Tables.Add(dt);

                    //dr_final9["Total"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                    dts.Rows.Add(dr_final9);
                    catIDTotal = 0;
                }

                if (ddlFirstLvl.SelectedIndex > 0)
                {
                    DataRow dr_final10 = dts.NewRow();
                    if (ddlFirstLvl.SelectedIndex > 0)
                    {
                        dr_final10[ddlFirstLvl.SelectedItem.Text] = "Total: " + fLvlValueTemp;
                    }
                    if (ddlSecondLvl.SelectedIndex > 0)
                    {
                        dr_final10[ddlSecondLvl.SelectedItem.Text] = "";
                    }
                    if (ddlThirdLvl.SelectedIndex > 0)
                    {
                        dr_final10[ddlThirdLvl.SelectedItem.Text] = "";
                    }
                    if (ddlFourthLvl.SelectedIndex > 0)
                    {
                        dr_final10[ddlFourthLvl.SelectedItem.Text] = "";
                    }

                    if (selLevels.IndexOf("ProductDesc") < 0)
                        dr_final10["ProductDesc"] = "";
                    if (selLevels.IndexOf("CategoryName") < 0)
                        dr_final10["CategoryName"] = "";
                    if (selLevels.IndexOf("Model") < 0)
                        dr_final10["Model"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final10["ItemCode"] = "";

                    colDur = 0;
                    nextDur = 0;
                    dt = new DataTable();

                    for (int i = 0; i < columnNo; i++)
                    {
                        nextDur = nextDur + duration;
                        dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
                        dt.Columns.Add(dc);
                        colDur = nextDur + 1;
                    }
                    dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
                    dt.Columns.Add(dc);
                    ds.Tables.Add(dt);

                    //dr_final10["Total"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                    dts.Rows.Add(dr_final10);
                    brandTotal = 0;
                }

                DataRow dr_final6 = dts.NewRow();
                if (ddlFirstLvl.SelectedIndex > 0)
                {
                    dr_final6[ddlFirstLvl.SelectedItem.Text] = "Grand Total: ";
                }
                if (ddlSecondLvl.SelectedIndex > 0)
                {
                    dr_final6[ddlSecondLvl.SelectedItem.Text] = "";
                }
                if (ddlThirdLvl.SelectedIndex > 0)
                {
                    dr_final6[ddlThirdLvl.SelectedItem.Text] = "";
                }
                if (ddlFourthLvl.SelectedIndex > 0)
                {
                    dr_final6[ddlFourthLvl.SelectedItem.Text] = "";
                }
                if (selLevels.IndexOf("ProductDesc") < 0)
                    dr_final6["ProductDesc"] = "";
                if (selLevels.IndexOf("CategoryName") < 0)
                    dr_final6["CategoryName"] = "";
                if (selLevels.IndexOf("Model") < 0)
                    dr_final6["Model"] = "";
                if (selLevels.IndexOf("ItemCode") < 0)
                    dr_final6["ItemCode"] = "";

                // dr_final6["Total"] = Convert.ToDecimal(Gtotal);
                dts.Rows.Add(dr_final6);
            }
            ExportToExcel(dts);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            string filename = "StockagingDownloading.xls";
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
    public DataSet UpdateSalesData(DataSet dsGrid)
    {
        BusinessLogic objBL = new BusinessLogic(sDataSource);

        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        //DataSet productData = rpt.GetProductData(sDataSource);
        DataSet productData = objBL.ListProdMdlItcd("ProductName");



        DateTime startDate;
        DateTime endDate;

        DateTime stdt = Convert.ToDateTime(txtStartDate.Text);
        DateTime etdt = Convert.ToDateTime(txtEndDate.Text);

        if (Request.QueryString["startDate"] != null)
        {
            stdt = Convert.ToDateTime(Request.QueryString["startDate"].ToString());
        }
        if (Request.QueryString["endDate"] != null)
        {
            etdt = Convert.ToDateTime(Request.QueryString["endDate"].ToString());
        }

        startDate = Convert.ToDateTime(stdt);
        endDate = Convert.ToDateTime(etdt);


        //DateTime refDate = DateTime.Parse("01/05/2011");

        string selecteditemCode = "0";
        string Product = "0";
        if (Request.QueryString["Product"] != null)
        {
            Product = Request.QueryString["Product"].ToString();
        }

        int productindex = 0;
        if (Request.QueryString["productindex"] != null)
        {
            productindex = Convert.ToInt32(Request.QueryString["productindex"].ToString());
        }

        if (productindex > 0)
            selecteditemCode = Product;

        DataSet salesData = rpt.GetSalesData(sDataSource, selecteditemCode);
        //DataSet salesData = objBL.GetSalesData(sDataSource, selecteditemCode);

        int duration = int.Parse(txtDuration.Text);
        int noOfColumns = int.Parse(txtColumns.Text);
        if (Request.QueryString["duration"] != null)
        {
            duration = Convert.ToInt32(Request.QueryString["duration"].ToString());
        }
        if (Request.QueryString["noOfColumns"] != null)
        {
            noOfColumns = Convert.ToInt32(Request.QueryString["noOfColumns"].ToString());
        }

        int maxColIndex = dsGrid.Tables[0].Columns.Count - 1;

        foreach (DataRow dr in salesData.Tables[0].Rows)
        {
            bool dupFlag = false;

            string itemCode = dr["ItemCode"].ToString();
            string Description = dr["ProductName"].ToString();
            DateTime purchaseDate = DateTime.Parse(dr["BillDate"].ToString());

            //int diffDays = int.Parse(( endDate - purchaseDate).TotalDays.ToString());
            int rowIndex = 0;

            foreach (DataRow dR in dsGrid.Tables[0].Rows)
            {
                if (dR["itemCode"] != null)
                {
                    if (dR["itemCode"].ToString().Trim() == itemCode.Trim())
                    {
                        dupFlag = true;
                        break;
                    }
                    rowIndex++;
                }
            }

            if (dupFlag)
            {
                //int colIndex = diffDays / duration;
                //colIndex = colIndex + 2;

                //if (colIndex >= maxColIndex)
                //{
                //    colIndex = maxColIndex;
                //}

                double currQty = double.Parse(dr["Qty"].ToString());

                for (int i = dsGrid.Tables[0].Columns.Count - 1; i > 1; i--)
                {
                    double purchaseQty = 0.0;

                    if (currQty > 0)
                    {
                        if (dsGrid.Tables[0].Rows[rowIndex][i] != null)
                        {
                            if (dsGrid.Tables[0].Rows[rowIndex][i].ToString() != "")
                            {
                                if (i > 3)
                                {
                                    purchaseQty = double.Parse(dsGrid.Tables[0].Rows[rowIndex][i].ToString());
                                    purchaseQty = currQty - purchaseQty;
                                }

                                if (purchaseQty > 0)
                                {
                                    dsGrid.Tables[0].Rows[rowIndex][i] = "0";
                                    dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                    dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                }
                                else
                                {
                                    dsGrid.Tables[0].Rows[rowIndex][i] = -(purchaseQty);
                                    dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                    dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                }

                                currQty = purchaseQty;
                            }
                        }
                    }
                    //if (i == 2)
                    //{
                    //    dsGrid.Tables[0].Rows[rowIndex][i] = currQty;
                    //    dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                    //    dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                    //}
                }
            }


        }

        return dsGrid;
    }
    private bool isValidLevels()
    {
        if ((ddlFirstLvl.SelectedItem.Text != "None") &&
            (ddlFirstLvl.SelectedItem.Text == ddlSecondLvl.SelectedItem.Text ||
            ddlFirstLvl.SelectedItem.Text == ddlThirdLvl.SelectedItem.Text ||
            ddlFirstLvl.SelectedItem.Text == ddlFourthLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddlSecondLvl.SelectedItem.Text != "None") &&
            (ddlSecondLvl.SelectedItem.Text == ddlThirdLvl.SelectedItem.Text ||
            ddlSecondLvl.SelectedItem.Text == ddlFourthLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddlThirdLvl.SelectedItem.Text != "None") &&
            (ddlThirdLvl.SelectedItem.Text == ddlFourthLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        return true;
    }
}
