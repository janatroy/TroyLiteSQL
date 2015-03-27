using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class CSTSumaryReport : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();


                txtStartDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();

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

                                lblCompany.Text = Convert.ToString(dr["CompanyName"]);



                            }
                        }
                    }
                }
                loadBranch();
                BranchEnable_Disable();
             

                dvCST.Visible = false;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }


    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            /*March 17*/
            DateTime startDate, endDate;
            startDate = Convert.ToDateTime(txtStartDate.Text);
            endDate = Convert.ToDateTime(txtEndDate.Text);
            /*March 17*/
            //GetCSTReport(startDate, endDate);
            dvCST.Visible = false;

            string branch = drpBranch.SelectedValue;

            Response.Write("<script language='javascript'> window.open('CSTSumaryReport1.aspx?startDate=" + Convert.ToDateTime(startDate) + "&endDate=" + Convert.ToDateTime(endDate) + "&Branch=" + branch + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void GetCSTReport(DateTime startDate, DateTime endDate)
    {

        if (Request.Cookies["Company"]  != null)
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        string branch = drpBranch.SelectedItem.Value;

        DataSet purchaseDs = new DataSet();
        DataSet salesDs = new DataSet();
        DataSet purchaseReturnDs = new DataSet();
        DataSet SalesReturnDs = new DataSet();

        string strPurchaseCST = string.Empty;
        string strSalesCST = string.Empty;

        double purchaseActual = 0.0;
        double salesActual = 0.0;
        double purchaseReturnActual = 0.0;
        double salesReturnActual = 0.0;
        double sumPurchaseActual = 0.0;
        double sumSalesActual = 0.0;
        double purchaseCST = 0.0;
        double salesCST = 0.0;
        double purchaseReturnCST = 0.0;
        double salesReturnCST = 0.0;
        double sumPurchaseCST = 0.0;
        double sumSalesCST = 0.0;
        double netPurchaseCST = 0.0;
        double netSalesCST = 0.0;
        double netPurchaseActual = 0.0;
        double netSalesActual = 0.0;
        double CST = 0;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet CSTDs = bl.avlCST("purchase", sDataSource);

        lblFromDate.Text = startDate.ToShortDateString();
        lblToDate.Text = endDate.ToShortDateString();

        if (CSTDs != null)
        {
            if (CSTDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in CSTDs.Tables[0].Rows)
                {
                    if (dr["CST"] != null && dr["CST"] != DBNull.Value)
                    {
                        CST = Convert.ToDouble(Convert.ToDouble(dr["CST"]).ToString("f2"));
                    }
                    if (CST > 0)
                    {
                        purchaseDs = bl.purchaseCSTSummary(startDate, endDate, CST, "No", branch);
                        purchaseReturnDs = bl.salesCSTSummary(startDate, endDate, CST, "Yes", branch);

                        if (purchaseDs != null)
                        {
                            if (purchaseDs.Tables[0] != null)
                            {
                                if (purchaseDs.Tables[0].Rows.Count > 0)
                                {
                                    if (purchaseDs.Tables[0].Rows[0]["CSTPaid"] != null && purchaseDs.Tables[0].Rows[0]["CSTPaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(purchaseDs.Tables[0].Rows[0]["CSTPaid"]) != string.Empty)
                                            purchaseCST = Convert.ToDouble(purchaseDs.Tables[0].Rows[0]["CSTPaid"]);
                                    }
                                    if (purchaseDs.Tables[0].Rows[0]["Actualpaid"] != null && purchaseDs.Tables[0].Rows[0]["Actualpaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(purchaseDs.Tables[0].Rows[0]["Actualpaid"]) != string.Empty)
                                            purchaseActual = Convert.ToDouble(purchaseDs.Tables[0].Rows[0]["Actualpaid"]);
                                    }
                                }
                            }
                        }
                        if (purchaseReturnDs != null)
                        {
                            if (purchaseReturnDs.Tables[0] != null)
                            {
                                if (purchaseReturnDs.Tables[0].Rows.Count > 0)
                                {
                                    if (purchaseReturnDs.Tables[0].Rows[0]["CSTPaid"] != null && purchaseReturnDs.Tables[0].Rows[0]["CSTPaid"] != DBNull.Value)
                                    {

                                        purchaseReturnCST = Convert.ToDouble(purchaseReturnDs.Tables[0].Rows[0]["CSTPaid"]);
                                    }
                                    if (purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"] != null && purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"]) != string.Empty)
                                            purchaseReturnActual = Convert.ToDouble(purchaseReturnDs.Tables[0].Rows[0]["Actualpaid"]);
                                    }
                                }
                            }
                        }

                        sumPurchaseCST = purchaseCST - purchaseReturnCST;
                        sumPurchaseActual = purchaseActual - purchaseReturnActual;
                        if (sumPurchaseCST > 0 && sumPurchaseActual > 0)
                            strPurchaseCST = strPurchaseCST + "Input CST @" + CST.ToString("f2") + "%~" + sumPurchaseActual.ToString("f2") + "~" + sumPurchaseCST.ToString("f2") + "^";

                        netPurchaseCST = netPurchaseCST + sumPurchaseCST;
                        netPurchaseActual = netPurchaseActual + sumPurchaseActual;





                        purchaseCST = 0;
                        purchaseActual = 0;
                        purchaseReturnActual = 0;
                        purchaseReturnCST = 0;

                    }

                }
            }
        }
        CSTDs = bl.avlCST("sales", sDataSource);
        if (CSTDs != null)
        {
            if (CSTDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in CSTDs.Tables[0].Rows)
                {

                    if (dr["CST"] != null && dr["CST"] != DBNull.Value)
                    {
                        CST = Convert.ToDouble(Convert.ToDouble(dr["CST"]).ToString("f2"));
                    }
                    if (CST > 0)
                    {

                        SalesReturnDs = bl.purchaseCSTSummary(startDate, endDate, CST, "Yes", branch);
                        salesDs = bl.salesCSTSummary(startDate, endDate, CST, "No", branch);



                        if (salesDs != null)
                        {
                            if (salesDs.Tables[0] != null)
                            {
                                if (salesDs.Tables[0].Rows.Count > 0)
                                {
                                    if (salesDs.Tables[0].Rows[0]["CSTPaid"] != null && salesDs.Tables[0].Rows[0]["CSTPaid"] != DBNull.Value)
                                    {
                                        salesCST = Convert.ToDouble(salesDs.Tables[0].Rows[0]["CSTPaid"]);
                                    }
                                    if (salesDs.Tables[0].Rows[0]["Actualpaid"] != null && salesDs.Tables[0].Rows[0]["Actualpaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(salesDs.Tables[0].Rows[0]["Actualpaid"]) != string.Empty)
                                            salesActual = Convert.ToDouble(salesDs.Tables[0].Rows[0]["Actualpaid"]);
                                    }
                                }
                            }
                        }
                        if (SalesReturnDs != null)
                        {
                            if (SalesReturnDs.Tables[0] != null)
                            {
                                if (SalesReturnDs.Tables[0].Rows.Count > 0)
                                {
                                    if (SalesReturnDs.Tables[0].Rows[0]["CSTPaid"] != null && SalesReturnDs.Tables[0].Rows[0]["CSTPaid"] != DBNull.Value)
                                    {
                                        salesReturnCST = Convert.ToDouble(SalesReturnDs.Tables[0].Rows[0]["CSTPaid"]);
                                    }
                                    if (SalesReturnDs.Tables[0].Rows[0]["Actualpaid"] != null && SalesReturnDs.Tables[0].Rows[0]["Actualpaid"] != DBNull.Value)
                                    {
                                        if (Convert.ToString(SalesReturnDs.Tables[0].Rows[0]["Actualpaid"]) != string.Empty)
                                            salesReturnActual = Convert.ToDouble(SalesReturnDs.Tables[0].Rows[0]["Actualpaid"]);
                                    }
                                }
                            }
                        }

                        sumSalesCST = salesCST - salesReturnCST;
                        sumSalesActual = salesActual - salesReturnActual;
                        if (sumSalesCST > 0 && sumSalesActual > 0)
                            strSalesCST = strSalesCST + "Output CST @" + CST.ToString("f2") + "%~" + sumSalesActual.ToString("f2") + "~" + sumSalesCST.ToString("f2") + "^";
                        netSalesCST = netSalesCST + sumSalesCST;
                        netSalesActual = netSalesActual + sumSalesActual;

                        salesActual = 0;
                        salesReturnActual = 0;
                        salesCST = 0;
                        salesReturnCST = 0;
                    }

                }
            }
        }

        lblActualPurchase.Text = netPurchaseActual.ToString("f2");
        lblActualSales.Text = netSalesActual.ToString("f2");
        lblCSTPurchase.Text = netPurchaseCST.ToString("f2");
        lblCSTSales.Text = netSalesCST.ToString("f2");
        lblCSTPayable.Text = (Convert.ToDouble(lblCSTSales.Text) - Convert.ToDouble(lblCSTPurchase.Text)).ToString();
        //lblCSTPayable.Text = (netSalesCST - netPurchaseCST).ToString("f2");
        if (strPurchaseCST.EndsWith("^"))
        {
            strPurchaseCST = strPurchaseCST.Remove(strPurchaseCST.Length - 1, 1);
        }
        if (strSalesCST.EndsWith("^"))
        {
            strSalesCST = strSalesCST.Remove(strSalesCST.Length - 1, 1);
        }

        if (strSalesCST != "")
            GenerateSalesCST(strSalesCST);
        if (strPurchaseCST != "")
            GeneratePurchaceCST(strPurchaseCST);      
    }
    public void GenerateSalesCST(string CSTStr)
    {
        DataSet grdDs = new DataSet();
        DataTable grdDt = new DataTable();
        DataTable dtNew = new DataTable();
        dtNew = GenerateDs();
        grdDs.Tables.Add(dtNew);
        string[] mainRec = CSTStr.Split('^');
        for (int k = 0; k <= mainRec.Length - 1; k++)
        {
            string[] childRec = mainRec[k].Split('~');
            grdDt = GenerateDs(childRec[0].ToString(), childRec[1].ToString(), childRec[2].ToString());
            if (grdDt != null)
            {
                for (int m = 0; m <= grdDt.Rows.Count - 1; m++)
                {

                    if (grdDt != null && grdDt.Rows.Count > 0)
                        grdDs.Tables[0].ImportRow(grdDt.Rows[m]);
                }
            }
        }
        grdSalesCst.DataSource = grdDs;
        grdSalesCst.DataBind();
      
    }
    public void GeneratePurchaceCST(string CSTStr)
    {
        DataSet grdDs = new DataSet();
        DataTable grdDt = new DataTable();
        DataTable dtNew = new DataTable();
        dtNew = GenerateDs();
        grdDs.Tables.Add(dtNew);
        string[] mainRec = CSTStr.Split('^');
        for (int k = 0; k <= mainRec.Length - 1; k++)
        {
            string[] childRec = mainRec[k].Split('~');
            grdDt = GenerateDs(childRec[0].ToString(), childRec[1].ToString(), childRec[2].ToString());
            if (grdDt != null)
            {
                for (int m = 0; m <= grdDt.Rows.Count - 1; m++)
                {

                    if (grdDt != null && grdDt.Rows.Count > 0)
                        grdDs.Tables[0].ImportRow(grdDt.Rows[m]);
                }
            }
        }
        grdPurchaseCST.DataSource = grdDs;
        grdPurchaseCST.DataBind();
    }
    public DataTable GenerateDs()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;


        dc = new DataColumn("OutputTax");
        dt.Columns.Add(dc);

        dc = new DataColumn("Actual");
        dt.Columns.Add(dc);

        dc = new DataColumn("CST");
        dt.Columns.Add(dc);



        dr = dt.NewRow();
        dr["OutputTax"] = "";

        dr["Actual"] = "";
        dr["CST"] = "";

        dt.Rows.Add(dr);

        return dt;
    }
    public DataTable GenerateDs(string Outout, string Actual, string CST)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;
        int days = 0;
        dc = new DataColumn("OutputTax");
        dt.Columns.Add(dc);

        dc = new DataColumn("Actual");
        dt.Columns.Add(dc);

        dc = new DataColumn("CST");
        dt.Columns.Add(dc);  //ds.Tables.Add(dt);


        dr = dt.NewRow();
        dr["OutputTax"] = Outout;

        dr["Actual"] = Actual;
        dr["CST"] = CST;
        dt.Rows.Add(dr);


        return dt;
    }
    protected void btnCSTDetailedReport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("CSTReprt.aspx", true);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void BranchEnable_Disable()
    {
        string sCustomer = string.Empty;
        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);

        sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["DefaultBranchCode"]);
        drpBranch.ClearSelection();
        ListItem li = drpBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        if (li != null) li.Selected = true;

        if (dsd.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
        {
            drpBranch.Enabled = true;
        }
        else
        {
            drpBranch.Enabled = false;
        }
        // UpdatePanel4.Update();
    }

    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        drpBranch.Items.Clear();
        drpBranch.Items.Add(new ListItem("ALL", "0"));
        ds = bl.ListBranch();
        drpBranch.DataSource = ds;
        drpBranch.DataBind();
        drpBranch.DataTextField = "BranchName";
        drpBranch.DataValueField = "Branchcode";
        //UpdatePanel4.Update();
    }
    protected void btndetails_Click(object sender, EventArgs e)
    {
        DateTime startDate, endDate;
        startDate = Convert.ToDateTime(txtStartDate.Text);
        endDate = Convert.ToDateTime(txtEndDate.Text);
        GetCSTReport(startDate, endDate);
        dvCST.Visible = true;
        ExportToExcel();
    }


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

            //TableCell cell2 = new TableCell();

            //cell2.Controls.Add(grdSalesCst.FindControl("grdSalesCst"));

            TableCell cell3 = new TableCell();

            cell3.Text = "&nbsp;";

            TableCell cell4 = new TableCell();

            cell4.Text = "Asset Balance";

            TableCell cell5 = new TableCell();

            cell5.Controls.Add(grdPurchaseCST.FindControl("grdPurchaseCST"));

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

            //TableRow tr2 = new TableRow();

            //tr2.Cells.Add(cell2);

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

            //tb.Rows.Add(tr2);

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
}
