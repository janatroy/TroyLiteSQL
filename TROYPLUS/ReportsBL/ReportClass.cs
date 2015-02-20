using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Xml;

namespace ReportsBL
{
    public class ReportClass
    {

        private string CreateConnectionString(string connStr)
        {
            string connectionString = string.Empty;
            string newConnection = string.Empty;

            if (connStr.IndexOf("Provider=Microsoft.Jet.OLEDB.4.0;") > -1)
                connectionString = connStr;
            else
                connectionString = connStr; //System.Configuration.ConfigurationManager.ConnectionStrings[connStr].ConnectionString;

            newConnection = connectionString.Remove(connectionString.LastIndexOf("Password=") + 9);

            newConnection = newConnection + this.GetPasswordForDB(connectionString);

            return newConnection;

        }

        public string GetPasswordForDB(string connStr)
        {
            string connection = string.Empty;

            string encrptedString = connStr.Remove(0, connStr.Remove(connStr.LastIndexOf("Password=") + 9).Length);

            connection = Decrypt.DecryptString(encrptedString, "XY$TRO*YUHJ&*MWEE");

            return connection;

        } 

        public void generateOutStandingReport(int iGroupID, string sXmlNodeName, string sDataSource, string sXmlPath)
        {
            /* Start Variable Declaration */

            Decimal dDebitAmt, dCreditAmt, dSumDr, dSumCr, dDiffDrCr,dOutAmt,dSumDebit,dSumCredit;
            string sLedgerName = string.Empty;
            string sAliasName = string.Empty;
            string sQry = string.Empty;
            string mQry = string.Empty;
            string sConStr = string.Empty;
            int iLedgerID = 0;
            OleDbConnection oleConn ;
            OleDbCommand oleCmd,oleCmd2;
            OleDbDataAdapter oleAdp,oleAdp2;
            DataSet dsParentQry,dsChild;
           
            StringWriter sWriter;
            XmlTextWriter reportXMLWriter;
            XmlDocument xmlDoc;
            /* End Variable Declaration */

            /* Start Ms Access Database Connection Information */
            sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            if(iGroupID > 0)
                sQry = "SELECT LedgerID,LedgerName,AliasName,Debit,Credit,OpenBalanceDR,OpenBalanceCR FROM tblLedger WHERE GroupID=" + iGroupID + " AND (Debit <> 0 OR Credit <> 0 OR OpenBalanceDR <>0 OR OpenBalanceCR <> 0) ORDER BY LedgerName  ";
            else
                sQry = "SELECT LedgerID,LedgerName,AliasName,Debit,Credit,OpenBalanceDR,OpenBalanceCR FROM tblLedger WHERE (Debit <> 0 OR Credit <> 0 OR OpenBalanceDR <>0 OR OpenBalanceCR <> 0) ORDER BY LedgerName  ";
            
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */
            /* XML Forming using the records gathered in the DayBook */

            sWriter = new StringWriter();
            reportXMLWriter = new XmlTextWriter(sWriter);
            reportXMLWriter.WriteStartDocument();
            reportXMLWriter.WriteStartElement(sXmlNodeName);
            reportXMLWriter.Formatting = Formatting.Indented;

            /* Intialization of Sum of Debit and Credit and their Difference */
            dSumDr = 0;
            dSumCr = 0;
            dDiffDrCr = 0;
            dOutAmt = 0;
            dSumDebit = 0;
            dSumCredit = 0;
            bool noFlag = false;
            try
            {
                if (dsParentQry.Tables[0].Rows.Count == 0)
                {
                    /* Empty XML Formation if there is no record */
                    reportXMLWriter.WriteStartElement("Transaction");
                    reportXMLWriter.WriteElementString("LedgerName", String.Empty);
                    reportXMLWriter.WriteElementString("AliasName", String.Empty);
                    reportXMLWriter.WriteElementString("Debit", "0.00");
                    reportXMLWriter.WriteElementString("Credit", "0.00");
                    reportXMLWriter.WriteEndElement();
                    reportXMLWriter.WriteStartElement("Sumation");
                    reportXMLWriter.WriteElementString("DebitSum", String.Empty);
                    reportXMLWriter.WriteElementString("CreditSum", String.Empty);
                    reportXMLWriter.WriteElementString("DebitDiff", String.Empty);
                    reportXMLWriter.WriteElementString("CreditDiff", String.Empty);
                    reportXMLWriter.WriteEndElement();
                }
                else
                {
                    foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                    {
                        dDebitAmt = 0;
                        dCreditAmt = 0;
                        dOutAmt = 0;
                        dSumCredit = 0;
                        dSumDebit = 0;

                        if (drParentQry["LedgerName"] != null)
                        {
                            sLedgerName = drParentQry["LedgerName"].ToString();
                        }
                       
                        if (drParentQry["AliasName"] != null)
                        {
                            sAliasName = drParentQry["AliasName"].ToString();
                        }
                        if (drParentQry["LedgerID"] != null)
                        {
                            iLedgerID  = Convert.ToInt32(drParentQry["LedgerID"]);
                        }
                      
                        //if (drParentQry["Debit"] != null)
                        //{
                        //    dDebitAmt = decimal.Parse(drParentQry["Debit"].ToString(), System.Globalization.NumberStyles.Any);
                        //    //dDebitAmt = Convert.ToDecimal(drParentQry["Debit"].ToString()); 
                        //    // dSumDr = dSumDr + dDebitAmt;
                        //}
                        //if (drParentQry["Credit"] != null)
                        //{
                        //    dCreditAmt = decimal.Parse(drParentQry["Credit"].ToString(), System.Globalization.NumberStyles.Any);
                        //    //dSumCr = dSumCr + dCreditAmt;
                        //}
                        mQry = "SELECT DebtorID,CreditorID,Amount FROM tblDayBook WHERE (DebtorID=" + iLedgerID + " OR CreditorID=" + iLedgerID + ")";
                        oleCmd2 = new OleDbCommand();
                        oleCmd2.CommandText = mQry;
                        oleCmd2.CommandType = CommandType.Text;
                        oleCmd2.Connection = oleConn;
                        oleAdp2 = new OleDbDataAdapter(oleCmd2);
                        dsChild = new DataSet();
                        oleAdp2.Fill(dsChild);
                        if (dsChild.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsChild.Tables[0].Rows)
                            {
                                if (Convert.ToInt32(dr["DebtorID"].ToString()) == iLedgerID)
                                {
                                    dDebitAmt = Convert.ToDecimal(dr["Amount"].ToString());
                                    dSumDr = dSumDr + dDebitAmt;
                                    dSumDebit = dSumDebit + dDebitAmt;
                                }
                                if (Convert.ToInt32(dr["CreditorID"].ToString()) == iLedgerID)
                                {
                                    dCreditAmt = Convert.ToDecimal(dr["Amount"].ToString());
                                    dSumDr = dSumDr + dCreditAmt;
                                    dSumCredit = dSumCredit + dCreditAmt;
                                }
                            }
                        }
                       


                        if (iGroupID == 1)
                        {
                            if (drParentQry["OpenBalanceDR"] != null)
                            {
                                dOutAmt = dOutAmt + decimal.Parse(drParentQry["OpenBalanceDR"].ToString());
                            }
                        }
                        else
                        {
                            if (drParentQry["OpenBalanceCR"] != null)
                            {
                                dOutAmt = dOutAmt + decimal.Parse(drParentQry["OpenBalanceCR"].ToString());
                            }
                        }

                        dOutAmt = dOutAmt + (dSumDebit - dSumCredit);
                       
                        if (dOutAmt >= 0)
                        {
                            if (dOutAmt == 0)
                                noFlag = true;
                            else
                                noFlag = false;
                            if (noFlag == false)
                            {
                                reportXMLWriter.WriteStartElement("Transaction");
                                reportXMLWriter.WriteElementString("LedgerName", sLedgerName);
                                reportXMLWriter.WriteElementString("AliasName", sAliasName);
                                reportXMLWriter.WriteElementString("Debit", dOutAmt.ToString("f2"));
                                reportXMLWriter.WriteElementString("Credit", "0.00");
                                reportXMLWriter.WriteEndElement(); //Transaction 

                                dSumDr = dSumDr + dOutAmt;
                            }
                        }
                        else
                        {

                            reportXMLWriter.WriteStartElement("Transaction");
                            reportXMLWriter.WriteElementString("LedgerName", sLedgerName);
                            reportXMLWriter.WriteElementString("AliasName", sAliasName);
                            reportXMLWriter.WriteElementString("Credit", Math.Abs(dOutAmt).ToString("f2"));
                            reportXMLWriter.WriteElementString("Debit", "0.00"); 
                            reportXMLWriter.WriteEndElement(); //Transaction 
                            dSumCr = dSumCr + Math.Abs(dOutAmt);

                        }


                        
                           
                    }

                    reportXMLWriter.WriteStartElement("Sumation");
                    /* Start Checking the Debit Sum and Credit Sum Assigning empty to the same if it has zero value*/
                    if (dSumDr > 0)
                        reportXMLWriter.WriteElementString("DebitSum", dSumDr.ToString());
                    else
                        reportXMLWriter.WriteElementString("DebitSum", String.Empty);

                    if (dSumCr > 0)
                        reportXMLWriter.WriteElementString("CreditSum", dSumCr.ToString());
                    else
                        reportXMLWriter.WriteElementString("CreditSum", String.Empty);
                    /* End Checking the Debit Sum and Credit Sum Assigning empty to the same if it has zero value*/

                    /*Difference Calculation*/
                    dDiffDrCr = dSumDr - dSumCr;
                    /* Start - Decide whether Difference need to be displayed in the credit side or debit side */
                    if (dDiffDrCr > 0)
                    {
                        reportXMLWriter.WriteElementString("DebitDiff", dDiffDrCr.ToString());
                        reportXMLWriter.WriteElementString("CreditDiff", String.Empty);
                    }
                    else
                    {
                        reportXMLWriter.WriteElementString("DebitDiff", String.Empty);
                        reportXMLWriter.WriteElementString("CreditDiff", Math.Abs(dDiffDrCr).ToString());
                    }
                    reportXMLWriter.WriteEndElement();
                }
                reportXMLWriter.WriteEndElement();
                reportXMLWriter.WriteEndDocument();

                /* Clossing the DB Connection */
                oleConn.Close();

                /* Start Saving the Report XML to a New File */
                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(sWriter.ToString());
                xmlDoc.Save(sXmlPath);
                /* End Saving the Report XML to a New File */
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void generateSalesReport(DateTime dtSdate, DateTime dtEdate, string sXmlNodeName, string sDataSource, string sXmlPath)
        {
            /* Start Variable Declaration */
            Decimal dTotalSales, dBillTotal,dRate,dDiscount,dVAT,dSoldAmt;
            string sBillDate = string.Empty;
            string sBillNo = string.Empty;
            string sCustomerName = string.Empty;
            string sQry = string.Empty;
            string sPayMode = string.Empty;
            string sProductName = string.Empty;
            string sProductModel = string.Empty;
            string sProductDesc = string.Empty;
            string sConStr = string.Empty;
            int iQty = 0;
            //sales

            dTotalSales = 0;
            dRate = 0;
            dDiscount = 0;
            dVAT = 0;
            dSoldAmt = 0;

            /* End Variable Declaration */

            OleDbConnection oleConn, oleSubConn ;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            DataSet dsChildQry;
            DataSet dsProduct;
            StringWriter sWriter;
            XmlTextWriter reportXMLWriter;
            XmlDocument xmlDoc;
            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            
                sQry = "SELECT Billno,BillDate,Customername,paymode,purchaseReturn,purchaseReturnReason FROM tblSales WHERE   BillDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND BillDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "# Order by BillDate Desc";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */
            /* XML Forming using the records gathered in the DayBook */
            sWriter = new StringWriter();
            reportXMLWriter = new XmlTextWriter(sWriter);
            reportXMLWriter.WriteStartDocument();
            reportXMLWriter.WriteStartElement(sXmlNodeName);
            reportXMLWriter.Formatting = Formatting.Indented;
            if (dsParentQry.Tables[0].Rows.Count == 0)
            {
                /* Empty XML Formation if there is no record */
                reportXMLWriter.WriteStartElement("Bill");
                reportXMLWriter.WriteElementString("Billno", String.Empty);
                reportXMLWriter.WriteElementString("BillDate", String.Empty);
                reportXMLWriter.WriteElementString("CustomerName", String.Empty);
                reportXMLWriter.WriteStartElement("Product");
                reportXMLWriter.WriteElementString("ProductName", String.Empty);
                reportXMLWriter.WriteElementString("Model", String.Empty);
                reportXMLWriter.WriteElementString("Description", String.Empty);
                reportXMLWriter.WriteElementString("Qty", String.Empty);
                reportXMLWriter.WriteElementString("Rate", String.Empty);
                reportXMLWriter.WriteElementString("Discount", String.Empty);
                reportXMLWriter.WriteElementString("VAT", String.Empty);
                reportXMLWriter.WriteElementString("SoldAmt", String.Empty);
                reportXMLWriter.WriteEndElement();
                reportXMLWriter.WriteElementString("BillTotal", String.Empty);
                reportXMLWriter.WriteEndElement();
                reportXMLWriter.WriteStartElement("Summation");
                reportXMLWriter.WriteElementString("TotalSales", String.Empty);
                reportXMLWriter.WriteEndElement();

            }
            else
            {
                foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                {
                    dBillTotal = 0;
                    reportXMLWriter.WriteStartElement("Bill");

                    if(drParentQry["Billno"] !=null)
                    {
                        sBillNo = drParentQry["Billno"].ToString();
                        reportXMLWriter.WriteElementString("Billno", sBillNo);
                    }
                    if (drParentQry["BillDate"] != null)
                    {
                        sBillDate = Convert.ToDateTime(drParentQry["BillDate"]).ToShortDateString();
                        reportXMLWriter.WriteElementString("BillDate", sBillDate);
                    }
                    if (drParentQry["CustomerName"] != null)
                    {
                        sCustomerName = drParentQry["CustomerName"].ToString();
                        reportXMLWriter.WriteElementString("CustomerName", sCustomerName);
                    }
                    if (drParentQry["Paymode"] != null)
                    {
                        sPayMode = drParentQry["Paymode"].ToString();
                        sQry = "SELECT AliasName FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(sPayMode); 
                        oleCmd = new OleDbCommand();
                        oleCmd.CommandText = sQry;
                        oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                        oleCmd.Connection = oleSubConn;
                        oleAdp = new OleDbDataAdapter(oleCmd); 
                        dsChildQry = new DataSet();
                        oleAdp.Fill(dsChildQry);
                        sPayMode  = dsChildQry.Tables[0].Rows[0]["AliasName"].ToString();
                        reportXMLWriter.WriteElementString("Paymode", sPayMode);
                        oleSubConn.Close();
                    }
                    /* Start Getting Product Details*/
                    sQry = "SELECT itemcode,qty,rate,discount,vat FROM tblSalesItems WHERE Billno="+ Convert.ToInt32(sBillNo);
                    oleCmd = new OleDbCommand();
                    oleCmd.CommandText = sQry;
                    oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                    oleCmd.Connection = oleSubConn;
                    oleAdp = new OleDbDataAdapter(oleCmd);
                    dsChildQry = new DataSet();
                    oleAdp.Fill(dsChildQry);
                    oleSubConn.Close();
                    /*Product Details*/
                    foreach (DataRow drProduct in dsChildQry.Tables[0].Rows)
                    {
                        reportXMLWriter.WriteStartElement("Product");
                        oleCmd = new OleDbCommand();
                        sQry = "SELECT ProductName,Model,ProductDesc FROM tblProductMaster WHERE Itemcode='" +drProduct["ItemCode"].ToString()+"'";
                        oleCmd.CommandText = sQry;
                        oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                        oleCmd.Connection = oleSubConn;
                        oleAdp = new OleDbDataAdapter(oleCmd);
                        dsProduct = new DataSet();
                        oleAdp.Fill(dsProduct);
                        oleSubConn.Close();
                        if (dsProduct != null)
                        {
                            sProductName = dsProduct.Tables[0].Rows[0]["ProductName"].ToString();
                            sProductModel = dsProduct.Tables[0].Rows[0]["Model"].ToString();
                            sProductDesc = dsProduct.Tables[0].Rows[0]["ProductDesc"].ToString();
                            reportXMLWriter.WriteElementString("ProductName", sProductName);
                            reportXMLWriter.WriteElementString("Model", sProductModel);
                            reportXMLWriter.WriteElementString("Description", sProductDesc);
 
                        }
                        if (drProduct["qty"] != null)
                        {
                            iQty = Convert.ToInt32(drProduct["qty"].ToString());
                            reportXMLWriter.WriteElementString("Qty", iQty.ToString());
                        }
                        if(drProduct["rate"]!=null)
                        {
                            dRate = Convert.ToDecimal(drProduct["rate"].ToString());
                            reportXMLWriter.WriteElementString("Rate", dRate.ToString());
                        }
                        if(drProduct["discount"]!=null )
                        {
                            dDiscount = Convert.ToDecimal(drProduct["discount"].ToString());
                            reportXMLWriter.WriteElementString("Discount", dDiscount.ToString());
                        }
                        if(drProduct["VAT"]!=null)
                        {
                        dVAT = Convert.ToDecimal(drProduct["VAT"].ToString());
                        reportXMLWriter.WriteElementString("VAT", dVAT.ToString());  
                        }

                        dSoldAmt = iQty * (dRate - dDiscount + dVAT);
                        dBillTotal = dBillTotal + dSoldAmt;
                        reportXMLWriter.WriteElementString("SoldAmt", dSoldAmt.ToString());
                        reportXMLWriter.WriteEndElement(); //Product Element End 
                    } //foreach end
                    /* End Getting Product Details*/

                    reportXMLWriter.WriteElementString("BillTotal",dBillTotal.ToString());
                    reportXMLWriter.WriteEndElement(); //End Bill Element
                    dTotalSales = dTotalSales + dBillTotal;
                }
                /* Summation */
                reportXMLWriter.WriteStartElement("Summation");
                reportXMLWriter.WriteElementString("TotalSales", dTotalSales.ToString());
                reportXMLWriter.WriteEndElement();
            }
            reportXMLWriter.WriteEndElement();
            reportXMLWriter.WriteEndDocument();

            /* Clossing the DB Connection */
            oleConn.Close();

            /* Start Saving the Report XML to a New File */
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sWriter.ToString());
            xmlDoc.Save(sXmlPath);
            /* End Saving the Report XML to a New File */

       
        }
        public double GetDayBookOB(DateTime dtSdate, string sDataSource)
         {
            //SELECT Sum(Amount) FROM tblDayBook Where TransDate < #09/20/2009#
             string sConStr = string.Empty;
             string sQry = string.Empty;
             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             sConStr = sDataSource;
             oleConn = new OleDbConnection(CreateConnectionString(sConStr));
             oleCmd = new OleDbCommand();
             oleConn.Open();
             oleCmd.Connection = oleConn;
             /* End Ms Access Database Connection Information */

             /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
             
                 sQry = "SELECT Sum(Amount) FROM tblDayBook Where  TransDate <#" + dtSdate.ToString("MM/dd/yyyy") + "#";
             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;
             //oleAdp = new OleDbDataAdapter(oleCmd);
             //dsParentQry = new DataSet();
             //oleAdp.Fill(dsParentQry);
             object amtObj;

             amtObj = oleCmd.ExecuteScalar();
             double amt = 0.0;
             if (amtObj != null && amtObj != DBNull.Value)
                 amt = (double)amtObj;
             oleConn.Close();
             return amt;
        }
        public void generateDayBookReport(DateTime dtSdate, DateTime dtEdate, string sXmlNodeName, string sDataSource, string sXmlPath)
        {
            /* Start Variable Declaration */
            Decimal dDebitAmt, dCreditAmt, dSumDr, dSumCr, dDiffDrCr;
            string sDebtor = string.Empty;
            string sCreditor = string.Empty;
            string sTranDate = string.Empty;
            string iQry = "";
            string sQry = string.Empty;
            string sConStr = string.Empty;
            string sNarration = string.Empty;
            OleDbConnection oleConn, oleSubConn; ;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            DataSet dsChildQry;
            StringWriter sWriter;
            XmlTextWriter reportXMLWriter;
            XmlDocument xmlDoc;
            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry = "SELECT TransDate,DebtorID,CreditorID,Amount,Narration FROM tblDayBook WHERE  (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) Order by TransDate Desc";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            /* XML Forming using the records gathered in the DayBook */

            sWriter = new StringWriter();
            reportXMLWriter = new XmlTextWriter(sWriter);
            reportXMLWriter.WriteStartDocument();
            reportXMLWriter.WriteStartElement(sXmlNodeName);
            reportXMLWriter.Formatting = Formatting.Indented;

            /* Intialization of Sum of Debit and Credit and their Difference */
            dSumDr = 0;
            dSumCr = 0;
            dDiffDrCr = 0;

            if (dsParentQry.Tables[0].Rows.Count == 0)
            {
                /* Empty XML Formation if there is no record */
                reportXMLWriter.WriteStartElement("Transaction");
                reportXMLWriter.WriteElementString("TransDate", String.Empty);
                reportXMLWriter.WriteElementString("Narration", String.Empty);
                reportXMLWriter.WriteElementString("Debit", "0.00");
                reportXMLWriter.WriteElementString("Credit", "0.00");
                reportXMLWriter.WriteElementString("Debitor", String.Empty);
                reportXMLWriter.WriteElementString("Creditor", String.Empty);

                reportXMLWriter.WriteEndElement();
                reportXMLWriter.WriteStartElement("Sumation");
                reportXMLWriter.WriteElementString("DebitSum", String.Empty);
                reportXMLWriter.WriteElementString("CreditSum", String.Empty);
                reportXMLWriter.WriteElementString("DebitDiff", String.Empty);
                reportXMLWriter.WriteElementString("CreditDiff", String.Empty);
                reportXMLWriter.WriteEndElement();
            }
            else
            {
                foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                {
                    dDebitAmt = 0;
                    dCreditAmt = 0;
                    reportXMLWriter.WriteStartElement("Transaction");
                    if (drParentQry["TransDate"] != null)
                    {
                        sTranDate = Convert.ToDateTime(drParentQry["TransDate"].ToString()).ToShortDateString();
                    }

                    reportXMLWriter.WriteElementString("TransDate", sTranDate);
                    if(drParentQry["Narration"]!=null)
                    {
                        sNarration = "(" + drParentQry["Narration"].ToString() + ")";
                    }
                    reportXMLWriter.WriteElementString("Narration", sNarration);
                    /* Start Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/
                    if(drParentQry["CreditorID"] != null)
                    {
                            dDebitAmt = Convert.ToDecimal(drParentQry["Amount"].ToString());
                            iQry = "SELECT Ledgername FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["CreditorID"].ToString());
                            dSumCr = dSumCr + dCreditAmt;
                            oleCmd = new OleDbCommand();
                            oleCmd.CommandText = iQry;
                            oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                            oleCmd.Connection = oleSubConn;
                            oleAdp = new OleDbDataAdapter(oleCmd);
                            dsChildQry = new DataSet();
                            oleAdp.Fill(dsChildQry);
                            if (dsChildQry != null)
                            {
                                if (dsChildQry.Tables[0].Rows.Count > 0)
                                    sCreditor = dsChildQry.Tables[0].Rows[0]["ledgername"].ToString();
                                else
                                    sCreditor = "";
                            }
                            else
                            {
                                sCreditor = "";
                            }
                            oleSubConn.Close();
                    }
                    if (drParentQry["DebtorID"] != null)
                    {
                        
                            dCreditAmt = Convert.ToDecimal(drParentQry["Amount"].ToString());
                            iQry = "SELECT Ledgername  FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["DebtorID"].ToString());
                            dSumDr = dSumDr + dDebitAmt;
                            oleCmd = new OleDbCommand();
                            oleCmd.CommandText = iQry;
                            oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                            oleCmd.Connection = oleSubConn;
                            oleAdp = new OleDbDataAdapter(oleCmd);
                            dsChildQry = new DataSet();
                            oleAdp.Fill(dsChildQry);
                            if (dsChildQry != null)
                            {
                                if (dsChildQry.Tables[0].Rows.Count > 0)
                                    sDebtor = dsChildQry.Tables[0].Rows[0]["ledgername"].ToString();
                                else
                                    sDebtor = "";
                            }
                            else
                            {
                                sDebtor = "";
                            }
                            oleSubConn.Close();
                    }
                    /* End Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/
                    reportXMLWriter.WriteElementString("Debitor", sDebtor);
                    reportXMLWriter.WriteElementString("Creditor", sCreditor);
                    reportXMLWriter.WriteElementString("Debit", dDebitAmt.ToString());
                    reportXMLWriter.WriteElementString("Credit", dCreditAmt.ToString());

                    reportXMLWriter.WriteEndElement();
                   
                }//End foreach


                reportXMLWriter.WriteStartElement("Sumation");
                /* Start Checking the Debit Sum and Credit Sum Assigning empty to the same if it has zero value*/
                if (dSumDr > 0)
                    reportXMLWriter.WriteElementString("DebitSum", dSumDr.ToString());
                else
                    reportXMLWriter.WriteElementString("DebitSum", String.Empty);

                if (dSumCr > 0)
                    reportXMLWriter.WriteElementString("CreditSum", dSumCr.ToString());
                else
                    reportXMLWriter.WriteElementString("CreditSum", String.Empty);
                /* End Checking the Debit Sum and Credit Sum Assigning empty to the same if it has zero value*/

                /*Difference Calculation*/
                dDiffDrCr = dSumDr - dSumCr;
                /* Start - Decide whether Difference need to be displayed in the credit side or debit side */
                if (dDiffDrCr > 0)
                {
                    reportXMLWriter.WriteElementString("DebitDiff",  dDiffDrCr.ToString());
                    reportXMLWriter.WriteElementString("CreditDiff", String.Empty);
                }
                else
                {
                    reportXMLWriter.WriteElementString("DebitDiff", String.Empty);
                    reportXMLWriter.WriteElementString("CreditDiff", Math.Abs(dDiffDrCr).ToString());
                }
                reportXMLWriter.WriteEndElement();
            }
           
            reportXMLWriter.WriteEndElement();
            reportXMLWriter.WriteEndDocument();

            /* Clossing the DB Connection */
            oleConn.Close();

            /* Start Saving the Report XML to a New File */
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sWriter.ToString());
            xmlDoc.Save(sXmlPath);
            /* End Saving the Report XML to a New File */
        }

        public void generateQtyReturnReport(int LedgerID, string sXmlNodeName, string sDataSource, string sXmlPath)
        {
            /* Start Variable Declaration */
            Decimal dSumQty, dQty;
            string sQtySale = string.Empty;
            string sQtyReturn = string.Empty;
            string sQtyPending = string.Empty;
            string sComments = string.Empty;
            string sBillDate = string.Empty;
            string iQry = "";
            string sQry = string.Empty;
            string sConStr = string.Empty;
            string sBillNo = string.Empty;
            OleDbConnection oleConn, oleSubConn; ;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            DataSet dsChildQry;
            StringWriter sWriter;
            XmlTextWriter reportXMLWriter;
            XmlDocument xmlDoc;
            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            //sQry = "SELECT 'RETURN' as Type,DateEntered as BillDate,- Qty as Qty,Comments FROM tblQtyReturn WHERE DateEntered >= (Select Format(KeyValue,'MM/dd/yyyy') From tblSettings Where Key = 'QTYDATE') And  LedgerID=" + LedgerID.ToString();
            //sQry = sQry + " UNION ALL ";
            //sQry = sQry + "SELECT 'SALE' as Type,s.BillDate,si.Qty,'Itemcode :' + si.ItemCode as Comments FROM tblSales s inner join tblSalesItems si on s.BillNo = si.BillNo Where s.BillDate >= (Select Format(KeyValue,'MM/dd/yyyy') From tblSettings Where Key = 'QTYDATE') And s.CustomerID = " + LedgerID.ToString();
            //sQry = sQry + " Order By BillDate Asc";

            sQry = "Select SUM(Qty) From tblQtyReturn Where LedgerID=" + LedgerID.ToString();
            oleConn.Open();
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            object objSumQtyRet = oleCmd.ExecuteScalar();
            double SumQtyRet = 0;

            if (objSumQtyRet.ToString() != "")
                SumQtyRet = double.Parse(objSumQtyRet.ToString());

            sQry = "SELECT s.BillNo,s.BillDate,si.Qty as QtySale FROM tblSales s Inner Join tblSalesItems si on s.BillNo = si.BillNo Where si.ItemCode = (Select KEYVALUE From tblSettings Where KEY='ITEMCODE') And s.BillDate >= (Select Format(KeyValue,'MM/dd/yyyy') From tblSettings Where Key = 'QTYDATE') And s.CustomerID = " + LedgerID.ToString();

            oleCmd.CommandText = sQry;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            /* XML Forming using the records gathered in the DayBook */

            sWriter = new StringWriter();
            reportXMLWriter = new XmlTextWriter(sWriter);
            reportXMLWriter.WriteStartDocument();
            reportXMLWriter.WriteStartElement(sXmlNodeName);
            reportXMLWriter.Formatting = Formatting.Indented;

            /* Intialization of Sum of Debit and Credit and their Difference */
            dSumQty = 0;
            dQty = 0;

            if (SumQtyRet < 0)
            {
                /* Empty XML Formation if there is no record */
                reportXMLWriter.WriteStartElement("Transaction");
                reportXMLWriter.WriteElementString("BillNo", "Open Balance");
                reportXMLWriter.WriteElementString("BillDate", String.Empty);
                reportXMLWriter.WriteElementString("QtySale", String.Empty);
                reportXMLWriter.WriteElementString("QtyReturn", String.Empty);
                //reportXMLWriter.WriteElementString("QtyPending", String.Empty);
                reportXMLWriter.WriteElementString("QtyPending", (-SumQtyRet).ToString());
                reportXMLWriter.WriteElementString("Comments", String.Empty);

                reportXMLWriter.WriteEndElement();

            }

            if ((dsParentQry.Tables[0].Rows.Count == 0) && (SumQtyRet > 0))
            {
                /* Empty XML Formation if there is no record */
                reportXMLWriter.WriteStartElement("Transaction");
                reportXMLWriter.WriteElementString("BillNo", String.Empty);
                reportXMLWriter.WriteElementString("BillDate", String.Empty);
                reportXMLWriter.WriteElementString("QtySale", String.Empty);
                if (SumQtyRet > 0)
                {
                    reportXMLWriter.WriteElementString("QtyReturn", SumQtyRet.ToString());
                    reportXMLWriter.WriteElementString("QtyPending", String.Empty);
                }
                else
                {
                    reportXMLWriter.WriteElementString("QtyReturn", String.Empty);
                    reportXMLWriter.WriteElementString("QtyPending", (-SumQtyRet).ToString());
                }
                //reportXMLWriter.WriteElementString("QtyPending", SumQtyRet.ToString());
                reportXMLWriter.WriteElementString("Comments", String.Empty);

                reportXMLWriter.WriteEndElement();
                reportXMLWriter.WriteStartElement("Total");
                reportXMLWriter.WriteElementString("SumSale", String.Empty);
                reportXMLWriter.WriteElementString("SumReturned", String.Empty);
                reportXMLWriter.WriteElementString("SumPending", String.Empty);
                reportXMLWriter.WriteEndElement();
            }
            else if ((dsParentQry.Tables[0].Rows.Count == 0) && (SumQtyRet == 0.0))
            {
                reportXMLWriter.WriteStartElement("Transaction");
                reportXMLWriter.WriteElementString("BillNo", string.Empty);
                reportXMLWriter.WriteElementString("BillDate", String.Empty);
                reportXMLWriter.WriteElementString("QtySale", String.Empty);
                reportXMLWriter.WriteElementString("QtyReturn", String.Empty);
                //reportXMLWriter.WriteElementString("QtyPending", String.Empty);
                reportXMLWriter.WriteElementString("QtyPending", string.Empty);
                reportXMLWriter.WriteElementString("Comments", String.Empty);

                reportXMLWriter.WriteEndElement();

            }
            else
            {
                int rowCount = 0;

                foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                {
                    reportXMLWriter.WriteStartElement("Transaction");

                    if (drParentQry["BillNo"] != null)
                    {
                        sBillNo = drParentQry["BillNo"].ToString();
                    }

                    reportXMLWriter.WriteElementString("BillNo", sBillNo);

                    if (drParentQry["BillDate"] != null)
                    {
                        sBillDate = Convert.ToDateTime(drParentQry["BillDate"].ToString()).ToShortDateString();
                    }

                    reportXMLWriter.WriteElementString("BillDate", sBillDate);

                    if (drParentQry["QtySale"] != null)
                    {
                        sQtySale = drParentQry["QtySale"].ToString();
                    }

                    reportXMLWriter.WriteElementString("QtySale", sQtySale);

                    rowCount++;

                    double QtyReturn = 0;
                    double prevSum = 0;

                    if (SumQtyRet > 0)
                    {
                        if (drParentQry["QtySale"] != null)
                        {
                            prevSum = SumQtyRet;
                            if (SumQtyRet > 0)
                                SumQtyRet = SumQtyRet - double.Parse(drParentQry["QtySale"].ToString());
                        }

                        if (SumQtyRet == 0 && (prevSum == 0))
                        {
                            sQtyPending = "0";
                            sQtyReturn = "0";
                        }
                        else if (SumQtyRet > 0 && (rowCount != dsParentQry.Tables[0].Rows.Count))
                        {
                            sQtyPending = "0";
                            sQtyReturn = drParentQry["QtySale"].ToString();
                        }
                        else if (SumQtyRet > 0 && (rowCount == dsParentQry.Tables[0].Rows.Count))
                        {
                            sQtyPending = (-SumQtyRet).ToString();
                            sQtyReturn = drParentQry["QtySale"].ToString();
                        }
                        else
                        {
                            sQtyReturn = prevSum.ToString();
                            sQtyPending = (-SumQtyRet).ToString();
                            SumQtyRet = 0;
                        }

                        //SumQtyRet = SumQtyRet - double.Parse(drParentQry["QtySale"].ToString());

                        reportXMLWriter.WriteElementString("QtyReturn", sQtyReturn);
                        reportXMLWriter.WriteElementString("QtyPending", sQtyPending);

                    }
                    else if (SumQtyRet == 0)
                    {
                        reportXMLWriter.WriteElementString("QtyReturn", SumQtyRet.ToString());
                        reportXMLWriter.WriteElementString("QtyPending", drParentQry["QtySale"].ToString());
                    }
                    else
                    {
                        //if (drParentQry["QtySale"] != null)
                        //{
                        //    prevSum = SumQtyRet;
                        //    if(SumQtyRet < 0)
                        //        SumQtyRet = SumQtyRet - double.Parse(drParentQry["QtySale"].ToString());
                        //}

                        /*
                        if ((SumQtyRet == 0) && (prevSum == 0))
                        {
                            sQtyPending = "0";
                            sQtyReturn = "0";
                        }
                        else if (SumQtyRet < 0 && (rowCount != dsParentQry.Tables[0].Rows.Count))
                        {
                            sQtyPending = "0";
                            //sQtyReturn = drParentQry["QtySale"].ToString();
                            sQtyReturn = "0";
                        }
                        else if (SumQtyRet < 0 && (rowCount == dsParentQry.Tables[0].Rows.Count))
                        {
                            //sQtyReturn = drParentQry["QtySale"].ToString();
                            sQtyReturn = "0";
                            sQtyPending = (- SumQtyRet).ToString();
                        }
                        else
                        {
                            if (prevSum >=0 )
                                sQtyReturn = prevSum.ToString();
                            else
                                sQtyReturn = (- prevSum).ToString();

                            if(SumQtyRet > 0)
                                sQtyPending = (SumQtyRet).ToString();
                            else
                                sQtyPending = (- SumQtyRet).ToString();

                            SumQtyRet = 0;
                        }

                        //SumQtyRet = SumQtyRet - double.Parse(drParentQry["QtySale"].ToString());
                         */

                        reportXMLWriter.WriteElementString("QtyReturn", "0");
                        reportXMLWriter.WriteElementString("QtyPending", drParentQry["QtySale"].ToString());
                    }


                    /* End Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/

                    reportXMLWriter.WriteEndElement();

                }//End foreach


                reportXMLWriter.WriteStartElement("Sumation");
                /* Start Checking the Debit Sum and Credit Sum Assigning empty to the same if it has zero value*/
                //if (dSumQty > 0)
                //    reportXMLWriter.WriteElementString("SumSales", dSumQty.ToString());
                //else
                reportXMLWriter.WriteElementString("SumSale", String.Empty);

                reportXMLWriter.WriteEndElement();
            }

            reportXMLWriter.WriteEndElement();
            reportXMLWriter.WriteEndDocument();

            /* Clossing the DB Connection */
            oleConn.Close();

            /* Start Saving the Report XML to a New File */
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sWriter.ToString());
            xmlDoc.Save(sXmlPath);
            /* End Saving the Report XML to a New File */
        }

