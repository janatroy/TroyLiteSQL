<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="UpdateProduct.aspx.cs" Inherits="UpdateProduct"
    Title="Accounts > UpdateProduct" %>

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
                            <asp:Panel ID="purchasePanel" runat="server" Style="width: 75%; display: none">
                                <asp:UpdatePanel ID="updatePnlPurchase" runat="server" RenderMode="Block" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                        <div id="Div1" style="background-color: White;">
                                            <table style="width: 100%;" align="center">
                                                <tr style="width: 100%">
                                                    <td style="width: 100%">
                                                        <table style="text-align: left;" width="100%" cellpadding="3" cellspacing="5">
                                                            <tr>
                                                                <td>
                                                                    <table class="headerPopUp" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                Products Updation
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                           <tr style="width: 100%">
                                                                <td style="width: 100%">
                                                                    <table width="100%" cellpadding="1" cellspacing="1">
                                                                         <tr>
                                                                            
                                                                                        <td class="ControlLabel" style="width: 17%" >
                                                                                            Category *
                                                                                            <asp:CompareValidator ID="cvCatergory" runat="server" ControlToValidate="cmbCategory"
                                                                                                Display="Dynamic" ErrorMessage="Category is Mandatory" Operator="GreaterThan"
                                                                                                Text="*" ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 22%">
                                                                                                <asp:DropDownList ID="cmbCategory" runat="server" AutoPostBack="true" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                    Width="100%" OnSelectedIndexChanged="LoadProducts" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px">
                                                                                                    <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Category</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                             
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 9%">
                                                                                            Product Code
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 22%">
                                                                                
                                                                                                <asp:DropDownList ID="cmbProdAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor = "#90c9fc" 
                                                                                                    CssClass="drpDownListMedium" DataTextField="ProductName" DataValueField="ItemCode"
                                                                                                    OnSelectedIndexChanged="LoadForProduct" ValidationGroup="product" Width="100%" style="border: 1px solid #90c9fc" height="26px">
                                                                                                    <asp:ListItem style="background-color: #90c9fc;" Text="Select Product" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                 
                                                                                        </td>
                                                                                        <td   style="width:15%"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                                                                            
                                                                                        <td class="ControlLabel" style="width: 17%" >
                                                                                            Product Name
                                                                                        </td>
                                                                                        <td  class="ControlDrpBorder" style="width: 22%" >
                                                            
                                                                                                <asp:DropDownList ID="cmbProdName" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                    AutoPostBack="true" OnSelectedIndexChanged="LoadForProductName" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px">
                                                                                                    <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select ItemName</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                                <asp:TextBox ID="lblProdNameAdd" runat="server" CssClass="cssTextBox" ReadOnly="true"
                                                                                                    Visible="false" Width="196px" Enabled="false"></asp:TextBox>
                                                           
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 9%">
                                                                                            Brand
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 22%">
                                                              
                                                                                                <asp:DropDownList ID="cmbBrand" runat="server" Width="100%" AutoPostBack="true" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                    OnSelectedIndexChanged="LoadForBrand" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px">
                                                                                                    <asp:ListItem Selected="True" Value="0" style="background-color: #bce1fe">Select Brand</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                             
                                                                                        </td>
                                                                                        <td style="width: 15%" align="center">
                                                                                                                                                
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 17%" >
                                                                                            Model
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 22%">
                                                                            
                                                                                                <asp:DropDownList ID="cmbModel" runat="server" OnSelectedIndexChanged="LoadForModel" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                    AutoPostBack="true" Width="100%" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px">
                                                                                                    <asp:ListItem Selected="True" Value="0" style="background-color: #bce1fe">Select Model</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                      
                                                                                        </td>
                                                                                        <td>
                                                                                                                                                
                                                                                        </td>
                                                                                        <td align="right" style="width: 22%">
                                                                                                                                               
                                                                                        </td>
                                                                                        <td style="width: 10%"  align="left" valign="middle">
                                                                                             <asp:Button ID="cmdMethod" runat="server" CssClass="Start" 
                                                                                                    EnableTheming="false" OnClick="cmdMethod_Click" Text=""
                                                                                                    ValidationGroup="contact" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                                                                            
                                                                                        <td>
                                                                                            <asp:TextBox ID="lblProdDescAdd" runat="server" SkinID="skinTxtBoxGrid" Enabled="false" Height="28px" Width="60px"
                                                                                                    Visible="false"></asp:TextBox>
                                                                                        </td>
                                                                                        <td></td>
                                                                                        <td></td>
                                                                                        <td   style="width: 15%"></td>
                                                                                    </tr>
                                                                                    <tr style="height:5px">
                                                                         </tr>
                                                                    </table>
                                                                  </td>
                                                               </tr>
                                                               <tr style="width: 100%">
                                                                 <td style="width: 100%">
                                                                        <asp:Panel ID="pnlItems" runat="server" CssClass="pnlPopUp" Visible="false">
                                                                            <asp:UpdatePanel ID="updatePnlProduct" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <div id="contentPopUp">
                                                                                        <table cellpadding="0" cellspacing="3" style="border: 1px solid #5078B3;"
                                                                                            width="100%">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:HiddenField ID="hdFilename" runat="server" Value="0" />
                                                                                                    <asp:HiddenField ID="hdPurchase" runat="server" Value="0" />
                                                                                                    <asp:HiddenField ID="hdStock" runat="server" Value="0" />
                                                                                                </td>
                                                                                            </tr>
                                                                                                
                                                                                            <tr style="width:100%">
                                                                                                <td style="width:100%">
                                                                                                    <asp:GridView ID="GrdViewItems" runat="server" BorderWidth="1px" DataKeyNames="itemcode"
                                                                                                        EmptyDataText="No Products Added." OnPageIndexChanging="GrdViewItems_PageIndexChanging"
                                                                                                        OnRowCancelingEdit="GrdViewItems_RowCancelingEdit" OnRowCreated="GrdViewItems_RowCreated"
                                                                                                        OnRowDataBound="GrdViewItems_RowDataBound" OnRowDeleting="GrdViewItems_RowDeleting"
                                                                                                        OnRowEditing="GrdViewItems_RowEditing" OnRowUpdating="GrdViewItems_RowUpdating"
                                                                                                        OnSelectedIndexChanged="GrdViewItems_SelectedIndexChanged" ShowFooter="false" CssClass="someClass"
                                                                                                        Width="100%">
                                                                                                        <EditRowStyle VerticalAlign="Middle" />
                                                                                                        <RowStyle Font-Bold="false" />
                                                                                                        <FooterStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="Brand" HeaderText="Brand" HeaderStyle-BorderColor="blue" />
                                                                                                            <asp:BoundField DataField="ProductName" HeaderText="Product Name"  HeaderStyle-BorderColor="blue"/>
                                                                                                            <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-BorderColor="blue"/>
                                                                                                            <asp:BoundField DataField="Itemcode" HeaderText="Itemcode"  HeaderStyle-BorderColor="blue"/>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="MRP" HeaderStyle-BorderColor="blue">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtMRP" runat="server" CssClass="cssTextBox"
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="MRP Date" HeaderStyle-BorderColor="blue">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtMRPDate" runat="server" CssClass="cssTextBox"
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="DP" HeaderStyle-BorderColor="blue">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtDP" runat="server" CssClass="cssTextBox"
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="DP Date" HeaderStyle-BorderColor="blue">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtDPDate" runat="server" CssClass="cssTextBox"
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="NLC" HeaderStyle-BorderColor="blue">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtNLC" runat="server" CssClass="cssTextBox"
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="NLC Date" HeaderStyle-BorderColor="blue">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtNLCDate" runat="server" CssClass="cssTextBox"
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Absolute" HeaderStyle-BorderColor="blue">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:DropDownList ID="ddType" runat="server" CssClass="drpDownListMedium"  BackColor = "#90C9FC" height="25px" style="border:1px solid black">
                                                                                                                        <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Stock Level" HeaderStyle-BorderColor="blue">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtReorder" runat="server" CssClass="cssTextBox"
                                                                                                                        ></asp:TextBox>
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
                                                                                                                        <asp:DropDownList ID="ddlPageSelector2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSelector2_SelectedIndexChanged"
                                                                                                                            SkinID="skinPagerDdlBox">
                                                                                                                        </asp:DropDownList>
                                                                                                                    </td>
                                                                                                                    <td  style=" border-color:white; Width:5px">
                                            
                                                                                                                    </td>
                                                                                                                    <td style=" border-color:white">
                                                                                                                        <asp:Button ID="btnFirst" runat="server" CommandArgument="First" CommandName="Page" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                            Text="" />
                                                                                                                    </td>
                                                                                                                    <td style=" border-color:white">
                                                                                                                        <asp:Button ID="btnPrevious" runat="server" CommandArgument="Prev" CommandName="Page" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                            Text="" />
                                                                                                                    </td>
                                                                                                                    <td style=" border-color:white">
                                                                                                                        <asp:Button ID="btnNext" runat="server" CommandArgument="Next" CommandName="Page" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                            Text="" />
                                                                                                                    </td>
                                                                                                                    <td style=" border-color:white">
                                                                                                                        <asp:Button ID="btnLast" runat="server" CommandArgument="Last" CommandName="Page" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                            Text="" />
                                                                                                                    </td>
                                                                                                                 </tr>
                                                                                                              </table>
                                                                                                        </PagerTemplate>
                                                                                                    </asp:GridView>
                                                                                                </td>
                                                                                                            
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                           </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4">
                                                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                                                                                 <ContentTemplate>
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td style="width:25%">
                                                                        
                                                                                        </td>
                                                                                        <td style="width:25%">
                                                                                            <asp:Button ID="cmdUpdate" ValidationGroup="purchaseval" runat="server" Text="" CssClass="savebutton"
                                                                                                EnableTheming="false" OnClick="cmdUpdate_Click" SkinID="skinBtnSave" />
                                                                                            <asp:Button ID="cmdSave" ValidationGroup="purchaseval" runat="server" Text="" CssClass="savebutton"
                                                                                                EnableTheming="false" OnClick="cmdSave_Click" SkinID="skinBtnSave" />
                                                                                        </td>
                                                                                        <td style="width:25%">
                                                                                            <asp:Button ID="btnCancel" runat="server" Text="" CssClass="cancelbutton" EnableTheming="false"
                                                                                                Visible="true" OnClick="cmdCancel_Click" SkinID="skinBtnCancel" />
                                                                                        </td>
                                                                                        <td style="width:25px">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                                                                                <asp:AsyncPostBackTrigger ControlID="cmdUpdate" EventName="Click" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
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
                                            <asp:GridView ID="GrdViewPurchase" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                CssClass="someClass" Width="100.2%" DataKeyNames="PurchaseID" AllowPaging="True"
                                                EmptyDataText="No Purchase Details Found" OnPageIndexChanging="GrdViewPurchase_PageIndexChanging"
                                                OnRowCommand="GrdViewPurchase_RowCommand" OnRowEditing="GrdViewPurchase_RowEditing"
                                                OnSelectedIndexChanged="GrdViewPurchase_SelectedIndexChanged" OnRowCreated="GrdViewPurchase_RowCreated"
                                                OnRowDataBound="GrdViewPurchase_RowDataBound" OnRowDeleting="GrdViewPurchase_RowDeleting">
                                                <Columns>
                                                    <asp:BoundField DataField="PurchaseID" HeaderText="Voucher No" HeaderStyle-Width="50px" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="blue"/>
                                                    <asp:BoundField DataField="TransNo" HeaderText="Trans. No." HeaderStyle-Wrap="false" HeaderStyle-BorderColor="blue"
                                                        HeaderStyle-Width="50px" />
                                                    <asp:BoundField DataField="Billno" HeaderText="Bill No." HeaderStyle-Width="60px" HeaderStyle-BorderColor="blue"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="BillDate" HeaderText="Date" HeaderStyle-Width="65px"  HeaderStyle-BorderColor="blue"/>
                                                    <asp:TemplateField HeaderText="Payment Mode" HeaderStyle-BorderColor="blue">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPaymode" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Supplier" HeaderStyle-Width="130px" HeaderText="Supplier"  HeaderStyle-BorderColor="blue"/>
                                                    <asp:BoundField DataField="Chequeno" Visible="false" HeaderText="Chequeno"  HeaderStyle-BorderColor="blue"/>
                                                    <asp:BoundField DataField="Creditor" HeaderStyle-Width="130px" HeaderText="Creditor"  HeaderStyle-BorderColor="blue"/>
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Width="60px" DataFormatString="{0:F2}"  HeaderStyle-BorderColor="blue"/>
                                                    <asp:BoundField DataField="SalesReturn" HeaderText="Sales Return" HeaderStyle-Wrap="true"  HeaderStyle-BorderColor="blue"/>
                                                    <asp:BoundField DataField="SalesReturnReason" HeaderText="Sales Return - Reason" HeaderStyle-BorderColor="blue"
                                                        Visible="false" />
                                                    <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" Visible="false"  HeaderStyle-BorderColor="blue" />
                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="45px" HeaderText="Edit" HeaderStyle-BorderColor="blue">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="45px" HeaderText="Print" HeaderStyle-BorderColor="blue">
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
