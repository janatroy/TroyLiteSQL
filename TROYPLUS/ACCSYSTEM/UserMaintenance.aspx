<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="UserMaintenance.aspx.cs" Inherits="UserMaintenance" Title="Security > Manage Users" %>

<%@ Register Namespace="UrlNameSpace" TagPrefix="uc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    
    <table style="width: 100%">
        <tr style="width: 100%">
            <td style="width: 100%">
                
                    <%--<div class="mainConHd">
                        <table cellspacing="0" cellpadding="0" border="0">
                            <tr valign="middle" align="center" style="text-align: center">
                                <td>
                                    <span>Users and Roles</span>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Users and Roles
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
                
                <asp:ObjectDataSource ID="GridSource" runat="server" DeleteMethod="DeleteUserInfo"
                    SelectMethod="ListUsers" TypeName="BusinessLogic">
                    <DeleteParameters>
                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                        <asp:ControlParameter ControlID="GrdViewCust" Name="username" Type="String" />
                    </DeleteParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
        <tr style="text-align: left">
            <td width="100%">
            <table width="100%" style="margin: -3px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                <div class="mainGridHold" id="searchGrid" align="center">
                
                    <asp:GridView ID="GrdViewCust" DataKeyNames="username" GridLines="Both" Width="99.8%"
                        runat="server" AutoGenerateColumns="False" OnRowCreated="GrdViewCust_RowCreated"
                        AllowPaging="true" DataSourceID="GridSource" OnRowCommand="GrdViewCust_RowCommand" CssClass="someClass">
                        <RowStyle BorderWidth="1" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="username" HeaderText="User" HeaderStyle-BorderColor="Blue"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-BorderColor="Blue"></asp:BoundField>
                            <uc:BoundUrlColumn DataField="username" HeaderStyle-HorizontalAlign="Center" HeaderText="Edit"
                                Tooltip="Edit User Roles" BaseUrl="UserDetails.aspx?ID=" IconPath="App_Themes/DefaultTheme/Images/edit.png"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </uc:BoundUrlColumn>
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
                </td>
                </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
