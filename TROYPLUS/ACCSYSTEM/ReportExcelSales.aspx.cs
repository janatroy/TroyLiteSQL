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

public partial class Sales : System.Web.UI.Page
{
    //int custid = 0;
    //string date = "";
    //int paymode = 0;
    //string brand = "";
    //int catid = 0;
    //string model = "";
    //string product = "";
    //string productcode = "";
    decimal Gtotal = 0;
    decimal Gttl = 0;
    decimal Pttls = 0;
    decimal brandTotal = 0, catIDTotal = 0, modelTotal = 0, brandTotal1 = 0, brandTotal2 = 0, brandTotal3 = 0, brandTotal4 = 0;
    string groupBy = "", selColumn = "", selLevels = "";
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
        ddlist.Items.Insert(1, "CustomerID");
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
        ds = objBL.getDistinctCustid();
        ddlCustid.DataSource = ds;
        ddlCustid.DataTextField = "CustomerID";
        ddlCustid.DataValueField = "CustomerID";
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
                getgroupByAndselColumn();
                DataSet ds = new DataSet();
                ds = objBL.getSales(selColumn, condtion, groupBy);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridSales.DataSource = ds;
                    GridSales.DataBind();
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
        //Where BillDate between CDate('" + StartDate + "') and CDate('" + EndDate + "') and Internaltransfer='" + Internals + "' and purchaseReturn='" + Models + "' and CustomerID=" + Catids + " and CategoryID=" + Categorys + " and PayMode=" + Stocks + " and ProductName='" + PrdctNme + "' and ProductDesc='" + Brands + "' and tblProductMaster.ItemCode='" + Productcd + "'");
        if (txtStrtDt.Text != "" && txtEndDt.Text != "")
        {
            objBL.StartDate = txtStrtDt.Text;
            objBL.EndDate = txtEndDt.Text;
            cond = " and (BillDate) Between CDate('" + txtStrtDt.Text + "') and  CDate('" + txtEndDt.Text + "')";
        }
        if ((rblPurchseRtn.SelectedItem.Text == "Yes") & (rblPurchseRtn.SelectedItem.Text == "No"))
        {
            objBL.Models = rblPurchseRtn.SelectedItem.Text;
            cond += " and purchaseReturn=" + rblPurchseRtn.SelectedItem.Text.ToUpper() + " ";
        }
        if ((rblIntrnlTrns.SelectedItem.Text == "Yes") & (rblIntrnlTrns.SelectedItem.Text == "No"))
        {
            objBL.Internals = rblIntrnlTrns.SelectedItem.Text;
            cond += " and Internaltransfer=" + rblIntrnlTrns.SelectedItem.Text + " ";
        }
        if (ddlCustid.SelectedIndex > 0)
        {
            objBL.Catids = Convert.ToInt32(ddlCustid.SelectedItem.Value);
            cond += " and CustomerID=" + Convert.ToInt32(ddlCustid.SelectedItem.Value) + " ";
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
    protected void getgroupByAndselColumn()
    {
        groupBy = "";
        selColumn = "";

        if (ddlFirstLvl.SelectedIndex > 0)
        {
            groupBy = " Order by " + ddlFirstLvl.SelectedItem.Text + " ";
            selColumn = " " + ddlFirstLvl.SelectedItem.Text + " ";
        }
        if (ddlSecondLvl.SelectedIndex > 0)
        {
            if (groupBy == "")
            {
                groupBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                groupBy += " , ";
                selColumn += " , ";
            }
            groupBy += " " + ddlSecondLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlSecondLvl.SelectedItem.Text + " ";
        }
        if (ddlThirdLvl.SelectedIndex > 0)
        {
            if (groupBy == "")
            {
                groupBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                groupBy += " , ";
                selColumn += " , ";
            }
            groupBy += " " + ddlThirdLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlThirdLvl.SelectedItem.Text + " ";
        }
        if (ddlFourthLvl.SelectedIndex > 0)
        {
            if (groupBy == "")
            {
                groupBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                groupBy += " , ";
                selColumn += " , ";
            }
            groupBy += " " + ddlFourthLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlFourthLvl.SelectedItem.Text + " ";
        }
        if (ddlFifthLvl.SelectedIndex > 0)
        {
            if (groupBy == "")
            {
                groupBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                groupBy += " , ";
                selColumn += " , ";
            }
            groupBy += " " + ddlFifthLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlFifthLvl.SelectedItem.Text + " ";
        }
        if (ddlSixthLvl.SelectedIndex > 0)
        {
            if (groupBy == "")
            {
                groupBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                groupBy += " , ";
                selColumn += " , ";
            }
            groupBy += " " + ddlSixthLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlSixthLvl.SelectedItem.Text + " ";
        }
        if (ddlSeventhLvl.SelectedIndex > 0)
        {
            if (groupBy == "")
            {
                groupBy = " Order by ";
                selColumn = " ";
            }
            else
            {
                groupBy += " , ";
                selColumn += " , ";
            }
            groupBy += " " + ddlSeventhLvl.SelectedItem.Text + " ";
            selColumn += " " + ddlSeventhLvl.SelectedItem.Text + " ";
        }
        if (groupBy == "" && selColumn == "")
        {
            groupBy = " order by CustomerID,BillDate,PayMode,ProductDesc,CategoryID,ProductName,ItemCode";
            selColumn = " tblSales.BillNo,BillDate,PayMode,Internaltransfer,purchaseReturn,ItemCode,ProductName,ProductDesc,CategoryID,Model,CustomerID,CustomerAddress,Phone,tblEmployee.empFirstName,tblSalesItems.Discount,tblSalesItems.Vat,tblSalesItems.Qty,Freight";
        }
        else
        {
            selLevels = selColumn;
            if (groupBy.IndexOf("CustomerID") < 0)
            {
                groupBy += " , CustomerID ";
                selColumn += " , CustomerID ";
            }
            if (groupBy.IndexOf("BillDate") < 0)
            {
                groupBy += " , BillDate ";
                selColumn += " , BillDate ";
            }
            if (groupBy.IndexOf("PayMode") < 0)
            {
                groupBy += " , PayMode ";
                selColumn += " , PayMode ";
            }
            if (groupBy.IndexOf("ProductDesc") < 0)
            {
                groupBy += " , ProductDesc ";
                selColumn += " , ProductDesc ";
            }
            if (groupBy.IndexOf("CategoryID") < 0)
            {
                groupBy += " , CategoryID ";
                selColumn += " , CategoryID ";
            }
            if (groupBy.IndexOf("ProductName") < 0)
            {
                groupBy += " , ProductName ";
                selColumn += " , ProductName ";
            }
            if (groupBy.IndexOf("ItemCode") < 0)
            {
                groupBy += " ,  ItemCode";
                selColumn += " , ItemCode ";
            }
            //groupBy = " order by CustomerID,BillDate,PayMode,ProductDesc,CategoryID,ProductName,tblProductMaster.ItemCode";
            selColumn = " tblSales.BillNo,BillDate,PayMode,Internaltransfer,purchaseReturn,ItemCode,ProductName,ProductDesc,CategoryID,Model,CustomerID,CustomerAddress,Phone,tblEmployee.empFirstName,tblSalesItems.Discount,tblSalesItems.Vat,tblSalesItems.Qty,Freight";
        }
        selColumn = selColumn.Replace("ItemCode", "tblProductMaster.ItemCode As ItemCode");
        selColumn = selColumn.Replace("Phone", "CustomerContacts As Phone");
        groupBy = groupBy.Replace("ItemCode", "tblProductMaster.ItemCode");
        //groupBy = groupBy.Replace("PaymentMode", "IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo)");
    }
    private bool isValidLevels()
    {
        if ((ddlFirstLvl.SelectedItem.Text != "None") &&
            (ddlFirstLvl.SelectedItem.Text == ddlSecondLvl.SelectedItem.Text) ||
            (ddlFirstLvl.SelectedItem.Text == ddlThirdLvl.SelectedItem.Text) ||
            (ddlFirstLvl.SelectedItem.Text == ddlFourthLvl.SelectedItem.Text) ||
            (ddlFirstLvl.SelectedItem.Text == ddlFifthLvl.SelectedItem.Text) ||
            (ddlFirstLvl.SelectedItem.Text == ddlSixthLvl.SelectedItem.Text) ||
            (ddlFirstLvl.SelectedItem.Text == ddlSeventhLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddlSecondLvl.SelectedItem.Text != "None") &&
            (ddlSecondLvl.SelectedItem.Text == ddlThirdLvl.SelectedItem.Text) ||
            (ddlSecondLvl.SelectedItem.Text == ddlFourthLvl.SelectedItem.Text) ||
            (ddlSecondLvl.SelectedItem.Text == ddlFifthLvl.SelectedItem.Text) ||
            (ddlSecondLvl.SelectedItem.Text == ddlSixthLvl.SelectedItem.Text) ||
            (ddlSecondLvl.SelectedItem.Text == ddlSeventhLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddlThirdLvl.SelectedItem.Text != "None") &&
            (ddlThirdLvl.SelectedItem.Text == ddlFourthLvl.SelectedItem.Text) ||
            (ddlThirdLvl.SelectedItem.Text == ddlFifthLvl.SelectedItem.Text) ||
            (ddlThirdLvl.SelectedItem.Text == ddlSixthLvl.SelectedItem.Text) ||
            (ddlThirdLvl.SelectedItem.Text == ddlSeventhLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddlFourthLvl.SelectedItem.Text != "None") &&
           (ddlFourthLvl.SelectedItem.Text == ddlFifthLvl.SelectedItem.Text) ||
           (ddlFourthLvl.SelectedItem.Text == ddlSixthLvl.SelectedItem.Text) ||
           (ddlFourthLvl.SelectedItem.Text == ddlSeventhLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddlFifthLvl.SelectedItem.Text != "None") &&
          (ddlFifthLvl.SelectedItem.Text == ddlSixthLvl.SelectedItem.Text) ||
          (ddlFifthLvl.SelectedItem.Text == ddlSeventhLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddlSixthLvl.SelectedItem.Text != "None") &&
          (ddlSixthLvl.SelectedItem.Text == ddlSeventhLvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        return true;
    }
    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!isValidLevels())
            //{
            //    return;
            //}
            string cond = "";
            cond = getCond();
            getgroupByAndselColumn();
            bindData(selColumn, cond, groupBy);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void bindData(string selColumn, string cond, string groupBy)
    {
        bool dispLastTotal = false;
        string condtion = "";
        condtion = getCond();
        getgroupByAndselColumn();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = objBL.getSales(selColumn, condtion, groupBy);
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

            if (selLevels.IndexOf("CustomerID") < 0)
                dt.Columns.Add(new DataColumn("CustomerID"));
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
            dt.Columns.Add(new DataColumn("purchaseReturn"));
            dt.Columns.Add(new DataColumn("CustomerAddress"));
            dt.Columns.Add(new DataColumn("Phone"));
            dt.Columns.Add(new DataColumn("empFirstName"));
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
                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final7["CustomerID"] = "";
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
                        dr_final7["purchaseReturn"] = "";
                        dr_final7["CustomerAddress"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["empFirstName"] = "";
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
                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final7["CustomerID"] = "";
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
                        dr_final7["purchaseReturn"] = "";
                        dr_final7["CustomerAddress"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["empFirstName"] = "";
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
                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final7["CustomerID"] = "";
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
                        dr_final7["purchaseReturn"] = "";
                        dr_final7["CustomerAddress"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["empFirstName"] = "";
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
                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final7["CustomerID"] = "";
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
                        dr_final7["purchaseReturn"] = "";
                        dr_final7["CustomerAddress"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["empFirstName"] = "";
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
                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final7["CustomerID"] = "";
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
                        dr_final7["purchaseReturn"] = "";
                        dr_final7["CustomerAddress"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["empFirstName"] = "";
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
                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final7["CustomerID"] = "";
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
                        dr_final7["purchaseReturn"] = "";
                        dr_final7["CustomerAddress"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["empFirstName"] = "";
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
                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final7["CustomerID"] = "";
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
                        dr_final7["purchaseReturn"] = "";
                        dr_final7["CustomerAddress"] = "";
                        dr_final7["Phone"] = "";
                        dr_final7["empFirstName"] = "";
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

                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final1["CustomerID"] = "";
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
                        dr_final1["purchaseReturn"] = "";
                        dr_final1["CustomerAddress"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["empFirstName"] = "";
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

                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final2["CustomerID"] = "";
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
                        dr_final2["purchaseReturn"] = "";
                        dr_final2["CustomerAddress"] = "";
                        dr_final2["Phone"] = "";
                        dr_final2["empFirstName"] = "";
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

                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final1["CustomerID"] = "";
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
                        dr_final1["purchaseReturn"] = "";
                        dr_final1["CustomerAddress"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["empFirstName"] = "";
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

                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final1["CustomerID"] = "";
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
                        dr_final1["purchaseReturn"] = "";
                        dr_final1["CustomerAddress"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["empFirstName"] = "";
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

                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final1["CustomerID"] = "";
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
                        dr_final1["purchaseReturn"] = "";
                        dr_final1["CustomerAddress"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["empFirstName"] = "";
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

                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final1["CustomerID"] = "";
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
                        dr_final1["purchaseReturn"] = "";
                        dr_final1["CustomerAddress"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["empFirstName"] = "";
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
                            dr_final1[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSeventhLvl.SelectedIndex > 0)
                        {
                            dr_final1[ddlSeventhLvl.SelectedItem.Text] = svthLvlValueTemp;
                        }

                        if (selLevels.IndexOf("CustomerID") < 0)
                            dr_final1["CustomerID"] = "";
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
                        dr_final1["purchaseReturn"] = "";
                        dr_final1["CustomerAddress"] = "";
                        dr_final1["Phone"] = "";
                        dr_final1["empFirstName"] = "";
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
                if (selLevels.IndexOf("CustomerID") < 0)
                    dr_final5["CustomerID"] = dr["CustomerID"];
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
                dr_final5["purchaseReturn"] = dr["purchaseReturn"];
                dr_final5["CustomerAddress"] = dr["CustomerAddress"];
                dr_final5["Phone"] = dr["Phone"];
                dr_final5["empFirstName"] = dr["empFirstName"];
                dr_final5["Discount"] = dr["Discount"];
                dr_final5["Freight"] = dr["Freight"];
                dr_final5["Qty"] = dr["Qty"];
                dr_final5["Rate"] = dr["Rate"];
                dr_final5["Amount"] = dr["Amount"];
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
                brandTotal4 = brandTotal4 + Convert.ToDecimal(dr["Amount"]);
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

                    if (selLevels.IndexOf("CustomerID") < 0)
                        dr_final7["CustomerID"] = "";
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
                    dr_final7["purchaseReturn"] = "";
                    dr_final7["CustomerAddress"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["empFirstName"] = "";
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

                    if (selLevels.IndexOf("CustomerID") < 0)
                        dr_final7["CustomerID"] = "";
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
                    dr_final7["purchaseReturn"] = "";
                    dr_final7["CustomerAddress"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["empFirstName"] = "";
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
                    if (selLevels.IndexOf("CustomerID") < 0)
                        dr_final7["CustomerID"] = "";
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
                    dr_final7["purchaseReturn"] = "";
                    dr_final7["CustomerAddress"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["empFirstName"] = "";
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

                    if (selLevels.IndexOf("CustomerID") < 0)
                        dr_final7["CustomerID"] = "";
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
                    dr_final7["purchaseReturn"] = "";
                    dr_final7["CustomerAddress"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["empFirstName"] = "";
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
                    if (selLevels.IndexOf("CustomerID") < 0)
                        dr_final7["CustomerID"] = "";
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
                    dr_final7["purchaseReturn"] = "";
                    dr_final7["CustomerAddress"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["empFirstName"] = "";
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
                    if (selLevels.IndexOf("CustomerID") < 0)
                        dr_final7["CustomerID"] = "";
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
                    dr_final7["purchaseReturn"] = "";
                    dr_final7["CustomerAddress"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["empFirstName"] = "";
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
                    if (selLevels.IndexOf("CustomerID") < 0)
                        dr_final7["CustomerID"] = "";
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
                    dr_final7["purchaseReturn"] = "";
                    dr_final7["CustomerAddress"] = "";
                    dr_final7["Phone"] = "";
                    dr_final7["empFirstName"] = "";
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
                if (selLevels.IndexOf("CustomerID") < 0)
                    dr_final6["CustomerID"] = "";
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
                dr_final6["purchaseReturn"] = "";
                dr_final6["CustomerAddress"] = "";
                dr_final6["Phone"] = "";
                dr_final6["empFirstName"] = "";
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
            string filename = "Sales.xls";
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
