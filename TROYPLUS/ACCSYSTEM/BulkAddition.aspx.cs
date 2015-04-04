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

public partial class BulkAddition : System.Web.UI.Page
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

        dt.Columns.Add(new DataColumn("ITEMCODE"));
        dt.Columns.Add(new DataColumn("BRAND"));
        dt.Columns.Add(new DataColumn("PRODUCTNAME"));
        dt.Columns.Add(new DataColumn("MODEL"));
        dt.Columns.Add(new DataColumn("CATEGORY"));

        dt.Columns.Add(new DataColumn("VAT"));
        dt.Columns.Add(new DataColumn("CST"));
        dt.Columns.Add(new DataColumn("ExecutiveCommision"));
        dt.Columns.Add(new DataColumn("Deviation"));
        dt.Columns.Add(new DataColumn("ReorderLevel"));
        //dt.Columns.Add(new DataColumn("Discount"));
        // dt.Columns.Add(new DataColumn("Price"));
        // dt.Columns.Add(new DataColumn("EffectiveDate"));
        double vat = 0;

        DataRow dr_final12 = dt.NewRow();
        dr_final12["ITEMCODE"] = "";
        dr_final12["BRAND"] = "";
        dr_final12["PRODUCTNAME"] = "";
        dr_final12["MODEL"] = "";

        dr_final12["VAT"] = vat;
        dr_final12["CST"] = vat;
        dr_final12["ExecutiveCommision"] = vat;
        dr_final12["Deviation"] = vat;
        dr_final12["ReorderLevel"] = vat;
        // dr_final12["Discount"] = vat;
        // dr_final12["Price"] = vat;


        // dr_final12["EffectiveDate"] = DateTime.Now.ToString("dd/MM/yyyy");


        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds1 = bl.GetPriceListName(sDataSource);

        for (int i = 0; i <= ds1.Tables[0].Rows.Count - 1; i++)
        {
            string s = ds1.Tables[0].Rows[i]["PriceName"].ToString();
            dt.Columns.Add(new DataColumn(s));
            dt.Columns.Add(new DataColumn(s + " - EffectiveDate"));
            dt.Columns.Add(new DataColumn(s + " - Discount"));
        }

        dt.Rows.Add(dr_final12);

        ExportToExcel(dt);
    }

    public void ExportToExcel(DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            //string filename = "Sales Report.xls";
            string filename = "NewProducts _" + DateTime.Now.ToString() + ".xls";
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


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["ItemCode"]) == null) || (Convert.ToString(dr["ItemCode"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item code is empty');", true);
                        return;
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["PRODUCTNAME"]) == null) || (Convert.ToString(dr["PRODUCTNAME"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Name is empty');", true);
                        return;
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["Deviation"]) == null) || (Convert.ToString(dr["Deviation"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Deviation(%) is empty');", true);
                        return;
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["Model"]) == null) || (Convert.ToString(dr["Model"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Model is empty');", true);
                        return;
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["Vat"]) == null) || (Convert.ToString(dr["Vat"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Vat(%) is empty');", true);
                        return;
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["ReorderLevel"]) == null) || (Convert.ToString(dr["ReorderLevel"]) == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Reorder Level is empty');", true);
                        return;
                    }
                }

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    if ((Convert.ToString(dr["EffectiveDate"]) == null) || (Convert.ToString(dr["EffectiveDate"]) == ""))
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Effective Date is empty');", true);
                //        return;
                //    }
                //}

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
                    if ((Convert.ToString(dr["Vat"]) != null) || (Convert.ToString(dr["Vat"]) != ""))
                    {
                        foreach (char c in Convert.ToString(dr["Vat"]))
                        {
                            if (!Char.IsDigit(c))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter only Numberic values in Vat');", true);
                                return;
                            }
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["Vat"]) != null) || (Convert.ToString(dr["Vat"]) != ""))
                    {
                        if (Convert.ToInt32(dr["Vat"]) > 100)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Vat cannot be Greater than 100% and Less than 0%');", true);
                            return;
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["ReorderLevel"]) != null) || (Convert.ToString(dr["ReorderLevel"]) != ""))
                    {
                        foreach (char c in Convert.ToString(dr["ReorderLevel"]))
                        {
                            if (!Char.IsDigit(c))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter only Numberic values in Reorder Level');", true);
                                return;
                            }
                        }
                    }
                }

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    if ((Convert.ToString(dr["Price"]) != null) || (Convert.ToString(dr["Price"]) != ""))
                //    {
                //        foreach (char c in Convert.ToString(dr["Price"]))
                //        {
                //            if (!Char.IsDigit(c))
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter only Numberic values in Price');", true);
                //                return;
                //            }
                //        }
                //    }
                //}

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    if ((Convert.ToString(dr["Price"]) != null) || (Convert.ToString(dr["Price"]) != ""))
                //    {
                //        if (Convert.ToInt32(dr["Price"]) > 200)
                //        {
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Price cannot be Greater than 200% and Less than 0%');", true);
                //            return;
                //        }
                //    }
                //}

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    if ((Convert.ToString(dr["Discount"]) != null) || (Convert.ToString(dr["Discount"]) != ""))
                //    {
                //        foreach (char c in Convert.ToString(dr["Discount"]))
                //        {
                //            if (!Char.IsDigit(c))
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter only Numberic values in Discount');", true);
                //                return;
                //            }
                //        }
                //    }
                //}

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    if ((Convert.ToString(dr["Discount"]) != null) || (Convert.ToString(dr["Discount"]) != ""))
                //    {
                //        if (Convert.ToInt32(dr["Discount"]) > 100)
                //        {
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Discount cannot be Greater than 100% and Less than 0%');", true);
                //            return;
                //        }
                //    }
                //}

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["CST"]) != null) || (Convert.ToString(dr["CST"]) != ""))
                    {
                        foreach (char c in Convert.ToString(dr["CST"]))
                        {
                            if (!Char.IsDigit(c))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter only Numberic values in CST');", true);
                                return;
                            }
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["CST"]) != null) || (Convert.ToString(dr["CST"]) != ""))
                    {
                        if (Convert.ToInt32(dr["CST"]) > 100)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('CST cannot be Greater than 100% and Less than 0%');", true);
                            return;
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["ExecutiveCommision"]) != null) || (Convert.ToString(dr["ExecutiveCommision"]) != ""))
                    {
                        foreach (char c in Convert.ToString(dr["ExecutiveCommision"]))
                        {
                            if (!Char.IsDigit(c))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Enter only Numberic values in Executive Commision');", true);
                                return;
                            }
                        }
                    }
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if ((Convert.ToString(dr["ExecutiveCommision"]) != null) || (Convert.ToString(dr["ExecutiveCommision"]) != ""))
                    {
                        if (Convert.ToInt32(dr["ExecutiveCommision"]) > 100)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Executive Commision cannot be Greater than 100% and Less than 0%');", true);
                            return;
                        }
                    }
                }


                BusinessLogic bl = new BusinessLogic(sDataSource);
                DataSet ds1 = bl.GetPriceListName(sDataSource);

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    string privalue = ds1.Tables[0].Rows[0]["PriceName"].ToString();
                //    string prieffdte = privalue + " - EffectiveDate";
                //    string pridisc = privalue + " - Discount";
                //    if (privalue == Convert.ToString(dr["" + privalue + ""]))
                //    {
                //        if ((Convert.ToString(dr["" + privalue + ""]) == null) || (Convert.ToString(dr["" + privalue + ""]) == ""))
                //        {
                //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + privalue + " cannot be Greater than 100% and Less than 0%');", true);
                //            return;
                //        }
                //    }
                //}

                for (int iw = 0; iw <= ds1.Tables[0].Rows.Count - 1; iw++)
                {
                    string privalue = ds1.Tables[0].Rows[iw]["PriceName"].ToString();
                    string prieffdte = privalue + " - EffectiveDate";
                    string pridisc = privalue + " - Discount";
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (Convert.ToString(dr["" + privalue + " - Discount"]) != null || Convert.ToString(dr["" + privalue + " - Discount"]) != "")
                        {
                            if (Convert.ToInt32(dr["" + privalue + " - Discount"]) > 100)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + privalue + " - Discount" + " cannot be Greater than 100% and Less than 0%');", true);
                                return;
                            }
                        }
                        if (Convert.ToString(dr["" + privalue + " - EffectiveDate"]) != null || Convert.ToString(dr["" + privalue + " - EffectiveDate"]) != "")
                        {
                            string[] format = new string[] { "dd-MM-yyyy" };
                            string value = Convert.ToString(dr["" + privalue + " - EffectiveDate"]);
                            DateTime datetime;

                            if (DateTime.TryParseExact(value, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out datetime))
                            {

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + privalue + " - EffectiveDate" + " is invalid format');", true);
                                return;
                            }

                        }
                    }
                }






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
                        if (objBL.CheckIfItemCodeDuplicate(item))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product with item code - " + item + " - already exists in the master.');", true);
                            return;
                        }
                    }
                }


                int i = 1;
                int ii = 1;
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
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product with item code - " + itemc + " - already exists in the excel.');", true);
                                    return;
                                }
                            }
                            ii = ii + 1;
                        }
                    }
                    i = i + 1;
                    ii = 1;
                }

                objBL.InsertBulkProducts(connection, ds, usernam);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Products Uploaded Successfully');", true);

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
}
