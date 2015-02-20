﻿<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="HRAdminHoliday.aspx.cs" Inherits="Holiday_HRAdminHoliday" Title="Human Resources > Admin Holiday" %>

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
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">

        <ContentTemplate>

            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">

                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%"></td>
                                    <td style="width: 16%; font-size: 22px; color: white;">Holiday List
                                    </td>
                                    <td style="width: 14%">
                                        <div style="text-align: right;">
                                         
                                        </div>
                                    </td>
                                    <td style="width: 13%; color: white;" align="right">Search
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <asp:TextBox ID="txtSearchInput" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddlSearchCriteria" runat="server" Width="150px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid white">
                                                <asp:ListItem Value="All">All</asp:ListItem>
                                                <asp:ListItem Value="HolidayName">Holiday Name</asp:ListItem>
                                                <asp:ListItem Value="Date">Date</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 17%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" />
                                    </td>

                                    <td style="width: 20%" class="tblLeftNoPad">
                                        <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="grdViewHolidaySummary" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Width="100.4%" DataSourceID="HolidaySummaryGridSource" AllowPaging="True" DataKeyNames="Holiday_ID"
                                            OnRowCommand="grdViewHolidaySummary_RowCommand" OnRowDataBound="grdViewHolidaySummary_RowDataBound"
                                            OnRowDeleted="grdViewHolidaySummary_RowDeleted" OnRowDeleting="grdViewHolidaySummary_RowDeleting"
                                            EmptyDataText="No Holiday List Found." Font-Names="Trebuchet MS" CssClass="someClass">
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Holiday_Name" HeaderText="Holiday Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" Visible="true" HeaderStyle-BorderColor="Gray" />

                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="command" Width="50px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Holiday Details?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" Enabled="false" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("Holiday_ID") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px"></HeaderStyle>
                                                    <ItemStyle CssClass="command"></ItemStyle>
                                                </asp:TemplateField>
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
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>
                        <input id="FakeCancelBtn" type="button" style="display: none" runat="server" />
                        <input id="FakeTargetBtn" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="FakeCancelBtn" DynamicServicePath="" Enabled="True" PopupControlID="HolidayDetailPopUp"
                            TargetControlID="FakeTargetBtn">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="HolidayDetailPopUp" Style="width: 57%">
                            <div id="contentPopUp">
                                <table style="width: 100%;" align="center">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">
                                            <asp:FormView ID="frmHolidayAdd" runat="server" Width="100%" DataSourceID="frmHolidaySource"
                                                DataKeyNames="Holiday_ID" OnItemCommand="frmHolidayAdd_ItemCommand" DefaultMode="Edit"
                                                OnItemCreated="frmHolidayAdd_ItemCreated" Visible="False"
                                                OnItemUpdating="frmHolidayAdd_ItemUpdating" EmptyDataText="No Records" OnItemInserted="frmHolidayAdd_ItemInserted"
                                                OnItemUpdated="frmHolidayAdd_ItemUpdated">
                                                <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"
                                                    BorderColor="#cccccc" Height="20px" />
                                                <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc"
                                                    VerticalAlign="middle" Height="20px" />
                                                <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad"
                                                    Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                                                <EditItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="5" class="headerPopUp">Holiday Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" align="left">

                                                                    <cc1:TabContainer ID="tabEdit" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabEditMain" runat="server" HeaderText="Holiday Entry">
                                                                            <ContentTemplate>

                                                                                <table style="width: 770px; vertical-align: text-top; border: 0px solid #86b2d1;"
                                                                                    align="center" cellspacing="2" cellpadding="3">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Date:
                                                                    <asp:RequiredFieldValidator ID="rvHolidayDateEdit" runat="server" ControlToValidate="txtHolidayDateEdit"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Date is mandatory"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 20%">
                                                                                            <asp:TextBox ID="txtHolidayDateEdit" Enabled="false" runat="server" Text='<%# Bind("Date") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calEditDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                PopupButtonID="btnEditDate" PopupPosition="BottomLeft" TargetControlID="txtHolidayDateEdit">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                        <td style="width: 30%" align="left">
                                                                                            <asp:ImageButton ID="btnEditDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                Width="20px" runat="server" />
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 14%">Holiday Name :
                                                                    <asp:RequiredFieldValidator ID="ReqHolidayName" runat="server" ControlToValidate="txtHolidayNameEdit"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Name is mandatory"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtHolidayNameEdit" runat="server" Text='<%# Bind("Holiday_Name") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 14%">Remarks :
                                                                    
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">

                                                                                            <asp:TextBox ID="txtHolidayRemarksEdit" runat="server" Text='<%# Bind("Remarks") %>'
                                                                                                Width="300px" Height="46px" SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
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
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 32%;"></td>
                                                                            <td style="width: 19%;">
                                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                    OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 19%;">
                                                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 30%;"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcGroupInfo" runat="server" SelectMethod="ListGroupInfoSupp"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                                <td>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ShowSummary="false" HeaderText="Validation Messages" Font-Names="Trebuchet MS"
                                                                        Font-Size="12" runat="server" />
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <InsertItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="5" class="headerPopUp">Holiday Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" align="left">
                                                                    <cc1:TabContainer ID="tablInsert" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabInsMain" runat="server" HeaderText="Holiday Entry">
                                                                            <ContentTemplate>
                                                                                <table style="width: 770px; border: 0px solid #86b2d1; vertical-align: text-top" align="center" cellpadding="3"
                                                                                    cellspacing="1">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Date :
                                                                    <asp:RequiredFieldValidator ID="rvHolidayDateAdd" runat="server" ControlToValidate="txtHolidayDateAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Date is mandatory"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 20%">
                                                                                            <asp:TextBox ID="txtHolidayDateAdd" Enabled="false" runat="server" Text='<%# Bind("Date") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calAddDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                PopupButtonID="btnAddDate" PopupPosition="BottomLeft" TargetControlID="txtHolidayDateAdd">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                        <td style="width: 30%" align="left">
                                                                                            <asp:ImageButton ID="btnAddDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                Width="20px" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 14%">Holiday Name :
                                                                    <asp:RequiredFieldValidator ID="rvHolidayNameAdd" runat="server" ControlToValidate="txtHolidayNameAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Alias is mandatory">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtHolidayNameAdd" runat="server" Text='<%# Bind("Holiday_Name") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 14%">Remarks :
                                                                    <%--<asp:RequiredFieldValidator ID="rvAliasNameAdd" runat="server" ControlToValidate="txtAliasNameAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Alias is mandatory">*</asp:RequiredFieldValidator>--%>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">

                                                                                            <asp:TextBox ID="txtRemarksAdd" runat="server" Text='<%# Bind("Remarks") %>'
                                                                                                Width="300px" Height="46px" SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
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
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 33%;"></td>
                                                                            <td style="width: 19%;">
                                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                    OnClick="InsertButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 19%;">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 29%;"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <%--<tr style="height:2px">
                                                            </tr>--%>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td style="width: 50%" colspan="2">
                                                                <asp:ValidationSummary ID="valSumAdd" DisplayMode="BulletList" ShowMessageBox="true"
                                                                    ShowSummary="false" HeaderText="Validation Messages" Font-Names="Trebuchet MS"
                                                                    Font-Size="12" runat="server" />
                                                            </td>
                                                            <td style="width: 50%" colspan="2">
                                                                <%--<asp:ObjectDataSource ID="srcGroupInfoAdd" runat="server" SelectMethod="ListGroupInfoSupp"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>

                    </td>
                </tr>

                <tr style="width: 100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="HolidaySummaryGridSource" runat="server" SelectMethod="GetHolidaySummary"
                            OnDeleting="HolidaySummaryGridSource_Deleting" DeleteMethod="DeleteHolidayInfo" TypeName="BusinessLogic">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="Holiday_ID" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>

                        <asp:ObjectDataSource ID="frmHolidaySource" runat="server" SelectMethod="GetHolidayInfoByID"
                            TypeName="BusinessLogic" OnUpdating="frmHolidaySource_Updating" OnInserting="frmHolidaySource_Inserting"
                            InsertMethod="InsertHolidayInfo" UpdateMethod="UpdateHolidayInfo">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="Holiday_ID" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Date" Type="DateTime" />
                                <asp:Parameter Name="Holiday_Name" Type="String" />
                                <asp:Parameter Name="Remarks" Type="String" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="grdViewHolidaySummary" Name="Holiday_ID" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Date" Type="DateTime" />
                                <asp:Parameter Name="Holiday_Name" Type="String" />
                                <asp:Parameter Name="Remarks" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>

                    </td>
                </tr>

            </table>
            <table width="100%">
                <tr>
                    <td align="center">
                           <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAddHoliday" runat="server" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text="" OnClick="lnkBtnAddHoliday_Click"></asp:Button>
                                            </asp:Panel>
                    </td>

                </tr>
            </table>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
