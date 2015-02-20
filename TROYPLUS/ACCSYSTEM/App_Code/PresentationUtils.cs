using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for PresentationUtils
/// </summary>
public class PresentationUtils
{
	public PresentationUtils()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void SetPagerButtonStates(GridView gridView, GridViewRow gvPagerRow, Page page)
    {


        int pageIndex = gridView.PageIndex;
        int pageCount = gridView.PageCount;

        Button btnFirst = (Button)gvPagerRow.FindControl("btnFirst");
        Button btnPrevious = (Button)gvPagerRow.FindControl("btnPrevious");
        Button btnNext = (Button)gvPagerRow.FindControl("btnNext");
        Button btnLast = (Button)gvPagerRow.FindControl("btnLast");

        btnFirst.Enabled = btnPrevious.Enabled = (pageIndex != 0);
        btnNext.Enabled = btnLast.Enabled = (pageIndex < (pageCount - 1));


        DropDownList ddlPageSelector = (DropDownList)gvPagerRow.FindControl("ddlPageSelector");

        ddlPageSelector.Items.Clear();

        for (int i = 1; i <= gridView.PageCount; i++)
        {

            ddlPageSelector.Items.Add(i.ToString());

        }

        ddlPageSelector.SelectedIndex = pageIndex;
        //Anonymous method (see another way to do this at the bottom)

        ddlPageSelector.SelectedIndexChanged += delegate
        {

            gridView.PageIndex = ddlPageSelector.SelectedIndex;

            gridView.DataBind();

        };



    }

    public static void SetPagerButtonStates2(GridView gridView, GridViewRow gvPagerRow, Page page)
    {


        int pageIndex = gridView.PageIndex;
        int pageCount = gridView.PageCount;

        Button btnFirst = (Button)gvPagerRow.FindControl("btnFirst");
        Button btnPrevious = (Button)gvPagerRow.FindControl("btnPrevious");
        Button btnNext = (Button)gvPagerRow.FindControl("btnNext");
        Button btnLast = (Button)gvPagerRow.FindControl("btnLast");

        btnFirst.Enabled = btnPrevious.Enabled = (pageIndex != 0);
        btnNext.Enabled = btnLast.Enabled = (pageIndex < (pageCount - 1));


        DropDownList ddlPageSelector = (DropDownList)gvPagerRow.FindControl("ddlPageSelector2");

        ddlPageSelector.Items.Clear();

        for (int i = 1; i <= gridView.PageCount; i++)
        {

            ddlPageSelector.Items.Add(i.ToString());

        }

        ddlPageSelector.SelectedIndex = pageIndex;
        //Anonymous method (see another way to do this at the bottom)

        ddlPageSelector.SelectedIndexChanged += delegate
        {

            gridView.PageIndex = ddlPageSelector.SelectedIndex;

            gridView.DataBind();

        };



    }


    public static void SetButtonStates(GridView gridView, GridViewRow gvPagerRow, Page page)
    {


        int pageIndex = gridView.PageIndex;

        int pageCount = gridView.PageCount;


        Button btnFirst = (Button)gvPagerRow.FindControl("btnFirst");

        Button btnPrevious = (Button)gvPagerRow.FindControl("btnPrevious");

        Button btnNext = (Button)gvPagerRow.FindControl("btnNext");

        Button btnLast = (Button)gvPagerRow.FindControl("btnLast");



        btnFirst.Enabled = btnPrevious.Enabled = (pageIndex != 0);

        btnNext.Enabled = btnLast.Enabled = (pageIndex < (pageCount - 1));


    }
    
    public static void SetPagerStates(GridView gridView, GridViewRow gvPagerRow, Page page)
    {


        int pageIndex = gridView.PageIndex;

        int pageCount = gridView.PageCount;


       
        DropDownList ddlPageSelector = (DropDownList)gvPagerRow.FindControl("ddlPageSelector");

        ddlPageSelector.Items.Clear();

        for (int i = 1; i <= gridView.PageCount; i++)
        {

            ddlPageSelector.Items.Add(i.ToString());

        }



        ddlPageSelector.SelectedIndex = pageIndex;



        //Anonymous method (see another way to do this at the bottom)

        ddlPageSelector.SelectedIndexChanged += delegate
        {

            gridView.PageIndex = ddlPageSelector.SelectedIndex;

            gridView.DataBind();

        };



    }

    public static void SetPagerDropDownState(GridView gridView, GridViewRow gvPagerRow, Page page)
    {


        int pageIndex = gridView.PageIndex;

        int pageCount = gridView.PageCount;


       
        DropDownList ddlPageSelector = (DropDownList)gvPagerRow.FindControl("ddlPageSelector");

        ddlPageSelector.Items.Clear();

        for (int i = 1; i <= gridView.PageCount; i++)
        {

            ddlPageSelector.Items.Add(i.ToString());

        }



        ddlPageSelector.SelectedIndex = pageIndex;



        //Anonymous method (see another way to do this at the bottom)

        ddlPageSelector.SelectedIndexChanged += delegate
        {

            gridView.PageIndex = ddlPageSelector.SelectedIndex;

            gridView.DataBind();

        };



    }

    

}
