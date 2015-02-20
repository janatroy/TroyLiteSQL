using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

public static class DateTimeExtension
{
    public static DateTime GetFirstDayOfWeek(this DateTime date)
    {
        var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        while (date.DayOfWeek != firstDayOfWeek)
        {
            date = date.AddDays(-1);
        }

        return date;
    }

    public static void GetWeek(DateTime now, CultureInfo cultureInfo, out DateTime begining, out DateTime end)
    {
        if (now == null)
            throw new ArgumentNullException("now");
        if (cultureInfo == null)
            throw new ArgumentNullException("cultureInfo");

        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        int offset = firstDayOfWeek - now.DayOfWeek;

        if (offset != 1)
        {
            DateTime weekStart = now.AddDays(offset);
            DateTime endOfWeek = weekStart.AddDays(6);

            begining = weekStart;
            end = endOfWeek;
        }
        else
        {
            begining = now.AddDays(-6);
            end = now;
        }
    }

    private static int CurrentWeekOfYear(DateTime date)
    {
        var day = (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
        return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date.AddDays(4 - (day == 0 ? 7 : day)), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    public static string CurrentWeekwithYear(DateTime date)
    {
        return CurrentWeekOfYear(date).ToString() + ":" + date.Year.ToString();
    }

    public static string CurrentDayString(int iDate)
    {
        string[] strMonths = new string[]{"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
        return strMonths[iDate - 1];
    }

    public static string WeekStartToEndDateString(DateTime dtBegining, DateTime dtEnd)
    {
        return dtBegining.Day.ToString() + " - " + dtEnd.Day.ToString() + " " + DateTimeExtension.CurrentDayString(dtBegining.Month) + " " + dtBegining.Year.ToString();
    }

    public static string GetFormatWeekForGivenWeekID(string strWeekID)
    {
        return strWeekID.Split(':').First<string>() + " week of " + strWeekID.Split(':').ElementAt<string>(1);
    }

}