        public void generateReportXML(int iLedgerID, DateTime dtSdate, DateTime dtEdate, string sXmlNodeName,string sDataSource,string sXmlPath)
        {
            /* Start Variable Declaration */

            Decimal dDebitAmt, dCreditAmt, dSumDr, dSumCr, dDiffDrCr, dOpenBalaceDR, dOpenBalanceCR, dOpenBalanceDiffDR, dOpenBalanceDiffCR;
            string sTranDate = string.Empty;
            string iQry = "";
            string sParticulars = "";
            string sQry = string.Empty;
            string pQry = string.Empty;
            string sConStr = string.Empty;
            OleDbConnection oleConn, oleSubConn; 

            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            DataSet dsChildQry;
            StringWriter sWriter;
            XmlTextWriter reportXMLWriter;
            XmlDocument xmlDoc;

            /* End Variable Declaration */

            /* Start Ms Access Database Connection Information */
            sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */

            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry = "SELECT TransDate,DebtorID,CreditorID,Amount,Narration FROM tblDayBook WHERE (DebtorID=" + iLedgerID + " OR CreditorID=" + iLedgerID + ") AND (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) Order by TransDate Desc";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            /* XML Forming using the records gathered in the DayBook */

            sWriter = new StringWriter();
            reportXMLWriter = new XmlTextWriter(sWriter);
            reportXMLWriter.WriteStartDocument();
            reportXMLWriter.WriteStartElement(sXmlNodeName);
            reportXMLWriter.Formatting = Formatting.Indented;

            /* Intialization of Sum of Debit and Credit and their Difference */
            dSumDr = 0;
            dSumCr = 0;
            dDiffDrCr = 0;
            dOpenBalaceDR = 0;
            dOpenBalanceCR = 0;
            dOpenBalanceDiffDR = 0;
            dOpenBalanceDiffCR = 0;

            if (dsParentQry.Tables[0].Rows.Count == 0)
            {
                /* Empty XML Formation if there is no record */
                reportXMLWriter.WriteStartElement("Transaction");
                reportXMLWriter.WriteElementString("TransDate", String.Empty);
                reportXMLWriter.WriteElementString("Particular", String.Empty);
                reportXMLWriter.WriteElementString("Debit", "0.00");
                reportXMLWriter.WriteElementString("Credit", "0.00");
                //if (sXmlNodeName == "LedgerAccount")
                //{
                //    reportXMLWriter.WriteElementString("OpeningBalanceDR", "0.00");
                //    reportXMLWriter.WriteElementString("OpeningBalanceCR", String.Empty);
                //    //reportXMLWriter.WriteElementString("OpeningBalanceDiffDr", String.Empty);
                //    //reportXMLWriter.WriteElementString("OpeningBalanceDiffCr", String.Empty);
                //}
                reportXMLWriter.WriteEndElement();
                reportXMLWriter.WriteStartElement("Sumation");
                reportXMLWriter.WriteElementString("DebitSum", "0.00");
                reportXMLWriter.WriteElementString("CreditSum", "0.00");
                reportXMLWriter.WriteElementString("DebitDiff", String.Empty);
                reportXMLWriter.WriteElementString("CreditDiff", String.Empty);
               
                reportXMLWriter.WriteEndElement();
            }
            else
            {
                /* Iterating through the records and forming the custom datamodel and write into XML file */

                foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                {
                    dDebitAmt = 0;
                    dCreditAmt = 0;

                    reportXMLWriter.WriteStartElement("Transaction");
                    if (drParentQry["TransDate"] != null)
                    {
                        sTranDate = Convert.ToDateTime(drParentQry["TransDate"].ToString()).ToShortDateString();
                    }
                    reportXMLWriter.WriteElementString("TransDate", sTranDate);
                    /* Start Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/
                    if (drParentQry["DebtorID"] != null)
                    {
                        if (Convert.ToInt32(drParentQry["DebtorID"].ToString()) == iLedgerID)
                        {
                            dDebitAmt = Convert.ToDecimal(drParentQry["Amount"].ToString());
                            //if (sXmlNodeName == "LedgerAccount")
                            //    iQry = "SELECT Ledgername,OpenBalanceDR,OpenBalanceCR FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["DebtorID"].ToString());
                            //else
                            //    iQry = "SELECT Ledgername,OpenBalanceDR,OpenBalanceCR FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["CreditorID"].ToString());
                            dSumDr = dSumDr + dDebitAmt;

                            pQry = "SELECT Ledgername FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["CreditorID"].ToString()); 
                        }
                    }
                    if (drParentQry["CreditorID"] != null)
                    {
                        if (Convert.ToInt32(drParentQry["CreditorID"].ToString()) == iLedgerID)
                        {
                           
                            dCreditAmt = Convert.ToDecimal(drParentQry["Amount"].ToString());
                            //if (sXmlNodeName == "LedgerAccount")
                            //    iQry = "SELECT Ledgername,OpenBalanceDR,OpenBalanceCR  FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["CreditorID"].ToString());
                            //else
                            //    iQry = "SELECT Ledgername,OpenBalanceDR,OpenBalanceCR  FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["DebtorID"].ToString());
                            dSumCr = dSumCr + dCreditAmt;
                            pQry = "SELECT Ledgername FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["DebtorID"].ToString()); 
                        }
                    }
                    /* End Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/

                    /* Start Getting the Particulars Information for the Ledger ID */
                    //if (iQry != "")
                    //{
                    //    oleCmd = new OleDbCommand();
                    //    oleCmd.CommandText = iQry;
                    //    oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                    //    oleCmd.Connection = oleSubConn;
                    //    oleAdp = new OleDbDataAdapter(oleCmd);
                    //    dsChildQry = new DataSet();
                    //    oleAdp.Fill(dsChildQry);
                    //    //sParticulars = dsChildQry.Tables[0].Rows[0]["ledgername"].ToString() + " " + drParentQry["Narration"].ToString();
                    //    if (sXmlNodeName == "LedgerAccount")
                    //    {
                    //        //dOpenBalanceDiffDR = Convert.ToDecimal(dsChildQry.Tables[0].Rows[0]["OpenBalanceDR"]) - Convert.ToDecimal(dsChildQry.Tables[0].Rows[0]["OpenBalanceCR"]);
                    //        //if(dOpenBalanceDiffDR < 0)
                    //        //    dOpenBalanceDiffCR = Math.Abs(dOpenBalanceDiffDR);

                    //        dOpenBalaceDR = Convert.ToDecimal(dsChildQry.Tables[0].Rows[0]["OpenBalanceDR"]);
                    //        dOpenBalanceCR = Convert.ToDecimal(dsChildQry.Tables[0].Rows[0]["OpenBalanceCR"]);
                    //    }
                        
                    //     oleSubConn.Close();
                       
                    //}
                    if (pQry != "")
                    {
                        oleCmd = new OleDbCommand();
                        oleCmd.CommandText = pQry;
                        oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                        oleCmd.Connection = oleSubConn;
                        oleAdp = new OleDbDataAdapter(oleCmd);
                        dsChildQry = new DataSet();
                        oleAdp.Fill(dsChildQry);
                        sParticulars = dsChildQry.Tables[0].Rows[0]["ledgername"].ToString() + " " + drParentQry["Narration"].ToString();
                        oleSubConn.Close();
                    }
                    /* Particularly for ledger Account opening balance*/
                   
                       
                    /* Writing Particular information into XML file*/
                    reportXMLWriter.WriteElementString("Particular", sParticulars);
                    /* End Getting the Particulars Information for the Ledger ID */

                    /* Start Checking the Debit  and Credit Assigning empty to the same if it has zero value*/
                    if (dDebitAmt > 0)
                        reportXMLWriter.WriteElementString("Debit", dDebitAmt.ToString());
                    else
                        reportXMLWriter.WriteElementString("Debit", "0.00");

                    if (dCreditAmt > 0)
                        reportXMLWriter.WriteElementString("Credit", dCreditAmt.ToString());
                    else
                        reportXMLWriter.WriteElementString("Credit", "0.00");
                    //if (sXmlNodeName == "LedgerAccount")
                    //{
                    //    if (dOpenBalaceDR >= 0)
                    //    {
                    //        if (dOpenBalaceDR == 0)
                    //            reportXMLWriter.WriteElementString("OpeningBalanceDR", "0.00");
                    //        else
                    //            reportXMLWriter.WriteElementString("OpeningBalanceDR", dOpenBalaceDR.ToString());
                    //        reportXMLWriter.WriteElementString("OpeningBalanceCR", string.Empty);
                    //    }
                    //    if (dOpenBalanceCR > 0)
                    //    {
                    //        reportXMLWriter.WriteElementString("OpeningBalanceDR", string.Empty);
                    //        reportXMLWriter.WriteElementString("OpeningBalanceCR", dOpenBalanceCR.ToString());
                    //    }
                    //    //reportXMLWriter.WriteElementString("OpeningBalanceDiffDr", dOpenBalanceDiffDR.ToString());
                    //    //reportXMLWriter.WriteElementString("OpeningBalanceDiffCr", dOpenBalanceDiffCR.ToString());

                    //}
                    reportXMLWriter.WriteEndElement();
                    /* End Checking the Debit Sum and Credit Sum Assigning empty to the same if it has zero value*/

                }

                reportXMLWriter.WriteStartElement("Sumation");
                /* Start Checking the Debit Sum and Credit Sum Assigning empty to the same if it has zero value*/
                if (dSumDr > 0)
                    reportXMLWriter.WriteElementString("DebitSum", dSumDr.ToString());
                else
                    reportXMLWriter.WriteElementString("DebitSum", String.Empty);

                if (dSumCr > 0)
                    reportXMLWriter.WriteElementString("CreditSum", dSumCr.ToString());
                else
                    reportXMLWriter.WriteElementString("CreditSum", String.Empty);
                /* End Checking the Debit Sum and Credit Sum Assigning empty to the same if it has zero value*/

                /*Difference Calculation*/
                dDiffDrCr = dSumDr - dSumCr;
                /* Start - Decide whether Difference need to be displayed in the credit side or debit side */
                if (dDiffDrCr > 0)
                {
                    reportXMLWriter.WriteElementString("DebitDiff",  dDiffDrCr.ToString());
                    reportXMLWriter.WriteElementString("CreditDiff", String.Empty);
                }
                else
                {
                    reportXMLWriter.WriteElementString("DebitDiff", String.Empty);
                    reportXMLWriter.WriteElementString("CreditDiff", Math.Abs(dDiffDrCr).ToString());
                }
                /* Start particular for ledger Account report */
                //if (sXmlNodeName == "LedgerAccount")
                //{
                //    if (dOpenBalaceDR  >= 0)
                //    {
                //        if (dOpenBalaceDR == 0)
                //            reportXMLWriter.WriteElementString("OpeningBalanceDR", "0.00");
                //        else                            
                //            reportXMLWriter.WriteElementString("OpeningBalanceDR", dOpenBalaceDR.ToString());
                //        reportXMLWriter.WriteElementString("OpeningBalanceCR", string.Empty);
                //    }
                //    if (dOpenBalanceCR > 0)
                //    {
                //        reportXMLWriter.WriteElementString("OpeningBalanceDR", string.Empty);
                //        reportXMLWriter.WriteElementString("OpeningBalanceCR", dOpenBalanceCR.ToString());
                //    }
                //    //reportXMLWriter.WriteElementString("OpeningBalanceDiffDr", dOpenBalanceDiffDR.ToString());
                //    //reportXMLWriter.WriteElementString("OpeningBalanceDiffCr", dOpenBalanceDiffCR.ToString());

                //}
                /* End particular for ledger Account report */

                /* End - Decide whether Difference need to be displayed in the credit side or debit side */
                reportXMLWriter.WriteEndElement();

            }
            reportXMLWriter.WriteEndElement();
            reportXMLWriter.WriteEndDocument();

            /* Clossing the DB Connection */
            oleConn.Close();

            /* Start Saving the Report XML to a New File */
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sWriter.ToString());
            xmlDoc.Save(sXmlPath);
            /* End Saving the Report XML to a New File */


        }



