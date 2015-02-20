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

public partial class Default6 : System.Web.UI.Page
{
    decimal Gtotal = 0;
    decimal Gttl = 0;
    decimal Pttls = 0;
    decimal P1ttls = 0;
    decimal P2ttls = 0;
    decimal P3ttls = 0;
    decimal P4ttls = 0;
    decimal P6ttls = 0;
    decimal P5ttls = 0;
    decimal brandTotal = 0, catIDTotal = 0, modelTotal = 0, modelTotal1 = 0, brandTotal1 = 0, brandTotal2 = 0, brandTotal3 = 0, brandTotal4 = 0;
    string groupBy = "", selColumn = "", selLevels = "", field1 = "", field2 = "", ordrby = "";


    BusinessLogic objBL;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ModalPopupSales.Show();

            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            if (!Page.IsPostBack)
            {
                RequiredFieldValidator5.Enabled = false;
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
                fillDdl(odlfirstlvl);
                fillDdl(odlsecondlvl);
                fillDdl(odlthirdlvl);
                fillDdl(odlfourlvl);
                fillDdl(odlfifthlvl);
                fillDdl(odlsixthlvl);
                fillDdl(odlseventhlvl);

                fillDdllist(firstsub);
                fillDdllist(secondsub);
                fillDdllist(thirdsub);
                fillDdllist(foursub);
                fillDdllist(fivesub);

                fillretrive();
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
        ddlist.Items.Insert(1, "CustomerName");
        ddlist.Items.Insert(2, "BillDate");
        ddlist.Items.Insert(3, "PayMode");
        ddlist.Items.Insert(4, "Brand");
        ddlist.Items.Insert(5, "CategoryName");
        ddlist.Items.Insert(6, "ProductName");
        ddlist.Items.Insert(7, "ItemCode");
    }

    private void fillDdllist(DropDownList ddlistl)
    {
        //DropDownList ddlList = new DropDownList();
        ddlistl.Items.Insert(0, "None");
        ddlistl.Items.Insert(1, "CustomerName");
        ddlistl.Items.Insert(2, "BillDate");
        ddlistl.Items.Insert(3, "PayMode");
        ddlistl.Items.Insert(4, "Brand");
        ddlistl.Items.Insert(5, "CategoryName");
        ddlistl.Items.Insert(4, "Model");
        ddlistl.Items.Insert(6, "ProductName");
        ddlistl.Items.Insert(7, "ItemCode");
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
        ddlCategory.DataSource = ds;
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataValueField = "CategoryID";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, "All");
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

