using System;
using System.Text;
using System.IO;

namespace ClientLog
{
	/// <summary>
	/// Summary description for CQOLog.
	/// </summary>
	public class Log
	{
		string filePath;
		string logName;
		System.IO.StreamWriter logfile = null;

		public Log()
		{
			//
			// TODO: Add constructor logic here
			//
			string year = DateTime.Now.Year.ToString();
			string month = DateTime.Now.Month.ToString();
			string day = DateTime.Now.Day.ToString();
			
			if(month.Length == 1)
				month = "0"+month;

			if(day.Length == 1)
				day = "0"+day;

			string formattedDate = year+month+day;

			filePath = System.Configuration.ConfigurationSettings.AppSettings["ClientLogPath"].ToString();

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

			logName = filePath+ "\\" + formattedDate + ".log";
		}

		public void WriteToLog(string message)
		{
			if(!File.Exists(logName))
				logfile = File.CreateText(logName);
			else
				logfile = File.AppendText(logName);

			logfile.WriteLine("{0} : {1}",DateTime.Now.ToString(),message);

			logfile.Flush();
			logfile.Close();

		}
		public void LineBreak()
		{
			if(!File.Exists(logName))
				logfile = File.CreateText(logName);
			else
				logfile = File.AppendText(logName);

			logfile.WriteLine("----------------------------------------------------------------------");
			logfile.WriteLine();

			logfile.Flush();
			logfile.Close();

		}

		public void JobComplete()
		{
			if(!File.Exists(logName))
				logfile = File.CreateText(logName);
			else
				logfile = File.AppendText(logName);
			
			logfile.WriteLine();
			logfile.WriteLine("*******************************************************************************");
			logfile.WriteLine();

			logfile.Flush();
			logfile.Close();

		}

		public void BlankLine()
		{
			if(!File.Exists(logName))
				logfile = File.CreateText(logName);
			else
				logfile = File.AppendText(logName);

			logfile.WriteLine();

			logfile.Flush();
			logfile.Close();

		}

		
	}
}
