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
using System.Xml;
using System.IO;

public partial class printsalesbill : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    private Double amtTotal = 0;
    public Double amt = 0.0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (Session["salesID"] != null)
            {
                if (!IsPostBack)
                {
                    hdFilename.Value = "Reports//" + System.Guid.NewGuid().ToString();
                    if (Session["PurchaseReturn"] != null)
                    {
                        if (Session["PurchaseReturn"].ToString() == "No")
                            lblHeader.Text = "Sales Bill";
                        else
                            lblHeader.Text = "Purchase Return";

                    }
                }
                //formXml();
                //BindProduct();
                BindSales();
                //lblDate.Text = DateTime.Now.ToShortDateString();
                //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
                BusinessLogic bl = new BusinessLogic(sDataSource);
                DataSet dsBill = new DataSet();
                dsBill = bl.GetSalesForId(Convert.ToInt32(Session["salesID"]));
                int customerID = 0;
                if (dsBill.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in dsBill.Tables[0].Rows)
                    {

                        lblBillno.Text = Convert.ToString(dr["billno"]);
                        lblBillDate.Text = Convert.ToDateTime(dr["billdate"]).ToShortDateString();
                        lblSupplier.Text = Convert.ToString(dr["Customername"]);
                        //lblReason.Text = Convert.ToString(dr["PurchaseReturnReason"]);
                        customerID = Convert.ToInt32(dr["CustomerID"]);
                    }
                }

                DataSet companyInfo = new DataSet();

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
                                lblSignCompany.Text = Convert.ToString(dr["CompanyName"]);
                                lblAddress.Text = Convert.ToString(dr["Address"]);
                                lblCity.Text = Convert.ToString(dr["city"]);
                                lblPincode.Text = Convert.ToString(dr["Pincode"]);
                                lblState.Text = Convert.ToString(dr["state"]);

                            }
                        }
                    }
                }



                DataSet dsSupplier = bl.getAddressInfo(customerID);
                if (dsSupplier != null)
                {
                    if (dsSupplier.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsSupplier.Tables[0].Rows)
                        {
                            lblSuppAdd1.Text = Convert.ToString(dr["Add1"]);
                            lblSuppAdd2.Text = Convert.ToString(dr["Add2"]);
                            lblSuppAdd3.Text = Convert.ToString(dr["Add3"]);
                            lblSuppPh.Text = Convert.ToString(dr["Phone"]);
                            if (dr["COntactname"].ToString() != string.Empty)
                                lblSupplier.Text = Convert.ToString(dr["ContactName"]);
                        }
                    }
                }

                //lblAmtWord.Text = bl.IntegerToWords(lblTotalSum.Text); 
            }
            else
            {
                Response.Redirect("Sales.aspx");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }



    public string GetTotal(Decimal qty, Decimal rate, Decimal discount, Decimal VAT)
    {

        Decimal tot = 0;
        tot = (qty * rate) - ((qty * rate) * (discount / 100)) + ((qty * rate) * (VAT / 100));
        amtTotal = amtTotal + Convert.ToDouble(tot);
        //disTotal = disTotal + discount;
        //rateTotal = rateTotal + rate;
        //vatTotal = vatTotal + VAT;
        hdTotalAmt.Value = amtTotal.ToString("#0.00");
        //lblGrandTotal.Text = Convert.ToString(Convert.ToDecimal(tot) +Convert.ToDecimal(hdTotalAmt.Value));
        return tot.ToString("#0.00");
    }

    private void BindSales()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        int salesID = 0;
        salesID = Convert.ToInt32(Session["salesID"]);

        DataSet ds = new DataSet();
        //ds = bl.GetSalesItemsForId(salesID);
        ds = bl.GetSalesBill(salesID);
        if (ds.Tables.Count > 0)
        {
            gvBillDetails.DataSource = ds.Tables[0];
            gvBillDetails.DataBind();
            //calcSum();
        }
        else
        {
            gvBillDetails.DataSource = null;
            gvBillDetails.DataBind();
        }
    }
    private void formXml()
    {
        int salesID = 0;
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = new DataSet();
        int cnt = 0;
        salesID = Convert.ToInt32(Session["salesID"]);
        DataSet ds = new DataSet();
        ds = bl.GetSalesItemsForId(salesID);
        if (ds != null)
        {
            StringWriter sWriter;
            XmlTextWriter reportXMLWriter;
            XmlDocument xmlDoc;
            string filename = string.Empty;
            sWriter = new StringWriter();
            reportXMLWriter = new XmlTextWriter(sWriter);
            reportXMLWriter.Formatting = Formatting.Indented;
            reportXMLWriter.WriteStartDocument();
            reportXMLWriter.WriteStartElement("Purchase");
            DataSet dsBill = new DataSet();
            dsBill = bl.GetSalesForId(salesID);
            if (dsBill.Tables[0].Rows.Count == 0)
            {
                reportXMLWriter.WriteStartElement("Bill");
                reportXMLWriter.WriteElementString("Billno", String.Empty);
                reportXMLWriter.WriteElementString("BillDate", String.Empty);
                reportXMLWriter.WriteElementString("Customername", String.Empty);

                reportXMLWriter.WriteEndElement();
            }
            else
            {
                foreach (DataRow dr in dsBill.Tables[0].Rows)
                {
                    cnt = cnt + 1;
                    reportXMLWriter.WriteStartElement("Bill");
                    reportXMLWriter.WriteElementString("Billno", Convert.ToString(dr["billno"]));
                    reportXMLWriter.WriteElementString("BillDate", Convert.ToString(dr["billdate"]));
                    reportXMLWriter.WriteElementString("Customername", Convert.ToString(dr["Debtor"]));
                    reportXMLWriter.WriteEndElement();
                }
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                reportXMLWriter.WriteStartElement("Product");
                reportXMLWriter.WriteElementString("SlNo", "0");
                reportXMLWriter.WriteElementString("Billno", String.Empty);
                reportXMLWriter.WriteElementString("itemCode", String.Empty);
                reportXMLWriter.WriteElementString("ProductName", String.Empty);
                reportXMLWriter.WriteElementString("ProductDesc", String.Empty);
                reportXMLWriter.WriteElementString("Qty", "0");
                reportXMLWriter.WriteElementString("Rate", "0.00");
                reportXMLWriter.WriteElementString("Discount", "0");
                reportXMLWriter.WriteElementString("VAT", "0");
                reportXMLWriter.WriteElementString("Total", "0.00");
                reportXMLWriter.WriteEndElement();

            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    reportXMLWriter.WriteStartElement("Product");
                    reportXMLWriter.WriteElementString("SlNo", Convert.ToString(cnt));
                    reportXMLWriter.WriteElementString("itemCode", Convert.ToString(dr["ItemCode"]));
                    reportXMLWriter.WriteElementString("Billno", Convert.ToString(dr["Billno"]));
                    reportXMLWriter.WriteElementString("ProductName", Convert.ToString(dr["ProductName"]));
                    reportXMLWriter.WriteElementString("ProductDesc", Convert.ToString(dr["ProductDesc"]));
                    reportXMLWriter.WriteElementString("Qty", Convert.ToString(dr["Qty"]));
                    reportXMLWriter.WriteElementString("Rate", Convert.ToString(dr["Rate"]));
                    reportXMLWriter.WriteElementString("Discount", Convert.ToString(dr["Discount"]));
                    reportXMLWriter.WriteElementString("VAT", Convert.ToString(dr["vat"]));
                    reportXMLWriter.WriteElementString("Total", "0.00");
                    reportXMLWriter.WriteEndElement();
                }
            }
            reportXMLWriter.WriteEndElement();

            reportXMLWriter.WriteEndDocument();
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sWriter.ToString());
            filename = hdFilename.Value;
            xmlDoc.Save(Server.MapPath(filename + "_ProductSalesPrint.xml"));
            hdFile.Value = Server.MapPath(filename + "_ProductSalesPrint.xml");
        }

    }
    private void BindProduct()
    {
        string filename = string.Empty;
        filename = hdFilename.Value;
        DataSet xmlDs = new DataSet();
        if (File.Exists(Server.MapPath(filename + "_ProductSalesPrint.xml")))
        {
            xmlDs.ReadXml(Server.MapPath(filename + "_ProductSalesPrint.xml"));
            if (xmlDs != null)
            {
                if (xmlDs.Tables.Count > 0)
                {
                    gvBillDetails.DataSource = xmlDs.Tables[1];
                    gvBillDetails.DataBind();
                    //calcSum();

                }
                else
                {
                    gvBillDetails.DataSource = null;
                    gvBillDetails.DataBind();
                }
                File.Delete(Server.MapPath(filename + "_ProductSalesPrint.xml"));
            }
        }

    }
    private void calcSum()
    {
        Double sumAmt = 0;
        DataSet ds = new DataSet();
        //ds.ReadXml(Server.MapPath(hdFilename.Value + "_ProductSalesPrint.xml"));
        ds = (DataSet)gvBillDetails.DataSource;
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Total"] != null)
                        sumAmt = sumAmt + Convert.ToDouble(GetTotal(Convert.ToDecimal(dr["Qty"]), Convert.ToDecimal(dr["Rate"]), Convert.ToDecimal(dr["Discount"]), Convert.ToDecimal(dr["VAT"])));
                }
            }
        }
        lblTotalSum.Text = sumAmt.ToString();



    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (File.Exists(Server.MapPath(hdFilename.Value + "_ProductSalesPrint.xml")))
                File.Delete(Server.MapPath(hdFilename.Value + "_ProductSalesPrint.xml"));
            if (Session["SalesFileName"] != null)
            {
                if (File.Exists(Session["SalesFileName"].ToString()))
                    File.Delete(Session["SalesFileName"].ToString());

            }
            Session["NEWSALES"] = "Y";
            Session["BillDate"] = lblBillDate.Text;
            Response.Redirect("CustomerSales.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private DataSet BindGrid(string strBillno)
    {


        DataSet ds = new DataSet();
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        ds = bl.GetPurchaseForId(strBillno);
        return ds;
    }
    protected void gvBillDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            amt = amt + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "total"));
            Label lblProductName = (Label)e.Row.FindControl("lblProductName");
            BusinessLogic bl = new BusinessLogic(sDataSource);
            lblProductName.Text = bl.getProductName(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "itemCode")));
            //amt = amt +Convert.ToDouble(GetTotal(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Qty")), Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PurchaseRate")), Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Discount")), Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "VAT"))));

        }

        //    Label lbl = (Label)gvBillDetails.HeaderRow.FindControl("lblDate2");
        //if(lbl !=null)
        //    lbl.Text = DateTime.Now.ToShortDateString();


        lblTotalSum.Text = amt.ToString("#0.00");
    }
}
