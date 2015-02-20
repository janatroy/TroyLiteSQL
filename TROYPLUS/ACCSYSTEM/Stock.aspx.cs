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

public partial class Stock : System.Web.UI.Page
{
    int catid = 0;
    string brand = "";
    string model = "";
    string product = "";
    decimal Gtotal = 0;
    decimal Gttl = 0;
    decimal Pttls = 0;
    decimal brandTotal = 0, catIDTotal = 0, modelTotal = 0;
    string grpBy = "", selCols = "";

    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!Page.IsPostBack)
            {
                fillfBrand();
                fillCategorys();
                fillProduct();
                fillModel();
                fillProduct();
                //fillStock();
                ddlFirst();
                ddlSconed();
                ddlThird();
                ddlFour();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void ddlFirst()
    {
        ddlfirstlvl.Items.Insert(0, "None");
        ddlfirstlvl.Items.Insert(1, "Brand");
        ddlfirstlvl.Items.Insert(2, "CategoryID");
        ddlfirstlvl.Items.Insert(3, "Model");
        ddlfirstlvl.Items.Insert(4, "ProductName");
    }
    private void ddlSconed()
    {
        ddlsecondlvl.Items.Insert(0, "None");
        ddlsecondlvl.Items.Insert(1, "Brand");
        ddlsecondlvl.Items.Insert(2, "CategoryID");
        ddlsecondlvl.Items.Insert(3, "Model");
        ddlsecondlvl.Items.Insert(4, "ProductName");
    }
    private void ddlThird()
    {
        ddlthirdlvl.Items.Insert(0, "None");
        ddlthirdlvl.Items.Insert(1, "Brand");
        ddlthirdlvl.Items.Insert(2, "CategoryID");
        ddlthirdlvl.Items.Insert(3, "Model");
        ddlthirdlvl.Items.Insert(4, "ProductName");
    }
    private void ddlFour()
    {
        ddlfourlvl.Items.Insert(0, "None");
        ddlfourlvl.Items.Insert(1, "Brand");
        ddlfourlvl.Items.Insert(2, "CategoryID");
        ddlfourlvl.Items.Insert(3, "Model");
        ddlfourlvl.Items.Insert(4, "ProductName");
    }
    private void fillfBrand()
    {
        DataSet ds = new DataSet();
        BusinessLogic objBL = new BusinessLogic(sDataSource);
        ds = objBL.getDistinctBrand();
        ddlBrand.DataSource = ds;
        ddlBrand.DataTextField = "Brand";
        ddlBrand.DataValueField = "Brand";
        ddlBrand.DataBind();
        ddlBrand.Items.Insert(0, "All");
    }
    private void fillCategorys()
    {
        DataSet ds = new DataSet();
        BusinessLogic objBL = new BusinessLogic(sDataSource);
        ds = objBL.getDistinctCategorys();
        ddlCategory.DataSource = ds;
        ddlCategory.DataTextField = "CategoryID";
        ddlCategory.DataValueField = "CategoryID";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, "All");
    }
    private void fillModel()
    {
        DataSet ds = new DataSet();
        BusinessLogic objBL = new BusinessLogic(sDataSource);
        ds = objBL.getDistinctModel();
        ddlMdl.DataSource = ds;
        ddlMdl.DataTextField = "Model";
        ddlMdl.DataValueField = "Model";
        ddlMdl.DataBind();
        ddlMdl.Items.Insert(0, "All");
    }
    private void fillProduct()
    {
        BusinessLogic objBL = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = objBL.getDistinctPrdctNme();
        ddlPrdctNme.DataSource = ds;
        ddlPrdctNme.DataTextField = "ProductName";
        ddlPrdctNme.DataValueField = "ProductName";
        ddlPrdctNme.DataBind();
        ddlPrdctNme.Items.Insert(0, "All");
    }
    protected void btnData_Click(object sender, EventArgs e)
    {
        try
        {
            Total = 0;
            string cond = "", grpBy = "";
            cond = getCond();
            getGrpByAndSelCols();
            BusinessLogic objBL = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();
            ds = objBL.getStock(selCols, cond, grpBy);
            GridStock.DataSource = ds;
            GridStock.DataBind();
            //var lblTotalQty = GridStock.FooterRow.FindControl("lblTotalQty") as Label;
            //if (lblTotalQty != null)
            //{
            //    lblTotalQty.Text = Total.ToString();
            //}
            var lblTotalRate = GridStock.FooterRow.FindControl("lblTotalRate") as Label;
            if (lblTotalRate != null)
            {
                lblTotalRate.Text = Total.ToString();
            }
            var lblVaule = GridStock.FooterRow.FindControl("lblVaule") as Label;
            if (lblVaule != null)
            {
                lblVaule.Text = Total.ToString();
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
        BusinessLogic objBL = new BusinessLogic(sDataSource);
        //where ProductDesc='" + Brand + "' and CategoryID=" + Convert.ToInt32(Categorys) + " and Model='" + Models + "' and ProductName='" + PrdctNme + "'
        if (ddlBrand.SelectedIndex > 0)
        {
            objBL.Brands = ddlBrand.SelectedItem.Text;
            cond = " where ProductDesc='" + ddlBrand.SelectedItem.Text + "' ";
        }
        if (ddlCategory.SelectedIndex > 0)
        {
            if (cond == "")
            {
                objBL.Categorys = Convert.ToInt32(ddlCategory.SelectedItem.Text);
                cond = " where ";
            }
            else
            {
                cond += " and ";
            }
            cond += " CategoryID=" + Convert.ToInt32(ddlCategory.SelectedItem.Text) + " ";
        }
        if (ddlMdl.SelectedIndex > 0)
        {
            if (cond == "")
            {
                objBL.Models = ddlMdl.SelectedItem.Text;
                cond = " where ";
            }
            else
            {
                cond += " and ";
            }
            cond += " Model='" + ddlMdl.SelectedItem.Text + "' ";
        }
        if (ddlPrdctNme.SelectedIndex > 0)
        {
            if (cond == "")
            {
                objBL.PrdctNmes = ddlPrdctNme.SelectedItem.Text;
                cond = " where ";
            }
            else
            {
                cond += " and ";
            }
            cond += " ProductName='" + ddlPrdctNme.SelectedItem.Text + "' ";
        }
        if (ddlStock.SelectedIndex > 0)
        {
            if (cond == "")
            {
                //objBL.Stock = ddlStock.SelectedItem.Text;
                cond = " where ";
            }
            else
            {
                cond += " and ";
            }
            cond += " Stock > 0 ";
        }
        //objBL.Stock = Convert.ToInt32(ddlStock.SelectedItem.Text);
        return cond;
    }
    protected void getGrpByAndSelCols()
    {
        grpBy = "";
        selCols = "";
        if (ddlfirstlvl.SelectedIndex > 0)
        {
            grpBy = " group by " + ddlfirstlvl.SelectedItem.Text + " ";
            selCols = " " + ddlfirstlvl.SelectedItem.Text + " ";
        }
        if (ddlsecondlvl.SelectedIndex > 0)
        {
            if (grpBy == "")
            {
                grpBy = " group by ";
                selCols = " ";
            }
            else
            {
                grpBy += " , ";
                selCols += " , ";
            }
            grpBy += " " + ddlsecondlvl.SelectedItem.Text + " ";
            selCols += " " + ddlsecondlvl.SelectedItem.Text + " ";
        }
        if (ddlthirdlvl.SelectedIndex > 0)
        {
            if (grpBy == "")
            {
                grpBy = " group by ";
                selCols = " ";
            }
            else
            {
                grpBy += " , ";
                selCols += " , ";
            }
            grpBy += " " + ddlthirdlvl.SelectedItem.Text + " ";
            selCols += " " + ddlthirdlvl.SelectedItem.Text + " ";
        }
        if (ddlfourlvl.SelectedIndex > 0)
        {
            if (grpBy == "")
            {
                grpBy = " group by ";
                selCols = " ";
            }
            else
            {
                grpBy += " , ";
                selCols += " , ";
            }
            grpBy += " " + ddlfourlvl.SelectedItem.Text + " ";
            selCols += " " + ddlfourlvl.SelectedItem.Text + " ";
        }
        if (grpBy == "")
        {
            grpBy = " group by ItemCode";
            selCols = " ItemCode ";
        }
        else
        {
            grpBy += " , ItemCode, Rate ";
            selCols += " , ItemCode , ";
        }
        selCols = selCols.Replace("Brand", "ProductDesc As Brand");
        grpBy = grpBy.Replace("Brand", "ProductDesc");
    }
    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            string cond = "";
            cond = getCond();
            getGrpByAndSelCols();
            bindData(selCols, cond, grpBy);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void bindData(string selCols, string cond, string grpBy)
    {
        bool dispLastTotal = false;
        BusinessLogic objBL = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = objBL.getStock(selCols, cond, grpBy);
        DataTable dt = new DataTable();
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ddlfirstlvl.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlfirstlvl.SelectedItem.Text));
            }
            if (ddlsecondlvl.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlsecondlvl.SelectedItem.Text));
            }
            if (ddlthirdlvl.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlthirdlvl.SelectedItem.Text));
            }
            if (ddlfourlvl.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlfourlvl.SelectedItem.Text));
            }
            //dt.Columns.Add(new DataColumn("Brand"));
            //dt.Columns.Add(new DataColumn("Category ID"));
            //dt.Columns.Add(new DataColumn("Model"));
            //dt.Columns.Add(new DataColumn("Product Name"));

            dt.Columns.Add(new DataColumn("Item Code"));
            dt.Columns.Add(new DataColumn("Quantity"));
            dt.Columns.Add(new DataColumn("Rate"));
            dt.Columns.Add(new DataColumn("Value"));

            //initialize column values for entire row
            string fLvlValue = "", sLvlValue = "", tLvlValue = "", frthLvlValue = "";
            string fLvlValueTemp = "", sLvlValueTemp = "", tLvlValueTemp = "", frthLvlValueTemp = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //initialize column values for entire row
                if (ddlfirstlvl.SelectedIndex > 0)
                    fLvlValueTemp = dr[ddlfirstlvl.SelectedItem.Text].ToString().ToUpper().Trim();
                if (ddlsecondlvl.SelectedIndex > 0)
                    sLvlValueTemp = dr[ddlsecondlvl.SelectedItem.Text].ToString().ToUpper().Trim();
                if (ddlthirdlvl.SelectedIndex > 0)
                    tLvlValueTemp = dr[ddlthirdlvl.SelectedItem.Text].ToString().ToUpper().Trim();
                if (ddlfourlvl.SelectedIndex > 0)
                    frthLvlValueTemp = dr[ddlfourlvl.SelectedItem.Text].ToString().ToUpper().Trim();
                dispLastTotal = true;
                if (ddlfourlvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                        (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp))
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlfirstlvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlfirstlvl.SelectedItem.Text] = "";
                        }
                        if (ddlsecondlvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlsecondlvl.SelectedItem.Text] = "";
                        }
                        if (ddlthirdlvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlthirdlvl.SelectedItem.Text] = "";
                        }
                        if (ddlfourlvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlfourlvl.SelectedItem.Text] = "Total:" + frthLvlValue;
                        }
                        //dr_final7["Brand"] = "";
                        ////dr_final7[ddlOrdBySel1.selectedvalue] = "";
                        //dr_final7["Category ID"] = "";
                        //dr_final7["Model"] = "";
                        //dr_final7["Product Name"] = "";

                        //dr_final7["Item Code"] = "Total:" + frthLvlValueTemp;
                        dr_final7["Item Code"] = "";
                        dr_final7["Quantity"] = "";
                        dr_final7["Rate"] = "";
                        dr_final7["Value"] = Convert.ToString(Convert.ToDecimal(Pttls));
                        dt.Rows.Add(dr_final7);
                        Pttls = 0;
                    }
                }

                if (ddlthirdlvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final8 = dt.NewRow();
                        if (ddlfirstlvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlfirstlvl.SelectedItem.Text] = "";
                        }
                        if (ddlsecondlvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlsecondlvl.SelectedItem.Text] = "";
                        }
                        if (ddlthirdlvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlthirdlvl.SelectedItem.Text] = "Total " + tLvlValue;
                        }
                        if (ddlfourlvl.SelectedIndex > 0)
                        {
                            //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                            dr_final8[ddlfourlvl.SelectedItem.Text] = "";
                        }

                        dr_final8["Item Code"] = "";
                        dr_final8["Quantity"] = "";
                        dr_final8["Rate"] = "";
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                        dt.Rows.Add(dr_final8);
                        //dt.Rows.Add(dr_final8);
                        modelTotal = 0;
                    }
                }

                if (ddlsecondlvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final8 = dt.NewRow();
                        if (ddlfirstlvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlfirstlvl.SelectedItem.Text] = "";
                        }
                        if (ddlsecondlvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlsecondlvl.SelectedItem.Text] = "Total " + sLvlValue;
                        }
                        if (ddlthirdlvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlthirdlvl.SelectedItem.Text] = "";
                        }
                        if (ddlfourlvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlfourlvl.SelectedItem.Text] = "";
                        }

                        dr_final8["Item Code"] = "";
                        dr_final8["Quantity"] = "";
                        dr_final8["Rate"] = "";
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                        dt.Rows.Add(dr_final8);
                        catIDTotal = 0;
                    }
                }
                if (ddlfirstlvl.SelectedIndex > 0)
                {
                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        if (ddlfirstlvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlfirstlvl.SelectedItem.Text] = "Total " + fLvlValue;
                        }
                        if (ddlsecondlvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlsecondlvl.SelectedItem.Text] = "";
                        }
                        if (ddlthirdlvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlthirdlvl.SelectedItem.Text] = "";
                        }
                        if (ddlfourlvl.SelectedIndex > 0)
                        {
                            dr_final8[ddlfourlvl.SelectedItem.Text] = "";
                        }
                        dr_final8["Item Code"] = "";
                        dr_final8["Quantity"] = "";
                        dr_final8["Rate"] = "";
                        dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                        dt.Rows.Add(dr_final8);
                        brandTotal = 0;
                    }
                }
                if (ddlfirstlvl.SelectedIndex > 0)
                {
                    if (fLvlValueTemp != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final1 = dt.NewRow();
                        if (ddlfirstlvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlfirstlvl.SelectedItem.Text] = fLvlValueTemp;
                        }
                        if (ddlsecondlvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlsecondlvl.SelectedItem.Text] = "";
                        }
                        if (ddlthirdlvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlthirdlvl.SelectedItem.Text] = "";
                        }
                        if (ddlfourlvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlfourlvl.SelectedItem.Text] = "";
                        }
                        dr_final1["Item Code"] = "";
                        dr_final1["Quantity"] = "";
                        dr_final1["Rate"] = "";
                        dr_final1["Value"] = "";
                        dt.Rows.Add(dr_final1);
                    }
                }
                if (ddlsecondlvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final2 = dt.NewRow();
                        if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp))
                        {
                            if (ddlsecondlvl.SelectedIndex > 0)
                            {
                                dr_final2[ddlsecondlvl.SelectedItem.Text] = sLvlValueTemp;
                            }
                        }
                        if (ddlfirstlvl.SelectedIndex > 0)
                        {
                            dr_final2[ddlfirstlvl.SelectedItem.Text] = "";
                        }
                        if (ddlsecondlvl.SelectedIndex > 0)
                        {
                            dr_final2[ddlsecondlvl.SelectedItem.Text] = sLvlValueTemp;
                        }
                        if (ddlthirdlvl.SelectedIndex > 0)
                        {
                            dr_final2[ddlthirdlvl.SelectedItem.Text] = "";
                        }
                        if (ddlfourlvl.SelectedIndex > 0)
                        {
                            dr_final2[ddlfourlvl.SelectedItem.Text] = "";
                        }

                        dr_final2["Item Code"] = "";
                        dr_final2["Quantity"] = "";
                        dr_final2["Rate"] = "";
                        dr_final2["Value"] = "";
                        dt.Rows.Add(dr_final2);
                    }
                }
                if (ddlthirdlvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final1 = dt.NewRow();
                        if (fLvlValueTemp != "" && fLvlValue != fLvlValueTemp)
                        {
                            if (ddlthirdlvl.SelectedIndex > 0)
                            {
                                dr_final1[ddlthirdlvl.SelectedItem.Text] = tLvlValueTemp;
                            }
                        }
                        //else
                        //{
                        if (ddlfirstlvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlfirstlvl.SelectedItem.Text] = "";
                        }
                        if (ddlsecondlvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlsecondlvl.SelectedItem.Text] = "";
                        }
                        if (ddlthirdlvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlthirdlvl.SelectedItem.Text] = tLvlValueTemp;
                        }
                        if (ddlfourlvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlfourlvl.SelectedItem.Text] = "";
                        }
                        dr_final1["Item Code"] = "";
                        dr_final1["Quantity"] = "";
                        dr_final1["Rate"] = "";
                        dr_final1["Value"] = "";
                        dt.Rows.Add(dr_final1);
                    }
                }
                if (ddlfourlvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp) ||
                        (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp))
                    {
                        DataRow dr_final1 = dt.NewRow();
                        if (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp)
                        {
                            if (ddlfourlvl.SelectedIndex > 0)
                            {
                                dr_final1[ddlfourlvl.SelectedItem.Text] = frthLvlValueTemp;
                            }
                            else
                            {
                                if (ddlfirstlvl.SelectedIndex > 0)
                                {
                                    dr_final1[ddlfirstlvl.SelectedItem.Text] = "";
                                }
                                if (ddlsecondlvl.SelectedIndex > 0)
                                {
                                    dr_final1[ddlsecondlvl.SelectedItem.Text] = "";
                                }
                                if (ddlthirdlvl.SelectedIndex > 0)
                                {
                                    dr_final1[ddlthirdlvl.SelectedItem.Text] = "";
                                }
                                if (ddlfourlvl.SelectedIndex > 0)
                                {
                                    dr_final1[ddlfourlvl.SelectedItem.Text] = frthLvlValueTemp;
                                }
                            }
                        }

                        dr_final1["Item Code"] = "";
                        dr_final1["Quantity"] = "";
                        dr_final1["Rate"] = "";
                        dr_final1["Value"] = "";
                        dt.Rows.Add(dr_final1);
                    }
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;
                frthLvlValue = frthLvlValueTemp;
                DataRow dr_final5 = dt.NewRow();
                if (ddlfirstlvl.SelectedIndex > 0)
                {

                    dr_final5[ddlfirstlvl.SelectedItem.Text] = "";
                }

                if (ddlsecondlvl.SelectedIndex > 0)
                {
                    dr_final5[ddlsecondlvl.SelectedItem.Text] = "";
                }
                if (ddlthirdlvl.SelectedIndex > 0)
                {
                    dr_final5[ddlthirdlvl.SelectedItem.Text] = "";
                }
                if (ddlfourlvl.SelectedIndex > 0)
                {
                    dr_final5[ddlfourlvl.SelectedItem.Text] = "";
                }
                dr_final5["Item Code"] = dr["ItemCode"];
                dr_final5["Quantity"] = dr["Quantity"];
                dr_final5["Rate"] = dr["Rate"];
                dr_final5["Value"] = dr["Vaule"];
                dt.Rows.Add(dr_final5);
                Gtotal = Gtotal + Convert.ToDecimal(dr["Vaule"]);
                Gttl = Gttl + Convert.ToInt32(dr["Quantity"]);
                modelTotal = modelTotal + Convert.ToDecimal(dr["Vaule"]);
                catIDTotal = catIDTotal + Convert.ToDecimal(dr["Vaule"]);
                Pttls = Pttls + Convert.ToDecimal(dr["Vaule"]);
                brandTotal = brandTotal + Convert.ToDecimal(dr["Vaule"]);
            }

            //Display the last Total and Grand Total
            if (dispLastTotal)
            {
                if (ddlfourlvl.SelectedIndex > 0)
                {
                    DataRow dr_final7 = dt.NewRow();
                    if (ddlfirstlvl.SelectedIndex > 0)
                    {
                        dr_final7[ddlfirstlvl.SelectedItem.Text] = "";
                    }
                    if (ddlsecondlvl.SelectedIndex > 0)
                    {
                        dr_final7[ddlsecondlvl.SelectedItem.Text] = "";
                    }
                    if (ddlthirdlvl.SelectedIndex > 0)
                    {
                        dr_final7[ddlthirdlvl.SelectedItem.Text] = "";
                    }
                    dr_final7[ddlfourlvl.SelectedItem.Text] = "Total:" + frthLvlValueTemp;
                    dr_final7["Item Code"] = "";
                    dr_final7["Quantity"] = "";
                    dr_final7["Rate"] = "";
                    dr_final7["Value"] = Convert.ToString(Convert.ToDecimal(Pttls));
                    dt.Rows.Add(dr_final7);
                    Pttls = 0;
                }

                if (ddlthirdlvl.SelectedIndex > 0)
                {
                    DataRow dr_final8 = dt.NewRow();
                    if (ddlfirstlvl.SelectedIndex > 0)
                    {
                        dr_final8[ddlfirstlvl.SelectedItem.Text] = "";
                    }
                    if (ddlsecondlvl.SelectedIndex > 0)
                    {
                        dr_final8[ddlsecondlvl.SelectedItem.Text] = "";
                    }
                    if (ddlthirdlvl.SelectedIndex > 0)
                    {
                        dr_final8[ddlthirdlvl.SelectedItem.Text] = "Total: " + tLvlValueTemp;
                    }
                    if (ddlfourlvl.SelectedIndex > 0)
                    {
                        dr_final8[ddlfourlvl.SelectedItem.Text] = "";
                    }
                    dr_final8["Item Code"] = "";
                    dr_final8["Quantity"] = "";
                    dr_final8["Rate"] = "";
                    dr_final8["Value"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                    dt.Rows.Add(dr_final8);
                    modelTotal = 0;
                }

                if (ddlsecondlvl.SelectedIndex > 0)
                {
                    DataRow dr_final9 = dt.NewRow();
                    if (ddlfirstlvl.SelectedIndex > 0)
                    {
                        dr_final9[ddlfirstlvl.SelectedItem.Text] = "";
                    }
                    if (ddlsecondlvl.SelectedIndex > 0)
                    {
                        dr_final9[ddlsecondlvl.SelectedItem.Text] = "Total: " + sLvlValueTemp;
                    }
                    if (ddlthirdlvl.SelectedIndex > 0)
                    {
                        dr_final9[ddlthirdlvl.SelectedItem.Text] = "";
                    }
                    if (ddlfourlvl.SelectedIndex > 0)
                    {
                        dr_final9[ddlfourlvl.SelectedItem.Text] = "";
                    }
                    dr_final9["Item Code"] = "";
                    dr_final9["Quantity"] = "";
                    dr_final9["Rate"] = "";
                    dr_final9["Value"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                    dt.Rows.Add(dr_final9);
                    catIDTotal = 0;
                }

                if (ddlfirstlvl.SelectedIndex > 0)
                {
                    DataRow dr_final10 = dt.NewRow();
                    if (ddlfirstlvl.SelectedIndex > 0)
                    {
                        dr_final10[ddlfirstlvl.SelectedItem.Text] = "Total: " + fLvlValueTemp;
                    }
                    if (ddlsecondlvl.SelectedIndex > 0)
                    {
                        dr_final10[ddlsecondlvl.SelectedItem.Text] = "";
                    }
                    if (ddlthirdlvl.SelectedIndex > 0)
                    {
                        dr_final10[ddlthirdlvl.SelectedItem.Text] = "";
                    }
                    if (ddlfourlvl.SelectedIndex > 0)
                    {
                        dr_final10[ddlfourlvl.SelectedItem.Text] = "";
                    }
                    dr_final10["Item Code"] = "";
                    dr_final10["Quantity"] = "";
                    dr_final10["Rate"] = "";
                    dr_final10["Value"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                    dt.Rows.Add(dr_final10);
                    brandTotal = 0;
                }

                DataRow dr_final6 = dt.NewRow();
                if (ddlfirstlvl.SelectedIndex > 0)
                {
                    dr_final6[ddlfirstlvl.SelectedItem.Text] = "Grand Total: ";
                }
                if (ddlsecondlvl.SelectedIndex > 0)
                {
                    dr_final6[ddlsecondlvl.SelectedItem.Text] = "";
                }
                if (ddlthirdlvl.SelectedIndex > 0)
                {
                    dr_final6[ddlthirdlvl.SelectedItem.Text] = "";
                }
                if (ddlfourlvl.SelectedIndex > 0)
                {
                    dr_final6[ddlfourlvl.SelectedItem.Text] = "";
                }
                dr_final6["Item Code"] = "Grand Total: "; ;
                dr_final6["Quantity"] = Convert.ToInt32(Gttl);
                dr_final6["Rate"] = "";
                dr_final6["Value"] = Convert.ToDecimal(Gtotal);
                dt.Rows.Add(dr_final6);
                ExportToExcel(dt);
            }
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

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
    protected void ddlfirstlvl_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*        if (ddlfirstlvl.SelectedItem.Text == "Brand")
                    ddlsecondlvl.Items.RemoveAt(1);
                     bool dispLstTotal = false;
                    objBL.Brands = ddlfirstlvl.SelectedItem.Text;
                    DataSet ds = new DataSet();
                    ds = objBL.getStockbrand();
                    DataTable dt = new DataTable();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dt.Columns.Add(new DataColumn("ItemCode"));
                        dt.Columns.Add(new DataColumn("ProductName"));
                        dt.Columns.Add(new DataColumn("CategoryID"));
                        dt.Columns.Add(new DataColumn("Brand"));
                        dt.Columns.Add(new DataColumn("Model"));
                        dt.Columns.Add(new DataColumn("Quantity"));
                        dt.Columns.Add(new DataColumn("Rate"));
                        dt.Columns.Add(new DataColumn("Vaule"));
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            dispLstTotal = true;
                            string tmpbrand = dr["Brand"].ToString();
                            tmpbrand = tmpbrand.ToString().ToUpper();
                            if (brand.ToUpper().Trim() == "" || brand.ToUpper().Trim() != dr["Brand"].ToString().ToUpper().Trim() || catid == 0 || catid != Convert.ToInt32(dr["CategoryID"]))
                            {
                                if (brand != "" || catid == 0)
                                {
                                    DataRow dr_final05 = dt.NewRow();
                                    dr_final05["ItemCode"] = "";
                                    dr_final05["ProductName"] = "";
                                    dr_final05["CategoryID"] = "";
                                    dr_final05["Brand"] = "";
                                    dr_final05["Model"] = "Total:" + brand;
                                    dr_final05["Quantity"] = "";
                                    dr_final05["Rate"] = "";
                                    dr_final05["Vaule"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                                    dt.Rows.Add(dr_final05);
                                    Gtotal = 0;
                                }
                            }
                            if (brand.ToUpper().Trim() == "" || brand.ToUpper().Trim() != dr["Brand"].ToString().ToUpper().Trim() || catid == 0 || catid != Convert.ToInt32(dr["CategoryID"]))
                            {
                                DataRow dr_final01 = dt.NewRow();
                                dr_final01["ItemCode"] = "";
                                dr_final01["ProductName"] = "";
                                dr_final01["CategoryID"] = "";
                                dr_final01["Brand"] = dr["Brand"];
                                dr_final01["Model"] = dr["CategoryID"];
                                dr_final01["Quantity"] = "";
                                dr_final01["Rate"] = "";
                                dr_final01["Vaule"] = "";
                                dt.Rows.Add(dr_final01);
                            }
                            if (brand.ToUpper().Trim() == "" || brand.ToUpper().Trim() != dr["Brand"].ToString().ToUpper().Trim() || catid == 0 || catid != Convert.ToInt32(dr["CategoryID"]))
                            {
                                DataRow dr_final02 = dt.NewRow();
                                dr_final02["ItemCode"] = "";
                                dr_final02["ProductName"] = "";
                                dr_final02["CategoryID"] = "";
                                dr_final02["Brand"] = "";
                                dr_final02["Model"] = "";
                                dr_final02["Quantity"] = "";
                                dr_final02["Rate"] = "";
                                dr_final02["Vaule"] = "";
                                dt.Rows.Add(dr_final02);
                                brand = Convert.ToString(dr["Brand"]);
                                catid = Convert.ToInt32(dr["CategoryID"]);
                            }
                            DataRow dr_final03 = dt.NewRow();
                            dr_final03["ItemCode"] = "";
                            dr_final03["ProductName"] = "";
                            dr_final03["CategoryID"] = "";
                            dr_final03["Brand"] = "";
                            dr_final03["Model"] = "";
                            dr_final03["Quantity"] = dr["Quantity"];
                            dr_final03["Rate"] = dr["Rate"];
                            dr_final03["Vaule"] = dr["Vaule"];
                            dt.Rows.Add(dr_final03);
                            Gtotal = Gtotal + Convert.ToDecimal(dr["Vaule"]);
                            Gttl = Gttl + Convert.ToDecimal(dr["Vaule"]);
                        }
                        //Display the last Total and Grand Total
                        if (dispLstTotal)
                        {
                            DataRow dr_final04 = dt.NewRow();
                            dr_final04["ItemCode"] = "";
                            dr_final04["ProductName"] = "";
                            dr_final04["CategoryID"] = "";
                            dr_final04["Brand"] = "";
                            dr_final04["Model"] = "Total:" + brand;
                            dr_final04["Quantity"] = "";
                            dr_final04["Rate"] = "";
                            dr_final04["Vaule"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                            dt.Rows.Add(dr_final04);
                            Gtotal = 0;

                            DataRow dr_final06 = dt.NewRow();
                            dr_final06["ItemCode"] = "";
                            dr_final06["ProductName"] = "";
                            dr_final06["CategoryID"] = "";
                            dr_final06["Brand"] = "";
                            dr_final06["Model"] = "Grand Total:";
                            dr_final06["Quantity"] = "";
                            dr_final06["Rate"] = "";
                            dr_final06["Vaule"] = Convert.ToString(Convert.ToDecimal(Gttl));
                            dt.Rows.Add(dr_final06);
                            Gtotal = 0;
                            ExportToExcel(dt);
                        }

                    }
                    //else if (ddlfirstlvl.SelectedItem.Text = "CategoryID")
                    //{
 
                    //}
        */
    }


    decimal Total;
    protected void GridStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var QtyAmt = e.Row.FindControl("lblQty") as Label;
                if (QtyAmt != null)
                {
                    Total += decimal.Parse(QtyAmt.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var RateAmt = e.Row.FindControl("lblRate") as Label;
                if (RateAmt != null)
                {
                    Total += decimal.Parse(RateAmt.Text);
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ValueAmt = e.Row.FindControl("lblVaule") as Label;
                if (ValueAmt != null)
                {
                    Total += decimal.Parse(ValueAmt.Text);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void btnLevls_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic objBL = new BusinessLogic(sDataSource);
            bool dispLstTotal = false;
            objBL.Brands = ddlfirstlvl.SelectedItem.Text;
            DataSet ds = new DataSet();
            ds = objBL.getStockbrand();
            DataTable dt = new DataTable();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("ItemCode"));
                dt.Columns.Add(new DataColumn("ProductName"));
                dt.Columns.Add(new DataColumn("CategoryID"));
                dt.Columns.Add(new DataColumn("Brand"));
                dt.Columns.Add(new DataColumn("Model"));
                dt.Columns.Add(new DataColumn("Quantity"));
                dt.Columns.Add(new DataColumn("Rate"));
                dt.Columns.Add(new DataColumn("Vaule"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dispLstTotal = true;
                    string tmpbrand = dr["Brand"].ToString();
                    tmpbrand = tmpbrand.ToString().ToUpper();
                    if (brand.ToUpper().Trim() == "" || brand.ToUpper().Trim() != dr["Brand"].ToString().ToUpper().Trim() || catid == 0 || catid != Convert.ToInt32(dr["CategoryID"]))
                    {
                        if (brand != "" || catid == 0)
                        {
                            DataRow dr_final05 = dt.NewRow();
                            dr_final05["ItemCode"] = "";
                            dr_final05["ProductName"] = "";
                            dr_final05["CategoryID"] = "";
                            dr_final05["Brand"] = "";
                            dr_final05["Model"] = "Total:" + brand;
                            dr_final05["Quantity"] = "";
                            dr_final05["Rate"] = "";
                            dr_final05["Vaule"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                            dt.Rows.Add(dr_final05);
                            Gtotal = 0;
                        }
                    }
                    if (brand.ToUpper().Trim() == "" || brand.ToUpper().Trim() != dr["Brand"].ToString().ToUpper().Trim() || catid == 0 || catid != Convert.ToInt32(dr["CategoryID"]))
                    {
                        DataRow dr_final01 = dt.NewRow();
                        dr_final01["ItemCode"] = "";
                        dr_final01["ProductName"] = "";
                        dr_final01["CategoryID"] = "";
                        dr_final01["Brand"] = dr["Brand"];
                        dr_final01["Model"] = dr["CategoryID"];
                        dr_final01["Quantity"] = "";
                        dr_final01["Rate"] = "";
                        dr_final01["Vaule"] = "";
                        dt.Rows.Add(dr_final01);
                    }
                    if (brand.ToUpper().Trim() == "" || brand.ToUpper().Trim() != dr["Brand"].ToString().ToUpper().Trim() || catid == 0 || catid != Convert.ToInt32(dr["CategoryID"]))
                    {
                        DataRow dr_final02 = dt.NewRow();
                        dr_final02["ItemCode"] = "";
                        dr_final02["ProductName"] = "";
                        dr_final02["CategoryID"] = "";
                        dr_final02["Brand"] = "";
                        dr_final02["Model"] = "";
                        dr_final02["Quantity"] = "";
                        dr_final02["Rate"] = "";
                        dr_final02["Vaule"] = "";
                        dt.Rows.Add(dr_final02);
                        brand = Convert.ToString(dr["Brand"]);
                        catid = Convert.ToInt32(dr["CategoryID"]);
                    }
                    DataRow dr_final03 = dt.NewRow();
                    dr_final03["ItemCode"] = "";
                    dr_final03["ProductName"] = "";
                    dr_final03["CategoryID"] = "";
                    dr_final03["Brand"] = "";
                    dr_final03["Model"] = "";
                    dr_final03["Quantity"] = dr["Quantity"];
                    dr_final03["Rate"] = dr["Rate"];
                    dr_final03["Vaule"] = dr["Vaule"];
                    dt.Rows.Add(dr_final03);
                    Gtotal = Gtotal + Convert.ToDecimal(dr["Vaule"]);
                    Gttl = Gttl + Convert.ToDecimal(dr["Vaule"]);
                }
                //Display the last Total and Grand Total
                if (dispLstTotal)
                {
                    DataRow dr_final04 = dt.NewRow();
                    dr_final04["ItemCode"] = "";
                    dr_final04["ProductName"] = "";
                    dr_final04["CategoryID"] = "";
                    dr_final04["Brand"] = "";
                    dr_final04["Model"] = "Total:" + brand;
                    dr_final04["Quantity"] = "";
                    dr_final04["Rate"] = "";
                    dr_final04["Vaule"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                    dt.Rows.Add(dr_final04);
                    Gtotal = 0;

                    DataRow dr_final06 = dt.NewRow();
                    dr_final06["ItemCode"] = "";
                    dr_final06["ProductName"] = "";
                    dr_final06["CategoryID"] = "";
                    dr_final06["Brand"] = "";
                    dr_final06["Model"] = "Grand Total:";
                    dr_final06["Quantity"] = "";
                    dr_final06["Rate"] = "";
                    dr_final06["Vaule"] = Convert.ToString(Convert.ToDecimal(Gttl));
                    dt.Rows.Add(dr_final06);
                    Gtotal = 0;
                    ExportToExcel(dt);
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}


