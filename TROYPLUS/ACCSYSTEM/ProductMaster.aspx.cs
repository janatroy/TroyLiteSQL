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

public partial class ProductMaster : System.Web.UI.Page
{
    public string sDataSource = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                string connStr = string.Empty;
                GenerateRoleDs();
                if (Request.Cookies["Company"] != null)
                    connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                else
                    Response.Redirect("~/Login.aspx");

                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                string dbfileName = connStr.Remove(0, connStr.LastIndexOf(@"App_Data\") + 9);
                dbfileName = dbfileName.Remove(dbfileName.LastIndexOf(";Persist Security Info"));
                BusinessLogic objChk = new BusinessLogic(connStr);

                if (objChk.CheckForOffline(Server.MapPath("Offline\\" + dbfileName + ".offline")))
                {
                    lnkBtnAdd.Visible = false;
                    GrdViewProduct.Columns[7].Visible = false;
                    GrdViewProduct.Columns[8].Visible = false;
                }



                GrdViewProduct.PageSize = 8;

                //((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtDpDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
                //((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtMrpDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
                //((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtNLCDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);

                if (bl.CheckUserHaveAdd(usernam, "PRDMST"))
                {
                    lnkBtnAdd.Enabled = false;
                    lnkBtnAdd.ToolTip = "You are not allowed to make Add New ";
                }
                else
                {
                    lnkBtnAdd.Enabled = true;
                    lnkBtnAdd.ToolTip = "Click to Add New ";
                }

                if (Request.QueryString["myname"] != null)
                {
                    string myNam = Request.QueryString["myname"].ToString();
                    if (myNam == "NEWPROD")
                    {

                        if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
                            return;
                        }

                        frmViewAdd.ChangeMode(FormViewMode.Insert);
                        frmViewAdd.Visible = true;

                        /*
                        if (frmViewAdd.CurrentMode == FormViewMode.Insert)
                        {
                            GrdViewProduct.Visible = false;
                            lnkBtnAdd.Visible = false;
                            //MyAccordion.Visible = false;
                        }*/

                        if (!DealerRequired())
                        {
                            if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                            {
                                if (this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("rowDealerAdd") != null)
                                {
                                    ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("rowDealerAdd")).Visible = false;
                                    ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd1")).Visible = false;
                                }
                            }
                        }
                        /*
                        if (!DealerRequired())
                        {
                            if (FindControlRecursive(frmViewAdd, "rowDealerAdd") != null) ;
                            {
                                (HtmlTableRow)FindControlRecursive(frmViewAdd, "rowDealerAdd").Visible = false;
                                (HtmlTableRow)FindControlRecursive(frmViewAdd, "rowDealerAdd1").Visible = false;
                            }
                        }*/

                        TextBox txtStock = null;

                        if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                        {
                            txtStock = (TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtStockAdd");

                        }

                        if (AllowStockEdit())
                        {
                            if (txtStock != null)
                            {
                                txtStock.Enabled = true;
                            }
                        }
                        else
                        {
                            if (txtStock != null)
                                txtStock.Enabled = false;
                        }

                        ModalPopupExtender1.Show();
                    }
                    else
                        if (myNam == "NEWP")
                        {
                            if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
                                return;
                            }

                            frmViewAdd.ChangeMode(FormViewMode.Insert);
                            frmViewAdd.Visible = true;

                            /*
                            if (frmViewAdd.CurrentMode == FormViewMode.Insert)
                            {
                                GrdViewProduct.Visible = false;
                                lnkBtnAdd.Visible = false;
                                //MyAccordion.Visible = false;
                            }*/

                            if (!DealerRequired())
                            {
                                if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                                {
                                    if (this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("rowDealerAdd") != null)
                                    {
                                        ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("rowDealerAdd")).Visible = false;
                                        ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd1")).Visible = false;
                                    }
                                }
                            }
                            /*
                            if (!DealerRequired())
                            {
                                if (FindControlRecursive(frmViewAdd, "rowDealerAdd") != null) ;
                                {
                                    (HtmlTableRow)FindControlRecursive(frmViewAdd, "rowDealerAdd").Visible = false;
                                    (HtmlTableRow)FindControlRecursive(frmViewAdd, "rowDealerAdd1").Visible = false;
                                }
                            }*/

                            TextBox txtStock = null;

                            if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                            {
                                txtStock = (TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtStockAdd");

                            }

                            if (AllowStockEdit())
                            {
                                if (txtStock != null)
                                {
                                    txtStock.Enabled = true;
                                }
                            }
                            else
                            {
                                if (txtStock != null)
                                    txtStock.Enabled = false;
                            }

                            ModalPopupExtender1.Show();
                        }

                        else
                            if (myNam == "NEWPP")
                            {
                                if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
                                    return;
                                }

                                frmViewAdd.ChangeMode(FormViewMode.Insert);
                                frmViewAdd.Visible = true;

                                /*
                                if (frmViewAdd.CurrentMode == FormViewMode.Insert)
                                {
                                    GrdViewProduct.Visible = false;
                                    lnkBtnAdd.Visible = false;
                                    //MyAccordion.Visible = false;
                                }*/

                                if (!DealerRequired())
                                {
                                    if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                                    {
                                        if (this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("rowDealerAdd") != null)
                                        {
                                            ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("rowDealerAdd")).Visible = false;
                                            ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd1")).Visible = false;
                                        }
                                    }
                                }
                                /*
                                if (!DealerRequired())
                                {
                                    if (FindControlRecursive(frmViewAdd, "rowDealerAdd") != null) ;
                                    {
                                        (HtmlTableRow)FindControlRecursive(frmViewAdd, "rowDealerAdd").Visible = false;
                                        (HtmlTableRow)FindControlRecursive(frmViewAdd, "rowDealerAdd1").Visible = false;
                                    }
                                }*/

