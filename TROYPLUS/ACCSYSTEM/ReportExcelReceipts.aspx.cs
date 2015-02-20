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

public partial class Receipts : System.Web.UI.Page
{
    string date = "";
    int payto = 0;
    decimal paymode = 0;
    decimal Gtotal = 0;
    decimal Gttl = 0;
    decimal Pttls = 0;
    decimal brandTotal = 0, catIDTotal = 0, modelTotal = 0, fLvlTotal = 0;
    string orderBy = "", selColumn = "", selLevels = "";
    //DBClass objdb = new DBClass();
    BusinessLogic objBL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

            txtStrtDt.Focus();
            if (!Page.IsPostBack)
            {
                DateEndformat();
                DateStartformat();
                ddlCategry();
                ddlPay();
                ddlfirst();
                ddlsecond();
                ddlthird();
                ddlSubCategory.Enabled = false;
                ddltwo.Enabled = false;
                ddlthree.Enabled = false;



                if (Request.QueryString["ID"] != null)
                {
                    if (Request.QueryString["ID"] == "SupRec")
                    {
                        ddlCategory.SelectedValue = "Sundry Creditors";
                        fillsubcat();
                        ddlSubCategory.Enabled = true;
                    }
                    else if (Request.QueryString["ID"] == "ExpRec")
                    {
                        ddlCategory.SelectedValue = "General Expenses";
                        fillsubcat();
                        ddlSubCategory.Enabled = true;
                    }
                    else if (Request.QueryString["ID"] == "BankRec")
                    {
                        ddlCategory.SelectedValue = "Bank Accounts";
                        fillsubcat();
                        ddlSubCategory.Enabled = true;
                    }
                    else if (Request.QueryString["ID"] == "CustRec")
                    {
                        ddlCategory.SelectedValue = "Sundry Debtors";
                        fillsubcat();
                        ddlSubCategory.Enabled = true;
                    }
                }


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void ddlCategry()
    {
        ddlCategory.Items.Add("All");
        ddlCategory.Items.Add("Sundry Debtors");
        ddlCategory.Items.Add("Sundry Creditors");
        ddlCategory.Items.Add("Bank Accounts");
        ddlCategory.Items.Add("General Expenses");
    }
    protected void ddlPay()
    {
        ddlPaymode.Items.Add("All");
        ddlPaymode.Items.Add("Cash");
        ddlPaymode.Items.Add("Cheque / Card");
    }
    protected void ddlfirst()
    {
        ddlone.Items.Insert(0, "None");
        ddlone.Items.Insert(1, "TransDate");
        ddlone.Items.Insert(2, "PaidTo");
        ddlone.Items.Insert(3, "PaymentMode");
    }
    protected void ddlsecond()
    {
        ddltwo.Items.Insert(0, "None");
        ddltwo.Items.Insert(1, "TransDate");
        ddltwo.Items.Insert(2, "PaidTo");
        ddltwo.Items.Insert(3, "PaymentMode");
    }
    protected void ddlthird()
    {
        ddlthree.Items.Insert(0, "None");
        ddlthree.Items.Insert(1, "TransDate");
        ddlthree.Items.Insert(2, "PaidTo");
        ddlthree.Items.Insert(3, "PaymentMode");
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

    protected void GridRcpts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridRcpts.PageIndex = e.NewPageIndex;

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
            GridRcpts.PageIndex = ((DropDownList)sender).SelectedIndex;

            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridRcpts_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        { 
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GridRcpts, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BindGrid()
    {
        if (txtStrtDt.Text != "" && txtEndDt.Text != "")
            {
                if (!isValidLevels())
                {
                    return;
                }
                Total = 0;
                //objBL.StartDate = String.Format("{0:MM/dd/yyyy}", txtStrtDt.Text);
                //objBL.EndDate = String.Format("{0:MM/dd/yyyy}", txtEndDt.Text);
                string condtion = "";
                condtion = getCond();
                getorderByAndselColumn();
                DataSet ds = new DataSet();
                ds = objBL.getRceipts(selColumn, condtion, orderBy);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridRcpts.DataSource = ds;
                    GridRcpts.DataBind();
                    var lblTotalAmt = GridRcpts.FooterRow.FindControl("lblTotalAmt") as Label;
                    if (lblTotalAmt != null)
                    {
                        lblTotalAmt.Text = Total.ToString();
                    }
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

    protected void btnRcpts_Click(object sender, EventArgs e)
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

    protected string getCond()
    {
        string cond = "";
        //WHERE ((tblDayBook.[TransDate]) Between CDate('" + StartDate + "') And CDate('" + EndDate + "')) and (((tblDayBook.VoucherType)='Payment')) And ((tblLedger.GroupID)=" + Convert.ToInt32(Categorys) + ") And ((tblLedger.LedgerName)='" + Brands + "')");
        if (txtStrtDt.Text != "" && txtEndDt.Text != "")
        {
            //objBL.StartDate = txtStrtDt.Text;
            //objBL.EndDate = txtEndDt.Text;

            string aa = txtStrtDt.Text;
            string dt = Convert.ToDateTime(aa).ToString("MM/dd/yyyy");

            string aaa = txtEndDt.Text;
            string dtt = Convert.ToDateTime(aaa).ToString("MM/dd/yyyy");

            cond = " and tblDayBook.TransDate >= #" + dt + "# and tblDayBook.TransDate <= #" + dtt + "# ";
        }

        if (ddlCategory.SelectedIndex > 0)
        {
            if (ddlCategory.SelectedItem.Value == "Sundry Debtors")
            {
                objBL.Categorys = 1;
                cond += " and tblLedger.GroupID=" + 1 + " ";
            }
            else if (ddlCategory.SelectedItem.Value == "Sundry Creditors")
            {
                objBL.Categorys = 2;
                cond += " and tblLedger.GroupID=" + 2 + " ";
            }
            else if (ddlCategory.SelectedItem.Value == "Bank Accounts")
            {
                objBL.Categorys = 3;
                cond += " and tblLedger.GroupID=" + 3 + " ";
            }
            else if (ddlCategory.SelectedItem.Value == "General Expenses")
            {
                objBL.Categorys = 8;
                cond += " and tblLedger.GroupID=" + 8 + " ";
            }
        }
        if (ddlSubCategory.SelectedIndex > 0)
        {
            objBL.Brands = ddlSubCategory.SelectedItem.Text;
            cond += " and tblLedger.LedgerName='" + ddlSubCategory.SelectedItem.Text + "' ";
        }
        if (ddlPaymode.SelectedIndex > 0)
        {
            objBL.Models = ddlPaymode.SelectedItem.Text;
            if (ddlPaymode.SelectedItem.Text == "Cash")
                //cond += " and IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo)='" + ddlPayment.SelectedItem.Text + "' ";
                cond += " and ((tblDayBook.ChequeNo == null or tblDayBook.ChequeNo == '') and (tblDayBook.CreditCardNo==null or tblDayBook.CreditCardNo=='')) ";
            else if (ddlPaymode.SelectedItem.Text == "Cheque / Card")
                cond += " and ((tblDayBook.ChequeNo != null and tblDayBook.ChequeNo != '') or (tblDayBook.CreditCardNo!=null nd tblDayBook.CreditCardNo!='')) ";
        }
        return cond;
    }
    protected void getorderByAndselColumn()
    {
        orderBy = "";
        selColumn = "";
        if (ddlone.SelectedIndex > 0)
        {
            orderBy = " order by " + ddlone.SelectedItem.Text + " ";
            selColumn = " " + ddlone.SelectedItem.Text + " ";
        }
        if (ddltwo.SelectedIndex > 0)
        {
            if (orderBy == "")
            {
                orderBy = " order by ";
                selColumn = " ";
            }
            else
            {
                orderBy += " , ";
                selColumn += " , ";
            }
            orderBy += " " + ddltwo.SelectedItem.Text + " ";
            selColumn += " " + ddltwo.SelectedItem.Text + " ";
        }
        if (ddlthree.SelectedIndex > 0)
        {
            if (orderBy == "")
            {
                orderBy = " order by ";
                selColumn = " ";
            }
            else
            {
                orderBy += " , ";
                selColumn += " , ";
            }
            orderBy += " " + ddlthree.SelectedItem.Text + " ";
            selColumn += " " + ddlthree.SelectedItem.Text + " ";
        }
        if (orderBy == "" && selColumn == "")
        {
            orderBy = " order by TransDate,PaidTo,PaymentMode,RefNo,IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo),Narration";
            selColumn = " RefNo,TransDate,PaidTo,PaymentMode,IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo) as CNo,Narration";
        }
        else
        {
            selLevels = selColumn;
            if (orderBy.IndexOf("TransDate") < 0)
            {
                orderBy += " , TransDate ";
                selColumn += " , TransDate ";
            }
            if (orderBy.IndexOf("PaidTo") < 0)
            {
                orderBy += " , PaidTo ";
                selColumn += " , PaidTo ";
            }
            if (orderBy.IndexOf("PaymentMode") < 0)
            {
                orderBy += " , PaymentMode ";
                selColumn += " , PaymentMode ";
            }
            orderBy += " ,RefNo,IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo),Narration";
            selColumn += " ,RefNo,IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo) as CNo,Narration";
        }
        selColumn = selColumn.Replace("TransDate", "format(tblDayBook.TransDate,'dd/mm/yyyy') As TransDate");
        selColumn = selColumn.Replace("PaidTo", "tblLedger.LedgerName As PaidTo");
        selColumn = selColumn.Replace("PaymentMode", " tblDayBook.ChequeNo AS PaymentMode");
        orderBy = orderBy.Replace("PaidTo", "tblLedger.LedgerName");
        orderBy = orderBy.Replace("PaymentMode", "tblDayBook.ChequeNo");
    }
    private bool isValidLevels()
    {
        if ((ddlone.SelectedItem.Text != "None") &&
            (ddlone.SelectedItem.Text == ddltwo.SelectedItem.Text ||
            ddlone.SelectedItem.Text == ddlthree.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddltwo.SelectedItem.Text != "None") &&
            (ddltwo.SelectedItem.Text == ddlthree.SelectedItem.Text))
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
            //objBL.StartDate = String.Format("{0:MM/dd/yyyy}", txtStrtDt.Text);
            //objBL.EndDate = String.Format("{0:MM/dd/yyyy}", txtEndDt.Text);
            //bindData();
            if (txtStrtDt.Text != "" && txtEndDt.Text != "")
            {
                string condtion = "";
                condtion = getCond();
                getorderByAndselColumn();
                bindData(selColumn, condtion, orderBy);
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
    public void bindData(string selColumn, string condtion, string orderBy)
    {
        bool dispLastTotal = false;
        DataSet ds = new DataSet();
        ds = objBL.getRceipts(selColumn, condtion, orderBy);
        DataTable dt = new DataTable();
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ddlone.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlone.SelectedItem.Text));
            }
            if (ddltwo.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddltwo.SelectedItem.Text));
            }
            if (ddlthree.SelectedIndex > 0)
            {
                dt.Columns.Add(new DataColumn(ddlthree.SelectedItem.Text));
            }
            if (selLevels.IndexOf("TransDate") < 0)
                dt.Columns.Add(new DataColumn("TransDate"));
            if (selLevels.IndexOf("PaidTo") < 0)
                dt.Columns.Add(new DataColumn("PaidTo"));
            if (selLevels.IndexOf("PaymentMode") < 0)
                dt.Columns.Add(new DataColumn("PaymentMode"));

            dt.Columns.Add(new DataColumn("RefNo"));
            dt.Columns.Add(new DataColumn("Cheque No"));
            dt.Columns.Add(new DataColumn("Amount"));
            dt.Columns.Add(new DataColumn("Narration"));


            //initialize column values for entire row
            string fLvlValue = "", sLvlValue = "", tLvlValue = "";
            string fLvlValueTemp = "", sLvlValueTemp = "", tLvlValueTemp = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["CNo"] != null && dr["CNo"].ToString() != "")
                    dr["PaymentMode"] = "Cheque / Card";
                else
                    dr["PaymentMode"] = "Cash";

                //initialize column values for entire row
                if (ddlone.SelectedIndex > 0)
                    fLvlValueTemp = dr[ddlone.SelectedItem.Text].ToString().ToUpper().Trim();
                if (ddltwo.SelectedIndex > 0)
                    sLvlValueTemp = dr[ddltwo.SelectedItem.Text].ToString().ToUpper().Trim();
                if (ddlthree.SelectedIndex > 0)
                    tLvlValueTemp = dr[ddlthree.SelectedItem.Text].ToString().ToUpper().Trim();
                dispLastTotal = true;
                if (ddlthree.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValue != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValue != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final8 = dt.NewRow();
                        if (ddlone.SelectedIndex > 0)
                        {
                            dr_final8[ddlone.SelectedItem.Text] = "";
                        }
                        if (ddltwo.SelectedIndex > 0)
                        {
                            dr_final8[ddltwo.SelectedItem.Text] = "";
                        }
                        if (ddlthree.SelectedIndex > 0)
                        {
                            dr_final8[ddlthree.SelectedItem.Text] = "Total " + tLvlValue;
                        }
                        dr_final8["RefNo"] = "";
                        if (selLevels.IndexOf("TransDate") < 0)
                            dr_final8["TransDate"] = "";
                        if (selLevels.IndexOf("PaidTo") < 0)
                            dr_final8["PaidTo"] = "";
                        if (selLevels.IndexOf("PaymentMode") < 0)
                            dr_final8["PaymentMode"] = "";

                        dr_final8["Cheque No"] = "";
                        dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                        dr_final8["Narration"] = "";
                        dt.Rows.Add(dr_final8);
                        //dt.Rows.Add(dr_final8);
                        modelTotal = 0;
                    }
                }

                if (ddltwo.SelectedIndex > 0)
                {
                    if ((fLvlValue != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValue != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final8 = dt.NewRow();
                        if (ddlone.SelectedIndex > 0)
                        {
                            dr_final8[ddlone.SelectedItem.Text] = "";
                        }
                        if (ddltwo.SelectedIndex > 0)
                        {
                            dr_final8[ddltwo.SelectedItem.Text] = "Total " + sLvlValue;
                        }
                        if (ddlthree.SelectedIndex > 0)
                        {
                            dr_final8[ddlthree.SelectedItem.Text] = "";
                        }
                        dr_final8["RefNo"] = "";
                        if (selLevels.IndexOf("TransDate") < 0)
                            dr_final8["TransDate"] = "";
                        if (selLevels.IndexOf("PaidTo") < 0)
                            dr_final8["PaidTo"] = "";
                        if (selLevels.IndexOf("PaymentMode") < 0)
                            dr_final8["PaymentMode"] = "";

                        dr_final8["Cheque No"] = "";
                        dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                        dr_final8["Narration"] = "";
                        dt.Rows.Add(dr_final8);
                        catIDTotal = 0;
                    }
                }
                if (ddlone.SelectedIndex > 0)
                {
                    if (fLvlValue != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final8 = dt.NewRow();
                        if (ddlone.SelectedIndex > 0)
                        {
                            dr_final8[ddlone.SelectedItem.Text] = "Total " + fLvlValue;
                        }
                        if (ddltwo.SelectedIndex > 0)
                        {
                            dr_final8[ddltwo.SelectedItem.Text] = "";
                        }
                        if (ddlthree.SelectedIndex > 0)
                        {
                            dr_final8[ddlthree.SelectedItem.Text] = "";
                        }
                        dr_final8["RefNo"] = "";
                        if (selLevels.IndexOf("TransDate") < 0)
                            dr_final8["TransDate"] = "";
                        if (selLevels.IndexOf("PaidTo") < 0)
                            dr_final8["PaidTo"] = "";
                        if (selLevels.IndexOf("PaymentMode") < 0)
                            dr_final8["PaymentMode"] = "";

                        dr_final8["Cheque No"] = "";
                        dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(fLvlTotal));
                        dr_final8["Narration"] = "";
                        dt.Rows.Add(dr_final8);
                        fLvlTotal = 0;
                    }
                }
                if (ddlone.SelectedIndex > 0)
                {
                    if (fLvlValueTemp != "" && fLvlValue != fLvlValueTemp)
                    {
                        DataRow dr_final1 = dt.NewRow();
                        if (ddlone.SelectedIndex > 0)
                        {
                            dr_final1[ddlone.SelectedItem.Text] = fLvlValueTemp;
                        }
                        if (ddltwo.SelectedIndex > 0)
                        {
                            dr_final1[ddltwo.SelectedItem.Text] = "";
                        }
                        if (ddlthree.SelectedIndex > 0)
                        {
                            dr_final1[ddlthree.SelectedItem.Text] = "";
                        }

                        dr_final1["RefNo"] = "";
                        if (selLevels.IndexOf("TransDate") < 0)
                            dr_final1["TransDate"] = "";
                        if (selLevels.IndexOf("PaidTo") < 0)
                            dr_final1["PaidTo"] = "";
                        if (selLevels.IndexOf("PaymentMode") < 0)
                            dr_final1["PaymentMode"] = "";

                        dr_final1["Cheque No"] = "";
                        dr_final1["Amount"] = "";
                        dr_final1["Narration"] = "";
                        dt.Rows.Add(dr_final1);
                    }
                }
                if (ddltwo.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp))
                    {
                        DataRow dr_final2 = dt.NewRow();
                        if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp))
                        {
                            if (ddltwo.SelectedIndex > 0)
                            {
                                dr_final2[ddltwo.SelectedItem.Text] = sLvlValueTemp;
                            }
                        }
                        if (ddlone.SelectedIndex > 0)
                        {
                            dr_final2[ddlone.SelectedItem.Text] = "";
                        }
                        if (ddltwo.SelectedIndex > 0)
                        {
                            dr_final2[ddltwo.SelectedItem.Text] = sLvlValueTemp;
                        }
                        if (ddlthree.SelectedIndex > 0)
                        {
                            dr_final2[ddlthree.SelectedItem.Text] = "";
                        }

                        dr_final2["RefNo"] = "";
                        if (selLevels.IndexOf("TransDate") < 0)
                            dr_final2["TransDate"] = "";
                        if (selLevels.IndexOf("PaidTo") < 0)
                            dr_final2["PaidTo"] = "";
                        if (selLevels.IndexOf("PaymentMode") < 0)
                            dr_final2["PaymentMode"] = "";

                        dr_final2["Cheque No"] = "";
                        dr_final2["Amount"] = "";
                        dr_final2["Narration"] = "";
                        dt.Rows.Add(dr_final2);
                    }
                }
                if (ddlthree.SelectedIndex > 0)
                {
                    if ((fLvlValueTemp != "" && fLvlValue != fLvlValueTemp) ||
                        (sLvlValueTemp != "" && sLvlValue != sLvlValueTemp) ||
                        (tLvlValueTemp != "" && tLvlValue != tLvlValueTemp))
                    {
                        DataRow dr_final1 = dt.NewRow();
                        if (fLvlValueTemp != "" && fLvlValue != fLvlValueTemp)
                        {
                            if (ddlthree.SelectedIndex > 0)
                            {
                                dr_final1[ddlthree.SelectedItem.Text] = tLvlValueTemp;
                            }
                        }
                        //else
                        //{
                        if (ddlone.SelectedIndex > 0)
                        {
                            dr_final1[ddlone.SelectedItem.Text] = "";
                        }
                        if (ddltwo.SelectedIndex > 0)
                        {
                            dr_final1[ddltwo.SelectedItem.Text] = "";
                        }
                        if (ddlthree.SelectedIndex > 0)
                        {
                            dr_final1[ddlthree.SelectedItem.Text] = tLvlValueTemp;
                        }

                        dr_final1["RefNo"] = "";
                        if (selLevels.IndexOf("TransDate") < 0)
                            dr_final1["TransDate"] = "";
                        if (selLevels.IndexOf("PaidTo") < 0)
                            dr_final1["PaidTo"] = "";
                        if (selLevels.IndexOf("PaymentMode") < 0)
                            dr_final1["PaymentMode"] = "";

                        dr_final1["Cheque No"] = "";
                        dr_final1["Amount"] = "";
                        dr_final1["Narration"] = "";
                        dt.Rows.Add(dr_final1);
                    }
                }
                fLvlValue = fLvlValueTemp;
                sLvlValue = sLvlValueTemp;
                tLvlValue = tLvlValueTemp;
                DataRow dr_final5 = dt.NewRow();
                if (ddlone.SelectedIndex > 0)
                {

                    dr_final5[ddlone.SelectedItem.Text] = "";
                }

                if (ddltwo.SelectedIndex > 0)
                {
                    dr_final5[ddltwo.SelectedItem.Text] = "";
                }
                if (ddlthree.SelectedIndex > 0)
                {
                    dr_final5[ddlthree.SelectedItem.Text] = "";
                }

                if (selLevels.IndexOf("TransDate") < 0)
                    dr_final5["TransDate"] = dr["TransDate"];
                if (selLevels.IndexOf("PaidTo") < 0)
                    dr_final5["PaidTo"] = dr["PaidTo"];
                if (selLevels.IndexOf("PaymentMode") < 0)
                    dr_final5["PaymentMode"] = dr["PaymentMode"];

                dr_final5["RefNo"] = dr["RefNo"];
                dr_final5["Cheque No"] = dr["CNo"];
                dr_final5["Amount"] = dr["Amount"];
                dr_final5["Narration"] = dr["Narration"];

                dt.Rows.Add(dr_final5);
                Gtotal = Gtotal + Convert.ToDecimal(dr["Amount"]);
                Gttl = Gttl + Convert.ToInt32(dr["Amount"]);
                modelTotal = modelTotal + Convert.ToDecimal(dr["Amount"]);
                catIDTotal = catIDTotal + Convert.ToDecimal(dr["Amount"]);
                fLvlTotal = fLvlTotal + Convert.ToDecimal(dr["Amount"]);
                Pttls = Pttls + Convert.ToDecimal(dr["Amount"]);
                brandTotal = brandTotal + Convert.ToDecimal(dr["Amount"]);
            }

            //Display the last Total and Grand Total
            if (dispLastTotal)
            {
                if (ddlthree.SelectedIndex > 0)
                {
                    DataRow dr_final8 = dt.NewRow();
                    if (ddlone.SelectedIndex > 0)
                    {
                        dr_final8[ddlone.SelectedItem.Text] = "";
                    }
                    if (ddltwo.SelectedIndex > 0)
                    {
                        dr_final8[ddltwo.SelectedItem.Text] = "";
                    }
                    if (ddlthree.SelectedIndex > 0)
                    {
                        dr_final8[ddlthree.SelectedItem.Text] = "Total: " + tLvlValueTemp;
                    }

                    dr_final8["RefNo"] = "";
                    if (selLevels.IndexOf("TransDate") < 0)
                        dr_final8["TransDate"] = "";
                    if (selLevels.IndexOf("PaidTo") < 0)
                        dr_final8["PaidTo"] = "";
                    if (selLevels.IndexOf("PaymentMode") < 0)
                        dr_final8["PaymentMode"] = "";


                    dr_final8["Cheque No"] = "";
                    dr_final8["Amount"] = Convert.ToString(Convert.ToDecimal(modelTotal));
                    dr_final8["Narration"] = "";
                    dt.Rows.Add(dr_final8);
                    modelTotal = 0;
                }

                if (ddltwo.SelectedIndex > 0)
                {
                    DataRow dr_final9 = dt.NewRow();
                    if (ddlone.SelectedIndex > 0)
                    {
                        dr_final9[ddlone.SelectedItem.Text] = "";
                    }
                    if (ddltwo.SelectedIndex > 0)
                    {
                        dr_final9[ddltwo.SelectedItem.Text] = "Total: " + sLvlValueTemp;
                    }
                    if (ddlthree.SelectedIndex > 0)
                    {
                        dr_final9[ddlthree.SelectedItem.Text] = "";
                    }

                    dr_final9["RefNo"] = "";
                    if (selLevels.IndexOf("TransDate") < 0)
                        dr_final9["TransDate"] = "";
                    if (selLevels.IndexOf("PaidTo") < 0)
                        dr_final9["PaidTo"] = "";
                    if (selLevels.IndexOf("PaymentMode") < 0)
                        dr_final9["PaymentMode"] = "";

                    dr_final9["Cheque No"] = "";
                    dr_final9["Amount"] = Convert.ToString(Convert.ToDecimal(catIDTotal));
                    dr_final9["Narration"] = "";
                    dt.Rows.Add(dr_final9);
                    catIDTotal = 0;
                }

                if (ddlone.SelectedIndex > 0)
                {
                    DataRow dr_final10 = dt.NewRow();
                    if (ddlone.SelectedIndex > 0)
                    {
                        dr_final10[ddlone.SelectedItem.Text] = "Total: " + fLvlValueTemp;
                    }
                    if (ddltwo.SelectedIndex > 0)
                    {
                        dr_final10[ddltwo.SelectedItem.Text] = "";
                    }
                    if (ddlthree.SelectedIndex > 0)
                    {
                        dr_final10[ddlthree.SelectedItem.Text] = "";
                    }

                    dr_final10["RefNo"] = "";
                    if (selLevels.IndexOf("TransDate") < 0)
                        dr_final10["TransDate"] = "";
                    if (selLevels.IndexOf("PaidTo") < 0)
                        dr_final10["PaidTo"] = "";
                    if (selLevels.IndexOf("PaymentMode") < 0)
                        dr_final10["PaymentMode"] = "";

                    dr_final10["Cheque No"] = "";
                    dr_final10["Amount"] = Convert.ToString(Convert.ToDecimal(brandTotal));
                    dr_final10["Narration"] = "";
                    dt.Rows.Add(dr_final10);
                    brandTotal = 0;
                }

                DataRow dr_final6 = dt.NewRow();
                if (ddlone.SelectedIndex > 0)
                {
                    dr_final6[ddlone.SelectedItem.Text] = "Grand Total: ";
                }
                if (ddltwo.SelectedIndex > 0)
                {
                    dr_final6[ddltwo.SelectedItem.Text] = "";
                }
                if (ddlthree.SelectedIndex > 0)
                {
                    dr_final6[ddlthree.SelectedItem.Text] = "";
                }

                dr_final6["RefNo"] = "Grand Total :";
                if (selLevels.IndexOf("TransDate") < 0)
                    dr_final6["TransDate"] = "";
                if (selLevels.IndexOf("PaidTo") < 0)
                    dr_final6["PaidTo"] = "";
                if (selLevels.IndexOf("PaymentMode") < 0)
                    dr_final6["PaymentMode"] = "";


                dr_final6["Cheque No"] = "";
                dr_final6["Amount"] = Convert.ToString(Convert.ToDecimal(Gtotal));
                dr_final6["Narration"] = "";
                dt.Rows.Add(dr_final6);
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
            string filename = "ReceiptsDownloadExcel.xls";
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
    decimal Total;
    protected void GridRcpts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var NewAmt = e.Row.FindControl("lblAmt") as Label;
                if (NewAmt != null)
                {
                    Total += decimal.Parse(NewAmt.Text);
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void fillsubcat()
    {
        if (ddlCategory.SelectedItem.Value == "Sundry Debtors")
        {
            //objBL.Categorys = Convert.ToInt32(ddlCategory.SelectedValue.ToString());
            objBL.Categorys = 1;
        }
        else if (ddlCategory.SelectedItem.Value == "Sundry Creditors")
        {
            objBL.Categorys = 2;
        }
        else if (ddlCategory.SelectedItem.Value == "Bank Accounts")
        {
            objBL.Categorys = 3;
        }
        else if (ddlCategory.SelectedItem.Value == "General Expenses")
        {
            objBL.Categorys = 8;
        }

        DataSet ds = new DataSet();
        ds = objBL.getSubCategory();
        ddlSubCategory.DataSource = ds;
        ddlSubCategory.DataTextField = "LedgerName";
        ddlSubCategory.DataValueField = "LedgerID";
        ddlSubCategory.DataBind();
        ddlSubCategory.Items.Insert(0, "All");
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedItem.Text == "Sundry Debtors")
            {
                fillsubcat();
            }
            else if (ddlCategory.SelectedItem.Text == "Sundry Creditors")
            {
                fillsubcat();
            }
            else if (ddlCategory.SelectedItem.Text == "Bank Accounts")
            {
                fillsubcat();
            }
            else if (ddlCategory.SelectedItem.Text == "General Expenses")
            {
                fillsubcat();
            }
            ddlSubCategory.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void ddlCategory_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedItem.Text == "Sundry Debtors")
            {
                fillsubcat();
            }
            else if (ddlCategory.SelectedItem.Text == "Sundry Creditors")
            {
                fillsubcat();
            }
            else if (ddlCategory.SelectedItem.Text == "Bank Accounts")
            {
                fillsubcat();
            }
            else if (ddlCategory.SelectedItem.Text == "General Expenses")
            {
                fillsubcat();
            }
            ddlSubCategory.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddlone_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddltwo.Enabled = true;
            ddlthree.Enabled = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddltwo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlthree.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
