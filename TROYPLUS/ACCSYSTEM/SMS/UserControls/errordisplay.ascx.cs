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


    public enum DisplayIcons
    {
        Information,
        Error,
        GreenTick
    }

    public partial class _UserControl_errordisplay : System.Web.UI.UserControl
    {
        #region Private Vars
        private Queue _MessageQueue = new Queue();
        private string _DefaultIcon = ConfigurationManager.AppSettings["defaultInfoIcon"];
        private string _IconLocation = ConfigurationManager.AppSettings["infoIconsLocation"];

        #endregion

        #region Protect Page Vars

        #endregion

        #region Event Handlers
        private void SetListeners()
        {
            this.PreRender += new EventHandler(info_PreRender);

        }

        private void Page_Load(object sender, System.EventArgs e)
        {
        }

        private void info_PreRender(object sender, EventArgs e)
        {
            if (_MessageQueue != null && _MessageQueue.Count > 0)
            {
                messageRepeater.DataSource = _MessageQueue;
                messageRepeater.DataBind();
                messageRepeater.Visible = true;
            }
        }
        #endregion

        #region Queue Handling

        public void ClearList()
        {
            _MessageQueue.Clear();
        }

        public void AddItem(string message)
        {
            AddItem(_DefaultIcon, message);
        }


        public void AddItem(Exception e)
        {
            AddItem(_DefaultIcon, "An Exception has occurred: " + e.Message);
        }

        public void AddItem(string icon, string message)
        {
            _MessageQueue.Enqueue(new MessageObject(ConfigurationManager.AppSettings["infoIconsLocation"] + icon, message));
        }

        public void AddItem(string message, DisplayIcons icon, bool bold)
        {
            if (bold)
            {
                _MessageQueue.Enqueue(new MessageObject(ConfigurationManager.AppSettings["infoIconsLocation"] + GetIconName(icon), "<B>" + message + "</B>"));
            }
            else
            {
                _MessageQueue.Enqueue(new MessageObject(ConfigurationManager.AppSettings["infoIconsLocation"] + GetIconName(icon), message));
            }
        }

        private string GetIconName(DisplayIcons icon)
        {
            switch (icon)
            {
                case DisplayIcons.GreenTick:
                    return "icon_green_tick.gif";
                case DisplayIcons.Error:
                    return "exclamation.png";
                default:
                    return "icon_info.gif";
            }
        }
        #endregion

        #region internal class
        internal class MessageObject
        {
            private string _IconFullName = "";
            private string _Message = "";

            public MessageObject(string iconFullName, string message)
            {
                _IconFullName = iconFullName;
                _Message = message;
            }

            public string IconFullName
            {
                get
                {
                    return _IconFullName;
                }
                set
                {
                    _IconFullName = value;
                }
            }
            public string Message
            {
                get
                {
                    return _Message;
                }
                set
                {
                    _Message = value;
                }
            }
        }
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            SetListeners();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion
    }

