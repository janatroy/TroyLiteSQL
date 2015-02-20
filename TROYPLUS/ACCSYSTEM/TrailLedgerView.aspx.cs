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

public partial class TrailLedgerView : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    public double debitTotal = 0;
    public double creditTotal = 0;
    public double netTotal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hdFilename.Value = "Reports//" + System.Guid.NewGuid().ToString() + ConfigurationSettings.AppSettings["LedgerReportFileName"].ToString();
            }
            int iGroupID = 0;
            if (Request["GroupName"] != null)
                lGroupName.Text = Request["GroupName"].ToString();
            if (Request["GroupID"] != null)
            {
                iGroupID = Convert.ToInt32(Request["GroupID"]);
                DataSet ds = new DataSet();
                if (ViewState["ds"] == null)
                {
                    ds = showTrialBalance(iGroupID);
                    ViewState["ds"] = ds;
                    gvTrailBalance.DataSource = ds;
                    gvTrailBalance.DataBind();
                }
                else
                {
                    gvTrailBalance.DataSource = (DataSet)ViewState["ds"];
                    gvTrailBalance.DataBind();

                }
                string sXmlPath = Server.MapPath(hdFilename.Value);
                string sXmlNodeName = "LedgerAccount";

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public DataSet showTrialBalance(int iGroupID)
    {
        DataTable grdDt = new DataTable();
        DataSet grdDs = new DataSet();
        DataTable dtNew = new DataTable();
        int ledgerID = 0;
        double debitSum = 0.0d;
        double creditSum = 0.0d;
        double totalSum = 0.0d;
        string strParticulars = string.Empty;
        string strDebit = string.Empty;
        string strCredit = string.Empty;
        dtNew = GenerateDs("", "", "");
        grdDs.Tables.Add(dtNew);

        string TrailFlag = string.Empty;
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet mainDs = bl.GetLedgerTrail(iGroupID);
        if (mainDs != null)
        {
            if (mainDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow mainRow in mainDs.Tables[0].Rows)
                {
                    ledgerID = Convert.ToInt32(mainRow["LedgerID"]);
                    debitSum = bl.GetDebitTrailSum(ledgerID);
                    creditSum = bl.GetCreditTrailSum(ledgerID);
                    debitTotal = debitTotal + debitSum;
                    creditTotal = creditTotal + creditSum;
                    strParticulars = Convert.ToString(mainRow["LedgerName"]);

                    //totalSum = debitSum - creditSum;
                    strDebit = Convert.ToString(debitSum.ToString("f2"));
                    strCredit = Convert.ToString(creditSum.ToString("f2"));

                    grdDt = GenerateDs(strParticulars, strDebit, strCredit);
                    if (grdDt != null)
                    {
                        for (int k = 0; k <= grdDt.Rows.Count - 1; k++)
                        {

                            if (grdDt != null && grdDt.Rows.Count > 0)
                                grdDs.Tables[0].ImportRow(grdDt.Rows[k]);
                        }
                    }
                } //mainDs foreach
            }//mainDs rows COunt
        }//mainDs null Check
        netTotal = debitTotal - creditTotal;
        if (netTotal > 0)
            lblDebitTotal.Text = netTotal.ToString("f2") + " Dr";
        else
        {
            netTotal = Math.Abs(netTotal);
            lblCreditTotal.Text = netTotal.ToString("f2") + " Cr";
        }
        return grdDs;
    }
    public DataTable GenerateDs(string strParticulars, string strDebit, string strCredit)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;


        dc = new DataColumn("Particulars");
        dt.Columns.Add(dc);

        //dc = new DataColumn("TransDate");
        //dt.Columns.Add(dc);
        dc = new DataColumn("Debit");
        dt.Columns.Add(dc);

        dc = new DataColumn("Credit");
        dt.Columns.Add(dc);


        //ds.Tables.Add(dt);
        dr = dt.NewRow();
        dr["Particulars"] = strParticulars;

        dr["Debit"] = strDebit;
        //dr["TransDate"] = "";
        dr["Credit"] = strCredit;
        dt.Rows.Add(dr);
        //ds.Tables[0].Rows.Add(dr);
        return dt;
    }
    public void gvTrailBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblDebit = (Label)e.Row.FindControl("lblDebit");
        //    Label lblCredit = (Label)e.Row.FindControl("lblCredit");
        //    if (lblDebit != null && lblDebit.Text != "")
        //        debitTotal = debitTotal + Convert.ToDouble(lblDebit.Text);
        //    if (lblCredit != null && lblCredit.Text != "")
        //        creditTotal = creditTotal + Convert.ToDouble(lblCredit.Text);
        //}

    }
    protected void gvTrailBalance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvTrailBalance.PageIndex = e.NewPageIndex;

            int iGroupID = 0;
            if (Request["GroupID"] != null)
            {
                iGroupID = Convert.ToInt32(Request["GroupID"]);
                DataSet ds = new DataSet();
                if (ViewState["ds"] == null)
                {
                    ds = showTrialBalance(iGroupID);
                    ViewState["ds"] = ds;
                    gvTrailBalance.DataSource = ds;
                    gvTrailBalance.DataBind();
                }
                else
                {
                    gvTrailBalance.DataSource = (DataSet)ViewState["ds"];
                    gvTrailBalance.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
