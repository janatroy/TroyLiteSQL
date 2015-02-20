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

public partial class JobManagementReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                            lblBillDate.Text = DateTime.Now.ToShortDateString();

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

    public void ExportToExcel()
    {
        try
        {
            Response.Clear();

            Response.Buffer = true;



            Response.AddHeader("content-disposition",

             "attachment;filename=JobManagementReport.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "Job Management Report";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(gvJob);



            tr1.Cells.Add(cell1);

            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);



            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

            tb.RenderControl(hw);

            string style = @"<style> .textmode { mso-number-format:\@; } </style>";

            Response.Write(style);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();

            //ExportToExcel(ds);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpIncharge_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            drpJob.ClearSelection();
            drpJob.Items.Clear();
            drpJob.Items.Add(new ListItem("ALL", "0"));
            int empno = Convert.ToInt32(drpIncharge.SelectedItem.Value);
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();
            ds = bl.ListJobAssignee(empno);
            drpJob.DataSource = ds;
            drpJob.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string strStatus = string.Empty;
                if (chkComp.Checked && chkPending.Checked)
                {
                    strStatus = "ALL";
                }
                else if (!chkComp.Checked && chkPending.Checked)
                {
                    strStatus = "N";
                }
                else if (chkComp.Checked && !chkPending.Checked)
                {
                    strStatus = "Y";
                }
                else
                {
                    strStatus = "ALL";
                }
                int empno = Convert.ToInt32(drpIncharge.SelectedItem.Value);
                int jobID = Convert.ToInt32(drpJob.SelectedItem.Value);

                BusinessLogic bl = new BusinessLogic(sDataSource);
                DataSet ds = new DataSet();
                if (jobID == 0 && empno == 0 && strStatus == "ALL")
                    ds = bl.ListJobDetails("0");
                else
                    ds = bl.ListJobDetails(jobID, empno, strStatus);
                gvJob.DataSource = ds;
                gvJob.DataBind();
                divPrint.Visible = true;

                ExportToExcel();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void gvJob_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string hdAss = ((HiddenField)e.Row.FindControl("hdass")).Value;
                Label Assign = (Label)e.Row.FindControl("lblAssign");
                BusinessLogic bl = new BusinessLogic(sDataSource);
                Assign.Text = bl.ListExecutive(Convert.ToInt32(hdAss));
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
