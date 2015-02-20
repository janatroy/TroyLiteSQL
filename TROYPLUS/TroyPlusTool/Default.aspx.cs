using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TroyPlusTool
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtKey.Text = "";
                txtEncryptedKey.Text = "";
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            txtEncryptedKey.Text = EncryptDecrypt.Encrypt(txtKey.Text.Trim());
        }
    }
}