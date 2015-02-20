using System;
using System.Data;
using DBLayer;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace SMSLibrary
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class UtilitySMS
	{
		private string _connectionstring = string.Empty;

		public UtilitySMS()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected string ConnectionString
		{
			get { return _connectionstring; }
			set { _connectionstring = value; }
		}

		public UtilitySMS(string ConnectionString)
		{
			this.ConnectionString = ConnectionString;
		}

		public bool SendSMS(string Provider, string Priority, string SenderID,string UserName, string Password, string Mobile, string Text, bool LogRequired, string LoggedInUserID)
		{
			//we are creating the necessary URL string:
			string ozSURL = Provider; //where Ozeki NG SMS Gateway is running
			string ozUser = HttpUtility.UrlEncode(UserName); //username for successful login
			string ozPassw = HttpUtility.UrlEncode(Password); //user's password
			string ozSenderID = HttpUtility.UrlEncode(SenderID); //Sender's ID
			string ozRecipients = HttpUtility.UrlEncode(Mobile); //who will get the message
			string ozMessageData = HttpUtility.UrlEncode(Text); //bodyof message

			string createdURL = ozSURL + "?" + "username=" +ozUser +
					"&password=" + ozPassw + 
					"&sender=" + ozSenderID +
					"&to=" + ozRecipients+
					"&message=" + ozMessageData;

			if((Priority != string.Empty) && (Priority == "2"))
			{
				createdURL = createdURL + "&priority=2";
			}
			
			try
			{
				WebRequest wrGETURL = WebRequest.Create(createdURL);
				WebResponse myWebResponse = wrGETURL.GetResponse();
				Stream ReceiveStream = myWebResponse.GetResponseStream();
				Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
				StreamReader readStream = new StreamReader(ReceiveStream, encode);
				string strResponse = readStream.ReadToEnd();

                Text = Text + " Response : " + strResponse;

				if(LogRequired)
				{
					AddToLog(LoggedInUserID,Text,Mobile);
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}

		}

        public string SendSMSWithResponse(string Provider, string Priority, string SenderID, string UserName, string Password, string Mobile, string Text, bool LogRequired, string LoggedInUserID)
        {
            //we are creating the necessary URL string:
            string ozSURL = Provider; //where Ozeki NG SMS Gateway is running
            string ozUser = HttpUtility.UrlEncode(UserName); //username for successful login
            string ozPassw = HttpUtility.UrlEncode(Password); //user's password
            string ozSenderID = HttpUtility.UrlEncode(SenderID); //Sender's ID
            string ozRecipients = HttpUtility.UrlEncode(Mobile); //who will get the message
            string ozMessageData = HttpUtility.UrlEncode(Text); //bodyof message

            string createdURL = ozSURL + "?" + "username=" + ozUser +
                    "&password=" + ozPassw +
                    "&sender=" + ozSenderID +
                    "&to=" + ozRecipients +
                    "&message=" + ozMessageData;

            if ((Priority != string.Empty) && (Priority == "2"))
            {
                createdURL = createdURL + "&priority=2";
            }

            try
            {
                WebRequest wrGETURL = WebRequest.Create(createdURL);
                WebResponse myWebResponse = wrGETURL.GetResponse();
                Stream ReceiveStream = myWebResponse.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(ReceiveStream, encode);
                string strResponse = readStream.ReadToEnd();
                
                Text = Text + " Response : " + strResponse;
                
                if (LogRequired)
                {
                    AddToLog(LoggedInUserID, Text, Mobile);
                }

                return strResponse;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


		public bool AddToLog(string LoggedInUserID, string Text, string Mobile)
		{
			DBManager manager = new DBManager(DataProvider.OleDb);
			manager.ConnectionString = this.ConnectionString;
			
			string dbQry = string.Empty;

			try
			{
				dbQry = string.Format("Insert Into tblSMSAudit(UserID,Mobile,TextData,DateCreated) Values ('{0}','{1}','{2}','{3}')", LoggedInUserID, Mobile, Text, DateTime.Now.ToLongDateString());
				manager.Open();
				manager.ExecuteNonQuery(CommandType.Text, dbQry);
			}
			catch (Exception ex)
			{
				return false;
			}
			
			return true;
		}

	}
}
