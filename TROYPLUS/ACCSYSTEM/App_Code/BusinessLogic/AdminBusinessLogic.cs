using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AdminBusinessLogic
/// </summary>
public class AdminBusinessLogic
{
    public AdminBusinessLogic()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public AdminBusinessLogic(string con)
    {
        this.ConnectionString = con;
    }

    public string ConnectionString { get; set; }

    public string CreateConnectionString(string connStr)
    {
        string connectionString = string.Empty;
        string newConnection = string.Empty;

        if (connStr.IndexOf("Provider=Microsoft.Jet.OLEDB.4.0;") > -1)
            connectionString = connStr;
        else
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[connStr].ConnectionString;

        newConnection = connectionString.Remove(connectionString.LastIndexOf("Password=") + 9);

        newConnection = newConnection + Helper.GetPasswordForDB(connectionString);
        if (connectionString.ToUpper().Contains("ATTACHDBFILENAME"))
        {
            return connectionString;
        }
        return newConnection;

    }

    #region Payroll Generation


    public void CheckQueueAndGeneratePayRoll()
    {
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        string dbQry = string.Empty;
        try
        {
            manager.Open();

            dbQry = string.Format(@"SELECT * FROM tblPayrollQueue WHERE Status = '{0}'", "Queued");
            DataSet ds = manager.ExecuteDataSet(CommandType.Text, dbQry);
            manager.Close();

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow drQueue in ds.Tables[0].Rows)
                {
                    int payrollId = 0;
                    int year = 0;
                    int month = 0;

                    if (int.TryParse(drQueue[0].ToString(), out payrollId))
                    {
                        int.TryParse(drQueue[1].ToString(), out year);
                        int.TryParse(drQueue[2].ToString(), out month);
                        //GeneratePayRoll(payrollId, year, month);
                    }
                }
            }

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

    public bool GeneratePayRoll(int payrollId, int year, int month, DataSet dsAdditionalPayComponent)
    {
        bool isPayrollGenerated = false;
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        try
        {
            if (payrollId > 0)
            {
                // Clear prior log details.
                ClearPayrollGenerationLog(payrollId);

                // Get all the employee list.       
                string dbQuery = "SELECT EmpNo, EmpFirstName FROM tblEmployee";
                manager.Open();
                DataSet ds = manager.ExecuteDataSet(CommandType.Text, dbQuery);
                string logMessage = string.Empty;
                if (ds != null && ds.Tables.Count > 0)
                {
                    // Prepare payslip datatable.
                    DataTable dtPaySlipInfo = new DataTable();
                    dtPaySlipInfo.Columns.Add(new DataColumn("EmployeeNo"));
                    dtPaySlipInfo.Columns.Add(new DataColumn("Deductions"));
                    dtPaySlipInfo.Columns.Add(new DataColumn("Payments"));
                    dtPaySlipInfo.Columns.Add(new DataColumn("LossOfPayDays"));
                    dtPaySlipInfo.Columns.Add(new DataColumn("OtherAllowance"));
                    dtPaySlipInfo.Columns.Add(new DataColumn("OtherDeductions"));
                    dtPaySlipInfo.Columns.Add(new DataColumn("SalesIncentive"));

                    UpdatePayrollStatus(payrollId, "In Progress");

                    // Validate necessary payroll associated details
                    bool validationResult = true;
                    foreach (DataRow drEmployee in ds.Tables[0].Rows)
                    {
                        int employeeNo = 0;
                        DataRow drPayslip = dtPaySlipInfo.NewRow();
                        if (int.TryParse(drEmployee[0].ToString(), out employeeNo))
                        {
                            // Validate payroll details for employee.
                            if (!ValidatePayrollDetailsForEmployee(employeeNo, year, month, ref logMessage))
                            {
                                validationResult = false;
                                InsertPayrollLog(logMessage, payrollId, employeeNo);
                            }
                        }
                    }

                    if (validationResult)
                    {
                        // Prepare payroll generation logtable.
                        bool payrollGenerationResult = true;
                        bool hasAdditionalPaycomponent = false;

                        if (dsAdditionalPayComponent.Tables.Count > 0)
                        {
                            hasAdditionalPaycomponent = true;
                        }

                        foreach (DataRow drEmployee in ds.Tables[0].Rows)
                        {
                            int employeeNo = 0;
                            DataRow drPayslip = dtPaySlipInfo.NewRow();
                            if (int.TryParse(drEmployee[0].ToString(), out employeeNo))
                            {
                                if (GetPayRollDetailsForEmployee(employeeNo, year, month, drPayslip, dsAdditionalPayComponent, ref logMessage))
                                {
                                    dtPaySlipInfo.Rows.Add(drPayslip);
                                    InsertPayrollLog("Payroll processed", payrollId, employeeNo);
                                }
                                else
                                {
                                    payrollGenerationResult = false;
                                    InsertPayrollLog(logMessage, payrollId, employeeNo);
                                }
                            }
                        }

                        if (payrollGenerationResult && InsertPayslipInfo(manager, dtPaySlipInfo, payrollId, month, year))
                        {
                            UpdatePayrollStatus(payrollId, "Completed");
                            isPayrollGenerated = true;
                        }
                        else
                        {
                            UpdatePayrollStatus(payrollId, "Failed");
                        }
                    }
                    else
                    {
                        UpdatePayrollStatus(payrollId, "Failed");
                    }

                }
            }
            return isPayrollGenerated;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
            InsertPayrollLog("Unknown Error - Please contact adminstrator", payrollId, 0);
            UpdatePayrollStatus(payrollId, "Failed");
            return false;
        }
        finally
        {
            if (manager != null)
                manager.Dispose();
        }

    }

