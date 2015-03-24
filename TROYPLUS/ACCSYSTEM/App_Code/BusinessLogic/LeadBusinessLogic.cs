using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccessLayer;
using System.Text;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;

/// <summary>
/// Summary description for LeadBusinessLogic
/// </summary>
public class LeadBusinessLogic : BaseLogic
{


    public LeadBusinessLogic()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public LeadBusinessLogic(string con)
        : base(con)
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet ListLeadContact(string LeadID)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select ContactRefID,ContactDate,ContactSummary From tblLeadContact Where LeadID=" + LeadID;

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    public DataSet GetLeadMasterDetails(string LeadID)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select LeadID,CreationDate, ProspectCustName, Address,Mobile,Landline,Email,ModeOfContact,PersonalResponsible,BusinessType,Branch,Status,LastCompletedAction,NextAction,Category,Information1,Information2,Information3,Information4,Information5,callbackflag,Callbackdate From tblLeadMaster Where LeadID=" + LeadID;

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    public DataSet GetLeadContacts(string LeadID)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select ContactRefID,ContactedDate,ContactSummary,callbackdate,callbackflag From tblLeadContact Where LeadID=" + LeadID;

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    public DataSet ListLeadMaster(string connection, string txtSearch, string dropDown)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select LeadID,CreationDate, ProspectCustName, Address,Mobile,Landline,Email,ModeOfContact,PersonalResponsible,BusinessType,Branch,Status,LastCompletedAction,NextAction,Category,callbackflag,Callbackdate from tblLeadMaster Where LeadID > 0 ";

        if (txtSearch == "" || txtSearch == null)
        {

        }
        else
        {
            if (dropDown == "CreationDate")
            {
                dbQry = dbQry + " AND CreationDate = #" + DateTime.Parse(txtSearch.ToString()).ToString("MM/dd/yyyy") + "#";
            }
            else if (dropDown == "CustomerName")
            {
                dbQry = dbQry + " AND ProspectCustName like '%" + txtSearch + "%'";
            }
            else if (dropDown == "LeadID")
            {
                dbQry = dbQry + " AND LeadID = " + txtSearch + " ";
            }
            else if (dropDown == "Status")
            {
                dbQry = dbQry + " AND Status like '%" + txtSearch + "%'";
            }
            else if (dropDown == "Mobile")
            {
                dbQry = dbQry + " AND Mobile like '%" + txtSearch + "%'";
            }
            else if (dropDown == "LastCompletedAction")
            {
                dbQry = dbQry + " AND LastCompletedAction like '%" + txtSearch + "%'";
            }
            else if (dropDown == "NextAction")
            {
                dbQry = dbQry + " AND NextAction like '%" + txtSearch + "%'";
            }
            else if (dropDown == "Category")
            {
                dbQry = dbQry + " AND Category like '%" + txtSearch + "%'";
            }
            else if (dropDown == "BusinessType")
            {
                dbQry = dbQry + " AND BusinessType like '%" + txtSearch + "%'";
            }
            else if (dropDown == "Branch")
            {
                dbQry = dbQry + " AND Branch like '%" + txtSearch + "%'";
            }
        }

        dbQry = dbQry + " Order By LeadID, CreationDate Desc";

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public void DeleteLeadContact(string refID)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(this.ConnectionString); // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();

        DataSet roleDs = new DataSet();

