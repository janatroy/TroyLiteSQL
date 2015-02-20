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

public partial class BalanceSheetLevel2 : System.Web.UI.Page
{

    public string sDataSource = string.Empty;
    public double debitTotal = 0;
    public double creditTotal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            double sumTotal = 0;
            double dTot = 0;
            int iHeadingID = 0;
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!IsPostBack)
            {
                if (Request["HeadingName"] != null)
                {
                    HeadingName.Text = Request["HeadingName"].ToString();
                }
                if (Request["HeadingID"] != null)
                {
                    iHeadingID = Convert.ToInt32(Request["HeadingID"]);
                }
                DataSet ds = GetBalanceSheet(iHeadingID);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvGroup.DataSource = ds;
                        gvGroup.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    public DataSet GetBalanceSheet(int HeadingID)
    {

        BusinessLogic bl = new BusinessLogic(sDataSource);

        DataSet mainDs = new DataSet();
        DataSet GroupDs = new DataSet();
        DataTable grdDt = new DataTable();
        DataTable dtNew = new DataTable();

        DataSet grdDs = new DataSet();

        Double debitSum = 0;
        Double creditSum = 0;
        Double totalSum = 0;
        Double netSum = 0;
        string sGroup = string.Empty;
        int iHeading = HeadingID;
        int groupID = 0;

        dtNew = GenerateDs("", "", "");
        grdDs.Tables.Add(dtNew);



        GroupDs = bl.GetGroupsForHeadiing(iHeading);
        if (GroupDs != null)
        {
            if (GroupDs.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow groupRow in GroupDs.Tables[0].Rows)
                {
                    if (groupRow["GroupID"] != null)
                    {
                        groupID = Convert.ToInt32(groupRow["GroupID"]);
                        debitSum = bl.GetDebitSum(groupID);
                        creditSum = bl.GetCreditSum(groupID);

                        totalSum = debitSum - creditSum;


                    }
                    if (groupRow["GroupName"] != null)
                    {
                        sGroup = groupRow["GroupName"].ToString();
                    }

                    if (totalSum < 0)
                        totalSum = Math.Abs(totalSum);
                    netSum = netSum + totalSum;
                    grdDt = GenerateDs(groupID.ToString(), sGroup, totalSum.ToString("f2"));
                    if (grdDt != null)
                    {
                        for (int k = 0; k <= grdDt.Rows.Count - 1; k++)
                        {

                            if (grdDt != null && grdDt.Rows.Count > 0)
                                grdDs.Tables[0].ImportRow(grdDt.Rows[k]);
                        }


                    }

                }// GroupDs ForEach


            }//GrouopDs Tables Count
        }//GroupDs null Check






        return grdDs;
    }


    public DataTable GenerateDs(string groupID, string GroupName, string strSum)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        DataColumn dc;
        DataRow dr;

        dc = new DataColumn("GroupID");
        dt.Columns.Add(dc);

        dc = new DataColumn("GroupName");
        dt.Columns.Add(dc);

        //dc = new DataColumn("TransDate");
        //dt.Columns.Add(dc);
        dc = new DataColumn("sum");
        dt.Columns.Add(dc);


        dr = dt.NewRow();
        dr["GroupID"] = groupID;
        dr["GroupName"] = GroupName;

        dr["sum"] = strSum;
        //dr["TransDate"] = "";

        dt.Rows.Add(dr);
        //ds.Tables[0].Rows.Add(dr);
        return dt;
    }

    //gvAssetBalance_RowDataBound
    //gvLiabilityBalance_RowDataBound
    public void gvGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSum = (Label)e.Row.FindControl("lblSum");

                if (lblSum != null && lblSum.Text != "")
                    debitTotal = debitTotal + Convert.ToDouble(lblSum.Text);

            }
            if (debitTotal > 0)
                lblDebitTotal.Text = debitTotal.ToString("f2") + " Dr";
            else
            {
                debitTotal = Math.Abs(debitTotal);
                lblDebitTotal.Text = debitTotal.ToString("f2") + " Cr";
            }
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='ALiceblue';");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='khaki';");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);

        }
    }
}
