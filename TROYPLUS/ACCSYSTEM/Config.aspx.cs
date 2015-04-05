using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public partial class Config : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            if (!Page.IsPostBack)
            {
                string connStr = string.Empty;

                //txtpassword.Attributes["type"] = "password";
                //GrdViewCust.PageSize = 8;
                //loadEmp();

                BindGrid();

                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                //string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                //dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                //BusinessLogic objChk = new BusinessLogic();



                //string connection = Request.Cookies["Company"].Value;
                //string usernam = Request.Cookies["LoggedUserName"].Value;
                //BusinessLogic bl = new BusinessLogic(sDataSource);

                lnkBtnAdd.Enabled = true;
                //if (bl.CheckUserHaveAdd(usernam, "ULOCK"))
                //{
                //    lnkBtnAdd.Enabled = false;
                //    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                //}
                //else
                //{
                //    lnkBtnAdd.Enabled = true;
                //    lnkBtnAdd.ToolTip = "Click to Add New ";
                //}

                

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BtnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            txtScreenName.Text = "";
            loadEmp();
            BindGrid();

            //ddlSearchCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void CheckSMSRequired()
    {
        DataSet appSettings;
        string smsRequired = string.Empty;
        string emailRequired = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "SMSREQ")
                {
                    smsRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["SMSREQUIRED"] = smsRequired.Trim().ToUpper();
                }
                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "EMAILREQ")
                {
                    emailRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                    Session["EMAILREQUIRED"] = emailRequired.Trim().ToUpper();
                }
                //if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "CREDITEXD")
                //{
                //    Session["CREDITEXD"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString().Trim().ToUpper();
                //    hdCREDITEXD.Value = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString().Trim().ToUpper();
                //}

                if (appSettings.Tables[0].Rows[i]["KEYNAME"].ToString() == "OWNERMOB")
                {
                    Session["OWNERMOB"] = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }

            }
        }

    }

    //protected override void OnInit(EventArgs e)
    //{
    //    base.OnInit(e);
    //    GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
    //    GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtScreenName.UniqueID, "Text"));
    //}

    private void loadEmp()
    {
        //BusinessLogic bl = new BusinessLogic(sDataSource);
        //DataSet ds = new DataSet();

        //ds = bl.ListExecutive();
        //drpIncharge.DataSource = ds;
        //drpIncharge.DataBind();
        //drpIncharge.DataTextField = "empFirstName";
        //drpIncharge.DataValueField = "empno";
    }

    protected void GrdViewCust_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string connection = Request.Cookies["Company"].Value;
        string recondate = string.Empty;
        BusinessLogic bl = new BusinessLogic();

        int Id = Convert.ToInt32(GrdViewCust.DataKeys[e.RowIndex].Value.ToString());

        string usernam = Request.Cookies["LoggedUserName"].Value;

        bl.DeleteScreenConfig(connection, Id, usernam);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Config Details Deleted Successfully')", true);
        BindGrid();
        
    }

    private void BindGrid()
    {
        string connection = Request.Cookies["Company"].Value;

        DataSet ds = new DataSet();
        BusinessLogic bl = new BusinessLogic();

        object usernam = Session["LoggedUserName"];

        string txtSearch = string.Empty;
        ds = bl.GetScreen(connection, txtSearch);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewCust.DataSource = ds.Tables[0].DefaultView;
                GrdViewCust.DataBind();
            }
        }
        else
        {
            GrdViewCust.DataSource = null;
            GrdViewCust.DataBind();
        }
    }

    protected void GrdViewCust_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

            //    if (bl.CheckUserHaveEdit(usernam, "ULOCK"))
            //    {
            //        ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
            //        ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
            //    }

            //    if (bl.CheckUserHaveDelete(usernam, "ULOCK"))
            //    {
            //        ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
            //        ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
            //    }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewCust_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewCust, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void lnkBtnSearch_Click(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string connection = Request.Cookies["Company"].Value;
        string txtSearch = txtScreenName.Text;
        DataSet ds = bl.GetScreen(connection, txtSearch);

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdViewCust.DataSource = ds.Tables[0].DefaultView;
                GrdViewCust.DataBind();
            }
        }
        else
        {
            GrdViewCust.DataSource = null;
            GrdViewCust.DataBind();
        }
    }

    protected void GrdViewCust_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string strPaymode = string.Empty;
            int id = 0;
            GridViewRow row = GrdViewCust.SelectedRow;
            string connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic();

            id = Convert.ToInt32(GrdViewCust.SelectedDataKey.Value);

            DataSet dsd = bl.GetScreenForId(connection,id);

            if (dsd != null)
            {
                if (dsd.Tables[0].Rows.Count > 0)
                {
                    txtEmailContent.Text = dsd.Tables[0].Rows[0]["EmailContent"].ToString();
                    txtSMSContent.Text = dsd.Tables[0].Rows[0]["SMSContent"].ToString();
                    txtEmailSubject.Text = dsd.Tables[0].Rows[0]["EmailSubject"].ToString();
                    
                    if (dsd.Tables[0].Rows[0]["screenid"] != null)
                    {
                        string screenid = Convert.ToString(dsd.Tables[0].Rows[0]["screenid"]);
                        drpScreenName.ClearSelection();
                        ListItem li = drpScreenName.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(screenid));
                        if (li != null) li.Selected = true;

                        drpScreenNo.ClearSelection();
                        ListItem lit = drpScreenNo.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(screenid));
                        if (lit != null) lit.Selected = true;

                    }

                    drpScreenName.Enabled = false;
                    drpScreenNo.Enabled = false;

                    int screentype = 2;
                    DataSet ds = bl.GetConfigSettingsForId(connection, id, screentype);
                    DataSet dst = Settingtab(id);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            GrdViewItem.DataSource = ds;
                            GrdViewItem.DataBind();
                            //ViewState["CurrentTable1"] = dt;

                            for (int vLoop = 0; vLoop < GrdViewItem.Rows.Count; vLoop++)
                            {
                                RadioButtonList txtType = (RadioButtonList)GrdViewItem.Rows[vLoop].FindControl("drpType");
                                DropDownList txtt = (DropDownList)GrdViewItem.Rows[vLoop].FindControl("drpName");
                                TextBox txttname = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtName");
                                TextBox txtt1 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtMobileNo");
                                TextBox txtt2 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtEmailId");
                                Label txtt3 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblMobileNo");
                                Label txtt123 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblEmailId");

                                CheckBox txt3 = (CheckBox)GrdViewItem.Rows[vLoop].FindControl("chkboxMobile");
                                CheckBox txt123 = (CheckBox)GrdViewItem.Rows[vLoop].FindControl("chkboxEmail");

                                DataSet dstt = new DataSet();
                                BusinessLogic bll = new BusinessLogic(sDataSource);
                                txtt.Items.Clear();
                                ListItem lifzzh = new ListItem("Select Name of Person", "0");
                                txtt.Items.Add(lifzzh);
                                dstt = bll.ListExecutive();
                                txtt.DataSource = dstt;
                                txtt.DataBind();
                                txtt.DataTextField = "empFirstName";
                                txtt.DataValueField = "empno";

                                if (ds.Tables[0].Rows[vLoop]["Type"] != null)
                                {
                                    string Type = Convert.ToString(ds.Tables[0].Rows[vLoop]["Type"]);
                                    ListItem lit = txtType.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(Type));
                                    if (lit != null) lit.Selected = true;
                                }

                                if (ds.Tables[0].Rows[vLoop]["Type"].ToString() == "1")
                                {
                                    if (ds.Tables[0].Rows[vLoop]["NameId"] != null)
                                    {
                                        string txttt = Convert.ToString(ds.Tables[0].Rows[vLoop]["NameId"]);
                                        ListItem li = txtt.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(txttt));
                                        if (li != null) li.Selected = true;

                                        txtt3.Text = ds.Tables[0].Rows[vLoop]["MobileNo"].ToString();
                                        txtt123.Text = ds.Tables[0].Rows[vLoop]["EmailId"].ToString();

                                        txtt.Visible = true;
                                        txttname.Visible = false;
                                        txtt3.Visible = true;
                                        txtt123.Visible = true;
                                        txtt1.Visible = false;
                                        txtt2.Visible = false;
                                    }
                                }
                                else
                                {
                                    txtt.Visible = false;
                                    txttname.Visible = true;
                                    txtt3.Visible = false;
                                    txtt123.Visible = false;
                                    txtt1.Visible = true;
                                    txtt2.Visible = true;

                                    txttname.Text = ds.Tables[0].Rows[vLoop]["Name1"].ToString();
                                    txtt1.Text = ds.Tables[0].Rows[vLoop]["MobileNo"].ToString();
                                    txtt2.Text = ds.Tables[0].Rows[vLoop]["EmailId"].ToString();
                                }

                                txt3.Checked = Convert.ToBoolean(ds.Tables[0].Rows[vLoop]["Mobile"].ToString());
                                txt123.Checked = Convert.ToBoolean(ds.Tables[0].Rows[vLoop]["Email"].ToString());
                            }
                        }
                    }





                    screentype = 1;
                    DataSet dstd = bl.GetConfigSettingsForId(connection, id, screentype);
                    DataSet dsttd = Settingtab2(id);

                    if (dstd != null)
                    {
                        if (dstd.Tables[0].Rows.Count > 0)
                        {
                            GridView1.DataSource = dstd;
                            GridView1.DataBind();
                            //ViewState["CurrentTable1"] = dt;

                            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
                            {
                                RadioButtonList txtType = (RadioButtonList)GridView1.Rows[vLoop].FindControl("drpType1");
                                DropDownList txtt = (DropDownList)GridView1.Rows[vLoop].FindControl("drpName1");
                                TextBox txttname = (TextBox)GridView1.Rows[vLoop].FindControl("txtName");
                                TextBox txtt1 = (TextBox)GridView1.Rows[vLoop].FindControl("txtMobileNo");
                                TextBox txtt2 = (TextBox)GridView1.Rows[vLoop].FindControl("txtEmailId");
                                Label txtt3 = (Label)GridView1.Rows[vLoop].FindControl("lblMobileNo");
                                Label txtt123 = (Label)GridView1.Rows[vLoop].FindControl("lblEmailId");

                                CheckBox txt3 = (CheckBox)GridView1.Rows[vLoop].FindControl("chkboxMobile1");
                                CheckBox txt123 = (CheckBox)GridView1.Rows[vLoop].FindControl("chkboxEmail1");

                                if (dstd.Tables[0].Rows[vLoop]["Type"] != null)
                                {
                                    string Type = Convert.ToString(dstd.Tables[0].Rows[vLoop]["Type"]);
                                    ListItem lit = txtType.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(Type));
                                    if (lit != null) lit.Selected = true;
                                }

                                if (dstd.Tables[0].Rows[vLoop]["NameId"] != null)
                                {
                                    string txttt = Convert.ToString(dstd.Tables[0].Rows[vLoop]["NameId"]);
                                    ListItem li = txtt.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(txttt));
                                    if (li != null) li.Selected = true;

                                    //txtt3.Text = dstd.Tables[0].Rows[vLoop]["MobileNo"].ToString();
                                    //txtt123.Text = dstd.Tables[0].Rows[vLoop]["EmailId"].ToString();
                                }
                             

                                txt3.Checked = Convert.ToBoolean(dstd.Tables[0].Rows[vLoop]["Mobile"].ToString());
                                txt123.Checked = Convert.ToBoolean(dstd.Tables[0].Rows[vLoop]["Email"].ToString());
                            }
                        }
                    }

                }
            }
            cmdSave.Visible = false;
            UpdateButton.Visible = true;
            ModalPopupGet.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public DataSet Settingtab(int ID)
    {
        DataSet ds;

        DataSet itemDscom = new DataSet();
        DataTable dt;
        DataRow dr;
        DataColumn dc;
        int p = 0;
        string strRole = string.Empty;
        string roleFlag = string.Empty;
        string strBundles = string.Empty;

        string strItemCode = string.Empty;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string connection = Request.Cookies["Company"].Value;

        int screentype = 2;
        ds = bl.GetConfigSettingsForId(connection, ID, screentype);


        if (ds != null)
        {
            dt = new DataTable();

            dc = new DataColumn("Type");
            dt.Columns.Add(dc);

            dc = new DataColumn("RowNumber");
            dt.Columns.Add(dc);

            dc = new DataColumn("Name");
            dt.Columns.Add(dc);

            dc = new DataColumn("NameId");
            dt.Columns.Add(dc);

            dc = new DataColumn("MobileNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("EmailId");
            dt.Columns.Add(dc);

            dc = new DataColumn("Mobile");
            dt.Columns.Add(dc);

            dc = new DataColumn("Email");
            dt.Columns.Add(dc);
            int sno = 1;

            itemDscom.Tables.Add(dt);
            ViewState["CurrentTable1"] = dt;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    p = p + 1;
                    dr = itemDscom.Tables[0].NewRow();
                    int d = Convert.ToInt32(dR["RowNumber"]);
                    //if (dR["RowNumber"] != null)
                        dr["RowNumber"] = sno;
                    if (dR["Type"] != null)
                        dr["Type"] = Convert.ToInt32(dR["Type"]);
                    if (dR["Name1"] != null)
                        dr["Name"] = Convert.ToString(dR["Name1"]);
                    if (dR["NameId"] != null)
                        dr["NameId"] = Convert.ToInt32(dR["NameId"]);
                    if (dR["MobileNo"] != null)
                        dr["MobileNo"] = Convert.ToString(dR["MobileNo"]);
                    if (dR["EmailId"] != null)
                        dr["EmailId"] = Convert.ToString(dR["EmailId"]);
                    if (dR["Email"] != null)
                        dr["Email"] = Convert.ToString(dR["Email"]);
                    if (dR["Mobile"] != null)
                        dr["Mobile"] = Convert.ToString(dR["Mobile"]);

                    itemDscom.Tables[0].Rows.Add(dr);
                    strRole = "";
                    sno = sno+1;
                }
            }
        }
        return itemDscom;


    }

    public DataSet Settingtab2(int ID)
    {
        DataSet ds;

        DataSet itemDscom = new DataSet();
        DataTable dt;
        DataRow dr;
        DataColumn dc;
        int p = 0;
        string strRole = string.Empty;
        string roleFlag = string.Empty;
        string strBundles = string.Empty;

        string strItemCode = string.Empty;
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string connection = Request.Cookies["Company"].Value;

        int screentype = 1;
        ds = bl.GetConfigSettingsForId(connection, ID, screentype);


        if (ds != null)
        {
            dt = new DataTable();

            dc = new DataColumn("Type");
            dt.Columns.Add(dc);

            dc = new DataColumn("RowNumber");
            dt.Columns.Add(dc);

            dc = new DataColumn("Name");
            dt.Columns.Add(dc);

            dc = new DataColumn("NameId");
            dt.Columns.Add(dc);

            dc = new DataColumn("MobileNo");
            dt.Columns.Add(dc);

            dc = new DataColumn("EmailId");
            dt.Columns.Add(dc);

            dc = new DataColumn("Mobile");
            dt.Columns.Add(dc);

            dc = new DataColumn("Email");
            dt.Columns.Add(dc);
            int sno = 1;

            itemDscom.Tables.Add(dt);
            ViewState["CurrentTable2"] = dt;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dR in ds.Tables[0].Rows)
                {
                    p = p + 1;
                    dr = itemDscom.Tables[0].NewRow();
                    int d = Convert.ToInt32(dR["RowNumber"]);
                    //if (dR["RowNumber"] != null)
                    dr["RowNumber"] = sno;
                    if (dR["Type"] != null)
                        dr["Type"] = Convert.ToInt32(dR["Type"]);
                    if (dR["Name1"] != null)
                        dr["Name"] = Convert.ToString(dR["Name1"]);
                    if (dR["NameId"] != null)
                        dr["NameId"] = Convert.ToInt32(dR["NameId"]);
                    if (dR["MobileNo"] != null)
                        dr["MobileNo"] = Convert.ToString(dR["MobileNo"]);
                    if (dR["EmailId"] != null)
                        dr["EmailId"] = Convert.ToString(dR["EmailId"]);
                    if (dR["Email"] != null)
                        dr["Email"] = Convert.ToString(dR["Email"]);
                    if (dR["Mobile"] != null)
                        dr["Mobile"] = Convert.ToString(dR["Mobile"]);

                    itemDscom.Tables[0].Rows.Add(dr);
                    strRole = "";
                    sno = sno + 1;
                }
            }
        }
        return itemDscom;


    }

    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            
            string connection = string.Empty;
            string userid = string.Empty;

            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;

            drpScreenName.Enabled = true;
            drpScreenNo.Enabled = true;

            drpScreenName.SelectedIndex = 0;
            drpScreenNo.SelectedIndex = 0;
            txtEmailContent.Text = "";

            cmdSave.Visible = true;
            UpdateButton.Visible = false;
            txtEmailSubject.Text = "";

            BusinessLogic objBus = new BusinessLogic();


            //GrdViewItem.DataSource = ds;
            //GrdViewItem.DataBind();

            FirstGridViewRow1();
            FirstGridViewRow2();
            ModalPopupGet.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void ButtonAdd1_Click(object sender, EventArgs e)
    {
        AddNewRow1();

    }

    protected void ButtonAdd2_Click(object sender, EventArgs e)
    {
        AddNewRow2();

    }

    private void AddNewRow1()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {

                    RadioButtonList DrpCreditor =
                     (RadioButtonList)GrdViewItem.Rows[rowIndex].Cells[1].FindControl("drpType");
                    TextBox TextBoxRefNo =
                      (TextBox)GrdViewItem.Rows[rowIndex].Cells[3].FindControl("txtMobileNo");
                    DropDownList DrpBank =
                     (DropDownList)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("drpName");
                    TextBox TextBoxAmount =
                      (TextBox)GrdViewItem.Rows[rowIndex].Cells[5].FindControl("txtEmailId");

                    TextBox TextBoxName =
                      (TextBox)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("txtName");

                    Label TextBoxMobileNo =
                      (Label)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("lblMobileNo");

                    Label TextBoxEmailId =
                      (Label)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("lblEmailId");

                    CheckBox TextBoxMobile =
                      (CheckBox)GrdViewItem.Rows[rowIndex].Cells[4].FindControl("chkboxMobile");

                    CheckBox TextBoxEmail =
                      (CheckBox)GrdViewItem.Rows[rowIndex].Cells[6].FindControl("chkboxEmail");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Type"] = DrpCreditor.SelectedValue;

                    if (DrpCreditor.SelectedValue=="1")
                    {
                        dtCurrentTable.Rows[i - 1]["NameId"] = DrpBank.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["MobileNo"] = TextBoxMobileNo.Text;
                        dtCurrentTable.Rows[i - 1]["EmailId"] = TextBoxEmailId.Text;
                        DrpBank.Visible = true;
                        TextBoxRefNo.Visible = false;
                        TextBoxAmount.Visible = false;                      
                        TextBoxMobileNo.Visible = true;
                        TextBoxEmailId.Visible = true;                                                
                        TextBoxName.Visible = false;
                    }
                    else
                    {
                        DrpBank.Visible = false;
                        TextBoxRefNo.Visible = true;
                        TextBoxAmount.Visible = true;
                        dtCurrentTable.Rows[i - 1]["Name"] = TextBoxName.Text;
                        dtCurrentTable.Rows[i - 1]["MobileNo"] = TextBoxRefNo.Text;
                        dtCurrentTable.Rows[i - 1]["EmailId"] = TextBoxAmount.Text;
                    }

                    dtCurrentTable.Rows[i - 1]["Mobile"] = TextBoxMobile.Checked;

                    dtCurrentTable.Rows[i - 1]["Email"] = TextBoxEmail.Checked;
                    
                   

                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable1"] = dtCurrentTable;

                GrdViewItem.DataSource = dtCurrentTable;
                GrdViewItem.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData1();
    }

    private void AddNewRow2()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable2.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable2.Rows.Count; i++)
                {

                    RadioButtonList DrpCreditor =
                     (RadioButtonList)GridView1.Rows[rowIndex].Cells[1].FindControl("drpType1");
                   
                    DropDownList DrpBank =
                     (DropDownList)GridView1.Rows[rowIndex].Cells[2].FindControl("drpName1");
                   
                    CheckBox TextBoxMobile =
                      (CheckBox)GridView1.Rows[rowIndex].Cells[3].FindControl("chkboxMobile1");

                    CheckBox TextBoxEmail =
                      (CheckBox)GridView1.Rows[rowIndex].Cells[4].FindControl("chkboxEmail1");

                    drCurrentRow = dtCurrentTable2.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable2.Rows[i - 1]["Type"] = DrpCreditor.SelectedValue;         
                    dtCurrentTable2.Rows[i - 1]["NameId"] = DrpBank.SelectedValue;              
                    dtCurrentTable2.Rows[i - 1]["Mobile"] = TextBoxMobile.Checked;

                    dtCurrentTable2.Rows[i - 1]["Email"] = TextBoxEmail.Checked;



                    rowIndex++;
                }
                dtCurrentTable2.Rows.Add(drCurrentRow);
                ViewState["CurrentTable2"] = dtCurrentTable2;

                GridView1.DataSource = dtCurrentTable2;
                GridView1.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData2();
    }

    private void SetPreviousData1()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);

        int rowIndex = 0;
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DropDownList TextBoxBank =
                     (DropDownList)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("drpName");

                    DataSet ds = new DataSet();

                    TextBoxBank.Items.Clear();
                    ListItem lifzzh = new ListItem("Select Name of Person", "0");
                    TextBoxBank.Items.Add(lifzzh);
                    ds = bl.ListExecutive();
                    TextBoxBank.DataSource = ds;
                    TextBoxBank.DataBind();
                    TextBoxBank.DataTextField = "empFirstName";
                    TextBoxBank.DataValueField = "empno";


                    RadioButtonList DrpCreditor =
                     (RadioButtonList)GrdViewItem.Rows[rowIndex].Cells[1].FindControl("drpType");
                    TextBox TextBoxRefNo =
                      (TextBox)GrdViewItem.Rows[rowIndex].Cells[3].FindControl("txtMobileNo");
                   
                    TextBox TextBoxAmount =
                      (TextBox)GrdViewItem.Rows[rowIndex].Cells[5].FindControl("txtEmailId");

                    CheckBox TextBoxMobile =
                      (CheckBox)GrdViewItem.Rows[rowIndex].Cells[4].FindControl("chkboxMobile");

                    CheckBox TextBoxEmail =
                      (CheckBox)GrdViewItem.Rows[rowIndex].Cells[6].FindControl("chkboxEmail");

                    TextBox TextBoxName =
                     (TextBox)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("txtName");

                    Label TextBoxMobileNo =
                      (Label)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("lblMobileNo");

                    Label TextBoxEmailId =
                      (Label)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("lblEmailId");


                    DrpCreditor.SelectedValue = dt.Rows[i]["Type"].ToString();
                    

                    if (DrpCreditor.SelectedValue == "1")
                    {
                        TextBoxBank.SelectedValue = dt.Rows[i]["NameId"].ToString();
                        TextBoxMobileNo.Text = dt.Rows[i]["MobileNo"].ToString();
                        TextBoxEmailId.Text = dt.Rows[i]["EmailId"].ToString();
                        TextBoxBank.Visible = true;
                        TextBoxMobileNo.Visible = true;
                        TextBoxEmailId.Visible = true;
                        TextBoxRefNo.Visible = false;
                        TextBoxAmount.Visible = false;
                        TextBoxName.Visible = false;
                    }
                    else
                    {
                        TextBoxName.Text = dt.Rows[i]["Name"].ToString();
                        TextBoxRefNo.Text = dt.Rows[i]["MobileNo"].ToString();
                        TextBoxAmount.Text = dt.Rows[i]["EmailId"].ToString();
                        TextBoxMobileNo.Visible = false;
                        TextBoxEmailId.Visible = false;
                        TextBoxBank.Visible = false;
                        TextBoxName.Visible = true;
                        TextBoxRefNo.Visible = true;
                        TextBoxAmount.Visible = true;
                       
                    }

                    if (dt.Rows[i]["Mobile"].ToString() != "")
                    {
                        TextBoxMobile.Checked = Convert.ToBoolean(dt.Rows[i]["Mobile"].ToString());
                    }
                    if (dt.Rows[i]["Email"].ToString() != "")
                    {
                        TextBoxEmail.Checked = Convert.ToBoolean(dt.Rows[i]["Email"].ToString());
                    }
                    rowIndex++;

                }
            }
        }
    }

    private void SetPreviousData2()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);

        int rowIndex = 0;
        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable2"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DropDownList TextBoxBank =
                     (DropDownList)GridView1.Rows[rowIndex].Cells[3].FindControl("drpName1");

                   RadioButtonList DrpCreditor =
                     (RadioButtonList)GridView1.Rows[rowIndex].Cells[0].FindControl("drpType1");
                    
                    CheckBox TextBoxMobile =
                      (CheckBox)GridView1.Rows[rowIndex].Cells[3].FindControl("chkboxMobile1");

                    CheckBox TextBoxEmail =
                      (CheckBox)GridView1.Rows[rowIndex].Cells[4].FindControl("chkboxEmail1");


                    DrpCreditor.SelectedValue = dt.Rows[i]["Type"].ToString();
                    
                    TextBoxBank.SelectedValue = dt.Rows[i]["Nameid"].ToString();
                    if (dt.Rows[i]["Mobile"].ToString() != "")
                    {
                        TextBoxMobile.Checked = Convert.ToBoolean(dt.Rows[i]["Mobile"].ToString());
                    }
                    if (dt.Rows[i]["Email"].ToString() != "")
                    {
                        TextBoxEmail.Checked = Convert.ToBoolean(dt.Rows[i]["Email"].ToString());
                    }

                    rowIndex++;

                }
            }
        }
    }

    protected void GrdViewItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //BusinessLogic bl = new BusinessLogic(sDataSource);
            //DataSet ds = new DataSet();

            //ds = bl.ListBanks();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //var ddl = (DropDownList)e.Row.FindControl("drpBank");
                //ddl.Items.Clear();
                //ListItem lifzzh = new ListItem("Select Ledger", "0");
                //lifzzh.Attributes.Add("style", "color:#006699");
                //ddl.Items.Add(lifzzh);
                //ddl.DataSource = ds;
                //ddl.Items[0].Attributes.Add("background-color", "color:White");
                //ddl.DataBind();
                //ddl.DataTextField = "LedgerName";
                //ddl.DataValueField = "LedgerID";

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet ds = new DataSet();

            //ds = bl.ListBanks();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //var ddl = (DropDownList)e.Row.FindControl("drpBank");
                //ddl.Items.Clear();
                //ListItem lifzzh = new ListItem("Select Ledger", "0");
                //lifzzh.Attributes.Add("style", "color:#006699");
                //ddl.Items.Add(lifzzh);
                //ddl.DataSource = ds;
                //ddl.Items[0].Attributes.Add("background-color", "color:White");
                //ddl.DataBind();
                //ddl.DataTextField = "LedgerName";
                //ddl.DataValueField = "LedgerID";

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData1();
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable1"] = dt;
                GrdViewItem.DataSource = dt;
                GrdViewItem.DataBind();




                for (int i = 0; i < GrdViewItem.Rows.Count; i++)
                {
                    GrdViewItem.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData1();

            }
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData2();
        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dt1 = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow1 = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt1.Rows.Count > 1)
            {
                dt1.Rows.Remove(dt1.Rows[rowIndex]);
                drCurrentRow1 = dt1.NewRow();
                ViewState["CurrentTable2"] = dt1;
                GridView1.DataSource = dt1;
                GridView1.DataBind();




                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridView1.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData2();

            }
        }
    }

    private void SetRowData1()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {

                    RadioButtonList DrpCreditor =
                     (RadioButtonList)GrdViewItem.Rows[rowIndex].Cells[1].FindControl("drpType");
                    TextBox TextBoxRefNo =
                      (TextBox)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("txtMobileNo");
                    DropDownList TextBoxBank =
                      (DropDownList)GrdViewItem.Rows[rowIndex].Cells[3].FindControl("drpName");
                    TextBox TextBoxAmount =
                      (TextBox)GrdViewItem.Rows[rowIndex].Cells[4].FindControl("txtEmailId");

                    TextBox TextBoxName =
                     (TextBox)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("txtName");

                    Label TextBoxMobileNo =
                      (Label)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("lblMobileNo");

                    Label TextBoxEmailId =
                      (Label)GrdViewItem.Rows[rowIndex].Cells[2].FindControl("lblEmailId");

                    CheckBox TextBoxMobile =
                      (CheckBox)GrdViewItem.Rows[rowIndex].Cells[4].FindControl("chkboxMobile");

                    CheckBox TextBoxEmail =
                      (CheckBox)GrdViewItem.Rows[rowIndex].Cells[6].FindControl("chkboxEmail");


                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Type"] = DrpCreditor.SelectedValue;
                    if (DrpCreditor.SelectedValue == "1")
                    {
                        dtCurrentTable.Rows[i - 1]["NameId"] = TextBoxBank.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["MobileNo"] = TextBoxMobileNo.Text;
                        dtCurrentTable.Rows[i - 1]["EmailId"] = TextBoxEmailId.Text;
                        TextBoxName.Visible=false;
                            TextBoxRefNo.Visible=false;
                            TextBoxAmount.Visible = false;
                        TextBoxBank.Visible = true;
                        TextBoxMobileNo.Visible = true;
                        TextBoxEmailId.Visible = true;
                    }
                    else
                    {
                        TextBoxName.Visible = true;
                        TextBoxRefNo.Visible = true;
                        TextBoxAmount.Visible = true;
                        TextBoxBank.Visible = false;
                        TextBoxMobileNo.Visible = false;
                        TextBoxEmailId.Visible = false;
                        dtCurrentTable.Rows[i - 1]["Name"] = TextBoxName.Text;
                        dtCurrentTable.Rows[i - 1]["MobileNo"] = TextBoxRefNo.Text;
                        dtCurrentTable.Rows[i - 1]["EmailId"] = TextBoxAmount.Text;
                    }

                    dtCurrentTable.Rows[i - 1]["Mobile"] = TextBoxMobile.Checked;
                    dtCurrentTable.Rows[i - 1]["Email"] = TextBoxEmail.Checked;
                
                    rowIndex++;

                }

                ViewState["CurrentTable1"] = dtCurrentTable;
                GrdViewItem.DataSource = dtCurrentTable;
                GrdViewItem.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData1();
    }

    private void SetRowData2()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable2"] != null)
        {
            DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable2"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable2.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable2.Rows.Count; i++)
                {

                    RadioButtonList DrpCreditor =
                     (RadioButtonList)GridView1.Rows[rowIndex].Cells[1].FindControl("drpType1");
                    
                    DropDownList TextBoxBank =
                      (DropDownList)GridView1.Rows[rowIndex].Cells[3].FindControl("drpName1");
                   
                    CheckBox TextBoxMobile =
                      (CheckBox)GridView1.Rows[rowIndex].Cells[4].FindControl("chkboxMobile1");

                    CheckBox TextBoxEmail =
                      (CheckBox)GridView1.Rows[rowIndex].Cells[6].FindControl("chkboxEmail1");

                    drCurrentRow = dtCurrentTable2.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable2.Rows[i - 1]["Type"] = DrpCreditor.SelectedValue;                         
                    dtCurrentTable2.Rows[i - 1]["NameId"] = TextBoxBank.SelectedValue;                    
                    dtCurrentTable2.Rows[i - 1]["Mobile"] = TextBoxMobile.Checked;

                    dtCurrentTable2.Rows[i - 1]["Email"] = TextBoxEmail.Checked;

                    rowIndex++;

                }

                ViewState["CurrentTable2"] = dtCurrentTable2;
                GridView1.DataSource = dtCurrentTable2;
                GridView1.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData2();
    }

    private void FirstGridViewRow1()
    {
        DataTable dtt = new DataTable();
        DataRow dr = null;
        dtt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dtt.Columns.Add(new DataColumn("Type", typeof(string)));
        dtt.Columns.Add(new DataColumn("Name", typeof(string)));
        dtt.Columns.Add(new DataColumn("NameId", typeof(string)));
        dtt.Columns.Add(new DataColumn("MobileNo", typeof(string)));
        dtt.Columns.Add(new DataColumn("EmailId", typeof(string)));
        dtt.Columns.Add(new DataColumn("Mobile", typeof(string)));
        dtt.Columns.Add(new DataColumn("Email", typeof(string)));
        dr = dtt.NewRow();
        dr["RowNumber"] = 1;
        dr["Type"] = string.Empty;
        dr["Name"] = string.Empty;
        dr["NameId"] = string.Empty;
        dr["MobileNo"] = string.Empty;
        dr["EmailId"] = string.Empty;
        dr["Mobile"] = string.Empty;
        dr["Email"] = string.Empty;
        dtt.Rows.Add(dr);

        ViewState["CurrentTable1"] = dtt;

        BusinessLogic bl = new BusinessLogic(sDataSource);

       
        GrdViewItem.DataSource = dtt;
        GrdViewItem.DataBind();

        for (int vLoop = 0; vLoop < GrdViewItem.Rows.Count; vLoop++)
        {
            RadioButtonList txt = (RadioButtonList)GrdViewItem.Rows[vLoop].FindControl("drpType");
            DropDownList txtt = (DropDownList)GrdViewItem.Rows[vLoop].FindControl("drpName");
            TextBox txttname = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtName");
            TextBox txtt1 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtMobileNo");
            TextBox txtt2 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtEmailId");
            Label txtt3 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblMobileNo");
            Label txtt123 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblEmailId");
            txt.SelectedValue = "1";

            if (txt.SelectedValue == "1")
            {
                txtt1.Visible = false;
                txtt2.Visible = false;
                txttname.Visible = false;
                txtt3.Visible = true;
                txtt123.Visible = true;
                txtt.Visible = true;

                DataSet ds = new DataSet();

                txtt.Items.Clear();
                ListItem lifzzh = new ListItem("Select Name of Person", "0");
                txtt.Items.Add(lifzzh);
                ds = bl.ListExecutive();
                txtt.DataSource = ds;
                txtt.DataBind();
                txtt.DataTextField = "empFirstName";
                txtt.DataValueField = "empno";


            }
            else
            {
                txtt.Visible = false;
                txttname.Visible = true;
                txtt1.Visible = true;
                txtt2.Visible = true;
                txtt3.Visible = false;
                txtt123.Visible = false;
            }
        }
    }

    private void FirstGridViewRow2()
    {
        DataTable dtt = new DataTable();
        DataRow dr = null;
        dtt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dtt.Columns.Add(new DataColumn("Type", typeof(string)));
        dtt.Columns.Add(new DataColumn("Name", typeof(string)));
        dtt.Columns.Add(new DataColumn("NameId", typeof(string)));
        dtt.Columns.Add(new DataColumn("MobileNo", typeof(string)));
        dtt.Columns.Add(new DataColumn("EmailId", typeof(string)));
        dtt.Columns.Add(new DataColumn("Mobile", typeof(string)));
        dtt.Columns.Add(new DataColumn("Email", typeof(string)));
        dr = dtt.NewRow();
        dr["RowNumber"] = 1;
        dr["Type"] = string.Empty;
        dr["Name"] = string.Empty;
        dr["NameId"] = string.Empty;
        dr["MobileNo"] = string.Empty;
        dr["EmailId"] = string.Empty;
        dr["Mobile"] = string.Empty;
        dr["Email"] = string.Empty;

        dtt.Rows.Add(dr);

        ViewState["CurrentTable2"] = dtt;

        BusinessLogic bl = new BusinessLogic(sDataSource);


        GridView1.DataSource = dtt;
        GridView1.DataBind();

    }

    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupGet.Hide();
            //GrdViewCust.DataBind();
            //UpdatePanelPage.Update();

            BindGrid();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        try
        {

            bool Emaild;
            bool Mobiled;

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                DropDownList txtt = (DropDownList)GridView1.Rows[vLoop].FindControl("drpName1");

                CheckBox txt = (CheckBox)GridView1.Rows[vLoop].FindControl("chkboxEmail1");
                if (txt.Checked)
                {
                    Emaild = txt.Checked;
                }
                else
                {
                    Emaild = false;
                }

                CheckBox txttt = (CheckBox)GridView1.Rows[vLoop].FindControl("chkboxMobile1");
                if (txttt.Checked)
                {
                    Mobiled = txttt.Checked;
                }
                else
                {
                    Mobiled = false;
                }

                int col = vLoop + 1;

                if ((Emaild == true) || (Mobiled == true))
                {
                    if (txtt.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Name in row " + col + " ')", true);
                        return;
                    }
                }
            }

            bool Email = false;
            bool Mobile = false;

            DataSet dsttN;
            DataTable dttN;
            DataRow drNewtN;

            DataColumn dctN;

            dsttN = new DataSet();

            dttN = new DataTable();

            dctN = new DataColumn("Type");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("Row");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("Name");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("NameId");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("MobileNo");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("EmailId");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("Mobile");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("Email");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("ScreenType");
            dttN.Columns.Add(dctN);

            dsttN.Tables.Add(dttN);

            int sno1 = 1;

            for (int vLoop = 0; vLoop < GrdViewItem.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GrdViewItem.Rows[vLoop].FindControl("chkboxEmail");
                if (txt.Checked)
                {
                    Email = txt.Checked;
                }
                else
                {
                    Email = false;
                }

                CheckBox txttt = (CheckBox)GrdViewItem.Rows[vLoop].FindControl("chkboxMobile");
                if (txttt.Checked)
                {
                    Mobile = txttt.Checked;
                }
                else
                {
                    Mobile = false;
                }

                RadioButtonList txtType = (RadioButtonList)GrdViewItem.Rows[vLoop].FindControl("drpType");
                DropDownList txtt = (DropDownList)GrdViewItem.Rows[vLoop].FindControl("drpName");
                TextBox txttname = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtName");
                TextBox txtt1 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtMobileNo");
                TextBox txtt2 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtEmailId");
                Label txtt3 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblMobileNo");
                Label txtt123 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblEmailId");

                drNewtN = dttN.NewRow();
                drNewtN["Row"] = sno1;

                drNewtN["Type"] = txtType.SelectedValue;
                if (txtType.SelectedValue == "1")
                {
                    drNewtN["Name"] = txtt.SelectedItem.Text;
                    drNewtN["NameId"] = txtt.SelectedValue;
                    drNewtN["MobileNo"] = txtt3.Text;
                    drNewtN["EmailId"] = txtt123.Text;
                }
                else
                {
                    drNewtN["Name"] = txttname.Text;
                    drNewtN["NameId"] = 0;
                    drNewtN["MobileNo"] = txtt1.Text;
                    drNewtN["EmailId"] = txtt2.Text;
                }

                drNewtN["Mobile"] = Mobile;
                drNewtN["Email"] = Email;
                drNewtN["ScreenType"] = 2;
                dsttN.Tables[0].Rows.Add(drNewtN);
                sno1 = sno1 + 1;
            }

            string Userna = Request.Cookies["LoggedUserName"].Value;
            string connection = string.Empty;

            int screenno = 0;
            string screenname = string.Empty;
            int screenid = 0;
            string emailcontent = string.Empty;
            string smscontent = string.Empty;
            string emailsubject = string.Empty;

            screenno = Convert.ToInt32(drpScreenNo.SelectedItem.Text);
            screenid = Convert.ToInt32(drpScreenName.SelectedValue);
            screenname = drpScreenName.SelectedItem.Text;
            smscontent = txtSMSContent.Text;
            emailsubject = txtEmailSubject.Text;
            emailcontent = txtEmailContent.Text;

            connection = Request.Cookies["Company"].Value;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            string usernam = Request.Cookies["LoggedUserName"].Value;

            int Id = int.Parse(GrdViewCust.SelectedDataKey.Value.ToString());



            bool Email1 = false;
            bool Mobile1 = false;

            DataSet dsttNN;
            DataTable dttNN;
            DataRow drNewtNN;

            DataColumn dctNN;

            dsttNN = new DataSet();

            dttNN = new DataTable();

            dctNN = new DataColumn("Type");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("Row");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("Name");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("NameId");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("MobileNo");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("EmailId");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("Mobile");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("Email");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("ScreenType");
            dttNN.Columns.Add(dctNN);

            dsttNN.Tables.Add(dttNN);

            int sno2 = 1;

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridView1.Rows[vLoop].FindControl("chkboxEmail1");
                if (txt.Checked)
                {
                    Email = txt.Checked;
                }
                else
                {
                    Email = false;
                }

                CheckBox txttt = (CheckBox)GridView1.Rows[vLoop].FindControl("chkboxMobile1");
                if (txttt.Checked)
                {
                    Mobile = txttt.Checked;
                }
                else
                {
                    Mobile = false;
                }

                RadioButtonList txtType = (RadioButtonList)GridView1.Rows[vLoop].FindControl("drpType1");
                DropDownList txtt = (DropDownList)GridView1.Rows[vLoop].FindControl("drpName1");


                drNewtNN = dttNN.NewRow();
                drNewtNN["Row"] = sno1;
                drNewtNN["Type"] = 2;
                drNewtNN["NameId"] = txtt.SelectedValue;
                drNewtNN["Name"] = txtt.SelectedItem.Text;
                drNewtNN["Mobile"] = Mobile;
                drNewtNN["Email"] = Email;
                drNewtNN["ScreenType"] = 1;
                dsttNN.Tables[0].Rows.Add(drNewtNN);
                sno2 = sno2 + 1;
            }



            bl.UpdateScreenConfig(connection, screenno, screenid, screenname, smscontent, emailsubject, emailcontent, dsttN, usernam, Id, dsttNN);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Config details Updated Successfully.');", true);

            BindGrid();

            ModalPopupGet.Hide();
            GrdViewCust.DataBind();
            UpdatePanelPage.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);

            if (bl.CheckIfScreenDuplicate(drpScreenName.SelectedItem.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Screen - " + drpScreenName.SelectedItem.Text + " - already exists.');", true);
                return;
            }
            bool Emaild;
            bool Mobiled;

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                DropDownList txtt = (DropDownList)GridView1.Rows[vLoop].FindControl("drpName1");

                CheckBox txt = (CheckBox)GridView1.Rows[vLoop].FindControl("chkboxEmail1");
                if (txt.Checked)
                {
                    Emaild = txt.Checked;
                }
                else
                {
                    Emaild = false;
                }

                CheckBox txttt = (CheckBox)GridView1.Rows[vLoop].FindControl("chkboxMobile1");
                if (txttt.Checked)
                {
                    Mobiled = txttt.Checked;
                }
                else
                {
                    Mobiled = false;
                }

                int col = vLoop + 1;

                if ((Emaild == true) || (Mobiled == true))
                {
                    if (txtt.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Name in row " + col + " ')", true);
                        return;
                    }
                }
            }


            int sno = 1;
            for (int vLoop = 0; vLoop < GrdViewItem.Rows.Count; vLoop++)
            {
               
                RadioButtonList txtType = (RadioButtonList)GrdViewItem.Rows[vLoop].FindControl("drpType");
                DropDownList txtt = (DropDownList)GrdViewItem.Rows[vLoop].FindControl("drpName");
                TextBox txttname = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtName");
                TextBox txtt1 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtMobileNo");
                TextBox txtt2 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtEmailId");
                Label txtt3 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblMobileNo");
                Label txtt123 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblEmailId");

                int col = vLoop + 1;

                bool isEmail = Regex.IsMatch(txtt123.Text.Trim(), @"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?)\Z");
                bool isEmail1 = Regex.IsMatch(txtt2.Text.Trim(), @"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?)\Z");

                if (txtType.SelectedValue == "1")
                {
                    if (!isEmail)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email Id is invalid in row " + col + "')", true);
                        return;
                    }
                    if (txtt.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Name in row " + col + " ')", true);
                        return;
                    }
                    else if (txtt3.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Mobile No in row " + col + " ')", true);
                        return;
                    }
                    else if (txtt123.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Email Id in row " + col + " ')", true);
                        return;
                    }
                }
                else if (txtType.SelectedValue == "2")
                {
                    if (!isEmail1)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email Id is invalid in row " + col + "')", true);
                        return;
                    }
                    if (txttname.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Name in row " + col + " ')", true);
                        return;
                    }
                    else if (txtt1.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Mobile No in row " + col + " ')", true);
                        return;
                    }
                    else if (txtt2.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please fill Email Id in row " + col + " ')", true);
                        return;
                    }
                }

                

                sno = sno+1;
            }





            bool Email = false;
            bool Mobile = false;
        
            DataSet dsttN;
            DataTable dttN;
            DataRow drNewtN;

            DataColumn dctN;

            dsttN = new DataSet();

            dttN = new DataTable();

            dctN = new DataColumn("Type");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("Row");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("Name");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("NameId");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("MobileNo");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("EmailId");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("Mobile");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("Email");
            dttN.Columns.Add(dctN);

            dctN = new DataColumn("ScreenType");
            dttN.Columns.Add(dctN);

            dsttN.Tables.Add(dttN);

            int sno1 = 1;

            for (int vLoop = 0; vLoop < GrdViewItem.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GrdViewItem.Rows[vLoop].FindControl("chkboxEmail");
                if (txt.Checked)
                {
                    Email = txt.Checked;
                }
                else
                {
                    Email = false;
                }

                CheckBox txttt = (CheckBox)GrdViewItem.Rows[vLoop].FindControl("chkboxMobile");
                if (txttt.Checked)
                {
                    Mobile = txttt.Checked;
                }
                else
                {
                    Mobile = false;
                }

                RadioButtonList txtType = (RadioButtonList)GrdViewItem.Rows[vLoop].FindControl("drpType");
                DropDownList txtt = (DropDownList)GrdViewItem.Rows[vLoop].FindControl("drpName");
                TextBox txttname = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtName");
                TextBox txtt1 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtMobileNo");
                TextBox txtt2 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtEmailId");
                Label txtt3 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblMobileNo");
                Label txtt123 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblEmailId");
                
                drNewtN = dttN.NewRow();
                drNewtN["Row"] = sno1;

                drNewtN["Type"] = txtType.SelectedValue;
                if (txtType.SelectedValue=="1")
                {
                    drNewtN["Name"] = txtt.SelectedItem.Text;
                    drNewtN["NameId"] = txtt.SelectedValue;
                    drNewtN["MobileNo"] = txtt3.Text;
                    drNewtN["EmailId"] = txtt123.Text;
                }
                else
                {
                    drNewtN["Name"] = txttname.Text;
                    drNewtN["NameId"] = 0;
                    drNewtN["MobileNo"] = txtt1.Text;
                    drNewtN["EmailId"] = txtt2.Text;
                }
               
                drNewtN["Mobile"] = Mobile;
                drNewtN["Email"] = Email;
                drNewtN["ScreenType"] = 2;
                dsttN.Tables[0].Rows.Add(drNewtN);
                sno1 = sno1 + 1;
            }
 
            string Userna = Request.Cookies["LoggedUserName"].Value;
            string connection = string.Empty;

            int screenno = 0;
            string screenname = string.Empty;
            int screenid = 0;
            string emailcontent = string.Empty;
            string smscontent = string.Empty;
            string emailsubject = string.Empty;

            screenno = Convert.ToInt32(drpScreenNo.SelectedItem.Text);
            screenid = Convert.ToInt32(drpScreenName.SelectedValue);
            screenname = drpScreenName.SelectedItem.Text;
            smscontent = txtSMSContent.Text;
            emailsubject = txtEmailSubject.Text;
            emailcontent = txtEmailContent.Text;

            connection = Request.Cookies["Company"].Value;
            
            string usernam = Request.Cookies["LoggedUserName"].Value;




            bool Email1 = false;
            bool Mobile1 = false;

            DataSet dsttNN;
            DataTable dttNN;
            DataRow drNewtNN;

            DataColumn dctNN;

            dsttNN = new DataSet();

            dttNN = new DataTable();

            dctNN = new DataColumn("Type");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("Row");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("Name");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("NameId");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("MobileNo");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("EmailId");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("Mobile");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("Email");
            dttNN.Columns.Add(dctNN);

            dctNN = new DataColumn("ScreenType");
            dttNN.Columns.Add(dctNN);

            dsttNN.Tables.Add(dttNN);

            int sno2 = 1;

            for (int vLoop = 0; vLoop < GridView1.Rows.Count; vLoop++)
            {
                CheckBox txt = (CheckBox)GridView1.Rows[vLoop].FindControl("chkboxEmail1");
                if (txt.Checked)
                {
                    Email = txt.Checked;
                }
                else
                {
                    Email = false;
                }

                CheckBox txttt = (CheckBox)GridView1.Rows[vLoop].FindControl("chkboxMobile1");
                if (txttt.Checked)
                {
                    Mobile = txttt.Checked;
                }
                else
                {
                    Mobile = false;
                }

                RadioButtonList txtType = (RadioButtonList)GridView1.Rows[vLoop].FindControl("drpType1");
                DropDownList txtt = (DropDownList)GridView1.Rows[vLoop].FindControl("drpName1");

                if (txtt.SelectedItem.Text == "Select Replacement Name")
                {
                }
                else
                {
                    drNewtNN = dttNN.NewRow();
                    drNewtNN["Row"] = sno1;
                    drNewtNN["Type"] = 2;
                    drNewtNN["NameId"] = txtt.SelectedValue;
                    drNewtNN["Name"] = txtt.SelectedItem.Text;
                    drNewtNN["Mobile"] = Mobile;
                    drNewtNN["Email"] = Email;
                    drNewtNN["ScreenType"] = 1;
                    dsttNN.Tables[0].Rows.Add(drNewtNN);
                    sno2 = sno2 + 1;
                }
            }








            bl.InsertScreenConfig(connection, screenno, screenid, screenname, smscontent, emailsubject, emailcontent, dsttN, usernam, dsttNN);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Config details Saved Successfully.');", true);

            BindGrid();

            ModalPopupGet.Hide();
            GrdViewCust.DataBind();
            UpdatePanelPage.Update();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void drpScreenNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        int iID = Convert.ToInt32(drpScreenNo.SelectedValue);
        string connection = Request.Cookies["Company"].Value;

        DataSet ScreenDs = bl.GetScreenForScreenId(connection, iID);

        if (ScreenDs != null && ScreenDs.Tables[0].Rows.Count > 0)
        {
            drpScreenName.ClearSelection();
            ListItem lit = drpScreenName.Items.FindByValue(Convert.ToString(iID));
            if (lit != null) lit.Selected = true;
        }
        else
        {

        }
    }

    protected void drpScreenName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        int iID = Convert.ToInt32(drpScreenName.SelectedValue);
        string connection = Request.Cookies["Company"].Value;

        DataSet ScreenDs = bl.GetScreenForScreenId(connection,iID);

        if (ScreenDs != null && ScreenDs.Tables[0].Rows.Count > 0)
        {
            drpScreenNo.ClearSelection();
            ListItem lit = drpScreenNo.Items.FindByValue(Convert.ToString(iID));
            if (lit != null) lit.Selected = true;
        }
        else
        {
          
        }
    }

    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);

        for (int vLoop = 0; vLoop < GrdViewItem.Rows.Count; vLoop++)
        {
            RadioButtonList txt = (RadioButtonList)GrdViewItem.Rows[vLoop].FindControl("drpType");
            DropDownList txtt = (DropDownList)GrdViewItem.Rows[vLoop].FindControl("drpName");
            TextBox txttname = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtName");
            TextBox txtt1 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtMobileNo");
            TextBox txtt2 = (TextBox)GrdViewItem.Rows[vLoop].FindControl("txtEmailId");
            Label txtt3 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblMobileNo");
            Label txtt123 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblEmailId");

            if (txt.SelectedValue == "1")
            {
                txtt1.Visible = false;
                txtt2.Visible = false;
                txttname.Visible = false;
                txtt3.Visible = true;
                txtt123.Visible = true;
                txtt.Visible = true;

                //DataSet ds = new DataSet();

                //txtt.Items.Clear();
                //ListItem lifzzh = new ListItem("Select Name of Person", "0");
                //txtt.Items.Add(lifzzh);
                //ds = bl.ListExecutive();
                //txtt.DataSource = ds;
                //txtt.DataBind();
                //txtt.DataTextField = "empFirstName";
                //txtt.DataValueField = "empno";


            }
            else
            {
                txtt.Visible = false;
                txttname.Visible = true;
                txtt1.Visible = true;
                txtt2.Visible = true;
                txtt3.Visible = false;
                txtt123.Visible = false;
            }
        }



    }

    protected void drpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        string connection = "";
        if (Request.Cookies["Company"] != null)
            connection = Request.Cookies["Company"].Value;
 
 
        for (int vLoop = 0; vLoop < GrdViewItem.Rows.Count; vLoop++)
        {
            DropDownList txt = (DropDownList)GrdViewItem.Rows[vLoop].FindControl("drpName");
            Label txtt3 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblMobileNo");
            Label txtt123 = (Label)GrdViewItem.Rows[vLoop].FindControl("lblEmailId");

            txtt3.Enabled = true;
            txtt123.Enabled = true;

            DataSet ds = bl.ListExecutiveEmpNo(connection, Convert.ToInt32(txt.SelectedValue));

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtt3.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    txtt123.Text = ds.Tables[0].Rows[0]["EmailId"].ToString();
                }
            }
        }

    }

    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (GrdViewCust.SelectedDataKey != null)
                e.InputParameters["username"] = Convert.ToInt32(GrdViewCust.SelectedDataKey.Value);

            string connection="";
            if (Request.Cookies["Company"] != null)
               connection = Request.Cookies["Company"].Value;

            BusinessLogic objBL = new BusinessLogic();

            objBL = new BusinessLogic(ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString());

          
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

}