        string dbQry = string.Empty;
        string sQry = string.Empty;
        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            dbQry = string.Format("Delete From tblLeadContact Where ContactRefID={0}", refID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public void DeleteLeadMaster(string leadID, string userID)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(this.ConnectionString); // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();

        DataSet roleDs = new DataSet();

        string dbQry = string.Empty;
        string sQry = string.Empty;


        string sAuditStr = string.Empty;
        string dbQryData = string.Empty;
        DataSet dsOld = new DataSet();
        int OldTransNo = 0;
        string Olddcreationdate = string.Empty;
        string OldCustomer = string.Empty;
        string OldContact = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();




            dbQryData = "select LeadID,CreationDate, ProspectCustName, Address,Mobile,Landline,Email,ModeOfContact,PersonalResponsible,BusinessType,Branch,Status,LastCompletedAction,NextAction,Category from tblLeadMaster Where LeadID =" + leadID;
            dsOld = manager.ExecuteDataSet(CommandType.Text, dbQryData.ToString());
            if (dsOld != null)
            {
                if (dsOld.Tables[0].Rows.Count > 0)
                {
                    if (dsOld.Tables[0].Rows[0]["LeadID"] != null)
                    {
                        if (dsOld.Tables[0].Rows[0]["LeadID"].ToString() != string.Empty)
                        {
                            OldTransNo = Convert.ToInt32(dsOld.Tables[0].Rows[0]["LeadID"].ToString());
                            Olddcreationdate = dsOld.Tables[0].Rows[0]["CreationDate"].ToString();
                            OldCustomer = dsOld.Tables[0].Rows[0]["ProspectCustName"].ToString();
                            OldContact = dsOld.Tables[0].Rows[0]["ModeOfContact"].ToString();
                        }
                    }
                }
            }




            dbQry = string.Format("Delete From tblLeadContact Where LeadID={0}", leadID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            dbQry = string.Format("Delete From tblLeadMaster Where LeadID={0}", leadID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);



            sAuditStr = "Lead Management Transaction: " + OldTransNo + " got deleted old Record Details : Customer=" + OldCustomer + ",Mode of contact=" + OldContact + ",CreationDate=" + Olddcreationdate + ", DateTime:" + DateTime.Now.ToString() + ", User:" + userID;
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Delete");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);



            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }


    public DataSet GetDropdownList(string connection, string type)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        if (connection.IndexOf("Provider=Microsoft.Jet.OLEDB.4.0;") > -1)
            manager.ConnectionString = CreateConnectionString(connection);
        else
            manager.ConnectionString = CreateConnectionString(connection);

        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            //dbQry = string.Format("select LedgerId, LedgerName from tblLedger inner join tblGroups on tblGroups.GroupID = tblLedger.GroupID Where tblGroups.GroupName IN ('{0}','{1}') Order By LedgerName Asc ", "Sundry Debtors", "Sundry Creditors");
            dbQry = string.Format("select TextValue, TextValue from tblLeadReferences Where Type = '" + type + "' Order By 1");
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    //public void AddUpdateLeadMasterold2(int LeadID, string creationDate, string prospectCustomer, string address, string mobile, string landline, string email, string modeOfContact, string personalResponsible, string businessType, string branch, string status, string LastCompletedAction, string nextAction, string category, DataSet dsLeadContact)
    //{
    //    DBManager manager = new DBManager(DataProvider.SqlServer);
    //    manager.ConnectionString = CreateConnectionString(this.ConnectionString);
    //    DataSet ds = new DataSet();
    //    string dbQry = string.Empty;

    //    try
    //    {

    //        manager.Open();
    //        manager.ProviderType = DataProvider.SqlServer;

    //        manager.BeginTransaction();

    //        if (LeadID < 1)
    //        {
    //            dbQry = string.Format("INSERT INTO tblLeadMaster(CreationDate,ProspectCustName,Address,Mobile,Landline,Email,ModeOfContact,PersonalResponsible,BusinessType,Branch,Status,LastCompletedAction,NextAction,Category) VALUES(Format('{0}', 'dd/mm/yyyy'),'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')",
    //                   creationDate.ToShortDateString(), prospectCustomer, address, mobile, landline, email, modeOfContact, personalResponsible, businessType, branch, status, LastCompletedAction, nextAction, category);

    //            manager.ExecuteNonQuery(CommandType.Text, dbQry);

    //            LeadID = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(LeadID) FROM tblLeadMaster");
    //            LeadID += 1;

    //        }
    //        else
    //        {
    //            dbQry = string.Format("Update tblLeadMaster Set CreationDate='{0}',ProspectCustName='{1}',Address='{2}',Mobile='{3}',Landline='{4}',Email='{5}',ModeOfContact='{6}',PersonalResponsible='{7}',BusinessType='{8}',Branch='{9}',Status='{10}',LastCompletedAction='{11}',NextAction='{12}',Category='{13}' Where LeadID={14}", creationDate.ToShortDateString(), prospectCustomer, address, mobile, landline, email, modeOfContact, personalResponsible, businessType, branch, status, LastCompletedAction, nextAction, category, LeadID);
    //            manager.ExecuteNonQuery(CommandType.Text, dbQry);
    //        }

    //        dbQry = string.Format("Delete From tblLeadContact Where LeadID={0}", LeadID);
    //        manager.ExecuteNonQuery(CommandType.Text, dbQry);

    //        if (dsLeadContact.Tables[0].Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in dsLeadContact.Tables[0].Rows)
    //            {
    //                dbQry = string.Format("INSERT INTO tblLeadContact(LeadID,ContactedDate,ContactSummary) VALUES({0},Format('{1}', 'dd/mm/yyyy'),'{2}')", LeadID, dr["ContactedDate"].ToString(), dr["ContactSummary"].ToString());
    //                manager.ExecuteNonQuery(CommandType.Text, dbQry);
    //            }

    //        }

    //        manager.CommitTransaction();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        manager.Dispose();
    //    }

    //}

    public void AddUpdateLeadMaster(int LeadID, DateTime creationDate, string prospectCustomer, string address, string mobile, string landline, string email, string modeOfContact, string personalResponsible, string businessType, string branch, string status, string LastCompletedAction, string nextAction, string category, DataSet dsLeadContact, string info1, string info2, string info3, string info4, string info5, string callbackflag, string callbackdate)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        int LeadIDD = 0;

        try
        {

            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            dbQry = string.Format("INSERT INTO tblLeadMaster(CreationDate,ProspectCustName,Address,Mobile,Landline,Email,ModeOfContact,PersonalResponsible,BusinessType,Branch,Status,LastCompletedAction,NextAction,Category,Information1,Information2,Information3,Information4,Information5,callbackflag,Callbackdate) VALUES(Format('{0}', 'dd/mm/yyyy'),'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')",
                    creationDate.ToShortDateString(), prospectCustomer, address, mobile, landline, email, modeOfContact, personalResponsible, businessType, branch, status, LastCompletedAction, nextAction, category, info1, info2, info3, info4, info5, callbackflag, callbackdate);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            LeadIDD = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(LeadID) FROM tblLeadMaster");

            if (dsLeadContact.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsLeadContact.Tables[0].Rows)
                {
                    dbQry = string.Format("INSERT INTO tblLeadContact(LeadID,ContactedDate,ContactSummary,callbackflag,callbackdate) VALUES({0},Format('{1}', 'dd/mm/yyyy'),'{2}','{3}','{4}')", LeadIDD, dr["ContactedDate"].ToString(), dr["ContactSummary"].ToString(), dr["Callbackflag"].ToString(), dr["CallbackDate"].ToString());
                    manager.ExecuteNonQuery(CommandType.Text, dbQry);
                }
            }

            manager.CommitTransaction();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    public void UpdateLeadMaster(int LeadID, DateTime creationDate, string prospectCustomer, string address, string mobile, string landline, string email, string modeOfContact, string personalResponsible, string businessType, string branch, string status, string LastCompletedAction, string nextAction, string category, DataSet dsLeadContact, string userID, string info1, string info2, string info3, string info4, string info5, string callbackflag, string callbackdate)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;

        string dbQryData = string.Empty;
        DataSet dsOld = new DataSet();
        int OldTransNo = 0;
        string Olddcreationdate = string.Empty;
        string OldCustomer = string.Empty;
        string OldContact = string.Empty;



        try
        {

            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            dbQryData = "select LeadID,CreationDate, ProspectCustName, Address,Mobile,Landline,Email,ModeOfContact,PersonalResponsible,BusinessType,Branch,Status,LastCompletedAction,NextAction,Category from tblLeadMaster Where LeadID =" + LeadID;
            dsOld = manager.ExecuteDataSet(CommandType.Text, dbQryData.ToString());
            if (dsOld != null)
            {
                if (dsOld.Tables[0].Rows.Count > 0)
                {
                    if (dsOld.Tables[0].Rows[0]["LeadID"] != null)
                    {
                        if (dsOld.Tables[0].Rows[0]["LeadID"].ToString() != string.Empty)
                        {
                            OldTransNo = Convert.ToInt32(dsOld.Tables[0].Rows[0]["LeadID"].ToString());
                            Olddcreationdate = dsOld.Tables[0].Rows[0]["CreationDate"].ToString();
                            OldCustomer = dsOld.Tables[0].Rows[0]["ProspectCustName"].ToString();
                            OldContact = dsOld.Tables[0].Rows[0]["ModeOfContact"].ToString();
                        }
                    }
                }
            }



            //if (LeadID < 1)
            //{
            //    dbQry = string.Format("INSERT INTO tblLeadMaster(CreationDate,ProspectCustName,Address,Mobile,Landline,Email,ModeOfContact,PersonalResponsible,BusinessType,Branch,Status,LastCompletedAction,NextAction,Category,Information1,Information2,Information3,Information4,Information5) VALUES(Format('{0}', 'dd/mm/yyyy'),'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
            //           creationDate.ToShortDateString(), prospectCustomer, address, mobile, landline, email, modeOfContact, personalResponsible, businessType, branch, status, LastCompletedAction, nextAction, category, info1, info2, info3, info4, info5);

            //    manager.ExecuteNonQuery(CommandType.Text, dbQry);

            //    LeadID = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(LeadID) FROM tblLeadMaster");
            //    //LeadID += 1;
            //}
            //else
            //{
            dbQry = string.Format("Update tblLeadMaster Set CreationDate=Format('{0}', 'dd/mm/yyyy'),ProspectCustName='{1}',Address='{2}',Mobile='{3}',Landline='{4}',Email='{5}',ModeOfContact='{6}',PersonalResponsible='{7}',BusinessType='{8}',Branch='{9}',Status='{10}',LastCompletedAction='{11}',NextAction='{12}',Category='{13}', Information1='{15}', Information2='{16}', Information3='{17}', Information4='{18}', Information5='{19}',callbackflag='{20}',Callbackdate='{21}'  Where LeadID={14}", creationDate.ToShortDateString(), prospectCustomer, address, mobile, landline, email, modeOfContact, personalResponsible, businessType, branch, status, LastCompletedAction, nextAction, category, LeadID, info1, info2, info3, info4, info5, callbackflag, callbackdate);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            //}

            dbQry = string.Format("Delete From tblLeadContact Where LeadID={0}", LeadID);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            if (dsLeadContact.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsLeadContact.Tables[0].Rows)
                {
                    dbQry = string.Format("INSERT INTO tblLeadContact(LeadID,ContactedDate,ContactSummary,CallBackFlag,CallBackDate) VALUES({0},Format('{1}', 'dd/mm/yyyy'),'{2}','{3}','{4}')", LeadID, dr["ContactedDate"].ToString(), dr["ContactSummary"].ToString(), dr["CallBackFlag"].ToString(), dr["CallBackDate"].ToString());
                    manager.ExecuteNonQuery(CommandType.Text, dbQry);
                }

            }



            sAuditStr = "Lead Management Transaction: " + LeadID + " got edited. Old Record Details : Lead Id No=" + OldTransNo + " Customer=" + OldCustomer + ",Mode of Contact=" + OldContact + ",CreationDate=" + Olddcreationdate + " DateTime:" + DateTime.Now.ToString() + " User:" + userID;

            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command) VALUES('{0}','{1}')", sAuditStr, "Edit and Update");
            manager.ExecuteNonQuery(CommandType.Text, dbQry);


