using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for EmailLogic
/// </summary>
public static class EmailLogic
{
    
    private static SmtpClient em;

    static EmailLogic()
    {
        SmtpClient sm = new SmtpClient();
    }

    public static void SendEmail(string smtphostname, int smtpport, string fromAddress, string toAddress, string subject, string body, string fromPassword)
    {
        try
        {
            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = smtphostname;
                smtp.Port = smtpport;
                //smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send(fromAddress, toAddress, subject, body);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}