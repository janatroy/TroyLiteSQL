<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="ProductsUpdation.aspx.cs" Inherits="ProductsUpdation"
    Title="Supplier > Products Updation" %>

<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script runat="server" type="text/javascript">

    </script>
    <style id="Style1" runat="server">
         .sometd
        {
            font-size: 12px;
            border : 1px solid Gray ;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePnlMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
            <div style="">
                <table style="width: 100%;" align="center">
                    <tr style="width: 100%">
                        <td style="width: 100%" valign="middle">
                            <table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Products Updation
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
                                                Bill No.
                                            </td>
                                            <td style="width: 22%; text-align: center" class="tblLeft cssTextBoxbg">
                                                <asp:TextBox ValidationGroup="search" ID="txtBillnoSrc" runat="server" MaxLength="8"
                                                    CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtBillnoSrc"
                                                    FilterType="Numbers" />
                                            </td>
                                            <td style="width: 8%" align="center">
                                                Trans. No.
                                            </td>
                                            <td style="width: 22%" class="tblLeft cssTextBoxbg">
                                                <asp:TextBox ValidationGroup="search" ID="txtTransNo" runat="server" MaxLength="8"
                                                    CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtTransNo"
                                                    FilterType="Numbers" />
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
                            <cc1:ModalPopupExtender ID="ModalPopupPurchase" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="BtnPopUpCancel1" DynamicServicePath="" Enabled="True" PopupControlID="purchasePanel"
                                RepositionMode="RepositionOnWindowResizeAndScroll" TargetControlID="dummyPurchase">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="purchasePanel" runat="server" Style="width: 45%; display: none">
                                <asp:UpdatePanel ID="updatePnlPurchase" runat="server" RenderMode="Block" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                        <div id="Div1" style="background-color: White;">
                                            <table style="width: 100%; border:1px solid black" align="center">
                                                <tr style="width: 100%">
                                                    <td style="width: 100%">
                                                        <table style="text-align: left; border:1px solid black"" width="100%" cellpadding="3" cellspacing="5">
                                                            <tr>
                                                                <td>
                                                                    <table class="headerPopUp" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                Products Details
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                    <table width="100%" cellpadding="1" cellspacing="1">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table style="width:100%" cellpadding="3" cellspacing="2">
                                                                                                <tr>
                                                                                                    <td style="width: 15%">
                                                                                                        
                                                                                                    </td>
                                                                                                    <td class="ControlLabel" style="width: 15%" >
                                                                                                        Category *
                                                                                                        <asp:RequiredFieldValidator ID="reqSuppllier" runat="server" ControlToValidate="cmbCategory"
                                                                                                            CssClass="lblFont" ErrorMessage="Please select any one Category" InitialValue="0" Text="*"
                                                                                                            ValidationGroup="purchaseval"></asp:RequiredFieldValidator>
                                                                                                    </td>
                                                                                                    <td class="ControlDrpBorder" style="width: 25%">
                                                                                                        <asp:DropDownList ID="cmbCategory" runat="server" AutoPostBack="true" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                            Width="100%" OnSelectedIndexChanged="LoadProducts" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px">
                                                                                                            <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Category</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    
                                                                                                    <td style="width:38%"></td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width:15%"></td>
                                                                                                    <td class="ControlLabel" style="width: 15%">
                                                                                                        
                                                                                                        Brand
                                                                                                    </td>
                                                                                                    <td class="ControlDrpBorder" style="width: 25%">
                                                                                                        <asp:DropDownList ID="cmbBrand" runat="server" Width="100%" AutoPostBack="true" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                             OnSelectedIndexChanged="LoadForBrand" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px">
                                                                                                             <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Brand</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                    </td>
                                                                                                    <td style="width: 30%" align="center">
                                                                                                                                                
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" colspan="4">
                                                                                                        <table style="width: 100%">
                                                                                                            <tr>
                                                                                                                <td style="width: 25%">
                                                                                                        
                                                                                                                </td>
                                                                                                                <td style="width: 15%">
                                                                                                                    <asp:Button ID="Button2" runat="server" CssClass="Start6" 
                                                                                                                            EnableTheming="false" OnClick="cmdMetho_Click" Text=""
                                                                                                                            ValidationGroup="purchaseval" />
                                                                                                                </td>
                                                                                                                <td style="width: 15%">
                                                                                                                    <asp:Button ID="btnCancel" runat="server" Text="" CssClass="cancelbutton6" EnableTheming="false"
                                                                                                                        Visible="true" OnClick="cmdCancel_Click" SkinID="skinBtnCancel" />
                                                                                                                </td>
                                                                                                                <td style="width: 25%">
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" colspan="4">
                                                                                                        <cc1:ModalPopupExtender ID="ModalPopupProduct" runat="server" 
                                                                                                            BackgroundCssClass="modalBackground" CancelControlID="CancelPopUp"
                                                                                                            DynamicServicePath="" Enabled="True" PopupControlID="pnlItems" 
                                                                                                            TargetControlID="ShowPopUp">
                                                                                                        </cc1:ModalPopupExtender>
                                                                                                        <input id="ShowPopUp" runat="server" style="display: none" type="button" />
                                                                                                        &nbsp;
                                                                                                        <input ID="CancelPopUp" runat="server" style="display: none" type="button"></input>
                                                                                                        <asp:Panel ID="pnlItems" runat="server" CssClass="pnlPopUp" Width="90%">
                                                                                                            <asp:UpdatePanel ID="updatePnlProduct" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                    <div id="contentPopUp">
                                                                                                                        <table cellpadding="0" cellspacing="3" class="tblLeft" style="border: 1px solid #5078B3;"
                                                                                                                            width="100%">
                                                                                                                            <tr>
                                                                                                                                <td colspan="4">
                                                                                                                                    <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                               Bulk Products Updation
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td colspan="4">
                                                                                                                                    <table style="border: 1px solid #5078B3" cellpadding="3" cellspacing="2"
                                                                                                                                        width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                <div id="div" runat="server" style="height:300px; overflow:scroll">
                                                                                                                                                    <%--<asp:GridView ID="GrdViewItems" runat="server" BorderWidth="1px" DataKeyNames="itemcode"
                                                                                                                                                    EmptyDataText="No Items"
                                                                                                                                                    OnRowCreated="GrdViewItems_RowCreated"
                                                                                                                                                    OnRowDataBound="GrdViewItems_RowDataBound"
                                                                                                                                             
                                                                                                                                                    OnSelectedIndexChanged="GrdViewItems_SelectedIndexChanged" ShowFooter="false" CssClass="someClass"
                                                                                                                                                    Width="100%">
                                                                                                                                                    <EditRowStyle VerticalAlign="Middle" />
                                                                                                                                                    <RowStyle Font-Bold="false" />
                                                                                                                                                    <FooterStyle CssClass="HeadataRow" Font-Bold="true" Height="27px" />--%>
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
                                                                                                                                                            <asp:BoundField DataField="Brand" HeaderText="Brand" HeaderStyle-BorderColor="Gray" ReadOnly="true" ApplyFormatInEditMode="false"  HeaderStyle-Width="50px"/>
                                                                                                                                                            <asp:BoundField DataField="ProductName" HeaderText="Product Name"  HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px" ReadOnly="true" ApplyFormatInEditMode="false"/>
                                                                                                                                                            <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px" ReadOnly="true" ApplyFormatInEditMode="false"/>
                                                                                                                                                            <asp:BoundField DataField="Itemcode" HeaderText="Itemcode"  HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px" ReadOnly="true" ApplyFormatInEditMode="false"/>
                                                                                                                                                            <asp:BoundField DataField="CategoryID" HeaderText="CategoryID" HeaderStyle-BorderColor="Gray" ReadOnly="true" ApplyFormatInEditMode="false"  HeaderStyle-Width="10px"/>
                                                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="MRP" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtMRP" runat="server" Width="60px" Text='<%# Bind("Rate") %>'
                                                                                                                                                                        ></asp:TextBox>
                                                                                                                                                               </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="MRP Eff Date" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtMRPDate" runat="server" Width="60px" Text='<%# Bind("MRPEffDate") %>'
                                                                                                                                                                        ></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="DP" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtDP" runat="server" Width="60px" Text='<%# Bind("Dealerrate") %>'
                                                                                                                                                                        ></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="DP Eff Date" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtDPDate" runat="server" Width="60px" Text='<%# Bind("DPEffDate") %>'
                                                                                                                                                                        ></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="NLC" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtNLC" runat="server" Width="60px" Text='<%# Bind("NLC") %>'
                                                                                                                                                                        ></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="NLC Eff Date" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtNLCDate" runat="server" Width="60px" Text='<%# Bind("NLCEFFDate") %>'
                                                                                                                                                                        ></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Stock Level" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtReorder" runat="server" Width="60px" Text='<%# Bind("ROL") %>'
                                                                                                                                                                        ></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Sell Vat" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="txtVat" runat="server" Width="60px" Text='<%# Bind("vat") %>'
                                                                                                                                                                        ></asp:TextBox>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Buy Vat" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px" Visible="False">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:TextBox ID="tBuyVat" runat="server" Width="60px" Text='<%# Bind("BuyVat") %>'
                                                                                                                                                                        ></asp:TextBox>
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
                                                                                                                                    <table style="border: 1px solid #5078B3" cellpadding="3" cellspacing="2"
                                                                                                                                        width="100%" align="center">
                                                                                                                                        <tr>
                                                                                                                                            <td style="width:30%">
                                                                                                                                                <asp:DropDownList ID="cmbProdAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor = "#90c9fc" 
                                                                                                                                                                    CssClass="drpDownListMedium" DataTextField="ProductName" DataValueField="ItemCode"
                                                                                                                                                                    OnSelectedIndexChanged="LoadForProduct" ValidationGroup="product" Width="100%" style="border: 1px solid #90c9fc" height="26px" Visible="False">
                                                                                                                                                                    <asp:ListItem style="background-color: #90c9fc;" Text="Select Product" Value="0"></asp:ListItem>
                                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </td>
                                                                                                                                            <td style="width:18%" align="right">
                                                                                                                                                <asp:Button ID="cmdsave" runat="server" CssClass="savebutton1231" 
                                                                                                                                                     EnableTheming="false" OnClick="cmdsave_Click" Text="" />
                                                                                                                                            </td>
                                                                                                                                            <td style="width:18%" align="left">
                                                                                                                                                <asp:Button ID="cmdprodcancel" runat="server" Text="" CssClass="cancelbutton6" EnableTheming="false"
                                                                                                                                                Visible="true" OnClick="cmdprodcancel_Click" SkinID="skinBtnCancel" />
                                                                                                                                            </td>
                                                                                                                                            <td style="width:37%">
                                                                                                                                
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
                            <asp:ObjectDataSource ID="empSrc" runat="server" SelectMethod="ListExecutive" TypeName="BusinessLogic">
                                <SelectParameters>
                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitor"
                                TypeName="BusinessLogic"></asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                <SelectParameters>
                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                   
                    <tr style="width: 100%">
                        <td style="width: 90%; text-align: left">
                            <asp:Panel ID="PanelBill" Direction="LeftToRight" runat="server">
                                <table width="100%" style="text-align: left; margin: -3px 0px 0px 0px;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GrdViewPurchase" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                CssClass="sometd" Width="100.2%" DataKeyNames="PurchaseID" AllowPaging="True"
                                                EmptyDataText="No Details Found" OnPageIndexChanging="GrdViewPurchase_PageIndexChanging"
                                                OnRowCommand="GrdViewPurchase_RowCommand" OnRowEditing="GrdViewPurchase_RowEditing"
                                                OnSelectedIndexChanged="GrdViewPurchase_SelectedIndexChanged" OnRowCreated="GrdViewPurchase_RowCreated"
                                                OnRowDataBound="GrdViewPurchase_RowDataBound" OnRowDeleting="GrdViewPurchase_RowDeleting">
                                                <Columns>
                                                    <asp:BoundField DataField="PurchaseID" HeaderText="Voucher No" HeaderStyle-Width="50px" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray"/>
                                                    <asp:BoundField DataField="TransNo" HeaderText="Trans. No." HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray"
                                                        HeaderStyle-Width="50px" />
                                                    <asp:BoundField DataField="Billno" HeaderText="Bill No." HeaderStyle-Width="60px" HeaderStyle-BorderColor="Gray"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="BillDate" HeaderText="Date" HeaderStyle-Width="65px"  HeaderStyle-BorderColor="Gray"/>
                                                    <asp:TemplateField HeaderText="Payment Mode" HeaderStyle-BorderColor="blue">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPaymode" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Supplier" HeaderStyle-Width="130px" HeaderText="Supplier"  HeaderStyle-BorderColor="Gray"/>
                                                    <asp:BoundField DataField="Chequeno" Visible="false" HeaderText="Chequeno"  HeaderStyle-BorderColor="Gray"/>
                                                    <asp:BoundField DataField="Creditor" HeaderStyle-Width="130px" HeaderText="Creditor"  HeaderStyle-BorderColor="Gray"/>
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Width="60px" DataFormatString="{0:F2}"  HeaderStyle-BorderColor="Gray"/>
                                                    <asp:BoundField DataField="SalesReturn" HeaderText="Sales Return" HeaderStyle-Wrap="true"  HeaderStyle-BorderColor="Gray"/>
                                                    <asp:BoundField DataField="SalesReturnReason" HeaderText="Sales Return - Reason" HeaderStyle-BorderColor="Gray"
                                                        Visible="false" />
                                                    <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" Visible="false"  HeaderStyle-BorderColor="Gray" />
                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="45px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="45px" HeaderText="Print" HeaderStyle-BorderColor="Gray">
                                                        <ItemTemplate>
                                                            <a href='<%# DataBinder.Eval(Container, "DataItem.PurchaseID", "javascript:PrintItem({0});") %>'>
                                                                <img alt="Print" border="0" src="App_Themes/DefaultTheme/Images/Print.png">
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="45px" HeaderStyle-BorderColor="Gray">
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Purchase?"
                                                                runat="server">
                                                            </cc1:ConfirmButtonExtender>
                                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                            <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("PurchaseID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerTemplate>
                                                    <table style=" border-color:white">
                                                        <tr style=" border-color:white">
                                                            <td style=" border-color:white">
                                                                Goto Page
                                                            </td>
                                                            <td style=" border-color:white">
                                                                <asp:DropDownList ID="ddlPageSelector" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                                                                    runat="server" AutoPostBack="true"  Width="70px" Height="24px" style="border:1px solid blue">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style=" border-color:white; Width:5px">
                                            
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
                                <input type="hidden" value="" id="hdDel" runat="server" />
                                <input type="hidden" id="delFlag" value="0" runat="server" />
                                <asp:HiddenField ID="hdToDelete" Value="0" runat="server" />
                            </asp:Panel>
                            <asp:Label ID="err" runat="server" Style="color: Red; font-weight: bold; font-family: 'Trebuchet MS';
                                font-size: 11px;" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
