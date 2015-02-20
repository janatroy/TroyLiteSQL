using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using TroyLite.Inventory.Data;
using TroyLite.Inventory.Entities;
//using NLog;

namespace TroyLite.Inventory.BusinessLayer
{
    ///
    /// Business Layer
    ///
    public class BusinessLayer : BusinessLogic
    {
        //NLog.Logger ObjNLog = LogManager.GetLogger("BusinessLayer");

        public BusinessLayer(string con)
        {
            this.ConnectionString = con;
        }

        public DataSet GetTSEWeekDataByEmpno(int EmpNumber, string sBeginingDate, string sEndDate, string WeekID, out DateTime dtSelectedDate)
        {
            try
            {
                DataSet dstweeklyTimeSheet = null;
                IGetTimeSheets iwTSheet = new Timesheet(this.ConnectionString);

                dstweeklyTimeSheet = iwTSheet.GetTSEWeekDataByEmpno(EmpNumber, sBeginingDate, sEndDate, WeekID, out dtSelectedDate);

                return dstweeklyTimeSheet;
            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }

       }

        public DataSet GetTSWeekNotEnteredByEmp(int EmpNumber, DateTime dtBeginingDate)
        {

            try
            {
                DataSet dsWeeks = new DataSet();

                IGetTimeSheets iwTSheet = new Timesheet(this.ConnectionString);

                dsWeeks = iwTSheet.GetTSWeekNotEnteredByEmp(EmpNumber, dtBeginingDate);

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
                string strMessage = "";

                IGetTimeSheets iwTSheet = new Timesheet(this.ConnectionString);

                strMessage = iwTSheet.GetTSWeekNotEnteredByEmp(EmpNumber, sCurrentWeekID, dtBeginingDate);

                return strMessage;
            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }

       }

        public DateTime GetSelectedDate(int iEmpNumber, string sFromToDateDesc, out string sWeekID)
        {
            try
            {
                DateTime dtSelectedDate;

                IGetTimeSheets iwTSheet = new Timesheet(this.ConnectionString);

                dtSelectedDate = iwTSheet.GetSelectedDate(iEmpNumber, sFromToDateDesc, out sWeekID);

                return dtSelectedDate;
            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }

       }


        public int SaveWeeklyTimesheetEfforts(List<WeeklyTimeSheetEntity> clsWTSE,
                                             List<TimesheetEntity> clsTE,
                                             List<TimesheetEffortDescriptionEntity> clsEDE,
                                             out string exMessage)
        {
            int iResult = 0;

            try
            {
                SaveTimeSheets clsSaveTimeSheets = new SaveTimeSheets(this.ConnectionString);

                iResult = clsSaveTimeSheets.SaveWeeklyTimesheetEfforts(clsWTSE, clsTE, clsEDE, out exMessage);
            }
            catch (Exception ex)
            {
                iResult = -1;
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }

            return iResult;
        }

        public int DeleteWeeklyTimesheet(WeeklyTimeSheetEntity WTSE, out string exMessage)
        {
            try
            {
                int iResult = 0;
                exMessage = null;

                SaveTimeSheets clsSaveTimeSheets = new SaveTimeSheets(this.ConnectionString);

                iResult = clsSaveTimeSheets.DeleteWeeklyTimesheet(WTSE, out exMessage);


                return iResult;
            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }
        }


        public int UpdateApprovedStatusOfTimesheet(WeeklyTimeSheetEntity clsTS, string strStatus, out string exMessage)
        {
            try
            {
                int iStatus;

                IWeeklyTimesheet iwTSheet = new WeeklyTimeSheet(this.ConnectionString);

                iStatus = iwTSheet.UpdateApprovedStatusOfTimesheet(clsTS, strStatus, out exMessage);

                return iStatus;
            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }
        }

        public DataSet GetEmployeeDetail(int iEmployeeNumber)
        {
            try
            {
                DataSet dstEmployeeDetails = null;

                IGetTimeSheets igTimeSheet = new Timesheet(this.ConnectionString);

                dstEmployeeDetails = igTimeSheet.GetEmployeeDetail(iEmployeeNumber);

                return dstEmployeeDetails;
            }
            catch (Exception ex)
            {
                //ObjNLog.Error(string.Format("Exception Raised {0}", ex.Message));
                throw ex;
            }
        }

        public List<SearchTimeSheetEntity> SearchTimeSheetEfforts(int iEmpNo, string sCriteriaKey, string sCriteriaValue, string sType)
        {
            List<SearchTimeSheetEntity> lstSearchTSE = new List<SearchTimeSheetEntity>();
            try
            {
                ISearchWeeklyTimesheetEfforts lstSearchWeeklyTSE = new SearchWeeklyTimesheetEfforts(this.ConnectionString);

                lstSearchTSE = lstSearchWeeklyTSE.SearchTimeSheetEfforts(iEmpNo, sCriteriaKey, sCriteriaValue, sType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstSearchTSE;
        }

        public List<SearchTimeSheetEntity> SearchTimeSheetApproval(int iEmpNo, string sCriteriaKey, string sCriteriaValue, string sType)
        {
            List<SearchTimeSheetEntity> lstSearchTSE = new List<SearchTimeSheetEntity>();
            try
            {
                ISearchWeeklyTimesheetEfforts lstSearchWeeklyTSE = new SearchWeeklyTimesheetEfforts(this.ConnectionString);

                lstSearchTSE = lstSearchWeeklyTSE.SearchTimeSheetApproval(iEmpNo, sCriteriaKey, sCriteriaValue, sType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstSearchTSE;
        }

        
    }
}