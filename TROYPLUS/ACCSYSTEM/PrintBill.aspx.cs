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

public partial class PrintBill : System.Web.UI.Page
{
    private Double amtTotal = 0;
    public Double amt = 0.0;
    public string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["Company"] != null)
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (Session["purchaseID"] != null)
            {
                if (!IsPostBack)
                {
                    hdFilename.Value = "Reports//" + System.Guid.NewGuid().ToString();
                    if (Session["SalesReturn"] != null)
                    {
                        if (Session["SalesReturn"].ToString() == "No")
                            lblHeader.Text = "Purchase Bill";
                        else
                            lblHeader.Text = "Sales Return";

                    }
                }
                //formXml();
                //BindProduct();
                BindPurchase();
                //lblDate.Text = DateTime.Now.ToShortDateString();
                //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
                BusinessLogic bl = new BusinessLogic(sDataSource);
                DataSet dsBill = new DataSet();
                dsBill = bl.GetPurchaseForId(Convert.ToInt32(Session["purchaseID"]));
                int supplierID = 0;
                if (dsBill.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in dsBill.Tables[0].Rows)
                    {

                        lblBillno.Text = Convert.ToString(dr["billno"]);
                        lblBillDate.Text = Convert.ToDateTime(dr["billdate"]).ToShortDateString();
                        //Convert.ToString(dr["creditor"]);
                        // lblReason.Text = Convert.ToString(dr["SalesReturnReason"]);
                        supplierID = Convert.ToInt32(dr["SupplierID"]);
                        lblSupplier.Text = bl.supplierName(sDataSource, supplierID);
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
                // string supplierName = lblSupplier.Text;
                DataSet dsSupplier = bl.getAddressInfo(supplierID);
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
                // lblAmtWord.Text = bl.IntegerToWords(lblTotalSum.Text); 
            }
            else
            {
                Response.Redirect("purchase.aspx");
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
    private void BindPurchase()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        int purchaseID = 0;
        DataColumn dc = new DataColumn("ProductName");
        purchaseID = Convert.ToInt32(Session["purchaseID"]);
        DataSet ds = new DataSet();
        //ds = bl.GetPurchaseItemsForId(purchaseID);

        ds = bl.GetPurchaseBill(purchaseID);


        if (ds != null)
        {
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
    }
    private void formXml()
    {
        int purchaseID = 0;
        //string sDataSource = Server.MapPath(ConfigurationSettings.AppSettings["DataSource"].ToString());
        BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = new DataSet();

        purchaseID = Convert.ToInt32(Session["purchaseID"]);
        DataSet ds = new DataSet();

        //ds = bl.GetPurchaseItemsForId(purchaseID);
        ds = bl.GetPurchaseBill(purchaseID);
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
            dsBill = bl.GetPurchaseForId(purchaseID);
            if (dsBill.Tables[0].Rows.Count == 0)
            {
                reportXMLWriter.WriteStartElement("Bill");
                reportXMLWriter.WriteElementString("Billno", String.Empty);
                reportXMLWriter.WriteElementString("BillDate", String.Empty);
                reportXMLWriter.WriteElementString("Creditor", String.Empty);

                reportXMLWriter.WriteEndElement();
            }
            else
            {
                foreach (DataRow dr in dsBill.Tables[0].Rows)
                {
                    reportXMLWriter.WriteStartElement("Bill");
                    reportXMLWriter.WriteElementString("Billno", Convert.ToString(dr["billno"]));
                    reportXMLWriter.WriteElementString("BillDate", Convert.ToString(dr["billdate"]));
                    reportXMLWriter.WriteElementString("Creditor", Convert.ToString(dr["creditor"]));
                    reportXMLWriter.WriteEndElement();
                }
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                reportXMLWriter.WriteStartElement("Product");
                //reportXMLWriter.WriteElementString("PurchaseID", String.Empty);
                //reportXMLWriter.WriteElementString("itemCode", String.Empty);
                reportXMLWriter.WriteElementString("ProductName", String.Empty);
                reportXMLWriter.WriteElementString("Qty", "0");
                reportXMLWriter.WriteElementString("Rate", "0.00");
                reportXMLWriter.WriteElementString("Total", "0.00");
                reportXMLWriter.WriteEndElement();

            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    reportXMLWriter.WriteStartElement("Product");
                    //reportXMLWriter.WriteElementString("itemCode", Convert.ToString(dr["ItemCode"]));
                    //reportXMLWriter.WriteElementString("PurchaseID", Convert.ToString(dr["PurchaseID"]));
                    reportXMLWriter.WriteElementString("ProductName", Convert.ToString(dr["ProductName"]));
                    //reportXMLWriter.WriteElementString("ProductDesc", Convert.ToString(dr["ProductDesc"]));
                    reportXMLWriter.WriteElementString("Qty", Convert.ToString(dr["Qty"]));
                    reportXMLWriter.WriteElementString("Rate", Convert.ToString(dr["Rate"]));
                    //reportXMLWriter.WriteElementString("Discount", Convert.ToString(dr["Discount"]));
                    //reportXMLWriter.WriteElementString("VAT", Convert.ToString(dr["vat"]));
                    reportXMLWriter.WriteElementString("Total", Convert.ToString(dr["Total"]));
                    reportXMLWriter.WriteEndElement();
                }
            }
            reportXMLWriter.WriteEndElement();

            reportXMLWriter.WriteEndDocument();
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sWriter.ToString());
            filename = hdFilename.Value;
            xmlDoc.Save(Server.MapPath(filename + "_ProductPrint.xml"));
        }

    }
    private void BindProduct()
    {
        string filename = string.Empty;
        filename = hdFilename.Value;
        DataSet xmlDs = new DataSet();
        if (File.Exists(Server.MapPath(filename + "_ProductPrint.xml")))
        {
            xmlDs.ReadXml(Server.MapPath(filename + "_ProductPrint.xml"));
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
            }
            File.Delete(Server.MapPath(filename + "_ProductPrint.xml"));
        }

    }
    private void calcSum()
    {
        Double sumAmt = 0;
        DataSet ds = new DataSet();
        //ds.ReadXml(Server.MapPath(hdFilename.Value + "_productprint.xml"));

        ds = (DataSet)gvBillDetails.DataSource;
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["Total"] != null)
                        sumAmt = sumAmt + Convert.ToDouble(GetTotal(Convert.ToDecimal(dr["Qty"]), Convert.ToDecimal(dr["PurchaseRate"]), Convert.ToDecimal(dr["Discount"]), Convert.ToDecimal(dr["VAT"])));
                }
            }
        }
        lblTotalSum.Text = sumAmt.ToString("#0.00");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (File.Exists(Server.MapPath(hdFilename.Value + "_productprint.xml")))
                File.Delete(Server.MapPath(hdFilename.Value + "_productprint.xml"));
            if (Session["PurchaseFileName"] != null)
            {
                if (File.Exists(Session["PurchaseFileName"].ToString()))
                    File.Delete(Session["PurchaseFileName"].ToString());

            }
            Session["NEWPURCHASE"] = "Y";
            Session["PurchaseBillDate"] = lblBillDate.Text;
            Response.Redirect("Purchase.aspx");
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
        try
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
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
