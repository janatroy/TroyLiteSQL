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

public partial class OutstandingBalanceExecutiveReport : System.Web.UI.Page
{
    Double amt = 0.00d;
    public string sDataSource = string.Empty;
    Double sumAmt = 0.0d;
    double SumOuts10 = 0.0;
    double SumOuts30 = 0.0;
    double SumOuts60 = 0.0;
    double SumOuts90 = 0.0;
    double SumOuts91 = 0.0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!IsPostBack)
            {
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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            divPrint.Visible = true;
            ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();

            int iLedgerID = 0;
            Double daySum = 0.0;
            Double amtSum = 0.0;
            Double CreditTotal = 0.00d;
            Double DebitAmt = 0.00d;
            Double TotalCredit = 0.0d;
            Double OpeningBal = 0.0d;
            DataSet grdDs = new DataSet();
            DataTable grdDt = new DataTable();
            string OutsDate = string.Empty;
            string sLedger = string.Empty;
            DataTable dtNew = new DataTable();
            CreditTotal = rpt.GetTotalCredit(sDataSource, iLedgerID);
            TotalCredit = CreditTotal;
            int cnt = 0;
            int rowcnt = 0;
            int Days = 0;
            //lblTotalAmt.Text = "0.00";
            dtNew = GenerateDs();
            string[] OutRec;
            string[] Record;
            grdDs.Tables.Add(dtNew);
            int iExecID = 0;
            if (drpIncharge.SelectedItem != null)
                iExecID = Convert.ToInt32(drpIncharge.SelectedItem.Value);
            DataSet dsMain = new DataSet();
            if (iExecID != 0)
                dsMain = rpt.GetLedgerExecutive(sDataSource, iExecID);
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Executives Found')", true);
                return;
            }
            if (dsMain != null)
            {
                if (dsMain.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow maindr in dsMain.Tables[0].Rows)
                    {
                        OutsDate = "";
                        daySum = 0;
                        amtSum = 0;
                        if (maindr["LedgerID"] != null && maindr["LedgerID"].ToString() != "")
                        {
                            iLedgerID = Convert.ToInt32(maindr["LedgerID"]);
                            sLedger = Convert.ToString(maindr["LedgerName"]);
                        }

                        DataSet dsDebit = rpt.GetDebitCreditLedger(sDataSource, iLedgerID, "Debit");
                        CreditTotal = rpt.GetTotalCredit(sDataSource, iLedgerID);
                        TotalCredit = CreditTotal;
                        cnt = 0;
                        rowcnt = 0;
                        Days = 0;
                        //lblTotalAmt.Text = "0.00";

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
                                        if (OpeningBal > 0)
                                            TotalCredit = TotalCredit - OpeningBal;
                                        else
                                            TotalCredit = TotalCredit + Math.Abs(OpeningBal);
                                    }

                                    if (dr["Amount"] != null)
                                    {
                                        DebitAmt = Convert.ToDouble(dr["Amount"].ToString());
                                        if (TotalCredit < 0)
                                        {
                                            //DebitAmt = DebitAmt + Math.Abs(TotalCredit);
                                            OutsDate = OutsDate + "-1" + "^" + "" + "^" + Convert.ToString(Convert.ToDouble(Math.Abs(TotalCredit))) + ",";
                                            amt = amt + Math.Abs(TotalCredit);
                                            Days = rpt.GetDays(sDataSource, dr["Transdate"].ToString(), Convert.ToInt32(dr["Transno"]));
                                            OutsDate = OutsDate + Days.ToString() + "^" + dr["Transdate"].ToString() + "^" + dr["Amount"].ToString() + ",";
                                            amt = amt + Convert.ToDouble(dr["Amount"]);
                                            TotalCredit = 0;
                                        }
                                        else if (TotalCredit > DebitAmt)
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
                                                OutsDate = OutsDate + Days.ToString() + "^" + dr["Transdate"].ToString() + "^" + Convert.ToString(Convert.ToDouble(dr["amount"]) - TotalCredit) + ",";
                                                amt = amt + Convert.ToDouble(dr["amount"]) - TotalCredit;

                                            }
                                            else
                                            {
                                                Days = rpt.GetDays(sDataSource, dr["Transdate"].ToString(), Convert.ToInt32(dr["Transno"]));
                                                OutsDate = OutsDate + Days.ToString() + "^" + dr["Transdate"].ToString() + "^" + dr["Amount"].ToString() + ",";
                                                amt = amt + Convert.ToDouble(dr["Amount"]);

                                            }
                                            cnt = cnt + 1;

                                        }
                                    }

                                    rowcnt = rowcnt + 1;
                                }
                                if (OutsDate != "")
                                {
                                    OutsDate = OutsDate.Remove(OutsDate.LastIndexOf(',', OutsDate.Length - 1));
                                    //OutRec = OutsDate.Split(',');
                                    //grdDt = GenerateDs(OutsDate, sLedger);
                                    Record = OutsDate.Split(',');
                                    for (int i = 0; i <= Record.Length - 1; i++)
                                    {
                                        OutRec = Record[i].ToString().Split('^');
                                        daySum = daySum + Convert.ToDouble(OutRec[0]);
                                        amtSum = amtSum + Convert.ToDouble(OutRec[2]);

                                    }
                                    OutsDate = "";
                                }
                                //else
                                //{
                                //    grdDt = null;
                                //}
                                //if (grdDt != null)
                                //{
                                //    for (int k = 0; k <= grdDt.Rows.Count - 1; k++)
                                //    {

                                //        if (grdDt != null && grdDt.Rows.Count > 0)
                                //            grdDs.Tables[0].ImportRow(grdDt.Rows[k]);
                                //    }
                                //}
                            }


                        }//nul debit
                        OutsDate = OutsDate + daySum.ToString() + "^" + "" + "^" + amtSum.ToString() + ",";
                        OutsDate = OutsDate.Remove(OutsDate.LastIndexOf(',', OutsDate.Length - 1));
                        grdDt = GenerateDs(OutsDate, sLedger);
                        if (grdDt != null)
                        {
                            for (int k = 0; k <= grdDt.Rows.Count - 1; k++)
                            {

                                if (grdDt != null && grdDt.Rows.Count > 0)
                                    grdDs.Tables[0].ImportRow(grdDt.Rows[k]);
                            }
                        }

                        // OutRec = "";
                    }
                }

            }



            gvOuts.DataSource = grdDs;
            gvOuts.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void row(string p)
    {
        throw new NotImplementedException();
    }

    public DataTable GenerateDs()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;
        int days = 0;

        dc = new DataColumn("Customer");
        dt.Columns.Add(dc);

        //dc = new DataColumn("TransDate");
        //dt.Columns.Add(dc);
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

        //ds.Tables.Add(dt);
        dr = dt.NewRow();
        dr["Customer"] = "";

        dr["Amount"] = "";
        //dr["TransDate"] = "";
        dr["Out10"] = "";
        dr["Out30"] = "";
        dr["Out60"] = "";
        dr["Out90"] = "";
        dr["Out90above"] = "";
        dt.Rows.Add(dr);
        //ds.Tables[0].Rows.Add(dr);
        return dt;
    }
    public DataTable GenerateDs(string Outs, string sLedger)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;
        int days = 0;
        dc = new DataColumn("Customer");
        dt.Columns.Add(dc);

        //dc = new DataColumn("TransDate");
        //dt.Columns.Add(dc);
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
        //ds.Tables.Add(dt);
        string[] Record = Outs.Split(',');
        for (int i = 0; i <= Record.Length - 1; i++)
        {
            dr = dt.NewRow();
            string[] OutRec = Record[i].ToString().Split('^');

            dr["Customer"] = sLedger;
            //dr["Days"] = OutRec[0].ToString();
            // dr["TransDate"] = Convert.ToDateTime(OutRec[1]).ToString("dd/MM/yyyy");
            dr["Amount"] = OutRec[2].ToString();

            days = Convert.ToInt32(OutRec[0]);
            if (days >= 0 && days <= 10)
            {

                dr["Out10"] = OutRec[0].ToString();
                //SumOuts10 = SumOuts10 + Convert.ToDouble(dr["Out10"]);
                // dr["Out10"] =  SumOuts10.ToString("#0.00");
            }
            else
                dr["Out10"] = "";

            if (days >= 11 && days <= 30)
            {
                dr["Out30"] = OutRec[0].ToString();
                ///SumOuts30 = SumOuts30 + Convert.ToDouble(dr["Out30"]);
                // dr["Out30"] = SumOuts30.ToString("#0.00");

            }
            else
                dr["Out30"] = "";

            if (days >= 31 && days <= 60)
            {
                dr["Out60"] = OutRec[0].ToString();
                // SumOuts60 = SumOuts60 + Convert.ToDouble(dr["Out60"]);
                // dr["Out60"]   = SumOuts60.ToString("#0.00");
            }
            else
                dr["Out60"] = "";

            if (days >= 61 && days <= 90)
            {
                dr["Out90"] = OutRec[0].ToString();

                // SumOuts90 = SumOuts90 + Convert.ToDouble(dr["Out90"]);
                // dr["Out90"]  = SumOuts90.ToString("#0.00");
            }
            else
                dr["Out90"] = "";

            if (days >= 91)
            {
                dr["Out90above"] = OutRec[0].ToString();
                // SumOuts91 = SumOuts91 + Convert.ToDouble(dr["Out90above"]);
                // dr["Out90above"] = SumOuts91.ToString("#0.00");
            }
            else
                dr["Out90above"] = "";


            //ds.Tables[0].Rows.Add(dr);
            dt.Rows.Add(dr);

        }
        return dt;
    }
    public void gvOuts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            Double amt = 0.00d;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl = (Label)e.Row.FindControl("lblAmount");
                Label lblOuts10 = (Label)e.Row.FindControl("lblOuts10");
                Label lblOuts30 = (Label)e.Row.FindControl("lblOuts30");
                Label lblOuts60 = (Label)e.Row.FindControl("lblOuts60");
                Label lblOuts90 = (Label)e.Row.FindControl("lblOuts90");
                Label lblOuts91 = (Label)e.Row.FindControl("lblOuts91");

                if (lbl.Text != "")
                    sumAmt = sumAmt + Convert.ToDouble(lbl.Text);

                if (lblOuts10 != null && lblOuts10.Text != "")
                    SumOuts10 = SumOuts10 + Convert.ToDouble(lblOuts10.Text);
                if (lblOuts30 != null && lblOuts30.Text != "")
                    SumOuts30 = SumOuts30 + Convert.ToDouble(lblOuts30.Text);
                if (lblOuts60 != null && lblOuts60.Text != "")
                    SumOuts60 = SumOuts60 + Convert.ToDouble(lblOuts60.Text);
                if (lblOuts90 != null && lblOuts90.Text != "")
                    SumOuts90 = SumOuts90 + Convert.ToDouble(lblOuts90.Text);
                if (lblOuts91 != null && lblOuts91.Text != "")
                    SumOuts91 = SumOuts91 + Convert.ToDouble(lblOuts91.Text);

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = SumOuts10.ToString();
                e.Row.Cells[2].Text = SumOuts30.ToString();
                e.Row.Cells[3].Text = SumOuts60.ToString();
                e.Row.Cells[4].Text = SumOuts90.ToString();
                e.Row.Cells[5].Text = SumOuts91.ToString();
                e.Row.Cells[6].Text = sumAmt.ToString();
            }
            //lblTotalAmt.Text = sumAmt.ToString("#0.00");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
