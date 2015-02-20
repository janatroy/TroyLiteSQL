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
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;


public partial class UserLock : System.Web.UI.Page
{  

    private string sDataSource = string.Empty;
    private double amtTotal = 0;
    double disTotalRate = 0.0;
    public double rateTotal = 0;
    public double vatTotal = 0;
    public double disTotal = 0;
    public double cstTotal = 0;
    public double WholeTotal;
    string BarCodeRequired = string.Empty;   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
            BusinessLogic objChk = new BusinessLogic();

            GrdViewCust.PageSize = 9;

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                GrdViewItems.Columns[8].Visible = false;
                GrdViewItems.Columns[9].Visible = false;
                //GrdViewPurchase.Columns[14].Visible = false;
                cmdSave.Enabled = false;

                lnkBtnAdd.Visible = false;
                pnlSearch.Visible = false;
            }


            if (!IsPostBack)
            {



            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtUserName.UniqueID, "Text"));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
    }

    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        try
        {
            GrdViewItems.DataSource = null;
            GrdViewItems.DataBind();

            ModalPopupLock.Hide();

            BusinessLogic objChk = new BusinessLogic();
            string dbfileName = sDataSource.Remove(0, sDataSource.LastIndexOf(@"App_Data\") + 9);
            dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));

            if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
            {
                lnkBtnAdd.Visible = false;
                pnlSearch.Visible = false;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

        }
    }

    //protected void GrdViewItems_SelectedIndexChanged(object sender, EventArgs e)
    //{
        

    //}


    //protected void GrdViewPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
        
    //}
 
    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupLock.Show();
            DataSet ds = new DataSet();

            string connection = string.Empty;
            string userid = string.Empty;

            if (Request.Cookies["Company"] != null)
                connection = Request.Cookies["Company"].Value;

            if (Request.QueryString["ID"] != null)
                userid = Request.QueryString["ID"].ToString();

            BusinessLogic objBus = new BusinessLogic();

            ds = objBus.GetMasterRoles(System.Configuration.ConfigurationManager.ConnectionStrings[connection].ConnectionString);

            GrdViewItems.DataSource = ds;
            GrdViewItems.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewCust_RowCommand(object sender, GridViewCommandEventArgs e)
    {

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

    //private void Reset()
    //{
        
    //}

 
    //protected void GrdViewPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
        
    //}
    
    //private void BindGrid(string strBillno, string strTransNo)
    //{
    //    DataSet ds = new DataSet();

    //    GrdViewPurchase.DataSource = null;
    //    GrdViewPurchase.DataBind();
    //}

    //protected void GrdViewPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
        
    //}

    //protected void GrdViewPurchase_SelectedIndexChanged(object sender, EventArgs e)
    //{
        
    //}



    //protected void GrdViewItems_RowEditing(object sender, GridViewEditEventArgs e)
    //{

    //}

    //protected void GrdViewItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{

    //}

    //protected void GrdViewItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{

    //}
    //protected void GrdViewItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{

    //}
    //protected void GrdViewItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{

    //}

    //protected void GrdViewItems_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Pager)
    //    {
    //        PresentationUtils.SetPagerButtonStates2(GrdViewItems, e.Row, this);
    //    }
    //}

    //protected void GrdViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //}

    //protected void GrdViewPurchase_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Pager)
    //    {
    //        PresentationUtils.SetPagerButtonStates(GrdViewPurchase, e.Row, this);
    //    }
    //}

   
}
