using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
//using NLog;
using DataAccessLayer;

using TroyLite.Inventory.Entities;

namespace TroyLite.Inventory.Data
{

    /// <summary>
    /// Summary description for Timesheets
    /// </summary>
    ///


    #region Interface Methods for Mocking....
    public interface ISearchWeeklyTimesheetEfforts
    {
        List<SearchTimeSheetEntity> SearchTimeSheetEfforts(int iEmpNo, string sCriteria, string sCriteriaValue, string sType);
        List<SearchTimeSheetEntity> SearchTimeSheetApproval(int iEmpNo, string sCriteria, string sCriteriaValue, string sType);
    }

    public interface ISaveTimesheets
    {
        int SaveWeeklyTimesheetEfforts(List<WeeklyTimeSheetEntity> clsWTSE,
                                              List<TimesheetEntity> clsTE,
                                              List<TimesheetEffortDescriptionEntity> clsEDE,
                                              out string exMessage);

        int DeleteWeeklyTimesheet(WeeklyTimeSheetEntity WTSE, out string exMessage);

    }

    public interface IWeeklyTimesheet
    {
        int SaveWeeklyTimesheet(List<WeeklyTimeSheetEntity> lstTSE, out string exMessage);

        int InsertWeeklyTimesheet(WeeklyTimeSheetEntity clsWTSE, out string exMessage);

        int UpdateWeeklyTimesheet(WeeklyTimeSheetEntity clsWTSE, out string exMessage);

        int UpdateApprovedStatusOfTimesheet(WeeklyTimeSheetEntity clsTS, string strStatus, out string exMessage);

        int DeleteWeeklyTimesheet(int iEmpno, string WeekID, out string exMessage);


        bool IsWeeklyTimeSheetExist(int EmpNumber, string WeekID, out string exMessage);
    }

    public interface IGetTimeSheets
    {
        DataSet GetTSEWeekDataByEmpno(int EmpNumber, string sBeginingDate, string sEndDate, string WeekID, out DateTime dtSelectedDate);

        DataSet GetTSWeekNotEnteredByEmp(int EmpNumber, DateTime dtBeginingDate);

        string GetTSWeekNotEnteredByEmp(int EmpNumber, string sCurrentWeekID, DateTime dtBeginingDate);

        DateTime GetSelectedDate(int iEmpNumber, string sFromToDateDesc, out string sWeekID);

        DataSet GetEmployeeDetail(int iEmployeeNumber);
    }

    public interface ITimesheet
    {
        int InsertTimesheet(TimesheetEntity clsTE, out string exMessage);
        int UpdateTimesheet(TimesheetEntity clsTE, out string exMessage);
        int SaveTimesheet(List<TimesheetEntity> lstTE, out string exMessage);
        int DeleteTimesheet(int iEmpno, string WeekID, out string exMessage);
        bool IsTimeSheetExist(int EmpNumber, string WeekID, string TSDate, out string exMessage);
    }

    public interface ITimesheetEffortDescription
    {
        int InsertTimesheetEffortDescription(TimesheetEffortDescriptionEntity clsTED, out string exMessage);
        int UpdateTimesheetEffortDescription(TimesheetEffortDescriptionEntity clsTED, out string exMessage);
        int SaveTimesheetEffortDescription(List<TimesheetEffortDescriptionEntity> lstTED, out string exMessage);
        int DeleteTimesheetEffortDescription(int iEmpno, string WeekID, out string exMessage);
        bool IsTimesheetEffortExist(int EmployeeNumber, string WeekID, string sWorkdate, int iRangeID, out string exMessage);
    }

    #endregion

    #region Inteface Implementations

    public class SearchWeeklyTimesheetEfforts : BusinessLogic, ISearchWeeklyTimesheetEfforts
    {
        public SearchWeeklyTimesheetEfforts(string sConnectionString)
        {
            this.ConnectionString = sConnectionString;
        }

