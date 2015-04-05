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

public partial class BulkAdditionSupplier : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    Double SumCashSales = 0.0d;
    BusinessLogic objBL;
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

        dt.Columns.Add(new DataColumn("LedgerName"));
        dt.Columns.Add(new DataColumn("AliasName"));
        dt.Columns.Add(new DataColumn("OpenBalanceDR"));
        dt.Columns.Add(new DataColumn("OpenBalanceCR"));
        dt.Columns.Add(new DataColumn("ContactName"));
        dt.Columns.Add(new DataColumn("Add1"));
        dt.Columns.Add(new DataColumn("Add2"));
        dt.Columns.Add(new DataColumn("Add3"));
        dt.Columns.Add(new DataColumn("Phone"));
        dt.Columns.Add(new DataColumn("Mobile"));
        dt.Columns.Add(new DataColumn("CreditLimit"));
        dt.Columns.Add(new DataColumn("CreditDays"));
        dt.Columns.Add(new DataColumn("TinNumber"));
        // dt.Columns.Add(new DataColumn("Emailid"));
      //  dt.Columns.Add(new DataColumn("Opduedate"));
        // dt.Columns.Add(new DataColumn("IsActive"));
        // dt.Columns.Add(new DataColumn("DC"));
        //dt.Columns.Add(new DataColumn("Paymentmade"));
        //dt.Columns.Add(new DataColumn("dc"));

        DataRow dr_final12 = dt.NewRow();
        dr_final12["LedgerName"] = "";
        dr_final12["AliasName"] = "";
        dr_final12["OpenBalanceDR"] = "0";
        dr_final12["OpenBalanceCR"] = "0";
        dr_final12["ContactName"] = "";
        dr_final12["Add1"] = "";
        dr_final12["Add2"] = "";
        dr_final12["Add3"] = "";
        dr_final12["Mobile"] = "";
        dr_final12["CreditLimit"] = "0";
        dr_final12["CreditDays"] = "0";
        dr_final12["TinNumber"] = "0";
        //  dr_final12["Emailid"] = "";
       // dr_final12["Opduedate"] = "";
        // dr_final12["IsActive"] = "YES";
        //  dr_final12["DC"] = "NO";
        //dr_final12["Inttrans"] = "NO";
        //dr_final12["Paymentmade"] = "NO";
        //dr_final12["dc"] = "NO";

        dt.Rows.Add(dr_final12);

        ExportToExcel(dt);
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            //string filename = "Sales Report.xls";
            string filename = "NewSuppliers _" + DateTime.Now.ToString() + ".xls";
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
            //    if (FileUpload1.HasFile)
            //    {
            //        string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //        string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            //        string FilePath = Server.MapPath(FolderPath + FileName);
            //        FileUpload1.SaveAs(FilePath);
            //        Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
            //    }
            //}
            //private void Import_To_Grid(string FilePath, string Extension, string isHDR)
            //{
            //    string conStr = "";
            //    switch (Extension)
            //    {
            //        case ".xls": //Excel 97-03
            //            conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
            //            break;
            //        case ".xlsx": //Excel 07
            //            conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
            //            break;
            //    }

            //    //if (FileExt == ".xls")
            //    //{
            //    //    con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=Excel 8.0;");

            //    //}
            //    //else if (FileExt == ".xlsx")
            //    //{
            //    //    con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;");
            //    //}
            //    conStr = String.Format(conStr, FilePath, isHDR);
            //    OleDbConnection connExcel = new OleDbConnection(conStr);
            //    OleDbCommand cmdExcel = new OleDbCommand();
            //    OleDbDataAdapter oda = new OleDbDataAdapter();
            //    DataTable dt = new DataTable();
            //    cmdExcel.Connection = connExcel;

            //    //Get the name of First Sheet
            //    connExcel.Open();
            //    //DataTable dtExcelSchema;
            //    DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            //    connExcel.Close();

            //    //Read Data from First Sheet
            //    connExcel.Open();
            //    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            //    oda.SelectCommand = cmdExcel;
            //    oda.Fill(dt);
            //    connExcel.Close();

            //    //Bind Data to GridView
            //    GridView1.Caption = Path.GetFileName(FilePath);
            //    GridView1.DataSource = dt;
            //    GridView1.DataBind();
            //}
            //protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
            //{
            //    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            //    string FileName = GridView1.Caption;
            //    string Extension = Path.GetExtension(FileName);
            //    string FilePath = Server.MapPath(FolderPath + FileName);

            //    Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
            //    GridView1.PageIndex = e.NewPageIndex;
            //    GridView1.DataBind();
            //}

            //string connectionString ="";
            //if (FileUpload1.HasFile)
            //{
            //    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //    string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //    string fileLocation = Server.MapPath("~/App_Data/" + fileName);
            //    FileUpload1.SaveAs(fileLocation);

            //    //Check whether file extension is xls or xslx

            //    if (fileExtension == ".xls")
            //    {
            //        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            //    }
            //    else if (fileExtension == ".xlsx")
            //    {
            //        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            //    }

            //    //Create OleDB Connection and OleDb Command

            //    OleDbConnection con = new OleDbConnection(connectionString);
            //    OleDbCommand cmd = new OleDbCommand();
            //    cmd.CommandType = System.Data.CommandType.Text;
            //    cmd.Connection = con;
            //    OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
            //    DataTable dtExcelRecords = new DataTable();
            //    con.Open();
            //    DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //    string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
            //    cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
            //    dAdapter.SelectCommand = cmd;
            //    dAdapter.Fill(dtExcelRecords);
            //    //con.Close();
            //    GridView1.DataSource = dtExcelRecords;
            //    GridView1.DataBind();
            //    Label2.Visible = false;

            //}
            //else
            //{
            //    Label2.Text = "Please Select An Excel File";
            //}


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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Upload Correct File Extension(.xls or .xlsx).');", true);
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

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["LedgerName"]) == null) || (Convert.ToString(dr["LedgerName"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Ledger name is empty');", true);
                        return;
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["TinNumber"]) == null) || (Convert.ToString(dr["TinNumber"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('TinNumber is empty');", true);
                        return;
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["OpenBalanceDR"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter OpenBalanceDR. It cannot be blank or otherwise put Zero.');", true);
                        return;
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["OpenBalanceCR"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter OpenBalanceCR. It cannot be blank or otherwise put Zero.');", true);
                        return;
                    }
                }
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string customer = Convert.ToString(dr["LedgerName"]);

                    if (objBL.CheckIfcustomerIsThere(customer))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Supplier with - " + customer + " - already exists.');", true);
                        return;
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string Mobile = Convert.ToString(dr["Mobile"]);

                    if (Convert.ToString(dr["Mobile"]) == null || Convert.ToString(dr["Mobile"]) == "" || Convert.ToString(dr["Mobile"]) == "0")
                    {

                    }
                    else
                    {
                        if (objBL.CheckIfcustomernumberIsThere(Mobile))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Supplier  with this - " + Mobile + " - number already exists.');", true);
                            return;
                        }
                    }
                }

                //DataTable Dt;

                ////  for (int i1 = 0; i1 < ds.Tables[0].Rows.Count; i1++)
                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{

                //    string date = Convert.ToString((dr["Opduedate"]).ToString());

                //    if (!ValidateDate(date))
                //    {

                //        // int RowNo = i1 + 2;

                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "InvalidArgs", "alert('Please Enter Correct Date format ');", true);



                //        return;

                //    }

                //}

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    if ((Convert.ToString(dr["Opduedate"]) != null) || (Convert.ToString(dr["Opduedate"]) != ""))
                //    {
                //        int index = Convert.ToString(dr["Opduedate"]).IndexOfAny(specialCharactersArray);
                //        //index == -1 no special characters
                //        if (index != -1)
                //        {
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Special characters not allowed in category');", true);
                //            return;
                //        }
                //    }
                //}

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    if ((Convert.ToString(dr["Opduedate"]) != null) || (Convert.ToString(dr["Opduedate"]) != ""))
                //    {
                //        foreach (char c in Convert.ToString(dr["Opduedate"]))
                //        {
                //            if (!Char.IsDigit(c))
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter Valid Date format');", true);
                //                return;
                //            }
                //        }
                //    }
                //}






                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Missing Datas');", true);
                //        return;
                //    }
                //}

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    string brand = Convert.ToString(dr["brand"]);
                //    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                //    {

                //    }
                //    else
                //    {
                //        if (!objBL.CheckIfbrandIsThere(brand))
                //        {
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Brand with - " + brand + " - does not exists in the Brand Master');", true);
                //            return;
                //        }
                //    }
                //}


                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    string category = Convert.ToString(dr["category"]);

                //    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                //    {

                //    }
                //    else
                //    {
                //        if (!objBL.CheckIfcategoryIsThere(category))
                //        {
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Category with - " + category + " - does not exists in the Category Master');", true);
                //            return;
                //        }
                //    }
                //}


                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    string item = Convert.ToString(dr["ItemCode"]);
                //    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                //    {

                //    }
                //    else
                //    {
                //        if (objBL.CheckIfItemCodeDuplicate(item))
                //        {
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product with item code - " + item + " - already exists in the master.');", true);
                //            return;
                //        }
                //    }
                //}


                int i = 1;
                int ii = 1;
                string itemc = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    itemc = Convert.ToString(dr["LedgerName"]);

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
                                if (itemc == Convert.ToString(drd["LedgerName"]))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Supplier - " + itemc + " - already exists in the excel.');", true);
                                    return;
                                }
                            }
                            ii = ii + 1;
                        }
                    }
                    i = i + 1;
                    ii = 1;
                }


                int j = 1;
                int jj = 1;
                string itemcj = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    itemcj = Convert.ToString(dr["Mobile"]);

                    if ((itemcj == null) || (itemcj == ""))
                    {
                    }
                    else
                    {
                        foreach (DataRow drd in ds.Tables[0].Rows)
                        {

                            if (jj == j)
                            {
                            }
                            else
                            {
                                if (itemcj == "" || itemcj=="0")
                                {

                                }
                                else
                                {

                                    if (itemcj == Convert.ToString(drd["Mobile"]))
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Mobile - " + itemc + " - already exists in the excel.');", true);
                                        return;
                                    }
                                }
                            }
                            jj = jj + 1;
                        }
                    }
                    j = j + 1;
                    jj = 1;
                }


                objBL.InsertBulkLedgerSupplier(connection, ds, usernam);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Suppliers Uploaded Successfully');", true);

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

    private bool ValidateDate(string date)
    {

        try
        {

            string[] dateParts = date.Split('/');

            DateTime testDate = new DateTime(Convert.ToInt32(dateParts[2]), Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[0]));

            return true;

        }

        catch
        {

            return false;

        }

    }
}