            manager.CommitTransaction();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }


    public DataSet ListReferenceInfo(string connection, string txtSearch, string dropDown)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        txtSearch = "%" + txtSearch + "%";

        if (dropDown == "NameDropDown")
        {
            dbQry = "select A.Id,A.Type,A.TextValue,A.TypeName, (Select count(*) from tblLeadReferences where A.Id>=Id) as Row from tblLeadReferences as A Where A.TypeName like '" + txtSearch + "'" + " Order By A.Id desc";
            //dbQry = "select A.Task_Status_Name,A.Task_Status_Id, (Select count(*) from tblTaskStatus where A.Task_Status_Id>=Task_Status_Id) as Row from tblTaskStatus as A Where A.Task_Status_Name like '" + txtSearch + "'" + " Order By A.Task_Status_Id";
        }
        else if (dropDown == "ValueDropDown")
        {
            dbQry = "select A.Id,A.Type,A.TextValue,A.TypeName, (Select count(*) from tblLeadReferences where A.Id>=Id) as Row from tblLeadReferences as A Where A.TextValue like '" + txtSearch + "'" + " Order By A.Id desc";
        }    
        else
        {
            dbQry = string.Format("select A.Id,A.Type,A.TextValue,A.TypeName, (Select count(*) from tblLeadReferences where A.Id<=Id) as Row from tblLeadReferences as A Order By A.Id desc", txtSearch);
        }

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    public void UpdateReference(string connection, int ID, string TextValue, string TypeName, string Types, int TypeID, string username)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            object exists = manager.ExecuteScalar(CommandType.Text, "SELECT Count(*) FROM tblLeadReferences Where TypeName='" + TypeName + "' and TextValue='" + TextValue + "' And ID <> " + ID.ToString() + "");

            if (exists.ToString() != string.Empty)
            {
                if (int.Parse(exists.ToString()) > 0)
                {
                    throw new Exception("Reference Exists");
                }
            }

            dbQry = string.Format("Update tblLeadReferences SET TextValue='{1}', TypeName='{2}', Type ='{3}',TypeID ={4} WHERE ID={0}", ID, TextValue, TypeName, Types, TypeID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            sAuditStr = "Lead Rererence Text Value: " + TextValue + " got edited. Record Details : User: " + username;

            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command,auditdate) VALUES('{0}','{1}','{2}')", sAuditStr, "Edit and Update", DateTime.Now.ToString("yyyy-MM-dd"));
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (manager != null)
                manager.Dispose();
        }
    }



    public void InsertReference(string connection, string TextValue, string TypeName, string Types, int TypeID, string username)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            object exists = manager.ExecuteScalar(CommandType.Text, "SELECT Count(*) FROM tblLeadReferences Where TypeName='"+ TypeName +"' and TextValue='" + TextValue + "'");

            if (exists.ToString() != string.Empty)
            {
                if (int.Parse(exists.ToString()) > 0)
                {
                    throw new Exception("Reference Exists");
                }
            }

            //int IDD = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(ID) FROM tblLeadReferences");

            dbQry = string.Format("INSERT INTO tblLeadReferences(TextValue, TypeName,Type,TypeID) VALUES('{0}','{1}','{2}',{3})",
                TextValue, TypeName, Types, TypeID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            sAuditStr = "Lead Rererence Text Value: " + TextValue + " added. Record Details : User: " + username;

            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command,auditdate) VALUES('{0}','{1}','{2}')", sAuditStr, "Add New", DateTime.Now.ToString("yyyy-MM-dd"));
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }


    public void DeleteReference(string connection, int ID, string username)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;
            manager.BeginTransaction();

            dbQry = string.Format("Delete From tblLeadReferences Where ID = {0}", ID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);



            sAuditStr = "Lead Reference Lead No: " + ID + " Deleted. Record Details : User: " + username;
            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command,auditdate) VALUES('{0}','{1}','{2}')", sAuditStr, "Delete", DateTime.Now.ToString("yyyy-MM-dd"));
            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.CommitTransaction();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }


    public DataSet GetReferenceForId(string connection, int ID)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select ID,TextValue,TypeName,Type,TypeID from tblLeadReferences where ID = " + ID.ToString();
            manager.Open();

            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (manager != null)
                manager.Dispose();
        }
    }

    public DataSet ListLeadMasterContacts(string connection, string txtSearch, string dropDown)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select tblLeadMaster.LeadID,CreationDate, ProspectCustName, Address,Mobile,Landline,Email,ModeOfContact,PersonalResponsible,BusinessType,Branch,Status,LastCompletedAction,NextAction,Category,tblLeadContact.ContactedDate,ContactSummary from tblLeadMaster inner join tblLeadContact on tblLeadMaster.leadid = tblLeadContact.leadid Where tblLeadMaster.LeadID > 0 ";

        if (txtSearch == "" || txtSearch == null)
        {

        }
        else
        {
            if (dropDown == "CreationDate")
            {
                dbQry = dbQry + " AND CreationDate = #" + DateTime.Parse(txtSearch.ToString()).ToString("MM/dd/yyyy") + "#";
            }
            else if (dropDown == "Status")
            {
                dbQry = dbQry + " AND Status like '%" + txtSearch + "%'";
            }
            else if (dropDown == "Mobile")
            {
                dbQry = dbQry + " AND Mobile like '%" + txtSearch + "%'";
            }
            else if (dropDown == "LastCompletedAction")
            {
                dbQry = dbQry + " AND LastCompletedAction like '%" + txtSearch + "%'";
            }
            else if (dropDown == "NextAction")
            {
                dbQry = dbQry + " AND NextAction like '%" + txtSearch + "%'";
            }
            else if (dropDown == "Category")
            {
                dbQry = dbQry + " AND Category like '%" + txtSearch + "%'";
            }
            else if (dropDown == "BusinessType")
            {
                dbQry = dbQry + " AND BusinessType like '%" + txtSearch + "%'";
            }
            else if (dropDown == "Branch")
            {
                dbQry = dbQry + " AND Branch like '%" + txtSearch + "%'";
            }
        }

        dbQry = dbQry + " Order By tblLeadMaster.LeadID, CreationDate Desc";

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    //public void AddLead(int LeadNo, DateTime startDate, string LeadName, string address, string mobile, string Telephone, string BpName, int BpId, string ContactName, int EmpId, string EmpName, string Status, string branch, string LeadStatus, double TotalAmount, int ClosingPer, DateTime ClosingDate, int PredictedClosing, DateTime PredictedClosingDate,string Info1,int Info3,int Info4,int businesstype,int category,int area,int InterestLevel, double PotentialPotAmount, double PotentialWeightedAmount, string PredictedClosingPeriod, string usernam, DataSet dsStages, DataSet dsCompetitor, DataSet dsActivity, DataSet dsProduct, string check)
    public void AddLead(string connection,int LeadNo, DateTime startDate, string LeadName, string address, string mobile, string Telephone, string BpName, int BpId, string ContactName, int EmpId, string EmpName, string Status, string LeadStatus, DateTime ClosingDate, DateTime PredictedClosingDate, string Info1, int Info3, int Info4, int businesstype, int category, int area, int InterestLevel, string usernam,string Branchcode, DataSet dsCompetitor, DataSet dsActivity, DataSet dsProduct, string check,string username)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        int LeadIDD = 0;
        string sAuditStr = string.Empty;
        try
        {

            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

          

            //dbQry = string.Format("INSERT INTO tblLeadHeader(Start_Date,Lead_Name,Address,Mobile,Telephone,BP_Name,Bp_Id,Contact_Name,Emp_Id,Emp_Name,Doc_Status,Branch,Lead_Status,InvoicedAmt,Closing_Per,Closing_Date,chec) VALUES(Format('{0}', 'dd/mm/yyyy'),'{1}','{2}','{3}','{4}','{5}',{6},'{7}',{8},'{9}','{10}','{11}','{12}',{13},{14},Format('{15}', 'dd/mm/yyyy'),'{16}')",
            //        startDate.ToShortDateString(), LeadName, address, mobile, Telephone, BpName, BpId, ContactName, EmpId, EmpName, Status, branch, LeadStatus, TotalAmount, ClosingPer, ClosingDate, check);


            dbQry = string.Format("INSERT INTO tblLeadHeader(Lead_Name,BP_Name,Address,Mobile,Telephone,Doc_Status,Closing_Date,Emp_Name,Emp_Id,Start_Date,Lead_Status,Contact_Name,Bp_Id,chec,Predicted_Closing_Date,Information1,Information3,Information4,BusinessType,Category,Area,InterestLevel,BranchCode) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}','{11}',{12},'{13}','{14}','{15}',{16},{17},{18},{19},{20},{21},'{22}')",
                LeadName, BpName, address, mobile, Telephone, Status, ClosingDate.ToString("yyyy-MM-dd"), EmpName, EmpId, startDate.ToString("yyyy-MM-dd"), Status, ContactName, BpId, check, PredictedClosingDate.ToString("yyyy-MM-dd"), Info1, Info3, Info4, businesstype, category, area, InterestLevel, Branchcode);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            LeadIDD = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(Lead_No) FROM tblLeadHeader");


            if (dsCompetitor != null)
            {
                if (dsCompetitor.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsCompetitor.Tables[0].Rows)
                    {
                        dbQry = string.Format("INSERT INTO tblCompetitors(Lead_No,Competitor_Name,OurStr_Weak,ComStr_Weak,Remarks,Threat_Level) VALUES({0},'{1}','{2}','{3}','{4}','{5}')", LeadIDD, dr["ComName"].ToString(), dr["OurStrWeak"].ToString(), dr["ComStrWeak"].ToString(), dr["Remarks"].ToString(), dr["ThrLvl"].ToString());
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                    }
                }
            }

            if (dsActivity != null)
            {
                if (dsActivity.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsActivity.Tables[0].Rows)
                    {
                        //dbQry = string.Format("INSERT INTO tblActivities(Lead_No,Start_Date,End_Date,NextActivity_Date,Activity_Name,Activity_Name_Id,Activity_Location,Next_Activity,Next_Activity_Id,FollowUp,Emp_Name,Emp_No,Remarks) VALUES({0},Format('{1}', 'dd/mm/yyyy'),Format('{2}', 'dd/mm/yyyy'),Format('{3}', 'dd/mm/yyyy'),'{4}',{5},'{6}','{7}',{8},'{9}','{10}',{11},'{12}')", LeadIDD, dr["Start_Date"].ToString(), dr["End_Date"].ToString(), dr["NextActivity_Date"].ToString(), dr["Activity_Name"].ToString(), Convert.ToInt32(dr["Activity_Name_Id"]), Convert.ToString(dr["Activity_Location"]), Convert.ToString(dr["Next_Activity"]), Convert.ToInt32(dr["Next_Activity_Id"]), Convert.ToString(dr["FollowUp"]), Convert.ToString(dr["Emp_Name"]), Convert.ToInt32(dr["Emp_No"]), dr["Remarks"].ToString());
                        dbQry = string.Format("INSERT INTO tblActivities(Lead_No,Activity_Name,Activity_Name_Id,Activity_Date,Activity_Location,Next_Activity,Next_Activity_Id,NextActivity_Date,Remarks,Emp_Name,Emp_No,ModeofContact,Information2,Information5) VALUES({0},'{1}',{2},'{3}','{4}','{5}',{6},'{7}','{8}','{9}',{10},'{11}',{12},{13})", LeadIDD, dr["ActName"].ToString(), Convert.ToInt32(dr["ActNameID"]),Convert.ToDateTime(dr["ActDate"].ToString()).ToString("yyyy-MM-dd"), dr["ActLoc"].ToString(), dr["NxtAct"].ToString(), Convert.ToInt32(dr["NxtActID"]), Convert.ToDateTime( dr["NxtActDte"].ToString()).ToString("yyyy-MM-dd"), dr["Remarks"].ToString(), dr["Emp"].ToString(), Convert.ToInt32(dr["EmpID"]), dr["MdeofCnt"].ToString(), Convert.ToInt32(dr["Info2"]), Convert.ToInt32(dr["Info5"]));
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                    }
                }
            }

            if (dsProduct != null)
            {
                if (dsProduct.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsProduct.Tables[0].Rows)
                    {
                        dbQry = string.Format("INSERT INTO tblProductInterest(Lead_No,Product_Id,Product_Name,Product_Model,Product_Category,Product_Brand,Product_Quantity) VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}')", LeadIDD, dr["PrdID"].ToString(), dr["Prd"].ToString(), dr["Model"].ToString(), dr["Cate"].ToString(), dr["Brd"].ToString(), dr["Qty"].ToString());
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                    }
                }
            }

            sAuditStr = "Lead Management Lead Name: " + LeadName + " added. Record Details : User: " + username;

            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command,auditdate) VALUES('{0}','{1}','{2}')", sAuditStr, "Add New", DateTime.Now.ToString("yyyy-MM-dd"));
            manager.ExecuteNonQuery(CommandType.Text, dbQry);


            manager.CommitTransaction();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public DataSet ListLead(string connection, string txtSearch, string dropDown,string branchcode)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string dQry = string.Empty;
        
       // dbQry = "select A.Id,A.Type,A.TextValue,A.TypeName, (Select count(*) from tblLeadReferences where A.Id>=Id) as Row from tblLeadReferences as A Where A.TextValue like '" + txtSearch + "'" + " and A.TypeName = 'Mode of Contact' Order By A.Id";

        if (txtSearch == "" || txtSearch == null)
        {
            if (branchcode != "All")
            {
                dbQry = "select A.Lead_No,A.Lead_Name,A.BP_Name,A.Address,A.Mobile,A.Telephone,A.Doc_Status,A.Closing_Date,A.Emp_Name,A.Emp_Id,A.Start_Date,A.Lead_Status,A.Contact_Name,A.Bp_Id,A.chec,A.Predicted_Closing_Date,A.Information1,A.Information3,A.Information4,A.BusinessType,A.Category,A.Area,A.InterestLevel,A.BranchCode, (Select count(*) from tblLeadHeader where A.Lead_No>=Lead_No) as Row from tblLeadHeader as A Where 1 = 1 AND A.BranchCode = '" + branchcode + "'";
            }
            else
            {
                dbQry = "select A.Lead_No,A.Lead_Name,A.BP_Name,A.Address,A.Mobile,A.Telephone,A.Doc_Status,A.Closing_Date,A.Emp_Name,A.Emp_Id,A.Start_Date,A.Lead_Status,A.Contact_Name,A.Bp_Id,A.chec,A.Predicted_Closing_Date,A.Information1,A.Information3,A.Information4,A.BusinessType,A.Category,A.Area,A.InterestLevel,A.BranchCode, (Select count(*) from tblLeadHeader where A.Lead_No>=Lead_No) as Row from tblLeadHeader as A Where 1 = 1 ";
            }
        }
        else
        {
            dbQry = "select A.Lead_No,A.Lead_Name,A.BP_Name,A.Address,A.Mobile,A.Telephone,A.Doc_Status,A.Closing_Date,A.Emp_Name,A.Emp_Id,A.Start_Date,A.Lead_Status,A.Contact_Name,A.Bp_Id,A.chec,A.Predicted_Closing_Date,A.Information1,A.Information3,A.Information4,A.BusinessType,A.Category,A.Area,A.InterestLevel,A.BranchCode, (Select count(*) from tblLeadHeader where A.Lead_No>=Lead_No) as Row from tblLeadHeader as A Where 1 = 1 ";
            if (dropDown == "StartDate")
            {
                if (branchcode != "All")
                {
                    dbQry = dbQry + " AND A.BranchCode = '" + branchcode + "' AND A.Start_Date = #" + DateTime.Parse(txtSearch.ToString()).ToString("MM/dd/yyyy") + "#";
                }
                else
                {
                    dbQry = dbQry + " AND A.Start_Date = #" + DateTime.Parse(txtSearch.ToString()).ToString("MM/dd/yyyy") + "#";
                }
            }
            else if (dropDown == "BPName")
            {
                if (branchcode != "All")
                {
                    dbQry = dbQry + " AND A.BranchCode = '" + branchcode + "' AND A.BP_Name like '%" + txtSearch + "%'";
                }
                else
                {
                    dbQry = dbQry + " AND A.BP_Name like '%" + txtSearch + "%'";
                }
            }
            else if (dropDown == "LeadNo")
            {
                if (branchcode != "All")
                {
                    dbQry = dbQry + " AND A.BranchCode = '" + branchcode + "' AND A.Lead_No = " + txtSearch + " ";
                }
                else
                {
                    dbQry = dbQry + " AND A.Lead_No = " + txtSearch + " ";
                }
            }
            else if (dropDown == "LeadStatus")
            {
                if (branchcode != "All")
                {
                    dbQry = dbQry + "AND A.BranchCode = '" + branchcode + "' AND A.Lead_Status like '%" + txtSearch + "%'";
                }
                else
                {
                    dbQry = dbQry + " AND A.Lead_Status like '%" + txtSearch + "%'";
                }
            }
            else if (dropDown == "DocStatus")
            {
                if (branchcode != "All")
                {
                    dbQry = dbQry + " AND A.BranchCode = '" + branchcode + "' AND A.Doc_Status like '%" + txtSearch + "%'";
                }
                else
                {
                    dbQry = dbQry + " AND A.Doc_Status like '%" + txtSearch + "%'";
                }
            }
            else if (dropDown == "Mobile")
            {
                if (branchcode != "All")
                {
                    dbQry = dbQry + "AND A.BranchCode = '" + branchcode + "' AND A.Mobile like '%" + txtSearch + "%'";
                }
                else
                {
                    dbQry = dbQry + " AND A.Mobile like '%" + txtSearch + "%'";
                }
            }
            else if (dropDown == "LeadName")
            {
                if (branchcode != "All")
                {
                    dbQry = dbQry + "AND A.BranchCode = '" + branchcode + "' AND A.Lead_Name like '%" + txtSearch + "%'";
                }
                else
                {
                    dbQry = dbQry + " AND A.Lead_Name like '%" + txtSearch + "%'";
                }
            }
            else if (dropDown == "All" || dropDown=="0")
            {
                
            }
            else if(branchcode != "All")
            {
                dbQry = dbQry + " AND A.BranchCode = '" + branchcode + "'";
            }
        }

        dbQry = dbQry + "Order By A.Lead_No Desc";

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataSet dsd;
                DataTable dt;
                DataRow drNew;
                DataColumn dc;
                dsd = new DataSet();
                dt = new DataTable();
                dc = new DataColumn("Lead_No");
                dt.Columns.Add(dc);
                dc = new DataColumn("Lead_Name");
                dt.Columns.Add(dc);
                dc = new DataColumn("BP_Name");
                dt.Columns.Add(dc);
                dc = new DataColumn("Address");
                dt.Columns.Add(dc);
                dc = new DataColumn("Mobile");
                dt.Columns.Add(dc);
                dc = new DataColumn("Telephone");
                dt.Columns.Add(dc);
                dc = new DataColumn("Doc_Status");
                dt.Columns.Add(dc);
                dc = new DataColumn("Closing_Date");
                dt.Columns.Add(dc);
                dc = new DataColumn("Emp_Name");
                dt.Columns.Add(dc);
                dc = new DataColumn("Emp_Id");
                dt.Columns.Add(dc);
                dc = new DataColumn("Start_Date");
                dt.Columns.Add(dc);
                dc = new DataColumn("Lead_Status");
                dt.Columns.Add(dc);
                dc = new DataColumn("Contact_Name");
                dt.Columns.Add(dc);
                dc = new DataColumn("Bp_Id");
                dt.Columns.Add(dc);
                dc = new DataColumn("chec");
                dt.Columns.Add(dc);
                dc = new DataColumn("Predicted_Closing_Date");
                dt.Columns.Add(dc);
                dc = new DataColumn("Information1");
                dt.Columns.Add(dc);
                dc = new DataColumn("Information3");
                dt.Columns.Add(dc);
                dc = new DataColumn("Information4");
                dt.Columns.Add(dc);
                dc = new DataColumn("BusinessType");
                dt.Columns.Add(dc);
                dc = new DataColumn("Category");
                dt.Columns.Add(dc);
                dc = new DataColumn("Area");
                dt.Columns.Add(dc);
                dc = new DataColumn("InterestLevel");
                dt.Columns.Add(dc);
                dc = new DataColumn("Activity_Name");
                dt.Columns.Add(dc);
                dc = new DataColumn("Next_Activity");
                dt.Columns.Add(dc);
                dc = new DataColumn("BranchCode");
                dt.Columns.Add(dc);
                dc = new DataColumn("Row");
                dt.Columns.Add(dc);
                dsd.Tables.Add(dt);
                int co=ds.Tables[0].Rows.Count;
                co = co - ds.Tables[0].Rows.Count;
                co = co + 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                { 
                    drNew = dt.NewRow();
                    if (dr["Lead_No"] != null)
                        drNew["Lead_No"] = dr["Lead_No"].ToString();

                    if (dr["Lead_No"] != null)
                    {
                        if (dr["Lead_No"].ToString() != "")
                        {
                            if (Convert.ToInt32(dr["Lead_No"].ToString()) > 0)
                            {
                                dQry = "SELECT Next_Activity,Activity_Name FROM tblActivities WHERE Lead_No=" + Convert.ToInt32(dr["Lead_No"].ToString()) + " order by Activity_Id asc";
                                DataSet dsdd = manager.ExecuteDataSet(CommandType.Text, dQry);
                                if (dsdd.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow drt in dsdd.Tables[0].Rows)
                                    {
                                        if (drt["Activity_Name"] != null)
                                            drNew["Activity_Name"] = drt["Activity_Name"].ToString();
                                        if (drt["Next_Activity"] != null)
                                            drNew["Next_Activity"] = drt["Next_Activity"].ToString();
                                    }
                                }
                                else
                                {
                                    drNew["Activity_Name"] = "";
                                    drNew["Next_Activity"] = "";
                                }
                            }
                        }
                    }


                    if (dr["Start_Date"] != null)
                        drNew["Start_Date"] = dr["Start_Date"].ToString();
                    if (dr["Lead_Name"] != null)
                        drNew["Lead_Name"] = dr["Lead_Name"].ToString();
                    if (dr["Address"] != null)
                        drNew["Address"] = dr["Address"].ToString();
                    if (dr["Mobile"] != null)
                        drNew["Mobile"] = dr["Mobile"].ToString();
                    if (dr["Telephone"] != null)
                        drNew["Telephone"] = dr["Telephone"].ToString();                  
                    if (dr["BP_Name"] != null)
                        drNew["BP_Name"] = dr["BP_Name"].ToString();                  
                    if (dr["Doc_Status"] != null)
                        drNew["Doc_Status"] = dr["Doc_Status"].ToString();
                    if (dr["Emp_Name"] != null)
                        drNew["Emp_Name"] = dr["Emp_Name"].ToString();
                    if (dr["Emp_Id"] != null)
                        drNew["Emp_Id"] = dr["Emp_Id"].ToString();
                    if (dr["Closing_Date"] != null)
                        drNew["Closing_Date"] = dr["Closing_Date"].ToString();
                    if (dr["Lead_Status"] != null)
                        drNew["Lead_Status"] = dr["Lead_Status"].ToString();                   
                    if (dr["Contact_Name"] != null)
                        drNew["Contact_Name"] = dr["Contact_Name"].ToString();
                    if (dr["Bp_Id"] != null)
                        drNew["Bp_Id"] = dr["Bp_Id"].ToString();
                    if (dr["BranchCode"] != null)
                        drNew["BranchCode"] = dr["BranchCode"].ToString();
                    if (dr["Row"] != null)
                        drNew["Row"] = co;// dr["Row"].ToString();
                    co = co + 1;
                    dsd.Tables[0].Rows.Add(drNew);                        
                }
                return dsd;
            }              
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public DataSet GetLeadDetails(string connection,int LeadNo)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select Lead_No,Lead_Name,BP_Name,Address,Mobile,Telephone,Doc_Status,Closing_Date,Emp_Name,Emp_Id,Start_Date,Lead_Status,Contact_Name,Bp_Id,chec,Predicted_Closing_Date,Information1,Information3,Information4,BusinessType,Category,Area,InterestLevel,BranchCode from tblLeadHeader Where Lead_No=" + LeadNo;

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public DataSet GetLeadPotential(int LeadNo)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select Lead_No, Predicted_Closing, Predicted_Closing_Period, Predicted_Closing_Date, Potential_Amount, Weighted_Amount, Interest_Level from tblLeadPotential Where Lead_No=" + LeadNo;

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }



    public void UpdateLead(string connection, int LeadNo, DateTime startDate, string LeadName, string address, string mobile, string Telephone, string BpName, int BpId, string ContactName, int EmpId, string EmpName, string Status, string LeadStatus, DateTime ClosingDate, DateTime PredictedClosingDate, string Info1, int Info3, int Info4, int businesstype, int category, int area, int InterestLevel, string usernam, string BranchCode, DataSet dsCompetitor, DataSet dsActivity, DataSet dsProduct, string check, string username)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;

        string dbQryData = string.Empty;
        DataSet dsOld = new DataSet();
        int OldTransNo = 0;
        string Olddcreationdate = string.Empty;
        string OldCustomer = string.Empty;
        string OldContact = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            dbQryData = "select Lead_No,Lead_Name,BP_Name,Address,Mobile,Telephone,Doc_Status,Closing_Date,Emp_Name,Emp_Id,Start_Date,Lead_Status,Contact_Name,Bp_Id,chec,Predicted_Closing_Date,Information1,Information3,Information4,BusinessType,Category,Area,InterestLevel,BranchCode from tblLeadHeader Where Lead_No =" + LeadNo;
            dsOld = manager.ExecuteDataSet(CommandType.Text, dbQryData.ToString());
            if (dsOld != null)
            {
                if (dsOld.Tables[0].Rows.Count > 0)
                {
                    if (dsOld.Tables[0].Rows[0]["Lead_No"] != null)
                    {
                        if (dsOld.Tables[0].Rows[0]["Lead_No"].ToString() != string.Empty)
                        {
                            OldTransNo = Convert.ToInt32(dsOld.Tables[0].Rows[0]["Lead_No"].ToString());
                            Olddcreationdate = dsOld.Tables[0].Rows[0]["Start_Date"].ToString();
                            OldCustomer = dsOld.Tables[0].Rows[0]["BP_Name"].ToString();
                            OldContact = dsOld.Tables[0].Rows[0]["Lead_Name"].ToString();
                        }
                    }
                }
            }

            //dbQry = string.Format("INSERT INTO tblLeadHeader(Lead_Name,BP_Name,Address,Mobile,Telephone,Doc_Status,Closing_Date,Emp_Name,Emp_Id,Start_Date,Lead_Status,Contact_Name,Bp_Id,chec,Predicted_Closing_Date,Information1,Information3,Information4,BusinessType,Category,Area,InterestLevel) VALUES('{0}','{1}','{2}','{3}','{4}','{5}',Format('{6}', 'dd/mm/yyyy'),'{7}',{8},Format('{9}', 'dd/mm/yyyy'),'{10}','{11}',{12},'{13}',Format('{14}', 'dd/mm/yyyy'),'{15}',{16},{17},{18},{19},{20},{21})",
            //LeadName, BpName, address, mobile, Telephone, Status, ClosingDate, EmpName, EmpId, startDate.ToShortDateString(), Status, ContactName, BpId, check, PredictedClosingDate.ToShortDateString(), Info1, Info3, Info4, businesstype, category, area, InterestLevel);


            dbQry = string.Format("Update tblLeadHeader Set Lead_Name='{1}',BP_Name='{2}',Address='{3}',Mobile='{4}',Telephone='{5}',Doc_Status='{6}',Closing_Date='{7}',Emp_Name='{8}',Emp_Id={9},Start_Date='{10}',Lead_Status='{11}',Contact_Name='{12}',Bp_Id={13},chec='{14}', Predicted_Closing_Date='{15}', Information1='{16}',Information3={17},Information4={18},BusinessType={19},Category={20},Area={21},InterestLevel={22},BranchCode='{23}'  Where Lead_No={0}", LeadNo, LeadName, BpName, address, mobile, Telephone, Status, ClosingDate.ToString("yyyy-MM-dd"), EmpName, EmpId, startDate.ToString("yyyy-MM-dd"), Status, ContactName, BpId, check, PredictedClosingDate.ToString("yyyy-MM-dd"), Info1, Info3, Info4, businesstype, category, area, InterestLevel, BranchCode);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);


            dbQry = string.Format("Delete From tblCompetitors Where Lead_No={0}", LeadNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            if (dsCompetitor != null)
            {
                if (dsCompetitor.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsCompetitor.Tables[0].Rows)
                    {
                        //dbQry = string.Format("INSERT INTO tblCompetitors(Lead_No,Competitor_Name,Threat_Level,Remarks) VALUES({0},'{1}','{2}','{3}')", LeadNo, dr["Competitor_Name"].ToString(), dr["Threat_Level"].ToString(), dr["Remarks"].ToString());
                        dbQry = string.Format("INSERT INTO tblCompetitors(Lead_No,Competitor_Name,OurStr_Weak,ComStr_Weak,Remarks,Threat_Level) VALUES({0},'{1}','{2}','{3}','{4}','{5}')", LeadNo, dr["ComName"].ToString(), dr["OurStrWeak"].ToString(), dr["ComStrWeak"].ToString(), dr["Remarks"].ToString(), dr["ThrLvl"].ToString());
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                    }
                }
            }

            dbQry = string.Format("Delete From tblActivities Where Lead_No={0}", LeadNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            if (dsActivity != null)
            {
                if (dsActivity.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsActivity.Tables[0].Rows)
                    {
                        //dbQry = string.Format("INSERT INTO tblActivities(Lead_No,Start_Date,End_Date,NextActivity_Date,Activity_Name,Activity_Name_Id,Activity_Location,Next_Activity,Next_Activity_Id,FollowUp,Emp_Name,Emp_No,Remarks) VALUES({0},Format('{1}', 'dd/mm/yyyy'),Format('{2}', 'dd/mm/yyyy'),Format('{3}', 'dd/mm/yyyy'),'{4}',{5},'{6}','{7}',{8},'{9}','{10}',{11},'{12}')", LeadNo, dr["Start_Date"].ToString(), dr["End_Date"].ToString(), dr["NextActivity_Date"].ToString(), dr["Activity_Name"].ToString(), Convert.ToInt32(dr["Activity_Name_Id"]), Convert.ToString(dr["Activity_Location"]), Convert.ToString(dr["Next_Activity"]), Convert.ToInt32(dr["Next_Activity_Id"]), Convert.ToString(dr["FollowUp"]), Convert.ToString(dr["Emp_Name"]), Convert.ToInt32(dr["Emp_No"]), dr["Remarks"].ToString());
                        dbQry = string.Format("INSERT INTO tblActivities(Lead_No,Activity_Name,Activity_Name_Id,Activity_Date,Activity_Location,Next_Activity,Next_Activity_Id,NextActivity_Date,Remarks,Emp_Name,Emp_No,ModeofContact,Information2,Information5) VALUES({0},'{1}',{2},'{3}','{4}','{5}',{6},'{7}','{8}','{9}',{10},'{11}',{12},{13})", LeadNo, dr["ActName"].ToString(), Convert.ToInt32(dr["ActNameID"]), Convert.ToDateTime( dr["ActDate"].ToString()).ToString("yyyy-MM-dd"), dr["ActLoc"].ToString(), dr["NxtAct"].ToString(), Convert.ToInt32(dr["NxtActID"]), Convert.ToDateTime (dr["NxtActDte"].ToString()).ToString("yyyy-MM-dd"), dr["Remarks"].ToString(), dr["Emp"].ToString(), Convert.ToInt32(dr["EmpID"]), dr["MdeofCnt"].ToString(), Convert.ToInt32(dr["Info2"]), Convert.ToInt32(dr["Info5"]));
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                    }
                }
            }

            dbQry = string.Format("Delete From tblProductInterest Where Lead_No={0}", LeadNo);
            manager.ExecuteNonQuery(CommandType.Text, dbQry);
            if (dsProduct != null)
            {
                if (dsProduct.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsProduct.Tables[0].Rows)
                    {
                        //dbQry = string.Format("INSERT INTO tblProductInterest(Lead_No,Product_Id,Product_Name,SlNo) VALUES({0},'{1}','{2}',{3})", LeadNo, dr["Product_Id"].ToString(), dr["Product_Name"].ToString(), Convert.ToInt32(dr["SlNo"]));
                        //dbQry = string.Format("INSERT INTO tblProductInterest(Lead_No,Product_Id,Product_Name) VALUES({0},'{1}','{2}')", LeadNo, dr["PrdID"].ToString(), dr["Prd"].ToString());
                        dbQry = string.Format("INSERT INTO tblProductInterest(Lead_No,Product_Id,Product_Name,Product_Model,Product_Category,Product_Brand,Product_Quantity) VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}')", LeadNo, dr["PrdID"].ToString(), dr["Prd"].ToString(), dr["Model"].ToString(), dr["Cate"].ToString(), dr["Brd"].ToString(), dr["Qty"].ToString());
                        manager.ExecuteNonQuery(CommandType.Text, dbQry);
                    }
                }
            }


            sAuditStr = "Lead Management LeadNo: " + LeadNo + " got edited. Old Record Details : Lead No=" + OldTransNo + " Bp Name=" + OldCustomer + ", Lead Name = " + OldContact + ", Start Date=" + Olddcreationdate + " DateTime:" + DateTime.Now.ToString("yyyy-MM-dd") + " User: " + usernam;

            dbQry = string.Format("INSERT INTO  tblAudit(Description,Command,auditdate) VALUES('{0}','{1}','{2}')", sAuditStr, "Edit and Update", DateTime.Now.ToString("yyyy-MM-dd"));
            manager.ExecuteNonQuery(CommandType.Text, dbQry);           

            manager.CommitTransaction();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public DataSet GetLeadStages(int LeadNo)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select Lead_No,Start_Date,End_Date,Stage_Name,Stage_Setup_Id,Stage_Perc,Potential_Amount,Weighted_Amount,Remarks,Stage_Id From tblStages Where Lead_No=" + LeadNo;

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public DataSet GetLeadCompetitor(string connection,int LeadNo)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select Lead_No,Competitor_Name,OurStr_Weak,ComStr_Weak,Threat_Level,Competitor_Id,Remarks From tblCompetitors Where Lead_No=" + LeadNo;

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public DataSet GetLeadActivity(string connection,int LeadNo)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select Activity_Id,Lead_No,Activity_Name,Activity_Name_Id,Activity_Date,Activity_Location,Next_Activity,Next_Activity_Id,NextActivity_Date,Remarks,Emp_Name,Emp_No,ModeofContact,Information2,Information5 From tblActivities Where Lead_No=" + LeadNo;

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public DataSet GetLeadProduct(string connection,int LeadNo)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select * From tblProductInterest Where Lead_No=" + LeadNo;

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds.Tables[0].Rows.Count > 0)
                return ds;
            else
                return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }



    public bool CheckIfleadreferenceused(string Connection,string referencename,int id)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(Connection); // +sPath; //System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
        int qty = 0;
        string dbQry = string.Empty;
        try
        {
            manager.Open();

            if(referencename=="Business Type")
            {
                dbQry = "SELECT Count(*) FROM tblLeadHeader where Businesstype=" + id;
            }

            else if (referencename == "Category")
            {
                dbQry = "SELECT Count(*) FROM tblLeadHeader where Category=" + id;
            }
            else if (referencename == "Area")
            {
                dbQry = "SELECT Count(*) FROM tblLeadHeader where Area=" + id;
            }
            else if (referencename == "Interest level")
            {
                dbQry = "SELECT Count(*) FROM tblLeadHeader where InterestLevel=" + id;
            }
            else if (referencename == "Additional Information 3")
            {
                dbQry = "SELECT Count(*) FROM tblLeadHeader where Information3=" + id;
            }
            else if (referencename == "Additional Information 4")
            {
                dbQry = "SELECT Count(*) FROM tblLeadHeader where Information4=" + id;
            }
            //if (referencename == "Business Type")
            //{
            //    dbQry = "SELECT Count(*) FROM tblLeadHeader where Businesstype=" + id;
            //}



                //Activity level


            else if(referencename=="Additional Information 5")
            {
                dbQry = "SELECT Count(*) FROM tblActivities where Information5=" + id;
            }
            else if (referencename == "Additional Information 2")
            {
                dbQry = "SELECT Count(*) FROM tblActivities where Information2=" + id;
            }
            else if (referencename == "Mode of Contact")
            {
                dbQry = "SELECT Count(*) FROM tblActivities where ModeofContact=" + id;
            }
            //else if (referencename == "Activity Name")
            //{
            //    dbQry = "SELECT Count(*) FROM tblActivities where Activity_Name_Id=" + id;
            //}
            //else if (referencename == "Activity Name")
            //{
            //    dbQry = "SELECT Count(*) FROM tblActivities where Next_Activity_Id=" + id;
            //}

            else
            {
                dbQry = "SELECT Count(*) FROM tblActivities where Activity_Name_Id=" + id;
            }

            object qtyObj = manager.ExecuteScalar(CommandType.Text, dbQry);

            if (qtyObj != null && qtyObj != DBNull.Value)
            {
                qty = (int)qtyObj;

                if (qty > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }

    }

    public bool IsLeadAlreadyFound(string connection, string leadName)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        string dbQry = string.Empty;
        int qty = 0;

        try
        {

            dbQry = ("SELECT Count(*) FROM tblLeadHeader Where Lead_Name='" + leadName + "'");

        

            manager.Open();
            object retVal = manager.ExecuteScalar(CommandType.Text, dbQry);

            if ((retVal != null) && (retVal != DBNull.Value))
            {
                qty = (int)retVal;

                if (qty > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            manager.Dispose();
        }
    }


}