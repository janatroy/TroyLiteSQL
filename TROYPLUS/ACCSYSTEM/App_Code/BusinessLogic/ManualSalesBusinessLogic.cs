using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Collections;
using System.Data.OleDb;
using System.Data;
using System.Text;
using System.Configuration;
//using NLog;
using DataAccessLayer;

/// <summary>
/// Summary description for ManualSalesBusinessLogic
/// </summary>
public partial class BusinessLogic
{

    public DataSet ListManualSalesBookInfo(string connection, string txtSearch, string dropDown)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        txtSearch = "%" + txtSearch + "%";

        if (dropDown == "BookName" && txtSearch != null)
        {
            dbQry = "select BookId, BookFrom, BookTo, BookName, CreatedBy, CreatedDate From tblManualSalesBook Where BookName like '" + txtSearch + "' Order By BookId";
        }
        else
        {
            dbQry = string.Format("select BookId, BookFrom, BookTo, BookName, CreatedBy, CreatedDate From tblManualSalesBook Order By BookId");
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

    public void InsertManualSalesBook(string connection, int BookFrom, int BookTo, string BookName, string Username, string Types,string branchcode)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string dbQry2 = string.Empty;

        string sAuditStr = string.Empty;
        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            dbQry = string.Format("INSERT INTO tblManualSalesBook(BookFrom, BookTo, BookName, CreatedBy, CreatedDate,Branchcode) VALUES({0},{1},'{2}','{3}','{4}','{5}')",
                BookFrom, BookTo, BookName, Username, DateTime.Now.ToString("yyyy-MM-dd"), branchcode);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            int BookId = (Int32)manager.ExecuteScalar(CommandType.Text, "SELECT MAX(BookID) FROM tblManualSalesBook");

            if (Types == "New")
            {
                sAuditStr = "Manual Sales Book For : " + BookName + " added. Record Details :  User :" + Username;
                dbQry = string.Format("INSERT INTO  tblAudit(Description,Command,auditdate) VALUES('{0}','{1}','{2}')", sAuditStr, "Add New", DateTime.Now.ToString("yyyy-MM-dd"));
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

    public void UpdateManualSalesBook(string connection, int BookId, string BookName, int BookFrom, int BookTo, string Username, string Types,string branchcode)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;

        string dbQ = string.Empty;
        DataSet dsd = new DataSet();
        string logdescription = string.Empty;
        string description = string.Empty;
        string Logsave = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            //DeleteManualSaleBook(connection, BookId, Username, Types);

            //InsertManualSalesBook(connection, BookFrom, BookTo, BookName,  Username, Types);

            dbQry = string.Format("Update tblManualSalesBook SET BookName='{0}', BookFrom={1}, BookTo={2},CreatedBy='{3}', CreatedDate={4},BranchCode='{5}' WHERE BookId={6}",
                BookName, BookFrom, BookTo, Username, DateTime.Now.ToString("yyyy-MM-dd"),branchcode,BookId);
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

    public void DeleteManualSaleBook(string connection, int BookId, string Username, string Types)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string sAuditStr = string.Empty;
        string description = string.Empty;
        string logdescription = string.Empty;
        string Logsave = string.Empty;
        DataSet dsd = new DataSet();
        string dbQry2 = string.Empty;

        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;
            manager.BeginTransaction();
            List<int> billNos = new List<int>();

            //ds = manager.ExecuteDataSet(CommandType.Text, "SELECT BillNo FROM tblSales where Manualsales ='YES' ");

            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        billNos.Add(int.Parse(ds.Tables[0].Rows[0]["BillNo"].ToString()));
            //    }
            //}


            //dbQry = "select BookId, BookName, BookTo, BookFrom from tblManualSalesBook where BookId = " + BookId.ToString();
            //ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        StartRange = int.Parse( ds.Tables[0].Rows[0]["BookFrom"].ToString());
            //        EndRange = int.Parse( ds.Tables[0].Rows[0]["BookTo"].ToString());
            //    }
            //}

            //bool IsBookUsed = false;

            //foreach (int item in billNos)
            //{
            //    if (item >= StartRange && item <= EndRange)
            //    {
            //        IsBookUsed = true;
            //    }
            //}

            if (true)
            {
                dbQry = string.Format("Delete From tblManualSalesBook Where BookId = {0}", BookId);

                manager.ExecuteNonQuery(CommandType.Text, dbQry);
            }

            //dbQry = string.Format("Delete From tblChequeitems Where ChequeBookId = {0}", ChequeBookId);

            //manager.ExecuteNonQuery(CommandType.Text, dbQry);

            if (Types == "Delete")
            {
                sAuditStr = "Manual Sale Book For : " + BookId + " deleted. Record Details :  User :" + Username;
                dbQry = string.Format("INSERT INTO  tblAudit(Description,Command,auditdate) VALUES('{0}','{1}',{2})", sAuditStr, "Delete", DateTime.Now.ToShortDateString());
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

    public DataSet GetManualSaleBookInfoForId(string connection, int BookId)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        try
        {
            dbQry = "select BookId, BookName, BookTo, BookFrom from tblManualSalesBook where BookId = " + BookId.ToString();
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

    public bool IsManualSaleBookAlreadyEntered(string connection, string BookName, string FromNo, string ToNo,string branchcode)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        string dbQry = string.Empty;

        DataSet itemDs = new DataSet();
        DataSet ds = new DataSet();
        string fromTemp = string.Empty;
        string toTemp = string.Empty;

        try
        {

            dbQry = string.Format("Select BookFrom,BookTo, BookName from tblManualSalesBook Where UPPER(BookName)='{0}' and Branchcode='{1}'", BookName.ToUpper(),branchcode);

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds != null && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    fromTemp = dr["BookFrom"].ToString();
                    toTemp = dr["BookTo"].ToString();

                    if (int.Parse(FromNo) >= int.Parse(fromTemp) && int.Parse(FromNo) <= int.Parse(toTemp))
                    {
                        return true;
                    }

                    if (int.Parse(ToNo) <= int.Parse(toTemp) && int.Parse(ToNo) >= int.Parse(fromTemp))
                    {
                        return true;
                    }

                }
            }

            return false;

        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public bool IsDamangedLeafAlreadyEntered(string connection, int BookId, int DamagedLeaf)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        string dbQry = string.Empty;

        DataSet itemDs = new DataSet();
        DataSet ds = new DataSet();
        string fromTemp = string.Empty;
        string toTemp = string.Empty;

        try
        {

            dbQry = string.Format("Select count(*) from tblManualSalesBookLeaf Where BookId={0} And LeafNo={1}", BookId, DamagedLeaf);

            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds != null && ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        return false;
                    else
                        return true;
                }
                else
                    return false;
            }

            return false;

        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            manager.Dispose();
        }
    }

