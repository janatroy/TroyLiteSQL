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

public partial class Trialbalance : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    public double debitTotal = 0;
    public double creditTotal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();

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
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btndetails_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable grdDt = new DataTable();
            DataSet grdDs = new DataSet();
            DataTable dtNew = new DataTable();
            int groupID = 0;
            double debitSum = 0.0d;
            double creditSum = 0.0d;
            double totalSum = 0.0d;
            string strParticulars = string.Empty;
            string strDebit = string.Empty;
            string strCredit = string.Empty;
            string sGroupName = string.Empty;
            dtNew = GenerateDs("", "", "", "", "");
            grdDs.Tables.Add(dtNew);
            /*March 17*/
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            /*March 17*/
            string TrailFlag = string.Empty;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet mainDs = bl.GetTrailGroups();
            if (mainDs != null)
            {
                if (mainDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow mainRow in mainDs.Tables[0].Rows)
                    {
                        groupID = Convert.ToInt32(mainRow["GroupID"]);
                        sGroupName = mainRow["GroupName"].ToString();
                        debitSum = bl.GetDebitSum(groupID, startDate, endDate);
                        creditSum = bl.GetCreditSum(groupID, startDate, endDate);
                        TrailFlag = Convert.ToString(mainRow["TrailBalance"]);
                        strParticulars = Convert.ToString(mainRow["GroupName"]);
                        if (TrailFlag == "Debit")
                        {
                            totalSum = debitSum - creditSum;
                            strDebit = Convert.ToString(totalSum.ToString("f2"));
                            strCredit = "0";

                            if (totalSum < 0)
                            {
                                strCredit = Convert.ToString(Math.Abs(totalSum));
                                strDebit = "0";
                            }
                        }
                        else
                        {
                            totalSum = creditSum - debitSum;
                            strCredit = Convert.ToString(totalSum.ToString("f2"));
                            strDebit = "0";
                            if (totalSum < 0)
                            {
                                strCredit = "0";
                                strDebit = Convert.ToString(Math.Abs(totalSum));
                            }
                        }

                        grdDt = GenerateDs(sGroupName, strParticulars, strDebit, strCredit, groupID.ToString());
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
            }
            grdDs.Tables[0].Rows[0].Delete();
            gvTrailBalance.DataSource = grdDs;
            gvTrailBalance.DataBind();


            double credit = 0;
            double debit = 0;

            if (grdDs.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Particulars"));
                dt.Columns.Add(new DataColumn("L.FNO"));
                dt.Columns.Add(new DataColumn("Ledger Name"));
                dt.Columns.Add(new DataColumn("Debit"));
                dt.Columns.Add(new DataColumn("Credit"));

                DataRow dr_export1 = dt.NewRow();
                dt.Rows.Add(dr_export1);

                if (grdDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in grdDs.Tables[0].Rows)
                    {
                        int groupIDD = Convert.ToInt32(dr["Groupid"]);

                        DataSet ds = bl.getLedgerTransaction(groupIDD, sDataSource, startDate, endDate);
                        int ii = 1;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drd in ds.Tables[0].Rows)
                            {
                                DataRow dr_export = dt.NewRow();
                                //if (ii == 1)
                                //{
                                dr_export["Particulars"] = dr["Particulars"];
                                //}
                                //else
                                //{
                                //    dr_export["Particulars"] = "";
                                //}
                                dr_export["L.FNO"] = drd["Folionumber"];
                                dr_export["Ledger Name"] = drd["LedgerName"];
                                dr_export["Debit"] = drd["Debit"];
                                //debit = debit + Convert.ToDouble(drd["Debit"]);
                                dr_export["Credit"] = drd["Credit"];
                                //credit = credit + Convert.ToDouble(drd["Credit"]);
                                dt.Rows.Add(dr_export);
                            }

                            DataRow dr_export2 = dt.NewRow();
                            dt.Rows.Add(dr_export2);

                            DataRow dr_export213 = dt.NewRow();
                            dr_export213["Particulars"] = " Total : " + dr["Particulars"];
                            dr_export213["Debit"] = dr["Debit"];
                            dr_export213["Credit"] = dr["Credit"];
                            dt.Rows.Add(dr_export213);

                            DataRow dr_export231 = dt.NewRow();
                            dt.Rows.Add(dr_export231);

                            debit = 0;
                            credit = 0;
                        }
                    }
                }
                ExportToExcel("Trial Balance.xls", dt);
            }
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

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime startDate, endDate;
            //showTrialBalance();
            dvTrail.Visible = false;
            Div1.Visible = true;
            //ExportToExcel();
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            Response.Write("<script language='javascript'> window.open('Trialbalance1.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btndet_Click(object sender, EventArgs e)
    {
        try
        {
            dvTrail.Visible = false;
            Div1.Visible = true;
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

             "attachment;filename=TrailBalance.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "Trail Balance";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(dvTrail.FindControl("divPrint").FindControl("gvTrailBalance"));



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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
        }
    }


    public void showTrialBalance()
    {
        DataTable grdDt = new DataTable();
        DataSet grdDs = new DataSet();
        DataTable dtNew = new DataTable();
        int groupID = 0;
        double debitSum = 0.0d;
        double creditSum = 0.0d;
        double totalSum = 0.0d;
        string strParticulars = string.Empty;
        string strDebit = string.Empty;
        string strCredit = string.Empty;
        string sGroupName = string.Empty;
        dtNew = GenerateDs("", "", "", "", "");
        grdDs.Tables.Add(dtNew);
        /*March 17*/
        DateTime startDate, endDate;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        /*March 17*/
        string TrailFlag = string.Empty;
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet mainDs = bl.GetTrailGroups();
        if (mainDs != null)
        {
            if (mainDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow mainRow in mainDs.Tables[0].Rows)
                {
                    groupID = Convert.ToInt32(mainRow["GroupID"]);
                    sGroupName = mainRow["GroupName"].ToString();
                    debitSum = bl.GetDebitSum(groupID, startDate, endDate);
                    creditSum = bl.GetCreditSum(groupID, startDate, endDate);
                    TrailFlag = Convert.ToString(mainRow["TrailBalance"]);
                    strParticulars = Convert.ToString(mainRow["GroupName"]);
                    if (TrailFlag == "Debit")
                    {
                        totalSum = debitSum - creditSum;
                        strDebit = Convert.ToString(totalSum.ToString("f2"));
                        strCredit = "0";

                        if (totalSum < 0)
                        {
                            strCredit = Convert.ToString(Math.Abs(totalSum));
                            strDebit = "0";
                        }
                    }
                    else
                    {
                        totalSum = creditSum - debitSum;
                        strCredit = Convert.ToString(totalSum.ToString("f2"));
                        strDebit = "0";
                        if (totalSum < 0)
                        {
                            strCredit = "0";
                            strDebit = Convert.ToString(Math.Abs(totalSum));
                        }
                    }

                    grdDt = GenerateDs(sGroupName, strParticulars, strDebit, strCredit, groupID.ToString());
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
        grdDs.Tables[0].Rows[0].Delete();
        gvTrailBalance.DataSource = grdDs;
        gvTrailBalance.DataBind();
    }
    public DataTable GenerateDs(string GroupName, string strParticulars, string strDebit, string strCredit, string iGroupID)
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

        dc = new DataColumn("GroupID");
        dt.Columns.Add(dc);
        dc = new DataColumn("GroupName");
        dt.Columns.Add(dc);
        //ds.Tables.Add(dt);
        dr = dt.NewRow();
        dr["Particulars"] = strParticulars;

        dr["Debit"] = strDebit;
        //dr["TransDate"] = "";
        dr["Credit"] = strCredit;

        dr["GroupID"] = iGroupID;
        dr["GroupName"] = GroupName;
        dt.Rows.Add(dr);
        //ds.Tables[0].Rows.Add(dr);
        return dt;
    }

    public void gvTrailBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            /*March 17*/
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            /*March 17*/

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDebit = (Label)e.Row.FindControl("lblDebit");
                Label lblCredit = (Label)e.Row.FindControl("lblCredit");
                if (lblDebit != null && lblDebit.Text != "")
                    debitTotal = debitTotal + Convert.ToDouble(lblDebit.Text);
                if (lblCredit != null && lblCredit.Text != "")
                    creditTotal = creditTotal + Convert.ToDouble(lblCredit.Text);
            }
            lblDebitTotal.Text = debitTotal.ToString("f2");
            lblCreditTotal.Text = creditTotal.ToString("f2");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='ALiceblue';");
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='khaki';");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvLiaLedger") as GridView;
                GridView gvGroup = (GridView)sender;
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);
                    ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
                    DataSet ds = rpt.getLedgerTransaction(groupID, sDataSource, startDate, endDate);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;
                        gv.DataBind();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
