<%@ WebHandler Language="C#" Class="DisplayImg" %>

using System;
using System.Web;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

public class DisplayImg : IHttpHandler {

    private string sDataSource = string.Empty;
    
    public void ProcessRequest (HttpContext context)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[context.Request.Cookies["Company"].Value].ToString();
        
        Int32 theID;
        if (context.Request.QueryString["id"] != null)
            theID = Convert.ToInt32(context.Request.QueryString["id"]);
        else
            throw new ArgumentException("No parameter specified");

        context.Response.ContentType = "image/jpeg";
        Stream strm = DisplayImage(theID);
        byte[] buffer = new byte[2048];
        int byteSeq = strm.Read(buffer, 0, 2048);

        while (byteSeq > 0)
        {
            context.Response.OutputStream.Write(buffer, 0, byteSeq);
            byteSeq = strm.Read(buffer, 0, 2048);
        }
    }
    
    public Stream DisplayImage(int theID)
    {
        BusinessLogic objChk = new BusinessLogic();
        using (OleDbConnection connection = new OleDbConnection(objChk.CreateConnectionString(sDataSource)))
        {
            string sql = "SELECT img_stream FROM tblimg WHERE id = @ID";
            OleDbCommand cmd = new OleDbCommand(sql, connection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", theID);
            connection.Open();
            object theImg = cmd.ExecuteScalar();
            try
            {
                return new MemoryStream((byte[])theImg);
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
    }
 
    public bool IsReusable {
        get
        {
            return false;
        }
    }

}