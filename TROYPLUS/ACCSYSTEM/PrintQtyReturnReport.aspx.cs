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
using System.IO;

public partial class PrintQtyReturnReport : System.Web.UI.Page
{
    public Double sSUM = 0.0;
    public Double sSUMRet = 0.0;
    public Double sSUMPend = 0.0;
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

        lblBillDate.Text = DateTime.Now.ToShortDateString();
        deleteFile();
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            deleteFile();
            if (Session["dataSet"] != null)
            {

                Response.Redirect("QtyReturnReport.aspx");

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
                if (DataBinder.Eval(e.Row.DataItem, "QtySale").ToString() != "")
                    sSUM = sSUM + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "QtySale"));
                if (DataBinder.Eval(e.Row.DataItem, "QtyReturn").ToString() != "")
                    sSUMRet = sSUMRet + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "QtyReturn"));
                if (DataBinder.Eval(e.Row.DataItem, "QtyPending").ToString() != "")
                    sSUMPend = sSUMPend + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "QtyPending"));

                lblQtySum.Text = Convert.ToString(sSUM);
                lblQtyReturn.Text = Convert.ToString(sSUMRet);
                lblQtyPend.Text = Convert.ToString(sSUMPend);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


}
