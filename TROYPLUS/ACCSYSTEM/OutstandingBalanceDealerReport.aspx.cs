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

public partial class OutstandingBalanceDealerReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    Double amt = 0.0d;
    double SumOuts10 = 0.0;
    double SumOuts30 = 0.0;
    double SumOuts60 = 0.0;
    double SumOuts90 = 0.0;
    double SumOuts91 = 0.0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Write("<script language='javascript'> { window.close(); }</script>");

            if (!IsPostBack)
            {
                loadSundrys();
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

                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void loadSundrys()
    {
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        ds = bl.ListLedger();
        drpLedgerName.DataSource = ds;
        drpLedgerName.DataBind();
        drpLedgerName.DataTextField = "Ledgername";
        drpLedgerName.DataValueField = "LedgerID";

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            divPrint.Visible = true;
            ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
            Reset();
            int iLedgerID = 0;
            iLedgerID = Convert.ToInt32(drpLedgerName.SelectedItem.Value);
            Double CreditTotal = 0.00d;
            Double DebitAmt = 0.00d;
            Double TotalCredit = 0.0d;
            Double OpeningBal = 0.0d;
            string OutsDate = string.Empty;
            CreditTotal = rpt.GetTotalCredit(sDataSource, iLedgerID);
            TotalCredit = CreditTotal;
            int cnt = 0;
            int rowcnt = 0;
            int Days = 0;
            lblTotalAmt.Text = "0.00";
            DataSet dsDebit = rpt.GetDebitCreditLedger(sDataSource, iLedgerID, "Debit");
            if (dsDebit != null)
            {
                if (dsDebit.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDebit.Tables[0].Rows)
                    {
                        if (rowcnt == 0)
                        {
                            if (dr["OB"] != null)
                            {
                                OpeningBal = Convert.ToDouble(dr["OB"]);
                            }
                            //if (OpeningBal > 0)
                            //    TotalCredit = TotalCredit - OpeningBal;
                            //else
                            //    TotalCredit = TotalCredit + Math.Abs(OpeningBal);
                        }
                        if (dr["Amount"] != null)
                        {
                            DebitAmt = Convert.ToDouble(dr["Amount"].ToString());
                            if (TotalCredit < 0)
                            {
                                //DebitAmt = DebitAmt + Math.Abs(TotalCredit);
                                //OutsDate = OutsDate + "-1" + "^" + "" + "^" + Convert.ToString(Convert.ToDouble(Math.Abs(TotalCredit))) + ",";
                                OutsDate = OutsDate + "-1" + "^" + "" + "^" + "" + "^" + Convert.ToString(Convert.ToDouble(Math.Abs(TotalCredit))) + ",";
                                amt = amt + Math.Abs(TotalCredit);
                                TotalCredit = 0;
                            }
                            if (TotalCredit > DebitAmt)
                            {
                                TotalCredit = TotalCredit - DebitAmt;
                                //if(TotalCredit <= 0)
                                //    OutsDate = OutsDate + Convert.ToDateTime(dr["TransDate"]).ToShortDateString()+ "^" + Math.Abs(TotalCredit)  + ",";
                            }
                            else
                            {

                                if (cnt == 0)
                                {
                                    Days = rpt.GetDays(sDataSource, dr["Transdate"].ToString(), Convert.ToInt32(dr["Transno"]));
                                    OutsDate = OutsDate + Days.ToString() + "^" + dr["Transno"].ToString() + "^" + Convert.ToDateTime(dr["TransDate"]).ToShortDateString() + "^" + Convert.ToString(Convert.ToDouble(dr["amount"]) - TotalCredit) + ",";
                                    amt = amt + Convert.ToDouble(dr["amount"]) - TotalCredit;
                                }
                                else
                                {
                                    Days = rpt.GetDays(sDataSource, dr["Transdate"].ToString(), Convert.ToInt32(dr["Transno"]));
                                    OutsDate = OutsDate + Days.ToString() + "^" + dr["Transno"].ToString() + "^" + Convert.ToDateTime(dr["TransDate"]).ToShortDateString() + "^" + dr["Amount"].ToString() + ",";
                                    amt = amt + Convert.ToDouble(dr["Amount"]);
                                }
                                TotalCredit = 0;
                                cnt = cnt + 1;
                            }
                        }
                        rowcnt = rowcnt + 1;
                    }
                    if (OutsDate != "")
                    {
                        OutsDate = OutsDate.Remove(OutsDate.LastIndexOf(',', OutsDate.Length - 1));
                        GenerateDs(OutsDate);
                        OutsDate = "";
                    }
                    else
                    {
                        GenerateDs();
                    }
                    lblTotalAmt.Text = amt.ToString("#0.00");
                }
                else
                {

                    GenerateDs();
                    if (CreditTotal > 0)
                        lblTotalAmt.Text = CreditTotal.ToString("#0.00") + "Cr ";
                    else
                        lblTotalAmt.Text = Math.Abs(CreditTotal).ToString("#0.00") + " Dr ";
                }
            }
            else
            {
                GenerateDs();

                if (CreditTotal > 0)
                    lblTotalAmt.Text = CreditTotal.ToString("#0.00") + "Cr ";
                else
                    lblTotalAmt.Text = Math.Abs(CreditTotal).ToString("#0.00") + " Dr ";
            }
            //lblDate.Text = OutsDate;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvOuts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {






        }
    }
    public void GenerateDs()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;
        int days = 0;
        dc = new DataColumn("Transno");
        dt.Columns.Add(dc);
        dc = new DataColumn("TransDate");
        dt.Columns.Add(dc);
        dc = new DataColumn("Amount");
        dt.Columns.Add(dc);
        dc = new DataColumn("Out10");
        dt.Columns.Add(dc);
        dc = new DataColumn("Out30");
        dt.Columns.Add(dc);
        dc = new DataColumn("Out60");
        dt.Columns.Add(dc);
        dc = new DataColumn("Out90");
        dt.Columns.Add(dc);
        dc = new DataColumn("Out90above");
        dt.Columns.Add(dc);
        ds.Tables.Add(dt);
        dr = dt.NewRow();
        dr["Transno"] = "";
        dr["TransDate"] = "";
        dr["Amount"] = "";
        dr["Out10"] = "";
        dr["Out30"] = "";
        dr["Out60"] = "";
        dr["Out90"] = "";
        dr["Out90above"] = "";
        ds.Tables[0].Rows.Add(dr);
        gvOuts.DataSource = ds;
        gvOuts.DataBind();
    }
    public void GenerateDs(string Outs)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;
        int days = 0;
        dc = new DataColumn("Transno");
        dt.Columns.Add(dc);
        dc = new DataColumn("TransDate");
        dt.Columns.Add(dc);
        dc = new DataColumn("Amount");
        dt.Columns.Add(dc);
        dc = new DataColumn("Out10");
        dt.Columns.Add(dc);
        dc = new DataColumn("Out30");
        dt.Columns.Add(dc);
        dc = new DataColumn("Out60");
        dt.Columns.Add(dc);
        dc = new DataColumn("Out90");
        dt.Columns.Add(dc);
        dc = new DataColumn("Out90above");
        dt.Columns.Add(dc);
        ds.Tables.Add(dt);
        string[] Record = Outs.Split(',');
        for (int i = 0; i <= Record.Length - 1; i++)
        {
            dr = dt.NewRow();
            string[] OutRec = Record[i].ToString().Split('^');

            dr["Transno"] = OutRec[1].ToString();
            dr["TransDate"] = OutRec[2].ToString();
            dr["Amount"] = OutRec[3].ToString();
            days = Convert.ToInt32(OutRec[0]);
            if (days >= 0 && days <= 10)
            {
                dr["Out10"] = OutRec[0].ToString();
                SumOuts10 = SumOuts10 + Convert.ToDouble(dr["Out10"]);
                lblTotalOuts10.Text = SumOuts10.ToString("#0.00");
            }
            else
                dr["Out10"] = "";

            if (days >= 11 && days <= 30)
            {
                dr["Out30"] = OutRec[0].ToString();
                SumOuts30 = SumOuts30 + Convert.ToDouble(dr["Out30"]);
                lblTotalOuts30.Text = SumOuts30.ToString("#0.00");

            }
            else
                dr["Out30"] = "";

            if (days >= 31 && days <= 60)
            {
                dr["Out60"] = OutRec[0].ToString();
                SumOuts60 = SumOuts60 + Convert.ToDouble(dr["Out60"]);
                lblTotalOuts60.Text = SumOuts60.ToString("#0.00");
            }
            else
                dr["Out60"] = "";

            if (days >= 61 && days <= 90)
            {
                dr["Out90"] = OutRec[0].ToString();

                SumOuts90 = SumOuts90 + Convert.ToDouble(dr["Out90"]);
                lblTotalOuts90.Text = SumOuts90.ToString("#0.00");
            }
            else
                dr["Out90"] = "";

            if (days >= 91)
            {
                dr["Out90above"] = OutRec[0].ToString();
                SumOuts91 = SumOuts91 + Convert.ToDouble(dr["Out90above"]);
                lblTotalOuts91.Text = SumOuts91.ToString("#0.00");
            }
            else
                dr["Out90above"] = "";







            ds.Tables[0].Rows.Add(dr);
            gvOuts.DataSource = ds;
            gvOuts.DataBind();

        }
    }
    public void Reset()
    {
        lblTotalOuts10.Text = "";
        lblTotalOuts30.Text = "";
        lblTotalOuts60.Text = "";
        lblTotalOuts90.Text = "";
        lblTotalOuts91.Text = "";
    }

}
