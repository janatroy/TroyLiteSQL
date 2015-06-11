using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.OleDb;
using System.Data;
using System.Text;
using System.Configuration;
//using NLog;
using DataAccessLayer;

/// <summary>
/// Summary description for InternalTransferBusinessLogic
/// </summary>
public partial class BusinessLogic : IInternalTransferService
{

    public void RaiseInternalTrasfer(string connection, InternalTransferRequest request)
    {
        string sDataSource = CreateConnectionString(connection);

        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = sDataSource.ToLower();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            //int BranchID = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(BranchID) FROM tblOfficeBranches");

            dbQry = string.Format("INSERT INTO tblInternalTransferRequests(UserID, RequestedDate, ItemCode, RequestedBranch, BranchHasStock, Status, Quantity) VALUES('{0}','{1}','{2}','{3}','{4}','{5}',{6})",
                request.UserID, request.RequestedDate.ToString("yyyy-MM-dd"), request.ItemCode, request.RequestedBranch, request.BranchHasStock, request.Status, request.Quantity);

            //dbQry = "Insert into tblUserRole(UserName, Role) VALUES('Prashanth', 'Test')";

            manager.ExecuteNonQuery(CommandType.Text, dbQry.ToString());

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

    public void ApproveInternalTrasfer(string connection, InternalTransferRequest request)
    {
        string sDataSource = CreateConnectionString(connection);

        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = sDataSource.ToLower();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        string dbQ = string.Empty;
        DataSet dsd = new DataSet();

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            //int BranchID = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(BranchID) FROM tblOfficeBranches");
            string completedDate = request.CompletedDate != null? request.CompletedDate.GetValueOrDefault().ToString("yyyy-MM-dd") : "null";
            dbQry = string.Format("UPDATE tblInternalTransferRequests SET CompletedUser='{0}', CompletedDate='{1}', Status='{2}' Where RequestID={3}",
                request.UserID, completedDate , request.Status, request.RequestID);

            //dbQry = "Insert into tblUserRole(UserName, Role) VALUES('Prashanth', 'Test')";

            manager.ExecuteNonQuery(CommandType.Text, dbQry.ToString());



            string savesap = string.Empty;
            dbQ = "SELECT KeyValue From tblSettings WHERE KEYNAME='SAPPROCESS'";
            dsd = manager.ExecuteDataSet(CommandType.Text, dbQ.ToString());
            if (dsd.Tables[0].Rows.Count > 0)
                savesap = dsd.Tables[0].Rows[0]["KeyValue"].ToString();

            if (savesap == "YES")
            {
                int Unique = 0;
                object retUniqueKey = manager.ExecuteScalar(CommandType.Text, "SELECT MAX(UniqueKey) FROM SAPOUT");

                if ((retUniqueKey != null) && (retUniqueKey != DBNull.Value))
                {
                    Unique = Convert.ToInt32(manager.ExecuteScalar(CommandType.Text, "SELECT MAX(UniqueKey) FROM SAPOUT"));
                    Unique = Unique + 1;
                }
                else
                {
                    Unique = 1;
                }

                string xmldata = "";
                xmldata = xmldata + "<InternalTransfer><Date>" + Convert.ToDateTime(completedDate).ToString("yyyyMMdd") + "</Date><ReqID>" + request.RequestID + "</ReqID><ReqUser>" + request.CompletedUser + "</ReqUser><ProductCode>" + request.ItemCode + "</ProductCode><Quantity>" + request.Quantity + "</Quantity><RequestingBranch>" + request.RequestedBranch + "</RequestingBranch><RequestedBranch>" + request.BranchHasStock + "</RequestedBranch><UniqueKey>" + Unique + "</UniqueKey></InternalTransfer>";

                dbQry = string.Format("INSERT INTO SAPOUT(EntryCreateDateTime,TroyTransNo,ObjName,XMLData,UniqueKey) VALUES('{0}',{1},'{2}','{3}',{4})",
                      DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), request.RequestID, "InternalTransfer", xmldata, Unique);

                manager.ExecuteNonQuery(CommandType.Text, dbQry);
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

    public void RejectInternalTrasfer(string connection, InternalTransferRequest request)
    {
        string sDataSource = CreateConnectionString(connection);

        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = sDataSource.ToLower();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        string Status = "Reject";

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            //int BranchID = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(BranchID) FROM tblOfficeBranches");
          //  DateTime completedDate = request.CompletedDate != null ? request.CompletedDate.GetValueOrDefault().ToString("yyyy-MM-dd") : "null";
            DateTime completedDate = DateTime.Now;

            dbQry = string.Format("UPDATE tblInternalTransferRequests SET CompletedUser='{0}', CompletedDate='{1}', Status='{2}' Where RequestID={3}",
                request.UserID, completedDate.ToString("yyyy-MM-dd"), Status, request.RequestID);

            //dbQry = "Insert into tblUserRole(UserName, Role) VALUES('Prashanth', 'Test')";

            manager.ExecuteNonQuery(CommandType.Text, dbQry.ToString());

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

    public bool CheckIftheItemHasStock(string connection, string ItemCode, string BranchCode, decimal Qty)
    {
        string sDataSource = CreateConnectionString(connection);

        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = sDataSource;
        DataSet ds = new DataSet();
        //string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            object returnStock = manager.ExecuteScalar(CommandType.Text, "Select stock from tblProductStock WHERE itemCode='" + ItemCode + "' and BranchCode='"+BranchCode+"'");
            if ((returnStock != null) && (returnStock != DBNull.Value))
            {
                if (Convert.ToDecimal(returnStock) < Qty)
                {
                    return false;
                }

            }

            return true;

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

    public DataSet GetRequestedBranchSupplierID(string connection, string BranchCode,int supid)
    {
        string sDataSource = CreateConnectionString(connection);

        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = sDataSource;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            dbQry = string.Format("select LedgerId, LedgerName from tblLedger inner join tblGroups on tblGroups.GroupID = tblLedger.GroupID Where (tblGroups.GroupName='Sundry Debtors' or GroupName = 'Sundry Creditors') and tblLedger.Inttrans ='YES' and LedgerId='" + supid  + "' ORDER By LedgerName");

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

    public DataSet GetProductList(string connection)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;



        //dbQry = "select ItemCode,ProductName from tblProductMaster  Order By ProductName";
        dbQry = "SELECT Model + ' - ' + ItemCode + ' - ' + Productdesc  As ProductName,ItemCode FROM tblProductMaster Order By ProductName,Model,ItemCode Asc";

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
            if (manager != null)
                manager.Dispose();
        }

    }

    public DataSet GetBranches(string connection)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;



        //dbQry = "select ItemCode,ProductName from tblProductMaster  Order By ProductName";
        dbQry = "SELECT [BranchID],[BranchName],[Branchcode],[BranchAddress1],[BranchAddress2],[BranchAddress3],[BranchLocation],[IsActive],[iCustomerID],[iSupplierID] FROM tblBranch Order By BranchName Asc";

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
            if (manager != null)
                manager.Dispose();
        }

    }


    public List<InternalTransferRequest> ListInternalRequests(string connection, string txtSearch, string dropDown,string branch)
    {
        DataSet dsData = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        List<InternalTransferRequest> list = new List<InternalTransferRequest>();
      
        try
        {
            string sDataSource = CreateConnectionString(connection);

            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = sDataSource;

            manager.Open();

            if (!(dropDown == "ItemCode" || dropDown == "Status" || dropDown == "RequestedBranch" || dropDown == "CompletedDate" || dropDown == "BranchHasStock" || dropDown == "RequestID" || dropDown == "UserID" || dropDown == "RequestedDate"))
                txtSearch = "%" + txtSearch + "%";
            if (branch == "All")
            {
                dbQry.Append("SELECT RequestID, UserID, RequestedDate, ItemCode, RequestedBranch, BranchHasStock, Status, Quantity, RejectedReason, CompletedDate, CompletedUser FROM tblInternalTransferRequests  ");
            }
            else
            {

                dbQry.Append("SELECT RequestID, UserID, RequestedDate, ItemCode, RequestedBranch, BranchHasStock, Status, Quantity, RejectedReason, CompletedDate, CompletedUser FROM tblInternalTransferRequests where RequestedBranch='"+ branch +"'  ");
            }

            if (dropDown == "ItemCode" && !string.IsNullOrEmpty(txtSearch))
            {
                dbQry.AppendFormat("Where ItemCode = '{0}' ", txtSearch);
            }
            else if (dropDown == "Status" && !string.IsNullOrEmpty(txtSearch))
            {
                dbQry.AppendFormat("Where Status = '{0}' ", txtSearch);
            }
            else if (dropDown == "RequestedBranch" && !string.IsNullOrEmpty(txtSearch))
            {
                dbQry.AppendFormat("Where RequestedBranch = '{0}' ", txtSearch);
            }
            else if (dropDown == "CompletedDate" && !string.IsNullOrEmpty(txtSearch))
            {
                dbQry.AppendFormat("Where CompletedDate = '{0}' ", Convert.ToDateTime(txtSearch).ToString("yyyy-MM-dd"));
            }
            else if (dropDown == "BranchHasStock" && !string.IsNullOrEmpty(txtSearch))
            {
                dbQry.AppendFormat("Where BranchHasStock = '{0}' ", txtSearch);
            }
            else if (dropDown == "UserID" && !string.IsNullOrEmpty(txtSearch))
            {
                dbQry.AppendFormat("Where UserID = '{0}' ", txtSearch);
            }
            else if (dropDown == "RequestID" && !string.IsNullOrEmpty(txtSearch))
            {
                dbQry.AppendFormat("Where RequestID = '{0}' ", txtSearch);
            }
            else if (dropDown == "RequestedDate" && !string.IsNullOrEmpty(txtSearch))
            {
                dbQry.AppendFormat("Where RequestedDate = '{0}' ", Convert.ToDateTime(txtSearch).ToString("yyyy-MM-dd"));
            }

            dbQry.Append(" Order By Status desc,RequestedDate desc ");

            dsData = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());
            if (dsData != null)
            {
                if (dsData.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow item in dsData.Tables[0].Rows)
                    {
                        InternalTransferRequest newRequest = new InternalTransferRequest();
                        newRequest.RequestID = item.Field<int>("RequestID");
                        newRequest.UserID = item["UserID"].ToString();
                        newRequest.RequestedDate = item.Field<DateTime>("RequestedDate");
                        newRequest.ItemCode = item["ItemCode"].ToString();
                        newRequest.Quantity = item["Quantity"].ToString() != "" ? Convert.ToDecimal( item["Quantity"].ToString()): 0;
                        newRequest.RequestedBranch = item["RequestedBranch"].ToString();
                        newRequest.RejectedReason = item["RejectedReason"].ToString();
                        newRequest.CompletedUser = item["CompletedUser"].ToString();
                        newRequest.Status = item["Status"].ToString();
                        newRequest.CompletedDate = item.Field<DateTime?>("CompletedDate");
                        newRequest.BranchHasStock = item["BranchHasStock"].ToString();

                        list.Add(newRequest);
                    }
                }
            }

            return list;
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }
    }

    public List<InternalTransferRequest> ListInternalRequests1(string connection, string txtSearch, string dropDown,string branch)
    {
        DataSet dsData = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        List<InternalTransferRequest> list = new List<InternalTransferRequest>();

        try
        {
            string sDataSource = CreateConnectionString(connection);

            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = sDataSource;

            manager.Open();

            if (!(dropDown == "ItemCode" || dropDown == "Status" || dropDown == "RequestedBranch" || dropDown == "CompletedDate" || dropDown == "BranchHasStock" || dropDown == "RequestID" || dropDown == "UserID" || dropDown == "RequestedDate"))
                txtSearch = "%" + txtSearch + "%";


            if (branch == "All")
            {
                dbQry.Append("SELECT RequestID, UserID, RequestedDate, ItemCode, RequestedBranch, BranchHasStock, Status, Quantity, RejectedReason, CompletedDate, CompletedUser FROM tblInternalTransferRequests ");
            }
            else
            {
                dbQry.Append("SELECT RequestID, UserID, RequestedDate, ItemCode, RequestedBranch, BranchHasStock, Status, Quantity, RejectedReason, CompletedDate, CompletedUser FROM tblInternalTransferRequests where BranchHasStock='"+ branch +"'  ");
            }

                if (dropDown == "ItemCode" && !string.IsNullOrEmpty(txtSearch))
                {
                    dbQry.AppendFormat("Where ItemCode = '{0}' ", txtSearch);
                }
                else if (dropDown == "Status" && !string.IsNullOrEmpty(txtSearch))
                {
                    dbQry.AppendFormat("Where Status = '{0}' ", txtSearch);
                }
                else if (dropDown == "RequestedBranch" && !string.IsNullOrEmpty(txtSearch))
                {
                    dbQry.AppendFormat("Where RequestedBranch = '{0}' ", txtSearch);
                }
                else if (dropDown == "CompletedDate" && !string.IsNullOrEmpty(txtSearch))
                {
                    dbQry.AppendFormat("Where CompletedDate = '{0}' ", Convert.ToDateTime(txtSearch).ToString("yyyy-MM-dd"));
                }
                else if (dropDown == "BranchHasStock" && !string.IsNullOrEmpty(txtSearch))
                {
                    dbQry.AppendFormat("Where BranchHasStock = '{0}' ", txtSearch);
                }
                else if (dropDown == "UserID" && !string.IsNullOrEmpty(txtSearch))
                {
                    dbQry.AppendFormat("Where UserID = '{0}' ", txtSearch);
                }
                else if (dropDown == "RequestID" && !string.IsNullOrEmpty(txtSearch))
                {
                    dbQry.AppendFormat("Where RequestID = '{0}' ", txtSearch);
                }
                else if (dropDown == "RequestedDate" && !string.IsNullOrEmpty(txtSearch))
                {
                    dbQry.AppendFormat("Where RequestedDate = '{0}' ", Convert.ToDateTime(txtSearch).ToString("yyyy-MM-dd"));
                }

            dbQry.Append(" Order By Status desc,RequestedDate desc ");

            dsData = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());
            if (dsData != null)
            {
                if (dsData.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow item in dsData.Tables[0].Rows)
                    {
                        InternalTransferRequest newRequest = new InternalTransferRequest();
                        newRequest.RequestID = item.Field<int>("RequestID");
                        newRequest.UserID = item["UserID"].ToString();
                        newRequest.RequestedDate = item.Field<DateTime>("RequestedDate");
                        newRequest.ItemCode = item["ItemCode"].ToString();
                        newRequest.Quantity = item["Quantity"].ToString() != "" ? Convert.ToDecimal(item["Quantity"].ToString()) : 0;
                        newRequest.RequestedBranch = item["RequestedBranch"].ToString();
                        newRequest.RejectedReason = item["RejectedReason"].ToString();
                        newRequest.CompletedUser = item["CompletedUser"].ToString();
                        newRequest.Status = item["Status"].ToString();
                        newRequest.CompletedDate = item.Field<DateTime?>("CompletedDate");
                        newRequest.BranchHasStock = item["BranchHasStock"].ToString();

                        list.Add(newRequest);
                    }
                }
            }

            return list;
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }
    }


    public InternalTransferRequest GetInternalTransferRequest(string connection, int RequestID)
    {
        DataSet dsData = new DataSet();
        StringBuilder dbQry = new StringBuilder();
        List<InternalTransferRequest> list = new List<InternalTransferRequest>();

        try
        {
            string sDataSource = CreateConnectionString(connection);

            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = sDataSource;

            manager.Open();

            
            dbQry.AppendFormat("SELECT RequestID, UserID, RequestedDate, ItemCode, RequestedBranch, BranchHasStock, Status, Quantity, RejectedReason, CompletedDate, CompletedUser FROM tblInternalTransferRequests Where RequestID={0}", RequestID);

            
            dsData = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());
            if (dsData != null)
            {
                if (dsData.Tables[0].Rows.Count > 0)
                {

                    DataRow item = dsData.Tables[0].Rows[0];
                    InternalTransferRequest newRequest = new InternalTransferRequest();
                    newRequest.RequestID = item.Field<int>("RequestID");
                    newRequest.UserID = item["UserID"].ToString();
                    newRequest.RequestedDate = item.Field<DateTime>("RequestedDate");
                    newRequest.ItemCode = item["ItemCode"].ToString();
                    newRequest.Quantity = item["Quantity"].ToString() != "" ? Convert.ToDecimal(item["Quantity"].ToString()) : 0;
                    newRequest.RequestedBranch = item["RequestedBranch"].ToString();
                    newRequest.RejectedReason = item["RejectedReason"].ToString();
                    newRequest.CompletedUser = item["CompletedUser"].ToString();
                    newRequest.Status = item["Status"].ToString();
                    newRequest.CompletedDate = item.Field<DateTime?>("CompletedDate");
                    newRequest.BranchHasStock = item["BranchHasStock"].ToString();

                    return newRequest;
                }
            }

         
        }
        catch (Exception ex)
        {
            //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
            throw ex;
        }

        return null;
    }


    public void UpdateInternalRequest(string connection, InternalTransferRequest request)
    {
        string sDataSource = CreateConnectionString(connection);

        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = sDataSource.ToLower();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            //int BranchID = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(BranchID) FROM tblOfficeBranches");

            dbQry = string.Format("UPDATE tblInternalTransferRequests SET UserID='{0}', RequestedDate='{1}', ItemCode='{2}', RequestedBranch='{3}', BranchHasStock='{4}', Status='{5}', Quantity={6} Where RequestID={7}",
                request.UserID, request.RequestedDate.ToString("yyyy-MM-dd"), request.ItemCode, request.RequestedBranch, request.BranchHasStock, request.Status, request.Quantity, request.RequestID);

            //dbQry = "Insert into tblUserRole(UserName, Role) VALUES('Prashanth', 'Test')";

            manager.ExecuteNonQuery(CommandType.Text, dbQry.ToString());

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


    public void DeleteInternalRequest(string connection, int RequestID)
    {
        string sDataSource = CreateConnectionString(connection);
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = sDataSource.ToLower();

        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;
            manager.BeginTransaction();

            dbQry = string.Format("Delete From tblInternalTransferRequests Where RequestID={0}", RequestID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry.ToString());

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

    public DataSet GeBranchHasStockCustomerID(string connection, string BranchCode,int cusid)
    {
        string connectionStr = CreateConnectionString(connection);

        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = connectionStr.ToLower();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            dbQry = string.Format("select LedgerId, LedgerName from tblLedger inner join tblGroups on tblGroups.GroupID = tblLedger.GroupID where tblGroups.GroupName='Sundry Debtors' and BranchCode='" + BranchCode + "' and LedgerID=" +  cusid + " Order By ledgerName");

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            return ds;
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

    public DataSet GeBranchHasStockExecutives(string connection, string BranchCode)
    {
        string connectionStr = CreateConnectionString(connection);

        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = connectionStr.ToLower();
        DataSet ds = new DataSet();
        string dbQry = "Select empno,empFirstName From tblEmployee Order By empFirstName";

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
            if (manager != null)
                manager.Dispose();
        }
    }


    public int GetCustomerIDForBranchCode(string connection, string BranchCode)
    {
        string connectionStr = CreateConnectionString(connection);

        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = connectionStr;
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        string BranchName = string.Empty;

        DataSet  dst =new DataSet();

        StringBuilder dbQrytt = new StringBuilder();

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;


            dbQrytt.Append("SELECT BranchName FROM  tblBranch ");
                    dbQrytt.AppendFormat("Where BranchCode = '{0}'", BranchCode);

                    dst = manager.ExecuteDataSet(CommandType.Text, dbQrytt.ToString());
                

                if (dst != null)
                {
                    foreach (DataRow drdd in dst.Tables[0].Rows)
                    {
                        BranchName = drdd["BranchName"].ToString();
                    }

                }

            //dbQry = string.Format("select iCustomerID, iSupplierID from tblBranch Where BranchCode = '{0}'", BranchCode.ToUpper());
            dbQry = string.Format("select LedgerId, LedgerName from tblLedger inner join tblGroups on tblGroups.GroupID = tblLedger.GroupID  Where tblGroups.GroupName='Sundry Debtors' and tblLedger.LedgerName ='"+ BranchName +"' ");

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LedgerId"].ToString() != "")
                    return int.Parse(ds.Tables[0].Rows[0]["LedgerId"].ToString());
                else
                    return 0;
            }
            else
                return 0;
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

    public int GetSupplierIDForBranchCode(string connection, string BranchCode)
    {
        string connectionStr = CreateConnectionString(connection);

        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = connectionStr.ToLower();
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        string BranchName = string.Empty;

        DataSet dst = new DataSet();

        StringBuilder dbQrytt = new StringBuilder();

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            dbQrytt.Append("SELECT BranchName FROM  tblBranch ");
            dbQrytt.AppendFormat("Where BranchCode = '{0}'", BranchCode);

            dst = manager.ExecuteDataSet(CommandType.Text, dbQrytt.ToString());


            if (dst != null)
            {
                foreach (DataRow drdd in dst.Tables[0].Rows)
                {
                    BranchName = drdd["BranchName"].ToString();
                }

            }

            //dbQry = string.Format("select iCustomerID, iSupplierID from tblBranch Where BranchCode = '{0}'", BranchCode.ToUpper());
            dbQry = string.Format("select LedgerId, LedgerName from tblLedger inner join tblGroups on tblGroups.GroupID = tblLedger.GroupID  Where tblGroups.GroupName='Sundry Creditors' and tblLedger.LedgerName ='" + BranchName + "' ");

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["LedgerId"].ToString() != "")
                    return int.Parse(ds.Tables[0].Rows[0]["LedgerId"].ToString());
                else
                    return 0;
            }
            else
                return 0;
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

public class InternalTransferRequest
{
    public int RequestID{get; set;}
    public string UserID { get; set; }
    public DateTime RequestedDate { get; set; }
    public string ItemCode { get; set; }
    public decimal Quantity { get; set; }
    public string RequestedBranch { get; set; }
    public string BranchHasStock { get; set; }
    public string Status { get; set; }
    public string RejectedReason { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string CompletedUser { get; set; }
}

public interface IInternalTransferService
{
    void RaiseInternalTrasfer(string connection, InternalTransferRequest request);
    void ApproveInternalTrasfer(string connection, InternalTransferRequest request);
    void RejectInternalTrasfer(string connection, InternalTransferRequest request);
    DataSet GetProductList(string connection);
    DataSet GetBranches(string connection);
    List<InternalTransferRequest> ListInternalRequests(string connection, string txtSearch, string dropDown,string branch);

    List<InternalTransferRequest> ListInternalRequests1(string connection, string txtSearch, string dropDown,string branch);
    InternalTransferRequest GetInternalTransferRequest(string connection, int RequestID);
    void UpdateInternalRequest(string connection, InternalTransferRequest request);
    void DeleteInternalRequest(string connection, int RequestID);
    bool CheckIftheItemHasStock(string connection, string ItemCode, string BranchCode, decimal Qty);
    DataSet GetRequestedBranchSupplierID(string connection, string BranchCode, int supid);
    DataSet GeBranchHasStockCustomerID(string connection, string BranchCode,int cusid);
    DataSet GeBranchHasStockExecutives(string connection, string BranchCode);
    int GetCustomerIDForBranchCode(string connection, string BranchCode);
    int GetSupplierIDForBranchCode(string connection, string BranchCode);
}