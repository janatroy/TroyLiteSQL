using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BaseLogic
/// </summary>
public class BaseLogic
{
    private string _connectionstring = string.Empty;

	public BaseLogic()
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

    public BaseLogic(string con)
    {
        //
        // TODO: Add constructor logic here
        this.ConnectionString = con;
        //
    }

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

        return newConnection;

    }

    private string GetPasswordForDB(string connStr)
    {
        string connection = string.Empty;

        string encrptedString = connStr.Remove(0, connStr.Remove(connStr.LastIndexOf("Password=") + 9).Length);

        connection = EncryptDecrypt.DecryptString(encrptedString, "Q£PW&*M");

        return connection;

    }
}