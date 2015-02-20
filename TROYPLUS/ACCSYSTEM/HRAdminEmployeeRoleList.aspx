<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="HRAdminEmployeeRoleList.aspx.cs" Inherits="EmployeeRole_HRAdminRoleList" EnableEventValidation="false" Title="Human Resources > EmployeeRoleList" %>

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
                            <table style="width: 99.8%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 30px; vertical-align: middle">
                                    <td style="width: 1%"></td>
                                    <td style="width: 30%; font-size: 22px; color: white;">EmployeeRole List
                                    </td>
                                    <td style="width: 12%">
                                        <div style="text-align: right;">
                                           
                                        </div>
                                    </td>
                                    <td style="width: 10%; color: white;" align="right">Search
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <asp:TextBox ID="txtSearchInput" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddlSearchCriteria" runat="server" Width="154px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid white">
                                                <asp:ListItem Value="All">All</asp:ListItem>
                                                <asp:ListItem Value="RoleName">Role Name</asp:ListItem>
                                                <asp:ListItem Value="IsActive">IsActive</asp:ListItem>
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
                                        <asp:GridView ID="grdViewEmpRoleSummary" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Width="100.4%" DataSourceID="EmpRoleSummaryGridSource" AllowPaging="True" DataKeyNames="ID"
                                            OnRowCommand="grdViewEmpRoleSummary_RowCommand" OnRowDataBound="grdViewEmpRoleSummary_RowDataBound"
                                            OnRowDeleted="grdViewEmpRoleSummary_RowDeleted" OnRowDeleting="grdViewEmpRoleSummary_RowDeleting"
                                            EmptyDataText="No Employee Role List Found." Font-Names="Trebuchet MS" CssClass="someClass">
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="Role ID" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Role_Name" HeaderText="Role Name" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%" />
                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%" />
                                                <asp:BoundField DataField="Is_Active" HeaderText="Is Active" Visible="true" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="15%" />

                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Managed Leaves">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnLeaveEdit" runat="server" SkinID="edit" CommandName="ManageLeave" CommandArgument='<%#Eval("Role_Name").ToString() + ":" + Eval("ID").ToString() %>' />
                                                        <asp:ImageButton ID="btnLeaveEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="command" Width="50px"></ItemStyle>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Managed Pay Component">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnPayEdit" runat="server" SkinID="edit" CommandName="ManagePay" CommandArgument='<%#Eval("Role_Name").ToString() + ":" + Eval("ID").ToString() %>' />
                                                        <asp:ImageButton ID="btnPayEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="command" Width="50px"></ItemStyle>
                                                </asp:TemplateField>

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
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ID") %>' />
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
                            CancelControlID="FakeCancelBtn" DynamicServicePath="" Enabled="True" PopupControlID="EmpRoleDetailPopUp"
                            TargetControlID="FakeTargetBtn">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="EmpRoleDetailPopUp" Style="width: 57%">
                            <div id="contentPopUp">
                                <table style="width: 100%;" align="center">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">
                                            <asp:FormView ID="frmEmpRoleAdd" runat="server" Width="100%" DataSourceID="frmEmpRoleSource"
                                                DataKeyNames="ID" OnItemCommand="frmEmpRoleAdd_ItemCommand" DefaultMode="Edit"
                                                OnItemCreated="frmEmpRoleAdd_ItemCreated" Visible="False"
                                                OnItemUpdating="frmEmpRoleAdd_ItemUpdating" EmptyDataText="No Records" OnItemInserted="frmEmpRoleAdd_ItemInserted"
                                                OnItemUpdated="frmEmpRoleAdd_ItemUpdated">
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
                                                                <td colspan="5" class="headerPopUp">Employee Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" align="left">

                                                                    <cc1:TabContainer ID="tabEdit" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabEditMain" runat="server" HeaderText="Employee Role">
                                                                            <ContentTemplate>

                                                                                <table style="width: 770px; vertical-align: text-top; border: 0px solid #86b2d1;"
                                                                                    align="center" cellspacing="2" cellpadding="3">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Role Name:
                                                                    <asp:RequiredFieldValidator ID="rvEmpRoleNameEdit" runat="server" ControlToValidate="txtEmpRoleNameEdit"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Role Name is mandatory"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 20%">
                                                                                            <asp:TextBox ID="txtEmpRoleNameEdit" runat="server" Text='<%# Bind("Role_Name") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                        </td>


                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 14%">Remarks :
                                                                    <asp:RequiredFieldValidator ID="ReqEmpRemarksName" runat="server" ControlToValidate="txtEmpRemarksEdit"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Remarks is mandatory"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtEmpRemarksEdit" runat="server" Text='<%# Bind("Remarks") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 14%">Is Active :                                                                   
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">

                                                                                            <asp:CheckBox ID="chkboxIsActiveEdit" runat="server" Style="color: Black"
                                                                                                Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Is_Active") %>'></asp:CheckBox>

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
                                                                <td colspan="5" class="headerPopUp">Employee Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" align="left">
                                                                    <cc1:TabContainer ID="tablInsert" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabInsMain" runat="server" HeaderText="Employee Role">
                                                                            <ContentTemplate>
                                                                                <table style="width: 770px; border: 0px solid #86b2d1; vertical-align: text-top" align="center" cellpadding="3"
                                                                                    cellspacing="1">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Role Name :
                                                                    <asp:RequiredFieldValidator ID="rvEmpRoleAdd" runat="server" ControlToValidate="txtEmpRoleAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Role Name is mandatory"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 20%">
                                                                                            <asp:TextBox ID="txtEmpRoleAdd" runat="server" Text='<%# Bind("Role_Name") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 14%">Remarks :
                                                                   
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">

                                                                                            <asp:TextBox ID="txtRemarksAdd" runat="server" Text='<%# Bind("Remarks") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 14%">Is Active :
                                                                   
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">

                                                                                            <asp:CheckBox ID="chkboxIsActiveAdd" runat="server" Style="color: Black"
                                                                                                Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Is_Active") %>'></asp:CheckBox>

                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
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

                        <input id="FakeCancelBtn1" type="button" style="display: none" runat="server" />
                        <input id="FakeTargetBtn1" type="button" style="display: none" runat="server" />

                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="FakeCancelBtn1" DynamicServicePath="" Enabled="True" PopupControlID="EmpRoleLeavePopUp"
                            TargetControlID="FakeTargetBtn1">
                        </cc1:ModalPopupExtender>

                        <asp:Panel runat="server" ID="EmpRoleLeavePopUp" Style="width: 57%">
                            <div id="manageLeavePopUp">
                                <table style="width: 100%;" align="center">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">
                                            <%--<asp:FormView ID="FormViewManageLeave" runat="server" Width="100%" DataSourceID="frmEmpRoleManageLeaveSource"
                                                DataKeyNames="ID" OnItemCommand="FormViewManageLeave_ItemCommand" DefaultMode="Edit"
                                                OnItemCreated="FormViewManageLeave_ItemCreated" Visible="False" 
                                                OnItemUpdating="FormViewManageLeave_ItemUpdating" EmptyDataText="No Records" OnItemInserted="FormViewManageLeave_ItemInserted"
                                                OnItemUpdated="FormViewManageLeave_ItemUpdated">
                                                <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"
                                                    BorderColor="#cccccc" Height="20px" />
                                                <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc"
                                                    VerticalAlign="middle" Height="20px" />
                                                <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad"
                                                    Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />--%>


                                            <div class="divArea">
                                                <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                    <tr>
                                                        <td colspan="5" class="headerPopUp">Employee Role Mapping
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 5px">
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5" align="left">
                                                            <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Employee Role">
                                                                    <ContentTemplate>
                                                                        <table style="width: 770px; border: 0px solid #86b2d1; vertical-align: text-top" align="center" cellpadding="3"
                                                                            cellspacing="1">

                                                                            <tr>
                                                                                <td style="width: 20%">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <asp:HiddenField ID="txtManageLeaveRoleID" Visible="false" runat="server"></asp:HiddenField>

                                                                                            <td class="ControlLabel" style="width: 20%">Role Name :                                                               
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 20%">
                                                                                                <asp:TextBox ID="txtManageLeaveRoleName" Enabled="false" runat="server"
                                                                                                    SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="ControlLabel" style="width: 20%; font-weight: bold;">RoleLeave Limitation List:
                                                                                            </td>
                                                                                            <td style="width: 20%"></td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </td>
                                                                                <td style="width: 20%"></td>
                                                                            </tr>

                                                                            <tr style="height: 10px">

                                                                                <asp:GridView ID="ManageLeaveGridView" runat="server" Visible="true" AllowSorting="True" AutoGenerateColumns="False"
                                                                                    Width="99.9%" AllowPaging="True" DataKeyNames="LeaveType_ID"
                                                                                    EmptyDataText="No Leave List Found." Font-Names="Trebuchet MS" CssClass="someClass">
                                                                                    <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                                                    <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                                                    <Columns>

                                                                                        <asp:BoundField DataField="LeaveType_ID" HeaderText="LeaveType ID" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                                                        <%--<asp:BoundField DataField="Role_ID" HeaderText="Role ID" Visible="false" HeaderStyle-BorderColor="Gray" />--%>
                                                                                        <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave Type Name" HeaderStyle-BorderColor="Gray" />
                                                                                        <%-- <asp:BoundField DataField="AllowedCount" HeaderText="Limit/Count" HeaderStyle-BorderColor="Gray" />
                                                                                        <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" Visible="true" HeaderStyle-BorderColor="Gray" />--%>

                                                                                        <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                                                            HeaderText="Leave Type ID" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="txtLeaveTypeID" runat="server" Text='<%# Bind("LeaveType_ID") %>'
                                                                                                    SkinID="skinTxtBoxGrid" TabIndex="1"></asp:Label>

                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                                                            HeaderText="Limit/Count">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtLeaveCountAdd" runat="server" Text='<%# Bind("AllowedCount") %>'
                                                                                                    SkinID="skinTxtBoxGrid" TabIndex="1" BorderColor="#999999" BorderWidth="1" BackColor="#cccccc"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="rvLevRoleCount" runat="server" ControlToValidate="txtLeaveCountAdd"
                                                                                                    Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Allowed Count is mandatory"></asp:RequiredFieldValidator>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="command" Width="50px"></ItemStyle>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                                                            HeaderText="Effective Date">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtLeaveEffDate" runat="server" Text='<%# Bind("EffectiveDate") %>'
                                                                                                    SkinID="skinTxtBoxGrid" TabIndex="1" BorderColor="#999999" BorderWidth="1" BackColor="#cccccc"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="calEffDate" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                    PopupButtonID="txtLeaveEffDate" PopupPosition="BottomLeft" TargetControlID="txtLeaveEffDate">
                                                                                                </cc1:CalendarExtender>
                                                                                                <asp:RequiredFieldValidator ID="rvLevEffDate" runat="server" ControlToValidate="txtLeaveEffDate"
                                                                                                    Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Effective Date is mandatory"></asp:RequiredFieldValidator>
                                                                                                <%--<asp:ImageButton ID="btnEffDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                 Width="20px" runat="server" />--%>
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

                                                                            </tr>

                                                                            <tr style="height: 3px">
                                                                            </tr>

                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <table width="100%">
                                                                                        <tr>
                                                                                            <td align="right" style="width: 33%;"></td>
                                                                                            <td style="width: 19%;">
                                                                                                <asp:Button ID="manageLeaveSaveBtn" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                                    OnClick="manageLeaveSaveBtn_Click"></asp:Button>
                                                                                            </td>
                                                                                            <td style="width: 19%;">
                                                                                                <asp:Button ID="manageLeaveCancelBtn" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="manageLeaveCancelBtn_Click"></asp:Button>
                                                                                            </td>
                                                                                            <td style="width: 29%;"></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>

                                                                        </table>
                                                                    </ContentTemplate>
                                                                </cc1:TabPanel>
                                                            </cc1:TabContainer>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 6px">
                                                    </tr>

                                                    <%--<tr style="height:2px">
                                                            </tr>--%>
                                                </table>
                                            </div>

                                            <table cellspacing="0">
                                                <tr>
                                                    <td style="width: 50%" colspan="2">
                                                        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" ShowMessageBox="true"
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

                                            <%-- </InsertItemTemplate>

                                                 <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:FormView>--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
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
                                                                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Role Pay Component Mapping">
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
                                                                                                <asp:TextBox ID="txtfrmDate" Enabled="false" runat="server"
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
                                                                                                <asp:TextBox ID="txtDeclaredAmt" Enabled="false" runat="server"
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
                                                                                        Width="99.9%" AllowPaging="True" DataKeyNames="PayComponent_ID" OnSelectedIndexChanged="PayCompRolePayGrid_SelectedIndexChanged"
                                                                                        OnRowDataBound="PayCompRolePayGrid_RowDataBound" EmptyDataText="No Pay Component for Roles List Found." Font-Names="Trebuchet MS">
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
                                                                                                <asp:Button ID="AssignRolePayMapBtn" Text="Assign" runat="server" CausesValidation="False" CommandName="AssignRolePay"
                                                                                                    EnableTheming="false" SkinID="skinBtnCancel"></asp:Button>
                                                                                                <asp:Button ID="CancelRolePayMapBtn" Text="Cancel" runat="server" CausesValidation="False" CommandName="CancelRolePay"
                                                                                                    EnableTheming="false" SkinID="skinBtnCancel" OnClick="CancelRolePayMapBtn_Click"></asp:Button>

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

                <tr style="width: 100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="EmpRoleSummaryGridSource" runat="server" SelectMethod="GetEmpRoleSummary"
                            OnDeleting="EmpRoleSummaryGridSource_Deleting" DeleteMethod="DeleteEmpRoleInfo" TypeName="BusinessLogic">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ID" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>

                        <asp:ObjectDataSource ID="frmEmpRoleSource" runat="server" SelectMethod="GetEmpRoleInfoByID"
                            TypeName="BusinessLogic" OnUpdating="frmEmpRoleSource_Updating" OnInserting="frmEmpRoleSource_Inserting"
                            InsertMethod="InsertEmpRoleInfo" UpdateMethod="UpdateEmpRoleInfo">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ID" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Role_Name" Type="String" />
                                <asp:Parameter Name="Remarks" Type="String" />
                                <asp:Parameter Name="Is_Active" Type="Boolean" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="grdViewEmpRoleSummary" Name="ID" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Role_Name" Type="String" />
                                <asp:Parameter Name="Remarks" Type="String" />
                                <asp:Parameter Name="Is_Active" Type="Boolean" />
                            </InsertParameters>
                        </asp:ObjectDataSource>

                        <asp:ObjectDataSource ID="frmEmpRoleManageLeaveSource" runat="server" SelectMethod="GetEmpManageLeaveInfoByID"
                            TypeName="BusinessLogic" OnUpdating="frmEmpRoleManageLeaveSource_Updating" OnInserting="frmEmpRoleManageLeaveSource_Inserting"
                            InsertMethod="InsertEmpRoleInfo">
                            <%--<UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ID" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Role_Name" Type="String" />
                                <asp:Parameter Name="Remarks" Type="String" />
                                <asp:Parameter Name="Is_Active" Type="Boolean" />                               
                            </UpdateParameters>--%>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="grdViewEmpRoleSummary" Name="ID" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Role_Name" Type="String" />
                                <asp:Parameter Name="Remarks" Type="String" />
                                <asp:Parameter Name="Is_Active" Type="Boolean" />
                            </InsertParameters>
                        </asp:ObjectDataSource>

                    </td>
                </tr>

            </table>
            <table width="100%">
                <tr>
                    <td align="center">
                         <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAddEmpRole" runat="server" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text="" OnClick="lnkBtnAddEmpRole_Click"></asp:Button>
                                            </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
