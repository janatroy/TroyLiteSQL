<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeTimesheetEntry.aspx.cs" Inherits="EmployeeTimesheetEntry" MasterPageFile="~/PageMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <div class="mainConBody">
                            <table style="width: 100%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%"></td>
                                    <td style="width: 16%; font-size: 22px; color: #000000;">Timesheet
                                    </td>
                                    <td style="width: 14%">
                                        <div style="text-align: right;">
                                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAddTimesheet" runat="server" CssClass="ButtonAdd66" CausesValidation="false"
                                                    EnableTheming="false" Width="80px" Text="" OnClick="lnkBtnAddTimesheet_Click"></asp:Button>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                    <td style="width: 13%; color: #000080;" align="right"></td>
                                    <td style="width: 20%; color: #000080;" align="right">Filter by year:
                                        <asp:TextBox ID="txtSearchInput" runat="server" SkinID="skinTxtBoxSearch" Visible="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddlSearchCriteria" runat="server" Width="154px" BackColor="#BBCAFB" Height="23px"
                                                Style="text-align: center; border: 1px solid #BBCAFB" Visible="true" DataTextField="TimesheetYear" DataValueField="TimesheetYear"
                                                OnSelectedIndexChanged="ddlSearchCriteria_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="All" Text="All" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 17%" class="tblLeftNoPad">
                                        <asp:Button ID="btnFilterTimesheet" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" Visible="false" />
                                    </td>

                                    <td style="width: 20%" class="tblLeftNoPad">
                                        <asp:Button ID="btnClearFilter" runat="server" EnableTheming="false" Text="" CssClass="ClearFilter6" Visible="false" />
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
                                        <asp:GridView ID="grdViewTimesheetSummary" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Width="99.9%" AllowPaging="True" DataKeyNames="Id"
                                            EmptyDataText="No Timesheet Data Found." OnRowCommand="grdViewTimesheetSummary_RowCommand" Font-Names="Trebuchet MS" CssClass="someClass">
                                            <Columns>
                                                <asp:BoundField DataField="Id" HeaderText="TimesheetId" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="StartDate" HeaderText="StartDate" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="EndDate" HeaderText="EndDate" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="TimesheetPeroid" HeaderText="Period" Visible="false" />
                                                <asp:BoundField DataField="TotalHours" HeaderText="Total Hours" Visible="false" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="SubmittedDate" HeaderText="Submitted Date" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:BoundField DataField="ApproverUserId" HeaderText="Approver" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="EditRecord" CommandArgument='<%#Eval("Id").ToString()%> ' />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="command" Width="50px"></ItemStyle>
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
                            CancelControlID="btnCancelPopup" Enabled="True" PopupControlID="TimesheetDetailPopUp"
                            TargetControlID="FakeTargetBtn">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="TimesheetDetailPopUp" Style="width:auto; height:60%">
                            <div id="contentPopUp" class="divBody">
                                <table style="width: 100%;height:100%" align="center">
                                    <tr>
                                        <td class="headerPopUp">Enter Timesheet
                                        </td>
                                    </tr>
                                    <caption>
                                        <tr style="width: 100%; height:100%">
                                            <td style="width: 100%">
                                                <asp:UpdatePanel ID="updtPnlTSEntry" runat="server" UpdateMode="Conditional">
                                                    <%-- <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="rbtnTimeEntry1" EventName="OnCheckedChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="rbtnTimeEntry2" EventName="OnCheckedChanged" />
                                                    </Triggers>--%>
                                                    <ContentTemplate>
                                                        <%--<div id="divGridContainer" style="overflow: scroll; width: 100%">--%>
                                                            <table style="margin: 5px;" width="100%">
                                                                <tr style="width: 100%">
                                                                    <td align="center" colspan="7">
                                                                        <asp:Panel ID="pnlTsEntry" runat="server" Visible="true">
                                                                            <asp:HiddenField ID="hdnfTSSummaryId" runat="server" Value="" />
                                                                            <asp:HiddenField ID="hdnfTSDetailId" runat="server" Value="" />
                                                                            <asp:HiddenField ID="hdnfIsNewEntry" runat="server" Value="" />
                                                                            <asp:HiddenField ID="hdnfStatus" runat="server" Value="" />
                                                                            <asp:HiddenField ID="hdnfApproverCmts" runat="server" Value="" />
                                                                            <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Text="" />
                                                                            <table>
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 40%">Date</td>
                                                                                    <td class="ControlNumberBox3" style="width: 30%">
                                                                                        <asp:Panel ID="Panel1" runat="server">
                                                                                            <asp:DropDownList ID="ddlTSDate" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid blue" TabIndex="3" Width="100%">
                                                                                            </asp:DropDownList>                                                                                            
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                    <td style="width: 25%"></td>
                                                                                    <td style="width: 5%"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 40%">
                                                                                        <asp:RadioButton ID="rbtnTimeEntry1" runat="server" AutoPostBack="true" CausesValidation="false" Checked="true" GroupName="TimeEntryOption" OnCheckedChanged="rbtnTimeEntry_CheckedChanged" Text="Start Time" ToolTip="Enter by Start time and End time" />
                                                                                    </td>
                                                                                    <td class="ControlNumberBox3" style="width: 30%">
                                                                                        <asp:Panel ID="pnlStartTImeControls" runat="server">
                                                                                            <asp:DropDownList ID="ddlStartTimeHour" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid blue" TabIndex="3" Width="55px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:DropDownList ID="ddlStartTimeMinute" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid blue" TabIndex="3" Width="55px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:DropDownList ID="ddlStartTimeMeridian" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid blue" TabIndex="3" Width="55px">
                                                                                                <asp:ListItem Selected="True" Text="AM" Value="AM"></asp:ListItem>
                                                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                    <td style="width: 25%"></td>
                                                                                    <td style="width: 5%"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 40%">End Time </td>
                                                                                    <td class="ControlNumberBox3" style="width: 30%">
                                                                                        <asp:Panel ID="pnlEndTimeControls" runat="server">
                                                                                            <asp:DropDownList ID="ddlEndTimeHour" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid blue" TabIndex="3" Width="55px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:DropDownList ID="ddlEndTimeMinute" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid blue" TabIndex="3" Width="55px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:DropDownList ID="ddlEndTimeMeridian" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid blue" TabIndex="3" Width="55px">
                                                                                                <asp:ListItem Selected="True" Text="AM" Value="AM"></asp:ListItem>
                                                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                    <td style="width: 25%"></td>
                                                                                    <td style="width: 5%"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 40%">
                                                                                        <asp:RadioButton ID="rbtnTimeEntry2" runat="server" AutoPostBack="true" CausesValidation="false" Checked="false" GroupName="TimeEntryOption" OnCheckedChanged="rbtnTimeEntry_CheckedChanged" Text="Hours" ToolTip="Enter by hours" />
                                                                                    </td>
                                                                                    <td class="ControlNumberBox3" style="width: 30%">
                                                                                        <asp:Panel ID="pnlTotalTimeControls" runat="server" Enabled="false">
                                                                                            <asp:DropDownList ID="ddlTotalTimeHour" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid blue" TabIndex="3" Width="48%">
                                                                                            </asp:DropDownList>
                                                                                            <asp:DropDownList ID="ddlTotalTimeMinute" runat="server" BackColor="#90c9fc" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid blue" TabIndex="3" Width="48%">
                                                                                            </asp:DropDownList>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                    <td style="width: 25%"></td>
                                                                                    <td style="width: 5%"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel" style="width: 40%;">Description *
                                                                                    <asp:RequiredFieldValidator ID="rfvdescription" runat="server" ControlToValidate="txtdescription" Display="Dynamic" EnableClientScript="True" ErrorMessage="Description is mandatory">*</asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td class="ControlTextBox3" style="width: 30%">
                                                                                        <asp:TextBox ID="txtDescription" runat="server" Height="" SkinID="skinTxtBoxGrid" TabIndex="4" MaxLength="250" TextMode="MultiLine"></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 25%"></td>
                                                                                    <td style="width: 5%"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" style="width: 40%"></td>
                                                                                    <td align="justify" style="width: 30%">
                                                                                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" UseSubmitBehavior="false" Width="75px" CausesValidation="false" />
                                                                                        <asp:Button ID="btnAddEntry" runat="server" OnClick="btnAddEntry_Click" Text="Add" UseSubmitBehavior="false" Width="75px" />
                                                                                    </td>
                                                                                    <td style="width: 25%"></td>
                                                                                    <td style="width: 5%"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr style="width: 100%">
                                                                    <td style="width: 14%; vertical-align:top">
                                                                        <%--<div id="TimesheetDetailGridMonday" class="mainGridHoldPopup">--%>
                                                                            <asp:LinkButton ID="lblMondayHeader" runat="server" Width="100%" Text=""></asp:LinkButton>                                                                            
                                                                            <asp:GridView ID="GridViewTimesheetDetailMonday" runat="server" AllowPaging="false" AutoGenerateColumns="false" CssClass="someClass" DataKeyNames="Id"
                                                                                EmptyDataText="No entries found." Font-Names="Trebuchet MS" OnRowDataBound="GridViewTimesheetDetailMonday_RowDataBound" Visible="false" Width="200px"
                                                                                OnRowCommand="GridViewTimesheetDetailMonday_RowCommand">
                                                                                <Columns>
                                                                                    <asp:BoundField AccessibleHeaderText="Id" DataField="Id" HeaderText="Id" ReadOnly="true" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="TsDate" DataField="TsDate" HeaderText="TsDate" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="StartTime" DataField="StartTime" HeaderText="StartTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="EndTime" DataField="EndTime" HeaderText="EndTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="Description" DataField="Description" HeaderText="Description" ItemStyle-Wrap="false" ItemStyle-Width="100px" ReadOnly="true" />
                                                                                    <asp:BoundField AccessibleHeaderText="TotalHours" DataField="TotalHours" HeaderText="Total Hours" />
                                                                                    <asp:BoundField AccessibleHeaderText="Status" DataField="Status" HeaderText="Status" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="ApproverComments" DataField="ApproverComments" HeaderText="ApproverComments" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="IsActive" DataField="IsActive" HeaderText="IsActive" Visible="false" />
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Edit" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="EditRecord" SkinID="edit" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnEditDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Delete" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="DeleteRecord" SkinID="delete" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnDeleteDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        <%--</div>--%>
                                                                    </td>
                                                                    <%--Day 2--%>
                                                                    <td style="width: 14%; vertical-align:top">
                                                                        <%--<div id="TimesheetDetailGridTuesday" class="mainGridHoldPopup">--%>
                                                                            <asp:LinkButton ID="lblTuesdayHeader" runat="server" Width="100%" Text=""></asp:LinkButton>
                                                                            <asp:GridView ID="GridViewTimesheetDetailTuesday" runat="server" AllowPaging="false" AutoGenerateColumns="false" CssClass="someClass" DataKeyNames="Id"
                                                                                EmptyDataText="No entries found." Font-Names="Trebuchet MS" OnRowDataBound="GridViewTimesheetDetailMonday_RowDataBound" Visible="false" Width="200px"
                                                                                OnRowCommand="GridViewTimesheetDetailMonday_RowCommand">
                                                                                <Columns>
                                                                                    <asp:BoundField AccessibleHeaderText="Id" DataField="Id" HeaderText="Id" ReadOnly="true" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="TsDate" DataField="TsDate" HeaderText="TsDate" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="StartTime" DataField="StartTime" HeaderText="StartTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="EndTime" DataField="EndTime" HeaderText="EndTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="Description" DataField="Description" HeaderText="Description" ItemStyle-Wrap="false" ItemStyle-Width="100px" ReadOnly="true" />
                                                                                    <asp:BoundField AccessibleHeaderText="TotalHours" DataField="TotalHours" HeaderText="Total Hours" />
                                                                                    <asp:BoundField AccessibleHeaderText="Status" DataField="Status" HeaderText="Status" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="ApproverComments" DataField="ApproverComments" HeaderText="ApproverComments" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="IsActive" DataField="IsActive" HeaderText="IsActive" Visible="false" />
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Edit" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="EditRecord" SkinID="edit" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnEditDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Delete" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="DeleteRecord" SkinID="delete" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnDeleteDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        <%--</div>--%>
                                                                    </td>
                                                                    <%--Day 3--%>
                                                                    <td style="width: 14%; vertical-align:top">
                                                                        <%--<div id="TimesheetDetailGridWednesday" class="mainGridHoldPopup">--%>
                                                                            <asp:LinkButton ID="lblWednesdayHeader" runat="server" Width="100%" Text=""></asp:LinkButton>
                                                                            <asp:GridView ID="GridViewTimesheetDetailWednesday" runat="server" AllowPaging="false" AutoGenerateColumns="false" CssClass="someClass" DataKeyNames="Id"
                                                                                EmptyDataText="No entries found." Font-Names="Trebuchet MS" OnRowDataBound="GridViewTimesheetDetailMonday_RowDataBound" Visible="false" Width="200px"
                                                                                OnRowCommand="GridViewTimesheetDetailMonday_RowCommand">
                                                                                <Columns>
                                                                                    <asp:BoundField AccessibleHeaderText="Id" DataField="Id" HeaderText="Id" ReadOnly="true" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="TsDate" DataField="TsDate" HeaderText="TsDate" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="StartTime" DataField="StartTime" HeaderText="StartTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="EndTime" DataField="EndTime" HeaderText="EndTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="Description" DataField="Description" HeaderText="Description" ItemStyle-Wrap="false" ItemStyle-Width="100px" ReadOnly="true" />
                                                                                    <asp:BoundField AccessibleHeaderText="TotalHours" DataField="TotalHours" HeaderText="Total Hours" />
                                                                                    <asp:BoundField AccessibleHeaderText="Status" DataField="Status" HeaderText="Status" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="ApproverComments" DataField="ApproverComments" HeaderText="ApproverComments" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="IsActive" DataField="IsActive" HeaderText="IsActive" Visible="false" />
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Edit" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="EditRecord" SkinID="edit" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnEditDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Delete" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="DeleteRecord" SkinID="delete" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnDeleteDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        <%--</div>--%>
                                                                    </td>
                                                                    <%--Day 4--%>
                                                                    <td style="width: 14%; vertical-align:top" >
                                                                        <%--<div id="TimesheetDetailGridThursday" class="mainGridHoldPopup">--%>
                                                                            <asp:LinkButton ID="lblThursdayHeader" runat="server" Width="100%" Text=""></asp:LinkButton>
                                                                            <asp:GridView ID="GridViewTimesheetDetailThursday" runat="server" AllowPaging="false" AutoGenerateColumns="false" CssClass="someClass" DataKeyNames="Id"
                                                                                EmptyDataText="No entries found." Font-Names="Trebuchet MS" OnRowDataBound="GridViewTimesheetDetailMonday_RowDataBound" Visible="false" Width="200px"
                                                                                OnRowCommand="GridViewTimesheetDetailMonday_RowCommand">
                                                                                <Columns>
                                                                                    <asp:BoundField AccessibleHeaderText="Id" DataField="Id" HeaderText="Id" ReadOnly="true" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="TsDate" DataField="TsDate" HeaderText="TsDate" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="StartTime" DataField="StartTime" HeaderText="StartTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="EndTime" DataField="EndTime" HeaderText="EndTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="Description" DataField="Description" HeaderText="Description" ItemStyle-Wrap="false" ItemStyle-Width="100px" ReadOnly="true" />
                                                                                    <asp:BoundField AccessibleHeaderText="TotalHours" DataField="TotalHours" HeaderText="Total Hours" />
                                                                                    <asp:BoundField AccessibleHeaderText="Status" DataField="Status" HeaderText="Status" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="ApproverComments" DataField="ApproverComments" HeaderText="ApproverComments" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="IsActive" DataField="IsActive" HeaderText="IsActive" Visible="false" />
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Edit" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="EditRecord" SkinID="edit" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnEditDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Delete" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="DeleteRecord" SkinID="delete" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnDeleteDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        <%--</div>--%>
                                                                    </td>
                                                                    <%--Day 5--%>
                                                                    <td style="width: 14%; vertical-align:top" >
                                                                        <%--<div id="TimesheetDetailGridFriday" class=mainGridHoldPopup">--%>
                                                                            <asp:LinkButton ID="lblFridayHeader" runat="server" Width="100%" Text=""></asp:LinkButton>
                                                                            <asp:GridView ID="GridViewTimesheetDetailFriday" runat="server" AllowPaging="false" AutoGenerateColumns="false" CssClass="someClass" DataKeyNames="Id"
                                                                                EmptyDataText="No entries found." Font-Names="Trebuchet MS" OnRowDataBound="GridViewTimesheetDetailMonday_RowDataBound" Visible="false" Width="200px"
                                                                                OnRowCommand="GridViewTimesheetDetailMonday_RowCommand">
                                                                                <Columns>
                                                                                    <asp:BoundField AccessibleHeaderText="Id" DataField="Id" HeaderText="Id" ReadOnly="true" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="TsDate" DataField="TsDate" HeaderText="TsDate" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="StartTime" DataField="StartTime" HeaderText="StartTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="EndTime" DataField="EndTime" HeaderText="EndTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="Description" DataField="Description" HeaderText="Description" ItemStyle-Wrap="false" ItemStyle-Width="100px" ReadOnly="true" />
                                                                                    <asp:BoundField AccessibleHeaderText="TotalHours" DataField="TotalHours" HeaderText="Total Hours" />
                                                                                    <asp:BoundField AccessibleHeaderText="Status" DataField="Status" HeaderText="Status" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="ApproverComments" DataField="ApproverComments" HeaderText="ApproverComments" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="IsActive" DataField="IsActive" HeaderText="IsActive" Visible="false" />
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Edit" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="EditRecord" SkinID="edit" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnEditDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Delete" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="DeleteRecord" SkinID="delete" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnDeleteDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        <%--</div>--%>
                                                                    </td>
                                                                    <%--Day 6--%>
                                                                    <td style="width: 14%; vertical-align:top" >
                                                                        <%--<div id="TimesheetDetailGridSaturday" class="mainGridHoldPopup">--%>
                                                                            <asp:LinkButton ID="lblSaturdayHeader" runat="server" Width="100%" Text=""></asp:LinkButton>
                                                                            <asp:GridView ID="GridViewTimesheetDetailSaturday" runat="server" AllowPaging="false" AutoGenerateColumns="false" CssClass="someClass" DataKeyNames="Id"
                                                                                EmptyDataText="No entries found." Font-Names="Trebuchet MS" OnRowDataBound="GridViewTimesheetDetailMonday_RowDataBound" Visible="false" Width="200px"
                                                                                OnRowCommand="GridViewTimesheetDetailMonday_RowCommand">
                                                                                <Columns>
                                                                                    <asp:BoundField AccessibleHeaderText="Id" DataField="Id" HeaderText="Id" ReadOnly="true" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="TsDate" DataField="TsDate" HeaderText="TsDate" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="StartTime" DataField="StartTime" HeaderText="StartTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="EndTime" DataField="EndTime" HeaderText="EndTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="Description" DataField="Description" HeaderText="Description" ItemStyle-Wrap="false" ItemStyle-Width="100px" ReadOnly="true" />
                                                                                    <asp:BoundField AccessibleHeaderText="TotalHours" DataField="TotalHours" HeaderText="Total Hours" />
                                                                                    <asp:BoundField AccessibleHeaderText="Status" DataField="Status" HeaderText="Status" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="ApproverComments" DataField="ApproverComments" HeaderText="ApproverComments" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="IsActive" DataField="IsActive" HeaderText="IsActive" Visible="false" />
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Edit" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="EditRecord" SkinID="edit" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnEditDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Delete" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="DeleteRecord" SkinID="delete" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnDeleteDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        <%--</div>--%>
                                                                    </td>
                                                                    <%--Day 7--%>
                                                                    <td style="width: 14%; vertical-align:top" >
                                                                        <%--<div id="TimesheetDetailGridSunday" class="mainGridHoldPopup">--%>
                                                                            <asp:LinkButton ID="lblSundayHeader" runat="server" Width="100%" Text=""></asp:LinkButton>
                                                                            <asp:GridView ID="GridViewTimesheetDetailSunday" runat="server" AllowPaging="false" AutoGenerateColumns="false" CssClass="someClass" DataKeyNames="Id"
                                                                                EmptyDataText="No entries found." Font-Names="Trebuchet MS" OnRowDataBound="GridViewTimesheetDetailMonday_RowDataBound" Visible="false" Width="200px"
                                                                                OnRowCommand="GridViewTimesheetDetailMonday_RowCommand">
                                                                                <Columns>
                                                                                    <asp:BoundField AccessibleHeaderText="Id" DataField="Id" HeaderText="Id" ReadOnly="true" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="TsDate" DataField="TsDate" HeaderText="TsDate" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="StartTime" DataField="StartTime" HeaderText="StartTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="EndTime" DataField="EndTime" HeaderText="EndTime" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="Description" DataField="Description" HeaderText="Description" ItemStyle-Wrap="true" ItemStyle-Width="100px" ReadOnly="true" />
                                                                                    <asp:BoundField AccessibleHeaderText="TotalHours" DataField="TotalHours" HeaderText="Total Hours" />
                                                                                    <asp:BoundField AccessibleHeaderText="Status" DataField="Status" HeaderText="Status" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="ApproverComments" DataField="ApproverComments" HeaderText="ApproverComments" Visible="false" />
                                                                                    <asp:BoundField AccessibleHeaderText="IsActive" DataField="IsActive" HeaderText="IsActive" Visible="false" />
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Edit" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="EditRecord" SkinID="edit" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnEditDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderStyle-BorderColor="Gray" HeaderText="Delete" ItemStyle-CssClass="command" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%#Eval("Id").ToString()%> ' CommandName="DeleteRecord" SkinID="delete" CausesValidation="false" />
                                                                                            <asp:ImageButton ID="btnDeleteDisabled" runat="Server" Enabled="false" SkinID="editDisable" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle CssClass="command" Width="50px" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        <%--</div>--%>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left; height: 25px; color: red;">
                                                                        <asp:Label ID="lblUserHint" runat="server" ForeColor="Red" Text="Entries highlighted in Red are rejected by the supervisor.">

                                                                        </asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        <%--</div>--%>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table align="left" width="100%">
                                                    <tr>
                                                        <td align="right" style="width: 20%;"></td>
                                                        <td style="width: 20%;">
                                                            <asp:Button ID="btnCancelPopup" runat="server" CausesValidation="False" CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" />
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:Button ID="btnSaveTimesheet" runat="server" CausesValidation="True" CssClass="savebutton1231" EnableTheming="false" OnClick="btnSaveTimesheet_Click" SkinID="skinBtnSave" />
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:Button ID="btnSubmitTimesheet" runat="server" CausesValidation="True" CssClass="AddGetRefNos6" EnableTheming="false" OnClick="btnSaveTimesheet_Click" SkinID="skinBtnSave" Visible="true" />
                                                        </td>
                                                        <td style="width: 20%;"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </caption>

                                </table>
                            </div>
                        </asp:Panel>

                    </td>
                </tr>
                <tr>
                    <td>
                        <input id="btnFakeModalTarget" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="CompOffModalPopupExtender" runat="server" PopupControlID="pnlCompOffContentPopUp"
                            BackgroundCssClass="modalBackground" TargetControlID="btnFakeModalTarget" CancelControlID="btnCancelCompOff">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="pnlCompOffContentPopUp">
                            <div id="CompOffContentPopUp" class="divBody" style="border-color: green; border-width: 2px; background-color: silver">

                                <table style="width: 770px; vertical-align: text-top; border: 0px solid #86b2d1;"
                                    align="center" cellspacing="2" cellpadding="3">
                                    <tr>
                                        <td class="headerPopUp">Rota/Comp-Off Approval
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="updPnlRotaCompOff" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td style="width: 100%"></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>--%>
                                            <table width="100%" align="left">
                                                <tr>
                                                    <td align="right" style="width: 20%;"></td>
                                                    <td style="width: 20%;">
                                                        <asp:Button ID="btnCancelCompOff" runat="server" CausesValidation="False"
                                                            Text="Cancel" EnableTheming="false"></asp:Button>
                                                    </td>
                                                    <td style="width: 20%;">
                                                        <asp:Button ID="btnApproveCompOff" runat="server" CausesValidation="True"
                                                            Text="Approve & Save" ToolTip="Approve and save Timesheet." EnableTheming="false"></asp:Button>
                                                    </td>
                                                    <td style="width: 20%;" colspan="2"></td>
                                                </tr>
                                            </table>
                                            <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </td>
                                    </tr>
                                </table>


                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
