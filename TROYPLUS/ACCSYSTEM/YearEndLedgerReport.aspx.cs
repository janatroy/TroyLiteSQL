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

public partial class YearEndLedgerReport : System.Web.UI.Page
{
    public int pagecnt = 0;
    public DataSet grdDs = new DataSet();
    public DataTable grdDt = new DataTable();
    public int cnt = 1;
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            double sumTotal = 0;
            double dTot = 0;
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                //GetLedgerIndex();

                //DataTable dtNew = GenerateDs("", "");

                //grdDs.Tables.Add(dtNew);
                //GetLedgerTran();


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public void GetLedgerTran(DateTime startDate, DateTime endDate)
    {
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        DataSet ds = rpt.getLedgerTransaction(0, sDataSource, startDate, endDate);
        if (ds != null)
        {
            gvMainLedger.DataSource = ds;
            gvMainLedger.DataBind();
            grdDs.Tables[0].Rows[0].Delete();
            gvLedger.DataSource = grdDs;
            gvLedger.DataBind();
        }
    }
    public void GetLedgerIndex()
    {
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        DataSet ds = rpt.getLedgerIndex(sDataSource);
        //gvLedger.DataSource = ds;
        //gvLedger.DataBind();

    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                DateTime startDate, endDate;
                startDate = Convert.ToDateTime(txtStartDate.Text);
                endDate = Convert.ToDateTime(txtEndDate.Text);

                lblStartDate.Text = txtStartDate.Text;
                lblEndDate.Text = txtEndDate.Text;
                dvYearEndReport.Visible = true;
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                GetLedgerIndex();

                DataTable dtNew = GenerateDs("", "");

                grdDs.Tables.Add(dtNew);
                GetLedgerTran(startDate, endDate);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvMainLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int modCnt = 0;

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvMainLedger = (GridView)sender;

                pagecnt = pagecnt + 1;
                //if (pagecnt > 47)
                //{
                //    cnt = cnt + 1;
                //    pagecnt = 0;
                //}

                cnt = pagecnt / 49;
                if (pagecnt % 49 > 0)
                    cnt = cnt + 1;
                //else
                //  cnt = cnt - 1;
                // (gvMainLedger.PageSize * gvMainLedger.PageIndex) + e.Row.RowIndex;
                Label lblPage = (Label)e.Row.FindControl("lblPg");
                string ledgername = e.Row.Cells[0].Text;
                string ledgerID = string.Empty;
                if (gvMainLedger.DataKeys[e.Row.RowIndex].Value != "")
                {
                    ledgerID = Convert.ToString(gvMainLedger.DataKeys[e.Row.RowIndex].Value);
                }
                lblPage.Text = cnt.ToString();

                BusinessLogic bl = new BusinessLogic(sDataSource);
                grdDt = GenerateDs(ledgername, cnt.ToString());
                bl.UpdateLedgerFolio(Convert.ToInt32(ledgerID), cnt);
                if (grdDt != null)
                {

                    for (int k = 0; k <= grdDt.Rows.Count - 1; k++)
                    {

                        if (grdDt != null && grdDt.Rows.Count > 0)
                            grdDs.Tables[0].ImportRow(grdDt.Rows[k]);
                    }


                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    public DataTable GenerateDs(string Ledgername, string pagenum)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;


        dc = new DataColumn("LedgerName");
        dt.Columns.Add(dc);


        dc = new DataColumn("pagenum");
        dt.Columns.Add(dc);



        dr = dt.NewRow();
        dr["LedgerName"] = Ledgername;

        dr["pagenum"] = pagenum;
        dt.Rows.Add(dr);
        return dt;
    }
}
