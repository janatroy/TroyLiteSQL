using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class StockReport : System.Web.UI.Page
{
    Double amtDbl = 0;
    Double sumDbl = 0;
    Double grandDbl = 0;
    private string sDataSource = string.Empty;
    private string connection = string.Empty;
    string brncode;
    string usernam;
    protected void Page_Load(object sender, EventArgs e)
    {

        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        connection = Request.Cookies["Company"].Value;
        try
        {
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
                loadBranch();
                BranchEnable_Disable();
                loadPriceList();
            }

            lblBillDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
            //string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
            //txtStartDate.Text = dtaa;

            lblHeadDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    private void loadBranch()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        lstBranch.Items.Clear();
        
        brncode = Request.Cookies["Branch"].Value;
        if (brncode == "All")
        {
            ds = bl.ListBranch();
            lstBranch.Items.Add(new ListItem("All", "0"));
        }
        else
        {
            ds = bl.ListDefaultBranch(brncode);
        }
        lstBranch.DataSource = ds;
        lstBranch.DataTextField = "BranchName";
        lstBranch.DataValueField = "Branchcode";
        lstBranch.DataBind();
    }

    private void BranchEnable_Disable()
    {
        string sCustomer = string.Empty;
        connection = Request.Cookies["Company"].Value;
        usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);

        sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["DefaultBranchCode"]);
        lstBranch.ClearSelection();
        ListItem li = lstBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        if (li != null) li.Selected = true;

    }

    private void loadPriceList()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        lstPricelist.Items.Clear();
        lstPricelist.Items.Add(new ListItem("All", "0"));
        ds = bl.ListPriceList(connection);
        lstPricelist.DataSource = ds;
        lstPricelist.DataTextField = "PriceName";
        lstPricelist.DataValueField = "PriceName";
        lstPricelist.DataBind();
    }


    protected string getCond()
    {
        string cond = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond += " BranchCode='" + listItem.Value + "' ,";
                }
            }
        }
        cond = cond.TrimEnd(',');
        cond = cond.Replace(",", "or");
        return cond;
    }

    protected string getCond1()
    {
        string cond1 = "";
        foreach (ListItem listItem1 in lstPricelist.Items)
        {
            if (listItem1.Text != "All")
            {
                if (listItem1.Selected)
                {
                    cond1 += "  tblPriceList.PriceName='" + listItem1.Value + "' ,";
                }
            }
        }
        cond1 = cond1.TrimEnd(',');
        cond1 = cond1.Replace(",", "or");
        return cond1;
    }

    protected string getCond2()
    {
        string cond2 = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond2 += " S.BranchCode='" + listItem.Value + "' ,";
                }
            }
        }
        cond2 = cond2.TrimEnd(',');
        cond2 = cond2.Replace(",", "or");
        return cond2;
    }

    protected string getCond3()
    {
        string cond3 = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond3 += " P.BranchCode='" + listItem.Value + "' ,";
                }
            }
        }
        cond3 = cond3.TrimEnd(',');
        cond3 = cond3.Replace(",", "or");
        return cond3;
    }

    protected string getCond4()
    {
        string cond4 = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond4 += " SI.BranchCode='" + listItem.Value + "' ,";
                }
            }
        }
        cond4 = cond4.TrimEnd(',');
        cond4 = cond4.Replace(",", "or");
        return cond4;
    }


    protected string getCond5()
    {
        string cond5 = "";

        foreach (ListItem listItem in lstBranch.Items)
        {
            if (listItem.Text != "All")
            {
                if (listItem.Selected)
                {
                    cond5 += listItem.Value + ",";
                }
            }
        }

        return cond5;
    }


    protected string getCond6()
    {
        string cond6 = "";
        foreach (ListItem listItem1 in lstPricelist.Items)
        {
            if (listItem1.Text != "All")
            {
                if (listItem1.Selected)
                {
                    cond6 += listItem1.Value + ",";
                }
            }
        }

        return cond6;
    }

    protected void btndet_Click(object sender, EventArgs e)
    {
        try
        {
            div1.Visible = true;
            divPrint.Visible = false;
            divPr.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }




    protected void btnxls_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            DataSet ddd = new DataSet();
            DateTime refDate = DateTime.Parse(txtStartDate.Text);

            BusinessLogic bl = new BusinessLogic(sDataSource);
            ds = bl.getProductsstock(connection, refDate);
            double Amount = 0;

            DataTable dt = new DataTable();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt.Columns.Add(new DataColumn("Category"));
                dt.Columns.Add(new DataColumn("Brand"));
                dt.Columns.Add(new DataColumn("Product Name"));
                dt.Columns.Add(new DataColumn("Item Code"));
                dt.Columns.Add(new DataColumn("Model"));
                dt.Columns.Add(new DataColumn("Qty"));
                dt.Columns.Add(new DataColumn("Rate"));
                dt.Columns.Add(new DataColumn("Amount"));

                DataRow dr_final113 = dt.NewRow();
                dt.Rows.Add(dr_final113);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_final122 = dt.NewRow();
                    dr_final122["Category"] = dr["CategoryName"].ToString();
                    dr_final122["Brand"] = dr["productdesc"].ToString();
                    dr_final122["Product Name"] = dr["ProductName"].ToString();
                    dr_final122["Item Code"] = dr["ItemCode"].ToString();
                    dr_final122["Model"] = dr["Model"].ToString();
                    dr_final122["Qty"] = Convert.ToDouble(dr["Stock"]);
                    dr_final122["Rate"] = Convert.ToDouble(dr["Rate"]);
                    dr_final122["Amount"] = Convert.ToDouble(dr["Stock"]) * Convert.ToDouble(dr["Rate"]);
                    Amount = Amount + (Convert.ToDouble(dr["Stock"]) * Convert.ToDouble(dr["Rate"]));
                    dt.Rows.Add(dr_final122);
                }

                DataRow dr_final12213 = dt.NewRow();
                dr_final12213["Category"] = "";
                dr_final12213["Brand"] = "";
                dr_final12213["Product Name"] = "";
                dr_final12213["Item Code"] = "";
                dr_final12213["Model"] = "";
                dr_final12213["Qty"] = "";
                dr_final12213["Rate"] = "";
                dr_final12213["Amount"] = "";
                dt.Rows.Add(dr_final12213);

                DataRow dr_final123 = dt.NewRow();
                dr_final12213["Category"] = "";
                dr_final123["Product Name"] = "";
                dr_final123["Brand"] = "";
                dr_final123["Item Code"] = "";
                dr_final123["Model"] = "";
                dr_final123["Qty"] = "";
                dr_final123["Rate"] = "";
                dr_final123["Amount"] = Amount;
                dt.Rows.Add(dr_final123);

                ExportToExcel(dt);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            string filename = "Stock Report.xls";
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
            if (lstBranch.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select any Branch')", true);
            }
            else if(lstPricelist.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select any PriceList')", true);
            }
            else
            {
                DateTime refDate = DateTime.Parse(txtStartDate.Text);

                string cond = "";
                cond = getCond();
                string cond1 = "";
                cond1 = getCond1();
                string cond2 = "";
                cond2 = getCond2();
                string cond3 = "";
                cond3 = getCond3();
                string cond4 = "";
                cond4 = getCond4();
                string cond5 = "";
                cond5 = getCond5();
                string cond6 = "";
                cond6 = getCond6();
                //Response.Write("<script language='javascript'> window.open('StockReport1.aspx?refDate=" + refDate + "&cond=" + Server.UrlEncode(cond) + "&cond1=" + Server.UrlEncode(cond1) + "' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
                Response.Write("<script language='javascript'> window.open('StockReport1.aspx?refDate=" + refDate + "&cond=" + Server.UrlEncode(cond) + "&cond1=" + Server.UrlEncode(cond1) + "&cond2=" + Server.UrlEncode(cond2) + "&cond3=" + Server.UrlEncode(cond3) + "&cond4=" + Server.UrlEncode(cond4) + "&cond5=" + cond5 + "&cond6=" + cond6 + "' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            sumDbl = 0;
            //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
            //    string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label lblCatID = e.Row.FindControl("lblCategoryID") as Label;
                Label lblTotal = e.Row.FindControl("lblTotal") as Label;
                //Label lblCategory = e.Row.FindControl("lblCategory") as Label;
                Label lblGrand = e.Row.FindControl("lblGrandTotal") as Label;
                // int catID = Convert.ToInt32(lblCatID.Text);
                int catID = Convert.ToInt32(gvCategory.DataKeys[e.Row.RowIndex].Value);
                ReportsBL.ReportClass rptProduct = new ReportsBL.ReportClass();

                BusinessLogic bl = new BusinessLogic(sDataSource);

                DateTime refDate = DateTime.Parse(txtStartDate.Text);
                string cond = "";
                cond = getCond();
                string cond1 = "";
                cond1 = getCond1();
                string cond2 = "";
                cond2 = getCond2();
                string cond3 = "";
                cond3 = getCond3();
                string cond4 = "";
                cond4 = getCond4();
                DataSet ds = bl.getProducts(sDataSource, refDate, cond, cond1, cond2, cond3, cond4, "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }
                else
                {
                    //lblCategory.Text = "";
                }
                lblTotal.Text = sumDbl.ToString("f2");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label amt = e.Row.FindControl("lblAmount") as Label;
                Label rate = e.Row.FindControl("lblRate") as Label;

                Label Stock = e.Row.FindControl("lblStock") as Label;

                amtDbl = Double.Parse(rate.Text) * Double.Parse(Stock.Text);
                amt.Text = amtDbl.ToString("f2");


                sumDbl = sumDbl + amtDbl;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label labelTotal = e.Row.FindControl("lblTotal") as Label;
                labelTotal.Text = Convert.ToString(sumDbl);
                grandDbl = grandDbl + sumDbl;
                lblGrandTotal.Text = grandDbl.ToString("#0.00");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lstBranch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lstBranch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        foreach (ListItem li in lstBranch.Items)
        {
            if (lstBranch.SelectedIndex == 0)
            {
                if (li.Text != "All")
                {
                    li.Selected = true;
                }
            }           
        }
    }
    protected void lstPricelist_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in lstPricelist.Items)
        {
            if (lstPricelist.SelectedIndex == 0)
            {
                if (li.Text != "All")
                {
                    li.Selected = true;
                }
            }
        }
    }
}
