<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data;
using System.Drawing;
using System.IO;
using System.Data.OleDb;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Linq;

public class Handler : IHttpHandler 
{
    private string sDataSource = string.Empty;
    
    public void ProcessRequest (HttpContext context)
    {
        //OleDbConnection myAccessConnection;

        //string connStr = string.Empty;

        //if (Request.Cookies["Company"] != null)
        //    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        //else
        //    Response.Redirect("~/Login.aspx");

        //BusinessLogic objBus = new BusinessLogic(connStr);
        
        //sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        
        BusinessLogic objChk = new BusinessLogic();
        
        // Create SQL Connection 
        //string strConn = "Data Source=serverName;Initial Catalog=dbName;User ID=userID;Password=passWord;";
        //SqlConnection con = new SqlConnection(strConn);
        //using (OleDbConnection connection = new OleDbConnection(objChk.CreateConnectionString(sDataSource)))
        //{
        //    OleDbCommand command = new OleDbCommand();
        //    OleDbTransaction transaction = null;
        //    OleDbDataAdapter adapter = null;

        //    // Set the Connection to the new OleDbConnection.
        //    command.Connection = connection;

            try
            {
                //connection.Open();


                //OleDbCommand myCommand = new OleDbCommand("select * from tblImg where img_id=@img_id");
                //myCommand.CommandType = CommandType.Text;

                //OleDbParameter ImageID = new OleDbParameter("@img_id", OleDbType.Integer);
                //ImageID.Value = context.Request.QueryString["ImageID"].ToString();
                //myCommand.Parameters.Add(ImageID);

                //OleDbDataAdapter myAdapter = new OleDbDataAdapter(myCommand);
                //DataSet myDataSet = new DataSet();
                //myAdapter.Fill(myDataSet);

                //string connection = Request.Cookies["Company"].Value;
                
                //DataSet ds = objChk.getImageInfo();
                //if (ds != null)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {

                //        foreach (DataRow dRow in ds.Tables[0].Rows)
                //        {
                //            context.Response.ContentType = dRow["img_type"].ToString();
                //            byte[] imgByte = (byte[])dRow["img_stream"];
                //            MemoryStream memoryStream = new MemoryStream();
                //            memoryStream.Write(imgByte, 0, imgByte.Length);
                //            System.Drawing.Image imagen = System.Drawing.Image.FromStream(memoryStream);
                //            context.Response.BinaryWrite(imgByte);
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
    }
    public bool IsReusable 
    {
        get {
                return false;
            }
    }

}