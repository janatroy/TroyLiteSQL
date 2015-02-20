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

public partial class Payments : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            {
                DateEndformat();
                DateStartformat();
                ddlCat();
                ddlPaymode();
                ddlfirst();
                ddlsecond();
                ddlthird();
                ddlsubcategory.Enabled = false;
                ddlsecondlvl.Enabled = false;
                ddlthirdlvl.Enabled = false;


                if (Request.QueryString["ID"] != null)
                {
                    if (Request.QueryString["ID"] == "SupPay")
                    {
                        ddlCategory.SelectedValue = "Sundry Creditors";
                        fillsubcat();
                        ddlsubcategory.Enabled = true;
                    }
                    else if (Request.QueryString["ID"] == "ExpPay")
                    {
                        ddlCategory.SelectedValue = "General Expenses";
                        fillsubcat();
                        ddlsubcategory.Enabled = true;
                    }
                    else if (Request.QueryString["ID"] == "BankPay")
                    {
                        ddlCategory.SelectedValue = "Bank Accounts";
                        fillsubcat();
                        ddlsubcategory.Enabled = true;
                    }
                    else if (Request.QueryString["ID"] == "CustPay")
                    {
                        ddlCategory.SelectedValue = "Sundry Debtors";
                        fillsubcat();
                        ddlsubcategory.Enabled = true;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void ddlCat()
    {
        ddlCategory.Items.Add("All");
        ddlCategory.Items.Add("Sundry Debtors");
        ddlCategory.Items.Add("Sundry Creditors");
        ddlCategory.Items.Add("Bank Accounts");
        ddlCategory.Items.Add("General Expenses");
    }
    protected void ddlPaymode()
    {
        ddlPayment.Items.Add("All");
        ddlPayment.Items.Add("Cash");
        ddlPayment.Items.Add("Cheque / Card");
    }
    protected void ddlfirst()
    {
        ddlfirstlvl.Items.Insert(0, "None");
        ddlfirstlvl.Items.Insert(1, "TransDate");
        ddlfirstlvl.Items.Insert(2, "PaidTo");
        ddlfirstlvl.Items.Insert(3, "PaymentMode");
    }
    protected void ddlsecond()
    {
        ddlsecondlvl.Items.Insert(0, "None");
        ddlsecondlvl.Items.Insert(1, "TransDate");
        ddlsecondlvl.Items.Insert(2, "PaidTo");
        ddlsecondlvl.Items.Insert(3, "PaymentMode");
    }
    protected void ddlthird()
    {
        ddlthirdlvl.Items.Insert(0, "None");
        ddlthirdlvl.Items.Insert(1, "TransDate");
        ddlthirdlvl.Items.Insert(2, "PaidTo");
        ddlthirdlvl.Items.Insert(3, "PaymentMode");
    }
    protected void DateStartformat()
    {
        DateTime dtCurrent = DateTime.Now;
        DateTime dtNew = new DateTime(dtCurrent.Year, dtCurrent.Month, 1);
        txtStrtDt.Text = string.Format("{0:dd/MM/yyyy}", dtNew);
    }

    protected void Gridpayments_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(Gridpayments, e.Row, this);
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
            //objBL.Cond = ddlPayMode.SelectedValue.ToString();
            string condtion = "";
            condtion = getCond();
            getorderByAndselColumn();
            DataSet ds = new DataSet();
            ds = objBL.getPayments(selColumn, condtion, orderBy);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gridpayments.DataSource = ds;
                Gridpayments.DataBind();
                var lblTotalAmt = Gridpayments.FooterRow.FindControl("lblTotalAmt") as Label;
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

    protected void Gridpayments_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Gridpayments.PageIndex = e.NewPageIndex;

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
            Gridpayments.PageIndex = ((DropDownList)sender).SelectedIndex;

            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void DateEndformat()
    {
        System.DateTime dt = System.DateTime.Now.Date;
        txtEndDt.Text = string.Format("{0:dd/MM/yyyy}", dt);
    }

    protected void btnGrd_Click(object sender, EventArgs e)
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

            cond = " and tblDayBook.TransDate >= #" + dt + "# and  tblDayBook.TransDate <= #" + dtt + "# ";
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
        if (ddlsubcategory.SelectedIndex > 0)
        {
            objBL.Brands = ddlsubcategory.SelectedItem.Text;
            cond += " and tblLedger.LedgerName='" + ddlsubcategory.SelectedItem.Text + "' ";
        }
        if (ddlPayment.SelectedIndex > 0)
        {
            objBL.Models = ddlPayment.SelectedItem.Text;
            if (ddlPayment.SelectedItem.Text == "Cash")
                //cond += " and IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo)='" + ddlPayment.SelectedItem.Text + "' ";
                cond += " and ((tblDayBook.ChequeNo = null or tblDayBook.ChequeNo = '') and (tblDayBook.CreditCardNo=null or tblDayBook.CreditCardNo='')) ";
            else if (ddlPayment.SelectedItem.Text == "Cheque / Card")
                cond += " and ((tblDayBook.ChequeNo <> null and tblDayBook.ChequeNo <> '') or (tblDayBook.CreditCardNo <> null and tblDayBook.CreditCardNo <> '')) ";
        }
        return cond;
    }
    protected void getorderByAndselColumn()
    {
        orderBy = "";
        selColumn = "";
        if (ddlfirstlvl.SelectedIndex > 0)
        {
            orderBy = " order by " + ddlfirstlvl.SelectedItem.Text + " ";
            selColumn = " " + ddlfirstlvl.SelectedItem.Text + " ";
        }
        if (ddlsecondlvl.SelectedIndex > 0)
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
            orderBy += " " + ddlsecondlvl.SelectedItem.Text + " ";
            selColumn += " " + ddlsecondlvl.SelectedItem.Text + " ";
        }
        if (ddlthirdlvl.SelectedIndex > 0)
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
            orderBy += " " + ddlthirdlvl.SelectedItem.Text + " ";
            selColumn += " " + ddlthirdlvl.SelectedItem.Text + " ";
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
            //orderBy = " order by TransDate,PaidTo,PaymentMode,RefNo,IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo),Narration";
            orderBy += " ,RefNo,IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo),Narration";
            selColumn += " ,RefNo,IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo) as CNo,Narration";
        }
        selColumn = selColumn.Replace("TransDate", "format(tblDayBook.TransDate,'dd/mm/yyyy') as TransDate");
        selColumn = selColumn.Replace("PaidTo", "tblLedger.LedgerName As PaidTo");
        //selColumn = selColumn.Replace("PaymentMode", "IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo) AS PaymentMode");
        //selColumn = selColumn.Replace("PaymentMode", "IIf(IsNull(IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo)),'Cash', 'Cheque / Card') AS PaymentMode");
        selColumn = selColumn.Replace("PaymentMode", " tblDayBook.ChequeNo AS PaymentMode");
        orderBy = orderBy.Replace("PaidTo", "tblLedger.LedgerName");
        //orderBy = orderBy.Replace("PaymentMode", "IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo)");
        //orderBy = orderBy.Replace("PaymentMode", "IIf(IsNull(IIf(IsNull(tblDayBook.ChequeNo),tblDayBook.CreditCardNo,tblDayBook.ChequeNo)),'Cash', 'Cheque / Card')");
        orderBy = orderBy.Replace("PaymentMode", "tblDayBook.ChequeNo");
    }

    private bool isValidLevels()
    {
        if ((ddlfirstlvl.SelectedItem.Text != "None") &&
            (ddlfirstlvl.SelectedItem.Text == ddlsecondlvl.SelectedItem.Text ||
            ddlfirstlvl.SelectedItem.Text == ddlthirdlvl.SelectedItem.Text))
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('Two levels can NOT be same. Please select different levels');", true);
            return false;
        }
        if ((ddlsecondlvl.SelectedItem.Text != "None") &&
            (ddlsecondlvl.SelectedItem.Text == ddlthirdlvl.SelectedItem.Text))
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
            if (txtStrtDt.Text != "" && txtEndDt.Text != "")
            {
                //objBL.StartDate = String.Format("{0:MM/dd/yyyy}", txtStrtDt.Text);
                //objBL.EndDate = String.Format("{0:MM/dd/yyyy}", txtEndDt.Text);
                //objBL.Categorys = Convert.ToInt32(ddlCategory.SelectedItem.Text);
                //objBL.Brands = ddlsubcategory.SelectedItem.Text;
                //objBL.Models = ddlPayment.SelectedItem.Text;
                // bindData();
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
        ds = objBL.getPayments(selColumn, condtion, orderBy);
        if (ds.Tables[0].Rows.Count > 0)
        {
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
                    if (ddlfirstlvl.SelectedIndex > 0)
                        fLvlValueTemp = dr[ddlfirstlvl.SelectedItem.Text].ToString().ToUpper().Trim();
                    if (ddlsecondlvl.SelectedIndex > 0)
                        sLvlValueTemp = dr[ddlsecondlvl.SelectedItem.Text].ToString().ToUpper().Trim();
                    if (ddlthirdlvl.SelectedIndex > 0)
                        tLvlValueTemp = dr[ddlthirdlvl.SelectedItem.Text].ToString().ToUpper().Trim();
                    dispLastTotal = true;
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
            string filename = "PaymentsDownloadExcel.xls";
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
    protected void Gridpayments_RowDataBound(object sender, GridViewRowEventArgs e)
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

                //var CCN = e.Row.FindControl("lblBank") as Label;

                //if (CCN != null && CCN.ToString() != "")
                //    e.Row.FindControl("lblPayMode") = "Cheque / Card";
                //else
                //    string CCN = "Cash";

                //e.Row.FindControl("lblBank")
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
        ds = objBL.getSubCategorys();
        ddlsubcategory.DataSource = ds;
        ddlsubcategory.DataTextField = "LedgerName";
        ddlsubcategory.DataValueField = "LedgerID";
        ddlsubcategory.DataBind();
        ddlsubcategory.Items.Insert(0, "All");
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
            ddlsubcategory.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddlfirstlvl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlsecondlvl.Enabled = true;
            ddlthirdlvl.Enabled = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ddlsecondlvl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlthirdlvl.Enabled = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}