        public DataSet generateOutStandingReportDS(int iGroupID, string sDataSource)
        {
            Decimal temp_balance;
            string sLedgerName = string.Empty, sConStr = string.Empty, sAliasName = string.Empty, sQry = string.Empty;
            string sLedgerId = string.Empty;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            sQry = "SELECT tblLedger.LedgerID,tblLedger.LedgerName,tblLedger.AliasName, (IIF(ISNULL(tblLedger.OpenBalanceDR),0,tblLedger.OpenBalanceDR)+ IIF(ISNULL(debittable.debitamount),0,debittable.debitamount)) - (IIF(ISNULL(tblLedger.OpenBalanceCR),0,tblLedger.OpenBalanceCR)+ IIF(ISNULL(credittable.creditamount),0,credittable.creditamount)) as balance FROM (tblLedger   left  join (SELECT DebtorID,sum(Amount) as debitamount FROM tblDayBook WHERE DebtorID > 0 group by DebtorID) debittable  on tblLedger.LedgerID=debittable.DebtorID) left join (SELECT CreditorID,sum(Amount) as creditamount FROM tblDayBook WHERE CreditorID > 0 group by CreditorID) credittable on tblLedger.LedgerID= credittable.CreditorID where GroupID=" + iGroupID + " and (IIF(ISNULL(tblLedger.OpenBalanceDR),0,tblLedger.OpenBalanceDR)+ IIF(ISNULL(debittable.debitamount),0,debittable.debitamount)) - (IIF(ISNULL(tblLedger.OpenBalanceCR),0,tblLedger.OpenBalanceCR)+ IIF(ISNULL(credittable.creditamount),0,credittable.creditamount)) <> 0 ORDER BY tblLedger.LedgerName";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            temp_balance = 0;
            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;
            ds = new DataSet();
            dt = new DataTable();
            dc = new DataColumn("LedgerName");
            dt.Columns.Add(dc);
            dc = new DataColumn("LedgerID");
            dt.Columns.Add(dc);
            dc = new DataColumn("AliasName");
            dt.Columns.Add(dc);
            dc = new DataColumn("Debit");
            dt.Columns.Add(dc);
            dc = new DataColumn("Credit");
            dt.Columns.Add(dc);
            ds.Tables.Add(dt);
            try
            {
                if (dsParentQry.Tables[0].Rows.Count == 0)
                {
                    drNew = dt.NewRow();
                    drNew["LedgerName"] = string.Empty;
                    drNew["AliasName"] = string.Empty;
                    drNew["LedgerID"] = string.Empty;
                    drNew["Debit"] = "0.00";
                    drNew["Credit"] = "0.00";
                    ds.Tables[0].Rows.Add(drNew);
                }
                else
                {
                    foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                    {
                        if (drParentQry["LedgerName"] != null)
                            sLedgerName = drParentQry["LedgerName"].ToString();
                        if (drParentQry["AliasName"] != null)
                            sAliasName = drParentQry["AliasName"].ToString();
                        if (drParentQry["LedgerID"] != null)
                            sLedgerId = drParentQry["LedgerID"].ToString();
                        if ((drParentQry["balance"] != null) && (drParentQry["balance"].ToString() != ""))
                            temp_balance = decimal.Parse(drParentQry["balance"].ToString(), System.Globalization.NumberStyles.Float);
                        else
                            temp_balance = 0;
                        if (temp_balance > 0)
                        {
                            drNew = dt.NewRow();
                            drNew["LedgerName"] = sLedgerName;
                            drNew["AliasName"] = sAliasName;
                            drNew["LedgerID"] = sLedgerId;
                            drNew["Debit"] = temp_balance;
                            drNew["Credit"] = "0.00";
                            ds.Tables[0].Rows.Add(drNew);
                        }
                        else
                        {
                            drNew = dt.NewRow();
                            drNew["LedgerName"] = sLedgerName;
                            drNew["AliasName"] = sAliasName;
                            drNew["LedgerID"] = sLedgerId;
                            drNew["Debit"] = "0.00";
                            drNew["Credit"] = Math.Abs(temp_balance).ToString(); /* convert the negative to positive */
                            ds.Tables[0].Rows.Add(drNew);
                        }
                    }
                }
                oleConn.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet generateOutStandingReportDS(int iGroupID, int ExecID, string sDataSource)
        {
            /* Start Variable Declaration */

            double dDebitAmt, dCreditAmt, dSumDr, dSumCr, dDiffDrCr, dOutAmt, dSumDebit, dSumCredit;
            string sLedgerName = string.Empty;
            string sLedgerID = string.Empty;
            double dOB = 0;
            string sAliasName = string.Empty;
            string sQry = string.Empty;
            string mQry = string.Empty;
            string sConStr = string.Empty;
            int iLedgerID = 0;
            OleDbConnection oleConn;
            OleDbCommand oleCmd, oleCmd2;
            OleDbDataAdapter oleAdp, oleAdp2;
            DataSet dsParentQry, dsChild;


            /* End Variable Declaration */

            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection( CreateConnectionString( sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            if (iGroupID > 0)
                sQry = "SELECT LedgerID,LedgerName,AliasName,Debit,Credit,OpenBalanceDR,OpenBalanceCR FROM tblLedger WHERE ExecutiveINcharge =" + ExecID + " AND GroupID=" + iGroupID + " AND (Debit <> 0 OR Credit <> 0 OR OpenBalanceDR <>0 OR OpenBalanceCR <> 0) ORDER BY LedgerName  ";
            else
                sQry = "SELECT LedgerID,LedgerName,AliasName,Debit,Credit,OpenBalanceDR,OpenBalanceCR FROM tblLedger WHERE ExecutiveINcharge =" + ExecID + " AND (Debit <> 0 OR Credit <> 0 OR OpenBalanceDR <>0 OR OpenBalanceCR <> 0) ORDER BY LedgerName  ";

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */




            /* Intialization of Sum of Debit and Credit and their Difference */
            dSumDr = 0;
            dSumCr = 0;
            dDiffDrCr = 0;
            dOutAmt = 0;
            dSumDebit = 0;
            dSumCredit = 0;
            bool noFlag = false;
            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;


            ds = new DataSet();
            dt = new DataTable();
            dc = new DataColumn("LedgerID");
            dt.Columns.Add(dc);
            dc = new DataColumn("LedgerName");
            dt.Columns.Add(dc);

            dc = new DataColumn("AliasName");
            dt.Columns.Add(dc);

            dc = new DataColumn("Debit");
            dt.Columns.Add(dc);


            dc = new DataColumn("Credit");
            dt.Columns.Add(dc);
            dc = new DataColumn("OB");
            dt.Columns.Add(dc);



            ds.Tables.Add(dt);
            try
            {
                if (dsParentQry.Tables[0].Rows.Count == 0)
                {
                    /* Empty XML Formation if there is no record */


                    drNew = dt.NewRow();
                    drNew["LedgerID"] = string.Empty;
                    drNew["LedgerName"] = string.Empty;
                    drNew["AliasName"] = string.Empty;
                    drNew["Debit"] = "0.00";
                    drNew["Credit"] = "0.00";
                    drNew["OB"] = "0.00";
                    ds.Tables[0].Rows.Add(drNew);

                }
                else
                {
                    foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                    {
                        dDebitAmt = 0;
                        dCreditAmt = 0;
                        dOutAmt = 0;
                        dSumCredit = 0;
                        dSumDebit = 0;

                        if (drParentQry["LedgerName"] != null)
                        {
                            sLedgerName = drParentQry["LedgerName"].ToString();
                        }
                        if (drParentQry["LedgerID"] != null)
                        {
                            sLedgerID = drParentQry["LedgerID"].ToString();
                        }
                        if (drParentQry["AliasName"] != null)
                        {
                            sAliasName = drParentQry["AliasName"].ToString();
                        }
                        if (drParentQry["LedgerID"] != null)
                        {
                            iLedgerID = Convert.ToInt32(drParentQry["LedgerID"]);
                        }


                        mQry = "SELECT DebtorID,CreditorID,Amount FROM tblDayBook WHERE (DebtorID=" + iLedgerID + " OR CreditorID=" + iLedgerID + ")";
                        oleCmd2 = new OleDbCommand();
                        oleCmd2.CommandText = mQry;
                        oleCmd2.CommandType = CommandType.Text;
                        oleCmd2.Connection = oleConn;
                        oleAdp2 = new OleDbDataAdapter(oleCmd2);
                        dsChild = new DataSet();
                        oleAdp2.Fill(dsChild);
                        if (dsChild.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in dsChild.Tables[0].Rows)
                            {
                                if (Convert.ToInt32(dr["DebtorID"].ToString()) == iLedgerID)
                                {
                                    dDebitAmt = Convert.ToDouble(dr["Amount"].ToString());
                                    dSumDr = dSumDr + dDebitAmt;
                                    dSumDebit = dSumDebit + dDebitAmt;
                                }
                                if (Convert.ToInt32(dr["CreditorID"].ToString()) == iLedgerID)
                                {
                                    dCreditAmt = Convert.ToDouble(dr["Amount"].ToString());
                                    dSumDr = dSumDr + dCreditAmt;
                                    dSumCredit = dSumCredit + dCreditAmt;
                                }
                            }
                        }



                        if (iGroupID == 1)
                        {
                            if (drParentQry["OpenBalanceDR"] != null)
                            {
                                dOB = Convert.ToDouble(drParentQry["OpenBalanceDR"].ToString()) - Convert.ToDouble(drParentQry["OpenBalanceCR"].ToString());
                                //dOutAmt = dOutAmt + decimal.Parse(drParentQry["OpenBalanceDR"].ToString());
                                dOutAmt = dOutAmt + dOB;
                            }
                            dOutAmt = dOutAmt + (dSumDebit - dSumCredit);
                            if (dOutAmt >= 0)
                            {
                                if (dOutAmt == 0)
                                    noFlag = true;
                                else
                                    noFlag = false;
                                if (noFlag == false)
                                {
                                    dSumDr = dSumDr + dOutAmt;
                                    drNew = dt.NewRow();

                                    drNew["LedgerID"] = sLedgerID;
                                    drNew["LedgerName"] = sLedgerName;
                                    drNew["AliasName"] = sAliasName;
                                    drNew["Debit"] = Math.Abs(dOutAmt).ToString("f2");
                                    drNew["Credit"] = "0.00";
                                    if (dOB > 0)
                                        drNew["OB"] = dOB + " Dr"; // Convert.ToDouble(drParentQry["OpenBalanceDR"].ToString()).ToString("f2");
                                    else
                                        drNew["OB"] = Math.Abs(dOB).ToString("f2") + " Cr";

                                    ds.Tables[0].Rows.Add(drNew);
                                }
                            }
                            else
                            {
                                dSumCr = dSumCr + Math.Abs(dOutAmt);
                                drNew = dt.NewRow();
                                drNew["LedgerID"] = sLedgerID;
                                drNew["LedgerName"] = sLedgerName;
                                drNew["AliasName"] = sAliasName;
                                drNew["Debit"] = "0.00";
                                drNew["Credit"] = Math.Abs(dOutAmt).ToString("f2");
                                //drNew["OB"] = Convert.ToDouble(drParentQry["OpenBalanceDR"].ToString()).ToString("f2");
                                if (dOB > 0)
                                    drNew["OB"] = dOB + " Dr";
                                else
                                    drNew["OB"] = Math.Abs(dOB).ToString("f2") + " Cr";
                                ds.Tables[0].Rows.Add(drNew);

                            }

                        }
                        else
                        {
                            if (drParentQry["OpenBalanceCR"] != null)
                            {
                                dOutAmt = dOutAmt + Convert.ToDouble(drParentQry["OpenBalanceCR"].ToString());
                            }
                            dOutAmt = dOutAmt + (dSumCredit - dSumDebit);
                            if (dOutAmt <= 0)
                            {
                                if (dOutAmt == 0)
                                    noFlag = true;
                                else
                                    noFlag = false;
                                if (noFlag == false)
                                {
                                    dSumDr = dSumDr + dOutAmt;
                                    drNew = dt.NewRow();
                                    drNew["LedgerID"] = sLedgerID;
                                    drNew["LedgerName"] = sLedgerName;
                                    drNew["AliasName"] = sAliasName;
                                    drNew["Debit"] = Math.Abs(dOutAmt).ToString("f2");
                                    drNew["Credit"] = "0.00";
                                    drNew["OB"] = Convert.ToDouble(drParentQry["OpenBalanceCR"].ToString()).ToString("f2");
                                    ds.Tables[0].Rows.Add(drNew);
                                }
                            }
                            else
                            {
                                dSumCr = dSumCr + Math.Abs(dOutAmt);
                                drNew = dt.NewRow();
                                drNew["LedgerID"] = sLedgerID;
                                drNew["LedgerName"] = sLedgerName;
                                drNew["AliasName"] = sAliasName;
                                drNew["Debit"] = "0.00";
                                drNew["Credit"] = Math.Abs(dOutAmt).ToString("f2");
                                drNew["OB"] = Convert.ToDouble(drParentQry["OpenBalanceDR"].ToString()).ToString("f2");
                                ds.Tables[0].Rows.Add(drNew);

                            }

                        }






                    }


                }


                /* Clossing the DB Connection */
                oleConn.Close();

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet generateSalesReportDS(DateTime dtSdate, DateTime dtEdate, string sDataSource)
        {
            /* Start Variable Declaration */
            
            string sBillDate = string.Empty;
            string sBillNo = string.Empty;
            string sCustomerName = string.Empty;
            string sQry = string.Empty;
            string sPayMode = string.Empty;
            string sProductName = string.Empty;
            string sProductModel = string.Empty;
            string sProductDesc = string.Empty;
            string sConStr = string.Empty;
            
            //sales

           

            /* End Variable Declaration */

            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            
            
            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */

            sQry = "SELECT Billno,BillDate,Customername,paymode FROM tblSales WHERE   BillDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND BillDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "# Order by BillDate Desc";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */
            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;


            ds = new DataSet();
            dt = new DataTable();
            dc = new DataColumn("Billno");
            dt.Columns.Add(dc);

            dc = new DataColumn("BillDate");
            dt.Columns.Add(dc);

            dc = new DataColumn("CustomerName");
            dt.Columns.Add(dc);

            dc = new DataColumn("Paymode");
            dt.Columns.Add(dc);


            ds.Tables.Add(dt);
           
            if (dsParentQry.Tables[0].Rows.Count == 0)
            {
                /* Empty XML Formation if there is no record */

                drNew = dt.NewRow();
                drNew["Billno"] = string.Empty;
                drNew["BillDate"] = string.Empty;
                drNew["CustomerName"] = string.Empty;
                drNew["Paymode"] = string.Empty;
                
                ds.Tables[0].Rows.Add(drNew);
            }
            else
            {
                foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                {
                    

                    if (drParentQry["Billno"] != null)
                    {
                        sBillNo = drParentQry["Billno"].ToString();
                        
                    }
                    if (drParentQry["BillDate"] != null)
                    {
                        sBillDate = Convert.ToDateTime(drParentQry["BillDate"]).ToShortDateString();
                        
                    }
                    if (drParentQry["CustomerName"] != null)
                    {
                        sCustomerName = drParentQry["CustomerName"].ToString();
                        
                    }
                    if (drParentQry["Paymode"] != null)
                    {
                        sPayMode = drParentQry["Paymode"].ToString();
                        //sQry = "SELECT AliasName FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(sPayMode);
                        //oleCmd = new OleDbCommand();
                        //oleCmd.CommandText = sQry;
                        //oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                        //oleCmd.Connection = oleSubConn;
                        //oleAdp = new OleDbDataAdapter(oleCmd);
                        //dsChildQry = new DataSet();
                        //oleAdp.Fill(dsChildQry);
                        //sPayMode = dsChildQry.Tables[0].Rows[0]["AliasName"].ToString();
                        //reportXMLWriter.WriteElementString("Paymode", sPayMode);
                        //oleSubConn.Close();
                    }
                    drNew = dt.NewRow();
                    drNew["Billno"] = sBillNo;
                    drNew["BillDate"] = sBillDate;
                    drNew["CustomerName"] = sCustomerName;
                    drNew["Paymode"] = sPayMode;

                    ds.Tables[0].Rows.Add(drNew);
                }
                /* Summation */
                
            }
            
            /* Clossing the DB Connection */
            oleConn.Close();

            return ds;


        }

        public DataSet generateSalesLevel1Report(DateTime dtSdate, DateTime dtEdate, string sDataSource)
        {
            /* Start Variable Declaration */

            string sBillDate = string.Empty;
            string sBillNo = string.Empty;
            string sCustomerName = string.Empty;
            StringBuilder sQry = new StringBuilder();
            string sPayMode = string.Empty;
            string sProductName = string.Empty;
            string sProductModel = string.Empty;
            string sProductDesc = string.Empty;
            string sConStr = string.Empty;

            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;

            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry.Append("SELECT tblProductMaster.ProductName, SUM(tblSalesItems.Qty) as SoldQty, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate)))) as TotalAmount, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate)))) / SUM(tblSalesItems.Qty) as AvgRate, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate))) - (tblSalesItems.Qty*tblProductMaster.NLC)) as AvgProfit ");
            sQry.Append("FROM tblSales, tblSalesItems, tblProductMaster WHERE tblSales.Billno = tblSalesItems.Billno AND   tblSalesItems.ItemCode = tblProductmaster.ItemCode AND   tblsales.cancelled<>true ");
            sQry.AppendFormat("AND   UCASE(tblSales.purchaseReturn)='NO' AND   UCASE(tblSales.InternalTransfer)='NO' AND   tblSales.BillDate>=#{0}# AND   tblSales.BillDate<=#{1}# ", dtSdate.ToString("MM/dd/yyyy"), dtEdate.ToString("MM/dd/yyyy"));
            sQry.Append("GROUP BY tblProductMaster.ProductName ORDER BY 1");
                        
            oleCmd.CommandText = sQry.ToString();
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            oleConn.Close();

            return dsParentQry;

        }

        public DataSet generateSalesLevel2Report(DateTime dtSdate, DateTime dtEdate, string sDataSource)
        {
            /* Start Variable Declaration */

            StringBuilder sQry = new StringBuilder();
            string sConStr = string.Empty;

            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;

            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry.Append("SELECT tblProductMaster.ProductName, tblProductMaster.ProductDesc, SUM(tblSalesItems.Qty) as QtySold, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate)))) as TotalAmount, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate)))) / SUM(tblSalesItems.Qty) as AvgRate, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate))) - (tblSalesItems.Qty*tblProductMaster.NLC)) as AvgProfit ");
            sQry.Append("FROM tblSales, tblSalesItems, tblProductMaster WHERE tblSales.Billno = tblSalesItems.Billno AND   tblSalesItems.ItemCode = tblProductmaster.ItemCode AND   tblsales.cancelled<>true ");
            sQry.AppendFormat("AND UCASE(tblSales.purchaseReturn)='NO' AND   UCASE(tblSales.InternalTransfer)='NO' AND   tblSales.BillDate>=#{0}# AND   tblSales.BillDate<=#{1}# ", dtSdate.ToString("MM/dd/yyyy"), dtEdate.ToString("MM/dd/yyyy"));
            sQry.Append("GROUP BY tblProductMaster.ProductName, tblProductMaster.ProductDesc ORDER BY 1,2 ");

            oleCmd.CommandText = sQry.ToString();
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            oleConn.Close();

            return dsParentQry;

        }

        public DataSet generateSalesLevel3Report(DateTime dtSdate, DateTime dtEdate, string sDataSource)
        {
            /* Start Variable Declaration */

            StringBuilder sQry = new StringBuilder();
            string sConStr = string.Empty;

            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;

            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry.Append("SELECT tblProductMaster.ProductName, tblProductMaster.ProductDesc, tblProductMaster.model, SUM(tblSalesItems.Qty) as QtySold, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate)))) as TotalAmount, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate)))) / SUM(tblSalesItems.Qty) as AvgRate, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate))) - (tblSalesItems.Qty*tblProductMaster.NLC)) as AvgProfit ");
            sQry.Append("FROM tblSales, tblSalesItems, tblProductMaster WHERE tblSales.Billno = tblSalesItems.Billno AND   tblSalesItems.ItemCode = tblProductmaster.ItemCode AND   tblsales.cancelled<>true ");
            sQry.AppendFormat("AND   UCASE(tblSales.purchaseReturn)='NO' AND   UCASE(tblSales.InternalTransfer)='NO' AND   tblSales.BillDate>=#{0}# AND   tblSales.BillDate<=#{1}#  ", dtSdate.ToString("MM/dd/yyyy"), dtEdate.ToString("MM/dd/yyyy"));
            sQry.Append("GROUP BY tblProductMaster.ProductName, tblProductMaster.ProductDesc, tblProductMaster.model ORDER BY 1,2,3");

            oleCmd.CommandText = sQry.ToString();
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            oleConn.Close();

            return dsParentQry;

        }

        public DataSet generateSalesLevel4Report(DateTime dtSdate, DateTime dtEdate, string sDataSource)
        {
            /* Start Variable Declaration */

            StringBuilder sQry = new StringBuilder();
            string sConStr = string.Empty;

            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;

            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry.Append("SELECT tblProductMaster.ProductName, tblProductMaster.ProductDesc, tblProductMaster.model, tblSales.billno, tblSales.BillDate, tblSalesItems.Qty as QtySold, ");
            sQry.Append("(tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate))) as TotalAmount, (tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate)))/tblSalesItems.Qty as AvgRate, ");
            sQry.Append("(tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate))) - (tblSalesItems.Qty*tblProductMaster.NLC) as AvgProfit ");
            sQry.Append("FROM tblSales, tblSalesItems, tblProductMaster WHERE tblSales.Billno = tblSalesItems.Billno AND   tblSalesItems.ItemCode = tblProductmaster.ItemCode ");
            sQry.AppendFormat("AND   tblsales.cancelled<>true AND   UCASE(tblSales.purchaseReturn)='NO' AND   UCASE(tblSales.InternalTransfer)='NO' AND   tblSales.BillDate>=#{0}# AND   tblSales.BillDate<=#{1}# ORDER BY 1,2,3,5 ", dtSdate.ToString("MM/dd/yyyy"), dtEdate.ToString("MM/dd/yyyy"));

            oleCmd.CommandText = sQry.ToString();
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            oleConn.Close();

            return dsParentQry;

        }

        public DataSet generatePurchaseLevel1Report(DateTime dtSdate, DateTime dtEdate, string sDataSource)
        {
            /* Start Variable Declaration */

            string sBillDate = string.Empty;
            string sBillNo = string.Empty;
            string sCustomerName = string.Empty;
            StringBuilder sQry = new StringBuilder();
            string sPayMode = string.Empty;
            string sProductName = string.Empty;
            string sProductModel = string.Empty;
            string sProductDesc = string.Empty;
            string sConStr = string.Empty;

            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;

            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry.Append("SELECT tblProductMaster.ProductName, SUM(tblPurchaseItems.Qty) as QtyBought, ");
            sQry.Append("SUM((tblPurchaseItems.Qty*(tblPurchaseItems.PurchaseRate-((tblPurchaseItems.discount/100)*tblPurchaseItems.PurchaseRate)))) as TotalAmount, ");
            sQry.Append("SUM((tblPurchaseItems.Qty*(tblPurchaseItems.PurchaseRate-((tblPurchaseItems.discount/100)*tblPurchaseItems.PurchaseRate)))) - SUM(tblPurchaseItems.Qty) as AvgRate ");
            sQry.Append("FROM tblPurchase, tblPurchaseItems, tblProductMaster WHERE tblPurchase.purchaseID=tblPurchaseItems.purchaseID AND   tblPurchaseItems.ItemCode = tblProductmaster.ItemCode ");
            sQry.AppendFormat("AND   UCASE(tblPurchase.SalesReturn)='NO' AND   UCASE(tblPurchase.InternalTransfer)='NO' AND   tblPurchase.BillDate>=#{0}# AND   tblPurchase.BillDate<=#{1}# ", dtSdate.ToString("MM/dd/yyyy"), dtEdate.ToString("MM/dd/yyyy"));
            sQry.Append("GROUP BY tblProductMaster.ProductName ORDER BY 1 ");
            /*
            sQry.Append("SELECT tblProductMaster.ProductName, SUM(tblSalesItems.Qty) as QtyBought, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate)))) as TotalAmount, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate)))) / SUM(tblSalesItems.Qty) as AvgRate, ");
            sQry.Append("SUM((tblSalesItems.Qty*(tblSalesItems.Rate-((tblSalesItems.discount/100)*tblSalesItems.Rate))) - (tblSalesItems.Qty*tblProductMaster.NLC)) as AvgProfit ");
            sQry.Append("FROM tblSales, tblSalesItems, tblProductMaster WHERE tblSales.Billno = tblSalesItems.Billno AND   tblSalesItems.ItemCode = tblProductmaster.ItemCode AND   tblsales.cancelled<>true ");
            sQry.AppendFormat("AND   UCASE(tblSales.purchaseReturn)='NO' AND   UCASE(tblSales.InternalTransfer)='NO' AND   tblSales.BillDate>=#{0}# AND   tblSales.BillDate<=#{1}# ", dtSdate.ToString("MM/dd/yyyy"), dtEdate.ToString("MM/dd/yyyy"));
            sQry.Append("GROUP BY tblProductMaster.ProductName ORDER BY 1");*/

            oleCmd.CommandText = sQry.ToString();
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            oleConn.Close();

            return dsParentQry;

        }

        public DataSet generatePurchaseLevel2Report(DateTime dtSdate, DateTime dtEdate, string sDataSource)
        {
            /* Start Variable Declaration */

            string sBillDate = string.Empty;
            string sBillNo = string.Empty;
            string sCustomerName = string.Empty;
            StringBuilder sQry = new StringBuilder();
            string sPayMode = string.Empty;
            string sProductName = string.Empty;
            string sProductModel = string.Empty;
            string sProductDesc = string.Empty;
            string sConStr = string.Empty;

            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;

            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry.Append("SELECT tblProductMaster.ProductName, tblProductMaster.ProductDesc, SUM(tblPurchaseItems.Qty) as QtyBought, ");
            sQry.Append("SUM((tblPurchaseItems.Qty*(tblPurchaseItems.PurchaseRate-((tblPurchaseItems.discount/100)*tblPurchaseItems.PurchaseRate)))) as TotalAmount, ");
            sQry.Append("SUM((tblPurchaseItems.Qty*(tblPurchaseItems.PurchaseRate-((tblPurchaseItems.discount/100)*tblPurchaseItems.PurchaseRate)))) - SUM(tblPurchaseItems.Qty) as AvgRate ");
            sQry.Append("FROM tblPurchase, tblPurchaseItems, tblProductMaster WHERE tblPurchase.purchaseID=tblPurchaseItems.purchaseID AND   tblPurchaseItems.ItemCode = tblProductmaster.ItemCode ");
            sQry.AppendFormat("AND   UCASE(tblPurchase.SalesReturn)='NO' AND   UCASE(tblPurchase.InternalTransfer)='NO' AND   tblPurchase.BillDate>=#{0}# AND   tblPurchase.BillDate<=#{1}# ", dtSdate.ToString("MM/dd/yyyy"), dtEdate.ToString("MM/dd/yyyy"));
            sQry.Append("GROUP BY tblProductMaster.ProductName, tblProductMaster.ProductDesc ORDER BY 1,2 ");

            oleCmd.CommandText = sQry.ToString();
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            oleConn.Close();

            return dsParentQry;

        }

        public DataSet generatePurchaseLevel3Report(DateTime dtSdate, DateTime dtEdate, string sDataSource)
        {
            /* Start Variable Declaration */

            string sBillDate = string.Empty;
            string sBillNo = string.Empty;
            string sCustomerName = string.Empty;
            StringBuilder sQry = new StringBuilder();
            string sPayMode = string.Empty;
            string sProductName = string.Empty;
            string sProductModel = string.Empty;
            string sProductDesc = string.Empty;
            string sConStr = string.Empty;

            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;

            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry.Append("SELECT tblProductMaster.ProductName, tblProductMaster.ProductDesc, tblProductMaster.model, SUM(tblPurchaseItems.Qty) as QtyBought, ");
            sQry.Append("SUM((tblPurchaseItems.Qty*(tblPurchaseItems.PurchaseRate-((tblPurchaseItems.discount/100)*tblPurchaseItems.PurchaseRate)))) as TotalAmount, ");
            sQry.Append("SUM((tblPurchaseItems.Qty*(tblPurchaseItems.PurchaseRate-((tblPurchaseItems.discount/100)*tblPurchaseItems.PurchaseRate)))) - SUM(tblPurchaseItems.Qty) as AvgRate ");
            sQry.Append("FROM tblPurchase, tblPurchaseItems, tblProductMaster WHERE tblPurchase.purchaseID=tblPurchaseItems.purchaseID AND   tblPurchaseItems.ItemCode = tblProductmaster.ItemCode ");
            sQry.AppendFormat("AND   UCASE(tblPurchase.SalesReturn)='NO' AND   UCASE(tblPurchase.InternalTransfer)='NO' AND   tblPurchase.BillDate>=#{0}# AND   tblPurchase.BillDate<=#{1}# ", dtSdate.ToString("MM/dd/yyyy"), dtEdate.ToString("MM/dd/yyyy"));
            sQry.Append("GROUP BY tblProductMaster.ProductName, tblProductMaster.ProductDesc, tblProductMaster.model ORDER BY 1,2,3 ");

            oleCmd.CommandText = sQry.ToString();
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            oleConn.Close();

            return dsParentQry;

        }

        public DataSet generatePurchaseLevel4Report(DateTime dtSdate, DateTime dtEdate, string sDataSource)
        {
            /* Start Variable Declaration */

            string sBillDate = string.Empty;
            string sBillNo = string.Empty;
            string sCustomerName = string.Empty;
            StringBuilder sQry = new StringBuilder();
            string sPayMode = string.Empty;
            string sProductName = string.Empty;
            string sProductModel = string.Empty;
            string sProductDesc = string.Empty;
            string sConStr = string.Empty;

            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;

            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry.Append("SELECT tblProductMaster.ProductName, tblProductMaster.ProductDesc, tblProductMaster.model, tblPurchase.billno, tblPurchase.BillDate, tblPurchaseItems.Qty as QtyBought, ");
            sQry.Append("(tblPurchaseItems.Qty*(tblPurchaseItems.PurchaseRate-((tblPurchaseItems.discount/100)*tblPurchaseItems.PurchaseRate))) as TotalAmount FROM tblPurchase, tblPurchaseItems, ");
            sQry.Append("tblProductMaster WHERE tblPurchase.purchaseID=tblPurchaseItems.purchaseID AND   tblPurchaseItems.ItemCode = tblProductmaster.ItemCode ");
            sQry.AppendFormat("AND UCASE(tblPurchase.SalesReturn)='NO' AND   UCASE(tblPurchase.InternalTransfer)='NO' AND   tblPurchase.BillDate>=#{0}# AND tblPurchase.BillDate<=#{1}# ORDER BY 1,2,3,5 ", dtSdate.ToString("MM/dd/yyyy"), dtEdate.ToString("MM/dd/yyyy"));
            
            oleCmd.CommandText = sQry.ToString();
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            oleConn.Close();

            return dsParentQry;

        }


        public DataSet generateDayBookDS(DateTime dtSdate, DateTime dtEdate, string sDataSource)
        {
            /* Start Variable Declaration */
            Decimal dDebitAmt, dCreditAmt, dSumDr, dSumCr, dDiffDrCr;
            string sDebtor = string.Empty;
            string sCreditor = string.Empty;
            string sTranDate = string.Empty;
            string iQry = "";
            string sQry = string.Empty;
            string sConStr = string.Empty;
            string sNarration = string.Empty;
            OleDbConnection oleConn, oleSubConn; ;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            DataSet dsChildQry;
           
            /* End Variable Declaration */
            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry = "SELECT TransDate,DebtorID,CreditorID,Amount,Narration FROM tblDayBook WHERE  (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) Order by TransDate Desc";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            /* XML Forming using the records gathered in the DayBook */

            

            /* Intialization of Sum of Debit and Credit and their Difference */
            dSumDr = 0;
            dSumCr = 0;
            dDiffDrCr = 0;
            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;


            ds = new DataSet();
            dt = new DataTable();
            dc = new DataColumn("Date");
            dt.Columns.Add(dc);

            dc = new DataColumn("Narration");
            dt.Columns.Add(dc);

            dc = new DataColumn("Debit");
            dt.Columns.Add(dc);

            dc = new DataColumn("Credit");
            dt.Columns.Add(dc);

            dc = new DataColumn("Debitor");
            dt.Columns.Add(dc);

            dc = new DataColumn("Creditor");
            dt.Columns.Add(dc);


            ds.Tables.Add(dt);
            if (dsParentQry.Tables[0].Rows.Count == 0)
            {
                /* Empty XML Formation if there is no record */
                
                drNew = dt.NewRow();
                drNew["Date"] = string.Empty;
                drNew["Narration"] = string.Empty;
                drNew["Debit"] = "0.00";
                drNew["Credit"] = "0.00";
                drNew["Debitor"] = string.Empty;
                drNew["Creditor"] = string.Empty;
                ds.Tables[0].Rows.Add(drNew);
               
            }
            else
            {
                foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                {
                    dDebitAmt = 0;
                    dCreditAmt = 0;
                    
                    if (drParentQry["TransDate"] != null)
                    {
                        sTranDate = Convert.ToDateTime(drParentQry["TransDate"].ToString()).ToShortDateString();
                    }

                    
                    if (drParentQry["Narration"] != null)
                    {
                        sNarration = "(" + drParentQry["Narration"].ToString() + ")";
                    }
                    
                    /* Start Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/
                    if (drParentQry["CreditorID"] != null)
                    {
                        dDebitAmt = Convert.ToDecimal(drParentQry["Amount"].ToString());
                        iQry = "SELECT Ledgername FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["CreditorID"].ToString());
                        dSumCr = dSumCr + dCreditAmt;
                        oleCmd = new OleDbCommand();
                        oleCmd.CommandText = iQry;
                        oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                        oleCmd.Connection = oleSubConn;
                        oleAdp = new OleDbDataAdapter(oleCmd);
                        dsChildQry = new DataSet();
                        oleAdp.Fill(dsChildQry);
                        if (dsChildQry != null)
                        {
                            if (dsChildQry.Tables[0].Rows.Count > 0)
                                sCreditor = dsChildQry.Tables[0].Rows[0]["ledgername"].ToString();
                            else
                                sCreditor = "";
                        }
                        else
                        {
                            sCreditor = "";
                        }
                        oleSubConn.Close();
                    }
                    if (drParentQry["DebtorID"] != null)
                    {

                        dCreditAmt = Convert.ToDecimal(drParentQry["Amount"].ToString());
                        iQry = "SELECT Ledgername  FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["DebtorID"].ToString());
                        dSumDr = dSumDr + dDebitAmt;
                        oleCmd = new OleDbCommand();
                        oleCmd.CommandText = iQry;
                        oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                        oleCmd.Connection = oleSubConn;
                        oleAdp = new OleDbDataAdapter(oleCmd);
                        dsChildQry = new DataSet();
                        oleAdp.Fill(dsChildQry);
                        if (dsChildQry != null)
                        {
                            if (dsChildQry.Tables[0].Rows.Count > 0)
                                sDebtor = dsChildQry.Tables[0].Rows[0]["ledgername"].ToString();
                            else
                                sDebtor = "";
                        }
                        else
                        {
                            sDebtor = "";
                        }
                        oleSubConn.Close();
                    }
                    /* End Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/
                    

                    drNew = dt.NewRow();
                    drNew["Date"] = sTranDate;
                    drNew["Narration"] = sNarration; 
                    drNew["Debit"] = dDebitAmt.ToString();
                    drNew["Credit"] = dCreditAmt.ToString();
                    drNew["Debitor"] = sDebtor;
                    drNew["Creditor"] = sCreditor;
                    ds.Tables[0].Rows.Add(drNew);

                }//End foreach


                
            }

           

            /* Clossing the DB Connection */
            oleConn.Close();
            return ds;
           
        }

        public DataSet generateReportDS(int iLedgerID, DateTime dtSdate, DateTime dtEdate, string sDataSource, int iOrder)
        {
            /* Start Variable Declaration */

            double dDebitAmt = 0;
            double dCreditAmt = 0;
            string sTranDate = string.Empty;
            string iQry = "";
            string sParticulars = "";
            string sVoucherType = string.Empty;
            string sQry = string.Empty;
            string pQry = string.Empty;
            string sConStr = string.Empty;
            OleDbConnection oleConn, oleSubConn;

            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            DataSet dsChildQry;
            string sOrder;
            if (iOrder == 0)
                sOrder = "asc";
            else
                sOrder = "desc";
            /* End Variable Declaration */

            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */

            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry = "SELECT TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType FROM tblDayBook WHERE (DebtorID=" + iLedgerID + " OR CreditorID=" + iLedgerID + ") AND (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) Order by TransDate " + sOrder;
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */


            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;


            ds = new DataSet();
            dt = new DataTable();
            dc = new DataColumn("Date");
            dt.Columns.Add(dc);

            dc = new DataColumn("Particulars");
            dt.Columns.Add(dc);

            dc = new DataColumn("Debit");
            dt.Columns.Add(dc);

            dc = new DataColumn("Credit");
            dt.Columns.Add(dc);

            dc = new DataColumn("VoucherType");
            dt.Columns.Add(dc);


            ds.Tables.Add(dt);

            if (dsParentQry.Tables[0].Rows.Count == 0)
            {
                drNew = dt.NewRow();
                drNew["Date"] = string.Empty;
                drNew["Particulars"] = string.Empty;
                drNew["Debit"] = "0.00";
                drNew["Credit"] = "0.00";
                drNew["VoucherType"] = string.Empty;
                ds.Tables[0].Rows.Add(drNew);
            }
            else
            {
                /* Iterating through the records and forming the custom datamodel and write into XML file */

                foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                {
                    dDebitAmt = 0;
                    dCreditAmt = 0;


                    if (drParentQry["TransDate"] != null)
                    {
                        sTranDate = Convert.ToDateTime(drParentQry["TransDate"].ToString()).ToShortDateString();
                    }
                    if (drParentQry["VoucherType"] != null)
                    {
                        sVoucherType = Convert.ToString(drParentQry["VoucherType"].ToString());
                    }

                    /* Start Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/
                    if (drParentQry["DebtorID"] != null)
                    {
                        if (Convert.ToInt32(drParentQry["DebtorID"].ToString()) == iLedgerID)
                        {
                            dDebitAmt = Convert.ToDouble(drParentQry["Amount"].ToString());
                            pQry = "SELECT Ledgername FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["CreditorID"].ToString());
                        }
                    }
                    if (drParentQry["CreditorID"] != null)
                    {
                        if (Convert.ToInt32(drParentQry["CreditorID"].ToString()) == iLedgerID)
                        {

                            dCreditAmt = Convert.ToDouble(drParentQry["Amount"].ToString());
                            pQry = "SELECT Ledgername FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["DebtorID"].ToString());
                        }
                    }
                    /* End Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/


                    if (pQry != "")
                    {
                        oleCmd = new OleDbCommand();
                        oleCmd.CommandText = pQry;
                        oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                        oleCmd.Connection = oleSubConn;
                        oleAdp = new OleDbDataAdapter(oleCmd);
                        dsChildQry = new DataSet();
                        oleAdp.Fill(dsChildQry);
                        if (dsChildQry != null)
                        {
                            if (dsChildQry.Tables[0].Rows.Count > 0)
                            {
                                sParticulars = dsChildQry.Tables[0].Rows[0]["ledgername"].ToString() + " " + drParentQry["Narration"].ToString();
                            }
                        }
                        oleSubConn.Close();
                    }

                    drNew = dt.NewRow();
                    drNew["Date"] = sTranDate;
                    drNew["Particulars"] = sParticulars;
                    drNew["Debit"] = dDebitAmt.ToString();
                    drNew["Credit"] = dCreditAmt.ToString();
                    drNew["VoucherType"] = sVoucherType;

                    ds.Tables[0].Rows.Add(drNew);
                }



            }


            /* Clossing the DB Connection */
            oleConn.Close();

            return ds;


        }



        public DataSet generateReportDSLedger(int iAccHeading, int iGroupID, int iLedgerID, DateTime dtSdate, DateTime dtEdate, string sDataSource,int iOrder)
        {
            /* Start Variable Declaration */

            double dDebitAmt=0;
            double dCreditAmt = 0 ;
            string sTranDate = string.Empty;
            string iQry = "";
            string sParticulars = "";
            string sVoucherType = string.Empty;
            string sLedgerID = "0";
            string sLedger = string.Empty;
            string sQry = string.Empty;
            string pQry = string.Empty;
            string sConStr = string.Empty;
            OleDbConnection oleConn, oleSubConn;

            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            DataSet dsChildQry;
            string sOrder;
            if (iOrder == 0)
                sOrder = "asc";
            else
                sOrder = "desc";
            /* End Variable Declaration */

            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
			sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */

            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            //sQry = "SELECT TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType FROM tblDayBook WHERE (DebtorID=" + iLedgerID + " OR CreditorID=" + iLedgerID + ") AND (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) Order by TransDate " +sOrder;
            sQry = "SELECT TransDate,NULL as DebtorID,CreditorID,Amount,Narration,VoucherType,L.LedgerName ";
            sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.CreditorID ) ";
            sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
            sQry = sQry + "WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";

            if (iLedgerID != 0)
                sQry = sQry + " AND ( CreditorID=" + iLedgerID + ") ";

            if (iGroupID != 0)
                sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

            if (iAccHeading != 0)
                sQry = sQry + " AND (H.HeadingID =" + iAccHeading + ") ";

            sQry = sQry + " Union All ";

            sQry = sQry + "SELECT TransDate,DebtorID,Null as CreditorID,Amount,Narration,VoucherType, L.LedgerName ";
            sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.DebtorID ) ";
            sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
            sQry = sQry + " WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
            
            if (iLedgerID != 0)
                sQry = sQry + " AND ( DebtorID=" + iLedgerID + ") ";

            if (iGroupID != 0)
                sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

            if (iAccHeading != 0)
                sQry = sQry + " AND (H.HeadingID =" + iAccHeading + ") ";

            sQry = sQry + "Order by TransDate " +sOrder;

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */
        

            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;


            ds = new DataSet();
            dt = new DataTable();
            dc = new DataColumn("Date");
            dt.Columns.Add(dc);

            dc = new DataColumn("Ledger");
            dt.Columns.Add(dc);

            dc = new DataColumn("LedgerID");
            dt.Columns.Add(dc);

            dc = new DataColumn("Particulars");
            dt.Columns.Add(dc);

            dc = new DataColumn("Debit");
            dt.Columns.Add(dc);

            dc = new DataColumn("Credit");
            dt.Columns.Add(dc);

            dc = new DataColumn("VoucherType");
            dt.Columns.Add(dc);

            ds.Tables.Add(dt);

            if (dsParentQry.Tables[0].Rows.Count == 0)
            {
                drNew = dt.NewRow();
                drNew["Date"] = string.Empty;
                drNew["Ledger"] = string.Empty;
                drNew["LedgerID"] = string.Empty;
                drNew["Particulars"] = string.Empty;
                drNew["Debit"] = "0.00";
                drNew["Credit"] = "0.00";
                drNew["VoucherType"] = string.Empty;
                ds.Tables[0].Rows.Add(drNew);
            }
            else
            {
                /* Iterating through the records and forming the custom datamodel and write into XML file */

                foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                {
                    dDebitAmt = 0;
                    dCreditAmt = 0;

                   
                    if (drParentQry["TransDate"] != null)
                    {
                        sTranDate = Convert.ToDateTime(drParentQry["TransDate"].ToString()).ToShortDateString();
                    }
                    if (drParentQry["VoucherType"] != null)
                    {
                        sVoucherType = Convert.ToString(drParentQry["VoucherType"].ToString());
                    }
                   
                    /* Start Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/
                    if (drParentQry["DebtorID"] != null)
                    {
                        if (drParentQry["DebtorID"].ToString() != "")
                        {
                            if (Convert.ToInt32(drParentQry["DebtorID"].ToString()) > 0)
                            {
                                dDebitAmt = Convert.ToDouble(drParentQry["Amount"].ToString());
                                pQry = "SELECT Ledgername,LedgerID FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["DebtorID"].ToString());
                            }
                        }
                    }
                    if (drParentQry["CreditorID"] != null)
                    {
                        if (drParentQry["CreditorID"].ToString() != "")
                        {
                            if (Convert.ToInt32(drParentQry["CreditorID"].ToString()) > 0)
                            {

                                dCreditAmt = Convert.ToDouble(drParentQry["Amount"].ToString());
                                pQry = "SELECT Ledgername, LedgerID FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["CreditorID"].ToString());
                            }
                        }
                    }
                    /* End Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/

                   
                    if (pQry != "")
                    {
                        oleCmd = new OleDbCommand();
                        oleCmd.CommandText = pQry;
                        oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                        oleCmd.Connection = oleSubConn;
                        oleAdp = new OleDbDataAdapter(oleCmd);
                        dsChildQry = new DataSet();
                        oleAdp.Fill(dsChildQry);
                        if (dsChildQry != null)
                        {
                            if (dsChildQry.Tables[0].Rows.Count > 0)
                            {
                                sParticulars = dsChildQry.Tables[0].Rows[0]["ledgername"].ToString() + " " + drParentQry["Narration"].ToString();
                                sLedger = dsChildQry.Tables[0].Rows[0]["ledgername"].ToString();
                                sLedgerID = dsChildQry.Tables[0].Rows[0]["ledgerID"].ToString();
                            }
                        }
                        oleSubConn.Close();
                    }
                    
                    drNew = dt.NewRow();
                    drNew["Date"] = sTranDate;
                    drNew["Ledger"] = sLedger;
                    drNew["LedgerID"] = sLedgerID;
                    drNew["Particulars"] = sParticulars;
                    drNew["Debit"] = dDebitAmt.ToString();
                    drNew["Credit"] = dCreditAmt.ToString();
                    drNew["VoucherType"] = sVoucherType;
                    
                    ds.Tables[0].Rows.Add(drNew);
                }

                

            }
            

            /* Clossing the DB Connection */
            oleConn.Close();

            return ds;


        }

        /*Start Ledger Report March 16*/
        public DataSet generateReportDS(int iAccHeading, int iGroupID,int iLedgerID, DateTime dtSdate, DateTime dtEdate, string sDataSource,string sType,string retFlag,int iOrder)
        {
            /* Start Variable Declaration */

            double dDebitAmt = 0;
            double dCreditAmt = 0;
            string sTranDate = string.Empty;
            string iQry = "";
            string sParticulars = "";
            string sVoucherType = string.Empty;
            string sQry = string.Empty;
            string pQry = string.Empty;
            string sLedger = string.Empty;
            string sLedgerID = "0";
            string rQry = string.Empty;
            string sConStr = string.Empty;
            int retValue = 0;
            OleDbConnection oleConn, oleSubConn,oleSubConn2;
            int transno = 0;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            DataSet dsChildQry;
            string sOrder;
            if (iOrder == 0)
                sOrder = "asc";
            else
                sOrder = "desc";
            /* End Variable Declaration */

            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
			sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */

           


            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            if (sType == "Sales" && retFlag == "Yes") /* Only Sales Return */
			{
                sQry = "SELECT TransDate,NULL as DebtorID,CreditorID,Amount,Narration,VoucherType,L.LedgerName ";
				sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.CreditorID ) ";
				sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
				sQry = sQry + "WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
				sQry = sQry + "AND VoucherType='Purchase Return' ";

                if(iLedgerID != 0)
                sQry = sQry + " AND ( CreditorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if (iAccHeading != 0)
                    sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";

				sQry = sQry + " Union All ";

				sQry = sQry + "SELECT TransDate,DebtorID,Null as CreditorID,Amount,Narration,VoucherType, L.LedgerName ";
				sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.DebtorID ) ";
				sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
				sQry = sQry + " WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
				sQry = sQry + "AND VoucherType='Purchase Return' ";
				
                if(iLedgerID != 0)
                    sQry = sQry + " AND ( DebtorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if (iAccHeading != 0)
                sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";

			}
            else if (sType == "Purchase" && retFlag == "Yes")/* Only Purchase Return */
			{
                //sQry = "SELECT TransNo,TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType FROM tblDayBook WHERE VoucherType='Purchase Return'  AND (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#)  Order by TransDate "+sOrder ;
                sQry = "SELECT TransDate,NULL as DebtorID,CreditorID,Amount,Narration,VoucherType,L.LedgerName ";
				sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.CreditorID ) ";
				sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
				sQry = sQry + "WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
				sQry = sQry + "AND VoucherType='Sales Return' ";

                if(iLedgerID != 0)
                  sQry = sQry + " AND ( CreditorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                    sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if(iAccHeading != 0)
                    sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";

				sQry = sQry + " Union All ";

				sQry = sQry + "SELECT TransDate,DebtorID,Null as CreditorID,Amount,Narration,VoucherType, L.LedgerName ";
				sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.DebtorID ) ";
				sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
				sQry = sQry + "WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
				sQry = sQry + "AND VoucherType='Sales Return' ";
				
                if(iLedgerID != 0)
                 sQry = sQry + " AND (DebtorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                    sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if(iAccHeading != 0)
                    sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";

			}
            else if (sType == "Purchase" && retFlag == "Both")/* Only Purchase Return */
            {
                //sQry = "SELECT TransNo,TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType FROM tblDayBook WHERE VoucherType='Purchase Return'  AND (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#)  Order by TransDate "+sOrder ;
                sQry = "SELECT TransDate,NULL as DebtorID,CreditorID,Amount,Narration,VoucherType,L.LedgerName ";
                sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.CreditorID ) ";
                sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
                sQry = sQry + "WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
                sQry = sQry + "AND ( VoucherType='Sales Return' OR VoucherType='Purchase' ) ";

                if(iLedgerID != 0)
                    sQry = sQry + " AND ( CreditorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                    sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if(iAccHeading != 0)
                    sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";

                sQry = sQry + " Union All ";

                sQry = sQry + "SELECT TransDate,DebtorID,Null as CreditorID,Amount,Narration,VoucherType, L.LedgerName ";
                sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.DebtorID ) ";
                sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
                sQry = sQry + "WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
                sQry = sQry + "AND ( VoucherType='Sales Return' OR VoucherType='Purchase' ) ";

                if(iLedgerID != 0)
                 sQry = sQry + " AND (DebtorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                    sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if(iAccHeading != 0)
                    sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";

            }
            else if (sType == "Sales" && retFlag == "Both") /* Both Sales and Sales Return */
			{
                //sQry = "SELECT TransNo,TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType FROM tblDayBook WHERE (VoucherType='Sales Return' OR VoucherType='Sales') AND (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#)  Order by TransDate " + sOrder;
                sQry = "SELECT TransDate,NULL as DebtorID,CreditorID,Amount,Narration,VoucherType,L.LedgerName ";
				sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.CreditorID ) ";
				sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
				sQry = sQry + "WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
                sQry = sQry + " AND ( VoucherType='Sales' OR VoucherType='Purchase Return') ";

                if(iLedgerID != 0)
                    sQry = sQry + " AND ( CreditorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                    sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if(iAccHeading != 0)
                    sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";

				sQry = sQry + " Union All ";

				sQry = sQry + "SELECT TransDate,DebtorID,Null as CreditorID,Amount,Narration,VoucherType, L.LedgerName ";
				sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.DebtorID ) ";
				sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
				sQry = sQry + " WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
                sQry = sQry + " AND ( VoucherType='Sales' OR VoucherType='Purchase Return') ";
				
                if(iLedgerID != 0)
                    sQry = sQry + " AND (DebtorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                    sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if(iAccHeading != 0)
                    sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";
			}
            else if (sType == "Sales" && retFlag == "No") /* Both Sales and Sales Return */
            {
                //sQry = "SELECT TransNo,TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType FROM tblDayBook WHERE (VoucherType='Sales Return' OR VoucherType='Sales') AND (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#)  Order by TransDate " + sOrder;
                sQry = "SELECT TransDate,NULL as DebtorID,CreditorID,Amount,Narration,VoucherType,L.LedgerName ";
                sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.CreditorID ) ";
                sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
                sQry = sQry + "WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
                sQry = sQry + " AND VoucherType='Sales' ";

                if(iLedgerID != 0)
                    sQry = sQry + " AND ( CreditorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                    sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if(iAccHeading != 0)
                    sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";

                sQry = sQry + " Union All ";

                sQry = sQry + "SELECT TransDate,DebtorID,Null as CreditorID,Amount,Narration,VoucherType, L.LedgerName ";
                sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.DebtorID ) ";
                sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
                sQry = sQry + " WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
                sQry = sQry + " AND (VoucherType='Sales') ";

                if(iLedgerID != 0)
                    sQry = sQry + " AND (DebtorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                    sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if(iAccHeading != 0)
                    sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";
            }
            else if (sType == "Purchase" && retFlag == "No")/* Both Purchase and Purchase Return */
			{
                //sQry = "SELECT TransNo,TransDate,DebtorID,CreditorID,Amount,Narration,VoucherType FROM tblDayBook WHERE (VoucherType='Purchase Return' OR VoucherType='Purchase')  AND (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#)  Order by TransDate " + sOrder;
                sQry = "SELECT TransDate,NULL as DebtorID,CreditorID,Amount,Narration,VoucherType,L.LedgerName ";
				sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.CreditorID ) ";
				sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
				sQry = sQry + "WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
				sQry = sQry + " AND (VoucherType='Purchase') ";

