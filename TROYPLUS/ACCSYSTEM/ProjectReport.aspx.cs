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

public partial class ProjectReport : System.Web.UI.Page
{
    private string sDataSource = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                sDataSource = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
                loadEmp();
                loadmanager();

                //BusinessLogic bl = new BusinessLogic(sDataSource);
                //string connection = Request.Cookies["Company"].Value;
                //string usernam = Request.Cookies["LoggedUserName"].Value;

                //if (bl.CheckUserHaveAdd(usernam, "Treport"))
                //{
                //    btnprojectreport.Enabled = false;
                //    btnprojectreport.ToolTip = "You are not allowed to make Add New ";
                //}
                //else
                //{
                //    btnprojectreport.Enabled = true;
                //    btnprojectreport.ToolTip = "Click to Add New item ";
                //}
            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    private void loadEmp()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();
        drpEmployee.Items.Clear();
        drpEmployee.Items.Add(new ListItem("Select Manager", "0"));
        ds = bl.ListExecutive();
        drpEmployee.DataSource = ds;
        drpEmployee.DataBind();
        drpEmployee.DataTextField = "empFirstName";
        drpEmployee.DataValueField = "empno";


        drpTaskStatus.Items.Clear();
        drpTaskStatus.Items.Add(new ListItem("---ALL---", "0"));
        DataSet dsd = bl.ListTaskStatusInfo(connection, "", "");
        drpTaskStatus.DataSource = dsd;
        drpTaskStatus.DataBind();
        drpTaskStatus.DataTextField = "Task_Status_Name";
        drpTaskStatus.DataValueField = "Task_Status_Id";

    }

    private void loadmanager()
    {
        BusinessLogic bl = new BusinessLogic(sDataSource);
        DataSet ds = new DataSet();
        string connection = ConfigurationManager.ConnectionStrings[Request.Cookies["Company"].Value].ToString();


        //if (Convert.ToInt32(drpIncharge.SelectedValue) == 0)
        //{
        //    string Username = Request.Cookies["LoggedUserName"].Value;

        //    ds = bl.ListManager(Username);
        //    drpIncharge.DataSource = ds;
        //    drpIncharge.DataBind();
        //    drpIncharge.DataTextField = "empFirstName";
        //    drpIncharge.DataValueField = "empno";
         
        //}
        //else
        //{

            string Username = Request.Cookies["LoggedUserName"].Value;

            ds = bl.ListManager(Username);

            //ds = bl.ListExecutive();
            drpIncharge.DataSource = ds;
            drpIncharge.DataBind();
            drpIncharge.DataTextField = "empFirstName";
            drpIncharge.DataValueField = "empno";



            drpBranch.Items.Clear();
            drpBranch.Items.Add(new ListItem("Select Branch", "0"));
            ds = bl.ListBranch();
            drpBranch.DataSource = ds;
            drpBranch.DataBind();
            drpBranch.DataTextField = "BranchName";
            drpBranch.DataValueField = "Branchcode";
            UpdatePanel444.Update();


            if (Username == null)
            {

            }
            else
            {
                drpEmployee.Items.Clear();
                drpEmployee.Items.Add(new ListItem("---ALL---", "0"));
                // ds = bl.ListExecutive();
                string Usernam = Request.Cookies["LoggedUserName"].Value;
                ds = bl.ListOwner(connection, Usernam);
                drpEmployee.DataSource = ds;
                drpEmployee.DataBind();
                drpEmployee.DataTextField = "empFirstName";
                drpEmployee.DataValueField = "empno";
            }
       // }

    }

    private void BranchEnable_Disable()
    {
        string sCustomer = string.Empty;
        string connection = Request.Cookies["Company"].Value;
        string usernam = Request.Cookies["LoggedUserName"].Value;
        BusinessLogic bl = new BusinessLogic();
        DataSet dsd = bl.GetBranch(connection, usernam);

        sCustomer = Convert.ToString(dsd.Tables[0].Rows[0]["DefaultBranchCode"]);
        drpBranch.ClearSelection();
        ListItem li = drpBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
        if (li != null) li.Selected = true;

        if (dsd.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
        {
            drpBranch.Enabled = true;
        }
        else
        {
            drpBranch.Enabled = false;
        }
    }

    protected void drpmanager_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int Emp_Id = 0;
            string connection = Request.Cookies["Company"].Value;

            string branch = Request.Cookies["Branch"].Value;

            Emp_Id = Convert.ToInt32(drpIncharge.SelectedValue);

            drpproject.Items.Clear();

            DataSet ds = bl.getfilterprojectfromemployee(connection, Emp_Id,branch);
            drpproject.Items.Add(new ListItem("---ALL---", "0"));
            drpproject.DataSource = ds;
            drpproject.DataBind();
            drpproject.DataTextField = "Project_Name";
            drpproject.DataValueField = "Project_Id";
            //  drpproject.Items.Add(new ListItem("---ALL---", "0"));

            UpdatePanel5.Update();

          //  connection = Request.Cookies["Company"].Value;
          // string usernam = Request.Cookies["LoggedUserName"].Value;
          // // BusinessLogic bl = new BusinessLogic();
          //  DataSet ds1 = bl.GetBranch(connection, usernam);

          //string sCustomer = Convert.ToString(ds1.Tables[0].Rows[0]["DefaultBranchCode"]);
          //  drpBranch.ClearSelection();
          //  ListItem li = drpBranch.Items.FindByValue(System.Web.HttpUtility.HtmlDecode(sCustomer));
          //  if (li != null) li.Selected = true;

          //  if (ds1.Tables[0].Rows[0]["BranchCheck"].ToString() == "True")
          //  {
          //      drpBranch.Enabled = true;
          //  }
          //  else
          //  {
               
          //  }

          //  UpdatePanel444.Update();


            //drpBranch.Items.Clear();
            //drpBranch.Items.Add(new ListItem("Select Branch", "0"));
            ////ds = bl.ListBranch();
            ////drpBranch.DataSource = ds;
            //drpBranch.DataBind();
          

            drpproject_SelectedIndexChanged(sender, e);

        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
        // UpdatePanel2.Update();
    }
    protected void drpproject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int Project_Id = 0;

            string connection = Request.Cookies["Company"].Value;
            Project_Id = Convert.ToInt32(drpproject.SelectedValue);

            drptask.Items.Clear();
            drptask.Items.Add(new ListItem("---ALL---", "0"));
            DataSet ds = bl.GetDependencytask(connection, Project_Id);

            drptask.DataSource = ds;
            drptask.DataBind();
            drptask.DataTextField = "Task_Name";
            drptask.DataValueField = "Task_Id";
            UpdatePanel2.Update();

            //drpdependencytask.Items.Clear();
            //drpdependencytask.Items.Add(new ListItem("---ALL---", "0"));
            //drpdependencytask.DataSource = ds;
            //drpdependencytask.DataBind();
            //drpdependencytask.DataTextField = "Task_Name";
            //drpdependencytask.DataValueField = "Task_Id";
            //UpdatePanel3.Update();



        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void radblocktask_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (radblocktask.SelectedItem.Text == "YES")
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                int Project_Id = 0;

                string connection = Request.Cookies["Company"].Value;
                Project_Id = Convert.ToInt32(drpproject.SelectedValue);

                drptask.Items.Clear();
                drptask.Items.Add(new ListItem("---ALL---", "0"));
                DataSet ds = bl.GetblocktaskYES(connection, Project_Id);

                drptask.DataSource = ds;
                drptask.DataBind();
                drptask.DataTextField = "Task_Name";
                drptask.DataValueField = "Task_Id";
                UpdatePanel2.Update();
                if (drpTaskStatus.SelectedValue != Convert.ToString(0))
                {
                    drpTaskStatus_SelectedIndexChanged(sender, e);
                }

            }
            else if (radblocktask.SelectedItem.Text == "NO")
            {
                BusinessLogic bl = new BusinessLogic(sDataSource);
                int Project_Id = 0;

                string connection = Request.Cookies["Company"].Value;
                Project_Id = Convert.ToInt32(drpproject.SelectedValue);

                drptask.Items.Clear();
                drptask.Items.Add(new ListItem("---ALL---", "0"));
                DataSet ds = bl.GetblocktaskNO(connection, Project_Id);

                drptask.DataSource = ds;
                drptask.DataBind();
                drptask.DataTextField = "Task_Name";
                drptask.DataValueField = "Task_Id";
                UpdatePanel2.Update();
                if (drpTaskStatus.SelectedValue != Convert.ToString(0))
                {
                    drpTaskStatus_SelectedIndexChanged(sender, e);
                }
            }

        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }
    protected void drpTaskStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int stat_Id = 0;
            int pro_Id = 0;

            string connection = Request.Cookies["Company"].Value;

            stat_Id = Convert.ToInt32(drpTaskStatus.SelectedValue);
            pro_Id = Convert.ToInt32(drpproject.SelectedValue);
            if (radblocktask.SelectedItem.Text == "YES" && drpproject.SelectedValue != null && radisactive.SelectedItem.Text == "YES")
            {



                drptask.Items.Clear();
                
                drptask.Items.Add(new ListItem("---ALL---", "0"));
                DataSet ds = bl.Getstatustasksyes(connection, stat_Id, pro_Id);
                drptask.DataSource = ds;
                drptask.DataBind();
                drptask.DataTextField = "Task_Name";
                drptask.DataValueField = "Task_Id";
                UpdatePanel2.Update();
                // drpemployee_SelectedIndexChanged(sender, e);
            }
            else if (radblocktask.SelectedItem.Text == "NO" && drpproject.SelectedValue != null && radisactive.SelectedItem.Text == "NO")
            {
                drptask.Items.Clear();

                drptask.Items.Add(new ListItem("---ALL---", "0"));
                DataSet ds = bl.Getstatustasksno(connection, stat_Id, pro_Id);
                drptask.DataSource = ds;
                drptask.DataBind();
                drptask.DataTextField = "Task_Name";
                drptask.DataValueField = "Task_Id";
                UpdatePanel2.Update();
                //drpemployee_SelectedIndexChanged(sender, e);

            }
            else
            {
                drptask.Items.Clear();

                drptask.Items.Add(new ListItem("---ALL---", "0"));
                DataSet ds = bl.Getstatustasksnostatus(connection, stat_Id, pro_Id);
                drptask.DataSource = ds;
                drptask.DataBind();
                drptask.DataTextField = "Task_Name";
                drptask.DataValueField = "Task_Id";
                UpdatePanel2.Update();
                // drpemployee_SelectedIndexChanged(sender, e);

            }



            // drpproject_SelectedIndexChanged(sender, e);

        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
        // UpdatePanel2.Update();
    }

    protected void drpemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int Emp_Id = 0;
            string connection = Request.Cookies["Company"].Value;

            Emp_Id = Convert.ToInt32(drpEmployee.SelectedValue);
            drptask.Items.Clear();

            drptask.Items.Add(new ListItem("---ALL---", "0"));
            DataSet ds = bl.Gettaskfromemployee(connection, Emp_Id);
            drptask.DataSource = ds;
            drptask.DataBind();
            drptask.DataTextField = "Task_Name";
            drptask.DataValueField = "Task_Id";
            UpdatePanel2.Update();
            //  drpproject.Items.Add(new ListItem("---ALL---", "0"));

        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
        // UpdatePanel2.Update();
    }

    protected void radisactive_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BusinessLogic bl = new BusinessLogic(sDataSource);
            int Project_Id = 0;

            if (radisactive.SelectedItem.Text == "Y")
            {

                string connection = Request.Cookies["Company"].Value;
                Project_Id = Convert.ToInt32(drpproject.SelectedValue);

                drptask.Items.Clear();
                drptask.Items.Add(new ListItem("---ALL---", "0"));
                DataSet ds = bl.Getactivetask(connection, Project_Id);

                drptask.DataSource = ds;
                drptask.DataBind();
                drptask.DataTextField = "Task_Name";
                drptask.DataValueField = "Task_Id";
                UpdatePanel2.Update();
            }
            else
            {


                string connection = Request.Cookies["Company"].Value;
                Project_Id = Convert.ToInt32(drpproject.SelectedValue);

                drptask.Items.Clear();
                drptask.Items.Add(new ListItem("---ALL---", "0"));
                DataSet ds = bl.Getinactivetask(connection, Project_Id);

                drptask.DataSource = ds;
                drptask.DataBind();
                drptask.DataTextField = "Task_Name";
                drptask.DataValueField = "Task_Id";
                UpdatePanel2.Update();
            }





        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }

    }

    protected void btnprojectreport_Click(object sender, EventArgs e)
    {
        try
        {
            int Empno = 0;
            int ProjectId = 0;
            string BlockedTask = "";
            int CompletedTask = 0;
            int Task = 0;
            int DependencyTask = 0;
            string isactive = "";
            int incharge = 0;
            Empno = Convert.ToInt32(drpEmployee.SelectedValue);
            incharge = Convert.ToInt32(drpIncharge.SelectedValue);

            ProjectId = Convert.ToInt32(drpproject.SelectedValue);
            BlockedTask = radblocktask.SelectedValue;
            CompletedTask = Convert.ToInt32(drpTaskStatus.SelectedValue);
            Task = Convert.ToInt32(drptask.SelectedValue);
            //  DependencyTask = Convert.ToInt32(drpdependencytask.SelectedValue);
            isactive = radisactive.SelectedValue;
            //string cond = getCond();

            //Response.Write("<script language='javascript'> window.open('ProjectReport1.aspx?incharge =" + Convert.ToString(incharge) + "&employee=" + Convert.ToString(Empno) + "&project=" + Convert.ToString(ProjectId) + "&BlockedTask=" + BlockedTask + "&CompletedTask=" + Convert.ToString(CompletedTask) + "&Task=" + Convert.ToString(Task) + "&DependencyTask=" + Convert.ToString(DependencyTask) + "&isactive=" + isactive + "&cond=" + Convert.ToString(cond) +" ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            Response.Write("<script language='javascript'> window.open('ProjectReport1.aspx?incharge=" + incharge + "&employee=" + Empno + "&project=" + ProjectId + "&BlockedTask=" + BlockedTask + "&CompletedTask=" + CompletedTask + "&Task=" + Task + "&DependencyTask=" + DependencyTask + "&isactive=" + isactive + " ' , 'window','height=700,width=1000,left=172,top=10,toolbar=yes,scrollbars=yes,resizable=yes');</script>");
            //Response.Redirect("ProjectReport1.aspx");
        }
        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }

    //protected string getCond()
    //{
    //    string cond = "";

    //    //cond = "1=1";


    //    if ((drpproject.SelectedItem.Text != "---ALL---"))
    //    {


    //        cond += "  tblProjects.ProjectId=" + drpproject.SelectedValue + "";
    //    }

    //    if ((drpEmployee.SelectedItem.Text != "---ALL---"))
    //    {

    //        cond += " and tblEmployee.empno=" + drpEmployee.SelectedValue + "";
    //    }



    //        cond += " and  tblTaskUpdates.Blocked_Flag ='" + radblocktask.SelectedValue + "'";


    //    if ((drpTaskStatus.SelectedItem.Text != "---ALL---"))
    //    {

    //        cond += " and tblTaskUpdates.Task_Status =" + Convert.ToInt32(drpTaskStatus.SelectedValue) + " ";

    //    }
    //    if ((drptask.SelectedItem.Text != "---ALL---"))
    //    {

    //        cond += " and tblTasks.Task_Id =" + Convert.ToInt32(drptask.SelectedValue) + " ";
    //    }

    //    if ((drpdependencytask.SelectedItem.Text != "---ALL---"))
    //    {

    //        cond += " and tblTasks.Dependency_Task =" + Convert.ToInt32(drpdependencytask.SelectedValue) + " ";
    //    }


    //        cond += " and tblTasks.IsActive ='" + radisactive.SelectedValue + "' ";


    //    cond += " and tblEmployee.ManagerID='" + drpIncharge.SelectedValue + "'";


    //    return cond;
    //}
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


            System.Text.StringBuilder htmlcode = new System.Text.StringBuilder();
            htmlcode.Append("<html><body>");
            //htmlcode.Append("<form id=form1 runat=server>");
            htmlcode.Append("<div id=divPrint style=font-family:'Trebuchet MS'; font-size:10px;  >");

            htmlcode.Append("<Table id = table1 border=1px solid blue cellpadding=0 cellspacing=0 class=tblLeft width=100% >");
            DataSet dsVat = new DataSet();
            DataSet dsCst = new DataSet();
            DataSet ds1 = new DataSet();
            BusinessLogic bl = new BusinessLogic(sDataSource);

            htmlcode.Append("<tr class=ReportHeadataRow style=text-align:left>");
            htmlcode.Append("<td> Project Name");
            htmlcode.Append("</td>");
            htmlcode.Append("<td> Project Date");
            htmlcode.Append("</td>");
            htmlcode.Append("<td> Expected Start Date");
            htmlcode.Append("</td>");
            htmlcode.Append("<td> Expected End Date");
            htmlcode.Append("</td>");
            htmlcode.Append("<td> Expected start date");
            htmlcode.Append("</td>");

            int i = 0;
            int j = 0;

            DataSet dspro = new DataSet();
            dspro = bl.GetProjectManagementReport(connection, incharge, empno, pro_Id, flag, status, taskid, deptask, isactive, cond);

            for (i = 0; i < dspro.Tables[0].Rows.Count; i++)
            {
                if (dspro.Tables[0].Rows[i].ItemArray[0].ToString() == "0")
                {
                    htmlcode.Append("<td> NO Data Found");
                    htmlcode.Append("</td>");

                }
                else
                {
                    htmlcode.Append("<tr class=ReportdataRow>");
                    htmlcode.Append("<td>" + dspro.Tables[0].Rows[j].ItemArray[0].ToString());
                    htmlcode.Append("</td>");
                    htmlcode.Append("<td>" + dspro.Tables[0].Rows[j].ItemArray[1].ToString());
                    htmlcode.Append("</td>");
                    htmlcode.Append("<td>" + dspro.Tables[0].Rows[j].ItemArray[2].ToString());
                    htmlcode.Append("</td>");
                    htmlcode.Append("<td>" + dspro.Tables[0].Rows[j].ItemArray[3].ToString());
                    htmlcode.Append("</td>");
                    htmlcode.Append("<td>" + dspro.Tables[0].Rows[j].ItemArray[4].ToString());
                    htmlcode.Append("</td>");
                    //htmlcode.Append("<td> " + dsVat.Tables[0].Rows[i].ItemArray[0].ToString() + "  Project Name");
                    //htmlcode.Append("</td>");
                    //htmlcode.Append("<td> " + dsVat.Tables[0].Rows[i].ItemArray[0].ToString() + "  Project Date");
                    //htmlcode.Append("</td>");
                    //htmlcode.Append("<td> " + dsVat.Tables[0].Rows[i].ItemArray[0].ToString() + "  Expected Start Date");
                    //htmlcode.Append("</td>");
                    //htmlcode.Append("<td> " + dsVat.Tables[0].Rows[i].ItemArray[0].ToString() + "  Expected End Date");
                    //htmlcode.Append("</td>");
                    //htmlcode.Append("<td> " + dsVat.Tables[0].Rows[i].ItemArray[0].ToString() + "  Expected start date");
                    //htmlcode.Append("</td>");


                    //htmlcode.Append("<td>" + dsVat.Tables[0].Rows[i].ItemArray[0].ToString() + " Purchase Value");
                    //htmlcode.Append("</td>");
                    //htmlcode.Append("<td>" + dsVat.Tables[0].Rows[i].ItemArray[0].ToString() + " Input VAT");
                    //htmlcode.Append("</td>");
                }
            }
            //dsCst = bl.ListPurchaseCst();
            //for (i = 1; i < dsCst.Tables[0].Rows.Count; i++)
            //{
            //    htmlcode.Append("<td>" + dsCst.Tables[0].Rows[i].ItemArray[0].ToString() + " Purchase Value");
            //    htmlcode.Append("</td>");
            //    htmlcode.Append("<td>" + dsCst.Tables[0].Rows[i].ItemArray[0].ToString() + " Input CST");
            //    htmlcode.Append("</td>");
            //}
            //htmlcode.Append("<td> Total Sales");
            //htmlcode.Append("</td>");
            //htmlcode.Append("</tr>");



            //Double[] Total;
            //Total = new double[50];
            //int TotCount = 0;

            //ds1 = bl.ListPurchaseVatCstAmtDet(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToInt16(drpLedgerName.SelectedValue), Convert.ToInt16(drpVat.SelectedValue), Convert.ToInt16(drpCst.SelectedValue));
            //if (ds1 != null)
            //{
            //    for (j = 0; j < ds1.Tables[0].Rows.Count; j++)
            //    {
            //        TotCount = 0;
            //        htmlcode.Append("<tr class=ReportdataRow>");
            //        htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[0].ToString());
            //        htmlcode.Append("</td>");
            //        htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[1].ToString());
            //        htmlcode.Append("</td>");
            //        htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[2].ToString());
            //        htmlcode.Append("</td>");
            //        TotSal = Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
            //        Total[TotCount] = Total[TotCount] + TotSal;
            //        for (i = 0; i < dsVat.Tables[0].Rows.Count; i++)
            //        {

            //            TotCount = TotCount + 1;
            //            if (ds1.Tables[0].Rows[j].ItemArray[6].ToString() == dsVat.Tables[0].Rows[i].ItemArray[0].ToString())
            //            {
            //                if (ds1.Tables[0].Rows[j].ItemArray[6].ToString() == "0")
            //                {
            //                    if (ds1.Tables[0].Rows[j].ItemArray[7].ToString() == "0")
            //                    {
            //                        htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[3].ToString());
            //                        htmlcode.Append("</td>");
            //                        Total[TotCount] = Total[TotCount] + Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
            //                        TotSal = TotSal + ((TotSal * Convert.ToDouble(Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString()))) / 100);
            //                        //htmlcode.Append("<td>" + ((Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString())) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString())) / 100);
            //                        //htmlcode.Append("</td>");

            //                    }
            //                    else
            //                    {

            //                        htmlcode.Append("<td>");
            //                        htmlcode.Append("</td>");
            //                        Total[TotCount] = Total[TotCount] + 0;
            //                    }
            //                }
            //                else
            //                {
            //                    htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[3].ToString());
            //                    htmlcode.Append("</td>");

            //                    TotSal = TotSal + ((TotSal * Convert.ToDouble(Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString()))) / 100);
            //                    htmlcode.Append("<td>" + ((Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString())) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString())) / 100);
            //                    htmlcode.Append("</td>");
            //                    Total[TotCount] = Total[TotCount] + Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
            //                    TotCount = TotCount + 1;
            //                    Total[TotCount] = Total[TotCount] + (Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString()) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[6].ToString()) / 100);
            //                }
            //            }
            //            else
            //            {
            //                if (dsVat.Tables[0].Rows[i].ItemArray[0].ToString() == "0")
            //                {
            //                    htmlcode.Append("<td>");
            //                    htmlcode.Append("</td>");
            //                    Total[TotCount] = Total[TotCount] + 0;
            //                }
            //                else
            //                {
            //                    htmlcode.Append("<td>");
            //                    htmlcode.Append("</td>");
            //                    htmlcode.Append("<td>");
            //                    htmlcode.Append("</td>");
            //                    Total[TotCount] = Total[TotCount] + 0;
            //                    TotCount = TotCount + 1;
            //                    Total[TotCount] = Total[TotCount] + 0;

            //                }
            //            }
            //        }
            //        for (i = 1; i < dsCst.Tables[0].Rows.Count; i++)
            //        {

            //            TotCount = TotCount + 1;
            //            if (ds1.Tables[0].Rows[j].ItemArray[7].ToString() == dsCst.Tables[0].Rows[i].ItemArray[0].ToString())
            //            {
            //                htmlcode.Append("<td>" + ds1.Tables[0].Rows[j].ItemArray[3].ToString());
            //                htmlcode.Append("</td>");
            //                TotSal = TotSal + ((TotSal * Convert.ToDouble(Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[7].ToString()))) / 100);
            //                htmlcode.Append("<td>" + ((Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString())) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[7].ToString())) / 100);
            //                htmlcode.Append("</td>");
            //                Total[TotCount] = Total[TotCount] + Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString());
            //                TotCount = TotCount + 1;
            //                Total[TotCount] = Total[TotCount] + (Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[3].ToString()) * Convert.ToDouble(ds1.Tables[0].Rows[j].ItemArray[7].ToString()) / 100);
            //            }
            //            //else
            //            //{
            //            //    htmlcode.Append("<td>");
            //            //    htmlcode.Append("</td>");
            //            //    htmlcode.Append("<td>");
            //            //    htmlcode.Append("</td>");
            //            //    Total[TotCount] = Total[TotCount] + 0;
            //            //    TotCount = TotCount + 1;
            //            //    Total[TotCount] = Total[TotCount] + 0;
            //            //}
            //        }
            //        //TotCount = TotCount + 1;
            //        //Total[TotCount] = Total[TotCount] + TotSal;
            //        //htmlcode.Append("<td>" + TotSal);
            //        //htmlcode.Append("</td>");
            //        //htmlcode.Append("</tr>");

            //    }
            //htmlcode.Append("<tr class=ReportFooterRow>");
            //htmlcode.Append("<td>");
            //htmlcode.Append("</td>");
            //htmlcode.Append("<td>");
            //htmlcode.Append("</td>");
            //htmlcode.Append("<td>");
            //htmlcode.Append("</td>");
            //TotCount = 1;
            //for (i = 0; i < dsVat.Tables[0].Rows.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        htmlcode.Append("<td>" + Total[TotCount]);
            //        htmlcode.Append("</td>");
            //        TotCount = TotCount + 1;
            //    }
            //    else
            //    {
            //        htmlcode.Append("<td>" + Total[TotCount]);
            //        htmlcode.Append("</td>");
            //        TotCount = TotCount + 1;
            //        htmlcode.Append("<td>" + Total[TotCount]);
            //        htmlcode.Append("</td>");
            //        TotCount = TotCount + 1;
            //    }
            //}
            //for (i = 1; i < dsCst.Tables[0].Rows.Count; i++)
            //{
            //    htmlcode.Append("<td>" + Total[TotCount]);
            //    htmlcode.Append("</td>");
            //    TotCount = TotCount + 1;
            //    htmlcode.Append("<td>" + Total[TotCount]);
            //    htmlcode.Append("</td>");
            //    TotCount = TotCount + 1;
            //}
            //htmlcode.Append("<td>" + Total[TotCount]);
            //htmlcode.Append("</td>");
            //htmlcode.Append("</tr>");
            //htmlcode.Append("</div>");
            //htmlcode.Append("</Table>");

            ////htmlcode.Append("</form>");
            //htmlcode.Append("</body></html>");

            //string s = htmlcode.ToString();
            //divReport.InnerHtml = htmlcode.ToString();

            //ExportToExcel();
        }

        catch (Exception ex)
        {
            TroyLiteExceptionManager.HandleException(ex);
        }
    }


}