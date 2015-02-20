<%@ Page Language="C#" AutoEventWireup="true" Title="Human Resource > Employee Permission Approval" CodeFile="EmployeePermissionApproval.aspx.cs" Inherits="EmployeePermissionApproval" MasterPageFile="~/PageMaster.master" %>

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
                                    <td style="width: 16%; font-size: 22px; color: #000000;">Permission Requests
                                    </td>
                                    <td style="width: 14%">
                                        <div style="text-align: right;">
                                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                            </asp:Panel>
                                        </div>
                                    </td>
                                    <td style="width: 13%; color: #000080;" align="right">
                                        <%--Search--%>
                                    </td>
                                    <td style="width: 20%">
                                        <asp:TextBox ID="txtSearchInput" runat="server" SkinID="skinTxtBoxSearch" Visible="false"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddlSearchCriteria" runat="server" Width="154px" BackColor="#BBCAFB" Height="23px" Style="text-align: center; border: 1px solid #BBCAFB" Visible="false">
                                                <asp:ListItem Value="All">All</asp:ListItem>
                                                <asp:ListItem Value="EmployeeName">Employee Name</asp:ListItem>
                                                <asp:ListItem Value="AliasName">Alias Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 17%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearchAttendance" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" Visible="false" />
                                    </td>

                                    <td style="width: 20%" class="tblLeftNoPad">
                                        <asp:Button ID="btnClearFilter" runat="server" EnableTheming="false" Text="" CssClass="ClearFilter6" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr style="width: 100%; height: 100%">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="grdViewPermissionSummary" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Width="99.9%" AllowPaging="True" DataKeyNames="PermissionId"
                                            EmptyDataText="No Permission Found." OnRowCommand="grdViewPermissionSummary_RowCommand" Font-Names="Trebuchet MS" CssClass="someClass">
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="PermissionId" HeaderText="LeaveId" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="TimeRange" HeaderText="Date Range" HeaderStyle-BorderColor="Gray" />
                                                <%--<asp:BoundField DataField="TotalDays" HeaderText="No. of Days" HeaderStyle-BorderColor="Gray" />--%>
                                                <asp:BoundField DataField="DateApplied" HeaderText="Date Applied" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />

                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnApprovePermission" runat="server" Text="Approve" CommandName="ApprovePermission" CausesValidation="false" CommandArgument='<%#Eval("PermissionId")%>' />

                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="command" Width="50px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnRejectPermission" runat="server" Text="Reject" CommandName="RejectPermission" CommandArgument='<%#Eval("PermissionId")%>' CausesValidation="false" />

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
                            CancelControlID="FakeCancelBtn" Enabled="True" PopupControlID="PermissionDetailPopUp"
                            TargetControlID="FakeTargetBtn">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="PermissionDetailPopUp" Style="width: 50%" Visible="true">
                            <div id="contentPopUp">
                                <table style="width: 100%;" align="center">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">

                                            <div class="divArea">
                                                <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                    <tr>
                                                        <td colspan="4" class="headerPopUp">
                                                            <asp:Label ID="lblPopupTitle" runat="server"
                                                                TabIndex="1" Text="Approve\Reject Permission Request"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 5px">
                                                        <asp:HiddenField ID="hdfPermissionId" runat="server" />
                                                        <asp:HiddenField ID="hdfEmpNo" runat="server" />
                                                        <asp:HiddenField ID="hdfPermissionRequestResponse" runat="server" />
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Employee                                                                    
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%" align="left">
                                                            <asp:Label ID="lblEmployeeName" runat="server"
                                                                TabIndex="1"></asp:Label>

                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Start Time
                                                                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Start Date is mandatory">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="ControlNumberBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtStartDate" runat="server"
                                                                SkinID="skinTxtBox" TabIndex="4" ReadOnly="true"></asp:TextBox>
                                                            <%--<cc1:CalendarExtender ID="calExtenderStartDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnStartDate" PopupPosition="BottomLeft" TargetControlID="txtStartDate">
                                                                            </cc1:CalendarExtender>--%>
                                                            <asp:DropDownList ID="ddlStartDateSession" runat="server" Width="55px" BackColor="#90c9fc" Style="border: 1px solid blue"
                                                                Height="26px" CssClass="drpDownListMedium" Enabled="false"
                                                                TabIndex="3">
                                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                        <td style="width: 30%" align="left"></td>

                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">End  Time
                                                                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="End Date is mandatory">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="ControlNumberBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtEndDate" runat="server"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4" ReadOnly="true"></asp:TextBox>
                                                            <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                PopupButtonID="btnEndDate" PopupPosition="BottomLeft" TargetControlID="txtEndDate">
                                                                            </cc1:CalendarExtender>--%>
                                                            <asp:DropDownList ID="ddlEndDateSession" runat="server" Width="55px" BackColor="#90c9fc" Style="border: 1px solid blue" Height="26px" CssClass="drpDownListMedium"
                                                                TabIndex="3" Enabled="false">
                                                                <asp:ListItem Text="PM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                        <td style="width: 30%" align="left"></td>

                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>

                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Reason *
                                                                     <asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtReason"
                                                                         Display="Dynamic" EnableClientScript="True" ErrorMessage="Reason is mandatory">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtReason" runat="server"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>
                                                    <tr style="height: 5px">
                                                        <td style="width: 30%" align="right">Contact details for Emergency:
                                                        </td>
                                                        <td colspan="2" align="left"></td>
                                                    </tr>
                                                    <tr style="height: 5px">
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Mobile No
                                                                     
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtPhoneContact" runat="server" ReadOnly="true"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Email address
                                                                     
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtEmailContact" runat="server" ReadOnly="true"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Approver Comments:
                                                                    <asp:RequiredFieldValidator ID="rfvComments" runat="server" ControlToValidate="txtApproverComments"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Approver comment is mandatory">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtApproverComments" runat="server"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4" TextMode="MultiLine" Height="50px"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>

                                                    <tr>
                                                        <td colspan="4">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td align="right" style="width: 15%"></td>
                                                                    <td align="right" style="width: 30%">
                                                                        <asp:Button ID="btnCancelNew" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                            CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel"></asp:Button>
                                                                    </td>
                                                                    <td align="center" style="width: 30%">
                                                                        <asp:Button ID="btnSubmit" runat="server" CausesValidation="True"
                                                                            CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="btnSubmit_Click"></asp:Button>
                                                                        <cc1:ConfirmButtonExtender ID="ConfirmApprove" TargetControlID="btnSubmit" ConfirmText="Are you sure to Approve/Reject this Permission ?"
                                                                            runat="server">
                                                                        </cc1:ConfirmButtonExtender>
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
                                                        <%--<asp:ObjectDataSource ID="dataSrcLeaveTypes" runat="server" SelectMethod="ListPermissionTypes"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--</InsertItemTemplate>

                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:FormView>--%>
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

