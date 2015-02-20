using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string SetSelectedLink(string linkURL)
    {
        string url = Request.Url.ToString();
        char[] sep = { '/' };

        string[] sArrProdID = url.Split(sep);

        string outstr;

        if (sArrProdID[sArrProdID.Length - 1].ToLower().Contains(linkURL.ToLower()))
        {
            outstr = "current";
        }
        else
        {
            outstr = " ";
        }
        return outstr;

    }

}
