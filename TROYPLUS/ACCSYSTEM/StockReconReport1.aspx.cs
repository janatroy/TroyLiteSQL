﻿using System;
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

public partial class StockReconReport1 : System.Web.UI.Page
{
    public string sDataSource = string.Empty;
    string cond;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!IsPostBack)
            {
                DataSet companyInfo = new DataSet();
                BusinessLogic bl = new BusinessLogic(sDataSource);
                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);
                    //companyInfo = bl.getCompanyInfo("");

                    if (companyInfo != null)
                    {
                        if (companyInfo.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in companyInfo.Tables[0].Rows)
                            {
                                //lblTNGST.Text = Convert.ToString(dr["TINno"]);
                                lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                                lblPhone.Text = Convert.ToString(dr["Phone"]);
                                //lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                                lblAddress.Text = Convert.ToString(dr["Address"]);
                                lblCity.Text = Convert.ToString(dr["city"]);
                                lblPinCode.Text = Convert.ToString(dr["Pincode"]);
                                lblState.Text = Convert.ToString(dr["state"]);

                            }
                        }


                        DataSet ds1 = bl.getImageInfo();
                        if (ds1 != null)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                                {
                                    Image1.ImageUrl = "App_Themes/NewTheme/images/" + ds1.Tables[0].Rows[i]["img_filename"];
                                    Image1.Height = 95;
                                    Image1.Width = 95;
                                }
                            }
                            else
                            {
                                Image1.Height = 95;
                                Image1.Width = 95;
                                Image1.ImageUrl = "App_Themes/NewTheme/images/TESTLogo.png";
                            }
                        }

                        //}
                    }

                }

                txtStartDate.Text = DateTime.Now.ToShortDateString();

                divPrint.Visible = true;

                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DateTime startDate;
                DataSet ds = new DataSet();

                DateTime stdt = Convert.ToDateTime(txtStartDate.Text);

                if (Request.QueryString["startDate"] != null)
                {
                    stdt = Convert.ToDateTime(Request.QueryString["startDate"].ToString());

                    cond = Request.QueryString["cond"].ToString();
                    cond = Server.UrlDecode(cond);
                }

                startDate = Convert.ToDateTime(stdt);
              

                ReportsBL.ReportClass rpt;
                rpt = new ReportsBL.ReportClass();
                Panel.Visible = true;
                div1.Visible = false;
                int zeroCnt = 0;
                int dataCnt = 0;
                ds = rpt.verifyStock(sDataSource, startDate, cond);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["PhysicalStock"] != null)
                        {
                            if (Convert.ToDouble(dr["PhysicalStock"]) == 0)
                                zeroCnt = zeroCnt + 1;
                        }
                    }
                    if (zeroCnt == ds.Tables[0].Rows.Count)
                    {
                        //err.Text = "No physical stock found for the  date " + startDate.ToShortDateString() + " be Generated";
                        gvStock.DataSource = null;
                        gvStock.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No physical stock found for the  date " + startDate.ToShortDateString() + " be Generated')", true);
                        divPrint.Visible = false;
                        Panel.Visible = false;
                        div1.Visible = false;
                        return;
                    }
                    else
                    {
                        err.Text = "";
                        gvStock.DataSource = ds;
                        gvStock.DataBind();
                    }
                }
                else
                {
                    divPrint.Visible = false;
                    Panel.Visible = false;
                    div1.Visible = true;
                    gvStock.DataSource = null;
                    gvStock.DataBind();
                }
                cond = cond.Replace("'", "");
                Label1.Text = " Report Date on " + startDate.ToString("dd/MM/yyyy") + " for " + cond;
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btndet_Click(object sender, EventArgs e)
    {
        try
        {
            div1.Visible = true;
            Panel.Visible = false;
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            divPrint.Visible = true;
            string sDataSource = string.Empty;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            DateTime startDate;
            DataSet ds = new DataSet();
            startDate = Convert.ToDateTime(txtStartDate.Text);
            ReportsBL.ReportClass rpt;
            rpt = new ReportsBL.ReportClass();
            Panel.Visible = true;
            div1.Visible = false;
            int zeroCnt = 0;
            int dataCnt = 0;
            ds = rpt.verifyStock(sDataSource, startDate,"");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["PhysicalStock"] != null)
                    {
                        if (Convert.ToDouble(dr["PhysicalStock"]) == 0)
                            zeroCnt = zeroCnt + 1;
                    }
                }
                if (zeroCnt == ds.Tables[0].Rows.Count)
                {
                    //err.Text = "No physical stock found for the  date " + startDate.ToShortDateString() + " be Generated";
                    gvStock.DataSource = null;
                    gvStock.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No physical stock found for the  date " + startDate.ToShortDateString() + " be Generated')", true);
                    divPrint.Visible = false;
                    Panel.Visible = false;
                    div1.Visible = true;
                    return;
                }
                else
                {
                    err.Text = "";
                    gvStock.DataSource = ds;
                    gvStock.DataBind();
                }
            }
            else
            {
                divPrint.Visible = false;
                Panel.Visible = false;
                div1.Visible = true;
                gvStock.DataSource = null;
                gvStock.DataBind();
            }

            //#region Export To Excel
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add(new DataColumn("ItemCode"));
            //    //dt.Columns.Add(new DataColumn("Productname"));
            //    //dt.Columns.Add(new DataColumn("Model"));
            //    //dt.Columns.Add(new DataColumn("ProductDesc"));
            //    dt.Columns.Add(new DataColumn("ActualStock"));
            //    dt.Columns.Add(new DataColumn("physicalStock"));

            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        DataRow dr_export = dt.NewRow();
            //        dr_export["ItemCode"] = dr["ItemCode"];
            //        //dr_export["Productname"] = dr["Productname"];
            //        //dr_export["Model"] = dr["Model"];
            //        //dr_export["ProductDesc"] = dr["ProductDesc"];
            //        dr_export["ActualStock"] = dr["ActualStock"];
            //        dr_export["physicalStock"] = dr["physicalStock"];
            //        dt.Rows.Add(dr_export);
            //    }

            //    DataRow dr_lastexport = dt.NewRow();
            //    dr_lastexport["ItemCode"] = "";
            //    //dr_lastexport["Productname"] = "";
            //    //dr_lastexport["Model"] = "";
            //    //dr_lastexport["ProductDesc"] = "";
            //    dr_lastexport["ActualStock"] = "";
            //    dr_lastexport["physicalStock"] = "";
            //    ExportToExcel("ReOrderLevelReport.xls", dt);
            //}
            //#endregion
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected void btnRep_Click(object sender, EventArgs e)
    {
        try
        {
            //divPrint.Visible = true;
            string sDataSource = string.Empty;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            DateTime startDate;
            DataSet ds = new DataSet();
            startDate = Convert.ToDateTime(txtStartDate.Text);
            ReportsBL.ReportClass rpt;
            rpt = new ReportsBL.ReportClass();
            int zeroCnt = 0;
            int dataCnt = 0;
            ds = rpt.verifyStock(sDataSource, startDate,"");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["PhysicalStock"] != null)
                    {
                        if (Convert.ToDouble(dr["PhysicalStock"]) == 0)
                            zeroCnt = zeroCnt + 1;
                    }
                }
                if (zeroCnt == ds.Tables[0].Rows.Count)
                {
                    err.Text = "No physical stock found for the  date " + startDate.ToShortDateString() + " be Generated";
                    //gvStock.DataSource = null;
                    //gvStock.DataBind();
                }
                else
                {
                    err.Text = "";
                    //gvStock.DataSource = ds;
                    //gvStock.DataBind();
                }
            }
            else
            {
                //gvStock.DataSource = null;
                //gvStock.DataBind();
            }

            #region Export To Excel
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ItemCode"));
                //dt.Columns.Add(new DataColumn("Productname"));
                //dt.Columns.Add(new DataColumn("Model"));
                //dt.Columns.Add(new DataColumn("ProductDesc"));
                dt.Columns.Add(new DataColumn("ActualStock"));
                dt.Columns.Add(new DataColumn("physicalStock"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow dr_export = dt.NewRow();
                    dr_export["ItemCode"] = dr["ItemCode"];
                    //dr_export["Productname"] = dr["Productname"];
                    //dr_export["Model"] = dr["Model"];
                    //dr_export["ProductDesc"] = dr["ProductDesc"];
                    dr_export["ActualStock"] = dr["ActualStock"];
                    dr_export["physicalStock"] = dr["physicalStock"];
                    dt.Rows.Add(dr_export);
                }

                DataRow dr_lastexport = dt.NewRow();
                dr_lastexport["ItemCode"] = "";
                //dr_lastexport["Productname"] = "";
                //dr_lastexport["Model"] = "";
                //dr_lastexport["ProductDesc"] = "";
                dr_lastexport["ActualStock"] = "";
                dr_lastexport["physicalStock"] = "";
                ExportToExcel("StockReconReport.xls", dt);
            }
            #endregion
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    public void ExportToExcel(string filename, DataTable dt)
    {

        if (dt.Rows.Count > 0)
        {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();

            //Get the HTML for the control.
            dgGrid.RenderControl(hw);
            //Write the HTML back to the browser.
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();
        }

    }

    protected void gvStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //string sDataSource = Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblActual = e.Row.FindControl("lblActual") as Label;
                Label lblPhysical = e.Row.FindControl("lblPhysical") as Label;
                Label lblItem = e.Row.FindControl("lblItem") as Label;
                double actualQty = 0;
                double physicalQty = 0;

                actualQty = Convert.ToDouble(lblActual.Text);
                physicalQty = Convert.ToDouble(lblPhysical.Text);

                if (actualQty != physicalQty)
                {

                    lblActual.Style.Add("Color", "red");
                    lblActual.Style.Add("Font-weight", "Bold");
                    lblActual.Style.Add("text-decoration", "blink");
                    lblPhysical.Style.Add("Color", "Red");
                    lblItem.Style.Add("Color", "Red");
                    lblPhysical.Style.Add("Font-weight", "Bold");
                    lblItem.Style.Add("Font-weight", "Bold");
                    lblPhysical.Style.Add("text-decoration", "blink");
                    //e.Row.Attributes.Add("onload", "this.style.backgroundColor='#000000';this.style.color='#FFFFFF';");
                    if (e.Row.RowIndex % 2 == 0)
                    {
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#E9F1F6';this.style.color='#E48484';");
                    }
                    else
                    {
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF';this.style.color='#E48484';");
                    }
                    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#000000';this.style.color='#FFFFFF';");

                }



            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
}
