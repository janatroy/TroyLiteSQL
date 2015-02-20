<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Querymaster.aspx.cs" Inherits="Querymaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stock Report</title>
        <%--<script language="javascript" type="text/javascript">

            function pageLoad() {
                //  get the behavior associated with the tab control
                var tabContainer = $find('ctl00_cplhControlPanel_tabContol');

//                if (tabContainer == null)
//                    tabContainer = $find('ctl00_cplhControlPanel_tabPanelMaster');

                if (tabContainer != null) {
                    //  get all of the tabs from the container
                    var tabs = tabContainer.get_tabs();

                    //  loop through each of the tabs and attach a handler to
                    //  the tab header's mouseover event
                    for (var i = 0; i < tabs.length; i++) {
                        var tab = tabs[i];

                        $addHandler(
                tab.get_headerTab(),
                'mouseover',
                Function.createDelegate(tab, function () {
                    tabContainer.set_activeTab(this);
                }
            ));
                    }
                }
            }

    </script>--%>

    <style id="Style1" runat="server">
        
        
        .fancy-green .ajax__tab_header
        {
	        background: url(App_Themes/NewTheme/Images/green_bg_Tab.gif) repeat-x;
	        cursor:pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
	        background: url(App_Themes/NewTheme/Images/green_left_Tab.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
	        background: url(App_Themes/NewTheme/Images/green_right_Tab.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
	        font-size: 13px;
	        font-weight: bold;
	        color: #000;
	        font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
	        height: 46px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
	        height: 46px;
	        margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
	        margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
	        color: #fff;
        }
        .fancy .ajax__tab_body
        {
	        font-family: Arial;
	        font-size: 10pt;
	        border-top: 0;
	        border:1px solid #999999;
	        padding: 8px;
	        background-color: #ffffff;
        }
        
    </style>

    </head>

    <body>
     
    <form id="form1" runat="server" style="width:auto; height:auto; color:Black">

     <asp:ScriptManager runat="server" ID="scr"></asp:ScriptManager>
     <div style=" width:100%; text-align: left">
     <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="salesval" HeaderText="Validation Messages"
                            Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
        <div id="Div1" style="width:100%;">
                    <table style="width: 100%;" align="center">
                        <tr style="width: 100%">
                            <td style="width: 100%">
                                <table style="text-align: left; border:1px solid white" width="100%">
                                    <tr style="height:3px">
                                                            </tr>
                                    <tr>
                                        <td colspan="4" class="headerPopUp">
                                             SQL REPORT
                                        </td>
                                    </tr>
                                    <tr style="height:2px">
                                                            </tr>
                                    <tr>
                                        <td colspan="4" style="width: 100%">
                                            <cc1:TabContainer ID="tabContol" runat="server" Width="100%" 
                                               ActiveTabIndex="1"  CssClass="fancy fancy-green">
                                               <cc1:TabPanel ID="tabPanel1" runat="server" HeaderText="Create Report">
                                                  <ContentTemplate>
                                                     <table cellpadding="1" cellspacing="1" width="520px">
                                                        <tr>
                                                            <td class="ControlLabel2" style="width: 25%">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtboxqrynme"  ValidationGroup="salesval"
                                                                     CssClass="lblFont" Display="Dynamic" ErrorMessage="Please enter a Query Name" Text="*"></asp:RequiredFieldValidator>
                                                               QueryName
                                                            </td>
                                                            <td style="width:20%" class="ControlTextBox3">
                                                                <asp:TextBox ID="txtboxqrynme" runat="server" Width="100px" 
                                                                     BackColor = "#90C9FC"   CssClass="cssTextBox" TabIndex="1"></asp:TextBox>
                                                                
                                                            </td>
                                                            <td style="width: 40%">
                                                               
                                                            </td>
                                                            <td style="width: 20%">
                                                               
                                                            </td>
                                                         </tr>
                                                         <tr style="height:1px">
                                                            </tr>
                                                         <tr>
                                                            <td class="ControlLabel2" style="width: 25%">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtboxdescrip"  ValidationGroup="salesval"
                                                                    CssClass="lblFont" Display="Dynamic" ErrorMessage="Please enter Query Description" Text="*" ></asp:RequiredFieldValidator>
                                                                Description
                                                            </td>
                                                            <td style="width:20%" class="ControlTextBox3">
                                                                <asp:TextBox ID="txtboxdescrip" runat="server" Width="150%" 
                                                                    BackColor = "#90C9FC"   CssClass="cssTextBox" TabIndex="2"></asp:TextBox>
                                                                
                                                            </td>
                                                            <td style="width: 40%">
                                                               
                                                            </td>
                                                            <td style="width: 20%">
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="width: 100%">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td class="ControlLabel2" style="width: 20%">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtboxqry" ValidationGroup="salesval"
                                                                                CssClass="lblFont" Display="Dynamic" ErrorMessage="Please enter a valid Query" Text="*"></asp:RequiredFieldValidator>
                                                                           Query
                                                                        </td>
                                                                        <td style="width:70%">
                                                                            <asp:TextBox ID="txtboxqry" runat="server" TextMode="MultiLine" Height="150px" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px"
                                                                                BackColor = "#90C9FC" Width="100%" TabIndex="3"></asp:TextBox>
                                                                            
                                                                        </td>
                                                                        <td style="width: 10%">
                                                               
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr style="height:3px">
                                                            </tr>
                                                        <tr>
                                                             <td style="width: 25%" align="right">
                                                                 
                                                             </td>
                                                             <td style="width: 20%" align="right">
                                                                 <asp:Button ID="btnsavenew" runat="server" CssClass="savebutton1231" EnableTheming="False" ValidationGroup="salesval"
                                                                        OnClick="btnsavenew_Click" TabIndex="4" />
                                                             </td>
                                                             <td style="width: 40%" align="left">
                                                                <asp:Button ID="BtnClear" runat="server" CssClass="RClear" EnableTheming="False"
                                                                      onclick="BtnClear_Click" TabIndex="5"/>
                                                            </td>
                                                            <td style="width: 25%" align="right">
                                                                 
                                                             </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </cc1:TabPanel>

                                            <cc1:TabPanel ID="tabPanelMaster" runat="server" HeaderText="Get Report">
                                                <ContentTemplate>
                                                    <table width="520px" cellpadding="1" cellspacing="1" >
                                                        <tr>
                                                            
                                                            <td class="ControlLabel2" style="width: 25%">
                                                                Reports
                                                            </td>
                                                            <td style="width: 20%;"  class="ControlDrpBorder">
                                                                <asp:DropDownList TabIndex="1" ID="cmbQuries" AppendDataBoundItems="True" style="border-color:#90C9FC"
                                                                  runat="server" AutoPostBack="True" DataValueField="ID" DataTextField="QueryName" height="26px"
                                                                   Width="100%" 
                                                                    CssClass="drpDownListMedium" BackColor = "#90C9FC"  onselectedindexchanged="cmbQuries_SelectedIndexChanged">
                                                                    <asp:ListItem Text="Select Report" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td  style="width: 40%">
                                                               
                                                            </td>
                                                            <td  style="width: 20%">
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr style="height:1px">
                                                            </tr>
                                                        
                                                        <tr>
                                                            <td class="ControlLabel2" style="width: 25%">
                                                                Description
                                                            </td>
                                                            <td style="width:20%" class="ControlTextBox3">
                                                                <asp:TextBox ID="lblDescription" runat="server" Width="150%" 
                                                                    BackColor = "#90C9FC"   CssClass="cssTextBox" TabIndex="2"></asp:TextBox>
                                                            </td>
                                                            <td  style="width: 40%">
                                                               
                                                            </td>
                                                            <td  style="width: 20%">
                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" style="width: 100%">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td class="ControlLabel2" style="width: 20%">
                                                                            Query
                                                                        </td>
                                                                        <td style="width:70%">
                                                                            <asp:TextBox ID="txtQuery" BorderColor="Blue" Width="100%" BorderStyle="Solid" BorderWidth="1px"  runat="server" TextMode="MultiLine" Height="150px" BackColor = "#90C9FC" ></asp:TextBox>   
                                                                        </td>
                                                                        <td style="width:15%">
                                                                           
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            
                                                        </tr>
                                                        <tr style="height:3px">
                                                            </tr>
                                                        
                                                        <tr>
                                                            <td style="width: 20%" align="right">
                                                            </td>
                           
                                                            <td style="width: 20%;" align="right">
                                                                 <asp:Button ID="BtnGenerateReport" runat="server" 
                                                                                            EnableTheming="false"
                                                                                            CssClass="exportexl6" 
                                                                    OnClick="BtnGenerateReport_Click" TabIndex="7" />
                                                            </td>
                                                            <td style="width: 40%;" align="left">
                                                                <asp:Button ID="btncancel2" runat="server" EnableTheming="False" 
                                                                    CssClass="cancelbutton6" OnClientClick="window.close();"/>
                                                             </td>
                                                            <td style="width: 20%" align="right">

                                                            </td>
                               
                                                        </tr>
                                                     </table>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel ID="tabPanel2" runat="server" HeaderText="Delete Report">
                                                <ContentTemplate>
                                                    <table width="520px"  cellpadding="1" cellspacing="1">
                                                        <tr style="height:25px">
                                                            </tr>
                                                        <tr>
                                                            <td class="ControlLabel2" style="width: 35%">
                                                                QueryName
                                                            </td>
                                                            <td style="width: 30%;" class="ControlDrpBorder">
                                                                <asp:DropDownList ID="ddlqueries" AppendDataBoundItems="True" Width="100%" CssClass="drpDownListMedium" BackColor = "#90C9FC" height="26px" style="border-color:#90C9FC"
                                                                        runat="server" AutoPostBack="True" DataValueField="ID" onselectedindexchanged="ddlqueries_SelectedIndexChanged" DataTextField="QueryName" >
                                                                        <asp:ListItem Text="Select Report" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 35%">
                                                            
                                                            </td>
                                                       </tr>
                                                       <tr style="height:25px">
                                                            </tr>
                                                       <tr>
                                                            <td style="width: 35%">
                                                            </td>
                                                            <td  style="width: 25%;" align="center">
                                                                <asp:Button ID="Btndelete" runat="server" CssClass="deletebutton6" ValidationGroup="sendSMS"
                                                                    OnClick="Btndelete_Click" EnableTheming="False" />
                                                            </td>
                                                            <td style="width: 35%">
                                                            </td>
                                                        </tr>
                                                     </table>
                                                 </ContentTemplate>
                                             </cc1:TabPanel>
                                        </cc1:TabContainer>
                                    </td>
                                </tr>
                             </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
