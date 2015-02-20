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

public partial class ShareInterestCalculation : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    private int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    private DateTime fromDate;
    private DateTime toDate;
    private int year;
    private int month;
    private int day;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!IsPostBack)
            {
                dvResult.Visible = false;
                GetShareHolders();

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }
    public void GetShareHolders()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = bl.ListShareHolder();
        cmbShareHolder.DataSource = ds;
        cmbShareHolder.DataBind();
        cmbShareHolder.DataTextField = "LedgerName";
        cmbShareHolder.DataValueField = "LedgerID";
    }
    public int monthDifference(DateTime startDate, DateTime endDate)
    {


        DateTime systemStartDate = new DateTime();



        /// Time span to set the total time difference

        TimeSpan timeDifference;



        ///This if condition to avoid the minus value for Time difference

        if (endDate > startDate)
        {

            timeDifference = endDate.Subtract(startDate);

        }

        else
        {

            timeDifference = startDate.Subtract(endDate);

        }



        ///This is to generate a date from the systemStartDate 

        DateTime generatedDate = systemStartDate.Add(timeDifference);



        ///Substract 1 because the systemStartDate is "0001/01/01"

        int noOfYears = generatedDate.Year - 1;

        int noOfMonths = generatedDate.Month - 1;

        int noOfDays = timeDifference.Days + 1;

        noOfMonths = noOfMonths + (noOfYears * 12);

        //    noOfMonths = DaysInMonth(noOfYears, noOfMonths, noOfDays);

        return noOfMonths;
    }




    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                DateTime startDate, endDate;
                startDate = Convert.ToDateTime(txtStartDate.Text);
                endDate = Convert.ToDateTime(txtEndDate.Text);
                double dblInterest = 0;
                string strShareName = cmbShareHolder.SelectedItem.Text.Trim();
                double dblPrincipal = 0;

                double NoOfMonths = 0;
                double dPNR = 0;

                if (lblAmount.Text != "")
                    dblPrincipal = Convert.ToDouble(lblAmount.Text);
                if (txtInterest.Text.Trim() != "")
                    dblInterest = Convert.ToDouble(txtInterest.Text.Trim());
                //NoOfMonths = monthDifference(startDate, endDate);
                NoOfMonths = DateDifference(startDate, endDate);
                dPNR = (dblPrincipal * NoOfMonths * (dblInterest / 100));
                lblShare.Text = strShareName;
                lblInterest.Text = txtInterest.Text.Trim();
                TimeSpan tDays = endDate.Subtract(startDate);
                int iDays = tDays.Days + 1;
                lblDate.Text = NoOfMonths.ToString("f2") + "  (" + iDays + " Days )";
                lblInterestAmount.Text = dPNR.ToString("f2");
                dvResult.Visible = true;

            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void cmbShareHolder_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            double amt = 0;
            int iLedgerID = 0;
            iLedgerID = Convert.ToInt32(cmbShareHolder.SelectedItem.Value);
            BusinessLogic bl = new BusinessLogic(sDataSource);
            amt = bl.GetShareAmount(iLedgerID);
            lblAmount.Text = amt.ToString("f2");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public double DateDifference(DateTime d1, DateTime d2)
    {
        int increment;

        if (d1 > d2)
        {
            this.fromDate = d2;
            this.toDate = d1;
        }
        else
        {
            this.fromDate = d1;
            this.toDate = d2;
        }

        /// 
        /// Day Calculation
        /// 
        increment = 0;

        if (this.fromDate.Day > this.toDate.Day)
        {
            increment = this.monthDay[this.fromDate.Month - 1];

        }
        /// if it is february month
        /// if it's to day is less then from day
        if (increment == -1)
        {
            if (DateTime.IsLeapYear(this.fromDate.Year))
            {
                // leap year february contain 29 days
                increment = 29;
            }
            else
            {
                increment = 28;
            }
        }
        if (increment != 0)
        {
            day = (this.toDate.Day + increment) - this.fromDate.Day;
            increment = 1;
        }
        else
        {
            day = this.toDate.Day - this.fromDate.Day + 1;
        }

        ///
        ///month calculation
        ///
        if ((this.fromDate.Month + increment) > this.toDate.Month)
        {
            this.month = (this.toDate.Month + 12) - (this.fromDate.Month + increment);
            increment = 1;
        }
        else
        {
            this.month = (this.toDate.Month) - (this.fromDate.Month + increment);
            increment = 0;
        }

        ///
        /// year calculation
        ///
        this.year = this.toDate.Year - (this.fromDate.Year + increment);
        double noOfmonths = 0;
        double noofDays = 0;
        noOfmonths = this.year * 12;
        noOfmonths = noOfmonths + this.month;



        increment = this.monthDay[this.toDate.Month - 1];

        if (increment == -1)
        {
            if (DateTime.IsLeapYear(this.fromDate.Year))
            {
                // leap year february contain 29 days
                increment = 29;
            }
            else
            {
                increment = 28;
            }
        }
        noofDays = this.day;
        noofDays = noofDays / increment;
        noOfmonths = noOfmonths + noofDays;
        return noOfmonths;

    }

    public int Years
    {
        get
        {
            return this.year;
        }
    }

    public int Months
    {
        get
        {
            return this.month;
        }
    }

    public int Days
    {
        get
        {
            return this.day;
        }
    }
}