                if(iLedgerID != 0)
                    sQry = sQry + " AND ( CreditorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                    sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if(iAccHeading != 0)
                    sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";

				sQry = sQry + " Union All ";

				sQry = sQry + "SELECT TransDate,DebtorID,Null as CreditorID,Amount,Narration,VoucherType, L.LedgerName ";
				sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.DebtorID ) ";
				sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
				sQry = sQry + " WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
                sQry = sQry + " AND ( VoucherType='Purchase') ";
				
                if(iLedgerID != 0)
                    sQry = sQry + " AND (DebtorID=" + iLedgerID + ") ";

                if(iGroupID != 0)
                    sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

                if(iAccHeading != 0)
                    sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";
			}	
            else
			{
                //sQry = "SELECT TransNo,TransDate,DebtorID,CreditorID,Amount,Narration,,VoucherType FROM tblDayBook WHERE (DebtorID=" + iLedgerID + " OR CreditorID=" + iLedgerID + ") AND (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) Order by TransDate " + sOrder;
                sQry = "SELECT TransDate,NULL as DebtorID,CreditorID,Amount,Narration,VoucherType,L.LedgerName ";
				sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.CreditorID ) ";
				sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
				sQry = sQry + "WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
				
				if(iLedgerID != 0)
				sQry = sQry + " AND ( CreditorID=" + iLedgerID + ") ";

				if(iGroupID != 0)
				sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

				if (iAccHeading != 0)
				sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";

				sQry = sQry + " Union All ";

				sQry = sQry + "SELECT TransDate,DebtorID,Null as CreditorID,Amount,Narration,VoucherType, L.LedgerName ";
				sQry = sQry + "FROM (((tblDayBook B INNER JOIN tblLedger L ON L.LedgerID = B.DebtorID ) ";
				sQry = sQry + "INNER JOIN tblGroups G ON G.GroupID = L.GroupID) INNER JOIN tblAccHeading H ON H.HeadingID = G.HeadingID ) ";
				sQry = sQry + " WHERE (TransDate >=#" + dtSdate.ToString("MM/dd/yyyy") + "# AND TransDate <=#" + dtEdate.ToString("MM/dd/yyyy") + "#) ";
				
				if(iLedgerID != 0)
				sQry = sQry + " AND (DebtorID=" + iLedgerID + ") ";

				if(iGroupID != 0)
				sQry = sQry + " AND (G.GroupID=" + iGroupID + ") ";

				if (iAccHeading != 0)
				sQry = sQry + " AND (H.HeadingID =" + iAccHeading +") ";
				
			}

            sQry = sQry + " Order by TransDate " + sOrder;

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            /* End DB Query Processing - Getting the Details of the Ledger int the Daybook */

            
            DataSet ds;
            DataTable dt;
            DataRow drNew;
            DataColumn dc;


            ds = new DataSet();
            dt = new DataTable();
            dc = new DataColumn("Date");
            dt.Columns.Add(dc);

            dc = new DataColumn("Ledger");
            dt.Columns.Add(dc);

            dc = new DataColumn("LedgerID");
            dt.Columns.Add(dc);

            dc = new DataColumn("Particulars");
            dt.Columns.Add(dc);

            dc = new DataColumn("Debit");
            dt.Columns.Add(dc);

            dc = new DataColumn("Credit");
            dt.Columns.Add(dc);
            dc = new DataColumn("VoucherType");
            dt.Columns.Add(dc);


            ds.Tables.Add(dt);

            if (dsParentQry.Tables[0].Rows.Count == 0)
            {
                drNew = dt.NewRow();
                drNew["Date"] = string.Empty;
                drNew["Ledger"] = string.Empty;
                drNew["LedgerID"] = "";
                drNew["Particulars"] = string.Empty;
                drNew["Debit"] = "0.00";
                drNew["Credit"] = "0.00";
                drNew["VoucherType"] = string.Empty;
                ds.Tables[0].Rows.Add(drNew);
            }
            else
            {
                /* Iterating through the records and forming the custom datamodel and write into XML file */

                foreach (DataRow drParentQry in dsParentQry.Tables[0].Rows)
                {
                    dDebitAmt = 0;
                    dCreditAmt = 0;
                    //if (drParentQry["TransNo"] != null)
                    //{
                    //    transno = Convert.ToInt32(drParentQry["TransNo"]);
                    //    if (sType == "Sales")
                    //    {
                    //        rQry = "Select Count(*) from tblPurchase Where Transno=" + transno + " And SalesReturn Like 'Yes'";
                    //    }
                    //    else
                    //    {
                    //        rQry = "Select Count(*) from tblSales Where Transno=" + transno + " And PurchaseReturn Like 'Yes'";
                    //    }
                    //    oleSubConn2 = new OleDbConnection(sConStr);
                    //    oleCmd.Connection = oleSubConn2;
                    //    oleCmd = new OleDbCommand();
                    //    oleCmd.CommandText = rQry;
                    //    oleCmd.Connection = oleSubConn2;
                    //    retValue=oleCmd.ExecuteNonQuery();
                    //    oleSubConn2.Close();
                    //}
                    if (drParentQry["TransDate"] != null)
                    {
                        sTranDate = Convert.ToDateTime(drParentQry["TransDate"].ToString()).ToShortDateString();
                    }
                    if (drParentQry["VoucherType"] != null)
                    {
                        sVoucherType = Convert.ToString(drParentQry["VoucherType"].ToString());
                    }
                    /* Start Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/
                    if (drParentQry["DebtorID"] != null)
                    {
                        if (drParentQry["DebtorID"].ToString() != "")
                        {
                            if (Convert.ToInt32(drParentQry["DebtorID"].ToString()) > 0)
                            {

                                dDebitAmt = Convert.ToDouble(drParentQry["Amount"].ToString());
                                pQry = "SELECT Ledgername,LedgerID FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["DebtorID"].ToString());

                            }
                        }
                    }
                    if (drParentQry["CreditorID"] != null)
                    {
                        if (drParentQry["CreditorID"].ToString() != "")
                        {
                            if (Convert.ToInt32(drParentQry["CreditorID"].ToString()) > 0)
                            {

                                dCreditAmt = Convert.ToDouble(drParentQry["Amount"].ToString());
                                pQry = "SELECT Ledgername, LedgerID FROM tblLedger WHERE LedgerID=" + Convert.ToInt32(drParentQry["CreditorID"].ToString());
                            }
                        }
                    }

                    /* End Sum up the Debit and Credit Transaction of the given ledgerID , Getting the Correcponding Debtor or creditor for the particulars section*/
                    if (sVoucherType == "Sales" || sVoucherType == "Purchase Return")
                    {
                        dDebitAmt = 0;

                    }
                    else if(sVoucherType == "Purchase" || sVoucherType == "Sales Return")
                    {
                        dCreditAmt =0;
                    }

                    if (pQry != "")
                    {
                        oleCmd = new OleDbCommand();
                        oleCmd.CommandText = pQry;
                        oleSubConn = new OleDbConnection(CreateConnectionString(sConStr));
                        oleCmd.Connection = oleSubConn;
                        oleAdp = new OleDbDataAdapter(oleCmd);
                        dsChildQry = new DataSet();
                        oleAdp.Fill(dsChildQry);
                        if (dsChildQry != null)
                        {
                            if (dsChildQry.Tables[0].Rows.Count > 0)
                            {
                                sParticulars = dsChildQry.Tables[0].Rows[0]["ledgername"].ToString() + " " + drParentQry["Narration"].ToString();
                                sLedger = dsChildQry.Tables[0].Rows[0]["ledgername"].ToString();
                                sLedgerID = dsChildQry.Tables[0].Rows[0]["ledgerID"].ToString();
                            }
                        }
                        oleSubConn.Close();
                    }
                    //if (retFlag == "No")
                    //{
                    //    if (retValue == 0)
                    //    {
                            drNew = dt.NewRow();
                            drNew["Date"] = sTranDate;
                            drNew["Ledger"] = sLedger;
                            drNew["LedgerID"] = sLedgerID;
                            drNew["Particulars"] = sParticulars;
                            drNew["Debit"] = dDebitAmt.ToString();
                            drNew["Credit"] = dCreditAmt.ToString();
                            drNew["VoucherType"] = sVoucherType;
                            ds.Tables[0].Rows.Add(drNew);
                    //    }
                    //}
                    //else if (retFlag == "Yes")
                    //{
                    //    if (retValue > 0)
                    //    {
                    //        drNew = dt.NewRow();
                    //        drNew["Date"] = sTranDate;
                    //        drNew["Particulars"] = sParticulars;
                    //        drNew["Debit"] = dDebitAmt.ToString();
                    //        drNew["Credit"] = dCreditAmt.ToString();
                    //        ds.Tables[0].Rows.Add(drNew);
                    //    }
                    //}
                    
                }



            }


            /* Clossing the DB Connection */

            
            oleConn.Close();

            return ds;


        }
        /*End Ledger Report March 16*/


        public double getLedgerOpeningBalance(int ledgerID, string type, string sDataSource)
        {
            string sConStr = string.Empty;
            string sQry = string.Empty;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleConn.Open();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */

            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            if (type == "debit")
                sQry = "SELECT OpenBalanceDr  FROM tblLedger Where  ledgerID=" + ledgerID ;
            else
                sQry = "SELECT OpenBalanceCr  FROM tblLedger Where  ledgerID=" + ledgerID;
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            //oleAdp = new OleDbDataAdapter(oleCmd);
            //dsParentQry = new DataSet();
            //oleAdp.Fill(dsParentQry);
            object amtObj;

            amtObj = oleCmd.ExecuteScalar();
            double amt = 0.0;
            if (amtObj != null && amtObj != DBNull.Value)
                amt = (double)amtObj;
            oleConn.Close();
            return amt;
        }

        public double getOpeningBalance(int AccHeadingID,int GroupID,int ledgerID, string type, DateTime oDate, string sDataSource)
        {
            string sConStr = string.Empty;
            string sQry = string.Empty;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            sConStr = sDataSource;
            oleConn = new OleDbConnection( CreateConnectionString( sConStr));
            oleCmd = new OleDbCommand();
            oleConn.Open();
            oleCmd.Connection = oleConn;
            double oBal = 0;
            /* End Ms Access Database Connection Information */

            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            if (type == "debit")
            {
                sQry = "SELECT SUM(Amount)  As OpeningBal  FROM (((tblDayBook B Inner Join tblLedger L On L.LedgerID = B.DebtorID) Inner Join tblGroups G On G.GroupID = L.GroupID) Inner Join tblAccHeading H On H.HeadingID = G.HeadingID) Where TransDate <#" + oDate.ToString("MM/dd/yyyy") + "#";

                if(ledgerID > 0)
                    sQry = sQry + " AND B.DebtorID = " + ledgerID.ToString() ;
                if (AccHeadingID > 0)
                    sQry = sQry + " AND G.HeadingID = " + AccHeadingID.ToString();
                if (GroupID > 0)
                    sQry = sQry + " AND L.GroupID = " + GroupID.ToString();


            }
            else
            {
                //sQry = "SELECT SUM(Amount)  As OpeningBal  FROM tblDayBook Where CreditorID = " + ledgerID + "  AND TransDate <#" + oDate.ToString("MM/dd/yyyy") + "#";
                sQry = "SELECT SUM(Amount)  As OpeningBal  FROM (((tblDayBook B Inner Join tblLedger L On L.LedgerID = B.CreditorID) Inner Join tblGroups G On G.GroupID = L.GroupID) Inner Join tblAccHeading H On H.HeadingID = G.HeadingID) Where TransDate <#" + oDate.ToString("MM/dd/yyyy") + "#";

                if (ledgerID > 0)
                    sQry = sQry + " AND B.CreditorID = " + ledgerID.ToString();
                if (AccHeadingID > 0)
                    sQry = sQry + " AND G.HeadingID = " + AccHeadingID.ToString();
                if (GroupID > 0)
                    sQry = sQry + " AND L.GroupID = " + GroupID.ToString();

            }
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oBal = getLedgerOpeningBalance(ledgerID, type, sDataSource);

            object amtObj;

            amtObj = oleCmd.ExecuteScalar();
            double amt = 0.0;
            if (amtObj != null && amtObj != DBNull.Value)
                amt = (double)amtObj;
            amt = amt + oBal;
            oleConn.Close();
            return amt;
        }

        public double getOpeningBalanceSalesPurchase(int ledgerID, string stype, DateTime oDate, string sDataSource)
        {
            string sConStr = string.Empty;
            string sQry = string.Empty;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            sConStr = sDataSource;
            oleConn = new OleDbConnection( CreateConnectionString( sConStr));
            oleCmd = new OleDbCommand();
            oleConn.Open();
            oleCmd.Connection = oleConn;
            string type = string.Empty;
            double oBal = 0;
            /* End Ms Access Database Connection Information */

            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */

            sQry = "SELECT SUM(Amount) As OpeningBal  FROM tblDayBook Where vouchertype='" + stype + "' AND TransDate <#" + oDate.ToString("MM/dd/yyyy") + "#";


            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;


            object amtObj;

            amtObj = oleCmd.ExecuteScalar();
            double amt = 0.0;
            if (amtObj != null && amtObj != DBNull.Value)
                amt = (double)amtObj;
            //amt = amt + oBal;
            oleConn.Close();
            return amt;

        }

        public double getOpeningBalanceSalesPurchaseReturn(int ledgerID, string stype, string returnType, DateTime oDate, string sDataSource)
        {
            string sConStr = string.Empty;
            string sQry = string.Empty;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            sConStr = sDataSource;
            oleConn = new OleDbConnection( CreateConnectionString( sConStr));
            oleCmd = new OleDbCommand();
            oleConn.Open();
            oleCmd.Connection = oleConn;
            string type = string.Empty;
            double oBal = 0;
            /* End Ms Access Database Connection Information */

            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */

            sQry = "SELECT SUM(Amount)  As OpeningBal  FROM tblDayBook Where (vouchertype='" + stype + "' OR VoucherType='" + returnType + "')AND TransDate <#" + oDate.ToString("MM/dd/yyyy") + "#";

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;


            object amtObj;

            amtObj = oleCmd.ExecuteScalar();
            double amt = 0.0;
            if (amtObj != null && amtObj != DBNull.Value)
                amt = (double)amtObj;
            //amt = amt + oBal;
            oleConn.Close();
            return amt;

        }

        public double getOpeningBalanceReturn(int ledgerID, string returntype, DateTime oDate, string sDataSource)
        {
            string sConStr = string.Empty;
            string sQry = string.Empty;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            sConStr = sDataSource;
            oleConn = new OleDbConnection( CreateConnectionString( sConStr));
            oleCmd = new OleDbCommand();
            oleConn.Open();
            oleCmd.Connection = oleConn;
            string type = string.Empty;
            double oBal = 0;
            /* End Ms Access Database Connection Information */

            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */

            sQry = "SELECT SUM(Amount)  As OpeningBal  FROM tblDayBook Where vouchertype='" + returntype + "' AND TransDate <#" + oDate.ToString("MM/dd/yyyy") + "#";

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;


            object amtObj;

            amtObj = oleCmd.ExecuteScalar();
            double amt = 0.0;
            if (amtObj != null && amtObj != DBNull.Value)
                amt = (double)amtObj;
            //amt = amt + oBal;
            oleConn.Close();
            return amt;

        }


        #region STOCK Report
        public DataSet getCategory(string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet ds;
            string sQry = string.Empty;
            string sConStr = string.Empty;

            sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
                
            sQry = "SELECT CategoryID,CategoryName FROM tblCategories";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            ds = new DataSet();
            oleAdp.Fill(ds);

            oleConn.Close();
            return ds;
        }

        public DataSet getBundleProducts(int purchaseID, string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry = "SELECT tblPurchaseItems.ItemCode, tblProductMaster.ProductName, tblPurchaseItems.Qty, tblPurchaseItems.PurchaseRate, tblPurchaseItems.discount, tblPurchaseItems.VAT FROM tblProductMaster INNER JOIN tblPurchaseItems ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE (((tblPurchaseItems.PurchaseID)=" + purchaseID + "));";


            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;
        }

        public DataSet getBundleProducts(string sDataSource, int iCategoryID)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet ds;
            string sQry = string.Empty;
            string sConStr = string.Empty;

            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT ItemCode,Productname,Model,ProductDesc,Stock,Rate,Unit,VAT,Discount,BuyUnit,(select sum(coir) from tblBundle where billno=0 and Itemcode=tblProductMaster.itemcode) as coir,(select Count(coir) from tblBundle where billno=0 and Itemcode=tblProductMaster.itemcode) as Bundle,(select sum(Qty) from tblBundle where billno=0 and Itemcode=tblProductMaster.itemcode) as BundleQty  FROM tblProductMaster WHERE CategoryID=" + iCategoryID + " ORDER BY Itemcode";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            ds = new DataSet();
            oleAdp.Fill(ds);

            oleConn.Close();
            return ds;

        }

        public DataSet getProducts(string sDataSource,int iCategoryID, DateTime refDate)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet ds, dsSales, dsPurcahse;
            string sQry = string.Empty;
            string sConStr = string.Empty;

            sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT ItemCode,Productname,Model,ProductDesc,Stock,Rate,Unit,VAT,Discount,BuyUnit FROM tblProductMaster WHERE CategoryID=" + iCategoryID + " ORDER BY Itemcode";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            ds = new DataSet();
            oleAdp.Fill(ds);

            sQry = "SELECT SUM(SI.Qty) as Qty,  SI.ItemCode From ((tblSales S Inner join tblSalesItems SI On S.BillNo = SI.BillNo) Inner join tblProductMaster P ON P.ItemCode = SI.ItemCode) Where P.CategoryID=" + iCategoryID + " AND S.BillDate >= #" + refDate.ToString("MM/dd/yyyy") + "#" + " Group By SI.ItemCode ORDER BY SI.Itemcode";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsSales = new DataSet();
            oleAdp.Fill(dsSales);
            int rowindex = -1;

            foreach (DataRow dr in dsSales.Tables[0].Rows)
            {
                var itemCode = dr["ItemCode"].ToString();
                decimal Qty = decimal.Parse( dr["Qty"].ToString());

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    rowindex = rowindex + 1;

                    if (row["ItemCode"].ToString() == itemCode)
                    {
                        var currentStock = decimal.Parse(ds.Tables[0].Rows[rowindex]["Stock"].ToString()) +  Qty;
                        ds.Tables[0].Rows[rowindex]["Stock"] = currentStock;
                        ds.Tables[0].Rows[rowindex].EndEdit();
                        ds.Tables[0].Rows[rowindex].AcceptChanges();
                    }

                }

                rowindex = -1;
            }

            sQry = "SELECT SUM(PI.Qty) as Qty,  PI.ItemCode From ((tblPurchase P Inner join tblPurchaseItems PI On P.PurchaseID = PI.PurchaseID) Inner join tblProductMaster PM ON PM.ItemCode = PI.ItemCode) Where PM.CategoryID=" + iCategoryID + " AND P.BillDate >= #" + refDate.ToString("MM/dd/yyyy") + "#" + " Group By PI.ItemCode ORDER BY PI.Itemcode";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsPurcahse = new DataSet();
            oleAdp.Fill(dsPurcahse);
            rowindex = -1;

            foreach (DataRow dr in dsPurcahse.Tables[0].Rows)
            {
                var itemCode = dr["ItemCode"].ToString();
                decimal Qty = decimal.Parse(dr["Qty"].ToString());

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    rowindex = rowindex + 1;

                    if (row["ItemCode"].ToString() == itemCode)
                    {
                        var currentStock = decimal.Parse(ds.Tables[0].Rows[rowindex]["Stock"].ToString()) - Qty;
                        ds.Tables[0].Rows[rowindex]["Stock"] = currentStock > 0 ? currentStock: 0;
                        ds.Tables[0].Rows[rowindex].EndEdit();
                        ds.Tables[0].Rows[rowindex].AcceptChanges();
                    }
                }

                rowindex = -1;
            }

            oleConn.Close();
            return ds;
        }

        #endregion

        #region ReOrder Level Report

        public DataSet getReorder(string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet ds;
            string sQry = string.Empty;
            string sConStr = string.Empty;

            sConStr =sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT ItemCode,Productname,Model,ProductDesc,Stock,Unit FROM tblProductMaster WHERE ROL>=Stock";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            ds = new DataSet();
            oleAdp.Fill(ds);

            oleConn.Close();
            return ds;
        }
        #endregion

        #region "Summary Report"
        public DataSet generateVatReport(string sDataSource, DateTime sDate, DateTime eDate, string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (sType == "Yes")
                sQry = "SELECT tblSales.BillDate,tblSalesItems.ItemCode,tblProductMaster.ProductName,tblSalesitems.Vat,tblProductMaster.BuyVat ,tblSalesItems.Vat-tblProductMaster.BuyVat  As DifferenceVat FROM tblSales, tblProductMaster,tblSalesItems WHERE tblSales.Cancelled=False AND tblSales.Billno=tblSalesItems.Billno and tblProductMaster.ItemCode=tblSalesitems.itemCode and (tblSales.Billdate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#)";
            else
                sQry = "SELECT tblSales.BillDate,tblSalesItems.ItemCode,tblProductMaster.ProductName,tblSalesitems.Vat,tblProductMaster.BuyVat ,tblSalesItems.Vat-tblProductMaster.BuyVat  As DifferenceVat FROM tblSales, tblProductMaster,tblSalesItems WHERE tblSales.Cancelled=False AND tblSales.purchasereturn='No' and tblSales.Billno=tblSalesItems.Billno and tblProductMaster.ItemCode=tblSalesitems.itemCode and (tblSales.Billdate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#)";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;
        }
        
        public DataSet generatePurchaseReport(int paymode, string sDataSource, DateTime sDate, DateTime eDate, string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
           // sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon";
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            //sQry = "SELECT tblPurchase.PurchaseiD,tblPurchase.BillNo, tblPurchase.BillDate, tblLedger.LedgerName, tblPurchase.TotalAmt FROM ((tblPurchase INNER JOIN (tblProductMaster INNER JOIN tblPurchaseItems ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode) ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID) INNER JOIN tblDayBook ON tblPurchase.JournalID = tblDayBook.TransNo) INNER JOIN tblLedger ON tblPurchase.SupplierID = tblLedger.LedgerID WHERE (paymode=" + paymode  + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
            if (sType == "Yes")
                sQry = "SELECT tblPurchase.PurchaseID, tblPurchase.BillNo, tblPurchase.BillDate, tblLedger.LedgerName, tblPurchase.TotalAmt,salesreturn,salesreturnreason FROM tblPurchase INNER JOIN tblLedger ON tblPurchase.SupplierID = tblLedger.LedgerID WHERE (paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
            else
                sQry = "SELECT tblPurchase.PurchaseID, tblPurchase.BillNo, tblPurchase.BillDate, tblLedger.LedgerName, tblPurchase.TotalAmt,salesreturn,salesreturnreason FROM tblPurchase INNER JOIN tblLedger ON tblPurchase.SupplierID = tblLedger.LedgerID WHERE (salesreturn='No' and paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }

        public DataSet getProducts(int purchaseID,string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry = "SELECT tblPurchaseItems.ItemCode, tblProductMaster.ProductName, tblPurchaseItems.Qty, tblPurchaseItems.PurchaseRate, tblPurchaseItems.discount, tblPurchaseItems.VAT FROM tblProductMaster INNER JOIN tblPurchaseItems ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE (((tblPurchaseItems.PurchaseID)="+ purchaseID +"));";


            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;
        }
        public DataSet generateSalesReport(int paymode, string sDataSource, DateTime sDate, DateTime eDate,string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            if(sType == "Yes")
                sQry = "SELECT tblSales.BillNo, tblSales.BillDate, tblLedger.LedgerName, tblDayBook.Amount,tblSales.purchasereturn,tblSales.purchasereturnreason FROM (tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN tblLedger ON tblDayBook.DebtorID = tblLedger.LedgerID WHERE (tblSales.PayMode=" + paymode + " and tblSales.Cancelled=False  and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
            else
                sQry = "SELECT tblSales.BillNo, tblSales.BillDate, tblLedger.LedgerName, tblDayBook.Amount,tblSales.purchasereturn,tblSales.purchasereturnreason FROM (tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN tblLedger ON tblDayBook.DebtorID = tblLedger.LedgerID WHERE (tblSales.PayMode=" + paymode + " and tblSales.Cancelled=False and purchasereturn='No'  and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
            //sQry = "SELECT tblSales.BillNo, tblSales.BillDate, tblLedger.LedgerName, tblDayBook.Amount FROM (tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN tblLedger ON tblDayBook.DebtorID = tblLedger.LedgerID WHERE  (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";

            //sQry = "SELECT tblSales.BillDate, tblSales.BillNo, tblLedger.LedgerName, tblDayBook.Amount FROM ((tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblLedger ON tblSales.CustomerID = tblLedger.LedgerID WHERE (tblSales.paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }

        public DataSet generateSalesReport(string sDataSource, DateTime sDate, DateTime eDate)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry; string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */

            sQry = "SELECT tblSales.BillNo, tblSales.BillDate,tblSales.CustomerName,tblSales.Paymode, tblLedger.LedgerName, tblDayBook.Amount FROM (tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN tblLedger ON tblDayBook.DebtorID = tblLedger.LedgerID WHERE tblSales.Cancelled=False and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";

            //sQry = "SELECT tblSales.BillDate, tblSales.BillNo, tblLedger.LedgerName, tblDayBook.Amount FROM ((tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblLedger ON tblSales.CustomerID = tblLedger.LedgerID WHERE (tblSales.paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }

        public DataSet getProductsSales(int Billno, string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry = "SELECT tblSalesItems.ItemCode,tblProductMaster.Model,tblProductMaster.ProductDesc, tblProductMaster.ProductName, tblSalesItems.Qty, tblSalesItems.Rate, tblSalesItems.Discount, tblSalesItems.Vat,tblSalesITems.cst, tblSalesItems.BillNo FROM tblSales INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo WHERE (((tblSalesItems.BillNo)=" + Billno  + "));";


            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;
        }
        public DataSet generateCashReceived(string sDataSource, DateTime sDate, DateTime eDate,string pType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            if (pType == "Yes")
                sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName As Debtor,tblLedger_1.LedgerName As Creditor, tblDayBook.Amount, tblDayBook.VoucherType FROM (tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.DebtorID = tblLedger.LedgerID) INNER JOIN tblLedger AS tblLedger_1 ON tblDayBook.CreditorID = tblLedger_1.LedgerID WHERE   (tblLedger_1.GroupID=4 OR tblLedger.GroupID=4)  AND (tblDayBook.VoucherType = 'Purchase Return' OR tblDayBook.VoucherType='Receipt' OR  (tblDayBook.VoucherType='Journal' AND tblLedger.GroupID=4))  AND  (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
              else
                sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName As Debtor,tblLedger_1.LedgerName As Creditor, tblDayBook.Amount, tblDayBook.VoucherType FROM (tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.DebtorID = tblLedger.LedgerID) INNER JOIN tblLedger AS tblLedger_1 ON tblDayBook.CreditorID = tblLedger_1.LedgerID WHERE   (tblLedger_1.GroupID=4 OR tblLedger.GroupID=4)  AND (tblDayBook.VoucherType='Receipt' OR  (tblDayBook.VoucherType='Journal' AND tblLedger.GroupID=4))  AND  (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
            //sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName, tblDayBook.Amount FROM (tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.CreditorID = tblLedger.LedgerID) INNER JOIN tblLedger AS tblLedger_1 ON tblDayBook.DebtorID = tblLedger_1.LedgerID WHERE tblLedger_1.GroupID=4 AND  tblDayBook.VoucherType='Journal' OR  tblDayBook.VoucherType='Receipt'OR  tblDayBook.VoucherType='Payment' and (TransDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) );";
            // sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName, tblDayBook.Amount, tblDayBook.DebtorID FROM tblDayBook INNER JOIN tblLedger ON tblDayBook.CreditorID = tblLedger.LedgerID WHERE (tblDayBook.DebtorID=1 and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }

        public DataSet generateChequeReceived(string sDataSource, DateTime sDate, DateTime eDate,string pType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            if(pType=="Yes")
                sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName As Debtor,tblLedger_1.LedgerName As Creditor, tblDayBook.Amount, tblDayBook.VoucherType FROM (tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.DebtorID = tblLedger.LedgerID) INNER JOIN tblLedger AS tblLedger_1 ON tblDayBook.CreditorID = tblLedger_1.LedgerID WHERE   (tblLedger_1.GroupID=3 OR tblLedger.GroupID=3)  AND (tblDayBook.VoucherType = 'Purchase Return' OR tblDayBook.VoucherType='Receipt' OR  (tblDayBook.VoucherType='Journal' AND tblLedger.GroupID=3))  AND  (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
            else
                sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName As Debtor,tblLedger_1.LedgerName As Creditor, tblDayBook.Amount, tblDayBook.VoucherType FROM (tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.DebtorID = tblLedger.LedgerID) INNER JOIN tblLedger AS tblLedger_1 ON tblDayBook.CreditorID = tblLedger_1.LedgerID WHERE (tblLedger_1.GroupID=3 OR tblLedger.GroupID=3)  AND (tblDayBook.VoucherType='Receipt' OR  (tblDayBook.VoucherType='Journal' AND tblLedger.GroupID=3))  AND  (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
            //sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName, tblDayBook.Amount FROM (tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.CreditorID = tblLedger.LedgerID) INNER JOIN tblLedger AS tblLedger_1 ON tblDayBook.DebtorID = tblLedger_1.LedgerID WHERE tblLedger_1.GroupID=4 AND  tblDayBook.VoucherType='Journal' OR  tblDayBook.VoucherType='Receipt'OR  tblDayBook.VoucherType='Payment' and (TransDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) );";
            // sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName, tblDayBook.Amount, tblLedger.GroupID FROM tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.DebtorID = tblLedger.LedgerID WHERE (tblLedger.GroupID=3 and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
            //sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName, tblDayBook.Amount FROM tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.CreditorID = tblLedger.LedgerID WHERE (tblLedger.GroupID=3 and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }


        public DataSet generateCashPaid(string sDataSource, DateTime sDate, DateTime eDate, string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */

            if (sType == "Yes")
                 sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName As Debtor,tblLedger_1.LedgerName As Creditor, tblDayBook.Amount, tblDayBook.VoucherType FROM (tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.DebtorID = tblLedger.LedgerID) INNER JOIN tblLedger AS tblLedger_1 ON tblDayBook.CreditorID = tblLedger_1.LedgerID WHERE   (tblLedger_1.GroupID=4 OR tblLedger.GroupID=4)  AND (tblDayBook.VoucherType='Sales Return' OR  tblDayBook.VoucherType='Payment' OR  (tblDayBook.VoucherType='Journal' AND tblLedger_1.GroupID=4))  AND  (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
            else
                sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName As Debtor,tblLedger_1.LedgerName As Creditor, tblDayBook.Amount, tblDayBook.VoucherType FROM (tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.DebtorID = tblLedger.LedgerID) INNER JOIN tblLedger AS tblLedger_1 ON tblDayBook.CreditorID = tblLedger_1.LedgerID WHERE   (tblLedger_1.GroupID=4 OR tblLedger.GroupID=4)  AND (tblDayBook.VoucherType='Payment' OR  (tblDayBook.VoucherType='Journal' AND tblLedger_1.GroupID=4))  AND  (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
        
        
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }
        public DataSet generateChequePaid(string sDataSource, DateTime sDate, DateTime eDate,string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            if(sType=="Yes")
                sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName As Debtor,tblLedger_1.LedgerName As Creditor, tblDayBook.Amount, tblDayBook.VoucherType FROM (tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.DebtorID = tblLedger.LedgerID) INNER JOIN tblLedger AS tblLedger_1 ON tblDayBook.CreditorID = tblLedger_1.LedgerID WHERE   (tblLedger_1.GroupID=3 OR tblLedger.GroupID=3)  AND (tblDayBook.VoucherType='Sales Return' OR tblDayBook.VoucherType='Payment' OR  (tblDayBook.VoucherType='Journal' AND tblLedger_1.GroupID=3))  AND  (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
            else
                sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName As Debtor,tblLedger_1.LedgerName As Creditor, tblDayBook.Amount, tblDayBook.VoucherType FROM (tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.DebtorID = tblLedger.LedgerID) INNER JOIN tblLedger AS tblLedger_1 ON tblDayBook.CreditorID = tblLedger_1.LedgerID WHERE   (tblLedger_1.GroupID=3 OR tblLedger.GroupID=3)  AND (tblDayBook.VoucherType='Payment' OR  (tblDayBook.VoucherType='Journal' AND tblLedger_1.GroupID=3))  AND  (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }

           public DataSet itemwisePurchase(string sDataSource, DateTime sDate, DateTime eDate,string sType)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               if(sType=="Yes")
                   sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, Sum(tblPurchaseItems.Qty) AS SumQty,tblPurchase.SalesReturn FROM tblProductMaster INNER JOIN (tblPurchase INNER JOIN tblPurchaseItems ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID) ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE   tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "# GROUP BY tblProductMaster.ItemCode, tblProductMaster.ProductName,tblPurchase.SalesReturn";
               else
                   sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, Sum(tblPurchaseItems.Qty) AS SumQty,tblPurchase.SalesReturn FROM tblProductMaster INNER JOIN (tblPurchase INNER JOIN tblPurchaseItems ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID) ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE tblPurchase.SalesReturn='No' OR tblPurchase.SalesReturn<>'Yes'  AND  tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "# GROUP BY tblProductMaster.ItemCode, tblProductMaster.ProductName,tblPurchase.SalesReturn";
               
               //sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName, tblDayBook.Amount FROM (tblDayBook INNER JOIN (tblGroups INNER JOIN tblLedger ON tblGroups.GroupID = tblLedger.GroupID) ON tblDayBook.DebtorID = tblLedger.LedgerID) INNER JOIN tblLedger AS tblLedger_1 ON tblDayBook.CreditorID = tblLedger_1.LedgerID WHERE (tblLedger_1.GroupID=3   and ( tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
               // sQry = "SELECT tblDayBook.TransDate, tblDayBook.Narration, tblLedger.LedgerName, tblDayBook.Amount, tblDayBook.DebtorID FROM tblDayBook INNER JOIN tblLedger ON tblDayBook.CreditorID = tblLedger.LedgerID WHERE (tblDayBook.DebtorID=1 and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);
               return dsParentQry;

           }
           public DataSet itemwiseSales(string sDataSource, DateTime sDate, DateTime eDate, string pType)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               if (pType == "Yes")
               {
                   sQry = "SELECT  tblSalesItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc,  SUM(tblSalesItems.qty) AS Qty, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.VAT/100))) As Amount,tblSales.PurchaseReturn FROM tblProductMaster INNER JOIN (tblSalesItems INNER JOIN (tblSales INNER JOIN tblDayBook ON tblsales.JournalID=tblDaybook.Transno) ON tblSales.Billno=tblSalesItems.Billno) ON tblProductMaster.itemcode=tblSalesItems.Itemcode Where (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblSales.Cancelled=False   GROUP BY tblSalesItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc,tblSales.purchasereturn";
                   //sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, tblSales.BillDate, Sum(tblSalesItems.Qty) AS SumQty FROM tblSales INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo GROUP BY tblProductMaster.ItemCode, tblProductMaster.ProductName, tblSales.BillDate,tblSales.Cancelled Having tblSales.Cancelled=False and tblSales.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
               }

               else
               {
                   sQry = "SELECT  tblSalesItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc,  SUM(tblSalesItems.qty) AS Qty, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.VAT/100))) As Amount,tblSales.PurchaseReturn FROM tblProductMaster INNER JOIN (tblSalesItems INNER JOIN (tblSales INNER JOIN tblDayBook ON tblsales.JournalID=tblDaybook.Transno) ON tblSales.Billno=tblSalesItems.Billno) ON tblProductMaster.itemcode=tblSalesItems.Itemcode Where (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblSales.Cancelled=False AND (tblSales.PurchaseReturn='No' OR tblSales.PurchaseReturn<>'Yes')   GROUP BY tblSalesItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc,tblSales.purchasereturn";
                   //sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, tblSales.BillDate, Sum(tblSalesItems.Qty) AS SumQty,tblSales.PurchaseReturn FROM tblSales INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo GROUP BY tblProductMaster.ItemCode, tblProductMaster.ProductName, tblSales.BillDate,tblSales.Cancelled,tblSales.PurchaseReturn Having tblSales.PurchaseReturn='No' AND tblSales.Cancelled=False and tblSales.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";

               }
               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);
               return dsParentQry;

           }

           public DataSet GrossProfit(string sDataSource, DateTime sDate, DateTime eDate,string sType,string pType)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               if (sType == "Yes" && pType == "Yes")
                   sQry = "SELECT tblProductMaster.ItemCode, tblDayBook.TransDate, tblProductMaster.ProductName, Sum(tblSalesItems.Qty) AS SumOfQty, tblProductMaster.Discount As Dis, tblProductMaster.VAT As Va, Sum(tblProductMaster.BuyRate*tblProductMaster.qty-(tblProductMaster.BuyRate*tblProductMaster.qty* (tblProductMaster.Discount/100))+(tblProductMaster.BuyRate*tblProductMaster.qty* (tblProductMaster.VAT/100))) As BuyRate, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.VAT/100))) As SoldRate, tblSalesItems.Vat, tblSalesItems.Discount,tblDaybook.VoucherType FROM (tblSales INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo GROUP BY tblProductMaster.ItemCode, tblDayBook.TransDate, tblProductMaster.ProductName, tblProductMaster.Discount, tblProductMaster.VAT, tblProductMaster.Rate, tblSalesItems.Rate, tblSalesItems.Vat, tblSalesItems.Discount,tblDayBook.VoucherType,tblSales.Cancelled  Having  tblSales.Cancelled=False AND (tblDayBook.TransDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
               else if (sType == "Yes" && pType == "No")
                   sQry = "SELECT tblProductMaster.ItemCode, tblDayBook.TransDate, tblProductMaster.ProductName, Sum(tblSalesItems.Qty) AS SumOfQty, tblProductMaster.Discount As Dis, tblProductMaster.VAT As Va, Sum(tblProductMaster.BuyRate*tblProductMaster.qty-(tblProductMaster.BuyRate*tblProductMaster.qty* (tblProductMaster.Discount/100))+(tblProductMaster.BuyRate*tblProductMaster.qty* (tblProductMaster.VAT/100))) As BuyRate, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.VAT/100))) As SoldRate, tblSalesItems.Vat, tblSalesItems.Discount,tblDayBook.VoucherType FROM (tblSales INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo   GROUP BY tblProductMaster.ItemCode, tblDayBook.TransDate, tblProductMaster.ProductName, tblProductMaster.Discount, tblProductMaster.VAT, tblProductMaster.Rate, tblSalesItems.Rate, tblSalesItems.Vat, tblSalesItems.Discount,tblDayBook.VoucherType,tblSales.Cancelled   Having  tblDayBook.VoucherType <> 'Purchase Return' AND  tblSales.Cancelled=False AND  (tblDayBook.TransDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;)";
               else if (sType == "No" && pType == "Yes")
                   sQry = "SELECT tblProductMaster.ItemCode, tblDayBook.TransDate, tblProductMaster.ProductName, Sum(tblSalesItems.Qty) AS SumOfQty, tblProductMaster.Discount As Dis, tblProductMaster.VAT As Va, Sum(tblProductMaster.BuyRate*tblProductMaster.qty-(tblProductMaster.BuyRate*tblProductMaster.qty* (tblProductMaster.Discount/100))+(tblProductMaster.BuyRate*tblProductMaster.qty* (tblProductMaster.VAT/100))) As BuyRate, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.VAT/100))) As SoldRate, tblSalesItems.Vat, tblSalesItems.Discount,tblDayBook.VoucherType FROM (tblSales INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo   GROUP BY tblProductMaster.ItemCode, tblDayBook.TransDate, tblProductMaster.ProductName, tblProductMaster.Discount, tblProductMaster.VAT, tblProductMaster.Rate, tblSalesItems.Rate, tblSalesItems.Vat, tblSalesItems.Discount,tblDayBook.VoucherType,tblSales.Cancelled   Having tblDayBook.VoucherType <> 'Sales Return' AND  tblSales.Cancelled=False AND tblDayBook.TransDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
               else
                   sQry = "SELECT tblProductMaster.ItemCode, tblDayBook.TransDate, tblProductMaster.ProductName, Sum(tblSalesItems.Qty) AS SumOfQty, tblProductMaster.Discount As Dis, tblProductMaster.VAT As Va, Sum(tblProductMaster.BuyRate*tblProductMaster.qty-(tblProductMaster.BuyRate*tblProductMaster.qty* (tblProductMaster.Discount/100))+(tblProductMaster.BuyRate*tblProductMaster.qty* (tblProductMaster.VAT/100))) As BuyRate, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.VAT/100))) As SoldRate, tblSalesItems.Vat, tblSalesItems.Discount,tblDayBook.VoucherType FROM (tblSales INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo   GROUP BY tblProductMaster.ItemCode, tblDayBook.TransDate, tblProductMaster.ProductName, tblProductMaster.Discount, tblProductMaster.VAT, tblProductMaster.Rate, tblSalesItems.Rate, tblSalesItems.Vat, tblSalesItems.Discount,tblDayBook.VoucherType,tblSales.Cancelled   Having (tblDayBook.VoucherType <> 'Sales Return' AND tblDayBook.VoucherType <> 'Purchase Return' AND tblSales.Cancelled=False) AND tblDayBook.TransDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
              
               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);
               return dsParentQry;

           }
           public DataSet GrossProfitAndLoss(string sDataSource, DateTime sDate, DateTime eDate)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;

               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               sQry = "SELECT tblProductMaster.ItemCode,tblDayBook.Transdate,  tblProductMaster.ProductName+ ' '+ tblProductMaster.ProductDesc  + ' '+ tblProductMaster.Model As ProductName, Sum(tblSalesItems.Qty) AS SumOfQty, Sum(tblProductMaster.Rate*tblSalesItems.qty-(tblProductMaster.Rate*tblSalesItems.qty* (tblProductMaster.Discount/100))+(tblProductMaster.Rate*tblSalesItems.qty* (tblProductMaster.VAT/100))) As BuyRate, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.VAT/100))) As SoldRate,tblProductMaster.Stock As ClosingStock, tblDaybook.VoucherType FROM (tblSales INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo    GROUP BY tblProductMaster.ItemCode, tblProductMaster.ProductName,tblProductMaster.ProductDesc  ,tblProductMaster.Model,tblDayBook.VoucherType,tblSales.Cancelled,tblProductMaster.stock,tblDayBook.Transdate  Having  tblSales.Cancelled=False AND (tblDayBook.TransDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) UNION ALL SELECT tblProductMaster.ItemCode,tblDayBook.Transdate,  tblProductMaster.ProductName+ ' '+ tblProductMaster.ProductDesc  + ' '+ tblProductMaster.Model As ProductName, Sum(tblPurchaseItems.Qty) AS SumOfQty, Sum(tblProductMaster.Rate*tblPurchaseItems.qty-(tblProductMaster.Rate*tblPurchaseItems.qty* (tblProductMaster.Discount/100))+(tblProductMaster.Rate*tblPurchaseItems.qty* (tblProductMaster.VAT/100))) As BuyRate, Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty* (tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty* (tblPurchaseItems.VAT/100))) As SoldRate,tblProductMaster.Stock As ClosingStock, tblDaybook.VoucherType FROM (tblPurchase INNER JOIN (tblProductMaster INNER JOIN tblPurchaseItems ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode) ON tblPurchase.purchaseID = tblPurchaseItems.purchaseID) INNER JOIN tblDayBook ON tblPurchase.JournalID = tblDayBook.TransNo    GROUP BY tblProductMaster.ItemCode, tblDayBook.VoucherType,tblProductMaster.ProductName,tblProductMaster.ProductDesc  ,tblProductMaster.Model,tblProductMaster.stock,tblDayBook.Transdate HAVING tblDayBook.VoucherType='Sales Return' AND (tblDayBook.TransDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#)";
               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);
               return dsParentQry;

           }
           public double GetPurchaseTotal(string sDataSource, DateTime sDate, DateTime eDate)
           {
               //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry; string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleConn.Open();
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;

               sQry = "SELECT Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS PurchaseRate FROM tblPurchaseITems,tblPurchase WHERE (tblPurchase.SalesReturn <> 'Yes' OR tblPurchase.SalesReturn is null) AND  tblPurchase.purchaseID = tblPurchaseitems.purchaseID AND tblPurchase.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               //oleAdp = new OleDbDataAdapter(oleCmd);
               //dsParentQry = new DataSet();
               //oleAdp.Fill(dsParentQry);
               //return dsParentQry;
               object retVal = oleCmd.ExecuteScalar();
               double amt = 0;
               if (retVal != null && retVal != DBNull.Value)
               {
                   amt = (double)oleCmd.ExecuteScalar();
               }
               oleConn.Close();
               return amt;

           }
           public double GetSalesTotal(string sDataSource, DateTime sDate, DateTime eDate)
           {
               //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry; string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleConn.Open();
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               sQry = "SELECT Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) As SoldRate FROM tblSalesItems,tblSales WHERE (tblSales.PurchaseReturn <> 'Yes' OR tblSales.PurchaseReturn is null) AND tblSales.billno = tblSalesItems.Billno AND tblSales.cancelled=false AND tblSales.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";
               //sQry = "SELECT Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS PurchaseRate FROM tblPurchaseITems,tblPurchase WHERE tblPurchase.purchaseID = tblPurchaseitems.purchaseID AND tblPurchase.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               //oleAdp = new OleDbDataAdapter(oleCmd);
               //dsParentQry = new DataSet();
               //oleAdp.Fill(dsParentQry);
               //return dsParentQry;
               object retVal = oleCmd.ExecuteScalar();
               double amt = 0;
               if (retVal != null && retVal != DBNull.Value)
               {
                   amt = (double)oleCmd.ExecuteScalar();
               }
               oleConn.Close();
               return amt;

           }

           public double GetExpenseTotal(string sDataSource, DateTime sDate, DateTime eDate)
           {
               //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry; string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleConn.Open();
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               sQry = "SELECT SUM(Amount) As Expenses FROM tblDayBook WHERE debtorID IN (SELECT LedgerID FROM tblLedger WHERE GroupID in(SELECT  GroupID From tblGroups Where HeadingID =11 AND tblDayBook.TransDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "# ) )";
               //sQry = "SELECT Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) As SoldRate FROM tblSalesItems,tblSales WHERE tblSales.billno = tblSalesItems.Billno AND tblSales.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";
               //sQry = "SELECT Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS PurchaseRate FROM tblPurchaseITems,tblPurchase WHERE tblPurchase.purchaseID = tblPurchaseitems.purchaseID AND tblPurchase.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               //oleAdp = new OleDbDataAdapter(oleCmd);
               //dsParentQry = new DataSet();
               //oleAdp.Fill(dsParentQry);
               //return dsParentQry;
               object retVal = oleCmd.ExecuteScalar();
               double amt = 0;
               if (retVal != null && retVal != DBNull.Value)
               {
                   amt = (double)oleCmd.ExecuteScalar();
               }
               oleConn.Close();
               return amt;

           }

           //SELECT  Sum(tblProductMaster.Rate*tblProductMaster.stock-(tblProductMaster.Rate*tblProductMaster.stock* (tblProductMaster.Discount/100))+(tblProductMaster.Rate*tblProductMaster.stock* (tblProductMaster.VAT/100))) As ClosingStock FROM tblProductMaster
           public double GetClosingStockTotal(string sDataSource)
           {
               //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry; string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleConn.Open();
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               sQry = "SELECT  Sum(tblProductMaster.Rate*tblProductMaster.stock-(tblProductMaster.Rate*tblProductMaster.stock* (tblProductMaster.Discount/100))+(tblProductMaster.Rate*tblProductMaster.stock* (tblProductMaster.VAT/100))) As ClosingStock FROM tblProductMaster";
               //sQry = "SELECT Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) As SoldRate FROM tblSalesItems,tblSales WHERE tblSales.billno = tblSalesItems.Billno AND tblSales.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";
               //sQry = "SELECT Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS PurchaseRate FROM tblPurchaseITems,tblPurchase WHERE tblPurchase.purchaseID = tblPurchaseitems.purchaseID AND tblPurchase.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               //oleAdp = new OleDbDataAdapter(oleCmd);
               //dsParentQry = new DataSet();
               //oleAdp.Fill(dsParentQry);
               //return dsParentQry;
               object retVal = oleCmd.ExecuteScalar();
               double amt = 0;
               if (retVal != null && retVal != DBNull.Value)
               {
                   amt = (double)oleCmd.ExecuteScalar();
               }
               oleConn.Close();
               return amt;

           }
        
        #endregion

        #region "Stock List Report"
           public DataTable GenerateDs()
           {
               DataSet ds = new DataSet();
               DataTable dt = new DataTable();

               DataColumn dc;
               DataRow dr;
               int days = 0;

               dc = new DataColumn("itemCode");
               dt.Columns.Add(dc);

               //dc = new DataColumn("TransDate");
               //dt.Columns.Add(dc);
               dc = new DataColumn("ActualStock");
               dt.Columns.Add(dc);
               dc = new DataColumn("PhysicalStock");
               dt.Columns.Add(dc);
               
               return dt;
           }
           public DataTable GenerateDs(string itemCode,double ActualStock,double PhysicalStock)
           {
               DataSet ds = new DataSet();
               DataTable dt = new DataTable();

               DataColumn dc;
               DataRow dr;
               int days = 0;

               dc = new DataColumn("itemCode");
               dt.Columns.Add(dc);

               //dc = new DataColumn("TransDate");
               //dt.Columns.Add(dc);
               dc = new DataColumn("ActualStock");
               dt.Columns.Add(dc);
               dc = new DataColumn("PhysicalStock");
               dt.Columns.Add(dc);
               

               //ds.Tables.Add(dt);
               dr = dt.NewRow();
               dr["itemCode"] = itemCode;

               dr["ActualStock"] = ActualStock.ToString();
               //dr["TransDate"] = "";
               dr["PhysicalStock"] = PhysicalStock.ToString();
               
               dt.Rows.Add(dr);
               //ds.Tables[0].Rows.Add(dr);
               return dt;
           }
           public DataSet verifyStock(string sDataSource,DateTime sDate)
           {
               DataTable dtNew = new DataTable();
               DataSet ds = new DataSet();
               DataSet returnDs = new DataSet();
               DataTable grdDt = new DataTable();
               double purchaseQty = 0;
               double salesQty = 0;
               double openingStock = 0;
               double physicalStock = 0;
               double ActualStock = 0;
              
               ds = getProductStock(sDataSource);
               
               string itemCode = string.Empty;
               string itemName = string.Empty;
               if (ds != null)
               {
                   dtNew = GenerateDs();
                   returnDs.Tables.Add(dtNew);
                   if (ds.Tables[0].Rows.Count > 0)
                   {
                       foreach (DataRow dr in ds.Tables[0].Rows)
                       {
                           if (dr["itemCode"] != null)
                           {
                               itemCode = dr["itemCode"].ToString().Replace("&quot;", "\"");
                               itemName = dr["Product"].ToString();
                               purchaseQty = getStockPurchase(sDataSource, itemCode, sDate);
                               salesQty = getStockSales(sDataSource, itemCode, sDate);
                               openingStock = getOpeningStock(sDataSource, itemCode);
                               physicalStock = getPhysicalStock(sDataSource, itemCode, sDate);
                               ActualStock = openingStock + (purchaseQty - salesQty);
                               //if (ActualStock != physicalStock)
                               //{
                                   grdDt = GenerateDs(itemName, ActualStock, physicalStock);
                                   if (grdDt != null)
                                   {
                                       for (int k = 0; k <= grdDt.Rows.Count - 1; k++)
                                       {

                                           if (grdDt != null && grdDt.Rows.Count > 0)
                                           {
                                            
                                               returnDs.Tables[0].ImportRow(grdDt.Rows[k]);
                                           }
                                             
                                       }
                                   }
                              // }
                           }
                       }
                   }


                    return returnDs;
               }
               else
               {
                   return null;
               }
           }
           public double getPhysicalStock(string sDataSource, string itemCode, DateTime sDate)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;

               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               sConStr = sDataSource;  //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleConn.Open();
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               sQry = "SELECT Stock FROM ClosingStock Where ClosingStock.Itemcode='" + itemCode + "' AND ClosingStock.ClosingDate =#" + sDate.ToString("MM/dd/yyyy") + "#";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               double qty = 0;

               object retVal = oleCmd.ExecuteScalar();
               if ((retVal != null) && (retVal != DBNull.Value))
               {
                   qty = Convert.ToDouble(oleCmd.ExecuteScalar());
                  

               }
               oleConn.Close();
               return qty;
           }
           public DataSet getProductList(string sDataSource)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               sConStr = sDataSource; // "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));

               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               //sQry = "SELECT ItemCode + ' - ' + ProductDesc As ProductCode,itemcode,ProductName + '-' + Stock As ProductName FROM tblProductMaster";

               sQry = "SELECT ItemCode + ' | ' + ProductDesc + ' | ' + Model As ProductCode,ProductName + '@' + CStr(stock) + '@' + ItemCode As Product FROM tblProductMaster";
               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);
               return dsParentQry;
           }
          
           public DataSet getProductStock(string sDataSource)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               sConStr = sDataSource; // "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleConn.Open();
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               //sQry = "SELECT ItemCode + ' - ' + ProductDesc As ProductCode,itemcode,ProductName + '-' + Stock As ProductName FROM tblProductMaster";

               sQry = "SELECT itemCode,ProductName+ ' - ' + Model + ' - ' + ProductDesc As Product  FROM tblProductMaster";
               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);
               oleConn.Close();
               return dsParentQry;
           }

           #region Stock Verification Report
          
           #endregion

           public double getStockIN(string sDataSource, string itemCode, DateTime sDate)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;

               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               sConStr = sDataSource;  //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));


               oleCmd = new OleDbCommand();
               oleConn.Open();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               sQry = "SELECT Sum(Qty) As INQty FROM tblExecution,tblCompProduct Where tblExecution.CompID = tblCompProduct.CompID AND tblExecution.Itemcode='" + itemCode + "' AND tblCompProduct.CDate <=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblExecution.InOut = 'IN'  Group By tblExecution.Itemcode ";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               double qty = 0;

               object retVal = oleCmd.ExecuteScalar();
               if ((retVal != null) && (retVal != DBNull.Value))
               {
                   qty = Convert.ToDouble(oleCmd.ExecuteScalar());


               }
               oleConn.Close();
               return qty;
           }

           public double getStockOUT(string sDataSource, string itemCode, DateTime sDate)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;

               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               sConStr = sDataSource;  //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));


               oleCmd = new OleDbCommand();
               oleConn.Open();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               sQry = "SELECT Sum(Qty) As INQty FROM tblExecution,tblCompProduct Where tblExecution.CompID = tblCompProduct.CompID AND tblExecution.Itemcode='" + itemCode + "' AND tblCompProduct.CDate <=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblExecution.InOut = 'OUT' Group By tblExecution.Itemcode ";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               double qty = 0;

               object retVal = oleCmd.ExecuteScalar();
               if ((retVal != null) && (retVal != DBNull.Value))
               {
                   qty = Convert.ToDouble(oleCmd.ExecuteScalar());


               }
               oleConn.Close();
               return qty;
           }

           public double getStockPurchase(string sDataSource, string itemCode, DateTime sDate)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
            
               string sQry = string.Empty;
               string sConStr = string.Empty;

              
               /* Start Ms Access Database Connection Information */
               sConStr = sDataSource;  //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));


               oleCmd = new OleDbCommand();
               oleConn.Open();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               sQry = "SELECT Sum(Qty) As PurchaseQty FROM tblPurchase,tblPurchaseItems Where tblPurchase.PurchaseID=tblPurchaseitems.PurchaseID AND tblPurchaseItems.Itemcode='" + itemCode + "' AND tblPurchase.BillDate <=#"+ sDate.ToString("MM/dd/yyyy") + "#  Group By tblPurchaseItems.itemcode";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               double qty = 0;

               object retVal = oleCmd.ExecuteScalar();
                if ((retVal != null) && (retVal != DBNull.Value))
                { 
                    qty = Convert.ToDouble(oleCmd.ExecuteScalar());
                    
                   
                }
                oleConn.Close();
               return qty;
           }
           public double getStockSales(string sDataSource, string itemCode, DateTime sDate)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
             
               string sQry = string.Empty;
               string sConStr = string.Empty;

               
               /* Start Ms Access Database Connection Information */
               sConStr = sDataSource;  //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleCmd = new OleDbCommand();
               oleConn.Open();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               sQry = "SELECT Sum(Qty) As SalesQty FROM tblSales,tblSalesItems Where tblSales.Cancelled=false AND tblSales.Billno=tblSalesitems.Billno AND tblSalesItems.Itemcode='" + itemCode +"' AND tblSales.BillDate <=#" + sDate.ToString("MM/dd/yyyy")   + "#  Group By tblSalesitems.itemcode";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               double qty = 0;

               object retVal = oleCmd.ExecuteScalar();
               if ((retVal != null) && (retVal != DBNull.Value))
               {
                   qty = Convert.ToDouble(oleCmd.ExecuteScalar());
                

               }
               oleConn.Close();
               return qty;
           }
           //SELECT SUM(qty) FROM tblSalesItems,tblSales WHERE tblSales.Billno = tblSalesItems.Billno AND ItemCode ='D01' AND cancelled = false
           public double getOpeningStockSales(string sDataSource, string itemCode, DateTime sDate)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               sConStr = sDataSource;  //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleConn.Open();
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               sQry = "SELECT SUM(qty) FROM tblSalesItems,tblSales WHERE tblSales.Billno = tblSalesItems.Billno AND ItemCode ='" + itemCode + "' AND cancelled = false AND billdate <#" + sDate.ToString("MM/dd/yyyy")  +"#";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;

               double cnt = 0;
               object retVal = oleCmd.ExecuteScalar();
               if ((retVal != null) && (retVal != DBNull.Value))
               {
                   cnt = Convert.ToDouble(oleCmd.ExecuteScalar());


               }
               oleConn.Close();
               return cnt;
           }
           public double getOpeningStockPurchase(string sDataSource, string itemCode, DateTime sDate)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               sConStr = sDataSource;  //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleConn.Open();
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               sQry = "SELECT SUM(qty) FROM tblPurchaseItems,tblPurchase WHERE tblPurchase.PurchaseID = tblPurchaseItems.purchaseID AND ItemCode ='" + itemCode + "' AND billdate <#" + sDate.ToString("MM/dd/yyyy") + "#";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;

               double cnt = 0;
               object retVal = oleCmd.ExecuteScalar();
               if ((retVal != null) && (retVal != DBNull.Value))
               {
                   cnt = Convert.ToDouble(oleCmd.ExecuteScalar());


               }
               oleConn.Close();
               return cnt;
           }  
           public double getOpeningStock(string sDataSource,string itemCode)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               sConStr = sDataSource;  //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleConn.Open();
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               sQry = "SELECT  OpeningStock  FROM tblStock WHERE itemCode='" + itemCode + "'";


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;

               double cnt = 0;
               object retVal = oleCmd.ExecuteScalar();
               if ((retVal != null) && (retVal != DBNull.Value))
               {
                   cnt = Convert.ToDouble(oleCmd.ExecuteScalar());
                   

               }
               oleConn.Close();
               return cnt;
           }  
           public DataSet getProductStockList(string sDataSource,string itemCode,DateTime sDate,DateTime eDate)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               sConStr = sDataSource;  //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               //sQry = "SELECT * FROM (SELECT  b.billdate ,'PURCHASE' As Purchase/Sale ,qty,c.ledgername As LedgerName  from tblpurchaseitems a,tblpurchase b,tblledger c WHERE a.purchaseid=b.purchaseid AND b.supplierid = c.ledgerid AND itemcode='" + itemCode + "' UNION SELECT  d.billdate,'SALES' As Purchase/Sale,c.qty,e.ledgername As LedgerName  from tblsalesitems c,tblsales d,tblledger e WHERE c.billno=d.billno  AND d.cancelled=false AND d.customerid = e.ledgerid AND itemcode='" + itemCode + "') order by 1 ";

               //sQry = "select * from (select  b.billdate ,'PURCHASE' As 'Purchase/Sale',qty,c.ledgername As LedgerName   from tblpurchaseitems a,tblpurchase b,tblledger c where a.purchaseid=b.purchaseid AND b.supplierid = c.ledgerid and itemcode='" + itemCode + "'union all select  d.billdate,'SALES' As 'Purchase/Sale',c.qty,d.customername As LedgerName   from tblsalesitems c,tblsales d  where c.billno=d.billno  AND d.cancelled=false and itemcode='" + itemCode + "') order by 1";
               sQry = "select d.billdate,'SALES' As 'Purchase/Sale',d.billno,c.qty,d.customername As LedgerName   from tblsalesitems c,tblsales d  where c.billno=d.billno AND (d.billdate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND d.billdate<=#" + eDate.ToString("MM/dd/yyyy") + "#)  AND d.cancelled=false and itemcode='" + itemCode + "' order by d.billdate union all    select * from (select  b.billdate ,'PURCHASE' As 'Purchase/Sale',b.BillNo ,qty,c.ledgername As LedgerName   from tblpurchaseitems a,tblpurchase b,tblledger c where a.purchaseid=b.purchaseid AND b.supplierid = c.ledgerid and (b.billdate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND b.billdate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND  itemcode='" + itemCode + "' ) order by 1,2";
               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);

               DataSet dsINOUT = new DataSet();

               sQry = "select  c.Cdate,'IN' As 'Purchase/Sale','' as billno,d.qty,d.FormulaName As LedgerName from tblCompProduct c,tblexecution d  where c.CompID=d.CompID AND (c.CDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND c.Cdate<=#" + eDate.ToString("MM/dd/yyyy") + "#)  AND d.InOut='IN' and d.itemcode='D01' order by c.cdate union all  select * from ( select  c.Cdate,'IN' As 'Purchase/Sale','' as billno, d.qty,d.FormulaName As LedgerName   from tblCompProduct c,tblexecution d  where c.CompID=d.CompID AND (c.CDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND c.Cdate<=#" + eDate.ToString("MM/dd/yyyy") + "#)  AND d.InOut='OUT' and d.itemcode='D01' ) order by 1,2";

               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               oleAdp.Fill(dsINOUT);

               if(dsINOUT.Tables[0].Rows.Count > 0)
                dsParentQry.Tables[0].Merge(dsINOUT.Tables[0]);

               return dsParentQry;

           }

