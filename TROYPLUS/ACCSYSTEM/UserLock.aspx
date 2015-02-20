<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="UserLock.aspx.cs" Inherits="UserLock"
    Title="Security > Users Lock" %>

    <%@ Register Namespace="UrlNameSpace" TagPrefix="uc" %>
    <%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script src="Scripts/JScriptPurchase.js" type="text/javascript">

    </script>
    <asp:UpdatePanel ID="UpdatePnlMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            <div style="">
                <table style="width: 100%;" align="center">
                    <tr style="width: 100%">
                        <td style="width: 100%" valign="middle">
                            <table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Users Lock
                                    </td>
                                </tr>
                            </table>
                            <div class="mainConBody">
                                <div>
                                    <table cellspacing="2px" cellpadding="3px" border="0" width="100%"
                                        class="searchbg">
                                        <tr>
                                            <td style="width: 8%">
                                            </td>
                                            <td style="width: 15%">
                                                <div style="text-align: right;">
                                                    <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                        <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd"
                                                            EnableTheming="false" Width="80px" Text="Add New"></asp:Button>
                                                    </asp:Panel>
                                                </div>
                                            </td>
                                            <td style="width: 8%;" align="center">
                                                 User Name
                                            </td>
                                            <td style="width: 22%; text-align: center" class="tblLeft cssTextBoxbg">
                                                <asp:TextBox ID="txtUserName" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                                <asp:TextBox ValidationGroup="search" ID="txtBillnoSrc" runat="server" MaxLength="8"
                                                    CssClass="cssTextBox" Width="92%" Visible="False"></asp:TextBox>
                                            </td>
                                            <td style="width: 8%" align="center">
                                                
                                            </td>
                                            <td style="width: 22%">
                                                <asp:TextBox ValidationGroup="search" ID="txtTransNo" runat="server" MaxLength="8"
                                                    CssClass="cssTextBox" Width="92%" Visible="False"></asp:TextBox>
                                            </td>
                                            <td style="width: 28%; text-align: left">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="skinBtnSearch" OnClick="btnSearch_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            
                                                                    
                            <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="product" HeaderText="Validation Messages"
                                Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                            <asp:ValidationSummary ID="VS" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false"
                                ValidationGroup="purchaseval" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                Font-Size="12" runat="server" />
                            <input id="dummyPurchase" type="button" style="display: none" runat="server" />
                            <input id="BtnPopUpCancel1" type="button" style="display: none" runat="server" />
                            <cc1:ModalPopupExtender ID="ModalPopupLock" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="BtnPopUpCancel1" DynamicServicePath="" Enabled="True" PopupControlID="purchasePanel"
                                RepositionMode="RepositionOnWindowResizeAndScroll" TargetControlID="dummyPurchase">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="purchasePanel" runat="server" Style="width: 67%; display: none">
                                <asp:UpdatePanel ID="updatePnlPurchase" runat="server" RenderMode="Block" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                        <div id="Div1" style="background-color: White;">
                                            <table style="width: 100%;" align="center">
                                                <tr style="width: 100%">
                                                    <td style="width: 100%">
                                                        <table style="text-align: left;" width="100%" cellpadding="1" cellspacing="1">
                                                            <tr>
                                                                <td>
                                                                    <table class="headerPopUp" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                Users Lock
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                    <table cellpadding="0" cellspacing="1" style="border: 1px solid Silver; text-align: center"
                                                                        width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <div id="div" runat="server" style="height:300px; overflow:scroll">
                                                                                <%--<asp:GridView ID="GrdViewItems" runat="server"
                                                                                    Width="100%" AllowPaging="True" AllowSorting="False" AutoGenerateColumns="False"
                                                                                    EmptyDataText="No Master found!" >
                                                                                    <EmptyDataRowStyle CssClass="GrdContent" />--%>
                                                                                <rwg:BulkEditGridView ID="GrdViewItems" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                    BorderStyle="Solid" GridLines="Both" SaveButtonID="SaveButton" runat="server" CssClass="someClass"
                                                                                    Width="100%">
                                                                                    <RowStyle CssClass="dataRow" />
                                                                                    <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                    <AlternatingRowStyle CssClass="altRow" />
                                                                                    <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                    <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                    <FooterStyle CssClass="dataRow" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="Section" HeaderText="Area" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="blue" HeaderStyle-Wrap="false" />
                                                                                        <asp:BoundField DataField="RoleDesc" HeaderText="Screen Name" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="blue" HeaderStyle-Wrap="false" />
                                                                                        <asp:BoundField DataField="Role" HeaderText="Roles" HeaderStyle-Wrap="false" ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-BorderColor="blue"/>
                                                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Add" HeaderStyle-BorderColor="blue">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkboxAdd" runat="server" style="color: Black" Text="" Font-Names="arial" Font-Size="11px">
                                                                                                </asp:CheckBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Edit" HeaderStyle-BorderColor="blue">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkboxEdit" runat="server" style="color: Black" Text="" Font-Names="arial" Font-Size="11px">
                                                                                                </asp:CheckBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="Delete" HeaderStyle-BorderColor="blue">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkboxDel" runat="server" style="color: Black" Text="" Font-Names="arial" Font-Size="11px">
                                                                                                </asp:CheckBox>
                                                                                             </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="60px" HeaderText="View" HeaderStyle-BorderColor="blue">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkboxView" runat="server" style="color: Black" Text="" Font-Names="arial" Font-Size="11px">
                                                                                                </asp:CheckBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    
                                                                                </rwg:BulkEditGridView>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                 </td>
                                                              </tr>
                                                              <tr>
                                                                  <td colspan="4">
                                                                      <table width="100%">
                                                                        <tr>     
                                                                            <td style="width:32%">
                                                                                
                                                                            </td>                                
                                                                            <td style="width:18%">
                                                                                <asp:Button ID="cmdSave" runat="server" Text="" CssClass="savebutton"
                                                                                    EnableTheming="false" OnClick="cmdSave_Click" SkinID="skinBtnSave" />
                                                                            </td>
                                                                            <td style="width:18%">
                                                                                <asp:Button ID="btnCancel" runat="server" Text="" CssClass="cancelbutton" EnableTheming="false"
                                                                                    Visible="true" OnClick="cmdCancel_Click" SkinID="skinBtnCancel" />
                                                                            </td>
                                                                            <td style="width:32%">
                                                                                
                                                                            </td> 
                                                                        </tr>
                                                                     </table>
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
                   
                    <tr style="width: 100%">
                        <td style="width: 90%; text-align: left">
                            <asp:Panel ID="PanelBill" Direction="LeftToRight" runat="server">
                                <table width="100%" style="text-align: left; margin: -3px 0px 0px 0px;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GrdViewCust" DataKeyNames="username" GridLines="Both" Width="100%"
                                            runat="server" AutoGenerateColumns="False" OnRowCreated="GrdViewCust_RowCreated"
                                            AllowPaging="true" DataSourceID="GridSource" OnRowCommand="GrdViewCust_RowCommand" CssClass="someClass">
                                            <RowStyle BorderWidth="1" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:BoundField DataField="username" HeaderText="User" HeaderStyle-BorderColor="Blue"></asp:BoundField>
                                                    <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-BorderColor="Blue"></asp:BoundField>
                                                    <uc:BoundUrlColumn DataField="username" HeaderStyle-HorizontalAlign="Center" HeaderText="Edit" HeaderStyle-BorderColor="blue"
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
                                                <SelectedRowStyle BackColor="#E3F6CE" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                
                            </asp:Panel>
                            
                        </td>
                    </tr>
                    <tr>
                        <asp:ObjectDataSource ID="GridSource" runat="server" DeleteMethod="DeleteUserInfo"
                            SelectMethod="ListUsers" TypeName="BusinessLogic">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:ControlParameter ControlID="GrdViewCust" Name="username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <input type="hidden" value="" id="hdDel" runat="server" />
                        <input type="hidden" id="delFlag" value="0" runat="server" />
                        <asp:HiddenField ID="hdToDelete" Value="0" runat="server" />
                        <asp:Label ID="err" runat="server" Style="color: Red; font-weight: bold; font-family: 'Trebuchet MS';
                                font-size: 11px;" Text=""></asp:Label>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
