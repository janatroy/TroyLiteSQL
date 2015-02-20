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


public partial class ReOrderlevelReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
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
            lblBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ReportsBL.ReportClass rptReorder = new ReportsBL.ReportClass();
            DataSet ds = rptReorder.getReorder(sDataSource);
            gvProducts.DataSource = ds;
            gvProducts.DataBind();

            #region Export To Excel
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ItemCode"));
                dt.Columns.Add(new DataColumn("Productname"));
                dt.Columns.Add(new DataColumn("Model"));
                dt.Columns.Add(new DataColumn("ProductDesc"));
                dt.Columns.Add(new DataColumn("Stock"));
                dt.Columns.Add(new DataColumn("Unit"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["ItemCode"] = dr["ItemCode"];
                    dr_export["Productname"] = dr["Productname"];
                    dr_export["Model"] = dr["Model"];
                    dr_export["ProductDesc"] = dr["ProductDesc"];
                    dr_export["Stock"] = dr["Stock"];
                    dr_export["Unit"] = dr["Unit"];
                    dt.Rows.Add(dr_export);
                }

                DataRow dr_lastexport = dt.NewRow();
                dr_lastexport["ItemCode"] = "";
                dr_lastexport["Productname"] = "";
                dr_lastexport["Model"] = "";
                dr_lastexport["ProductDesc"] = "";
                dr_lastexport["Stock"] = "";
                dr_lastexport["Unit"] = "";
                ExportToExcel("ReOrderLevelReport.xls", dt);
            }
            #endregion
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel(string filename, DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
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
}