        public List<SearchTimeSheetEntity> SearchTimeSheetEfforts(int iEmpNo, string sCriteriaKey, string sCriteriaValue, string sType)
        {
            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = CreateConnectionString(this.ConnectionString);

            DataSet ds = new DataSet();
            List<SearchTimeSheetEntity> listSearchTSE = new List<SearchTimeSheetEntity>();
            StringBuilder dbQry = new StringBuilder();
            String searchStr = string.Empty;

            if (iEmpNo > 0)
            {
                searchStr = " Empno=" + iEmpNo;
            }


            if (sCriteriaKey != string.Empty && sType.ToLower() == "string")
            {
                if (searchStr == string.Empty)
                    searchStr = searchStr + " " + sCriteriaKey + " LIKE '" + sCriteriaValue + "%'";
                else
                    searchStr = searchStr + " AND " + sCriteriaKey + " LIKE '" + sCriteriaValue + "%'";
            }

            if (sCriteriaKey.ToString() != string.Empty && sType.ToLower() == "datetime")
            {
                if (searchStr == string.Empty)
                    searchStr = searchStr + " " + sCriteriaKey + " =#" + Convert.ToDateTime(sCriteriaValue).ToString("MM/dd/yyyy").Trim() + "#";
                else
                    searchStr = searchStr + " AND " + sCriteriaKey + " =#" + Convert.ToDateTime(sCriteriaValue).ToString("MM/dd/yyyy").Trim() + "#";
            }

            #region "Criteria Code for Bool Type"
            //if (sCriteriaKey.ToString() != string.Empty && sType.ToLower() == "bool")
            //{
            //    if (searchStr == string.Empty)
            //        searchStr = searchStr + " wte.IsApproved=false";
            //    else
            //    {
            //        if (sAprroved == "No")
            //            searchStr = searchStr + " AND wte.IsApproved=false";
            //        else
            //            searchStr = searchStr + " AND wte.IsApproved=true";
            //    }
            //}
            #endregion "Criteria Code for Bool Type"

            if (searchStr != string.Empty)
            {
                dbQry.Append("SELECT * FROM (SELECT Supervisor.ManagerID, DateSubmitted, FromToDateDesc as DateRange, Supervisor.Empno, Supervisor.EmployeeName, ");
                dbQry.Append("(Select empFirstname from tblEmployee where Empno = Supervisor.ManagerID) as Pendingwith, StatusWeekly, Rejectreason,");
                dbQry.Append("IIf(wte.IsApproved=True,'Approved', StatusWeekly) as Approved, Supervisor.UserGroups,wte.WeekID FROM tblWeeklyTimeSheet as wte ");
                dbQry.Append("INNER JOIN ((SELECT DISTINCT e.ManagerID, ts.Empno As Empno, e.empFirstname as EmployeeName, e.UserGroup as UserGroups FROM tblTimeSheet ts ");
                dbQry.Append("Inner join tblEmployee e on ts.Empno = e.Empno Where ts.Empno = " + iEmpNo.ToString() + ")  AS Supervisor) ON wte.EmployeeNumber = Supervisor.Empno) ");
                dbQry.AppendFormat(" Where {0} ", searchStr);
            }

            try
            {
                manager.Open();
                ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        SearchTimeSheetEntity obj = new SearchTimeSheetEntity();
                        obj.ManagerID = Convert.ToInt32(dr["ManagerID"]);
                        obj.Empno = Convert.ToInt32(dr["Empno"]);
                        obj.EmployeeName = Convert.ToString(dr["EmployeeName"]);
                        obj.DateSubmitted = Convert.ToDateTime(dr["DateSubmitted"]);
                        obj.DateRange = Convert.ToString(dr["DateRange"]);
                        obj.Approved = Convert.ToString(dr["Approved"]);
                        obj.Pendingwith = Convert.ToString(dr["Pendingwith"]);
                        obj.Rejectreason = Convert.ToString(dr["Rejectreason"]);
                        obj.StatusWeekly = Convert.ToString(dr["StatusWeekly"]);
                        obj.UserGroups = Convert.ToString(dr["UserGroups"]);
                        obj.WeekID = Convert.ToString(dr["WeekID"]);

                        listSearchTSE.Add(obj);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listSearchTSE;
        }

        public List<SearchTimeSheetEntity> SearchTimeSheetApproval(int iEmpNo, string sCriteriaKey, string sCriteriaValue, string sType)
        {
            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = CreateConnectionString(this.ConnectionString);

            DataSet ds = new DataSet();
            List<SearchTimeSheetEntity> listSearchTSE = new List<SearchTimeSheetEntity>();
            StringBuilder dbQry = new StringBuilder();
            String searchStr = string.Empty;

            if (iEmpNo > 0)
            {
                searchStr = " ManagerID=" + iEmpNo;
            }


            if (sCriteriaKey != string.Empty && sType.ToLower() == "string")
            {
                if (searchStr == string.Empty)
                    searchStr = searchStr + " " + sCriteriaKey + " LIKE '" + sCriteriaValue + "%'";
                else
                    searchStr = searchStr + " AND " + sCriteriaKey + " LIKE '" + sCriteriaValue + "%'";
            }

            if (sCriteriaKey.ToString() != string.Empty && sType.ToLower() == "datetime")
            {
                if (searchStr == string.Empty)
                    searchStr = searchStr + " " + sCriteriaKey + " =#" + Convert.ToDateTime(sCriteriaValue).ToString("MM/dd/yyyy").Trim() + "#";
                else
                    searchStr = searchStr + " AND " + sCriteriaKey + " =#" + Convert.ToDateTime(sCriteriaValue).ToString("MM/dd/yyyy").Trim() + "#";
            }

            #region "Criteria Code for Bool Type"
            //if (sCriteriaKey.ToString() != string.Empty && sType.ToLower() == "bool")
            //{
            //    if (searchStr == string.Empty)
            //        searchStr = searchStr + " wte.IsApproved=false";
            //    else
            //    {
            //        if (sAprroved == "No")
            //            searchStr = searchStr + " AND wte.IsApproved=false";
            //        else
            //            searchStr = searchStr + " AND wte.IsApproved=true";
            //    }
            //}
            #endregion "Criteria Code for Bool Type"

            if (searchStr != string.Empty)
            {
                dbQry.Append("SELECT * FROM (SELECT Subordinates.ManagerID, DateSubmitted, FromToDateDesc as DateRange, Subordinates.Empno, Subordinates.EmployeeName, ");
                dbQry.Append("(Select empFirstname from tblEmployee where Empno = Subordinates.ManagerID) as Pendingwith, StatusWeekly, Rejectreason, ");
                dbQry.Append("IIf(wte.IsApproved=True,'Approved', StatusWeekly) as Approved, Subordinates.UserGroups, wte.WeekID FROM tblWeeklyTimeSheet as wte ");
                dbQry.Append("INNER JOIN ((SELECT DISTINCT e.ManagerID, ts.Empno As Empno, e.empFirstname as EmployeeName, e.UserGroup as UserGroups FROM tblTimesheet ts ");
                dbQry.Append("Inner join tblEmployee e on ts.Empno = e.Empno)  AS Subordinates) ON wte.EmployeeNumber = Subordinates.Empno ) ");
                dbQry.AppendFormat(" where {0} ", searchStr);
            }

            try
            {
                manager.Open();
                ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        SearchTimeSheetEntity obj = new SearchTimeSheetEntity();
                        obj.ManagerID = Convert.ToInt32(dr["ManagerID"]);
                        obj.Empno = Convert.ToInt32(dr["Empno"]);
                        obj.EmployeeName = Convert.ToString(dr["EmployeeName"]);
                        obj.DateSubmitted = Convert.ToDateTime(dr["DateSubmitted"]);
                        obj.DateRange = Convert.ToString(dr["DateRange"]);
                        obj.Approved = Convert.ToString(dr["Approved"]);
                        obj.Pendingwith = Convert.ToString(dr["Pendingwith"]);
                        obj.Rejectreason = Convert.ToString(dr["Rejectreason"]);
                        obj.StatusWeekly = Convert.ToString(dr["StatusWeekly"]);
                        obj.UserGroups = Convert.ToString(dr["UserGroups"]);
                        obj.WeekID = Convert.ToString(dr["WeekID"]);

                        listSearchTSE.Add(obj);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listSearchTSE;
        }
    }

    public class SaveTimeSheets : ISaveTimesheets
    {
        //NLog.Logger ObjNLog = LogManager.GetLogger("SaveTimeSheets");
        string ConnectionString;
        public SaveTimeSheets(string strCon)
        {
            this.ConnectionString = strCon;
        }
        public int SaveWeeklyTimesheetEfforts(List<WeeklyTimeSheetEntity> clsWTSE,
                                              List<TimesheetEntity> clsTE,
                                              List<TimesheetEffortDescriptionEntity> clsEDE,
                                              out string exMessage)
        {
            int iResult = 0;
            exMessage = null;



            try
            {

                #region WeeklyTimeSheet Manipulation
                WeeklyTimeSheet clsWTS = new WeeklyTimeSheet(this.ConnectionString);

                if (clsWTS.IsWeeklyTimeSheetExist(clsWTSE.First<WeeklyTimeSheetEntity>().EmployeeNumber, clsWTSE.First<WeeklyTimeSheetEntity>().WeekID, out exMessage))
                {
                    iResult = clsWTS.UpdateWeeklyTimesheet(clsWTSE.First<WeeklyTimeSheetEntity>(), out exMessage);
                    //ObjNLog.Info("IsWeeklyTimeSheetExist validated true = Updated weekly time sheet");
                }
                else
                {
                    iResult = clsWTS.InsertWeeklyTimesheet(clsWTSE.First<WeeklyTimeSheetEntity>(), out exMessage);
                    //ObjNLog.Info("IsWeeklyTimeSheetExist validated false = Inserted weekly time sheet");
                }

                #endregion WeeklyTimeSheet Manipulation

                #region Timesheet Manipulation

                if (iResult > 0)
                {
                    iResult = 0;
                    Timesheet clsTimeSheet = new Timesheet(this.ConnectionString);

                    foreach (TimesheetEntity timeSheet in clsTE)
                    {
                        if (clsTimeSheet.IsTimeSheetExist(timeSheet.Empno, timeSheet.WeekID, timeSheet.TSDate.ToString(), out exMessage))
                        {
                            iResult = clsTimeSheet.UpdateTimesheet(timeSheet, out exMessage);
                            //ObjNLog.Info("IsTimeSheetExist validated true = Updated time sheet: Work date: " + timeSheet.TSDate.ToShortDateString());
                        }

                        else
                        {
                            iResult = clsTimeSheet.InsertTimesheet(timeSheet, out exMessage);
                            //ObjNLog.Info("IsTimeSheetExist validated false = Inserted time sheet: Work date: " + timeSheet.TSDate.ToShortDateString());
                        }
                        if (iResult <= 0) break;
                    }
                }

                #endregion Timesheet Manipulation

                #region TimesheetEffort Manipulation
                int x = 0;
                if (iResult > 0)
                {
                    TimesheetEffortDescription clsTimeSheetEffort = new TimesheetEffortDescription(this.ConnectionString);

                    foreach (TimesheetEffortDescriptionEntity timeSheetEffort in clsEDE)
                    {
                        if (clsTimeSheetEffort.IsTimesheetEffortExist(timeSheetEffort.Empno,
                                                                      timeSheetEffort.WeekID,
                                                                      timeSheetEffort.TSDate.ToString(),
                                                                      timeSheetEffort.RangeID, out exMessage))
                        {
                            iResult = clsTimeSheetEffort.UpdateTimesheetEffortDescription(timeSheetEffort, out exMessage);
                            //ObjNLog.Info(string.Format("IsTimesheetEffortExist true = Updated effort desc for WeekID {0} Work date: {1}", timeSheetEffort.WeekID, timeSheetEffort.TSDate.ToShortDateString()));

                            //ObjNLog.Info("Range Description: " + timeSheetEffort.RangeDescription);
                        }
                        else
                        {
                            iResult = clsTimeSheetEffort.InsertTimesheetEffortDescription(timeSheetEffort, out exMessage);
                            //ObjNLog.Info(string.Format("IsTimesheetEffortExist true = Inserted effort desc for WeekID {0} Work date: {1}", timeSheetEffort.WeekID, timeSheetEffort.TSDate.ToShortDateString()));

                            //ObjNLog.Info("Range Description: " + timeSheetEffort.RangeDescription);
                        }

                        x++;
                        if (iResult <= 0) break;
                    }
                }

                #endregion TimesheetEffort Manipulation

            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }

            return iResult;
        }

        public int DeleteWeeklyTimesheet(WeeklyTimeSheetEntity WTSE, out string exMessage)
        {
            int iResult = 0;
            exMessage = null;

            try
            {

                #region WeeklyTimeSheet Manipulation
                WeeklyTimeSheet clsWTS = new WeeklyTimeSheet(this.ConnectionString);

                iResult = clsWTS.DeleteWeeklyTimesheet(WTSE.EmployeeNumber, WTSE.WeekID, out exMessage);

                #endregion WeeklyTimeSheet Manipulation

                #region Timesheet Manipulation

                if (iResult > 0)
                {
                    Timesheet clsTimeSheet = new Timesheet(this.ConnectionString);

                    iResult = clsTimeSheet.DeleteTimesheet(WTSE.EmployeeNumber, WTSE.WeekID, out exMessage);
                }

                #endregion Timesheet Manipulation

                #region TimesheetEffort Manipulation
                if (iResult > 0)
                {
                    TimesheetEffortDescription clsTimeSheetEffort = new TimesheetEffortDescription(this.ConnectionString);

                    iResult = clsTimeSheetEffort.DeleteTimesheetEffortDescription(WTSE.EmployeeNumber, WTSE.WeekID, out exMessage);

                }

                #endregion TimesheetEffort Manipulation
            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }

            return iResult;
        }

    }

    public class WeeklyTimeSheet : BusinessLogic, IWeeklyTimesheet
    {
        //NLog.Logger ObjNLog = LogManager.GetLogger("WeeklyTimeSheet");
        public WeeklyTimeSheet(string strCon)
        {
            this.ConnectionString = strCon;
        }

        public bool IsWeeklyTimeSheetExist(int EmpNumber, string WeekID, out string exMessage)
        {
            bool bExists = false;

            exMessage = null;
            string sTimesheetSelectCommand = string.Empty;
            IDataReader iDataReader;
            sTimesheetSelectCommand = "SELECT * FROM tblWeeklyTimesheet Where EmployeeNumber = " + EmpNumber + " and WeekID ='" + WeekID + "'";

            using (DBManager manager = new DBManager(DataProvider.SqlServer))
            {
                manager.ConnectionString = CreateConnectionString(this.ConnectionString);

                try
                {
                    manager.Open();
                    DataSet ds = new DataSet();
                    ds = manager.ExecuteDataSet(CommandType.Text, sTimesheetSelectCommand);

                    if (ds != null)
                        bExists = (ds.Tables[0].Rows.Count > 0);


                }
                catch (SqlException ex)
                {
                    exMessage = "SELECTING WeeklyTimesheets:" + "\n" +
                               "    Error: " + ex.Message + "\n" +
                               "    SQL  : " + sTimesheetSelectCommand;

                }
                catch (Exception ex)
                {
                    //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                    throw ex;
                }
            }

            return bExists;
        }

        public int InsertWeeklyTimesheet(WeeklyTimeSheetEntity clsTS, out string exMessage)
        {
            int nNoAdded = 0;
            exMessage = null;


            string sTimesheetInsertCommand = string.Empty;

            sTimesheetInsertCommand = "INSERT INTO tblWeeklyTimesheet (EmployeeNumber, WeekID, FromToDateDesc, BeginingweekDate, EndweekDate, DateSubmitted, UsersGroup, StatusWeekly, Rejectreason, IsApproved, SelectedDate)  " +
             "VALUES( " + clsTS.EmployeeNumber + ", '" + clsTS.WeekID + "', '" + clsTS.Fromdatetodesc + "', Format('" + clsTS.BeginingweekDate + "', 'dd/mm/yyyy'), Format('" + clsTS.EndweekDate + "','dd/mm/yyyy'), Format('" + clsTS.DateSubmitted + "','dd/mm/yyyy'), null, '" + clsTS.StatusWeekly + "' , '" + clsTS.Rejectreason + "', " + clsTS.IsApproved + ", Format('" + clsTS.SelectedDate + "','dd/mm/yyyy'));";

            using (DBManager manager = new DBManager(DataProvider.SqlServer))
            {
                manager.ConnectionString = CreateConnectionString(this.ConnectionString);

                try
                {

                    manager.Open();


                    nNoAdded = manager.ExecuteNonQuery(CommandType.Text, sTimesheetInsertCommand);
                }
                catch (SqlException ex)
                {
                    exMessage = "INSERTING WeeklyTimesheets:" + "\n" +
                                "    Error: " + ex.Message + "\n" +
                                "    SQL  : " + sTimesheetInsertCommand;

                    nNoAdded = -1;
                }
                catch (Exception ex)
                {
                    //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                    throw ex;
                }

            }
            return nNoAdded;
        }

        public int UpdateWeeklyTimesheet(WeeklyTimeSheetEntity clsTS, out string exMessage)
        {
            int nUpdated = 0;
            string sTimesheetUpdateCommand = string.Empty;
            exMessage = null;

            //
            sTimesheetUpdateCommand = "UPDATE tblWeeklyTimesheet SET DateSubmitted = Format('" + clsTS.DateSubmitted + "','dd/mm/yyyy'), StatusWeekly='" + clsTS.StatusWeekly + "', SelectedDate = Format('" + clsTS.SelectedDate + "','dd/mm/yyyy'), FromToDateDesc = '" + clsTS.Fromdatetodesc + "'"
             + " WHERE EmployeeNumber = " + clsTS.EmployeeNumber + " and WeekID= '" + clsTS.WeekID + "'";

            //sTimesheetUpdateCommand = "UPDATE tblWeeklyTimesheet SET StatusWeekly = '" + clsTS.StatusWeekly + "', IsApproved = " + clsTS.IsApproved + " " +
            //" WHERE EmployeeNumber = " + clsTS.EmployeeNumber + " and WeekID= '" + clsTS.WeekID + "'";

            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = CreateConnectionString(this.ConnectionString);

            try
            {

                manager.Open();


                nUpdated = manager.ExecuteNonQuery(CommandType.Text, sTimesheetUpdateCommand);
            }
            //try
            //{
            //    OleDbCommand cmdAdder = new OleDbCommand(sTimesheetUpdateCommand);
            //    cmdAdder.Connection = new OleDbConnection(this.ConnectionString);
            //    cmdAdder.Connection.Open();
            //    nUpdated = cmdAdder.ExecuteNonQuery();

        //    cmdAdder.Connection.BeginTransaction().Commit();
            //}
            catch (Exception ex)
            {
                exMessage = "UPDATING WeeklyTimesheets:" + "\n" +
                            "    Error: " + ex.Message + "\n" +
                            "    SQL  : " + sTimesheetUpdateCommand;

                nUpdated = -1;
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }

            return nUpdated;
        }

        public int UpdateApprovedStatusOfTimesheet(WeeklyTimeSheetEntity clsTS, string strStatus, out string exMessage)
        {
            int nUpdated = 0;
            string sTimesheetUpdateCommand = string.Empty;
            exMessage = null;
            string sStatusWeekly = string.Empty;


            if (strStatus == "Rejected")
            {
                sTimesheetUpdateCommand = "UPDATE tblWeeklyTimesheet SET StatusWeekly = '" + strStatus + "', IsApproved = " + clsTS.IsApproved + ", Rejectreason ='" + clsTS.Rejectreason + "' " +
                " WHERE EmployeeNumber = " + clsTS.EmployeeNumber + " and WeekID = '" + clsTS.WeekID + "'";
            }
            else
            {
                sTimesheetUpdateCommand = "UPDATE tblWeeklyTimesheet SET StatusWeekly = 'Submitted', IsApproved = " + clsTS.IsApproved + ", Rejectreason ='' " +
                " WHERE EmployeeNumber = " + clsTS.EmployeeNumber + " and WeekID = '" + clsTS.WeekID + "'";
            }

            bool isApproved = GetSelectedStatus(clsTS.EmployeeNumber, clsTS.WeekID, out sStatusWeekly);

            if (isApproved == false && sStatusWeekly == "Submitted")
            {
                DBManager manager = new DBManager(DataProvider.SqlServer);
                manager.ConnectionString = CreateConnectionString(this.ConnectionString);

                try
                {
                    manager.Open();

                    nUpdated = manager.ExecuteNonQuery(CommandType.Text, sTimesheetUpdateCommand);
                }
                catch (Exception ex)
                {
                    exMessage = "UPDATING WeeklyTimesheets:" + "\n" +
                                "    Error: " + ex.Message + "\n" +
                                "    SQL  : " + sTimesheetUpdateCommand;

                    nUpdated = -1;
                    //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                    throw ex;

                }
            }
            else if (isApproved == false && sStatusWeekly == "Saved")
            {
                exMessage = "Approval of Selected Employee needs submitted status!";
            }
            else if (isApproved == true)
            {
                exMessage = "Selected Employee already in approved status!";
            }
            else
            {
                if (strStatus != "Rejected")
                    exMessage = "Approval of Selected Employee needs submitted status!";
            }

            return nUpdated;
        }

        public int DeleteWeeklyTimesheet(int iEmpno, string WeekID, out string exMessage)
        {
            int nDeleted = 0;
            exMessage = string.Empty;

            String sCommand = "DELETE FROM tblWeeklyTimesheet " +
            "WHERE EmployeeNumber = " + iEmpno + " and WeekID = '" + WeekID + "'";

            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = CreateConnectionString(this.ConnectionString);

            try
            {
                manager.Open();

                nDeleted = manager.ExecuteNonQuery(CommandType.Text, sCommand);
            }
            catch (Exception ex)
            {
                exMessage = "Deleting WeeklyTimesheets:" + "\n" +
                            "    Error: " + ex.Message + "\n" +
                            "    SQL  : " + sCommand;

                nDeleted = -1;
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }

            return nDeleted;
        }

        public int SaveWeeklyTimesheet(List<WeeklyTimeSheetEntity> clsTS, out string exMessage)
        {
            exMessage = string.Empty;
            return 0;
        }

        public bool GetSelectedStatus(int iEmpNumber, string sWeekID, out string sStatusWeekly)
        {
            sStatusWeekly = string.Empty;
            bool isApproved = false;

            string sDataSource = ConfigurationManager.ConnectionStrings["DEMO"].ToString();

            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = CreateConnectionString(sDataSource);

            DataSet dsWeeklyData = null;

            try
            {
                manager.Open();

                string strSelectDateQuery = "Select StatusWeekly, IsApproved from tblWeeklyTimeSheet Where EmployeeNumber = " + iEmpNumber + " and WeekID = '" + sWeekID + "'";
                dsWeeklyData = manager.ExecuteDataSet(CommandType.Text, strSelectDateQuery);

                if (dsWeeklyData != null && dsWeeklyData.Tables.Count > 0 && dsWeeklyData.Tables[0].Rows.Count > 0)
                {
                    sStatusWeekly = Convert.ToString(dsWeeklyData.Tables[0].Rows[0][0]);
                    isApproved = (bool)dsWeeklyData.Tables[0].Rows[0][1];
                }
            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }
            finally
            {
                manager.Close();
            }

            return isApproved;
        }

    }



    public class Timesheet : BusinessLogic, ITimesheet, IGetTimeSheets
    {
        //NLog.Logger ObjNLog = LogManager.GetLogger("Timesheet");
        public Timesheet(string strCon)
        {
            this.ConnectionString = strCon;
        }

        #region Timesheet Methods
        public int InsertTimesheet(TimesheetEntity clsTS, out string exMessage)
        {
            int nNoAdded = 0;
            exMessage = null;
            string sTimesheetInsertCommand = string.Empty;

            DateTime dtTSDate = clsTS.TSDate;

            sTimesheetInsertCommand = "INSERT INTO tblTimesheet (Empno, TSDate, Hours, Rate, Overtime, Ratefactor, Approve, WeekId)  " +
             "VALUES( " + clsTS.Empno + ", Format('" + clsTS.TSDate.ToString() + "', 'dd/mm/yyyy'), null, null, null, null, " + clsTS.Approve + ", '" + clsTS.WeekID + "');";

            using (DBManager manager = new DBManager(DataProvider.SqlServer))
            {
                manager.ConnectionString = CreateConnectionString(this.ConnectionString);

                try
                {

                    manager.Open();


                    nNoAdded = manager.ExecuteNonQuery(CommandType.Text, sTimesheetInsertCommand);
                }
                catch (SqlException ex)
                {
                    exMessage = "INSERTING Timesheets:" + "\n" +
                                "    Error: " + ex.Message + "\n" +
                                "    SQL  : " + sTimesheetInsertCommand;

                    nNoAdded = -1;
                }
                catch (Exception ex)
                {
                    //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                    throw ex;
                }
            }
            //ObjNLog.Info("GetSelectedStatus method end");
            return nNoAdded;
        }

        public int UpdateTimesheet(TimesheetEntity clsTS, out string exMessage)
        {
            int nUpdated = 0;
            string sTimesheetUpdateCommand = string.Empty;
            exMessage = null;

            sTimesheetUpdateCommand = "UPDATE tblTimesheet SET Approve = " + clsTS.Approve + " " +
            " WHERE Empno = " + clsTS.Empno + " and TSDate = Format('" + clsTS.TSDate.ToString() + "', 'dd/mm/yyyy')";

            using (DBManager manager = new DBManager(DataProvider.SqlServer))
            {
                manager.ConnectionString = CreateConnectionString(this.ConnectionString);

                try
                {
                    manager.Open();

                    nUpdated = manager.ExecuteNonQuery(CommandType.Text, sTimesheetUpdateCommand);
                }

                //try
                //{
                //    OleDbCommand cmdAdder = new OleDbCommand(sTimesheetUpdateCommand);
                //    cmdAdder.Connection = new OleDbConnection(this.ConnectionString);
                //    cmdAdder.Connection.Open();
                //     = cmdAdder.ExecuteNonQuery();

            //    cmdAdder.Connection.BeginTransaction().Commit();
                //}
                catch (Exception ex)
                {
                    exMessage = "UPDATING Timesheets:" + "\n" +
                                "    Error: " + ex.Message + "\n" +
                                "    SQL  : " + sTimesheetUpdateCommand;

                    nUpdated = -1;

                    //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                    throw ex;

                }
            }

            return nUpdated;
        }

        public int DeleteTimesheet(int iEmpno, string WeekID, out string exMessage)
        {
            int nDeleted = 0;
            exMessage = string.Empty;

            String sCommand = "DELETE FROM tblTimesheet " +
            "WHERE Empno = " + iEmpno + " and WeekID = '" + WeekID + "'";

            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = CreateConnectionString(this.ConnectionString);

            try
            {
                manager.Open();

                nDeleted = manager.ExecuteNonQuery(CommandType.Text, sCommand);
            }
            catch (Exception ex)
            {
                exMessage = "Deleting Timesheets:" + "\n" +
                            "    Error: " + ex.Message + "\n" +
                            "    SQL  : " + sCommand;

                nDeleted = -1;
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }

            return nDeleted;
        }

        public int SaveTimesheet(List<TimesheetEntity> clsTS, out string exMessage)
        {
            exMessage = string.Empty;
            return 0;
        }

        public bool IsTimeSheetExist(int EmpNumber, string WeekID, string TSDate, out string exMessage)
        {
            bool bExists = false;
            exMessage = null;
            string sTimesheetSelectCommand = string.Empty;
            IDataReader iDataReader;

            sTimesheetSelectCommand = "SELECT * FROM tblTimesheet Where Empno = " + EmpNumber + " and WeekID ='" + WeekID + "' and TSDate = Format('" + TSDate + "', 'dd/mm/yyyy')";

            using (DBManager manager = new DBManager(DataProvider.SqlServer))
            {
                manager.ConnectionString = CreateConnectionString(this.ConnectionString);

                try
                {
                    manager.Open();

                    DataSet ds = new DataSet();
                    ds = manager.ExecuteDataSet(CommandType.Text, sTimesheetSelectCommand);

                    bExists = (ds.Tables[0].Rows.Count > 0);

                }
                catch (SqlException ex)
                {
                    exMessage = "SELECTING Timesheets:" + "\n" +
                               "    Error: " + ex.Message + "\n" +
                               "    SQL  : " + sTimesheetSelectCommand;
                    //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                    throw ex;
                }
            }

            return bExists;
        }

        #endregion

        #region Get Methods

        public DataSet GetTSEWeekDataByEmpno(int EmpNumber, string sBeginingDate, string sEndDate, string WeekID, out DateTime dtSelectedDate)
        {
            DataSet ds = new DataSet();
            //manager.ConnectionString = CreateConnectionString(sDataSource);

            try
            {
                string sDataSource = ConfigurationManager.ConnectionStrings["DEMO"].ToString();
                DBManager manager = new DBManager(DataProvider.SqlServer);
                manager.ConnectionString = CreateConnectionString(sDataSource);



                StringBuilder dbQry = new StringBuilder();
                String searchStr = string.Empty;

                if (EmpNumber > 0)
                    searchStr = searchStr + " EmpNo=" + EmpNumber;

                if ((sBeginingDate.ToString() != string.Empty && sBeginingDate.ToString() != null) && (sEndDate.ToString() != string.Empty && sEndDate.ToString() != null))
                {
                    if (searchStr == string.Empty)
                        searchStr = searchStr + " Cdate(Format(tblTimeSheetEffortDescription.TSDate, 'MM/dd/yyyy'))>=#" + Convert.ToDateTime(sBeginingDate).ToString("MM/dd/yyyy").Trim() + "# AND Cdate(Format(tblTimeSheetEffortDescription.TSDate, 'MM/dd/yyyy')) <=#" + Convert.ToDateTime(sEndDate).ToString("MM/dd/yyyy").Trim() + "#";
                    else
                        searchStr = searchStr + " and Cdate(Format(tblTimeSheetEffortDescription.TSDate, 'MM/dd/yyyy'))>=#" + Convert.ToDateTime(sBeginingDate).ToString("MM/dd/yyyy").Trim() + "# AND Cdate(Format(tblTimeSheetEffortDescription.TSDate, 'MM/dd/yyyy')) <=#" + Convert.ToDateTime(sEndDate).ToString("MM/dd/yyyy").Trim() + "#";
                }

                //if (sApproved != string.Empty)
                //  dbQry.Append(" AND Approved=" + sApproved);

                if (searchStr != string.Empty)
                {
                    dbQry.Append("TRANSFORM first (tblTimeSheetEffortDescription.RangeDescription) AS Expr1 ");

                    //   dbQry.Append("SELECT  lblRangeLookup.Range as Range, lblRangeLookup.RangeID, tblTimeSheetEffortDescription.Empno ");
                    dbQry.Append("SELECT  lblRangeLookup.Range as Range ");
                    dbQry.Append("FROM tblTimeSheetEffortDescription Inner Join lblRangeLookup on tblTimeSheetEffortDescription.RangeID = lblRangeLookup.RangeID ");
                    dbQry.Append(" Where " + searchStr);
                    dbQry.Append(" GROUP BY lblRangeLookup.RangeID, lblRangeLookup.Range ");
                    dbQry.Append(" Order By lblRangeLookup.RangeID ");
                    dbQry.Append(" PIVOT Format(tblTimeSheetEffortDescription.TSDate, 'MM/dd/yyyy');");
                }


                manager.Open();

                string strSelectDateQuery = "Select SelectedDate from tblWeeklyTimeSheet Where EmployeeNumber = " + EmpNumber + " and WeekId = '" + WeekID + "'";
                ds = manager.ExecuteDataSet(CommandType.Text, strSelectDateQuery);

                dtSelectedDate = DateTime.Now;
                if (ds != null)
                    if (ds.Tables.Count > 0)
                        if (ds.Tables[0].Rows.Count > 0)
                            dtSelectedDate = (ds.Tables[0].Rows[0][0] != System.DBNull.Value) ? Convert.ToDateTime(ds.Tables[0].Rows[0][0]) : DateTime.Now;

                ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

                //if (ds != null)
                //{
                //    if (ds.Tables.Count > 0)
                //    {
                //        if (ds.Tables[0].Rows.Count <= 0)
                //        {
                //            string strSelectRangeQuery = "SELECT  lblRangeLookup.Range as Range FROM  lblRangeLookup";
                //            ds = manager.ExecuteDataSet(CommandType.Text, strSelectRangeQuery);
                //        }
                //    }
                //}

                //SelectedDate = dt;
                return ds;

                //if (ds != null)
                //{
                //    if (ds.Tables.Count > 0)
                //        if (ds.Tables[0].Rows.Count > 0)
                //            return ds;
                //}
                //else
                //    return null;
            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }

        }

        public DateTime GetSelectedDate(int iEmpNumber, string sFromToDateDesc, out string sWeekID)
        {

            DateTime dtSelectedDate = DateTime.Now;
            try
            {
                string sDataSource = ConfigurationManager.ConnectionStrings["DEMO"].ToString();

                DBManager manager = new DBManager(DataProvider.SqlServer);
                manager.ConnectionString = CreateConnectionString(sDataSource);

                sWeekID = string.Empty;

                DataSet dsWeeklyData = null;

                manager.Open();

                string strSelectDateQuery = "Select SelectedDate, WeekID from tblWeeklyTimeSheet Where EmployeeNumber = " + iEmpNumber + " and FromToDateDesc = '" + sFromToDateDesc + "'";
                dsWeeklyData = manager.ExecuteDataSet(CommandType.Text, strSelectDateQuery);

                if (dsWeeklyData != null && dsWeeklyData.Tables.Count > 0 && dsWeeklyData.Tables[0].Rows.Count > 0)
                {
                    dtSelectedDate = Convert.ToDateTime(dsWeeklyData.Tables[0].Rows[0][0]);
                    sWeekID = dsWeeklyData.Tables[0].Rows[0][1].ToString();
                }
            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }
            return dtSelectedDate;
        }

        public DataSet GetTSWeekNotEnteredByEmp(int EmpNumber, DateTime dtBeginingDate)
        {

            DataSet dsWeeks = new DataSet();
            try
            {
                string sDataSource = ConfigurationManager.ConnectionStrings["DEMO"].ToString();

                DBManager manager = new DBManager(DataProvider.SqlServer);
                manager.ConnectionString = CreateConnectionString(sDataSource);

                StringBuilder dbQry = new StringBuilder();

                manager.Open();

                dbQry.Append("SELECT WeekID, StatusWeekly FROM tblWeeklyTimeSheet WHERE EmployeeNumber = " + EmpNumber.ToString() + " and  BeginingweekDate <= #" + dtBeginingDate.ToShortDateString() + "# Order by BeginingweekDate Desc");

                dsWeeks = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

                return dsWeeks;
            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }
        }

        public string GetTSWeekNotEnteredByEmp(int EmpNumber, string sCurrentWeekID, DateTime dtBeginingDate)
        {

            try
            {
                string sDataSource = ConfigurationManager.ConnectionStrings["DEMO"].ToString();

                DBManager manager = new DBManager(DataProvider.SqlServer);
                manager.ConnectionString = CreateConnectionString(sDataSource);
                // string strMessage = string.Empty;
                string sPreviousWeek = string.Empty;
                string sStatusWeekly = string.Empty;

                DataSet dsWeeks = new DataSet();
                StringBuilder dbQry = new StringBuilder("");


                manager.Open();

                dbQry.Append("SELECT Top 2 WeekID, StatusWeekly FROM tblWeeklyTimeSheet WHERE EmployeeNumber = " + EmpNumber.ToString() + " and  BeginingweekDate <=  format('" + dtBeginingDate.ToShortDateString() + "', 'dd/MM/yyyy') Order by BeginingweekDate Desc");

                dsWeeks = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

                string sCurrentWeek = string.Empty;
                string sCurrentStatusWeekly = string.Empty;
                StringBuilder sbText = new StringBuilder();

                if (dsWeeks != null && dsWeeks.Tables.Count > 0 && dsWeeks.Tables[0].Rows.Count > 0)
                {
                    if (dsWeeks.Tables[0].Rows[0]["WeekID"].ToString().Trim() != sCurrentWeekID.Trim())
                    {
                        sCurrentWeek = sCurrentWeekID;
                        sCurrentStatusWeekly = "Submitted";

                        sPreviousWeek = dsWeeks.Tables[0].Rows[0]["WeekID"].ToString();
                        sStatusWeekly = dsWeeks.Tables[0].Rows[0]["StatusWEekly"].ToString();
                    }
                    else if (dsWeeks.Tables[0].Rows[0]["WeekID"].ToString().Trim() == sCurrentWeekID.Trim() && dsWeeks.Tables[0].Rows.Count==2)
                    {
                        sCurrentWeek = dsWeeks.Tables[0].Rows[0]["WeekID"].ToString();
                        sCurrentStatusWeekly = "Submitted";

                        sPreviousWeek = dsWeeks.Tables[0].Rows[1]["WeekID"].ToString();
                        sStatusWeekly = dsWeeks.Tables[0].Rows[1]["StatusWEekly"].ToString();
                    }

                    if (!(dsWeeks.Tables[0].Rows[0]["WeekID"].ToString().Trim() == sCurrentWeekID.Trim() && dsWeeks.Tables[0].Rows.Count == 1))
                    {
                        int iPrevious = Convert.ToInt16(sPreviousWeek.Split(':').First<string>());
                        int iCurrent = Convert.ToInt16(sCurrentWeek.Split(':').First<string>());

                        int iDiff = iCurrent - iPrevious;
                        //sbText.Append("TimeSheet Weeks from " + iPrevious + " to " + iCurrent + " not entered! and Week " + (iPrevious) + " is in Saved status or Rejected status");
                        if (iDiff == 1 && (sStatusWeekly == "Saved" || sStatusWeekly == "Rejected"))
                            sbText.Append((iPrevious) + " is in Saved status or Rejected status. Please change to Submitted for Approval");
                        else if (iDiff == 2)
                        {
                            sbText.Append("TimeSheet Weeks  for " + (iCurrent - 1) + " not entered! Still time sheet entry stored in a Saved status!");
                            if (sStatusWeekly == "Saved" || sStatusWeekly == "Rejected")
                            {
                                sbText.Append((iPrevious) + " is in Saved status or Rejected status.");
                                sbText.Append(" Please change to Submitted for Approval");
                            }
                        }
                        else if (iDiff > 2)
                        {
                            sbText.Append("TimeSheet Weeks from " + (iPrevious + 1) + " to " + (iCurrent - 1) + " are not entered! Still time sheet entry stored in a Saved status!");
                            if (sStatusWeekly == "Saved" || sStatusWeekly == "Rejected")
                            {
                                sbText.Append((iPrevious) + " is in Saved status or Rejected status.");
                                sbText.Append(" Please change to Submitted for Approval");
                            }
                        }
                    }
                }
                else
                {
                    sbText.Append("");
                }
                
                return sbText.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetEmployeeDetail(int iEmployeeNumber)
        {
            string sDataSource = ConfigurationManager.ConnectionStrings["DEMO"].ToString();

            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = CreateConnectionString(sDataSource);

            DataSet dsEmployeeDetails = null;

            try
            {
                manager.Open();

                string strSelectDateQuery = "Select * from tblEmployee Where Empno = " + iEmployeeNumber;

                dsEmployeeDetails = manager.ExecuteDataSet(CommandType.Text, strSelectDateQuery);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                manager.Close();
            }

            return dsEmployeeDetails;
        }

        #endregion
    }

    public class TimesheetEffortDescription : BusinessLogic, ITimesheetEffortDescription
    {
        public TimesheetEffortDescription(string strCon)
        {
            this.ConnectionString = strCon;
        }

        public bool IsTimesheetEffortExist(int EmployeeNumber, string WeekID, string sWorkdate, int iRangeID, out string exMessage)
        {
            bool bExists = false;
            exMessage = null;
            string sTimesheetSelectCommand = string.Empty;
            IDataReader iDataReader;

            sTimesheetSelectCommand = "SELECT * FROM tblTimeSheetEffortDescription Where Empno = " + EmployeeNumber + " and WeekID = '" + WeekID + "' and TSDate = Format('" + sWorkdate + "', 'dd/mm/yyyy') and RangeID = " + iRangeID;

            using (DBManager manager = new DBManager(DataProvider.SqlServer))
            {
                manager.ConnectionString = CreateConnectionString(this.ConnectionString);

                try
                {
                    manager.Open();

                    DataSet ds = new DataSet();
                    ds = manager.ExecuteDataSet(CommandType.Text, sTimesheetSelectCommand);

                    bExists = (ds.Tables[0].Rows.Count > 0);
                }
                catch (SqlException ex)
                {
                    exMessage = "SELECTING Effortdescription:" + "\n" +
                               "    Error: " + ex.Message + "\n" +
                               "    SQL  : " + sTimesheetSelectCommand;

                }
            }

            return bExists;
        }

        public int InsertTimesheetEffortDescription(TimesheetEffortDescriptionEntity clsTS, out string exMessage)
        {
            int nNoAdded = 0;
            exMessage = null;
            string sTimesheetInsertCommand = string.Empty;

            sTimesheetInsertCommand = "INSERT INTO tblTimeSheetEffortDescription (Empno, TSDate, RangeID, RangeDescription, WeekId)  " +
             "VALUES( " + clsTS.Empno + ", Format('" + clsTS.TSDate.ToString() + "', 'dd/mm/yyyy'), " + clsTS.RangeID + ", '" + clsTS.RangeDescription + "', '" + clsTS.WeekID + "')";



            using (DBManager manager = new DBManager(DataProvider.SqlServer))
            {
                manager.ConnectionString = CreateConnectionString(this.ConnectionString);

                try
                {
                    manager.Open();

                    nNoAdded = manager.ExecuteNonQuery(CommandType.Text, sTimesheetInsertCommand);

                }

                catch (SqlException ex)
                {
                    exMessage = "INSERTING Effortdescription:" + "\n" +
                                "    Error: " + ex.Message + "\n" +
                                "    SQL  : " + sTimesheetInsertCommand;

                    nNoAdded = -1;
                }
            }

            return nNoAdded;
        }

        public int UpdateTimesheetEffortDescription(TimesheetEffortDescriptionEntity clsTS, out string exMessage)
        {
            int nUpdated = 0;
            string sTimesheetUpdateCommand = string.Empty;
            exMessage = null;

            sTimesheetUpdateCommand = "UPDATE tblTimeSheetEffortDescription SET RangeDescription = '" + clsTS.RangeDescription + "' " +
            " WHERE Empno = " + clsTS.Empno + " and TSDate = Format('" + clsTS.TSDate.ToString() + "', 'dd/mm/yyyy') and RangeID = " + clsTS.RangeID;

            using (DBManager manager = new DBManager(DataProvider.SqlServer))
            {
                manager.ConnectionString = CreateConnectionString(this.ConnectionString);

                try
                {
                    manager.Open();

                    nUpdated = manager.ExecuteNonQuery(CommandType.Text, sTimesheetUpdateCommand);
                }
                //try
                //{
                //    OleDbCommand cmdAdder = new OleDbCommand(sTimesheetUpdateCommand);
                //    cmdAdder.Connection = new OleDbConnection(this.ConnectionString);
                //    cmdAdder.Connection.Open();
                //    nUpdated = cmdAdder.ExecuteNonQuery();

                //    cmdAdder.Connection.BeginTransaction().Commit();
                //}
                catch (Exception ex)
                {
                    exMessage = "UPDATING Effortdescription:" + "\n" +
                                "    Error: " + ex.Message + "\n" +
                                "    SQL  : " + sTimesheetUpdateCommand;

                    nUpdated = -1;
                }
            }
            return nUpdated;
        }

        public int DeleteTimesheetEffortDescription(int iEmpno, string WeekID, out string exMessage)
        {

            int nDeleted = 0;
            exMessage = string.Empty;

            String sCommand = "DELETE FROM tblTimeSheetEffortDescription " +
            "WHERE Empno = " + iEmpno + " and WeekID = '" + WeekID + "'";

            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = CreateConnectionString(this.ConnectionString);

            try
            {
                manager.Open();

                nDeleted = manager.ExecuteNonQuery(CommandType.Text, sCommand);
            }
            catch (Exception ex)
            {
                exMessage = "Deleting Effortdescription:" + "\n" +
                            "    Error: " + ex.Message + "\n" +
                            "    SQL  : " + sCommand;

                nDeleted = -1;
            }

            return nDeleted;
        }

        public int SaveTimesheetEffortDescription(List<TimesheetEffortDescriptionEntity> lstTSED, out string exMessage)
        {
            exMessage = string.Empty;
            return 0;
        }
    }

    public class timesheetentrymethods : BusinessLogic
    {
        #region "legacy format"

        public DataSet GetTSEWorkDateByEmpno(int EmpNumber, string sWorkDate, string sApproved)
        {
            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = CreateConnectionString(this.ConnectionString);

            DataSet ds = new DataSet();
            StringBuilder dbQry = new StringBuilder();
            String searchStr = string.Empty;

            if (EmpNumber > 0)
                searchStr = searchStr + " tblTimeSheetEffortDescription.EmpNo=" + EmpNumber;

            if (sWorkDate.ToString() != string.Empty)
            {
                if (searchStr == string.Empty)
                    searchStr = searchStr + " tblTimeSheetEffortDescription.TSDate=#" + Convert.ToDateTime(sWorkDate).ToString("MM/dd/yyyy").Trim() + "#";
                else
                    searchStr = searchStr + " and tblTimeSheetEffortDescription.TSDate=#" + Convert.ToDateTime(sWorkDate).ToString("MM/dd/yyyy").Trim() + "#";
            }

            if (sApproved != string.Empty)
            {
                if (searchStr == string.Empty)
                    searchStr = searchStr + " Approved=false";
                else
                {
                    if (sApproved == "No")
                        searchStr = searchStr + " AND Approved=false";
                    else
                        searchStr = searchStr + " AND Approved=true";
                }
            }


            if (searchStr != string.Empty)
            {
                dbQry.Append("TRANSFORM first (tblTimeSheetEffortDescription.RangeDescription) AS Expr1 ");

                dbQry.Append("SELECT  lblRangeLookup.Range, lblRangeLookup.RangeID, tblTimeSheetEffortDescription.Empno, tblTimeSheetEffortDescription.TSDate FROM tblTimeSheet ");
                dbQry.Append(" Inner Join (tblTimeSheetEffortDescription Inner Join lblRangeLookup on tblTimeSheetEffortDescription.RangeID = lblRangeLookup.RangeID) on tblTimeSheet.Empno = tblTimeSheetEffortDescription.Empno ");
                dbQry.Append(" Where " + searchStr);

                //dbQry.Append(" Where  tblEffortDescription.EmpNo=2007 and tblTimeSheetEffortDescription.TSDate=#09/22/2014# AND Approved=true ");
                dbQry.Append(" GROUP BY tblTimeSheetEffortDescription.Empno, lblRangeLookup.RangeID, lblRangeLookup.Range, tblTimeSheetEffortDescription.TSDate  ");
                dbQry.Append(" Order By lblRangeLookup.RangeID ");
                dbQry.Append(" PIVOT Format(tblTimeSheetEffortDescription.TSDate, 'dd/MM/yyyy');");
            }

            try
            {
                manager.Open();
                ds = manager.ExecuteDataSet(CommandType.Text, dbQry.ToString());

                if (ds.Tables[0].Rows.Count > 0)
                    return ds;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void InsertTSEDetails(int empno, string TSEDate, string Approved, string sWeekID)
        {
            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = CreateConnectionString(this.ConnectionString); // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
            DataSet ds = new DataSet();
            string dbQry = string.Empty;

            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            dbQry = string.Format("INSERT INTO tblTimesheet(TSDate, Empno, Approve, WeekID) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},{2}, '{3}')", TSEDate, empno, Approved, sWeekID);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.Dispose();
        }

        public void InsertTSEHoursDetails(int empno, string TSEDate, string sHoursDesc, string sWeekID)
        {
            DBManager manager = new DBManager(DataProvider.SqlServer);
            manager.ConnectionString = CreateConnectionString(this.ConnectionString); // System.Configuration.ConfigurationManager.ConnectionStrings["ACCSYS"].ToString();
            DataSet ds = new DataSet();
            string dbQry = string.Empty;

            manager.Open();
            manager.ProviderType = DataProvider.SqlServer;

            dbQry = string.Format("INSERT INTO tblTimeSheetEffortDescription(TSDate, Empno, RangeDescription, WeekID) VALUES(Format('{0}', 'dd/mm/yyyy'),{1},'{2}', '{3}')", TSEDate, empno, sHoursDesc);

            manager.ExecuteNonQuery(CommandType.Text, dbQry);

            manager.Dispose();
        }

        #endregion
    }
    #endregion
}

