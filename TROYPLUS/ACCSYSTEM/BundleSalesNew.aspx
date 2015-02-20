<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="BundleSalesNew.aspx.cs" Inherits="BundleSalesNew" Title="Sales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <table width="100%">
        <tr>
            <td align="left">
                <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" style="border: 1px solid #5078B3">
                    <tr>
                        <td colspan="3" class="searchHeader">
                            Search Sales Bill
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" class="tblLeft">
                            Bill No. &nbsp;
                            <asp:TextBox ValidationGroup="search" ID="txtBillnoSrc" CssClass="lblFont" Width="80px"
                                runat="server" MaxLength="8"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender221" runat="server" TargetControlID="txtBillnoSrc"
                                WatermarkText="Search Bill No." WatermarkCssClass="watermark" />
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender221" runat="server" TargetControlID="txtBillnoSrc"
                                FilterType="Numbers" />
                        </td>
                        <td style="width: 50%" class="tblLeft">
                            <asp:Button ValidationGroup="search" ID="btnSearch" OnClick="btnSearch_Click" runat="server"
                                Text="Search" SkinID="skinBtnSearch" />
                        </td>
                        <td style="width: 25%" class="tblLeft">
                            <asp:RequiredFieldValidator ValidationGroup="search" ID="rqSearchBill" runat="server"
                                Text="Search Box is Empty" ControlToValidate="txtBillnoSrc"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" SkinID="skinBtnAddNew">
                </asp:Button>
                <br />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlSalesForm" runat="server">
        <table style="width: 100%;" align="center" class="SalesBg" cellpadding="5" cellspacing="5">
            <tr>
                <td align="left">
                    <asp:Panel ID="pnlSales" runat="server">
                        <table class="tblLeft" width="100%" cellpadding="3" cellspacing="3">
                            <tr align="center">
                                <td colspan="4" class="SalesHeader">
                                    Sales Form
                                </td>
                            </tr>
                            <tr style="height: 26px">
                                <td style="width: 25%" class="tblLeft allPad">
                                    Bill Date :
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                    <asp:TextBox TabIndex="0" ID="txtBillDate" CssClass="TxtBoxSales" Width="80px" runat="server"
                                        MaxLength="10" ValidationGroup="salesval"></asp:TextBox>
                                    <script language="JavaScript">                                        new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00$cplhControlPanel$txtBillDate' });</script>
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                    Customer Name
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                    <asp:DropDownList TabIndex="1" ID="cmbCustomer" Width="100%" AppendDataBoundItems="true"
                                        CssClass="drpDownListMedium" runat="server" AutoPostBack="true" DataValueField="LedgerID"
                                        DataTextField="LedgerName" OnSelectedIndexChanged="cmbCustomer_SelectedIndexChanged"
                                        ValidationGroup="salesval">
                                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Label ID="lblledgerCategory" runat="server" CssClass="lblFont" Style="color: royalblue;
                                                font-weight: bold;"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 25%" class="tblLeft allPad">
                                    Executive Incharge:
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:DropDownList ID="drpIncharge" TabIndex="4" AppendDataBoundItems="true" runat="server"
                                                Width="100%" CssClass="drpDownListMedium" DataTextField="empFirstName" DataValueField="empno">
                                                <asp:ListItem Text=" -- Executive -- " Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                    Loading / Unloading
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                    <asp:TextBox ID="txtLU" SkinID="skinTextBox" runat="server" Width="100%" ValidationGroup="product"
                                        Text="0"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtLU"
                                        FilterType="Custom, Numbers" ValidChars="." />
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 25%" class="tblLeft allPad">
                                    Freight :
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                    <asp:TextBox ID="txtFreight" SkinID="skinTextBox" runat="server" Width="99%" ValidationGroup="product"
                                        Text="0"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtFreight"
                                        FilterType="Custom, Numbers" ValidChars="." />
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 25%" class="tblLeft allPad">
                                    Purchase Return :
                                </td>
                                <td width="25%">
                                    <asp:DropDownList ID="drpPurchaseReturn" TabIndex="2" AutoPostBack="true" runat="server"
                                        CssClass="drpDownList" Width="100%">
                                        <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="tblLeft">
                                    Paymode :
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <asp:DropDownList ID="drpPaymode" TabIndex="6" AppendDataBoundItems="true" CssClass="drpDownListMedium"
                                        runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="drpPaymode_SelectedIndexChanged"
                                        ValidationGroup="salesval">
                                        <%--<asp:ListItem Text=" -- Paymode -- " Value="0"></asp:ListItem>--%>
                                        <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Bank" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Credit" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td valign="middle">
                                    Customer Address :
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtAddress" TabIndex="5" TextMode="MultiLine" Height="40px" CssClass="TxtBoxSales"
                                                Width="97%" runat="server" MaxLength="200"></asp:TextBox>
                                            <cc1:TextBoxWatermarkExtender ID="twAdd" runat="server" TargetControlID="txtAddress"
                                                WatermarkText="Address" WatermarkCssClass="watermark" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td valign="middle">
                                    Return Reason :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPRReason" CssClass="TxtBoxSales" TabIndex="3" runat="server"
                                        TextMode="MultiLine" Width="97%" Height="40px" MaxLength="255"></asp:TextBox>
                                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtPRReason"
                                        WatermarkText="Reason" WatermarkCssClass="watermark" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:UpdatePanel ID="bankPanel" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="drpPaymode" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlBank" Visible="false" runat="Server">
                                                <table width="100%" class="FntSales">
                                                    <tr>
                                                        <td class="tblLeft" style="width: 25%">
                                                            Cheque / Credit Card No.
                                                        </td>
                                                        <td class="tblLeft" style="width: 25%">
                                                            <asp:TextBox ID="txtCreditCardNo" runat="server" MaxLength="20" SkinID="skinTextBox"
                                                                Width="99%" ValidationGroup="salesval"></asp:TextBox>
                                                        </td>
                                                        <td class="tblLeft allpad" style="width: 25%">
                                                            &nbsp;Bank Name
                                                        </td>
                                                        <td class="tblLeft" style="width: 25%">
                                                            &nbsp;
                                                            <asp:DropDownList ID="drpBankName" Width="96%" CssClass="drpDownListMedium" runat="server"
                                                                DataValueField="LedgerID" DataTextField="LedgerName" AppendDataBoundItems="true"
                                                                ValidationGroup="salesval">
                                                                <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <asp:RequiredFieldValidator CssClass="lblFont" ID="reqSuppllier" Text=" * - Customer Selection is mandatory"
                            InitialValue="0" ControlToValidate="cmbCustomer" runat="server" ValidationGroup="salesval" />
                        <asp:RequiredFieldValidator CssClass="lblFont" ID="RequiredFieldValidator5" runat="server"
                            ControlToValidate="txtBillDate" Display="Dynamic" EnableClientScript="False"
                            ValidationGroup="salesval" Text=" * -  BillDate is mandatory" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text=" * - Paymode is Required"
                            InitialValue="0" ControlToValidate="drpPaymode" CssClass="lblFont" ValidationGroup="salesval"
                            runat="server" />
                        <asp:RequiredFieldValidator CssClass="lblFont" ID="rvCredit" Text=" * - Creditcard number is mandatory"
                            ControlToValidate="txtCreditCardNo" runat="server" ValidationGroup="salesval"
                            Enabled="false" />
                        <asp:RequiredFieldValidator ID="rvbank" Text=" * - Bankname is mandatory" InitialValue="0"
                            ControlToValidate="drpBankName" runat="server" ValidationGroup="salesval" Enabled="false"
                            CssClass="lblFont" />
                        <asp:RequiredFieldValidator runat="server" ValidationGroup="rolevalidation" ID="rvR"
                            ControlToValidate="txtRoleQty" ErrorMessage=" * - Role Qty. used is Required"
                            CssClass="lblFont"></asp:RequiredFieldValidator>
                        <%--<asp:RequiredFieldValidator ID="rqPurchaseReturn" runat="server" CssClass="lblFont" Text="Reason is mandatory" Enabled="false" ControlToValidate="txtPRReason" ValidationGroup="salesval" />--%>
                        <asp:RequiredFieldValidator ID="rv1" ValidationGroup="product" Text=" * - Product Selection is Required"
                            InitialValue="0" ControlToValidate="cmbProdAdd" runat="server" CssClass="lblFont" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text=" * - Rate is Required"
                            ControlToValidate="txtRateAdd" runat="server" ValidationGroup="product" CssClass="lblFont" />
                        <%--March 18--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text=" * - Qty. is Required"
                            ControlToValidate="txtQtyAdd" runat="server" ValidationGroup="product" CssClass="lblFont" />
                        <%--March 18--%>
                        &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQtyAdd"
                            Display="Dynamic" ErrorMessage=" * - Qty. must be greater than Zero!!" Operator="GreaterThan"
                            ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                        &nbsp;<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="drpQtyAdd"
                            Display="Dynamic" ErrorMessage=" * - Qty. must be greater than Zero!!" Operator="GreaterThan"
                            ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                        &nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtRateAdd"
                            Display="Dynamic" ErrorMessage=" * - Rate must be greater than Zero!!" Operator="GreaterThan"
                            ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:UpdatePanel ID="pnlItems" runat="server">
                        <ContentTemplate>
                            <table class="tblLeft" width="100%" cellpadding="3" cellspacing="3" style="border: 1px solid #5078B3">
                                <tr>
                                    <td>
                                        Barcode :
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssC0lass="lblFont"
                                            Text="BarCode is mandatory" ControlToValidate="txtBarcode" ValidationGroup="lookUp" />
                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtBarcode" runat="server" />
                                    </td>
                                    <td valign="top">
                                        <asp:Button ID="cmdBarcode" runat="server" Text="Lookup Product" SkinID="skinBtnLookUp"
                                            ValidationGroup="lookUp" />
                                    </td>
                                </tr>
                                <tr class="SalesHeader">
                                    <td width="25%">
                                        Product
                                    </td>
                                    <td width="35%">
                                        Description
                                    </td>
                                    <td width="10%">
                                        Rate
                                    </td>
                                    <td width="20%">
                                        Qty.
                                    </td>
                                    <td width="5%">
                                        Discount
                                    </td>
                                    <td width="5%">
                                        VAT
                                    </td>
                                    <td width="5%">
                                        CST
                                    </td>
                                    <td align="left" width="5%" id="colheadBundles" runat="server">
                                        <span id="headBundles" visible="false" runat="server">Bundles</span>
                                    </td>
                                    <td align="left" width="5%" id="colheadRods" runat="server">
                                        <span id="headRods" visible="false" runat="server">Rods</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cmbProdAdd" Width="185px" AppendDataBoundItems="true" CssClass="drpDownListMedium"
                                                    runat="server" AutoPostBack="true" DataTextField="ProductName" DataValueField="ItemCode"
                                                    OnSelectedIndexChanged="cmbProdAdd_SelectedIndexChanged" ValidationGroup="product">
                                                    <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:Label ID="lblProdNameAdd" runat="server"></asp:Label>
                                                <asp:Label ID="lblProdDescAdd" runat="server"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtRateAdd" CssClass="TxtBoxSales" runat="server" Width="80px" ValidationGroup="product"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" TargetControlID="txtRateAdd"
                                                    FilterType="Custom, Numbers" ValidChars="." />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                                <asp:PostBackTrigger ControlID="GridView1" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtQtyAdd" CssClass="TxtBoxSales" runat="server" ValidationGroup="product"
                                                    Text="0" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="drpQtyAdd" runat="server" Width="80px" CssClass="drpDownList"
                                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="drpQtyAdd_SelectedIndexChanged">
                                                    <asp:ListItem Value="">Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtQtyAdd"
                                                    FilterType="Custom, Numbers" ValidChars="." />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:TextBox ID="lblDisAdd" CssClass="TxtBoxSales" runat="server" Width="20px" Text="0"
                                                    ValidationGroup="product"></asp:TextBox>(%)
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="lblDisAdd"
                                                    FilterType="Custom, Numbers" ValidChars="." />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:TextBox ID="lblVATAdd" CssClass="TxtBoxSales" runat="server" Width="20px" Text="0"
                                                    ValidationGroup="product"></asp:TextBox>(%)
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="lblVATAdd"
                                                    FilterType="Custom, Numbers" ValidChars="." />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td valign="top">
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Always">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:TextBox ID="lblCSTAdd" CssClass="TxtBoxSales" runat="server" Width="20px" Text="0"
                                                    ValidationGroup="product"></asp:TextBox>(%)
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="lblCSTAdd"
                                                    FilterType="Custom, Numbers" ValidChars="." />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td align="left">
                                        <div id="dvBundle" runat="server" visible="false">
                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                                    <asp:PostBackTrigger ControlID="GridView1" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtBundle" CssClass="TxtBoxSales" runat="server" Width="40px" Text="0"
                                                        ValidationGroup="product"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtBundle"
                                                        FilterType="Numbers" ValidChars="." />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                    <td align="left">
                                        <div id="dvRod" runat="server" visible="false">
                                            <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                                    <asp:PostBackTrigger ControlID="GridView1" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtRod" CssClass="TxtBoxSales" runat="server" Width="40px" Text="0"
                                                        ValidationGroup="product"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtRod"
                                                        FilterType="Numbers" ValidChars="." />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="Left" colspan="9">
                                        <asp:Button ID="cmdSaveProduct" runat="server" Text="Add Product" OnClick="cmdSaveProduct_Click"
                                            ValidationGroup="product" SkinID="skinBtnAddProduct" />
                                        &nbsp;
                                        <asp:Button ID="cmdUpdateProduct" runat="server" Text="Update Product" SkinID="skinBtnUpdateProduct"
                                            OnClick="cmdUpdateProduct_Click" ValidationGroup="product" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="btnCancel" />
                            <asp:PostBackTrigger ControlID="btnClick" />
                            <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnDisplay" runat="server" Value="N" />
                            <asp:HiddenField ID="hdStock" runat="server" Value="0" />
                            <asp:HiddenField ID="hdTotalAmt" runat="server" Value="0" />
                            <asp:HiddenField ID="hdsales" runat="server" Value="0" />
                            <asp:HiddenField ID="hdRole" runat="server" Value="N" />
                            <asp:HiddenField ID="hdContact" runat="server" />
                            <asp:HiddenField ID="hdCurrentRow" Value="0" runat="server" />
                            <asp:HiddenField ID="hdCurrRole" runat="server" />
                            <asp:HiddenField ID="hdOpr" runat="server" />
                            <!-- /*Start March 15 Modification */ -->
                            <asp:HiddenField ID="hdEditQty" Value="0" runat="server" />
                            <!-- /*End March 15 Modification */ -->
                            <asp:HiddenField ID="hdRoleSelection" runat="server" />
                            <asp:Panel ID="pnlRole" runat="server" Visible="false">
                                <table width="100%" cellpadding="3" cellspacing="2" style="border: 1px solid black;
                                    font-size: 11px" class="left">
                                    <tr>
                                        <td colspan="4" align="Left" class="SalesHeader">
                                            Role Details
                                        </td>
                                    </tr>
                                    <tr style="background-color: #000066; color: white; font-weight: bold;">
                                        <td>
                                            Roles Intial Qty.
                                        </td>
                                        <td>
                                            Roles Current Qty. Available
                                        </td>
                                        <td>
                                            Roles Used Now
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="drpRoleAvl" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtIntialQty" runat="server" ReadOnly="true"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="drpRoleAvl" AutoPostBack="true" OnSelectedIndexChanged="drpRoleAvl_SelectedIndexChanged"
                                                        runat="server" DataTextField="Qty_Available" DataValueField="RoleID">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtRoleQty" runat="server"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtRoleQty"
                                                FilterType="Custom, Numbers" ValidChars="." />
                                        </td>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Button ID="btnCLick" Style="width: 100px;" OnClick="btnCLick_Click" CssClass="button"
                                                    runat="server" Text="Use Role" ValidationGroup="rolevalidation" />
                                                &nbsp;&nbsp;<asp:Button ID="btnCancel" Style="width: 60px;" OnClick="btnCancel_Click"
                                                    CssClass="button" runat="server" Text="Cancel" />
                                            </td>
                                        </tr>
                                    </tr>
                                    <tr>
                                        <td align="Left" colspan="4">
                                            <asp:GridView ID="GridView1" Width="50%" runat="server" OnRowDeleting="GridView1_RowDeleting"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Vertical">
                                                <Columns>
                                                    <asp:BoundField DataField="RoleID" HeaderText="Role.No" />
                                                    <asp:BoundField DataField="Qty" HeaderText="Qty." />
                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="4%">
                                                        <ItemTemplate>
                                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to delete this Role?"
                                                                runat="server">
                                                            </cc1:ConfirmButtonExtender>
                                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="4%" />
                                                        <ItemStyle CssClass="command" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                            <asp:PostBackTrigger ControlID="GrdViewItems" />
                            <asp:PostBackTrigger ControlID="lnkBtnAdd" />
                            <asp:PostBackTrigger ControlID="cmdDelete" />
                            <asp:PostBackTrigger ControlID="cmdUpdate" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnlProduct" Visible="false" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="cmbCustomer" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="cmdSave" />
                            <asp:PostBackTrigger ControlID="cmdUpdate" />
                            <asp:PostBackTrigger ControlID="cmdCancel" />
                            <asp:PostBackTrigger ControlID="cmdUpdateProduct" />
                            <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="PanelCmd" runat="server">
                                <asp:Button ID="cmdSave" runat="server" ValidationGroup="salesval" Text="Save" OnClick="cmdSave_Click"
                                    SkinID="skinBtnSave" />
                                &nbsp;
                                <asp:Button ID="cmdUpdate" runat="server" ValidationGroup="salesval" Text="Update"
                                    SkinID="skinBtnSave" OnClick="cmdUpdate_Click" Enabled="false" />&nbsp;
                                <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="cmdUpdate" ConfirmText="This Bill will be cancelled new bill will be generated with your current updated information. Are you sure want to continue ?"
                                    runat="server">
                                </cc1:ConfirmButtonExtender>
                                <asp:Button ID="cmdDelete" runat="server" SkinID="skinBtnDelete" ValidationGroup="salesval"
                                    Text="Delete" OnClick="cmdDelete_Click" Enabled="false" OnClientClick="return confirm_delete()" />&nbsp;
                                <asp:Button ID="cmdPrint" runat="server" ValidationGroup="salesval" Text="Print"
                                    SkinID="skinBtnPrint" Enabled="false" OnClick="cmdPrint_Click" />
                                <asp:Button ID="cmdCancel" runat="server" Text="Cancel" Enabled="false" OnClick="cmdCancel_Click"
                                    SkinID="skinBtnCancel" />
                                <br />
                                <br />
                                <br />
                                <asp:GridView ID="GrdViewItems" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataKeyNames="Roles" OnRowDeleting="GrdViewItems_RowDeleting"
                                    OnSelectedIndexChanged="GrdViewItems_SelectedIndexChanged" SkinID="ReportGrid1"
                                    Width="100%">
                                    <EmptyDataRowStyle CssClass="GrdContent" />
                                    <Columns>
                                        <asp:BoundField DataField="itemcode" HeaderText="Product Code" />
                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                        <asp:BoundField DataField="ProductDesc" HeaderText="Description" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty." />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                        <asp:BoundField DataField="Discount" HeaderText="Discount" />
                                        <asp:BoundField DataField="Vat" HeaderText="Vat" />
                                        <asp:BoundField DataField="CST" HeaderText="CST" />
                                        <asp:BoundField DataField="isRole" HeaderText="is Role" />
                                        <asp:BoundField DataField="Roles" HeaderText="Role" InsertVisible="false" Visible="false" />
                                        <asp:BoundField DataField="Bundles" HeaderText="Bundles" />
                                        <asp:BoundField DataField="Rods" HeaderText="Rods" />
                                        <asp:BoundField DataField="BundleNo" HeaderText="BundleNo" />
                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Total">
                                            <ItemTemplate>
                                                <%# GetTotal(Convert.ToDouble(Eval("Qty").ToString()), Convert.ToDouble(Eval("rate").ToString()), Convert.ToDouble(Eval("discount").ToString()), Convert.ToDouble(Eval("vat").ToString()), Convert.ToDouble(Eval("CST").ToString()))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="25px" ItemStyle-CssClass="command">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="Select" SkinID="edit" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="4%" ItemStyle-CssClass="command">
                                            <ItemTemplate>
                                                <cc1:ConfirmButtonExtender ID="CnrfmDel" runat="server" ConfirmText="Are you sure to delete this product from Sales?"
                                                    TargetControlID="lnkB">
                                                </cc1:ConfirmButtonExtender>
                                                <asp:ImageButton ID="lnkB" runat="Server" CommandName="Delete" SkinID="delete" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="4%" />
                                            <ItemStyle CssClass="command" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <br />
                            <center>
                                <table cellpadding="3" cellspacing="3" style="border: 1px solid silver">
                                    <tr>
                                        <td align="left">
                                            <span class="item">Total (INR)</span>
                                        </td>
                                        <td width="1px">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblTotalSum" runat="server" CssClass="item" Font-Bold="true"></asp:Label><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <span class="item">Discounted Rate (INR)
                                        </td>
                                        <td width="1px">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblTotalDis" runat="server" CssClass="item" Font-Bold="true"></asp:Label><br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <span class="item">Inclusive VAT (INR)</span>
                                        </td>
                                        <td width="1px">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblTotalVAT" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <span class="item">Inclusive CST (INR)</span>
                                        </td>
                                        <td width="1px">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblTotalCST" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <span class="item">Loading / Unloading / Freight (INR)</span>
                                        </td>
                                        <td width="1px">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblFreight" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <span class="item">GRAND Total (INR)</span>
                                        </td>
                                        <td width="1px">
                                            :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblNet" Text="0" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="errPanel" runat="server" Visible="false">
                        <table width="100%" cellpadding="3" cellspacing="3" class="tblLeft">
                            <tr>
                                <td colspan="4" class="SalesHeader">
                                    Exception !!!
                                </td>
                            </tr>
                            <tr>
                                <td width="15%">
                                    Error Message:
                                </td>
                                <td colspan="3">
                                    <asp:Label runat="server" ID="ErrMsg" CssClass="errorMsg"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelBill" runat="server" Visible="false">
        <br />
        <table width="100%">
            <tr>
                <td>
                    <asp:GridView ID="GrdViewSales" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Width="100%" DataKeyNames="Billno" AllowPaging="True" EmptyDataText="No Sales found for the search criteria"
                        OnRowCreated="GrdViewSales_RowCreated" OnRowDataBound="GrdViewSales_RowDataBound"
                        OnSelectedIndexChanged="GrdViewSales_SelectedIndexChanged" OnPageIndexChanging="GrdViewSales_PageIndexChanging">
                        <EmptyDataRowStyle CssClass="GrdContent" />
                        <Columns>
                            <asp:BoundField DataField="Billno" HeaderText="Bill No." />
                            <asp:BoundField DataField="BillDate" HeaderText="Date" />
                            <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" />
                            <asp:TemplateField HeaderText="Paymode">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymode" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}" />
                            <asp:BoundField DataField="CreditCardNo" HeaderText="Card No." />
                            <asp:BoundField DataField="Debtor" HeaderText="Ledger Name" HtmlEncode="false" />
                            <asp:BoundField DataField="PurchaseReturn" HeaderText="Purchase Return" />
                            <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="35px">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                            Goto Page
                            <asp:DropDownList ID="ddlPageSelector" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                                runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox">
                            </asp:DropDownList>
                            <asp:Button Text="First" CommandName="Page" CommandArgument="First" runat="server"
                                ID="btnFirst" />
                            <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server"
                                ID="btnPrevious" />
                            <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server"
                                ID="btnNext" />
                            <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server"
                                ID="btnLast" />
                        </PagerTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
    </asp:Panel>
    <br />
    <br />
</asp:Content>
