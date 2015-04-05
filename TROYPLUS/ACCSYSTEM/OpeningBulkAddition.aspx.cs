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
using System.Net.NetworkInformation;
using System.Data.OleDb;

public partial class OpeningBulkAddition : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    Double SumCashSales = 0.0d;
    BusinessLogic objBL;
    string connection;
    string usernam;
    string barncode;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            //printPreview();
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                //lblBillDate.Text = DateTime.Now.ToShortDateString();
                //SalesPanel.Visible = false;

                loadBranch();
                BranchEnable_Disable();

                //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                //Label1.Text = nics[0].GetPhysicalAddress().ToString();


                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

                    if (companyInfo != null)
                    {
                        if (companyInfo.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in companyInfo.Tables[0].Rows)
                            {
                                //lblTNGST.Text = Convert.ToString(dr["TINno"]);
                                //lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                                //lblPhone.Text = Convert.ToString(dr["Phone"]);
                                //lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                                //lblAddress.Text = Convert.ToString(dr["Address"]);
                                //lblCity.Text = Convert.ToString(dr["city"]);
                                //lblPincode.Text = Convert.ToString(dr["Pincode"]);
                                //lblState.Text = Convert.ToString(dr["state"]);

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

    protected void btnFormat_Click(object sender, EventArgs e)
    {
        try
        {
            bindData();
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
            DateTime startDate, endDate;
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            //SalesPanel.Visible = true;

            //startDate = Convert.ToDateTime(txtStartDate.Text);
            //endDate = Convert.ToDateTime(txtEndDate.Text);
            //lblStartDate.Text = txtStartDate.Text;
            //lblEndDate.Text = txtEndDate.Text;
            //ReportsBL.ReportClass rptSalesReport;
            //rptSalesReport = new ReportsBL.ReportClass();
            //DataSet ds = rptSalesReport.generateSalesReportDS(startDate, endDate, sDataSource);
            //gvSales.DataSource = ds;
            //gvSales.DataBind();
            //SalesPanel.Visible = true;
            //div1.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    

    protected void btnExl_Click(object sender, EventArgs e)
    {
       
    }

    public void bindData()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("ITEMCODE"));
        dt.Columns.Add(new DataColumn("BRAND"));
        dt.Columns.Add(new DataColumn("PRODUCTNAME"));
        dt.Columns.Add(new DataColumn("MODEL"));
        dt.Columns.Add(new DataColumn("CATEGORY"));
        dt.Columns.Add(new DataColumn("Opening"));
        dt.Columns.Add(new DataColumn("Remarks"));
        dt.Columns.Add(new DataColumn("DueDate"));
        
        DataRow dr_final12 = dt.NewRow();
        dr_final12["ITEMCODE"] = "";
        dr_final12["BRAND"] = "";
        dr_final12["PRODUCTNAME"] = "";
        dr_final12["MODEL"] = "";
        dr_final12["Opening"] = "";
        dr_final12["Remarks"] = "";
        dr_final12["DueDate"] = "dd-MM-yyyy";
        dt.Rows.Add(dr_final12);

        ExportToExcel(dt);
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            //string filename = "Sales Report.xls";
            string filename = "OpeningStockProducts _" + DateTime.Now.ToString() + ".xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();
            //dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            //dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            //dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
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

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            


            String strConnection = "ConnectionString";
            string connectionString = "";

            string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialCharactersArray = specialCharacters.ToCharArray();

            if (FileUpload1.HasFile)
            {
                string datett = DateTime.Now.ToString();
                string dtaa = Convert.ToDateTime(datett).ToString("dd-MM-yyyy-hh-mm-ss");
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName) + dtaa;
                string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string fileLocation = Server.MapPath("~/App_Data/" + fileName);
                FileUpload1.SaveAs(fileLocation);
                if (fileExtension == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (fileExtension == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                    //OleDbConnection Conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Attach Correct Format file Extension.(.xls or .xlsx)');", true);
                    return;
                }
                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                DataTable dtExcelRecords = new DataTable();
                con.Open();
                DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                dAdapter.SelectCommand = cmd;
                dAdapter.Fill(dtExcelRecords);
                DataSet ds = new DataSet();
                ds.Tables.Add(dtExcelRecords);

                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic objBL = new BusinessLogic();
                objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());
                string connection = Request.Cookies["Company"].Value;

                if (ds == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Uploading Excel is Empty');", true);
                    return;
                }



                //for empty validation

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ItemCode is empty');", true);
                        return;
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["brand"]) == null) || (Convert.ToString(dr["brand"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('brand is empty');", true);
                        return;
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["category"]) == null) || (Convert.ToString(dr["category"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('category is empty');", true);
                        return;
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["PRODUCTNAME"]) == null) || (Convert.ToString(dr["PRODUCTNAME"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Productname is empty');", true);
                        return;
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["Remarks"]) == null) || (Convert.ToString(dr["Remarks"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Remarks is empty');", true);
                        return;
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["DueDate"]) == null) || (Convert.ToString(dr["DueDate"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Due Date is empty');", true);
                        return;
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["Opening"]) == null) || (Convert.ToString(dr["Opening"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Opening Stock  is empty');", true);
                        return;
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["MODEL"]) == null) || (Convert.ToString(dr["MODEL"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Model is empty');", true);
                        return;
                    }
                }

                //Check character validation:-
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["ItemCode"]) != null) || (Convert.ToString(dr["ItemCode"]) != ""))
                    {
                        int index = Convert.ToString(dr["ItemCode"]).IndexOfAny(specialCharactersArray);
                        //index == -1 no special characters
                        if (index != -1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Special characters not allowed in Item code');", true);
                            return;
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["PRODUCTNAME"]) != null) || (Convert.ToString(dr["PRODUCTNAME"]) != ""))
                    {
                        int index = Convert.ToString(dr["PRODUCTNAME"]).IndexOfAny(specialCharactersArray);
                        //index == -1 no special characters
                        if (index != -1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Special characters not allowed in Product Name');", true);
                            return;
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["Model"]) != null) || (Convert.ToString(dr["Model"]) != ""))
                    {
                        int index = Convert.ToString(dr["Model"]).IndexOfAny(specialCharactersArray);
                        //index == -1 no special characters
                        if (index != -1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Special characters not allowed in Model');", true);
                            return;
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["Brand"]) != null) || (Convert.ToString(dr["Brand"]) != ""))
                    {
                        int index = Convert.ToString(dr["Brand"]).IndexOfAny(specialCharactersArray);
                        //index == -1 no special characters
                        if (index != -1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Special characters not allowed in Brand');", true);
                            return;
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["Category"]) != null) || (Convert.ToString(dr["Category"]) != ""))
                    {
                        int index = Convert.ToString(dr["Category"]).IndexOfAny(specialCharactersArray);
                        //index == -1 no special characters
                        if (index != -1)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Special characters not allowed in Category');", true);
                            return;
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["Opening"]) != null) || (Convert.ToString(dr["Opening"]) != ""))
                    {
                        foreach (char c in Convert.ToString(dr["Opening"]))
                        {
                            if (!Char.IsDigit(c))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter only Numberic values in Opening Stock');", true);
                                return;
                            }
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["Opening"]) != null) || (Convert.ToString(dr["Opening"]) != ""))
                    {
                        int openingstock = Convert.ToInt32(dr["Opening"]);

                        if (openingstock <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Opening Stock cannot Less than 0');", true);
                            return;
                        }
                    }
                }
                 foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   // DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                  //  DateTime date = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                     DateTime date1= DateTime.Now;

                    if ((Convert.ToString(dr["DueDate"]) != null) || (Convert.ToString(dr["DueDate"]) != ""))
                    {
                        DateTime prieffdte = (Convert.ToDateTime(dr["DueDate"]));

                        if (prieffdte.Date >= date1.Date)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Due date may not be furture date');", true);
                            return;
                        }
                    }
                }

                 //else if (dt > DateTime.Now)
                 //   {
                 //       ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Effective date not greater then Current date in row " + col + " ');", true);
                 //       return;
                 //   }

                BusinessLogic bl = new BusinessLogic(sDataSource);
               // DataSet ds1 = bl.GetPriceListName(sDataSource);

               // for (int iw = 0; iw <= ds1.Tables[0].Rows.Count - 1; iw++)
             //   foreach (DataRow dr in ds.Tables[0].Rows)
               // {

                   
                   
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //     string prieffdte = (Convert.ToString(dr["DueDate"]));
                    //    if ((Convert.ToString(dr["DueDate"]) != null) || (Convert.ToString(dr["DueDate"]) != ""))
                    //    {
                    //        //string[] format = new string[] { "dd-MM-yyyy" };
                    //        //string value = Convert.ToString(dr["DueDate"]);
                    //        //DateTime datetime;

                    //        //if (DateTime.TryParseExact(value, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out datetime))
                    //        //{

                    //        //}
                    //        //else
                    //        {
                    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + prieffdte + " - DueDate" + " is invalid format');", true);
                    //            return;
                    //        }

                    //    }
                    //}
              //  }



               
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string brand = Convert.ToString(dr["brand"]);
                    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    {

                    }
                    else
                    {
                        if (!objBL.CheckIfbrandIsThere(brand))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Brand with - " + brand + " - does not exists in the Brand Master');", true);
                            return;

                        }
                    }
                }


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string category = Convert.ToString(dr["category"]);

                    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    {

                    }
                    else
                    {
                        if (!objBL.CheckIfcategoryIsThere(category))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Category with - " + category + " - does not exists in the Category Master');", true);
                            return;
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string item = Convert.ToString(dr["ItemCode"]);

                    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    {

                    }
                    else
                    {
                        if (!objBL.CheckIfItemCodeDuplicate(item))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Code - " + item + " - does not exists in the Product master.');", true);
                            return;
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string item = Convert.ToString(dr["ItemCode"]);
                    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    {

                    }
                    else
                    {
                        if (drpBranch.Text.Trim() != string.Empty)
                           barncode = Convert.ToString(drpBranch.SelectedValue);

                        if (objBL.IsItemAlreadyInOpening(connection, item, barncode))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product code " + item + " already added in opening stock. For same branch "+barncode+"');", true);
                            return;
                        }
                    }
                }


                int i = 1;
                int ii = 1;
                string brncode = string.Empty;
                string itemc = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    itemc = Convert.ToString(dr["ItemCode"]);

                    if ((itemc == null) || (itemc == ""))
                    {
                    }
                    else
                    {
                        foreach (DataRow drd in ds.Tables[0].Rows)
                        {
                            if (ii == i)
                            {
                            }
                            else
                            {
                                if (itemc == Convert.ToString(drd["ItemCode"]))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product code - " + itemc + " - already exists in the excel.');", true);
                                    return;
                                }
                            }
                            ii = ii + 1;
                        }
                    }
                    i = i + 1;
                    ii = 1;
                }
                if (drpBranch.Text.Trim() != string.Empty)
                    brncode = Convert.ToString(drpBranch.SelectedValue);

                objBL.InsertBulkOpeningStock(connection, ds, usernam, brncode);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Opening Stock Uploaded Successfully');", true);

                //GridView1.DataSource = dtExcelRecords;
                //GridView1.DataBind(); 

                con.Close();
            }
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

        drpBranch.Items.Clear();
        drpBranch.Items.Add(new ListItem("All Branch", "All"));
        ds = bl.ListBranch();
        drpBranch.DataSource = ds;
        drpBranch.DataBind();
        drpBranch.DataTextField = "BranchName";
        drpBranch.DataValueField = "Branchcode";
    }
    private void BranchEnable_Disable()
    {
        string sCustomer = string.Empty;
        connection = Request.Cookies["Company"].Value;
        usernam = Request.Cookies["LoggedUserName"].Value;
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
    }
}
