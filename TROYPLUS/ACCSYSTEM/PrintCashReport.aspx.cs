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

public partial class PrintCashReport : System.Web.UI.Page
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
        ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
        if (Request.Cookies["Company"] != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        double opCr = 0.0;
        double opDr = 0.0;
        double netOp = 0.0;
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


            gvCash.DataSource = ds;
            gvCash.DataBind();

        }
        if (Session["startDate"] != null)
        {
            lblStartDate.Text = Session["startDate"].ToString();
        }
        if (Session["endDate"] != null)
        {
            lblEndDate.Text = Session["endDate"].ToString();
        }


        opCr = rpt.getLedgerOpeningBalance(1, "credit", sDataSource) + rpt.getOpeningBalance(0, 0, 1, "credit", Convert.ToDateTime(Session["startDate"]), sDataSource);
        opDr = rpt.getLedgerOpeningBalance(1, "debit", sDataSource) + rpt.getOpeningBalance(0, 0, 1, "debit", Convert.ToDateTime(Session["startDate"]), sDataSource);

        if (opDr > opCr)
        {
            netOp = opDr - opCr;
            lblOBDR.Text = Convert.ToString(netOp);
            lblOBCR.Text = "0.00";
            if (damt > camt)
            {
                dDiffamt = netOp + (damt - camt);
                cDiffamt = 0;
            }
            else
            {
                if (((camt - damt) - netOp) > 0)
                {
                    cDiffamt = (camt - damt) - netOp;
                    dDiffamt = 0;
                }
                else
                {
                    dDiffamt = Math.Abs((camt - damt) - netOp);
                    cDiffamt = 0;
                }
            }

            lblOBCR.Text = "0.00";
            lblOBDR.Text = Convert.ToString(netOp);

        }
        else
        {
            netOp = opCr - opDr;
            if (damt > camt)
            {
                if (((damt - camt) - netOp) > 0)
                {
                    dDiffamt = (damt - camt) - netOp;
                    cDiffamt = 0;
                }
                else
                {
                    cDiffamt = Math.Abs((damt - camt) - netOp);
                    dDiffamt = 0;
                }

            }
            else
            {
                cDiffamt = netOp + (camt - damt);
                dDiffamt = 0;
            }
            lblOBDR.Text = "0.00";
            lblOBCR.Text = Convert.ToString(netOp);

        }
        if (dDiffamt > 0)
        {
            lblDebitDiff.Text = Convert.ToString(dDiffamt);
            lblCreditDiff.Text = "0";
        }
        if (cDiffamt > 0)
        {
            lblDebitDiff.Text = "0";
            lblCreditDiff.Text = Convert.ToString(cDiffamt);

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

                Response.Redirect("CashAccountReport.aspx");

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void gvCash_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                damt = damt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
                camt = camt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));
                lblDebitSum.Text = Convert.ToString(damt);
                lblCreditSum.Text = Convert.ToString(camt);
                //dDiffamt = damt - camt;
                //cDiffamt = camt - damt;

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
