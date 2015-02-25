<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ProjectEntry.aspx.cs" Inherits="ProjectEntry" Title="Project > ProjectEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">
        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_tbMain');

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

        function EnableDisableButton(sender, target) {
            if (sender.value.length > 0) 
                document.getElementById('<%= BtnClearFilter.ClientID %>').disabled = false;

            else 
                document.getElementById('<%= BtnClearFilter.ClientID %>').disabled = true;
                alert('tested');
            

        }


        //Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        //function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }



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

    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <%--<div class="mainConHd">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr style="text-align: center; vertical-align: text-top">
                                <td>
                                    <span>Resource Work Entries</span>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Resource Work Entries
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <div style="text-align: left">
                                <table style="width: 99.8%; margin: -2px 0px 0px 2px;" cellpadding="3" cellspacing="2" class="searchbg">
                                    <tr>
                                        <td style="width: 8%;"></td>
                                        <td style="width: 40%; font-size: 22px; color: White">Manage Projects
                                        </td>
                                        <td style="width: 17%">
                                            <div style="text-align: right;">
                                                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                    <%--    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                        EnableTheming="false" Width="80px" Text=""></asp:Button>--%>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                        <%--<td style="width: 12%" align="center">
                                        Executive Name
                                    </td>--%>

                                        <td style="width: 15%; color: White;" align="right">Search
                                        </td>
                                        <td style="width: 20%" class="NewBox">
                                            <asp:TextBox ID="txtSearch" runat="server" Width="152px" OnTextChanged="txtSearch_TextChanged"  AutoPostBack="true"></asp:TextBox>

                                          <%--  <asp:TextBox ID="txtSearch" onkeyup="EnableDisableButton(this,'btnReset')" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>--%>
                                        </td>
                                        <td style="width: 20%" class="NewBox">
                                            <div style="width: 150px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" BackColor="White" Width="157px" Height="24px" Style="text-align: center; border: 1px solid White">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <%--<asp:ListItem Value="ProjectID">Project ID</asp:ListItem>--%>
                                                    <asp:ListItem Value="ProjectCode">Project ID</asp:ListItem>
                                                    <asp:ListItem Value="ProjectDate">Project Date</asp:ListItem>
                                                    <asp:ListItem Value="ProjectName">Project Title</asp:ListItem>
                                                    <asp:ListItem Value="ProjectManager">Project Manager</asp:ListItem>
                                                    <asp:ListItem Value="ProjectStatus">Project Status</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 20%" class="tblLeftNoPad">
                                            <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click"
                                                CssClass="ButtonSearch6" EnableTheming="false" />
                                        </td>
                                        <td style="width: 16%" class="tblLeftNoPad">
                                            <asp:Button ID="BtnClearFilter" Enabled="false" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td></td>
                                        <td style="width: 25%" class="tblRight">Expected Work Start Date
                                        </td>
                                        <td style="width: 22%" class="cssTextBoxbgnew2">
                                            <asp:TextBox ID="txtStartDate" Enabled="false" runat="server" CssClass="cssTextBox" Width="100px"
                                                MaxLength="10" />
                                            <script type="text/javascript" language="JavaScript">
                                                new tcal({ 'formname': 'aspnetForm', 'controlname': GettxtBoxName('txtStartDate') });
                                            </script>
                                        </td>
                                        <td style="width: 15%">Expected Work End Date
                                        </td>
                                        <td style="width: 22%" class="cssTextBoxbgnew2">
                                            <asp:TextBox ID="txtEndDate" Enabled="false" runat="server" CssClass="cssTextBox" Width="100px" MaxLength="10" />
                                            <script type="text/javascript" language="JavaScript">
                                                new tcal({ 'formname': 'aspnetForm', 'controlname': GettxtBoxName('txtEndDate') });
                                            </script>
                                        </td>
                                        <td style="width: 10%"></td>
                                    </tr>
                                    <tr style="display: none">
                                        <td></td>
                                        <td style="width: 25%" class="tblRight">Creation Date
                                        </td>
                                        <td style="width: 22%" class="cssTextBoxbgnew2">
                                            <asp:TextBox ID="txtsCreationDate" Enabled="false" runat="server" CssClass="cssTextBox" Width="100px"
                                                MaxLength="10" />
                                            <script type="text/javascript" language="JavaScript">                                            new tcal({ 'formname': 'aspnetForm', 'controlname': GettxtBoxName('txtsCreationDate') });</script>
                                        </td>
                                        <td style="width: 15%">Status
                                        </td>
                                        <td style="width: 22%">
                                            <div style="border-width: 1px; border-color: #90c9fc; border-style: solid; width: 130px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="drpsStatus" runat="server" Width="98%" CssClass="drpDownListMedium" BackColor="#90c9fc"
                                                    AppendDataBoundItems="True">
                                                    <asp:ListItem Value="" style="background-color: #90c9fc">All Status</asp:ListItem>
                                                    <asp:ListItem Value="Open">Open</asp:ListItem>
                                                    <asp:ListItem Value="Resolved">Resolved</asp:ListItem>
                                                    <asp:ListItem Value="Closed">Closed</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 10%; text-align: left"></td>
                                    </tr>
                                    <tr style="display: none">
                                        <td align="left" colspan="4">
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                                                ControlToValidate="txtEndDate" Display="None" ErrorMessage="Expected Start Date should be less than or equal to Expected End Date."
                                                CssClass="lblFont" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <%--</div>--%>

                        <asp:HiddenField ID="hdTse" runat="server" Value="0" />
                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <div class="alignLeft">
                            <asp:Panel runat="server" ID="popUp" Style="width: 65%; display: none">
                                <div id="contentPopUp">
                                    <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3; background-color: #fff; color: #000;"
                                        width="100%">
                                        <tr>
                                            <td>
                                                <div>
                                                    <table class="tblLeft" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3;"
                                                        width="100%">
                                                        <tr>
                                                            <td colspan="8">
                                                                <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                    <tr>
                                                                        <td><%--Add New Project--%>
                                                                            <asp:Label runat="server" ID="headtitle">
                                                                               
                                                                            </asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div style="text-align: left">
                                                                    <cc1:TabContainer ID="tbMain" runat="server" Width="100%" Visible="false" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabMaster" runat="server" OnDataBinding="GrdWME_SelectedIndexChanged" HeaderText="New Project Details">
                                                                            <ContentTemplate>
                                                                                <table style="width: 1000px;" align="center" cellpadding="2" cellspacing="2">
                                                                                    <tr>
                                                                                        <td style="width: 20%" class="ControlLabelproject1">
                                                                                            <asp:RequiredFieldValidator ValidationGroup="Save" ID="RequiredFieldValidator2" runat="server"
                                                                                                Text="*" ErrorMessage="Please enter Project ID. It cannot be left blank. " ControlToValidate="txtProjectCode"></asp:RequiredFieldValidator>
                                                                                            Project ID *
                                                                                        </td>
                                                                                        <td style="width: 20%" class="ControlTextProject1">
                                                                                            <asp:TextBox ID="txtProjectCode" CssClass="cssTextBox" runat="server"
                                                                                                TabIndex="1" />
                                                                                        </td>
                                                                                        <td style="width: 5%"></td>
                                                                                        <td class="ControlLabelproject1" style="width: 30%">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEWstartDate" ErrorMessage="Please select Expected Start Date. It cannot be left blank." Text="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="txtEWstartDate" ControlToValidate="txtEWEndDate" CssClass="lblFont" ErrorMessage="Due Date should be Greater than or equal to Expected Start Date" Operator="GreaterThanEqual" SetFocusOnError="True" Text="*" Type="Date" ValidationGroup="Save"></asp:CompareValidator>
                                                                                            Expected Start Date * </td>
                                                                                        <td class="ControlTextProject1" style="width: 20%">
                                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtEWstartDate" runat="server" CssClass="cssTextBox" Enabled="false" MaxLength="10" Width="100px" />
                                                                                                    <cc1:CalendarExtender ID="calEWstartDate" runat="server" Animated="true" Format="dd/MM/yyyy" PopupButtonID="btnEWstartDate" PopupPosition="BottomLeft" TargetControlID="txtEWstartDate">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <td style="width: 5%">
                                                                                            <asp:ImageButton ID="btnEWstartDate" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" TabIndex="7" Width="20px" />
                                                                                        </td>



                                                                                    </tr>
                                                                                    <tr style="height: 2px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabelproject1" style="width: 20%">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtProjectName" ErrorMessage="Please enter Title of Project. It cannot be left blank." Text="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                            Title of Project * </td>
                                                                                        <td class="ControlTextProject1" style="width: 20%">
                                                                                            <asp:TextBox ID="txtProjectName" CssClass="cssTextBox" runat="server" TabIndex="2"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 5%"></td>
                                                                                        <td class="ControlLabelproject1" style="width: 30%">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Text="*" runat="server"
                                                                                                ControlToValidate="txtEWEndDate" ValidationGroup="Save" ErrorMessage="Please select Due Date. It cannot be left blank."></asp:RequiredFieldValidator>
                                                                                            Due Date *
                                                                                        </td>
                                                                                        <td style="width: 20%" class="ControlTextProject1">
                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtEWEndDate" Enabled="false" runat="server" CssClass="cssTextBox" Width="100px"
                                                                                                        MaxLength="10" />
                                                                                                    <cc1:CalendarExtender ID="CalEWEndDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                        PopupButtonID="BtnCalDate2" PopupPosition="BottomLeft" TargetControlID="txtEWEndDate">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>

                                                                                        </td>
                                                                                        <td style="width: 5%">
                                                                                            <asp:ImageButton ID="BtnCalDate2" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                CausesValidation="False" Width="20px" runat="server" TabIndex="8" />
                                                                                        </td>


                                                                                    </tr>
                                                                                    <tr style="height: 2px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabelproject1" style="width: 20%">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCDate" ErrorMessage="Creation Date is mandatory" Text="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                            Project Status * </td>
                                                                                        <td class="ControlDrpBorder" style="width: 20%">
                                                                                            <asp:DropDownList ID="drpProjectstatus" runat="server" AppendDataBoundItems="True" BackColor="#E7E7E7" CssClass="drpDownListMedium" EnableTheming="False" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="3" Width="100%">
                                                                                                <asp:ListItem Text="Open" Value="Open"></asp:ListItem>
                                                                                                <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="width: 5%"></td>

                                                                                        <td class="ControlLabelproject1" style="width: 30%">Estimated Effort Duration
                                                                                                 <asp:Label runat="server" ID="estimateheading">
                                                                                                 </asp:Label>

                                                                                        </td>
                                                                                        <td class="ControlTextProject1" style="width: 20%">
                                                                                            <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                    <ContentTemplate>--%>
                                                                                            <cc1:FilteredTextBoxExtender ID="OBvalid" runat="server" FilterType="Numbers" TargetControlID="txtEffortDays" />
                                                                                            <asp:TextBox ID="txtEffortDays" runat="server" Width="100%"
                                                                                                CssClass="cssTextBox"></asp:TextBox>
                                                                                            <%--  </ContentTemplate>
                                                                                                </asp:UpdatePanel>--%>

                                                                                            <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtEffortDays" runat="server" Enabled="false" MaxLength="10" SkinID="skinTxtBoxGrid" Text="0" />
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>--%>
                                                                                        </td>
                                                                                        <td style="width: 5%"></td>


                                                                                    </tr>
                                                                                    <tr style="height: 2px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 20%;" class="ControlLabelproject1">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpIncharge" ErrorMessage="Please select Project Manager. It cannot be left blank." InitialValue="0" Text="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                                            Project Manager *
                                                                                        </td>
                                                                                        <td style="width: 22%;" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpIncharge" TabIndex="4" EnableTheming="False" AppendDataBoundItems="True" CssClass="drpDownListMedium"
                                                                                                runat="server" Width="100%" DataTextField="empFirstName" BackColor="#E7E7E7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                DataValueField="empno">
                                                                                                <asp:ListItem Text="Select Project Manager" Value="0"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="width: 5%"></td>
                                                                                        <td class="ControlLabelproject1" style="width: 30%">
                                                                                            <%-- <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToCompare="txtactdate"
                                                                                                ControlToValidate="txtCDate" Text="*" ErrorMessage="Actual Start Date should be Greater than or equal to Project Record Created Date."
                                                                                                CssClass="lblFont" Operator="LessThanEqual" ValidationGroup="Save" SetFocusOnError="True"
                                                                                                Type="Date"></asp:CompareValidator>--%>
                                                                                           Actual Start Date
                                                                                        </td>

                                                                                        <td style="width: 20%" class="ControlTextProject1">
                                                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtactdate" Enabled="False" Width="100px" MaxLength="10" runat="server" CssClass="cssTextBox" />
                                                                                                    <cc1:CalendarExtender ID="CalAstartDate" runat="server" Format="dd/MM/yyyy"
                                                                                                        PopupButtonID="btnCLDate1" Animated="true" TargetControlID="txtactdate" Enabled="True">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <td style="width: 5%">
                                                                                            <asp:ImageButton ID="btnCLDate1" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="False"
                                                                                                Width="20px" runat="server" TabIndex="10" />
                                                                                        </td>


                                                                                    </tr>
                                                                                    <tr style="height: 2px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabelproject1" style="width: 20%">Project Description </td>
                                                                                        <td class="ControlTextBoxforproject" style="width: 20%">
                                                                                            <asp:TextBox ID="txtProjectDesc" runat="server" BorderWidth="25px" BackColor="#E7E7E7" Height="27px" Style="overflow: hidden; padding: 0px; font-family: 'Trebuchet MS'; border: 1px solid #e7e7e7; font-size: 13px;" TabIndex="5" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 5%"></td>
                                                                                        <td class="ControlLabelproject1" style="width: 30%">
                                                                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToCompare="txtacenddate"
                                                                                                ControlToValidate="txtactdate" Text="*" ErrorMessage=" Date of Completion should be Greater than or equal to Actual Start Date."
                                                                                                CssClass="lblFont" Operator="LessThanEqual" ValidationGroup="Save" SetFocusOnError="True"
                                                                                                Type="Date"></asp:CompareValidator>
                                                                                            <%--  <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToCompare="txtacenddate"
                                                                                                ControlToValidate="txtCDate" Text="*" ErrorMessage="Actual End Date should be Greater than or equal to Project Created Date."
                                                                                                CssClass="lblFont" Operator="LessThanEqual" ValidationGroup="Save" SetFocusOnError="True"
                                                                                                Type="Date"></asp:CompareValidator>--%>

                                                                                            Date of Completion
                                                                                        </td>

                                                                                        <td style="width: 20%" class="ControlTextProject1">
                                                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:TextBox ID="txtacenddate" Enabled="False" Width="100px" MaxLength="10" runat="server" CssClass="cssTextBox" />
                                                                                                    <cc1:CalendarExtender ID="CalEnddate" runat="server" Format="dd/MM/yyyy"
                                                                                                        PopupButtonID="btnCLDate2" TargetControlID="txtacenddate" Enabled="True">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <td style="width: 5%">
                                                                                            <asp:ImageButton ID="btnCLDate2" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="False"
                                                                                                Width="20px" runat="server" TabIndex="11" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 2px">
                                                                                    </tr>
                                                                                    <td class="ControlLabelproject1" style="width: 24%">
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text="*" runat="server"
                                                                                            ControlToValidate="txtCDate" ValidationGroup="Save" ErrorMessage="Creation Date is mandatory"></asp:RequiredFieldValidator>
                                                                                        <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtCDate"
                                                                                                ControlToValidate="txtEWstartDate" Text="*" ErrorMessage="Expected Start Date should be Greater than or equal to Project Created Date."
                                                                                                CssClass="lblFont" Operator="GreaterThanEqual" ValidationGroup="Save" SetFocusOnError="True"
                                                                                                Type="Date"></asp:CompareValidator>--%>
                                                                                            Project Record Created Date *
                                                                                    </td>
                                                                                    <td style="width: 20%" class="ControlTextProject1">
                                                                                        <asp:TextBox ID="txtCDate" Enabled="False" Width="100px" MaxLength="10" runat="server" CssClass="cssTextBox" />
                                                                                        <cc1:CalendarExtender ID="calCDate" runat="server" Format="dd/MM/yyyy"
                                                                                            PopupButtonID="btnCDate" TargetControlID="txtCDate" Enabled="True">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <td style="width: 5%">
                                                                                        <asp:ImageButton ID="btnCDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="False"
                                                                                            Width="20px" runat="server" TabIndex="6" />
                                                                                    </td>
                                                                                    <td class="ControlLabelproject1" style="width: 30%">Unit of Measure * </td>
                                                                                    <td class="ControlDrpBorder" style="width: 20%">
                                                                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList ID="drpunitmeasure" TabIndex="11" Enabled="True" EnableTheming="false" AppendDataBoundItems="true" CssClass="drpDownListMedium"
                                                                                                    runat="server" Width="100%" BackColor="#e7e7e7" Style="border: 1px solid Gray">
                                                                                                    <asp:ListItem Text="Days" Value="Days"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Months" Value="Months"></asp:ListItem>
                                                                                                    <asp:ListItem Text="Hours" Value="Hours"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </ContentTemplate>
                                                                                        </asp:UpdatePanel>
                                                                                        <%-- <asp:DropDownList ID="drpunitmeasure" runat="server" AppendDataBoundItems="True" BackColor="#E7E7E7" CssClass="drpDownListMedium" EnableTheming="False" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="3" Width="100%">
                                                                                                <asp:ListItem Text="Days" Value="Days"></asp:ListItem>
                                                                                                <asp:ListItem Text="Hours" Value="Hours"></asp:ListItem>
                                                                                              <asp:ListItem Text="Month" Value="Month"></asp:ListItem>                                                                                           
                                                                                                </asp:DropDownList>--%>
                                                                                    </td>
                                                                                    <td style="width: 5%"></td>
                                                                                    <tr>
                                                                                    </tr>

                                                                                    <%-- </tr>--%>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                    </cc1:TabContainer>
                                                                    <asp:Panel ID="pnsSave" runat="server" Visible="False">
                                                                        <table style="width: 100%;" cellpadding="1" cellspacing="2">
                                                                            <tr>
                                                                                <td style="width: 30%"></td>
                                                                                <td style="width: 19%" align="right">
                                                                                    <asp:Button ID="btnsavereturn" ValidationGroup="Save" runat="server" CssClass="saveandreturnbuttonforproject"
                                                                                        EnableTheming="false" SkinID="skinBtnSave" OnClick="btnsavereturn_Click" />
                                                                                </td>
                                                                                <td style="width: 8%" align="right">

                                                                                    <asp:Button ID="btnSave" ValidationGroup="Save" runat="server" CssClass="savebuttonforproject"
                                                                                        EnableTheming="false" SkinID="skinBtnSave" OnClick="btnSave_Click" TabIndex="12" />
                                                                                    <asp:Button ID="btnUpdate" runat="server" ValidationGroup="Save" CssClass="Updatebuttonforproject"
                                                                                        EnableTheming="false" SkinID="skinBtnSave" OnClick="btnUpdate_Click" Enabled="false" />
                                                                                </td>
                                                                                <td style="width: 8%" align="left">
                                                                                    <asp:Button ID="btnCancel" runat="server" CssClass="cancelbuttonforproject" EnableTheming="false"
                                                                                        SkinID="skinBtnCancel" OnClick="btnCancel_Click" Enabled="false" TabIndex="13" />
                                                                                </td>
                                                                                <td style="width: 30%"></td>


                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ValidationGroup="Save" ShowSummary="false" HeaderText=" " Font-Names="'Trebuchet MS'"
                                                                        Font-Size="12" runat="server" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                        <table width="100%" style="margin: -2px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div style="margin: -1px 0px 0px 1px;">
                                        <asp:GridView ID="GrdWME" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" Width="100%" CssClass="someClass"
                                            AllowPaging="True" OnPageIndexChanging="GrdWME_PageIndexChanging" OnRowCreated="GrdWME_RowCreated"
                                            DataKeyNames="Project_Id" EmptyDataText="No Project Details found." OnSelectedIndexChanged="GrdWME_SelectedIndexChanged"
                                            OnRowDeleting="GrdWME_RowDeleting" OnRowDataBound="GrdView_RowDataBound">
                                            <HeaderStyle Height="40px" Font-Bold="true" />
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="15px" ForeColor="#0567AE" />
                                            <Columns>
                                              <%--  <asp:BoundField DataField="Row" HeaderText="#" HeaderStyle-Width="30px" />--%>
                                                <asp:BoundField DataField="Project_Id" HeaderText="Project ID" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Green" Visible="false" />
                                                <asp:BoundField DataField="Project_Code" HeaderText="Project ID" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true"  HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Project_Date" HeaderText="Project Record Created Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true"  HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Project_Name" HeaderText="Project Title" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Left" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true"  HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Expected_Start_Date" HeaderText="Expected Start Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true"  HeaderStyle-BorderColor="Gray"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="Expected_End_Date" HeaderText="Due Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true"  HeaderStyle-BorderColor="Gray"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="empfirstname" HeaderText="Project Manager" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Left" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true"  HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Project_Status" HeaderText="Project Status" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="30px" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true"  HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="Gray"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Project Entry?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <asp:HiddenField ID="ProjectID" runat="server" Value='<%# Bind("Project_Id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" Style="border: 1px solid blue" BackColor="#e7e7e7"
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
                            <tr>
                                <td>
                                    <table align="center" style="width: 100%">
                                        <tr>
                                            <td style="width: 35%"></td>

                                            <td style="width: 15%" align="left">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="Buttonsampleproject"
                                                    EnableTheming="false" Text=""></asp:Button>
                                            </td>
                                            <td style="width: 5%" align="left">
                                                <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6forproject" OnClientClick="window.open('ReportExcelProjects.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                                    EnableTheming="false"></asp:Button>
                                            </td>
                                            <td style="width: 40%"></td>

                                        </tr>

                                    </table>
                                </td>
                            </tr>
                        </table>
                        <%-- </div>--%>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
