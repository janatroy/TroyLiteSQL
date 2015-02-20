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

public partial class PurchaseRpt : System.Web.UI.Page
{
    decimal Gtotal = 0;
    decimal Gttl = 0;
    decimal Pttls = 0;
    decimal brandTotal = 0, catIDTotal = 0, modelTotal = 0, brandTotal1 = 0, brandTotal2 = 0, brandTotal3 = 0;
    string orderBy = "", selColumn = "", selLevels = "";
    //DBClass objdb = new DBClass();
    BusinessLogic objBL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            if (!Page.IsPostBack)
            {
                fillfBrand();
                fillCategorys();
                fillProductName();
                fillProductCode();
                fillctCustid();
                fillPayMode();
                DateStartformat();
                DateEndformat();
                fillDdl(ddlFirstLvl);
                fillDdl(ddlSecondLvl);
                fillDdl(ddlThirdLvl);
                fillDdl(ddlFourthLvl);
                fillDdl(ddlFifthLvl);
                fillDdl(ddlSixthLvl);
                fillDdl(ddlSeventhLvl);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void fillDdl(DropDownList ddlist)
    {
        //DropDownList ddlList = new DropDownList();
        ddlist.Items.Insert(0, "None");
        ddlist.Items.Insert(1, "SupplierID");
        ddlist.Items.Insert(2, "BillDate");
        ddlist.Items.Insert(3, "PayMode");
        ddlist.Items.Insert(4, "ProductDesc");
        ddlist.Items.Insert(5, "CategoryID");
        ddlist.Items.Insert(6, "ProductName");
        ddlist.Items.Insert(7, "ItemCode");
    }
    private void fillfBrand()
    {
        DataSet ds = new DataSet();
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
        ds = objBL.getDistinctCategorys();
        ddlCatgryid.DataSource = ds;
        ddlCatgryid.DataTextField = "CategoryID";
        ddlCatgryid.DataValueField = "CategoryID";
        ddlCatgryid.DataBind();
        ddlCatgryid.Items.Insert(0, "All");
    }
    private void fillProductName()
    {
        DataSet ds = new DataSet();
        ds = objBL.getDistinctPrdctNme();
        ddlPrdctNme.DataSource = ds;
        ddlPrdctNme.DataTextField = "ProductName";
        ddlPrdctNme.DataValueField = "ProductName";
        ddlPrdctNme.DataBind();
        ddlPrdctNme.Items.Insert(0, "All");
    }
    private void fillProductCode()
    {
        DataSet ds = new DataSet();
        ds = objBL.getDistinctPrdctCode();
        ddlPrdctCode.DataSource = ds;
        ddlPrdctCode.DataTextField = "ItemCode";
        ddlPrdctCode.DataValueField = "ItemCode";
        ddlPrdctCode.DataBind();
        ddlPrdctCode.Items.Insert(0, "All");
    }
    private void fillctCustid()
    {
        DataSet ds = new DataSet();
        ds = objBL.getDistinctSupplierID();
        ddlCustid.DataSource = ds;
        ddlCustid.DataTextField = "SupplierID";
        ddlCustid.DataValueField = "SupplierID";
        ddlCustid.DataBind();
        ddlCustid.Items.Insert(0, "All");
    }
    private void fillPayMode()
    {
        DataSet ds = new DataSet();
        ds = objBL.getDistinctCustid();
        ddlPayMode.DataSource = ds;
        ddlPayMode.DataTextField = "PayMode";
        ddlPayMode.DataValueField = "PayMode";
        ddlPayMode.DataBind();
        ddlPayMode.Items.Insert(0, "All");
    }
    protected void DateStartformat()
    {
        DateTime dtCurrent = DateTime.Now;
        DateTime dtNew = new DateTime(dtCurrent.Year, dtCurrent.Month, 1);
        txtStrtDt.Text = string.Format("{0:dd/MM/yyyy}", dtNew);
    }
    protected void DateEndformat()
    {
        System.DateTime dt = System.DateTime.Now.Date;
        txtEndDt.Text = string.Format("{0:dd/MM/yyyy}", dt);
    }
    protected void btnData_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtStrtDt.Text != "" && txtEndDt.Text != "")
            {
                //if (!isValidLevels())
                //{
                //    return;
                //}
                //Total = 0;
                string condtion = "";
                condtion = getCond();
                getorderByAndselColumn();
                DataSet ds = new DataSet();
                ds = objBL.getPurchase(selColumn, condtion, orderBy);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridPur.DataSource = ds;
                    GridPur.DataBind();
                    //var lblTotalAmt = Gridpayments.FooterRow.FindControl("lblTotalAmt") as Label;
                    //if (lblTotalAmt != null)
                    //{
                    //    lblTotalAmt.Text = Total.ToString();
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Not Data Found');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(TextBox), "MyScript", "alert('Please Enter Dates');", true);
                txtStrtDt.Focus();
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
        //Where BillDate between CDate('" + StartDate + "') and CDate('" + EndDate + "') and Internaltransfer='" + Internals + "' and purchaseReturn='" + Models + "' and SupplierID=" + Catids + " and CategoryID=" + Categorys + " and PayMode=" + Stocks + " and ProductName='" + PrdctNme + "' and ProductDesc='" + Brands + "' and tblProductMaster.ItemCode='" + Productcd + "'");
        if (txtStrtDt.Text != "" && txtEndDt.Text != "")
        {
            objBL.StartDate = txtStrtDt.Text;
            objBL.EndDate = txtEndDt.Text;
            cond = " and (BillDate) Between CDate('" + txtStrtDt.Text + "') and  CDate('" + txtEndDt.Text + "')";
        }
        if ((rblPurchseRtn.SelectedItem.Text == "Yes") & (rblPurchseRtn.SelectedItem.Text == "No"))
        {
            objBL.Models = rblPurchseRtn.SelectedItem.Text;
            cond += " and salesReturn=" + rblPurchseRtn.SelectedItem.Text.ToUpper() + " ";
        }
        if ((rblIntrnlTrns.SelectedItem.Text == "Yes") & (rblIntrnlTrns.SelectedItem.Text == "No"))
        {
            objBL.Internals = rblIntrnlTrns.SelectedItem.Text;
            cond += " and Internaltransfer=" + rblIntrnlTrns.SelectedItem.Text + " ";
        }
        if (ddlCustid.SelectedIndex > 0)
        {
            objBL.Catids = Convert.ToInt32(ddlCustid.SelectedItem.Value);
            cond += " and SupplierID=" + Convert.ToInt32(ddlCustid.SelectedItem.Value) + " ";
        }
        if (ddlCatgryid.SelectedIndex > 0)
        {
            objBL.Categorys = Convert.ToInt32(ddlCatgryid.SelectedItem.Value); ;
            cond += " and CategoryID=" + Convert.ToInt32(ddlCatgryid.SelectedItem.Value) + " ";
        }
        if (ddlPayMode.SelectedIndex > 0)
        {
            objBL.Stocks = Convert.ToInt32(ddlPayMode.SelectedItem.Value); ;
            cond += " and PayMode=" + Convert.ToInt32(ddlPayMode.SelectedItem.Value) + " ";
        }
        if (ddlPrdctCode.SelectedIndex > 0)
        {
            objBL.Productcd = ddlPrdctCode.SelectedItem.Text;
            cond += " and tblProductMaster.ItemCode='" + ddlPrdctCode.SelectedItem.Text + "' ";
        }
        if (ddlPrdctNme.SelectedIndex > 0)
        {
            objBL.PrdctNmes = ddlPrdctNme.SelectedItem.Text;
            cond += " and ProductName='" + ddlPrdctNme.SelectedItem.Text + "' ";
        }
        if (ddlBrand.SelectedIndex > 0)
        {
            objBL.Brands = ddlBrand.SelectedItem.Text;
            cond += " and ProductDesc='" + ddlBrand.SelectedItem.Text + "' ";
        }
        return cond;
    }
    protected void getorderByAndselColumn()
    {
        orderBy = "";
        selColumn = "";

        if (ddlFirstLvl.SelectedIndex > 0)
        {
            orderBy = " Order by " + ddlFirstLvl.SelectedItem.Text + " ";
            selColumn = " " + ddlFirstLvl.SelectedItem.Text + " ";
        }
        if (ddlSecondLvl.SelectedIndex > 0)
        {
            if (orderBy == "")
            {
                orderBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                orderBy += " , ";
                selColumn += " , ";
            }
            orderBy += " " + ddlSecondLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlSecondLvl.SelectedItem.Text + " ";
        }
        if (ddlThirdLvl.SelectedIndex > 0)
        {
            if (orderBy == "")
            {
                orderBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                orderBy += " , ";
                selColumn += " , ";
            }
            orderBy += " " + ddlThirdLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlThirdLvl.SelectedItem.Text + " ";
        }
        if (ddlFourthLvl.SelectedIndex > 0)
        {
            if (orderBy == "")
            {
                orderBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                orderBy += " , ";
                selColumn += " , ";
            }
            orderBy += " " + ddlFourthLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlFourthLvl.SelectedItem.Text + " ";
        }
        if (ddlFifthLvl.SelectedIndex > 0)
        {
            if (orderBy == "")
            {
                orderBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                orderBy += " , ";
                selColumn += " , ";
            }
            orderBy += " " + ddlFifthLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlFifthLvl.SelectedItem.Text + " ";
        }
        if (ddlSixthLvl.SelectedIndex > 0)
        {
            if (orderBy == "")
            {
                orderBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                orderBy += " , ";
                selColumn += " , ";
            }
            orderBy += " " + ddlSixthLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlSixthLvl.SelectedItem.Text + " ";
        }
        if (ddlSeventhLvl.SelectedIndex > 0)
        {
            if (orderBy == "")
            {
                orderBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                orderBy += " , ";
                selColumn += " , ";
            }
            orderBy += " " + ddlSeventhLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlSeventhLvl.SelectedItem.Text + " ";
        }
        if (orderBy == "" && selColumn == "")
        {
            orderBy = " order by SupplierID,BillDate,PayMode,ProductDesc,CategoryID,ProductName,ItemCode";
            selColumn = " tblPurchase.BillNo,BillDate,PayMode,Internaltransfer,salesReturn,ItemCode,ProductName,ProductDesc,CategoryID,Model,SupplierID,Add1,Phone,tblPurchaseItems.Discount,tblPurchaseItems.Vat,tblPurchaseItems.Qty,Freight";
        }
        else
        {
            selLevels = selColumn;
            if (orderBy.IndexOf("SupplierID") < 0)
            {
                orderBy += " , SupplierID ";
                selColumn += " , SupplierID ";
            }
            if (orderBy.IndexOf("BillDate") < 0)
            {
                orderBy += " , BillDate ";
                selColumn += " , BillDate ";
            }
            if (orderBy.IndexOf("PayMode") < 0)
            {
                orderBy += " , PayMode ";
                selColumn += " , PayMode ";
            }
            if (orderBy.IndexOf("ProductDesc") < 0)
            {
                orderBy += " , ProductDesc ";
                selColumn += " , ProductDesc ";
            }
            if (orderBy.IndexOf("CategoryID") < 0)
            {
                orderBy += " , CategoryID ";
                selColumn += " , CategoryID ";
            }
            if (orderBy.IndexOf("ProductName") < 0)
            {
                orderBy += " , ProductName ";
                selColumn += " , ProductName ";
            }
            if (orderBy.IndexOf("ItemCode") < 0)
            {
                orderBy += " , ItemCode ";
                selColumn += " , ItemCode ";
            }
            //orderBy += " , TransDate, DebtorID, PaymentMode";
            selColumn = " tblPurchase.BillNo,BillDate,PayMode,Internaltransfer,salesReturn,ItemCode,ProductName,ProductDesc,CategoryID,Model,SupplierID,Add1,Phone,tblPurchaseItems.Discount,tblPurchaseItems.Vat,tblPurchaseItems.Qty,Freight";
        }
        selColumn = selColumn.Replace("ItemCode", "tblProductMaster.ItemCode As ItemCode");
        orderBy = orderBy.Replace("ItemCode", "tblProductMaster.ItemCode");
    }
    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            string cond = "";
            cond = getCond();
            getorderByAndselColumn();
            bindData(selColumn, cond, orderBy);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void bindData(string selColumn, string cond, string orderBy)
    {
        bool dispLastTotal = false;
        string condtion = "";
        condtion = getCond();
        getorderByAndselColumn();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = objBL.getPurchase(selColumn, condtion, orderBy);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ddlFirstLvl.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlFirstLvl.SelectedItem.Text));
            }
            if (ddlSecondLvl.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlSecondLvl.SelectedItem.Text));
            }
            if (ddlThirdLvl.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlThirdLvl.SelectedItem.Text));
            }
            if (ddlFourthLvl.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlFourthLvl.SelectedItem.Text));
            }
            if (ddlFifthLvl.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlFifthLvl.SelectedItem.Text));
            }
            if (ddlSixthLvl.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlSixthLvl.SelectedItem.Text));
            }
            if (ddlSeventhLvl.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlSeventhLvl.SelectedItem.Text));
            }

            if (selLevels.IndexOf("SupplierID") < 0)
                dt.Columns.Add(new DataColumn("SupplierID"));
            if (selLevels.IndexOf("BillDate") < 0)
                dt.Columns.Add(new DataColumn("BillDate"));
            if (selLevels.IndexOf("PayMode") < 0)
                dt.Columns.Add(new DataColumn("PayMode"));
            if (selLevels.IndexOf("ProductDesc") < 0)
                dt.Columns.Add(new DataColumn("ProductDesc"));
            if (selLevels.IndexOf("CategoryID") < 0)
                dt.Columns.Add(new DataColumn("CategoryID"));
            if (selLevels.IndexOf("ProductName") < 0)
                dt.Columns.Add(new DataColumn("ProductName"));
            if (selLevels.IndexOf("ItemCode") < 0)
                dt.Columns.Add(new DataColumn("ItemCode"));

            dt.Columns.Add(new DataColumn("Model"));
            dt.Columns.Add(new DataColumn("BillNo"));
            dt.Columns.Add(new DataColumn("Internaltransfer"));
            dt.Columns.Add(new DataColumn("salesReturn"));
            dt.Columns.Add(new DataColumn("Add1"));
            dt.Columns.Add(new DataColumn("Phone"));
            dt.Columns.Add(new DataColumn("Discount"));
            dt.Columns.Add(new DataColumn("Freight"));
            dt.Columns.Add(new DataColumn("Qty"));
            dt.Columns.Add(new DataColumn("Rate"));
            dt.Columns.Add(new DataColumn("Amount"));

            //initialize column values for entire row
            string fLvlValue = "", sLvlValue = "", tLvlValue = "", frthLvlValue = "", fifLvlValue = "", sixLvlValue = "", svthLvlValue = "";
            string fLvlValueTemp = "", sLvlValueTemp = "", tLvlValueTemp = "", frthLvlValueTemp = "", fifLvlValueTemp = "", sixLvlValueTemp = "", svthLvlValueTemp = "";
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
                if (ddlFifthLvl.SelectedIndex > 0)
                    fifLvlValueTemp = dr[ddlFifthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                if (ddlSixthLvl.SelectedIndex > 0)
                    sixLvlValueTemp = dr[ddlSixthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                if (ddlSeventhLvl.SelectedIndex > 0)
                    svthLvlValueTemp = dr[ddlSeventhLvl.SelectedItem.Text].ToString().ToUpper().Trim();

                dispLastTotal = true;

                if (ddlSeventhLvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                        (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                        (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp) ||
                        (sixLvlValue != "" && sixLvlValue != sixLvlValueTemp) ||
                        (svthLvlValue != "" && svthLvlValue != svthLvlValueTemp))
                    {
                        DataRow dr_final7 = dt.NewRow();
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
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "Total:" + svthLvlValue;
                        }
                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final7["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final7["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final7["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final7["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final7["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final7["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final7["ItemCode"] = "";

                        dr_final7["Model"] = "";
                        dr_final7["BillNo"] = "";
                        dr_final7["Internaltransfer"] = "";
                        dr_final7["salesReturn"] = "";
                        dr_final7["Add1"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Amount"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(Pttls));
                        dt.Rows.Add(dr_final7);
                        Pttls = 0;
                    }
                }

                if (ddlSixthLvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                        (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                        (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp) ||
                        (sixLvlValue != "" && sixLvlValue != sixLvlValueTemp))
                    {
                        DataRow dr_final7 = dt.NewRow();
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
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSixthLvl.SelectedItem.Text] = "Total:" + sixLvlValue;
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final7["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final7["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final7["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final7["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final7["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final7["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final7["ItemCode"] = "";

                        dr_final7["Model"] = "";
                        dr_final7["BillNo"] = "";
                        dr_final7["Internaltransfer"] = "";
                        dr_final7["salesReturn"] = "";
                        dr_final7["Add1"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Amount"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                        dt.Rows.Add(dr_final7);
                        modelTotal = 0;
                    }
                }

                if (ddlFifthLvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                       (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                       (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                       (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                       (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp))
                    {
                        DataRow dr_final7 = dt.NewRow();
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
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "Total:" + fifLvlValue;
                        }
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final7["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final7["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final7["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final7["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final7["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final7["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final7["ItemCode"] = "";

                        dr_final7["Model"] = "";
                        dr_final7["BillNo"] = "";
                        dr_final7["Internaltransfer"] = "";
                        dr_final7["salesReturn"] = "";
                        dr_final7["Add1"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Amount"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                        dt.Rows.Add(dr_final7);
                        catIDTotal = 0;
                    }
                }

                if (ddlFourthLvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                       (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                       (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                       (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp))
                    {
                        DataRow dr_final7 = dt.NewRow();
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
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final7["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final7["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final7["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final7["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final7["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final7["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final7["ItemCode"] = "";

                        dr_final7["Model"] = "";
                        dr_final7["BillNo"] = "";
                        dr_final7["Internaltransfer"] = "";
                        dr_final7["salesReturn"] = "";
                        dr_final7["Add1"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Amount"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                        dt.Rows.Add(dr_final7);
                        brandTotal = 0;
                    }
                }

                if (ddlThirdLvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                       (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                       (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final7 = dt.NewRow();
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
                            dr_final7[ddlThirdLvl.SelectedItem.Text] = "Total:" + tLvlValue;
                        }
                        if (ddlFourthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final7["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final7["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final7["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final7["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final7["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final7["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final7["ItemCode"] = "";

                        dr_final7["Model"] = "";
                        dr_final7["BillNo"] = "";
                        dr_final7["Internaltransfer"] = "";
                        dr_final7["salesReturn"] = "";
                        dr_final7["Add1"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Amount"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));
                        dt.Rows.Add(dr_final7);
                        brandTotal1 = 0;
                    }
                }

                if (ddlSecondLvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                       (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "Total:" + sLvlValue;
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
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final7["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final7["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final7["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final7["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final7["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final7["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final7["ItemCode"] = "";

                        dr_final7["Model"] = "";
                        dr_final7["BillNo"] = "";
                        dr_final7["Internaltransfer"] = "";
                        dr_final7["salesReturn"] = "";
                        dr_final7["Add1"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Amount"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));
                        dt.Rows.Add(dr_final7);
                        brandTotal2 = 0;
                    }
                }

                if (ddlFirstLvl.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp))
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "Total:" + fLvlValue;
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
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                        }
                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final7["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final7["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final7["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final7["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final7["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final7["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final7["ItemCode"] = "";

                        dr_final7["Model"] = "";
                        dr_final7["BillNo"] = "";
                        dr_final7["Internaltransfer"] = "";
                        dr_final7["salesReturn"] = "";
                        dr_final7["Add1"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Amount"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));
                        dt.Rows.Add(dr_final7);
                        brandTotal3 = 0;
                    }
                }

                ///////////////////////////////////////
                if (ddlFirstLvl.SelectedIndex > 0)
                {
                    if (fLvlValueTemp != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final1 = dt.NewRow();
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
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSeventhLvl.SelectedItem.Text] = "";
                        }

                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final1["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final1["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final1["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final1["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final1["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final1["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final1["ItemCode"] = "";

                        dr_final1["Model"] = "";
                        dr_final1["BillNo"] = "";
                        dr_final1["Internaltransfer"] = "";
                        dr_final1["salesReturn"] = "";
                        dr_final1["Add1"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["Discount"] = "";
                        dr_final1["Freight"] = "";
                        dr_final1["Qty"] = "";
                        dr_final1["Rate"] = "";
                        dt.Rows.Add(dr_final1);
                    }
                }
                if (ddlSecondLvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final2 = dt.NewRow();
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
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final2[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final2[ddlSeventhLvl.SelectedItem.Text] = "";
                        }

                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final2["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final2["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final2["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final2["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final2["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final2["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final2["ItemCode"] = "";

                        dr_final2["Model"] = "";
                        dr_final2["BillNo"] = "";
                        dr_final2["Internaltransfer"] = "";
                        dr_final2["salesReturn"] = "";
                        dr_final2["Add1"] = "";
                        dr_final2["Phone"] = "";
                        dr_final2["Discount"] = "";
                        dr_final2["Freight"] = "";
                        dr_final2["Qty"] = "";
                        dr_final2["Rate"] = "";
                        dr_final2["Amount"] = "";
                        dt.Rows.Add(dr_final2);
                    }
                }
                if (ddlThirdLvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final1 = dt.NewRow();
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
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSeventhLvl.SelectedItem.Text] = "";
                        }

                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final1["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final1["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final1["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final1["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final1["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final1["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final1["ItemCode"] = "";

                        dr_final1["Model"] = "";
                        dr_final1["BillNo"] = "";
                        dr_final1["Internaltransfer"] = "";
                        dr_final1["salesReturn"] = "";
                        dr_final1["Add1"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["Discount"] = "";
                        dr_final1["Freight"] = "";
                        dr_final1["Qty"] = "";
                        dr_final1["Rate"] = "";
                        dr_final1["Amount"] = "";
                        dt.Rows.Add(dr_final1);
                    }
                }

                if (ddlFourthLvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp) ||
                    (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp))
                    {
                        DataRow dr_final1 = dt.NewRow();
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
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSeventhLvl.SelectedItem.Text] = "";
                        }

                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final1["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final1["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final1["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final1["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final1["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final1["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final1["ItemCode"] = "";

                        dr_final1["Model"] = "";
                        dr_final1["BillNo"] = "";
                        dr_final1["Internaltransfer"] = "";
                        dr_final1["salesReturn"] = "";
                        dr_final1["Add1"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["Discount"] = "";
                        dr_final1["Freight"] = "";
                        dr_final1["Qty"] = "";
                        dr_final1["Rate"] = "";
                        dr_final1["Amount"] = "";
                        dt.Rows.Add(dr_final1);

                    }
                }

                if (ddlFifthLvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                       (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                       (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp) ||
                   (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp) ||
                   (fifLvlValueTemp != "" && fifLvlValue != fifLvlValueTemp))
                    {
                        DataRow dr_final1 = dt.NewRow();
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
                            dr_final1[ddlFifthLvl.SelectedItem.Text] = fifLvlValueTemp;
                        }
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSeventhLvl.SelectedItem.Text] = "";
                        }

                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final1["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final1["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final1["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final1["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final1["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final1["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final1["ItemCode"] = "";

                        dr_final1["Model"] = "";
                        dr_final1["BillNo"] = "";
                        dr_final1["Internaltransfer"] = "";
                        dr_final1["salesReturn"] = "";
                        dr_final1["Add1"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["Discount"] = "";
                        dr_final1["Freight"] = "";
                        dr_final1["Qty"] = "";
                        dr_final1["Rate"] = "";
                        dr_final1["Amount"] = "";
                        dt.Rows.Add(dr_final1);

                    }
                }
                if (ddlSixthLvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                       (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                       (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp) ||
                       (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp) ||
                       (fifLvlValueTemp != "" && fifLvlValue != fifLvlValueTemp) ||
                       (sixLvlValueTemp != "" && sixLvlValue != sixLvlValueTemp))
                    {
                        DataRow dr_final1 = dt.NewRow();
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
                            dr_final1[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSixthLvl.SelectedItem.Text] = sixLvlValueTemp;
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSeventhLvl.SelectedItem.Text] = "";
                        }

                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final1["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final1["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final1["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final1["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final1["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final1["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final1["ItemCode"] = "";

                        dr_final1["Model"] = "";
                        dr_final1["BillNo"] = "";
                        dr_final1["Internaltransfer"] = "";
                        dr_final1["salesReturn"] = "";
                        dr_final1["Add1"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["Discount"] = "";
                        dr_final1["Freight"] = "";
                        dr_final1["Qty"] = "";
                        dr_final1["Rate"] = "";
                        dr_final1["Amount"] = "";
                        dt.Rows.Add(dr_final1);

                    }
                }

                if (ddlSeventhLvl.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                      (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                      (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp) ||
                      (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp) ||
                      (fifLvlValueTemp != "" && fifLvlValue != fifLvlValueTemp) ||
                      (sixLvlValueTemp != "" && sixLvlValue != sixLvlValueTemp) ||
                      (svthLvlValueTemp != "" && svthLvlValue != svthLvlValueTemp))
                    {
                        DataRow dr_final1 = dt.NewRow();
                        if (ddlFifthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlFifthLvl.SelectedItem.Text] = "";
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
                        if (ddlSixthLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSeventhLvl.SelectedItem.Text] = svthLvlValueTemp;
                        }

                        if (selLevels.IndexOf("SupplierID") < 0)
                            dr_final1["SupplierID"] = "";
                        if (selLevels.IndexOf("BillDate") < 0)
                            dr_final1["BillDate"] = "";
                        if (selLevels.IndexOf("PayMode") < 0)
                            dr_final1["PayMode"] = "";
                        if (selLevels.IndexOf("ProductDesc") < 0)
                            dr_final1["ProductDesc"] = "";
                        if (selLevels.IndexOf("CategoryID") < 0)
                            dr_final1["CategoryID"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final1["ProductName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final1["ItemCode"] = "";

                        dr_final1["Model"] = "";
                        dr_final1["BillNo"] = "";
                        dr_final1["Internaltransfer"] = "";
                        dr_final1["salesReturn"] = "";
                        dr_final1["Add1"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["Discount"] = "";
                        dr_final1["Freight"] = "";
                        dr_final1["Qty"] = "";
                        dr_final1["Rate"] = "";
                        dr_final1["Amount"] = "";
                        dt.Rows.Add(dr_final1);
                    }
                }

                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;
                frthLvlValue = frthLvlValueTemp;
                fifLvlValue = fifLvlValueTemp;
                sixLvlValue = sixLvlValueTemp;
                svthLvlValue = sixLvlValueTemp;
                DataRow dr_final5 = dt.NewRow();
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
                if (ddlSixthLvl.SelectedIndex > 0)
                {
                    dr_final5[ddlSixthLvl.SelectedItem.Text] = "";
                }
                if (ddlSeventhLvl.SelectedIndex > 0)
                {
                    dr_final5[ddlSeventhLvl.SelectedItem.Text] = "";
                }
                if (selLevels.IndexOf("SupplierID") < 0)
                    dr_final5["SupplierID"] = dr["SupplierID"];
                if (selLevels.IndexOf("BillDate") < 0)
                    dr_final5["BillDate"] = dr["BillDate"];
                if (selLevels.IndexOf("PayMode") < 0)
                    dr_final5["PayMode"] = dr["PayMode"];
                if (selLevels.IndexOf("ProductDesc") < 0)
                    dr_final5["ProductDesc"] = dr["ProductDesc"];
                if (selLevels.IndexOf("CategoryID") < 0)
                    dr_final5["CategoryID"] = dr["CategoryID"];
                if (selLevels.IndexOf("ProductName") < 0)
                    dr_final5["ProductName"] = dr["ProductName"];
                if (selLevels.IndexOf("ItemCode") < 0)
                    dr_final5["ItemCode"] = dr["ItemCode"];

                dr_final5["Model"] = dr["Model"];
                dr_final5["BillNo"] = dr["BillNo"];
                dr_final5["Internaltransfer"] = dr["Internaltransfer"];
                dr_final5["salesReturn"] = dr["salesReturn"];
                dr_final5["Add1"] = dr["Add1"];
                dr_final5["Phone"] = dr["Phone"];
                dr_final5["Discount"] = dr["Discount"];
                dr_final5["Freight"] = dr["Freight"];
                dr_final5["Qty"] = dr["Qty"];
                dr_final5["Rate"] = dr["Rate"];
                //dr_final5["Amount"] = dr["Amount"];
                dt.Rows.Add(dr_final5);
                Gtotal = Gtotal + Convert.ToDecimal(dr["Rate"]);
                Gttl = Gttl + Convert.ToInt32(dr["Qty"]);
                modelTotal = modelTotal + Convert.ToDecimal(dr["Rate"]);
                catIDTotal = catIDTotal + Convert.ToDecimal(dr["Rate"]);
                Pttls = Pttls + Convert.ToDecimal(dr["Rate"]);
                brandTotal = brandTotal + Convert.ToDecimal(dr["Rate"]);
                brandTotal1 = brandTotal1 + Convert.ToDecimal(dr["Rate"]);
                brandTotal2 = brandTotal2 + Convert.ToDecimal(dr["Rate"]);
                brandTotal3 = brandTotal3 + Convert.ToDecimal(dr["Rate"]);
                //brandTotal4 = brandTotal4 + Convert.ToDecimal(dr["Amount"]);
            }


            //Display the last Total and Grand Total
            if (dispLastTotal)
            {
                if (ddlSeventhLvl.SelectedIndex > 0)
                {
                    DataRow dr_final7 = dt.NewRow();
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
                        dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                    }
                    if (ddlSixthLvl.SelectedIndex > 0)
                    {
                        dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                    }
                    dr_final7[ddlSeventhLvl.SelectedItem.Text] = "Total:" + svthLvlValueTemp;

                    if (selLevels.IndexOf("SupplierID") < 0)
                        dr_final7["SupplierID"] = "";
                    if (selLevels.IndexOf("BillDate") < 0)
                        dr_final7["BillDate"] = "";
                    if (selLevels.IndexOf("PayMode") < 0)
                        dr_final7["PayMode"] = "";
                    if (selLevels.IndexOf("ProductDesc") < 0)
                        dr_final7["ProductDesc"] = "";
                    if (selLevels.IndexOf("CategoryID") < 0)
                        dr_final7["CategoryID"] = "";
                    if (selLevels.IndexOf("ProductName") < 0)
                        dr_final7["ProductName"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final7["ItemCode"] = "";

                    dr_final7["Model"] = ""; ;
                    dr_final7["BillNo"] = ""; ;
                    dr_final7["Internaltransfer"] = ""; ;
                    dr_final7["salesReturn"] = "";
                    dr_final7["Add1"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["Discount"] = "";
                    dr_final7["Freight"] = "";
                    dr_final7["Qty"] = "";
                    dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(Pttls));
                    dt.Rows.Add(dr_final7);
                    Pttls = 0;
                }

                if (ddlSixthLvl.SelectedIndex > 0)
                {
                    DataRow dr_final7 = dt.NewRow();
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
                        dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                    }
                    dr_final7[ddlSixthLvl.SelectedItem.Text] = "Total:" + sixLvlValueTemp;

                    if (selLevels.IndexOf("SupplierID") < 0)
                        dr_final7["SupplierID"] = "";
                    if (selLevels.IndexOf("BillDate") < 0)
                        dr_final7["BillDate"] = "";
                    if (selLevels.IndexOf("PayMode") < 0)
                        dr_final7["PayMode"] = "";
                    if (selLevels.IndexOf("ProductDesc") < 0)
                        dr_final7["ProductDesc"] = "";
                    if (selLevels.IndexOf("CategoryID") < 0)
                        dr_final7["CategoryID"] = "";
                    if (selLevels.IndexOf("ProductName") < 0)
                        dr_final7["ProductName"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final7["ItemCode"] = "";

                    dr_final7["Model"] = ""; ;
                    dr_final7["BillNo"] = ""; ;
                    dr_final7["Internaltransfer"] = ""; ;
                    dr_final7["salesReturn"] = "";
                    dr_final7["Add1"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["Discount"] = "";
                    dr_final7["Freight"] = "";
                    dr_final7["Qty"] = "";
                    dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                    dt.Rows.Add(dr_final7);
                    modelTotal = 0;
                }

                if (ddlFifthLvl.SelectedIndex > 0)
                {
                    DataRow dr_final7 = dt.NewRow();
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
                    dr_final7[ddlFifthLvl.SelectedItem.Text] = "Total:" + fifLvlValueTemp;
                    if (selLevels.IndexOf("SupplierID") < 0)
                        dr_final7["SupplierID"] = "";
                    if (selLevels.IndexOf("BillDate") < 0)
                        dr_final7["BillDate"] = "";
                    if (selLevels.IndexOf("PayMode") < 0)
                        dr_final7["PayMode"] = "";
                    if (selLevels.IndexOf("ProductDesc") < 0)
                        dr_final7["ProductDesc"] = "";
                    if (selLevels.IndexOf("CategoryID") < 0)
                        dr_final7["CategoryID"] = "";
                    if (selLevels.IndexOf("ProductName") < 0)
                        dr_final7["ProductName"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final7["ItemCode"] = "";

                    dr_final7["Model"] = ""; ;
                    dr_final7["BillNo"] = ""; ;
                    dr_final7["Internaltransfer"] = ""; ;
                    dr_final7["salesReturn"] = "";
                    dr_final7["Add1"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["Discount"] = "";
                    dr_final7["Freight"] = "";
                    dr_final7["Qty"] = "";
                    dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                    dt.Rows.Add(dr_final7);
                    catIDTotal = 0;
                }

                if (ddlFourthLvl.SelectedIndex > 0)
                {
                    DataRow dr_final7 = dt.NewRow();
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

                    if (selLevels.IndexOf("SupplierID") < 0)
                        dr_final7["SupplierID"] = "";
                    if (selLevels.IndexOf("BillDate") < 0)
                        dr_final7["BillDate"] = "";
                    if (selLevels.IndexOf("PayMode") < 0)
                        dr_final7["PayMode"] = "";
                    if (selLevels.IndexOf("ProductDesc") < 0)
                        dr_final7["ProductDesc"] = "";
                    if (selLevels.IndexOf("CategoryID") < 0)
                        dr_final7["CategoryID"] = "";
                    if (selLevels.IndexOf("ProductName") < 0)
                        dr_final7["ProductName"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final7["ItemCode"] = "";

                    dr_final7["Model"] = ""; ;
                    dr_final7["BillNo"] = ""; ;
                    dr_final7["Internaltransfer"] = ""; ;
                    dr_final7["salesReturn"] = "";
                    dr_final7["Add1"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["Discount"] = "";
                    dr_final7["Freight"] = "";
                    dr_final7["Qty"] = "";
                    dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                    dt.Rows.Add(dr_final7);
                    brandTotal = 0;
                }

                if (ddlThirdLvl.SelectedIndex > 0)
                {
                    DataRow dr_final7 = dt.NewRow();
                    if (ddlFirstLvl.SelectedIndex > 0)
                    {
                        dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    }
                    if (ddlSecondLvl.SelectedIndex > 0)
                    {
                        dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    }
                    dr_final7[ddlThirdLvl.SelectedItem.Text] = "Total:" + tLvlValueTemp;
                    if (selLevels.IndexOf("SupplierID") < 0)
                        dr_final7["SupplierID"] = "";
                    if (selLevels.IndexOf("BillDate") < 0)
                        dr_final7["BillDate"] = "";
                    if (selLevels.IndexOf("PayMode") < 0)
                        dr_final7["PayMode"] = "";
                    if (selLevels.IndexOf("ProductDesc") < 0)
                        dr_final7["ProductDesc"] = "";
                    if (selLevels.IndexOf("CategoryID") < 0)
                        dr_final7["CategoryID"] = "";
                    if (selLevels.IndexOf("ProductName") < 0)
                        dr_final7["ProductName"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final7["ItemCode"] = "";

                    dr_final7["Model"] = ""; ;
                    dr_final7["BillNo"] = ""; ;
                    dr_final7["Internaltransfer"] = ""; ;
                    dr_final7["salesReturn"] = "";
                    dr_final7["Add1"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["Discount"] = "";
                    dr_final7["Freight"] = "";
                    dr_final7["Qty"] = "";
                    dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));
                    dt.Rows.Add(dr_final7);
                    brandTotal1 = 0;
                }

                if (ddlSecondLvl.SelectedIndex > 0)
                {
                    DataRow dr_final7 = dt.NewRow();
                    if (ddlFirstLvl.SelectedIndex > 0)
                    {
                        dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    }
                    dr_final7[ddlSecondLvl.SelectedItem.Text] = "Total:" + sLvlValueTemp; ;
                    if (selLevels.IndexOf("SupplierID") < 0)
                        dr_final7["SupplierID"] = "";
                    if (selLevels.IndexOf("BillDate") < 0)
                        dr_final7["BillDate"] = "";
                    if (selLevels.IndexOf("PayMode") < 0)
                        dr_final7["PayMode"] = "";
                    if (selLevels.IndexOf("ProductDesc") < 0)
                        dr_final7["ProductDesc"] = "";
                    if (selLevels.IndexOf("CategoryID") < 0)
                        dr_final7["CategoryID"] = "";
                    if (selLevels.IndexOf("ProductName") < 0)
                        dr_final7["ProductName"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final7["ItemCode"] = "";

                    dr_final7["Model"] = ""; ;
                    dr_final7["BillNo"] = ""; ;
                    dr_final7["Internaltransfer"] = ""; ;
                    dr_final7["salesReturn"] = "";
                    dr_final7["Add1"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["Discount"] = "";
                    dr_final7["Freight"] = "";
                    dr_final7["Qty"] = "";
                    dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));
                    dt.Rows.Add(dr_final7);
                    brandTotal2 = 0;
                }
                if (ddlFirstLvl.SelectedIndex > 0)
                {
                    DataRow dr_final7 = dt.NewRow();
                    dr_final7[ddlFirstLvl.SelectedItem.Text] = "Total:" + fLvlValueTemp;
                    if (selLevels.IndexOf("SupplierID") < 0)
                        dr_final7["SupplierID"] = "";
                    if (selLevels.IndexOf("BillDate") < 0)
                        dr_final7["BillDate"] = "";
                    if (selLevels.IndexOf("PayMode") < 0)
                        dr_final7["PayMode"] = "";
                    if (selLevels.IndexOf("ProductDesc") < 0)
                        dr_final7["ProductDesc"] = "";
                    if (selLevels.IndexOf("CategoryID") < 0)
                        dr_final7["CategoryID"] = "";
                    if (selLevels.IndexOf("ProductName") < 0)
                        dr_final7["ProductName"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final7["ItemCode"] = "";

                    dr_final7["Model"] = ""; ;
                    dr_final7["BillNo"] = ""; ;
                    dr_final7["Internaltransfer"] = ""; ;
                    dr_final7["salesReturn"] = "";
                    dr_final7["Add1"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["Discount"] = "";
                    dr_final7["Freight"] = "";
                    dr_final7["Qty"] = "";
                    dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));
                    dt.Rows.Add(dr_final7);
                    brandTotal3 = 0;
                }
                DataRow dr_final6 = dt.NewRow();
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
                if (ddlSixthLvl.SelectedIndex > 0)
                {
                    dr_final6[ddlSixthLvl.SelectedItem.Text] = "";
                }
                if (ddlSeventhLvl.SelectedIndex > 0)
                {
                    dr_final6[ddlSeventhLvl.SelectedItem.Text] = "";
                }
                if (selLevels.IndexOf("SupplierID") < 0)
                    dr_final6["SupplierID"] = "";
                if (selLevels.IndexOf("BillDate") < 0)
                    dr_final6["BillDate"] = "";
                if (selLevels.IndexOf("PayMode") < 0)
                    dr_final6["PayMode"] = "";
                if (selLevels.IndexOf("ProductDesc") < 0)
                    dr_final6["ProductDesc"] = "";
                if (selLevels.IndexOf("CategoryID") < 0)
                    dr_final6["CategoryID"] = "";
                if (selLevels.IndexOf("ProductName") < 0)
                    dr_final6["ProductName"] = "";
                if (selLevels.IndexOf("ItemCode") < 0)
                    dr_final6["ItemCode"] = "";

                dr_final6["Model"] = "Grand Total: ";
                dr_final6["BillNo"] = "";
                dr_final6["Internaltransfer"] = "";
                dr_final6["salesReturn"] = "";
                dr_final6["Add1"] = "";
                dr_final6["Phone"] = "";
                dr_final6["Discount"] = "";
                dr_final6["Freight"] = Convert.ToString(Convert.ToDecimal(Gttl)); ;
                dr_final6["Qty"] = "";
                dr_final6["Rate"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                dt.Rows.Add(dr_final6);
            }
            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Not Data Found');", true);
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            string filename = "PurchaseRpt.xls";
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
}
