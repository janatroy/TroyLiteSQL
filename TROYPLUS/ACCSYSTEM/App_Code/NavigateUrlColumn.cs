using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
/// <summary>
/// Summary description for NavigateUrlColumn
/// </summary>
/// 
namespace UrlNameSpace
{
    public class NavigateUrlColumn : BoundField
    {
        public NavigateUrlColumn()
            : base()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private string mbaseUrl;
        private string mtext;

        public string Text
        {
            get { return mtext; }
            set { mtext = value; }
        }

        public string BaseUrl
        {
            get { return mbaseUrl; }
            set { mbaseUrl = value; }
        }

        protected override string FormatDataValue(object dataValue, bool encode)
        {
            string newValue = "<a href=\"" + mbaseUrl + dataValue.ToString() + "\">"+mtext+"</a>";
            return newValue;
        }

    }
}