                                TextBox txtStock = null;

                                if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                                {
                                    txtStock = (TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtStockAdd");

                                }

                                if (AllowStockEdit())
                                {
                                    if (txtStock != null)
                                    {
                                        txtStock.Enabled = true;
                                    }
                                }
                                else
                                {
                                    if (txtStock != null)
                                        txtStock.Enabled = false;
                                }

                                ModalPopupExtender1.Show();
                            }

                }

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
            txtSearch.Text = "";
            ddCriteria.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void BlkUpd_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ProductsUpdation.aspx?ID=Update");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdshowhistory_click(object sender, EventArgs e)
    {
        try
        {
            //ModalPopupContact.Show();
            ModalPopupExtender1.Show();

            string Itemcode = string.Empty;

            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            BusinessLogic bl = new BusinessLogic();

            Itemcode = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtItemCode")).Text.Trim();

            DataSet dsSales = bl.ListProductHistory(connStr.Trim(), Itemcode);

            if (dsSales != null)
            {
                //if (dsSales.Tables[0].Rows.Count > 0)
                //{
                //    GrdViewHistory.DataSource = dsSales;
                //    GrdViewHistory.DataBind();
                //}
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void CloseWindow_Click(object sender, EventArgs e)
    {
        try
        {
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private string GetConnectionString()
    {
        string connStr = string.Empty;

        if (Request.Cookies["Company"] != null)
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        else
            Response.Redirect("~/Login.aspx");

        return connStr;


    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        //TextBox search = (TextBox)Accordion1.FindControl("txtSearch");
        //DropDownList dropDown = (DropDownList)Accordion1.FindControl("ddCriteria");
        GridSource.SelectParameters.Add(new CookieParameter("connection", "Company"));
        GridSource.SelectParameters.Add(new ControlParameter("txtSearch", TypeCode.String, txtSearch.UniqueID, "Text"));
        GridSource.SelectParameters.Add(new ControlParameter("dropDown", TypeCode.String, ddCriteria.UniqueID, "SelectedValue"));


    }
    protected void GrdViewProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string sDataSource = string.Empty;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            frmViewAdd.Visible = true;
            frmViewAdd.ChangeMode(FormViewMode.Edit);
            ModalPopupExtender1.Show();
            //GrdViewProduct.Visible = false;
            //lnkBtnAdd.Visible = false;
            ////MyAccordion.Visible = false;
            GridViewRow row = GrdViewProduct.SelectedRow;
            string itemcode = row.Cells[0].Text;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            //DataSet dsRole = bl.getRoleInfo(itemcode);
            DataSet dsRole = null;
            //string strStockCheck = bl.GetStockEdit();
            TextBox txtStock = null;

            if (this.frmViewAdd.FindControl("tabEditContol") != null)
            {
                txtStock = (TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtStock");
            }

            if (AllowStockEdit())
            {
                if (txtStock != null)
                {
                    txtStock.Enabled = true;
                }
            }
            else
            {
                if (txtStock != null)
                    txtStock.Enabled = false;
            }

            pnlRole.Visible = true;
            grdRole.DataSource = dsRole;
            grdRole.DataBind();

            /*
            if (!DealerRequired())
            {
                if ((this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer")) != null)
                {
                    ((HtmlTableRow)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer")).Visible = false;
                    ((HtmlTableRow)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer1")).Visible = false;
                }
            }*/


            string Itemcode = string.Empty;

            string connStr = string.Empty;

            if (Request.Cookies["Company"] != null)
                connStr = System.Configuration.ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            else
                Response.Redirect("~/Login.aspx");

            //BusinessLogic bl = new BusinessLogic();

            //Itemcode = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtItemCode")).Text.Trim();

            //DataSet dsSales = bl.ListProductHistory(connStr.Trim(), itemcode);

            //if (dsSales != null)
            //{
            //    if (dsSales.Tables[0].Rows.Count > 0)
            //    {
            //        ((GridView)this.frmViewAdd.FindControl("tabEditContol").FindControl("TabPanel1").FindControl("GrdViewHistory")).DataSource = dsSales;
            //        ((GridView)this.frmViewAdd.FindControl("tabEditContol").FindControl("TabPanel1").FindControl("GrdViewHistory")).DataBind();
            //        //GrdViewHistory.DataSource = dsSales;
            //        //GrdViewHistory.DataBind();
            //    }
            //    else
            //    {
            //        ((GridView)this.frmViewAdd.FindControl("tabEditContol").FindControl("TabPanel1").FindControl("GrdViewHistory")).DataSource = null;
            //        ((GridView)this.frmViewAdd.FindControl("tabEditContol").FindControl("TabPanel1").FindControl("GrdViewHistory")).DataBind();
            //        //GrdViewHistory.DataSource = null;
            //        //GrdViewHistory.DataBind();
            //    }
            //}
            //else
            //{
            //    GrdViewHistory.DataSource = null;
            //    GrdViewHistory.DataBind();
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }



    protected void txtProdDesc_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string productname = ((DataRowView)frmV.DataItem)["productdesc"].ToString();



                string connection = Request.Cookies["Company"].Value;
                BusinessLogic bl = new BusinessLogic();
                string brand = bl.GetBrandName(connection, productname);



                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(brand);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void grdRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            string sDataSource = string.Empty;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            grdRole.PageIndex = e.NewPageIndex;
            GridViewRow row = GrdViewProduct.SelectedRow;
            string itemcode = row.Cells[0].Text;
            BusinessLogic bl = new BusinessLogic(sDataSource);
            DataSet dsRole = bl.getRoleInfo(itemcode);

            pnlRole.Visible = true;
            grdRole.DataSource = dsRole;
            grdRole.DataBind();
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //private void BindSearchGrid()
    //{
    //    string sDataSource = string.Empty;
    //    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
    //    BusinessLogic bl = new BusinessLogic(sDataSource);
    //    GrdViewProduct.DataSource = bl.ListProducts(sDataSource, txtSItemCode.Text, txtSBrand.Text, txtSModel.Text);
    //    GrdViewProduct.DataBind();

    //}

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    BindSearchGrid();
    //}

    protected void GrdViewProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //    if (e.CommandName == "Select")
        //    {

        //        //if (frmViewAdd.CurrentMode == FormViewMode.Edit)
        //            //Accordion1.SelectedIndex = 1;
        //    }
    }
    protected void GrdViewProduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                PresentationUtils.SetPagerButtonStates(GrdViewProduct, e.Row, this);
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


    protected void GrdViewProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GrdViewProduct.SelectedIndex = e.RowIndex;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GrdViewProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BusinessLogic bl = new BusinessLogic(GetConnectionString());

                if (bl.CheckIfProductUsed(((HiddenField)e.Row.FindControl("ldgID")).Value))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

                string connection = Request.Cookies["Company"].Value;
                string usernam = Request.Cookies["LoggedUserName"].Value;

                if (bl.CheckUserHaveEdit(usernam, "PRDMST"))
                {
                    ((ImageButton)e.Row.FindControl("btnEdit")).Visible = false;
                    ((ImageButton)e.Row.FindControl("btnEditDisabled")).Visible = true;
                }

                if (bl.CheckUserHaveDelete(usernam, "PRDMST"))
                {
                    ((ImageButton)e.Row.FindControl("lnkB")).Visible = false;
                    ((ImageButton)e.Row.FindControl("lnkBDisabled")).Visible = true;
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void GridSource_Deleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            if (GrdViewProduct.SelectedDataKey != null)
                e.InputParameters["ItemCode"] = GrdViewProduct.SelectedDataKey.Value;

            e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmViewAdd_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        try
        {
            if (frmViewAdd.DefaultMode == FormViewMode.Insert)
            {
                if (!DealerRequired())
                {
                    if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                    {
                        if (this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd") != null)
                        {
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd")).Visible = false;
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd1")).Visible = false;
                        }
                    }
                }
            }

            if (frmViewAdd.DefaultMode == FormViewMode.Edit)
            {
                if (!DealerRequired())
                {
                    if (this.frmViewAdd.FindControl("tabEditContol") != null)
                    {
                        if ((this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer")) != null)
                        {
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer")).Visible = false;
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer1")).Visible = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmViewAdd_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                lnkBtnAdd.Visible = true;
                //MyAccordion.Visible = true;
                frmViewAdd.Visible = false;
                GrdViewProduct.Visible = true;
                System.Threading.Thread.Sleep(1000);
                GrdViewProduct.DataBind();
                pnlRole.Visible = false;
                StringBuilder scriptMsg = new StringBuilder();

                DataSet ds;
                DataTable dt;
                DataRow drNew;

                DataColumn dc;

                ds = new DataSet();

                dt = new DataTable();

                dc = new DataColumn("Price");
                dt.Columns.Add(dc);

                dc = new DataColumn("EffDate");
                dt.Columns.Add(dc);

                dc = new DataColumn("Discount");
                dt.Columns.Add(dc);

                dc = new DataColumn("PriceName");
                dt.Columns.Add(dc);

                ds.Tables.Add(dt);

                for (int vLoop = 0; vLoop < ((GridView)this.frmViewAdd.FindControl("tablInsertControl").FindControl("TabPanel1").FindControl("GrdViewItems")).Rows.Count; vLoop++)
                {
                    TextBox txttt = (TextBox)((GridView)this.frmViewAdd.FindControl("tablInsertControl").FindControl("TabPanel1").FindControl("GrdViewItems")).Rows[vLoop].FindControl("txtPrice");
                    TextBox txtttd = (TextBox)((GridView)this.frmViewAdd.FindControl("tablInsertControl").FindControl("TabPanel1").FindControl("GrdViewItems")).Rows[vLoop].FindControl("txtEffDate");
                    TextBox txt = (TextBox)((GridView)this.frmViewAdd.FindControl("tablInsertControl").FindControl("TabPanel1").FindControl("GrdViewItems")).Rows[vLoop].FindControl("txtDiscount");
                    TextBox txtt = (TextBox)((GridView)this.frmViewAdd.FindControl("tablInsertControl").FindControl("TabPanel1").FindControl("GrdViewItems")).Rows[vLoop].FindControl("txtPriceName");
                    drNew = dt.NewRow();
                    drNew["Price"] = txttt.Text;
                    drNew["EffDate"] = txtttd.Text;
                    drNew["Discount"] = txt.Text;
                    drNew["PriceName"] = txtt.Text;
                    ds.Tables[0].Rows.Add(drNew);
                }
                string connection = string.Empty;
                connection = Request.Cookies["Company"].Value;

                //bl.InsertProductPrices(connection, ds, usernam);

                scriptMsg.Append("alert('Product Details Saved Successfully.');");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), scriptMsg.ToString(), true);


                if (Request.QueryString["myname"] != null)
                {
                    string myNam = Request.QueryString["myname"].ToString();
                    if (myNam == "NEWP")
                    {
                        Response.Redirect("CustomerSales.aspx?myname=" + "NEWSAL");
                    }

                    else
                        if (myNam == "NEWPP")
                        {
                            Response.Redirect("Purchase.aspx?myname=" + "NEWPUR");
                        }
                }



            }
            else
            {
                if (e.Exception != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('Product with this code already exists, Please try with a different code.');");

                    if (e.Exception.InnerException != null)
                    {
                        if (e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                        else
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.InnerException.Message + "');", true);
                    }
                    e.KeepInInsertMode = true;
                    e.ExceptionHandled = true;

                }
                //else
                //{
                //    lnkBtnAdd.Visible = true;
                //    frmViewAdd.Visible = false;
                //    GrdViewProduct.Visible = true;
                //    e.ExceptionHandled = true;
                //    pnlRole.Visible = false;
                //}
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_ItemInserting(object sender, FormViewInsertEventArgs e)
    {

    }
    protected void frmViewAdd_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        try
        {
            if (e.Exception == null)
            {
                ModalPopupExtender1.Hide();
                //MyAccordion.Visible = true;
                lnkBtnAdd.Visible = true;
                frmViewAdd.Visible = false;
                GrdViewProduct.Visible = true;
                System.Threading.Thread.Sleep(1000);
                GrdViewProduct.DataBind();
                pnlRole.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Details Updated Successfully.');", true);
            }
            else
            {
                if (e.Exception != null)
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("alert('Product with this code already exists, Please try with a different code.');");

                    if (e.Exception.InnerException != null)
                    {
                        if (e.Exception.InnerException.Message.IndexOf("duplicate values in the index") > -1)
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
                        else
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.Message + " " + e.Exception.InnerException.Message + "');", true);

                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exception Occured : " + e.Exception.Message + " " + e.Exception.StackTrace + "');", true);

                    e.KeepInEditMode = true;
                    e.ExceptionHandled = true;
                }
                //else
                //{
                //    lnkBtnAdd.Visible = true;
                //    frmViewAdd.Visible = false;
                //    GrdViewProduct.Visible = true;
                //    //e.ExceptionHandled = true;
                //    pnlRole.Visible = false;
                //}
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

    }
    protected void frmViewAdd_ItemCreated(object sender, EventArgs e)
    {
        try
        {
            if (!DealerRequired())
            {
                if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                {
                    if (this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd") != null)
                    {
                        ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd")).Visible = false;
                        ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd1")).Visible = false;
                    }
                }

                if (this.frmViewAdd.FindControl("tabEditContol") != null)
                {
                    if ((this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer")) != null)
                    {
                        ((HtmlTableRow)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer")).Visible = false;
                        ((HtmlTableRow)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer1")).Visible = false;
                    }
                }
            }

            if (this.frmViewAdd.FindControl("tablInsertControl") != null)
            {
                TextBox txt = (TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtItemCodeAdd");

                if (txt != null)
                {
                    Helper.SetFocus(txt);
                }
            }

            if (!AllowStockEdit())
            {
                if (this.frmViewAdd.FindControl("tabEditContol") != null)
                {
                    TextBox txtStock = (TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtStock");
                    txtStock.Enabled = false;
                }

                if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                {
                    TextBox txtStock = (TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtStockAdd");
                    txtStock.Enabled = false;
                }
            }

            if (this.frmViewAdd.FindControl("tabEditContol") != null)
            {
                TextBox txt = (TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtItemName");

                if (txt != null)
                {
                    Helper.SetFocus(txt);
                }
            }

            //if (this.frmViewAdd.FindControl("tablInsertControl") != null)
            //{
            //    if (this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtTransDateAdd") != null)
            //    {

            //        if (ViewState["DpEffDate"] == null)
            //        {
            //            ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtDpDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
            //        }
            //        else if (ViewState["MRPEffDate"] == null)
            //        {
            //            ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtMrpDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
            //        }
            //        else if (ViewState["NLCEffDate"] == null)
            //        {
            //            ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtNLCDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
            //        }
            //        else
            //        {
            //            ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtDpDateAdd")).Text = ViewState["DpEffDate"].ToString();
            //            ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtMrpDateAdd")).Text = ViewState["MRPEffDate"].ToString();
            //            ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtNLCDateAdd")).Text = ViewState["NLCEffDate"].ToString();
            //        }

            //    }

            //}


            if (this.frmViewAdd.FindControl("tablInsertControl") != null)
            {
                ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDpDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
                ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtMrpDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
                ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtNLCDateAdd")).Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmSource_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            string vat = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtVATAdd")).Text.Trim();
            ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtBuyVATAdd")).Text = vat;

            this.setInsertParameters(e);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void frmSource_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        try
        {
            string vat = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtVAT")).Text.Trim();
            ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtBuyVAT")).Text = vat;

            string refDate = string.Empty;
            refDate = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtMrpDate")).Text;
            string dt = Convert.ToDateTime(refDate).ToString("MM/dd/yyyy");

            string itemcode = string.Empty;
            itemcode = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtItemCode")).Text;

            string rate = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtUnitRate")).Text;

            BusinessLogic bl = new BusinessLogic(sDataSource);
            string connection = Request.Cookies["Company"].Value;

            if (!bl.IsRateModified(connection, Convert.ToDouble(rate), DateTime.Parse(refDate), itemcode))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Rate is Modified. Please modify the MRP Effective Date also.')", true);
                ModalPopupExtender1.Show();
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                return;
            }

            if (!bl.IsRateDateModified(connection, Convert.ToDouble(rate), dt, itemcode))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('MRP Effective Date is Modified. Please modify the Rate also.')", true);
                ModalPopupExtender1.Show();
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                return;
            }

            if (!bl.IsRateOldDateModified(connection, Convert.ToDouble(rate), DateTime.Parse(refDate), itemcode))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('New MRP Effective Date Should be greater than Old MRP Effective Date.')", true);
                ModalPopupExtender1.Show();
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                return;
            }

            string dpDate = string.Empty;
            dpDate = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtDpDate")).Text;
            string dprate = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtDealerRate")).Text;
            string dtt = Convert.ToDateTime(dpDate).ToString("MM/dd/yyyy");
            if (!bl.IsDPRateModified(connection, Convert.ToDouble(dprate), DateTime.Parse(dpDate), itemcode))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('DP Rate is Modified. Please modify the DP Effective Date also.')", true);
                ModalPopupExtender1.Show();
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                return;
            }

            if (!bl.IsDPRateDateModified(connection, Convert.ToDouble(dprate), dtt, itemcode))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('DP Effective Date is Modified. Please modify the DP Rate also.')", true);
                ModalPopupExtender1.Show();
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                return;
            }

            if (!bl.IsDPRateOldDateModified(connection, Convert.ToDouble(dprate), dpDate, itemcode))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('New DP Effective Date Should be greater than Old DP Effective Date.')", true);
                ModalPopupExtender1.Show();
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                return;
            }

            string nlcDate = string.Empty;
            nlcDate = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtNLCDate")).Text;
            string nlcrate = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtNLC")).Text;
            string dtd = Convert.ToDateTime(nlcDate).ToString("MM/dd/yyyy");
            if (!bl.IsNLCRateModified(connection, Convert.ToDouble(nlcrate), DateTime.Parse(nlcDate), itemcode))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('NLC Rate is Modified. Please modify the NLC Effective Date also.')", true);
                ModalPopupExtender1.Show();
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                return;
            }

            if (!bl.IsNLCRateDateModified(connection, Convert.ToDouble(nlcrate), dtd, itemcode))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('NLC Effective Date is Modified. Please modify the NLC Rate also.')", true);
                ModalPopupExtender1.Show();
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                return;
            }

            if (!bl.IsNLCRateOldDateModified(connection, Convert.ToDouble(nlcrate), DateTime.Parse(nlcDate), itemcode))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('New NLC Effective Date Should be greater than Old NLC Effective Date.')", true);
                ModalPopupExtender1.Show();
                frmViewAdd.Visible = true;
                frmViewAdd.ChangeMode(FormViewMode.Edit);
                return;
            }

            this.setUpdateParameters(e);
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            ////MyAccordion.Visible = true;
            frmViewAdd.Visible = false;
            ModalPopupExtender1.Hide();
            //lnkBtnAdd.Visible = true;
            //GrdViewProduct.Visible = true;
            //pnlRole.Visible = false; 
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
            if (!Page.IsValid)
            {
                StringBuilder msg = new StringBuilder();

                foreach (IValidator validator in Page.Validators)
                {
                    if (!validator.IsValid)
                    {
                        msg.Append(" - " + validator.ErrorMessage);
                        msg.Append("\\n");
                    }
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + msg.ToString() + "');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //lnkBtnAdd.Visible = true;
            ////MyAccordion.Visible = true;
            frmViewAdd.Visible = false;
            ModalPopupExtender1.Hide();
            //pnlRole.Visible = true;
            //GrdViewProduct.Visible = true;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void InsertButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsValid)
            {

                

                StringBuilder msg = new StringBuilder();

                foreach (IValidator validator in Page.Validators)
                {
                    if (!validator.IsValid)
                    {
                        msg.Append(" - " + validator.ErrorMessage);
                        msg.Append("\\n");
                    }
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + msg.ToString() + "');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private bool DealerRequired()
    {
        DataSet appSettings;
        string dealerRequired = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "DEALER")
                {
                    dealerRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }
        else if (Session["AppSettings"] == null)
        {
            BusinessLogic bl = new BusinessLogic();
            DataSet ds = bl.GetAppSettings(Request.Cookies["Company"].Value);

            if (ds != null)
                Session["AppSettings"] = ds;

            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "DEALER")
                {
                    dealerRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }



        if (dealerRequired == "YES")
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool AllowStockEdit()
    {
        DataSet appSettings;
        string dealerRequired = string.Empty;

        if (Session["AppSettings"] != null)
        {
            appSettings = (DataSet)Session["AppSettings"];

            for (int i = 0; i < appSettings.Tables[0].Rows.Count; i++)
            {
                if (appSettings.Tables[0].Rows[i]["KEY"].ToString() == "STOCKEDIT")
                {
                    dealerRequired = appSettings.Tables[0].Rows[i]["KEYVALUE"].ToString();
                }
            }
        }

        if (dealerRequired == "YES")
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id)
        {
            return root;
        }

        foreach (Control c in root.Controls)
        {
            Control t = FindControlRecursive(c, id);
            if (t != null)
            {
                return t;
            }
        }

        return null;
    }

    protected void lnkBtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //if (!Helper.IsLicenced(Request.Cookies["Company"].Value))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Trial Version, Please upgrade to Full Version of this Software. Thank You.');", true);
            //    return;
            //}

            frmViewAdd.ChangeMode(FormViewMode.Insert);
            frmViewAdd.Visible = true;
            BusinessLogic bl = new BusinessLogic(sDataSource);

            string connection = Request.Cookies["Company"].Value;
            DataSet ds = bl.ListPriceListInfo(connection, "", "");

            DataTable dtt;
            DataRow drNew;
            DataColumn dct;
            DataSet dst = new DataSet();
            dtt = new DataTable();

            dct = new DataColumn("ID");
            dtt.Columns.Add(dct);

            dct = new DataColumn("PriceName");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Price");
            dtt.Columns.Add(dct);

            dct = new DataColumn("EffDate");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Discount");
            dtt.Columns.Add(dct);

            dst.Tables.Add(dtt);




            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        drNew = dtt.NewRow();
                        drNew["ID"] = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"]);
                        drNew["PriceName"] = Convert.ToString(ds.Tables[0].Rows[i]["PriceName"]);
                        drNew["Price"] = "";
                        drNew["EffDate"] = "";
                        drNew["Discount"] = "";
                        dst.Tables[0].Rows.Add(drNew);
                    }
                }

                ((GridView)this.frmViewAdd.FindControl("tablInsertControl").FindControl("TabPanel1").FindControl("GrdViewItems")).DataSource = dst.Tables[0].DefaultView;
                ((GridView)this.frmViewAdd.FindControl("tablInsertControl").FindControl("TabPanel1").FindControl("GrdViewItems")).DataBind();
            }
            else
            {
                ((GridView)this.frmViewAdd.FindControl("tablInsertControl").FindControl("TabPanel1").FindControl("GrdViewItems")).EmptyDataText = "No Price List found";
                ((GridView)this.frmViewAdd.FindControl("tablInsertControl").FindControl("TabPanel1").FindControl("GrdViewItems")).DataSource = null;
                ((GridView)this.frmViewAdd.FindControl("tablInsertControl").FindControl("TabPanel1").FindControl("GrdViewItems")).DataBind();
            }


            /*
            if (frmViewAdd.CurrentMode == FormViewMode.Insert)
            {
                GrdViewProduct.Visible = false;
                lnkBtnAdd.Visible = false;
                //MyAccordion.Visible = false;
            }*/

            if (!DealerRequired())
            {
                if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                {
                    if (this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("rowDealerAdd") != null)
                    {
                        ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("rowDealerAdd")).Visible = false;
                        ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd1")).Visible = false;
                    }
                }
            }
            /*
            if (!DealerRequired())
            {
                if (FindControlRecursive(frmViewAdd, "rowDealerAdd") != null) ;
                {
                    (HtmlTableRow)FindControlRecursive(frmViewAdd, "rowDealerAdd").Visible = false;
                    (HtmlTableRow)FindControlRecursive(frmViewAdd, "rowDealerAdd1").Visible = false;
                }
            }*/

            TextBox txtStock = null;

            if (this.frmViewAdd.FindControl("tablInsertControl") != null)
            {
                txtStock = (TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtStockAdd");

            }

            if (AllowStockEdit())
            {
                if (txtStock != null)
                {
                    txtStock.Enabled = true;
                }
            }
            else
            {
                if (txtStock != null)
                    txtStock.Enabled = false;
            }

            //TextBox txtROL = null;
            //txtROL = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtROLAdd"));
            //txtROL.Text = "0";
            //TextBox txtDPDiscount = null;
            //txtDPDiscount = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDealerDiscountAdd"));
            //txtDPDiscount.Text = "0";

            ModalPopupExtender1.Show();

            //if (this.frmViewAdd.FindControl("tabEditContol") != null)
            //{
            //    ((GridView)this.frmViewAdd.FindControl("tabEditContol").FindControl("TabPanel1").FindControl("GrdViewHistory")).DataSource = null;
            //    ((GridView)this.frmViewAdd.FindControl("tabEditContol").FindControl("TabPanel1").FindControl("GrdViewHistory")).DataBind();
            //}
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    private void setInsertParameters(ObjectDataSourceMethodEventArgs e)
    {

        if (((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("ddCategoryAdd")) != null)
            e.InputParameters["CategoryID"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("ddCategoryAdd")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtItemCodeAdd")).Text != "")
            e.InputParameters["ItemCode"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtItemCodeAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtStockAdd")).Text != "")
            e.InputParameters["Stock"] = "0";

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtItemNameAdd")).Text != "")
            e.InputParameters["ProductName"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtItemNameAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtROLAdd")).Text != "")
            e.InputParameters["ROL"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtROLAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtModelAdd")).Text != "")
            e.InputParameters["Model"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtModelAdd")).Text.Trim();

        if (((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtProdDescAdd")).Text != "")
            e.InputParameters["ProductDesc"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtProdDescAdd")).SelectedItem.Text;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtUnitRateAdd")).Text != "")
            e.InputParameters["Rate"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtUnitRateAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtMrpDateAdd")).Text != "")
            e.InputParameters["MRPEffDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtMrpDateAdd")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtUnitAdd")).Text != "")
            e.InputParameters["Unit"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtUnitAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtVATAdd")).Text != "")
            e.InputParameters["VAT"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtVATAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDiscountAdd")).Text != "")
            e.InputParameters["Discount"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDiscountAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtBuyUnitAdd")).Text != "")
            e.InputParameters["BuyUnit"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtBuyUnitAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtBuyVATAdd")).Text != "")
            e.InputParameters["BuyVAT"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtBuyVATAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtBuyRateAdd")).Text != "")
            e.InputParameters["BuyRate"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtBuyRateAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtBuyDiscountAdd")).Text != "")
            e.InputParameters["BuyDiscount"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtBuyDiscountAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDealerUnitAdd")).Text != "")
            e.InputParameters["DealerUnit"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDealerUnitAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtDealerVATAdd")).Text != "")
            e.InputParameters["DealerVAT"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtDealerVATAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtCSTAdd")).Text != "")
            e.InputParameters["CST"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtCSTAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDealerRateAdd")).Text != "")
            e.InputParameters["DealerRate"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDealerRateAdd")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDpDateAdd")).Text != "")
            e.InputParameters["DPEffDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDpDateAdd")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDealerDiscountAdd")).Text != "")
            e.InputParameters["DealerDiscount"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtDealerDiscountAdd")).Text.Trim();

        if (((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("drpMeasureAdd")) != null)
            e.InputParameters["Measure_Unit"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("drpMeasureAdd")).SelectedValue;
        else
            e.InputParameters["Measure_Unit"] = "";

        if (((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("drpComplexAdd")) != null)
            e.InputParameters["Complex"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("drpComplexAdd")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("drpRoleTypeAdd")) != null)
            e.InputParameters["Accept_Role"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("drpRoleTypeAdd")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtBarCodeAdd")).Text != "") //Jolo Barcode
            e.InputParameters["Barcode"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtBarcodeAdd")).Text.Trim(); //Jolo Barcode
        else
            e.InputParameters["Barcode"] = string.Empty;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtCommCodeAdd")).Text != "") //Jolo Barcode
            e.InputParameters["CommodityCode"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtCommCodeAdd")).Text.Trim(); //Jolo Barcode
        else
            e.InputParameters["CommodityCode"] = string.Empty;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtExecutiveCommissionAdd")).Text != "")
            e.InputParameters["ExecutiveCommission"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtExecutiveCommissionAdd")).Text.Trim();
        else
            e.InputParameters["ExecutiveCommission"] = "0";

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtNLCAdd")).Text != "") //Jolo Barcode
            e.InputParameters["NLC"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtNLCAdd")).Text.Trim(); //Jolo Barcode
        else
            e.InputParameters["NLC"] = "0.0";

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtNLCDateAdd")).Text != "")
            e.InputParameters["NLCEffDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtNLCDateAdd")).Text);

        if (((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("drpblockadd")) != null)
            e.InputParameters["block"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("drpblockadd")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtproductlevel")).Text != "")
            e.InputParameters["Productlevel"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("txtproductlevel")).Text.Trim(); 
        else
            e.InputParameters["Productlevel"] = "0";

        if (((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("drpOutdatedAdd")) != null)
            e.InputParameters["Outdated"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("drpOutdatedAdd")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtAllowedPriceAdd")).Text != "")
            e.InputParameters["Deviation"] = ((TextBox)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("txtAllowedPriceAdd")).Text.Trim();
        else
            e.InputParameters["Deviation"] = "0";

        if (((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("drpIsActiveAdd")) != null)
            e.InputParameters["IsActive"] = ((DropDownList)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsProdMaster").FindControl("drpIsActiveAdd")).SelectedValue;

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

        

        //e.InputParameters["Ds"]=

    }

    private void setUpdateParameters(ObjectDataSourceMethodEventArgs e)
    {
        if (((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("ddCategory")) != null)
            e.InputParameters["CategoryID"] = ((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("ddCategory")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtItemCode")).Text != "")
            e.InputParameters["ItemCode"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtItemCode")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtStock")).Text != "")
            e.InputParameters["Stock"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtStock")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtItemName")).Text != "")
            e.InputParameters["ProductName"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtItemName")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtROL")).Text != "")
            e.InputParameters["ROL"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtROL")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtModel")).Text != "")
            e.InputParameters["Model"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtModel")).Text.Trim();

        if (((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtProdDesc")).Text != "")
            e.InputParameters["ProductDesc"] = ((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtProdDesc")).SelectedItem.Text;

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtUnitRate")).Text != "")
            e.InputParameters["Rate"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtUnitRate")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtMrpDate")).Text != "")
            e.InputParameters["MRPEffDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtMrpDate")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtUnit")).Text != "")
            e.InputParameters["Unit"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtUnit")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtVAT")).Text != "")
            e.InputParameters["VAT"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtVAT")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtCST")).Text != "")
            e.InputParameters["CST"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtCST")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtDiscount")).Text != "")
            e.InputParameters["Discount"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtDiscount")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtBuyUnit")).Text != "")
            e.InputParameters["BuyUnit"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtBuyUnit")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtBuyVAT")).Text != "")
            e.InputParameters["BuyVAT"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtBuyVAT")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtBuyRate")).Text != "")
            e.InputParameters["BuyRate"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtBuyRate")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtBuyDiscount")).Text != "")
            e.InputParameters["BuyDiscount"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtBuyDiscount")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtDealerUnit")).Text != "")
            e.InputParameters["DealerUnit"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtDealerUnit")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtDealerVAT")).Text != "")
            e.InputParameters["DealerVAT"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtDealerVAT")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtDealerRate")).Text != "")
            e.InputParameters["DealerRate"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtDealerRate")).Text.Trim();

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtDpDate")).Text != "")
            e.InputParameters["DPEffDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtDpDate")).Text);

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtDealerDiscount")).Text != "")
            e.InputParameters["DealerDiscount"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtDealerDiscount")).Text.Trim();

        if (((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("drpMeasure")) != null)
            e.InputParameters["Measure_Unit"] = ((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("drpMeasure")).SelectedValue;
        else
            e.InputParameters["Measure_Unit"] = string.Empty;

        if (((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("drpComplex")) != null)
            e.InputParameters["Complex"] = ((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("drpComplex")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("drpRoleType")) != null)
            e.InputParameters["Accept_Role"] = ((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("drpRoleType")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtBarCode")).Text != "") //Jolo Barcode
            e.InputParameters["Barcode"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtBarcode")).Text.Trim(); //Jolo Barcode
        else
            e.InputParameters["Barcode"] = string.Empty;

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtCommCode")).Text != "")
            e.InputParameters["CommodityCode"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtCommCode")).Text.Trim();
        else
            e.InputParameters["CommodityCode"] = string.Empty;

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtEditExecutiveCommission")).Text != "")
            e.InputParameters["ExecutiveCommission"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtEditExecutiveCommission")).Text.Trim();
        else
            e.InputParameters["ExecutiveCommission"] = "0";

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtNLC")).Text != "")
            e.InputParameters["NLC"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtNLC")).Text.Trim();
        else
            e.InputParameters["NLC"] = "0.0";

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtNLCDate")).Text != "")
            e.InputParameters["NLCEffDate"] = DateTime.Parse(((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtNLCDate")).Text);

        if (((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("drpblock")) != null)
            e.InputParameters["block"] = ((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("drpblock")).SelectedValue;

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtProductlevel")).Text != "")
            e.InputParameters["Productlevel"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("txtProductlevel")).Text.Trim();
        else
            e.InputParameters["Productlevel"] = "0";

        if (((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtAllowedPrice")).Text != "")
            e.InputParameters["Deviation"] = ((TextBox)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("txtAllowedPrice")).Text.Trim();
        else
            e.InputParameters["Deviation"] = "0";

        if (((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("drpOutdated")) != null)
            e.InputParameters["Outdated"] = ((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("drpOutdated")).SelectedValue;

        if (((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("drpIsActive")) != null)
            e.InputParameters["IsActive"] = ((DropDownList)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditProdMaster").FindControl("drpIsActive")).SelectedValue;

        e.InputParameters["Username"] = Request.Cookies["LoggedUserName"].Value;

    }
    //protected void GrdViewItems_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    GridView1.EditIndex = e.NewEditIndex;
    //    DataSet ds = (DataSet)Session["data"];
    //    GridView1.DataSource = ds;
    //    GridView1.DataBind();

    //}
    //protected void GrdViewItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    int i;
    //    i = GridView1.Rows[e.RowIndex].DataItemIndex;


    //    TextBox txtQtyB = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0];
    //    TextBox txtQtyA = (TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0];

    //    GridView1.EditIndex = -1;
    //    DataSet ds1 = (DataSet)Session["data"];

    //    GridView1.DataSource = ds1;
    //    GridView1.DataBind();

    //    DataSet ds = (DataSet)GridView1.DataSource;

    //    ds.Tables[0].Rows[i]["Qty_Bought"] = txtQtyB.Text;
    //    ds.Tables[0].Rows[i]["Qty_Avail"] = txtQtyA.Text;
    //    GridView1.DataSource = ds;
    //    GridView1.DataBind();
    //}
    private void GenerateRoleDs()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dcQtyB = new DataColumn("Qty_Bought");
        DataColumn dcQtyA = new DataColumn("Qty_Avail");

        dt.Columns.Add(dcQtyB);
        dt.Columns.Add(dcQtyA);

        ds.Tables.Add(dt);
        Session["data"] = ds;
    }

    // protected void drpRoleType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string roleType = string.Empty; 
    //           if (((DropDownList)this.frmViewAdd.FindControl("drpRoleType")) != null)
    //               roleType = ((DropDownList)this.frmViewAdd.FindControl("drpRoleType")).SelectedValue;

    //           if (roleType == "Y")
    //           {
    //              // GenerateRoleDs();
    //               pnlRole.Visible = true;
    //           }
    //           else
    //           {
    //               txtRole.Text = "";
    //               GridView1.DataSource = null;
    //               GridView1.DataBind();
    //               Session["data"] = null;
    //               pnlRole.Visible = false;
    //           }

    //    }
    //protected void GrdViewItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    GridView1.EditIndex = -1;
    //    DataSet ds = (DataSet)Session["data"];
    //    GridView1.DataSource = ds;
    //    GridView1.DataBind();
    //}
    protected void btnCLick_Click(object sender, EventArgs e)
    {
        //DataSet ds = (DataSet)Session["data"];
        //DataRow dr = ds.Tables[0].NewRow();
        //dr[0] = txtRole.Text;
        //dr[1] = txtRole.Text;

        //ds.Tables[0].Rows.Add(dr);
        //GridView1.DataSource = ds;
        //GridView1.DataBind();
        //txtRole.Text = "";
    }

    protected void drpMeasure_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;

            FormView frmV = (FormView)((AjaxControlToolkit.TabContainer)((AjaxControlToolkit.TabPanel)ddl.NamingContainer).NamingContainer).NamingContainer;

            if (frmV.DataItem != null)
            {
                string MeasureUnit = ((DataRowView)frmV.DataItem)["Measure_Unit"].ToString();

                ddl.ClearSelection();

                ListItem li = ddl.Items.FindByValue(MeasureUnit);
                if (li != null) li.Selected = true;

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (frmViewAdd.DefaultMode == FormViewMode.Insert)
            {
                if (!DealerRequired())
                {
                    if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                    {
                        if (this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd") != null)
                        {
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd")).Visible = false;
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd1")).Visible = false;
                        }
                    }
                }

            }

            if (frmViewAdd.DefaultMode == FormViewMode.Edit)
            {
                if (!DealerRequired())
                {
                    if (this.frmViewAdd.FindControl("tabEditContol") != null)
                    {
                        if ((this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer")) != null)
                        {
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer")).Visible = false;
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer1")).Visible = false;
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void frmViewAdd_ModeChanged(object sender, EventArgs e)
    {
        try
        {
            if (frmViewAdd.DefaultMode == FormViewMode.Insert)
            {
                if (!DealerRequired())
                {
                    if (this.frmViewAdd.FindControl("tablInsertControl") != null)
                    {
                        if (this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd") != null)
                        {
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd")).Visible = false;
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tablInsertControl").FindControl("tabInsRates").FindControl("rowDealerAdd1")).Visible = false;
                        }
                    }
                }
            }

            if (frmViewAdd.DefaultMode == FormViewMode.Edit)
            {
                if (!DealerRequired())
                {
                    if (this.frmViewAdd.FindControl("tabEditContol") != null)
                    {
                        if ((this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer")) != null)
                        {
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer")).Visible = false;
                            ((HtmlTableRow)this.frmViewAdd.FindControl("tabEditContol").FindControl("tabEditRates").FindControl("rowDealer1")).Visible = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void ddCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

    }

    protected void cmdcat_click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("CategoryMaster.aspx?myname=" + "NEWCAT");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void cmdcategory_click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("CategoryMaster.aspx?myname=" + "NEWCAT");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    

}
