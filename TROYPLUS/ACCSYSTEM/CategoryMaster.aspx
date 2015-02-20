<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" EnableViewState="false"
    AutoEventWireup="true" CodeFile="CategoryMaster.aspx.cs" Inherits="CategoryMaster"
    Title="Inventory > Category Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>
        
            <table style="width: 100%"> 
                <tr style="width: 100%">
                    <td style="width: 100%">
                        
                        
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Categories</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Categories
                                    </td>
                                </tr>
                            </table>
                        <%--<asp:Panel ID="Panel1" runat="server" BackColor="#282828" width="100%">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr style="height:21px; color: #FFFFFF; text-transform: uppercase; font-family: Arial, Helvetica, sans-serif; font-size: x-large;" valign="middle">
                                    <td>
                                        <span>Categories</span>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel> 
                        <cc1:RoundedCornersExtender ID="RCEADD" runat="server" TargetControlID="Panel1">
                        </cc1:RoundedCornersExtender>--%>
                        <%--<div class="searchbg" style="width:994px">--%>
                        <div class="mainConBody">
                            <table cellspacing="2px" cellpadding="3px" border="0" width="100%"
                                        class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 8%"></td>
                                    <td style="width: 15%">
                                        <%--<div style="text-align: right;">
                                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">--%>
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd"
                                                    EnableTheming="false" Text="Add New"></asp:Button>
                                            <%--</asp:Panel>
                                        </div>--%>
                                    </td>
                                    <td style="width: 8%">
                                        <%--<div style="text-align: left;">
                                            <asp:Panel ID="Panel1" runat="server" Width="20px">--%>
                                                Search
                                            <%--</asp:Panel> 
                                        </div>--%>
                                    </td>
                                    <td style="width: 23%" class="cssTextBoxbg" >
                                        <%--<div style="text-align: left;">
                                            <asp:Panel ID="Panel2" runat="server" Width="100px">--%>
                                                <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch" 
                                                ontextchanged="txtSearch_TextChanged"></asp:TextBox>
                                            <%--</asp:Panel> 
                                        </div>--%>
                                    </td>
                                    <td style="width: 23%;"  class="Box">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <%--<asp:Panel ID="Panel3" runat="server" Width="80px">--%>
                                                <asp:DropDownList ID="ddCriteria" runat="server"  Width="150px" Height="23px" style="text-align:center;border:1px solid White "
                                                AutoPostBack="false" 
                                                onselectedindexchanged="ddCriteria_SelectedIndexChanged">
                                                <asp:ListItem Value="CategoryName">Category Name</asp:ListItem>
                                                </asp:DropDownList>
                                            <%--</asp:Panel> --%>
                                        </div>
                                    </td>
                                    <td style="width: 20%" class="tblLeftNoPad" >
                                        <%--<div style="text-align: left;">
                                            <asp:Panel ID="Panel4" runat="server" Width="100px">--%>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="skinBtnSearch" 
                                                    onclick="btnSearch_Click" />
                                            <%--</asp:Panel> 
                                        </div>--%>
                                    </td>
                                    <td  style="width: 20%">
                                            <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl" OnClientClick="window.open('ReportExcelCategory.aspx ','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=1000,width=900,left=210,top=10, scrollbars=yes');"
                                                                    EnableTheming="false"></asp:Button>
                                       </td>
                                </tr>
                            </table>
                            </div>
                        
                            
                            <tr style="width: 100%" align="left">
                                <td>
                                <table width="100%" style="margin: -3px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                                    <div class="mainConDiv3" id="Div5" align="center" >
                                        <asp:GridView ID="GrdCategory" CssClass="someClass" runat="server" DataSourceID="srcGridView" AutoGenerateColumns="False"
                                            OnRowCreated="GrdCategory_RowCreated" Width="520px" OnRowCancelingEdit="GrdCategory_RowCancelingEdit"
                                            OnRowCommand="GrdCategory_RowCommand" EmptyDataText="No Categories found!" OnRowDataBound="GrdCategory_RowDataBound"
                                            AllowPaging="True" DataKeyNames="CategoryId" OnDataBound="GrdCategory_DataBound"
                                            EnableViewState="False" OnPreRender="GrdCategory_PreRender"
                                            OnRowUpdated="GrdCategory_RowUpdated" OnRowUpdating="GrdCategory_RowUpdating"
                                                onselectedindexchanged="GrdCategory_SelectedIndexChanged" PageSize="5">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Product Category" HeaderStyle-BorderColor="Blue">
                                                    <ItemStyle Width="360px" />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCatDescr" runat="server" Width="98%" Text='<%# Bind("CategoryName") %>'
                                                            CssClass="cssTextBox" EnableTheming="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rvDescr" runat="server" ControlToValidate="txtCatDescr"
                                                            Display="Dynamic" EnableClientScript="False" ErrorMessage="Description is mandatory">*</asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategoryDescr" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtAddDescr" runat="server" MaxLength = '50' Width="98%" CssClass="cssTextBox" EnableTheming="false"></asp:TextBox><asp:RequiredFieldValidator
                                                            ID="rvAddDescr" runat="server" ControlToValidate="txtAddDescr" Display="Dynamic"
                                                            EnableClientScript="true" ErrorMessage="Description is mandatory">*</asp:RequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category Level" HeaderStyle-BorderColor="Blue">
                                                    <ItemStyle Width="120px" />
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCatLvl" runat="server" Width="98%" Text='<%# Bind("CategoryLevel") %>'
                                                            CssClass="cssTextBox" EnableTheming="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rvCatlvl" runat="server" ControlToValidate="txtCatLvl"
                                                            Display="Dynamic" EnableClientScript="False" ErrorMessage="Level is mandatory">*</asp:RequiredFieldValidator>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategoryLvl" runat="server" Text='<%# Bind("CategoryLevel") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtAddLvl" runat="server" MaxLength = '50' Width="98%" CssClass="cssTextBox" EnableTheming="false"></asp:TextBox><asp:RequiredFieldValidator
                                                            ID="rvAddlvl" runat="server" ControlToValidate="txtAddLvl" Display="Dynamic"
                                                            EnableClientScript="true" ErrorMessage="Level is mandatory">*</asp:RequiredFieldValidator>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-BorderColor="Blue">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    <FooterStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Edit" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ID="lbUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                                            Text="Update" SkinID="GridUpdate"></asp:ImageButton>
                                                        <asp:ImageButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                            Text="Cancel" SkinID="GridCancel"></asp:ImageButton>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:ImageButton ID="lbInsert" runat="server" SkinID="GridUpdate" CommandName="Insert"
                                                            Text="Save"></asp:ImageButton>
                                                        <asp:ImageButton ID="lblInsCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                            SkinID="GridCancel" Text="Cancel"></asp:ImageButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Blue">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Category?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:Panel ID="divDelete" runat="server">
                                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server">
                                                            </asp:ImageButton>
                                                        </asp:Panel>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("CategoryId") %>' />
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
                            <tr style="height:10%;width:100%">
                                <td style="width: 100%" align="left">
                                    <asp:ObjectDataSource ID="srcGridView" runat="server" InsertMethod="InsertRecord"
                                        SelectMethod="GetCategoryData" TypeName="BusinessLogic" OnUpdating="srcGridView_Updating"
                                        DeleteMethod="DeleteCategory" OnDeleting="GridSource_Deleting">
                                        <SelectParameters>
                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                        </SelectParameters>
                                        <DeleteParameters>
                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                            <asp:Parameter Name="CategoryID" Type="Int32" />
                                            <asp:Parameter Name="Username" Type="String" />
                                        </DeleteParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        <%--</div>--%>
                    </td>
                </tr>
            </table>

            <%--0066FF--%>
            <%--<asp:Panel ID="Panel2" runat="server" BackColor="#0066FF" width="100%">
                <table>
                    <tr style="height:21px">
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel> 
            <cc1:RoundedCornersExtender ID="RCEEADD" runat="server" TargetControlID="Panel2">
            </cc1:RoundedCornersExtender>--%>
                            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
