using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TroyLite.Inventory.Entities
{

    /// <summary>
    /// Summary description for TimesheetEntities
    /// </summary>
    /// 
    public class SearchTimeSheetEntity
    {
        int _ManagerID;
        DateTime _DateSubmitted;
        string _DateRange;
        int _Empno;
        string _EmployeeName;
        string _Pendingwith;
        string _StatusWeekly;
        string _Rejectreason;
        string _Approved;
        string _UserGroups;
        string _WeekID;

        public int ManagerID
        {
            get { return _ManagerID; }
            set { _ManagerID = value; }
        }
        public DateTime DateSubmitted
        {
            get { return _DateSubmitted; }
            set { _DateSubmitted = value; }
        }
        public string DateRange
        {
            get { return _DateRange; }
            set { _DateRange = value; }
        }
        public int Empno
        {
            get { return _Empno; }
            set { _Empno = value; }
        }
        public string EmployeeName
        {
            get { return _EmployeeName; }
            set { _EmployeeName = value; }
        }
        public string Pendingwith
        {
            get { return _Pendingwith; }
            set { _Pendingwith = value; }
        }
        public string StatusWeekly
        {
            get { return _StatusWeekly; }
            set { _StatusWeekly = value; }
        }
        public string Rejectreason
        {
            get { return _Rejectreason; }
            set { _Rejectreason = value; }
        }
        public string Approved
        {
            get { return _Approved; }
            set { _Approved = value; }
        }
        public string UserGroups
        {
            get { return _UserGroups; }
            set { _UserGroups = value; }
        }

        public string WeekID
        {
            get { return _WeekID; }
            set { _WeekID = value; }
        }
    }

    public class WeeklyTimeSheetEffortsEntity
    {
        public WeeklyTimeSheetEffortsEntity()
        {

        }

        private int _ManagerID;
        private DateTime _DateSubmitted;
        private string _DateRange;
        private int _EmployeeNumber;
        private string _Employees;
        private string _Pendingwith;
        private string _StatusWeekly;
        private string _Rejectreason;
        private bool _Approved;
        private string _UserGroups;


        public int ManagerID
        {
            set { _ManagerID = value; }
            get { return _ManagerID; }
        }

        public DateTime DateSubmitted
        {
            set { _DateSubmitted = value; }
            get { return _DateSubmitted; }
        }

        public string DateRange
        {
            set { _DateRange = value; }
            get { return _DateRange; }
        }

        public int EmployeeNumber
        {
            set { _EmployeeNumber = value; }
            get { return _EmployeeNumber; }
        }

        public string Employees
        {
            set { _Employees = value; }
            get { return _Employees; }
        }

        public string Pendingwith
        {
            set { _Pendingwith = value; }
            get { return _Pendingwith; }
        }

        public string StatusWeekly
        {
            set { _StatusWeekly = value; }
            get { return _StatusWeekly; }
        }

        public string Rejectreason
        {
            set { _Rejectreason = value; }
            get { return _Rejectreason; }
        }

        public bool Approved
        {
            set { _Approved = value; }
            get { return _Approved; }
        }

        public string UserGroups
        {
            set { _UserGroups = value; }
            get { return _UserGroups; }
        }
    }

    public class WeeklyTimeSheetEntity
    {
        private int _WeeklyTimeSheetID;
        private int _EmployeeNumber;
        private string _WeekID;
        private string _Fromdatetodesc;
        private DateTime _BeginingweekDate;
        private DateTime _EndweekDate;
        private DateTime _DateSubmitted;
        private string _UserGroup;
        private string _StatusWeekly;
        private string _Rejectreason;
        private bool _IsApproved;
        private DateTime _SelectedDate;

        public int WeeklyTimesheetID
        {
            set { _WeeklyTimeSheetID = value; }
            get { return _WeeklyTimeSheetID; }

        }
        public int EmployeeNumber
        {
            set { _EmployeeNumber = value; }
            get { return _EmployeeNumber; }
        }

        public string WeekID
        {
            set { _WeekID = value; }
            get { return _WeekID; }
        }

        public string Fromdatetodesc
        {
            set { _Fromdatetodesc = value; }
            get { return _Fromdatetodesc; }
        }
        public DateTime BeginingweekDate
        {
            set { _BeginingweekDate = value; }
            get { return _BeginingweekDate; }
        }
        public DateTime EndweekDate
        {
            set { _EndweekDate = value; }
            get { return _EndweekDate; }
        }

        public DateTime DateSubmitted
        {
            set { _DateSubmitted = value; }
            get { return _DateSubmitted; }
        }

        public string UserGroup
        {
            set { _UserGroup = value; }
            get { return _UserGroup; }
        }

        public string StatusWeekly
        {
            set { _StatusWeekly = value; }
            get { return _StatusWeekly; }
        }

        public string Rejectreason
        {
            set { _Rejectreason = value; }
            get { return _Rejectreason; }
        }

        public bool IsApproved
        {
            set { _IsApproved = value; }
            get { return _IsApproved; }
        }

        public DateTime SelectedDate
        {
            set { _SelectedDate = value; }
            get { return _SelectedDate; }
        }

    }

    public class TimesheetEntity
    {
        private int _TimeSheetID;
        private int _Empno;
        private DateTime _TSDate;
        private int _Hours;
        private decimal _Rate;
        private int _Overtime;
        private decimal _RateFactor;
        private string _WeekID;
        private bool _Approve;

        public TimesheetEntity()
        {

        }

        public int TimeSheetID
        {
            set { _TimeSheetID = value; }
            get { return _TimeSheetID; }
        }

        public int Empno
        {
            set { _Empno = value; }
            get { return _Empno; }
        }

        public DateTime TSDate
        {
            set { _TSDate = value; }
            get { return _TSDate; }
        }

        public int Hours
        {
            set { _Hours = value; }
            get { return _Hours; }
        }

        public decimal Rate
        {
            set { _Rate = value; }
            get { return _Rate; }
        }

        public int Overtime
        {
            set { _Overtime = value; }
            get { return _Overtime; }
        }

        public decimal RateFactor
        {
            set { _RateFactor = value; }
            get { return _RateFactor; }
        }

        public bool Approve
        {
            set { _Approve = value; }
            get { return _Approve; }
        }

        public string WeekID
        {
            set { _WeekID = value; }
            get { return _WeekID; }
        }
    }

    public class TimesheetEffortDescriptionEntity
    {
        private int _TimeSheetEffortDescriptionID;
        private int _Empno;
        private DateTime _TSDate;
        private int _RangeID;
        private string _RangeDescription;
        private string _WeekID;

        public TimesheetEffortDescriptionEntity()
        {

        }

        public int TimeSheetEffortDescriptionID
        {
            set { _TimeSheetEffortDescriptionID = value; }
            get { return _TimeSheetEffortDescriptionID; }
        }
        public int Empno
        {
            set { _Empno = value; }
            get { return _Empno; }
        }

        public DateTime TSDate
        {
            set { _TSDate = value; }
            get { return _TSDate; }
        }

        public int RangeID
        {
            set { _RangeID = value; }
            get { return _RangeID; }
        }

        public string RangeDescription
        {
            set { _RangeDescription = value; }
            get { return _RangeDescription; }
        }

        public string WeekID
        {
            set { _WeekID = value; }
            get { return _WeekID; }
        }
    }

}


