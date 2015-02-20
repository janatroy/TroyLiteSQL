<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="HRAdminSettings.aspx.cs" Inherits="HR_AdminSettings" Title="Human Resources > Admin Settings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script type="text/javascript">

        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_tabs2');
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
    
    </script>

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

    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%;">
               
                <div class="mainConBody">
                    <table style="width: 100%;margin: -1px 0px 0px 1px;" cellpadding="2px" cellspacing="2px" class="searchbg">
                        <tr>
                            <td style="width: 1%">
                            </td>
                            <td style="width: 25%; font-size: 22px; color: #000000;" >
                                 HR Admin Settings
                            </td>
                            <td style="width: 14%">
                                            
                            </td>
                            <td style="width: 10%; color: #000000;" align="right">
                                            
                            </td>
                            <td style="width: 19%">
                                            
                            </td>
                            <td style="width: 18%">
                                            
                            </td>
                                        
                            </tr>
                    </table>
                </div>
                <table style="text-align: left; border: 0px solid #5078B3; padding-left:3px; width: 1105px" cellpadding="0"
                    cellspacing="0">
                    <tr>
                        <td>
                            <div align="center" style="width: 980px; text-align: left">
                                <cc1:TabContainer ID="tabs2" runat="server" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                    <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Settings">
                                        <ContentTemplate>
                                            <div style="text-align: left;">
                                                <table style="width: 960px; font-size: 11px; font-family: 'Trebuchet MS';" cellpadding="3"
                                                    cellspacing="1">
                                                    <tr>
                                                        <asp:HiddenField ID="SettingsID" Visible="false" runat="server"></asp:HiddenField>  
                                                                                                                                  
                                                        <td style="width: 25%" class="ControlLabel">                                                          
                                                            Holiday count per year :
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtHolidayCount" runat="server" Width="300px" CssClass="cssTextBox"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="holidayCount" ForeColor="Red" ValidationGroup="adminInfo"
                                                                ErrorMessage="Holiday per year is mandatory" Font-Bold="true" runat="server"  Text="*"
                                                                ControlToValidate="txtHolidayCount"></asp:RequiredFieldValidator>
                                                        </td>                                                         
                                                        
                                                        <td style="width: 25%">
                                                            <asp:RangeValidator ValidationGroup="adminInfo" ControlToValidate="txtHolidayCount" MinimumValue="1" MaximumValue="365" Type="Integer" EnableClientScript="false"
                                                                Text="Holiday cannot be more than 365 Days!" runat="server" />
                                                            <asp:RegularExpressionValidator runat="server" id="rexNumber" controltovalidate="txtHolidayCount" 
                                                               Display="Static" EnableClientScript="true" validationexpression="\d+" errormessage="Holiday count should be number" />
                                                        </td>
                                                        <td style="width: 25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%;"  class="ControlLabel">
                                                            Permission hours allowed per day :
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtPermissionHr" runat="server" CssClass="cssTextBox" ></asp:TextBox>
                                                             <asp:RequiredFieldValidator ID="ReqPermission" ForeColor="Red" ValidationGroup="adminInfo"
                                                                ErrorMessage="Permission hour is mandatory" Font-Bold="true" runat="server"  Text="*"
                                                                ControlToValidate="txtPermissionHr"></asp:RequiredFieldValidator>                                                             
                                                        </td>
                                                        <td style="width: 25%">
                                                            <asp:RangeValidator ValidationGroup="adminInfo" ControlToValidate="txtPermissionHr" MinimumValue="1" MaximumValue="4" Type="Integer" EnableClientScript="false"
                                                                Text="Permission cannot be more than 4 hours!" runat="server" />
                                                            <asp:RegularExpressionValidator runat="server" id="RegularExpressionValidator1" controltovalidate="txtPermissionHr" 
                                                              Display="Static" EnableClientScript="true"  validationexpression="\d+" errormessage="Permission hour should be number" />
                                                        </td>
                                                        <td style="width: 25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">
                                                            No of permission allowed per month :
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox TextMode="MultiLine" ID="txtNumPermission" runat="server" CssClass="cssTextBox"
                                                                Width="300px" Height="16px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="adminInfo"
                                                                ErrorMessage="Permission allowed is mandatory" Font-Bold="true" runat="server"  Text="*"
                                                                ControlToValidate="txtNumPermission"></asp:RequiredFieldValidator>   
                                                        </td>                                                       
                                                        <td style="width: 25%">
                                                            <asp:RegularExpressionValidator runat="server" id="RegularExpressionValidator2" controltovalidate="txtNumPermission" 
                                                          Display="Static" EnableClientScript="true" validationexpression="\d+" errormessage="Permission allowed should be number" />
                                                        </td>
                                                        <td style="width: 25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">                                                           
                                                            No of days Comp off / overtime will be valid :
                                                        </td>
                                                        <td style="width: 25%" valign="top" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtCompOff" runat="server" CssClass="cssTextBox" Width="300px" Height="16px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="adminInfo"
                                                                ErrorMessage="Comp off/overtime is mandatory" Font-Bold="true" runat="server" Text="*" ControlToValidate="txtCompOff"></asp:RequiredFieldValidator>
                                                        </td>
                                                        
                                                        
                                                        <td style="width: 25%">
                                                            <asp:RegularExpressionValidator runat="server" id="RegularExpressionValidator3" controltovalidate="txtCompOff" 
                                                            Display="Static" EnableClientScript="true" validationexpression="\d+" errormessage="Comp off/Overtime should be number" />
                                                        </td>
                                                        <td style="width: 25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">                                                            
                                                            No of work days per week :
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:TextBox ID="txtWorkDays" runat="server" CssClass="cssTextBox" Width="300px" Height="16px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rvPin" ForeColor="Red" ValidationGroup="adminInfo"
                                                                ErrorMessage="Work days is mandatory" Font-Bold="true" runat="server" Text="*"
                                                                ControlToValidate="txtWorkDays"></asp:RequiredFieldValidator>
                                                        </td>                                                         
                                                        
                                                        <td style="width: 25%">
                                                            <asp:RangeValidator ValidationGroup="adminInfo" ControlToValidate="txtWorkDays" MinimumValue="1" MaximumValue="6" Type="Integer" EnableClientScript="false"
                                                                Text="Work days per week cannot be more than 6 days!" runat="server" />
                                                            <asp:RegularExpressionValidator runat="server" id="RegularExpressionValidator4" controltovalidate="txtWorkDays" 
                                                           Display="Static" EnableClientScript="true"  validationexpression="\d+" errormessage="Work days should be number" />
                                                        </td>
                                                        <td style="width: 25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="ControlLabel">                                                          
                                                            Can supervisor overwrite no of work days per week : 
                                                        </td>
                                                        <td style="width: 25%" class="ControlTextBox3">
                                                            <asp:CheckBox ID="chkSupervisor" runat="server" CssClass="cssTextBox" Width="300px" Height="16px"></asp:CheckBox>
                                                        </td>
                                                        <td style="width: 25%">
                                                        </td>
                                                        <td style="width: 25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px">
                                                        </td>
                                                    </tr>
                                                    <tr>     
                                                        
                                                         <td align="right" style="width: 10%;">
                                                                            </td>
                                                                            <td style="width: 10%;">                                                                               
                                                                                 <asp:Button ID="btnSettingsSave" runat="server" SkinID="skinBtnSave" ValidationGroup="adminInfo"
                                                                CssClass="Updatebutton1231" EnableTheming="false" OnClick="btnSettingsSave_Click" />
                                                                            </td>
                                                                            <td style="width: 10%;">
                                                                                <asp:Button ID="btnSettingsCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="btnSettingsCancel_Click">
                                                                                </asp:Button>

                                                            <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                ShowSummary="false" HeaderText="Validation Messages" ValidationGroup="adminInfo"
                                                                Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                                            </td>
                                                                            <td style="width: 70%;">
                                                                            </td>
                                                                                                           
               
                                                    </tr>
                                                   
                                                    <%--<tr>
                                            <td colspan="4" align="center">
                                                <%--<hr />--%>
                                                    <%--</td>
                                        </tr>--%>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </cc1:TabPanel>                                   
                                </cc1:TabContainer>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
