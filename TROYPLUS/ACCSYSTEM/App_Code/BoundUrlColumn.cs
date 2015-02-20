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
/// Summary description for BoundUrlColumn
/// </summary>
/// 
namespace UrlNameSpace
{
    public class BoundUrlColumn : BoundField
    {
        public BoundUrlColumn(): base()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private string mbaseUrl;
        private string mIconPath;
        private string mTooltip;

        #region Properties
        [
        Bindable(true),
        DefaultValue(""),
        Description("Base Url to Navigate to"),
        DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)
        ]
        public virtual string BaseUrl
        {
            get { return mbaseUrl; }
            set { mbaseUrl = value; }
        }

        [
        Bindable(true),
        DefaultValue(""),
        Description("Path to icon to display"),
        DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public virtual string IconPath
        {
            get
            {
                return mIconPath;
            }
            set
            {
                mIconPath = value;
            }
        }

        [
        Bindable(true),
        DefaultValue(""),
        Description("Tooltip to display"),
        DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public virtual string Tooltip
        {
            get
            {
                return mTooltip;
            }
            set
            {
                mTooltip = value;
            }
        }

        #endregion

        protected override string FormatDataValue(object dataValue, bool encode)
        {
            //string newValue = "<a href=\"" + mbaseUrl + dataValue.ToString() + "\"></a>";
            string newValue = "<a href=\"" + mbaseUrl + dataValue.ToString().Replace("'","''") + "\" width=50 >  <img border=0 src=\"" + mIconPath + "\" title=\"" + mTooltip + "\" />  </a>";
            return newValue;
        }
    }
}