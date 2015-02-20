using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for clsCompany
/// </summary>
public class clsCompany
{
	public clsCompany()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _company= string.Empty;
    private string _tin = string.Empty;
    private string _cst = string.Empty;
    private string _address = string.Empty;
    private string _city = string.Empty;
    private string _state = string.Empty;
    private string _pincode = string.Empty;
    private string _phone = string.Empty;
    private string _fax = string.Empty;
    private string _email = string.Empty;


    public string Company
    {
        get { return _company; }
        set { _company = value; }
    }
    public string TIN
    {
        get { return _tin; }
        set { _tin = value; }
    }
    public string CST
    {
        get { return _cst; }
        set { _cst = value; }
    }
    public string Address
    {
        get { return _address; }
        set { _address = value; }
    }
    public string City
    {
        get { return _city; }
        set { _city = value; }
    }
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    public string Pincode
    {
        get { return _pincode; }
        set { _pincode = value; }
    }
    public string Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }
    public string Fax
    {
        get { return _fax; }
        set { _fax = value; }
    }
    public string Email
    {
        get { return _email ; }
        set { _email = value; }
    }
}