#endregion

        #region "Sales Performance Report"
        public DataSet GenerateSalesBillwise(string sDataSource, DateTime sDate, DateTime eDate,int iLedgerID)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry; string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               //if(sType=="No")
               sQry = "SELECT tblSales.BillNo, tblSales.BillDate,tblSales.CustomerName,tblSales.Paymode, tblDayBook.Amount FROM (tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) WHERE tblSales.Cancelled=False AND   tblSales.CustomerID=" + iLedgerID + " AND tblSales.PurchaseReturn='No' AND  (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
               //else
               //    sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, tblPurchase.BillDate, tblPurchaseItems.Qty,tblPurchase.SalesReturnReason FROM tblProductMaster INNER JOIN (tblPurchase INNER JOIN tblPurchaseItems ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID) ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE tblLedger.LedgerID=" + iLedgerID + " AND tblPurchase.SalesReturn='Yes' AND tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
               //sQry = "SELECT tblSales.BillDate, tblSales.BillNo, tblLedger.LedgerName, tblDayBook.Amount FROM ((tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblLedger ON tblSales.CustomerID = tblLedger.LedgerID WHERE (tblSales.paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);
               return dsParentQry;

           }
         
        public DataSet GenerateSalesReturnBillwise(string sDataSource, DateTime sDate, DateTime eDate, int iLedgerID)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry; string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               //if (sType == "No")
               //    sQry = "SELECT tblSales.BillNo, tblSales.BillDate,tblSales.CustomerName,tblSales.Paymode, tblLedger.LedgerName, tblDayBook.Amount FROM (tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN tblLedger ON tblDayBook.DebtorID = tblLedger.LedgerID WHERE  tblLedger.LedgerID=" + iLedgerID + " AND  (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
               //else
                 //  sQry = "SELECT tblPurchase.PurchaseID, tblPurchase.BillDate,tblPurchase.Paymode, tblPurchaseItems.Qty,tblPurchase.SalesReturnReason FROM tblPurchase INNER JOIN tblPurchaseItems ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID WHERE tblPurchase.SupplierID=" + iLedgerID + " AND tblPurchase.SalesReturn='Yes' AND tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
               sQry = "SELECT tblPurchase.PurchaseID, tblPurchase.BillDate,tblPurchase.Paymode FROM tblPurchase  WHERE tblPurchase.SupplierID=" + iLedgerID + " AND tblPurchase.SalesReturn='Yes' AND tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
               //sQry = "SELECT tblSales.BillDate, tblSales.BillNo, tblLedger.LedgerName, tblDayBook.Amount FROM ((tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblLedger ON tblSales.CustomerID = tblLedger.LedgerID WHERE (tblSales.paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);
               return dsParentQry;

           }
          
        public DataSet getProductsPurchase(int purchaseID, string sDataSource)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               sQry = "SELECT tblPurchaseItems.ItemCode,tblProductMaster.Model,tblProductMaster.ProductDesc, tblProductMaster.ProductName, tblPurchaseItems.Qty, tblPurchaseItems.PurchaseRate, tblPurchaseItems.Discount,tblPurchaseItems.VAT  FROM tblProductMaster INNER JOIN tblPurchaseItems ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE tblPurchaseItems.PurchaseID=" + purchaseID ;


               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);
               return dsParentQry;
           }
             
        public DataSet GenerateSalesItemwise(string sDataSource, DateTime sDate, DateTime eDate, int iLedgerID)
        {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry;
               string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               //if(sType=="No")
               //sQry = "SELECT tblSales.BillDate, tblSalesItems.ItemCode,tblProductMaster.ProductName,tblProductMaster.Model,tblProductMaster.ProductDesc,tblSales.CustomerID,SUM(tblSalesItems.qty) As Qty,Sum(tblDayBook.Amount) As Amount FROM tblProductMaster INNER JOIN (tblSalesItems INNER JOIN  (tblSales INNER JOIN tblDayBook ON tblsales.JournalID = tblDaybook.Transno) ON tblSales.Billno = tblSalesItems.Billno) ON tblProductmaster.itemcode=tblSalesItems.Itemcode  Group By tblSalesItems.ItemCode ,tblProductMaster.ProductName,tblProductMaster.Model,tblProductMaster.ProductDesc,tblSales.CustomerID,tblSales.BillDate  HAVING    tblSales.CustomerID=" + iLedgerID + " AND (tblSales.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#)";
               sQry = "SELECT  tblSalesItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc,  SUM(tblSalesItems.qty) AS Qty, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty* (tblSalesItems.VAT/100))) As Amount FROM tblProductMaster INNER JOIN (tblSalesItems INNER JOIN (tblSales INNER JOIN tblDayBook ON tblsales.JournalID=tblDaybook.Transno) ON tblSales.Billno=tblSalesItems.Billno) ON tblProductMaster.itemcode=tblSalesItems.Itemcode Where tblSales.Cancelled=False AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND (tblSales.PurchaseReturn='No' OR tblSales.PurchaseReturn<>'Yes') AND  tblSales.CustomerID=" + iLedgerID + "   GROUP BY tblSalesItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc";   
            //else
               //    sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, tblPurchase.BillDate, tblPurchaseItems.Qty,tblPurchase.SalesReturnReason FROM tblProductMaster INNER JOIN (tblPurchase INNER JOIN tblPurchaseItems ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID) ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE tblLedger.LedgerID=" + iLedgerID + " AND tblPurchase.SalesReturn='Yes' AND tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
               //sQry = "SELECT tblSales.BillDate, tblSales.BillNo, tblLedger.LedgerName, tblDayBook.Amount FROM ((tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblLedger ON tblSales.CustomerID = tblLedger.LedgerID WHERE (tblSales.paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);
               return dsParentQry;

           }
        public DataSet GenerateSalesExecutiveItemwise(string sDataSource, DateTime sDate, DateTime eDate, string iLedgerID)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            //if(sType=="No")
            //sQry = "SELECT tblSales.BillDate, tblSalesItems.ItemCode,tblProductMaster.ProductName,tblProductMaster.Model,tblProductMaster.ProductDesc,tblSales.CustomerID,SUM(tblSalesItems.qty) As Qty,Sum(tblDayBook.Amount) As Amount FROM tblProductMaster INNER JOIN (tblSalesItems INNER JOIN  (tblSales INNER JOIN tblDayBook ON tblsales.JournalID = tblDaybook.Transno) ON tblSales.Billno = tblSalesItems.Billno) ON tblProductmaster.itemcode=tblSalesItems.Itemcode  Group By tblSalesItems.ItemCode ,tblProductMaster.ProductName,tblProductMaster.Model,tblProductMaster.ProductDesc,tblSales.CustomerID,tblSales.BillDate  HAVING    tblSales.CustomerID=" + iLedgerID + " AND (tblSales.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#)";
            // krishnavelu 26 June
            //////sQry = "SELECT  tblSalesItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc,  SUM(tblSalesItems.qty) AS Qty, Sum( (tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate) + ((tblSalesItems.VAT/100) * ((tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate)))+ ((tblSalesItems.CST/100) * ((tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate)))) As Amount,sum(ExecCharge) as ExecCharge  FROM tblProductMaster INNER JOIN (tblSalesItems INNER JOIN (tblSales INNER JOIN tblDayBook ON tblsales.JournalID=tblDaybook.Transno) ON tblSales.Billno=tblSalesItems.Billno) ON tblProductMaster.itemcode=tblSalesItems.Itemcode Where tblSales.Cancelled=False AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND (tblSales.PurchaseReturn='No' OR tblSales.PurchaseReturn<>'Yes') AND  tblSales.CustomerID IN (" + iLedgerID + ")   GROUP BY tblSalesItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc";
            //else
            //    sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, tblPurchase.BillDate, tblPurchaseItems.Qty,tblPurchase.SalesReturnReason FROM tblProductMaster INNER JOIN (tblPurchase INNER JOIN tblPurchaseItems ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID) ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE tblLedger.LedgerID=" + iLedgerID + " AND tblPurchase.SalesReturn='Yes' AND tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
            //sQry = "SELECT tblSales.BillDate, tblSales.BillNo, tblLedger.LedgerName, tblDayBook.Amount FROM ((tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblLedger ON tblSales.CustomerID = tblLedger.LedgerID WHERE (tblSales.paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
            sQry = "select tblSalesItems.itemcode,tblProductMaster.Productname,tblProductMaster.productDesc,sum(tblSalesItems.Qty) as Qty,Sum( (tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate) + ((tblSalesItems.VAT/100) * ((tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate)))+ ((tblSalesItems.CST/100) * ((tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate)))) As Amount , sum(tblSalesItems.execCharge*tblsalesItems.Qty) as execCharge from tblSalesItems,tblProductMaster  where tblSalesItems.Itemcode=tblProductMaster.itemcode and billNo in(select Billno from tblSales where tblSales.Cancelled=False AND tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "# and tblSales.PurchaseReturn = 'No') and tblSalesItems.ExecIncharge=" + iLedgerID + " group by tblSalesItems.itemcode,Productname,ProductDesc";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }
        public DataSet GenerateSalesReturnExecutiveItemwise(string sDataSource, DateTime sDate, DateTime eDate, string iLedgerID)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry; string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            //if(sType=="No")
            //sQry = "SELECT tblPurchase.BillDate, tblPurchaseItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc, tblPurchase.SupplierID, SUM(tblPurchaseItems.qty) AS Qty, Sum(tblDayBook.Amount) AS Amount,tblPurchase.SalesReturn FROM tblProductMaster INNER JOIN (tblPurchaseItems INNER JOIN (tblPurchase INNER JOIN tblDayBook ON tblPurchase.JournalID=tblDaybook.Transno) ON tblPurchase.PurchaseID=tblPurchaseItems.PurchaseID) ON tblProductmaster.itemcode=tblPurchaseItems.Itemcode GROUP BY tblPurchaseItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc, tblPurchase.SupplierID, tblPurchase.BillDate,tblPurchase.SalesReturn HAVING tblPurchase.SupplierID=" + iLedgerID  + " And (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#)  AND tblPurchase.SalesReturn='Yes'";
            sQry = "SELECT  tblPurchaseItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc, tblPurchase.SupplierID, SUM(tblPurchaseItems.qty) AS Qty,  Sum( (tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) - ((tblPurchaseItems.discount/100) * tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) + ((tblPurchaseItems.VAT/100) * ((tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) - ((tblPurchaseItems.discount/100) * tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate)))+ ((tblPurchaseItems.CST/100) * ((tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) - ((tblPurchaseItems.discount/100) * tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate)))) As Amount , tblPurchase.SalesReturn FROM tblProductMaster INNER JOIN (tblPurchaseItems INNER JOIN (tblPurchase INNER JOIN tblDayBook ON tblPurchase.JournalID=tblDaybook.Transno) ON tblPurchase.PurchaseID=tblPurchaseItems.PurchaseID) ON tblProductmaster.itemcode=tblPurchaseItems.Itemcode Where (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND  tblPurchase.SUpplierID IN (" + iLedgerID + ") And  tblPurchase.SalesReturn='Yes' GROUP BY tblPurchaseItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc, tblPurchase.SupplierID, tblPurchase.SalesReturn;";
            //else
            //    sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, tblPurchase.BillDate, tblPurchaseItems.Qty,tblPurchase.SalesReturnReason FROM tblProductMaster INNER JOIN (tblPurchase INNER JOIN tblPurchaseItems ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID) ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE tblLedger.LedgerID=" + iLedgerID + " AND tblPurchase.SalesReturn='Yes' AND tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
            //sQry = "SELECT tblSales.BillDate, tblSales.BillNo, tblLedger.LedgerName, tblDayBook.Amount FROM ((tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblLedger ON tblSales.CustomerID = tblLedger.LedgerID WHERE (tblSales.paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }
        public DataSet GenerateSalesReturnItemwise(string sDataSource, DateTime sDate, DateTime eDate, int iLedgerID)
           {
               OleDbConnection oleConn;
               OleDbCommand oleCmd;
               OleDbDataAdapter oleAdp;
               DataSet dsParentQry; string sQry = string.Empty;
               string sConStr = string.Empty;


               /* Start Ms Access Database Connection Information */
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
               sConStr = sDataSource;
               //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
               oleConn = new OleDbConnection(CreateConnectionString(sConStr));
               oleCmd = new OleDbCommand();
               oleCmd.Connection = oleConn;
               /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
               //if(sType=="No")
               //sQry = "SELECT tblPurchase.BillDate, tblPurchaseItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc, tblPurchase.SupplierID, SUM(tblPurchaseItems.qty) AS Qty, Sum(tblDayBook.Amount) AS Amount,tblPurchase.SalesReturn FROM tblProductMaster INNER JOIN (tblPurchaseItems INNER JOIN (tblPurchase INNER JOIN tblDayBook ON tblPurchase.JournalID=tblDaybook.Transno) ON tblPurchase.PurchaseID=tblPurchaseItems.PurchaseID) ON tblProductmaster.itemcode=tblPurchaseItems.Itemcode GROUP BY tblPurchaseItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc, tblPurchase.SupplierID, tblPurchase.BillDate,tblPurchase.SalesReturn HAVING tblPurchase.SupplierID=" + iLedgerID  + " And (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#)  AND tblPurchase.SalesReturn='Yes'";
               sQry = "SELECT  tblPurchaseItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc, tblPurchase.SupplierID, SUM(tblPurchaseItems.qty) AS Qty, Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty* (tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty* (tblPurchaseItems.VAT/100))) AS Amount, tblPurchase.SalesReturn FROM tblProductMaster INNER JOIN (tblPurchaseItems INNER JOIN (tblPurchase INNER JOIN tblDayBook ON tblPurchase.JournalID=tblDaybook.Transno) ON tblPurchase.PurchaseID=tblPurchaseItems.PurchaseID) ON tblProductmaster.itemcode=tblPurchaseItems.Itemcode Where (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND  tblPurchase.SUpplierID=" + iLedgerID + " And  tblPurchase.SalesReturn='Yes' GROUP BY tblPurchaseItems.ItemCode, tblProductMaster.ProductName, tblProductMaster.Model, tblProductMaster.ProductDesc, tblPurchase.SupplierID, tblPurchase.SalesReturn;";   
            //else
               //    sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, tblPurchase.BillDate, tblPurchaseItems.Qty,tblPurchase.SalesReturnReason FROM tblProductMaster INNER JOIN (tblPurchase INNER JOIN tblPurchaseItems ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID) ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE tblLedger.LedgerID=" + iLedgerID + " AND tblPurchase.SalesReturn='Yes' AND tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
               //sQry = "SELECT tblSales.BillDate, tblSales.BillNo, tblLedger.LedgerName, tblDayBook.Amount FROM ((tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblLedger ON tblSales.CustomerID = tblLedger.LedgerID WHERE (tblSales.paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
               oleCmd.CommandText = sQry;
               oleCmd.CommandType = CommandType.Text;
               oleAdp = new OleDbDataAdapter(oleCmd);
               dsParentQry = new DataSet();
               oleAdp.Fill(dsParentQry);
               return dsParentQry;

           }

        public DataSet GenerateSalesDetails(string sDataSource, DateTime sDate, DateTime eDate, int iLedgerID)
         {
             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             OleDbDataAdapter oleAdp;
             DataSet dsParentQry; string sQry = string.Empty;
             string sConStr = string.Empty;


             /* Start Ms Access Database Connection Information */
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
             sConStr = sDataSource;
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
             oleConn = new OleDbConnection(CreateConnectionString(sConStr));
             oleCmd = new OleDbCommand();
             oleCmd.Connection = oleConn;
             /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
             //if(sType=="No")
             sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblSales.Paymode,tblDayBook.Amount FROM tblSales,tblDaybook WHERE tblSales.JournalID = tblDayBook.Transno AND tblSales.Cancelled=False AND tblSales.CustomerID=" + iLedgerID + " AND tblSales.PurchaseReturn='No' AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
             //else
             //    sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, tblPurchase.BillDate, tblPurchaseItems.Qty,tblPurchase.SalesReturnReason FROM tblProductMaster INNER JOIN (tblPurchase INNER JOIN tblPurchaseItems ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID) ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE tblLedger.LedgerID=" + iLedgerID + " AND tblPurchase.SalesReturn='Yes' AND tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
             //sQry = "SELECT tblSales.BillDate, tblSales.BillNo, tblLedger.LedgerName, tblDayBook.Amount FROM ((tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblLedger ON tblSales.CustomerID = tblLedger.LedgerID WHERE (tblSales.paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;
             oleAdp = new OleDbDataAdapter(oleCmd);
             dsParentQry = new DataSet();
             oleAdp.Fill(dsParentQry);
             return dsParentQry;

         }

        public DataSet GenerateSalesReturnDetails(string sDataSource, DateTime sDate, DateTime eDate, int iLedgerID)
         {
             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             OleDbDataAdapter oleAdp;
             DataSet dsParentQry; string sQry = string.Empty;
             string sConStr = string.Empty;


             /* Start Ms Access Database Connection Information */
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
             sConStr = sDataSource;
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
             oleConn = new OleDbConnection(CreateConnectionString(sConStr));
             oleCmd = new OleDbCommand();
             oleCmd.Connection = oleConn;
             /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
             //if(sType=="No")
            //sQry = "SELECT tblPurchase.purchaseID,tblPurchase.BillDate,tblPurchase.Paymode,tblDayBook.Amount,tblPurchase.SalesReturnReason FROM tblPurchase,tblDaybook WHERE SalesReturn='Yes' AND tblPurchase.JournalID = tblDayBook.Transno AND   tblPurchase.SupplierID="+ iLedgerID +" AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
             sQry = "SELECT tblPurchase.purchaseID,tblPurchase.BillDate,tblPurchase.Paymode,tblDayBook.Amount,tblPurchase.SalesReturnReason FROM tblPurchase,tblDaybook WHERE SalesReturn='Yes' AND tblPurchase.JournalID = tblDayBook.Transno AND   tblPurchase.SupplierID=" + iLedgerID + " AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
             //else
             //    sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, tblPurchase.BillDate, tblPurchaseItems.Qty,tblPurchase.SalesReturnReason FROM tblProductMaster INNER JOIN (tblPurchase INNER JOIN tblPurchaseItems ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID) ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE tblLedger.LedgerID=" + iLedgerID + " AND tblPurchase.SalesReturn='Yes' AND tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
             //sQry = "SELECT tblSales.BillDate, tblSales.BillNo, tblLedger.LedgerName, tblDayBook.Amount FROM ((tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblLedger ON tblSales.CustomerID = tblLedger.LedgerID WHERE (tblSales.paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;
             oleAdp = new OleDbDataAdapter(oleCmd);
             dsParentQry = new DataSet();
             oleAdp.Fill(dsParentQry);
             return dsParentQry;

         }

        public DataSet GenerateCreditDebitTran(string sDataSource, DateTime sDate, DateTime eDate, int iLedgerID)
         {
             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             OleDbDataAdapter oleAdp;
             DataSet dsParentQry; string sQry = string.Empty;
             string sConStr = string.Empty;


             /* Start Ms Access Database Connection Information */
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
             sConStr = sDataSource;
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
             oleConn = new OleDbConnection(CreateConnectionString(sConStr));
             oleCmd = new OleDbCommand();
             oleCmd.Connection = oleConn;
             /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
             //if(sType=="No")
             //sQry = "SELECT tblPurchase.purchaseID,tblPurchase.BillDate,tblPurchase.Paymode,tblDayBook.Amount,tblPurchase.SalesReturnReason FROM tblPurchase,tblDaybook WHERE SalesReturn='Yes' AND tblPurchase.JournalID = tblDayBook.Transno AND   tblPurchase.SupplierID=" + iLedgerID + " AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#);";
             //sQry = "SELECT tblDaybook.Transno, tblDayBook.TransDate, Debtor.LedgerName As Debitor, tblDayBook.Amount, Creditor.LedgerName As Creditor FROM tblDayBook, tblLedger AS Debtor, tblLedger AS Creditor WHERE tblDayBook.CreditorID=Creditor.LedgerID And tblDayBook.DebtorID=Debtor.LedgerID And (tblDayBook.DebtorID=" + iLedgerID + " Or tblDayBook.CreditorID=" + iLedgerID + ") AND (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#)";
             sQry = "SELECT tblDaybook.Transno, tblDayBook.TransDate, Debtor.LedgerName As Debitor, tblDayBook.Amount,Creditor.LedgerName As Creditor,tblDayBook.VoucherType FROM tblDayBook, tblLedger AS Debtor,tblLedger As Creditor WHERE  tblDayBook.DebtorID=Debtor.LedgerID And tblDayBook.CreditorID=Creditor.LedgerID And (tblDayBook.DebtorID=" + iLedgerID + " Or tblDayBook.CreditorID=" + iLedgerID + ") AND (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#); UNION  SELECT tblDaybook.Transno, tblDayBook.TransDate, tblSales.CustomerName As Debitor, tblDayBook.Amount,Creditor.LedgerName As Creditor,tblDayBook.VoucherType FROM tblDayBook,tblSales,tblLedger As Creditor WHERE tblSales.JournalID = tblDayBook.Transno And   tblDayBook.CreditorID=Creditor.LedgerID AND (tblDayBook.DebtorID=" + iLedgerID + " Or tblDayBook.CreditorID=" + iLedgerID + " OR tblSales.CustomerID=" + iLedgerID + ") AND (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# And tblDayBook.TransDate<=#" + eDate.ToString("MM/dd/yyyy") + "#)";
             //else
             //    sQry = "SELECT tblProductMaster.ItemCode, tblProductMaster.ProductName, tblPurchase.BillDate, tblPurchaseItems.Qty,tblPurchase.SalesReturnReason FROM tblProductMaster INNER JOIN (tblPurchase INNER JOIN tblPurchaseItems ON tblPurchase.PurchaseID = tblPurchaseItems.purchaseID) ON tblProductMaster.ItemCode = tblPurchaseItems.ItemCode WHERE tblLedger.LedgerID=" + iLedgerID + " AND tblPurchase.SalesReturn='Yes' AND tblPurchase.BillDate >= #" + sDate.ToString("MM/dd/yyyy") + "# And  tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#;";
             //sQry = "SELECT tblSales.BillDate, tblSales.BillNo, tblLedger.LedgerName, tblDayBook.Amount FROM ((tblSales INNER JOIN tblDayBook ON tblSales.JournalID = tblDayBook.TransNo) INNER JOIN (tblProductMaster INNER JOIN tblSalesItems ON tblProductMaster.ItemCode = tblSalesItems.ItemCode) ON tblSales.BillNo = tblSalesItems.BillNo) INNER JOIN tblLedger ON tblSales.CustomerID = tblLedger.LedgerID WHERE (tblSales.paymode=" + paymode + " and (Billdate >= #" + sDate.ToString("MM/dd/yyyy") + "# And BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#));";
             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;
             oleAdp = new OleDbDataAdapter(oleCmd);
             dsParentQry = new DataSet();
             oleAdp.Fill(dsParentQry);
             return dsParentQry;

         }

        public DataSet GenerateExecutiveSales(string sDataSource, int executiveID, DateTime sDate, DateTime eDate)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry; string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            sQry = "select  tblSales.CustomerName as CustomerName ,Sum( (tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate) + ((tblSalesItems.VAT/100) * ((tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate)))+ ((tblSalesItems.CST/100) * ((tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate)))) As Amount ,sum(tblSalesItems.ExecCharge * tblSalesItems.Qty) as ExecCharge from tblSales ,tblSalesItems  where tblSales.Cancelled=False AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblSales.billno =tblSalesItems.billno and tblSales.PurchaseReturn = 'No' and tblSalesItems.ExecIncharge=" + executiveID + " group by tblSales.customername";
            //if (executiveID != 0)
            //    sQry = "SELECT Sum( (tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate) + ((tblSalesItems.VAT/100) * ((tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate)))+ ((tblSalesItems.CST/100) * ((tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate)))) As Amount ,  Sum((tblSalesitems.Qty * tblSalesitems.Rate) - ((tblSalesitems.discount/100) * tblSalesitems.Qty * tblSalesitems.Rate)) AS Total,tblSales.CustomerName,sum(ExecCharge) as ExecCharge FROM tblSalesitems,tblSales,tblLedger WHERE tblSales.Cancelled=False AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblSales.Billno = tblSalesItems.Billno AND tblSales.CustomerID= tblLedger.LedgerID AND tblLedger.ExecutiveIncharge=" + executiveID + " AND tblSales.PurchaseReturn = 'No' GROUP BY  tblSales.CustomerName;";
            //else
            //    sQry = "SELECT  Sum( (tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate) + ((tblSalesItems.VAT/100) * ((tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate)))+ ((tblSalesItems.CST/100) * ((tblSalesItems.Qty * tblSalesItems.Rate) - ((tblSalesItems.discount/100) * tblSalesItems.Qty * tblSalesItems.Rate)))) AS Amount,tblSales.CustomerName,sum(ExecCharge) as ExecCharge FROM tblSalesitems,tblSales,tblLedger WHERE tblSales.Cancelled=False AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblSales.Billno = tblSalesItems.Billno AND tblSales.CustomerID= tblLedger.LedgerID AND  tblSales.PurchaseReturn = 'No' GROUP BY  tblSales.CustomerName;";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }
        public DataSet GenerateExecutiveSalesReturn(string sDataSource, int executiveID, DateTime sDate, DateTime eDate)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry; string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (executiveID != 0)
                sQry = "SELECT Sum( (tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) - ((tblPurchaseItems.discount/100) * tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) + ((tblPurchaseItems.VAT/100) * ((tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) - ((tblPurchaseItems.discount/100) * tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate)))+ ((tblPurchaseItems.CST/100) * ((tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) - ((tblPurchaseItems.discount/100) * tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate)))) As Amount ,  Sum((tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) - ((tblPurchaseItems.discount/100) * tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate)) AS Total, tblLedger.LedgerName FROM tblPurchase,tblPurchaseItems,tblLedger WHERE (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND   tblPurchase.SalesReturn='Yes' AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND tblPurchase.SupplierID=tblLedger.LedgerID AND tblLedger.ExecutiveIncharge=" + executiveID + " GROUP BY tblLedger.LedgerName;";
            else
                sQry = "SELECT Sum( (tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) - ((tblPurchaseItems.discount/100) * tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) + ((tblPurchaseItems.VAT/100) * ((tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) - ((tblPurchaseItems.discount/100) * tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate)))+ ((tblPurchaseItems.CST/100) * ((tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate) - ((tblPurchaseItems.discount/100) * tblPurchaseItems.Qty * tblPurchaseItems.PurchaseRate)))) As Amount, tblLedger.LedgerName FROM tblPurchase,tblPurchaseItems,tblLedger WHERE (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#) AND   tblPurchase.SalesReturn='Yes' AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND tblPurchase.SupplierID=tblLedger.LedgerID   GROUP BY tblLedger.LedgerName;";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;
        }
        #endregion

        #region "outstanding Dealer/Executive Report"
        //SELECT (tblLedger.OpenBalanceDR) AS OB, tblDayBook.Transno,tblDayBook.TransDate, Debtor.LedgerName As Debtor, Creditor.ledgerName As Creditor ,tblDayBook.Amount,tblDayBook.VoucherType FROM tblDaybook, tblLedger,tblLedger As Creditor,tblLedger As Debtor WHERE tblDayBook.DebtorID = Debtor.LedgerID AND tblDayBook.CreditorID = Creditor.LedgerID AND (Debtor.LedgerID=734 ) AND tblLedger.LedgerID = 734
        public DataSet GetDebitCreditLedger(string sDataSource, int iLedgerID,string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry; string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if(sType == "Debit")
                sQry = "SELECT (tblLedger.OpenBalanceDR-tblLedger.OpenbalanceCR) AS OB, tblDayBook.Transno,tblDayBook.TransDate, Debtor.LedgerName As Debtor, Creditor.ledgerName As Creditor ,tblDayBook.Amount,tblDayBook.VoucherType FROM tblDaybook, tblLedger,tblLedger As Creditor,tblLedger As Debtor WHERE tblDayBook.DebtorID = Debtor.LedgerID AND tblDayBook.CreditorID = Creditor.LedgerID AND (Debtor.LedgerID="+ iLedgerID +" ) AND tblLedger.LedgerID = " + iLedgerID;
            else
                sQry = "SELECT (tblLedger.OpenBalanceCR-tblLedger.OpenBalanceDR) AS OB, tblDayBook.Transno,tblDayBook.TransDate, Debtor.LedgerName As Debtor, Creditor.ledgerName As Creditor ,tblDayBook.Amount,tblDayBook.VoucherType FROM tblDaybook, tblLedger,tblLedger As Creditor,tblLedger As Debtor WHERE tblDayBook.DebtorID = Debtor.LedgerID AND tblDayBook.CreditorID = Creditor.LedgerID AND (Creditor.LedgerID=" + iLedgerID + " ) AND tblLedger.LedgerID = " + iLedgerID;
           

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }
        public double GetTotalCredit(string sDataSource, int iLedgerID)
        {
            //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry; 
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleConn.Open(); 
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID="+ iLedgerID  ;


            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            
            object retVal = oleCmd.ExecuteScalar();
            

            double amt =0.0d;
            if (retVal != null)
            {
                if(retVal.ToString() !="") 
                    amt = (double)retVal;
            }
            sQry = "SELECT OpenBalanceCR AS OB FROM tblLedger WHERE LedgerID=" + iLedgerID;
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            retVal = oleCmd.ExecuteScalar();
            double oB = 0;
            if (retVal != null)
            {
                oB = (double)retVal;
            }
            
                amt = amt + oB;

            oleConn.Close(); 
            return amt;
 
        }
        public int GetDays(string sDataSource, string sDate,int transno)
        {
            //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry; string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleConn.Open();
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT DateDiff('d',#" + DateTime.Parse(sDate).ToString("MM/dd/yyyy") + "#,Date()) As Days FROM tblDayBook WHERE Transno = " +transno;


            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            //oleAdp = new OleDbDataAdapter(oleCmd);
            //dsParentQry = new DataSet();
            //oleAdp.Fill(dsParentQry);
            //return dsParentQry;
            object retVal = oleCmd.ExecuteScalar();
            int days = 0;
            if (retVal != null)
            {
                days = (Int32)oleCmd.ExecuteScalar();
            }
            oleConn.Close();
            return days;

        }
        //SELECT DateDiff('d',tblDayBook.transdate,#12/25/2009#) As Days FROM tblDayBook WHERE Transno = 7373
        //SELECT LedgerID,LedgerName FROM tblLedger WHERE ExecutiveIncharge=1
        public DataSet GetLedgerExecutive(string sDataSource, int Executive)
        {
            //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleConn.Open();
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if(Executive !=0)
                sQry = "SELECT LedgerID,LedgerName FROM tblLedger WHERE ExecutiveIncharge=" + Executive + " ORDER BY LedgerName ";
            else
                sQry = "SELECT LedgerID,LedgerName FROM tblLedger";


            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            //oleAdp = new OleDbDataAdapter(oleCmd);
            //dsParentQry = new DataSet();
            //oleAdp.Fill(dsParentQry);
            //return dsParentQry;
            
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }

        public DataSet generatePurchaseVATReport(string sDataSource, DateTime sDate, DateTime eDate, string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection( CreateConnectionString( sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (sType == "Yes")
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS VatRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                sQry = "SELECT tblPurchase.BillNo,tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND (tblPurchase.Salesreturn is null OR tblPurchase.SalesReturn='No') GROUP By tblPurchase.BillNo,tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber order by tblLedger.LedgerName,tblPurchase.BillDate";
            else
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS VatRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales Return') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales Return') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                sQry = "SELECT tblPurchase.BillNo,tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND (tblPurchase.SalesReturn='Yes') GROUP By tblPurchase.BillNo,tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber order by tblLedger.LedgerName,tblPurchase.BillDate";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);

            return dsParentQry;

        }

        public DataSet GetAllVATPayment(string connection, DateTime sDate)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet ds = new DataSet();
            StringBuilder dbQry = new StringBuilder();
            oleConn = new OleDbConnection(CreateConnectionString(connection));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            try
            {
                dbQry.Append("SELECT  tblDayBook.TransNo, tblDayBook.TransDate, Creditor.LedgerName,tblDayBook.CreditorID,tblDayBook.DebtorID, Debitor.LedgerName AS Debi, tblDayBook.Amount, tblDayBook.Narration, ");
                dbQry.Append("tblDayBook.VoucherType, tblDayBook.RefNo, tblDayBook.ChequeNo, Payment.Paymode,Payment.Billno  FROM  (((tblDayBook INNER JOIN ");
                dbQry.Append("tblLedger Debitor ON tblDayBook.DebtorID = Debitor.LedgerID) INNER JOIN  tblLedger Creditor ON tblDayBook.CreditorID = Creditor.LedgerID) LEFT JOIN ");
                dbQry.Append(" tblPayMent Payment ON tblDayBook.TransNo = Payment.JournalID)");
                dbQry.AppendFormat("Where Debitor.LedgerName = 'VAT A/c' AND (tblDayBook.TransDate > #" + sDate.ToString("MM/dd/yyyy") + "# ) Order By tblDayBook.RefNo, tblDayBook.TransDate");

                oleCmd.CommandText = dbQry.ToString();
                oleCmd.CommandType = CommandType.Text;
                oleAdp = new OleDbDataAdapter(oleCmd);
                oleAdp.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet generatePurchaseVATReconReport(string sDataSource, DateTime sDate, string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (sType == "Yes")
                sQry = "SELECT tblPurchase.BillNo,tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE (tblPurchase.BillDate > #" + sDate.ToString("MM/dd/yyyy") + "#) AND  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND (tblPurchase.Salesreturn is null OR tblPurchase.SalesReturn='No') GROUP By tblPurchase.BillNo,tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber Order By tblPurchase.BillNo,tblPurchase.BillDate ";
            else
                sQry = "SELECT tblPurchase.BillNo,tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE (tblPurchase.BillDate > #" + sDate.ToString("MM/dd/yyyy") + "#) AND  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND (tblPurchase.SalesReturn='Yes') GROUP By tblPurchase.BillNo,tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber Order By tblPurchase.BillNo,tblPurchase.BillDate";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);

            return dsParentQry;

        }

        public System.Collections.ArrayList getMissingCommodityCodes(string sDataSource, DateTime sDate, DateTime eDate, string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (sType == "Yes")
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS VatRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                sQry = "SELECT tblPurchase.BillNo,tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblProductMaster.CommodityCode,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND (tblPurchase.Salesreturn is null OR tblPurchase.SalesReturn='No') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchase.BillNo,tblProductMaster.CommodityCode";
            else
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS VatRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales Return') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales Return') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                sQry = "SELECT tblPurchase.BillNo,tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblProductMaster.CommodityCode,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND (tblPurchase.SalesReturn='Yes') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchase.BillNo,tblProductMaster.CommodityCode";

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);

            System.Collections.ArrayList lsItems = new System.Collections.ArrayList();

            if (dsParentQry != null)
            {
                foreach (DataRow dR in dsParentQry.Tables[0].Rows)
                {
                    if (dR["CommodityCode"].ToString() == "")
                    {
                        if(!lsItems.Contains(dR["ProductName"].ToString()))
                        {
                            lsItems.Add(dR["ProductName"].ToString());
                        }
                    }
                }
            }

            if (sType == "Yes")
            {
                sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblLedger.TINnumber,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblProductMaster.CommodityCode,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblProductMaster,tblLedger WHERE   tblSales.Billno = tblSalesItems.Billno AND tblSales.CustomerID = tblLedger.LedgerID AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSAles.purchasereturn is null OR tblSales.PurchaseReturn='No') and tblSales.cancelled<>true GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName,tblLedger.TINnumber,tblProductMaster.CommodityCode";
            }
            else
            {
                sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblLedger.TINnumber,,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblProductMaster.CommodityCode,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblProductMaster,tblLedger WHERE  tblSales.Billno = tblSalesItems.Billno AND tblSales.CustomerID = tblLedger.LedgerID AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSales.PurchaseReturn='Yes') and tblSales.cancelled<>true GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName,tblLedger.TINnumber,tblProductMaster.CommodityCode";
            }

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);

            if (dsParentQry != null)
            {
                foreach (DataRow dR in dsParentQry.Tables[0].Rows)
                {
                    if (dR["CommodityCode"].ToString() == "")
                    {
                        if (!lsItems.Contains(dR["ProductName"].ToString()))
                        {
                            lsItems.Add(dR["ProductName"].ToString());
                        }
                    }
                }
            }

            return lsItems;
        }

        public DataTable generatePurchaseVATTable(string sDataSource, DateTime sDate, DateTime eDate, string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (sType == "Yes")
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS VatRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                sQry = "SELECT tblPurchase.BillNo,tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblProductMaster.CommodityCode,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND (tblPurchase.Salesreturn is null OR tblPurchase.SalesReturn='No') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchase.BillNo,tblProductMaster.CommodityCode";
            else
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS VatRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales Return') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales Return') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                sQry = "SELECT tblPurchase.BillNo,tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblProductMaster.CommodityCode,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.VAT,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.VAT/100) ) AS VATRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND (tblPurchase.SalesReturn='Yes') GROUP By tblPurchaseItems.VAT,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchase.BillNo,tblProductMaster.CommodityCode";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);

            DataTable dt = new DataTable("ANNEX I");
            dt.Columns.Add("serial_no", typeof(Int32));
            dt.Columns.Add("Name_of_seller", typeof(string));
            dt.Columns.Add("Seller_TIN", typeof(string));
            dt.Columns.Add("commodity_code", typeof(string));
            dt.Columns.Add("Invoice_No", typeof(string));
            dt.Columns.Add("Invoice_Date", typeof(DateTime));
            dt.Columns.Add("Purchase_Value", typeof(double));
            dt.Columns.Add("Tax_rate", typeof(double));
            dt.Columns.Add("VAT_CST_paid", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            //dsParentQry.Tables.Add(dt);

            DataRow dr = null;
            int i = 0;
            if (dsParentQry != null)
            {
                foreach (DataRow dR in dsParentQry.Tables[0].Rows)
                {
                    i = i + 1;
                    dr = dt.NewRow();
                    dr["serial_no"] = i.ToString();
                    dr["Name_of_seller"] = dR["LedgerName"];
                    dr["Seller_TIN"] = dR["TinNumber"];
                    dr["commodity_code"] = dR["CommodityCode"];
                    dr["Invoice_No"] = dR["BillNo"];
                    dr["Invoice_Date"] = dR["BillDate"];
                    dr["Purchase_Value"] = dR["Rate"];
                    dr["Tax_rate"] = dR["VAT"];
                    dr["VAT_CST_paid"] = "";
                    dr["Category"] = "";
                    dt.Rows.Add(dr);
                }
            }

            return dt;

        }

        public DataSet avlVAT(string sType,string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (sType == "sales")
                sQry = "SELECT Distinct(VAT) from tblPurchaseitems;";
            else
                sQry = "SELECT Distinct(VAT) from tblSalesitems;";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }

        public DataSet generateSalesVATReport(string sDataSource, DateTime sDate, DateTime eDate, string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection( CreateConnectionString( sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (sType == "Yes")
            {
                //sQry = "SELECT tblSales.Billno,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) AS  Rate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales') GROUP By tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName,tblSalesItems.VAT";
                //sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+' ' +  tblProductMaster.productname+' '+tblProductmaster.ProductDesc+' '+tblProductMaster.Model AS ProductName, tblLedger.LedgerName,tblLedger.TinNumber, tblSalesItems.VAT, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) AS VatRate FROM tblSales, tblSalesItems, tblLedger, tblProductMaster WHERE tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales') GROUP BY tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno, tblProductMaster.ItemCode, tblProductMaster.productname, tblProductmaster.ProductDesc, tblProductMaster.Model, tblLedger.LedgerName, tblLedger.TinNumber";
                //sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales') GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                //sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSAles.purchasereturn is null OR tblSales.PurchaseReturn='No') GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblProductMaster WHERE   tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSAles.purchasereturn is null OR tblSales.PurchaseReturn='No') and tblSales.cancelled<>true GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName order by tblSales.CustomerName,tblSales.BillDate";
            }
            else
            {
                //sQry = "SELECT tblSales.Billno,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) AS  Rate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase Return') GROUP By tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName,tblSalesItems.VAT";
                //sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+' ' +  tblProductMaster.productname+' '+tblProductmaster.ProductDesc+' '+tblProductMaster.Model AS ProductName, tblLedger.LedgerName,tblLedger.TinNumber, tblSalesItems.VAT, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) AS VatRate FROM tblSales, tblSalesItems, tblLedger, tblProductMaster WHERE tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase Return') GROUP BY tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno, tblProductMaster.ItemCode, tblProductMaster.productname, tblProductmaster.ProductDesc, tblProductMaster.Model, tblLedger.LedgerName, tblLedger.TinNumber";
                //sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSales.PurchaseReturn='Yes') GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblProductMaster WHERE  tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSales.PurchaseReturn='Yes') and tblSales.cancelled<>true GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName order by tblSales.CustomerName,tblSales.BillDate";

            }

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            DataTable dt = new DataTable("ANNEX II");
            dsParentQry.Tables.Add(dt);
            oleAdp.Fill(dsParentQry.Tables["ANNEX II"]);
            return dsParentQry;
        }

        public DataSet generateSalesVATReconReport(string sDataSource,DateTime sDate , string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (sType == "Yes")
            {
                sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblProductMaster WHERE   tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate > #" + sDate.ToString("MM/dd/yyyy") + "# ) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSAles.purchasereturn is null OR tblSales.PurchaseReturn='No') and tblSales.cancelled<>true GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName Order By tblSales.Billno,tblSales.BillDate ";
            }
            else
            {
                sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblProductMaster WHERE  tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate > #" + sDate.ToString("MM/dd/yyyy") + "# ) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSales.PurchaseReturn='Yes') and tblSales.cancelled<>true GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName Order By tblSales.Billno,tblSales.BillDate";

            }

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            DataTable dt = new DataTable("ANNEX II");
            dsParentQry.Tables.Add(dt);
            oleAdp.Fill(dsParentQry.Tables["ANNEX II"]);
            return dsParentQry;
        }

        public DataTable generateSalesVATTable(string sDataSource, DateTime sDate, DateTime eDate, string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (sType == "Yes")
            {
                //sQry = "SELECT tblSales.Billno,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) AS  Rate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales') GROUP By tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName,tblSalesItems.VAT";
                //sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+' ' +  tblProductMaster.productname+' '+tblProductmaster.ProductDesc+' '+tblProductMaster.Model AS ProductName, tblLedger.LedgerName,tblLedger.TinNumber, tblSalesItems.VAT, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) AS VatRate FROM tblSales, tblSalesItems, tblLedger, tblProductMaster WHERE tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales') GROUP BY tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno, tblProductMaster.ItemCode, tblProductMaster.productname, tblProductmaster.ProductDesc, tblProductMaster.Model, tblLedger.LedgerName, tblLedger.TinNumber";
                //sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales') GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                //sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSAles.purchasereturn is null OR tblSales.PurchaseReturn='No') GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblLedger.TINnumber,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblProductMaster.CommodityCode,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblProductMaster,tblLedger WHERE   tblSales.Billno = tblSalesItems.Billno AND tblSales.CustomerID = tblLedger.LedgerID AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSAles.purchasereturn is null OR tblSales.PurchaseReturn='No') and tblSales.cancelled<>true GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName,tblLedger.TINnumber,tblProductMaster.CommodityCode";
            }
            else
            {
                //sQry = "SELECT tblSales.Billno,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) AS  Rate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase Return') GROUP By tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName,tblSalesItems.VAT";
                //sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+' ' +  tblProductMaster.productname+' '+tblProductmaster.ProductDesc+' '+tblProductMaster.Model AS ProductName, tblLedger.LedgerName,tblLedger.TinNumber, tblSalesItems.VAT, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) AS VatRate FROM tblSales, tblSalesItems, tblLedger, tblProductMaster WHERE tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase Return') GROUP BY tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno, tblProductMaster.ItemCode, tblProductMaster.productname, tblProductmaster.ProductDesc, tblProductMaster.Model, tblLedger.LedgerName, tblLedger.TinNumber";
                //sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSales.PurchaseReturn='Yes') GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblLedger.TINnumber,,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblProductMaster.CommodityCode,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.VAT/100) ) AS VatRate FROM tblSales,tblSalesItems,tblProductMaster,tblLedger WHERE  tblSales.Billno = tblSalesItems.Billno AND tblSales.CustomerID = tblLedger.LedgerID AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND (tblSales.PurchaseReturn='Yes') and tblSales.cancelled<>true GROUP By tblSalesItems.VAT,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName,tblLedger.TINnumber,tblProductMaster.CommodityCode";

            }

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);

            DataTable dt = new DataTable("ANNEX II");
            dt.Columns.Add("serial_no", typeof(Int32));
            dt.Columns.Add("Name_of_buyer", typeof(string));
            dt.Columns.Add("Buyer_TIN", typeof(string));
            dt.Columns.Add("commodity_code", typeof(string));
            dt.Columns.Add("Invoice_No", typeof(Int32));
            dt.Columns.Add("Invoice_Date", typeof(DateTime));
            dt.Columns.Add("Sales_Value", typeof(double));
            dt.Columns.Add("VatRate", typeof(double));
            dt.Columns.Add("VAT_CST_paid", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            //dsParentQry.Tables.Add(dt);

            DataRow dr = null;
            int i = 0;
            foreach (DataRow dR in dsParentQry.Tables[0].Rows)
            {
                i = i + 1;
                dr = dt.NewRow();
                dr["serial_no"] = i.ToString();
                dr["Name_of_buyer"] = dR["CustomerName"];
                dr["Buyer_TIN"] = dR["TINnumber"];
                dr["commodity_code"] = dR["CommodityCode"];
                dr["Invoice_No"] = dR["Billno"];
                dr["Invoice_Date"] = dR["BillDate"];
                dr["Sales_Value"] = dR["Rate"];
                dr["VatRate"] = dR["VAT"];
                dr["VAT_CST_paid"] = "";
                dr["Category"] = "";
                dt.Rows.Add(dr);
            }
            
            return dt;
        }

        #endregion

        #region Stock Verification Report.
        public DataSet getProductStockVerify(string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            sConStr = sDataSource; 

            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            sQry = "SELECT ItemCode + ' - ' + ProductName + ' - ' + ProductDesc + ' - ' + Model As Product,ItemCode,stock FROM tblProductMaster";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;
        }
        
        #endregion

        #region Year End Report
        public DataSet getLedgerIndex(string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            
            /* Start Ms Access Database Connection Information */
            sConStr = sDataSource;
           
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            
            sQry = "SELECT  LedgerName FROM tblLedger,tblAccHeading,tblGroups Where tblLedger.GroupID=tblGroups.GroupID AND tblGroups.HeadingID = tblAccHeading.HeadingID Order By tblGroups.GroupName,LedgerName";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;
        }
        public DataSet getHeading(string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;

            /* Start Ms Access Database Connection Information */
            sConStr = sDataSource;

            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            //sQry = "SELECT  Distinct(Heading) FROM tblLedger,tblAccHeading,tblGroups Where tblLedger.GroupID=tblGroups.GroupID AND tblGroups.HeadingID = tblAccHeading.HeadingID  ORDER by Heading";
            sQry = "SELECT tblAccHeading.HeadingID, Heading FROM tblAccHeading Where tblAccHeading.HeadingID IN (Select HeadingID from tblGroups) Order By Heading";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;
        }

        public DataSet getGroups(int HeadID,string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;

            /* Start Ms Access Database Connection Information */
            sConStr = sDataSource;

            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            //sQry = "SELECT  Distinct(GroupName) FROM tblLedger,tblAccHeading,tblGroups Where tblLedger.GroupID=tblGroups.GroupID AND tblGroups.HeadingID = tblAccHeading.HeadingID AND tblAccHeading.HeadingID="+ HeadID +"  ORDER by GroupName";
            sQry = "SELECT tblGroups.GroupID, GroupName FROM tblGroups Where tblGroups.GroupID IN (Select GroupID from tblLedger) And headingID="+ HeadID  + " Order By GroupName";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;
        }
        public DataSet getLedgers(int groupID, string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;

            /* Start Ms Access Database Connection Information */
            sConStr = sDataSource;

            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT  LedgerID, LedgerName FROM tblLedger,tblGroups Where tblLedger.GroupID=tblGroups.GroupID AND tblGroups.GroupID=" + groupID  + "  ORDER by LedgerName,LedgerID";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;
        }



        public double getLedgerOB(int ledgerID,string sDataSource)
        {
            string sConStr = string.Empty;
            string sQry = string.Empty;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleConn.Open();
            oleCmd.Connection = oleConn;
            /* End Ms Access Database Connection Information */

            /* Start DB Query Processing - Getting the Details of the Ledger int the Daybook */
            
                sQry = "SELECT (OpenBalanceDr-OpenBalanceCr) As OpeningBalance  FROM tblLedger Where  ledgerID=" + ledgerID;
            
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            //oleAdp = new OleDbDataAdapter(oleCmd);
            //dsParentQry = new DataSet();
            //oleAdp.Fill(dsParentQry);
            object amtObj;

            amtObj = oleCmd.ExecuteScalar();
            double amt = 0.0;
            if (amtObj != null && amtObj != DBNull.Value)
                amt = (double)amtObj;
            oleConn.Close();
            return amt;
        }
        public string getLedgerName(int iLedgerID, string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            string sLedger = string.Empty;

            /* Start Ms Access Database Connection Information */
            sConStr = sDataSource;

            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT LedgerName FROM tblLedger WHERE LedgerID=" + iLedgerID ;
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            if (dsParentQry != null)
            {
                if(dsParentQry.Tables[0].Rows[0]["LedgerName"]!=null)
                    sLedger = dsParentQry.Tables[0].Rows[0]["LedgerName"].ToString();

            }
            return sLedger;

        }
        public double GetTotalDebit(string sDataSource, int iLedgerID)
        {
            //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleConn.Open();
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.DebtorID=" + iLedgerID ;


            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;

            object retVal = oleCmd.ExecuteScalar();


            double amt = 0.0d;
            if (retVal != null)
            {
                if (retVal.ToString() != "") 
                amt = (double)retVal;
            }
            sQry = "SELECT OpenBalanceDR AS OB FROM tblLedger WHERE LedgerID=" + iLedgerID;
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            retVal = oleCmd.ExecuteScalar();
            double oB = 0;
            if (retVal != null)
            {
                oB = (double)retVal;
            }

            amt = amt + oB;


            oleConn.Close();
            return amt;

        }
        public DataSet getLedgerTransaction(int groupID, string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            double db = 0;
            double cr = 0;
            double tot=0;

            /* Start Ms Access Database Connection Information */
            sConStr = sDataSource;

            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if(groupID !=0)
                sQry = "SELECT  LedgerID,folionumber, LedgerName FROM tblLedger,tblGroups Where tblLedger.GroupID=tblGroups.GroupID AND tblGroups.GroupID=" + groupID + "  ORDER by LedgerName,LedgerID";
            else
                sQry = "SELECT  LedgerID,folionumber, LedgerName FROM tblLedger,tblGroups Where tblLedger.GroupID=tblGroups.GroupID   ORDER by LedgerName,LedgerID";
            //sQry = "SELECT TransDate,DebtorID,CreditorID,Amount,Narration FROM tblDayBook WHERE (DebtorID=" + iLedgerID + "OR CreditorID=" + iLedgerID + ") ";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);

            DataSet dsNew = new DataSet();
            DataTable dtNew = new DataTable();
            
            DataColumn dcNew = new DataColumn();
            DataRow drNew;
            dcNew = new DataColumn("LedgerName");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("LedgerID");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("Folionumber");
            dtNew.Columns.Add(dcNew);
           
            dcNew = new DataColumn("Debit");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("Credit");
            dtNew.Columns.Add(dcNew);


            dsNew.Tables.Add(dtNew);
           
            if (dsParentQry != null)
            {
                if (dsParentQry.Tables[0] != null)
                {
                    if (dsParentQry.Tables[0].Rows.Count == 0)
                    {
                        drNew = dtNew.NewRow();
                        drNew["LedgerID"] = "";
                        drNew["LedgerName"] = "";
                        drNew["Debit"] = "";
                        drNew["Credit"] = "";
                        drNew["Folionumber"] = "";
                        
                        dsNew.Tables[0].Rows.Add(drNew);
                        
                    }
                    else
                    {
                        foreach (DataRow dr in dsParentQry.Tables[0].Rows)
                        {
                            drNew = dtNew.NewRow();
                            drNew["LedgerID"] = Convert.ToString(dr["LedgerID"]);
                            drNew["LedgerName"] = Convert.ToString(dr["LedgerName"]);
                            drNew["Folionumber"] = Convert.ToString(dr["Folionumber"]);
                            db  = GetTotalDebit(sDataSource,Convert.ToInt32(dr["LedgerID"]));
                            cr= GetTotalCredit(sDataSource,Convert.ToInt32(dr["LedgerID"]));

                            tot = db - cr;
                            if (tot > 0)
                            {
                                drNew["Debit"] = tot.ToString("f2");
                                drNew["Credit"] = "0";
                            }
                            else
                            {
                                drNew["Debit"] = "0";
                                drNew["Credit"] = Math.Abs(tot).ToString("f2");
                            }
                            
                            dsNew.Tables[0].Rows.Add(drNew);

                        }
                       
                    }
                }
            }

            return dsNew;
        }

        public DataSet getLedgerTransaction(int groupID, string sDataSource, DateTime sDate, DateTime eDate)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            double db = 0;
            double cr = 0;
            double tot = 0;

            /* Start Ms Access Database Connection Information */
            sConStr = sDataSource;

            oleConn = new OleDbConnection( CreateConnectionString( sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (groupID != 0)
                sQry = "SELECT  LedgerID,folionumber, LedgerName FROM tblLedger,tblGroups Where tblLedger.GroupID=tblGroups.GroupID AND tblGroups.GroupID=" + groupID + "  ORDER by LedgerName,LedgerID";
            else
                sQry = "SELECT  LedgerID,folionumber, LedgerName FROM tblLedger,tblGroups Where tblLedger.GroupID=tblGroups.GroupID   ORDER by LedgerName,LedgerID";
            //sQry = "SELECT TransDate,DebtorID,CreditorID,Amount,Narration FROM tblDayBook WHERE (DebtorID=" + iLedgerID + "OR CreditorID=" + iLedgerID + ") ";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);

            DataSet dsNew = new DataSet();
            DataTable dtNew = new DataTable();

            DataColumn dcNew = new DataColumn();
            DataRow drNew;
            dcNew = new DataColumn("LedgerName");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("LedgerID");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("Folionumber");
            dtNew.Columns.Add(dcNew);

            dcNew = new DataColumn("Debit");
            dtNew.Columns.Add(dcNew);
            dcNew = new DataColumn("Credit");
            dtNew.Columns.Add(dcNew);


            dsNew.Tables.Add(dtNew);

            if (dsParentQry != null)
            {
                if (dsParentQry.Tables[0] != null)
                {
                    if (dsParentQry.Tables[0].Rows.Count == 0)
                    {
                        drNew = dtNew.NewRow();
                        drNew["LedgerID"] = "";
                        drNew["LedgerName"] = "";
                        drNew["Debit"] = "";
                        drNew["Credit"] = "";
                        drNew["Folionumber"] = "";

                        dsNew.Tables[0].Rows.Add(drNew);

                    }
                    else
                    {
                        foreach (DataRow dr in dsParentQry.Tables[0].Rows)
                        {
                            drNew = dtNew.NewRow();
                            drNew["LedgerID"] = Convert.ToString(dr["LedgerID"]);
                            drNew["LedgerName"] = Convert.ToString(dr["LedgerName"]);
                            drNew["Folionumber"] = Convert.ToString(dr["Folionumber"]);
                            db = GetTotalDebit(sDataSource, Convert.ToInt32(dr["LedgerID"]), sDate, eDate);
                            cr = GetTotalCredit(sDataSource, Convert.ToInt32(dr["LedgerID"]), sDate, eDate);

                            tot = db - cr;
                            if (tot > 0)
                            {
                                drNew["Debit"] = tot.ToString("f2");
                                drNew["Credit"] = "0";
                            }
                            else
                            {
                                drNew["Debit"] = "0";
                                drNew["Credit"] = Math.Abs(tot).ToString("f2");
                            }
                            if (db != 0 || cr != 0)
                                dsNew.Tables[0].Rows.Add(drNew);

                        }

                    }
                }
            }

            return dsNew;
        }

        public double GetTotalCredit(string sDataSource, int iLedgerID, DateTime sDate, DateTime eDate)
        {
            //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection( CreateConnectionString( sConStr));
            oleConn.Open();
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT SUM(Amount) FROM tblDayBook Where  (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblDayBook.TransDate <=#" + eDate.ToString("MM/dd/yyyy") + "#)  AND  tblDaybook.CreditorID=" + iLedgerID;


            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;

            object retVal = oleCmd.ExecuteScalar();


            double amt = 0.0d;
            if (retVal != null)
            {
                if (retVal.ToString() != "")
                    amt = (double)retVal;
            }
            //sQry = "SELECT OpenBalanceCR AS OB FROM tblLedger WHERE LedgerID=" + iLedgerID;
            //oleCmd.CommandText = sQry;
            //oleCmd.CommandType = CommandType.Text;
            //retVal = oleCmd.ExecuteScalar();
            //getOpeningBalance(

            double oB = 0;
            oB = getOpeningBalance(0,0,iLedgerID, "credit", sDate, sConStr);

            amt = amt + oB;

            oleConn.Close();
            return amt;

        }

        public double GetTotalDebit(string sDataSource, int iLedgerID, DateTime sDate, DateTime eDate)
        {
            //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection( CreateConnectionString( sConStr));
            oleConn.Open();
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT SUM(Amount) FROM tblDayBook Where  (tblDayBook.TransDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblDayBook.TransDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblDaybook.DebtorID=" + iLedgerID;


            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;

            object retVal = oleCmd.ExecuteScalar();


            double amt = 0.0d;
            if (retVal != null)
            {
                if (retVal.ToString() != "")
                    amt = (double)retVal;
            }
            //sQry = "SELECT OpenBalanceDR AS OB FROM tblLedger WHERE LedgerID=" + iLedgerID;
            //oleCmd.CommandText = sQry;
            //oleCmd.CommandType = CommandType.Text;
            //retVal = oleCmd.ExecuteScalar();
            double oB = 0;
            //if (retVal != null)
            //{
            //    oB = (double)retVal;
            //}
            oB = getOpeningBalance(0,0,iLedgerID, "debit", sDate, sConStr);
            amt = amt + oB;


            oleConn.Close();
            return amt;

        }

        #endregion

        #region "Profit and Loss"
        
         public double plOpeningStock(string sDataSource)
        {
            //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleConn.Open();
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT  Sum(tblStock.OpeningStock * BuyRate) From tblProductMaster,tblStock Where tblStock.itemcode= tblproductmaster.itemcode";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;

            object retVal = oleCmd.ExecuteScalar();


            double amt = 0.0d;
            if (retVal != null)
            {
                amt = (double)retVal;
            }
           

            oleConn.Close();
            return amt;

        }
         public double plGetClosingStockTotal(string sDataSource)
         {
             //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             OleDbDataAdapter oleAdp;
             DataSet dsParentQry; string sQry = string.Empty;
             string sConStr = string.Empty;


             /* Start Ms Access Database Connection Information */
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
             sConStr = sDataSource;
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
             oleConn = new OleDbConnection(CreateConnectionString(sConStr));
             oleConn.Open();
             oleCmd = new OleDbCommand();
             oleCmd.Connection = oleConn;
             sQry = "SELECT  Sum(tblProductMaster.Rate*tblProductMaster.stock-(tblProductMaster.Rate*tblProductMaster.stock* (tblProductMaster.Discount/100))+(tblProductMaster.Rate*tblProductMaster.stock* (tblProductMaster.VAT/100))) As ClosingStock FROM tblProductMaster";
             //sQry = "SELECT Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) As SoldRate FROM tblSalesItems,tblSales WHERE tblSales.billno = tblSalesItems.Billno AND tblSales.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";
             //sQry = "SELECT Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS PurchaseRate FROM tblPurchaseITems,tblPurchase WHERE tblPurchase.purchaseID = tblPurchaseitems.purchaseID AND tblPurchase.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";


             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;
             //oleAdp = new OleDbDataAdapter(oleCmd);
             //dsParentQry = new DataSet();
             //oleAdp.Fill(dsParentQry);
             //return dsParentQry;
             object retVal = oleCmd.ExecuteScalar();
             double amt = 0;
             if (retVal != null && retVal != DBNull.Value)
             {
                 amt = (double)oleCmd.ExecuteScalar();
             }
             oleConn.Close();
             return amt;

         }
         public double plPurchaseTotal(string sDataSource)
         {
             
             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             OleDbDataAdapter oleAdp;
             DataSet dsParentQry; string sQry = string.Empty;
             string sConStr = string.Empty;


             /* Start Ms Access Database Connection Information */
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
             sConStr = sDataSource;
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
             oleConn = new OleDbConnection(CreateConnectionString(sConStr));
             oleConn.Open();
             oleCmd = new OleDbCommand();
             oleCmd.Connection = oleConn;

             sQry = "SELECT Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS PurchaseRate FROM tblPurchaseITems,tblPurchase WHERE (tblPurchase.SalesReturn <> 'Yes' OR tblPurchase.SalesReturn is null) AND  tblPurchase.purchaseID = tblPurchaseitems.purchaseID";

             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;
             
             object retVal = oleCmd.ExecuteScalar();
             double amt = 0;
             if (retVal != null && retVal != DBNull.Value)
             {
                 amt = (double)oleCmd.ExecuteScalar();
             }
             oleConn.Close();
             return amt;

         }
         public double plGetPurchaseReturnTotal(string sDataSource)
         {

             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             OleDbDataAdapter oleAdp;
             DataSet dsParentQry; string sQry = string.Empty;
             string sConStr = string.Empty;


             /* Start Ms Access Database Connection Information */
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
             sConStr = sDataSource;

             oleConn = new OleDbConnection(CreateConnectionString(sConStr));
             oleConn.Open();
             oleCmd = new OleDbCommand();
             oleCmd.Connection = oleConn;
             sQry = "SELECT Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) As SoldRate FROM tblSalesItems,tblSales WHERE (tblSales.PurchaseReturn = 'Yes') AND tblSales.billno = tblSalesItems.Billno AND tblSales.cancelled=false";
             //sQry = "SELECT Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS PurchaseRate FROM tblPurchaseITems,tblPurchase WHERE tblPurchase.purchaseID = tblPurchaseitems.purchaseID AND tblPurchase.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";


             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;
             //oleAdp = new OleDbDataAdapter(oleCmd);
             //dsParentQry = new DataSet();
             //oleAdp.Fill(dsParentQry);
             //return dsParentQry;
             object retVal = oleCmd.ExecuteScalar();
             double amt = 0;
             if (retVal != null && retVal != DBNull.Value)
             {
                 amt = (double)oleCmd.ExecuteScalar();
             }
             oleConn.Close();
             return amt;

         }

         public double plSalesReturnTotal(string sDataSource)
         {

             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             OleDbDataAdapter oleAdp;
             DataSet dsParentQry; string sQry = string.Empty;
             string sConStr = string.Empty;


             /* Start Ms Access Database Connection Information */
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
             sConStr = sDataSource;
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
             oleConn = new OleDbConnection(CreateConnectionString(sConStr));
             oleConn.Open();
             oleCmd = new OleDbCommand();
             oleCmd.Connection = oleConn;

             sQry = "SELECT Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS PurchaseRate FROM tblPurchaseITems,tblPurchase WHERE (tblPurchase.SalesReturn = 'Yes') AND  tblPurchase.purchaseID = tblPurchaseitems.purchaseID";

             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;

             object retVal = oleCmd.ExecuteScalar();
             double amt = 0;
             if (retVal != null && retVal != DBNull.Value)
             {
                 amt = (double)oleCmd.ExecuteScalar();
             }
             oleConn.Close();
             return amt;

         }
         public double plGetSalesTotal(string sDataSource)
         {
             
             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             OleDbDataAdapter oleAdp;
             DataSet dsParentQry; string sQry = string.Empty;
             string sConStr = string.Empty;


             /* Start Ms Access Database Connection Information */
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
             sConStr = sDataSource;
             
             oleConn = new OleDbConnection(CreateConnectionString(sConStr));
             oleConn.Open();
             oleCmd = new OleDbCommand();
             oleCmd.Connection = oleConn;
             sQry = "SELECT Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) As SoldRate FROM tblSalesItems,tblSales WHERE (tblSales.PurchaseReturn <> 'Yes' OR tblSales.PurchaseReturn is null) AND tblSales.billno = tblSalesItems.Billno AND tblSales.cancelled=false";
             //sQry = "SELECT Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS PurchaseRate FROM tblPurchaseITems,tblPurchase WHERE tblPurchase.purchaseID = tblPurchaseitems.purchaseID AND tblPurchase.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";


             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;
             //oleAdp = new OleDbDataAdapter(oleCmd);
             //dsParentQry = new DataSet();
             //oleAdp.Fill(dsParentQry);
             //return dsParentQry;
             object retVal = oleCmd.ExecuteScalar();
             double amt = 0;
             if (retVal != null && retVal != DBNull.Value)
             {
                 amt = (double)oleCmd.ExecuteScalar();
             }
             oleConn.Close();
             return amt;

         }
         //SELECT LedgerName, SUM(Amount) As Expenses FROM tblDayBook,tblLedger WHERE debtorid=ledgerid AND debtorID IN (SELECT LedgerID FROM tblLedger WHERE GroupID in(SELECT  GroupID From tblGroups Where HeadingID =13 ) ) Group By LedgerName
         public DataSet plGetExpenseIncomeSplit(string sDataSource, string expType)
         {
             //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             OleDbDataAdapter oleAdp;
             DataSet dsParentQry; string sQry = string.Empty;
             DataSet dsChildQry;
             string sConStr = string.Empty;
             string oQry = string.Empty;

             /* Start Ms Access Database Connection Information */
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
             sConStr = sDataSource;
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
             oleConn = new OleDbConnection(CreateConnectionString(sConStr));
             oleConn.Open();
             oleCmd = new OleDbCommand();
             oleCmd.Connection = oleConn;
             if (expType == "IDX")
             {
                 sQry = "SELECT folionumber,LedgerName, SUM(Amount) As Expenses FROM tblDayBook,tblLedger WHERE debtorid=ledgerid AND debtorID IN (SELECT LedgerID FROM tblLedger WHERE GroupID in(SELECT  GroupID From tblGroups Where HeadingID =11 ) ) Group By LedgerName,folionumber";
                 //sQry = "SELECT SUM(Amount) As Expenses FROM tblDayBook WHERE debtorID IN (SELECT LedgerID FROM tblLedger WHERE GroupID in(SELECT  GroupID From tblGroups Where HeadingID =11 ) )";
                 oQry = "SELECT LedgerName,folionumber, (OpenBalanceCR-OpenBalanceDR) AS OB FROM tblLedger Where GroupID IN (Select GroupID from tblGroups where headingID in( Select headingID from tblAccHeading where headingID=11))";
             }

             else if (expType == "DX")
             {
                 sQry = "SELECT folionumber, LedgerName, SUM(Amount) As Expenses FROM tblDayBook,tblLedger WHERE debtorid=ledgerid AND debtorID IN (SELECT LedgerID FROM tblLedger WHERE GroupID in(SELECT  GroupID From tblGroups Where HeadingID =13 ) ) Group By LedgerName,folionumber";
                 oQry = "SELECT LedgerName,folionumber,  (OpenBalanceCR-OpenBalanceDR) AS OB FROM tblLedger Where GroupID IN (Select GroupID from tblGroups where headingID in( Select headingID from tblAccHeading where headingID=13))";
             }
             else if (expType == "IDI")
             {
                 sQry = "SELECT LedgerName,folionumber, SUM(Amount) As Expenses FROM tblDayBook,tblLedger WHERE debtorid=ledgerid AND debtorID IN (SELECT LedgerID FROM tblLedger WHERE GroupID in(SELECT  GroupID From tblGroups Where HeadingID =12 ) ) Group By LedgerName,folionumber";
                 oQry = "SELECT LedgerName,folionumber, (OpenBalanceCR-OpenBalanceDR) AS OB FROM tblLedger Where GroupID IN (Select GroupID from tblGroups where headingID in( Select headingID from tblAccHeading where headingID=12))";
             }
             else
             {
                 sQry = "SELECT LedgerName,folionumber, SUM(Amount) As Expenses FROM tblDayBook,tblLedger WHERE debtorid=ledgerid AND debtorID IN (SELECT LedgerID FROM tblLedger WHERE GroupID in(SELECT  GroupID From tblGroups Where HeadingID =14 ) ) Group By LedgerName,folionumber";
                 oQry = "SELECT LedgerName,folionumber, (OpenBalanceCR-OpenBalanceDR) AS OB FROM tblLedger Where GroupID IN (Select GroupID from tblGroups where headingID in( Select headingID from tblAccHeading where headingID=14))";
             }
             //sQry = "SELECT Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) As SoldRate FROM tblSalesItems,tblSales WHERE tblSales.billno = tblSalesItems.Billno AND tblSales.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";
             //sQry = "SELECT Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS PurchaseRate FROM tblPurchaseITems,tblPurchase WHERE tblPurchase.purchaseID = tblPurchaseitems.purchaseID AND tblPurchase.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";


             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;
             oleAdp = new OleDbDataAdapter(oleCmd);
             dsParentQry = new DataSet();
             oleAdp.Fill(dsParentQry);
             //return dsParentQry;
             
             


             oleCmd.CommandText = oQry;
             oleCmd.CommandType = CommandType.Text;
             dsChildQry  = new DataSet();
             oleAdp.Fill(dsChildQry);
             int i =0;
             double ob = 0;
             double amt=0;
             DataRow dR;
             if (dsParentQry.Tables[0].Rows.Count > 0)
             {
                 foreach (DataRow dr in dsParentQry.Tables[0].Rows)
                 {
                     if (dsChildQry.Tables[0].Rows[i]["OB"] != null)
                     {
                         ob = Convert.ToDouble(dsChildQry.Tables[0].Rows[i]["OB"]);
                         amt = Convert.ToDouble(dr["Expenses"]);
                         if (ob > 0)
                         {
                             amt = amt - ob;
                         }
                         else
                         {
                             amt = amt + ob;
                         }
                         dr["Expenses"] = Math.Abs(amt).ToString();
                     }
                     i++;
                 }
             }
             else
             {
                 if (dsChildQry != null)
                 {
                     if (dsChildQry.Tables[0].Rows.Count > 0)
                     {
                         if (dsChildQry.Tables[0].Rows[0]["OB"] != null)
                         {
                             ob = Convert.ToDouble(dsChildQry.Tables[0].Rows[i]["OB"]);

                             if (ob < 0)
                                 ob = Math.Abs(ob);
                             //dr["Expenses"] = Math.Abs(amt).ToString();
                             dR = dsParentQry.Tables[0].NewRow();

                             dR["LedgerName"] = Convert.ToString(dsChildQry.Tables[0].Rows[i]["LedgerName"]);
                             dR["Expenses"] = ob;
                             if (dsChildQry.Tables[0].Rows[i]["folionumber"].ToString() != "")
                                 dR["folionumber"] = Convert.ToString(dsChildQry.Tables[0].Rows[i]["folionumber"]);
                             else
                                 dR["folionumber"] = "0";
                            
                             dsParentQry.Tables[0].Rows.Add(dR);

                         }
                     }
                 }
             }
             oleConn.Close();
             return dsParentQry;

         }

         public double plGetExpenseIncomeTotal(string sDataSource,string expType)
         {
             //SELECT SUM(Amount) FROM tblDayBook Where  tblDaybook.CreditorID=734 Group By  tblDayBook.CreditorID;
             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             OleDbDataAdapter oleAdp;
             DataSet dsParentQry; string sQry = string.Empty;
             string sConStr = string.Empty;
             string oQry = string.Empty;

             /* Start Ms Access Database Connection Information */
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
             sConStr = sDataSource;
             //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
             oleConn = new OleDbConnection(CreateConnectionString(sConStr));
             oleConn.Open();
             oleCmd = new OleDbCommand();
             oleCmd.Connection = oleConn;
             if (expType == "IDX")
             {
                 sQry = "SELECT SUM(Amount) As Expenses FROM tblDayBook WHERE debtorID IN (SELECT LedgerID FROM tblLedger WHERE GroupID in(SELECT  GroupID From tblGroups Where HeadingID =11 ) )";
                 oQry = "SELECT (OpenBalanceCR-OpenBalanceDR) AS OB FROM tblLedger Where GroupID IN (Select GroupID from tblGroups where headingID in( Select headingID from tblAccHeading where headingID=11))";
             }

             else if (expType == "DX")
             {
                 sQry = "SELECT SUM(Amount) As Expenses FROM tblDayBook WHERE debtorID IN (SELECT LedgerID FROM tblLedger WHERE GroupID in(SELECT  GroupID From tblGroups Where HeadingID =13 ) )";
                 oQry = "SELECT (OpenBalanceCR-OpenBalanceDR) AS OB FROM tblLedger Where GroupID IN (Select GroupID from tblGroups where headingID in( Select headingID from tblAccHeading where headingID=13))";
             }
             else if (expType == "IDI")
             {
                 oQry = "SELECT (OpenBalanceCR-OpenBalanceDR) AS OB FROM tblLedger Where GroupID IN (Select GroupID from tblGroups where headingID in( Select headingID from tblAccHeading where headingID=12))";
                 sQry = "SELECT SUM(Amount) As Expenses FROM tblDayBook WHERE debtorID IN (SELECT LedgerID FROM tblLedger WHERE GroupID in(SELECT  GroupID From tblGroups Where HeadingID =12 ) )";
             }
             else
             {
                 oQry = "SELECT (OpenBalanceCR-OpenBalanceDR) AS OB FROM tblLedger Where GroupID IN (Select GroupID from tblGroups where headingID in( Select headingID from tblAccHeading where headingID=14))";
                 sQry = "SELECT SUM(Amount) As Expenses FROM tblDayBook WHERE debtorID IN (SELECT LedgerID FROM tblLedger WHERE GroupID in(SELECT  GroupID From tblGroups Where HeadingID =14 ) )";
             }
             //sQry = "SELECT Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) As SoldRate FROM tblSalesItems,tblSales WHERE tblSales.billno = tblSalesItems.Billno AND tblSales.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";
             //sQry = "SELECT Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.VAT/100))) AS PurchaseRate FROM tblPurchaseITems,tblPurchase WHERE tblPurchase.purchaseID = tblPurchaseitems.purchaseID AND tblPurchase.BillDate >=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate<=#" + eDate.ToString("MM/dd/yyyy") + "#";


             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;
             //oleAdp = new OleDbDataAdapter(oleCmd);
             //dsParentQry = new DataSet();
             //oleAdp.Fill(dsParentQry);
             //return dsParentQry;
             object retVal = oleCmd.ExecuteScalar();
             double amt = 0;
             if (retVal != null && retVal != DBNull.Value)
             {
                 amt = (double)oleCmd.ExecuteScalar();
             }

             
             oleCmd.CommandText = oQry;
             oleCmd.CommandType = CommandType.Text;
             retVal = oleCmd.ExecuteScalar();
             double oB = 0;
             if (retVal != null)
             {
                 oB = (double)retVal;
             }
             if (oB > 0)
             {
                 amt = amt - oB;
                 amt = Math.Abs(amt);
             }
             else
                 amt = amt + Math.Abs(oB);
             
             oleConn.Close();
             return amt;

         }

         //SELECT  Sum(tblProductMaster.Rate*tblProductMaster.stock-(tblProductMaster.Rate*tblProductMaster.stock* (tblProductMaster.Discount/100))+(tblProductMaster.Rate*tblProductMaster.stock* (tblProductMaster.VAT/100))) As ClosingStock FROM tblProductMaster
         
        #endregion

		#region CST Detailed Summary

         public DataSet generatePurchaseCSTReport(string sDataSource, DateTime sDate, DateTime eDate, string sType)
         {
             OleDbConnection oleConn;
             OleDbCommand oleCmd;
             OleDbDataAdapter oleAdp;
             DataSet dsParentQry;
             string sQry = string.Empty;
             string sConStr = string.Empty;
             sConStr = sDataSource;
             oleConn = new OleDbConnection( CreateConnectionString( sConStr));
             oleCmd = new OleDbCommand();
             oleCmd.Connection = oleConn;
             if (sType == "Yes")
                 //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.CST,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100)) ) *(tblPurchaseItems.CST/100))) AS CSTRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase') GROUP By tblPurchaseItems.CST,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                 sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.CST,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.CST/100) ) AS CSTRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase') GROUP By tblPurchaseItems.CST,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
             else
                 //sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.CST,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))+(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.CST/100))) AS CSTRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales Return') GROUP By tblPurchaseItems.CST,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
                 sQry = "SELECT tblPurchase.PurchaseID,tblPurchase.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblPurchaseItems.CST,Sum(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty-(tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty*(tblPurchaseItems.Discount/100))) AS Rate,Sum( (tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)- ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty)*(tblPurchaseItems.Discount/100))+((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) - ((tblPurchaseItems.PurchaseRate*tblPurchaseItems.qty) *(tblPurchaseItems.Discount/100))) *(tblPurchaseItems.CST/100) ) AS CSTRate FROM tblPurchase,tblPurchaseItems,tblLedger,tblProductMaster WHERE  tblPurchase.SupplierID= tblLedger.LedgerID  AND tblPurchase.PurchaseID = tblPurchaseItems.PurchaseID AND (tblPurchase.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblPurchase.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblPurchaseItems.itemcode AND tblPurchase.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales Return') GROUP By tblPurchaseItems.CST,tblPurchase.BillDate,tblPurchase.PurchaseID,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
             oleCmd.CommandText = sQry;
             oleCmd.CommandType = CommandType.Text;
             oleAdp = new OleDbDataAdapter(oleCmd);
             dsParentQry = new DataSet();
             oleAdp.Fill(dsParentQry);
             return dsParentQry;

         }

        public DataSet avlCST(string sType, string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (sType == "sales")
                sQry = "SELECT Distinct(CST) from tblPurchaseitems;";
            else
                sQry = "SELECT Distinct(CST) from tblSalesitems;";
            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }

        public DataSet generateSalesCSTReport(string sDataSource, DateTime sDate, DateTime eDate, string sType)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry;
            string sQry = string.Empty;
            string sConStr = string.Empty;
            sConStr = sDataSource;
            oleConn = new OleDbConnection( CreateConnectionString( sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;
            if (sType == "Yes")
            {
                //sQry = "SELECT tblSales.Billno,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) AS  Rate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales') GROUP By tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName,tblSalesItems.VAT";
                // sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+' ' +  tblProductMaster.productname+' '+tblProductmaster.ProductDesc+' '+tblProductMaster.Model AS ProductName, tblLedger.LedgerName,tblLedger.TinNumber, tblSalesItems.CST, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.CST/100))) AS CSTRate FROM tblSales, tblSalesItems, tblLedger, tblProductMaster WHERE tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales') GROUP BY tblSalesItems.CST,tblSales.BillDate,tblSales.Billno, tblProductMaster.ItemCode, tblProductMaster.productname, tblProductmaster.ProductDesc, tblProductMaster.Model, tblLedger.LedgerName, tblLedger.TinNumber";
                sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblSalesItems.CST,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.CST/100) ) AS CSTRate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Sales') GROUP By tblSalesItems.CST,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";
            }
            else
            {
                //sQry = "SELECT tblSales.Billno,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblSales.CustomerName,tblSalesItems.VAT,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.VAT/100))) AS  Rate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase Return') GROUP By tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblSales.CustomerName,tblSalesItems.VAT";
                //sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+' ' +  tblProductMaster.productname+' '+tblProductmaster.ProductDesc+' '+tblProductMaster.Model AS ProductName, tblLedger.LedgerName,tblLedger.TinNumber, tblSalesItems.CST, Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))+(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.CST/100))) AS CSTRate FROM tblSales, tblSalesItems, tblLedger, tblProductMaster WHERE tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "# AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase Return') GROUP BY tblSalesItems.CST,tblSales.BillDate,tblSales.Billno, tblProductMaster.ItemCode, tblProductMaster.productname, tblProductmaster.ProductDesc, tblProductMaster.Model, tblLedger.LedgerName, tblLedger.TinNumber";
                sQry = "SELECT tblSales.Billno,tblSales.BillDate,tblProductMaster.ItemCode+ ' '+  tblProductMaster.productname + ' ' + tblProductmaster.ProductDesc  + ' ' + tblProductMaster.Model As ProductName,tblLedger.LedgerName,tblLedger.TinNumber,tblSalesItems.CST,Sum(tblSalesItems.Rate*tblSalesItems.qty-(tblSalesItems.Rate*tblSalesItems.qty*(tblSalesItems.Discount/100))) AS Rate,Sum( (tblSalesItems.Rate*tblSalesItems.qty)- ((tblSalesItems.Rate*tblSalesItems.qty)*(tblSalesItems.Discount/100))+((tblSalesItems.Rate*tblSalesItems.qty) - ((tblSalesItems.Rate*tblSalesItems.qty) *(tblSalesItems.Discount/100))) *(tblSalesItems.CST/100) ) AS CSTRate FROM tblSales,tblSalesItems,tblLedger,tblProductMaster WHERE  tblSales.CustomerID= tblLedger.LedgerID  AND tblSales.Billno = tblSalesItems.Billno AND (tblSales.BillDate>=#" + sDate.ToString("MM/dd/yyyy") + "#  AND tblSales.BillDate <=#" + eDate.ToString("MM/dd/yyyy") + "#) AND tblProductMaster.itemcode = tblSalesItems.itemcode AND tblSales.JournalID IN (Select Transno FROM tblDayBook Where VoucherType = 'Purchase Return') GROUP By tblSalesItems.CST,tblSales.BillDate,tblSales.Billno,tblProductMaster.ItemCode,tblProductMaster.productname,tblProductmaster.ProductDesc,tblProductMaster.Model,tblLedger.LedgerName,tblLedger.TinNumber";

            }

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;
        }

        #endregion

        public DataSet GetPurchaseData(string sDataSource, string itemCode)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry; string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            if(itemCode != "0")
                sQry = "SELECT PI.ItemCode,P.BillDate,SUM(PI.Qty) as Qty,M.ProductName FROM ((tblPurchase P Inner Join tblPurchaseItems PI On P.PurchaseID = PI.PurchaseID) Inner Join tblProductMaster M On PI.ItemCode = M.ItemCode) Where PI.ItemCode='"+ itemCode+"' Group By PI.ItemCode,P.BillDate,M.ProductName";    
            else
                sQry = "SELECT PI.ItemCode,P.BillDate,SUM(PI.Qty) as Qty,M.ProductName FROM ((tblPurchase P Inner Join tblPurchaseItems PI On P.PurchaseID = PI.PurchaseID) Inner Join tblProductMaster M On PI.ItemCode = M.ItemCode) Group By PI.ItemCode,P.BillDate,M.ProductName";    

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }

        public DataSet GetSalesData(string sDataSource, string itemCode)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry; string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            if(itemCode != "0")
                sQry = "SELECT PI.ItemCode,P.BillDate,SUM(PI.Qty) as Qty,M.ProductName FROM ((tblSales P Inner Join tblSalesItems PI On P.BillNo = PI.BillNo) Inner Join tblProductMaster M On PI.ItemCode = M.ItemCode) Where PI.ItemCode='" + itemCode + "'  Group By PI.ItemCode,P.BillDate,M.ProductName";
            else
                sQry = "SELECT PI.ItemCode,P.BillDate,SUM(PI.Qty) as Qty,M.ProductName FROM ((tblSales P Inner Join tblSalesItems PI On P.BillNo = PI.BillNo) Inner Join tblProductMaster M On PI.ItemCode = M.ItemCode) Group By PI.ItemCode,P.BillDate,M.ProductName";

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }

        public DataSet GetProductData(string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry; string sQry = string.Empty;
            string sConStr = string.Empty;


            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            sQry = "SELECT Distinct ItemCode FROM tblProductMaster Order By ItemCode Asc";

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }

        public DataSet ListServiceEntries(string customer,string frequency, string sDataSource)
        {
            
            string sConStr = string.Empty;
            string sQry = string.Empty;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleConn.Open();
            oleCmd.Connection = oleConn;
            DataSet ds = new DataSet();

            sQry = "SELECT ServiceID,RefNumber,Details,CustomerID,Frequency,StartDate,EndDate,Amount FROM tblServiceMaster Where 1=1 ";

            if (customer != "0")
                sQry += " AND CustomerID=" + customer;
                
            
            if(frequency != "0")
                sQry+= " AND Frequency=" + frequency;

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);

            oleAdp.Fill(ds);

            return ds;
            
        }

        public DataSet GetServiceEntryDetails(string serviceID, string sDataSource)
        {

            string sConStr = string.Empty;
            string sQry = string.Empty;
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            sConStr = sDataSource;
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleConn.Open();
            oleCmd.Connection = oleConn;
            DataSet ds = new DataSet();

            sQry = "SELECT ServiceID,RefNumber,Details,CustomerID,IIF(Frequency = 1, 'Monthly' , IIF(Frequency = 3 ,'Quarterly','Annually')) AS Frequency,LedgerName as Customer,StartDate,EndDate,Amount FROM tblServiceMaster INNER JOIN tblLedger ON tblServiceMaster.CustomerID = tblLedger.LedgerID Where ServiceID = " + serviceID;

            oleCmd.CommandText = sQry;
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);

            oleAdp.Fill(ds);

            return ds;

        }

        public DataSet GetServiceVisitDetails(string serviceID, DateTime dueDate, string sDataSource)
        {
            OleDbConnection oleConn;
            OleDbCommand oleCmd;
            OleDbDataAdapter oleAdp;
            DataSet dsParentQry; string sQry = string.Empty;
            string sConStr = string.Empty;
            StringBuilder dbQry = new StringBuilder();

            /* Start Ms Access Database Connection Information */
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Jet OLEDB:Database Password=moonmoon"; ;
            sConStr = sDataSource;
            //sConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sDataSource + ";User Id=admin;Password=moonmoon;Jet OLEDB:System Database=C:\\Program Files\\Microsoft Office\\Office\\SYSTEM.MDW;";
            oleConn = new OleDbConnection(CreateConnectionString(sConStr));
            oleCmd = new OleDbCommand();
            oleCmd.Connection = oleConn;

            dbQry.Append("SELECT V.VisitID,V.Visited,V.ServiceID,E.Details,E.RefNumber,E.StartDate,E.EndDate,E.Amount,IIF(E.Frequency = 1, 'Monthly' , IIF(E.Frequency = 3 ,'Quarterly','Annually')) AS Frequency,L.LedgerName as Customer,V.DueDate,V.VisitDate,V.Amount as AmountReceived,V.Visited,IIF(V.PayMode = 1, 'Cash' , 'Bank/Card') AS PayMode,V.VisitDetails,D.CreditCardNo,V.CustomerID  ");
            dbQry.Append("FROM (((tblServiceVisit V Inner Join tblServiceMaster E ON V.ServiceID = E.ServiceID) INNER JOIN ");
            dbQry.Append("tblLedger L ON V.CustomerID = L.LedgerID) LEFT JOIN tblDayBook D ON D.TransNo = V.TransNo) ");
            dbQry.AppendFormat("Where V.ServiceID = {0}", serviceID);
            dbQry.AppendFormat(" AND V.DueDate = #{0}#", dueDate.ToString("MM/dd/yyyy"));

            oleCmd.CommandText = dbQry.ToString();
            oleCmd.CommandType = CommandType.Text;
            oleAdp = new OleDbDataAdapter(oleCmd);
            dsParentQry = new DataSet();
            oleAdp.Fill(dsParentQry);
            return dsParentQry;

        }
    }

}
