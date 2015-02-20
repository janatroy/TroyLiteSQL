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

public partial class ProjectReport1 : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
            if (!IsPostBack)
            {
                if (Request.Cookies["Company"] == null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                DataSet companyInfo = new DataSet();
                string connection = Request.Cookies["Company"].Value;
                BusinessLogic bl = new BusinessLogic(sDataSource);
                lblBillDate.Text = DateTime.Now.ToShortDateString();
                if (Request.Cookies["Company"] != null)
                {
                    companyInfo = bl.getCompanyInfo(Request.Cookies["Company"].Value);

                    if (companyInfo != null)
                    {
                        if (companyInfo.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in companyInfo.Tables[0].Rows)
                            {
                                lblTNGST.Text = Convert.ToString(dr["TINno"]);
                                lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                                lblPhone.Text = Convert.ToString(dr["Phone"]);
                                lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                                lblAddress.Text = Convert.ToString(dr["Address"]);
                                lblCity.Text = Convert.ToString(dr["city"]);
                                lblPincode.Text = Convert.ToString(dr["Pincode"]);
                                lblState.Text = Convert.ToString(dr["state"]);

                            }
                        }
                    }
                }
                divPrint.Visible = true;
                int empno = 0;
                int pro_Id = 0;
                string flag = "";
                int status = 0;
                int taskid = 0;
                string isactive = "";
                int deptask = 0;
                int incharge = 0;

                if (Request.QueryString["incharge"] != null)
                {
                    incharge = Convert.ToInt32(Request.QueryString["incharge"].ToString());
                }

                if (Request.QueryString["employee"] != null)
                {
                    empno = Convert.ToInt32(Request.QueryString["employee"].ToString());
                }
                if (Request.QueryString["project"] != null)
                {
                    pro_Id = Convert.ToInt32(Request.QueryString["project"].ToString());
                }
                if (Request.QueryString["BlockedTask"] != null)
                {
                    flag = Convert.ToString(Request.QueryString["BlockedTask"].ToString());
                }
                if (Request.QueryString["CompletedTask"] != null)
                {
                    status = Convert.ToInt32(Request.QueryString["CompletedTask"].ToString());
                }
                if (Request.QueryString["Task"] != null)
                {
                    taskid = Convert.ToInt32(Request.QueryString["Task"].ToString());
                }
                if (Request.QueryString["DependencyTask"] != null)
                {
                    deptask = Convert.ToInt32(Request.QueryString["DependencyTask"].ToString());
                }
                if (Request.QueryString["isactive"] != null)
                {
                    isactive = Convert.ToString(Request.QueryString["isactive"].ToString());
                }
                string cond = string.Empty;




                cond = "1=1  ";


                if ((pro_Id != 0))
                {


                    cond += " and tblProjects.Project_Id=" + pro_Id + "";
                }






                cond += " and tblEmployee.empno=" + incharge + "";


                DataSet ds = new DataSet();
                ds = bl.GetProjectManagementReport(connection, incharge, empno, pro_Id, flag, status, taskid, deptask, isactive, cond);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        //gvOuts1.DataSource = ds;
                        //gvOuts1.DataBind();
                    }
                    else
                    {
                        //gvOuts1.DataSource = null;
                        //gvOuts1.DataBind();
                    }
                }
                else
                {

                    //gvOuts1.DataSource = null;
                    //gvOuts1.DataBind();
                }

            }


            btnReport_Click(sender, e);

        }


        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected string getCond()
    {
        int empno = 0;
        int pro_Id = 0;
        string flag = "";
        int status = 0;
        int taskid = 0;
        string isactive = "";
        int deptask = 0;
        int incharge = 0;

        if (Request.QueryString["incharge"] != null)
        {
            incharge = Convert.ToInt32(Request.QueryString["incharge"].ToString());
        }

        if (Request.QueryString["employee"] != null)
        {
            empno = Convert.ToInt32(Request.QueryString["employee"].ToString());
        }
        if (Request.QueryString["project"] != null)
        {
            pro_Id = Convert.ToInt32(Request.QueryString["project"].ToString());
        }
        if (Request.QueryString["BlockedTask"] != null)
        {
            flag = Convert.ToString(Request.QueryString["BlockedTask"].ToString());
        }
        if (Request.QueryString["CompletedTask"] != null)
        {
            status = Convert.ToInt32(Request.QueryString["CompletedTask"].ToString());
        }
        if (Request.QueryString["Task"] != null)
        {
            taskid = Convert.ToInt32(Request.QueryString["Task"].ToString());
        }
        if (Request.QueryString["DependencyTask"] != null)
        {
            deptask = Convert.ToInt32(Request.QueryString["DependencyTask"].ToString());
        }
        if (Request.QueryString["isactive"] != null)
        {
            isactive = Convert.ToString(Request.QueryString["isactive"].ToString());
        }
        string cond = string.Empty;




        cond = "1=1  ";




        if ((empno != 0))
        {

            cond += " and tblEmployee.empno=" + empno + "";
        }

        if (flag != "NA")
        {

            cond += " and  tblTaskUpdates.Blocked_Flag ='" + flag + "'";
        }

        if (isactive != "NA")
        {

            cond += " and tblTasks.IsActive ='" + isactive + "'";
        }

        if ((status != 0))
        {

            cond += " and tblTaskUpdates.Task_Status =" + Convert.ToInt32(status) + " ";

        }
        if ((taskid != 0))
        {

            cond += " and tblTasks.Task_Id =" + Convert.ToInt32(taskid) + " ";
        }

        if ((deptask != 0))
        {

            cond += " and tblTasks.Dependency_Task =" + Convert.ToInt32(deptask) + " ";
        }


        // cond += " and tblTasks.IsActive ='" + isactive + "' ";





        return cond;
    }

    protected void gvProducts_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvOuts1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string flag = "";
            int status = 0;
            string condition = getCond();
            //string sDataSource =  Server.MapPath("App_Data\\Store0910.mdb");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Request.QueryString["BlockedTask"] != null)
                {
                    flag = Convert.ToString(Request.QueryString["BlockedTask"].ToString());
                }
                if (Request.QueryString["CompletedTask"] != null)
                {
                    status = Convert.ToInt32(Request.QueryString["CompletedTask"].ToString());
                }

                GridView gv = e.Row.FindControl("gvProducts") as GridView;
                Label ProjectName = e.Row.FindControl("lblprojectname") as Label;
                BusinessLogic bl = new BusinessLogic();
                string connection = Request.Cookies["Company"].Value;

                if (Request.Cookies["Company"] != null)
                    sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();

                string Project_Name = Convert.ToString(ProjectName.Text);

                DataSet ds = bl.GettaskforProjectName(connection, Project_Name, condition, flag, status);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }


            }
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
            int empno = 0;
            int pro_Id = 0;
            string flag = "";
            int status = 0;
            int taskid = 0;
            string isactive = "";
            int deptask = 0;
            int incharge = 0;
            string connection = Request.Cookies["Company"].Value;
            string cond = string.Empty;
            string condtask = string.Empty;
            string condtaskupdate = string.Empty;



            System.Text.StringBuilder htmlcode = new System.Text.StringBuilder();
            htmlcode.Append("<html><body>");
            //htmlcode.Append("<form id=form1 runat=server>");
            htmlcode.Append("<div id=divPrint1 style=font-family:'Trebuchet MS'; font-size:11px;  >");

            DataSet companyInfo = new DataSet();
            // string connection = Request.Cookies["Company"].Value;

            lblBillDate.Text = DateTime.Now.ToShortDateString();
            if (Request.Cookies["Company"] != null)
            {
                BusinessLogic bl1 = new BusinessLogic(sDataSource);
                companyInfo = bl1.getCompanyInfo(Request.Cookies["Company"].Value);
                int l = 0;
                if (companyInfo != null)
                {
                    if (companyInfo.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in companyInfo.Tables[0].Rows)
                        {
                            lblTNGST.Text = Convert.ToString(dr["TINno"]);
                            lblCompany.Text = Convert.ToString(dr["CompanyName"]);
                            lblPhone.Text = Convert.ToString(dr["Phone"]);
                            lblGSTno.Text = Convert.ToString(dr["GSTno"]);

                            lblAddress.Text = Convert.ToString(dr["Address"]);
                            lblCity.Text = Convert.ToString(dr["city"]);
                            lblPincode.Text = Convert.ToString(dr["Pincode"]);
                            lblState.Text = Convert.ToString(dr["state"]);
                            //htmlcode.Append("<Table id = table4  >");
                            //htmlcode.Append("<tr>");
                            //htmlcode.Append("<td align=center width=320px style=font-size: 20px;>company name ");
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("<td align=Centre>" + companyInfo.Tables[0].Rows[l].ItemArray[0].ToString());
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("<tr>");
                            //htmlcode.Append("<tr>");
                            //htmlcode.Append("<td align=center width=320px >Address");
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("<td width=320px>" + companyInfo.Tables[0].Rows[l].ItemArray[1].ToString());
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("<tr>");
                            //htmlcode.Append("<td align=center>City");
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("<td>" + companyInfo.Tables[0].Rows[l].ItemArray[2].ToString());
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("</tr>");
                            //htmlcode.Append("<tr>");
                            //htmlcode.Append("<td align=center> State");
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("<td>" + companyInfo.Tables[0].Rows[l].ItemArray[3].ToString());
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("</tr>");
                            //htmlcode.Append("<tr>");                           
                            //htmlcode.Append("<td width=140px align=left> Pincode");
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("<td>" + companyInfo.Tables[0].Rows[l].ItemArray[5].ToString());
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("</tr>");
                            //htmlcode.Append("<td width=140px align=left>Tin Number");
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("<td>" + companyInfo.Tables[0].Rows[l].ItemArray[4].ToString());
                            //htmlcode.Append("</td>");









                        }
                    }
                }
            }
            divPrint.Visible = true;


            DataSet dsVat = new DataSet();
            DataSet dsCst = new DataSet();
            DataSet ds1 = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);



            int i = 0;
            int j = 0;
            int k = 0;

            DataSet dspro = new DataSet();



            //int empno = 0;
            //int pro_Id = 0;
            //string flag = "";
            //int status = 0;
            //int taskid = 0;
            //string isactive = "";
            //int deptask = 0;
            //int incharge = 0;
            if (Request.QueryString["employee"] != null)
            {
                empno = Convert.ToInt32(Request.QueryString["employee"].ToString());
            }

            if (Request.QueryString["incharge"] != null)
            {
                incharge = Convert.ToInt32(Request.QueryString["incharge"].ToString());
            }


            if (Request.QueryString["project"] != null)
            {
                pro_Id = Convert.ToInt32(Request.QueryString["project"].ToString());
            }
            if (Request.QueryString["BlockedTask"] != null)
            {
                flag = Convert.ToString(Request.QueryString["BlockedTask"].ToString());
            }
            if (Request.QueryString["CompletedTask"] != null)
            {
                status = Convert.ToInt32(Request.QueryString["CompletedTask"].ToString());
            }
            if (Request.QueryString["Task"] != null)
            {
                taskid = Convert.ToInt32(Request.QueryString["Task"].ToString());
            }
            if (Request.QueryString["DependencyTask"] != null)
            {
                deptask = Convert.ToInt32(Request.QueryString["DependencyTask"].ToString());
            }
            if (Request.QueryString["isactive"] != null)
            {
                isactive = Convert.ToString(Request.QueryString["isactive"].ToString());
            }
            //  string cond = string.Empty;
            cond = " 1=1 ";
            if ((pro_Id != 0))
            {


                cond += " and  tblProjects.Project_Id=" + pro_Id + "";
            }




            cond += " and tblEmployee.empno=" + incharge + "";

            dspro = bl.GetProjectManagementReport(connection, pro_Id, incharge, empno, flag, status, taskid, deptask, isactive, cond);
            DataSet ds = new DataSet();
            int projectid = 0;
            int tasksid = 0;
            if (dspro.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < dspro.Tables[0].Rows.Count; i++)
                {
                    if (dspro.Tables[0].Rows[i].ItemArray[0].ToString() == "0")
                    {
                        //htmlcode.Append("<td> NO Data Found");
                        //htmlcode.Append("</td>");

                    }
                    else
                    {
                        htmlcode.Append("<Table id = table1 border=1px solid blue cellpadding=0 cellspacing=0 class=tblLeft width=100% >");
                        htmlcode.Append("<tr class=ReportHeadataRow style=text-align:left>");
                        htmlcode.Append("<td style=width:15%> Project Name");
                        htmlcode.Append("</td>");
                        htmlcode.Append("<td style=width:15%> Manager Name");
                        htmlcode.Append("</td>");
                        htmlcode.Append("<td style=width:15%> Project Date");
                        htmlcode.Append("</td>");
                        htmlcode.Append("<td style=width:15%> Expected Start Date");
                        htmlcode.Append("</td>");
                        htmlcode.Append("<td style=width:15%> Expected End Date");
                        htmlcode.Append("</td>");
                        htmlcode.Append("<td style=width:15%> Project Status");
                        htmlcode.Append("</td>");

                        htmlcode.Append("<tr class=ReportdataRow>");
                        htmlcode.Append("<td>" + dspro.Tables[0].Rows[i].ItemArray[0].ToString());
                        htmlcode.Append("</td>");
                        htmlcode.Append("<td>" + dspro.Tables[0].Rows[i].ItemArray[1].ToString());
                        htmlcode.Append("</td>");
                        htmlcode.Append("<td>" + dspro.Tables[0].Rows[i].ItemArray[2].ToString());
                        htmlcode.Append("</td>");
                        htmlcode.Append("<td>" + dspro.Tables[0].Rows[i].ItemArray[3].ToString());
                        htmlcode.Append("</td>");
                        htmlcode.Append("<td>" + dspro.Tables[0].Rows[i].ItemArray[5].ToString());
                        htmlcode.Append("</td>");
                        htmlcode.Append("<td>" + dspro.Tables[0].Rows[i].ItemArray[4].ToString());
                        htmlcode.Append("</td>");


                        projectid = Convert.ToInt32(dspro.Tables[0].Rows[i].ItemArray[6].ToString());










                        //     DataSet dsproject = bl.GetDependencytask(connection, pro_Id);




                        //  string pro_Id = string.Empty;

                        if (Request.QueryString["project"] != null)
                        {
                            pro_Id = Convert.ToInt32(Request.QueryString["project"].ToString());
                        }

                        if (Request.QueryString["isactive"] != null)
                        {
                            isactive = Convert.ToString(Request.QueryString["isactive"].ToString());
                        }
                        if (Request.QueryString["CompletedTask"] != null)
                        {
                            status = Convert.ToInt32(Request.QueryString["CompletedTask"].ToString());
                        }
                        condtask = " and 1=1 ";


                        if ((empno != 0))
                        {

                            condtask += " and tblEmployee.empno=" + empno + "";
                        }

                        if ((taskid != 0))
                        {

                            condtask += " and tblTasks.Task_Id =" + Convert.ToInt32(taskid) + " ";
                        }

                        if ((deptask != 0))
                        {

                            condtask += " and tblTasks.Dependency_Task =" + Convert.ToInt32(deptask) + " ";
                        }



                        if (isactive != "NA")
                        {

                            condtask += " and tblTasks.IsActive ='" + isactive + "'";
                        }


                        // condtask += " and tblTasks.IsActive ='" + isactive + "' ";
                        //  int Project_Id = Convert.ToInt32(pro_Id);

                        DataSet dstask = bl.GettaskforProjectNamereport(connection, projectid, isactive, status, condtask);
                        if (dstask != null)
                        {
                            if (dstask.Tables[0].Rows.Count > 0)
                            {

                                for (j = 0; j < dstask.Tables[0].Rows.Count; j++)
                                {
                                    if (dstask.Tables[0].Rows[j].ItemArray[0].ToString() == "0")
                                    {
                                        //htmlcode.Append("<td> NO Data Found");
                                        //htmlcode.Append("</td>");

                                    }
                                    else
                                    {

                                        htmlcode.Append("<Table id = table2 border=1px solid blue cellpadding=0 cellspacing=50px class=tblLeft width=100% >");
                                        htmlcode.Append("<tr class=Titlereport style=width:15% >Task and its Updates");
                                        htmlcode.Append("</tr>");
                                        htmlcode.Append("<tr class=ReportHeadataRow style=text-align:left>");
                                        htmlcode.Append("<td style=width:15%> Task Name");
                                        htmlcode.Append("</td>");
                                        htmlcode.Append("<td style=width:15%> Expected Start Date");
                                        htmlcode.Append("</td>");
                                        htmlcode.Append("<td style=width:15%> Expected End Date");
                                        htmlcode.Append("</td>");
                                        htmlcode.Append("<td style=width:15%> Owner");
                                        htmlcode.Append("</td>");
                                        htmlcode.Append("<td style=width:15%> Task Type");
                                        htmlcode.Append("</td>");
                                        //htmlcode.Append("<td style=width:15%> Dependency Task");
                                        //htmlcode.Append("</td>");
                                        htmlcode.Append("<td style=width:15%> Is Active");
                                        htmlcode.Append("</td>");


                                        htmlcode.Append("<tr class=ReportdataRow>");
                                        htmlcode.Append("<td>" + dstask.Tables[0].Rows[j].ItemArray[0].ToString());
                                        htmlcode.Append("</td>");
                                        htmlcode.Append("<td>" + dstask.Tables[0].Rows[j].ItemArray[1].ToString());
                                        htmlcode.Append("</td>");
                                        htmlcode.Append("<td>" + dstask.Tables[0].Rows[j].ItemArray[2].ToString());
                                        htmlcode.Append("</td>");
                                        htmlcode.Append("<td>" + dstask.Tables[0].Rows[j].ItemArray[3].ToString());
                                        htmlcode.Append("</td>");
                                        htmlcode.Append("<td>" + dstask.Tables[0].Rows[j].ItemArray[4].ToString());
                                        htmlcode.Append("</td>");
                                        //htmlcode.Append("<td>" + dstask.Tables[0].Rows[j].ItemArray[5].ToString());
                                        //htmlcode.Append("</td>");
                                        htmlcode.Append("<td>" + dstask.Tables[0].Rows[j].ItemArray[5].ToString());
                                        htmlcode.Append("</td>");

                                        tasksid = Convert.ToInt32(dstask.Tables[0].Rows[j].ItemArray[6].ToString());






                                        // condtaskupdate = " 1=1 ";






                                        condtaskupdate = " and 1=1 ";
                                        if (flag != "NA")
                                        {

                                            condtaskupdate += " and tblTaskUpdatesHistory.Blocked_Flag ='" + flag + "'";
                                        }

                                        if ((status != 0))
                                        {

                                            condtaskupdate += " and tblTaskUpdatesHistory.Task_Status =" + Convert.ToInt32(status) + " ";

                                        }

                                        DataSet dstaskupdate = bl.GettaskupdatehistoryfortaskNamereport(connection, tasksid, flag, status, condtaskupdate);
                                        if (dstaskupdate != null)
                                        {
                                            if (dstaskupdate.Tables[0].Rows.Count > 0)
                                            {
                                                htmlcode.Append("<Table id = table3 border=1px solid blue cellpadding=0 cellspacing=0 class=tblLeft width=100% >");

                                                htmlcode.Append("<tr class=ReportHeadataRow style=text-align:left>");
                                                htmlcode.Append("<td> Actual Start Date");
                                                htmlcode.Append("</td>");
                                                htmlcode.Append("<td> Actual End Date");
                                                htmlcode.Append("</td>");
                                                htmlcode.Append("<td> % of completion");
                                                htmlcode.Append("</td>");
                                                htmlcode.Append("<td> Task Status");
                                                htmlcode.Append("</td>");
                                                htmlcode.Append("<td> Effort remaning");
                                                htmlcode.Append("</td>");
                                                htmlcode.Append("<td> Effort putin last update");
                                                htmlcode.Append("</td>");
                                                htmlcode.Append("<td> Blocked flag");
                                                htmlcode.Append("</td>");
                                                htmlcode.Append("<td> Blocked description");
                                                htmlcode.Append("</td>");

                                                for (k = 0; k < dstaskupdate.Tables[0].Rows.Count; k++)
                                                {
                                                    if (dstaskupdate.Tables[0].Rows[k].ItemArray[0].ToString() == "0")
                                                    {
                                                        //htmlcode.Append("<td> NO Data Found");
                                                        //htmlcode.Append("</td>");

                                                    }
                                                    else
                                                    {



                                                        htmlcode.Append("<tr class=ReportdataRow>");
                                                        htmlcode.Append("<td>" + dstaskupdate.Tables[0].Rows[k].ItemArray[0].ToString());
                                                        htmlcode.Append("</td>");
                                                        htmlcode.Append("<td>" + dstaskupdate.Tables[0].Rows[k].ItemArray[1].ToString());
                                                        htmlcode.Append("</td>");
                                                        htmlcode.Append("<td>" + dstaskupdate.Tables[0].Rows[k].ItemArray[2].ToString());
                                                        htmlcode.Append("</td>");
                                                        htmlcode.Append("<td>" + dstaskupdate.Tables[0].Rows[k].ItemArray[3].ToString());
                                                        htmlcode.Append("</td>");
                                                        htmlcode.Append("<td>" + dstaskupdate.Tables[0].Rows[k].ItemArray[4].ToString());
                                                        htmlcode.Append("</td>");
                                                        htmlcode.Append("<td>" + dstaskupdate.Tables[0].Rows[k].ItemArray[5].ToString());
                                                        htmlcode.Append("</td>");
                                                        htmlcode.Append("<td>" + dstaskupdate.Tables[0].Rows[k].ItemArray[6].ToString());
                                                        htmlcode.Append("</td>");
                                                        htmlcode.Append("<td>" + dstaskupdate.Tables[0].Rows[k].ItemArray[7].ToString());
                                                        htmlcode.Append("</td>");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //htmlcode.Append("<tr>");
                                            //htmlcode.Append("<td> NO Data Found");
                                            //htmlcode.Append("</td>");
                                            //htmlcode.Append("</tr>");
                                        }
                                        htmlcode.Append("</tr>");
                                        htmlcode.Append("<tr height=12px > ");
                                        htmlcode.Append("</tr>");
                                        htmlcode.Append("</Table>");
                                    }

                                }
                            }
                        }
                        else
                        {
                            //htmlcode.Append("<tr>");
                            //htmlcode.Append("<td> NO Data Found");
                            //htmlcode.Append("</td>");
                            //htmlcode.Append("</tr>");
                        }
                        //htmlcode.Append("</tr>");
                        //htmlcode.Append("<tr height=12px > ");
                        //htmlcode.Append("</tr>");
                        htmlcode.Append("</Table>");

                    }
                }

            }

            else
            {
                //Label.text="Error";
            }
            htmlcode.Append("<tr height=12px > ");
            htmlcode.Append("</tr>");
            htmlcode.Append("</Table>");
            htmlcode.Append("</Table>");

            htmlcode.Append("</div>");


            //htmlcode.Append("</form>");
            htmlcode.Append("</body></html>");

            string s = htmlcode.ToString();
            divPrint.InnerHtml = htmlcode.ToString();
        }



        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    protected string condtask()
    {
        int empno = 0;
        int pro_Id = 0;
        int taskid = 0;
        string isactive = "";
        int deptask = 0;
        //int incharge = 0;

        //if (Request.QueryString["incharge"] != null)
        //{
        //    incharge = Convert.ToInt32(Request.QueryString["incharge"].ToString());
        //}

        if (Request.QueryString["employee"] != null)
        {
            empno = Convert.ToInt32(Request.QueryString["employee"].ToString());
        }
        if (Request.QueryString["project"] != null)
        {
            pro_Id = Convert.ToInt32(Request.QueryString["project"].ToString());
        }
        //if (Request.QueryString["BlockedTask"] != null)
        //{
        //    flag = Convert.ToString(Request.QueryString["BlockedTask"].ToString());
        //}
        //if (Request.QueryString["CompletedTask"] != null)
        //{
        //    status = Convert.ToInt32(Request.QueryString["CompletedTask"].ToString());
        //}
        if (Request.QueryString["Task"] != null)
        {
            taskid = Convert.ToInt32(Request.QueryString["Task"].ToString());
        }
        if (Request.QueryString["DependencyTask"] != null)
        {
            deptask = Convert.ToInt32(Request.QueryString["DependencyTask"].ToString());
        }
        if (Request.QueryString["isactive"] != null)
        {
            isactive = Convert.ToString(Request.QueryString["isactive"].ToString());
        }
        string condtask = string.Empty;




        condtask = "1=1  ";




        if ((empno != 0))
        {

            condtask += " and tblEmployee.empno=" + empno + "";
        }

        //if (flag != "NA")
        //{

        //    cond += " and  tblTaskUpdates.Blocked_Flag ='" + flag + "'";
        //}

        //if ((status != 0))
        //{

        //    cond += " and tblTaskUpdates.Task_Status =" + Convert.ToInt32(status) + " ";

        //}
        if ((taskid != 0))
        {

            condtask += " and tblTasks.Task_Id =" + Convert.ToInt32(taskid) + " ";
        }

        if ((deptask != 0))
        {

            condtask += " and tblTasks.Dependency_Task =" + Convert.ToInt32(deptask) + " ";
        }


        condtask += " and tblTasks.IsActive ='" + isactive + "' ";





        return condtask;
    }


    protected string condtaskupdate()
    {
        int empno = 0;
        int pro_Id = 0;
        int taskid = 0;
        string isactive = "";
        int deptask = 0;
        string flag = "";
        int status = 0;
        //int incharge = 0;

        //if (Request.QueryString["incharge"] != null)
        //{
        //    incharge = Convert.ToInt32(Request.QueryString["incharge"].ToString());
        //}

        //if (Request.QueryString["employee"] != null)
        //{
        //    empno = Convert.ToInt32(Request.QueryString["employee"].ToString());
        //}
        //if (Request.QueryString["project"] != null)
        //{
        //    pro_Id = Convert.ToInt32(Request.QueryString["project"].ToString());
        //}
        //if (Request.QueryString["BlockedTask"] != null)
        //{
        //    flag = Convert.ToString(Request.QueryString["BlockedTask"].ToString());
        //}
        //if (Request.QueryString["CompletedTask"] != null)
        //{
        //    status = Convert.ToInt32(Request.QueryString["CompletedTask"].ToString());
        //}
        if (Request.QueryString["Task"] != null)
        {
            taskid = Convert.ToInt32(Request.QueryString["Task"].ToString());
        }
        if (Request.QueryString["DependencyTask"] != null)
        {
            deptask = Convert.ToInt32(Request.QueryString["DependencyTask"].ToString());
        }
        if (Request.QueryString["isactive"] != null)
        {
            isactive = Convert.ToString(Request.QueryString["isactive"].ToString());
        }


        if (Request.QueryString["BlockedTask"] != null)
        {
            flag = Convert.ToString(Request.QueryString["BlockedTask"].ToString());
        }
        if (Request.QueryString["CompletedTask"] != null)
        {
            status = Convert.ToInt32(Request.QueryString["CompletedTask"].ToString());
        }

        string condtaskupdate = string.Empty;


        condtaskupdate = "1=1  ";




        if ((empno != 0))
        {

            condtaskupdate += " and tblEmployee.empno=" + empno + "";
        }

        //if (flag != "NA")
        //{

        //    cond += " and  tblTaskUpdates.Blocked_Flag ='" + flag + "'";
        //}

        //if ((status != 0))
        //{

        //    cond += " and tblTaskUpdates.Task_Status =" + Convert.ToInt32(status) + " ";

        //}
        if ((taskid != 0))
        {

            condtaskupdate += " and tblTasks.Task_Id =" + Convert.ToInt32(taskid) + " ";
        }

        //if ((deptask != 0))
        //{

        //    condtaskupdate += " and tblTasks.Dependency_Task =" + Convert.ToInt32(deptask) + " ";
        //}

        if (flag != "NA")
        {

            condtaskupdate += " and  tblTaskUpdatesHistory.Blocked_Flag ='" + flag + "'";
        }

        if ((status != 0))
        {

            condtaskupdate += " and tblTaskUpdatesHistory.Task_Status =" + Convert.ToInt32(status) + " ";

        }


        //condtaskupdate += " and tblTasks.IsActive ='" + isactive + "' ";





        return condtaskupdate;
    }



}