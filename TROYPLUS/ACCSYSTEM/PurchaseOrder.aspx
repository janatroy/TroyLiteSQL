<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="PurchaseOrder.aspx.cs" Inherits="PurchaseOrder"
    Title="Supplier > Purchase Order" %>

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
                            <div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Purchase Order</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
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
                                            <td style="width: 18%; text-align: center" class="tblLeft cssTextBoxbg">
                                                <asp:TextBox ValidationGroup="search" ID="txtBillnoSrc" runat="server" MaxLength="8"
                                                    CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtBillnoSrc"
                                                    FilterType="Numbers" />
                                            </td>
                                            <td align="right" style="width: 10%">
                                                Date
                                            </td>
                                            <td class="cssTextBoxbg" style="width: 19%">
                                                <asp:TextBox ID="txtDate" runat="server" Width="92%" MaxLength="10" CssClass="cssTextBox"></asp:TextBox>
                                                <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                    PopupButtonID="bt3" PopupPosition="BottomLeft" TargetControlID="txtDate">
                                                </cc1:CalendarExtender>
                                                <asp:ImageButton ID="bt3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                    Width="20px" runat="server" />
                                            </td>
                                            <td style="width: 2%">
                                                <%--<asp:TextBox ValidationGroup="search" ID="txtTransNo" runat="server" MaxLength="8"
                                                    CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtTransNo"
                                                    FilterType="Numbers" />--%>
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
                            <asp:Panel ID="purchasePanel" runat="server" Style="width: 63%; display: none">
                                <asp:UpdatePanel ID="updatePnlPurchase" runat="server" RenderMode="Block" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                        <div id="Div1" class="divArea" style="background-color: White;">
                                            <table style="width: 100%;" align="center">
                                                <tr style="width: 100%">
                                                    <td style="width: 100%">
                                                        <table style="text-align: left;" width="100%" cellpadding="3" cellspacing="5">
                                                            <tr>
                                                                <td>
                                                                    <table class="headerPopUp" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                Purchase Order Details
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td >
                                                                    <cc1:TabContainer ID="tabs2" runat="server" Width="100%" >
                                                                        <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Purchase Details" >
                                                                            <HeaderTemplate>
                                                                                Purchase Order Details
                                                                            </HeaderTemplate>
                                                                            <ContentTemplate>
                                                                                <table width="100%" cellpadding="1" cellspacing="1">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <table width="730px" cellpadding="3" cellspacing="1">
                                                                                                <tr>
                                                                                                    <td style="width:17%;">
                                                                                                    </td>
                                                                                                   <td class="ControlLabel" style="width:25%;">
                                                                                                        Bill No. *
                                                                                                        <asp:RequiredFieldValidator ID="rvBill" runat="server" ControlToValidate="txtBillno"
                                                                                                            CssClass="lblFont" Display="Dynamic" ErrorMessage="Bill No. is mandatory" ValidationGroup="purchaseval">*</asp:RequiredFieldValidator>
                                                                                                    </td>
                                                                                                    <td class="ControlTextBox3" style="width:24%;">
                                                                                                        <asp:TextBox ID="txtBillno" runat="server" MaxLength="8" CssClass="cssTextBox" BackColor = "#90c9fc" Width="80%"
                                                                                                                    ValidationGroup="purchaseval" BorderStyle="NotSet" Height="23px"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td style="width:25%;">
                                                                                                    </td>
                                                                                               </tr>
                                                                                               <tr>
                                                                                               </tr>
                                                                                               <tr>
                                                                                                    <td style="width:17%;">
                                                                                                    </td>
                                                                                                    <td class="ControlLabel" style="width:15%;">
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBillDate"
                                                                                                            CssClass="lblFont" Display="Dynamic" ErrorMessage="BillDate is mandatory" Text="*"
                                                                                                            ValidationGroup="purchaseval"></asp:RequiredFieldValidator>
                                                                                                        <asp:RangeValidator ID="rvBillDate" runat="server" ControlToValidate="txtBillDate"
                                                                                                            ErrorMessage="Purchase date cannot be future date." Text="*" Type="Date" ValidationGroup="purchaseval"></asp:RangeValidator>
                                                                                                        Bill Date *
                                                                                                    </td>
                                                                                                    <td class="ControlTextBox3" style="width:24%;">
                                                                                                        <asp:TextBox ID="txtBillDate" runat="server" CssClass="cssTextBox" MaxLength="10" Height="23px" BackColor = "#90c9fc" 
                                                                                                              ValidationGroup="purchaseval" Width="80%"></asp:TextBox>
                                                                                                        <cc1:CalendarExtender ID="calBillDate" runat="server" Format="dd/MM/yyyy"
                                                                                                             OnClientDateSelectionChanged="checkDate" PopupButtonID="btnBillDate"
                                                                                                                TargetControlID="txtBillDate" Enabled="True">
                                                                                                        </cc1:CalendarExtender>
                                                                                                   </td>
                                                                                                   <td style="width:25%;">
                                                                                                        &nbsp;
                                                                                                        <asp:ImageButton ID="btnBillDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="False"
                                                                                                                Width="20px" runat="server" />
                                                                                                   </td> 
                                                                                               </tr>
                                                                                               <tr>
                                                                                               </tr>
                                                                                               <tr>
                                                                                                    <td style="width:17%;">
                                                                                                    </td>
                                                                                                   <td class="ControlLabel" style="width:25%;">
                                                                                                        <asp:RequiredFieldValidator ID="reqSuppllier" runat="server" ControlToValidate="cmbSupplier"
                                                                                                            CssClass="lblFont" ErrorMessage="Supplier is mandatory" InitialValue="0" Text="*"
                                                                                                            ValidationGroup="purchaseval"></asp:RequiredFieldValidator>
                                                                                                        Supplier *
                                                                                                    </td>
                                                                                                    <td  style="width:24%;"  class="ControlDrpBorder" >
                                                                                                        <%--<asp:Panel ID="Panel18" runat="server" Width="100px">--%>
                                                                                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                                                                <Triggers>
                                                                                                                    <asp:AsyncPostBackTrigger ControlID="tabs2$TabPanel1$drpSalesReturn" EventName="SelectedIndexChanged" />
                                                                                                                </Triggers>
                                                                                                                <ContentTemplate>
                                                                                                                    <asp:DropDownList ID="cmbSupplier" runat="server" AppendDataBoundItems="true" AutoCompleteMode="Suggest"
                                                                                                                        AutoPostBack="false" DataTextField="LedgerName" DataValueField="LedgerID" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                                        ValidationGroup="purchaseval" style="border: 1px solid #90c9fc" height="26px">
                                                                                                                        <asp:ListItem style="background-color: #90c9fc; color: Black" Text="Select Supplier"
                                                                                                                            Value="0"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                        <%--</asp:Panel> --%>
                                                                                                    </td>
                                                                                                    
                                                                                                    <td style="width:25%;">
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
                                                                                                        <asp:Panel ID="pnlItems" runat="server" CssClass="pnlPopUp" Width="750px">
                                                                                                            <asp:UpdatePanel ID="updatePnlProduct" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                    <div id="contentPopUp">
                                                                                                                        <table cellpadding="0" cellspacing="3" class="tblLeft" style="border: 1px solid #5078B3;"
                                                                                                                            width="100%">
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td>
                                                                                                                                                Product Details
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <table style="border: 1px solid #5078B3" cellpadding="3" cellspacing="2"
                                                                                                                                        width="100%">
                                                                                                                                        <tr>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                        </tr>
                                                                                                                                        <tr id="rowBarcode" runat="server">
                                                                                                                                            <td runat="server" class="ControlLabel" style="width: 17%">
                                                                                                                                                Barcode
                                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBarcode"
                                                                                                                                                    CssClass="lblFont" Text="BarCode is mandatory" ValidationGroup="lookUp"></asp:RequiredFieldValidator>
                                                                                                                                            </td>
                                                                                                                                            <td runat="server" class="ControlTextBox3" style="width: 22%">
                                                                                                                                                <asp:TextBox ID="txtBarcode" runat="server" CssClass="cssTextBox" Width="50px"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                            <td runat="server"  style="width: 9%">
                                                                                                                                                <asp:Button ID="cmdBarcode" runat="server" OnClick="txtBarcode_Populated" SkinID="skinBtnMedium"
                                                                                                                                                    Text="Lookup Product" ValidationGroup="lookUp" />
                                                                                                                                            </td>
                                                                                                                                            <td  style="width: 22%"></td>
                                                                                                                                            <td  style="width: 15%"></td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            
                                                                                                                                            <td class="ControlLabel" style="width: 17%" >
                                                                                                                                                Category *
                                                                                                                                                <asp:CompareValidator ID="cvCatergory" runat="server" ControlToValidate="cmbCategory"
                                                                                                                                                    Display="Dynamic" ErrorMessage="Category is Mandatory" Operator="GreaterThan"
                                                                                                                                                    Text="*" ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                            </td>
                                                                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                                <%--<asp:Panel ID="Panel1" runat="server" Width="90%">--%>
                                                                                                                                                    <asp:DropDownList ID="cmbCategory" runat="server" AutoPostBack="true" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                                                                        Width="100%" OnSelectedIndexChanged="LoadProducts" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px">
                                                                                                                                                        <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select Category</asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                <%--</asp:Panel>--%> 
                                                                                                                                            </td>
                                                                                                                                            <td class="ControlLabel" style="width: 9%">
                                                                                                                                                Product Code
                                                                                                                                            </td>
                                                                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                                <%--<asp:Panel ID="Panel4" runat="server"  Width="90%">--%>
                                                                                                                                                    <asp:DropDownList ID="cmbProdAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor = "#90c9fc" 
                                                                                                                                                        CssClass="drpDownListMedium" DataTextField="ProductName" DataValueField="ItemCode"
                                                                                                                                                        OnSelectedIndexChanged="LoadForProduct" ValidationGroup="product" Width="100%" style="border: 1px solid #90c9fc" height="26px">
                                                                                                                                                        <asp:ListItem style="background-color: #90c9fc;" Text="Select Product" Value="0"></asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                <%--</asp:Panel>--%> 
                                                                                                                                            </td>
                                                                                                                                            <td   style="width:15%"></td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            
                                                                                                                                            <td class="ControlLabel" style="width: 17%" >
                                                                                                                                                Product Name
                                                                                                                                            </td>
                                                                                                                                            <td  class="ControlDrpBorder" style="width: 22%" >
                                                                                                                                                <%--<asp:Panel ID="Panel5" runat="server" Width="90%">--%>
                                                                                                                                                    <asp:DropDownList ID="cmbProdName" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                                                                        AutoPostBack="true" OnSelectedIndexChanged="LoadForProductName" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px">
                                                                                                                                                        <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">Select ItemName</asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                    <asp:TextBox ID="lblProdNameAdd" runat="server" CssClass="cssTextBox" ReadOnly="true"
                                                                                                                                                        Visible="false" Width="196px" Enabled="false"></asp:TextBox>
                                                                                                                                                <%--</asp:Panel> --%>
                                                                                                                                            </td>
                                                                                                                                            <td class="ControlLabel" style="width: 9%">
                                                                                                                                                Brand
                                                                                                                                            </td>
                                                                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                                <%--<asp:Panel ID="Panel6" runat="server"  Width="90%">--%>
                                                                                                                                                    <asp:DropDownList ID="cmbBrand" runat="server" Width="100%" AutoPostBack="true" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                                                                        OnSelectedIndexChanged="LoadForBrand" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px">
                                                                                                                                                        <asp:ListItem Selected="True" Value="0" style="background-color: #bce1fe">Select Brand</asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                <%--</asp:Panel> --%>
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 15%" align="center">
                                                                                                                                                
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td class="ControlLabel" style="width: 17%" >
                                                                                                                                                Model
                                                                                                                                            </td>
                                                                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                                <%--<asp:Panel ID="Panel7" runat="server" Width="90%">--%>
                                                                                                                                                    <asp:DropDownList ID="cmbModel" runat="server" OnSelectedIndexChanged="LoadForModel" CssClass="drpDownListMedium" BackColor = "#90c9fc" 
                                                                                                                                                        AutoPostBack="true" Width="100%" AppendDataBoundItems="True" style="border: 1px solid #90c9fc" height="26px">
                                                                                                                                                        <asp:ListItem Selected="True" Value="0" style="background-color: #bce1fe">Select Model</asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                <%--</asp:Panel> --%>
                                                                                                                                            </td>
                                                                                                                                            <td>
                                                                                                                                                
                                                                                                                                            </td>
                                                                                                                                            <td align="right" style="width: 22%">
                                                                                                                                               
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 10%"  align="left" valign="middle">
                                                                                                                                                <asp:Panel ID="Panel9" runat="server"  Width="70%">
                                                                                                                                                    
                                                                                                                                                </asp:Panel> 
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
                                                                                                                            
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <table style="width: 100%; border: 1px solid #5078B3;" cellpadding="3" cellspacing="2">
                                                                                                                                        <tr style="height:5px">
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                                                                Rate
                                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRateAdd"  
                                                                                                                                                    ErrorMessage="Product Rate is mandatory" Text="*" ValidationGroup="product"></asp:RequiredFieldValidator>
                                                                                                                                                
                                                                                                                                            </td>
                                                                                                                                            <td class="ControlTextBox3" style="width: 26%">
                                                                                                                                                  <asp:TextBox ID="txtRateAdd" runat="server" ValidationGroup="product" BackColor = "#90c9fc" Width="100%"
                                                                                                                                                        Height="20px"  CssClass="cssTextBox" ></asp:TextBox>
                                                                                                                                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                        TargetControlID="txtRateAdd" ValidChars="." />
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 11%" class="ControlLabel">
                                                                                                                                                NLP
                                                                                                                                            </td>
                                                                                                                                            <td  class="ControlTextBox3"  style="width: 25%">
                                                                                                                                                <asp:TextBox ID="txtNLPAdd" runat="server" ValidationGroup="product"  Width="100%" BackColor = "#90c9fc" CssClass="cssTextBox" 
                                                                                                                                                        Height="20px"></asp:TextBox>
                                                                                                                                                <cc1:FilteredTextBoxExtender ID="fltNLP" runat="server" FilterType="Numbers, Custom"
                                                                                                                                                        TargetControlID="txtNLPAdd" ValidChars="." />
                                                                                                                                            </td>
                                                                                                                                            <td  style="width: 12%">
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                                                                Qty.
                                                                                                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQtyAdd"
                                                                                                                                                    Display="Dynamic" ErrorMessage="Qty. must be greater than Zero!!" Operator="GreaterThan"
                                                                                                                                                    Text="*" ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txtQtyAdd"
                                                                                                                                                    ErrorMessage="Qty. is mandatory" Text="*" ValidationGroup="product"></asp:RequiredFieldValidator>
                                                                                                                                            
                                                                                                                                            </td>
                                                                                                                                            
                                                                                                                                            <td  class="ControlTextBox3" style="width: 26%">
                                                                                                                                                 <asp:TextBox ID="txtQtyAdd" runat="server" CssClass="cssTextBox" ValidationGroup="product"  Width="100%" BackColor = "#90c9fc" 
                                                                                                                                                        Height="23px"></asp:TextBox>
                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                        TargetControlID="txtQtyAdd" ValidChars="." />
                                                                                                                                            </td>
                                                                                                                                            
                                                                                                                                            <td style="width: 11%" class="ControlLabel">
                                                                                                                                                Disc (%)
                                                                                                                                                <asp:RegularExpressionValidator ID="regextxtPercentage" runat="server" ControlToValidate="lblDisAdd"
                                                                                                                                                    Display="None" ErrorMessage="Invalid % in Discount" Text="*" ValidationExpression="^\d{1,3}($|\.\d{1,2}$)"
                                                                                                                                                    ValidationGroup="product"></asp:RegularExpressionValidator>
                                                                                                                                                <asp:RangeValidator ID="cvDisc" runat="server" ControlToValidate="lblDisAdd" Display="Dynamic"
                                                                                                                                                    ErrorMessage="Discount cannot be Greater than 100% and Less than 0%" MaximumValue="100"
                                                                                                                                                    MinimumValue="0" Text="*" Type="Double"></asp:RangeValidator>
                                                                                                                                                
                                                                                                                                            </td>
                                                                                                                                            
                                                                                                                                            <td  class="ControlTextBox3" style="width: 25%">
                                                                                                                                                   <asp:TextBox ID="lblDisAdd" runat="server" CssClass="cssTextBox" Height="23px"  Width="100%" BackColor = "#90c9fc" ></asp:TextBox>
                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                        TargetControlID="lblDisAdd" ValidChars="." />
                                                                                                                                                    <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="cssTextBox" Width="20%" Height="23px"  BackColor = "#90c9fc" ></asp:TextBox>--%>
                                                                                                                                            </td>
                                                                                                                                            <td  style="width: 12%">
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                                                                VAT (%)
                                                                                                                                                <asp:RegularExpressionValidator ID="regextxtPercentage2" runat="server" ControlToValidate="lblVATAdd"
                                                                                                                                                    Display="None" ErrorMessage="Invalid % in VAT" Text="*" ValidationExpression="^\d{1,3}($|\.\d{1,2}$)"
                                                                                                                                                    ValidationGroup="product"></asp:RegularExpressionValidator>
                                                                                                                                                <asp:RangeValidator ID="cvVAT" runat="server" ControlToValidate="lblVATAdd" Display="Dynamic"
                                                                                                                                                    ErrorMessage="VAT cannot be Greater than 100% and Less than 0%" MaximumValue="100"
                                                                                                                                                    MinimumValue="0" Text="*" Type="Double"></asp:RangeValidator>
                                                                                                                                            
                                                                                                                                            </td>
                                                                                                                                            <td class="ControlTextBox3" style="width: 26%">
                                                                                                                                                <asp:TextBox ID="lblVATAdd" runat="server" CssClass="cssTextBox" Height="23px" Width="100%"  BackColor = "#90c9fc" > </asp:TextBox>
                                                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender55" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                        TargetControlID="lblVATAdd" ValidChars="." />
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 11%" class="ControlLabel">
                                                                                                                                                CST (%)
                                                                                                                                                <asp:RangeValidator ID="cvCST" runat="server" ControlToValidate="lblCSTAdd" Display="Dynamic"
                                                                                                                                                    ErrorMessage="CST cannot be Greater than 100% and Less than 0%" MaximumValue="100"
                                                                                                                                                    MinimumValue="0" Text="*" Type="Double"></asp:RangeValidator>
                                                                                                                                                    
                                                                                                                                            </td>
                                                                                                                                            <td  class="ControlTextBox3" style="width: 25%">
                                                                                                                                                 <asp:TextBox ID="lblCSTAdd" runat="server" CssClass="cssTextBox" Text="0" Width="100%" ValidationGroup="product"  BackColor = "#90c9fc"  Height="23px"
                                                                                                                                                        ></asp:TextBox>
                                                                                                                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                        TargetControlID="lblCSTAdd" ValidChars="." />
                                                                                                                                            </td>
                                                                                                                                            <td  style="width: 12%">
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 20%">
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 26%">
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 11%" class="ControlLabel">
                                                                                                                                                Disc Amt
                                                                                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="lbldiscamt"
                                                                                                                                                    Display="None" ErrorMessage="Invalid % in Discount" Text="*" ValidationExpression="^\d{1,3}($|\.\d{1,2}$)"
                                                                                                                                                    ValidationGroup="product"></asp:RegularExpressionValidator>
                                                                                                                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="lbldiscamt" Display="Dynamic"
                                                                                                                                                    ErrorMessage="Discount cannot be Greater than 100% and Less than 0%" MaximumValue="100"
                                                                                                                                                    MinimumValue="0" Text="*" Type="Double"></asp:RangeValidator>
                                                                                                                                                
                                                                                                                                            </td>
                                                                                                                                            
                                                                                                                                            <td  class="ControlTextBox3" style="width: 25%">
                                                                                                                                                   <asp:TextBox ID="lbldiscamt" runat="server" CssClass="cssTextBox" Width="100%" Height="23px"  BackColor = "#90c9fc" ></asp:TextBox>
                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                        TargetControlID="lbldiscamt" ValidChars="." />
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 12%">
                                                                                                                                            </td>

                                                                                                                                        </tr>
                                                                                                                                       <tr style="height:10px">
                                                                                                                                       </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td align="center" colspan="6">
                                                                                                                                                 <table style="border: 1px solid #5078B3" >
                                                                                                                                                    <td align="center">
                                                                                                                                                        <asp:Panel ID="Panel2" runat="server" Width="120px">
                                                                                                                                                            <asp:Button ID="cmdCancelProduct" runat="server"  EnableTheming="false" CssClass="CloseWindow" Text=""/>
                                                                                                                                                            <%--<asp:Label ID="Label1" runat="server" Text="Close Window" Font-Bold="True"></asp:Label>--%>
                                                                                                                                                         </asp:Panel> 
                                                                                                                                                    </td> 
                                                                                                                                                    <td align="center">
                                                                                                                                                        <asp:Panel ID="Panel3" runat="server" Width="120px">
                                                                                                                                                            <asp:Button ID="cmdSaveProduct" runat="server" OnClick="cmdSaveProduct_Click" EnableTheming="false" CssClass="AddProd"
                                                                                                                                                                    Text="" ValidationGroup="product" />
                                                                                                                                                            <%--<asp:Label ID="Label2" runat="server" Text="Add Product" Font-Bold="True"></asp:Label>--%>              
                                                                                                                                                            <asp:Button ID="cmdUpdateProduct" runat="server" Enabled="false" OnClick="cmdUpdateProduct_Click"  EnableTheming="false" CssClass="UpdateProd" Width="38px" Height="37px"
                                                                                                                                                                Text="" ValidationGroup="product"/>
                                                                                                                                                            <%--<asp:Label ID="Label3" runat="server" Enabled="false" Text="Update Product" Font-Bold="True"></asp:Label>--%>
                                                                                                                                                            <asp:TextBox ID="lblUnitMrmnt" runat="server" CssClass="cssTextBox" Visible="False"></asp:TextBox>
                                                                                                                                                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ListProducts"
                                                                                                                                                                TypeName="BusinessLogic"></asp:ObjectDataSource>
                                                                                                                                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                                                                                                                                <Triggers>
                                                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                                                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                                                                                                                                </Triggers>
                                                                                                                                                                <ContentTemplate>
                                                                                                                                                                    <asp:HiddenField ID="hdCurrentRow" runat="server" Value="0" />
                                                                                                                                                                </ContentTemplate>
                                                                                                                                                            </asp:UpdatePanel>
                                                                                                                                                        </asp:Panel> 
                                                                                                                                                    </td>
                                                                                                                                                    <td>
                                                                                                                                                        <asp:Panel ID="Panel8" runat="server">
                                                                                                                                                            <asp:Button ID="BtnClearFilter" runat="server" EnableTheming="false" CssClass="ClearFilter" OnClick="btnClearFilter_Click"
                                                                                                                                                                Text="" />
                                                                                                                                                        </asp:Panel> 
                                                                                                                                                    </td>
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
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="center" class="product-summaryNew" style="padding-top: 40px;">
                                                                                            <asp:HiddenField ID="hdRole" runat="server" Value="N" />
                                                                                            <asp:HiddenField ID="hdStock" runat="server" Value="0" />
                                                                                            <asp:HiddenField ID="hdPurchase" runat="server" Value="0" />
                                                                                            <asp:HiddenField ID="hdFilename" runat="server" Value="0" />
                                                                                            <asp:HiddenField ID="hdTotalAmt" runat="server" Value="0" />
                                                                                            <asp:HiddenField ID="hdMode" runat="server" Value="New" />
                                                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdUpdateProduct" EventName="Click" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="GrdViewItems" EventName="SelectedIndexChanged" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdUpdate" EventName="Click" />
                                                                                                </Triggers>
                                                                                                <ContentTemplate>
                                                                                                    <asp:Panel ID="pnlPurchase" runat="server">
                                                                                                        <table cellpadding="0" cellspacing="1" style="border: 1px solid #86b2d1; text-align: center"
                                                                                                            width="96%">
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <asp:GridView ID="GrdViewItems" runat="server" BorderWidth="1px" DataKeyNames="itemcode"
                                                                                                                        EmptyDataText="No Purchase Items added." OnPageIndexChanging="GrdViewItems_PageIndexChanging"
                                                                                                                        OnRowCancelingEdit="GrdViewItems_RowCancelingEdit" OnRowCreated="GrdViewItems_RowCreated"
                                                                                                                        OnRowDataBound="GrdViewItems_RowDataBound" OnRowDeleting="GrdViewItems_RowDeleting"
                                                                                                                        OnRowEditing="GrdViewItems_RowEditing" OnRowUpdating="GrdViewItems_RowUpdating"
                                                                                                                        OnSelectedIndexChanged="GrdViewItems_SelectedIndexChanged" ShowFooter="false"
                                                                                                                        Width="100%">
                                                                                                                        <%--<HeaderStyle ForeColor="Black" />--%>
                                                                                                                        <EditRowStyle VerticalAlign="Middle" />
                                                                                                                        <RowStyle Font-Bold="false" />
                                                                                                                        <FooterStyle CssClass="HeadataRow" Font-Bold="true" Height="27px" />
                                                                                                                        <Columns>
                                                                                                                            <asp:BoundField DataField="itemcode" HeaderText="Product Code" />
                                                                                                                            <asp:BoundField DataField="ProductName" HeaderText="Product Name &nbsp;" />
                                                                                                                            <asp:BoundField DataField="PurchaseRate" HeaderText="Rate &nbsp;" />
                                                                                                                            <asp:BoundField DataField="NLP" HeaderText="NLP &nbsp;" />
                                                                                                                            <asp:BoundField DataField="Qty" HeaderText="Qty. &nbsp;" />
                                                                                                                            <asp:BoundField DataField="Measure_Unit" HeaderText="Unit &nbsp;" Visible="false" />
                                                                                                                            <asp:BoundField DataField="Discount" HeaderText="Disc(%) &nbsp;" />
                                                                                                                            <asp:BoundField DataField="vat" HeaderText="VAT(%) &nbsp;" />
                                                                                                                            <asp:BoundField DataField="CST" HeaderText="CST(%) &nbsp;" />
                                                                                                                            <asp:BoundField DataField="Discountamt" HeaderText="DiscAmt &nbsp;" />
                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Total &nbsp;">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%# GetTotal(Convert.ToDouble(Eval("Qty").ToString()), Convert.ToDouble(Eval("purchaserate").ToString()), Convert.ToDouble(Eval("discount").ToString()), Convert.ToDouble(Eval("vat").ToString()), Convert.ToDouble(Eval("CST").ToString()), Convert.ToDouble(Eval("Discountamt").ToString()))%>
                                                                                                                                </ItemTemplate>
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="Role Type" Visible="false">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%# Eval("isRole")%>
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle VerticalAlign="Top" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-CssClass="command">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Select" SkinID="edit" />
                                                                                                                                </ItemTemplate>
                                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                            </asp:TemplateField>
                                                                                                                            <asp:TemplateField HeaderStyle-Width="30px">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <asp:ImageButton ID="lnkB" runat="Server" CommandName="Delete" SkinID="delete" />
                                                                                                                                    <cc1:ConfirmButtonExtender ID="CnrfmDel" runat="server" ConfirmText="Are you sure to delete this product from purchase?"
                                                                                                                                        TargetControlID="lnkB">
                                                                                                                                    </cc1:ConfirmButtonExtender>
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle Width="30px" />
                                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                            </asp:TemplateField>
                                                                                                                        </Columns>
                                                                                                                        <PagerTemplate>
                                                                                                                            Goto Page
                                                                                                                            <asp:DropDownList ID="ddlPageSelector2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSelector2_SelectedIndexChanged"
                                                                                                                                SkinID="skinPagerDdlBox">
                                                                                                                            </asp:DropDownList>
                                                                                                                            <asp:Button ID="btnFirst" runat="server" CommandArgument="First" CommandName="Page"
                                                                                                                                Text="First" />
                                                                                                                            <asp:Button ID="btnPrevious" runat="server" CommandArgument="Prev" CommandName="Page"
                                                                                                                                Text="Previous" />
                                                                                                                            <asp:Button ID="btnNext" runat="server" CommandArgument="Next" CommandName="Page"
                                                                                                                                Text="Next" />
                                                                                                                            <asp:Button ID="btnLast" runat="server" CommandArgument="Last" CommandName="Page"
                                                                                                                                Text="Last" />
                                                                                                                        </PagerTemplate>
                                                                                                                    </asp:GridView>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                            <%--<br>
                                                                                            <br>
                                                                                            <br></br>
                                                                                            <br></br>
                                                                                            </br>
                                                                                            </br>--%>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <br />
                                                                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <table width="100%">
                                                                                                        <tr>
                                                                                                            <td style="width:13px">
                                                                                                            </td>
                                                                                                            <td class="uploadibgbg3Co" style="text-align: left;">
                                                                                                                <div>
                                                                                                                    <div>
                                                                                                                        <%--<div>--%>
                                                                                                                            <table border="0" cellpadding="0px" cellspacing="5px" style="margin: 0px auto;">
                                                                                                                                <tr style="display: none">
                                                                                                                                    <td align="left">
                                                                                                                                        <asp:Label ID="lblDispTotal" runat="server" CssClass="item3"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td width="1px">
                                                                                                                                    </td>
                                                                                                                                    <td align="left">
                                                                                                                                        <asp:Label ID="lblTotalSum" runat="server" CssClass="item3" Font-Bold="true"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr style="display: none">
                                                                                                                                    <td align="left">
                                                                                                                                        <asp:Label ID="lblDispDisRate" runat="server" CssClass="item3"></asp:Label>
                                                                                                                                        <asp:Label ID="lblDispTotalRate" runat="server" CssClass="item3"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td width="1px">
                                                                                                                                    </td>
                                                                                                                                    <td align="left">
                                                                                                                                        <asp:Label ID="lblTotalDis" runat="server" CssClass="item3" Font-Bold="true"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr style="display: none">
                                                                                                                                    <td align="left">
                                                                                                                                        <asp:Label ID="lblDispIncVAT" runat="server" CssClass="item3"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td width="1px">
                                                                                                                                    </td>
                                                                                                                                    <td align="left">
                                                                                                                                        <asp:Label ID="lblTotalVAT" runat="server" CssClass="item3" Font-Bold="true"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr style="display: none">
                                                                                                                                    <td align="left">
                                                                                                                                        <asp:Label ID="lblDispIncCST" runat="server" CssClass="item"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td width="1px">
                                                                                                                                    </td>
                                                                                                                                    <td align="left">
                                                                                                                                        <asp:Label ID="lblTotalCST" runat="server" CssClass="item3" Font-Bold="true"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="left">
                                                                                                                                        <asp:Label ID="lblDispLoad" runat="server" CssClass="item3"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td width="1px">
                                                                                                                                    </td>
                                                                                                                                    <td align="right">
                                                                                                                                        <asp:Label ID="lblFreight" runat="server" CssClass="item2" Font-Bold="true"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                                <tr>
                                                                                                                                    <td align="left">
                                                                                                                                        <asp:Label ID="lblDispGrandTtl" runat="server" CssClass="item3"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    <td width="1px">
                                                                                                                                    </td>
                                                                                                                                    <td align="right">
                                                                                                                                        <asp:Label ID="lblNet" runat="server" CssClass="item2" Font-Bold="true" Text="0"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                       <%-- </div>--%>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            <td style="width:13px">
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <table align="center" style="border: 1px solid #86b2d1">
                                                                                                                    <tr  style="padding: 3px">
                                                                                                                    
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </td>
                                                                                                            <td style="text-align: right">
                                                                                                                <div style="text-align: right">
                                                                                                                    <asp:Panel ID="PanelCmd" runat="server">
                                                                                                                        <table align="right" >
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Button ID="CmdProd" runat="server" Text=""  
                                                                                                                                        OnClick="CmdProd_Click" EnableTheming="false" CssClass="Newproductbutton"  
                                                                                                                                        Width="28px" Height="27px"/>
                                                                                                                                </td>
                                                                                                                                <td style="padding: 1px;">
                                                                                                                                    <asp:Button ID="AddNewProd" runat="server" Text="" CssClass="addproductbutton" EnableTheming="false"
                                                                                                                                        SkinID="skinBtnAddProduct" OnClick="lnkAddProduct_Click" />
                                                                                                                                </td>
                                                                                                                                <td style="padding: 1px;">
                                                                                                                                    <asp:Button ID="cmdPrint" CausesValidation="false" runat="server" Text="" Enabled="false"
                                                                                                                                        CssClass="printbutton" EnableTheming="false" OnClick="cmdPrint_Click" SkinID="skinBtnPrint" />
                                                                                                                                </td>
                                                                                                                                <td style="width:13px">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                                <td>
                                                                                                                                    <asp:Button ID="CmdCat" runat="server" Text=""  
                                                                                                                                        OnClick="cmdcat_click"  EnableTheming="false" CssClass="NewSupplierbutton"  
                                                                                                                                        Width="28px" Height="27px"/>
                                                                                                                                </td>

                                                                                                                                <td style="padding: 1px;">
                                                                                                                                    <asp:Button ID="cmdUpdate" ValidationGroup="purchaseval" runat="server" Text="" CssClass="savebutton"
                                                                                                                                        EnableTheming="false" OnClick="cmdUpdate_Click" SkinID="skinBtnSave" />
                                                                                                                                    <asp:Button ID="cmdSave" ValidationGroup="purchaseval" runat="server" Text="" CssClass="savebutton"
                                                                                                                                        EnableTheming="false" OnClick="cmdSave_Click" SkinID="skinBtnSave" />
                                                                                                                                </td>
                                                                                                                                <td style="padding: 1px;">
                                                                                                                                    <asp:Button ID="btnCancel" runat="server" Text="" CssClass="cancelbutton" EnableTheming="false"
                                                                                                                                        Visible="true" OnClick="cmdCancel_Click" SkinID="skinBtnCancel" />
                                                                                                                                </td>
                                                                                                                                <td style="width:13px">
                                                                                                                                </td>
                                                                                                                            </tr>
                                                                                                                            
                                                                                                                        </table>
                                                                                                                        
                                                                                                                    </asp:Panel>
                                                                                                                </div>
                                                                                                            </td>
                                                                                                            
                                                                                                         </tr>
                                                                                                         </table>
                                                                                                                
                                                                                                   
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmbSupplier" EventName="SelectedIndexChanged" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdUpdate" EventName="Click" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdUpdateProduct" EventName="Click" />
                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Additional Details">

                                                                            <ContentTemplate>
                                                                                <table class="tblLeft" width="735px" cellpadding="0" cellspacing="1">
                                                                                    <tr>
                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                            Loading / Unloading
                                                                                        </td>
                                                                                        <td class="ControlTextBox3"  style="width: 23%">
                                                                                            <asp:TextBox ID="txtLU" runat="server" SkinID="skinTxtBox" Text="0" ValidationGroup="product" BackColor = "#90c9fc" TabIndex="6" Height="24px"
                                                                                                        AutoPostBack="True"  width="200px" OnTextChanged="txtFreight_TextChanged"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers" Enabled="True"
                                                                                                        TargetControlID="txtLU" ValidChars="." />
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 20%" >
                                                                                            Freight
                                                                                        </td>
                                                                                        <td class="ControlTextBox3"  style="width: 23%">
                                                                                            <asp:TextBox ID="txtFreight" BackColor = "#90c9fc" width="200px" Height="23px" runat="server" SkinID="skinTxtBox" Text="0" TabIndex="7" ValidationGroup="product"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers" Enabled="True"
                                                                                                        TargetControlID="txtFreight" ValidChars="." />
                                                                                        </td>
                                                                                        <td style="width: 35%">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                       <td style="width: 20%" class="ControlLabel">
                                                                                            Bilits
                                                                                        </td>
                                                                                        <td style="width: 23%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="ddBilts" AutoPostBack="false" AppendDataBoundItems="true" runat="server"  BackColor = "#90c9fc" Width="100%"
                                                                                                     CssClass="drpDownListMedium" style="border: 1px solid #90c9fc" height="26px">
                                                                                            </asp:DropDownList>
                                                                                         </td>
                                                                                        <td class="ControlLabel"  style="width: 20%">
                                                                                            Internal Transfer
                                                                                        </td>
                                                                                        <td style="width: 23%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpIntTrans" AutoPostBack="false" runat="server" CssClass="drpDownListMedium" BackColor = "#90c9fc" style="border: 1px solid #90c9fc" height="26px" Width="100%">
                                                                                                    <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                    <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                       </td>
                                                                                       <td style="width: 35%">
                                                                                       </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                            Sales Return
                                                                                        </td>
                                                                                        <td style="width: 23%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpSalesReturn" AutoPostBack="true" runat="server"  OnSelectedIndexChanged="drpSalesReturn_SelectedIndexChanged"
                                                                                                    CssClass="drpDownListMedium" BackColor = "#90c9fc" Width="100%" style="border: 1px solid #90c9fc" height="26px">
                                                                                                    <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                    <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 20%">
                                                                                            Delivery Note
                                                                                        </td>
                                                                                        <td style="width: 23%" class="ControlDrpBorder">
                                                                                             <asp:DropDownList ID="ddDeliveryNote" TabIndex="10" AutoPostBack="false" runat="server" BackColor = "#90c9fc" Width="100%"
                                                                                                    CssClass="drpDownListMedium" style="border: 1px solid #90c9fc" height="26px">
                                                                                                    <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                    <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                             </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="width: 35%">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5">
                                                                                            <asp:UpdatePanel ID="UpdatePanel222" RenderMode="Inline" runat="server" UpdateMode="Conditional">
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="drpSalesReturn" EventName="SelectedIndexChanged" />
                                                                                                </Triggers>
                                                                                                <ContentTemplate>
                                                                                                    <table runat="server" id="rowSalesRet" cellpadding="0" cellspacing="0" width="100%"
                                                                                                        class="tblLeft">
                                                                                                        <tr>
                                                                                                            <td class="ControlLabel" style="width: 19.7%">
                                                                                                                <asp:RequiredFieldValidator ID="rqSalesReturn" runat="server" ErrorMessage="Reason is mandatory"
                                                                                                                    Text="*" Enabled="false" ControlToValidate="txtSRReason" ValidationGroup="purchaseval" />
                                                                                                                Sales Return Reason *
                                                                                                            </td>
                                                                                                            <td style="width: 23.5%" class="ControlTextBox3">
                                                                                                                <asp:TextBox ID="txtSRReason" runat="server" TextMode="MultiLine" CssClass="cssTextBox"  BackColor = "#90c9fc"  Height="23px"
                                                                                                                    MaxLength="200" Width="90%"></asp:TextBox>
                                                                                                            </td>
                                                                                                            <td style="width: 13%">
                                                                                                            </td>
                                                                                                            <td style="width: 23%">
                                                                                                            </td>
                                                                                                            <td style="width: 35%">
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <td >
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                    </cc1:TabContainer>
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
                                <table width="100%" style="text-align: left; margin: -3px 0px 0px 0px;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GrdViewPurchase" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                CssClass="GridCss" Width="100%" DataKeyNames="PurchaseID" AllowPaging="True"
                                                EmptyDataText="No Purchase Order Details Found" OnPageIndexChanging="GrdViewPurchase_PageIndexChanging"
                                                OnRowCommand="GrdViewPurchase_RowCommand" OnRowEditing="GrdViewPurchase_RowEditing"
                                                OnSelectedIndexChanged="GrdViewPurchase_SelectedIndexChanged" OnRowCreated="GrdViewPurchase_RowCreated"
                                                OnRowDataBound="GrdViewPurchase_RowDataBound" OnRowDeleting="GrdViewPurchase_RowDeleting">
                                                <Columns>
                                                    <asp:BoundField DataField="PurchaseID" Visible="false" />
                                                    <asp:BoundField DataField="Billno" HeaderText="Bill No." HeaderStyle-Width="80px"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="BillDate" HeaderText="Date" HeaderStyle-Width="80px" />
                                                    <asp:BoundField DataField="Supplier" HeaderStyle-Width="250px" HeaderText="Supplier" />
                                                    <asp:BoundField DataField="TotalAmt" HeaderText="Amount" HeaderStyle-Width="80px" DataFormatString="{0:F2}" />
                                                    <asp:BoundField DataField="SalesReturn" HeaderText="Sales Return" HeaderStyle-Wrap="true" HeaderStyle-Width="70px" />
                                                    <asp:BoundField DataField="SalesReturnReason" HeaderText="Sales Return - Reason"
                                                        Visible="false" />
                                                    <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" Visible="false" />
                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="45px" HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="45px" HeaderText="Print">
                                                        <ItemTemplate>
                                                            <a href='<%# DataBinder.Eval(Container, "DataItem.PurchaseID", "javascript:PrintItem({0});") %>'>
                                                                <img alt="Print" border="0" src="App_Themes/DefaultTheme/Images/Print.png">
                                                            </a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="45px">
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Purchase Order?"
                                                                runat="server">
                                                            </cc1:ConfirmButtonExtender>
                                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                            <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("PurchaseID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Goto Page
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlPageSelector" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                                                                    runat="server" AutoPostBack="true"  Width="65px" style="border:1px solid blue">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="Width:5px">
                                            
                                                            </td>
                                                            <td>
                                                                <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                    ID="btnFirst" />
                                                            </td>
                                                            <td>
                                                                <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                    ID="btnPrevious" />
                                                            </td>
                                                            <td>
                                                                <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                    ID="btnNext" />
                                                            </td>
                                                            <td>
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
