<%@ Page Title="" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="EmployeeLeave.aspx.cs" Inherits="EmployeeLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script type="text/javascript">
       
    </script>
    <asp:UpdatePanel ID="UpdatePanelMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <div class="mainConBody">
                            <table style="width: 100%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%"></td>
                                    <td style="width: 16%; font-size: 22px; color: #000000;">Leave
                                    </td>
                                    <td style="width: 14%">
                                        <div style="text-align: right;">
                                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnApplyLeave" runat="server" CssClass="ButtonAdd66" CausesValidation="false"
                                                    EnableTheming="false" Width="80px" Text="" OnClick="lnkBtnApplyLeave_Click" ToolTip="Click to apply leave"></asp:Button>
                                            </asp:Panel>
                                        </div>
                                    </td>
                                    <td style="width: 13%; color: #000080;" align="right">Search
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtSearchInput" runat="server" Visible="true"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddlSearchCriteria" runat="server" Width="154px" BackColor="#BBCAFB" Height="23px" Style="text-align: center; border: 1px solid #BBCAFB" Visible="true">
                                                <asp:ListItem Value="DateApplied" Text="Date Applied" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="LeaveTypeName" Text="Leave Type" ></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 17%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearchAttendance" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" Visible="true" OnClick="btnSearchAttendance_Click" CausesValidation="false" />
                                    </td>

                                    <td style="width: 20%" class="tblLeftNoPad">
                                        <asp:Button ID="btnClearFilter" runat="server" EnableTheming="false" Text="" CssClass="ClearFilter6" Visible="true" OnClick="btnClearFilter_Click" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr style="width: 100%;">
                    <td style="width: 100%;">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="grdViewLeaveSummary" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Width="99.9%" AllowPaging="True" DataKeyNames="LeaveId"
                                            EmptyDataText="No Leaves Found." OnRowCommand="grdViewLeaveSummary_RowCommand" Font-Names="Trebuchet MS" CssClass="someClass">
                                            <Columns>
                                                <asp:BoundField DataField="LeaveId" HeaderText="LeaveId" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="DateRange" HeaderText="Date Range" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="DateApplied" HeaderText="Date Applied" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:BoundField DataField="ApproverName" HeaderText="Approver" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="ApproverComments" HeaderText="Approver Comments" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditLeave" runat="server" SkinID="edit" CommandName="EditLeave" CausesValidation="false" CommandArgument='<%#Eval("LeaveId")%>' />
                                                        <asp:ImageButton ID="btnEditLeaveDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="command" Width="50px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Cancel">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnCancelLeave" runat="server" SkinID="delete" CommandName="CancelLeave" CommandArgument='<%#Eval("LeaveId")%>' CausesValidation="false" />
                                                        <asp:ImageButton ID="btnCancelLeaveDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="btnCancelLeave" ConfirmText="Are you sure to Cancel this Leave ?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
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
                            CancelControlID="FakeCancelBtn" Enabled="True" PopupControlID="LeaveDetailPopUp"
                            TargetControlID="FakeTargetBtn">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="LeaveDetailPopUp" Style="width: 50%" Visible="true">
                            <div id="contentPopUp">
                                <table style="width: 100%;" align="center">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">

                                            <div class="divArea">
                                                <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                    <tr>
                                                        <td colspan="3" class="headerPopUp">Apply Leave
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 5px">
                                                        <asp:HiddenField ID="hdfLeaveId" runat="server" />
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Approver                                                                    
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%" align="left">
                                                            <asp:Label ID="lblApproverName" runat="server"
                                                                TabIndex="1"></asp:Label>
                                                            <asp:HiddenField ID="hdfApproverEmpNo" runat="server" />
                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Leave Type *
                                                                    
                                                                    <asp:RequiredFieldValidator ID="rfvLeaveType" runat="server" ControlToValidate="ddlLeaveType"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Leave Type is mandatory">*</asp:RequiredFieldValidator>

                                                        </td>
                                                        <td style="width: 30%">

                                                            <asp:DropDownList ID="ddlLeaveType" runat="server" Width="100%" BackColor="#90c9fc" Style="border: 1px solid blue" Height="26px" CssClass="drpDownListMedium"
                                                                TabIndex="3" DataSourceID="dataSrcLeaveTypes" AutoPostBack="true"
                                                                DataTextField="LeaveTypeName" DataValueField="LeaveTypeId" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 30%" class="ControlLabel" align="left"></td>

                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Balance Leave(s)*                                                                                                                                   
                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:TextBox ID="txtBalanceLeaves" runat="server" ReadOnly="true"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4" Text="0"></asp:TextBox>
                                                            <asp:RangeValidator ID="rngValidatorBalanceLeave" runat="server" Display="Dynamic"
                                                                ControlToValidate="txtBalanceLeaves" MinimumValue="1" MaximumValue="999999"
                                                                ErrorMessage="Balance leave(s) should not be zero"></asp:RangeValidator>
                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Start Date
                                                                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Start Date is mandatory">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="ControlNumberBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtStartDate" runat="server"
                                                                SkinID="skinTxtBox" TabIndex="4" OnTextChanged="txtEndDate_TextChanged" AutoPostBack="true" CausesValidation="false"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calExtenderStartDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                PopupButtonID="btnStartDate" PopupPosition="BottomLeft" TargetControlID="txtStartDate">
                                                            </cc1:CalendarExtender>

                                                            <asp:DropDownList ID="ddlStartDateSession" runat="server" Width="55px" BackColor="#90c9fc" Style="border: 1px solid blue"
                                                                Height="26px" CssClass="drpDownListMedium" OnSelectedIndexChanged="ddlStartDateSession_SelectedIndexChanged"
                                                                TabIndex="3" AutoPostBack="true" CausesValidation="false">
                                                                <asp:ListItem Text="FN" Value="FN" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="AN" Value="AN"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                        <td style="width: 30%" align="left">
                                                            <asp:ImageButton ID="btnStartDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                Width="20px" runat="server" />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">End Date
                                                                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="End Date is mandatory">*</asp:RequiredFieldValidator>

                                                            <asp:CompareValidator ID="cvDateRange" runat="server" ControlToValidate="txtEndDate"
                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="End date should not be less than Start date."
                                                                Operator="GreaterThanEqual" ControlToCompare="txtStartDate" Type="Date">*</asp:CompareValidator>
                                                        </td>
                                                        <td class="ControlNumberBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtEndDate" runat="server" SkinID="skinTxtBoxGrid" TabIndex="4" OnTextChanged="txtEndDate_TextChanged" AutoPostBack="true" CausesValidation="false"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                PopupButtonID="btnEndDate" PopupPosition="BottomLeft" TargetControlID="txtEndDate">
                                                            </cc1:CalendarExtender>
                                                            <asp:DropDownList ID="ddlEndDateSession" runat="server" Width="55px" BackColor="#90c9fc" Style="border: 1px solid blue" Height="26px" CssClass="drpDownListMedium"
                                                                TabIndex="3" OnSelectedIndexChanged="ddlStartDateSession_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false">
                                                                <asp:ListItem Text="FN" Value="FN"></asp:ListItem>
                                                                <asp:ListItem Text="AN" Value="AN" Selected="True"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                        <td style="width: 30%" align="left">
                                                            <asp:ImageButton ID="btnEndDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                Width="20px" runat="server" />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Total Leave Day(s)*
                                                                     <asp:RequiredFieldValidator ID="rfvTotalLeaves" runat="server" ControlToValidate="txtTotalLeaveDays"
                                                                         Display="Dynamic" EnableClientScript="True" ErrorMessage="Calculate total leave days.">*</asp:RequiredFieldValidator>

                                                            <asp:CompareValidator ID="cvLeaveLimitExceeds" runat="server" ControlToValidate="txtTotalLeaveDays"
                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Total leave days should be less than or equal to balance leaves."
                                                                Operator="LessThanEqual" ControlToCompare="txtBalanceLeaves" Type="Double">*</asp:CompareValidator>

                                                        </td>
                                                        <td style="width: 30%">
                                                            <asp:RangeValidator ID="rngValidatorTotalLeaves" runat="server" Display="Dynamic"
                                                                ControlToValidate="txtTotalLeaveDays" MinimumValue="1" MaximumValue="999999"
                                                                ErrorMessage="Total leave(s) should not be zero"></asp:RangeValidator>
                                                            <asp:TextBox ID="txtTotalLeaveDays" runat="server" ReadOnly="true"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30%" align="left">
                                                            <asp:Button ID="btnCalculateTotalLeaveDays" runat="server" Text="Calculate Total Leave Days" OnClick="btnCalculateTotalLeaveDays_Click"
                                                                UseSubmitBehavior="false" CausesValidation="false" EnableTheming="false" />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Reason *
                                                                     <asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtReason"
                                                                         Display="Dynamic" EnableClientScript="True" ErrorMessage="Reason is mandatory">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtReason" runat="server"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr style="height: 5px">
                                                        <td style="width: 30%" align="right">Contact details for Emergency:
                                                        </td>
                                                        <td colspan="2" align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Mobile No
                                                                     
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtPhoneContact" runat="server"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Email address
                                                                     
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtEmailContact" runat="server"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr style="height: 25px">
                                                        <td colspan="2" style="height: 25px"></td>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td align="right" style="width: 15%"></td>
                                                                    <td align="right" style="width: 30%">
                                                                        <asp:Button ID="btnApplyLeave" runat="server" CausesValidation="True" CssClass="savebutton1231" EnableTheming="false" OnClick="btnApplyLeave_Click" SkinID="skinBtnSave" />
                                                                    </td>
                                                                    <td align="center" style="width: 30%">
                                                                        <asp:Button ID="btnCancelNew" runat="server" CausesValidation="False" CommandName="Cancel" CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="btnCancelNew_Click" />
                                                                    </td>
                                                                    <td align="right" style="width: 15%"></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>


                                                </table>
                                            </div>
                                            <table cellspacing="0">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:ValidationSummary ID="valSumAdd" DisplayMode="BulletList" ShowMessageBox="true"
                                                            ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                            Font-Size="12" runat="server" />
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:ObjectDataSource ID="dataSrcLeaveTypes" runat="server" SelectMethod="ListLeaveTypes"
                                                            TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                            <SelectParameters>
                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                            </div>
                        </asp:Panel>

                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

