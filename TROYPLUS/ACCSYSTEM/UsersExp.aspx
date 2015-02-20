<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="UsersExp.aspx.cs" Inherits="UsersExp" Title="Security > Users Lock" %>

<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Namespace="UrlNameSpace" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
<asp:UpdatePanel ID="UpdatePanelPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">
                <table class="mainConHd" style="width: 994px;">
                    <tr valign="middle">
                        <td style="font-size: 20px;">
                            Users And Options
                        </td>
                    </tr>
                </table>
                <div class="mainConBody">
                    <table style="width: 100%;" cellpadding="3" cellspacing="2" class="searchbg">
                        <tr style="height: 25px; vertical-align: middle">
                            <td style="width: 20%">

                            </td>
                            <td style="width: 15%">
                                <div style="text-align: right;">
                                    <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                        <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd"
                                            EnableTheming="false" Width="80px" Text="Add New"></asp:Button>
                                    </asp:Panel>
                                </div>
                            </td>
                            <td style="width: 10%;" align="center">
                                User Name
                            </td>
                            <td style="width: 25%" class="cssTextBoxbg">
                                <asp:TextBox ID="txtUserName" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                            </td>
                            <td colspan="2" class="tblLeftNoPad" style="width: 50%">
                                <asp:Button ID="lnkBtnSearchId" runat="server" OnClick="lnkBtnSearch_Click" Text="Search"
                                    ToolTip="Click here to submit" SkinID="skinBtnSearch" TabIndex="3" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="product" HeaderText="Validation Messages"
                        Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                    <asp:ValidationSummary ID="VS" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="purchaseval" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                        Font-Size="12" runat="server" />
                    <input id="dummyPurchase" type="button" style="display: none" runat="server" />
                    <input id="BtnPopUpCancel1" type="button" style="display: none" runat="server" />
                    <cc1:ModalPopupExtender ID="ModalPopupGet" runat="server" BackgroundCssClass="modalBackground"
                        CancelControlID="BtnPopUpCancel1" DynamicServicePath="" Enabled="True" PopupControlID="purchasePanel"
                        RepositionMode="RepositionOnWindowResizeAndScroll" TargetControlID="dummyPurchase">
                    </cc1:ModalPopupExtender>
                    <asp:Panel ID="purchasePanel" runat="server" Style="width: 67%; display: none">
                        <asp:UpdatePanel ID="updatePnlPurchase" runat="server" UpdateMode="Conditional" >
                            <ContentTemplate>
                                <div id="Div1" style="background-color: White;">
                                    <table style="width: 100%;" align="center">
                                        <tr style="width: 100%">
                                            <td style="width: 100%">
                                                <table style="text-align: left; border: 1px solid blue;" width="100%" cellpadding="3" cellspacing="2">
                                                    <tr>
                                                        <td>
                                                            <table class="headerPopUp" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        Users And Options
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5">
                                                            <table style="border: 1px solid #5078B3" cellpadding="3" cellspacing="2"
                                                                width="100%">
                                                                <tr style="height:5px">
                                                                </tr>
                                                                <tr>
                                                                    <td class="ControlLabel" style="width: 27%">
                                                                        UserName *
                                                                    </td>
                                                                    <td class="ControlTextBox3" style="width: 20%">
                                                                        <asp:TextBox ID="txtUser" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                    </td>
                                                                    <td class="ControlLabel" style="width: 9%">
                                                                        Email
                                                                    </td>
                                                                    <td class="ControlTextBox3" style="width: 20%">
                                                                        <asp:TextBox ID="txtEmail" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 24%">
                                                                        <asp:CheckBox ID="chkboxdatelock" runat="server" Text="Date Lock" Font-Size="15px" AutoPostBack="true" />
                                                                        <asp:CheckBox runat="server" ID="chkAccLocked" Visible="False" />
                                                                    </td>
                                                                </tr>
                                                                <tr style="height:5px">
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <div id="div" runat="server" style="height:300px; overflow:scroll">
                                                                            <rwg:BulkEditGridView ID="GrdViewItem" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                Width="100%">
                                                                                <RowStyle CssClass="dataRow" />
                                                                                <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                <AlternatingRowStyle CssClass="altRow" />
                                                                                <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                <FooterStyle CssClass="dataRow" />
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="Area" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="blue" HeaderStyle-Wrap="false" />
                                                                                    <asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="blue" HeaderStyle-Wrap="false" />
                                                                                        <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="blue" HeaderStyle-Wrap="false" />
                                                                                        <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="blue"/>
                                                                                        <asp:TemplateField  Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label id="lblDebtorID" runat ="server" Text='<%# Eval("OrderNo")%>'/>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="blue">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkboxAdd" runat="server" style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Add") %>'>
                                                                                                </asp:CheckBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="blue">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkboxEdit" runat="server" style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Edit") %>'>
                                                                                                </asp:CheckBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="blue">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkboxDel" runat="server" style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Delete") %>'>
                                                                                                </asp:CheckBox>
                                                                                             </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="blue">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkboxView" runat="server" style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Views") %>'>
                                                                                                </asp:CheckBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                            </rwg:BulkEditGridView>
                                                                        </div>
                                                                    </td>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <table width="100%">
                                                            <tr>     
                                                                <td style="width:34%">
                                                                                
                                                                </td>                                
                                                                <td style="width:16%">
                                                                    <asp:Button ID="cmdSave" runat="server" Text="" CssClass="savebutton"
                                                                        EnableTheming="false" OnClick="cmdSave_Click" SkinID="skinBtnSave" />
                                                                </td>
                                                                <td style="width:16%">
                                                                    <asp:Button ID="btnCancel" runat="server" Text="" CssClass="cancelbutton" EnableTheming="false"
                                                                        Visible="true" OnClick="cmdCancel_Click" SkinID="skinBtnCancel" />
                                                                </td>
                                                                <td style="width:34%">
                                                                                
                                                                </td> 
                                                            </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                            
                    
                </td>
            </tr>
            <tr style="text-align: left">
                <td width="100%">
                    <table width="100%" style="margin: -3px 0px 0px 0px;">
                        <tr style="width: 100%">
                            <td>
                                <div class="mainGridHold" id="searchGrid" align="center">
                                    <asp:GridView ID="GrdViewCust" DataKeyNames="username" GridLines="Both" Width="99.8%" OnRowDataBound="GrdViewCust_RowDataBound"
                                        runat="server" AutoGenerateColumns="False" DataSourceID="GridSource" OnRowCreated="GrdViewCust_RowCreated" OnSelectedIndexChanged="GrdViewCust_SelectedIndexChanged"
                                        AllowPaging="true" CssClass="someClass" OnRowDeleting="GrdViewCust_RowDeleting">
                                        <RowStyle BorderWidth="1" HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:BoundField DataField="username" HeaderText="User" HeaderStyle-BorderColor="Blue"></asp:BoundField>
                                            <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-BorderColor="Blue"></asp:BoundField>
                                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="blue">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Blue"
                                                HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" OnClientClick="return confirm('Are you sure you want to delete the User?');"
                                                        CommandName="Delete"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderStyle-BorderColor="Blue">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerTemplate>
                                            <table style=" border-color:white">
                                                <tr style=" border-color:white">
                                                    <td style=" border-color:white">
                                                        Goto Page
                                                    </td>
                                                    <td style=" border-color:white">
                                                        <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="55px" style="border:1px solid blue">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style=" border-color:white;Width:5px">
                                            
                                                    </td>
                                                    <td style=" border-color:white">
                                                        <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                            ID="btnFirst" />
                                                    </td>
                                                    <td style=" border-color:white">
                                                        <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                            ID="btnPrevious" />
                                                    </td>
                                                    <td style=" border-color:white">
                                                        <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                            ID="btnNext" />
                                                    </td>
                                                    <td style=" border-color:white">
                                                        <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                            ID="btnLast" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </PagerTemplate>
                                    </asp:GridView>
                                </div>
                                <asp:ObjectDataSource ID="GridSource" runat="server" DeleteMethod="DeleteUserOptions" SelectMethod="ListUsers"
                                    TypeName="BusinessLogic" OnDeleting="GridSource_Deleting">
                                    <DeleteParameters>
                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                        <asp:ControlParameter ControlID="GrdViewCust" Name="username" Type="String" />
                                    </DeleteParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
