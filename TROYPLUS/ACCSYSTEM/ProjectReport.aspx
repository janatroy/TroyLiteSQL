<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProjectReport.aspx.cs" Inherits="ProjectReport"  Title="Project Report" %>

<%@ Register Assembly="Shared.WebControls" Namespace="Shared.WebControls" TagPrefix="wc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center">
    <br />
    <div id="div1" runat="server">
        <table style="border: 1px solid blue; background-color:White;" width="100%">
            <%--<tr>
                <td colspan="5">
                    
                        <table cellspacing="0" cellpadding="0" border="0"  class="headerPopUp">
                            <tr valign="middle">
                                <td>
                                    <span>Stock Ageing Report</span>
                                </td>
                            </tr>
                        </table>
                    
                </td>--%>
                <%--<td colspan="4" class="mainConHd" style="text-align:center; vertical-align:middle">
                    <span>Stock Ageing Report</span>
                </td>--%>
            <%--</tr>--%>
            <tr>
                <td colspan="5" class="headerPopUp">
                      Print Project Report
                </td>
            </tr>
            <tr style="height:6px">
                    
            </tr>
            <tr>
                <td colspan="5">
                    <table style="width:100%">
                        <tr>
                             <td  style="width:25%; font-family:'ARIAL';font-size:14px;font-weight:bold; color: #0567AE;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                               Select the Manager
                            </td>
                               <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                 <asp:UpdatePanel ID="UpdatePanel6" runat="server"  UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                <asp:DropDownList ID="drpIncharge" OnSelectedIndexChanged="drpmanager_SelectedIndexChanged" AutoPostBack="true" TabIndex="4" EnableTheming="False" AppendDataBoundItems="True" CssClass="drpDownListMedium"
                                                                                                runat="server" Width="100%" DataTextField="empFirstName" BackColor="#E7E7E7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                DataValueField="empno">
                                                                                                <asp:ListItem Text="Select Project Manager" Value="0"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                                           </ContentTemplate>
                                                                                            </asp:UpdatePanel>

                            </td>
                              <td  style="width:25%; font-family:'ARIAL';font-size:14px;font-weight:bold; color: #0567AE;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Task Status
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                <asp:DropDownList ID="drpTaskStatus" OnSelectedIndexChanged="drpTaskStatus_SelectedIndexChanged" AutoPostBack="true" TabIndex="5" Enabled="True" EnableTheming="false" AppendDataBoundItems="true" CssClass="drpDownListMedium"
                                                                                            runat="server" Width="100%" DataTextField="Task_Status_Name" backcolor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px"
                                                                                            DataValueField="Task_Status_Id" >
                                                                                           <%-- <asp:ListItem Text="Select Task Status" Value="0"></asp:ListItem>--%>
                                                                                        </asp:DropDownList>
                            </td>
                           
                           
                          
                            <td style="width:10%">
                            </td>
                        </tr>
                        <tr style="height: 2px;"/> 
                        <tr>
                              <td  style="width:25%; font-family:'ARIAL';font-size:14px;font-weight:bold; color: #0567AE;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Select the Project
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                  <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                <asp:DropDownList ID="drpproject" OnSelectedIndexChanged="drpproject_SelectedIndexChanged" AutoPostBack="true" runat="server" DataTextField="Project_Name" DataValueField="Project_Id" AppendDataBoundItems="True"  BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" Width="100%"
                                    CssClass="textbox">
                                    <%--<asp:ListItem Selected="True" Value="0" style="background-color: #bce1fe">Select Project</asp:ListItem>--%>
                                </asp:DropDownList>
                                                                                                       </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                            </td>
                            <td  style="width:25%; font-family:'ARIAL';font-size:14px;font-weight:bold; color: #0567AE;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Is Active
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                  <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                               <asp:RadioButtonList id="radisactive"  OnSelectedIndexChanged="radisactive_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                   <asp:listitem   Text="NO"  value="N"  ></asp:ListItem>
                                   <asp:listitem   Text="YES" value="Y"></asp:ListItem>
                                    <asp:listitem   Text="ALL" value="NA" selected="true"></asp:ListItem>
                                       </asp:RadioButtonList>
                                                                                                        </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                            </td>
                           
                                 <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                <asp:DropDownList ID="drpdependencytask" runat="server" DataTextField="Task_Name" DataValueField="Task_Id"  CssClass="drpDownListMedium" BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" Width="100%" AppendDataBoundItems="True">
                                </asp:DropDownList>
                                                                                                           </ContentTemplate>
                                                                                            </asp:UpdatePanel>--%>

                            


                           
                          
                            <td style="width:10%">
                            </td>
                        </tr>
                      
                        <tr style="height: 2px;"/> 
                        <tr>
                             <td  style="width:25%; font-family:'ARIAL';font-size:14px;font-weight:bold; color: #0567AE;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                               Select the Employee
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                               <asp:DropDownList ID="drpEmployee" OnSelectedIndexChanged="drpemployee_SelectedIndexChanged" AutoPostBack="true"  TabIndex="3" Enabled="True" EnableTheming="false" AppendDataBoundItems="true" CssClass="drpDownListMedium"
                                                                                                runat="server" Width="100%" DataTextField="empFirstName" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                DataValueField="empno">
                                                                                                <%--<asp:ListItem Text="Select Employee" Value="0"></asp:ListItem>--%>
                                                                                            </asp:DropDownList>
                                                                                                           </ContentTemplate>
                                                                                            </asp:UpdatePanel>

                            </td>
                               <td  style="width:25%; font-family:'ARIAL';font-size:14px;font-weight:bold; color: #0567AE;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                                Blocked Task
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                  <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                               <asp:RadioButtonList id="radblocktask" OnSelectedIndexChanged="radblocktask_SelectedIndexChanged" AutoPostBack="true"  runat="server">
                                   <asp:listitem   Text="NO"  value="N" selected="true" ></asp:ListItem>
                                   <asp:listitem   Text="YES" value="Y" ></asp:ListItem>
                                    <asp:listitem   Text="ALL" value="NA" selected="true"></asp:ListItem>
                                       </asp:RadioButtonList>
                                                                                                        </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                            </td>
                               

                            
                          
                        
                            <td style="width:10%">
                            </td>
                        </tr>
                        <tr style="height: 2px;"/> 
                        <tr>
                              <td  style="width:25%; font-family:'ARIAL';font-size:14px;font-weight:bold; color: #0567AE;text-align:right;text-decoration:none;padding-right:5px;padding-left:5px;padding-top:5px;" height="27px">
                            
                                Select task
                            </td>
                            <td style="text-align: left; width:20%" class="ControlDrpBorder">
                                 <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                <asp:DropDownList ID="drptask" runat="server" DataTextField="Task_Name" DataValueField="Task_Id"  CssClass="drpDownListMedium" BackColor = "#e7e7e7" style="border: 1px solid #e7e7e7" height="26px" Width="100%" AppendDataBoundItems="True">
                                </asp:DropDownList>
                                                                                                          </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                            </td>
                          
                       <td style="width:25%"> </td>
                           <td style="width:20%">
                               </td>
                         
                          
                             <td style="width:10%">
                            </td>
                            </tr>
                      
                        <tr>
                            <td colspan="5">
                                <asp:Label ID="lblDate" runat="server" CssClass="label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                
            </tr>
            
            <tr>
                <td colspan="5">
                    <table style="width:100%" >
                        <tr>
                            <td style="width:30%;" >
                            </td>
                            <td  style="width:10%;">
                               
                            </td>
                            <td style="width:20%;">
                                <asp:Button ID="btnprojectreport" OnClick="btnprojectreport_Click" runat="server" CssClass="NewReport6"
                                    ValidationGroup="btnAgeing" EnableTheming="false"/>
                            </td>
                            <td  style="width:10%;">
                               
                            </td>
                            <td style="width:30%;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style1" colspan="5">
                </td>
            </tr>
            </table>
            </div>
            <div runat="server" id="divmain" visible="false">
                <table width="600px">
                    <tr>
                        <td colspan="5">
                            <table width="100%">
                                <tr>
                                    <td style="width:31%">

                                    </td>
                                    <td style="width:19%">
                                        <input type="button" value="" id="Button1" runat="Server" onclick="javascript: CallPrint('divPrint')"
                                            class="printbutton6" />
                                    </td>
                                    <td style="width:19%">
                                        <asp:Button ID="btndet" CssClass="GoBack" EnableTheming="false" runat="server"
                                             />
                                    </td>
                                    <td style="width:31%">

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <div id="divPrint" style="font-family: 'Trebuchet MS'; font-size: 11px; width: 100%"
                    runat="server">
                    <table width="600px" border="0" style="font-family: Trebuchet MS; font-size: 14px;">
                         <tr>
                    <td width="140px" align="left">
                        TIN#:
                        <asp:Label ID="lblTNGST" runat="server"></asp:Label>
                    </td>
                    <td align="center" width="320px" style="font-size: 20px;">
                        <asp:Label ID="lblCompany" runat="server"></asp:Label>
                    </td>
                    <td width="140px" align="left">
                        Ph:
                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                    </td>
                </tr>
                         <tr>
                    <td align="left">
                        GST#:
                        <asp:Label ID="lblGSTno" runat="server"></asp:Label>
                    </td>
                    <td align="center">
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                    </td>
                    <td align="left">
                        Date:
                        <asp:Label ID="lblBillDate" runat="server"></asp:Label>
                    </td>
                </tr>
                          <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Label ID="lblCity" runat="server" />
                        -
                        <asp:Label ID="lblPincode" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                        <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center">
                        <asp:Label ID="lblState" runat="server"> </asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                       <tr>
                    <td colspan="5">
                        <br />
                        <h5>
                            Project Of
                            <asp:Label ID="lblLedger" runat="server"></asp:Label>
                            <br />
                            Date From
                            <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                            To
                            <asp:Label ID="lblEndDate" runat="server"> </asp:Label></h5>
                    </td>
                </tr>
                    </table>
                    <br />
                    <br />
                </div>
                &nbsp;
            
       <%-- <asp:GridView ID="proOuts" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge"
            BorderWidth="2px" CellPadding="3" CellSpacing="1" Font-Size="Small" ShowFooter="True"
             AutoGenerateColumns="True">
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
         <%--   <PageHeaderTemplate>
                <br />
                <br />
            </PageHeaderTemplate>--%>
            <%-- <Columns>
                        <asp:BoundField ItemStyle-Width="15%" DataField="empfirstname" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField ItemStyle-Width="12%" DataField="Project_Name" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Project Name" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField ItemStyle-Width="12%" DataField="Task_Name" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Task Name" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Balance" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblBalance" runat="server" CssClass="lblFont" Font-Bold="true" ForeColor="Blue"
                                    Text="0.00"> </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ItemStyle-Width="10%" DataField="LedgerID" Visible="false" />
                    </Columns>
            <PagerTemplate>
            </PagerTemplate>--%>
           <%-- <PageFooterTemplate>
                <br />
            </PageFooterTemplate>--%>
      <%--  </asp:GridView>--%>--%>
       <%--<wc:ReportGridView runat="server" BorderWidth="1" ID="gvOuts" GridLines="Both"
                                AlternatingRowStyle-CssClass="even" AutoGenerateColumns="true"
                                AllowPrintPaging="true" EmptyDataText="No Data Found" SkinID="gridview" CssClass="someClass"
                                Width="100%" >
                                <RowStyle CssClass="dataRow" />
                                <SelectedRowStyle CssClass="SelectdataRow" />
                                <AlternatingRowStyle CssClass="altRow" />
                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                <HeaderStyle CssClass="HeadataRow" Wrap="false" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px" />
                                <FooterStyle CssClass="dataRow" />
                                <PagerStyle CssClass="footer-row allPad" VerticalAlign="Middle" HorizontalAlign="Left" />
                                <PageHeaderTemplate>
                                    <br />
                                    <br />
                                </PageHeaderTemplate>
                                <Columns>--%>
                        <%--<asp:BoundField ItemStyle-Width="15%" DataField="empfirstname" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Center" />--%>
                        <%--<asp:BoundField ItemStyle-Width="12%" DataField="Project_Name" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Project Name" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />--%>
                      <%--  <asp:BoundField ItemStyle-Width="12%" DataField="Task_Name" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Task Name" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Right" />--%>
                     
                       
                    <%--</Columns>
                                <PagerTemplate>
                                </PagerTemplate>
                                <PageFooterTemplate>
                                    <br />
                                </PageFooterTemplate>
                            </wc:ReportGridView>
                            <br />--%>
    </div>
    </div>

</asp:Content>