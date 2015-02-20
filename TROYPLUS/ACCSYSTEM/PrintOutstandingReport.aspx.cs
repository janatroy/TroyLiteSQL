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
public partial class PrintOutstandingReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    public Double damt = 0.0;
    public Double camt = 0.0;
    public Double dDiffamt = 0.0;
    public Double cDiffamt = 0.0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
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


        if (Session["sundry"] != null)
        {
            lblSundry.Text = Session["sundry"].ToString();
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

                Response.Redirect("OutstandingReport.aspx");

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
            Double cbDr = 0.00;
            Double cbCr = 0.00;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                damt = damt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit"));
                camt = camt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit"));




                lblDebitSum.Text = Convert.ToString(damt);
                lblCreditSum.Text = Convert.ToString(camt);
                lblCreditSum.Text = String.Format("{0:f2}", lblCreditSum.Text);
                lblDebitSum.Text = String.Format("{0:f2}", lblDebitSum.Text);
                dDiffamt = damt - camt;
                cDiffamt = camt - damt;

                if (dDiffamt >= 0)
                {
                    lblDebitDiff.Text = Convert.ToString(dDiffamt);
                    lblCreditDiff.Text = "0.00";
                    lblCreditDiff.Text = String.Format("{0:f2}", lblCreditDiff.Text);
                    lblDebitDiff.Text = String.Format("{0:f2}", lblDebitDiff.Text);
                }
                if (cDiffamt > 0)
                {
                    lblDebitDiff.Text = "0.00";
                    lblCreditDiff.Text = Convert.ToString(cDiffamt);
                    lblCreditDiff.Text = String.Format("{0:f2}", lblCreditDiff.Text);
                    lblDebitDiff.Text = String.Format("{0:f2}", lblDebitDiff.Text);
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
