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

public partial class ExecutiveOutstandingReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    public double dTotal = 0;
    public double dSDebit = 0;
    public double dSCredit = 0;
    public double dExTot = 0;
    public double dAmtC = 0;
    public double dAmtD = 0;
    public string viewFlag = string.Empty;

    public string sTotal = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                viewFlag = "Both Outstanding";
                DataSet ds = bl.GetExecutiveOuts();

                gvExecutive.DataSource = ds;
                gvExecutive.DataBind();

                if (Request.Cookies["Company"] != null)
                {
                    DataSet companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

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
    public void gvExecutive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double dTot = 0;
            int iExec = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Empno") != DBNull.Value)
                    iExec = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Empno"));
                GridView gv = e.Row.FindControl("gvDealer") as GridView;


                ReportsBL.ReportClass rptOutstandingReport;
                DataSet ds = new DataSet();

                rptOutstandingReport = new ReportsBL.ReportClass();
                //Sundry Debtors (Customers) - 1
                ds = rptOutstandingReport.generateOutStandingReportDS(1, iExec, sDataSource);
                gv.DataSource = ds;
                gv.DataBind();
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                lblTotal.Text = sTotal; //dTotal.ToString("f2");
                if (sTotal.Contains("Dr"))
                    dExTot = dExTot + dTotal;
                else
                {
                    dExTot = dExTot - dTotal;
                    //e.Row.Style.Add("Display", "none");
                }
                if (dTotal == 0 || dTotal < 0)
                {
                    e.Row.Style.Add("Display", "none");
                }

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                Label lblTotEx = (Label)e.Row.FindControl("lblTotEx");
                if (dExTot > 0)
                {
                    lblTotEx.Text = dExTot.ToString("f2") + " Dr ";
                }

                else
                {

                    lblTotEx.Text = Math.Abs(dExTot).ToString("f2") + " Cr ";
                }
                dExTot = 0;
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
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            viewFlag = drpView.SelectedItem.Text;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = bl.GetExecutiveOuts();
            gvExecutive.DataSource = ds;
            gvExecutive.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void gvDealer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double dDebit = 0;
            double dCredit = 0;
            int iLedgerID = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Debit") != DBNull.Value)
                    dDebit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
                if (DataBinder.Eval(e.Row.DataItem, "Credit") != DBNull.Value)
                    dCredit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
                if (DataBinder.Eval(e.Row.DataItem, "LedgerID") != DBNull.Value)
                {
                    if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "LedgerID")) != "")
                        iLedgerID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "LedgerID"));
                    else
                        return;
                }

                if (viewFlag == "Customer Outstanding")
                {
                    if (dDebit < dCredit)
                    {
                        e.Row.Style.Add("Display", "none");
                    }
                    dSDebit = dSDebit + dDebit;
                    //dSCredit = dSCredit + dCredit;
                }
                else if (viewFlag == "Company Outstanding")
                {
                    if (dCredit < dDebit)
                    {
                        e.Row.Style.Add("Display", "none");
                    }
                    //dSDebit = dSDebit + dDebit;
                    dSCredit = dSCredit + dCredit;
                }
                else if (viewFlag == "Both Outstanding")
                {
                    dSDebit = dSDebit + dDebit;
                    dSCredit = dSCredit + dCredit;
                }
                if (iLedgerID == 0)
                    iLedgerID = 0;
                if (dDebit < dCredit)
                {
                    e.Row.Style.Add("Display", "none");
                    dSCredit = dSCredit - dCredit;
                }
                BusinessLogic bl = new BusinessLogic(sDataSource);
                GridView gCS = e.Row.FindControl("gvCredit") as GridView;
                DataSet ds = new DataSet();
                ds = bl.GetSalesBillsCustomer(iLedgerID);
                gCS.DataSource = ds;
                gCS.DataBind();
                GridView gDS = e.Row.FindControl("gvDebit") as GridView;
                DataSet dsR = new DataSet();
                dsR = bl.GetReceivedAmountCustomer(iLedgerID);
                gDS.DataSource = dsR;
                gDS.DataBind();

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (dSDebit > dSCredit)
                {
                    dTotal = dSDebit - dSCredit;
                    sTotal = dTotal.ToString("f2") + " Dr";
                    //dSDebit = 0;
                    //dSCredit = 0;
                }
                else
                {
                    dTotal = dSCredit - dSDebit;
                    sTotal = dTotal.ToString("f2") + " Cr";
                    ////dSDebit = 0;
                    ////dSCredit = 0;
                }

                Label lblDTotal = (Label)e.Row.FindControl("lblTDebit");
                Label lblCTotal = (Label)e.Row.FindControl("lblTCredit");
                lblDTotal.Text = dSDebit.ToString("f2");
                lblCTotal.Text = dSCredit.ToString("f2");
                dSDebit = 0;
                dSCredit = 0;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void gvDebit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double dAmt = 0;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Amount") != DBNull.Value)
                    dAmt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                dAmtD = dAmtD + dAmt;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblDTotal = (Label)e.Row.FindControl("lblT");
                lblDTotal.Text = dAmtD.ToString("f2");
                dAmtD = 0;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void gvCredit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double dAmt = 0;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Amount") != DBNull.Value)
                    dAmt = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                dAmtC = dAmtC + dAmt;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblCTotal = (Label)e.Row.FindControl("lblT");
                lblCTotal.Text = dAmtC.ToString("f2");
                dAmtC = 0;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


}
