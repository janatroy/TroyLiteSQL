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
using System.IO;

public partial class PrintLedgerReport : System.Web.UI.Page
{
    public Double damt = 0.0;
    public Double camt = 0.0;
    public Double dDiffamt = 0.0;
    public Double cDiffamt = 0.0;
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
                printPreview();
            if (hdToDelete.Value == "BrowserClose")
            {
                deleteFile();
            }
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
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
    protected void printPreview()
    {
        string sFilename = string.Empty;
        DataSet ds = new DataSet();

        double opCr = 0.0;
        double opDr = 0.0;
        double netOp = 0.0;
        Double cbDr = 0.00;
        Double cbCr = 0.00;
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();

        if (Session["dataSet"] != null)
        {
            sFilename = Server.MapPath(Session["dataSet"].ToString());
            if (File.Exists(sFilename))
            {
                ds.ReadXml(sFilename, XmlReadMode.InferSchema);
                ViewState["filename"] = ds;
            }
            else
            {
                ds = (DataSet)ViewState["filename"];
            }


            gvLedger.DataSource = ds;
            gvLedger.DataBind();

        }
        if (Session["startDate"] != null)
        {
            lblStartDate.Text = Session["startDate"].ToString();
        }
        if (Session["endDate"] != null)
        {
            lblEndDate.Text = Session["endDate"].ToString();
        }
        if (Session["Ledger"] != null)
        {
            lblLedger.Text = Session["Ledger"].ToString();
        }
        int ledgerID = 0;
        if (Session["LedgerID"] != null)
        {
            ledgerID = Convert.ToInt32(Session["LedgerID"]);

        }
        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        if (Session["ledgerID"] != null)
        {
            opCr = rpt.getLedgerOpeningBalance(Convert.ToInt32(Session["LedgerID"]), "credit", sDataSource) + rpt.getOpeningBalance(0, 0, Convert.ToInt32(Session["LedgerID"]), "credit", Convert.ToDateTime(Session["startDate"]), sDataSource);
            opDr = rpt.getLedgerOpeningBalance(Convert.ToInt32(Session["LedgerID"]), "debit", sDataSource) + rpt.getOpeningBalance(0, 0, Convert.ToInt32(Session["LedgerID"]), "debit", Convert.ToDateTime(Session["startDate"]), sDataSource);

            cbDr = opDr + Convert.ToDouble(lblDebitDiff.Text);
            cbCr = opCr + Convert.ToDouble(lblCreditDiff.Text);
        }
        if (opDr > opCr)
        {
            netOp = opDr - opCr;
            lblOBDR.Text = Convert.ToString(netOp);
            lblOBCR.Text = "0.00";
        }
        else
        {
            netOp = opCr - opDr;
            lblOBDR.Text = "0.00";
            lblOBCR.Text = Convert.ToString(netOp);
        }
        if (cbDr > cbCr)
        {

            cbDr = cbDr - cbCr;
            lblClosDr.Text = Convert.ToString(cbDr);
            lblClosCr.Text = "0.00";
        }
        else
        {
            cbCr = cbCr - cbDr;
            lblClosCr.Text = Convert.ToString(cbCr);
            lblClosDr.Text = "0.00";
        }

        lblBillDate.Text = DateTime.Now.ToShortDateString();
        deleteFile();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            deleteFile();
            if (Session["dataSet"] != null)
            {

                Response.Redirect("LedgerReport.aspx");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                damt = damt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
                camt = camt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));

                //Label lblOpDr = e.Row.FindControl("obDr") as Label;
                //if(DataBinder.Eval(e.Row.DataItem, "OpeningBalanceDR")!=null)
                //lblOpDr.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "OpeningBalanceDR"));
                //Label lblOpCr = e.Row.FindControl("obCr") as Label;
                //if (DataBinder.Eval(e.Row.DataItem, "OpeningBalanceCR")!=null)
                //lblOpCr.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "OpeningBalanceCR"));

                //BusinessLogic bl = new BusinessLogic(sDataSource);
                //if (lblOpDr.Text != "")
                // lblOBDR.Text = "1000" ;
                //else
                //  lblOBDR.Text = "0.00";

                //if (lblOpCr.Text != "")
                // lblOBCR.Text = lblOpCr.Text;
                //else
                //  lblOBCR.Text = "0.00";



                lblDebitSum.Text = Convert.ToString(damt);
                lblCreditSum.Text = Convert.ToString(camt);
                dDiffamt = damt - camt;
                cDiffamt = camt - damt;

                if (dDiffamt >= 0)
                {
                    lblDebitDiff.Text = Convert.ToString(dDiffamt);
                    lblCreditDiff.Text = "0.00";
                }
                if (cDiffamt > 0)
                {
                    lblDebitDiff.Text = "0.00";
                    lblCreditDiff.Text = Convert.ToString(cDiffamt);

                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void deleteFile()
    {
        if (Session["Filename"] != null)
        {
            string delFilename = Session["Filename"].ToString();
            if (File.Exists(Server.MapPath(delFilename)))
                File.Delete(Server.MapPath(delFilename));
        }
    }
}
