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
using AjaxControlToolkit;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class OutStandingMasterReport : System.Web.UI.Page
{
    string selLevels = "";
    decimal Gtotal = 0;
    decimal Gttl = 0;
    decimal Pttls = 0;
    decimal Pttl = 0;
    decimal brandTotal = 0, catIDTotal = 0, modelTotal = 0;

    public Double damt = 0.0;
    public Double camt = 0.0;
    public Double dDiffamt = 0.0;
    public Double cDiffamt = 0.0;
    public Double ddamt = 0.0;

    double debitnew=0;

    string firstLevel = "";
    string secondLevel = "";
    string thirdLevel = "";
    string fourthLevel = "";
    string fifthLevel = "";


    public string sDataSource = string.Empty;
    public int lastrow = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Write("<script language='javascript'> { window.close(); }</script>");

            if (!Page.IsPostBack)
            {
                divPrint.Visible = false;
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);

                //if (Request.Cookies["Company"] != null)
                //{
                //    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);
                companyInfo = bl.getCompanyInfo("");
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

                loadBranch();

                txtDuration.Text = "7";
                txtColumns.Text = "4";
                loadCategories();
                loadModels();
                loadBrands();
                //loadEmp();
                loadProducts();

                fillDdl(ddlFirstLvl);
                fillDdl(ddlSecondLvl);
                fillDdl(ddlThirdLvl);
                fillDdl(ddlFourthLvl);
                fillDdl(ddlFifthLvl);
                //divPrint.Visible = true;

                Label2.Visible = false;
                Label3.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void fillDdl(DropDownList ddlist)
    {
        //DropDownList ddlList = new DropDownList();,,
        ddlist.Items.Insert(0, "None");
        ddlist.Items.Insert(1, "Customer");
        ddlist.Items.Insert(2, "ProductDesc");
        ddlist.Items.Insert(3, "CategoryName");
        ddlist.Items.Insert(4, "Model");
        ddlist.Items.Insert(5, "ItemCode");
    }

    protected void btndet_Click(object sender, EventArgs e)
    {
        try
        {
            div1.Visible = true;
            divPrint.Visible = false;
            btndet.Visible = false;

            Button1.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnOutstand_Click(object sender, EventArgs e)
    {
        try
        {
            if (!isValidLevels())
            {
                return;
            }
            divPrint.Visible = false;
            btndet.Visible = false;

            Button1.Visible = false;

            div1.Visible = true;
            //DataSet dsGird = GenerateGridColumns();

            //DataSet ds = GetCustDebitData();
            //ds = GetReceivedAmount(ds);
            //ds = GetCreditData(ds);

            //dsGird = UpdateColumnsData(dsGird, ds);

            //DataSet dsFinal = ConsolidatedGridColumns();
            //ds = UpdateFinalData(dsGird, dsFinal);

            //ds = CalculateTotal(ds);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    gvReport.DataSource = ds;
            //    gvReport.DataBind();
            //    gvReport.Visible = true;
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            //}

            //DateTime startDate = DateTime.Parse(txtStartDate.Text);
            //DateTime endDate = DateTime.Parse(DateTime.Now.ToShortDateString());

            int ddl1 = ddlFirstLvl.SelectedIndex;
            int ddl2 = ddlSecondLvl.SelectedIndex;
            int ddl3 = ddlThirdLvl.SelectedIndex;
            int ddl4 = ddlFourthLvl.SelectedIndex;
            int ddl5 = ddlFifthLvl.SelectedIndex;
            string itemCode = cmbProduct.SelectedValue.Trim();
            string Category = cmbCategory.SelectedItem.Text;
            string Model = cmbModel.SelectedItem.Text;
            string Brand = cmbBrand.SelectedItem.Text;
            string Product = cmbProduct.SelectedValue;
            string Productname = cmbProduct.SelectedItem.Text;

            int duration = int.Parse(txtDuration.Text);
            int noOfColumns = int.Parse(txtColumns.Text);
            firstLevel = ddlFirstLvl.SelectedValue;
            secondLevel = ddlSecondLvl.SelectedValue;
            thirdLevel = ddlThirdLvl.SelectedValue;
            fourthLevel = ddlFourthLvl.SelectedValue;
            fifthLevel = ddlFifthLvl.SelectedValue;
            divPrint.Visible = false;
            string Catval = cmbCategory.SelectedValue;
            string Brandval = cmbCategory.SelectedValue;
            string Modelval = cmbCategory.SelectedValue;
            string Branch = drpBranchAdd.SelectedValue;

            Response.Write("<script language='javascript'> window.open('OutstandingMasterReport1.aspx?Catval=" + Catval + "&Brandval=" + Brandval + "&Modelval=" + Modelval + "&ddl3=" + ddl3 + "&ddl4=" + ddl4 + "&ddl5=" + ddl5 + "&ddl1=" + ddl1 + "&ddl2=" + ddl2 + "&Product=" + Product + "&Model=" + Model + "&Brand=" + Brand + "&itemCode=" + itemCode + "&Category=" + Category + "&firstLevel=" + firstLevel + "&secondLevel=" + secondLevel + "&thirdLevel=" + thirdLevel + "&fourthLevel=" + fourthLevel + "&fifthLevel=" + fifthLevel + "&duration=" + duration + "&noOfColumns=" + noOfColumns + "&Branch=" + Branch + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");



            //int iGroupID = 1;

            //BusinessLogic bl = new BusinessLogic(sDataSource);


            //if (chkboxpay.Checked == true)
            //{

            //    DataSet dst = new DataSet();
            //    dst = bl.generateOutStandingID(iGroupID, sDataSource);


            //    DataSet dsGirdd = GenerateGridColumnsNew();

            //    DataSet dso = GetCustDebitDataNew();
            //    dso = GetReceivedAmountNew(dso);
            //    dso = GetCreditDataNew(dso);

            //    dsGird = UpdateColumnsDataNew(dsGirdd, dso);

            //    DataSet dsF = ConsolidatedGridColumn();
            //    dsGird = UpdateFinalDat(dsGird, dsF);

            //    dsGird = CalculateTota(dsGird);

            //    dsGird = FMake(dsF,dsGird);

            //    if (dsGird.Tables[0].Rows.Count > 0)
            //    {
            //        gvLedger.Visible = true;
            //        gvLedger.DataSource = dsGird;
            //        gvLedger.DataBind();
            //    }

            //    //gvLedger.Visible = true;
            //    //gvLedger.DataSource = dst;
            //    //gvLedger.DataBind();
            //    Label2.Visible = true;
            //    Label6.Visible = true;
            //    Label10.Visible = true;
            //    //Label4.Visible = true;
            //    Label5.Visible = true;

            ////}
            ////else
            ////{
            //    DataSet dsty = new DataSet();
            //    dsty = bl.generateOutStandingIDPay(iGroupID, sDataSource);
            //    GVPay.Visible = true;
            //    Label3.Visible = true;
            //    GVPay.DataSource = dsty;
            //    GVPay.DataBind();
            //}

            //DataSet dstt = new DataSet();
            //dstt = bl.generateOutStandingIDTotal(iGroupID, sDataSource);

            //decimal svthLvlValueTemp;

            //if (dstt.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dstt.Tables[0].Rows)
            //    {
            //        svthLvlValueTemp = Convert.ToDecimal(dr["Maintotal"]);
            //        Label2.Text = svthLvlValueTemp.ToString();
            //    }
            //}

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    public DataSet GenerateGridColumnsNew()
    {
        selLevels = "";
        int duration = int.Parse(txtDuration.Text);
        int noOfColumns = int.Parse(txtColumns.Text);
        firstLevel = ddlFirstLvl.SelectedValue;
        secondLevel = ddlSecondLvl.SelectedValue;
        thirdLevel = ddlThirdLvl.SelectedValue;
        fourthLevel = ddlFourthLvl.SelectedValue;
        fifthLevel = ddlFifthLvl.SelectedValue;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn dc;

        if (ddlFirstLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(firstLevel);
            dt.Columns.Add(dc);
            selLevels += firstLevel;
        }

        if (ddlSecondLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(secondLevel);
            dt.Columns.Add(dc);
            selLevels += secondLevel;
        }

        if (ddlThirdLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(thirdLevel);
            dt.Columns.Add(dc);
            selLevels += thirdLevel;
        }

        if (ddlFourthLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(fourthLevel);
            dt.Columns.Add(dc);
            selLevels += fourthLevel;
        }

        if (ddlFifthLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(fifthLevel);
            dt.Columns.Add(dc);
            selLevels += fifthLevel;
        }

        if (selLevels.IndexOf("Customer") < 0)
            dt.Columns.Add(new DataColumn("Customer"));
        if (selLevels.IndexOf("ProductDesc") < 0)
            dt.Columns.Add(new DataColumn("ProductDesc"));
        if (selLevels.IndexOf("CategoryName") < 0)
            dt.Columns.Add(new DataColumn("CategoryName"));
        if (selLevels.IndexOf("Model") < 0)
            dt.Columns.Add(new DataColumn("Model"));
        if (selLevels.IndexOf("ItemCode") < 0)
            dt.Columns.Add(new DataColumn("ItemCode"));

        int colDur = 0;
        int nextDur = 0;

        for (int i = 0; i < noOfColumns; i++)
        {
            nextDur = nextDur + duration;
            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
            dt.Columns.Add(dc);
            colDur = nextDur + 1;
        }

        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        return ds;

    }

    public DataSet UpdateColumnsDataNew(DataSet dsGrid, DataSet debitData)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);


        DateTime endDate = DateTime.Parse(DateTime.Now.ToShortDateString());



        if (debitData != null)
        {

            //IEnumerable<DataRow> query = null; 

            //if(cmbCategory.SelectedValue != "0")
            //    query = from data in debitData.Tables[0].AsEnumerable() where data.Field<string>("CategoryName") == cmbCategory.SelectedItem.Text select data;
            //else
            //    query = from data in debitData.Tables[0].AsEnumerable() select data;

            //if (query.Count<DataRow>() > 0)
            if (true)
            {

                //DataTable dt = query.CopyToDataTable<DataRow>();

                DataTable dt = debitData.Tables[0];

                DataView dv = dt.AsDataView();

                if (cmbCategory.SelectedValue != "0")
                    dv.RowFilter = "CategoryName='" + cmbCategory.SelectedItem.Text + "'";

                if (cmbModel.SelectedValue != "0")
                    dv.RowFilter = "Model='" + cmbModel.SelectedItem.Text + "'";

                if (cmbBrand.SelectedValue != "0")
                    dv.RowFilter = "ProductDesc='" + cmbBrand.SelectedItem.Text + "'";

                if (cmbProduct.SelectedValue != "" && cmbProduct.SelectedItem.Text != "All")
                    dv.RowFilter = "itemcode='" + cmbProduct.SelectedValue + "'";

                int duration = int.Parse(txtDuration.Text);
                int noOfColumns = int.Parse(txtColumns.Text);
                int maxColIndex = int.Parse(txtColumns.Text) + 5;

                dt = dv.ToTable();

                foreach (DataRow dr in dt.Rows)
                {
                    bool dupFlag = false;
                    string fiLevel = "";
                    string seLevel = "";
                    string thLevel = "";
                    string foLevel = "";
                    string fifLevel = "";
                    string customer = dr["Customer"].ToString();
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

                    if (fifthLevel != "None")
                    {
                        fifLevel = dr[fifthLevel].ToString();
                    }

                    //string model = dr["Model"].ToString();
                    //string category = dr["CategoryName"].ToString();

                    DateTime transDate = DateTime.Parse(dr["TransDate"].ToString());
                    //DateTimeHelper.DateDifference dateHelper = new DateTimeHelper.DateDifference(refDate, purchaseDate);

                    int diffDays = int.Parse((endDate - transDate).TotalDays.ToString());
                    int rowIndex = 0;

                    foreach (DataRow dR in dsGrid.Tables[0].Rows)
                    {
                        //if ((dR["Customer"] != null) || (dR["Model"] != null) || (dR["CategoryName"] != null))
                        //if ((dR["Customer"] != null) && (dR["Customer"].ToString().Trim() == customer))
                        //{
                        //    dupFlag = true;
                        //    break;
                        //}
                        if ((firstLevel != "None") && (dR[firstLevel] != null) && (dR[firstLevel].ToString().Trim() == fiLevel))
                        {
                            dupFlag = true;
                            break;
                        }
                        if ((secondLevel != "None") && (dR[secondLevel] != null) && (dR[secondLevel].ToString().Trim() == seLevel))
                        {
                            dupFlag = true;
                            break;
                        }
                        if ((thirdLevel != "None") && (dR[thirdLevel] != null) && (dR[thirdLevel].ToString().Trim() == thLevel))
                        {
                            dupFlag = true;
                            break;
                        }
                        if ((fourthLevel != "None") && (dR[fourthLevel] != null) && (dR[fourthLevel].ToString().Trim() == foLevel))
                        {
                            dupFlag = true;
                            break;
                        }
                        if ((fifthLevel != "None") && (dR[fifthLevel] != null) && (dR[fifthLevel].ToString().Trim() == fifLevel))
                        {
                            dupFlag = true;
                            break;
                        }
                        rowIndex++;
                        //if ((dR[fiLevelCol] != null) && (dR[seLevelCol] != null) && (dR[thLevelCol] != null) && (dR[foLevelCol] != null))
                        //{
                        //    if (
                        //        (dR[fiLevelCol].ToString().Trim() == fiLevel) &&
                        //        (dR[seLevelCol].ToString().Trim() == seLevel) &&
                        //        (dR[thLevelCol].ToString().Trim() == thLevel) &&
                        //        (dR[foLevelCol].ToString().Trim() == foLevel))
                        //    {
                        //        dupFlag = true;
                        //        break;
                        //    }
                        //    rowIndex++;
                        //}
                    }

                    if (dupFlag)
                    {
                        int colIndex = diffDays / duration;
                        colIndex = colIndex + 2;

                        if (colIndex >= maxColIndex)
                        {
                            colIndex = maxColIndex;
                        }

                        double currAmount = 0.0;

                        if (dsGrid.Tables[0].Rows[rowIndex][colIndex] != null)
                        {
                            if (dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString() != "")
                                currAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString());
                        }

                        double totAmount = 0.0;

                        if (dr["VoucherType"].ToString() == "Sales")
                        {
                            if (colIndex > 4)
                            {
                                //totAmount = currAmount + (double.Parse(dr["NetRate"].ToString()) - double.Parse(dr["ActualDiscount"].ToString()) + double.Parse(dr["ActualVAT"].ToString()) + double.Parse(dr["ActualCST"].ToString()));
                                totAmount = currAmount + double.Parse(dr["ItemTotal"].ToString());
                            }
                        }
                        else
                        {
                            if (colIndex > 4)
                            {
                            totAmount = currAmount + double.Parse(dr["Amount"].ToString());
                            }
                        }

                        dsGrid.Tables[0].Rows[rowIndex][colIndex] = totAmount.ToString("#0");
                        dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                        dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                    }
                    else
                    {
                        DataRow gridRow = dsGrid.Tables[0].NewRow();

                        //gridRow["Customer"] = customer;
                        if (firstLevel != "None")
                            gridRow[firstLevel] = fiLevel;
                        if (secondLevel != "None")
                            gridRow[secondLevel] = seLevel;
                        if (thirdLevel != "None")
                            gridRow[thirdLevel] = thLevel;
                        if (fourthLevel != "None")
                            gridRow[fourthLevel] = foLevel;
                        if (fifthLevel != "None")
                            gridRow[fifthLevel] = fifLevel;

                        if (selLevels.IndexOf("Customer") < 0)
                        {
                            gridRow["Customer"] = dr["Customer"].ToString();
                            string cat = dr["Customer"].ToString();
                        }
                        if (selLevels.IndexOf("ProductDesc") < 0)
                        {
                            gridRow["ProductDesc"] = dr["ProductDesc"].ToString();
                            string catt = dr["ProductDesc"].ToString();
                        }
                        if (selLevels.IndexOf("CategoryName") < 0)
                            gridRow["CategoryName"] = dr["CategoryName"].ToString();
                        if (selLevels.IndexOf("Model") < 0)
                            gridRow["Model"] = dr["Model"].ToString();
                        if (selLevels.IndexOf("ItemCode") < 0)
                            gridRow["ItemCode"] = dr["ItemCode"].ToString();
                        //gridRow["Model"] = model;
                        //gridRow["CategoryName"] = category;

                        int colIndex = diffDays / duration;
                        colIndex = colIndex + 2;
                        if (colIndex >= maxColIndex)
                        {
                            colIndex = maxColIndex;
                        }

                        if (dr["VoucherType"].ToString() == "Sales")
                        {
                            if (colIndex > 4)
                            {
                                //gridRow[colIndex] = (double.Parse(dr["NetRate"].ToString()) - double.Parse(dr["ActualDiscount"].ToString()) + double.Parse(dr["ActualVAT"].ToString()) + double.Parse(dr["ActualCST"].ToString()));
                                gridRow[colIndex] = double.Parse(dr["ItemTotal"].ToString()).ToString("#0");
                            }
                        }
                        else
                        {
                            if (colIndex > 4)
                            {
                                gridRow[colIndex] = double.Parse(dr["Amount"].ToString()).ToString("#0");
                            }
                        }


                        /*
                        if (dr["OpeningBalance"] != null)
                        {
                            double maxValue = 0.0;

                            if (gridRow[colIndex].ToString() != "")
                                maxValue = double.Parse(gridRow[colIndex].ToString());

                            gridRow[maxColIndex] = maxValue + double.Parse(dr["OpeningBalance"].ToString());

                        }*/

                        dsGrid.Tables[0].Rows.Add(gridRow);
                    }

                }
            }
        }

        return dsGrid;
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
        BusinessLogic bl = new BusinessLogic();
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

    private DataSet CalculateTotal(DataSet dsGrid)
    {

        DataView dv = dsGrid.Tables[0].AsDataView();

        string sortString = "";
        if (firstLevel.Trim() != "None")
            sortString = firstLevel.Trim() + ",";
        if (secondLevel.Trim() != "None")
            sortString = secondLevel.Trim() + ",";
        if (thirdLevel.Trim() != "None")
            sortString = thirdLevel.Trim() + ",";
        if (fourthLevel.Trim() != "None")
            sortString = fourthLevel.Trim() + ",";
        if (fifthLevel.Trim() != "None")
            sortString = fifthLevel.Trim() + ",";
        if (sortString.Contains(","))
            sortString = sortString.Substring(0, sortString.Length - 1);

        dv.Sort = sortString;

        DataTable dt = dv.ToTable();

        DataSet ds = new DataSet();

        ds.Tables.Add(dt);

        dsGrid = ds;

        DataRow footerRow = dsGrid.Tables[0].NewRow();
        double total = 0.0;

        footerRow[0] = "Total";

        for (int i = dsGrid.Tables[0].Columns.Count - 1; i >= 5; i--)
        {
            double amount = 0.0;

            for (int j = 0; j < dsGrid.Tables[0].Rows.Count; j++)
            {
                if (dsGrid.Tables[0].Rows[j][i].ToString() != "")
                {
                    double colAmount = double.Parse(dsGrid.Tables[0].Rows[j][i].ToString());

                    amount = amount + colAmount;

                }
            }

            footerRow[i] = amount.ToString();

            total = total + amount;

        }


        dsGrid.Tables[0].Rows.Add(footerRow);


        DataColumn dc = new DataColumn("Total");
        dsGrid.Tables[0].Columns.Add(dc);

        DataSet newData = new DataSet();

        DataTable d1 = dsGrid.Tables[0].Clone();

        newData.Tables.Add(d1);

        for (int j = 0; j < dsGrid.Tables[0].Rows.Count; j++)
        {
            double rowTotal = 0.0;

            for (int i = dsGrid.Tables[0].Columns.Count - 1; i > 5; i--)
            {
                if (dsGrid.Tables[0].Rows[j][i].ToString() != "")
                {
                    double colAmount = double.Parse(dsGrid.Tables[0].Rows[j][i].ToString());
                    rowTotal = rowTotal + colAmount;
                }
            }

            if (rowTotal > 0)
            {
                dsGrid.Tables[0].Rows[j]["Total"] = rowTotal.ToString();
                dsGrid.Tables[0].Rows[j].EndEdit();
                dsGrid.Tables[0].Rows[j].AcceptChanges();
                DataRow dr1 = newData.Tables[0].NewRow();

                for (int i = dsGrid.Tables[0].Columns.Count - 1; i >= 0; i--)
                {
                    dr1[i] = dsGrid.Tables[0].Rows[j][i].ToString();
                }

                newData.Tables[0].Rows.Add(dr1);
            }
            //else
            //{
            //    dsGrid.Tables[0].Rows[j].Delete();
            //    dsGrid.Tables[0].AcceptChanges();
            //}
        }

        lblTotalOutstanding.Text = total.ToString();



        return newData;
    }

    private DataSet CalculateTota(DataSet dsGrid)
    {

        DataView dv = dsGrid.Tables[0].AsDataView();

        string sortString = "";
        if (firstLevel.Trim() != "None")
            sortString = firstLevel.Trim() + ",";
        if (secondLevel.Trim() != "None")
            sortString = secondLevel.Trim() + ",";
        if (thirdLevel.Trim() != "None")
            sortString = thirdLevel.Trim() + ",";
        if (fourthLevel.Trim() != "None")
            sortString = fourthLevel.Trim() + ",";
        if (fifthLevel.Trim() != "None")
            sortString = fifthLevel.Trim() + ",";
        if (sortString.Contains(","))
            sortString = sortString.Substring(0, sortString.Length - 1);

        dv.Sort = sortString;

        DataTable dt = dv.ToTable();

        DataSet ds = new DataSet();

        ds.Tables.Add(dt);

        dsGrid = ds;

        DataRow footerRow = dsGrid.Tables[0].NewRow();
        double total = 0.0;

        footerRow[0] = "Total";

        for (int i = dsGrid.Tables[0].Columns.Count - 1; i >= 5; i--)
        {
            double amount = 0.0;

            for (int j = 0; j < dsGrid.Tables[0].Rows.Count; j++)
            {
                if (dsGrid.Tables[0].Rows[j][i].ToString() != "")
                {
                    double colAmount = double.Parse(dsGrid.Tables[0].Rows[j][i].ToString());

                    amount = amount + colAmount;

                }
            }

            footerRow[i] = amount.ToString();

            total = total + amount;

        }


        dsGrid.Tables[0].Rows.Add(footerRow);


        DataColumn dc = new DataColumn("Total");
        dsGrid.Tables[0].Columns.Add(dc);

        DataSet newData = new DataSet();

        DataTable d1 = dsGrid.Tables[0].Clone();

        newData.Tables.Add(d1);

        for (int j = 0; j < dsGrid.Tables[0].Rows.Count; j++)
        {
            double rowTotal = 0.0;

            for (int i = dsGrid.Tables[0].Columns.Count - 1; i > 5; i--)
            {
                if (dsGrid.Tables[0].Rows[j][i].ToString() != "")
                {
                    double colAmount = double.Parse(dsGrid.Tables[0].Rows[j][i].ToString());
                    rowTotal = rowTotal + colAmount;
                }
            }

            if (rowTotal > 0)
            {
                dsGrid.Tables[0].Rows[j]["Total"] = rowTotal.ToString();
                dsGrid.Tables[0].Rows[j].EndEdit();
                dsGrid.Tables[0].Rows[j].AcceptChanges();
                DataRow dr1 = newData.Tables[0].NewRow();

                for (int i = dsGrid.Tables[0].Columns.Count - 1; i >= 0; i--)
                {
                    dr1[i] = dsGrid.Tables[0].Rows[j][i].ToString();
                }

                newData.Tables[0].Rows.Add(dr1);
            }
            //else
            //{
            //    dsGrid.Tables[0].Rows[j].Delete();
            //    dsGrid.Tables[0].AcceptChanges();
            //}
        }

        lblTotalOutstanding.Text = total.ToString();



        return newData;
    }

    private DataSet FMake(DataSet dsF,DataSet dsGrid)
    {

        //DataView dv = dsGrid.Tables[0].AsDataView();
 
        //DataTable dt = dv.ToTable();
        //DataSet ds = new DataSet();

        //ds.Tables.Add(dt);

        //dsGrid = ds;

        
        

        DataSet newData = new DataSet();
        DataTable dtt = new DataTable();
        DataColumn dcc;

        //dtt.Columns.Add(new DataColumn("Customer"));
        //dtt.Columns.Add(new DataColumn("Total"));

        dcc = new DataColumn("Customer");
        dtt.Columns.Add(dcc);
        dcc = new DataColumn("Total");
        dtt.Columns.Add(dcc);

        newData.Tables.Add(dtt);

        string firlvl = "";
        double total = 0.0;
        string firlvlDRVal = "";
        string firlvlDVal = "";

        bool dupFlag = false;
        int rowIndex = 0;
        double tot = 1;
        double tota = 0.0;
        double totall = 0.0;

        //foreach (DataRow dF in dsF.Tables[0].Rows)
        //{
           
        //    string allDRVal = "";
        //    if (dF["Customer"].ToString() != "")
        //        allDRVal = dF["Customer"].ToString().Trim();
        //    else
        //        allDRVal = "OTHERS";

        tot = 1;
            foreach (DataRow dr in dsGrid.Tables[0].Rows)
            {
                if (dr["Customer"].ToString() != "")
                {
                    firlvl = dr["Customer"].ToString().Trim();

                    if (tot == 1)
                        firlvlDRVal = dr["Customer"].ToString().Trim();
                    
                   
                }
                total = double.Parse(dr["Total"].ToString());

                if (firlvl.Trim() == firlvlDRVal.Trim())
                {
                    tota = tota + double.Parse(dr["Total"].ToString());
                    firlvlDVal = dr["Customer"].ToString().Trim();
                }
                else
                {
                    dupFlag = true;
                    DataRow dr_final5 = dtt.NewRow();
                    dr_final5["Customer"] = firlvlDVal;
                    dr_final5["Total"] = tota;
                    dtt.Rows.Add(dr_final5);
                    tota = 0.0;

                    tota = tota + double.Parse(dr["Total"].ToString());
                    firlvlDVal = dr["Customer"].ToString().Trim();
                }

                firlvlDRVal = dr["Customer"].ToString().Trim();

                if (firlvl == "Total")
                {
                    
                }
                else
                {
                    totall = totall + double.Parse(dr["Total"].ToString());
                }

                tot = 2;
                rowIndex++;

                

                if (!dupFlag)
                {
                    
                }


            }

        //}

            Label2.Text = totall.ToString();

        return newData;
        
    }

    public DataSet UpdateFinalDat(DataSet dsGrid, DataSet dsFinal)
    {
        foreach (DataRow dr in dsGrid.Tables[0].Rows)
        {
            bool dupFlag = false;
            string firlvlDRVal = "";
            if (firstLevel != "None")
                firlvlDRVal = dr[firstLevel].ToString();
            string selvlDRVal = "";
            if (secondLevel != "None")
                selvlDRVal = dr[secondLevel].ToString();
            string thlvlDRVal = "";
            if (thirdLevel != "None")
                thlvlDRVal = dr[thirdLevel].ToString();
            string folvlDRVal = "";
            if (fourthLevel != "None")
                folvlDRVal = dr[fourthLevel].ToString();
            string fiflvlDRVal = "";
            if (fifthLevel != "None")
                fiflvlDRVal = dr[fifthLevel].ToString();
            int rowIndex = 0;

            //if (firlvlDRVal == string.Empty)
            //{
            //    firlvlDRVal = "OTHERS";
            //}
            string allDRVal = "";
            if (dr["Customer"].ToString() != "")
                allDRVal += dr["Customer"].ToString().Trim() + ",";
            else
                allDRVal += "OTHERS,";
            if (dr["ItemCode"].ToString() != "")
                allDRVal += dr["ItemCode"].ToString().Trim();
            else
                allDRVal += "OTHERS";


            foreach (DataRow df in dsFinal.Tables[0].Rows)
            {
                if (allDRVal != null)
                {
                    string allDFVal = "";
                    //if (firstLevel != "None")
                    //    allDFVal += df[firstLevel].ToString().Trim() + ",";
                    //else
                    //    allDFVal += "OTHERS,";
                    if (df["Customer"].ToString() != "")
                        allDFVal += df["Customer"].ToString().Trim() + ",";
                    else
                        allDFVal += "OTHERS,";
                    if (df["ItemCode"].ToString() != "")
                        allDFVal += df["ItemCode"].ToString().Trim();
                    else
                        allDFVal += "OTHERS";

                    if (allDFVal.Trim() == allDRVal.Trim())
                    {
                        //if (firstLevel!="None" && (((df[firstLevel].ToString().Trim()) + "," + (df[firstLevel].ToString().Trim()) 
                        //    + "," + (df[firstLevel].ToString().Trim()) + "," + (df[firstLevel].ToString().Trim())) 
                        //    == alllvlDRVal.Trim()))
                        //{
                        dupFlag = true;
                        break;
                    }
                    rowIndex++;
                }
            }

            if (!dupFlag)
            {

                DataRow newRow = dsFinal.Tables[0].NewRow();
                int cnt = 0;
                //newRow[0] = firlvlDRVal;
                //newRow[1] = selvlDRVal;
                //newRow[2] = thlvlDRVal;
                //newRow[3] = folvlDRVal;

                if (firlvlDRVal != "")
                {
                    newRow[cnt] = firlvlDRVal;
                    cnt++;
                }
                if (selvlDRVal != "")
                {
                    newRow[cnt] = selvlDRVal;
                    cnt++;
                }
                if (thlvlDRVal != "")
                {
                    newRow[cnt] = thlvlDRVal;
                    cnt++;
                }
                if (folvlDRVal != "")
                {
                    newRow[cnt] = folvlDRVal;
                    cnt++;
                }

                if (fiflvlDRVal != "")
                {
                    newRow[cnt] = fiflvlDRVal;
                    cnt++;
                }

                if (selLevels.IndexOf("Customer") < 0)
                {
                    newRow[cnt] = dr["Customer"];
                    cnt++;
                }
                if (selLevels.IndexOf("ProductDesc") < 0)
                {
                    newRow[cnt] = dr["ProductDesc"];
                    cnt++;
                }
                if (selLevels.IndexOf("CategoryName") < 0)
                {
                    newRow[cnt] = dr["CategoryName"];
                    cnt++;
                }
                if (selLevels.IndexOf("Model") < 0)
                {
                    newRow[cnt] = dr["Model"];
                    cnt++;
                }
                if (selLevels.IndexOf("ItemCode") < 0)
                {
                    newRow[cnt] = dr["ItemCode"];
                    cnt++;
                }
                for (int i = dsGrid.Tables[0].Columns.Count - 1; i > 4; i--)
                {
                    if ((dr[i] != null) && (dr[i].ToString() != ""))
                    {
                        double amount = double.Parse(dr[i].ToString());
                        newRow[i] = amount;
                    }
                    else
                    {
                        newRow[i] = "0";
                    }
                }

                dsFinal.Tables[0].Rows.Add(newRow);
            }
            else
            {

                for (int i = dsGrid.Tables[0].Columns.Count - 1; i > 5; i--)
                {

                    if (dr[i].ToString() != "")
                    {
                        double amount = 0;
                        double existamount = 0;

                        if (dr[i].ToString() != "")
                            amount = double.Parse(dr[i].ToString());

                        if ((dsFinal.Tables[0].Rows[rowIndex][i - 1] != null) && (dsFinal.Tables[0].Rows[rowIndex][i - 1].ToString() != ""))
                            existamount = double.Parse(dsFinal.Tables[0].Rows[rowIndex][i - 1].ToString());

                        dsFinal.Tables[0].Rows[rowIndex][i - 1] = existamount + amount;
                    }


                }
            }

        }

        return dsFinal;
    }

    public DataSet UpdateFinalData(DataSet dsGrid, DataSet dsFinal)
    {
        foreach (DataRow dr in dsGrid.Tables[0].Rows)
        {
            bool dupFlag = false;
            string firlvlDRVal = "";
            if (firstLevel != "None")
                firlvlDRVal = dr[firstLevel].ToString();
            string selvlDRVal = "";
            if (secondLevel != "None")
                selvlDRVal = dr[secondLevel].ToString();
            string thlvlDRVal = "";
            if (thirdLevel != "None")
                thlvlDRVal = dr[thirdLevel].ToString();
            string folvlDRVal = "";
            if (fourthLevel != "None")
                folvlDRVal = dr[fourthLevel].ToString();
            string fiflvlDRVal = "";
            if (fifthLevel != "None")
                fiflvlDRVal = dr[fifthLevel].ToString();
            int rowIndex = 0;

            //if (firlvlDRVal == string.Empty)
            //{
            //    firlvlDRVal = "OTHERS";
            //}
            string allDRVal = "";
            if (dr["Customer"].ToString() != "")
                allDRVal += dr["Customer"].ToString().Trim() + ",";
            else
                allDRVal += "OTHERS,";
            if (dr["ItemCode"].ToString() != "")
                allDRVal += dr["ItemCode"].ToString().Trim();
            else
                allDRVal += "OTHERS";


            foreach (DataRow df in dsFinal.Tables[0].Rows)
            {
                if (allDRVal != null)
                {
                    string allDFVal = "";
                    //if (firstLevel != "None")
                    //    allDFVal += df[firstLevel].ToString().Trim() + ",";
                    //else
                    //    allDFVal += "OTHERS,";
                    if (df["Customer"].ToString() != "")
                        allDFVal += df["Customer"].ToString().Trim() + ",";
                    else
                        allDFVal += "OTHERS,";
                    if (df["ItemCode"].ToString() != "")
                        allDFVal += df["ItemCode"].ToString().Trim();
                    else
                        allDFVal += "OTHERS";

                    if (allDFVal.Trim() == allDRVal.Trim())
                    {
                        //if (firstLevel!="None" && (((df[firstLevel].ToString().Trim()) + "," + (df[firstLevel].ToString().Trim()) 
                        //    + "," + (df[firstLevel].ToString().Trim()) + "," + (df[firstLevel].ToString().Trim())) 
                        //    == alllvlDRVal.Trim()))
                        //{
                        dupFlag = true;
                        break;
                    }
                    rowIndex++;
                }
            }

            if (!dupFlag)
            {

                DataRow newRow = dsFinal.Tables[0].NewRow();
                int cnt = 0;
                //newRow[0] = firlvlDRVal;
                //newRow[1] = selvlDRVal;
                //newRow[2] = thlvlDRVal;
                //newRow[3] = folvlDRVal;

                if (firlvlDRVal != "")
                {
                    newRow[cnt] = firlvlDRVal;
                    cnt++;
                }
                if (selvlDRVal != "")
                {
                    newRow[cnt] = selvlDRVal;
                    cnt++;
                }
                if (thlvlDRVal != "")
                {
                    newRow[cnt] = thlvlDRVal;
                    cnt++;
                }
                if (folvlDRVal != "")
                {
                    newRow[cnt] = folvlDRVal;
                    cnt++;
                }

                if (fiflvlDRVal != "")
                {
                    newRow[cnt] = fiflvlDRVal;
                    cnt++;
                }

                if (selLevels.IndexOf("Customer") < 0)
                {
                    newRow[cnt] = dr["Customer"];
                    cnt++;
                }
                if (selLevels.IndexOf("ProductDesc") < 0)
                {
                    newRow[cnt] = dr["ProductDesc"];
                    cnt++;
                }
                if (selLevels.IndexOf("CategoryName") < 0)
                {
                    newRow[cnt] = dr["CategoryName"];
                    cnt++;
                }
                if (selLevels.IndexOf("Model") < 0)
                {
                    newRow[cnt] = dr["Model"];
                    cnt++;
                }
                if (selLevels.IndexOf("ItemCode") < 0)
                {
                    newRow[cnt] = dr["ItemCode"];
                    cnt++;
                }
                for (int i = dsGrid.Tables[0].Columns.Count - 1; i > 4; i--)
                {
                    if ((dr[i] != null) && (dr[i].ToString() != ""))
                    {
                        double amount = double.Parse(dr[i].ToString());
                        newRow[i] = amount;
                    }
                    else
                    {
                        newRow[i] = "0";
                    }
                }

                dsFinal.Tables[0].Rows.Add(newRow);
            }
            else
            {

                for (int i = dsGrid.Tables[0].Columns.Count - 1; i > 5; i--)
                {

                    if (dr[i].ToString() != "")
                    {
                        double amount = 0;
                        double existamount = 0;

                        if (dr[i].ToString() != "")
                            amount = double.Parse(dr[i].ToString());

                        if ((dsFinal.Tables[0].Rows[rowIndex][i - 1] != null) && (dsFinal.Tables[0].Rows[rowIndex][i - 1].ToString() != ""))
                            existamount = double.Parse(dsFinal.Tables[0].Rows[rowIndex][i - 1].ToString());

                        dsFinal.Tables[0].Rows[rowIndex][i - 1] = existamount + amount;
                    }


                }
            }

        }

        return dsFinal;
    }

    public DataSet GetCustDebitDataNew()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet debitDataNew = bl.GetCustDebitDataNew();

        return debitDataNew;
    }

    public DataSet GetCustDebitData()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);

        string Branch = string.Empty;


        Branch = drpBranchAdd.SelectedValue;
      


        DataSet debitData = bl.GetCustDebitData(Branch);

        return debitData;
    }


    public DataSet GetReceivedAmountNew(DataSet dsGrid)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet creditData = bl.GetAllReceivedAmountNew();

        int maxColIndex = int.Parse(txtColumns.Text) + 4;

        if (creditData != null)
        {
            foreach (DataRow dr in creditData.Tables[0].Rows)
            {
                //bool dupFlag = false;

                string customer = dr["Creditor"].ToString();
                string billNo = dr["BillNo"].ToString();
                //DateTime transDate = DateTime.Parse(dr["TransDate"].ToString());
                double currAmount = double.Parse(dr["Amount"].ToString());

                int rowIndex = 0;

                foreach (DataRow dR in dsGrid.Tables[0].Rows)
                {
                    if (currAmount > 0)
                    {
                        if (dR["BillNo"].ToString() != "")
                        {
                            if (dR["BillNo"].ToString().Trim() == billNo.Trim())
                            {

                                double debitAmount = 0.0;

                                if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    debitAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"].ToString());
                                else
                                    continue;

                                debitAmount = debitAmount - currAmount;

                                if (debitAmount <= 0)
                                {
                                    if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    {
                                        dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = "0";
                                        dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                        dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                    }
                                }
                                else
                                {
                                    if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    {
                                        dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = (debitAmount).ToString("#0");
                                        dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                        dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                    }
                                }

                                currAmount = debitAmount;
                            }
                        }
                    }

                    rowIndex++;
                }

                dsGrid.Tables[0].AcceptChanges();

                if (currAmount == 0)
                {
                    for (int i = 0; i < dsGrid.Tables[0].Rows.Count; i++)
                    {
                        int rIndex = 0;

                        if (dsGrid.Tables[0].Rows[i]["BillNo"] != null)
                        {
                            if (dsGrid.Tables[0].Rows[i]["BillNo"].ToString().Trim() == billNo.Trim())
                            {
                                dsGrid.Tables[0].Rows[i].Delete();
                                dsGrid.Tables[0].AcceptChanges();
                            }

                        }

                        rIndex++;
                    }

                }


            }
        }

        return dsGrid;
    }

    public DataSet GetCreditDataNew(DataSet dsGrid)
    {

        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet creditData = bl.GetAllCustCreditDataNew();

        //DataSet receivedData = bl.GetAllReceivedAmount();

        int maxColIndex = int.Parse(txtColumns.Text) + 4;

        if (creditData != null)
        {
            foreach (DataRow dr in creditData.Tables[0].Rows)
            {
                //bool dupFlag = false;

                string customer = dr["Creditor"].ToString();
                //DateTime transDate = DateTime.Parse(dr["TransDate"].ToString());
                double currAmount = double.Parse(dr["Amount"].ToString());
                double openBalCR = double.Parse(dr["OpenbalanceCR"].ToString());

                currAmount = currAmount + openBalCR;

                bool flag = true;

                int rowIndex = 0;

                foreach (DataRow dR in dsGrid.Tables[0].Rows)
                {
                    if (currAmount > 0)
                    {
                        if (dR["LedgerName"] != null)
                        {
                            if (dR["LedgerName"].ToString().Trim() == customer.Trim())
                            {

                                if (dR["LedgerName"].ToString().Trim() == customer.Trim())
                                {

                                    double debitAmount = 0.0;

                                    if (dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales")
                                        debitAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"].ToString());
                                    //else if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    //    continue;
                                    else
                                        debitAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex]["Amount"].ToString());

                                    if (flag == true)
                                    {
                                        debitAmount = currAmount - (double.Parse(dsGrid.Tables[0].Rows[rowIndex]["OpenbalanceDR"].ToString()) + debitAmount);
                                        flag = false;
                                    }
                                    else
                                        debitAmount = currAmount - debitAmount;



                                    if (debitAmount > 0)
                                    {
                                        if (dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales")
                                        {
                                            dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = "0";
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                        else
                                        {
                                        dsGrid.Tables[0].Rows[rowIndex]["Amount"] = "0";
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        if (dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales")
                                        {
                                            dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = (-(debitAmount)).ToString("#0");
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                        else
                                        {
                                            dsGrid.Tables[0].Rows[rowIndex]["Amount"] = (-(debitAmount)).ToString("#0");
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                    }

                                    currAmount = debitAmount;
                                }
                            }
                        }
                    }

                    rowIndex++;
                }

                dsGrid.Tables[0].AcceptChanges();

                if (currAmount > 0)
                {
                    for (int i = 0; i < dsGrid.Tables[0].Rows.Count; i++)
                    {
                        int rIndex = 0;

                        if (dsGrid.Tables[0].Rows[i]["LedgerName"] != null)
                        {
                            if (dsGrid.Tables[0].Rows[i]["LedgerName"].ToString().Trim() == customer.Trim())
                            {
                                dsGrid.Tables[0].Rows[i].Delete();
                                dsGrid.Tables[0].AcceptChanges();
                            }

                        }

                        rIndex++;
                    }

                }


            }
        }

        return dsGrid;
    }


    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpBranchAdd.Items.Clear();

     
            ds = bl.ListBranch();
            //drpBranchAdd.Items.Add(new ListItem("All", "0"));

            drpBranchAdd.DataSource = ds;
        drpBranchAdd.DataTextField = "BranchName";
        drpBranchAdd.DataValueField = "Branchcode";
        drpBranchAdd.DataBind();
    }

    private void BranchEnable_Disable()
    {
        string sCustomer = string.Empty;
        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);

        sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["DefaultBranchCode"]);
        drpBranchAdd.ClearSelection();
        ListItem li = drpBranchAdd.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        if (li != null) li.Selected = true;

    }

    public DataSet GetReceivedAmount(DataSet dsGrid)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);

        string Branch = string.Empty;


        Branch = drpBranchAdd.SelectedValue;

        DataSet creditData = bl.GetAllReceivedAmount(Branch);

        int maxColIndex = int.Parse(txtColumns.Text) + 4;

        if (creditData != null)
        {
            foreach (DataRow dr in creditData.Tables[0].Rows)
            {
                //bool dupFlag = false;

                string customer = dr["Creditor"].ToString();
                string billNo = dr["BillNo"].ToString();
                //DateTime transDate = DateTime.Parse(dr["TransDate"].ToString());
                double currAmount = double.Parse(dr["Amount"].ToString());

                int rowIndex = 0;

                foreach (DataRow dR in dsGrid.Tables[0].Rows)
                {
                    if (currAmount > 0)
                    {
                        if (dR["BillNo"].ToString() != "")
                        {
                            if (dR["BillNo"].ToString().Trim() == billNo.Trim())
                            {

                                double debitAmount = 0.0;

                                if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    debitAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"].ToString());
                                else
                                    continue;

                                debitAmount = debitAmount - currAmount;

                                if (debitAmount <= 0)
                                {
                                    if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    {
                                        dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = "0";
                                        dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                        dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                    }
                                }
                                else
                                {
                                    if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    {
                                        dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = (debitAmount).ToString("#0");
                                        dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                        dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                    }
                                }

                                currAmount = debitAmount;
                            }
                        }
                    }

                    rowIndex++;
                }

                dsGrid.Tables[0].AcceptChanges();

                if (currAmount == 0)
                {
                    for (int i = 0; i < dsGrid.Tables[0].Rows.Count; i++)
                    {
                        int rIndex = 0;

                        if (dsGrid.Tables[0].Rows[i]["BillNo"] != null)
                        {
                            if (dsGrid.Tables[0].Rows[i]["BillNo"].ToString().Trim() == billNo.Trim())
                            {
                                dsGrid.Tables[0].Rows[i].Delete();
                                dsGrid.Tables[0].AcceptChanges();
                            }

                        }

                        rIndex++;
                    }

                }


            }
        }

        return dsGrid;
    }

    public DataSet GetCreditData(DataSet dsGrid)
    {

        BusinessLogic bl = new BusinessLogic(sDataSource);

        string Branch = string.Empty;


        Branch = drpBranchAdd.SelectedValue;

        DataSet creditData = bl.GetAllCustCreditData1(Branch);

        //DataSet receivedData = bl.GetAllReceivedAmount();

        int maxColIndex = int.Parse(txtColumns.Text) + 4;

        if (creditData != null)
        {
            foreach (DataRow dr in creditData.Tables[0].Rows)
            {
                //bool dupFlag = false;

                string customer = dr["Creditor"].ToString();
                //DateTime transDate = DateTime.Parse(dr["TransDate"].ToString());
                double currAmount = double.Parse(dr["Amount"].ToString());
                double openBalCR = double.Parse(dr["OpenbalanceCR"].ToString());

                currAmount = currAmount + openBalCR;

                bool flag = true;

                int rowIndex = 0;

                foreach (DataRow dR in dsGrid.Tables[0].Rows)
                {
                    if (currAmount > 0)
                    {
                        if (dR["Customer"] != null)
                        {
                            if (dR["Customer"].ToString().Trim() == customer.Trim())
                            {

                                if (dR["Customer"].ToString().Trim() == customer.Trim())
                                {

                                    double debitAmount = 0.0;

                                    if (dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales")
                                        debitAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"].ToString());
                                    //else if ((dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales") && (dsGrid.Tables[0].Rows[rowIndex]["BillNo"].ToString() != ""))
                                    //    continue;
                                    else
                                        debitAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex]["Amount"].ToString());

                                    if (flag == true)
                                    {
                                        debitAmount = currAmount - (double.Parse(dsGrid.Tables[0].Rows[rowIndex]["OpenbalanceDR"].ToString()) + debitAmount);
                                        flag = false;
                                    }
                                    else
                                        debitAmount = currAmount - debitAmount;



                                    if (debitAmount > 0)
                                    {
                                        if (dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales")
                                        {
                                            dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = "0";
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                        else
                                        {
                                            dsGrid.Tables[0].Rows[rowIndex]["Amount"] = "0";
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        if (dsGrid.Tables[0].Rows[rowIndex]["VoucherType"].ToString() == "Sales")
                                        {
                                            dsGrid.Tables[0].Rows[rowIndex]["ItemTotal"] = (-(debitAmount)).ToString("#0");
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                        else
                                        {
                                            dsGrid.Tables[0].Rows[rowIndex]["Amount"] = (-(debitAmount)).ToString("#0");
                                            dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                                            dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                                        }
                                    }

                                    currAmount = debitAmount;
                                }
                            }
                        }
                    }

                    rowIndex++;
                }

                dsGrid.Tables[0].AcceptChanges();

                if (currAmount > 0)
                {
                    for (int i = 0; i < dsGrid.Tables[0].Rows.Count; i++)
                    {
                        int rIndex = 0;

                        if (dsGrid.Tables[0].Rows[i]["Customer"] != null)
                        {
                            if (dsGrid.Tables[0].Rows[i]["Customer"].ToString().Trim() == customer.Trim())
                            {
                                dsGrid.Tables[0].Rows[i].Delete();
                                dsGrid.Tables[0].AcceptChanges();
                            }

                        }

                        rIndex++;
                    }

                }


            }
        }

        return dsGrid;
    }

    public DataSet UpdateColumnsData(DataSet dsGrid, DataSet debitData)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);


        DateTime endDate = DateTime.Parse(DateTime.Now.ToShortDateString());



        if (debitData != null)
        {

            //IEnumerable<DataRow> query = null; 

            //if(cmbCategory.SelectedValue != "0")
            //    query = from data in debitData.Tables[0].AsEnumerable() where data.Field<string>("CategoryName") == cmbCategory.SelectedItem.Text select data;
            //else
            //    query = from data in debitData.Tables[0].AsEnumerable() select data;

            //if (query.Count<DataRow>() > 0)
            if (true)
            {

                //DataTable dt = query.CopyToDataTable<DataRow>();

                DataTable dt = debitData.Tables[0];

                DataView dv = dt.AsDataView();

                if (cmbCategory.SelectedValue != "0")
                    dv.RowFilter = "CategoryName='" + cmbCategory.SelectedItem.Text + "'";

                if (cmbModel.SelectedValue != "0")
                    dv.RowFilter = "Model='" + cmbModel.SelectedItem.Text + "'";

                if (cmbBrand.SelectedValue != "0")
                    dv.RowFilter = "ProductDesc='" + cmbBrand.SelectedItem.Text + "'";

                if (cmbProduct.SelectedValue != "" && cmbProduct.SelectedItem.Text != "All")
                    dv.RowFilter = "itemcode='" + cmbProduct.SelectedValue + "'";

                int duration = int.Parse(txtDuration.Text);
                int noOfColumns = int.Parse(txtColumns.Text);
                int maxColIndex = int.Parse(txtColumns.Text) + 5;

                dt = dv.ToTable();

                foreach (DataRow dr in dt.Rows)
                {
                    bool dupFlag = false;
                    string fiLevel = "";
                    string seLevel = "";
                    string thLevel = "";
                    string foLevel = "";
                    string fifLevel = "";
                    string customer = dr["Customer"].ToString();
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

                    if (fifthLevel != "None")
                    {
                        fifLevel = dr[fifthLevel].ToString();
                    }

                    //string model = dr["Model"].ToString();
                    //string category = dr["CategoryName"].ToString();

                    DateTime transDate = DateTime.Parse(dr["TransDate"].ToString());
                    //DateTimeHelper.DateDifference dateHelper = new DateTimeHelper.DateDifference(refDate, purchaseDate);

                    int diffDays = int.Parse((endDate - transDate).TotalDays.ToString());
                    int rowIndex = 0;

                    foreach (DataRow dR in dsGrid.Tables[0].Rows)
                    {
                        //if ((dR["Customer"] != null) || (dR["Model"] != null) || (dR["CategoryName"] != null))
                        //if ((dR["Customer"] != null) && (dR["Customer"].ToString().Trim() == customer))
                        //{
                        //    dupFlag = true;
                        //    break;
                        //}
                        if ((firstLevel != "None") && (dR[firstLevel] != null) && (dR[firstLevel].ToString().Trim() == fiLevel))
                        {
                            dupFlag = true;
                            break;
                        }
                        if ((secondLevel != "None") && (dR[secondLevel] != null) && (dR[secondLevel].ToString().Trim() == seLevel))
                        {
                            dupFlag = true;
                            break;
                        }
                        if ((thirdLevel != "None") && (dR[thirdLevel] != null) && (dR[thirdLevel].ToString().Trim() == thLevel))
                        {
                            dupFlag = true;
                            break;
                        }
                        if ((fourthLevel != "None") && (dR[fourthLevel] != null) && (dR[fourthLevel].ToString().Trim() == foLevel))
                        {
                            dupFlag = true;
                            break;
                        }
                        if ((fifthLevel != "None") && (dR[fifthLevel] != null) && (dR[fifthLevel].ToString().Trim() == fifLevel))
                        {
                            dupFlag = true;
                            break;
                        }
                        rowIndex++;
                        //if ((dR[fiLevelCol] != null) && (dR[seLevelCol] != null) && (dR[thLevelCol] != null) && (dR[foLevelCol] != null))
                        //{
                        //    if (
                        //        (dR[fiLevelCol].ToString().Trim() == fiLevel) &&
                        //        (dR[seLevelCol].ToString().Trim() == seLevel) &&
                        //        (dR[thLevelCol].ToString().Trim() == thLevel) &&
                        //        (dR[foLevelCol].ToString().Trim() == foLevel))
                        //    {
                        //        dupFlag = true;
                        //        break;
                        //    }
                        //    rowIndex++;
                        //}
                    }

                    if (dupFlag)
                    {
                        int colIndex = diffDays / duration;
                        colIndex = colIndex + 2;

                        if (colIndex >= maxColIndex)
                        {
                            colIndex = maxColIndex;
                        }

                        double currAmount = 0.0;

                        if (dsGrid.Tables[0].Rows[rowIndex][colIndex] != null)
                        {
                            if (dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString() != "")
                                currAmount = double.Parse(dsGrid.Tables[0].Rows[rowIndex][colIndex].ToString());
                        }

                        double totAmount = 0.0;

                        if (dr["VoucherType"].ToString() == "Sales")
                            //totAmount = currAmount + (double.Parse(dr["NetRate"].ToString()) - double.Parse(dr["ActualDiscount"].ToString()) + double.Parse(dr["ActualVAT"].ToString()) + double.Parse(dr["ActualCST"].ToString()));
                            totAmount = currAmount + double.Parse(dr["ItemTotal"].ToString());
                        else
                            totAmount = currAmount + double.Parse(dr["Amount"].ToString());

                        dsGrid.Tables[0].Rows[rowIndex][colIndex] = totAmount.ToString("#0");
                        dsGrid.Tables[0].Rows[rowIndex].EndEdit();
                        dsGrid.Tables[0].Rows[rowIndex].AcceptChanges();
                    }
                    else
                    {
                        DataRow gridRow = dsGrid.Tables[0].NewRow();

                        //gridRow["Customer"] = customer;
                        if (firstLevel != "None")
                            gridRow[firstLevel] = fiLevel;
                        if (secondLevel != "None")
                            gridRow[secondLevel] = seLevel;
                        if (thirdLevel != "None")
                            gridRow[thirdLevel] = thLevel;
                        if (fourthLevel != "None")
                            gridRow[fourthLevel] = foLevel;
                        if (fifthLevel != "None")
                            gridRow[fifthLevel] = fifLevel;

                        if (selLevels.IndexOf("Customer") < 0)
                        {
                            gridRow["Customer"] = dr["Customer"].ToString();
                            string cat = dr["Customer"].ToString();
                        }
                        if (selLevels.IndexOf("ProductDesc") < 0)
                        {
                            gridRow["ProductDesc"] = dr["ProductDesc"].ToString();
                            string cat = dr["ProductDesc"].ToString();
                        }
                        if (selLevels.IndexOf("CategoryName") < 0)
                            gridRow["CategoryName"] = dr["CategoryName"].ToString();
                        if (selLevels.IndexOf("Model") < 0)
                            gridRow["Model"] = dr["Model"].ToString();
                        if (selLevels.IndexOf("ItemCode") < 0)
                            gridRow["ItemCode"] = dr["ItemCode"].ToString();
                        //gridRow["Model"] = model;
                        //gridRow["CategoryName"] = category;

                        int colIndex = diffDays / duration;
                        colIndex = colIndex + 2;
                        if (colIndex >= maxColIndex)
                        {
                            colIndex = maxColIndex;
                        }


                        if ((diffDays >= 1) && (diffDays <= 5))
                        {
                            colIndex = 4;
                        }
                        else if ((diffDays >= 6) && (diffDays <= 10))
                        {
                            colIndex = 5;
                        }
                        else if ((diffDays >= 11) && (diffDays <= 15))
                        {
                            colIndex = 6;
                        }
                        else if ((diffDays >= 15) && (diffDays <= 20))
                        {
                            colIndex = 7;
                        }
                        else if (diffDays >= 20)
                        {
                            colIndex = 8;
                        }

                        if (dr["VoucherType"].ToString() == "Sales")
                        {
                            if (colIndex > 3)
                            {
                                //gridRow[colIndex] = (double.Parse(dr["NetRate"].ToString()) - double.Parse(dr["ActualDiscount"].ToString()) + double.Parse(dr["ActualVAT"].ToString()) + double.Parse(dr["ActualCST"].ToString()));
                                gridRow[colIndex] = double.Parse(dr["ItemTotal"].ToString()).ToString("#0");
                            }
                        }
                        else
                        {
                            if (colIndex > 4)
                            {
                                gridRow[colIndex] = double.Parse(dr["Amount"].ToString()).ToString("#0");
                            }
                        }
                        //}

                        /*
                        if (dr["OpeningBalance"] != null)
                        {
                            double maxValue = 0.0;

                            if (gridRow[colIndex].ToString() != "")
                                maxValue = double.Parse(gridRow[colIndex].ToString());

                            gridRow[maxColIndex] = maxValue + double.Parse(dr["OpeningBalance"].ToString());

                        }*/

                        dsGrid.Tables[0].Rows.Add(gridRow);
                    }

                }
            }
        }

        return dsGrid;
    }

    public DataSet GenerateGridColumns()
    {
        selLevels = "";
        int duration = int.Parse(txtDuration.Text);
        int noOfColumns = int.Parse(txtColumns.Text);
        firstLevel = ddlFirstLvl.SelectedValue;
        secondLevel = ddlSecondLvl.SelectedValue;
        thirdLevel = ddlThirdLvl.SelectedValue;
        fourthLevel = ddlFourthLvl.SelectedValue;
        fifthLevel = ddlFifthLvl.SelectedValue;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn dc;

        if (ddlFirstLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(firstLevel);
            dt.Columns.Add(dc);
            selLevels += firstLevel;
        }

        if (ddlSecondLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(secondLevel);
            dt.Columns.Add(dc);
            selLevels += secondLevel;
        }

        if (ddlThirdLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(thirdLevel);
            dt.Columns.Add(dc);
            selLevels += thirdLevel;
        }

        if (ddlFourthLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(fourthLevel);
            dt.Columns.Add(dc);
            selLevels += fourthLevel;
        }

        if (ddlFifthLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(fifthLevel);
            dt.Columns.Add(dc);
            selLevels += fifthLevel;
        }

        if (selLevels.IndexOf("Customer") < 0)
            dt.Columns.Add(new DataColumn("Customer"));
        if (selLevels.IndexOf("ProductDesc") < 0)
            dt.Columns.Add(new DataColumn("ProductDesc"));
        if (selLevels.IndexOf("CategoryName") < 0)
            dt.Columns.Add(new DataColumn("CategoryName"));
        if (selLevels.IndexOf("Model") < 0)
            dt.Columns.Add(new DataColumn("Model"));
        if (selLevels.IndexOf("ItemCode") < 0)
            dt.Columns.Add(new DataColumn("ItemCode"));

        int colDur = 0;
        int nextDur = duration;

        for (int i = 0; i < noOfColumns; i++)
        {
            
            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")", typeof(double));
            dt.Columns.Add(dc);
            colDur = nextDur + 1;
            nextDur = nextDur + duration;
        }

        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        return ds;

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
        bool dispLastTotal = false;
        divPrint.Visible = true;
        DataSet dsGird = GenerateGridColumns();
        DataSet ds = GetCustDebitData();
        ds = GetReceivedAmount(ds);
        ds = GetCreditData(ds);
        dsGird = UpdateColumnsData(dsGird, ds);
        DataSet dsFinal = ConsolidatedGridColumns();
        ds = UpdateFinalData(dsGird, dsFinal);
        ds = CalculateTotal(ds);
        DataTable dts = new DataTable("OutStanding");
        if (dsGird.Tables[0].Rows.Count > 0)
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
            if (ddlFifthLvl.SelectedIndex > 0)
            {
                dts.Columns.Add(new DataColumn(ddlFifthLvl.SelectedItem.Text));
            }

            if (selLevels.IndexOf("Customer") < 0)
                dts.Columns.Add(new DataColumn("Customer"));
            if (selLevels.IndexOf("ProductDesc") < 0)
                dts.Columns.Add(new DataColumn("ProductDesc"));
            if (selLevels.IndexOf("CategoryName") < 0)
                dts.Columns.Add(new DataColumn("CategoryName"));
            if (selLevels.IndexOf("Model") < 0)
                dts.Columns.Add(new DataColumn("Model"));
            if (selLevels.IndexOf("ItemCode") < 0)
                dts.Columns.Add(new DataColumn("ItemCode"));

            int columnNo = ds.Tables[0].Columns.Count - 7;
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
            dts.Columns.Add(new DataColumn("Total"));

            //initialize column values for entire row
            string fLvlValue = "", sLvlValue = "", tLvlValue = "", frthLvlValue = "", fifthLvlValue = "";
            string fLvlValueTemp = "", sLvlValueTemp = "", tLvlValueTemp = "", frthLvlValueTemp = "", fifthLvlValueTemp = "";

            foreach (DataRow dr in dsGird.Tables[0].Rows)
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
                if (ddlFifthLvl.SelectedIndex > 0)
                    frthLvlValueTemp = dr[ddlFifthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                dispLastTotal = true;

                if (ddlFifthLvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                        (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                        (fifthLvlValue != "" && fifthLvlValue != fifthLvlValueTemp))
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
                            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "Total:" + fifthLvlValue;
                        }
                        if (selLevels.IndexOf("Customer") < 0)
                            dr_final7["Customer"] = "";
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

                        dr_final7["Total"] = Convert.ToString(Convert.ToDecimal(Pttl));
                        dts.Rows.Add(dr_final7);
                        Pttls = 0;
                    }
                }

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
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("Customer") < 0)
                            dr_final7["Customer"] = "";
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

                        dr_final7["Total"] = Convert.ToString(Convert.ToDecimal(Pttls));
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
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("Customer") < 0)
                            dr_final8["Customer"] = "";
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

                        dr_final8["Total"] = Convert.ToString(Convert.ToDecimal(modelTotal));
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
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("Customer") < 0)
                            dr_final8["Customer"] = "";
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

                        dr_final8["Total"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
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
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("Customer") < 0)
                            dr_final8["Customer"] = "";
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

                        dr_final8["Total"] = Convert.ToString(Convert.ToDecimal(brandTotal));
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
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("Customer") < 0)
                            dr_final1["Customer"] = "";
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

                        dr_final1["Total"] = "";
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
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final2[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("Customer") < 0)
                            dr_final2["Customer"] = "";
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

                        dr_final2["Total"] = "";
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
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("Customer") < 0)
                            dr_final1["Customer"] = "";
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

                        dr_final1["Total"] = "";
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
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("Customer") < 0)
                            dr_final1["Customer"] = "";
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

                        dr_final1["Total"] = "";
                        dts.Rows.Add(dr_final1);
                    }
                }

                if (ddlFourthLvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp) ||
                        (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp) ||
                        (fifthLvlValueTemp != "" && fifthLvlValue != fifthLvlValueTemp))
                    {
                        DataRow dr_final1 = dts.NewRow();

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
                            dr_final1[ddlFourthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlFifthLvl.SelectedItem.Text] = frthLvlValueTemp;
                        }
                        if (selLevels.IndexOf("Customer") < 0)
                            dr_final1["Customer"] = "";
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

                        dr_final1["Total"] = "";
                        dts.Rows.Add(dr_final1);
                    }
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;
                frthLvlValue = frthLvlValueTemp;
                fifthLvlValue = fifthLvlValueTemp;
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
                if (ddlFifthLvl.SelectedIndex > 0)
                {
                    dr_final5[ddlFifthLvl.SelectedItem.Text] = "";
                }
                if (selLevels.IndexOf("Customer") < 0)
                    dr_final5["Customer"] = dr["Customer"].ToString();
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

                dr_final5["Total"] = dr["Total"];
                dts.Rows.Add(dr_final5);
                Gtotal = Gtotal + Convert.ToDecimal(dr["Total"]);
                modelTotal = modelTotal + Convert.ToDecimal(dr["Total"]);
                catIDTotal = catIDTotal + Convert.ToDecimal(dr["Total"]);
                Pttls = Pttls + Convert.ToDecimal(dr["Total"]);
                Pttl = Pttl + Convert.ToDecimal(dr["Total"]);
                brandTotal = brandTotal + Convert.ToDecimal(dr["Total"]);
            }
            //Display the last Total and Grand Total
            if (dispLastTotal)
            {
                if (ddlFifthLvl.SelectedIndex > 0)
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
                        dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                    }
                    dr_final7[ddlFifthLvl.SelectedItem.Text] = "Total:" + fifthLvlValueTemp;

                    if (selLevels.IndexOf("Customer") < 0)
                        dr_final7["Customer"] = "";
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

                    dr_final7["Total"] = Convert.ToString(Convert.ToDecimal(Pttl));
                    dts.Rows.Add(dr_final7);
                    Pttls = 0;
                }

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
                    if (ddlFifthLvl.SelectedIndex > 0)
                    {
                        dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                    }
                    if (selLevels.IndexOf("Customer") < 0)
                        dr_final7["Customer"] = "";
                    if (selLevels.IndexOf("Customer") < 0)
                        dr_final7["Customer"] = "";
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

                    dr_final7["Total"] = Convert.ToString(Convert.ToDecimal(Pttls));
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
                    if (ddlFifthLvl.SelectedIndex > 0)
                    {
                        dr_final8[ddlFifthLvl.SelectedItem.Text] = "";
                    }
                    if (selLevels.IndexOf("Customer") < 0)
                        dr_final8["Customer"] = "";
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

                    dr_final8["Total"] = Convert.ToString(Convert.ToDecimal(modelTotal));
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
                    if (ddlFifthLvl.SelectedIndex > 0)
                    {
                        dr_final9[ddlFifthLvl.SelectedItem.Text] = "";
                    }
                    if (selLevels.IndexOf("Customer") < 0)
                        dr_final9["Customer"] = "";
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

                    dr_final9["Total"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
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
                    if (ddlFifthLvl.SelectedIndex > 0)
                    {
                        dr_final10[ddlFifthLvl.SelectedItem.Text] = "";
                    }
                    if (selLevels.IndexOf("Customer") < 0)
                        dr_final10["Customer"] = "";
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

                    dr_final10["Total"] = Convert.ToString(Convert.ToDecimal(brandTotal));
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
                if (ddlFifthLvl.SelectedIndex > 0)
                {
                    dr_final6[ddlFifthLvl.SelectedItem.Text] = "";
                }
                if (selLevels.IndexOf("Customer") < 0)
                    dr_final6["Customer"] = "";
                if (selLevels.IndexOf("ProductDesc") < 0)
                    dr_final6["ProductDesc"] = "";
                if (selLevels.IndexOf("CategoryName") < 0)
                    dr_final6["CategoryName"] = "";
                if (selLevels.IndexOf("Model") < 0)
                    dr_final6["Model"] = "";
                if (selLevels.IndexOf("ItemCode") < 0)
                    dr_final6["ItemCode"] = "";

                dr_final6["Total"] = Convert.ToDecimal(Gtotal);
                dts.Rows.Add(dr_final6);
            }
            ExportToExcel(dts);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
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

    public DataSet ConsolidatedGridColumn()
    {
        int duration = int.Parse(txtDuration.Text);
        int noOfColumns = int.Parse(txtColumns.Text);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn dc;

        if (ddlFirstLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(firstLevel);
            dt.Columns.Add(dc);
        }

        if (ddlSecondLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(secondLevel);
            dt.Columns.Add(dc);
        }

        if (ddlThirdLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(thirdLevel);
            dt.Columns.Add(dc);
        }

        if (ddlFourthLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(fourthLevel);
            dt.Columns.Add(dc);
        }
        if (ddlFifthLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(fifthLevel);
            dt.Columns.Add(dc);
        }

        if (selLevels.IndexOf("Customer") < 0)
            dt.Columns.Add(new DataColumn("Customer"));
        if (selLevels.IndexOf("ProductDesc") < 0)
            dt.Columns.Add(new DataColumn("ProductDesc"));
        if (selLevels.IndexOf("CategoryName") < 0)
            dt.Columns.Add(new DataColumn("CategoryName"));
        if (selLevels.IndexOf("Model") < 0)
            dt.Columns.Add(new DataColumn("Model"));
        if (selLevels.IndexOf("ItemCode") < 0)
            dt.Columns.Add(new DataColumn("ItemCode"));

        int colDur = 0;
        int nextDur = 0;

        for (int i = 0; i < noOfColumns; i++)
        {
            nextDur = nextDur + duration;
            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")");
            dt.Columns.Add(dc);
            colDur = nextDur + 1;
        }

        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
        dt.Columns.Add(dc);

        //dc = new DataColumn("Ledger Name");
        //dt.Columns.Add(dc);
                
        //dc = new DataColumn("Total");
        //dt.Columns.Add(dc);
        

        
        ds.Tables.Add(dt);

        return ds;

    }

    public DataSet ConsolidatedGridColumns()
    {
        int duration = int.Parse(txtDuration.Text);
        int noOfColumns = int.Parse(txtColumns.Text);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataColumn dc;

        if (ddlFirstLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(firstLevel);
            dt.Columns.Add(dc);
        }

        if (ddlSecondLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(secondLevel);
            dt.Columns.Add(dc);
        }

        if (ddlThirdLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(thirdLevel);
            dt.Columns.Add(dc);
        }

        if (ddlFourthLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(fourthLevel);
            dt.Columns.Add(dc);
        }
        if (ddlFifthLvl.SelectedIndex > 0)
        {
            dc = new DataColumn(fifthLevel);
            dt.Columns.Add(dc);
        }

        if (selLevels.IndexOf("Customer") < 0)
            dt.Columns.Add(new DataColumn("Customer"));
        if (selLevels.IndexOf("ProductDesc") < 0)
            dt.Columns.Add(new DataColumn("ProductDesc"));
        if (selLevels.IndexOf("CategoryName") < 0)
            dt.Columns.Add(new DataColumn("CategoryName"));
        if (selLevels.IndexOf("Model") < 0)
            dt.Columns.Add(new DataColumn("Model"));
        if (selLevels.IndexOf("ItemCode") < 0)
            dt.Columns.Add(new DataColumn("ItemCode"));
            
        int colDur = 0;
        int nextDur = 0;

        for (int i = 0; i < noOfColumns; i++)
        {
            nextDur = nextDur + duration;
            dc = new DataColumn("Days(" + colDur.ToString() + "-" + nextDur.ToString() + ")");
            dt.Columns.Add(dc);
            colDur = nextDur + 1;
        }

        dc = new DataColumn("Days(" + nextDur.ToString() + ") Above");
        dt.Columns.Add(dc);

        ds.Tables.Add(dt);

        return ds;

    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes["align"] = "Left";
                int noofCols = int.Parse(txtColumns.Text) + 4;
                for (int i = 1; i <= noofCols; i++)
                {
                    e.Row.Cells[i].Attributes["align"] = "Right";
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private bool isValidLevels()
    {
        if ((ddlFirstLvl.SelectedItem.Text != "None") &&
            (ddlFirstLvl.SelectedItem.Text == ddlSecondLvl.SelectedItem.Text ||
            ddlFirstLvl.SelectedItem.Text == ddlThirdLvl.SelectedItem.Text ||
            ddlFirstLvl.SelectedItem.Text == ddlFourthLvl.SelectedItem.Text ||
            ddlFirstLvl.SelectedItem.Text == ddlFifthLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddlSecondLvl.SelectedItem.Text != "None") &&
            (ddlSecondLvl.SelectedItem.Text == ddlThirdLvl.SelectedItem.Text ||
            ddlSecondLvl.SelectedItem.Text == ddlFourthLvl.SelectedItem.Text ||
            ddlSecondLvl.SelectedItem.Text == ddlFifthLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddlThirdLvl.SelectedItem.Text != "None") &&
            (ddlThirdLvl.SelectedItem.Text == ddlFourthLvl.SelectedItem.Text ||
            ddlThirdLvl.SelectedItem.Text == ddlFifthLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddlFourthLvl.SelectedItem.Text != "None") &&
            (ddlFourthLvl.SelectedItem.Text == ddlFifthLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        return true;
    }

    protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            double debit = 0;
            double credit = 0;

            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[2].Visible = false;
            //e.Row.Cells[3].Visible = false;
            //e.Row.Cells[4].Visible = false;
            //e.Row.Cells[5].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[6].Visible = false;
            //e.Row.Cells[7].Visible = false;
            //e.Row.Cells[8].Visible = false;
            //e.Row.Cells[9].Visible = false;

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (DataBinder.Eval(e.Row.DataItem, "Total") == null)
            //    {
            //        debit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            //    }
            //    else
            //    {
            //        debit = 0;
            //    }

            //    //credit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
            //    damt = damt + debit;
            //    //camt = camt + credit;




            //    //lblDebitSum.Text = damt.ToString("f2");  //Convert.ToString(damt);
            //    //lblCreditSum.Text = camt.ToString("f2");
            //    //lblCreditSum.Text = String.Format("{0:f2}", lblCreditSum.Text);
            //    //lblDebitSum.Text = String.Format("{0:f2}", lblDebitSum.Text);
            //    //dDiffamt = damt - camt;
            //    //cDiffamt = camt - damt;
            //    //e.Row.Cells[1].Text = debit.ToString("f2");
            //    //e.Row.Cells[2].Text = credit.ToString("f2");
            //    /*Start Outstanding Report*/
            //    //Label lblBal = (Label)e.Row.FindControl("Label2");
            //    /*End Outstanding Report*/
            //    //if (dDiffamt >= 0)
            //    //{
            //    //Label2.Text = damt.ToString("f2");
            //    //lblCreditDiff.Text = "0.00";
            //    //lblBal.Text = damt.ToString("f2") + " Dr";
            //    //lblBal.ForeColor = System.Drawing.Color.Blue;

            //    //}
            //    //if (cDiffamt > 0)
            //    //{
            //    //    lblDebitDiff.Text = "0.00";
            //    //    lblCreditDiff.Text = cDiffamt.ToString("f2");
            //    //    lblBal.Text = cDiffamt.ToString("f2") + " Cr";
            //    //    lblBal.ForeColor = System.Drawing.Color.Red;

            //    //}

            //}

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Cells[0].Attributes["align"] = "Left";
            //    int noofCols = int.Parse(txtColumns.Text) + 4;
            //    for (int i = 1; i <= noofCols; i++)
            //    {
            //        e.Row.Cells[i].Attributes["align"] = "Right";
            //    }
            //}




        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
            
    }

    protected void GVPay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double debit = 0;
            double cdit = 0;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                cdit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "balance"));
                //credit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
                ddamt = ddamt + cdit;
                //camt = camt + credit;



                //lblDebitSum.Text = damt.ToString("f2");  //Convert.ToString(damt);
                //lblCreditSum.Text = camt.ToString("f2");
                //lblCreditSum.Text = String.Format("{0:f2}", lblCreditSum.Text);
                //lblDebitSum.Text = String.Format("{0:f2}", lblDebitSum.Text);
                //dDiffamt = damt - camt;
                //cDiffamt = camt - damt;
                //e.Row.Cells[1].Text = debit.ToString("f2");
                //e.Row.Cells[2].Text = credit.ToString("f2");
                /*Start Outstanding Report*/
                //Label lblBal = (Label)e.Row.FindControl("Label2");
                /*End Outstanding Report*/
                //if (dDiffamt >= 0)
                //{
                Label3.Text = ddamt.ToString("f2");
                //lblCreditDiff.Text = "0.00";
                //lblBal.Text = damt.ToString("f2") + " Dr";
                //lblBal.ForeColor = System.Drawing.Color.Blue;

                //}
                //if (cDiffamt > 0)
                //{
                //    lblDebitDiff.Text = "0.00";
                //    lblCreditDiff.Text = cDiffamt.ToString("f2");
                //    lblBal.Text = cDiffamt.ToString("f2") + " Cr";
                //    lblBal.ForeColor = System.Drawing.Color.Red;

                //}

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    
    
}
