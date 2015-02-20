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
    decimal Ittls = 0;

    string txtstock = "";
    string txtvat = "";
    string txtbuyrate = "";
    string txtrate = "";
    string txtnlc = "";
    string dat = "";

    decimal brandTotal = 0, catIDTotal = 0, modelTotal = 0;
    string grpBy = "", selCols = "", selLevels = "", field1 = "", field2 = "", ordrby = "", cond = "";
    
    //DBClass objBL = new DBClass();
    BusinessLogic objBL = new BusinessLogic();
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
            //  sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            //hiding label and displaying popup message
            lblMsg.Visible = false;
            if (!Page.IsPostBack)
            {
                fillfBrand();
                fillCategorys();
                fillProduct();
                fillModel();
                fillitemcode();
                txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //  fillProduct();
                //fillStock();
                ddlFirst();
                ddlSconed();
                ddlThird();
                ddlFour();
                ddlFifth();
                odlFirst();
                odlSconed();
                odlThird();
                odlFour();
                odlFifth();


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
        ddlfirstlvl.Items.Insert(2, "CategoryName");
        ddlfirstlvl.Items.Insert(3, "Model");
        ddlfirstlvl.Items.Insert(4, "ProductName");
        ddlfirstlvl.Items.Insert(5, "ItemCode");

    }
    private void ddlSconed()
    {
        ddlsecondlvl.Items.Insert(0, "None");
        ddlsecondlvl.Items.Insert(1, "Brand");
        ddlsecondlvl.Items.Insert(2, "CategoryName");
        ddlsecondlvl.Items.Insert(3, "Model");
        ddlsecondlvl.Items.Insert(4, "ProductName");
        ddlsecondlvl.Items.Insert(5, "ItemCode");

    }
    private void ddlThird()
    {
        ddlthirdlvl.Items.Insert(0, "None");
        ddlthirdlvl.Items.Insert(1, "Brand");
        ddlthirdlvl.Items.Insert(2, "CategoryName");
        ddlthirdlvl.Items.Insert(3, "Model");
        ddlthirdlvl.Items.Insert(4, "ProductName");
        ddlthirdlvl.Items.Insert(5, "ItemCode");
    }
    private void ddlFour()
    {
        ddlfourlvl.Items.Insert(0, "None");
        ddlfourlvl.Items.Insert(1, "Brand");
        ddlfourlvl.Items.Insert(2, "CategoryName");
        ddlfourlvl.Items.Insert(3, "Model");
        ddlfourlvl.Items.Insert(4, "ProductName");
        ddlfourlvl.Items.Insert(5, "ItemCode");
    }
    private void ddlFifth()
    {
        ddlfifthlvl.Items.Insert(0, "None");
        ddlfifthlvl.Items.Insert(1, "Brand");
        ddlfifthlvl.Items.Insert(2, "CategoryName");
        ddlfifthlvl.Items.Insert(3, "Model");
        ddlfifthlvl.Items.Insert(4, "ProductName");
        ddlfifthlvl.Items.Insert(5, "ItemCode");
    }
    private void odlFirst()
    {
        
        odlfirstlvl.Items.Insert(0, "None");
        odlfirstlvl.Items.Insert(1, "Brand");
        odlfirstlvl.Items.Insert(2, "CategoryName");
        odlfirstlvl.Items.Insert(3, "Model");
        odlfirstlvl.Items.Insert(4, "ProductName");
        odlfirstlvl.Items.Insert(5, "ItemCode");
    }
    private void odlSconed()
    {
        odlsecondlvl.Items.Insert(0, "None");
        odlsecondlvl.Items.Insert(1, "Brand");
        odlsecondlvl.Items.Insert(2, "CategoryName");
        odlsecondlvl.Items.Insert(3, "Model");
        odlsecondlvl.Items.Insert(4, "ProductName");
        odlsecondlvl.Items.Insert(5, "ItemCode");
    }
    private void odlThird()
    {
        odlthirdlvl.Items.Insert(0, "None");
        odlthirdlvl.Items.Insert(1, "Brand");
        odlthirdlvl.Items.Insert(2, "CategoryName");
        odlthirdlvl.Items.Insert(3, "Model");
        odlthirdlvl.Items.Insert(4, "ProductName");
        odlthirdlvl.Items.Insert(5, "ItemCode");
    }
    private void odlFour()
    {
        odlfourlvl.Items.Insert(0, "None");
        odlfourlvl.Items.Insert(1, "Brand");
        odlfourlvl.Items.Insert(2, "CategoryName");
        odlfourlvl.Items.Insert(3, "Model");
        odlfourlvl.Items.Insert(4, "ProductName");
        odlfourlvl.Items.Insert(5, "ItemCode");
    }
    private void odlFifth()
    {
        odlfifthlvl.Items.Insert(0, "None");
        odlfifthlvl.Items.Insert(1, "Brand");
        odlfifthlvl.Items.Insert(2, "CategoryName");
        odlfifthlvl.Items.Insert(3, "Model");
        odlfifthlvl.Items.Insert(4, "ProductName");
        odlfifthlvl.Items.Insert(5, "ItemCode");
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
    private void fillModel()
    {
        DataSet ds = new DataSet();
        ds = objBL.getDistinctModel();
        ddlMdl.DataSource = ds;
        ddlMdl.DataTextField = "Model";
        ddlMdl.DataValueField = "Model";
        ddlMdl.DataBind();
        ddlMdl.Items.Insert(0, "All");

    }
    private void fillProduct()
    {
        DataSet ds = new DataSet();
        ds = objBL.getDistinctPrdctNme();
        ddlPrdctNme.DataSource = ds;
        ddlPrdctNme.DataTextField = "ProductName";
        ddlPrdctNme.DataValueField = "ProductName";
        ddlPrdctNme.DataBind();
        ddlPrdctNme.Items.Insert(0, "All");
    }
    private void fillitemcode()
    {
        DataSet ds = new DataSet();
        ds = objBL.getDistinctItemCode();
        ddlItemCode.DataSource = ds;
        ddlItemCode.DataTextField = "ItemCode";
        ddlItemCode.DataValueField = "ItemCode";
        ddlItemCode.DataBind();
        ddlItemCode.Items.Insert(0, "All");
    }

  /*  protected void btnData_Click(object sender, EventArgs e)
    {
        if (!isValidLevels())
        {
            return;
        }
        Total = 0;
        string field1 = "";
        string field2 = "";
        field1 = getfield();
        field2 = getfield2();
        string cond = "";
        cond = getCond();
        string grpby = "";
        grpby = getGrpByAndSelCols();
        DataSet ds = new DataSet();
        ds = objBL.getStock(selCols, field2, cond, grpBy, ordrby);
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
        var lblTotalValue = GridStock.FooterRow.FindControl("lblTotalValue") as Label;
        if (lblTotalValue != null)
        {
            lblTotalValue.Text = Total.ToString();
        }

    }*/
    protected string getfield()
    {
        string field1 = "";
        //string field2="";
        string field = "";
        if (chkboxCategory.Checked)
        {
            ddlfirstlvl.Items.Insert(1, "Category");
            odlfirstlvl.Items.Insert(1, "Category");
            fillCategorys();
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
        if (chkboxBrand.Checked)
        {
            ddlfirstlvl.Items.Insert(2, "Brand");
            odlfirstlvl.Items.Insert(2, "Brand");
            fillfBrand();
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
        if (chkboxProduct.Checked)
        {
            ddlfirstlvl.Items.Insert(3, "ProductName");
            odlfirstlvl.Items.Insert(3, "ProductName");
            fillProduct();
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
        if (chkboxModel.Checked)
        {
            ddlfirstlvl.Items.Insert(4, "Model");
            odlfirstlvl.Items.Insert(4, "Model");
            fillProduct();
            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }
            field1 += "Model";

        }
        if (chkboxItemCode.Checked)
        {
            ddlfirstlvl.Items.Insert(5, "ItemCode");
            odlfirstlvl.Items.Insert(5, "ItemCode");
            fillitemcode();
            if (field1 == "")
            {

                field1 = "";
            }
            else
            {

                field1 += " , ";
            }
            field1 += "ItemCode";

        }
        field1 = field1.Replace("Brand", "ProductDesc");
        return field1;
    }
    protected string getfield2()
    {
        string field2 = "";
        if (chkboxRate.Checked)
        {
            field2 += ",Sum(Rate) as Rate";
        }
        if (chkboxVat.Checked)
        {
            field2 += ",Sum(VAT) as VAT";
        }
        if (chkboxBuyrate.Checked)
        {
            field2 += ",Sum(BuyRate) as BuyRate";
        }

        if (chkboxNlc.Checked)
        {
            field2 += ",Sum(NLC) as NLC";
        }
        if (chkboxStock.Checked)
        {
            field2 += ",Sum(Quantity) as Stock";
        }

        field2 = field2.Replace("Quantity", "Stock");

        return field2;

    }



    protected string getCond()
    {
        string cond = "";

        //where ProductDesc='" + Brand + "' and CategoryID=" + Convert.ToInt32(Categorys) + " and Model='" + Models + "' and ProductName='" + PrdctNme + "'
        if (ddlBrand.SelectedIndex > 0)
        {
            objBL.Brands = ddlBrand.SelectedItem.Text;
            cond = " and ProductDesc='" + ddlBrand.SelectedItem.Text + "' ";
        }
        if (ddlCategory.SelectedIndex > 0)
        {
            objBL.Categorys = Convert.ToInt32(ddlCategory.SelectedItem.Value);
            cond += " and PM.CategoryID=" + Convert.ToInt32(ddlCategory.SelectedItem.Value) + " ";
        }
        if (ddlMdl.SelectedIndex > 0)
        {
            objBL.Models = ddlMdl.SelectedItem.Text;
            cond += " and Model='" + ddlMdl.SelectedItem.Text + "' ";
        }
        if (ddlPrdctNme.SelectedIndex > 0)
        {
            objBL.PrdctNmes = ddlPrdctNme.SelectedItem.Text;
            cond += " and ProductName='" + ddlPrdctNme.SelectedItem.Text + "' ";
        }
        /*if (ddlStock.SelectedIndex > 0)
        {
            cond += " and Stock > 0 ";
        }*/
        if (ddlItemCode.SelectedIndex > 0)
        {
            objBL.Itemcodes = ddlItemCode.SelectedItem.Text;
            cond += " and ItemCode= '" + ddlItemCode.SelectedItem.Text + "'";
        }
        if (ddlStock.SelectedIndex > 0)
        {

            if (ddlStock.SelectedItem.Text == "= 0")
            {
                objBL.StockValues = ddlStock.SelectedItem.Text;
                cond += " and Stock" + ddlStock.SelectedItem.Text;
            }
            if (ddlStock.SelectedItem.Text == "> 0")
            {
                objBL.StockValues = ddlStock.SelectedItem.Text;
                cond += " and Stock " + ddlStock.SelectedItem.Text;
            }
            else if (ddlStock.SelectedItem.Text == " >Value")
            {
                // buyratetxtbox.show();
                objBL.StockValues = Stocktxtbox.Text;
                cond += " and Stock >" + Stocktxtbox.Text;
            }
            else if (ddlStock.SelectedItem.Text == "<Value")
            {
                //buyratetxtbox.show();
                objBL.StockValues = Stocktxtbox.Text;
                cond += " and Stock < " + Stocktxtbox.Text;
            }

            /* if (ddlStock.SelectedItem.Text = "All")
             {
                 cond +=" ";
             }*/


        }
        if (ddlRate.SelectedIndex > 0)
        {


            if (ddlRate.SelectedItem.Text == "=Value")
            {
                objBL.Rated = Ratetxtbox.Text;
                cond += " and Rate =" + Ratetxtbox.Text;
            }
            else if (ddlRate.SelectedItem.Text == ">Value")
            {
                //Ratetxtbox.text.show();
                objBL.Rated = Ratetxtbox.Text;
                cond += " and Rate > " + Ratetxtbox.Text;
            }
            else if (ddlRate.SelectedItem.Text == "<Value")
            {
                //Ratetxtbox.text.show();
                objBL.Rated = Ratetxtbox.Text;
                cond += " and Rate <" + Ratetxtbox.Text;
            }
        }
        if (ddlNlc.SelectedIndex > 0)
        {
            // int valueNLC = 0;

            if (ddlNlc.SelectedItem.Text == "=Value")
            {
                objBL.Nlcs = Nlctxtbox.Text;
                cond += " and NLC =" + Nlctxtbox.Text;
            }
            else if (ddlNlc.SelectedItem.Text == ">Value")
            {
                // NLCtxtbox.text.show();
                objBL.Nlcs = Nlctxtbox.Text;
                cond += " and NLC >" + Nlctxtbox.Text;
            }
            else if (ddlNlc.SelectedItem.Text == "<Value")
            {
                //NLCtxtbox.text.show();
                objBL.Nlcs = Nlctxtbox.Text;
                cond += " and NLC <" + Nlctxtbox.Text;
            }
        }

        if (ddlBuyrate.SelectedIndex > 0)
        {
            //  int valuebuyrate = 0;

            if (ddlBuyrate.SelectedItem.Text == "=Value")
            {
                objBL.Buyrates = buyratetxtbox.Text;
                cond += " and BuyRate =" + buyratetxtbox.Text;
            }
            else if (ddlBuyrate.SelectedItem.Text == ">Value")
            {
                //buyratetxtbox.text.show();
                objBL.Buyrates = buyratetxtbox.Text;
                cond += " and BuyRate >" + buyratetxtbox.Text;
            }
            else if (ddlBuyrate.SelectedItem.Text == "<Value")
            {
                objBL.Buyrates = buyratetxtbox.Text;
                cond += " and BuyRate <" + buyratetxtbox.Text;
            }
        }

        if (ddlVat.SelectedIndex > 0)
        {
            // int valuevat = 0;

            if (ddlVat.SelectedItem.Text == "=Value")
            {
                objBL.Vats = Vattxtbox.Text;
                cond += " and VAT =" + Vattxtbox.Text;
            }
            else if (ddlVat.SelectedItem.Text == ">Value")
            {
                //VATtxtbox.text.show();
                objBL.Vats = Vattxtbox.Text;
                cond += " and VAT >" + Vattxtbox.Text;

            }
            else if (ddlVat.SelectedItem.Text == "<Value")
            {
                //VATtxtbox.text.show();
                objBL.Vats = Vattxtbox.Text;
                cond += " and VAT <" + Vattxtbox.Text;
            }
        }


        //objBL.Stock = Convert.ToInt32(ddlStock.SelectedItem.Text);
        return cond;
    }

    protected string getGrpByAndSelCols()
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
        if (ddlfifthlvl.SelectedIndex > 0)
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
            grpBy += " " + ddlfifthlvl.SelectedItem.Text + " ";
            selCols += " " + ddlfifthlvl.SelectedItem.Text + " ";
        }

        if (grpBy == "")
        {
            field1 = getfield();
            //field2 = getfield2();
            grpBy += " group by " + field1;
            selCols += field1;
        }


        /*selCols = selCols.Replace("Brand", "ProductDesc");
        selCols = selCols.Replace("CategoryID", "CategoryName");
        grpBy = grpBy.Replace("Brand", "ProductDesc");
        grpBy = grpBy.Replace("CategoryID", "CategoryName");*/
        return grpBy;
        return selCols;
    }
    protected string odrby()
    {

        string ordrby = "";
        if (odlfirstlvl.SelectedIndex > 0)
        {

            ordrby += " Order by " + odlfirstlvl.SelectedItem.Text + " ";
        }
        if (ddlsecondlvl.SelectedIndex > 0)
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
        if (odlthirdlvl.SelectedIndex > 0)
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
        if (odlfourlvl.SelectedIndex > 0)
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
        if (odlfifthlvl.SelectedIndex > 0)
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
        /*if (ordrby == "")
        {
            field1 = getfield();
            ordrby = field1;
        }*/
        ordrby = ordrby.Replace("Brand", "ProductDesc");
        ordrby = ordrby.Replace("CategoryID", "CategoryName");
        return ordrby;
    }


    private bool isValidLevels()
    {
        if ((ddlfirstlvl.SelectedItem.Text != "None") &&
     (ddlfirstlvl.SelectedItem.Text == ddlsecondlvl.SelectedItem.Text ||
     ddlfirstlvl.SelectedItem.Text == ddlthirdlvl.SelectedItem.Text ||
     ddlfirstlvl.SelectedItem.Text == ddlfourlvl.SelectedItem.Text ||
ddlfirstlvl.SelectedItem.Text == ddlfifthlvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            lblMsg.Text = "Two levels can NOT be same. Please select different levels";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return false;
        }
        if ((ddlsecondlvl.SelectedItem.Text != "None") &&
            (ddlsecondlvl.SelectedItem.Text == ddlthirdlvl.SelectedItem.Text ||
            ddlsecondlvl.SelectedItem.Text == ddlfourlvl.SelectedItem.Text ||
ddlsecondlvl.SelectedItem.Text == ddlfifthlvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            lblMsg.Text = "Two levels can NOT be same. Please select different levels";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return false;
        }
        if ((ddlthirdlvl.SelectedItem.Text != "None") &&
            (ddlthirdlvl.SelectedItem.Text == ddlfourlvl.SelectedItem.Text ||
ddlthirdlvl.SelectedItem.Text == ddlfifthlvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            lblMsg.Text = "Two levels can NOT be same. Please select different levels";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return false;
        }
        if ((ddlfourlvl.SelectedItem.Text != "None") &&
               (ddlfourlvl.SelectedItem.Text == ddlfifthlvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            lblMsg.Text = "Two levels can NOT be same. Please select different levels";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            return false;
        }
        if ((!chkboxBrand.Checked) && (!chkboxCategory.Checked) && (!chkboxModel.Checked) && (!chkboxProduct.Checked) && (!chkboxItemCode.Checked))
        {
            lblMsg.Text = "Atleast one field should be selected";
            return false;
        }


        return true;
    }
    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            if (!isValidLevels())
            {
                return;
            }
            string field1 = "";
            string field2 = "";
            field1 = getfield();
            field2 = getfield2();
            string cond = "";
            cond = getCond();
            grpBy = getGrpByAndSelCols();
            selCols = field1;
            DateTime refDate = DateTime.Parse(txtStartDate.Text);
            bindData(selCols, field2, cond, grpBy, ordrby);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void bindData(string selCols, string field2, string cond, string grpBy, string ordrby)
    {
        //if (ddlfourlvl.SelectedIndex > 0)
        bool dispLastTotal = false;
        DataSet ds = new DataSet();
        ds = objBL.getStock(selCols, field2, cond, grpBy, ordrby);
        DataTable dt = new DataTable();
        if (ds.Tables[0].Rows.Count > 0)
        {
            if ((ddlfirstlvl.SelectedIndex > 0) || (ddlsecondlvl.SelectedIndex > 0) || (ddlthirdlvl.SelectedIndex > 0) || (ddlfourlvl.SelectedIndex > 0) || (ddlfifthlvl.SelectedIndex > 0))
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
                if (ddlfifthlvl.SelectedIndex > 0)
                {
                    dt.Columns.Add(new DataColumn(ddlfifthlvl.SelectedItem.Text));
                }
            }
            if ((chkboxCategory.Checked) || (chkboxBrand.Checked) || (chkboxProduct.Checked) || (chkboxModel.Checked) || (chkboxItemCode.Checked))
            {
                if (chkboxCategory.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("CategoryName"));
                }
                if (chkboxBrand.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("ProductDesc"));
                }
                if (chkboxProduct.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("ProductName"));
                }
                if (chkboxModel.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("Model"));
                }
                if (chkboxItemCode.Checked == true)
                {
                    dt.Columns.Add(new DataColumn("ItemCode"));
                }
            }
            /* if (selLevels.IndexOf("Brand") < 0)
                 dt.Columns.Add(new DataColumn("Brand"));
             if (selLevels.IndexOf("CategoryName") < 0)
                 dt.Columns.Add(new DataColumn("CategoryName"));
             if (selLevels.IndexOf("ItemCode") < 0)
                 dt.Columns.Add(new DataColumn("ItemCode"));
             if (selLevels.IndexOf("Model") < 0)
                 dt.Columns.Add(new DataColumn("Model"));
             if (selLevels.IndexOf("ProductName") < 0)
                 dt.Columns.Add(new DataColumn("ProductName"));*/

            dt.Columns.Add(new DataColumn("Rate"));
            dt.Columns.Add(new DataColumn("NLC"));
            dt.Columns.Add(new DataColumn("BuyRate"));
            dt.Columns.Add(new DataColumn("VAT"));
            dt.Columns.Add(new DataColumn("Quantity"));



            dt.Columns.Add(new DataColumn("SalesValue"));

            //initialize column values for entire row
            string fLvlValue = "", sLvlValue = "", tLvlValue = "", frthLvlValue = "", fifLvlValue = "";
            string fLvlValueTemp = "", sLvlValueTemp = "", tLvlValueTemp = "", frthLvlValueTemp = "", fifthLvlValueTemp = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if ((chkboxCategory.Checked) || (chkboxBrand.Checked) || (chkboxProduct.Checked) || (chkboxModel.Checked) || (chkboxItemCode.Checked))
                {
                    if (chkboxCategory.Checked == true)
                    {
                        fLvlValueTemp = dr["CategoryName"].ToString().ToUpper().Trim();

                    }

                    if (chkboxBrand.Checked == true)
                    {
                        sLvlValueTemp = dr["ProductDesc"].ToString().ToUpper().Trim();
                    }
                    if (chkboxProduct.Checked == true)
                    {
                        tLvlValueTemp = dr["ProductName"].ToString().ToUpper().Trim();
                    }
                    if (chkboxModel.Checked == true)
                    {
                        frthLvlValueTemp = dr["Model"].ToString().ToUpper().Trim();
                    }
                    if (chkboxItemCode.Checked == true)
                    {
                        fifthLvlValueTemp = dr["ItemCode"].ToString().ToUpper().Trim();
                    }
                    dispLastTotal = true;
                    if (chkboxItemCode.Checked == true)
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                             (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                             (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                             (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
     (fifLvlValue != "" && fifLvlValue != fifthLvlValueTemp))
                        {
                            DataRow dr_final17 = dt.NewRow();
                            if (chkboxCategory.Checked == true)
                            {
                                dr_final17["CategoryName"] = "";
                            }
                            if (chkboxBrand.Checked == true)
                            {
                                dr_final17["ProductDesc"] = "";
                            }
                            if (chkboxProduct.Checked == true)
                            {
                                dr_final17["ProductName"] = "";
                            }
                            if (chkboxModel.Checked == true)
                            {
                                dr_final17["Model"] = "";
                            }
                            if (chkboxItemCode.Checked == true)
                            {
                                dr_final17["ItemCode"] = "Total:" + fifLvlValue;
                            }
                            dr_final17["Rate"] = "";
                            dr_final17["NLC"] = "";
                            dr_final17["BuyRate"] = "";
                            dr_final17["VAT"] = "";
                            dr_final17["Quantity"] = "";


                            dr_final17["SalesValue"] = Convert.ToString(Convert.ToDecimal(Ittls));
                            dt.Rows.Add(dr_final17);
                            Ittls = 0;
                        }
                    }

                    if (chkboxModel.Checked == true)
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                            (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp))
                        {
                            DataRow dr_final7 = dt.NewRow();
                            if (chkboxCategory.Checked == true)
                            {
                                dr_final7["CategoryName"] = "";
                            }
                            if (chkboxBrand.Checked == true)
                            {
                                dr_final7["ProductDesc"] = "";
                            }
                            if (chkboxProduct.Checked == true)
                            {
                                dr_final7["ProductName"] = "";
                            }
                            if (chkboxModel.Checked == true)
                            {
                                dr_final7["Model"] = "Total:" + frthLvlValue;
                            }
                            if (chkboxItemCode.Checked == true)
                            {
                                dr_final7["ItemCode"] = "";
                            }
                            //dr_final7["Brand"] = "";
                            ////dr_final7[ddlOrdBySel1.selectedvalue] = "";
                            //dr_final7["CategoryName"] = "";
                            //dr_final7["Model"] = "";
                            //dr_final7["ProductName"] = "";

                            //dr_final7["ItemCode"] = "Total:" + frthLvlValueTemp;
                            /* if (selLevels.IndexOf("Brand") < 0)
                                 dr_final7["Brand"] = "";
                             if (selLevels.IndexOf("CategoryName") < 0)
                                 dr_final7["CategoryName"] = "";
                             if (selLevels.IndexOf("ItemCode") < 0)
                                 dr_final7["ItemCode"] = "";
                             if (selLevels.IndexOf("Model") < 0)
                                 dr_final7["Model"] = "";
                             if (selLevels.IndexOf("ProductName") < 0)
                                 dr_final7["ProductName"] = "";*/

                            dr_final7["Rate"] = "";
                            dr_final7["NLC"] = "";
                            dr_final7["BuyRate"] = "";
                            dr_final7["VAT"] = "";
                            dr_final7["Quantity"] = "";


                            dr_final7["SalesValue"] = Convert.ToString(Convert.ToDecimal(Pttls));
                            dt.Rows.Add(dr_final7);
                            Pttls = 0;
                        }
                    }

                    if (chkboxProduct.Checked == true)
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                        {
                            DataRow dr_final8 = dt.NewRow();
                            if (chkboxCategory.Checked == true)
                            {
                                dr_final8["CategoryName"] = "";
                            }
                            if (chkboxBrand.Checked == true)
                            {
                                dr_final8["ProductDesc"] = "";
                            }
                            if (chkboxProduct.Checked == true)
                            {
                                dr_final8["ProductName"] = "Total " + tLvlValue;
                            }
                            if (chkboxModel.Checked == true)
                            {
                                dr_final8["Model"] = "";
                            }
                            if (chkboxItemCode.Checked == true)
                            {
                                dr_final8["ItemCode"] = "";
                            }
                            /*  if (selLevels.IndexOf("Brand") < 0)
                                  dr_final8["Brand"] = "";
                              if (selLevels.IndexOf("CategoryName") < 0)
                                  dr_final8["CategoryName"] = "";
                              if (selLevels.IndexOf("ItemCode") < 0)
                                  dr_final8["ItemCode"] = "";
                              if (selLevels.IndexOf("Model") < 0)
                                  dr_final8["Model"] = "";
                              if (selLevels.IndexOf("ProductName") < 0)
                                  dr_final8["ProductName"] = "";*/

                            dr_final8["Rate"] = "";
                            dr_final8["NLC"] = "";
                            dr_final8["BuyRate"] = "";
                            dr_final8["VAT"] = "";
                            dr_final8["Quantity"] = "";


                            dr_final8["SalesValue"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                            dt.Rows.Add(dr_final8);
                            //dt.Rows.Add(dr_final8);
                            modelTotal = 0;
                        }
                    }

                    if (chkboxBrand.Checked == true)
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                        {
                            DataRow dr_final8 = dt.NewRow();
                            if (chkboxCategory.Checked == true)
                            {
                                dr_final8["CategoryName"] = "";
                            }
                            if (chkboxBrand.Checked == true)
                            {
                                dr_final8["ProductDesc"] = "Total " + sLvlValue;
                            }
                            if (chkboxProduct.Checked == true)
                            {
                                dr_final8["ProductName"] = "";
                            }
                            if (chkboxModel.Checked == true)
                            {
                                dr_final8["Model"] = "";
                            }
                            if (chkboxItemCode.Checked == true)
                            {
                                dr_final8["ItemCode"] = "";
                            }
                            /*if (selLevels.IndexOf("Brand") < 0)
                                dr_final8["Brand"] = "";
                            if (selLevels.IndexOf("CategoryName") < 0)
                                dr_final8["CategoryName"] = "";
                            if (selLevels.IndexOf("ItemCode") < 0)
                                dr_final8["ItemCode"] = "";
                            if (selLevels.IndexOf("Model") < 0)
                                dr_final8["Model"] = "";
                            if (selLevels.IndexOf("ProductName") < 0)
                                dr_final8["ProductName"] = "";*/
                            dr_final8["Rate"] = "";
                            dr_final8["NLC"] = "";
                            dr_final8["BuyRate"] = "";
                            dr_final8["VAT"] = "";

                            dr_final8["Quantity"] = "";


                            dr_final8["SalesValue"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                            dt.Rows.Add(dr_final8);
                            brandTotal = 0;
                        }
                    }
                    if (chkboxCategory.Checked == true)
                    {
                        if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                        {
                            DataRow dr_final8 = dt.NewRow();
                            if (chkboxCategory.Checked == true)
                            {
                                dr_final8["CategoryName"] = "Total " + fLvlValue;
                            }
                            if (chkboxBrand.Checked == true)
                            {
                                dr_final8["ProductDesc"] = "";
                            }
                            if (chkboxProduct.Checked == true)
                            {
                                dr_final8["ProductName"] = "";
                            }
                            if (chkboxModel.Checked == true)
                            {
                                dr_final8["Model"] = "";
                            }
                            if (chkboxItemCode.Checked == true)
                            {
                                dr_final8["ItemCode"] = "";
                            }
                            /* if (selLevels.IndexOf("Brand") < 0)
                                 dr_final8["Brand"] = "";
                             if (selLevels.IndexOf("CategoryName") < 0)
                                 dr_final8["CategoryName"] = "";
                             if (selLevels.IndexOf("ItemCode") < 0)
                                 dr_final8["ItemCode"] = "";
                             if (selLevels.IndexOf("Model") < 0)
                                 dr_final8["Model"] = "";
                             if (selLevels.IndexOf("ProductName") < 0)
                                 dr_final8["ProductName"] = "";*/

                            dr_final8["Rate"] = "";
                            dr_final8["NLC"] = "";
                            dr_final8["BuyRate"] = "";
                            dr_final8["VAT"] = "";
                            dr_final8["Quantity"] = "";


                            dr_final8["SalesValue"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                            dt.Rows.Add(dr_final8);
                            catIDTotal = 0;
                        }
                    }
                    if (chkboxCategory.Checked == true)
                    {
                        if (fLvlValueTemp != "" && fLvlValue != fLvlValueTemp)
                        {
                            DataRow dr_final1 = dt.NewRow();
                            if (chkboxCategory.Checked == true)
                            {
                                dr_final1["CategoryName"] = fLvlValueTemp;
                            }
                            if (chkboxBrand.Checked == true)
                            {
                                dr_final1["ProductDesc"] = "";
                            }
                            if (chkboxProduct.Checked == true)
                            {
                                dr_final1["ProductName"] = "";
                            }
                            if (chkboxModel.Checked == true)
                            {
                                dr_final1["Model"] = "";
                            }
                            if (chkboxItemCode.Checked == true)
                            {
                                dr_final1["ItemCode"] = "";
                            }
                            /*if (selLevels.IndexOf("Brand") < 0)
                                dr_final1["Brand"] = "";
                            if (selLevels.IndexOf("CategoryName") < 0)
                                dr_final1["CategoryName"] = "";
                            if (selLevels.IndexOf("ItemCode") < 0)
                                dr_final1["ItemCode"] = "";
                            if (selLevels.IndexOf("Model") < 0)
                                dr_final1["Model"] = "";
                            if (selLevels.IndexOf("ProductName") < 0)
                                dr_final1["ProductName"] = "";*/
                            dr_final1["Rate"] = "";
                            dr_final1["NLC"] = "";
                            dr_final1["BuyRate"] = "";
                            dr_final1["VAT"] = "";

                            dr_final1["Quantity"] = "";


                            dr_final1["SalesValue"] = "";
                            dt.Rows.Add(dr_final1);
                        }
                    }
                    if (chkboxBrand.Checked == true)
                    {
                        if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp))
                        {
                            DataRow dr_final2 = dt.NewRow();
                            if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp))
                            {
                                if (chkboxBrand.Checked == true)
                                {
                                    dr_final2["ProductDesc"] = sLvlValueTemp;
                                }
                            }
                            if (chkboxCategory.Checked == true)
                            {
                                dr_final2["CategoryName"] = "";
                            }
                            if (chkboxBrand.Checked == true)
                            {
                                dr_final2["ProductDesc"] = sLvlValueTemp;
                            }
                            if (chkboxProduct.Checked == true)
                            {
                                dr_final2["ProductName"] = "";
                            }
                            if (chkboxModel.Checked == true)
                            {
                                dr_final2["Model"] = "";
                            }
                            if (chkboxItemCode.Checked == true)
                            {
                                dr_final2["ItemCode"] = "";
                            }
                            /*  if (selLevels.IndexOf("Brand") < 0)
                                  dr_final2["Brand"] = "";
                              if (selLevels.IndexOf("CategoryName") < 0)
                                  dr_final2["CategoryName"] = "";
                              if (selLevels.IndexOf("ItemCode") < 0)
                                  dr_final2["ItemCode"] = "";
                              if (selLevels.IndexOf("Model") < 0)
                                  dr_final2["Model"] = "";
                              if (selLevels.IndexOf("ProductName") < 0)
                                  dr_final2["ProductName"] = "";*/
                            dr_final2["Rate"] = "";
                            dr_final2["NLC"] = "";
                            dr_final2["BuyRate"] = "";
                            dr_final2["VAT"] = "";

                            dr_final2["Quantity"] = "";


                            dr_final2["SalesValue"] = "";
                            dt.Rows.Add(dr_final2);
                        }
                    }
                    if (chkboxProduct.Checked == true)
                    {
                        if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp))
                        {
                            DataRow dr_final1 = dt.NewRow();
                            if (fLvlValueTemp != "" && fLvlValue != fLvlValueTemp)
                            {
                                if (chkboxProduct.Checked == true)
                                {
                                    dr_final1["ProductName"] = tLvlValueTemp;
                                }
                            }
                            //else
                            //{
                            if (chkboxCategory.Checked == true)
                            {
                                dr_final1["CategoryName"] = "";
                            }
                            if (chkboxBrand.Checked == true)
                            {
                                dr_final1["ProductDesc"] = "";
                            }
                            if (chkboxProduct.Checked == true)
                            {
                                dr_final1["ProductName"] = tLvlValueTemp;
                            }
                            if (chkboxModel.Checked == true)
                            {
                                dr_final1["Model"] = "";
                            }
                            if (chkboxItemCode.Checked == true)
                            {
                                dr_final1["ItemCode"] = "";
                            }
                            /*if (selLevels.IndexOf("Brand") < 0)
                                dr_final1["Brand"] = "";
                            if (selLevels.IndexOf("CategoryName") < 0)
                                dr_final1["CategoryName"] = "";
                            if (selLevels.IndexOf("ItemCode") < 0)
                                dr_final1["ItemCode"] = "";
                            if (selLevels.IndexOf("Model") < 0)
                                dr_final1["Model"] = "";
                            if (selLevels.IndexOf("ProductName") < 0)
                                dr_final1["ProductName"] = "";*/

                            dr_final1["Rate"] = "";
                            dr_final1["NLC"] = "";
                            dr_final1["BuyRate"] = "";
                            dr_final1["VAT"] = "";

                            dr_final1["Quantity"] = "";
                            dr_final1["Rate"] = "";
                            dr_final1["NLC"] = "";
                            dr_final1["BuyRate"] = "";
                            dr_final1["VAT"] = "";

                            dr_final1["SalesValue"] = "";
                            dt.Rows.Add(dr_final1);
                        }
                    }
                    if (chkboxModel.Checked == true)
                    {
                        if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp) ||
                            (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp))
                        {
                            DataRow dr_final1 = dt.NewRow();
                            if (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp)
                            {
                                if (chkboxModel.Checked == true)
                                {
                                    dr_final1["Model"] = frthLvlValueTemp;
                                }
                                else
                                {
                                    if (chkboxCategory.Checked == true)
                                    {
                                        dr_final1["CategoryName"] = "";
                                    }
                                    if (chkboxBrand.Checked == true)
                                    {
                                        dr_final1["ProductDesc"] = "";
                                    }
                                    if (chkboxProduct.Checked == true)
                                    {
                                        dr_final1["ProductName"] = "";
                                    }
                                    if (chkboxModel.Checked == true)
                                    {
                                        dr_final1["Model"] = frthLvlValueTemp;
                                    }
                                    if (chkboxItemCode.Checked == true)
                                    {
                                        dr_final1["ItemCode"] = "";
                                    }
                                }
                            }

                            /*  if (selLevels.IndexOf("Brand") < 0)
                                  dr_final1["Brand"] = "";
                              if (selLevels.IndexOf("CategoryName") < 0)
                                  dr_final1["CategoryName"] = "";
                              if (selLevels.IndexOf("ItemCode") < 0)

                                  dr_final1["ItemCode"] = "";
                              if (selLevels.IndexOf("Model") < 0)
                                  dr_final1["Model"] = "";
                              if (selLevels.IndexOf("ProductName") < 0)
                                  dr_final1["ProductName"] = "";*/
                            dr_final1["Rate"] = "";
                            dr_final1["NLC"] = "";
                            dr_final1["BuyRate"] = "";
                            dr_final1["VAT"] = "";
                            dr_final1["Quantity"] = "";


                            dr_final1["SalesValue"] = "";
                            dt.Rows.Add(dr_final1);
                        }
                    }
                    if (chkboxItemCode.Checked == true)
                    {
                        if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp) ||
                            (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp) ||
    (fifthLvlValueTemp != "" && fifLvlValue != fifthLvlValueTemp))
                        {
                            DataRow dr_final111 = dt.NewRow();
                            if (fifthLvlValueTemp != "" && fifLvlValue != fifthLvlValueTemp)
                            {
                                if (chkboxItemCode.Checked == true)
                                {
                                    dr_final111["ItemCode"] = fifthLvlValueTemp;
                                }
                                else
                                {
                                    if (chkboxCategory.Checked == true)
                                    {
                                        dr_final111["CategoryName"] = "";
                                    }
                                    if (chkboxBrand.Checked == true)
                                    {
                                        dr_final111["ProductDesc"] = "";
                                    }
                                    if (chkboxProduct.Checked == true)
                                    {
                                        dr_final111["ProductName"] = "";
                                    }
                                    if (chkboxModel.Checked == true)
                                    {
                                        dr_final111["Model"] = "";
                                    }
                                    if (chkboxItemCode.Checked == true)
                                    {
                                        dr_final111["ItemCode"] = fifthLvlValueTemp;
                                    }

                                }
                            }

                            /*  if (selLevels.IndexOf("Brand") < 0)
                                  dr_final111["Brand"] = "";
                              if (selLevels.IndexOf("CategoryName") < 0)
                                  dr_final111["CategoryName"] = "";
                              if (selLevels.IndexOf("ItemCode") < 0)
                                  dr_final111["ItemCode"] = "";
                              if (selLevels.IndexOf("Model") < 0)
                                  dr_final111["Model"] = "";
                              if (selLevels.IndexOf("ProductName") < 0)
                                  dr_final111["ProductName"] = "";*/

                            dr_final111["Rate"] = "";
                            dr_final111["NLC"] = "";
                            dr_final111["BuyRate"] = "";
                            dr_final111["VAT"] = "";
                            dr_final111["Quantity"] = "";


                            dr_final111["SalesValue"] = "";
                            dt.Rows.Add(dr_final111);
                        }
                    }

                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;
                    frthLvlValue = frthLvlValueTemp;
                    fifLvlValue = fifthLvlValueTemp;

                    DataRow dr_final5 = dt.NewRow();
                    if (chkboxCategory.Checked == true)
                    {
                        dr_final5["CategoryName"] = dr["CategoryName"];
                    }

                    if (chkboxBrand.Checked == true)
                    {
                        dr_final5["ProductDesc"] = dr["ProductDesc"];
                    }
                    if (chkboxProduct.Checked == true)
                    {
                        dr_final5["ProductName"] = dr["ProductName"];
                    }
                    if (chkboxModel.Checked == true)
                    {
                        dr_final5["Model"] = dr["Model"];
                    }
                    if (chkboxItemCode.Checked == true)
                    {
                        dr_final5["ItemCode"] = dr["ItemCode"];
                    }

                    dr_final5["Rate"] = dr["Rate"];
                    dr_final5["NLC"] = dr["NLC"];
                    dr_final5["BuyRate"] = dr["BuyRate"];
                    dr_final5["VAT"] = dr["VAT"];
                    dr_final5["Quantity"] = dr["Stock"];


                    dr_final5["SalesValue"] = dr["SalesValue"];
                    dt.Rows.Add(dr_final5);
                    Gtotal = Gtotal + Convert.ToDecimal(dr["SalesValue"]);
                    Gttl = Gttl + Convert.ToInt32(dr["Stock"]);
                    modelTotal = modelTotal + Convert.ToDecimal(dr["SalesValue"]);
                    catIDTotal = catIDTotal + Convert.ToDecimal(dr["SalesValue"]);
                    Pttls = Pttls + Convert.ToDecimal(dr["SalesValue"]);
                    brandTotal = brandTotal + Convert.ToDecimal(dr["SalesValue"]);
                    Ittls = Ittls + Convert.ToDecimal(dr["SalesValue"]);
                }
                if ((ddlfirstlvl.SelectedIndex > 0) || (ddlsecondlvl.SelectedIndex > 0) || (ddlthirdlvl.SelectedIndex > 0) || (ddlfourlvl.SelectedIndex > 0) || (ddlfifthlvl.SelectedIndex > 0))
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

                    if (ddlfifthlvl.SelectedIndex > 0)
                        fifthLvlValueTemp = dr[ddlfifthlvl.SelectedItem.Text].ToString().ToUpper().Trim();
                    //dispLastTotal = true;
                    if (ddlfifthlvl.SelectedIndex > 0)
                    {
                        if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValue != "" && tLvlValue != tLvlValueTemp) ||
                            (frthLvlValue != "" && frthLvlValue != frthLvlValueTemp) ||
    (fifLvlValue != "" && fifLvlValue != fifthLvlValueTemp))
                        {
                            DataRow dr_final17 = dt.NewRow();
                            if (ddlfirstlvl.SelectedIndex > 0)
                            {
                                dr_final17[ddlfirstlvl.SelectedItem.Text] = "";
                            }
                            if (ddlsecondlvl.SelectedIndex > 0)
                            {
                                dr_final17[ddlsecondlvl.SelectedItem.Text] = "";
                            }
                            if (ddlthirdlvl.SelectedIndex > 0)
                            {
                                dr_final17[ddlthirdlvl.SelectedItem.Text] = "";
                            }
                            if (ddlfourlvl.SelectedIndex > 0)
                            {
                                dr_final17[ddlfourlvl.SelectedItem.Text] = "";
                            }

                            if (ddlfifthlvl.SelectedIndex > 0)
                            {
                                dr_final17[ddlfifthlvl.SelectedItem.Text] = "Total:" + fifLvlValue;
                            }
                            //dr_final7["Brand"] = "";
                            ////dr_final7[ddlOrdBySel1.selectedvalue] = "";
                            //dr_final7["CategoryName"] = "";
                            //dr_final7["Model"] = "";
                            //dr_final7["ProductName"] = "";

                            //dr_final7["ItemCode"] = "Total:" + frthLvlValueTemp;
                            /*  if (selLevels.IndexOf("Brand") < 0)
                                  dr_final17["Brand"] = "";
                              if (selLevels.IndexOf("CategoryName") < 0)
                                  dr_final17["CategoryName"] = "";
                              if (selLevels.IndexOf("ItemCode") < 0)
                                  dr_final17["ItemCode"] = "";
                              if (selLevels.IndexOf("Model") < 0)
                                  dr_final17["Model"] = "";
                              if (selLevels.IndexOf("ProductName") < 0)
                                  dr_final17["ProductName"] = "";*/

                            dr_final17["Rate"] = "";
                            dr_final17["NLC"] = "";
                            dr_final17["BuyRate"] = "";
                            dr_final17["VAT"] = "";
                            dr_final17["Quantity"] = "";


                            dr_final17["SalesValue"] = Convert.ToString(Convert.ToDecimal(Ittls));
                            dt.Rows.Add(dr_final17);
                            Ittls = 0;
                        }
                    }


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
                            //dr_final7["CategoryName"] = "";
                            //dr_final7["Model"] = "";
                            //dr_final7["ProductName"] = "";

                            //dr_final7["ItemCode"] = "Total:" + frthLvlValueTemp;
                            /* if (selLevels.IndexOf("Brand") < 0)
                                 dr_final7["Brand"] = "";
                             if (selLevels.IndexOf("CategoryName") < 0)
                                 dr_final7["CategoryName"] = "";
                             if (selLevels.IndexOf("ItemCode") < 0)
                                 dr_final7["ItemCode"] = "";
                             if (selLevels.IndexOf("Model") < 0)
                                 dr_final7["Model"] = "";
                             if (selLevels.IndexOf("ProductName") < 0)
                                 dr_final7["ProductName"] = "";*/

                            dr_final7["Rate"] = "";
                            dr_final7["NLC"] = "";
                            dr_final7["BuyRate"] = "";
                            dr_final7["VAT"] = "";
                            dr_final7["Quantity"] = "";


                            dr_final7["SalesValue"] = Convert.ToString(Convert.ToDecimal(Pttls));
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
                            if (ddlfifthlvl.SelectedIndex > 0)
                            {
                                //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                                dr_final8[ddlfifthlvl.SelectedItem.Text] = "";
                            }
                            /*  if (selLevels.IndexOf("Brand") < 0)
                                  dr_final8["Brand"] = "";
                              if (selLevels.IndexOf("CategoryName") < 0)
                                  dr_final8["CategoryName"] = "";
                              if (selLevels.IndexOf("ItemCode") < 0)
                                  dr_final8["ItemCode"] = "";
                              if (selLevels.IndexOf("Model") < 0)
                                  dr_final8["Model"] = "";
                              if (selLevels.IndexOf("ProductName") < 0)
                                  dr_final8["ProductName"] = "";*/

                            dr_final8["Rate"] = "";
                            dr_final8["NLC"] = "";
                            dr_final8["BuyRate"] = "";
                            dr_final8["VAT"] = "";
                            dr_final8["Quantity"] = "";


                            dr_final8["SalesValue"] = Convert.ToString(Convert.ToDecimal(modelTotal));
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
                            if (ddlfifthlvl.SelectedIndex > 0)
                            {
                                //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                                dr_final8[ddlfifthlvl.SelectedItem.Text] = "";
                            }
                            /*if (selLevels.IndexOf("Brand") < 0)
                                dr_final8["Brand"] = "";
                            if (selLevels.IndexOf("CategoryName") < 0)
                                dr_final8["CategoryName"] = "";
                            if (selLevels.IndexOf("ItemCode") < 0)
                                dr_final8["ItemCode"] = "";
                            if (selLevels.IndexOf("Model") < 0)
                                dr_final8["Model"] = "";
                            if (selLevels.IndexOf("ProductName") < 0)
                                dr_final8["ProductName"] = "";*/
                            dr_final8["Rate"] = "";
                            dr_final8["NLC"] = "";
                            dr_final8["BuyRate"] = "";
                            dr_final8["VAT"] = "";

                            dr_final8["Quantity"] = "";


                            dr_final8["SalesValue"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
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
                            if (ddlfifthlvl.SelectedIndex > 0)
                            {
                                //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                                dr_final8[ddlfifthlvl.SelectedItem.Text] = "";
                            }
                            /* if (selLevels.IndexOf("Brand") < 0)
                                 dr_final8["Brand"] = "";
                             if (selLevels.IndexOf("CategoryName") < 0)
                                 dr_final8["CategoryName"] = "";
                             if (selLevels.IndexOf("ItemCode") < 0)
                                 dr_final8["ItemCode"] = "";
                             if (selLevels.IndexOf("Model") < 0)
                                 dr_final8["Model"] = "";
                             if (selLevels.IndexOf("ProductName") < 0)
                                 dr_final8["ProductName"] = "";*/

                            dr_final8["Rate"] = "";
                            dr_final8["NLC"] = "";
                            dr_final8["BuyRate"] = "";
                            dr_final8["VAT"] = "";
                            dr_final8["Quantity"] = "";


                            dr_final8["SalesValue"] = Convert.ToString(Convert.ToDecimal(brandTotal));
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
                            if (ddlfifthlvl.SelectedIndex > 0)
                            {
                                //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                                dr_final1[ddlfifthlvl.SelectedItem.Text] = "";
                            }
                            /*if (selLevels.IndexOf("Brand") < 0)
                                dr_final1["Brand"] = "";
                            if (selLevels.IndexOf("CategoryName") < 0)
                                dr_final1["CategoryName"] = "";
                            if (selLevels.IndexOf("ItemCode") < 0)
                                dr_final1["ItemCode"] = "";
                            if (selLevels.IndexOf("Model") < 0)
                                dr_final1["Model"] = "";
                            if (selLevels.IndexOf("ProductName") < 0)
                                dr_final1["ProductName"] = "";*/
                            dr_final1["Rate"] = "";
                            dr_final1["NLC"] = "";
                            dr_final1["BuyRate"] = "";
                            dr_final1["VAT"] = "";

                            dr_final1["Quantity"] = "";


                            dr_final1["SalesValue"] = "";
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
                            if (ddlfifthlvl.SelectedIndex > 0)
                            {
                                //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                                dr_final2[ddlfifthlvl.SelectedItem.Text] = "";
                            }
                            /*  if (selLevels.IndexOf("Brand") < 0)
                                  dr_final2["Brand"] = "";
                              if (selLevels.IndexOf("CategoryName") < 0)
                                  dr_final2["CategoryName"] = "";
                              if (selLevels.IndexOf("ItemCode") < 0)
                                  dr_final2["ItemCode"] = "";
                              if (selLevels.IndexOf("Model") < 0)
                                  dr_final2["Model"] = "";
                              if (selLevels.IndexOf("ProductName") < 0)
                                  dr_final2["ProductName"] = "";*/
                            dr_final2["Rate"] = "";
                            dr_final2["NLC"] = "";
                            dr_final2["BuyRate"] = "";
                            dr_final2["VAT"] = "";

                            dr_final2["Quantity"] = "";


                            dr_final2["SalesValue"] = "";
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
                            if (ddlfifthlvl.SelectedIndex > 0)
                            {
                                //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                                dr_final1[ddlfifthlvl.SelectedItem.Text] = "";
                            }
                            /*if (selLevels.IndexOf("Brand") < 0)
                                dr_final1["Brand"] = "";
                            if (selLevels.IndexOf("CategoryName") < 0)
                                dr_final1["CategoryName"] = "";
                            if (selLevels.IndexOf("ItemCode") < 0)
                                dr_final1["ItemCode"] = "";
                            if (selLevels.IndexOf("Model") < 0)
                                dr_final1["Model"] = "";
                            if (selLevels.IndexOf("ProductName") < 0)
                                dr_final1["ProductName"] = "";*/

                            dr_final1["Rate"] = "";
                            dr_final1["NLC"] = "";
                            dr_final1["BuyRate"] = "";
                            dr_final1["VAT"] = "";

                            dr_final1["Quantity"] = "";
                            dr_final1["Rate"] = "";
                            dr_final1["NLC"] = "";
                            dr_final1["BuyRate"] = "";
                            dr_final1["VAT"] = "";

                            dr_final1["SalesValue"] = "";
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
                                    if (ddlfifthlvl.SelectedIndex > 0)
                                    {
                                        //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                                        dr_final1[ddlfifthlvl.SelectedItem.Text] = "";
                                    }
                                }
                            }

                            /*  if (selLevels.IndexOf("Brand") < 0)
                                  dr_final1["Brand"] = "";
                              if (selLevels.IndexOf("CategoryName") < 0)
                                  dr_final1["CategoryName"] = "";
                              if (selLevels.IndexOf("ItemCode") < 0)

                                  dr_final1["ItemCode"] = "";
                              if (selLevels.IndexOf("Model") < 0)
                                  dr_final1["Model"] = "";
                              if (selLevels.IndexOf("ProductName") < 0)
                                  dr_final1["ProductName"] = "";*/
                            dr_final1["Rate"] = "";
                            dr_final1["NLC"] = "";
                            dr_final1["BuyRate"] = "";
                            dr_final1["VAT"] = "";
                            dr_final1["Quantity"] = "";


                            dr_final1["SalesValue"] = "";
                            dt.Rows.Add(dr_final1);
                        }
                    }
                    if (ddlfifthlvl.SelectedIndex > 0)
                    {
                        if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                            (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                            (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp) ||
                            (frthLvlValueTemp != "" && frthLvlValue != frthLvlValueTemp) ||
    (fifthLvlValueTemp != "" && fifLvlValue != fifthLvlValueTemp))
                        {
                            DataRow dr_final111 = dt.NewRow();
                            if (fifthLvlValueTemp != "" && fifLvlValue != fifthLvlValueTemp)
                            {
                                if (ddlfifthlvl.SelectedIndex > 0)
                                {
                                    dr_final111[ddlfifthlvl.SelectedItem.Text] = fifthLvlValueTemp;
                                }
                                else
                                {
                                    if (ddlfirstlvl.SelectedIndex > 0)
                                    {
                                        dr_final111[ddlfirstlvl.SelectedItem.Text] = "";
                                    }
                                    if (ddlsecondlvl.SelectedIndex > 0)
                                    {
                                        dr_final111[ddlsecondlvl.SelectedItem.Text] = "";
                                    }
                                    if (ddlthirdlvl.SelectedIndex > 0)
                                    {
                                        dr_final111[ddlthirdlvl.SelectedItem.Text] = "";
                                    }
                                    if (ddlfourlvl.SelectedIndex > 0)
                                    {
                                        dr_final111[ddlfourlvl.SelectedItem.Text] = "";
                                    }

                                    if (ddlfifthlvl.SelectedIndex > 0)
                                    {
                                        dr_final111[ddlfifthlvl.SelectedItem.Text] = fifthLvlValueTemp;
                                    }

                                }
                            }

                            /*  if (selLevels.IndexOf("Brand") < 0)
                                  dr_final111["Brand"] = "";
                              if (selLevels.IndexOf("CategoryName") < 0)
                                  dr_final111["CategoryName"] = "";
                              if (selLevels.IndexOf("ItemCode") < 0)
                                  dr_final111["ItemCode"] = "";
                              if (selLevels.IndexOf("Model") < 0)
                                  dr_final111["Model"] = "";
                              if (selLevels.IndexOf("ProductName") < 0)
                                  dr_final111["ProductName"] = "";*/

                            dr_final111["Rate"] = "";
                            dr_final111["NLC"] = "";
                            dr_final111["BuyRate"] = "";
                            dr_final111["VAT"] = "";
                            dr_final111["Quantity"] = "";


                            dr_final111["SalesValue"] = "";
                            dt.Rows.Add(dr_final111);
                        }
                    }

                    fLvlValue = fLvlValueTemp;
                    sLvlValue = sLvlValueTemp;
                    tLvlValue = tLvlValueTemp;
                    frthLvlValue = frthLvlValueTemp;
                    fifLvlValue = fifthLvlValueTemp;

                    DataRow dr_final5 = dt.NewRow();
                    if (ddlfirstlvl.SelectedIndex > 0)
                    {

                        if (dr_final5[ddlfirstlvl.SelectedItem.Text] == "Brand")
                        {
                            dr_final5[ddlfirstlvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (dr_final5[ddlfirstlvl.SelectedItem.Text] == "CategoryName")
                        {
                            dr_final5[ddlfirstlvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (dr_final5[ddlfirstlvl.SelectedItem.Text] == "ItemCode")
                        {
                            dr_final5[ddlfirstlvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                        if (dr_final5[ddlfirstlvl.SelectedItem.Text] == "ProductName")
                        {
                            dr_final5[ddlfirstlvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (dr_final5[ddlfirstlvl.SelectedItem.Text] == "Model")
                        {
                            dr_final5[ddlfirstlvl.SelectedItem.Text] = dr["Model"];
                        }
                    }

                    if (ddlsecondlvl.SelectedIndex > 0)
                    {
                        if (dr_final5[ddlsecondlvl.SelectedItem.Text] == "Brand")
                        {
                            dr_final5[ddlsecondlvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (dr_final5[ddlsecondlvl.SelectedItem.Text] == "CategoryName")
                        {
                            dr_final5[ddlsecondlvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (dr_final5[ddlsecondlvl.SelectedItem.Text] == "ItemCode")
                        {
                            dr_final5[ddlsecondlvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                        if (dr_final5[ddlsecondlvl.SelectedItem.Text] == "ProductName")
                        {
                            dr_final5[ddlsecondlvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (dr_final5[ddlsecondlvl.SelectedItem.Text] == "Model")
                        {
                            dr_final5[ddlsecondlvl.SelectedItem.Text] = dr["Model"];
                        }

                    }
                    if (ddlthirdlvl.SelectedIndex > 0)
                    {
                        if (dr_final5[ddlthirdlvl.SelectedItem.Text] == "Brand")
                        {
                            dr_final5[ddlthirdlvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (dr_final5[ddlthirdlvl.SelectedItem.Text] == "CategoryName")
                        {
                            dr_final5[ddlthirdlvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (dr_final5[ddlthirdlvl.SelectedItem.Text] == "ItemCode")
                        {
                            dr_final5[ddlthirdlvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                        if (dr_final5[ddlthirdlvl.SelectedItem.Text] == "ProductName")
                        {
                            dr_final5[ddlthirdlvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (dr_final5[ddlthirdlvl.SelectedItem.Text] == "Model")
                        {
                            dr_final5[ddlthirdlvl.SelectedItem.Text] = dr["Model"];
                        }


                    }
                    if (ddlfourlvl.SelectedIndex > 0)
                    {
                        if (dr_final5[ddlfourlvl.SelectedItem.Text] == "Brand")
                        {
                            dr_final5[ddlfourlvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (dr_final5[ddlfourlvl.SelectedItem.Text] == "CategoryName")
                        {
                            dr_final5[ddlfourlvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (dr_final5[ddlfourlvl.SelectedItem.Text] == "ItemCode")
                        {
                            dr_final5[ddlfourlvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                        if (dr_final5[ddlfourlvl.SelectedItem.Text] == "ProductName")
                        {
                            dr_final5[ddlfourlvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (dr_final5[ddlfourlvl.SelectedItem.Text] == "Model")
                        {
                            dr_final5[ddlfourlvl.SelectedItem.Text] = dr["Model"];
                        }


                    }
                    if (ddlfifthlvl.SelectedIndex > 0)
                    {
                        if (dr_final5[ddlfifthlvl.SelectedItem.Text] == "Brand")
                        {
                            dr_final5[ddlfifthlvl.SelectedItem.Text] = dr["ProductDesc"];
                        }
                        if (dr_final5[ddlfifthlvl.SelectedItem.Text] == "CategoryName")
                        {
                            dr_final5[ddlfifthlvl.SelectedItem.Text] = dr["CategoryName"];
                        }
                        if (dr_final5[ddlfifthlvl.SelectedItem.Text] == "ItemCode")
                        {
                            dr_final5[ddlfifthlvl.SelectedItem.Text] = dr["ItemCode"];
                        }
                        if (dr_final5[ddlfifthlvl.SelectedItem.Text] == "ProductName")
                        {
                            dr_final5[ddlfifthlvl.SelectedItem.Text] = dr["ProductName"];
                        }
                        if (dr_final5[ddlfifthlvl.SelectedItem.Text] == "Model")
                        {
                            dr_final5[ddlfifthlvl.SelectedItem.Text] = dr["Model"];
                        }
                        dr_final5["Rate"] = dr["Rate"];
                        dr_final5["NLC"] = dr["NLC"];
                        dr_final5["BuyRate"] = dr["BuyRate"];
                        dr_final5["VAT"] = dr["VAT"];
                        dr_final5["Quantity"] = dr["Stock"];


                        dr_final5["SalesValue"] = dr["SalesValue"];
                        dt.Rows.Add(dr_final5);
                        Gtotal = Gtotal + Convert.ToDecimal(dr["SalesValue"]);
                        Gttl = Gttl + Convert.ToInt32(dr["Stock"]);
                        modelTotal = modelTotal + Convert.ToDecimal(dr["SalesValue"]);
                        catIDTotal = catIDTotal + Convert.ToDecimal(dr["SalesValue"]);
                        Pttls = Pttls + Convert.ToDecimal(dr["SalesValue"]);
                        brandTotal = brandTotal + Convert.ToDecimal(dr["SalesValue"]);
                        Ittls = Ittls + Convert.ToDecimal(dr["SalesValue"]);

                    }

                }
                /* if (selLevels.IndexOf("Brand") < 0)
                     dr_final5["Brand"] = dr["ProductDesc"];
                 if (selLevels.IndexOf("CategoryName") < 0)
                     dr_final5["CategoryName"] = dr["CategoryName"];
                 if (selLevels.IndexOf("ItemCode") < 0)
                     dr_final5["ItemCode"] = dr["ItemCode"];
                 if (selLevels.IndexOf("Model") < 0)
                     dr_final5["Model"] = dr["Model"];
                 if (selLevels.IndexOf("ProductName") < 0)
                     dr_final5["ProductName"] = dr["ProductName"];*/

                
            }

            //Display the last Total and Grand Total
            if (dispLastTotal)
            {
                if ((chkboxCategory.Checked) || (chkboxBrand.Checked) || (chkboxProduct.Checked) || (chkboxModel.Checked) || (chkboxItemCode.Checked))
                {
                    if (chkboxItemCode.Checked == true)
                    {
                        DataRow dr_final17 = dt.NewRow();
                        if (chkboxCategory.Checked == true)
                        {
                            dr_final17["CategoryName"] = "";
                        }
                        if (chkboxBrand.Checked == true)
                        {
                            dr_final17["ProductDesc"] = "";
                        }
                        if (chkboxProduct.Checked == true)
                        {
                            dr_final17["ProductName"] = "";
                        }
                        if (chkboxModel.Checked == true)
                        {
                            dr_final17["Model"] = "";
                        }

                        dr_final17["Itemcode"] = "Total:" + fifthLvlValueTemp;
                        dr_final17["Rate"] = "";
                        dr_final17["NLC"] = "";
                        dr_final17["BuyRate"] = "";
                        dr_final17["VAT"] = "";


                        dr_final17["Quantity"] = "";

                        dr_final17["SalesValue"] = Convert.ToString(Convert.ToDecimal(Ittls));
                        dt.Rows.Add(dr_final17);
                        Ittls = 0;
                    }
                    if (chkboxModel.Checked == true)
                    {
                        DataRow dr_final7 = dt.NewRow();
                        if (chkboxCategory.Checked == true)
                        {
                            dr_final7["CategoryName"] = "";
                        }
                        if (chkboxBrand.Checked == true)
                        {
                            dr_final7["ProductDesc"] = "";
                        }
                        if (chkboxProduct.Checked == true)
                        {
                            dr_final7["ProductName"] = "";
                        }
                        if (chkboxItemCode.Checked == true)
                        {
                            dr_final7["ItemCode"] = "";
                        }

                        dr_final7["Model"] = "Total:" + frthLvlValueTemp;

                        /*if (selLevels.IndexOf("Brand") < 0)
                            dr_final7["Brand"] = "";
                        if (selLevels.IndexOf("CategoryName") < 0)
                            dr_final7["CategoryName"] = "";
                        if (selLevels.IndexOf("ItemCode") < 0)
                            dr_final7["ItemCode"] = "";
                        if (selLevels.IndexOf("Model") < 0)
                            dr_final7["Model"] = "";
                        if (selLevels.IndexOf("ProductName") < 0)
                            dr_final7["ProductName"] = "";*/
                        dr_final7["Rate"] = "";
                        dr_final7["NLC"] = "";
                        dr_final7["BuyRate"] = "";
                        dr_final7["VAT"] = "";

                        dr_final7["Quantity"] = "";


                        dr_final7["SalesValue"] = Convert.ToString(Convert.ToDecimal(Pttls));
                        dt.Rows.Add(dr_final7);
                        Pttls = 0;
                    }

                    if (chkboxProduct.Checked == true)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        if (chkboxCategory.Checked == true)
                        {
                            dr_final8["CategoryName"] = "";
                        }
                        if (chkboxBrand.Checked == true)
                        {
                            dr_final8["ProductDesc"] = "";
                        }
                        if (chkboxModel.Checked == true)
                        {
                            dr_final8["Model"] = "";
                        }
                        if (chkboxItemCode.Checked == true)
                        {
                            dr_final8["ItemCode"] = "";
                        }
                        dr_final8["ProductName"] = "Total:" + tLvlValueTemp;
                        /* if (selLevels.IndexOf("Brand") < 0)
                             dr_final8["Brand"] = "";
                         if (selLevels.IndexOf("CategoryName") < 0)
                             dr_final8["CategoryName"] = "";
                         if (selLevels.IndexOf("ItemCode") < 0)
                             dr_final8["ItemCode"] = "";
                         if (selLevels.IndexOf("Model") < 0)
                             dr_final8["Model"] = "";
                         if (selLevels.IndexOf("ProductName") < 0)
                             dr_final8["ProductName"] = "";*/
                        dr_final8["Rate"] = "";
                        dr_final8["NLC"] = "";
                        dr_final8["BuyRate"] = "";
                        dr_final8["VAT"] = "";

                        dr_final8["Quantity"] = "";


                        dr_final8["SalesValue"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                        dt.Rows.Add(dr_final8);
                        modelTotal = 0;
                    }

                    if (chkboxBrand.Checked == true)
                    {
                        DataRow dr_final9 = dt.NewRow();
                        if (chkboxCategory.Checked == true)
                        {
                            dr_final9["CategoryName"] = "";
                        }
                        if (chkboxProduct.Checked == true)
                        {
                            dr_final9["ProductName"] = "";
                        }
                        if (chkboxModel.Checked == true)
                        {
                            dr_final9["Model"] = "";
                        }
                        if (chkboxItemCode.Checked == true)
                        {
                            dr_final9["ItemCode"] = "";
                        }
                        dr_final9["ProductDesc"] = "Total:" + sLvlValueTemp;
                        /* if (selLevels.IndexOf("Brand") < 0)
                             dr_final9["Brand"] = "";
                         if (selLevels.IndexOf("CategoryName") < 0)
                             dr_final9["CategoryName"] = "";
                         if (selLevels.IndexOf("ItemCode") < 0)
                             dr_final9["ItemCode"] = "";
                         if (selLevels.IndexOf("Model") < 0)
                             dr_final9["Model"] = "";
                         if (selLevels.IndexOf("ProductName") < 0)
                             dr_final9["ProductName"] = "";*/
                        dr_final9["Rate"] = "";
                        dr_final9["NLC"] = "";
                        dr_final9["BuyRate"] = "";
                        dr_final9["VAT"] = "";

                        dr_final9["Quantity"] = "";


                        dr_final9["SalesValue"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                        dt.Rows.Add(dr_final9);
                        catIDTotal = 0;
                    }

                    if (chkboxCategory.Checked == true)
                    {
                        DataRow dr_final10 = dt.NewRow();
                        if (chkboxBrand.Checked == true)
                        {
                            dr_final10["CategoryName"] = "";
                        }
                        if (chkboxProduct.Checked == true)
                        {
                            dr_final10["ProductName"] = "";
                        }
                        if (chkboxModel.Checked == true)
                        {
                            dr_final10["Model"] = "";
                        }
                        if (chkboxItemCode.Checked == true)
                        {
                            dr_final10["ItemCode"] = "";
                        }
                        dr_final10["CategoryName"] = "Total:" + fLvlValueTemp;

                        /* if (selLevels.IndexOf("Brand") < 0)
                             dr_final10["Brand"] = "";
                         if (selLevels.IndexOf("CategoryName") < 0)
                             dr_final10["CategoryName"] = "";
                         if (selLevels.IndexOf("ItemCode") < 0)
                             dr_final10["ItemCode"] = "";
                         if (selLevels.IndexOf("Model") < 0)
                             dr_final10["Model"] = "";
                         if (selLevels.IndexOf("ProductName") < 0)
                             dr_final10["ProductName"] = "";*/

                        dr_final10["Rate"] = "";
                        dr_final10["NLC"] = "";
                        dr_final10["BuyRate"] = "";
                        dr_final10["VAT"] = "";

                        dr_final10["Quantity"] = "";

                        dr_final10["SalesValue"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                        dt.Rows.Add(dr_final10);
                        brandTotal = 0;
                    }

                    DataRow dr_final6 = dt.NewRow();
                    if (chkboxCategory.Checked == true)
                    {
                        dr_final6["CategoryName"] = "Grand Total: ";
                    }
                    if (chkboxBrand.Checked == true)
                    {
                        dr_final6["ProductDesc"] = "";
                    }
                    if (chkboxProduct.Checked == true)
                    {
                        dr_final6["ProductName"] = "";
                    }
                    if (chkboxModel.Checked == true)
                    {
                        dr_final6["Model"] = "";
                    }
                    if (chkboxItemCode.Checked == true)
                    {
                        dr_final6["ItemCode"] = "";
                    }

                    /* if (selLevels.IndexOf("Brand") < 0)
                         dr_final6["Brand"] = "";
                     if (selLevels.IndexOf("CategoryName") < 0)
                         dr_final6["CategoryName"] = "";
                     if (selLevels.IndexOf("ItemCode") < 0)
                         dr_final6["ItemCode"] = "Grand Total: ";
                     if (selLevels.IndexOf("Model") < 0)
                         dr_final6["Model"] = "";
                     if (selLevels.IndexOf("ProductName") < 0)
                         dr_final6["ProductName"] = "";*/

                    dr_final6["Rate"] = "";
                    dr_final6["NLC"] = "";
                    dr_final6["BuyRate"] = "";
                    dr_final6["VAT"] = "";
                    dr_final6["Quantity"] = Convert.ToInt32(Gttl);


                    dr_final6["SalesValue"] = Convert.ToDecimal(Gtotal);
                    dt.Rows.Add(dr_final6);
                }
            }
            if ((ddlfirstlvl.SelectedIndex > 0) || (ddlsecondlvl.SelectedIndex > 0) || (ddlthirdlvl.SelectedIndex > 0) || (ddlfourlvl.SelectedIndex > 0) || (ddlfifthlvl.SelectedIndex > 0))
            {

                if (ddlfifthlvl.SelectedIndex > 0)
                {
                    DataRow dr_final17 = dt.NewRow();
                    if (ddlfirstlvl.SelectedIndex > 0)
                    {
                        dr_final17[ddlfirstlvl.SelectedItem.Text] = "";
                    }
                    if (ddlsecondlvl.SelectedIndex > 0)
                    {
                        dr_final17[ddlsecondlvl.SelectedItem.Text] = "";
                    }
                    if (ddlthirdlvl.SelectedIndex > 0)
                    {
                        dr_final17[ddlthirdlvl.SelectedItem.Text] = "";
                    }
                    if (ddlfourlvl.SelectedIndex > 0)
                    {
                        dr_final17[ddlfourlvl.SelectedItem.Text] = "";
                    }


                    dr_final17[ddlfifthlvl.SelectedItem.Text] = "Total:" + fifthLvlValueTemp;

                    /* if (selLevels.IndexOf("Brand") < 0)
                         dr_final17["Brand"] = "";
                     if (selLevels.IndexOf("CategoryName") < 0)
                         dr_final17["CategoryName"] = "";
                     if (selLevels.IndexOf("ItemCode") < 0)
                         dr_final17["ItemCode"] = "";
                     if (selLevels.IndexOf("Model") < 0)
                         dr_final17["Model"] = "";
                     if (selLevels.IndexOf("ProductName") < 0)
                         dr_final17["ProductName"] = "";*/
                    dr_final17["Rate"] = "";
                    dr_final17["NLC"] = "";
                    dr_final17["BuyRate"] = "";
                    dr_final17["VAT"] = "";


                    dr_final17["Quantity"] = "";

                    dr_final17["SalesValue"] = Convert.ToString(Convert.ToDecimal(Ittls));
                    dt.Rows.Add(dr_final17);
                    Ittls = 0;
                }


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
                    if (ddlfifthlvl.SelectedIndex > 0)
                    {
                        //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                        dr_final7[ddlfifthlvl.SelectedItem.Text] = "";
                    }

                    dr_final7[ddlfourlvl.SelectedItem.Text] = "Total:" + frthLvlValueTemp;

                    /*if (selLevels.IndexOf("Brand") < 0)
                        dr_final7["Brand"] = "";
                    if (selLevels.IndexOf("CategoryName") < 0)
                        dr_final7["CategoryName"] = "";
                    if (selLevels.IndexOf("ItemCode") < 0)
                        dr_final7["ItemCode"] = "";
                    if (selLevels.IndexOf("Model") < 0)
                        dr_final7["Model"] = "";
                    if (selLevels.IndexOf("ProductName") < 0)
                        dr_final7["ProductName"] = "";*/
                    dr_final7["Rate"] = "";
                    dr_final7["NLC"] = "";
                    dr_final7["BuyRate"] = "";
                    dr_final7["VAT"] = "";

                    dr_final7["Quantity"] = "";


                    dr_final7["SalesValue"] = Convert.ToString(Convert.ToDecimal(Pttls));
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
                    if (ddlfifthlvl.SelectedIndex > 0)
                    {
                        //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                        dr_final8[ddlfifthlvl.SelectedItem.Text] = "";
                    }
                    /* if (selLevels.IndexOf("Brand") < 0)
                         dr_final8["Brand"] = "";
                     if (selLevels.IndexOf("CategoryName") < 0)
                         dr_final8["CategoryName"] = "";
                     if (selLevels.IndexOf("ItemCode") < 0)
                         dr_final8["ItemCode"] = "";
                     if (selLevels.IndexOf("Model") < 0)
                         dr_final8["Model"] = "";
                     if (selLevels.IndexOf("ProductName") < 0)
                         dr_final8["ProductName"] = "";*/
                    dr_final8["Rate"] = "";
                    dr_final8["NLC"] = "";
                    dr_final8["BuyRate"] = "";
                    dr_final8["VAT"] = "";

                    dr_final8["Quantity"] = "";


                    dr_final8["SalesValue"] = Convert.ToString(Convert.ToDecimal(modelTotal));
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
                    if (ddlfifthlvl.SelectedIndex > 0)
                    {
                        //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                        dr_final9[ddlfifthlvl.SelectedItem.Text] = "";
                    }
                    /* if (selLevels.IndexOf("Brand") < 0)
                         dr_final9["Brand"] = "";
                     if (selLevels.IndexOf("CategoryName") < 0)
                         dr_final9["CategoryName"] = "";
                     if (selLevels.IndexOf("ItemCode") < 0)
                         dr_final9["ItemCode"] = "";
                     if (selLevels.IndexOf("Model") < 0)
                         dr_final9["Model"] = "";
                     if (selLevels.IndexOf("ProductName") < 0)
                         dr_final9["ProductName"] = "";*/
                    dr_final9["Rate"] = "";
                    dr_final9["NLC"] = "";
                    dr_final9["BuyRate"] = "";
                    dr_final9["VAT"] = "";

                    dr_final9["Quantity"] = "";


                    dr_final9["SalesValue"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
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
                    if (ddlfifthlvl.SelectedIndex > 0)
                    {
                        //dr_final8[ddlfourlvl.SelectedItem.Text] = "Total " + tLvlValueTemp;
                        dr_final10[ddlfifthlvl.SelectedItem.Text] = "";
                    }

                    /* if (selLevels.IndexOf("Brand") < 0)
                         dr_final10["Brand"] = "";
                     if (selLevels.IndexOf("CategoryName") < 0)
                         dr_final10["CategoryName"] = "";
                     if (selLevels.IndexOf("ItemCode") < 0)
                         dr_final10["ItemCode"] = "";
                     if (selLevels.IndexOf("Model") < 0)
                         dr_final10["Model"] = "";
                     if (selLevels.IndexOf("ProductName") < 0)
                         dr_final10["ProductName"] = "";*/

                    dr_final10["Rate"] = "";
                    dr_final10["NLC"] = "";
                    dr_final10["BuyRate"] = "";
                    dr_final10["VAT"] = "";

                    dr_final10["Quantity"] = "";

                    dr_final10["SalesValue"] = Convert.ToString(Convert.ToDecimal(brandTotal));
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
                if (ddlfifthlvl.SelectedIndex > 0)
                {
                    dr_final6[ddlfifthlvl.SelectedItem.Text] = "";
                }

                /* if (selLevels.IndexOf("Brand") < 0)
                     dr_final6["Brand"] = "";
                 if (selLevels.IndexOf("CategoryName") < 0)
                     dr_final6["CategoryName"] = "";
                 if (selLevels.IndexOf("ItemCode") < 0)
                     dr_final6["ItemCode"] = "Grand Total: ";
                 if (selLevels.IndexOf("Model") < 0)
                     dr_final6["Model"] = "";
                 if (selLevels.IndexOf("ProductName") < 0)
                     dr_final6["ProductName"] = "";*/

                dr_final6["Rate"] = "";
                dr_final6["NLC"] = "";
                dr_final6["BuyRate"] = "";
                dr_final6["VAT"] = "";
                dr_final6["Quantity"] = Convert.ToInt32(Gttl);


                dr_final6["SalesValue"] = Convert.ToDecimal(Gtotal);
                dt.Rows.Add(dr_final6);
            }
        }
            ExportToExcel(dt);
            //}
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

    decimal Total;
  /*  protected void GridStock_RowDataBound(object sender, GridViewRowEventArgs e)
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
            var ValueAmt = e.Row.FindControl("lblSalesValue") as Label;
            if (ValueAmt != null)
            {
                Total += decimal.Parse(ValueAmt.Text);
            }
        }

    }
    protected void GridStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridStock.PageIndex = e.NewPageIndex;
        // bindGridStock();
        //bindData(selCols, field2, cond, grpBy, ordrby);
        GridStock.DataBind();
    }*/
protected void  Button2_Click(object sender, EventArgs e)
{
    try
    {
        int i = 0;
        int j = 0;
        objBL.savenames = TextBox6.Text;
        string savedname = TextBox6.Text;
        DataSet ds1 = new DataSet();
        ds1 = objBL.getsaveddata(savedname);
        //DataGrid dsg = new Datagrid();
        GridView1.DataSource = ds1;
        //ddlBrand.DataTextField = "Brand";
        //ddlBrand.DataValueField = "Brand";
        GridView1.DataBind();
        GridView1.Visible = false;
        // GridView1.EnableViewState = true;
        DataTable dts = new DataTable();

        //if(DataRow dr[savedname] == "kanna")
        // {
        /*  if (ds1.Tables[0].Columns[1].ToString() == savedname)
           {
               for (i = 1; i < ds1.Tables[2].Columns.Count; i++)
               {


                   if (ds1.Tables[0].Columns[i].ToString() == "chkCategory")
                   {
                       chkboxCategory.Checked = true;
                       i++;
                   }

                   if (ds1.Tables[0].Columns[i].ToString() == "chkBrand")
                   {
                       chkboxBrand.Checked = true;
                       i++;
                   }
                   if (ds1.Tables[0].Columns[i].ToString() == "chkModel")
                   {
                       chkboxModel.Checked = true;
                       i++;
                   }
                   if (ds1.Tables[0].Columns[i].ToString() == "chkProduct")
                   {
                       chkboxProduct.Checked = true;
                       i++;
                   }

               }

           }*/
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
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkProduct")
                    {
                        chkboxProduct.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkModel")
                    {
                        chkboxModel.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkItemcode")
                    {
                        chkboxItemCode.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkstock")
                    {
                        chkboxStock.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkNlc")
                    {
                        chkboxNlc.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkVat")
                    {
                        chkboxVat.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkRate")
                    {
                        chkboxRate.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkBuyRate")
                    {
                        chkboxBuyrate.Checked = true;

                    }
                    j++;
                    if (ds1.Tables[0].Rows[i][j].ToString() == "chkAll")
                    {
                        chkboxAll.Checked = true;

                    }
                    j++;
                    string sdBrand = ds1.Tables[0].Rows[i][12].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdBrand)
                    {
                        ddlBrand.SelectedItem.Text = sdBrand;
                        j++;
                    }
                    string sdCat = ds1.Tables[0].Rows[i][13].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdCat)
                    {
                        ddlCategory.SelectedItem.Text = sdCat;
                        j++;
                    }
                    string sdProd = ds1.Tables[0].Rows[i][14].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdProd)
                    {
                        ddlPrdctNme.SelectedItem.Text = sdProd;
                        j++;
                    }
                    string sdMod = ds1.Tables[0].Rows[i][15].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdMod)
                    {
                        ddlMdl.SelectedItem.Text = sdMod;
                        j++;
                    }
                    string sditem = ds1.Tables[0].Rows[i][16].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sditem)
                    {
                        ddlItemCode.SelectedItem.Text = sditem;
                        j++;
                    }
                    string sdstoc = ds1.Tables[0].Rows[i][17].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdstoc)
                    {
                        ddlStock.SelectedItem.Text = sdstoc;
                        j++;
                    }
                    string sdVat = ds1.Tables[0].Rows[i][18].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdVat)
                    {
                        ddlVat.SelectedItem.Text = sdVat;
                        j++;
                    }
                    string sdNlc = ds1.Tables[0].Rows[i][19].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdNlc)
                    {
                        ddlNlc.SelectedItem.Text = sdNlc;
                        j++;
                    }
                    string sdBuyrate = ds1.Tables[0].Rows[i][20].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdBuyrate)
                    {
                        ddlBuyrate.SelectedItem.Text = sdBuyrate;
                        j++;
                    }
                    string sdrate = ds1.Tables[0].Rows[i][21].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdrate)
                    {
                        ddlRate.SelectedItem.Text = sdrate;
                        j++;
                    }
                    string sdstock = ds1.Tables[0].Rows[i][22].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdstock)
                    {
                        ddlBrand.SelectedItem.Text = sdstock;
                        j++;
                    }
                    string sdfirstlvl = ds1.Tables[0].Rows[i][23].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdfirstlvl)
                    {
                        ddlfirstlvl.SelectedItem.Text = sdfirstlvl;
                        j++;
                    }
                    string sdsecondlvl = ds1.Tables[0].Rows[i][24].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdsecondlvl)
                    {
                        ddlsecondlvl.SelectedItem.Text = sdsecondlvl;
                        j++;
                    }
                    string sdthirdlvl = ds1.Tables[0].Rows[i][25].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdthirdlvl)
                    {
                        ddlthirdlvl.SelectedItem.Text = sdthirdlvl;
                        j++;
                    }
                    string sdfourlvl = ds1.Tables[0].Rows[i][26].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdfourlvl)
                    {
                        ddlfourlvl.SelectedItem.Text = sdfourlvl;
                        j++;
                    }
                    string sdfifthlvl = ds1.Tables[0].Rows[i][27].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sdfifthlvl)
                    {
                        ddlfifthlvl.SelectedItem.Text = sdfifthlvl;
                        j++;
                    }
                    string sodfirstlvl = ds1.Tables[0].Rows[i][28].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodfirstlvl)
                    {
                        odlfirstlvl.SelectedItem.Text = sodfirstlvl;
                        j++;
                    }
                    string sodsecondlvl = ds1.Tables[0].Rows[i][29].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodsecondlvl)
                    {
                        odlsecondlvl.SelectedItem.Text = sodsecondlvl;
                        j++;
                    }
                    string sodthirdlvl = ds1.Tables[0].Rows[i][30].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodthirdlvl)
                    {
                        odlthirdlvl.SelectedItem.Text = sodthirdlvl;
                        j++;
                    }
                    string sodfourlvl = ds1.Tables[0].Rows[i][31].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodfourlvl)
                    {
                        odlfourlvl.SelectedItem.Text = sodfourlvl;
                        j++;
                    }
                    string sodfifthlvl = ds1.Tables[0].Rows[i][32].ToString();
                    if (ds1.Tables[0].Rows[i][j].ToString() == sodfifthlvl)
                    {
                        odlfifthlvl.SelectedItem.Text = sodfifthlvl;
                        j++;
                    }
                }

                //}

            }
        }

        //row["b"] = gv.Rows[i][column2].ToString();
        //MynewDatatable.Rows.Add(row);



        //}  
    }
    catch (Exception ex)
    {
        TroyLiteExceptionManager.HandleException(ex);
    }
}   
protected void  AllChecked(object sender, EventArgs e)
{
    try
    {
        chkboxBrand.Checked = true;
        chkboxCategory.Checked = true;
        chkboxModel.Checked = true;
        chkboxProduct.Checked = true;
        chkboxItemCode.Checked = true;
        chkboxBuyrate.Checked = true;
        chkboxNlc.Checked = true;
        chkboxRate.Checked = true;
        chkboxStock.Checked = true;
        chkboxVat.Checked = true;
    }
    catch (Exception ex)
    {
        TroyLiteExceptionManager.HandleException(ex);
    }
}



protected void Button1_Click(object sender, EventArgs e)
{
    try
    {
        int i = 0;
        int j = 0;
        objBL.savenames = TextBox6.Text;
        string savedname = TextBox6.Text;
        string chkCat = "";
        string chkBran = "";
        string chkProd = "";
        string chkMod = "";
        string chkitem = "";
        string chkstoc = "";
        string chksNlc = "";
        string chksVat = "";
        string chksBuyrate = "";
        string chksrate = "";
        string chksAll = "";
        string sdBrand = "";
        string sdCat = "";

        string sdProd = "";
        string sdMod = "";
        string sditem = "";
        string sdstoc = "";
        string sdVat = "";
        string sdBuyrate = "";
        string sdNlc = "";
        string sdrate = "";
        string sdfirstlvl = "";
        string sdsecondlvl = "";
        string sdthirdlvl = "";
        string sdfourlvl = "";
        string sdfifthlvl = "";
        string sodfirstlvl = "";
        string sodsecondlvl = "";
        string sodthirdlvl = "";
        string sodfourlvl = "";
        string sodfifthlvl = "";

        string sdFirstSub = "";
        string sdSecondSub = "";
        string sdThirdSub = "";
        string sdFourSub = "";
        string sdFiveSub = "";

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

        if (chkboxProduct.Checked == true)
        {
            objBL.chkProds = "chkProduct";
            chkProd = "chkProduct";
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
        if (chkboxItemCode.Checked == true)
        {
            objBL.chkitems = "chkItemcode";
            chkitem = "chkItemcode";
        }
        else
        {
            objBL.chkitems = "null";
            chkitem = "null";
        }
        if (chkboxStock.Checked == true)
        {
            objBL.chkstocs = "chkstock";
            chkstoc = "chkstock";
        }
        else
        {
            objBL.chkstocs = "null";
            chkstoc = "null";
        }
        if (chkboxNlc.Checked == true)
        {
            objBL.chksNlcs = "chkNlc";
            chksNlc = "chkNlc";
        }
        else
        {
            objBL.chksNlcs = "null";
            chksNlc = "null";
        }
        if (chkboxVat.Checked == true)
        {
            objBL.chksVats = "chkVat";
            chksVat = "chkVat";
        }
        else
        {
            objBL.chksVats = "null";
            chksVat = "null";
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
        if (chkboxBuyrate.Checked == true)
        {
            objBL.chksBuyrates = "chkBuyRate";
            chksBuyrate = "chkBuyRate";
        }
        else
        {
            objBL.chksBuyrates = "null";
            chksBuyrate = "null";
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
        if (ddlBrand.SelectedIndex > 0)
        {
            objBL.sdBrands = ddlBrand.SelectedItem.Text;
            sdBrand = ddlBrand.SelectedItem.Text;
        }
        else
        {
            objBL.sdBrands = "null";
            sdBrand = "null";
        }

        if (ddlCategory.SelectedIndex > 0)
        {
            objBL.sdCats = ddlCategory.SelectedItem.Text;
            sdCat = ddlCategory.SelectedItem.Text;
        }
        else
        {
            objBL.sdCats = "null";
            sdCat = "null";
        }
        if (ddlPrdctNme.SelectedIndex > 0)
        {
            objBL.sdProds = ddlPrdctNme.SelectedItem.Text;
            sdProd = ddlPrdctNme.SelectedItem.Text;
        }
        else
        {
            objBL.sdProds = "null";
            sdProd = "null";
        }
        if (ddlMdl.SelectedIndex > 0)
        {
            objBL.sdMods = ddlMdl.SelectedItem.Text;
            sdMod = ddlMdl.SelectedItem.Text;
        }
        else
        {
            objBL.sdMods = "null";
            sdMod = "null";
        }
        if (ddlItemCode.SelectedIndex > 0)
        {
            objBL.sditems = ddlItemCode.SelectedItem.Text;
            sditem = ddlItemCode.SelectedItem.Text;
        }
        else
        {
            objBL.sditems = "null";
            sditem = "null";
        }
        if (ddlStock.SelectedIndex > 0)
        {
            objBL.sdstocs = ddlStock.SelectedItem.Text;
            sdstoc = ddlStock.SelectedItem.Text;
        }
        else
        {
            objBL.sdstocs = "null";
            sdstoc = "null";
        }
        if (ddlVat.SelectedIndex > 0)
        {
            objBL.sdVats = ddlVat.SelectedItem.Text;
            sdVat = ddlVat.SelectedItem.Text;
        }
        else
        {
            objBL.sdVats = "null";
            sdVat = "null";
        }
        if (ddlNlc.SelectedIndex > 0)
        {
            objBL.sdNlcs = ddlNlc.SelectedItem.Text;
            sdNlc = ddlNlc.SelectedItem.Text;
        }
        else
        {
            objBL.sdNlcs = "null";
            sdNlc = "null";
        }

        if (ddlBuyrate.SelectedIndex > 0)
        {
            objBL.sdBuyrates = ddlBuyrate.SelectedItem.Text;
            sdBuyrate = ddlBuyrate.SelectedItem.Text;
        }
        else
        {
            objBL.sdBuyrates = "null";
            sdBuyrate = "null";
        }
        if (ddlRate.SelectedIndex > 0)
        {
            objBL.sdrates = ddlRate.SelectedItem.Text;
            sdrate = ddlRate.SelectedItem.Text;
        }
        else
        {
            objBL.sdrates = "null";
            sdrate = "null";
        }

        if (ddlfirstlvl.SelectedIndex > 0)
        {
            objBL.sdfirstlvls = ddlfirstlvl.SelectedItem.Text;
            sdfirstlvl = ddlfirstlvl.SelectedItem.Text;
        }
        else
        {
            objBL.sdfirstlvls = "null";
            sdfirstlvl = "null";
        }
        if (ddlsecondlvl.SelectedIndex > 0)
        {
            objBL.sdsecondlvls = ddlsecondlvl.SelectedItem.Text;
            sdsecondlvl = ddlsecondlvl.SelectedItem.Text;
        }
        else
        {
            objBL.sdsecondlvls = "null";
            sdsecondlvl = "null";
        }
        if (ddlthirdlvl.SelectedIndex > 0)
        {
            objBL.sdthirdlvls = ddlthirdlvl.SelectedItem.Text;
            sdthirdlvl = ddlthirdlvl.SelectedItem.Text;
        }
        else
        {
            objBL.sdthirdlvls = "null";
            sdthirdlvl = "null";
        }
        if (ddlfourlvl.SelectedIndex > 0)
        {
            objBL.sdfourlvls = ddlfourlvl.SelectedItem.Text;
            sdfourlvl = ddlfourlvl.SelectedItem.Text;
        }
        else
        {
            objBL.sdfourlvls = "null";
            sdfourlvl = "null";
        }
        if (ddlfifthlvl.SelectedIndex > 0)
        {
            objBL.sdfifthlvls = ddlfifthlvl.SelectedItem.Text;
            sdfifthlvl = ddlfifthlvl.SelectedItem.Text;
        }
        else
        {
            objBL.sdfifthlvls = "null";
            sdfifthlvl = "null";
        }
        if (odlfirstlvl.SelectedIndex > 0)
        {
            objBL.sodfirstlvls = odlfirstlvl.SelectedItem.Text;
            sodfirstlvl = odlfirstlvl.SelectedItem.Text;
        }
        else
        {
            objBL.sodfirstlvls = "null";
            sodfirstlvl = "null";
        }
        if (odlsecondlvl.SelectedIndex > 0)
        {
            objBL.sodsecondlvls = odlsecondlvl.SelectedItem.Text;
            sodsecondlvl = odlsecondlvl.SelectedItem.Text;
        }
        else
        {
            objBL.sodsecondlvls = "null";
            sodsecondlvl = "null";
        }
        if (odlthirdlvl.SelectedIndex > 0)
        {
            objBL.sodthirdlvls = odlthirdlvl.SelectedItem.Text;
            sodthirdlvl = odlthirdlvl.SelectedItem.Text;
        }
        else
        {
            objBL.sodthirdlvls = "null";
            sodthirdlvl = "null";
        }
        if (odlfourlvl.SelectedIndex > 0)
        {
            objBL.sodfourlvls = odlfourlvl.SelectedItem.Text;
            sodfourlvl = odlfourlvl.SelectedItem.Text;
        }
        else
        {
            objBL.sodfourlvls = "null";
            sodfourlvl = "null";
        }
        if (odlfifthlvl.SelectedIndex > 0)
        {
            objBL.sodfifthlvls = odlfifthlvl.SelectedItem.Text;
            sodfifthlvl = odlfifthlvl.SelectedItem.Text;
        }
        else
        {
            objBL.sodfifthlvls = "null";
            sodfifthlvl = "null";
        }

        DataSet ds1 = new DataSet();
        //objBL.insertsaveddata(savedname, chkCat, chkBran, chkProd, chkMod, chkitem, chkstoc, chksNlc, chksVat, chksBuyrate, chksrate, chksAll, sdBrand, sdCat, sdProd, sdMod, sditem, sdstoc, sdNlc, sdVat, sdBuyrate, sdrate, sdfirstlvl, sdsecondlvl, sdthirdlvl, sdfourlvl, sdfifthlvl, sodfirstlvl, sodsecondlvl, sodthirdlvl, sodfourlvl, sodfifthlvl, txtstock, txtvat, txtbuyrate, txtrate, txtnlc, dat, sdFirstSub, sdSecondSub, sdThirdSub, sdFourSub, sdFiveSub);

        //objBL.insertsaveddata(savedname, chkCat, chkBran, chkProd, chkMod, chkitem, chkstoc, chksNlc, chksVat, chksBuyrate, chksrate, chksAll, sdBrand, sdCat, sdProd, sdMod, sditem, sdstoc, sdNlc, sdVat, sdBuyrate, sdrate, sdfirstlvl, sdsecondlvl, sdthirdlvl, sdfourlvl, sdfifthlvl, sodfirstlvl, sodsecondlvl, sodthirdlvl, sodfourlvl, sodfifthlvl, txtstock, txtvat, txtbuyrate, txtrate, txtnlc, dat);
        //DataGrid dsg = new Datagrid();ss
        lblMsg.Text = "saved";
    }
    catch (Exception ex)
    {
        TroyLiteExceptionManager.HandleException(ex);
    }
}
}
 