    private double GetOtherAllowanceForTheEmployee(DataSet dsPayComponent, int empNo)
    {
        DataTable dtEmployeePayComponent = dsPayComponent.Tables[0];
        DataTable dtEmployeeRolePayComponent = dsPayComponent.Tables[1];

        double otherAllowance = 0;
        int roleId = 0;
        UserInfo userInfo = new BusinessLogic(this.ConnectionString).GetUserInfoByEmpNo(empNo);
        if (userInfo != null)
        {
            roleId = userInfo.RoleId;
        }

        otherAllowance = (from dr in dtEmployeePayComponent.AsEnumerable()
                          where dr.Field<string>("EmpNo").Equals(empNo.ToString())
                          select dr.Field<double>("PayableAmount") == null ? 0 : dr.Field<double>("PayableAmount")).FirstOrDefault();


        otherAllowance += (from dr in dtEmployeeRolePayComponent.AsEnumerable()
                           where dr.Field<string>("RoleId").Equals(roleId.ToString())
                           select dr.Field<double>("PayableAmount") == null ? 0 : dr.Field<double>("PayableAmount")).FirstOrDefault();

        return otherAllowance;
    }

    private double GetOtherDeductionForTheEmployee(DataSet dsPayComponent, int empNo)
    {
        DataTable dtEmployeePayComponent = dsPayComponent.Tables[0];
        DataTable dtEmployeeRolePayComponent = dsPayComponent.Tables[1];

        double otherDeduction = 0;
        int roleId = 0;

        UserInfo userInfo = new BusinessLogic(this.ConnectionString).GetUserInfoByEmpNo(empNo);
        if (userInfo != null)
        {
            roleId = userInfo.RoleId;
        }

        otherDeduction = (from dr in dtEmployeePayComponent.AsEnumerable()
                          where dr.Field<string>("EmpNo").Equals(empNo.ToString())
                          select dr.Field<double>("DeductionAmount") == null ? 0 : dr.Field<double>("DeductionAmount")).FirstOrDefault();



        otherDeduction += (from dr in dtEmployeeRolePayComponent.AsEnumerable()
                           where dr.Field<string>("RoleId").Equals(roleId.ToString())
                           select dr.Field<double>("DeductionAmount") == null ? 0 : dr.Field<double>("DeductionAmount")).FirstOrDefault();

        return otherDeduction;
    }