    public void InsertManualSalesLeaf(string connection, int BookId, int LeafNo, string Comments, string Username)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;
        string dbQry2 = string.Empty;

        string sAuditStr = string.Empty;
        try
        {
            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            manager.BeginTransaction();

            dbQry = string.Format("INSERT INTO tblManualSalesBookLeaf(BookId, LeafNo, Comments, CreatedBy, CreatedDate) VALUES({0},{1},'{2}','{3}',{4})",
                BookId, LeafNo, Comments, Username, DateTime.Now.ToShortDateString());

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

    public DataSet ListManualSalesBookInfo(string connection, int bookId)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet dsDamangedLeafs = new DataSet();
        DataSet dsUnusedLeafs = new DataSet();
        DataSet bookDetails = new DataSet();
        DataSet dsUsedLeafs = new DataSet();

        string dbQry = string.Empty;
        int bookStart = 0;
        int bookEnd = 0;
        List<int> damagedLeafs = new List<int>();
        string bookName = string.Empty;

        bookDetails = GetManualSaleBookInfoForId(connection, bookId);

        if (bookDetails == null)
            return null;

        bookStart = int.Parse(bookDetails.Tables[0].Rows[0]["BookFrom"].ToString());
        bookEnd = int.Parse(bookDetails.Tables[0].Rows[0]["BookTo"].ToString());
        bookName = bookDetails.Tables[0].Rows[0]["BookName"].ToString();

        DataTable dt = new DataTable();
        DataColumn dc;

        dc = new DataColumn("LeafNo");
        dt.Columns.Add(dc);

        dc = new DataColumn("BookName");
        dt.Columns.Add(dc);

        //dc = new DataColumn("Comments");
        //dt.Columns.Add(dc);

        dsUnusedLeafs.Tables.Add(dt);



        dbQry = string.Format("select LeafId, BookId, LeafNo, Comments From tblManualSalesBookLeaf Where BookId={0}", bookId);

        try
        {
            manager.Open();
            dsDamangedLeafs = manager.ExecuteDataSet(CommandType.Text, dbQry);

            //if (dsDamangedLeafs.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow item in dsDamangedLeafs.Tables[0].Rows)
            //    {
            //        DataRow dr = dsUnusedLeafs.Tables[0].NewRow();
            //        dr["LeafNo"] = item["LeafNo"].ToString();
            //        //dr["ManualSalesBillNo"] = "";
            //        dr["Comments"] = item["Comments"].ToString();

            //        dsUnusedLeafs.Tables[0].Rows.Add(dr);
            //    }

            //}



            for (int i = bookStart; i <= bookEnd; i++)
            {
                object billNo = manager.ExecuteScalar(CommandType.Text, "Select BillNo from tblSales Where ManualSales='YES' and BillNo=" + i.ToString());

                object damangedLeafNo = manager.ExecuteScalar(CommandType.Text, string.Format("select LeafNo From tblManualSalesBookLeaf Where BookId={0} And LeafNo={1}", bookId, i));

                if (billNo == null && damangedLeafNo == null)
                {
                    DataRow dr = dsUnusedLeafs.Tables[0].NewRow();
                    dr["LeafNo"] = i.ToString();
                    dr["BookName"] = bookName;
                    //dr["Comments"] = "";

                    dsUnusedLeafs.Tables[0].Rows.Add(dr);
                }
            }

            return dsUnusedLeafs;
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

    public bool IsManualSalesBillNoValid(string connection, string billNo, int bookId)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        manager.Open();

        string Branch = (string)manager.ExecuteScalar(CommandType.Text, "SELECT branchcode FROM tblManualSalesBook where bookid='"+ bookId +"'");

        try
        {
            object objBillNo = manager.ExecuteScalar(CommandType.Text, string.Format("Select ManualNo from tblSales Where ManualSales='YES' and ManualNo={0} and BookId={1}", billNo, bookId));
            object damangedLeafNo = manager.ExecuteScalar(CommandType.Text, string.Format("select LeafNo From tblManualSalesBookLeaf Where LeafNo={0} and LeafID={1}", billNo, bookId));
            object isBillNoWithInBookRange = manager.ExecuteScalar(CommandType.Text, string.Format("select BookId From tblManualSalesBook Where {0} >= BookFrom and {0} <= BookTo and BookId={1} and branchcode='{2}'", billNo, bookId,Branch));

            if (isBillNoWithInBookRange != null && objBillNo == null && damangedLeafNo == null)
            {
                return true;
            }
            else
                return false;
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

    public DataSet GetManualSalesBooks(string connection,string branch)
    {
        DBManager manager = new DBManager(DataProvider.SqlServer);
        manager.ConnectionString = CreateConnectionString(connection);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = "select BookId, BookFrom, BookTo, BookName, CreatedBy, CreatedDate,branchcode From tblManualSalesBook where branchcode='"+ branch +"' Order By BookName";

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

}