using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;
using System.Data.OleDb;

public partial class retrieveImages : System.Web.UI.Page
{
    private string sDataSource = string.Empty;

    // Access Database oledb connection string
    // Using Provider Microsoft.Jet.OLEDB.4.0
    //String connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath("App_Data/db1.mdb");

    BusinessLogic objChk = new BusinessLogic();

            

    // Object created for Oledb Connection
    //OleDbConnection myAccessConnection;

    //protected void openAccessConnection()
    //{
    //    // If condition that can be used to check the access database connection
    //    // whether it is already open or not.
    //    if (myAccessConnection.State == ConnectionState.Closed)
    //    {
    //        myAccessConnection.Open();
    //    }
    //}

    //protected void closeAccessConnection()
    //{
    //    // If condition to check the access database connection state
    //    // If it is open then close it.
    //    if (myAccessConnection.State == ConnectionState.Open)
    //    {
    //        myAccessConnection.Close();
    //    }

    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

        //myAccessConnection = new OleDbConnection(connStr);
        using (OleDbConnection connection = new OleDbConnection(objChk.CreateConnectionString(sDataSource)))
        {
            OleDbCommand command = new OleDbCommand();
            OleDbTransaction transaction = null;
            OleDbDataAdapter adapter = null;

            // Set the Connection to the new OleDbConnection.
            command.Connection = connection;

            try
            {
                string ID = Request.QueryString["id"];

                connection.Open();

                // Start a local transaction with ReadCommitted isolation level.
                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                // Assign transaction object for a pending local transaction.
                command.Connection = connection;
                command.Transaction = transaction;

                //command.CommandText = string.Format("select * from tblImg where img_id=@img_id");

                //OleDbParameter img_id = new OleDbParameter("@img_id", OleDbType.Integer);
                //img_id.Value = ID;
                //command.Parameters.Add(img_id);

                ////command.ExecuteNonQuery();

                //OleDbDataAdapter myAdapter = new OleDbDataAdapter(command);
                //DataSet myDataSet = new DataSet();
                //myAdapter.Fill(myDataSet);

                //DataSet ds = objChk.getImageInfo(sDataSource, ID);
                //if (ds != null)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {


                //        foreach (DataRow dRow in ds.Tables[0].Rows)
                //        {
                //            Response.ContentType = dRow["img_type"].ToString();
                //            byte[] imageContent = (byte[])((dRow["img_stream"]));
                //            Response.BinaryWrite(imageContent);
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                TroyLiteExceptionManager.HandleException(ex);
            }
        }
        
        
    }
}