    public DataTable GetEmployeeList()
    {
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        string dbQuery = "SELECT EmpNo, EmpFirstName FROM tblEmployee";
        manager.Open();
        DataSet ds = manager.ExecuteDataSet(CommandType.Text, dbQuery);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        else
        {
            return null;
        }
    }

    public DataTable GetEmployeeRolesList()
    {
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        string dbQuery = "SELECT ID, Role_Name FROM tblEmployeeRoles WHERE Is_Active =true";
        manager.Open();
        DataSet ds = manager.ExecuteDataSet(CommandType.Text, dbQuery);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        else
        {
            return null;
        }
    }

    private void ClearPayrollGenerationLog(int payrollId)
    {
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        string dbQuery = string.Format("DELETE * FROM tblPayrollGenerationLog l WHERE PayRollId={0}", payrollId);
        manager.Open();
        manager.ExecuteNonQuery(CommandType.Text, dbQuery);
        manager.Close();
    }

    private bool InsertPayslipInfo(DBManager manager, DataTable dtPaySlipInfo, int payrollId, int month, int year)
    {
        string dbQry = string.Empty;
        bool result = true;
        try
        {
            manager.BeginTransaction();
            foreach (DataRow dr in dtPaySlipInfo.Rows)
            {
                int employeeNo = 0;
                int.TryParse(dr[0].ToString(), out employeeNo);
                dbQry = string.Format(@"INSERT INTO tblEmployeePayslip (EmployeeId,PayrollDate,PayrollMonth,Deductions,Payments,PayrollId,PayrollYear,LossOfPayDays,OtherAllowance,OtherDeductions,SalesIncentive) 
                                    VALUES ({0},'{1}',{2},{3},{4},{5},{6},{7},{8},{9},{10})", employeeNo, DateTime.Now.Date, month, dr[1], dr[2], payrollId, year, dr["LossOfPayDays"], dr["OtherAllowance"], dr["OtherDeductions"], dr["SalesIncentive"]);

                if (manager.ExecuteNonQuery(CommandType.Text, dbQry) <= 0)
                {
                    result = false;
                    TroyLiteExceptionManager.HandleException(new Exception("Payslip not generated " + dbQry));
                    InsertPayrollLog("Payroll processed but payslip not inserted. Please contact Administrator.", payrollId, employeeNo);
                }
            }
            if (result)
            {
                manager.CommitTransaction();
            }
            else
            {
                manager.RollbackTransaction();
            }
            return result;

        }
        catch (Exception ex)
        {
            manager.RollbackTransaction();
            throw ex;
        }

    }

    private void UpdatePayrollStatus(int payrollId, string status)
    {
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        string dbQry = string.Empty;
        try
        {
            manager.Open();

            if (status.ToUpper().Equals("COMPLETED"))
            {
                dbQry = string.Format(@"UPDATE tblPayrollQueue SET Status = '{0}', PayrollCompletedDateTime='{1}' WHERE PayrollId={2}", status, DateTime.Now, payrollId);
            }
            else
            {
                dbQry = string.Format(@"UPDATE tblPayrollQueue SET Status = '{0}' WHERE PayrollId={1}", status, payrollId);
            }

            int result = manager.ExecuteNonQuery(CommandType.Text, dbQry);

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

    private void InsertPayrollLog(string logMessage, int payrollId, int employeeNo)
    {
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        string dbQry = string.Empty;
        try
        {
            manager.Open();
            dbQry = string.Format(@"INSERT INTO tblPayrollGenerationLog (PayrollId,EmployeeNo,Message) 
                                    VALUES ({0},{1},'{2}')", payrollId, employeeNo, logMessage);

            int result = manager.ExecuteNonQuery(CommandType.Text, dbQry);

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

    public bool GetPayRollDetailsForEmployee(int employeeNo, int year, int month, DataRow payslipInfoRow, DataSet dsAdditionalPayComponent, ref string logMessage)
    {
        string dbQry = string.Empty;
        logMessage = string.Empty;
        try
        {
            // Get declared payable/deductiable amount for the employee.
            int totalPayable = GetEmployeeTotalPayComponent(employeeNo, year, month);
            int totalDeductions = GetEmployeeTotalDeduction(employeeNo, year, month);

            // Get additional payable/deductiable amount for the employee.
            double otherAllowance = 0;
            double otherDeduction = 0;
            if (dsAdditionalPayComponent != null && dsAdditionalPayComponent.Tables.Count > 0)
            {
                otherAllowance = GetOtherAllowanceForTheEmployee(dsAdditionalPayComponent, employeeNo);
                otherDeduction = GetOtherDeductionForTheEmployee(dsAdditionalPayComponent, employeeNo);
            }


            // Get loss of pay leaves.
            DataTable dtEmpLeavesApplied = GetEmployeeLOPLeavesAppliedForTheMonth(employeeNo, year, month);
            double totalLeavesDaysAppliedLeaves = GetTotalLeavesInTheLeaveSummary(dtEmpLeavesApplied, year, month);

            int totalDaysInTheMonth = DateTime.DaysInMonth(year, month);

            // Check product sales incentive eligibility
            double salesIncentiveAmount = GetSalesIncentiveForEmployee(employeeNo, year, month);

            payslipInfoRow[0] = employeeNo;
            payslipInfoRow[1] = totalDeductions;
            payslipInfoRow[2] = totalPayable;
            payslipInfoRow[3] = totalLeavesDaysAppliedLeaves;
            payslipInfoRow[4] = otherAllowance;
            payslipInfoRow[5] = otherDeduction;
            payslipInfoRow[6] = salesIncentiveAmount;
            return true;
        }
        catch (Exception ex)
        {
            logMessage = ex.Message;
            TroyLiteExceptionManager.HandleException(ex);
            return false;
        }
        finally
        {

        }
    }

    private double GetSalesIncentiveForEmployee(int employeeNo, int year, int month)
    {
        // Check eligibilty from product sales.
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);

        int empSalesIncentive = 0;
        try
        {
            manager.Open();
            string dbQuery = string.Format(@"SELECT si.BillNo,s.BillDate,ExecutiveName,CustomerId, si.ItemCode,pp.Price,Rate,(Rate-pp.Price) As Profit, (((Rate-pp.Price)*100)/pp.Price) as ProfitMargin
                            FROM tblSalesItems si
                            JOIN tblSales s on S.BillNo = si.BillNo
                            JOIN tblProductPrices pp on pp.ItemCode = si.ItemCode AND UPPER(pp.PriceName)='DP'
                            WHERE ExecutiveName={0} AND YEAR(s.BillDate) ={1} AND MONTH(s.BillDate)={2}", employeeNo, year, month);
            DataSet ds = manager.ExecuteDataSet(CommandType.Text, dbQuery);

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string itemCode = dr["ItemCode"].ToString();
                    int profitAmt = int.Parse(dr["Profit"].ToString());
                    int profitPercent = 0;
                    int.TryParse(dr["ProfitMargin"].ToString(), out profitPercent);
                    int incentivePercent = GetIncentivePercentForProduct(itemCode, profitPercent);
                    int incentiveAmt = (profitAmt * incentivePercent) / 100;
                    empSalesIncentive += incentiveAmt;
                }
            }
            return empSalesIncentive;
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

    private int GetIncentivePercentForProduct(string itemCode, int profitPercent)
    {
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);

        int empSalesIncentivePercent = 0;
        string incentiveSlab = getIncentiveSlab(profitPercent);

        try
        {
            if (!string.IsNullOrEmpty(incentiveSlab))
            {
                manager.Open();
                string dbQuery = string.Format(@"SELECT {0}
                            FROM tblProdSalesIncentive                            
                            WHERE ItemCode={1}", incentiveSlab, itemCode);
                DataSet ds = manager.ExecuteDataSet(CommandType.Text, dbQuery);

                if (ds != null && ds.Tables.Count > 0)
                {
                    var tempValue = ds.Tables[0].Rows[0][0].ToString();
                    int.TryParse(tempValue, out empSalesIncentivePercent);
                }
            }
            return empSalesIncentivePercent;
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

    private string getIncentiveSlab(int profitPercent)
    {
        string slab = string.Empty;
        if (profitPercent >= 91)
        {
            // slab 5
            slab = "FifthSlab";
        }
        else if (profitPercent >= 61)
        {
            // slab #4
            slab = "FourthSlab";
        }
        else if (profitPercent >= 26)
        {
            // slab #3
            slab = "ThirdSlab";
        }
        else if (profitPercent >= 11)
        {
            // slab #2
            slab = "SecondSlab";
        }
        else if (profitPercent >= 5)
        {
            // slab #1
            slab = "FirstSlab";
        }

        return slab;
    }

    private int GetEmployeeTotalPayComponent(int employeeNo, int year, int month)
    {
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        string dbQry = string.Empty;
        int empPayComponent = 0;
        try
        {
            manager.Open();
            dbQry = string.Format(@"Select SUM(pcm.DeclaredAmount) as ComponentTotalPay,'EmployeePayComponent' as PayComponentType
                                    FROM tblPayComponentEmployeeMapping pcm
                                    INNER JOIN tblPayComponents pc ON pc.PayComponentId=pcm.PayComponent_Id                                    
                                    WHERE pcm.EmpNo={0} AND pc.PayComponentType_id=1 AND pc.IsDeduction=False 
                                    AND EffectiveDate<= #{1}#
                                    UNION
                                    Select SUM(pcm.DeclaredAmount) as ComponentTotalPay,'EmployeeRolePayComponent' as PayComponentType 
                                    FROM (( tblPayComponentRoleMapping pcm                                    
                                    INNER JOIN tblPayComponents pc ON pc.PayComponentId=pcm.PayComponent_Id )  
                                    INNER JOIN tblEmployee e ON e.EmployeeRoleId=pcm.Role_Id      )                           
                                    WHERE e.EmpNo={0} AND pc.PayComponentType_id=2 AND pc.IsDeduction=False 
                                    AND EffectiveDate<= #{1}#", employeeNo, string.Format("01-{0}-{1}", month, year));


            DataSet ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var empPayComponent1 = ds.Tables[0].Rows[0][0].ToString();
                    int.TryParse(empPayComponent1, out empPayComponent);
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        var empPayComponent2 = ds.Tables[0].Rows[1][0].ToString();
                        var totalPay = empPayComponent;
                        int.TryParse(empPayComponent2, out empPayComponent);
                        empPayComponent = empPayComponent + totalPay;
                    }

                    return empPayComponent;
                }
            }
            return empPayComponent;
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

    private int GetEmployeeTotalDeduction(int employeeNo, int year, int month)
    {
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        string dbQry = string.Empty;
        int empPayComponent = 0;
        try
        {
            manager.Open();

            dbQry = string.Format(@"Select SUM(pcm.DeclaredAmount) as ComponentTotalPay,'EmployeePayComponent' as PayComponentType
                                    FROM tblPayComponentEmployeeMapping pcm
                                    INNER JOIN tblPayComponents pc ON pc.PayComponentId=pcm.PayComponent_Id                                    
                                    WHERE pcm.EmpNo={0} AND pc.PayComponentType_id=1 AND pc.IsDeduction=True 
                                    AND EffectiveDate<= #{1}#
                                    UNION
                                    Select SUM(pcm.DeclaredAmount) as ComponentTotalPay,'EmployeeRolePayComponent' as PayComponentType 
                                    FROM (( tblPayComponentRoleMapping pcm                                    
                                    INNER JOIN tblPayComponents pc ON pc.PayComponentId=pcm.PayComponent_Id )  
                                    INNER JOIN tblEmployee e ON e.EmployeeRoleId=pcm.Role_Id      )                           
                                    WHERE e.EmpNo={0} AND pc.PayComponentType_id=2 AND pc.IsDeduction=True 
                                    AND EffectiveDate<= #{1}#", employeeNo, string.Format("01-{0}-{1}", month, year));

            DataSet ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var empPayComponent1 = ds.Tables[0].Rows[0][0].ToString();
                    int.TryParse(empPayComponent1, out empPayComponent);
                    if (ds.Tables[0].Rows.Count > 1)
                    {
                        var empPayComponent2 = ds.Tables[0].Rows[1][0].ToString();
                        var totalPay = empPayComponent;
                        int.TryParse(empPayComponent2, out empPayComponent);
                        empPayComponent = empPayComponent + totalPay;
                    }

                    return empPayComponent;
                }
            }
            return empPayComponent;
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

    public bool ValidatePayrollDetailsForEmployee(int empNo, int year, int month, ref string logMessage)
    {
        List<string> logMessageSummary = new List<string>();
        bool result = true;
        if (HaveUnApprovedLeavesForTheMonth(empNo, year, month, ref  logMessage))
        {
            logMessageSummary.Add(logMessage);
            result = false;
        }
        if (HaveUnSubmittedAttendance(year, month))
        {

        }
        if (!HaveAppliedTheLeavesTaken(empNo, year, month, ref  logMessage))
        {
            logMessageSummary.Add(logMessage);
            result = false;
        }
        logMessage = string.Join(Environment.NewLine, logMessageSummary);
        return result;
    }

    private bool HaveUnSubmittedAttendance(int year, int month)
    {
        return false;
    }

    private bool HaveUnApprovedAttendanceBySupervisor(int year, int month, ref string logMessage)
    {
        string dbQuery = string.Format(@"Select a.Remarks,Count(AttendanceDate) from ((tblAttendanceDetail a
                                            INNER JOIN tblEmployee e1 ON a.EmployeeNo = e1.EmpNo)
                                            INNER JOIN tblEmployee e2 ON e1.ManagerID = e2.EmpNo)
                                        WHERE a.EmployeeNo = {0} AND
                                            (YEAR(a.AttendanceDate)={1} AND MONTH(a.AttendanceDate)={2})
                                        GROUP BY a.Remarks", year, month, "Approved");


        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        manager.Open();
        DataSet ds = manager.ExecuteDataSet(CommandType.Text, dbQuery);
        if (ds != null && ds.Tables.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HaveAppliedTheLeavesTaken(int empNo, int year, int month, ref string logMessage)
    {
        logMessage = string.Empty;
        DataTable dtEmpAttendanceSummary = GetEmployeeAttendanceSummaryForTheMonth(empNo, year, month);
        DataTable dtEmpLeavesApplied = GetEmployeeLeavesAppliedForTheMonth(empNo, year, month);
        if (dtEmpAttendanceSummary != null && dtEmpAttendanceSummary != null)
        {
            double totalLeaveDaysByAttendance = GetTotalLeavesInTheAttendance(dtEmpAttendanceSummary);
            double totalLeavesDaysAppliedLeaves = Math.Floor(GetTotalLeavesInTheLeaveSummary(dtEmpLeavesApplied, year, month));
            TroyLiteExceptionManager.HandleException(new Exception(string.Format("totalLeaveDaysByAttendance {0} - totalLeavesDaysAppliedLeaves {1}", totalLeaveDaysByAttendance, totalLeavesDaysAppliedLeaves)));
            if (!totalLeaveDaysByAttendance.Equals(totalLeavesDaysAppliedLeaves))
            {
                logMessage = string.Format("Leave entries and attendance records mismatching for the employee {1}({0}) [Attendance Leave Entry-{2} LeavesApplied-{3}]", empNo, string.Empty, totalLeaveDaysByAttendance, totalLeavesDaysAppliedLeaves);
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }

    private double GetTotalLeavesInTheAttendance(DataTable dtEmpAttendanceSummary)
    {
        double totalLeavesByAttendance = 0;
        DataRow[] drLeaves = dtEmpAttendanceSummary.Select("Remarks IN ('Leave','Absent')");
        if (drLeaves.Count() > 0)
        {
            foreach (DataRow dr in drLeaves)
            {
                double tempValue = 0;
                if (double.TryParse(dr[1].ToString(), out tempValue))
                {
                    totalLeavesByAttendance += tempValue;
                }
            }
        }
        return totalLeavesByAttendance;
    }

    private double GetTotalLeavesInTheLeaveSummary(DataTable dtEmpLeavesApplied, int year, int month)
    {
        double totalLeavesDaysAppliedLeaves = 0;
        foreach (DataRow drLeaveEntry in dtEmpLeavesApplied.Rows)
        {
            DateTime startDate, endDate;
            string startDateSession, endDateSession;
            DateTime.TryParse(drLeaveEntry["StartDate"].ToString(), out startDate);
            DateTime.TryParse(drLeaveEntry["EndDate"].ToString(), out endDate);
            startDateSession = drLeaveEntry["StartDateSession"].ToString();
            endDateSession = drLeaveEntry["EndDateSession"].ToString();
            if (!startDate.Year.Equals(year) || !startDate.Month.Equals(month))
            {
                startDate = DateTime.Parse(string.Format("{0}-{1}-{2}", year, month, "01"));
                startDateSession = "FN";
            }
            if (!endDate.Year.Equals(year) || !endDate.Month.Equals(month))
            {
                endDate = DateTime.Parse(string.Format("{0}-{1}-{2}", year, month, DateTime.DaysInMonth(year, month)));
                endDateSession = "AN";
            }
            totalLeavesDaysAppliedLeaves += CalculateTotalLeaveDays(startDate, startDateSession, endDate, endDateSession);
        }
        return totalLeavesDaysAppliedLeaves;
    }

    private DataTable GetEmployeeLeavesAppliedForTheMonth(int empNo, int year, int month)
    {
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;

        dbQry = string.Format(@"SELECT a.LeaveId, a.EmployeeNo, a.StartDate,a.StartDateSession, a.EndDate,a.EndDateSession,a.TotalDays
                                FROM tblEmployeeLeave a
                                WHERE a.EmployeeNo ={0}
                                AND ((YEAR(a.StartDate) = {1} AND MONTH(a.StartDate) = {2}) 
                                        OR (YEAR(a.EndDate) = {1} AND MONTH(a.EndDate) = {2}))", empNo, year, month);

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
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

    private DataTable GetEmployeeLOPLeavesAppliedForTheMonth(int empNo, int year, int month)
    {
        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        DataSet ds = new DataSet();
        string dbQry = string.Empty;


        dbQry = string.Format(@"SELECT a.LeaveId, a.EmployeeNo, a.StartDate,a.StartDateSession, a.EndDate,a.EndDateSession,a.TotalDays
                                FROM (tblEmployeeLeave a 
                                        INNER JOIN tblLeaveTypes l on a.LeaveTypeId = l.ID)
                                WHERE a.EmployeeNo ={0}
                                AND l.IsPayable=False
                                AND ((YEAR(a.StartDate) = {1} AND MONTH(a.StartDate) = {2}) 
                                        OR (YEAR(a.EndDate) = {1} AND MONTH(a.EndDate) = {2}))", empNo, year, month);

        try
        {
            manager.Open();
            ds = manager.ExecuteDataSet(CommandType.Text, dbQry);

            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
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

    private DataTable GetEmployeeAttendanceSummaryForTheMonth(int empNo, int year, int month)
    {
        string dbQuery = string.Format(@"Select a.Remarks,Count(AttendanceDate) from ((tblAttendanceDetail a
                                            INNER JOIN tblEmployee e1 ON a.EmployeeNo = e1.EmpNo)
                                            INNER JOIN tblEmployee e2 ON e1.ManagerID = e2.EmpNo)
                                        WHERE a.EmployeeNo = {0} AND
                                            (YEAR(a.AttendanceDate)={1} AND MONTH(a.AttendanceDate)={2})
                                        GROUP BY a.Remarks", empNo, year, month, "Approved");


        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        manager.Open();
        DataSet ds = manager.ExecuteDataSet(CommandType.Text, dbQuery);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        else
        {
            return null;
        }

    }

    public bool HaveUnApprovedLeavesForTheMonth(int empNo, int year, int month, ref string logMessage)
    {
        logMessage = string.Empty;
        string dbQuery = string.Format(@"Select a.EmployeeNo,e1.EmpFirstName as EmpName,a.Approver,e2.EmpFirstName as ApproverName from ((tblEmployeeLeave a
                                            INNER JOIN tblEmployee e1 ON a.EmployeeNo = e1.EmpNo)
                                            INNER JOIN tblEmployee e2 ON a.Approver = e2.EmpNo)
                            WHERE a.EmployeeNo = {0} AND
                                    ((YEAR(a.StartDate)={1} OR YEAR(a.EndDate)={1}) AND (MONTH(a.StartDate)={2} OR MONTH(a.EndDate)={2}))
                                        AND a.Status ='{3}'", empNo, year, month, "Submitted");


        DBManager manager = new DBManager();
        manager.ConnectionString = CreateConnectionString(this.ConnectionString);
        manager.Open();
        DataSet ds = manager.ExecuteDataSet(CommandType.Text, dbQuery);
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dRow = ds.Tables[0].Rows[0];
                logMessage = string.Format("Leaves are not approved for the employee {1}({0}) [Approver: {3}({2})]", dRow["EmployeeNo"].ToString(), dRow["EmpName"].ToString(), dRow["Approver"].ToString(), dRow["ApproverName"].ToString());
                return true;
            }
        }
        return false;
    }

    private double CalculateTotalLeaveDays(DateTime StartDate, string StartDateSession, DateTime EndDate, string EndDateSession)
    {
        double totalLeaveDays = 0;
        int dateDiffDays = new DateTimeHelper.DateDifference(StartDate, EndDate).Days;
        if (dateDiffDays.Equals(0))
        {
            if (StartDateSession.Equals("FN") && EndDateSession.Equals("AN"))
            {
                totalLeaveDays = 1.0;
            }
            else if (StartDateSession.Equals("FN") && EndDateSession.Equals("FN"))
            {
                totalLeaveDays = 0.5;
            }
            else if (StartDateSession.Equals("AN") && EndDateSession.Equals("AN"))
            {
                totalLeaveDays = 0.5;
            }
        }
        else
        {
            if (StartDateSession.Equals("FN") && EndDateSession.Equals("AN"))
            {
                totalLeaveDays = dateDiffDays + 1;
            }
            else if (StartDateSession.Equals("FN") && EndDateSession.Equals("FN"))
            {
                totalLeaveDays = dateDiffDays + 0.5;
            }
            else if (StartDateSession.Equals("AN") && EndDateSession.Equals("AN"))
            {
                totalLeaveDays = dateDiffDays + 0.5;
            }
            else if (StartDateSession.Equals("AN") && EndDateSession.Equals("FN"))
            {
                totalLeaveDays = dateDiffDays;
            }
        }
        return totalLeaveDays;
    }
    #endregion
}