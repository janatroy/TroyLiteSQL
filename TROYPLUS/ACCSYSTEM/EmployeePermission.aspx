<%@ Page Title="Human Resource > Employee Permission" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="EmployeePermission.aspx.cs" Inherits="EmployeePermission" %>

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
                                    <td style="width: 16%; font-size: 22px; color: #000000;">Permission
                                    </td>
                                    <td style="width: 14%">
                                        <div style="text-align: right;">
                                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnApplyPermission" runat="server" CssClass="ButtonAdd66" CausesValidation="false"
                                                    EnableTheming="false" Width="80px" Text="" OnClick="lnkBtnApplyPermission_Click"></asp:Button>
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
                                                <asp:ListItem Value="PermissionDate">Permission Date</asp:ListItem>
                                                <asp:ListItem Value="AppliedDate">Applied Date </asp:ListItem>
                                                <asp:ListItem Value="Status">Status</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 17%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearchPermission" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" Visible="false" />
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
                                                <asp:BoundField DataField="PermissionId" HeaderText="PermissionId" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="TimeRange" HeaderText="Time Range" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="DateApplied" HeaderText="Date Applied" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:BoundField DataField="ApproverName" HeaderText="Approver" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="ApproverComments" HeaderText="Approver Comments" HeaderStyle-BorderColor="Gray" NullDisplayText="NA" />
                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditPermission" runat="server" SkinID="edit" CommandName="EditPermission" CausesValidation="false" CommandArgument='<%#Eval("PermissionId")%>' />
                                                        <asp:ImageButton ID="btnEditPermissionDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="command" Width="50px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Cancel">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnCancelPermission" runat="server" SkinID="delete" CommandName="CancelPermission" CommandArgument='<%#Eval("PermissionId")%>' CausesValidation="false" />
                                                        <asp:ImageButton ID="btnCancelPermissionDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="btnCancelPermission" ConfirmText="Are you sure to Cancel this Leave ?"
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
                            CancelControlID="FakeCancelBtn" Enabled="True" PopupControlID="PermissionDetailPopUp"
                            TargetControlID="FakeTargetBtn">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="PermissionDetailPopUp" Style="width: 50%" Visible="true">
                            <div id="contentPopUp">
                                <table style="width: 100%;" align="center">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">
                                            <%-- <asp:FormView ID="frmViewApplyLeave" runat="server" Width="100%" DataSourceID="frmViewSource"
                                                DataKeyNames="LedgerID" OnItemCommand="frmViewApplyLeave_ItemCommand" DefaultMode="Edit"
                                                OnItemCreated="frmViewApplyLeave_ItemCreated" Visible="False" OnItemInserting="frmViewApplyLeave_ItemInserting"
                                                OnItemUpdating="frmViewApplyLeave_ItemUpdating" EmptyDataText="No Records" OnItemInserted="frmViewApplyLeave_ItemInserted"
                                                OnItemUpdated="frmViewApplyLeave_ItemUpdated">
                                                <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"
                                                    BorderColor="#cccccc" Height="20px" />
                                                <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc"
                                                    VerticalAlign="middle" Height="20px" />
                                                <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad"
                                                    Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                                                <InsertItemTemplate>--%>
                                            <div class="divArea">
                                                <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                    <tr>
                                                        <td colspan="4" class="headerPopUp">Apply Permission
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 5px">
                                                        <asp:HiddenField ID="hdfPermissionId" runat="server" />
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Approver                                                                    
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%">
                                                            <asp:Label ID="lblApproverName" runat="server"
                                                                TabIndex="1"></asp:Label>
                                                            <asp:HiddenField ID="hdfApproverEmpNo" runat="server" />
                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Permission Date
                                                                    <asp:RequiredFieldValidator ID="rfvPermissionDate" runat="server" ControlToValidate="txtPermissionDate"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Start Date is mandatory">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="ControlNumberBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtPermissionDate" runat="server"
                                                                SkinID="skinTxtBox" TabIndex="4"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calExtenderStartDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                PopupButtonID="btnStartDate" PopupPosition="BottomLeft" TargetControlID="txtPermissionDate">
                                                            </cc1:CalendarExtender>
                                                            <%--<asp:DropDownList ID="ddlStartDateSession" runat="server" Width="55px" BackColor="#90c9fc" Style="border: 1px solid blue" Height="26px" CssClass="drpDownListMedium"
                                                                        TabIndex="3">
                                                                        <asp:ListItem Text="FN" Value="FN"></asp:ListItem>
                                                                        <asp:ListItem Text="AN" Value="AN"></asp:ListItem>
                                                                    </asp:DropDownList>--%>
                                                        </td>

                                                        <td style="width: 30%" align="left">
                                                            <asp:ImageButton ID="btnStartDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                Width="20px" runat="server" />
                                                        </td>

                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Start Time
                                                                    <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ControlToValidate="txtStartTime"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Start Time is mandatory">*</asp:RequiredFieldValidator>

                                                        </td>
                                                        <td class="ControlNumberBox3" style="width: 30%">
                                                            <%--<asp:TextBox ID="txtStartTime" runat="server" 
                                                                        SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox> --%>

                                                            <asp:DropDownList ID="txtStartTime" runat="server" Width="55px" BackColor="#90c9fc" Style="border: 1px solid blue" Height="26px" CssClass="drpDownListMedium"
                                                                TabIndex="3">
                                                                <asp:ListItem Text="01" Value="01:00"></asp:ListItem>
                                                                <asp:ListItem Text="02" Value="02:00"></asp:ListItem>
                                                                <asp:ListItem Text="03" Value="03:00"></asp:ListItem>
                                                                <asp:ListItem Text="04" Value="04:00"></asp:ListItem>
                                                                <asp:ListItem Text="05" Value="05:00"></asp:ListItem>
                                                                <asp:ListItem Text="06" Value="06:00"></asp:ListItem>
                                                                <asp:ListItem Text="07" Value="07:00"></asp:ListItem>
                                                                <asp:ListItem Text="08" Value="08:00"></asp:ListItem>
                                                                <asp:ListItem Text="09" Value="09:00"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10:00"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11:00"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12:00"></asp:ListItem>

                                                            </asp:DropDownList>

                                                            <asp:DropDownList ID="ddlStartTimeSession" runat="server" Width="55px" BackColor="#90c9fc" Style="border: 1px solid blue" Height="26px" CssClass="drpDownListMedium"
                                                                TabIndex="3">
                                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                        <%--<td style="width: 30%" align="left">
                                                                     <asp:ImageButton ID="btnEndDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                Width="20px" runat="server" />
                                                                </td>--%>

                                                        <td>
                                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtStartTime" 
                                                                        runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+">
                                                                    </asp:RegularExpressionValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">End Time
                                                                    <asp:RequiredFieldValidator ID="rfvEndTime" runat="server" ControlToValidate="txtEndTime"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="End Time is mandatory">*</asp:RequiredFieldValidator>

                                                            <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtEndTime"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="End Time should not be less than Start Time."
                                                                        Operator="GreaterThanEqual" ControlToCompare="txtStartTime" Type="Date">*</asp:CompareValidator>--%>
                                                        </td>
                                                        <td class="ControlNumberBox3" style="width: 30%">
                                                            <%--<asp:TextBox ID="txtEndTime" runat="server" 
                                                                        SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox>--%>

                                                            <asp:DropDownList ID="txtEndTime" runat="server" Width="55px" BackColor="#90c9fc" Style="border: 1px solid blue" Height="26px" CssClass="drpDownListMedium"
                                                                TabIndex="3">
                                                                <asp:ListItem Text="01" Value="01:00"></asp:ListItem>
                                                                <asp:ListItem Text="02" Value="02:00"></asp:ListItem>
                                                                <asp:ListItem Text="03" Value="03:00"></asp:ListItem>
                                                                <asp:ListItem Text="04" Value="04:00"></asp:ListItem>
                                                                <asp:ListItem Text="05" Value="05:00"></asp:ListItem>
                                                                <asp:ListItem Text="06" Value="06:00"></asp:ListItem>
                                                                <asp:ListItem Text="07" Value="07:00"></asp:ListItem>
                                                                <asp:ListItem Text="08" Value="08:00"></asp:ListItem>
                                                                <asp:ListItem Text="09" Value="09:00"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10:00"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11:00"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12:00"></asp:ListItem>

                                                            </asp:DropDownList>

                                                            <asp:DropDownList ID="ddlEndTimeSession" runat="server" Width="55px" BackColor="#90c9fc" Style="border: 1px solid blue" Height="26px" CssClass="drpDownListMedium"
                                                                TabIndex="3">
                                                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                        <%-- <td style="width: 30%" align="left">
                                                                     <asp:ImageButton ID="ImageButton1" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                Width="20px" runat="server" />
                                                                </td>--%>

                                                        <td>
                                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtEndTime" 
                                                                        runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+">
                                                                    </asp:RegularExpressionValidator>--%>
                                                        </td>

                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>

                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Permission Reason 
                                                                     <asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtReason"
                                                                         Display="Dynamic" EnableClientScript="True" ErrorMessage="Reason is mandatory">*</asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtReason" runat="server"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox>
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
                                                            <asp:TextBox ID="txtPhoneContact" runat="server"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                ControlToValidate="txtPhoneContact" ErrorMessage="Mobile No is required"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPhoneContact"
                                                                runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="[0-9]{10}">
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                        <td style="width: 30%"></td>

                                                    </tr>
                                                    <tr style="height: 3px">
                                                    </tr>
                                                    <tr>
                                                        <td class="ControlLabel" style="width: 40%">Email address
                                                                     
                                                        </td>
                                                        <td class="ControlTextBox3" style="width: 30%">
                                                            <asp:TextBox ID="txtEmailContact" runat="server"
                                                                SkinID="skinTxtBoxGrid" TabIndex="4"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtEmailContact" ErrorMessage="Email is required"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                            ErrorMessage="Invalid Email" ControlToValidate="txtEmailContact"
                                                            SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                        </asp:RegularExpressionValidator>

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
                                                                        <asp:Button ID="btnApplyPermission" runat="server" CausesValidation="True"
                                                                            CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                            OnClick="btnApplyPermission_Click"></asp:Button>
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
                                                        <asp:ObjectDataSource ID="dataSrcLeaveTypes" runat="server" SelectMethod="ListPermissionTypes"
                                                            TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                            <SelectParameters>
                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
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

