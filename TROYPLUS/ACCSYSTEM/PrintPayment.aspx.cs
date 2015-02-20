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
using System.IO;

public partial class PrintPayment : System.Web.UI.Page
{

    public string sDataSource = string.Empty;
    private string currency = string.Empty;
    private string currencyType = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                GetCurrencyType();

                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

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
                                lblPincode.Text = Convert.ToString(dr["Pincode"]);
                                lblState.Text = Convert.ToString(dr["state"]);
                                lblBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            }
                        }
                    }
                }
                loadBillFormat();

                string BillFormat = cmdBill.SelectedItem.Text;

                if (BillFormat == "GENERAL")
                {
                    GENREALFORMAT.Visible = true;
                    PREPRINTEDFORMAT2.Visible = false;
                    printPreview();
                }
                else if (BillFormat == "PREPRINTED FORMAT 2")
                {
                    PREPRINTEDFORMAT2.Visible = true;
                    GENREALFORMAT.Visible = false;
                    printPreview_PrePrint();
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdBill_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string BillFormat = cmdBill.SelectedItem.Text;

            if (BillFormat == "GENERAL")
            {
                GENREALFORMAT.Visible = true;
                PREPRINTEDFORMAT2.Visible = false;
                printPreview();
            }
            else if (BillFormat == "PREPRINTED FORMAT 2")
            {
                GENREALFORMAT.Visible = false;
                PREPRINTEDFORMAT2.Visible = true;
                printPreview_PrePrint();
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void loadBillFormat()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListBillFormat();
        cmdBill.DataSource = ds;
        cmdBill.DataBind();
        cmdBill.DataTextField = "Key";
        cmdBill.DataValueField = "Key";
    }

    private void GetCurrencyType()
    {
        DataSet appSettings;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "CURRENCY")
                {
                    currency = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }

        if (currency == "INR")
        {
            currencyType = "Rs";
        }
        if (currency == "GBP")
        {
            currencyType = "£";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("CustomerPayment.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void printPreview()
    {
        string sFilename = string.Empty;
        DataSet ds = new DataSet();
        int ID = 0;

        if (Request.QueryString["ID"] != null)
        {
            ID = int.Parse(Request.QueryString["ID"].ToString());
        }

        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetPaymentForId(Request.Cookies["Company"].Value, ID);

        if (ds.Tables[0].Rows.Count == 1)
        {

            lblRefNo.Text = ": " + ds.Tables[0].Rows[0]["RefNo"].ToString();
            lblPayedTo.Text = ": " + ds.Tables[0].Rows[0]["Debi"].ToString();
            lblAmount.Text = ": " + currencyType + " " + ds.Tables[0].Rows[0]["Amount"].ToString();
            lblPayMode.Text = ": " + ds.Tables[0].Rows[0]["Paymode"].ToString();

            if (ds.Tables[0].Rows[0]["Paymode"].ToString().Trim() == "Cheque")
            {
                rowBank.Visible = true;
                rowCheque.Visible = true;
                lblBank.Text = ": " + ds.Tables[0].Rows[0]["LedgerName"].ToString();
                lblCheque.Text = ": " + ds.Tables[0].Rows[0]["ChequeNo"].ToString();
            }
            else
            {
                rowBank.Visible = false;
                rowCheque.Visible = false;
            }
            lblNarration.Text = ": " + ds.Tables[0].Rows[0]["Narration"].ToString();
            lblPayDate.Text = ": " + DateTime.Parse(ds.Tables[0].Rows[0]["TransDate"].ToString()).ToString("dd/MM/yyyy");

            decimal dec = Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"].ToString());
            lblinwords.Text = ": " + RupeeInWords(dec);         
        }

    }


    protected void printPreview_PrePrint()
    {
        string sFilename = string.Empty;
        DataSet ds = new DataSet();
        int ID = 0;

        if (Request.QueryString["ID"] != null)
        {
            ID = int.Parse(Request.QueryString["ID"].ToString());
        }

        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetPaymentForId(Request.Cookies["Company"].Value, ID);

        if (ds.Tables[0].Rows.Count == 1)
        {
            lblPayedToPP.Text = ds.Tables[0].Rows[0]["Debi"].ToString();
            lblAmountPP.Text = currencyType + " " + ds.Tables[0].Rows[0]["Amount"].ToString();
            lblPayDatePP.Text = DateTime.Parse(ds.Tables[0].Rows[0]["TransDate"].ToString()).ToString("dd/MM/yyyy");

            decimal dec = Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"].ToString());
            lblAmttxtPP.Text = RupeeInWords(dec);
        }

    }



    public string RupeeInWords(decimal value)
    {
        return Towords(value);
    }

    public string Towords(decimal Num)
    {
        string stmp = ""; int i, leng, prevdigit = 0, digit = 0, pos = 0;
        string snum = "", laststr = "";
        string pointVlu = "";
        string[] sVlu = Convert.ToString(Num).Split('.');

        stmp = getInWord(Convert.ToInt32(sVlu[0]));
        if (sVlu.Length > 1) pointVlu = getInWord(Convert.ToInt32(sVlu[1]));

        if (!pointVlu.Equals(""))
        {
            if (stmp.Equals(""))
                stmp = "Zero ";
            stmp += " Rupee and " + pointVlu + " Paisa";
        }
        stmp = stmp + " only";
        return stmp.Trim();
    }

    string getInWord(int Num)
    {
        string stmp = ""; int i, leng, prevdigit = 0, digit = 0, pos = 0;
        string snum = "", laststr = "";
        snum = Convert.ToString(Num);

        leng = snum.Length;

        #region Loop Process
        for (i = leng; i > 0; i--)
        {
            if (snum.Substring(i - 1, 1) == "-")
            {
                continue;
            }
            digit = Convert.ToInt32(snum.Substring(i - 1, 1));
            pos = leng - i + 1;

            //'1 units
            //'3 hundreds
            //'4 thousands
            //'6 lakhs
            //'8 crores
            //'10 hundred crores
            //'11 thousand crores
            //'13 lakh crores
            //'15 crore crores
            //'sub groups
            //'1,8,15
            //'3,10
            //'4,11
            //'6,13
            switch (pos)
            {
                case 1:
                case 3:
                case 4:
                case 6:
                case 8:
                case 10:
                case 11:
                case 13:
                case 15:
                    {
                        if (digit != 0)
                        {
                            switch (pos)
                            {
                                case 8:
                                case 15:
                                    laststr = "Crore ";
                                    break;
                                case 3:
                                case 10:
                                    laststr = "Hundred ";
                                    break;
                                case 4:
                                case 11:
                                    laststr = "Thousand ";
                                    break;
                                case 6:
                                case 13:
                                    laststr = "Lakh ";
                                    break;

                            }

                            //if (stmp != "")
                            //    stmp = laststr + " and " + stmp;
                            //else
                                stmp = laststr + stmp;
                        }

                        switch (digit)
                        {
                            case 0:
                                if (pos == 1)
                                    laststr = "Zero ";
                                else
                                    laststr = "";
                                break;
                            case 1:
                                laststr = "One ";
                                break;

                            case 2:
                                laststr = "Two ";
                                break;

                            case 3:
                                laststr = "Three ";
                                break;

                            case 4:
                                laststr = "Four ";
                                break;

                            case 5:
                                laststr = "Five ";
                                break;

                            case 6:
                                laststr = "Six ";
                                break;

                            case 7:
                                laststr = "Seven ";
                                break;

                            case 8:
                                laststr = "Eight ";
                                break;

                            case 9:
                                laststr = "Nine ";
                                break;

                        }
                        stmp = laststr + stmp;
                    } break;
                case 2:
                case 5:
                case 7:
                case 9:
                case 12:
                case 14:
                    //'2 tens
                    //'5 ten thousands
                    //'7 ten lakhs
                    //'9 ten crore
                    //'12 ten thousand crore
                    //'14 ten lakh crores
                    {
                        if (prevdigit == 0)
                        {
                            switch (pos)
                            {
                                case 9:
                                    laststr = "Crore ";
                                    break;
                                case 7:
                                case 14:
                                    if (digit != 0) laststr = "Lakh "; break;
                                case 5:
                                case 12:
                                    if (digit != 0) laststr = "Thousand "; break;
                            }
                            stmp = laststr + stmp;
                        }

                        switch (digit)
                        {
                            case 0:
                                if (prevdigit != 0)
                                    laststr = "";
                                else
                                    laststr = "";
                                break;
                            case 1:
                                switch (prevdigit)
                                {
                                    case 0:
                                        laststr = "Ten ";
                                        break;
                                    case 1:
                                        stmp = "Eleven " + stmp.Substring(4);
                                        break;
                                    case 2:
                                        stmp = "Twelve " + stmp.Substring(4);
                                        //stmp = stmp.Replace("Two ", "Twelve ");
                                        break;
                                    case 3:
                                        stmp = "Thirteen " + stmp.Substring(6);
                                        //stmp = stmp.Replace("Three ", "Thirteen ");
                                        break;
                                    case 4:
                                        stmp = "Fourteen " + stmp.Substring(5);
                                        /// stmp = stmp.Replace("Four ", "Fourteen ");
                                        break;
                                    case 5:
                                        stmp = "Fifteen " + stmp.Substring(5);
                                        //stmp = stmp.Replace("Five ", "Fifteen ");
                                        break;
                                    case 6:
                                        stmp = "Sixteen " + stmp.Substring(4);
                                        //stmp = stmp.Replace("Six ", "Sixteen ");
                                        break;
                                    case 7:
                                        stmp = "Seventeen " + stmp.Substring(6);
                                        //stmp = stmp.Replace("Seven ", "Seventeen ");
                                        break;
                                    case 8:
                                        stmp = "Eighteen " + stmp.Substring(6);
                                        //stmp = stmp.Replace("Eight ", "Eighteen ");
                                        break;
                                    case 9:
                                        stmp = "Nineteen " + stmp.Substring(5);
                                        //stmp = stmp.Replace("Nine ", "Nineteen ");
                                        break;

                                } break;
                            case 2:
                                laststr = "Twenty "; break;
                            case 3:
                                laststr = "Thirty "; break;
                            case 4:
                                laststr = "Forty "; break;
                            case 5:
                                laststr = "Fifty "; break;
                            case 6:
                                laststr = "Sixty "; break;
                            case 7:
                                laststr = "Seventy "; break;
                            case 8:
                                laststr = "Eighty "; break;
                            case 9:
                                laststr = "Ninty "; break;
                        }
                        //'if stmp contains "Zero " at the end, remove it
                        //if InStr(stmp, "Zero ") <> 0 Then stmp = Replace(stmp, "Zero ", "")
                        stmp = stmp.Replace("Zero ", "");
                        //if ((stmp != "") || (laststr != "")) 
                        stmp = laststr + stmp;
                    } break;
            }
            prevdigit = digit;
            laststr = "";
        }
        #endregion
        
        return stmp.Trim();
    }

}
