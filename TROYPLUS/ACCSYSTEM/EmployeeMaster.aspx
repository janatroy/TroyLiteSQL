<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="EmployeeMaster.aspx.cs" Inherits="EmployeeMaster" Title="Human Resources > Business Partner" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">

    <script language="javascript" type="text/javascript">        
    </script>

    <style id="Style1" runat="server">
        .fancy-green .ajax__tab_header {
            background: url(App_Themes/NewTheme/Images/green_bg_Tab.gif) repeat-x;
            cursor: pointer;
        }

        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer {
            background: url(App_Themes/NewTheme/Images/green_left_Tab.gif) no-repeat left top;
        }

        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner {
            background: url(App_Themes/NewTheme/Images/green_right_Tab.gif) no-repeat right top;
        }

        .fancy .ajax__tab_header {
            font-size: 13px;
            font-weight: bold;
            color: #000;
            font-family: sans-serif;
        }

            .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer {
                height: 46px;
            }

            .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner {
                height: 46px;
                margin-left: 16px; /* offset the width of the left image */
            }

            .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab {
                margin: 16px 16px 0px 0px;
            }

        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab {
            color: #fff;
        }

        .fancy .ajax__tab_body {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanelPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 695px;">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <div>

                            <%--<div class="mainConHd">
                                    <table cellspacing="0" cellpadding="0" border="0" style="text-align: center">
                                        <tr valign="middle" align="center">
                                            <td>
                                                <span>Business Partners</span>
                                            </td>
                                        </tr>
                                    </table>
                                </div>--%>
                            <%--<table class="mainConHd" style="width: 994px;">
                                    <tr valign="middle">
                                        <td style="font-size: 20px;">
                                            Business Partners
                                        </td>
                                    </tr>
                                </table>--%>
                            <div class="mainConBody">
                                <table style="width: 100%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr style="vertical-align: middle">
                                        <td style="width: 2%;"></td>
                                        <td style="width: 22%; font-size: 22px; color: #000000;">Business Partners
                                        </td>
                                        <td style="width: 16%">
                                            <div style="text-align: right;">
                                                <%--<asp:Panel ID="pnlSearch" runat="server" Width="100px">--%>
                                              
                                                <%--</asp:Panel>--%>
                                            </div>
                                        </td>
                                        <td style="width: 13%; color: #000000;" align="right">Search
                                        </td>
                                        <td style="width: 20%" class="Box1">
                                            <asp:TextBox ID="txtSEmpno" runat="server" CssClass="cssTextBox" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                            <asp:TextBox ID="txtSDesig" runat="server" Width="125px" Height="16px" CssClass="cssTextBox" Visible="False"> </asp:TextBox>
                                            <asp:TextBox ID="txtSDoj" MaxLength="10" runat="server" CssClass="cssTextBox" Visible="False" />
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                PopupButtonID="btnSDate3" PopupPosition="BottomLeft" TargetControlID="txtSDoj">
                                            </cc1:CalendarExtender>
                                            <asp:ImageButton ID="btnSDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                Width="20px" runat="server" Visible="False" />
                                            <asp:TextBox ID="txtEmpMName" runat="server" CssClass="txtBox" Width="150px" Visible="false"></asp:TextBox>
                                            <asp:HiddenField ID="hdEmp" runat="server" Value="0" />
                                        </td>
                                        <td style="width: 1%" align="right"></td>
                                        <td style="width: 20%" class="Box1">
                                            <asp:TextBox ID="txtSEmpName" runat="server" CssClass="cssTextBox" Visible="False"></asp:TextBox>
                                            <div style="width: 150px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="157px" BackColor="#BBCAFB" Height="23px" Style="text-align: center; border: 1px solid #BBCAFB">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="Partner">Partner</asp:ListItem>
                                                    <asp:ListItem Value="PartnerNo">Partner No.</asp:ListItem>
                                                    <asp:ListItem Value="Designation">Designation</asp:ListItem>
                                                    <asp:ListItem Value="DOJ">DOJ</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 18%" class="tblLeftNoPad">
                                            <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click"
                                                EnableTheming="false" CssClass="ButtonSearch6" />
                                        </td>

                                    </tr>
                                </table>
                            </div>
                            <%--<div class="mainConBody">
                                    <table style="width: 100%;" cellpadding="3" cellspacing="2" class="searchbg">
                                        <tr style="height: 20px; vertical-align: middle">
                                            <%-- style="display: none">--%>
                            <%--<td style="width: 21%">
                                            </td>
                                            <td style="width: 10%" align="right">
                                                
                                            </td>
                                            <td style="width: 20%" class="cssTextBoxbg">
                                                
                                            </td>
                                            <td style="width: 8%" align="right">
                                                
                                            </td>
                                            <td style="width: 20%" class="cssTextBoxbg">
                                                
                                            </td>
                                            <td align="left" style="width: 3%">
                                                
                                            </td>
                                            <%--<td class="tblLeft">
                                            </td>--%>
                            <%--<td style="width: 20%; text-align: left" class="cssTextBoxbgnew2">
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </div>--%>
                        </div>

                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="popUp" Style="width: 60%">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="contentPopUp">
                                        <asp:Panel ID="pnlEmp" runat="server" Visible="false">
                                            <div class="divArea">
                                                <table class="tblLeft" cellpadding="3" cellspacing="5" style="border: 1px solid #5078B3;"
                                                    width="100%">
                                                    <tr>
                                                        <td colspan="4">
                                                            <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                <tr>
                                                                    <td>Business Partners
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div align="left">
                                                                <table style="width: 100%; border: 0px solid #86b2d1" align="center" cellpadding="3" cellspacing="1">
                                                                    <tr style="height: 20px">
                                                                        <td style="width: 25%" class="ControlLabel">
                                                                            <asp:RequiredFieldValidator ValidationGroup="Save" ID="rq" runat="server" ErrorMessage="Emp no is mandatory"
                                                                                Text="*" ControlToValidate="txtEmpno"></asp:RequiredFieldValidator>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtEmpno"
                                                                                FilterType="Numbers" />
                                                                            Partner No. *
                                                                        </td>
                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                            <asp:TextBox ID="txtEmpno" runat="server" SkinID="skinTxtBox"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 15%" class="ControlLabel">Type
                                                                            <%--<asp:RequiredFieldValidator ID="reqSuppllier" runat="server" ControlToValidate="drptype"
                                                                                ErrorMessage="Partner Type is mandatory" InitialValue="0" Text="*"
                                                                                ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                            <asp:DropDownList ID="drptype" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor="#90c9fc" Style="border: 1px solid #90c9fc" Height="26px">
                                                                                <asp:ListItem Value="0" Selected="True">Select Type</asp:ListItem>
                                                                                <asp:ListItem Value="Partner">Partner</asp:ListItem>
                                                                                <asp:ListItem Value="Employee">Employee</asp:ListItem>
                                                                                <asp:ListItem Value="Others">Others</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                    </tr>
                                                                    <tr style="vertical-align: bottom">
                                                                        <td style="width: 25%" class="ControlLabel">Title
                                                                        </td>
                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                            <asp:DropDownList ID="drpTitle" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor="#90c9fc" Style="border: 1px solid #90c9fc" Height="26px">
                                                                                <asp:ListItem Value="Mr" Selected="True">Mr</asp:ListItem>
                                                                                <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                                                                                <asp:ListItem Value="Miss">Miss</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width: 15%" class="ControlLabel">
                                                                            <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator1" runat="server"
                                                                                ErrorMessage="Emp First Name is mandatory" Text="*" ControlToValidate="txtEmpFName"></asp:RequiredFieldValidator>
                                                                            Partner Name *
                                                                        </td>
                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                            <asp:TextBox ID="txtEmpFName" runat="server" CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 25%" class="ControlLabel">Surname
                                                                        </td>
                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                            <asp:TextBox ID="txtEmpSName" runat="server" SkinID="skinTxtBox"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 15%" class="ControlLabel">
                                                                            <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator4" runat="server"
                                                                                Text="*" ErrorMessage="Date Of Birth is mandatory" ControlToValidate="txtDOB"></asp:RequiredFieldValidator>
                                                                            Date Of Birth *
                                                                        </td>
                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                            <asp:TextBox ID="txtDOB" Width="100px" MaxLength="10" runat="server" CssClass="cssTextBox" />
                                                                            <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtDOB">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                Width="20px" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                    </tr>
                                                                    <tr style="width: 100%">
                                                                        <td style="width: 25%" class="ControlLabel">
                                                                            <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator3" runat="server"
                                                                                Text="*" ErrorMessage="Designation is mandatory" ControlToValidate="txtDesig"></asp:RequiredFieldValidator>
                                                                            Designation *
                                                                        </td>
                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                            <asp:TextBox ID="txtDesig" runat="server" SkinID="skinTxtBox"> </asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 15%" class="ControlLabel">
                                                                            <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator2" runat="server"
                                                                                Text="*" ErrorMessage="Date OF Joining is mandatory" ControlToValidate="txtDoj"></asp:RequiredFieldValidator>
                                                                            Date Of Joining *
                                                                        </td>
                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                            <asp:TextBox ID="txtDoj" Width="100px" MaxLength="10" runat="server" CssClass="cssTextBox" />
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnD3" PopupPosition="BottomLeft" TargetControlID="txtDoj">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnD3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                Width="20px" runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="vertical-align: bottom">
                                                                        <td style="width: 25%" class="ControlLabel">Manager
                                                                        </td>
                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                            <asp:DropDownList ID="drpIncharge" TabIndex="11" Enabled="True" EnableTheming="false" AppendDataBoundItems="true" CssClass="drpDownListMedium"
                                                                                runat="server" Width="100%" DataTextField="empFirstName" BackColor="#90c9fc" Style="border: 1px solid #90c9fc" Height="26px"
                                                                                DataValueField="empno">
                                                                                <asp:ListItem Text="Select Manager" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width: 15%" class="ControlLabel">
                                                                            <%--<asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator5" runat="server"
                                                                                ErrorMessage="User Group is mandatory" Text="*" ControlToValidate="txtUserGroup"></asp:RequiredFieldValidator>--%>
                                                                            User Group
                                                                        </td>
                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                            <asp:TextBox ID="txtUserGroup" runat="server" CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="width: 100%">
                                                                        <td colspan="4" style="width: 100%">
                                                                            <table cellpadding="3" cellspacing="1" style="width: 100%">
                                                                                <tr runat="server" id="rowremarks" style="width: 100%">
                                                                                    <td style="width: 25%" class="ControlLabel">Remarks
                                                                                    </td>
                                                                                    <td style="width: 25%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtRemarks" Height="24px" TextMode="MultiLine" MaxLength="10"
                                                                                            runat="server" CssClass="cssTextBox" />
                                                                                    </td>
                                                                                    <td style="width: 15%"></td>
                                                                                    <td style="width: 25%"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                                <table align="center" width="100%">
                                                                    <tr style="width: 20px">
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 30%"></td>
                                                                        <td style="width: 20%;" align="center">
                                                                            <asp:Button ID="btnCancel" runat="server" CssClass="cancelbutton6" EnableTheming="false"
                                                                                SkinID="skinBtnCancel" OnClick="btnCancel_Click" />
                                                                        </td>
                                                                        <td style="width: 20%;" align="center">
                                                                            <asp:Button ID="btnSave" ValidationGroup="Save" runat="server" CssClass="savebutton1231"
                                                                                EnableTheming="false" SkinID="skinBtnSave" OnClick="btnSave_Click" />
                                                                            <asp:Button ID="btnUpdate" runat="server" ValidationGroup="Save" CssClass="Updatebutton1231"
                                                                                EnableTheming="false" SkinID="skinBtnSave" OnClick="btnUpdate_Click" />
                                                                        </td>
                                                                        <td style="width: 30%"></td>
                                                                    </tr>
                                                                </table>
                                                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" ShowMessageBox="true"
                                                                    ShowSummary="false" HeaderText="Validation Messages" ValidationGroup="Save" Font-Names="'Trebuchet MS'"
                                                                    Font-Size="12" runat="server" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>


                        <input id="FakeCancelBtn2" type="button" style="display: none" runat="server" />
                        <input id="FakeTargetBtn2" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="FakeCancelBtn2" DynamicServicePath="" Enabled="True" PopupControlID="EmployeePayCompPopUp"
                            TargetControlID="FakeTargetBtn2">
                        </cc1:ModalPopupExtender>

                        <asp:Panel runat="server" ID="EmployeePayCompPopUp" Style="width: 57%">
                            <div id="contentEmployeePopUp">
                                <table style="width: 100%;" align="center">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">
                                            <div class="divArea" style="height: 600px; overflow-x: auto;">
                                                <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                    <%--<tr>
                                                                <td colspan="5" class="headerPopUp">
                                                                    Role Pay Component Mapping
                                                                </td>
                                                            </tr>--%>
                                                    <tr style="height: 5px">
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5" align="left">
                                                            <cc1:TabContainer ID="TabContainer2" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                <cc1:TabPanel ID="TabPanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <table style="width: 770px; border: 0px solid #86b2d1; vertical-align: text-top" align="center" cellpadding="3"
                                                                            cellspacing="1">

                                                                            <tr>
                                                                                <td style="width: 40%">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <asp:HiddenField ID="ManagePayRoleID" Visible="false" runat="server"></asp:HiddenField>

                                                                                            <td class="ControlLabel" style="width: 20%">Role Name :                                                               
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 20%">
                                                                                                <asp:TextBox ID="txtManagePayCompRoleName" Enabled="false" runat="server"
                                                                                                    SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="ControlLabel" style="width: 25%; font-weight: bold;">Role Based Pay Component List:                                                                   
                                                                                            </td>
                                                                                            <td class="ControlLabel" style="width: 12%">Filter By: </td>

                                                                                            <td style="width: 10%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="roleSearch" Enabled="true" runat="server"
                                                                                                    SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%" class="tblLeftNoPad">
                                                                                                <asp:Button ID="SearchRoleBtn" Text="Search" runat="server" CausesValidation="False" CommandName="searchPay"
                                                                                                    EnableTheming="false" SkinID="skinBtnCancel" OnClick="SearchRoleBtn_Click"></asp:Button>
                                                                                            </td>

                                                                                        </tr>

                                                                                    </table>
                                                                                </td>
                                                                                <td style="width: 10%"></td>
                                                                            </tr>

                                                                            <tr>

                                                                                <asp:GridView ID="ManagePayComponentGrid" runat="server" Visible="true" AllowSorting="True" AutoGenerateColumns="False"
                                                                                    Width="99.9%" AllowPaging="True" DataKeyNames="PayComponentID" OnSelectedIndexChanged="ManagePayComponentGrid_SelectedIndexChanged"
                                                                                    OnRowDataBound="ManagePayComponentGrid_RowDataBound" EmptyDataText="No Pay Component List Found." Font-Names="Trebuchet MS">

                                                                                    <Columns>

                                                                                        <asp:BoundField DataField="PayComponentID" HeaderText="PayComponent ID" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                                                        <%--<asp:BoundField DataField="Role_ID" HeaderText="Role ID" Visible="false" HeaderStyle-BorderColor="Gray" />--%>
                                                                                        <asp:BoundField DataField="PayComponentName" HeaderText="Pay Component Name" HeaderStyle-BorderColor="Gray" />
                                                                                        <%-- <asp:BoundField DataField="AllowedCount" HeaderText="Limit/Count" HeaderStyle-BorderColor="Gray" />
                                                                                    <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" Visible="true" HeaderStyle-BorderColor="Gray" />--%>
                                                                                        <asp:BoundField DataField="Description" HeaderText="Pay Component Description" Visible="true" HeaderStyle-BorderColor="Gray" />

                                                                                    </Columns>
                                                                                    <PagerTemplate>
                                                                                        <table style="border-color: white">
                                                                                            <tr style="border-color: white">
                                                                                                <td style="border-color: white">Goto Page
                                                                                                </td>
                                                                                                <td style="border-color: white">
                                                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Style="border: 1px solid blue" BackColor="#BBCAFB" Width="75px" Height="25px">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td style="border-color: white; width: 5px"></td>
                                                                                                <td style="border-color: white">
                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                                                        ID="btnFirst" />
                                                                                                </td>
                                                                                                <td style="border-color: white">
                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                                                        ID="btnPrevious" />
                                                                                                </td>
                                                                                                <td style="border-color: white">
                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                                                        ID="btnNext" />
                                                                                                </td>
                                                                                                <td style="border-color: white">
                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                                                        ID="btnLast" />
                                                                                                </td>

                                                                                            </tr>
                                                                                        </table>
                                                                                    </PagerTemplate>
                                                                                </asp:GridView>

                                                                            </tr>

                                                                            <tr style="height: 3px">
                                                                            </tr>

                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td class="ControlLabel" style="width: 20%">From Date :
                                                                                    <%--<asp:RequiredFieldValidator ID="rvFromDateEdit" runat="server" ControlToValidate="txtfrmDate"
                                                                            Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="From Date is mandatory"></asp:RequiredFieldValidator>--%>
                                                                 
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 20%">
                                                                                                <asp:TextBox ID="txtfrmDate" Enabled="false" runat="server" ToolTip="Select Pay Name"
                                                                                                    SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="calEditDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                    PopupButtonID="btnEditDate" PopupPosition="BottomLeft" TargetControlID="txtfrmDate">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                            <td style="width: 30%" align="left">
                                                                                                <asp:ImageButton ID="btnEditDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                    Width="20px" runat="server" />
                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td class="ControlLabel" style="width: 20%">Amount Declared :
                                                                                            <%--<asp:RequiredFieldValidator ID="ReqAmountName" runat="server" ControlToValidate="txtDeclaredAmt"
                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Amount is mandatory"></asp:RequiredFieldValidator>--%>
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 20%">
                                                                                                <asp:TextBox ID="txtDeclaredAmt" Enabled="false" runat="server" ToolTip="Select Pay Name"
                                                                                                    SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>

                                                                            <tr style="height: 3px">
                                                                            </tr>

                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td style="width: 20%">
                                                                                                <asp:Button ID="addSelectedBtn" Text="Add Selected" runat="server" CausesValidation="False" CommandName="addPayAmount"
                                                                                                    EnableTheming="false" SkinID="skinBtnCancel" OnClick="addSelectedBtn_Click"></asp:Button>

                                                                                                <asp:Button ID="removeSelectedBtn" Text="Remove Selected" runat="server" CausesValidation="False" CommandName="removePayAmount"
                                                                                                    EnableTheming="false" SkinID="skinBtnCancel" OnClick="removeSelectedBtn_Click"></asp:Button>
                                                                                            </td>
                                                                                            <td style="width: 20%"></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>

                                                                            <tr style="height: 3px">
                                                                            </tr>

                                                                            <tr>
                                                                                <td style="width: 10%">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td class="ControlLabel" style="width: 4%; font-weight: bold;">Pay components selected for role:
                                                                                            </td>
                                                                                            <td class="ControlLabel" style="width: 10%"></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td>
                                                                                    <asp:GridView ID="PayCompRolePayGrid" runat="server" Visible="true" AllowSorting="True" AutoGenerateColumns="False"
                                                                                        Width="99.9%" AllowPaging="True" DataKeyNames="PayComponent_ID"
                                                                                        EmptyDataText="No Pay Component for Roles List Found." Font-Names="Trebuchet MS">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="PayComponent_ID" HeaderText="PayComponent ID" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                                                            <asp:BoundField DataField="Role_ID" HeaderText="Role ID" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                                                            <asp:BoundField DataField="PayComponentName" HeaderText="Pay Component Name" HeaderStyle-BorderColor="Gray" />
                                                                                            <asp:BoundField DataField="Description" HeaderText="Pay Component Description" Visible="true" HeaderStyle-BorderColor="Gray" />
                                                                                            <asp:BoundField DataField="EffectiveDate" HeaderText="From Date" Visible="true" HeaderStyle-BorderColor="Gray" />
                                                                                            <asp:BoundField DataField="DeclaredAmount" HeaderText="Amount Declared" Visible="true" HeaderStyle-BorderColor="Gray" />
                                                                                        </Columns>
                                                                                        <PagerTemplate>
                                                                                            <table style="border-color: white">
                                                                                                <tr style="border-color: white">
                                                                                                    <td style="border-color: white">Goto Page
                                                                                                    </td>
                                                                                                    <td style="border-color: white">
                                                                                                        <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Style="border: 1px solid blue" BackColor="#BBCAFB" Width="75px" Height="25px">
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="border-color: white; width: 5px"></td>
                                                                                                    <td style="border-color: white">
                                                                                                        <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                                                            ID="btnFirst" />
                                                                                                    </td>
                                                                                                    <td style="border-color: white">
                                                                                                        <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                                                            ID="btnPrevious" />
                                                                                                    </td>
                                                                                                    <td style="border-color: white">
                                                                                                        <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                                                            ID="btnNext" />
                                                                                                    </td>
                                                                                                    <td style="border-color: white">
                                                                                                        <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                                                            ID="btnLast" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </PagerTemplate>
                                                                                    </asp:GridView>
                                                                                </td>

                                                                            </tr>


                                                                            <tr>
                                                                                <td>
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td style="width: 20%">
                                                                                                <asp:Button ID="CancelRolePayMapBtn" Text="Cancel" runat="server" CausesValidation="False" CommandName="CancelRolePay"
                                                                                                    EnableTheming="false" SkinID="skinBtnCancel" OnClick="CancelRolePayMapBtn_Click"></asp:Button>

                                                                                                <asp:Button ID="AssignRolePayMapBtn" Text="Assign" runat="server" CausesValidation="False" CommandName="AssignRolePay"
                                                                                                    EnableTheming="false" SkinID="skinBtnCancel" OnClick="AssignRolePayMapBtn_Click"></asp:Button>
                                                                                            </td>
                                                                                            <td style="width: 20%"></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>


                                                                            <tr style="height: 3px">
                                                                            </tr>

                                                                        </table>
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel>
                                                            </cc1:TabContainer>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 6px">
                                                    </tr>
                                                    <%-- <tr>
                                                                <td colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 32%;">
                                                                            </td>
                                                                            <td style="width: 19%;">
                                                                                <asp:Button ID="managePayCompCancelbtn" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td style="width: 19%;">
                                                                                <asp:Button ID="managePayCompAssignbtn" runat="server" CausesValidation="True" CommandName="Assign"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave" 
                                                                                    OnClick="managePayCompAssignbtn_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 30%;">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>--%>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>

                    </td>
                </tr>
                <%--<tr>
                    
                </tr>--%>
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdEmp" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowCommand="GrdEmp_RowCommand"
                                            Width="99.9%" AllowPaging="True" OnPageIndexChanging="GrdEmp_PageIndexChanging" OnRowDataBound="GrdEmp_RowDataBound"
                                            OnRowCreated="GrdEmp_RowCreated" DataKeyNames="Empno" PageSize="7" EmptyDataText="No Business Partner found"
                                            OnSelectedIndexChanged="GrdEmp_SelectedIndexChanged" OnRowDeleting="GrdEmp_RowDeleting" CssClass="someClass">
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="empno" HeaderText="Partner No" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="empTitle" HeaderText="Title" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="empFirstname" HeaderText="First Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="empMiddlename" HeaderText="Middle Name" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="empSurname" HeaderText="Sur Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="empDOJ" HeaderText="Date of Join" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="empDOB" HeaderText="Date of Birth" Visible="false" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-BorderColor="Gray" />
                                                <%--<asp:BoundField DataField="empType" HeaderText="Type"  HeaderStyle-BorderColor="Gray"/>--%>
                                                <asp:BoundField DataField="empDesig" HeaderText="Designation" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="empRemarks" HeaderText="Remarks" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Managed Pay Component">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnPayEdit" runat="server" SkinID="edit" CommandName="ManagePay" CommandArgument='<%#Eval("empSurname").ToString() + ":" + Eval("empno").ToString() %>' />
                                                        <asp:ImageButton ID="btnPayEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="command" Width="80px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Employee Details?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" BackColor="#BBCAFB" Width="70px" Height="24px" Style="border: 1px solid blue"
                                                                OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="border-color: white; width: 5px"></td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnFirst" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnPrevious" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnNext" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnLast" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td style="width:50%" align="right" >
                  <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Text=""></asp:Button>
            </td>
            <td style="width:50%">
                <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6" OnClientClick="window.open('ReportExcelEmployee.aspx ','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                    EnableTheming="false"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