        ddlCustNme.DataSource = ds;
        ddlCustNme.DataTextField = "CustomerName";
        ddlCustNme.DataValueField = "CustomerID";
        ddlCustNme.DataBind();
        ddlCustNme.Items.Insert(0, "All");
    }

    private void fillretrive()
    {
        DataSet ds = new DataSet();
        ds = objBL.getsavenamesales();
        ddlretrive.DataSource = ds;
        ddlretrive.DataTextField = "savname";
        ddlretrive.DataValueField = "savname";
        ddlretrive.DataBind();
        ddlretrive.Items.Insert(0, "Select a Name");
    }

    private void fillPayMode()
    {
        //DataSet ds = new DataSet();
        //ds = objBL.getDistinctCustid();
        //ddlPayMode.DataSource = ds;
        //ddlPayMode.DataTextField = "LedgerName";
        //ddlPayMode.DataValueField = "PayMode";
        //ddlPayMode.DataBind();
        //ddlPayMode.Items.Insert(0, "All");

        ddlPayMode.Items.Insert(0, "All");
        ddlPayMode.Items.Insert(1, "Credit");
        ddlPayMode.Items.Insert(2, "Cash");
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

    protected string getfield()
    {
        string field1 = "";
        //string field2="";
        // string field = "";
        if (chkboxCategory.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }

            field1 += "CategoryName";
        }
        if (chkboxCustomer.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }

            field1 += "CustomerName";
        }
        if (chkboxBrand.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }
            field1 += "Brand";
        }
        if (chkboxProductName.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }
            field1 += "ProductName";

        }
        if (chkboxProductCode.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }
            field1 += "ProductCode";

        }

        if (chkboxInternalTransfer.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }

            field1 += "InternalTransfer";
        }
        if (chkboxPurchaseReturn.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }

            field1 += "PurchaseReturn";
        }
        if (ChkboxPaymode.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }

            //field1 += "LedgerName";
            field1 += "Paymode";
        }
        if (chkboxBillDate.Checked)
        {

            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }

            field1 += "BillDate";
        }
        if (chkboxBillno.Checked == true)
        {
            if (field1 == "")
            {
                field1 = "";

            }
            else
            {
                field1 += ",";
            }
            field1 += "tblSales.Billno";
        }
        if (chkboxModel.Checked == true)
        {
            if (field1 == "")
            {
                field1 = "";

            }
            else
            {
                field1 += ",";
            }
            field1 += "Model";
        }
        if (ChkboxCustaddr.Checked == true)
        {
            if (field1 == "")
            {
                field1 = "";

            }
            else
            {
                field1 += ",";
            }
            field1 += "Add1";
        }
        if (ChkboxCustphone.Checked == true)
        {
            if (field1 == "")
            {
                field1 = "";

            }
            else
            {
                field1 += ",";
            }
            field1 += "CustomerContacts";
        }

        if (ChkboxEmpname.Checked == true)
        {
            if (field1 == "")
            {
                field1 = "";

            }
            else
            {
                field1 += ",";
            }
            //field1 += "tblEmployee.empFirstName";

            field1 += "executivename";

        }
        if (field1 != "")
        {
            field1 += ",";
        }
        //field2 = ",Model,CustomerAddress,CustomerContacts,tblEmployee.empFirstName,tblSales.BillNo";
        field1 = field1.Replace("Brand", "ProductDesc");
        field1 = field1.Replace("ProductCode", "ItemCode");
        //field = field1 + field2;
        return field1;
    }
    protected string getfield2()
    {
        string field2 = "";
        getgroupByAndselColumn();
        if (groupBy != "")
        {
            if (chkboxRate.Checked)
            {
                if (field2 == "")
                {

                    field2 = "";
                }
                else
                {

                    field2 += " , ";
                }
                //field1 += "ItemCode";

                field2 += "Sum(tblSalesItems.Rate) as Rate";
            }


            if (chkboxFreight.Checked)
            {
                if (field2 == "")
                {

                    field2 = "";
                }
                else
                {

                    field2 += " , ";
                }

                field2 += "Sum(tblSales.Freight) as Freight";
            }
            if (chkboxStock.Checked)
            {
                if (field2 == "")
                {

                    field2 = "";
                }
                else
                {

                    field2 += " , ";
                }

                field2 += "Sum(tblSalesItems.Qty) as Qty";
            }
            if (chkboxDiscount.Checked)
            {
                if (field2 == "")
                {

                    field2 = "";
                }
                else
                {

                    field2 += " , ";
                }

                field2 += "Sum(tblSales.Discount) as Discount";
            }
            if (field2 != "")
            {
                field2 += ",sum((tblSalesItems.Qty)*(tblSalesItems.Rate)) As Amount";
            }
            // field2 = field2.Replace("Quantity", "Stock");
        }
        else
        {
            if (chkboxRate.Checked)
            {
                if (field2 == "")
                {

                    field2 = "";
                }
                else
                {

                    field2 += " , ";
                }
                //field1 += "ItemCode";

                field2 += "tblSalesItems.Rate";
            }

            if (chkboxFreight.Checked)
            {
                if (field2 == "")
                {

                    field2 = "";
                }
                else
                {

                    field2 += " , ";
                }

                field2 += "tblSales.Freight";
            }

            if (chkboxDiscount.Checked)
            {
                if (field2 == "")
                {

                    field2 = "";
                }
                else
                {

                    field2 += " , ";
                }

                field2 += "tblSales.Discount";
            }
            if (chkboxStock.Checked)
            {
                if (field2 == "")
                {

                    field2 = "";
                }
                else
                {

                    field2 += " , ";
                }

                field2 += "tblSalesItems.Qty";
            }

            // field2 = field2.Replace("Quantity", "Stock");
            if (field2 != "")
            {
                field2 += ",((tblSalesItems.Qty)*(tblSalesItems.Rate)) As Amount";
            }
        }
        return field2;

    }
    protected string getCond()
    {
        string cond = "";
        //Where BillDate between CDate('" + StartDate + "') and CDate('" + EndDate + "') and Internaltransfer='" + Internals + "' and purchaseReturn='" + Models + "' and CustomerID=" + Catids + " and CategoryID=" + Categorys + " and PayMode=" + Stocks + " and ProductName='" + PrdctNme + "' and ProductDesc='" + Brands + "' and tblProductMaster.ItemCode='" + Productcd + "'");
        if (txtStrtDt.Text != "" && txtEndDt.Text != "")
        {

            objBL.StartDate = txtStrtDt.Text;

            objBL.StartDate = string.Format("{0:MM/dd/yyyy}", txtStrtDt.Text);
            objBL.EndDate = txtEndDt.Text;
            objBL.EndDate = string.Format("{0:MM/dd/yyyy}", txtEndDt.Text);
            cond = " (BillDate) Between CDate('" + txtStrtDt.Text + "') and  CDate('" + txtEndDt.Text + "')";
        }
        if ((rblPurchseRtn.SelectedItem.Text == "Yes"))
        {
            objBL.Models = rblPurchseRtn.SelectedItem.Text;

            cond += " and purchaseReturn='" + rblPurchseRtn.SelectedItem.Text.ToUpper() + "'";
        }
        else if ((rblPurchseRtn.SelectedItem.Text == "No"))
        {
            objBL.Models = rblPurchseRtn.SelectedItem.Text;
            cond += " and purchaseReturn='" + rblPurchseRtn.SelectedItem.Text.ToUpper() + "'";
        }
        if ((rblIntrnlTrns.SelectedItem.Text == "Yes"))
        {
            objBL.Internals = rblIntrnlTrns.SelectedItem.Text;
            cond += " and Internaltransfer='" + rblIntrnlTrns.SelectedItem.Text + "'";
        }
        else if ((rblIntrnlTrns.SelectedItem.Text == "No"))
        {
            objBL.Internals = rblIntrnlTrns.SelectedItem.Text;
            cond += " and Internaltransfer='" + rblIntrnlTrns.SelectedItem.Text + "'";
        }

        if ((ddlCustNme.SelectedItem.Text != "All"))
        {
            objBL.Catids = Convert.ToInt32(ddlCustNme.SelectedItem.Value);
            cond += " and CustomerID=" + Convert.ToInt32(ddlCustNme.SelectedItem.Value) + " ";

        }
        if ((ddlCategory.SelectedItem.Text != "All"))
        {
            objBL.Categorys = Convert.ToInt32(ddlCategory.SelectedItem.Value);
            cond += " and tblproductmaster.CategoryID=" + Convert.ToInt32(ddlCategory.SelectedItem.Value) + " ";
        }

        if ((ddlPayMode.SelectedItem.Text != "All"))
        {
            objBL.Stocks = Convert.ToInt32(ddlPayMode.SelectedItem.Value);
            cond += " and PayMode=" + Convert.ToInt32(ddlPayMode.SelectedItem.Value) + " ";
        }
        if (ddlPrdctCode.SelectedItem.Text != "All")
        {
            objBL.Productcd = ddlPrdctCode.SelectedItem.Text;
            cond += " and tblProductMaster.ItemCode='" + ddlPrdctCode.SelectedItem.Text + "' ";
        }
        if (ddlPrdctNme.SelectedItem.Text != "All")
        {
            objBL.PrdctNmes = ddlPrdctNme.SelectedItem.Text;
            cond += " and ProductName='" + ddlPrdctNme.SelectedItem.Text + "' ";
        }
        if (ddlBrand.SelectedItem.Text != "All")
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

        if (ddlFirstLvl.SelectedItem.Text != "None")
        {
            groupBy = " Group by " + ddlFirstLvl.SelectedItem.Text + " ";
            selColumn = " " + ddlFirstLvl.SelectedItem.Text + " ";
        }
        if (ddlSecondLvl.SelectedItem.Text != "None")
        {
            if (groupBy == "")
            {
                groupBy = " Group by ";
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
        if (ddlThirdLvl.SelectedItem.Text != "None")
        {
            if (groupBy == "")
            {
                groupBy = " Group by ";
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
        if (ddlFourthLvl.SelectedItem.Text != "None")
        {
            if (groupBy == "")
            {
                groupBy = " Group by ";
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
        if (ddlFifthLvl.SelectedItem.Text != "None")
        {
            if (groupBy == "")
            {
                groupBy = " Group by ";
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
        if (ddlSixthLvl.SelectedItem.Text != "None")
        {
            if (groupBy == "")
            {
                groupBy = " Group by ";
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
        if (ddlSeventhLvl.SelectedItem.Text != "None")
        {
            if (groupBy == "")
            {
                groupBy = " Group by ";
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
        if (groupBy == "")
        {
            // groupBy = " Group by CustomerID,BillDate,PayMode,ProductDesc,CategoryID,ProductName,ItemCode";
            //selColumn = " tblSales.BillNo,BillDate,PayMode,Internaltransfer,purchaseReturn,ItemCode,ProductName,ProductDesc,CategoryID,Model,CustomerID,CustomerAddress,CustomerContacts,tblEmployee.empFirstName,tblSalesItems.Discount,tblSalesItems.Vat,tblSalesItems.Qty,Freight";
            field1 = getfield();
            // groupBy += " group by "+ field1;
            selColumn += field1;
        }
        else
        {
            selColumn += ",";
        }
        /* else
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
            selColumn = " tblSales.BillNo,BillDate,PayMode,Internaltransfer,purchaseReturn,ItemCode,ProductName,ProductDesc,CategoryID,Model,CustomerID,CustomerAddress,CustomerContacts,tblEmployee.empFirstName,tblSalesItems.Discount,tblSalesItems.Vat,tblSalesItems.Qty,Freight";
        }*/
        selColumn = selColumn.Replace("ItemCode", "(tblProductMaster.ItemCode) As ItemCode");
        //selColumn = selColumn.Replace("Phone", "CustomerContacts As Phone");
        groupBy = groupBy.Replace("ItemCode", "tblProductMaster.ItemCode");
        groupBy = groupBy.Replace("Brand", "ProductDesc");
        selColumn = selColumn.Replace("Brand", "ProductDesc");
        //groupBy = groupBy.Replace("PaymentMode", "IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo)");
    }
    private bool isValidLevels()
    {
        if ((ddlFirstLvl.SelectedItem.Text != "None"))
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
        }
        if (ddlSecondLvl.SelectedItem.Text != "None")
        {
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
        }
        if (ddlThirdLvl.SelectedItem.Text != "None")
        {
            if ((ddlThirdLvl.SelectedItem.Text != "None") &&
                (ddlThirdLvl.SelectedItem.Text == ddlFourthLvl.SelectedItem.Text) ||
                (ddlThirdLvl.SelectedItem.Text == ddlFifthLvl.SelectedItem.Text) ||
                (ddlThirdLvl.SelectedItem.Text == ddlSixthLvl.SelectedItem.Text) ||
                (ddlThirdLvl.SelectedItem.Text == ddlSeventhLvl.SelectedItem.Text))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
                return false;
            }
        }
        if (ddlFourthLvl.SelectedItem.Text != "None")
        {
            if ((ddlFourthLvl.SelectedItem.Text != "None") &&
                   (ddlFourthLvl.SelectedItem.Text == ddlFifthLvl.SelectedItem.Text) ||
                   (ddlFourthLvl.SelectedItem.Text == ddlSixthLvl.SelectedItem.Text) ||
                   (ddlFourthLvl.SelectedItem.Text == ddlSeventhLvl.SelectedItem.Text))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
                return false;
            }
        }
        if (ddlFifthLvl.SelectedItem.Text != "None")
        {
            if ((ddlFifthLvl.SelectedItem.Text != "None") &&
              (ddlFifthLvl.SelectedItem.Text == ddlSixthLvl.SelectedItem.Text) ||
              (ddlFifthLvl.SelectedItem.Text == ddlSeventhLvl.SelectedItem.Text))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
                return false;
            }
        }
        if (ddlSixthLvl.SelectedItem.Text != "None")
        {
            if ((ddlSixthLvl.SelectedItem.Text != "None") &&
              (ddlSixthLvl.SelectedItem.Text == ddlSeventhLvl.SelectedItem.Text))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
                return false;
            }
        }


        if ((chkboxCategory.Checked == false) && (chkboxBrand.Checked == false) && (chkboxProductCode.Checked == false) && (chkboxProductName.Checked == false) && (chkboxInternalTransfer.Checked == false) && (chkboxBillDate.Checked == false) && (chkboxCustomer.Checked == false) && (ChkboxPaymode.Checked == false) && (chkboxPurchaseReturn.Checked == false) && (chkboxModel.Checked == false) && (ChkboxCustaddr.Checked == false) && (ChkboxCustphone.Checked == false) && (ChkboxEmpname.Checked == false) && (chkboxBillno.Checked == false) && (ddlFirstLvl.SelectedItem.Text == "None") && (ddlSecondLvl.SelectedItem.Text == "None") && (ddlThirdLvl.SelectedItem.Text == "None") && (ddlFourthLvl.SelectedItem.Text == "None") && (ddlFifthLvl.SelectedItem.Text == "None") && (ddlSixthLvl.SelectedItem.Text == "None") && (ddlSeventhLvl.SelectedItem.Text == "None") && (chkboxStock.Checked) && (!chkboxRate.Checked) && (!chkboxDiscount.Checked) && (!chkboxFreight.Checked))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Atleast One Field or one Groupby has to be Selected');", true);
            return false;
        }
        if (odlfirstlvl.SelectedItem.Text != "None")
        {
            if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
            {
                if ((odlfirstlvl.SelectedItem.Text != ddlFirstLvl.SelectedItem.Text) && (odlfirstlvl.SelectedItem.Text != ddlSecondLvl.SelectedItem.Text) && (odlfirstlvl.SelectedItem.Text != ddlThirdLvl.SelectedItem.Text) && (odlfirstlvl.SelectedItem.Text != ddlFourthLvl.SelectedItem.Text) && (odlfirstlvl.SelectedItem.Text != ddlFifthLvl.SelectedItem.Text) && (odlfirstlvl.SelectedItem.Text != ddlSixthLvl.SelectedItem.Text) && (odlfirstlvl.SelectedItem.Text != ddlSeventhLvl.SelectedItem.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
        }
        if (odlsecondlvl.SelectedItem.Text != "None")
        {
            if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
            {

                if ((odlsecondlvl.SelectedItem.Text != ddlFirstLvl.SelectedItem.Text) && (odlsecondlvl.SelectedItem.Text != ddlSecondLvl.SelectedItem.Text) && (odlsecondlvl.SelectedItem.Text != ddlThirdLvl.SelectedItem.Text) && (odlsecondlvl.SelectedItem.Text != ddlFourthLvl.SelectedItem.Text) && (odlsecondlvl.SelectedItem.Text != ddlFifthLvl.SelectedItem.Text) && (odlsecondlvl.SelectedItem.Text != ddlSixthLvl.SelectedItem.Text) && (odlsecondlvl.SelectedItem.Text != ddlSeventhLvl.SelectedItem.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
        }
        if (odlthirdlvl.SelectedItem.Text != "None")
        {
            if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
            {
                if ((odlthirdlvl.SelectedItem.Text != ddlFirstLvl.SelectedItem.Text) && (odlthirdlvl.SelectedItem.Text != ddlSecondLvl.SelectedItem.Text) && (odlthirdlvl.SelectedItem.Text != ddlThirdLvl.SelectedItem.Text) && (odlthirdlvl.SelectedItem.Text != ddlFourthLvl.SelectedItem.Text) && (odlthirdlvl.SelectedItem.Text != ddlFifthLvl.SelectedItem.Text) && (odlthirdlvl.SelectedItem.Text != ddlSixthLvl.SelectedItem.Text) && (odlthirdlvl.SelectedItem.Text != ddlSeventhLvl.SelectedItem.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
        }
        if (odlfourlvl.SelectedItem.Text != "None")
        {
            if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
            {
                if ((odlfourlvl.SelectedItem.Text != ddlFirstLvl.SelectedItem.Text) && (odlfourlvl.SelectedItem.Text != ddlSecondLvl.SelectedItem.Text) && (odlfourlvl.SelectedItem.Text != ddlThirdLvl.SelectedItem.Text) && (odlfourlvl.SelectedItem.Text != ddlFourthLvl.SelectedItem.Text) && (odlfourlvl.SelectedItem.Text != ddlFifthLvl.SelectedItem.Text) && (odlfourlvl.SelectedItem.Text != ddlSixthLvl.SelectedItem.Text) && (odlfourlvl.SelectedItem.Text != ddlSeventhLvl.SelectedItem.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
        }
        if (odlfifthlvl.SelectedItem.Text != "None")
        {
            if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
            {
                if ((odlfifthlvl.SelectedItem.Text != ddlFirstLvl.SelectedItem.Text) && (odlfifthlvl.SelectedItem.Text != ddlSecondLvl.SelectedItem.Text) && (odlfifthlvl.SelectedItem.Text != ddlThirdLvl.SelectedItem.Text) && (odlfifthlvl.SelectedItem.Text != ddlFourthLvl.SelectedItem.Text) && (odlfifthlvl.SelectedItem.Text != ddlFifthLvl.SelectedItem.Text) && (odlfifthlvl.SelectedItem.Text != ddlSixthLvl.SelectedItem.Text) && (odlfifthlvl.SelectedItem.Text != ddlSeventhLvl.SelectedItem.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
        }
        if (odlsixthlvl.SelectedItem.Text != "None")
        {
            if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
            {
                if ((odlsixthlvl.SelectedItem.Text != ddlFirstLvl.SelectedItem.Text) && (odlsixthlvl.SelectedItem.Text != ddlSecondLvl.SelectedItem.Text) && (odlsixthlvl.SelectedItem.Text != ddlThirdLvl.SelectedItem.Text) && (odlsixthlvl.SelectedItem.Text != ddlFourthLvl.SelectedItem.Text) && (odlsixthlvl.SelectedItem.Text != ddlFifthLvl.SelectedItem.Text) && (odlsixthlvl.SelectedItem.Text != ddlSixthLvl.SelectedItem.Text) && (odlsixthlvl.SelectedItem.Text != ddlSeventhLvl.SelectedItem.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
        }
        if (odlseventhlvl.SelectedItem.Text != "None")
        {
            if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
            {
                if ((odlseventhlvl.SelectedItem.Text != ddlFirstLvl.SelectedItem.Text) && (odlseventhlvl.SelectedItem.Text != ddlSecondLvl.SelectedItem.Text) && (odlseventhlvl.SelectedItem.Text != ddlThirdLvl.SelectedItem.Text) && (odlseventhlvl.SelectedItem.Text != ddlFourthLvl.SelectedItem.Text) && (odlseventhlvl.SelectedItem.Text != ddlFifthLvl.SelectedItem.Text) && (odlseventhlvl.SelectedItem.Text != ddlSixthLvl.SelectedItem.Text) && (odlseventhlvl.SelectedItem.Text != ddlSeventhLvl.SelectedItem.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
        }
        if (odlfirstlvl.SelectedItem.Text != "None")
        {
            if (odlfirstlvl.SelectedItem.Text == "ItemCode")
            {
                if (chkboxProductCode.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            //if ((odlfirstlvl.SelectedItem.Text != chkboxCategory.Text) && (odlfirstlvl.SelectedItem.Text != chkboxBrand.Text) && (odlfirstlvl.SelectedItem.Text != chkboxProductName.Text) && (odlfirstlvl.SelectedItem.Text != chkboxModel.Text) && (odlfirstlvl.SelectedItem.Text != chkboxBillDate.Text) && (odlfirstlvl.SelectedItem.Text !=ChkboxPaymode.Text))
            if ((chkboxCategory.Checked == false) && (chkboxBrand.Checked == false) && (chkboxProductName.Checked == false) && (chkboxBillDate.Checked == false) && (chkboxCustomer.Checked == false) && (ChkboxPaymode.Checked == false))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                // lblMsg.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }
        if (odlsecondlvl.SelectedItem.Text != "None")
        {
            if (odlsecondlvl.SelectedItem.Text == "ItemCode")
            {
                if (chkboxProductCode.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            // if ((odlsecondlvl.SelectedItem.Text != chkboxCategory.Text) && (odlsecondlvl.SelectedItem.Text != chkboxBrand.Text) && (odlsecondlvl.SelectedItem.Text != chkboxProductName.Text) && (odlsecondlvl.SelectedItem.Text != chkboxModel.Text))
            if ((chkboxCategory.Checked == false) && (chkboxBrand.Checked == false) && (chkboxProductName.Checked == false) && (chkboxBillDate.Checked == false) && (chkboxCustomer.Checked == false) && (ChkboxPaymode.Checked == false))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                // lblMsg.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }
        if (odlthirdlvl.SelectedItem.Text != "None")
        {
            if (odlthirdlvl.SelectedItem.Text == "ItemCode")
            {
                if (chkboxProductCode.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            //if ((odlthirdlvl.SelectedItem.Text != chkboxCategory.Text) && (odlthirdlvl.SelectedItem.Text != chkboxBrand.Text) && (odlthirdlvl.SelectedItem.Text != chkboxProductName.Text) && (odlthirdlvl.SelectedItem.Text != chkboxModel.Text))
            if ((chkboxCategory.Checked == false) && (chkboxBrand.Checked == false) && (chkboxProductName.Checked == false) && (chkboxBillDate.Checked == false) && (chkboxCustomer.Checked == false) && (ChkboxPaymode.Checked == false))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                // lblMsg.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }
        if (odlfourlvl.SelectedItem.Text != "None")
        {
            if (odlfourlvl.SelectedItem.Text == "ItemCode")
            {
                if (chkboxProductCode.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }

            // if ((odlfourlvl.SelectedItem.Text != chkboxCategory.Text) && (odlfourlvl.SelectedItem.Text != chkboxBrand.Text) && (odlfourlvl.SelectedItem.Text != chkboxProductName.Text) && (odlfourlvl.SelectedItem.Text != chkboxModel.Text))
            if ((chkboxCategory.Checked == false) && (chkboxBrand.Checked == false) && (chkboxProductName.Checked == false) && (chkboxBillDate.Checked == false) && (chkboxCustomer.Checked == false) && (ChkboxPaymode.Checked == false))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                // lblMsg.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }
        if (odlfifthlvl.SelectedItem.Text != "None")
        {
            if (odlfifthlvl.SelectedItem.Text == "ItemCode")
            {
                if (chkboxProductCode.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            if ((chkboxCategory.Checked == false) && (chkboxBrand.Checked == false) && (chkboxProductName.Checked == false) && (chkboxBillDate.Checked == false) && (chkboxCustomer.Checked == false) && (ChkboxPaymode.Checked == false))
            //if ((odlfifthlvl.SelectedItem.Text != chkboxCategory.Text) && (odlfifthlvl.SelectedItem.Text != chkboxBrand.Text) && (odlfifthlvl.SelectedItem.Text != chkboxProductName.Text) && (odlfifthlvl.SelectedItem.Text != chkboxModel.Text))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                // lblMsg.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }

        if (odlsixthlvl.SelectedItem.Text != "None")
        {
            if (odlsixthlvl.SelectedItem.Text == "ItemCode")
            {
                if (chkboxProductCode.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            // if ((odlsixthlvl.SelectedItem.Text != chkboxCategory.Text) && (odlsixthlvl.SelectedItem.Text != chkboxBrand.Text) && (odlsixthlvl.SelectedItem.Text != chkboxProductName.Text) && (odlsixthlvl.SelectedItem.Text != chkboxModel.Text))
            if ((chkboxCategory.Checked == false) && (chkboxBrand.Checked == false) && (chkboxProductName.Checked == false) && (chkboxBillDate.Checked == false) && (chkboxCustomer.Checked == false) && (ChkboxPaymode.Checked == false))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                // lblMsg.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }
        if (odlseventhlvl.SelectedItem.Text != "None")
        {
            if (odlseventhlvl.SelectedItem.Text == "ItemCode")
            {
                if (chkboxProductCode.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                    //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                    // lblMsg.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            //if ((odlseventhlvl.SelectedItem.Text != chkboxCategory.Text) && (odlseventhlvl.SelectedItem.Text != chkboxBrand.Text) && (odlseventhlvl.SelectedItem.Text != chkboxProductName.Text) && (odlseventhlvl.SelectedItem.Text != chkboxModel.Text))
            if ((chkboxCategory.Checked == false) && (chkboxBrand.Checked == false) && (chkboxProductName.Checked == false) && (chkboxBillDate.Checked == false) && (chkboxCustomer.Checked == false) && (ChkboxPaymode.Checked == false))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select particular field to display orderby ');", true);
                //lblMsg.Text = "Atleast one field should be selected in checkboxes";
                // lblMsg.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }
        field2 = getfield2();
        if (field2 == "")
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Please select any one on aggregate funtions like:Qty,Rate,Discount,Freight');", true);
            //lblMsg.Text = "Atleast one field should be selected in checkboxes";
            // lblMsg.ForeColor = System.Drawing.Color.Red;
            return false;
        }
        return true;
    }

    protected string odrby()
    {

        string ordrby = "";
        if (odlfirstlvl.SelectedItem.Text != "None")
        {

            ordrby += " order by " + odlfirstlvl.SelectedItem.Text + " ";
        }
        if (odlsecondlvl.SelectedItem.Text != "None")
        {
            if (ordrby == "")
            {
                ordrby += " order by";
            }
            else
            {
                ordrby += " , ";
            }
            ordrby += " " + odlsecondlvl.SelectedItem.Text + " ";
        }
        if (odlthirdlvl.SelectedItem.Text != "None")
        {
            if (ordrby == "")
            {
                ordrby += " order by";
            }
            else
            {
                ordrby += " , ";
            }
            ordrby += " " + odlthirdlvl.SelectedItem.Text + " ";
        }
        if (odlfourlvl.SelectedItem.Text != "None")
        {
            if (ordrby == "")
            {
                ordrby += " order by";
            }
            else
            {
                ordrby += " , ";
            }
            ordrby += " " + odlfourlvl.SelectedItem.Text + " ";
        }
        if (odlfifthlvl.SelectedItem.Text != "None")
        {
            if (ordrby == "")
            {
                ordrby += " order by";
            }
            else
            {
                ordrby += " , ";

            }
            ordrby += " " + odlfifthlvl.SelectedItem.Text + " ";
        }
        if (odlsixthlvl.SelectedIndex > 0)
        {
            if (ordrby == "")
            {
                ordrby += " order by";
            }
            else
            {
                ordrby += " , ";

            }
            ordrby += " " + odlsixthlvl.SelectedItem.Text + " ";
        }
        if (odlseventhlvl.SelectedItem.Text != "None")
        {
            if (ordrby == "")
            {
                ordrby += " order by";
            }
            else
            {
                ordrby += " , ";

            }
            ordrby += " " + odlseventhlvl.SelectedItem.Text + " ";
        }
        /*if (ordrby == "")
        {
            field1 = getfield();
            ordrby = field1;
        }*/
        ordrby = ordrby.Replace("Brand", "ProductDesc");
        ordrby = ordrby.Replace("CategoryID", "CategoryName");
        ordrby = ordrby.Replace("ItemCode", "tblProductMaster.ItemCode");

        return ordrby;
    }


    protected string sodrby()
    {

        string sordrby = "";
        if (firstsub.SelectedItem.Text != "None")
        {
            sordrby += " order by " + firstsub.SelectedItem.Text + " ";
        }
        if (secondsub.SelectedItem.Text != "None")
        {
            if (sordrby == "")
            {
                sordrby += " order by";
            }
            else
            {
                sordrby += " , ";
            }
            sordrby += " " + secondsub.SelectedItem.Text + " ";
        }
        if (thirdsub.SelectedItem.Text != "None")
        {
            if (sordrby == "")
            {
                sordrby += " order by";
            }
            else
            {
                sordrby += " , ";
            }
            sordrby += " " + thirdsub.SelectedItem.Text + " ";
        }
        if (foursub.SelectedItem.Text != "None")
        {
            if (sordrby == "")
            {
                sordrby += " order by";
            }
            else
            {
                sordrby += " , ";
            }
            sordrby += " " + foursub.SelectedItem.Text + " ";
        }
        if (fivesub.SelectedItem.Text != "None")
        {
            if (sordrby == "")
            {
                sordrby += " order by";
            }
            else
            {
                sordrby += " , ";

            }
            sordrby += " " + fivesub.SelectedItem.Text + " ";
        }
        sordrby = sordrby.Replace("Brand", "ProductDesc");
        sordrby = sordrby.Replace("CategoryID", "CategoryName");
        sordrby = sordrby.Replace("ItemCode", "tblProductMaster.ItemCode");

        return sordrby;
    }

    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            if (!isValidLevels())
            {
                return;
            }
            string cond = "";
            string field2 = "";
            cond = getCond();
            string field1 = "";
            field1 = getfield();
            string ordrby;
            ordrby = odrby();
            getgroupByAndselColumn();
            field2 = getfield2();

            if ((firstsub.SelectedItem.Text != "None") || (secondsub.SelectedItem.Text != "None") || (thirdsub.SelectedItem.Text != "None") || (foursub.SelectedItem.Text != "None") || (fivesub.SelectedItem.Text != "None"))
            {
                string sordrby;
                sordrby = sodrby();
                bindDataSubTot(selColumn, field2, cond, groupBy, sordrby);
            }
            else
            {
                bindData(selColumn, field2, cond, groupBy, ordrby);
            }


            /* catch
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Not Data Found');", true);
            }*/
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void bindData(string selColumn, string field2, string cond, string groupBy, string ordrby)
    {
        bool dispLastTotal = false;
        string condtion = "";
        condtion = getCond();
        getgroupByAndselColumn();

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = objBL.getSales1(selColumn, field2, condtion, groupBy, ordrby);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
            {
                if (ddlFirstLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlFirstLvl.SelectedItem.Text));
                }
                if (ddlSecondLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlSecondLvl.SelectedItem.Text));
                }
                if (ddlThirdLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlThirdLvl.SelectedItem.Text));
                }
                if (ddlFourthLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlFourthLvl.SelectedItem.Text));
                }
                if (ddlFifthLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlFifthLvl.SelectedItem.Text));
                }
                if (ddlSixthLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlSixthLvl.SelectedItem.Text));
                }
                if (ddlSeventhLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlSeventhLvl.SelectedItem.Text));
                }
                if (chkboxStock.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Qty"));
                }
                if (chkboxFreight.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Freight"));
                }
                if (chkboxDiscount.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Discount"));
                }
                if (chkboxRate.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Rate"));
                }
                /* if (selLevels.IndexOf("CustomerID") < 0)
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
                     dt.Columns.Add(new DataColumn("ItemCode"));*/

                //dt.Columns.Add(new DataColumn("Model"));
                //dt.Columns.Add(new DataColumn("BillNo"));
                //  dt.Columns.Add(new DataColumn("Internaltransfer"));
                // dt.Columns.Add(new DataColumn("purchaseReturn"));
                //dt.Columns.Add(new DataColumn("CustomerAddress"));
                //dt.Columns.Add(new DataColumn("CustomerContacts"));
                //dt.Columns.Add(new DataColumn("empFirstName"));
                //dt.Columns.Add(new DataColumn("Discount"));
                //dt.Columns.Add(new DataColumn("Freight"));
                //dt.Columns.Add(new DataColumn("Qty"));
                //dt.Columns.Add(new DataColumn("Rate"));
                dt.Columns.Add(new DataColumn("Amount"));
            }
            else if ((chkboxCategory.Checked == true) || (chkboxBrand.Checked == true) || (chkboxProductCode.Checked == true) || (chkboxProductName.Checked == true) || (chkboxInternalTransfer.Checked == true) || (chkboxBillDate.Checked == true) || (chkboxCustomer.Checked == true) || (ChkboxPaymode.Checked == true) || (chkboxPurchaseReturn.Checked == true) || (chkboxModel.Checked == true) || (ChkboxCustaddr.Checked == true) || (ChkboxCustphone.Checked == true) || (ChkboxEmpname.Checked == true) || (chkboxBillno.Checked == true) || (chkboxStock.Checked == true) || (chkboxRate.Checked == true) || (chkboxDiscount.Checked == true) || (chkboxFreight.Checked == true))
            {

                if (chkboxCategory.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CategoryName"));
                }
                if (chkboxProductName.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("ProductName"));
                }
                if (chkboxBrand.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Brand"));
                }

                if (chkboxModel.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Model"));
                }
                if (chkboxProductCode.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("ItemCode"));
                }
                if (chkboxBillDate.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("BillDate"));
                }
                if (chkboxCustomer.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CustomerName"));
                }
                if (ChkboxPaymode.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Paymode"));
                }
                if (chkboxBillno.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("BillNo"));
                }
                if (chkboxPurchaseReturn.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("PurchaseReturn"));
                }
                if (chkboxInternalTransfer.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("InternalTransfer"));
                }



                if (ChkboxCustaddr.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CustomerAddress"));
                }
                if (ChkboxCustphone.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CustomerContacts"));
                }
                if (ChkboxEmpname.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Executive"));
                }
                // dt.Columns.Add(new DataColumn("Discount"));
                // dt.Columns.Add(new DataColumn("Freight"));
                //dt.Columns.Add(new DataColumn("Qty"));
                //dt.Columns.Add(new DataColumn("Rate"));
                if (chkboxStock.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Qty"));
                }
                if (chkboxFreight.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Freight"));
                }
                if (chkboxDiscount.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Discount"));
                }
                if (chkboxRate.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Rate"));
                }
                dt.Columns.Add(new DataColumn("Amount"));

            }


            DataRow dr_final99 = dt.NewRow();
            dt.Rows.Add(dr_final99);

            //initialize column values for entire row
            string fLvlValue = "", sLvlValue = "", tLvlValue = "", frthLvlValue = "", fifLvlValue = "", sixLvlValue = "", svthLvlValue = "", eightLvlValue = "", ninthLvlValue = "", tenthLvlValue = "", eleventhLvlValue = "", twelthLvlValue = "", thirteenLvlValue = "", fourteenLvlValue = "";
            string fLvlValueTemp = "", sLvlValueTemp = "", tLvlValueTemp = "", frthLvlValueTemp = "", fifLvlValueTemp = "", sixLvlValueTemp = "", svthLvlValueTemp = "", eightLvlValueTemp = "", ninthLvlValueTemp = "", tenthLvlValueTemp = "", eleventhLvlValueTemp = "", twelthLvlValueTemp = "", thirteenLvlValueTemp = "", fourteenLvlValueTemp = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
                {
                    //initialize column values for entire row
                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFirstLvl.SelectedItem.Text == "Brand")
                        {
                            fLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            fLvlValueTemp = dr[ddlFirstLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    }
                    if (ddlSecondLvl.SelectedItem.Text != "None")
                        if (ddlSecondLvl.SelectedItem.Text == "Brand")
                        {
                            sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            sLvlValueTemp = dr[ddlSecondLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlThirdLvl.SelectedItem.Text != "None")
                        if (ddlThirdLvl.SelectedItem.Text == "Brand")
                        {
                            tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            tLvlValueTemp = dr[ddlThirdLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlFourthLvl.SelectedItem.Text != "None")
                        if (ddlFourthLvl.SelectedItem.Text == "Brand")
                        {
                            frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            frthLvlValueTemp = dr[ddlFourthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlFifthLvl.SelectedItem.Text != "None")
                        if (ddlFifthLvl.SelectedItem.Text == "Brand")
                        {
                            fifLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            fifLvlValueTemp = dr[ddlFifthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlSixthLvl.SelectedItem.Text != "None")
                        if (ddlSixthLvl.SelectedItem.Text == "Brand")
                        {
                            sixLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            sixLvlValueTemp = dr[ddlSixthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                        if (ddlSeventhLvl.SelectedItem.Text == "Brand")
                        {
                            svthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            svthLvlValueTemp = dr[ddlSeventhLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }

                    dispLastTotal = true;

                    //if (ddlSeventhLvl.SelectedItem.Text != "None")
                    //{
                    //    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    //        (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                    //        (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                    //        (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                    //        (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp) ||
                    //        (sixLvlValue != "" && sixLvlValue != sixLvlValueTemp) ||
                    //        (svthLvlValue != "" && svthLvlValue != svthLvlValueTemp)||(eightLvlValueTemp !="" && eightLvlValue!=eightLvlValueTemp))
                    //    {
                    //        DataRow dr_final888 = dt.NewRow();
                    //        if (ddlFirstLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final888[ddlFirstLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSecondLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final888[ddlSecondLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlThirdLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final888[ddlThirdLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFourthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final888[ddlFourthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFifthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final888[ddlFifthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSixthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final888[ddlSixthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSeventhLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final888[ddlSeventhLvl.SelectedItem.Text] = "Total:" + svthLvlValue;
                    //        }
                    //      /*  if (selLevels.IndexOf("CustomerID") < 0)
                    //            dr_final7["CustomerID"] = "";
                    //        if (selLevels.IndexOf("BillDate") < 0)
                    //            dr_final7["BillDate"] = "";
                    //        if (selLevels.IndexOf("PayMode") < 0)
                    //            dr_final7["PayMode"] = "";
                    //        if (selLevels.IndexOf("ProductDesc") < 0)
                    //            dr_final7["ProductDesc"] = "";
                    //        if (selLevels.IndexOf("CategoryID") < 0)
                    //            dr_final7["CategoryID"] = "";
                    //        if (selLevels.IndexOf("ProductName") < 0)
                    //            dr_final7["ProductName"] = "";
                    //        if (selLevels.IndexOf("ItemCode") < 0)
                    //            dr_final7["ItemCode"] = "";*/

                    //        //dr_final7["Model"] = "";
                    //        //dr_final7["BillNo"] = "";
                    //        //dr_final7["Internaltransfer"] = "";
                    //        //dr_final7["purchaseReturn"] = "";
                    //        //dr_final7["CustomerAddress"] = "";
                    //        //dr_final7["CustomerContacts"] = "";
                    //        //dr_final7["empFirstName"] = "";
                    //        if (chkboxDiscount.Checked == true)
                    //        {
                    //            dr_final888["Discount"] = "";
                    //        }
                    //        if (chkboxFreight.Checked == true)
                    //        {
                    //            dr_final888["Freight"] = "";
                    //        }
                    //        if (chkboxStock.Checked == true)
                    //        {
                    //            dr_final888["Qty"] = "";
                    //        }

                    //        dr_final888["Amount"] = "";
                    //        if (chkboxRate.Checked == true)
                    //        {
                    //            dr_final888["Rate"] = Convert.ToString(Convert.ToDecimal(Pttls));
                    //        }
                    //        dt.Rows.Add(dr_final888);
                    //        Pttls = 0;
                    //    }
                    //}

                    //if (ddlSixthLvl.SelectedItem.Text != "None")
                    //{
                    //    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    //        (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                    //        (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                    //        (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                    //        (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp) ||
                    //        (sixLvlValue != "" && sixLvlValue != sixLvlValueTemp))
                    //    {
                    //        DataRow dr_final7 = dt.NewRow();
                    //        if (ddlFirstLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSecondLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlThirdLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFourthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFifthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSixthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSixthLvl.SelectedItem.Text] = "Total:" + sixLvlValue;
                    //        }
                    //        if (ddlSeventhLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                    //        }
                    //      /*  if (selLevels.IndexOf("CustomerID") < 0)
                    //            dr_final7["CustomerID"] = "";
                    //        if (selLevels.IndexOf("BillDate") < 0)
                    //            dr_final7["BillDate"] = "";
                    //        if (selLevels.IndexOf("PayMode") < 0)
                    //            dr_final7["PayMode"] = "";
                    //        if (selLevels.IndexOf("ProductDesc") < 0)
                    //            dr_final7["ProductDesc"] = "";
                    //        if (selLevels.IndexOf("CategoryID") < 0)
                    //            dr_final7["CategoryID"] = "";
                    //        if (selLevels.IndexOf("ProductName") < 0)
                    //            dr_final7["ProductName"] = "";
                    //        if (selLevels.IndexOf("ItemCode") < 0)
                    //            dr_final7["ItemCode"] = "";*/

                    //        //dr_final7["Model"] = "";
                    //        //dr_final7["BillNo"] = "";
                    //        //dr_final7["Internaltransfer"] = "";
                    //        //dr_final7["purchaseReturn"] = "";
                    //        //dr_final7["CustomerAddress"] = "";
                    //        //dr_final7["CustomerContacts"] = "";
                    //        //dr_final7["empFirstName"] = "";
                    //        /*dr_final7["Discount"] = "";
                    //        dr_final7["Freight"] = "";
                    //        dr_final7["Qty"] = "";
                    //        dr_final7["Amount"] = "";
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));*/
                    //        if (chkboxDiscount.Checked == true)
                    //        {
                    //            dr_final7["Discount"] = "";
                    //        }
                    //        if (chkboxFreight.Checked == true)
                    //        {
                    //            dr_final7["Freight"] = "";
                    //        }
                    //        if (chkboxStock.Checked == true)
                    //        {
                    //            dr_final7["Qty"] = "";
                    //        }

                    //        dr_final7["Amount"] = "";
                    //        if (chkboxRate.Checked == true)
                    //        {
                    //            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                    //        }
                    //        dt.Rows.Add(dr_final7);
                    //        modelTotal = 0;
                    //    }
                    //}

                    //if (ddlFifthLvl.SelectedItem.Text != "None")
                    //{
                    //    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    //       (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                    //       (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                    //       (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                    //       (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp))
                    //    {
                    //        DataRow dr_final7 = dt.NewRow();
                    //        if (ddlFirstLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSecondLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlThirdLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFourthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFifthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFifthLvl.SelectedItem.Text] = "Total:" + fifLvlValue;
                    //        }
                    //        if (ddlSixthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSeventhLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                    //        }
                    //       /* if (selLevels.IndexOf("CustomerID") < 0)
                    //            dr_final7["CustomerID"] = "";
                    //        if (selLevels.IndexOf("BillDate") < 0)
                    //            dr_final7["BillDate"] = "";
                    //        if (selLevels.IndexOf("PayMode") < 0)
                    //            dr_final7["PayMode"] = "";
                    //        if (selLevels.IndexOf("ProductDesc") < 0)
                    //            dr_final7["ProductDesc"] = "";
                    //        if (selLevels.IndexOf("CategoryID") < 0)
                    //            dr_final7["CategoryID"] = "";
                    //        if (selLevels.IndexOf("ProductName") < 0)
                    //            dr_final7["ProductName"] = "";
                    //        if (selLevels.IndexOf("ItemCode") < 0)
                    //            dr_final7["ItemCode"] = "";*/

                    //        //dr_final7["Model"] = "";
                    //        //dr_final7["BillNo"] = "";
                    //        //dr_final7["Internaltransfer"] = "";
                    //        //dr_final7["purchaseReturn"] = "";
                    //        //dr_final7["CustomerAddress"] = "";
                    //        //dr_final7["CustomerContacts"] = "";
                    //        //dr_final7["empFirstName"] = "";
                    //        /*dr_final7["Discount"] = "";
                    //        dr_final7["Freight"] = "";
                    //        dr_final7["Qty"] = "";
                    //        dr_final7["Amount"] = "";
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));*/
                    //        if (chkboxDiscount.Checked == true)
                    //        {
                    //            dr_final7["Discount"] = "";
                    //        }
                    //        if (chkboxFreight.Checked == true)
                    //        {
                    //            dr_final7["Freight"] = "";
                    //        }
                    //        if (chkboxStock.Checked == true)
                    //        {
                    //            dr_final7["Qty"] = "";
                    //        }

                    //        dr_final7["Amount"] = "";
                    //        if (chkboxRate.Checked == true)
                    //        {
                    //            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                    //        }
                    //        dt.Rows.Add(dr_final7);
                    //        catIDTotal = 0;
                    //    }
                    //}

                    //if (ddlFourthLvl.SelectedItem.Text != "None")
                    //{
                    //    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    //       (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                    //       (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                    //       (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp))
                    //    {
                    //        DataRow dr_final7 = dt.NewRow();
                    //        if (ddlFirstLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSecondLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlThirdLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFourthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFourthLvl.SelectedItem.Text] = "Total:" + frthLvlValue;
                    //        }
                    //        if (ddlFifthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSixthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSeventhLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                    //        }
                    //       /* if (selLevels.IndexOf("CustomerID") < 0)
                    //            dr_final7["CustomerID"] = "";
                    //        if (selLevels.IndexOf("BillDate") < 0)
                    //            dr_final7["BillDate"] = "";
                    //        if (selLevels.IndexOf("PayMode") < 0)
                    //            dr_final7["PayMode"] = "";
                    //        if (selLevels.IndexOf("ProductDesc") < 0)
                    //            dr_final7["ProductDesc"] = "";
                    //        if (selLevels.IndexOf("CategoryID") < 0)
                    //            dr_final7["CategoryID"] = "";
                    //        if (selLevels.IndexOf("ProductName") < 0)
                    //            dr_final7["ProductName"] = "";
                    //        if (selLevels.IndexOf("ItemCode") < 0)
                    //            dr_final7["ItemCode"] = "";*/

                    //        //dr_final7["Model"] = "";
                    //        //dr_final7["BillNo"] = "";
                    //        //dr_final7["Internaltransfer"] = "";
                    //        //dr_final7["purchaseReturn"] = "";
                    //        //dr_final7["CustomerAddress"] = "";
                    //        //dr_final7["CustomerContacts"] = "";
                    //        //dr_final7["empFirstName"] = "";
                    //     /*   dr_final7["Discount"] = "";
                    //        dr_final7["Freight"] = "";
                    //        dr_final7["Qty"] = "";
                    //        dr_final7["Amount"] = "";
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));*/
                    //        if (chkboxDiscount.Checked == true)
                    //        {
                    //            dr_final7["Discount"] = "";
                    //        }
                    //        if (chkboxFreight.Checked == true)
                    //        {
                    //            dr_final7["Freight"] = "";
                    //        }
                    //        if (chkboxStock.Checked == true)
                    //        {
                    //            dr_final7["Qty"] = "";
                    //        }

                    //        dr_final7["Amount"] = "";
                    //        if (chkboxRate.Checked == true)
                    //        {
                    //            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                    //        }
                    //        dt.Rows.Add(dr_final7);
                    //        brandTotal = 0;
                    //    }
                    //}

                    //if (ddlThirdLvl.SelectedItem.Text != "None")
                    //{
                    //    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    //       (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                    //       (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                    //    {
                    //        DataRow dr_final7 = dt.NewRow();
                    //        if (ddlFirstLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSecondLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlThirdLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlThirdLvl.SelectedItem.Text] = "Total:" + tLvlValue;
                    //        }
                    //        if (ddlFourthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFifthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSixthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSeventhLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                    //        }
                    //       /* if (selLevels.IndexOf("CustomerID") < 0)
                    //            dr_final7["CustomerID"] = "";
                    //        if (selLevels.IndexOf("BillDate") < 0)
                    //            dr_final7["BillDate"] = "";
                    //        if (selLevels.IndexOf("PayMode") < 0)
                    //            dr_final7["PayMode"] = "";
                    //        if (selLevels.IndexOf("ProductDesc") < 0)
                    //            dr_final7["ProductDesc"] = "";
                    //        if (selLevels.IndexOf("CategoryID") < 0)
                    //            dr_final7["CategoryID"] = "";
                    //        if (selLevels.IndexOf("ProductName") < 0)
                    //            dr_final7["ProductName"] = "";
                    //        if (selLevels.IndexOf("ItemCode") < 0)
                    //            dr_final7["ItemCode"] = "";*/

                    //       // dr_final7["Model"] = "";
                    //        //dr_final7["BillNo"] = "";
                    //        //dr_final7["Internaltransfer"] = "";
                    //        //dr_final7["purchaseReturn"] = "";
                    //        //dr_final7["CustomerAddress"] = "";
                    //        //dr_final7["CustomerContacts"] = "";
                    //        //dr_final7["empFirstName"] = "";
                    //      /*  dr_final7["Discount"] = "";
                    //        dr_final7["Freight"] = "";
                    //        dr_final7["Qty"] = "";
                    //        dr_final7["Amount"] = "";
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));*/
                    //        if (chkboxDiscount.Checked == true)
                    //        {
                    //            dr_final7["Discount"] = "";
                    //        }
                    //        if (chkboxFreight.Checked == true)
                    //        {
                    //            dr_final7["Freight"] = "";
                    //        }
                    //        if (chkboxStock.Checked == true)
                    //        {
                    //            dr_final7["Qty"] = "";
                    //        }

                    //        dr_final7["Amount"] = "";
                    //        if (chkboxRate.Checked == true)
                    //        {
                    //            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));
                    //        }
                    //        dt.Rows.Add(dr_final7);
                    //        brandTotal1 = 0;
                    //    }
                    //}

                    //if (ddlSecondLvl.SelectedItem.Text != "None")
                    //{
                    //    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                    //       (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    //    {
                    //        DataRow dr_final7 = dt.NewRow();
                    //        if (ddlFirstLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSecondLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSecondLvl.SelectedItem.Text] = "Total:" + sLvlValue;
                    //        }
                    //        if (ddlThirdLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFourthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFifthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSixthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSeventhLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                    //        }
                    //       /* if (selLevels.IndexOf("CustomerID") < 0)
                    //            dr_final7["CustomerID"] = "";
                    //        if (selLevels.IndexOf("BillDate") < 0)
                    //            dr_final7["BillDate"] = "";
                    //        if (selLevels.IndexOf("PayMode") < 0)
                    //            dr_final7["PayMode"] = "";
                    //        if (selLevels.IndexOf("ProductDesc") < 0)
                    //            dr_final7["ProductDesc"] = "";
                    //        if (selLevels.IndexOf("CategoryID") < 0)
                    //            dr_final7["CategoryID"] = "";
                    //        if (selLevels.IndexOf("ProductName") < 0)
                    //            dr_final7["ProductName"] = "";
                    //        if (selLevels.IndexOf("ItemCode") < 0)
                    //            dr_final7["ItemCode"] = "";*/

                    //        //dr_final7["Model"] = "";
                    //        //dr_final7["BillNo"] = "";
                    //        //dr_final7["Internaltransfer"] = "";
                    //        //dr_final7["purchaseReturn"] = "";
                    //        //dr_final7["CustomerAddress"] = "";
                    //        //dr_final7["CustomerContacts"] = "";
                    //        //dr_final7["empFirstName"] = "";
                    //       /* dr_final7["Discount"] = "";
                    //        dr_final7["Freight"] = "";
                    //        dr_final7["Qty"] = "";
                    //        dr_final7["Amount"] = "";
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));*/
                    //        if (chkboxDiscount.Checked == true)
                    //        {
                    //            dr_final7["Discount"] = "";
                    //        }
                    //        if (chkboxFreight.Checked == true)
                    //        {
                    //            dr_final7["Freight"] = "";
                    //        }
                    //        if (chkboxStock.Checked == true)
                    //        {
                    //            dr_final7["Qty"] = "";
                    //        }

                    //        dr_final7["Amount"] = "";
                    //        if (chkboxRate.Checked == true)
                    //        {
                    //            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));
                    //        }
                    //        dt.Rows.Add(dr_final7);
                    //        brandTotal2 = 0;
                    //    }
                    //}

                    //if (ddlFirstLvl.SelectedItem.Text != "None")
                    //{
                    //    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp))
                    //    {
                    //        DataRow dr_final7 = dt.NewRow();
                    //        if (ddlFirstLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFirstLvl.SelectedItem.Text] = "Total:" + fLvlValue;
                    //        }
                    //        if (ddlSecondLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlThirdLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFourthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlFifthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSixthLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                    //        }
                    //        if (ddlSeventhLvl.SelectedItem.Text != "None")
                    //        {
                    //            dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                    //        }
                    //        /*if (selLevels.IndexOf("CustomerID") < 0)
                    //            dr_final7["CustomerID"] = "";*/
                    //        /*if (selLevels.IndexOf("BillDate") < 0)
                    //            dr_final7["BillDate"] = "";
                    //        if (selLevels.IndexOf("PayMode") < 0)
                    //            dr_final7["PayMode"] = "";
                    //        if (selLevels.IndexOf("ProductDesc") < 0)
                    //            dr_final7["ProductDesc"] = "";
                    //        if (selLevels.IndexOf("CategoryID") < 0)
                    //            dr_final7["CategoryID"] = "";
                    //        if (selLevels.IndexOf("ProductName") < 0)
                    //            dr_final7["ProductName"] = "";
                    //        if (selLevels.IndexOf("ItemCode") < 0)
                    //            dr_final7["ItemCode"] = "";*/

                    //        //dr_final7["Model"] = "";
                    //        //dr_final7["BillNo"] = "";
                    //        //dr_final7["Internaltransfer"] = "";
                    //        //dr_final7["purchaseReturn"] = "";
                    //        //dr_final7["CustomerAddress"] = "";
                    //        //dr_final7["CustomerContacts"] = "";
                    //        //dr_final7["empFirstName"] = "";
                    //      /*  dr_final7["Discount"] = "";
                    //        dr_final7["Freight"] = "";
                    //        dr_final7["Qty"] = "";
                    //        dr_final7["Amount"] = "";
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));*/
                    //        if (chkboxDiscount.Checked == true)
                    //        {
                    //            dr_final7["Discount"] = "";
                    //        }
                    //        if (chkboxFreight.Checked == true)
                    //        {
                    //            dr_final7["Freight"] = "";
                    //        }
                    //        if (chkboxStock.Checked == true)
                    //        {
                    //            dr_final7["Qty"] = "";
                    //        }

                    //        dr_final7["Amount"] = "";
                    //        if (chkboxRate.Checked == true)
                    //        {
                    //            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));
                    //        }
                    //        dt.Rows.Add(dr_final7);
                    //        brandTotal3 = 0;
                    //    }
                    //}
                    ///////////////////////////////////////

                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;
                    frthLvlValue = frthLvlValueTemp;
                    fifLvlValue = fifLvlValueTemp;
                    sixLvlValue = sixLvlValueTemp;
                    svthLvlValue = sixLvlValueTemp;
                    DataRow dr_final5 = dt.NewRow();
                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFirstLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                    }

                    if (ddlSecondLvl.SelectedItem.Text != "None")
                    {
                        if (ddlSecondLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["ItemCode"];
                        }

                    }
                    if (ddlThirdLvl.SelectedItem.Text != "None")
                    {
                        if (ddlThirdLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlFourthLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFourthLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlFifthLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFifthLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlSixthLvl.SelectedItem.Text != "None")
                    {
                        if (ddlSixthLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                    {
                        if (ddlSeventhLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    /*  if (selLevels.IndexOf("CustomerID") < 0)
                          dr_final5["CustomerID"] = dr["CustomerID"];
                      if (selLevels.IndexOf("BillDate") < 0)
                          dr_final5["BillDate"] = dr["BillDate"];
                      if (selLevels.IndexOf("PayMode") < 0)
                          dr_final5["PayMode"] = dr["LedgerName"];
                      if (selLevels.IndexOf("ProductDesc") < 0)
                          dr_final5["ProductDesc"] = dr["ProductDesc"];
                      if (selLevels.IndexOf("CategoryID") < 0)
                          dr_final5["CategoryID"] = dr["CategoryID"];
                      if (selLevels.IndexOf("ProductName") < 0)
                          dr_final5["ProductName"] = dr["ProductName"];
                      if (selLevels.IndexOf("ItemCode") < 0)
                          dr_final5["ItemCode"] = dr["ItemCode"];*/

                    // dr_final5["Model"] = dr["Model"];
                    //dr_final5["BillNo"] = dr["BillNo"];
                    //dr_final5["Internaltransfer"] = dr["Internaltransfer"];
                    //dr_final5["purchaseReturn"] = dr["purchaseReturn"];
                    //dr_final5["CustomerAddress"] = dr["CustomerAddress"];
                    //dr_final5["CustomerContacts"] = dr["CustomerContacts"];
                    //dr_final5["empFirstName"] = dr["empFirstName"];
                    /* dr_final5["Discount"] = dr["Discount"];
                     dr_final5["Freight"] = dr["Freight"];
                     dr_final5["Qty"] = dr["Qty"];
                     dr_final5["Rate"] = dr["Rate"];*/
                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final5["Discount"] = dr["Discount"];
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final5["Freight"] = dr["Freight"];
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final5["Qty"] = dr["Qty"];
                    }

                    // dr_final1["Amount"] = "";
                    if (chkboxRate.Checked == true)
                    {
                        dr_final5["Rate"] = dr["Rate"];
                    }
                    dr_final5["Amount"] = dr["Amount"];
                    dt.Rows.Add(dr_final5);
                    if (chkboxStock.Checked == true)
                    {
                        Gttl = Gttl + Convert.ToInt32(dr["Qty"]);
                    }
                    if (chkboxRate.Checked == true)
                    {
                        Gtotal = Gtotal + Convert.ToDecimal(dr["Rate"]);
                        modelTotal = modelTotal + Convert.ToDecimal(dr["Rate"]);
                        catIDTotal = catIDTotal + Convert.ToDecimal(dr["Rate"]);
                        Pttls = Pttls + Convert.ToDecimal(dr["Rate"]);
                        brandTotal = brandTotal + Convert.ToDecimal(dr["Rate"]);
                        brandTotal1 = brandTotal1 + Convert.ToDecimal(dr["Rate"]);
                        brandTotal2 = brandTotal2 + Convert.ToDecimal(dr["Rate"]);
                        brandTotal3 = brandTotal3 + Convert.ToDecimal(dr["Rate"]);
                    }
                    brandTotal4 = brandTotal4 + Convert.ToDecimal(dr["Amount"]);

                }
                else if ((chkboxCategory.Checked == true) || (chkboxBrand.Checked == true) || (chkboxProductCode.Checked == true) || (chkboxProductName.Checked == true) || (chkboxInternalTransfer.Checked == true) || (chkboxBillDate.Checked == true) || (chkboxCustomer.Checked == true) || (ChkboxPaymode.Checked == true) || (chkboxPurchaseReturn.Checked == true) || (chkboxModel.Checked == true) || (ChkboxCustaddr.Checked == true) || (ChkboxCustphone.Checked == true) || (ChkboxEmpname.Checked == true) || (chkboxBillno.Checked == true) || (chkboxStock.Checked == true) || (chkboxRate.Checked == true) || (chkboxDiscount.Checked == true) || (chkboxFreight.Checked == true))
                {

                    if (chkboxCategory.Checked == true)
                        fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                    if (chkboxCustomer.Checked == true)
                        svthLvlValueTemp = dr["CustomerName"].ToString().ToUpper().Trim();
                    if (chkboxBillDate.Checked == true)
                        tLvlValueTemp = dr["BillDate"].ToString().ToUpper().Trim();
                    if (chkboxBrand.Checked == true)
                        frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                    if (chkboxProductCode.Checked == true)
                        fifLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                    if (chkboxPurchaseReturn.Checked == true)
                        sixLvlValueTemp = dr["PurchaseReturn"].ToString().ToUpper().Trim();

                    if (chkboxInternalTransfer.Checked == true)
                        svthLvlValueTemp = dr["InternalTransfer"].ToString().ToUpper().Trim();
                    if (ChkboxPaymode.Checked == true)


                        eightLvlValueTemp = dr["Paymode"].ToString().ToUpper().Trim();
                    if (chkboxProductName.Checked == true)
                        ninthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                    if (chkboxBillno.Checked == true)
                        tenthLvlValueTemp = dr["Billno"].ToString().ToUpper().Trim();
                    if (chkboxModel.Checked == true)
                        eleventhLvlValueTemp = dr["Model"].ToString().ToUpper().ToUpper().Trim();
                    if (ChkboxCustaddr.Checked == true)
                        twelthLvlValueTemp = dr["Add1"].ToString().ToUpper().Trim();
                    if (ChkboxCustphone.Checked == true)
                        thirteenLvlValueTemp = dr["CustomerContacts"].ToString().ToUpper().Trim();
                    if (ChkboxEmpname.Checked == true)
                        fourteenLvlValueTemp = dr["executivename"].ToString().ToUpper().Trim();

                    dispLastTotal = true;





                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;
                    frthLvlValue = frthLvlValueTemp;
                    fifLvlValue = fifLvlValueTemp;
                    sixLvlValue = sixLvlValueTemp;
                    svthLvlValue = svthLvlValueTemp;
                    eightLvlValue = eightLvlValueTemp;
                    ninthLvlValue = ninthLvlValueTemp;
                    tenthLvlValue = tenthLvlValueTemp;
                    eleventhLvlValue = eleventhLvlValueTemp;
                    twelthLvlValue = twelthLvlValueTemp;
                    thirteenLvlValue = thirteenLvlValueTemp;
                    fourteenLvlValue = fourteenLvlValueTemp;



                    DataRow dr_final5 = dt.NewRow();
                    if (chkboxCategory.Checked == true)
                    {

                        dr_final5["CategoryName"] = dr["CategoryName"];
                    }

                    if (chkboxProductName.Checked == true)
                    {
                        dr_final5["ProductName"] = dr["ProductName"];

                    }

                    if (chkboxBrand.Checked == true)
                    {
                        dr_final5["Brand"] = dr["ProductDesc"];
                    }

                    if (chkboxModel.Checked == true)
                    {
                        dr_final5["Model"] = dr["Model"];

                    }
                    if (chkboxProductCode.Checked == true)
                    {
                        dr_final5["ItemCode"] = dr["ItemCode"];
                    }
                    if (chkboxBillDate.Checked == true)
                    {
                        dr_final5["BillDate"] = dr["BillDate"];
                    }
                    if (chkboxCustomer.Checked == true)
                    {
                        dr_final5["CustomerName"] = dr["CustomerName"];
                    }
                    if (ChkboxPaymode.Checked == true)
                    {

                        if (eightLvlValueTemp == "1")
                        {
                            dr_final5["Paymode"] = "Cash";
                        }
                        else if (eightLvlValueTemp == "2")
                        {
                            dr_final5["Paymode"] = "Bank/Credit";
                        }
                        else if (eightLvlValueTemp == "3")
                        {
                            dr_final5["Paymode"] = "Credit";
                        }

                        //dr_final5["Paymode"] = dr["Paymode"];
                    }
                    if (chkboxBillno.Checked == true)
                    {
                        dr_final5["Billno"] = dr["Billno"];

                    }
                    if (chkboxPurchaseReturn.Checked == true)
                    {
                        dr_final5["PurchaseReturn"] = dr["PurchaseReturn"];
                    }
                    if (chkboxInternalTransfer.Checked == true)
                    {
                        dr_final5["InternalTransfer"] = dr["InternalTransfer"];
                    }

                    //if (chkboxProductName.Checked == true)
                    //{
                    //    dr_final5["ProductName"] = dr["ProductName"];

                    //}


                    if (ChkboxCustaddr.Checked == true)
                    {
                        dr_final5["CustomerAddress"] = dr["Add1"];

                    }
                    if (ChkboxCustphone.Checked == true)
                    {
                        dr_final5["CustomerContacts"] = dr["CustomerContacts"];

                    }
                    if (ChkboxEmpname.Checked == true)
                    {
                        dr_final5["Executive"] = dr["executivename"];

                    }

                    /*if (selLevels.IndexOf("CustomerID") < 0)
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
                        dr_final5["ItemCode"] = dr["ItemCode"];*/

                    //dr_final5["Model"] = dr["Model"];
                    //dr_final5["BillNo"] = dr["BillNo"];
                    //dr_final5["Internaltransfer"] = dr["Internaltransfer"];
                    // dr_final5["purchaseReturn"] = dr["purchaseReturn"];
                    //dr_final5["CustomerAddress"] = dr["CustomerAddress"];
                    //dr_final5["CustomerContacts"] = dr["CustomerContacts"];
                    // dr_final5["empFirstName"] = dr["empFirstName"];
                    /* dr_final5["Discount"] = dr["Discount"];
                     dr_final5["Freight"] = dr["Freight"];
                     dr_final5["Qty"] = dr["Qty"];
                     dr_final5["Rate"] = dr["Rate"];*/
                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final5["Discount"] = dr["Discount"];
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final5["Freight"] = dr["Freight"];
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final5["Qty"] = dr["Qty"];
                    }

                    // dr_final1["Amount"] = "";
                    if (chkboxRate.Checked == true)
                    {
                        dr_final5["Rate"] = dr["Rate"];
                    }
                    dr_final5["Amount"] = dr["Amount"];
                    dt.Rows.Add(dr_final5);
                    if (chkboxStock.Checked == true)
                    {
                        Gttl = Gttl + Convert.ToInt32(dr["Qty"]);
                    }
                    if (chkboxRate.Checked == true)
                    {
                        Gtotal = Gtotal + Convert.ToDecimal(dr["Rate"]);
                        modelTotal = modelTotal + Convert.ToDecimal(dr["Rate"]);
                        catIDTotal = catIDTotal + Convert.ToDecimal(dr["Rate"]);
                        Pttls = Pttls + Convert.ToDecimal(dr["Rate"]);
                        brandTotal = brandTotal + Convert.ToDecimal(dr["Rate"]);
                        brandTotal1 = brandTotal1 + Convert.ToDecimal(dr["Rate"]);
                        brandTotal2 = brandTotal2 + Convert.ToDecimal(dr["Rate"]);
                        brandTotal3 = brandTotal3 + Convert.ToDecimal(dr["Rate"]);
                        modelTotal1 = modelTotal1 + Convert.ToDecimal(dr["Rate"]);
                    }
                    brandTotal4 = brandTotal4 + Convert.ToDecimal(dr["Amount"]);

                }
            }

            //Display the last Total and Grand Total
            if (dispLastTotal)
            {
                if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
                {
                    //if (ddlSeventhLvl.SelectedItem.Text != "None")
                    //{
                    //    DataRow dr_final7 = dt.NewRow();
                    //    if (ddlFirstLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlSecondLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlThirdLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlFourthLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlFifthLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlSixthLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                    //    }
                    //    dr_final7[ddlSeventhLvl.SelectedItem.Text] = "Total:" + svthLvlValueTemp;

                    //    /* if (selLevels.IndexOf("CustomerID") < 0)
                    //         dr_final7["CustomerID"] = "";
                    //     if (selLevels.IndexOf("BillDate") < 0)
                    //         dr_final7["BillDate"] = "";
                    //     if (selLevels.IndexOf("PayMode") < 0)
                    //         dr_final7["PayMode"] = "";
                    //     if (selLevels.IndexOf("ProductDesc") < 0)
                    //         dr_final7["ProductDesc"] = "";
                    //     if (selLevels.IndexOf("CategoryID") < 0)
                    //         dr_final7["CategoryID"] = "";
                    //     if (selLevels.IndexOf("ProductName") < 0)
                    //         dr_final7["ProductName"] = "";
                    //     if (selLevels.IndexOf("ItemCode") < 0)
                    //         dr_final7["ItemCode"] = "";*/

                    //    //dr_final7["Model"] = ""; 
                    //    //dr_final7["BillNo"] = "";
                    //    //dr_final7["Internaltransfer"] = ""; 
                    //    //dr_final7["purchaseReturn"] = "";
                    //    //dr_final7["CustomerAddress"] = "";
                    //    //dr_final7["CustomerContacts"] = "";
                    //    //dr_final7["empFirstName"] = "";
                    //    /* dr_final7["Discount"] = "";
                    //     dr_final7["Freight"] = "";
                    //     dr_final7["Qty"] = "";
                    //     dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(Pttls));*/
                    //    if (chkboxDiscount.Checked == true)
                    //    {
                    //        dr_final7["Discount"] = "";
                    //    }
                    //    if (chkboxFreight.Checked == true)
                    //    {
                    //        dr_final7["Freight"] = "";
                    //    }
                    //    if (chkboxStock.Checked == true)
                    //    {
                    //        dr_final7["Qty"] = "";
                    //    }

                    //    //  dr_final7["Amount"] = "";
                    //    if (chkboxRate.Checked == true)
                    //    {
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(Pttls));
                    //    }
                    //    dt.Rows.Add(dr_final7);
                    //    Pttls = 0;
                    //}

                    //if (ddlSixthLvl.SelectedItem.Text != "None")
                    //{
                    //    DataRow dr_final7 = dt.NewRow();
                    //    if (ddlFirstLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlSecondLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlThirdLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlFourthLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlFifthLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                    //    }
                    //    dr_final7[ddlSixthLvl.SelectedItem.Text] = "Total:" + sixLvlValueTemp;

                    //    /* if (selLevels.IndexOf("CustomerID") < 0)
                    //         dr_final7["CustomerID"] = "";
                    //     if (selLevels.IndexOf("BillDate") < 0)
                    //         dr_final7["BillDate"] = "";
                    //     if (selLevels.IndexOf("PayMode") < 0)
                    //         dr_final7["PayMode"] = "";
                    //     if (selLevels.IndexOf("ProductDesc") < 0)
                    //         dr_final7["Brand"] = "";
                    //     if (selLevels.IndexOf("CategoryID") < 0)
                    //         dr_final7["CategoryID"] = "";
                    //     if (selLevels.IndexOf("ProductName") < 0)
                    //         dr_final7["ProductName"] = "";
                    //     if (selLevels.IndexOf("ItemCode") < 0)
                    //         dr_final7["ItemCode"] = "";*/

                    //    //dr_final7["Model"] = ""; ;
                    //    //dr_final7["BillNo"] = ""; ;
                    //    //dr_final7["Internaltransfer"] = ""; ;
                    //    //dr_final7["purchaseReturn"] = "";
                    //    //dr_final7["CustomerAddress"] = "";
                    //    //dr_final7["CustomerContacts"] = "";
                    //    //dr_final7["empFirstName"] = "";
                    //    /*  dr_final7["Discount"] = "";
                    //      dr_final7["Freight"] = "";
                    //      dr_final7["Qty"] = "";
                    //      dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));*/
                    //    if (chkboxDiscount.Checked == true)
                    //    {
                    //        dr_final7["Discount"] = "";
                    //    }
                    //    if (chkboxFreight.Checked == true)
                    //    {
                    //        dr_final7["Freight"] = "";
                    //    }
                    //    if (chkboxStock.Checked == true)
                    //    {
                    //        dr_final7["Qty"] = "";
                    //    }

                    //    //   dr_final7["Amount"] = "";
                    //    if (chkboxRate.Checked == true)
                    //    {
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                    //    }
                    //    dt.Rows.Add(dr_final7);
                    //    modelTotal = 0;
                    //}

                    //if (ddlFifthLvl.SelectedItem.Text != "None")
                    //{
                    //    DataRow dr_final7 = dt.NewRow();
                    //    if (ddlFirstLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlSecondLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlThirdLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlFourthLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                    //    }
                    //    dr_final7[ddlFifthLvl.SelectedItem.Text] = "Total:" + fifLvlValueTemp;
                    //    /* if (selLevels.IndexOf("CustomerID") < 0)
                    //         dr_final7["CustomerID"] = "";
                    //     if (selLevels.IndexOf("BillDate") < 0)
                    //         dr_final7["BillDate"] = "";
                    //     if (selLevels.IndexOf("PayMode") < 0)
                    //         dr_final7["PayMode"] = "";
                    //     if (selLevels.IndexOf("ProductDesc") < 0)
                    //         dr_final7["ProductDesc"] = "";
                    //     if (selLevels.IndexOf("CategoryID") < 0)
                    //         dr_final7["CategoryID"] = "";
                    //     if (selLevels.IndexOf("ProductName") < 0)
                    //         dr_final7["ProductName"] = "";
                    //     if (selLevels.IndexOf("ItemCode") < 0)
                    //         dr_final7["ItemCode"] = "";*/

                    //    //dr_final7["Model"] = ""; ;
                    //    //dr_final7["BillNo"] = ""; ;
                    //    //dr_final7["Internaltransfer"] = ""; ;
                    //    //dr_final7["purchaseReturn"] = "";
                    //    //dr_final7["CustomerAddress"] = "";
                    //    //dr_final7["CustomerContacts"] = "";
                    //    //dr_final7["empFirstName"] = "";
                    //    /*dr_final7["Discount"] = "";
                    //    dr_final7["Freight"] = "";
                    //    dr_final7["Qty"] = "";
                    //    dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));*/
                    //    if (chkboxDiscount.Checked == true)
                    //    {
                    //        dr_final7["Discount"] = "";
                    //    }
                    //    if (chkboxFreight.Checked == true)
                    //    {
                    //        dr_final7["Freight"] = "";
                    //    }
                    //    if (chkboxStock.Checked == true)
                    //    {
                    //        dr_final7["Qty"] = "";
                    //    }

                    //    //   dr_final7["Amount"] = "";
                    //    if (chkboxRate.Checked == true)
                    //    {
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                    //    }
                    //    dt.Rows.Add(dr_final7);
                    //    catIDTotal = 0;
                    //}

                    //if (ddlFourthLvl.SelectedItem.Text != "None")
                    //{
                    //    DataRow dr_final7 = dt.NewRow();
                    //    if (ddlFirstLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlSecondLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlThirdLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                    //    }
                    //    dr_final7[ddlFourthLvl.SelectedItem.Text] = "Total:" + frthLvlValueTemp;

                    //    /* if (selLevels.IndexOf("CustomerID") < 0)
                    //         dr_final7["CustomerID"] = "";
                    //     if (selLevels.IndexOf("BillDate") < 0)
                    //         dr_final7["BillDate"] = "";
                    //     if (selLevels.IndexOf("PayMode") < 0)
                    //         dr_final7["PayMode"] = "";
                    //     if (selLevels.IndexOf("ProductDesc") < 0)
                    //         dr_final7["ProductDesc"] = "";
                    //     if (selLevels.IndexOf("CategoryID") < 0)
                    //         dr_final7["CategoryID"] = "";
                    //     if (selLevels.IndexOf("ProductName") < 0)
                    //         dr_final7["ProductName"] = "";
                    //     if (selLevels.IndexOf("ItemCode") < 0)
                    //         dr_final7["ItemCode"] = "";*/

                    //    //dr_final7["Model"] = ""; ;
                    //    //dr_final7["BillNo"] = ""; ;
                    //    //dr_final7["Internaltransfer"] = ""; ;
                    //    //dr_final7["purchaseReturn"] = "";
                    //    //dr_final7["CustomerAddress"] = "";
                    //    //dr_final7["CustomerContacts"] = "";
                    //    //dr_final7["empFirstName"] = "";
                    //    /* dr_final7["Discount"] = "";
                    //     dr_final7["Freight"] = "";
                    //     dr_final7["Qty"] = "";
                    //     dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));*/
                    //    if (chkboxDiscount.Checked == true)
                    //    {
                    //        dr_final7["Discount"] = "";
                    //    }
                    //    if (chkboxFreight.Checked == true)
                    //    {
                    //        dr_final7["Freight"] = "";
                    //    }
                    //    if (chkboxStock.Checked == true)
                    //    {
                    //        dr_final7["Qty"] = "";
                    //    }

                    //    //dr_final7["Amount"] = "";
                    //    if (chkboxRate.Checked == true)
                    //    {
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                    //    }
                    //    dt.Rows.Add(dr_final7);
                    //    brandTotal = 0;
                    //}

                    //if (ddlThirdLvl.SelectedItem.Text != "None")
                    //{
                    //    DataRow dr_final7 = dt.NewRow();
                    //    if (ddlFirstLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    //    }
                    //    if (ddlSecondLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                    //    }
                    //    dr_final7[ddlThirdLvl.SelectedItem.Text] = "Total:" + tLvlValueTemp;
                    //    /*if (selLevels.IndexOf("CustomerID") < 0)
                    //        dr_final7["CustomerID"] = "";
                    //    if (selLevels.IndexOf("BillDate") < 0)
                    //        dr_final7["BillDate"] = "";
                    //    if (selLevels.IndexOf("PayMode") < 0)
                    //        dr_final7["PayMode"] = "";
                    //    if (selLevels.IndexOf("ProductDesc") < 0)
                    //        dr_final7["ProductDesc"] = "";
                    //    if (selLevels.IndexOf("CategoryID") < 0)
                    //        dr_final7["CategoryID"] = "";
                    //    if (selLevels.IndexOf("ProductName") < 0)
                    //        dr_final7["ProductName"] = "";
                    //    if (selLevels.IndexOf("ItemCode") < 0)
                    //        dr_final7["ItemCode"] = "";*/

                    //    //dr_final7["Model"] = ""; ;
                    //    //dr_final7["BillNo"] = ""; ;
                    //    //dr_final7["Internaltransfer"] = ""; ;
                    //    //dr_final7["purchaseReturn"] = "";
                    //    //dr_final7["CustomerAddress"] = "";
                    //    //dr_final7["CustomerContacts"] = "";
                    //    //dr_final7["empFirstName"] = "";
                    //    /*  dr_final7["Discount"] = "";
                    //      dr_final7["Freight"] = "";
                    //      dr_final7["Qty"] = "";
                    //      dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));*/
                    //    if (chkboxDiscount.Checked == true)
                    //    {
                    //        dr_final7["Discount"] = "";
                    //    }
                    //    if (chkboxFreight.Checked == true)
                    //    {
                    //        dr_final7["Freight"] = "";
                    //    }
                    //    if (chkboxStock.Checked == true)
                    //    {
                    //        dr_final7["Qty"] = "";
                    //    }

                    //    //    dr_final7["Amount"] = "";
                    //    if (chkboxRate.Checked == true)
                    //    {
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));
                    //    }
                    //    dt.Rows.Add(dr_final7);
                    //    brandTotal1 = 0;
                    //}

                    //if (ddlSecondLvl.SelectedItem.Text != "None")
                    //{
                    //    DataRow dr_final7 = dt.NewRow();
                    //    if (ddlFirstLvl.SelectedItem.Text != "None")
                    //    {
                    //        dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                    //    }
                    //    dr_final7[ddlSecondLvl.SelectedItem.Text] = "Total:" + sLvlValueTemp; ;
                    //    /*  if (selLevels.IndexOf("CustomerID") < 0)
                    //          dr_final7["CustomerID"] = "";
                    //      if (selLevels.IndexOf("BillDate") < 0)
                    //          dr_final7["BillDate"] = "";
                    //      if (selLevels.IndexOf("PayMode") < 0)
                    //          dr_final7["PayMode"] = "";
                    //      if (selLevels.IndexOf("ProductDesc") < 0)
                    //          dr_final7["ProductDesc"] = "";
                    //      if (selLevels.IndexOf("CategoryID") < 0)
                    //          dr_final7["CategoryID"] = "";
                    //      if (selLevels.IndexOf("ProductName") < 0)
                    //          dr_final7["ProductName"] = "";
                    //      if (selLevels.IndexOf("ItemCode") < 0)
                    //          dr_final7["ItemCode"] = "";*/

                    //    //dr_final7["Model"] = ""; ;
                    //    //dr_final7["BillNo"] = ""; ;
                    //    //dr_final7["Internaltransfer"] = ""; ;
                    //    //dr_final7["purchaseReturn"] = "";
                    //    //dr_final7["CustomerAddress"] = "";
                    //    //dr_final7["CustomerContacts"] = "";
                    //    //dr_final7["empFirstName"] = "";
                    //    /*dr_final7["Discount"] = "";
                    //    dr_final7["Freight"] = "";
                    //    dr_final7["Qty"] = "";
                    //    dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));*/
                    //    if (chkboxDiscount.Checked == true)
                    //    {
                    //        dr_final7["Discount"] = "";
                    //    }
                    //    if (chkboxFreight.Checked == true)
                    //    {
                    //        dr_final7["Freight"] = "";
                    //    }
                    //    if (chkboxStock.Checked == true)
                    //    {
                    //        dr_final7["Qty"] = "";
                    //    }

                    //    //    dr_final7["Amount"] = "";
                    //    if (chkboxRate.Checked == true)
                    //    {
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));
                    //    }
                    //    dt.Rows.Add(dr_final7);
                    //    brandTotal2 = 0;
                    //}
                    //if (ddlFirstLvl.SelectedItem.Text != "None")
                    //{
                    //    DataRow dr_final7 = dt.NewRow();
                    //    dr_final7[ddlFirstLvl.SelectedItem.Text] = "Total:" + fLvlValueTemp;
                    //    /* if (selLevels.IndexOf("CustomerID") < 0)
                    //         dr_final7["CustomerID"] = "";
                    //     if (selLevels.IndexOf("BillDate") < 0)
                    //         dr_final7["BillDate"] = "";
                    //     if (selLevels.IndexOf("PayMode") < 0)
                    //         dr_final7["PayMode"] = "";
                    //     if (selLevels.IndexOf("ProductDesc") < 0)
                    //         dr_final7["ProductDesc"] = "";
                    //     if (selLevels.IndexOf("CategoryID") < 0)
                    //         dr_final7["CategoryID"] = "";
                    //     if (selLevels.IndexOf("ProductName") < 0)
                    //         dr_final7["ProductName"] = "";
                    //     if (selLevels.IndexOf("ItemCode") < 0)
                    //         dr_final7["ItemCode"] = "";*/

                    //    //dr_final7["Model"] = ""; ;
                    //    //dr_final7["BillNo"] = ""; ;
                    //    //dr_final7["Internaltransfer"] = ""; ;
                    //    //dr_final7["purchaseReturn"] = "";
                    //    //dr_final7["CustomerAddress"] = "";
                    //    //dr_final7["CustomerContacts"] = "";
                    //    //dr_final7["empFirstName"] = "";
                    //    /*dr_final7["Discount"] = "";
                    //    dr_final7["Freight"] = "";
                    //    dr_final7["Qty"] = "";
                    //    dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));*/
                    //    if (chkboxDiscount.Checked == true)
                    //    {
                    //        dr_final7["Discount"] = "";
                    //    }
                    //    if (chkboxFreight.Checked == true)
                    //    {
                    //        dr_final7["Freight"] = "";
                    //    }
                    //    if (chkboxStock.Checked == true)
                    //    {
                    //        dr_final7["Qty"] = "";
                    //    }

                    //    //    dr_final7["Amount"] = "";
                    //    if (chkboxRate.Checked == true)
                    //    {
                    //        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));
                    //    }
                    //    dt.Rows.Add(dr_final7);
                    //    brandTotal3 = 0;
                    //}

                    DataRow dr_final66 = dt.NewRow();
                    dt.Rows.Add(dr_final66);

                    DataRow dr_final6 = dt.NewRow();
                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlFirstLvl.SelectedItem.Text] = "Grand Total: ";
                    }
                    if (ddlSecondLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlSecondLvl.SelectedItem.Text] = "";
                    }
                    if (ddlThirdLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlThirdLvl.SelectedItem.Text] = "";
                    }
                    if (ddlFourthLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlFourthLvl.SelectedItem.Text] = "";
                    }
                    if (ddlFifthLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlFifthLvl.SelectedItem.Text] = "";
                    }
                    if (ddlSixthLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlSixthLvl.SelectedItem.Text] = "";
                    }
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlSeventhLvl.SelectedItem.Text] = "";
                    }
                    /* if (selLevels.IndexOf("CustomerID") < 0)
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
                         dr_final6["ItemCode"] = "";*/

                    //dr_final6["Model"] = "Grand Total: ";
                    //dr_final6["BillNo"] = "";
                    //dr_final6["Internaltransfer"] = "";
                    //dr_final6["purchaseReturn"] = "";
                    //dr_final6["CustomerAddress"] = "";
                    //dr_final6["CustomerContacts"] = "";
                    //dr_final6["empFirstName"] = "";
                    /*dr_final6["Discount"] = "Grand Total :";
                    dr_final6["Freight"] = Convert.ToString(Convert.ToDecimal(Gttl)); ;
                    dr_final6["Qty"] = "";
                    dr_final6["Rate"] = Convert.ToString(Convert.ToDecimal(Gtotal));*/
                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final6["Discount"] = "";
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final6["Freight"] = "";
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final6["Qty"] = Convert.ToString(Convert.ToDecimal(Gttl));
                    }

                    dr_final6["Rate"] = "";

                    if (chkboxRate.Checked == true)
                    {
                        dr_final6["Amount"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                    }
                    dt.Rows.Add(dr_final6);
                }

                else if ((chkboxCategory.Checked == true) || (chkboxBrand.Checked == true) || (chkboxProductCode.Checked == true) || (chkboxProductName.Checked == true) || (chkboxInternalTransfer.Checked == true) || (chkboxBillDate.Checked == true) || (chkboxCustomer.Checked == true) || (ChkboxPaymode.Checked == true) || (chkboxPurchaseReturn.Checked == true) || (chkboxModel.Checked == true) || (ChkboxCustaddr.Checked == true) || (ChkboxCustphone.Checked == true) || (ChkboxEmpname.Checked == true) || (chkboxBillno.Checked == true))
                {

                    DataRow dr_final88 = dt.NewRow();
                    dt.Rows.Add(dr_final88);

                    DataRow dr_final6 = dt.NewRow();
                    if (chkboxCategory.Checked == true)
                    {
                        dr_final6["CategoryName"] = "Grand Total: ";
                    }
                    if (chkboxPurchaseReturn.Checked == true)
                    {
                        dr_final6["PurchaseReturn"] = "";
                    }
                    if (chkboxCustomer.Checked == true)
                    {
                        dr_final6["CustomerName"] = "";
                    }
                    if (chkboxBillDate.Checked == true)
                    {
                        dr_final6["BillDate"] = "";
                    }
                    if (chkboxBrand.Checked == true)
                    {
                        dr_final6["Brand"] = "";
                    }
                    if (chkboxProductCode.Checked == true)
                    {
                        dr_final6["ItemCode"] = "";
                    }
                    if (ChkboxPaymode.Checked == true)
                    {
                        dr_final6["Paymode"] = "";
                    }
                    if (chkboxInternalTransfer.Checked == true)
                    {
                        dr_final6["InternalTransfer"] = "";
                    }
                    if (chkboxProductName.Checked == true)
                    {
                        dr_final6["ProductName"] = "";
                    } if (chkboxBillno.Checked == true)
                    {
                        dr_final6["Billno"] = "";

                    }
                    if (chkboxModel.Checked == true)
                    {
                        dr_final6["Model"] = "";

                    }
                    if (ChkboxCustaddr.Checked == true)
                    {
                        dr_final6["CustomerAddress"] = "";

                    }
                    if (ChkboxCustphone.Checked == true)
                    {
                        dr_final6["CustomerContacts"] = "";

                    }
                    if (ChkboxEmpname.Checked == true)
                    {
                        dr_final6["Executive"] = "";

                    }
                    /*    if (selLevels.IndexOf("CustomerID") < 0)
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
                            dr_final6["ItemCode"] = "";*/

                    //dr_final6["Model"] = "Grand Total: ";
                    ////dr_final6["BillNo"] = "";
                    // dr_final6["Internaltransfer"] = "";
                    // dr_final6["purchaseReturn"] = "";
                    //dr_final6["CustomerAddress"] = "";
                    //dr_final6["CustomerContacts"] = "";
                    //dr_final6["empFirstName"] = "";
                    /*   dr_final6["Discount"] = "";
                       dr_final6["Freight"] = Convert.ToString(Convert.ToDecimal(Gttl)); ;
                       dr_final6["Qty"] = "";
                       dr_final6["Rate"] = Convert.ToString(Convert.ToDecimal(Gtotal));*/
                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final6["Discount"] = "";
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final6["Freight"] = "";
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final6["Qty"] = Convert.ToString(Convert.ToDecimal(Gttl));
                    }

                    dr_final6["Rate"] = "";
                    if (chkboxRate.Checked == true)
                    {
                        dr_final6["Amount"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                    }
                    dt.Rows.Add(dr_final6);
                }
            }
            ExportToExcel(dt);
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
    protected void chkboxAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkboxAll.Checked == true)
        {
            chkboxBrand.Checked = true;
            chkboxCategory.Checked = true;
            chkboxBillDate.Checked = true;
            ChkboxPaymode.Checked = true;
            //  chkboxItemCode.Checked = true;
            chkboxProductCode.Checked = true;
            chkboxProductName.Checked = true;
            chkboxPurchaseReturn.Checked = true;
            chkboxCustomer.Checked = true;
            chkboxInternalTransfer.Checked = true;
            chkboxBillno.Checked = true;
            ChkboxCustaddr.Checked = true;
            ChkboxCustphone.Checked = true;
            ChkboxEmpname.Checked = true;
            chkboxModel.Checked = true;
            chkboxFreight.Checked = true;
            chkboxDiscount.Checked = true;
            chkboxStock.Checked = true;
            chkboxRate.Checked = true;
        }
        else
        {
            chkboxBrand.Checked = false;
            chkboxCategory.Checked = false;
            chkboxBillDate.Checked = false;
            ChkboxPaymode.Checked = false;
            // chkboxItemCode.Checked = false;
            chkboxProductCode.Checked = false;
            chkboxProductName.Checked = false;
            chkboxPurchaseReturn.Checked = false;
            chkboxCustomer.Checked = false;
            chkboxInternalTransfer.Checked = false;
            chkboxBillno.Checked = false;
            ChkboxCustaddr.Checked = false;

            ChkboxCustphone.Checked = false;
            ChkboxEmpname.Checked = false;
            chkboxModel.Checked = false;
            chkboxFreight.Checked = false;
            chkboxDiscount.Checked = false;
            chkboxStock.Checked = false;
            chkboxRate.Checked = false;
        }
    }
    protected void clearbtn_Click(object sender, EventArgs e)
    {

        try
        {
            chkboxAll.Checked = false;
            Savetxtbox.Text = "";
            chkboxBrand.Checked = false;
            chkboxCategory.Checked = false;
            chkboxBillDate.Checked = false;
            ChkboxPaymode.Checked = false;
            // chkboxItemCode.Checked = false;
            chkboxProductCode.Checked = false;
            chkboxProductName.Checked = false;
            chkboxPurchaseReturn.Checked = false;
            chkboxCustomer.Checked = false;
            chkboxInternalTransfer.Checked = false;

            RequiredFieldValidator5.Enabled = false;
            chkboxBillno.Checked = false;
            ChkboxCustaddr.Checked = false;

            ChkboxCustphone.Checked = false;
            ChkboxEmpname.Checked = false;
            chkboxModel.Checked = false;
            chkboxFreight.Checked = false;
            chkboxDiscount.Checked = false;
            chkboxStock.Checked = false;
            chkboxRate.Checked = false;
            ddlBrand.SelectedItem.Text = "All";
            ddlCategory.SelectedItem.Text = "All";
            ddlPrdctNme.SelectedItem.Text = "All";
            ddlPrdctCode.SelectedItem.Text = "All";
            ddlPayMode.SelectedItem.Text = "All";
            DateStartformat();
            DateEndformat();
            ddlFirstLvl.SelectedItem.Text = "None";
            ddlSecondLvl.SelectedItem.Text = "None";
            ddlThirdLvl.SelectedItem.Text = "None";
            ddlFourthLvl.SelectedItem.Text = "None";
            ddlFifthLvl.SelectedItem.Text = "None";
            ddlSixthLvl.SelectedItem.Text = "None";
            ddlSeventhLvl.SelectedItem.Text = "None";
            odlfirstlvl.SelectedItem.Text = "None";
            odlsecondlvl.SelectedItem.Text = "None";
            odlthirdlvl.SelectedItem.Text = "None";
            odlfourlvl.SelectedItem.Text = "None";
            odlfifthlvl.SelectedItem.Text = "None";
            odlsixthlvl.SelectedItem.Text = "None";
            odlseventhlvl.SelectedItem.Text = "None";
            ddlretrive.SelectedItem.Text = "Select a Name";
            //ModalPopupSales.Show();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Items Cleared')", true);
            return;
        }
        catch (Exception ex)
        {
            var error = ex.Message.Replace("'", "");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Error Occured : " + @error + "');", true);
            return;
        }

    }
    protected void retrivebtn_Click(object sender, EventArgs e)
    {
        string cond = "";
        RequiredFieldValidator5.Enabled = false;
        int i = 0;
        int j = 0;
        objBL.savenames = ddlretrive.SelectedItem.Text;
        string savedname = ddlretrive.SelectedItem.Text;
        DataSet ds1 = new DataSet();
        ds1 = objBL.getsavedsales(savedname);
        //DataGrid dsg = new Datagrid();
        GridView1.DataSource = ds1;
        //ddlBrand.DataTextField = "Brand";
        //ddlBrand.DataValueField = "Brand";
        GridView1.DataBind();
        GridView1.Visible = false;
        // GridView1.EnableViewState = true;
        DataTable dts = new DataTable();
        DataRow row;
        foreach (DataTable thisTable in ds1.Tables)
        {
            // For each row, print the values of each column.
            foreach (DataRow myRow in thisTable.Rows)
            {
                //foreach(DataColumn myCol in thisTable.Columns)
                //{
                if (ds1.Tables[0].Rows[i][j].ToString() == savedname)
                {
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkCategory")
                    {
                        chkboxCategory.Checked = true;

                    }

                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkBrand")
                    {
                        chkboxBrand.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkProductname")
                    {
                        chkboxProductName.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkModel")
                    {
                        chkboxModel.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkProductcode")
                    {
                        chkboxProductCode.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkCustomer")
                    {
                        chkboxCustomer.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkCustaddr")
                    {
                        ChkboxCustaddr.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkCustphone")
                    {
                        ChkboxCustphone.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkEmpname")
                    {
                        ChkboxEmpname.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkInternaltransfer")
                    {
                        chkboxInternalTransfer.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkPaymode")
                    {
                        ChkboxPaymode.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkPurchasereturn")
                    {
                        chkboxPurchaseReturn.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkBillDate")
                    {
                        chkboxBillDate.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkBillno")
                    {
                        chkboxBillno.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkStock")
                    {
                        chkboxStock.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkDiscount")
                    {
                        chkboxDiscount.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkFreight")
                    {
                        chkboxFreight.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkRate")
                    {
                        chkboxRate.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkAll")
                    {
                        chkboxAll.Checked = true;

                    }
                    j++;

                    string sdBrand = ds1.Tables[0].Rows[i][20].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdBrand)
                    {
                        ddlBrand.SelectedItem.Text = sdBrand;
                        if (sdBrand != "All")
                        {
                            // objBL.Brands = sdBrand;
                            cond += " and ProductDesc='" + sdBrand + "' ";
                        }

                        j++;
                    }
                    string sdCat = ds1.Tables[0].Rows[i][21].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdCat)
                    {
                        ddlCategory.SelectedItem.Value = sdCat;
                        if ((sdCat != "All"))
                        {
                            //objBL.Catids = Convert.ToInt32(ddlCustNme.SelectedItem.Value);
                            cond += " and CustomerID=" + Convert.ToInt32(sdCat) + " ";

                        }

                        j++;
                    }
                    string sdProd = ds1.Tables[0].Rows[i][22].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdProd)
                    {
                        ddlPrdctNme.SelectedItem.Text = sdProd;
                        if (sdProd != "All")
                        {
                            // objBL.PrdctNmes = ddlPrdctNme.SelectedItem.Text;
                            cond += " and ProductName='" + sdProd + "' ";
                        }

                        j++;
                    }

                    string sditem = ds1.Tables[0].Rows[i][23].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sditem)
                    {
                        ddlPrdctCode.SelectedItem.Text = sditem;
                        if (sditem != "All")
                        {
                            //objBL.Productcd = ddlPrdctCode.SelectedItem.Text;
                            cond += " and tblProductMaster.ItemCode='" + sditem + "' ";
                        }

                        j++;
                    }
                    string sdpaymode = ds1.Tables[0].Rows[i][24].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdpaymode)
                    {
                        ddlPayMode.SelectedItem.Value = sdpaymode;
                        if ((sdpaymode != "All"))
                        {
                            // objBL.Stocks = Convert.ToInt32(ddlPayMode.SelectedItem.Value);
                            cond += " and PayMode=" + Convert.ToInt32(sdpaymode) + " ";
                        }

                        j++;
                    }
                    string sdCust = ds1.Tables[0].Rows[i][25].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdCust)
                    {
                        ddlCustNme.SelectedItem.Value = sdCust;
                        if ((sdCust != "All"))
                        {
                            // objBL.Catids = Convert.ToInt32(ddlCustNme.SelectedItem.Value);
                            cond += " and CustomerID=" + Convert.ToInt32(sdCust) + " ";

                        }

                        j++;
                    }
                    string sdfirstlvl = ds1.Tables[0].Rows[i][26].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdfirstlvl)
                    {
                        ddlFirstLvl.SelectedItem.Text = sdfirstlvl;
                        j++;
                    }
                    string sdsecondlvl = ds1.Tables[0].Rows[i][27].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdsecondlvl)
                    {
                        ddlSecondLvl.SelectedItem.Text = sdsecondlvl;
                        j++;
                    }
                    string sdthirdlvl = ds1.Tables[0].Rows[i][28].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdthirdlvl)
                    {
                        ddlThirdLvl.SelectedItem.Text = sdthirdlvl;
                        j++;
                    }
                    string sdfourlvl = ds1.Tables[0].Rows[i][29].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdfourlvl)
                    {
                        ddlFourthLvl.SelectedItem.Text = sdfourlvl;
                        j++;
                    }
                    string sdfifthlvl = ds1.Tables[0].Rows[i][30].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdfifthlvl)
                    {
                        ddlFifthLvl.SelectedItem.Text = sdfifthlvl;
                        j++;
                    }
                    string sdsixthlvl = ds1.Tables[0].Rows[i][31].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdsixthlvl)
                    {
                        ddlSixthLvl.SelectedItem.Text = sdsixthlvl;
                        j++;
                    }
                    string sdseventhlvl = ds1.Tables[0].Rows[i][32].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdseventhlvl)
                    {
                        ddlSeventhLvl.SelectedItem.Text = sdseventhlvl;
                        j++;
                    }
                    string sodfirstlvl = ds1.Tables[0].Rows[i][33].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodfirstlvl)
                    {
                        odlfirstlvl.SelectedItem.Text = sodfirstlvl;
                        j++;
                    }
                    string sodsecondlvl = ds1.Tables[0].Rows[i][34].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodsecondlvl)
                    {
                        odlsecondlvl.SelectedItem.Text = sodsecondlvl;
                        j++;
                    }
                    string sodthirdlvl = ds1.Tables[0].Rows[i][35].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodthirdlvl)
                    {
                        odlthirdlvl.SelectedItem.Text = sodthirdlvl;
                        j++;
                    }
                    string sodfourlvl = ds1.Tables[0].Rows[i][36].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodfourlvl)
                    {
                        odlfourlvl.SelectedItem.Text = sodfourlvl;
                        j++;
                    }
                    string sodfifthlvl = ds1.Tables[0].Rows[i][37].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodfifthlvl)
                    {
                        odlfifthlvl.SelectedItem.Text = sodfifthlvl;

                    }
                    string sodsixthlvl = ds1.Tables[0].Rows[i][38].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodsixthlvl)
                    {
                        odlsixthlvl.SelectedItem.Text = sodsixthlvl;

                    }
                    string sodseventhlvl = ds1.Tables[0].Rows[i][39].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodseventhlvl)
                    {
                        odlseventhlvl.SelectedItem.Text = sodseventhlvl;

                    }
                    string rblpurd = ds1.Tables[0].Rows[i][40].ToString();
                    //if (ds1.Tables[0].Rows[i][j].ToString() == rblpurd)
                    {
                        rblPurchseRtn.SelectedItem.Text = rblpurd;
                        cond += " and purchaseReturn='" + rblpurd + "'";

                    }
                    string rblintd = ds1.Tables[0].Rows[i][41].ToString();
                    //if (ds1.Tables[0].Rows[i][j].ToString() == rblpurd)
                    {
                        rblIntrnlTrns.SelectedItem.Text = rblintd;
                        cond += " and Internaltransfer='" + rblintd + "'";

                    }
                    string startd = ds1.Tables[0].Rows[i][42].ToString();
                    //if (ds1.Tables[0].Rows[i][j].ToString() == rblpurd)
                    {
                        txtStrtDt.Text = startd;


                    }
                    string endd = ds1.Tables[0].Rows[i][43].ToString();
                    //if (ds1.Tables[0].Rows[i][j].ToString() == rblpurd)
                    {
                        txtEndDt.Text = endd;


                    }
                    cond = " and (BillDate) Between CDate('" + startd + "') and  CDate('" + endd + "')";
                }
            }
        }
        if (!isValidLevels())
        {
            return;
        }
        // string cond = "";
        // cond = getCond();
        string field1 = "";
        field1 = getfield();
        getgroupByAndselColumn();
        string field2 = "";
        field2 = getfield2();
        string ordrby;
        ordrby = odrby();
        getgroupByAndselColumn();
        bindData(selColumn, field2, cond, groupBy, ordrby);

    }



    public void bindDataSubTot(string selColumn, string field2, string cond, string groupBy, string sordrby)
    {
        bool dispLastTotal = false;
        string condtion = "";
        condtion = getCond();
        getgroupByAndselColumn();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        string FLvlSub = "", SLvlSub = "", TLvlSub = "", FourLvlSub = "", FiveLvlSUb = "";

        ds = objBL.getSales1Sub(selColumn, field2, condtion, groupBy, sordrby);

        if (ds.Tables[0].Rows.Count > 0)
        {
            if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
            {
                if (ddlFirstLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlFirstLvl.SelectedItem.Text));
                }
                if (ddlSecondLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlSecondLvl.SelectedItem.Text));
                }
                if (ddlThirdLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlThirdLvl.SelectedItem.Text));
                }
                if (ddlFourthLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlFourthLvl.SelectedItem.Text));
                }
                if (ddlFifthLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlFifthLvl.SelectedItem.Text));
                }
                if (ddlSixthLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlSixthLvl.SelectedItem.Text));
                }
                if (ddlSeventhLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlSeventhLvl.SelectedItem.Text));
                }
                if (chkboxStock.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Qty"));
                }
                if (chkboxFreight.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Freight"));
                }
                if (chkboxDiscount.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Discount"));
                }
                if (chkboxRate.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Rate"));
                }

                dt.Columns.Add(new DataColumn("Amount"));
            }
            else if ((firstsub.SelectedItem.Text != "None") || (secondsub.SelectedItem.Text != "None") || (thirdsub.SelectedItem.Text != "None") || (foursub.SelectedItem.Text != "None") || (fivesub.SelectedItem.Text != "None"))
            {
                if (chkboxAll.Checked == true)
                {
                    if (fivesub.SelectedItem.Text != "None")
                    {
                        FLvlSub = firstsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(FLvlSub));
                        SLvlSub = secondsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(SLvlSub));
                        TLvlSub = thirdsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(TLvlSub));
                        FourLvlSub = foursub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(FourLvlSub));
                        FiveLvlSUb = fivesub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(FiveLvlSUb));
                    }
                    else if (foursub.SelectedItem.Text != "None")
                    {
                        FLvlSub = firstsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(FLvlSub));
                        SLvlSub = secondsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(SLvlSub));
                        TLvlSub = thirdsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(TLvlSub));
                        FourLvlSub = foursub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(FourLvlSub));

                        if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName") && (FourLvlSub != "CategoryName"))
                        {
                            dt.Columns.Add(new DataColumn("CategoryName"));
                            FiveLvlSUb = "CategoryName";
                        }
                        else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model") && (FourLvlSub != "Model"))
                        {
                            dt.Columns.Add(new DataColumn("Model"));
                            FiveLvlSUb = "Model";
                        }
                        else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand") && (FourLvlSub != "Brand"))
                        {
                            dt.Columns.Add(new DataColumn("Brand"));
                            FiveLvlSUb = "Brand";
                        }
                        else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName") && (FourLvlSub != "ProductName"))
                        {
                            dt.Columns.Add(new DataColumn("ProductName"));
                            FiveLvlSUb = "ProductName";
                        }
                        else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode") && (FourLvlSub != "ItemCode"))
                        {
                            dt.Columns.Add(new DataColumn("ItemCode"));
                            FiveLvlSUb = "ItemCode";
                        }
                    }
                    else if (thirdsub.SelectedItem.Text != "None")
                    {
                        FLvlSub = firstsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(FLvlSub));
                        SLvlSub = secondsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(SLvlSub));
                        TLvlSub = thirdsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(TLvlSub));

                        if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName"))
                        {
                            dt.Columns.Add(new DataColumn("CategoryName"));
                            FourLvlSub = "CategoryName";
                        }
                        else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model"))
                        {
                            dt.Columns.Add(new DataColumn("Model"));
                            FourLvlSub = "Model";
                        }
                        else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand"))
                        {
                            dt.Columns.Add(new DataColumn("Brand"));
                            FourLvlSub = "Brand";
                        }
                        else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName"))
                        {
                            dt.Columns.Add(new DataColumn("ProductName"));
                            FourLvlSub = "ProductName";
                        }
                        else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode"))
                        {
                            dt.Columns.Add(new DataColumn("ItemCode"));
                            FourLvlSub = "ItemCode";
                        }

                        if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName") && (FourLvlSub != "CategoryName"))
                        {
                            dt.Columns.Add(new DataColumn("CategoryName"));
                            FiveLvlSUb = "CategoryName";
                        }
                        else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model") && (FourLvlSub != "Model"))
                        {
                            dt.Columns.Add(new DataColumn("Model"));
                            FiveLvlSUb = "Model";
                        }
                        else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand") && (FourLvlSub != "Brand"))
                        {
                            dt.Columns.Add(new DataColumn("Brand"));
                            FiveLvlSUb = "Brand";
                        }
                        else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName") && (FourLvlSub != "ProductName"))
                        {
                            dt.Columns.Add(new DataColumn("ProductName"));
                            FiveLvlSUb = "ProductName";
                        }
                        else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode") && (FourLvlSub != "ItemCode"))
                        {
                            dt.Columns.Add(new DataColumn("ItemCode"));
                            FiveLvlSUb = "ItemCode";
                        }

                    }
                    else if (secondsub.SelectedItem.Text != "None")
                    {
                        FLvlSub = firstsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(FLvlSub));
                        SLvlSub = secondsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(SLvlSub));


                        if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName"))
                        {
                            dt.Columns.Add(new DataColumn("CategoryName"));
                            TLvlSub = "CategoryName";
                        }
                        else if ((FLvlSub != "Model") && (SLvlSub != "Model"))
                        {
                            dt.Columns.Add(new DataColumn("Model"));
                            TLvlSub = "Model";
                        }
                        else if ((FLvlSub != "Brand") && (SLvlSub != "Brand"))
                        {
                            dt.Columns.Add(new DataColumn("Brand"));
                            TLvlSub = "Brand";
                        }
                        else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName"))
                        {
                            dt.Columns.Add(new DataColumn("ProductName"));
                            TLvlSub = "ProductName";
                        }
                        else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode"))
                        {
                            dt.Columns.Add(new DataColumn("ItemCode"));
                            TLvlSub = "ItemCode";
                        }

                        if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName"))
                        {
                            dt.Columns.Add(new DataColumn("CategoryName"));
                            FourLvlSub = "CategoryName";
                        }
                        else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model"))
                        {
                            dt.Columns.Add(new DataColumn("Model"));
                            FourLvlSub = "Model";
                        }
                        else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand"))
                        {
                            dt.Columns.Add(new DataColumn("Brand"));
                            FourLvlSub = "Brand";
                        }
                        else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName"))
                        {
                            dt.Columns.Add(new DataColumn("ProductName"));
                            FourLvlSub = "ProductName";
                        }
                        else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode"))
                        {
                            dt.Columns.Add(new DataColumn("ItemCode"));
                            FourLvlSub = "ItemCode";
                        }


                        if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName") && (FourLvlSub != "CategoryName"))
                        {
                            dt.Columns.Add(new DataColumn("CategoryName"));
                            FiveLvlSUb = "CategoryName";
                        }
                        else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model") && (FourLvlSub != "Model"))
                        {
                            dt.Columns.Add(new DataColumn("Model"));
                            FiveLvlSUb = "Model";
                        }
                        else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand") && (FourLvlSub != "Brand"))
                        {
                            dt.Columns.Add(new DataColumn("Brand"));
                            FiveLvlSUb = "Brand";
                        }
                        else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName") && (FourLvlSub != "ProductName"))
                        {
                            dt.Columns.Add(new DataColumn("ProductName"));
                            FiveLvlSUb = "ProductName";
                        }
                        else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode") && (FourLvlSub != "ItemCode"))
                        {
                            dt.Columns.Add(new DataColumn("ItemCode"));
                            FiveLvlSUb = "ItemCode";
                        }

                    }
                    else if (firstsub.SelectedItem.Text != "None")
                    {
                        FLvlSub = firstsub.SelectedItem.Text;
                        dt.Columns.Add(new DataColumn(FLvlSub));
                        if (FLvlSub == "CategoryName")
                        {
                            dt.Columns.Add(new DataColumn("Brand"));
                            dt.Columns.Add(new DataColumn("ProductName"));
                            dt.Columns.Add(new DataColumn("Model"));
                            dt.Columns.Add(new DataColumn("ItemCode"));
                            //dt.Columns.Add(new DataColumn("BillDate"));
                            //dt.Columns.Add(new DataColumn("CustomerName"));
                            //dt.Columns.Add(new DataColumn("Paymode"));

                        }
                        else if (FLvlSub == "Model")
                        {
                            dt.Columns.Add(new DataColumn("CategoryName"));
                            dt.Columns.Add(new DataColumn("Brand"));
                            dt.Columns.Add(new DataColumn("ProductName"));
                            dt.Columns.Add(new DataColumn("ItemCode"));
                            //dt.Columns.Add(new DataColumn("BillDate"));
                            //dt.Columns.Add(new DataColumn("CustomerName"));
                            //dt.Columns.Add(new DataColumn("Paymode"));

                        }
                        else if (FLvlSub == "Brand")
                        {
                            dt.Columns.Add(new DataColumn("CategoryName"));
                            dt.Columns.Add(new DataColumn("Model"));
                            dt.Columns.Add(new DataColumn("ProductName"));
                            dt.Columns.Add(new DataColumn("ItemCode"));
                            //dt.Columns.Add(new DataColumn("BillDate"));
                            //dt.Columns.Add(new DataColumn("CustomerName"));
                            //dt.Columns.Add(new DataColumn("Paymode"));
                        }
                        else if (FLvlSub == "ProductName")
                        {
                            dt.Columns.Add(new DataColumn("CategoryName"));
                            dt.Columns.Add(new DataColumn("Brand"));
                            dt.Columns.Add(new DataColumn("Model"));
                            dt.Columns.Add(new DataColumn("ItemCode"));
                            //dt.Columns.Add(new DataColumn("BillDate"));
                            //dt.Columns.Add(new DataColumn("CustomerName"));
                            //dt.Columns.Add(new DataColumn("Paymode"));
                        }
                        else if (FLvlSub == "ItemCode")
                        {
                            dt.Columns.Add(new DataColumn("CategoryName"));
                            dt.Columns.Add(new DataColumn("Brand"));
                            dt.Columns.Add(new DataColumn("Model"));
                            dt.Columns.Add(new DataColumn("ProductName"));
                            //dt.Columns.Add(new DataColumn("BillDate"));
                            //dt.Columns.Add(new DataColumn("CustomerName"));
                            //dt.Columns.Add(new DataColumn("Paymode"));
                        }

                    }
                    if (chkboxBillDate.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("BillDate"));
                    }
                    if (chkboxCustomer.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("CustomerName"));
                    }
                    if (ChkboxPaymode.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("Paymode"));
                    }
                    if (chkboxPurchaseReturn.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("PurchaseReturn"));
                    }
                    if (chkboxInternalTransfer.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("InternalTransfer"));
                    }

                    if (chkboxBillno.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("BillNo"));
                    }

                    if (ChkboxCustaddr.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("CustomerAddress"));
                    }
                    if (ChkboxCustphone.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("CustomerContacts"));
                    }
                    if (ChkboxEmpname.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("empFirstName"));
                    }

                    if (chkboxStock.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("Qty"));
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("Freight"));
                    }
                    if (chkboxDiscount.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("Discount"));
                    }
                    if (chkboxRate.Checked == true)
                    {
                        dt.Columns.Add(new DataColumn("Rate"));
                    }
                    dt.Columns.Add(new DataColumn("Amount"));

                }
            }
            else if ((chkboxCategory.Checked == true) || (chkboxBrand.Checked == true) || (chkboxProductCode.Checked == true) || (chkboxProductName.Checked == true) || (chkboxInternalTransfer.Checked == true) || (chkboxBillDate.Checked == true) || (chkboxCustomer.Checked == true) || (ChkboxPaymode.Checked == true) || (chkboxPurchaseReturn.Checked == true) || (chkboxModel.Checked == true) || (ChkboxCustaddr.Checked == true) || (ChkboxCustphone.Checked == true) || (ChkboxEmpname.Checked == true) || (chkboxBillno.Checked == true) || (chkboxStock.Checked == true) || (chkboxRate.Checked == true) || (chkboxDiscount.Checked == true) || (chkboxFreight.Checked == true))
            {

                if (chkboxCategory.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CategoryName"));
                }
                if (chkboxBrand.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Brand"));
                }
                if (chkboxProductName.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("ProductName"));
                }
                if (chkboxModel.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Model"));
                }
                if (chkboxProductCode.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("ItemCode"));
                }

                if (chkboxPurchaseReturn.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("PurchaseReturn"));
                }
                if (chkboxInternalTransfer.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("InternalTransfer"));
                }
                if (chkboxBillDate.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("BillDate"));
                }
                if (chkboxCustomer.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CustomerName"));
                }
                if (ChkboxPaymode.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Paymode"));
                }
                if (chkboxBillno.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("BillNo"));
                }

                if (ChkboxCustaddr.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CustomerAddress"));
                }
                if (ChkboxCustphone.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CustomerContacts"));
                }
                if (ChkboxEmpname.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("empFirstName"));
                }

                if (chkboxStock.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Qty"));
                }
                if (chkboxFreight.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Freight"));
                }
                if (chkboxDiscount.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Discount"));
                }
                if (chkboxRate.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Rate"));
                }
                dt.Columns.Add(new DataColumn("Amount"));

            }

            DataRow dr_final8888 = dt.NewRow();
            dt.Rows.Add(dr_final8888);

            //initialize column values for entire row
            string fLvlValue = "", sLvlValue = "", tLvlValue = "", frthLvlValue = "", fifLvlValue = "", sixLvlValue = "", svthLvlValue = "", eightLvlValue = "", ninthLvlValue = "", tenthLvlValue = "", eleventhLvlValue = "", twelthLvlValue = "", thirteenLvlValue = "", fourteenLvlValue = "";
            string fLvlValueTemp = "", sLvlValueTemp = "", tLvlValueTemp = "", frthLvlValueTemp = "", fifLvlValueTemp = "", sixLvlValueTemp = "", svthLvlValueTemp = "", eightLvlValueTemp = "", ninthLvlValueTemp = "", tenthLvlValueTemp = "", eleventhLvlValueTemp = "", twelthLvlValueTemp = "", thirteenLvlValueTemp = "", fourteenLvlValueTemp = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
                {
                    //initialize column values for entire row
                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFirstLvl.SelectedItem.Text == "Brand")
                        {
                            fLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            fLvlValueTemp = dr[ddlFirstLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    }
                    if (ddlSecondLvl.SelectedItem.Text != "None")
                        if (ddlSecondLvl.SelectedItem.Text == "Brand")
                        {
                            sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            sLvlValueTemp = dr[ddlSecondLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlThirdLvl.SelectedItem.Text != "None")
                        if (ddlThirdLvl.SelectedItem.Text == "Brand")
                        {
                            tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            tLvlValueTemp = dr[ddlThirdLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlFourthLvl.SelectedItem.Text != "None")
                        if (ddlFourthLvl.SelectedItem.Text == "Brand")
                        {
                            frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            frthLvlValueTemp = dr[ddlFourthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlFifthLvl.SelectedItem.Text != "None")
                        if (ddlFifthLvl.SelectedItem.Text == "Brand")
                        {
                            fifLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            fifLvlValueTemp = dr[ddlFifthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlSixthLvl.SelectedItem.Text != "None")
                        if (ddlSixthLvl.SelectedItem.Text == "Brand")
                        {
                            sixLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            sixLvlValueTemp = dr[ddlSixthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                        if (ddlSeventhLvl.SelectedItem.Text == "Brand")
                        {
                            svthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            svthLvlValueTemp = dr[ddlSeventhLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }

                    dispLastTotal = true;

                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                            (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                            (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp) ||
                            (sixLvlValue != "" && sixLvlValue != sixLvlValueTemp) ||
                            (svthLvlValue != "" && svthLvlValue != svthLvlValueTemp) || (eightLvlValueTemp != "" && eightLvlValue != eightLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "Total:" + svthLvlValue;
                            }
                            /*  if (selLevels.IndexOf("CustomerID") < 0)
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
                                  dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(Pttls));
                            }
                            dt.Rows.Add(dr_final7);
                            Pttls = 0;
                        }
                    }

                    if (ddlSixthLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                            (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                            (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp) ||
                            (sixLvlValue != "" && sixLvlValue != sixLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "Total:" + sixLvlValue;
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /*  if (selLevels.IndexOf("CustomerID") < 0)
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
                                  dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /*dr_final7["Discount"] = "";
                            dr_final7["Freight"] = "";
                            dr_final7["Qty"] = "";
                            dr_final7["Amount"] = "";
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                            }
                            dt.Rows.Add(dr_final7);
                            modelTotal = 0;
                        }
                    }

                    if (ddlFifthLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                           (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                           (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                           (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                           (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "Total:" + fifLvlValue;
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /* if (selLevels.IndexOf("CustomerID") < 0)
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
                                 dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /*dr_final7["Discount"] = "";
                            dr_final7["Freight"] = "";
                            dr_final7["Qty"] = "";
                            dr_final7["Amount"] = "";
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                            }
                            dt.Rows.Add(dr_final7);
                            catIDTotal = 0;
                        }
                    }

                    if (ddlFourthLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                           (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                           (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                           (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "Total:" + frthLvlValue;
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /* if (selLevels.IndexOf("CustomerID") < 0)
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
                                 dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /*   dr_final7["Discount"] = "";
                               dr_final7["Freight"] = "";
                               dr_final7["Qty"] = "";
                               dr_final7["Amount"] = "";
                               dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                            }
                            dt.Rows.Add(dr_final7);
                            brandTotal = 0;
                        }
                    }

                    if (ddlThirdLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                           (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                           (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "Total:" + tLvlValue;
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /* if (selLevels.IndexOf("CustomerID") < 0)
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
                                 dr_final7["ItemCode"] = "";*/

                            // dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /*  dr_final7["Discount"] = "";
                              dr_final7["Freight"] = "";
                              dr_final7["Qty"] = "";
                              dr_final7["Amount"] = "";
                              dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));
                            }
                            dt.Rows.Add(dr_final7);
                            brandTotal1 = 0;
                        }
                    }

                    if (ddlSecondLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                           (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "Total:" + sLvlValue;
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /* if (selLevels.IndexOf("CustomerID") < 0)
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
                                 dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /* dr_final7["Discount"] = "";
                             dr_final7["Freight"] = "";
                             dr_final7["Qty"] = "";
                             dr_final7["Amount"] = "";
                             dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));
                            }
                            dt.Rows.Add(dr_final7);
                            brandTotal2 = 0;
                        }
                    }

                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "Total:" + fLvlValue;
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /*if (selLevels.IndexOf("CustomerID") < 0)
                                dr_final7["CustomerID"] = "";*/
                            /*if (selLevels.IndexOf("BillDate") < 0)
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
                                dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /*  dr_final7["Discount"] = "";
                              dr_final7["Freight"] = "";
                              dr_final7["Qty"] = "";
                              dr_final7["Amount"] = "";
                              dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));
                            }
                            dt.Rows.Add(dr_final7);
                            brandTotal3 = 0;
                        }
                    }
                    ///////////////////////////////////////

                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;
                    frthLvlValue = frthLvlValueTemp;
                    fifLvlValue = fifLvlValueTemp;
                    sixLvlValue = sixLvlValueTemp;
                    svthLvlValue = sixLvlValueTemp;
                    DataRow dr_final5 = dt.NewRow();
                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFirstLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                    }

                    if (ddlSecondLvl.SelectedItem.Text != "None")
                    {
                        if (ddlSecondLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["ItemCode"];
                        }

                    }
                    if (ddlThirdLvl.SelectedItem.Text != "None")
                    {
                        if (ddlThirdLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlFourthLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFourthLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlFifthLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFifthLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlSixthLvl.SelectedItem.Text != "None")
                    {
                        if (ddlSixthLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                    {
                        if (ddlSeventhLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    /*  if (selLevels.IndexOf("CustomerID") < 0)
                          dr_final5["CustomerID"] = dr["CustomerID"];
                      if (selLevels.IndexOf("BillDate") < 0)
                          dr_final5["BillDate"] = dr["BillDate"];
                      if (selLevels.IndexOf("PayMode") < 0)
                          dr_final5["PayMode"] = dr["LedgerName"];
                      if (selLevels.IndexOf("ProductDesc") < 0)
                          dr_final5["ProductDesc"] = dr["ProductDesc"];
                      if (selLevels.IndexOf("CategoryID") < 0)
                          dr_final5["CategoryID"] = dr["CategoryID"];
                      if (selLevels.IndexOf("ProductName") < 0)
                          dr_final5["ProductName"] = dr["ProductName"];
                      if (selLevels.IndexOf("ItemCode") < 0)
                          dr_final5["ItemCode"] = dr["ItemCode"];*/

                    // dr_final5["Model"] = dr["Model"];
                    //dr_final5["BillNo"] = dr["BillNo"];
                    //dr_final5["Internaltransfer"] = dr["Internaltransfer"];
                    //dr_final5["purchaseReturn"] = dr["purchaseReturn"];
                    //dr_final5["CustomerAddress"] = dr["CustomerAddress"];
                    //dr_final5["CustomerContacts"] = dr["CustomerContacts"];
                    //dr_final5["empFirstName"] = dr["empFirstName"];
                    /* dr_final5["Discount"] = dr["Discount"];
                     dr_final5["Freight"] = dr["Freight"];
                     dr_final5["Qty"] = dr["Qty"];
                     dr_final5["Rate"] = dr["Rate"];*/
                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final5["Discount"] = dr["Discount"];
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final5["Freight"] = dr["Freight"];
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final5["Qty"] = dr["Qty"];
                    }

                    // dr_final1["Amount"] = "";
                    if (chkboxRate.Checked == true)
                    {
                        dr_final5["Rate"] = dr["Rate"];
                    }
                    dr_final5["Amount"] = dr["Amount"];
                    dt.Rows.Add(dr_final5);
                    if (chkboxStock.Checked == true)
                    {
                        Gttl = Gttl + Convert.ToInt32(dr["Qty"]);
                    }
                    if (chkboxRate.Checked == true)
                    {
                        Gtotal = Gtotal + Convert.ToDecimal(dr["Rate"]);
                        modelTotal = modelTotal + Convert.ToDecimal(dr["Rate"]);
                        catIDTotal = catIDTotal + Convert.ToDecimal(dr["Rate"]);
                        Pttls = Pttls + Convert.ToDecimal(dr["Rate"]);
                        brandTotal = brandTotal + Convert.ToDecimal(dr["Rate"]);
                        brandTotal1 = brandTotal1 + Convert.ToDecimal(dr["Rate"]);
                        brandTotal2 = brandTotal2 + Convert.ToDecimal(dr["Rate"]);
                        brandTotal3 = brandTotal3 + Convert.ToDecimal(dr["Rate"]);
                    }
                    brandTotal4 = brandTotal4 + Convert.ToDecimal(dr["Amount"]);

                }
                else if ((chkboxCategory.Checked == true) || (chkboxBrand.Checked == true) || (chkboxProductCode.Checked == true) || (chkboxProductName.Checked == true) || (chkboxInternalTransfer.Checked == true) || (chkboxBillDate.Checked == true) || (chkboxCustomer.Checked == true) || (ChkboxPaymode.Checked == true) || (chkboxPurchaseReturn.Checked == true) || (chkboxModel.Checked == true) || (ChkboxCustaddr.Checked == true) || (ChkboxCustphone.Checked == true) || (ChkboxEmpname.Checked == true) || (chkboxBillno.Checked == true) || (chkboxStock.Checked == true) || (chkboxRate.Checked == true) || (chkboxDiscount.Checked == true) || (chkboxFreight.Checked == true))
                {
                    if (chkboxAll.Checked == true)
                    {
                        if (fivesub.SelectedItem.Text != "None")
                        {
                            if (firstsub.SelectedItem.Text == "Brand")
                            {
                                fLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                fLvlValueTemp = dr[firstsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (secondsub.SelectedItem.Text == "Brand")
                            {
                                sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                sLvlValueTemp = dr[secondsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (thirdsub.SelectedItem.Text == "Brand")
                            {
                                tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                tLvlValueTemp = dr[thirdsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (foursub.SelectedItem.Text == "Brand")
                            {
                                frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                frthLvlValueTemp = dr[foursub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (fivesub.SelectedItem.Text == "Brand")
                            {
                                fifLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                fifLvlValueTemp = dr[fivesub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                        }
                        else if (foursub.SelectedItem.Text != "None")
                        {
                            if (firstsub.SelectedItem.Text == "Brand")
                            {
                                fLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                fLvlValueTemp = dr[firstsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (secondsub.SelectedItem.Text == "Brand")
                            {
                                sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                sLvlValueTemp = dr[secondsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (thirdsub.SelectedItem.Text == "Brand")
                            {
                                tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                tLvlValueTemp = dr[thirdsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (foursub.SelectedItem.Text == "Brand")
                            {
                                frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                frthLvlValueTemp = dr[foursub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }


                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName") && (FourLvlSub != "CategoryName"))
                            {
                                fifLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model") && (FourLvlSub != "Model"))
                            {
                                fifLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand") && (FourLvlSub != "Brand"))
                            {
                                fifLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName") && (FourLvlSub != "ProductName"))
                            {
                                fifLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode") && (FourLvlSub != "ItemCode"))
                            {
                                fifLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }
                        }
                        else if (thirdsub.SelectedItem.Text != "None")
                        {
                            if (firstsub.SelectedItem.Text == "Brand")
                            {
                                fLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                fLvlValueTemp = dr[firstsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (secondsub.SelectedItem.Text == "Brand")
                            {
                                sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                sLvlValueTemp = dr[secondsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (thirdsub.SelectedItem.Text == "Brand")
                            {
                                tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                tLvlValueTemp = dr[thirdsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }


                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName"))
                            {
                                frthLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model"))
                            {
                                frthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand"))
                            {
                                frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName"))
                            {
                                frthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode"))
                            {
                                frthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }

                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName") && (FourLvlSub != "CategoryName"))
                            {
                                fifLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model") && (FourLvlSub != "Model"))
                            {
                                fifLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand") && (FourLvlSub != "Brand"))
                            {
                                fifLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName") && (FourLvlSub != "ProductName"))
                            {
                                fifLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode") && (FourLvlSub != "ItemCode"))
                            {
                                fifLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }

                        }
                        else if (secondsub.SelectedItem.Text != "None")
                        {
                            if (firstsub.SelectedItem.Text == "Brand")
                            {
                                fLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                fLvlValueTemp = dr[firstsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (secondsub.SelectedItem.Text == "Brand")
                            {
                                sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                sLvlValueTemp = dr[secondsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }

                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName"))
                            {
                                tLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model"))
                            {
                                tLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand"))
                            {
                                tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName"))
                            {
                                tLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode"))
                            {
                                tLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }

                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName"))
                            {
                                frthLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model"))
                            {
                                frthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand"))
                            {
                                frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName"))
                            {
                                frthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode"))
                            {
                                frthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }


                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName") && (FourLvlSub != "CategoryName"))
                            {
                                fifLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model") && (FourLvlSub != "Model"))
                            {
                                fifLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand") && (FourLvlSub != "Brand"))
                            {
                                fifLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName") && (FourLvlSub != "ProductName"))
                            {
                                fifLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode") && (FourLvlSub != "ItemCode"))
                            {
                                fifLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }

                        }
                        else if (firstsub.SelectedItem.Text != "None")
                        {
                            if (firstsub.SelectedItem.Text == "Brand")
                            {
                                fLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                fLvlValueTemp = dr[firstsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }

                            if (FLvlSub == "CategoryName")
                            {
                                sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                                tLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                                frthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                                fifLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }
                            else if (FLvlSub == "Model")
                            {
                                sLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                                tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                                frthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                                fifLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();

                            }
                            else if (FLvlSub == "Brand")
                            {
                                sLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                                tLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                                frthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                                fifLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();

                            }
                            else if (FLvlSub == "ProductName")
                            {
                                sLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                                tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                                frthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                                fifLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();

                            }
                            else if (FLvlSub == "ItemCode")
                            {
                                sLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                                tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                                frthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                                fifLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                            }

                        }
                    }

                    //if (chkboxCategory.Checked == true)
                    //    fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                    if (chkboxCustomer.Checked == true)
                        svthLvlValueTemp = dr["CustomerName"].ToString().ToUpper().Trim();
                    //if (chkboxBillDate.Checked == true)
                    //    tLvlValueTemp = dr["BillDate"].ToString().ToUpper().Trim();
                    //if (chkboxBrand.Checked == true)
                    //    frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                    //if (chkboxProductCode.Checked == true)
                    //    fifLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                    //if (chkboxModel.Checked == true)
                    //    eleventhLvlValueTemp = dr["Model"].ToString().ToUpper().ToUpper().Trim();

                    if (chkboxBillDate.Checked == true)
                        eleventhLvlValueTemp = dr["BillDate"].ToString().ToUpper().Trim();

                    if (chkboxPurchaseReturn.Checked == true)
                        sixLvlValueTemp = dr["PurchaseReturn"].ToString().ToUpper().Trim();

                    if (chkboxInternalTransfer.Checked == true)
                        svthLvlValueTemp = dr["InternalTransfer"].ToString().ToUpper().Trim();
                    if (ChkboxPaymode.Checked == true)
                        eightLvlValueTemp = dr["Paymode"].ToString().ToUpper().Trim();
                    //if (chkboxProductName.Checked == true)
                    //    ninthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                    if (chkboxBillno.Checked == true)
                        tenthLvlValueTemp = dr["Billno"].ToString().ToUpper().Trim();

                    if (ChkboxCustaddr.Checked == true)
                        twelthLvlValueTemp = dr["Add1"].ToString().ToUpper().Trim();
                    if (ChkboxCustphone.Checked == true)
                        thirteenLvlValueTemp = dr["CustomerContacts"].ToString().ToUpper().Trim();
                    if (ChkboxEmpname.Checked == true)
                        fourteenLvlValueTemp = dr["empFirstName"].ToString().ToUpper().Trim();

                    dispLastTotal = true;
                }


                if (chkboxAll.Checked == true)
                {
                    if (fivesub.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                             (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                             (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                             (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                             (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp))
                        {
                            DataRow dr_final888 = dt.NewRow();
                            dt.Rows.Add(dr_final888);

                            DataRow dr_final17 = dt.NewRow();

                            if (firstsub.SelectedItem.Text != "None")
                            {
                                dr_final17[firstsub.SelectedItem.Text] = "";
                            }
                            if (secondsub.SelectedItem.Text != "None")
                            {
                                dr_final17[secondsub.SelectedItem.Text] = "";
                            }
                            if (thirdsub.SelectedItem.Text != "None")
                            {
                                dr_final17[thirdsub.SelectedItem.Text] = "";
                            }
                            if (foursub.SelectedItem.Text != "None")
                            {
                                dr_final17[foursub.SelectedItem.Text] = "";
                            }
                            if (fivesub.SelectedItem.Text != "None")
                            {
                                dr_final17[fivesub.SelectedItem.Text] = "Total : " + fifLvlValue;
                            }
                            if (chkboxBillDate.Checked == true)
                            {
                                dr_final17["BillDate"] = "";
                            }
                            if (chkboxCustomer.Checked == true)
                            {
                                dr_final17["CustomerName"] = "";

                            }
                            if (ChkboxPaymode.Checked == true)
                            {
                                if (eightLvlValueTemp == "1")
                                {
                                    dr_final17["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "2")
                                {
                                    dr_final17["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "3")
                                {
                                    dr_final17["Paymode"] = "";
                                }

                                //dr_final5["Paymode"] = dr["Paymode"];
                            }
                            if (chkboxPurchaseReturn.Checked == true)
                            {
                                dr_final17["PurchaseReturn"] = "";
                            }
                            if (chkboxInternalTransfer.Checked == true)
                            {
                                dr_final17["InternalTransfer"] = "";
                            }

                            if (chkboxBillno.Checked == true)
                            {
                                dr_final17["Billno"] = "";

                            }

                            if (ChkboxCustaddr.Checked == true)
                            {
                                dr_final17["CustomerAddress"] = "";

                            }
                            if (ChkboxCustphone.Checked == true)
                            {
                                dr_final17["CustomerContacts"] = "";

                            }
                            if (ChkboxEmpname.Checked == true)
                            {
                                dr_final17["empFirstName"] = "";
                            }

                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final17["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final17["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final17["Qty"] = "";
                            }

                            if (chkboxRate.Checked == true)
                            {
                                dr_final17["Rate"] = "";
                            }


                            if (FiveLvlSUb == "CategoryName")
                            {
                                dr_final17["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                                catIDTotal = 0;
                            }
                            else if (FiveLvlSUb == "Brand")
                            {
                                dr_final17["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                                brandTotal = 0;
                            }
                            else if (FiveLvlSUb == "ProductName")
                            {
                                dr_final17["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                                modelTotal = 0;
                            }
                            else if (FiveLvlSUb == "Model")
                            {
                                dr_final17["Amount"] = Convert.ToString(Convert.ToDecimal(Pttls));
                                Pttls = 0;
                            }
                            else if (FiveLvlSUb == "ItemCode")
                            {
                                dr_final17["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal1));
                                modelTotal1 = 0;
                            }
                            dt.Rows.Add(dr_final17);

                            //DataRow dr_final8888 = dt.NewRow();
                            //dt.Rows.Add(dr_final8888);
                        }
                    }

                    if (foursub.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                            (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp))
                        {

                            DataRow dr_final86 = dt.NewRow();
                            dt.Rows.Add(dr_final86);

                            DataRow dr_final7 = dt.NewRow();

                            if (firstsub.SelectedItem.Text != "None")
                            {
                                dr_final7[firstsub.SelectedItem.Text] = "";
                            }
                            if (secondsub.SelectedItem.Text != "None")
                            {
                                dr_final7[secondsub.SelectedItem.Text] = "";
                            }
                            if (thirdsub.SelectedItem.Text != "None")
                            {
                                dr_final7[thirdsub.SelectedItem.Text] = "";
                            }
                            if (foursub.SelectedItem.Text != "None")
                            {
                                dr_final7[foursub.SelectedItem.Text] = "Total : " + frthLvlValue;
                            }
                            if (fivesub.SelectedItem.Text != "None")
                            {
                                dr_final7[fivesub.SelectedItem.Text] = "";
                            }
                            if (chkboxBillDate.Checked == true)
                            {
                                dr_final7["BillDate"] = "";
                            }
                            if (chkboxCustomer.Checked == true)
                            {
                                dr_final7["CustomerName"] = "";
                            }
                            if (ChkboxPaymode.Checked == true)
                            {

                                if (eightLvlValueTemp == "1")
                                {
                                    dr_final7["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "2")
                                {
                                    dr_final7["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "3")
                                {
                                    dr_final7["Paymode"] = "";
                                }

                                //dr_final5["Paymode"] = dr["Paymode"];
                            }
                            if (chkboxPurchaseReturn.Checked == true)
                            {
                                dr_final7["PurchaseReturn"] = "";
                            }
                            if (chkboxInternalTransfer.Checked == true)
                            {
                                dr_final7["InternalTransfer"] = "";
                            }



                            if (chkboxBillno.Checked == true)
                            {
                                dr_final7["Billno"] = "";

                            }

                            if (ChkboxCustaddr.Checked == true)
                            {
                                dr_final7["CustomerAddress"] = "";

                            }
                            if (ChkboxCustphone.Checked == true)
                            {
                                dr_final7["CustomerContacts"] = "";

                            }
                            if (ChkboxEmpname.Checked == true)
                            {
                                dr_final7["empFirstName"] = "";
                            }

                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            // dr_final1["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = "";
                            }


                            if (FourLvlSub == "CategoryName")
                            {
                                dr_final7["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                                catIDTotal = 0;
                            }
                            else if (FourLvlSub == "Brand")
                            {
                                dr_final7["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                                brandTotal = 0;
                            }
                            else if (FourLvlSub == "ProductName")
                            {
                                dr_final7["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                                modelTotal = 0;
                            }
                            else if (FourLvlSub == "Model")
                            {
                                dr_final7["Amount"] = Convert.ToString(Convert.ToDecimal(Pttls));
                                Pttls = 0;
                            }
                            else if (FourLvlSub == "ItemCode")
                            {
                                dr_final7["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal1));
                                modelTotal1 = 0;
                            }

                            dt.Rows.Add(dr_final7);

                            if (fivesub.SelectedItem.Text == "None")
                            {
                                DataRow dr_final887 = dt.NewRow();
                                dt.Rows.Add(dr_final887);
                            }
                            else
                            {
                                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                                (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                                (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                                {
                                }
                                else
                                {
                                    DataRow dr_final888 = dt.NewRow();
                                    dt.Rows.Add(dr_final888);
                                }
                            }

                        }
                    }


                    if (thirdsub.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                        {

                            if ((foursub.SelectedItem.Text == "None") && (fivesub.SelectedItem.Text == "None"))
                            {
                                DataRow dr_final888 = dt.NewRow();
                                dt.Rows.Add(dr_final888);
                            }
                            else if (fivesub.SelectedItem.Text == "None")
                            {
                            }
                            else
                            {
                                DataRow dr_final888 = dt.NewRow();
                                dt.Rows.Add(dr_final888);
                            }

                            DataRow dr_final8 = dt.NewRow();

                            if (firstsub.SelectedItem.Text != "None")
                            {
                                dr_final8[firstsub.SelectedItem.Text] = "";
                            }
                            if (secondsub.SelectedItem.Text != "None")
                            {
                                dr_final8[secondsub.SelectedItem.Text] = "";
                            }
                            if (thirdsub.SelectedItem.Text != "None")
                            {
                                dr_final8[thirdsub.SelectedItem.Text] = "Total : " + tLvlValue;
                            }
                            if (foursub.SelectedItem.Text != "None")
                            {
                                dr_final8[foursub.SelectedItem.Text] = "";
                            }
                            if (fivesub.SelectedItem.Text != "None")
                            {
                                dr_final8[fivesub.SelectedItem.Text] = "";
                            }
                            if (chkboxBillDate.Checked == true)
                            {
                                dr_final8["BillDate"] = "";
                            }
                            if (chkboxCustomer.Checked == true)
                            {
                                dr_final8["CustomerName"] = "";

                            }
                            if (ChkboxPaymode.Checked == true)
                            {
                                if (eightLvlValueTemp == "1")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "2")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "3")
                                {
                                    dr_final8["Paymode"] = "";
                                }

                                //dr_final5["Paymode"] = dr["Paymode"];
                            }

                            if (chkboxPurchaseReturn.Checked == true)
                            {
                                dr_final8["PurchaseReturn"] = "";
                            }
                            if (chkboxInternalTransfer.Checked == true)
                            {
                                dr_final8["InternalTransfer"] = "";
                            }

                            if (chkboxBillno.Checked == true)
                            {
                                dr_final8["Billno"] = "";

                            }

                            if (ChkboxCustaddr.Checked == true)
                            {
                                dr_final8["CustomerAddress"] = "";

                            }
                            if (ChkboxCustphone.Checked == true)
                            {
                                dr_final8["CustomerContacts"] = "";

                            }
                            if (ChkboxEmpname.Checked == true)
                            {
                                dr_final8["empFirstName"] = "";
                            }

                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final8["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final8["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final8["Qty"] = "";
                            }

                            // dr_final1["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final8["Rate"] = "";
                            }


                            if (TLvlSub == "CategoryName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                                catIDTotal = 0;
                            }
                            else if (TLvlSub == "Brand")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                                brandTotal = 0;
                            }
                            else if (TLvlSub == "ProductName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                                modelTotal = 0;
                            }
                            else if (TLvlSub == "Model")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(Pttls));
                                Pttls = 0;
                            }
                            else if (TLvlSub == "ItemCode")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal1));
                                modelTotal1 = 0;
                            }

                            dt.Rows.Add(dr_final8);

                            if ((foursub.SelectedItem.Text == "None") || (fivesub.SelectedItem.Text == "None"))
                            {
                                DataRow dr_final887 = dt.NewRow();
                                dt.Rows.Add(dr_final887);
                            }
                            else
                            {
                                if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                               (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                                {
                                }
                                else
                                {
                                    DataRow dr_final889 = dt.NewRow();
                                    dt.Rows.Add(dr_final889);
                                }
                            }
                        }
                    }

                    if (secondsub.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                               (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                        {
                            if (thirdsub.SelectedItem.Text == "None")
                            {
                                DataRow dr_final889 = dt.NewRow();
                                dt.Rows.Add(dr_final889);
                            }
                            else if (fivesub.SelectedItem.Text != "None")
                            {
                                DataRow dr_final888 = dt.NewRow();
                                dt.Rows.Add(dr_final888);
                            }
                            else
                            {
                            }



                            DataRow dr_final8 = dt.NewRow();
                            //if (chkboxAll.Checked == true)
                            //{
                            if (firstsub.SelectedItem.Text != "None")
                            {
                                dr_final8[firstsub.SelectedItem.Text] = "";
                            }
                            if (secondsub.SelectedItem.Text != "None")
                            {
                                dr_final8[secondsub.SelectedItem.Text] = "Total : " + sLvlValue;
                            }
                            if (thirdsub.SelectedItem.Text != "None")
                            {
                                dr_final8[thirdsub.SelectedItem.Text] = "";
                            }
                            if (foursub.SelectedItem.Text != "None")
                            {
                                dr_final8[foursub.SelectedItem.Text] = "";
                            }
                            if (fivesub.SelectedItem.Text != "None")
                            {
                                dr_final8[fivesub.SelectedItem.Text] = "";
                            }
                            if (chkboxBillDate.Checked == true)
                            {
                                dr_final8["BillDate"] = "";
                            }
                            if (chkboxCustomer.Checked == true)
                            {
                                dr_final8["CustomerName"] = "";

                            }
                            if (ChkboxPaymode.Checked == true)
                            {
                                if (eightLvlValueTemp == "1")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "2")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "3")
                                {
                                    dr_final8["Paymode"] = "";
                                }

                                //dr_final5["Paymode"] = dr["Paymode"];
                            }
                            if (chkboxPurchaseReturn.Checked == true)
                            {
                                dr_final8["PurchaseReturn"] = "";
                            }
                            if (chkboxInternalTransfer.Checked == true)
                            {
                                dr_final8["InternalTransfer"] = "";
                            }

                            if (ChkboxPaymode.Checked == true)
                            {

                                if (eightLvlValueTemp == "1")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "2")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "3")
                                {
                                    dr_final8["Paymode"] = "";
                                }

                                //dr_final5["Paymode"] = dr["Paymode"];
                            }
                            if (chkboxCustomer.Checked == true)
                            {
                                dr_final8["CustomerName"] = "";

                            }
                            if (chkboxBillno.Checked == true)
                            {
                                dr_final8["Billno"] = "";

                            }

                            if (ChkboxCustaddr.Checked == true)
                            {
                                dr_final8["CustomerAddress"] = "";

                            }
                            if (ChkboxCustphone.Checked == true)
                            {
                                dr_final8["CustomerContacts"] = "";

                            }
                            if (ChkboxEmpname.Checked == true)
                            {
                                dr_final8["empFirstName"] = "";
                            }

                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final8["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final8["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final8["Qty"] = "";
                            }

                            // dr_final1["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final8["Rate"] = "";
                            }


                            if (SLvlSub == "CategoryName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                                catIDTotal = 0;
                            }
                            else if (SLvlSub == "Brand")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                                brandTotal = 0;
                            }
                            else if (SLvlSub == "ProductName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                                modelTotal = 0;
                            }
                            else if (SLvlSub == "Model")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(Pttls));
                                Pttls = 0;
                            }
                            else if (SLvlSub == "ItemCode")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal1));
                                modelTotal1 = 0;
                            }

                            dt.Rows.Add(dr_final8);


                            if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                            {

                            }

                            else
                            {
                                DataRow dr_final888 = dt.NewRow();
                                dt.Rows.Add(dr_final888);
                            }

                        }
                    }

                    if (firstsub.SelectedItem.Text != "None")
                    {
                        if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                        {
                            DataRow dr_final889 = dt.NewRow();
                            dt.Rows.Add(dr_final889);

                            DataRow dr_final8 = dt.NewRow();
                            if (firstsub.SelectedItem.Text != "None")
                            {
                                dr_final8[firstsub.SelectedItem.Text] = "Total : " + fLvlValue;
                            }
                            if (secondsub.SelectedItem.Text != "None")
                            {
                                dr_final8[secondsub.SelectedItem.Text] = "";
                            }
                            if (thirdsub.SelectedItem.Text != "None")
                            {
                                dr_final8[thirdsub.SelectedItem.Text] = "";
                            } if (foursub.SelectedItem.Text != "None")
                            {
                                dr_final8[foursub.SelectedItem.Text] = "";
                            }
                            if (fivesub.SelectedItem.Text != "None")
                            {
                                dr_final8[fivesub.SelectedItem.Text] = "";
                            }
                            if (chkboxBillDate.Checked == true)
                            {
                                dr_final8["BillDate"] = "";
                            }
                            if (chkboxCustomer.Checked == true)
                            {
                                dr_final8["CustomerName"] = "";

                            }
                            if (ChkboxPaymode.Checked == true)
                            {
                                if (eightLvlValueTemp == "1")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "2")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "3")
                                {
                                    dr_final8["Paymode"] = "";
                                }

                                //dr_final5["Paymode"] = dr["Paymode"];
                            }
                            if (chkboxPurchaseReturn.Checked == true)
                            {
                                dr_final8["PurchaseReturn"] = "";
                            }
                            if (chkboxInternalTransfer.Checked == true)
                            {
                                dr_final8["InternalTransfer"] = "";
                            }


                            if (chkboxBillno.Checked == true)
                            {
                                dr_final8["Billno"] = "";

                            }

                            if (ChkboxCustaddr.Checked == true)
                            {
                                dr_final8["CustomerAddress"] = "";

                            }
                            if (ChkboxCustphone.Checked == true)
                            {
                                dr_final8["CustomerContacts"] = "";

                            }
                            if (ChkboxEmpname.Checked == true)
                            {
                                dr_final8["empFirstName"] = "";
                            }

                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final8["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final8["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final8["Qty"] = "";
                            }

                            // dr_final1["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final8["Rate"] = "";
                            }


                            if (FLvlSub == "CategoryName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                                catIDTotal = 0;
                            }
                            else if (FLvlSub == "Brand")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                                brandTotal = 0;
                            }
                            else if (FLvlSub == "ProductName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                                modelTotal = 0;
                            }
                            else if (FLvlSub == "Model")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(Pttls));
                                Pttls = 0;
                            }
                            else if (FLvlSub == "ItemCode")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal1));
                                modelTotal1 = 0;
                            }

                            dt.Rows.Add(dr_final8);

                            DataRow dr_final888 = dt.NewRow();
                            dt.Rows.Add(dr_final888);
                        }
                    }
                }

                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;
                frthLvlValue = frthLvlValueTemp;
                fifLvlValue = fifLvlValueTemp;
                sixLvlValue = sixLvlValueTemp;
                svthLvlValue = svthLvlValueTemp;
                eightLvlValue = eightLvlValueTemp;
                ninthLvlValue = ninthLvlValueTemp;
                tenthLvlValue = tenthLvlValueTemp;
                eleventhLvlValue = eleventhLvlValueTemp;
                twelthLvlValue = twelthLvlValueTemp;
                thirteenLvlValue = thirteenLvlValueTemp;
                fourteenLvlValue = fourteenLvlValueTemp;


                if (chkboxAll.Checked == true)
                {
                    if ((firstsub.SelectedItem.Text != "None") || (secondsub.SelectedItem.Text != "None") || (thirdsub.SelectedItem.Text != "None") || (foursub.SelectedItem.Text != "None") || (fivesub.SelectedItem.Text != "None"))
                    {
                        DataRow dr_final5 = dt.NewRow();


                        if (fivesub.SelectedItem.Text != "None")
                        {
                            if (firstsub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[firstsub.SelectedItem.Text] = fLvlValueTemp;
                                //fLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[firstsub.SelectedItem.Text] = fLvlValueTemp;
                                fLvlValueTemp = dr[firstsub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (secondsub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[secondsub.SelectedItem.Text] = sLvlValueTemp;
                                //sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[secondsub.SelectedItem.Text] = sLvlValueTemp;
                                //sLvlValueTemp = dr[DdlSecondSub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (thirdsub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[thirdsub.SelectedItem.Text] = tLvlValueTemp;
                                //tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[thirdsub.SelectedItem.Text] = tLvlValueTemp;
                                //tLvlValueTemp = dr[DdlThirdSub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (foursub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[foursub.SelectedItem.Text] = frthLvlValueTemp;
                                //frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[foursub.SelectedItem.Text] = frthLvlValueTemp;
                                //frthLvlValueTemp = dr[DdlFourSub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (fivesub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[fivesub.SelectedItem.Text] = fifLvlValueTemp;
                                //fifthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[fivesub.SelectedItem.Text] = fifLvlValueTemp;
                                //fifthLvlValueTemp = dr[DdlFiveSub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                        }
                        else if (foursub.SelectedItem.Text != "None")
                        {
                            if (firstsub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[firstsub.SelectedItem.Text] = fLvlValueTemp;
                                //fLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[firstsub.SelectedItem.Text] = fLvlValueTemp;
                                //fLvlValueTemp = dr[DdlFirstSub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (secondsub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[secondsub.SelectedItem.Text] = sLvlValueTemp;
                                //sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[secondsub.SelectedItem.Text] = sLvlValueTemp;
                                //sLvlValueTemp = dr[DdlSecondSub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (thirdsub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[thirdsub.SelectedItem.Text] = tLvlValueTemp;
                                //tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[thirdsub.SelectedItem.Text] = tLvlValueTemp;
                                //tLvlValueTemp = dr[DdlThirdSub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            if (foursub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[foursub.SelectedItem.Text] = frthLvlValueTemp;
                                //frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[foursub.SelectedItem.Text] = frthLvlValueTemp;
                                //frthLvlValueTemp = dr[DdlFourSub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }


                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName") && (FourLvlSub != "CategoryName"))
                            {
                                dr_final5["CategoryName"] = dr["CategoryName"];
                                //fifthLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model") && (FourLvlSub != "Model"))
                            {
                                dr_final5["Model"] = dr["Model"];
                                //fifthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand") && (FourLvlSub != "Brand"))
                            {
                                dr_final5["Brand"] = dr["ProductDesc"];
                                //fifthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName") && (FourLvlSub != "ProductName"))
                            {
                                dr_final5["ProductName"] = dr["ProductName"];
                                //fifthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode") && (FourLvlSub != "ItemCode"))
                            {
                                dr_final5["ItemCode"] = dr["ItemCode"];
                                //fifthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }
                        }
                        else if (thirdsub.SelectedItem.Text != "None")
                        {
                            if (firstsub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[firstsub.SelectedItem.Text] = fLvlValueTemp;
                                //fLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[firstsub.SelectedItem.Text] = fLvlValueTemp;
                            }
                            if (secondsub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[secondsub.SelectedItem.Text] = sLvlValueTemp;
                                //sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[secondsub.SelectedItem.Text] = sLvlValueTemp;
                            }
                            if (thirdsub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[thirdsub.SelectedItem.Text] = tLvlValueTemp;
                                //tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[thirdsub.SelectedItem.Text] = tLvlValueTemp;
                            }



                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName"))
                            {
                                dr_final5["CategoryName"] = dr["CategoryName"];
                                //frthLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model"))
                            {
                                dr_final5["Model"] = dr["Model"];
                                //frthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand"))
                            {
                                dr_final5["Brand"] = dr["ProductDesc"];
                                //frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName"))
                            {
                                dr_final5["ProductName"] = dr["ProductName"];
                                //frthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode"))
                            {
                                dr_final5["ItemCode"] = dr["ItemCode"];
                                //frthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }

                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName") && (FourLvlSub != "CategoryName"))
                            {
                                dr_final5["CategoryName"] = dr["CategoryName"];
                                //fifthLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model") && (FourLvlSub != "Model"))
                            {
                                dr_final5["Model"] = dr["Model"];
                                //fifthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand") && (FourLvlSub != "Brand"))
                            {
                                dr_final5["Brand"] = dr["ProductDesc"];
                                //fifthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName") && (FourLvlSub != "ProductName"))
                            {
                                dr_final5["ProductName"] = dr["ProductName"];
                                //fifthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode") && (FourLvlSub != "ItemCode"))
                            {
                                dr_final5["ItemCode"] = dr["ItemCode"];
                                //fifthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }

                        }
                        else if (secondsub.SelectedItem.Text != "None")
                        {
                            if (firstsub.SelectedItem.Text != "None")
                            {
                                dr_final5[firstsub.SelectedItem.Text] = fLvlValueTemp;
                            }
                            else
                            {
                                dr_final5[firstsub.SelectedItem.Text] = fLvlValueTemp;
                            }
                            if (secondsub.SelectedItem.Text != "None")
                            {
                                dr_final5[secondsub.SelectedItem.Text] = sLvlValueTemp;
                            }
                            else
                            {
                                dr_final5[secondsub.SelectedItem.Text] = sLvlValueTemp;
                            }
                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName"))
                            {
                                dr_final5["CategoryName"] = dr["CategoryName"];
                                //tLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model"))
                            {
                                dr_final5["Model"] = dr["Model"];
                                //tLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand"))
                            {
                                dr_final5["Brand"] = dr["ProductDesc"];
                                //tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName"))
                            {
                                dr_final5["ProductName"] = dr["ProductName"];
                                //tLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode"))
                            {
                                dr_final5["ItemCode"] = dr["ItemCode"];
                                //tLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }

                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName"))
                            {
                                dr_final5["CategoryName"] = dr["CategoryName"];
                                //frthLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model"))
                            {
                                dr_final5["Model"] = dr["Model"];
                                //frthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand"))
                            {
                                dr_final5["Brand"] = dr["ProductDesc"];
                                //frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName"))
                            {
                                dr_final5["ProductName"] = dr["ProductName"];
                                //frthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode"))
                            {
                                dr_final5["ItemCode"] = dr["ItemCode"];
                                //frthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }


                            if ((FLvlSub != "CategoryName") && (SLvlSub != "CategoryName") && (TLvlSub != "CategoryName") && (FourLvlSub != "CategoryName"))
                            {
                                dr_final5["CategoryName"] = dr["CategoryName"];
                                //fifthLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Model") && (SLvlSub != "Model") && (TLvlSub != "Model") && (FourLvlSub != "Model"))
                            {
                                dr_final5["Model"] = dr["Model"];
                                //fifthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "Brand") && (SLvlSub != "Brand") && (TLvlSub != "Brand") && (FourLvlSub != "Brand"))
                            {
                                dr_final5["Brand"] = dr["ProductDesc"];
                                //fifthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ProductName") && (SLvlSub != "ProductName") && (TLvlSub != "ProductName") && (FourLvlSub != "ProductName"))
                            {
                                dr_final5["ProductName"] = dr["ProductName"];
                                //fifthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                            }
                            else if ((FLvlSub != "ItemCode") && (SLvlSub != "ItemCode") && (TLvlSub != "ItemCode") && (FourLvlSub != "ItemCode"))
                            {
                                dr_final5["ItemCode"] = dr["ItemCode"];
                                //fifthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }

                        }
                        else if (firstsub.SelectedItem.Text != "None")
                        {
                            if (firstsub.SelectedItem.Text == "Brand")
                            {
                                dr_final5[firstsub.SelectedItem.Text] = fLvlValueTemp;
                                //fLvlValueTemp = dr[DdlFirstSub.SelectedItem.Text].ToString().ToUpper().Trim();
                            }
                            else
                            {
                                dr_final5[firstsub.SelectedItem.Text] = fLvlValueTemp;
                            }
                            if (FLvlSub == "CategoryName")
                            {
                                dr_final5["Brand"] = dr["ProductDesc"];
                                dr_final5["ProductName"] = dr["ProductName"];
                                dr_final5["Model"] = dr["Model"];
                                dr_final5["ItemCode"] = dr["ItemCode"];
                                //sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                                //tLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                                //frthLvlValueTemp = dr[""].ToString().ToUpper().Trim();
                                //fifthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                            }
                            else if (FLvlSub == "Model")
                            {
                                dr_final5["CategoryName"] = dr["CategoryName"];
                                dr_final5["Brand"] = dr["ProductDesc"];
                                dr_final5["ProductName"] = dr["ProductName"];
                                dr_final5["ItemCode"] = dr["ItemCode"];
                                //sLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                                //tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                                //frthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                                //fifthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();

                            }
                            else if (FLvlSub == "Brand")
                            {
                                dr_final5["CategoryName"] = dr["CategoryName"];
                                dr_final5["Model"] = dr["Model"];
                                dr_final5["ProductName"] = dr["ProductName"];
                                dr_final5["ItemCode"] = dr["ItemCode"];
                                //sLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                                //tLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                                //frthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                                //fifthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();

                            }
                            else if (FLvlSub == "ProductName")
                            {
                                dr_final5["CategoryName"] = dr["CategoryName"];
                                dr_final5["Brand"] = dr["ProductDesc"];
                                dr_final5["Model"] = dr["Model"];
                                dr_final5["ItemCode"] = dr["ItemCode"];
                                //sLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                                //tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                                //frthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                                //fifthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();

                            }
                            else if (FLvlSub == "ItemCode")
                            {
                                dr_final5["CategoryName"] = dr["CategoryName"];
                                dr_final5["Brand"] = dr["ProductDesc"];
                                dr_final5["Model"] = dr["Model"];
                                dr_final5["ProductName"] = dr["ProductName"];
                                //sLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                                //tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                                //frthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                                //fifthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();

                            }
                        }

                        if (chkboxPurchaseReturn.Checked == true)
                        {
                            dr_final5["PurchaseReturn"] = dr["PurchaseReturn"];
                        }
                        if (chkboxInternalTransfer.Checked == true)
                        {
                            dr_final5["InternalTransfer"] = dr["InternalTransfer"];
                        }
                        if (ChkboxPaymode.Checked == true)
                        {

                            if (eightLvlValueTemp == "1")
                            {
                                dr_final5["Paymode"] = "Cash";
                            }
                            else if (eightLvlValueTemp == "2")
                            {
                                dr_final5["Paymode"] = "Bank/Credit";
                            }
                            else if (eightLvlValueTemp == "3")
                            {
                                dr_final5["Paymode"] = "Credit";
                            }

                            //dr_final5["Paymode"] = dr["Paymode"];
                        }
                        if (chkboxCustomer.Checked == true)
                        {
                            dr_final5["CustomerName"] = dr["CustomerName"];

                        }
                        if (chkboxBillno.Checked == true)
                        {
                            dr_final5["Billno"] = dr["Billno"];

                        }
                        if (chkboxBillDate.Checked == true)
                        {
                            dr_final5["BillDate"] = dr["BillDate"];

                        }
                        if (ChkboxCustaddr.Checked == true)
                        {
                            dr_final5["CustomerAddress"] = dr["Add1"];

                        }
                        if (ChkboxCustphone.Checked == true)
                        {
                            dr_final5["CustomerContacts"] = dr["CustomerContacts"];

                        }
                        if (ChkboxEmpname.Checked == true)
                        {
                            dr_final5["empFirstName"] = dr["empFirstName"];
                        }

                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final5["Discount"] = dr["Discount"];
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final5["Freight"] = dr["Freight"];
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final5["Qty"] = dr["Qty"];
                        }

                        // dr_final1["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final5["Rate"] = dr["Rate"];
                        }
                        dr_final5["Amount"] = dr["Amount"];

                        dt.Rows.Add(dr_final5);
                    }
                }



                if (chkboxStock.Checked == true)
                {
                    Gttl = Gttl + Convert.ToInt32(dr["Qty"]);
                }
                if (chkboxRate.Checked == true)
                {
                    Gtotal = Gtotal + Convert.ToDecimal(dr["Amount"]);
                    modelTotal = modelTotal + Convert.ToDecimal(dr["Amount"]);
                    catIDTotal = catIDTotal + Convert.ToDecimal(dr["Amount"]);
                    Pttls = Pttls + Convert.ToDecimal(dr["Amount"]);
                    brandTotal = brandTotal + Convert.ToDecimal(dr["Amount"]);
                    brandTotal1 = brandTotal1 + Convert.ToDecimal(dr["Rate"]);
                    brandTotal2 = brandTotal2 + Convert.ToDecimal(dr["Rate"]);
                    brandTotal3 = brandTotal3 + Convert.ToDecimal(dr["Rate"]);
                    modelTotal1 = modelTotal1 + Convert.ToDecimal(dr["Amount"]);
                }
                brandTotal4 = brandTotal4 + Convert.ToDecimal(dr["Amount"]);

            }




            //Display the last Total and Grand Total
            if (dispLastTotal)
            {
                if ((firstsub.SelectedItem.Text != "None") || (secondsub.SelectedItem.Text != "None") || (thirdsub.SelectedItem.Text != "None") || (foursub.SelectedItem.Text != "None") || (fivesub.SelectedItem.Text != "None"))
                {
                    if (chkboxAll.Checked == true)
                    {
                        if (fivesub.SelectedItem.Text != "None")
                        {
                            DataRow dr_final888 = dt.NewRow();
                            dt.Rows.Add(dr_final888);

                            DataRow dr_final17 = dt.NewRow();


                            if (firstsub.SelectedItem.Text != "None")
                            {
                                dr_final17[firstsub.SelectedItem.Text] = "";
                            }
                            if (secondsub.SelectedItem.Text != "None")
                            {
                                dr_final17[secondsub.SelectedItem.Text] = "";
                            }
                            if (thirdsub.SelectedItem.Text != "None")
                            {
                                dr_final17[thirdsub.SelectedItem.Text] = "";
                            }
                            if (foursub.SelectedItem.Text != "None")
                            {
                                dr_final17[foursub.SelectedItem.Text] = "";
                            }
                            if (fivesub.SelectedItem.Text != "None")
                            {
                                dr_final17[fivesub.SelectedItem.Text] = "Total : " + fifLvlValueTemp;
                            }
                            if (chkboxBillDate.Checked == true)
                            {
                                dr_final17["BillDate"] = "";
                            }
                            if (chkboxCustomer.Checked == true)
                            {
                                dr_final17["CustomerName"] = "";

                            }
                            if (ChkboxPaymode.Checked == true)
                            {
                                if (eightLvlValueTemp == "1")
                                {
                                    dr_final17["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "2")
                                {
                                    dr_final17["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "3")
                                {
                                    dr_final17["Paymode"] = "";
                                }

                                //dr_final5["Paymode"] = dr["Paymode"];
                            }
                            if (chkboxPurchaseReturn.Checked == true)
                            {
                                dr_final17["PurchaseReturn"] = "";
                            }
                            if (chkboxInternalTransfer.Checked == true)
                            {
                                dr_final17["InternalTransfer"] = "";
                            }

                            if (chkboxBillno.Checked == true)
                            {
                                dr_final17["Billno"] = "";

                            }

                            if (ChkboxCustaddr.Checked == true)
                            {
                                dr_final17["CustomerAddress"] = "";

                            }
                            if (ChkboxCustphone.Checked == true)
                            {
                                dr_final17["CustomerContacts"] = "";

                            }
                            if (ChkboxEmpname.Checked == true)
                            {
                                dr_final17["empFirstName"] = "";
                            }

                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final17["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final17["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final17["Qty"] = "";
                            }

                            if (chkboxRate.Checked == true)
                            {
                                dr_final17["Rate"] = "";
                            }


                            if (FiveLvlSUb == "CategoryName")
                            {
                                dr_final17["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                                catIDTotal = 0;
                            }
                            else if (FiveLvlSUb == "Brand")
                            {
                                dr_final17["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                                brandTotal = 0;
                            }
                            else if (FiveLvlSUb == "ProductName")
                            {
                                dr_final17["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                                modelTotal = 0;
                            }
                            else if (FiveLvlSUb == "Model")
                            {
                                dr_final17["Amount"] = Convert.ToString(Convert.ToDecimal(Pttls));
                                Pttls = 0;
                            }
                            else if (FiveLvlSUb == "ItemCode")
                            {
                                dr_final17["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal1));
                                modelTotal1 = 0;
                            }
                            dt.Rows.Add(dr_final17);
                            modelTotal1 = 0;
                        }



                        if (foursub.SelectedItem.Text != "None")
                        {
                            DataRow dr_final8881 = dt.NewRow();
                            dt.Rows.Add(dr_final8881);

                            DataRow dr_final7 = dt.NewRow();

                            if (firstsub.SelectedItem.Text != "None")
                            {
                                dr_final7[firstsub.SelectedItem.Text] = "";
                            }
                            if (secondsub.SelectedItem.Text != "None")
                            {
                                dr_final7[secondsub.SelectedItem.Text] = "";
                            }
                            if (thirdsub.SelectedItem.Text != "None")
                            {
                                dr_final7[thirdsub.SelectedItem.Text] = "";
                            }
                            if (foursub.SelectedItem.Text != "None")
                            {
                                dr_final7[foursub.SelectedItem.Text] = "Total : " + frthLvlValueTemp;
                            }
                            if (fivesub.SelectedItem.Text != "None")
                            {
                                dr_final7[fivesub.SelectedItem.Text] = "";
                            }
                            if (chkboxBillDate.Checked == true)
                            {
                                dr_final7["BillDate"] = "";
                            }
                            if (chkboxCustomer.Checked == true)
                            {
                                dr_final7["CustomerName"] = "";

                            }
                            if (ChkboxPaymode.Checked == true)
                            {
                                if (eightLvlValueTemp == "1")
                                {
                                    dr_final7["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "2")
                                {
                                    dr_final7["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "3")
                                {
                                    dr_final7["Paymode"] = "";
                                }

                                //dr_final5["Paymode"] = dr["Paymode"];
                            }
                            if (chkboxPurchaseReturn.Checked == true)
                            {
                                dr_final7["PurchaseReturn"] = "";
                            }
                            if (chkboxInternalTransfer.Checked == true)
                            {
                                dr_final7["InternalTransfer"] = "";
                            }

                            if (chkboxBillno.Checked == true)
                            {
                                dr_final7["Billno"] = "";

                            }

                            if (ChkboxCustaddr.Checked == true)
                            {
                                dr_final7["CustomerAddress"] = "";

                            }
                            if (ChkboxCustphone.Checked == true)
                            {
                                dr_final7["CustomerContacts"] = "";

                            }
                            if (ChkboxEmpname.Checked == true)
                            {
                                dr_final7["empFirstName"] = "";
                            }

                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = "";
                            }


                            if (FourLvlSub == "CategoryName")
                            {
                                dr_final7["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                                catIDTotal = 0;
                            }
                            else if (FourLvlSub == "Brand")
                            {
                                dr_final7["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                                brandTotal = 0;
                            }
                            else if (FourLvlSub == "ProductName")
                            {
                                dr_final7["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                                modelTotal = 0;
                            }
                            else if (FourLvlSub == "Model")
                            {
                                dr_final7["Amount"] = Convert.ToString(Convert.ToDecimal(Pttls));
                                Pttls = 0;
                            }
                            else if (FourLvlSub == "ItemCode")
                            {
                                dr_final7["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal1));
                                modelTotal1 = 0;
                            }

                            dt.Rows.Add(dr_final7);




                            Pttls = 0;
                        }



                        if (thirdsub.SelectedItem.Text != "None")
                        {
                            DataRow dr_final8881 = dt.NewRow();
                            dt.Rows.Add(dr_final8881);

                            DataRow dr_final8 = dt.NewRow();

                            if (firstsub.SelectedItem.Text != "None")
                            {
                                dr_final8[firstsub.SelectedItem.Text] = "";
                            }
                            if (secondsub.SelectedItem.Text != "None")
                            {
                                dr_final8[secondsub.SelectedItem.Text] = "";
                            }
                            if (thirdsub.SelectedItem.Text != "None")
                            {
                                dr_final8[thirdsub.SelectedItem.Text] = "Total : " + tLvlValueTemp;
                            }
                            if (foursub.SelectedItem.Text != "None")
                            {
                                dr_final8[foursub.SelectedItem.Text] = "";
                            }
                            if (fivesub.SelectedItem.Text != "None")
                            {
                                dr_final8[fivesub.SelectedItem.Text] = "";
                            }

                            if (chkboxBillDate.Checked == true)
                            {
                                dr_final8["BillDate"] = "";
                            }
                            if (chkboxCustomer.Checked == true)
                            {
                                dr_final8["CustomerName"] = "";

                            }
                            if (ChkboxPaymode.Checked == true)
                            {
                                if (eightLvlValueTemp == "1")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "2")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "3")
                                {
                                    dr_final8["Paymode"] = "";
                                }

                                //dr_final5["Paymode"] = dr["Paymode"];
                            }
                            if (chkboxPurchaseReturn.Checked == true)
                            {
                                dr_final8["PurchaseReturn"] = "";
                            }
                            if (chkboxInternalTransfer.Checked == true)
                            {
                                dr_final8["InternalTransfer"] = "";
                            }

                            if (chkboxBillno.Checked == true)
                            {
                                dr_final8["Billno"] = "";

                            }

                            if (ChkboxCustaddr.Checked == true)
                            {
                                dr_final8["CustomerAddress"] = "";

                            }
                            if (ChkboxCustphone.Checked == true)
                            {
                                dr_final8["CustomerContacts"] = "";

                            }
                            if (ChkboxEmpname.Checked == true)
                            {
                                dr_final8["empFirstName"] = "";
                            }

                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final8["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final8["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final8["Qty"] = "";
                            }

                            if (chkboxRate.Checked == true)
                            {
                                dr_final8["Rate"] = "";
                            }


                            if (TLvlSub == "CategoryName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                                catIDTotal = 0;
                            }
                            else if (TLvlSub == "Brand")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                                brandTotal = 0;
                            }
                            else if (TLvlSub == "ProductName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                                modelTotal = 0;
                            }
                            else if (TLvlSub == "Model")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(Pttls));
                                Pttls = 0;
                            }
                            else if (TLvlSub == "ItemCode")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal1));
                                modelTotal1 = 0;
                            }

                            dt.Rows.Add(dr_final8);


                            modelTotal = 0;
                        }

                        if (secondsub.SelectedItem.Text != "None")
                        {
                            DataRow dr_final8881 = dt.NewRow();
                            dt.Rows.Add(dr_final8881);

                            DataRow dr_final8 = dt.NewRow();

                            if (firstsub.SelectedItem.Text != "None")
                            {
                                dr_final8[firstsub.SelectedItem.Text] = "";
                            }
                            if (secondsub.SelectedItem.Text != "None")
                            {
                                dr_final8[secondsub.SelectedItem.Text] = "Total : " + sLvlValueTemp;
                            }
                            if (thirdsub.SelectedItem.Text != "None")
                            {
                                dr_final8[thirdsub.SelectedItem.Text] = "";
                            }
                            if (foursub.SelectedItem.Text != "None")
                            {
                                dr_final8[foursub.SelectedItem.Text] = "";
                            }
                            if (fivesub.SelectedItem.Text != "None")
                            {
                                dr_final8[fivesub.SelectedItem.Text] = "";
                            }
                            if (chkboxBillDate.Checked == true)
                            {
                                dr_final8["BillDate"] = "";
                            }
                            if (chkboxCustomer.Checked == true)
                            {
                                dr_final8["CustomerName"] = "";

                            }
                            if (ChkboxPaymode.Checked == true)
                            {
                                if (eightLvlValueTemp == "1")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "2")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "3")
                                {
                                    dr_final8["Paymode"] = "";
                                }

                                //dr_final5["Paymode"] = dr["Paymode"];
                            }
                            if (chkboxPurchaseReturn.Checked == true)
                            {
                                dr_final8["PurchaseReturn"] = "";
                            }
                            if (chkboxInternalTransfer.Checked == true)
                            {
                                dr_final8["InternalTransfer"] = "";
                            }

                            if (chkboxBillno.Checked == true)
                            {
                                dr_final8["Billno"] = "";

                            }

                            if (ChkboxCustaddr.Checked == true)
                            {
                                dr_final8["CustomerAddress"] = "";

                            }
                            if (ChkboxCustphone.Checked == true)
                            {
                                dr_final8["CustomerContacts"] = "";

                            }
                            if (ChkboxEmpname.Checked == true)
                            {
                                dr_final8["empFirstName"] = "";
                            }

                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final8["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final8["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final8["Qty"] = "";
                            }

                            if (chkboxRate.Checked == true)
                            {
                                dr_final8["Rate"] = "";
                            }


                            if (SLvlSub == "CategoryName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                                catIDTotal = 0;
                            }
                            else if (SLvlSub == "Brand")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                                brandTotal = 0;
                            }
                            else if (SLvlSub == "ProductName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                                modelTotal = 0;
                            }
                            else if (SLvlSub == "Model")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(Pttls));
                                Pttls = 0;
                            }
                            else if (SLvlSub == "ItemCode")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal1));
                                modelTotal1 = 0;
                            }

                            dt.Rows.Add(dr_final8);


                            brandTotal = 0;
                        }




                        if (firstsub.SelectedItem.Text != "None")
                        {
                            DataRow dr_final8881 = dt.NewRow();
                            dt.Rows.Add(dr_final8881);

                            DataRow dr_final8 = dt.NewRow();

                            if (firstsub.SelectedItem.Text != "None")
                            {
                                dr_final8[firstsub.SelectedItem.Text] = "Total : " + fLvlValueTemp;
                            }
                            if (secondsub.SelectedItem.Text != "None")
                            {
                                dr_final8[secondsub.SelectedItem.Text] = "";
                            }
                            if (thirdsub.SelectedItem.Text != "None")
                            {
                                dr_final8[thirdsub.SelectedItem.Text] = "";
                            } if (foursub.SelectedItem.Text != "None")
                            {
                                dr_final8[foursub.SelectedItem.Text] = "";
                            }
                            if (fivesub.SelectedItem.Text != "None")
                            {
                                dr_final8[fivesub.SelectedItem.Text] = "";
                            }

                            if (chkboxBillDate.Checked == true)
                            {
                                dr_final8["BillDate"] = "";
                            }
                            if (chkboxCustomer.Checked == true)
                            {
                                dr_final8["CustomerName"] = "";

                            }
                            if (ChkboxPaymode.Checked == true)
                            {
                                if (eightLvlValueTemp == "1")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "2")
                                {
                                    dr_final8["Paymode"] = "";
                                }
                                else if (eightLvlValueTemp == "3")
                                {
                                    dr_final8["Paymode"] = "";
                                }

                                //dr_final5["Paymode"] = dr["Paymode"];
                            }
                            if (chkboxPurchaseReturn.Checked == true)
                            {
                                dr_final8["PurchaseReturn"] = "";
                            }
                            if (chkboxInternalTransfer.Checked == true)
                            {
                                dr_final8["InternalTransfer"] = "";
                            }

                            if (chkboxBillno.Checked == true)
                            {
                                dr_final8["Billno"] = "";

                            }

                            if (ChkboxCustaddr.Checked == true)
                            {
                                dr_final8["CustomerAddress"] = "";

                            }
                            if (ChkboxCustphone.Checked == true)
                            {
                                dr_final8["CustomerContacts"] = "";

                            }
                            if (ChkboxEmpname.Checked == true)
                            {
                                dr_final8["empFirstName"] = "";
                            }

                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final8["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final8["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final8["Qty"] = "";
                            }

                            if (chkboxRate.Checked == true)
                            {
                                dr_final8["Rate"] = "";
                            }


                            if (FLvlSub == "CategoryName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                                catIDTotal = 0;
                            }
                            else if (FLvlSub == "Brand")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                                brandTotal = 0;
                            }
                            else if (FLvlSub == "ProductName")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                                modelTotal = 0;
                            }
                            else if (FLvlSub == "Model")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(Pttls));
                                Pttls = 0;
                            }
                            else if (FLvlSub == "ItemCode")
                            {
                                dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal1));
                                modelTotal1 = 0;
                            }

                            dt.Rows.Add(dr_final8);

                            catIDTotal = 0;
                        }

                    }

                    DataRow dr_final88 = dt.NewRow();
                    dt.Rows.Add(dr_final88);

                    DataRow dr_final6 = dt.NewRow();
                    if (firstsub.SelectedItem.Text != "None")
                    {
                        dr_final6[firstsub.SelectedItem.Text] = "Grand Total: ";
                    }
                    if (secondsub.SelectedItem.Text != "None")
                    {
                        dr_final6[secondsub.SelectedItem.Text] = "";
                    }
                    if (thirdsub.SelectedItem.Text != "None")
                    {
                        dr_final6[thirdsub.SelectedItem.Text] = "";
                    }
                    if (foursub.SelectedItem.Text != "None")
                    {
                        dr_final6[foursub.SelectedItem.Text] = "";
                    }
                    if (fivesub.SelectedItem.Text != "None")
                    {
                        dr_final6[fivesub.SelectedItem.Text] = "";
                    }

                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final6["Discount"] = "";
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final6["Freight"] = "";
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final6["Qty"] = Convert.ToString(Convert.ToDecimal(Gttl));
                    }
                    if (chkboxRate.Checked == true)
                    {
                        dr_final6["Amount"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                    }
                    dt.Rows.Add(dr_final6);

                }
                else if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
                {
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFourthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFifthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSixthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlSeventhLvl.SelectedItem.Text] = "Total:" + svthLvlValueTemp;

                        /* if (selLevels.IndexOf("CustomerID") < 0)
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
                             dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; 
                        //dr_final7["BillNo"] = "";
                        //dr_final7["Internaltransfer"] = ""; 
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /* dr_final7["Discount"] = "";
                         dr_final7["Freight"] = "";
                         dr_final7["Qty"] = "";
                         dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(Pttls));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //  dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(Pttls));
                        }
                        dt.Rows.Add(dr_final7);
                        Pttls = 0;
                    }

                    if (ddlSixthLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFourthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFifthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlSixthLvl.SelectedItem.Text] = "Total:" + sixLvlValueTemp;

                        /* if (selLevels.IndexOf("CustomerID") < 0)
                             dr_final7["CustomerID"] = "";
                         if (selLevels.IndexOf("BillDate") < 0)
                             dr_final7["BillDate"] = "";
                         if (selLevels.IndexOf("PayMode") < 0)
                             dr_final7["PayMode"] = "";
                         if (selLevels.IndexOf("ProductDesc") < 0)
                             dr_final7["Brand"] = "";
                         if (selLevels.IndexOf("CategoryID") < 0)
                             dr_final7["CategoryID"] = "";
                         if (selLevels.IndexOf("ProductName") < 0)
                             dr_final7["ProductName"] = "";
                         if (selLevels.IndexOf("ItemCode") < 0)
                             dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /*  dr_final7["Discount"] = "";
                          dr_final7["Freight"] = "";
                          dr_final7["Qty"] = "";
                          dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //   dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                        }
                        dt.Rows.Add(dr_final7);
                        modelTotal = 0;
                    }

                    if (ddlFifthLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFourthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlFifthLvl.SelectedItem.Text] = "Total:" + fifLvlValueTemp;
                        /* if (selLevels.IndexOf("CustomerID") < 0)
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
                             dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /*dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //   dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                        }
                        dt.Rows.Add(dr_final7);
                        catIDTotal = 0;
                    }

                    if (ddlFourthLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlFourthLvl.SelectedItem.Text] = "Total:" + frthLvlValueTemp;

                        /* if (selLevels.IndexOf("CustomerID") < 0)
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
                             dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /* dr_final7["Discount"] = "";
                         dr_final7["Freight"] = "";
                         dr_final7["Qty"] = "";
                         dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                        }
                        dt.Rows.Add(dr_final7);
                        brandTotal = 0;
                    }

                    if (ddlThirdLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlThirdLvl.SelectedItem.Text] = "Total:" + tLvlValueTemp;
                        /*if (selLevels.IndexOf("CustomerID") < 0)
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
                            dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /*  dr_final7["Discount"] = "";
                          dr_final7["Freight"] = "";
                          dr_final7["Qty"] = "";
                          dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //    dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));
                        }
                        dt.Rows.Add(dr_final7);
                        brandTotal1 = 0;
                    }

                    if (ddlSecondLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlSecondLvl.SelectedItem.Text] = "Total:" + sLvlValueTemp; ;
                        /*  if (selLevels.IndexOf("CustomerID") < 0)
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
                              dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /*dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //    dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));
                        }
                        dt.Rows.Add(dr_final7);
                        brandTotal2 = 0;
                    }
                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        dr_final7[ddlFirstLvl.SelectedItem.Text] = "Total:" + fLvlValueTemp;
                        /* if (selLevels.IndexOf("CustomerID") < 0)
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
                             dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /*dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //    dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));
                        }
                        dt.Rows.Add(dr_final7);
                        brandTotal3 = 0;
                    }

                    DataRow dr_final6 = dt.NewRow();
                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlFirstLvl.SelectedItem.Text] = "Grand Total: ";
                    }
                    if (ddlSecondLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlSecondLvl.SelectedItem.Text] = "";
                    }
                    if (ddlThirdLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlThirdLvl.SelectedItem.Text] = "";
                    }
                    if (ddlFourthLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlFourthLvl.SelectedItem.Text] = "";
                    }
                    if (ddlFifthLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlFifthLvl.SelectedItem.Text] = "";
                    }
                    if (ddlSixthLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlSixthLvl.SelectedItem.Text] = "";
                    }
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlSeventhLvl.SelectedItem.Text] = "";
                    }
                    /* if (selLevels.IndexOf("CustomerID") < 0)
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
                         dr_final6["ItemCode"] = "";*/

                    //dr_final6["Model"] = "Grand Total: ";
                    //dr_final6["BillNo"] = "";
                    //dr_final6["Internaltransfer"] = "";
                    //dr_final6["purchaseReturn"] = "";
                    //dr_final6["CustomerAddress"] = "";
                    //dr_final6["CustomerContacts"] = "";
                    //dr_final6["empFirstName"] = "";
                    /*dr_final6["Discount"] = "Grand Total :";
                    dr_final6["Freight"] = Convert.ToString(Convert.ToDecimal(Gttl)); ;
                    dr_final6["Qty"] = "";
                    dr_final6["Rate"] = Convert.ToString(Convert.ToDecimal(Gtotal));*/
                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final6["Discount"] = "";
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final6["Freight"] = "";
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final6["Qty"] = Convert.ToString(Convert.ToDecimal(Gttl));
                    }

                    //    dr_final7["Amount"] = "";
                    if (chkboxRate.Checked == true)
                    {
                        dr_final6["Rate"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                    }
                    dt.Rows.Add(dr_final6);
                }

                else if ((chkboxCategory.Checked == true) || (chkboxBrand.Checked == true) || (chkboxProductCode.Checked == true) || (chkboxProductName.Checked == true) || (chkboxInternalTransfer.Checked == true) || (chkboxBillDate.Checked == true) || (chkboxCustomer.Checked == true) || (ChkboxPaymode.Checked == true) || (chkboxPurchaseReturn.Checked == true) || (chkboxModel.Checked == true) || (ChkboxCustaddr.Checked == true) || (ChkboxCustphone.Checked == true) || (ChkboxEmpname.Checked == true) || (chkboxBillno.Checked == true))
                {

                    DataRow dr_final6 = dt.NewRow();
                    if (chkboxCategory.Checked == true)
                    {
                        dr_final6["CategoryName"] = "Grand Total: ";
                    }
                    if (chkboxPurchaseReturn.Checked == true)
                    {
                        dr_final6["PurchaseReturn"] = "";
                    }
                    if (chkboxCustomer.Checked == true)
                    {
                        dr_final6["CustomerName"] = "";
                    }
                    if (chkboxBillDate.Checked == true)
                    {
                        dr_final6["BillDate"] = "";
                    }
                    if (chkboxBrand.Checked == true)
                    {
                        dr_final6["Brand"] = "";
                    }
                    if (chkboxProductCode.Checked == true)
                    {
                        dr_final6["ItemCode"] = "";
                    }
                    if (ChkboxPaymode.Checked == true)
                    {
                        dr_final6["Paymode"] = "";
                    }
                    if (chkboxInternalTransfer.Checked == true)
                    {
                        dr_final6["InternalTransfer"] = "";
                    }
                    if (chkboxProductName.Checked == true)
                    {
                        dr_final6["ProductName"] = "";
                    } if (chkboxBillno.Checked == true)
                    {
                        dr_final6["Billno"] = "";

                    }
                    if (chkboxModel.Checked == true)
                    {
                        dr_final6["Model"] = "";

                    }
                    if (ChkboxCustaddr.Checked == true)
                    {
                        dr_final6["CustomerAddress"] = "";

                    }
                    if (ChkboxCustphone.Checked == true)
                    {
                        dr_final6["CustomerContacts"] = "";

                    }
                    if (ChkboxEmpname.Checked == true)
                    {
                        dr_final6["empFirstName"] = "";

                    }
                    /*    if (selLevels.IndexOf("CustomerID") < 0)
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
                            dr_final6["ItemCode"] = "";*/

                    //dr_final6["Model"] = "Grand Total: ";
                    ////dr_final6["BillNo"] = "";
                    // dr_final6["Internaltransfer"] = "";
                    // dr_final6["purchaseReturn"] = "";
                    //dr_final6["CustomerAddress"] = "";
                    //dr_final6["CustomerContacts"] = "";
                    //dr_final6["empFirstName"] = "";
                    /*   dr_final6["Discount"] = "";
                       dr_final6["Freight"] = Convert.ToString(Convert.ToDecimal(Gttl)); ;
                       dr_final6["Qty"] = "";
                       dr_final6["Rate"] = Convert.ToString(Convert.ToDecimal(Gtotal));*/
                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final6["Discount"] = "";
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final6["Freight"] = "";
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final6["Qty"] = Convert.ToString(Convert.ToDecimal(Gttl));
                    }

                    //    dr_final7["Amount"] = "";
                    if (chkboxRate.Checked == true)
                    {
                        dr_final6["Rate"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                    }
                    dt.Rows.Add(dr_final6);
                }
            }
            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }


    public void bindDataSubTot1(string selColumn, string field2, string cond, string groupBy, string sordrby)
    {
        bool dispLastTotal = false;
        string condtion = "";
        condtion = getCond();
        getgroupByAndselColumn();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = objBL.getSales1Sub(selColumn, field2, condtion, groupBy, sordrby);

        if (ds.Tables[0].Rows.Count > 0)
        {
            if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
            {
                if (ddlFirstLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlFirstLvl.SelectedItem.Text));
                }
                if (ddlSecondLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlSecondLvl.SelectedItem.Text));
                }
                if (ddlThirdLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlThirdLvl.SelectedItem.Text));
                }
                if (ddlFourthLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlFourthLvl.SelectedItem.Text));
                }
                if (ddlFifthLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlFifthLvl.SelectedItem.Text));
                }
                if (ddlSixthLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlSixthLvl.SelectedItem.Text));
                }
                if (ddlSeventhLvl.SelectedItem.Text != "None")
                {
                    dt.Columns.Add(new DataColumn(ddlSeventhLvl.SelectedItem.Text));
                }
                if (chkboxStock.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Qty"));
                }
                if (chkboxFreight.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Freight"));
                }
                if (chkboxDiscount.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Discount"));
                }
                if (chkboxRate.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Rate"));
                }

                dt.Columns.Add(new DataColumn("Amount"));
            }
            else if ((chkboxCategory.Checked == true) || (chkboxBrand.Checked == true) || (chkboxProductCode.Checked == true) || (chkboxProductName.Checked == true) || (chkboxInternalTransfer.Checked == true) || (chkboxBillDate.Checked == true) || (chkboxCustomer.Checked == true) || (ChkboxPaymode.Checked == true) || (chkboxPurchaseReturn.Checked == true) || (chkboxModel.Checked == true) || (ChkboxCustaddr.Checked == true) || (ChkboxCustphone.Checked == true) || (ChkboxEmpname.Checked == true) || (chkboxBillno.Checked == true) || (chkboxStock.Checked == true) || (chkboxRate.Checked == true) || (chkboxDiscount.Checked == true) || (chkboxFreight.Checked == true))
            {

                if (chkboxCategory.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CategoryName"));
                }
                if (chkboxBrand.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Brand"));
                }
                if (chkboxProductName.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("ProductName"));
                }
                if (chkboxModel.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Model"));
                }
                if (chkboxProductCode.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("ItemCode"));
                }

                if (chkboxPurchaseReturn.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("PurchaseReturn"));
                }
                if (chkboxInternalTransfer.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("InternalTransfer"));
                }
                if (chkboxBillDate.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("BillDate"));
                }
                if (chkboxCustomer.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CustomerName"));
                }
                if (ChkboxPaymode.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Paymode"));
                }
                if (chkboxBillno.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("BillNo"));
                }

                if (ChkboxCustaddr.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CustomerAddress"));
                }
                if (ChkboxCustphone.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CustomerContacts"));
                }
                if (ChkboxEmpname.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("empFirstName"));
                }

                if (chkboxStock.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Qty"));
                }
                if (chkboxFreight.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Freight"));
                }
                if (chkboxDiscount.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Discount"));
                }
                if (chkboxRate.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Rate"));
                }
                dt.Columns.Add(new DataColumn("Amount"));

            }


            //initialize column values for entire row
            string fLvlValue = "", sLvlValue = "", tLvlValue = "", frthLvlValue = "", fifLvlValue = "", sixLvlValue = "", svthLvlValue = "", eightLvlValue = "", ninthLvlValue = "", tenthLvlValue = "", eleventhLvlValue = "", twelthLvlValue = "", thirteenLvlValue = "", fourteenLvlValue = "";
            string fLvlValueTemp = "", sLvlValueTemp = "", tLvlValueTemp = "", frthLvlValueTemp = "", fifLvlValueTemp = "", sixLvlValueTemp = "", svthLvlValueTemp = "", eightLvlValueTemp = "", ninthLvlValueTemp = "", tenthLvlValueTemp = "", eleventhLvlValueTemp = "", twelthLvlValueTemp = "", thirteenLvlValueTemp = "", fourteenLvlValueTemp = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
                {
                    //initialize column values for entire row
                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFirstLvl.SelectedItem.Text == "Brand")
                        {
                            fLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            fLvlValueTemp = dr[ddlFirstLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    }
                    if (ddlSecondLvl.SelectedItem.Text != "None")
                        if (ddlSecondLvl.SelectedItem.Text == "Brand")
                        {
                            sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            sLvlValueTemp = dr[ddlSecondLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlThirdLvl.SelectedItem.Text != "None")
                        if (ddlThirdLvl.SelectedItem.Text == "Brand")
                        {
                            tLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            tLvlValueTemp = dr[ddlThirdLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlFourthLvl.SelectedItem.Text != "None")
                        if (ddlFourthLvl.SelectedItem.Text == "Brand")
                        {
                            frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            frthLvlValueTemp = dr[ddlFourthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlFifthLvl.SelectedItem.Text != "None")
                        if (ddlFifthLvl.SelectedItem.Text == "Brand")
                        {
                            fifLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            fifLvlValueTemp = dr[ddlFifthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlSixthLvl.SelectedItem.Text != "None")
                        if (ddlSixthLvl.SelectedItem.Text == "Brand")
                        {
                            sixLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            sixLvlValueTemp = dr[ddlSixthLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                        if (ddlSeventhLvl.SelectedItem.Text == "Brand")
                        {
                            svthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                        }
                        else
                        {
                            svthLvlValueTemp = dr[ddlSeventhLvl.SelectedItem.Text].ToString().ToUpper().Trim();
                        }

                    dispLastTotal = true;

                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                            (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                            (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp) ||
                            (sixLvlValue != "" && sixLvlValue != sixLvlValueTemp) ||
                            (svthLvlValue != "" && svthLvlValue != svthLvlValueTemp) || (eightLvlValueTemp != "" && eightLvlValue != eightLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "Total:" + svthLvlValue;
                            }
                            /*  if (selLevels.IndexOf("CustomerID") < 0)
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
                                  dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(Pttls));
                            }
                            dt.Rows.Add(dr_final7);
                            Pttls = 0;
                        }
                    }

                    if (ddlSixthLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                            (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                            (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp) ||
                            (sixLvlValue != "" && sixLvlValue != sixLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "Total:" + sixLvlValue;
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /*  if (selLevels.IndexOf("CustomerID") < 0)
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
                                  dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /*dr_final7["Discount"] = "";
                            dr_final7["Freight"] = "";
                            dr_final7["Qty"] = "";
                            dr_final7["Amount"] = "";
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                            }
                            dt.Rows.Add(dr_final7);
                            modelTotal = 0;
                        }
                    }

                    if (ddlFifthLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                           (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                           (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                           (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
                           (fifLvlValue != "" && fifLvlValue != fifLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "Total:" + fifLvlValue;
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /* if (selLevels.IndexOf("CustomerID") < 0)
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
                                 dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /*dr_final7["Discount"] = "";
                            dr_final7["Freight"] = "";
                            dr_final7["Qty"] = "";
                            dr_final7["Amount"] = "";
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                            }
                            dt.Rows.Add(dr_final7);
                            catIDTotal = 0;
                        }
                    }

                    if (ddlFourthLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                           (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                           (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                           (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "Total:" + frthLvlValue;
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /* if (selLevels.IndexOf("CustomerID") < 0)
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
                                 dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /*   dr_final7["Discount"] = "";
                               dr_final7["Freight"] = "";
                               dr_final7["Qty"] = "";
                               dr_final7["Amount"] = "";
                               dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                            }
                            dt.Rows.Add(dr_final7);
                            brandTotal = 0;
                        }
                    }

                    if (ddlThirdLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                           (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                           (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "Total:" + tLvlValue;
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /* if (selLevels.IndexOf("CustomerID") < 0)
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
                                 dr_final7["ItemCode"] = "";*/

                            // dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /*  dr_final7["Discount"] = "";
                              dr_final7["Freight"] = "";
                              dr_final7["Qty"] = "";
                              dr_final7["Amount"] = "";
                              dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));
                            }
                            dt.Rows.Add(dr_final7);
                            brandTotal1 = 0;
                        }
                    }

                    if (ddlSecondLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                           (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "Total:" + sLvlValue;
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /* if (selLevels.IndexOf("CustomerID") < 0)
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
                                 dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /* dr_final7["Discount"] = "";
                             dr_final7["Freight"] = "";
                             dr_final7["Qty"] = "";
                             dr_final7["Amount"] = "";
                             dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));
                            }
                            dt.Rows.Add(dr_final7);
                            brandTotal2 = 0;
                        }
                    }

                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (ddlFirstLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFirstLvl.SelectedItem.Text] = "Total:" + fLvlValue;
                            }
                            if (ddlSecondLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                            }
                            if (ddlThirdLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFourthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlFifthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSixthLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                            }
                            if (ddlSeventhLvl.SelectedItem.Text != "None")
                            {
                                dr_final7[ddlSeventhLvl.SelectedItem.Text] = "";
                            }
                            /*if (selLevels.IndexOf("CustomerID") < 0)
                                dr_final7["CustomerID"] = "";*/
                            /*if (selLevels.IndexOf("BillDate") < 0)
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
                                dr_final7["ItemCode"] = "";*/

                            //dr_final7["Model"] = "";
                            //dr_final7["BillNo"] = "";
                            //dr_final7["Internaltransfer"] = "";
                            //dr_final7["purchaseReturn"] = "";
                            //dr_final7["CustomerAddress"] = "";
                            //dr_final7["CustomerContacts"] = "";
                            //dr_final7["empFirstName"] = "";
                            /*  dr_final7["Discount"] = "";
                              dr_final7["Freight"] = "";
                              dr_final7["Qty"] = "";
                              dr_final7["Amount"] = "";
                              dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));*/
                            if (chkboxDiscount.Checked == true)
                            {
                                dr_final7["Discount"] = "";
                            }
                            if (chkboxFreight.Checked == true)
                            {
                                dr_final7["Freight"] = "";
                            }
                            if (chkboxStock.Checked == true)
                            {
                                dr_final7["Qty"] = "";
                            }

                            dr_final7["Amount"] = "";
                            if (chkboxRate.Checked == true)
                            {
                                dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));
                            }
                            dt.Rows.Add(dr_final7);
                            brandTotal3 = 0;
                        }
                    }
                    ///////////////////////////////////////

                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;
                    frthLvlValue = frthLvlValueTemp;
                    fifLvlValue = fifLvlValueTemp;
                    sixLvlValue = sixLvlValueTemp;
                    svthLvlValue = sixLvlValueTemp;
                    DataRow dr_final5 = dt.NewRow();
                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFirstLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlFirstLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlFirstLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                    }

                    if (ddlSecondLvl.SelectedItem.Text != "None")
                    {
                        if (ddlSecondLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlSecondLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlSecondLvl.SelectedItem.Text] = dr["ItemCode"];
                        }

                    }
                    if (ddlThirdLvl.SelectedItem.Text != "None")
                    {
                        if (ddlThirdLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlThirdLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlThirdLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlFourthLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFourthLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlFourthLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlFourthLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlFifthLvl.SelectedItem.Text != "None")
                    {
                        if (ddlFifthLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlFifthLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlFifthLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlSixthLvl.SelectedItem.Text != "None")
                    {
                        if (ddlSixthLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlSixthLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlSixthLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                    {
                        if (ddlSeventhLvl.SelectedItem.Text == "CategoryName")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "CustomerName")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["CustomerName"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "BillDate")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["BillDate"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "ProductName")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "Brand")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "Paymode")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["Paymode"];
                        }
                        if (ddlSeventhLvl.SelectedItem.Text == "ItemCode")
                        {
                            dr_final5[ddlSeventhLvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                    }
                    /*  if (selLevels.IndexOf("CustomerID") < 0)
                          dr_final5["CustomerID"] = dr["CustomerID"];
                      if (selLevels.IndexOf("BillDate") < 0)
                          dr_final5["BillDate"] = dr["BillDate"];
                      if (selLevels.IndexOf("PayMode") < 0)
                          dr_final5["PayMode"] = dr["LedgerName"];
                      if (selLevels.IndexOf("ProductDesc") < 0)
                          dr_final5["ProductDesc"] = dr["ProductDesc"];
                      if (selLevels.IndexOf("CategoryID") < 0)
                          dr_final5["CategoryID"] = dr["CategoryID"];
                      if (selLevels.IndexOf("ProductName") < 0)
                          dr_final5["ProductName"] = dr["ProductName"];
                      if (selLevels.IndexOf("ItemCode") < 0)
                          dr_final5["ItemCode"] = dr["ItemCode"];*/

                    // dr_final5["Model"] = dr["Model"];
                    //dr_final5["BillNo"] = dr["BillNo"];
                    //dr_final5["Internaltransfer"] = dr["Internaltransfer"];
                    //dr_final5["purchaseReturn"] = dr["purchaseReturn"];
                    //dr_final5["CustomerAddress"] = dr["CustomerAddress"];
                    //dr_final5["CustomerContacts"] = dr["CustomerContacts"];
                    //dr_final5["empFirstName"] = dr["empFirstName"];
                    /* dr_final5["Discount"] = dr["Discount"];
                     dr_final5["Freight"] = dr["Freight"];
                     dr_final5["Qty"] = dr["Qty"];
                     dr_final5["Rate"] = dr["Rate"];*/
                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final5["Discount"] = dr["Discount"];
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final5["Freight"] = dr["Freight"];
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final5["Qty"] = dr["Qty"];
                    }

                    // dr_final1["Amount"] = "";
                    if (chkboxRate.Checked == true)
                    {
                        dr_final5["Rate"] = dr["Rate"];
                    }
                    dr_final5["Amount"] = dr["Amount"];
                    dt.Rows.Add(dr_final5);
                    if (chkboxStock.Checked == true)
                    {
                        Gttl = Gttl + Convert.ToInt32(dr["Qty"]);
                    }
                    if (chkboxRate.Checked == true)
                    {
                        Gtotal = Gtotal + Convert.ToDecimal(dr["Rate"]);
                        modelTotal = modelTotal + Convert.ToDecimal(dr["Rate"]);
                        catIDTotal = catIDTotal + Convert.ToDecimal(dr["Rate"]);
                        Pttls = Pttls + Convert.ToDecimal(dr["Rate"]);
                        brandTotal = brandTotal + Convert.ToDecimal(dr["Rate"]);
                        brandTotal1 = brandTotal1 + Convert.ToDecimal(dr["Rate"]);
                        brandTotal2 = brandTotal2 + Convert.ToDecimal(dr["Rate"]);
                        brandTotal3 = brandTotal3 + Convert.ToDecimal(dr["Rate"]);
                    }
                    brandTotal4 = brandTotal4 + Convert.ToDecimal(dr["Amount"]);

                }
                else if ((chkboxCategory.Checked == true) || (chkboxBrand.Checked == true) || (chkboxProductCode.Checked == true) || (chkboxProductName.Checked == true) || (chkboxInternalTransfer.Checked == true) || (chkboxBillDate.Checked == true) || (chkboxCustomer.Checked == true) || (ChkboxPaymode.Checked == true) || (chkboxPurchaseReturn.Checked == true) || (chkboxModel.Checked == true) || (ChkboxCustaddr.Checked == true) || (ChkboxCustphone.Checked == true) || (ChkboxEmpname.Checked == true) || (chkboxBillno.Checked == true) || (chkboxStock.Checked == true) || (chkboxRate.Checked == true) || (chkboxDiscount.Checked == true) || (chkboxFreight.Checked == true))
                {

                    if (chkboxCategory.Checked == true)
                        fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();
                    if (chkboxCustomer.Checked == true)
                        svthLvlValueTemp = dr["CustomerName"].ToString().ToUpper().Trim();
                    if (chkboxBillDate.Checked == true)
                        tLvlValueTemp = dr["BillDate"].ToString().ToUpper().Trim();
                    if (chkboxBrand.Checked == true)
                        frthLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                    if (chkboxProductCode.Checked == true)
                        fifLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                    if (chkboxModel.Checked == true)
                        eleventhLvlValueTemp = dr["Model"].ToString().ToUpper().ToUpper().Trim();
                    if (chkboxPurchaseReturn.Checked == true)
                        sixLvlValueTemp = dr["PurchaseReturn"].ToString().ToUpper().Trim();

                    if (chkboxInternalTransfer.Checked == true)
                        svthLvlValueTemp = dr["InternalTransfer"].ToString().ToUpper().Trim();
                    if (ChkboxPaymode.Checked == true)
                        eightLvlValueTemp = dr["Paymode"].ToString().ToUpper().Trim();
                    if (chkboxProductName.Checked == true)
                        ninthLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                    if (chkboxBillno.Checked == true)
                        tenthLvlValueTemp = dr["Billno"].ToString().ToUpper().Trim();

                    if (ChkboxCustaddr.Checked == true)
                        twelthLvlValueTemp = dr["CustomerAddress"].ToString().ToUpper().Trim();
                    if (ChkboxCustphone.Checked == true)
                        thirteenLvlValueTemp = dr["CustomerContacts"].ToString().ToUpper().Trim();
                    if (ChkboxEmpname.Checked == true)
                        fourteenLvlValueTemp = dr["empFirstName"].ToString().ToUpper().Trim();

                    dispLastTotal = true;





                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;
                    frthLvlValue = frthLvlValueTemp;
                    fifLvlValue = fifLvlValueTemp;
                    sixLvlValue = sixLvlValueTemp;
                    svthLvlValue = svthLvlValueTemp;
                    eightLvlValue = eightLvlValueTemp;
                    ninthLvlValue = ninthLvlValueTemp;
                    tenthLvlValue = tenthLvlValueTemp;
                    eleventhLvlValue = eleventhLvlValueTemp;
                    twelthLvlValue = twelthLvlValueTemp;
                    thirteenLvlValue = thirteenLvlValueTemp;
                    fourteenLvlValue = fourteenLvlValueTemp;
                    DataRow dr_final5 = dt.NewRow();
                    if (chkboxCategory.Checked == true)
                    {

                        dr_final5["CategoryName"] = dr["CategoryName"];
                    }

                    if (chkboxCustomer.Checked == true)
                    {
                        dr_final5["CustomerName"] = dr["CustomerName"];
                    }
                    if (chkboxBillDate.Checked == true)
                    {
                        dr_final5["BillDate"] = dr["BillDate"];
                    }
                    if (chkboxBrand.Checked == true)
                    {
                        dr_final5["Brand"] = dr["ProductDesc"];
                    }
                    if (chkboxProductCode.Checked == true)
                    {
                        dr_final5["ItemCode"] = dr["ItemCode"];
                    }
                    if (chkboxModel.Checked == true)
                    {
                        dr_final5["Model"] = dr["Model"];

                    }
                    if (chkboxPurchaseReturn.Checked == true)
                    {
                        dr_final5["PurchaseReturn"] = dr["PurchaseReturn"];
                    }
                    if (chkboxInternalTransfer.Checked == true)
                    {
                        dr_final5["InternalTransfer"] = dr["InternalTransfer"];
                    }
                    if (ChkboxPaymode.Checked == true)
                    {

                        if (eightLvlValueTemp == "1")
                        {
                            dr_final5["Paymode"] = "Cash";
                        }
                        else if (eightLvlValueTemp == "2")
                        {
                            dr_final5["Paymode"] = "Bank/Credit";
                        }
                        else if (eightLvlValueTemp == "3")
                        {
                            dr_final5["Paymode"] = "Credit";
                        }

                        //dr_final5["Paymode"] = dr["Paymode"];
                    }
                    if (chkboxProductName.Checked == true)
                    {
                        dr_final5["ProductName"] = dr["ProductName"];

                    }
                    if (chkboxBillno.Checked == true)
                    {
                        dr_final5["Billno"] = dr["Billno"];

                    }

                    if (ChkboxCustaddr.Checked == true)
                    {
                        dr_final5["CustomerAddress"] = dr["CustomerAddress"];

                    }
                    if (ChkboxCustphone.Checked == true)
                    {
                        dr_final5["CustomerContacts"] = dr["CustomerContacts"];

                    }
                    if (ChkboxEmpname.Checked == true)
                    {
                        dr_final5["empFirstName"] = dr["empFirstName"];

                    }

                    /*if (selLevels.IndexOf("CustomerID") < 0)
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
                        dr_final5["ItemCode"] = dr["ItemCode"];*/

                    //dr_final5["Model"] = dr["Model"];
                    //dr_final5["BillNo"] = dr["BillNo"];
                    //dr_final5["Internaltransfer"] = dr["Internaltransfer"];
                    // dr_final5["purchaseReturn"] = dr["purchaseReturn"];
                    //dr_final5["CustomerAddress"] = dr["CustomerAddress"];
                    //dr_final5["CustomerContacts"] = dr["CustomerContacts"];
                    // dr_final5["empFirstName"] = dr["empFirstName"];
                    /* dr_final5["Discount"] = dr["Discount"];
                     dr_final5["Freight"] = dr["Freight"];
                     dr_final5["Qty"] = dr["Qty"];
                     dr_final5["Rate"] = dr["Rate"];*/
                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final5["Discount"] = dr["Discount"];
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final5["Freight"] = dr["Freight"];
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final5["Qty"] = dr["Qty"];
                    }

                    // dr_final1["Amount"] = "";
                    if (chkboxRate.Checked == true)
                    {
                        dr_final5["Rate"] = dr["Rate"];
                    }
                    dr_final5["Amount"] = dr["Amount"];
                    dt.Rows.Add(dr_final5);
                    if (chkboxStock.Checked == true)
                    {
                        Gttl = Gttl + Convert.ToInt32(dr["Qty"]);
                    }
                    if (chkboxRate.Checked == true)
                    {
                        Gtotal = Gtotal + Convert.ToDecimal(dr["Rate"]);
                        modelTotal = modelTotal + Convert.ToDecimal(dr["Rate"]);
                        catIDTotal = catIDTotal + Convert.ToDecimal(dr["Rate"]);
                        Pttls = Pttls + Convert.ToDecimal(dr["Rate"]);
                        brandTotal = brandTotal + Convert.ToDecimal(dr["Rate"]);
                        brandTotal1 = brandTotal1 + Convert.ToDecimal(dr["Rate"]);
                        brandTotal2 = brandTotal2 + Convert.ToDecimal(dr["Rate"]);
                        brandTotal3 = brandTotal3 + Convert.ToDecimal(dr["Rate"]);
                        modelTotal1 = modelTotal1 + Convert.ToDecimal(dr["Rate"]);
                    }
                    brandTotal4 = brandTotal4 + Convert.ToDecimal(dr["Amount"]);

                }
            }

            //Display the last Total and Grand Total
            if (dispLastTotal)
            {
                if ((ddlFirstLvl.SelectedItem.Text != "None") || (ddlSecondLvl.SelectedItem.Text != "None") || (ddlThirdLvl.SelectedItem.Text != "None") || (ddlFourthLvl.SelectedItem.Text != "None") || (ddlFifthLvl.SelectedItem.Text != "None") || (ddlSixthLvl.SelectedItem.Text != "None") || (ddlSeventhLvl.SelectedItem.Text != "None"))
                {
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFourthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFifthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSixthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSixthLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlSeventhLvl.SelectedItem.Text] = "Total:" + svthLvlValueTemp;

                        /* if (selLevels.IndexOf("CustomerID") < 0)
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
                             dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; 
                        //dr_final7["BillNo"] = "";
                        //dr_final7["Internaltransfer"] = ""; 
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /* dr_final7["Discount"] = "";
                         dr_final7["Freight"] = "";
                         dr_final7["Qty"] = "";
                         dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(Pttls));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //  dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(Pttls));
                        }
                        dt.Rows.Add(dr_final7);
                        Pttls = 0;
                    }

                    if (ddlSixthLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFourthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFifthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFifthLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlSixthLvl.SelectedItem.Text] = "Total:" + sixLvlValueTemp;

                        /* if (selLevels.IndexOf("CustomerID") < 0)
                             dr_final7["CustomerID"] = "";
                         if (selLevels.IndexOf("BillDate") < 0)
                             dr_final7["BillDate"] = "";
                         if (selLevels.IndexOf("PayMode") < 0)
                             dr_final7["PayMode"] = "";
                         if (selLevels.IndexOf("ProductDesc") < 0)
                             dr_final7["Brand"] = "";
                         if (selLevels.IndexOf("CategoryID") < 0)
                             dr_final7["CategoryID"] = "";
                         if (selLevels.IndexOf("ProductName") < 0)
                             dr_final7["ProductName"] = "";
                         if (selLevels.IndexOf("ItemCode") < 0)
                             dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /*  dr_final7["Discount"] = "";
                          dr_final7["Freight"] = "";
                          dr_final7["Qty"] = "";
                          dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //   dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                        }
                        dt.Rows.Add(dr_final7);
                        modelTotal = 0;
                    }

                    if (ddlFifthLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        if (ddlFourthLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFourthLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlFifthLvl.SelectedItem.Text] = "Total:" + fifLvlValueTemp;
                        /* if (selLevels.IndexOf("CustomerID") < 0)
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
                             dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /*dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //   dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                        }
                        dt.Rows.Add(dr_final7);
                        catIDTotal = 0;
                    }

                    if (ddlFourthLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        if (ddlThirdLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlThirdLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlFourthLvl.SelectedItem.Text] = "Total:" + frthLvlValueTemp;

                        /* if (selLevels.IndexOf("CustomerID") < 0)
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
                             dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /* dr_final7["Discount"] = "";
                         dr_final7["Freight"] = "";
                         dr_final7["Qty"] = "";
                         dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                        }
                        dt.Rows.Add(dr_final7);
                        brandTotal = 0;
                    }

                    if (ddlThirdLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        if (ddlSecondLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlSecondLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlThirdLvl.SelectedItem.Text] = "Total:" + tLvlValueTemp;
                        /*if (selLevels.IndexOf("CustomerID") < 0)
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
                            dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /*  dr_final7["Discount"] = "";
                          dr_final7["Freight"] = "";
                          dr_final7["Qty"] = "";
                          dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //    dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal1));
                        }
                        dt.Rows.Add(dr_final7);
                        brandTotal1 = 0;
                    }

                    if (ddlSecondLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (ddlFirstLvl.SelectedItem.Text != "None")
                        {
                            dr_final7[ddlFirstLvl.SelectedItem.Text] = "";
                        }
                        dr_final7[ddlSecondLvl.SelectedItem.Text] = "Total:" + sLvlValueTemp; ;
                        /*  if (selLevels.IndexOf("CustomerID") < 0)
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
                              dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /*dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //    dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal2));
                        }
                        dt.Rows.Add(dr_final7);
                        brandTotal2 = 0;
                    }
                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        DataRow dr_final7 = dt.NewRow();
                        dr_final7[ddlFirstLvl.SelectedItem.Text] = "Total:" + fLvlValueTemp;
                        /* if (selLevels.IndexOf("CustomerID") < 0)
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
                             dr_final7["ItemCode"] = "";*/

                        //dr_final7["Model"] = ""; ;
                        //dr_final7["BillNo"] = ""; ;
                        //dr_final7["Internaltransfer"] = ""; ;
                        //dr_final7["purchaseReturn"] = "";
                        //dr_final7["CustomerAddress"] = "";
                        //dr_final7["CustomerContacts"] = "";
                        //dr_final7["empFirstName"] = "";
                        /*dr_final7["Discount"] = "";
                        dr_final7["Freight"] = "";
                        dr_final7["Qty"] = "";
                        dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));*/
                        if (chkboxDiscount.Checked == true)
                        {
                            dr_final7["Discount"] = "";
                        }
                        if (chkboxFreight.Checked == true)
                        {
                            dr_final7["Freight"] = "";
                        }
                        if (chkboxStock.Checked == true)
                        {
                            dr_final7["Qty"] = "";
                        }

                        //    dr_final7["Amount"] = "";
                        if (chkboxRate.Checked == true)
                        {
                            dr_final7["Rate"] = Convert.ToString(Convert.ToDecimal(brandTotal3));
                        }
                        dt.Rows.Add(dr_final7);
                        brandTotal3 = 0;
                    }

                    DataRow dr_final6 = dt.NewRow();
                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlFirstLvl.SelectedItem.Text] = "Grand Total: ";
                    }
                    if (ddlSecondLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlSecondLvl.SelectedItem.Text] = "";
                    }
                    if (ddlThirdLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlThirdLvl.SelectedItem.Text] = "";
                    }
                    if (ddlFourthLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlFourthLvl.SelectedItem.Text] = "";
                    }
                    if (ddlFifthLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlFifthLvl.SelectedItem.Text] = "";
                    }
                    if (ddlSixthLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlSixthLvl.SelectedItem.Text] = "";
                    }
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                    {
                        dr_final6[ddlSeventhLvl.SelectedItem.Text] = "";
                    }
                    /* if (selLevels.IndexOf("CustomerID") < 0)
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
                         dr_final6["ItemCode"] = "";*/

                    //dr_final6["Model"] = "Grand Total: ";
                    //dr_final6["BillNo"] = "";
                    //dr_final6["Internaltransfer"] = "";
                    //dr_final6["purchaseReturn"] = "";
                    //dr_final6["CustomerAddress"] = "";
                    //dr_final6["CustomerContacts"] = "";
                    //dr_final6["empFirstName"] = "";
                    /*dr_final6["Discount"] = "Grand Total :";
                    dr_final6["Freight"] = Convert.ToString(Convert.ToDecimal(Gttl)); ;
                    dr_final6["Qty"] = "";
                    dr_final6["Rate"] = Convert.ToString(Convert.ToDecimal(Gtotal));*/
                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final6["Discount"] = "";
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final6["Freight"] = "";
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final6["Qty"] = Convert.ToString(Convert.ToDecimal(Gttl));
                    }

                    //    dr_final7["Amount"] = "";
                    if (chkboxRate.Checked == true)
                    {
                        dr_final6["Rate"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                    }
                    dt.Rows.Add(dr_final6);
                }

                else if ((chkboxCategory.Checked == true) || (chkboxBrand.Checked == true) || (chkboxProductCode.Checked == true) || (chkboxProductName.Checked == true) || (chkboxInternalTransfer.Checked == true) || (chkboxBillDate.Checked == true) || (chkboxCustomer.Checked == true) || (ChkboxPaymode.Checked == true) || (chkboxPurchaseReturn.Checked == true) || (chkboxModel.Checked == true) || (ChkboxCustaddr.Checked == true) || (ChkboxCustphone.Checked == true) || (ChkboxEmpname.Checked == true) || (chkboxBillno.Checked == true))
                {

                    DataRow dr_final6 = dt.NewRow();
                    if (chkboxCategory.Checked == true)
                    {
                        dr_final6["CategoryName"] = "Grand Total: ";
                    }
                    if (chkboxPurchaseReturn.Checked == true)
                    {
                        dr_final6["PurchaseReturn"] = "";
                    }
                    if (chkboxCustomer.Checked == true)
                    {
                        dr_final6["CustomerName"] = "";
                    }
                    if (chkboxBillDate.Checked == true)
                    {
                        dr_final6["BillDate"] = "";
                    }
                    if (chkboxBrand.Checked == true)
                    {
                        dr_final6["Brand"] = "";
                    }
                    if (chkboxProductCode.Checked == true)
                    {
                        dr_final6["ItemCode"] = "";
                    }
                    if (ChkboxPaymode.Checked == true)
                    {
                        dr_final6["Paymode"] = "";
                    }
                    if (chkboxInternalTransfer.Checked == true)
                    {
                        dr_final6["InternalTransfer"] = "";
                    }
                    if (chkboxProductName.Checked == true)
                    {
                        dr_final6["ProductName"] = "";
                    } if (chkboxBillno.Checked == true)
                    {
                        dr_final6["Billno"] = "";

                    }
                    if (chkboxModel.Checked == true)
                    {
                        dr_final6["Model"] = "";

                    }
                    if (ChkboxCustaddr.Checked == true)
                    {
                        dr_final6["CustomerAddress"] = "";

                    }
                    if (ChkboxCustphone.Checked == true)
                    {
                        dr_final6["CustomerContacts"] = "";

                    }
                    if (ChkboxEmpname.Checked == true)
                    {
                        dr_final6["empFirstName"] = "";

                    }
                    /*    if (selLevels.IndexOf("CustomerID") < 0)
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
                            dr_final6["ItemCode"] = "";*/

                    //dr_final6["Model"] = "Grand Total: ";
                    ////dr_final6["BillNo"] = "";
                    // dr_final6["Internaltransfer"] = "";
                    // dr_final6["purchaseReturn"] = "";
                    //dr_final6["CustomerAddress"] = "";
                    //dr_final6["CustomerContacts"] = "";
                    //dr_final6["empFirstName"] = "";
                    /*   dr_final6["Discount"] = "";
                       dr_final6["Freight"] = Convert.ToString(Convert.ToDecimal(Gttl)); ;
                       dr_final6["Qty"] = "";
                       dr_final6["Rate"] = Convert.ToString(Convert.ToDecimal(Gtotal));*/
                    if (chkboxDiscount.Checked == true)
                    {
                        dr_final6["Discount"] = "";
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        dr_final6["Freight"] = "";
                    }
                    if (chkboxStock.Checked == true)
                    {
                        dr_final6["Qty"] = Convert.ToString(Convert.ToDecimal(Gttl));
                    }

                    //    dr_final7["Amount"] = "";
                    if (chkboxRate.Checked == true)
                    {
                        dr_final6["Rate"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                    }
                    dt.Rows.Add(dr_final6);
                }
            }
            ExportToExcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
        }
    }

    protected void savebtn_Click(object sender, EventArgs e)
    {
        try
        {
            if (Savetxtbox.Text == "")
            {
                RequiredFieldValidator5.Enabled = true;
            }
            else
            {
                try
                {

                    int i = 0;
                    int j = 0;
                    objBL.savenames = Savetxtbox.Text;
                    string savename = Savetxtbox.Text;

                    string chkCat = "";
                    string chkBran = "";
                    string chkProd = "";
                    string chkMod = "";
                    string chkitem = "";

                    string chkcustnme = "";
                    string chkcustaddr = "";
                    string chkcustphone = "";
                    string chkpur = "";
                    string chkint = "";
                    string chkbilln = "";
                    string chkbilldt = "";
                    string chkpay = "";
                    string chkemp = "";
                    string chksAll = "";

                    string sdBrand = "";
                    string sdCat = "";
                    string sdProd = "";
                    string sdMod = "";
                    string sditem = "";

                    string sdpay = "";
                    string sdcust = "";
                    string chkdiscount = "";
                    string chkstoc = "";
                    string chkfreight = "";
                    string chksrate = "";
                    string sdfirstlvl = "";
                    string sdsecondlvl = "";
                    string sdthirdlvl = "";
                    string sdfourlvl = "";
                    string sdfifthlvl = "";

                    string sdsixthlvl = "";
                    string sdseventhlvl = "";

                    string sodfirstlvl = "";
                    string sodsecondlvl = "";
                    string sodthirdlvl = "";
                    string sodfourlvl = "";
                    string sodfifthlvl = "";

                    string sodsixthlvl = "";
                    string sodseventhlvl = "";



                    string subfirstlevel = "";
                    string subsecondlevel = "";
                    string subthirdlevel = "";
                    string subfourlevel = "";
                    string subfifthlevel = "";
                    string subsixthlevel = "";
                    string subseventhlevel = "";
                    string subeighthlevel = "";




                    string rblpur = "";
                    string rblint = "";
                    string stdt = "";
                    string endt = "";
                    objBL.rblpurs = rblPurchseRtn.SelectedItem.Text;
                    rblpur = rblPurchseRtn.SelectedItem.Text;
                    objBL.rblints = rblIntrnlTrns.SelectedItem.Text;
                    rblint = rblIntrnlTrns.SelectedItem.Text;
                    objBL.stdts = txtStrtDt.Text;
                    stdt = txtStrtDt.Text;
                    objBL.endts = txtEndDt.Text;
                    endt = txtEndDt.Text;
                    if (chkboxCategory.Checked == true)
                    {
                        objBL.chkCats = "chkCategory";
                        chkCat = "chkCategory";
                    }
                    else
                    {
                        objBL.chkCats = "null";
                        chkCat = "null";
                    }

                    if (chkboxProductName.Checked == true)
                    {
                        objBL.chkProds = "chkProductname";
                        chkProd = "chkProductname";
                    }
                    else
                    {
                        objBL.chkProds = "null";
                        chkProd = "null";
                    }
                    if (chkboxBrand.Checked == true)
                    {
                        objBL.chkBrans = "chkBrand";
                        chkBran = "chkBrand";
                    }
                    else
                    {
                        objBL.chkBrans = "null";
                        chkBran = "null";
                    }
                    if (chkboxModel.Checked == true)
                    {
                        objBL.chkMods = "chkModel";
                        chkMod = "chkModel";
                    }
                    else
                    {
                        objBL.chkMods = "null";
                        chkMod = "null";
                    }
                    if (chkboxProductCode.Checked == true)
                    {
                        objBL.chkitems = "chkProductcode";
                        chkitem = "chkProductcode";
                    }
                    else
                    {
                        objBL.chkitems = "null";
                        chkitem = "null";
                    }
                    if (chkboxBillno.Checked == true)
                    {
                        objBL.chkbillns = "chkBillno";
                        chkbilln = "chkBillno";
                    }
                    else
                    {
                        objBL.chkbillns = "null";
                        chkbilln = "null";
                    }
                    if (chkboxBillDate.Checked == true)
                    {
                        objBL.chkbilldts = "chkBillDate";
                        chkbilldt = "chkBillDate";
                    }
                    else
                    {
                        objBL.chkbilldts = "null";
                        chkbilldt = "null";
                    }
                    if (chkboxPurchaseReturn.Checked == true)
                    {
                        objBL.chkpurs = "chkPurchasereturn";
                        chkpur = "chkPurchasereturn";
                    }
                    else
                    {
                        objBL.chkpurs = "null";
                        chkpur = "null";
                    }
                    if (ChkboxEmpname.Checked == true)
                    {
                        objBL.chkemps = "chkEmpname";
                        chkemp = "chkEmpname";
                    }
                    else
                    {
                        objBL.chkemps = "null";
                        chkemp = "null";
                    }
                    if (chkboxInternalTransfer.Checked == true)
                    {
                        objBL.chkints = "chkInternaltransfer";
                        chkint = "chkInternaltransfer";
                    }
                    else
                    {
                        objBL.chkints = "null";
                        chkint = "null";
                    }

                    if (chkboxCustomer.Checked == true)
                    {
                        objBL.chkcustnmes = "chkCustomer";
                        chkcustnme = "chkCustomer";
                    }
                    else
                    {
                        objBL.chkcustnmes = "null";
                        chkcustnme = "null";
                    }
                    if (ChkboxCustaddr.Checked == true)
                    {
                        objBL.chkcustaddrs = "chkCustaddr";
                        chkcustaddr = "chkCustaddr";
                    }
                    else
                    {
                        objBL.chkcustaddrs = "null";
                        chkcustaddr = "null";
                    }
                    if (ChkboxCustphone.Checked == true)
                    {
                        objBL.chkcustphones = "chkCustphone";
                        chkcustphone = "chkCustphone";
                    }
                    else
                    {
                        objBL.chkcustphones = "null";
                        chkcustphone = "null";
                    }
                    if (ChkboxPaymode.Checked == true)
                    {
                        objBL.chkpays = "chkPaymode";
                        chkpay = "chkPaymode";
                    }
                    else
                    {
                        objBL.chkpays = "null";
                        chkpay = "null";
                    }
                    if (chkboxStock.Checked == true)
                    {
                        objBL.chkstocs = "chkStock";
                        chkstoc = "chkStock";
                    }
                    else
                    {
                        objBL.chkstocs = "null";
                        chkstoc = "null";
                    }
                    if (chkboxDiscount.Checked == true)
                    {
                        objBL.chkdiscounts = "chkStock";
                        chkdiscount = "chkStock";
                    }
                    else
                    {
                        objBL.chkdiscounts = "null";
                        chkdiscount = "null";
                    }
                    if (chkboxFreight.Checked == true)
                    {
                        objBL.chkfreights = "chkFreight";
                        chkfreight = "chkFreight";
                    }
                    else
                    {
                        objBL.chkfreights = "null";
                        chkfreight = "null";
                    }
                    if (chkboxRate.Checked == true)
                    {
                        objBL.chksrates = "chkRate";
                        chksrate = "chkRate";
                    }
                    else
                    {
                        objBL.chksrates = "null";
                        chksrate = "null";
                    }
                    if (chkboxAll.Checked == true)
                    {
                        objBL.chksAlls = "chkAll";
                        chksAll = "chkAll";
                    }
                    else
                    {
                        objBL.chksAlls = "null";
                        chksAll = "null";
                    }
                    if (ddlBrand.SelectedItem.Text != "All")
                    {
                        objBL.sdBrands = ddlBrand.SelectedItem.Text;
                        sdBrand = ddlBrand.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sdBrands = "All";
                        sdBrand = "All";
                    }

                    if (ddlCategory.SelectedItem.Text != "All")
                    {
                        objBL.sdCats = ddlCategory.SelectedItem.Value;
                        sdCat = ddlCategory.SelectedItem.Value;
                    }
                    else
                    {
                        objBL.sdCats = "All";
                        sdCat = "All";
                    }
                    if (ddlPrdctNme.SelectedItem.Text != "All")
                    {
                        objBL.sdProds = ddlPrdctNme.SelectedItem.Text;
                        sdProd = ddlPrdctNme.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sdProds = "All";
                        sdProd = "All";
                    }
                    /*if (ddlMdl.SelectedItem.Text != "None")
                    {
                        objBL.sdMods = ddlMdl.SelectedItem.Text;
                        sdMod = ddlMdl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sdMods = "All";
                        sdMod = "All";
                    }*/
                    if (ddlPrdctCode.SelectedItem.Text != "All")
                    {
                        objBL.sditems = ddlPrdctCode.SelectedItem.Text;
                        sditem = ddlPrdctCode.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sditems = "All";
                        sditem = "All";
                    }
                    if (ddlPayMode.SelectedItem.Text != "All")
                    {
                        objBL.sdpays = ddlPayMode.SelectedItem.Value;
                        sdpay = ddlPayMode.SelectedItem.Value;
                    }
                    else
                    {
                        objBL.sdpays = "All";
                        sdpay = "All";
                    }
                    if (ddlCustNme.SelectedItem.Text != "All")
                    {
                        objBL.sdcusts = ddlCustNme.SelectedItem.Value;
                        sdcust = ddlCustNme.SelectedItem.Value;
                    }
                    else
                    {
                        objBL.sdcusts = "All";
                        sdcust = "All";
                    }

                    if (ddlFirstLvl.SelectedItem.Text != "None")
                    {
                        objBL.sdfirstlvls = ddlFirstLvl.SelectedItem.Text;
                        sdfirstlvl = ddlFirstLvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sdfirstlvls = "None";
                        sdfirstlvl = "None";
                    }
                    if (ddlSecondLvl.SelectedItem.Text != "None")
                    {
                        objBL.sdsecondlvls = ddlSecondLvl.SelectedItem.Text;
                        sdsecondlvl = ddlSecondLvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sdsecondlvls = "None";
                        sdsecondlvl = "None";
                    }
                    if (ddlThirdLvl.SelectedItem.Text != "None")
                    {
                        objBL.sdthirdlvls = ddlThirdLvl.SelectedItem.Text;
                        sdthirdlvl = ddlThirdLvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sdthirdlvls = "None";
                        sdthirdlvl = "None";
                    }
                    if (ddlFourthLvl.SelectedItem.Text != "None")
                    {
                        objBL.sdfourlvls = ddlFourthLvl.SelectedItem.Text;
                        sdfourlvl = ddlFourthLvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sdfourlvls = "None";
                        sdfourlvl = "None";
                    }
                    if (ddlFifthLvl.SelectedItem.Text != "None")
                    {
                        objBL.sdfifthlvls = ddlFifthLvl.SelectedItem.Text;
                        sdfifthlvl = ddlFifthLvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sdfifthlvls = "None";
                        sdfifthlvl = "None";
                    }

                    if (ddlSixthLvl.SelectedItem.Text != "None")
                    {
                        objBL.sdsixthlvls = ddlSixthLvl.SelectedItem.Text;
                        sdsixthlvl = ddlSixthLvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sdsixthlvls = "None";
                        sdsixthlvl = "None";
                    }
                    if (ddlSeventhLvl.SelectedItem.Text != "None")
                    {
                        objBL.sdseventhlvls = ddlSeventhLvl.SelectedItem.Text;
                        sdseventhlvl = ddlSeventhLvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sdseventhlvls = "None";
                        sdseventhlvl = "None";
                    }
                    if (odlfirstlvl.SelectedItem.Text != "None")
                    {
                        objBL.sodfirstlvls = odlfirstlvl.SelectedItem.Text;
                        sodfirstlvl = odlfirstlvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sodfirstlvls = "None";
                        sodfirstlvl = "None";
                    }
                    if (odlsecondlvl.SelectedItem.Text != "None")
                    {
                        objBL.sodsecondlvls = odlsecondlvl.SelectedItem.Text;
                        sodsecondlvl = odlsecondlvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sodsecondlvls = "None";
                        sodsecondlvl = "None";
                    }
                    if (odlthirdlvl.SelectedItem.Text != "None")
                    {
                        objBL.sodthirdlvls = odlthirdlvl.SelectedItem.Text;
                        sodthirdlvl = odlthirdlvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sodthirdlvls = "None";
                        sodthirdlvl = "None";
                    }
                    if (odlfourlvl.SelectedItem.Text != "None")
                    {
                        objBL.sodfourlvls = odlfourlvl.SelectedItem.Text;
                        sodfourlvl = odlfourlvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sodfourlvls = "None";
                        sodfourlvl = "None";
                    }
                    if (odlsixthlvl.SelectedItem.Text != "None")
                    {
                        objBL.sodsixthlvls = odlsixthlvl.SelectedItem.Text;
                        sodsixthlvl = odlsixthlvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sodsixthlvls = "None";
                        sodsixthlvl = "None";
                    }
                    if (odlseventhlvl.SelectedItem.Text != "None")
                    {
                        objBL.sodseventhlvls = odlseventhlvl.SelectedItem.Text;
                        sodseventhlvl = odlseventhlvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sodseventhlvls = "None";
                        sodseventhlvl = "None";
                    }
                    if (odlfifthlvl.SelectedItem.Text != "None")
                    {
                        objBL.sodfifthlvls = odlfifthlvl.SelectedItem.Text;
                        sodfifthlvl = odlfifthlvl.SelectedItem.Text;
                    }
                    else
                    {
                        objBL.sodfifthlvls = "None";
                        sodfifthlvl = "None";
                    }
                    DataSet ds1 = new DataSet();
                    objBL.insertsavedsales(savename, chkCat, chkBran, chkProd, chkMod, chkitem, chkcustnme, chkcustaddr, chkcustphone, chkemp, chkint, chkpay, chkpur, chkbilldt, chkbilln, chkstoc, chkdiscount, chkfreight, chksrate, chksAll, sdBrand, sdCat, sdProd, sditem, sdpay, sdcust, sdfirstlvl, sdsecondlvl, sdthirdlvl, sdfourlvl, sdfifthlvl, sdsixthlvl, sdseventhlvl, sodfirstlvl, sodsecondlvl, sodthirdlvl, sodfourlvl, sodfifthlvl, sodsixthlvl, sodseventhlvl, rblpur, rblint, stdt, endt, subfirstlevel, subsecondlevel, subthirdlevel, subfourlevel, subfifthlevel, subsixthlevel, subseventhlevel, subeighthlevel);
                    //DataGrid dsg = new Datagrid();ss


                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selections Saved')", true);
                    return;
                }
                catch (Exception ex)
                {
                    TroyLiteExceptionManager.HandleException(ex);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}