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

public partial class SalesPurCompReport : System.Web.UI.Page
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
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

                    //if (companyInfo != null)
                    //{
                    //    if (companyInfo.Tables[0].Rows.Count > 0)
                    //    {
                    //        foreach (DataRow dr in companyInfo.Tables[0].Rows)
                    //        {
                    //            lblTNGST.Text = Convert.ToString(dr["TINno"]);
                    //            lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                    //            lblPhone.Text = Convert.ToString(dr["Phone"]);
                    //            lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                    //            lblAddress.Text = Convert.ToString(dr["Address"]);
                    //            lblCity.Text = Convert.ToString(dr["city"]);
                    //            lblPincode.Text = Convert.ToString(dr["Pincode"]);
                    //            lblState.Text = Convert.ToString(dr["state"]);
                    //            lblBillDate.Text = DateTime.Now.ToShortDateString();
                    //        }
                    //    }
                    //}
                }

                //double sumTotal = 0;
                //double dTot = 0;
                //double assTot = 0;
                //double liaTot = 0;
                ///* March17 */
                //DataTable dt = new DataTable();

                //DateTime startDate, endDate;
                ////startDate = Convert.ToDateTime(txtStartDate.Text);
                ////endDate = Convert.ToDateTime(txtEndDate.Text);
                ///* March17 */

                //startDate = Convert.ToDateTime(Request.QueryString["SID"].ToString());
                //endDate = Convert.ToDateTime(Request.QueryString["RT"].ToString());

                ////lblStartDate.Text = txtStartDate.Text;
                ////lblEndDate.Text = txtEndDate.Text;
                //sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                //DataSet assetDs = GetBalanceSheet("Asset");
                //DataSet liabilityDs = GetBalanceSheet("Liability");

                //if (assetDs != null)
                //{
                //    if (assetDs.Tables[0].Rows.Count > 0)
                //    {
                //        assetDs.Tables[0].Rows[0].Delete();
                //        gvAssetBalance.DataSource = assetDs;
                //        gvAssetBalance.DataBind();
                //        foreach (DataRow sumDr in assetDs.Tables[0].Rows)
                //        {
                //            assTot = assTot + Convert.ToDouble(sumDr["sum"]);
                //        }
                //    }
                //}
                //if (liabilityDs != null)
                //{
                //    if (liabilityDs.Tables[0].Rows.Count > 0)
                //    {
                //        liabilityDs.Tables[0].Rows[0].Delete();
                //        gvLiabilityBalance.DataSource = liabilityDs;
                //        gvLiabilityBalance.DataBind();
                //        foreach (DataRow sumCr in liabilityDs.Tables[0].Rows)
                //        {
                //            liaTot = liaTot + Convert.ToDouble(sumCr["sum"]);
                //        }
                //    }
                //}

                //dt = assetDs.Tables[0];
                //ExportToExcel("", dt);


                //if (Page.IsValid)
                //{
                double sumTotal = 0;
                double dTot = 0;
                double assTot = 0;
                double liaTot = 0;
                /* March17 */
                DateTime startDate, endDate;

                startDate = Convert.ToDateTime(Request.QueryString["SID"].ToString());
                endDate = Convert.ToDateTime(Request.QueryString["RT"].ToString());
                /* March17 */

                //lblStartDate.Text = txtStartDate.Text;
                //lblEndDate.Text = txtEndDate.Text;
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                dvBalanceSheet.Visible = true;
                DataSet assetDs = GetBalanceSheet("Asset");
                DataSet liabilityDs = GetBalanceSheet("Liability");

                if (assetDs != null)
                {
                    if (assetDs.Tables[0].Rows.Count > 0)
                    {
                        assetDs.Tables[0].Rows[0].Delete();
                        gvAssetBalance.DataSource = assetDs;
                        gvAssetBalance.DataBind();
                        foreach (DataRow sumDr in assetDs.Tables[0].Rows)
                        {
                            assTot = assTot + Convert.ToDouble(sumDr["sum"]);
                        }
                    }
                }
                if (liabilityDs != null)
                {
                    if (liabilityDs.Tables[0].Rows.Count > 0)
                    {
                        liabilityDs.Tables[0].Rows[0].Delete();
                        gvLiabilityBalance.DataSource = liabilityDs;
                        gvLiabilityBalance.DataBind();
                        foreach (DataRow sumCr in liabilityDs.Tables[0].Rows)
                        {
                            liaTot = liaTot + Convert.ToDouble(sumCr["sum"]);
                        }
                    }
                }
                //Difference in Opening Balance Calculation


                sumTotal = assTot - liaTot;
                if (sumTotal > 0)
                {
                    pnlLib.Visible = true;
                    pnlAst.Visible = false;
                    sumTotal = Math.Abs(sumTotal);
                    lblLib.Text = sumTotal.ToString("f2");
                    dTot = liaTot + sumTotal;
                    lblCreditTotal.Text = dTot.ToString("f2");
                    lblDebitTotal.Text = assTot.ToString("f2");
                }
                else
                {
                    pnlLib.Visible = false;
                    pnlAst.Visible = true;
                    sumTotal = Math.Abs(sumTotal);
                    lblAst.Text = sumTotal.ToString("f2");
                    dTot = assTot + sumTotal;
                    lblDebitTotal.Text = dTot.ToString("f2");
                    lblCreditTotal.Text = liaTot.ToString("f2");
                }




                //}

                ExportToExcel();
                //txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                //txtEndDate.Text = DateTime.Now.ToShortDateString();


            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    /*March 17*/

    public void ExportToExcel()
    {

        try
        {
            Response.Clear();

            Response.Buffer = true;



            Response.AddHeader("content-disposition",

             "attachment;filename=GridViewExport.xls");

            Response.Charset = "";

            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(sw);


            Table tb = new Table();

            TableRow tr1 = new TableRow();

            TableCell cell1 = new TableCell();

            cell1.Text = "Liability Balance";

            TableCell cell2 = new TableCell();

            cell2.Controls.Add(gvLiabilityBalance.FindControl("gvLiaGroup").FindControl("gvLiaLedger"));

            TableCell cell3 = new TableCell();

            cell3.Text = "&nbsp;";

            TableCell cell4 = new TableCell();

            cell4.Text = "Asset Balance";

            TableCell cell5 = new TableCell();

            cell5.Controls.Add(gvAssetBalance.FindControl("gvAssetGroup").FindControl("gvAssetLedger"));

            TableCell cell6 = new TableCell();

            cell6.Text = "&nbsp;";

            ////TableCell cell7 = new TableCell();

            ////cell7.Text = "Missing Ledger in Daybook (Debit)";

            ////TableCell cell8 = new TableCell();

            ////cell8.Controls.Add(ReportGridView2);

            ////TableCell cell9 = new TableCell();

            ////cell9.Text = "&nbsp;";

            ////TableCell cell10 = new TableCell();

            ////cell10.Text = "Missing Ledger in sales";


            tr1.Cells.Add(cell1);

            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);

            TableRow tr3 = new TableRow();

            tr3.Cells.Add(cell3);

            TableRow tr4 = new TableRow();

            tr4.Cells.Add(cell4);

            TableRow tr5 = new TableRow();

            tr5.Cells.Add(cell5);

            TableRow tr6 = new TableRow();

            tr6.Cells.Add(cell6);

            //TableRow tr7 = new TableRow();

            //tr7.Cells.Add(cell7);


            //TableRow tr8 = new TableRow();

            //tr8.Cells.Add(cell8);

            //TableRow tr9 = new TableRow();

            //tr9.Cells.Add(cell9);

            //TableRow tr10 = new TableRow();

            //tr10.Cells.Add(cell10);


            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

            tb.Rows.Add(tr3);

            tb.Rows.Add(tr4);

            tb.Rows.Add(tr5);

            tb.Rows.Add(tr6);

            //tb.Rows.Add(tr7);

            //tb.Rows.Add(tr8);

            //tb.Rows.Add(tr9);

            //tb.Rows.Add(tr10);


            tb.RenderControl(hw);



            //style to format numbers to string

            string style = @"<style> .textmode { mso-number-format:\@; } </style>";

            Response.Write(style);

            Response.Output.Write(sw.ToString());

            Response.Flush();

            Response.End();

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
            double sumTotal = 0;
            double dTot = 0;
            double assTot = 0;
            double liaTot = 0;
            /* March17 */
            DataTable dt = new DataTable();

            DateTime startDate, endDate;
            //startDate = Convert.ToDateTime(txtStartDate.Text);
            //endDate = Convert.ToDateTime(txtEndDate.Text);
            /* March17 */

            //lblStartDate.Text = txtStartDate.Text;
            //lblEndDate.Text = txtEndDate.Text;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            DataSet assetDs = GetBalanceSheet("Asset");
            DataSet liabilityDs = GetBalanceSheet("Liability");

            if (assetDs != null)
            {
                if (assetDs.Tables[0].Rows.Count > 0)
                {
                    assetDs.Tables[0].Rows[0].Delete();
                    //gvAssetBalance.DataSource = assetDs;
                    //gvAssetBalance.DataBind();
                    foreach (DataRow sumDr in assetDs.Tables[0].Rows)
                    {
                        assTot = assTot + Convert.ToDouble(sumDr["sum"]);
                    }
                }
            }
            if (liabilityDs != null)
            {
                if (liabilityDs.Tables[0].Rows.Count > 0)
                {
                    liabilityDs.Tables[0].Rows[0].Delete();
                    //gvLiabilityBalance.DataSource = liabilityDs;
                    //gvLiabilityBalance.DataBind();
                    foreach (DataRow sumCr in liabilityDs.Tables[0].Rows)
                    {
                        liaTot = liaTot + Convert.ToDouble(sumCr["sum"]);
                    }
                }
            }

            dt = assetDs.Tables[0];
            //ExportToExcel("", dt);

            ExportToExcel();
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
            if (Page.IsValid)
            {
                double sumTotal = 0;
                double dTot = 0;
                double assTot = 0;
                double liaTot = 0;
                /* March17 */
                DateTime startDate, endDate;

                startDate = Convert.ToDateTime(Request.QueryString["SID"].ToString());
                endDate = Convert.ToDateTime(Request.QueryString["RT"].ToString());
                /* March17 */

                //lblStartDate.Text = txtStartDate.Text;
                //lblEndDate.Text = txtEndDate.Text;
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                dvBalanceSheet.Visible = true;
                DataSet assetDs = GetBalanceSheet("Asset");
                DataSet liabilityDs = GetBalanceSheet("Liability");

                if (assetDs != null)
                {
                    if (assetDs.Tables[0].Rows.Count > 0)
                    {
                        assetDs.Tables[0].Rows[0].Delete();
                        gvAssetBalance.DataSource = assetDs;
                        gvAssetBalance.DataBind();
                        foreach (DataRow sumDr in assetDs.Tables[0].Rows)
                        {
                            assTot = assTot + Convert.ToDouble(sumDr["sum"]);
                        }
                    }
                }
                if (liabilityDs != null)
                {
                    if (liabilityDs.Tables[0].Rows.Count > 0)
                    {
                        liabilityDs.Tables[0].Rows[0].Delete();
                        gvLiabilityBalance.DataSource = liabilityDs;
                        gvLiabilityBalance.DataBind();
                        foreach (DataRow sumCr in liabilityDs.Tables[0].Rows)
                        {
                            liaTot = liaTot + Convert.ToDouble(sumCr["sum"]);
                        }
                    }
                }
                //Difference in Opening Balance Calculation


                sumTotal = assTot - liaTot;
                if (sumTotal > 0)
                {
                    pnlLib.Visible = true;
                    pnlAst.Visible = false;
                    sumTotal = Math.Abs(sumTotal);
                    lblLib.Text = sumTotal.ToString("f2");
                    dTot = liaTot + sumTotal;
                    lblCreditTotal.Text = dTot.ToString("f2");
                    lblDebitTotal.Text = assTot.ToString("f2");
                }
                else
                {
                    pnlLib.Visible = false;
                    pnlAst.Visible = true;
                    sumTotal = Math.Abs(sumTotal);
                    lblAst.Text = sumTotal.ToString("f2");
                    dTot = assTot + sumTotal;
                    lblDebitTotal.Text = dTot.ToString("f2");
                    lblCreditTotal.Text = liaTot.ToString("f2");
                }




            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    /*March 17 */
    public DataSet GetBalanceSheet(string btype)
    {

        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet mainDs = new DataSet();
        DataSet GroupDs = new DataSet();
        DataTable grdDt = new DataTable();
        DataTable dtNew = new DataTable();
        /*March 17*/
        DateTime startDate, endDate;
        startDate = Convert.ToDateTime(Request.QueryString["SID"].ToString());
        endDate = Convert.ToDateTime(Request.QueryString["RT"].ToString());
        /*March 17*/
        DataSet grdDs = new DataSet();

        Double debitSum = 0;
        Double creditSum = 0;
        Double totalSum = 0;
        Double netSum = 0;
        string sHeading = string.Empty;
        int iHeading = 0;
        int groupID = 0;

        dtNew = GenerateDs("", "", "");
        grdDs.Tables.Add(dtNew);

        mainDs = bl.GetBalanceSheetHeadings(btype);
        if (mainDs != null)
        {
            if (mainDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow mainRow in mainDs.Tables[0].Rows)
                {
                    netSum = 0;
                    totalSum = 0;
                    debitSum = 0;
                    creditSum = 0;
                    if (mainRow["Heading"] != null)
                    {
                        sHeading = Convert.ToString(mainRow["Heading"]);
                    }
                    if (mainRow["HeadingID"] != null)
                    {
                        iHeading = Convert.ToInt32(mainRow["HeadingID"]);
                        GroupDs = bl.GetGroupsForHeadiing(iHeading);
                        if (GroupDs != null)
                        {
                            if (GroupDs.Tables[0].Rows.Count > 0)
                            {


                                foreach (DataRow groupRow in GroupDs.Tables[0].Rows)
                                {
                                    if (groupRow["GroupID"] != null)
                                    {
                                        groupID = Convert.ToInt32(groupRow["GroupID"]);
                                        /*March 17 */
                                        debitSum = bl.GetDebitSum(groupID, startDate, endDate);
                                        creditSum = bl.GetCreditSum(groupID, startDate, endDate);
                                        /*March 17 */
                                        if (btype == "Asset")
                                        {
                                            totalSum = debitSum - creditSum;
                                        }
                                        else
                                        {
                                            totalSum = creditSum - debitSum;
                                        }

                                    }


                                    netSum = netSum + totalSum;

                                }// GroupDs ForEach


                            }//GrouopDs Tables Count
                        }//GroupDs null Check

                        grdDt = GenerateDs(iHeading.ToString(), sHeading, netSum.ToString("f2"));

                        if (grdDt != null)
                        {

                            for (int k = 0; k <= grdDt.Rows.Count - 1; k++)
                            {

                                if (grdDt != null && grdDt.Rows.Count > 0)
                                    grdDs.Tables[0].ImportRow(grdDt.Rows[k]);
                            }


                        }
                    }

                }// mainDs ForEach
            }//mainDs Tables Count
        } //mainDs null Check

        return grdDs;
    }


    public DataTable GenerateDs(string headingID, string HeadingName, string strSum)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;

        dc = new DataColumn("HeadingID");
        dt.Columns.Add(dc);

        dc = new DataColumn("HeadingName");
        dt.Columns.Add(dc);

        //dc = new DataColumn("TransDate");
        //dt.Columns.Add(dc);
        dc = new DataColumn("sum");
        dt.Columns.Add(dc);


        dr = dt.NewRow();
        dr["HeadingID"] = headingID;
        dr["HeadingName"] = HeadingName;

        dr["sum"] = strSum;
        //dr["TransDate"] = "";

        dt.Rows.Add(dr);
        //ds.Tables[0].Rows.Add(dr);
        return dt;
    }

    //gvAssetBalance_RowDataBound
    //gvLiabilityBalance_RowDataBound
    public void gvAssetBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSum = (Label)e.Row.FindControl("lblSum");

                if (lblSum != null && lblSum.Text != "")
                    debitTotal = debitTotal + Convert.ToDouble(lblSum.Text);


                lblDebitTotal.Text = debitTotal.ToString("f2");

                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='ALiceblue';");
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='khaki';");
                GridView gv = e.Row.FindControl("gvAssetGroup") as GridView;
                if (gvAssetBalance.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int headID = Convert.ToInt32(gvAssetBalance.DataKeys[e.Row.RowIndex].Value);
                    ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
                    DataSet ds = GetBalanceSheet2(headID);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ds.Tables[0].Rows[0].Delete();
                            gv.DataSource = ds;
                            gv.DataBind();
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

    public void gvLiabilityBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblSum = (Label)e.Row.FindControl("lblSum");

                if (lblSum != null && lblSum.Text != "")
                    creditTotal = creditTotal + Convert.ToDouble(lblSum.Text);


                lblCreditTotal.Text = creditTotal.ToString("f2");

                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='ALiceblue';");
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='khaki';");

                GridView gv = e.Row.FindControl("gvLiaGroup") as GridView;
                if (gvLiabilityBalance.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int headID = Convert.ToInt32(gvLiabilityBalance.DataKeys[e.Row.RowIndex].Value);
                    ReportsBL.ReportClass rpt = new ReportsBL.ReportClass();
                    DataSet ds = GetBalanceSheet2(headID);
                    if (ds != null)
                    {
                        ds.Tables[0].Rows[0].Delete();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gv.DataSource = ds;
                            gv.DataBind();
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
    public DataSet GetBalanceSheet2(int HeadingID)
    {

        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet mainDs = new DataSet();
        DataSet GroupDs = new DataSet();
        DataTable grdDt = new DataTable();
        DataTable dtNew = new DataTable();

        DataSet grdDs = new DataSet();

        Double debitSum = 0;
        Double creditSum = 0;
        Double totalSum = 0;
        Double netSum = 0;
        string sGroup = string.Empty;
        int iHeading = HeadingID;
        int groupID = 0;

        dtNew = GenerateDs2("", "", "");
        grdDs.Tables.Add(dtNew);

        /*March 17*/
        DateTime startDate, endDate;
        startDate = Convert.ToDateTime(Request.QueryString["SID"].ToString());
        endDate = Convert.ToDateTime(Request.QueryString["RT"].ToString());
        /*March 17*/

        GroupDs = bl.GetGroupsForHeadiing(iHeading);
        if (GroupDs != null)
        {
            if (GroupDs.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow groupRow in GroupDs.Tables[0].Rows)
                {
                    if (groupRow["GroupID"] != null)
                    {
                        groupID = Convert.ToInt32(groupRow["GroupID"]);
                        debitSum = bl.GetDebitSum(groupID, startDate, endDate);
                        creditSum = bl.GetCreditSum(groupID, startDate, endDate);

                        totalSum = debitSum - creditSum;


                    }
                    if (groupRow["GroupName"] != null)
                    {
                        sGroup = groupRow["GroupName"].ToString();
                    }

                    if (totalSum < 0)
                        totalSum = Math.Abs(totalSum);
                    netSum = netSum + totalSum;
                    grdDt = GenerateDs2(groupID.ToString(), sGroup, totalSum.ToString("f2"));
                    if (grdDt != null)
                    {
                        //grdDs.Tables.Add(grdDt);
                        for (int k = 0; k <= grdDt.Rows.Count - 1; k++)
                        {

                            if (grdDt != null && grdDt.Rows.Count > 0)
                                grdDs.Tables[0].ImportRow(grdDt.Rows[k]);
                        }


                    }
                    //grdDs.Tables[0].Rows[0].Delete();   

                }// GroupDs ForEach


            }//GroupDs Tables Count
        }//GroupDs null Check






        return grdDs;
    }


    public DataTable GenerateDs2(string groupID, string GroupName, string strSum)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;

        dc = new DataColumn("GroupID");
        dt.Columns.Add(dc);

        dc = new DataColumn("GroupName");
        dt.Columns.Add(dc);

        //dc = new DataColumn("TransDate");
        //dt.Columns.Add(dc);
        dc = new DataColumn("sum");
        dt.Columns.Add(dc);


        dr = dt.NewRow();
        dr["GroupID"] = groupID;
        dr["GroupName"] = GroupName;

        dr["sum"] = strSum;
        //dr["TransDate"] = "";

        dt.Rows.Add(dr);
        //ds.Tables[0].Rows.Add(dr);
        return dt;
    }

    //gvAssetBalance_RowDataBound
    //gvLiabilityBalance_RowDataBound
    public void gvLiaGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            /*March 17*/
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(Request.QueryString["SID"].ToString());
            endDate = Convert.ToDateTime(Request.QueryString["RT"].ToString());
            /*March 17*/
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSum = (Label)e.Row.FindControl("lblSum");

                if (lblSum != null && lblSum.Text != "")
                    debitTotal = debitTotal + Convert.ToDouble(lblSum.Text);

            }
            if (debitTotal > 0)
                lblDebitTotal.Text = debitTotal.ToString("f2");
            else
            {
                debitTotal = Math.Abs(debitTotal);
                lblDebitTotal.Text = debitTotal.ToString("f2");
            }
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='ALiceblue';");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='khaki';");
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

    public void gvAssetGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            /*March 17*/
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(Request.QueryString["SID"].ToString());
            endDate = Convert.ToDateTime(Request.QueryString["RT"].ToString());
            /*March 17*/
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSum = (Label)e.Row.FindControl("lblSum");

                if (lblSum != null && lblSum.Text != "")
                    debitTotal = debitTotal + Convert.ToDouble(lblSum.Text);


                if (debitTotal > 0)
                    lblDebitTotal.Text = debitTotal.ToString("f2");
                else
                {
                    debitTotal = Math.Abs(debitTotal);
                    lblDebitTotal.Text = debitTotal.ToString("f2");
                }
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='ALiceblue';");
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='khaki';");

                GridView gv = e.Row.FindControl("gvAssetLedger") as GridView;
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
