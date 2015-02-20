<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="BundlePurchase.aspx.cs" Inherits="BundlePurchase" Title="Purchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajX" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script type="text/javascript" language="JavaScript">
        function confirm_delete() {
            if (confirm("Are you sure you want to delete the purchase bill ?") == true)
                return true;
            else
                return false;
        }
        function unl() {
            document.getElementById("hdDel").value = "BrowserClose";
            document.form1.submit();
        }

        function setDelFlag() {
            document.getElementById("delFlag").value = "1";
        }
   
    </script>
    <div style="font-size: 11px; font-family: 'Trebuchet MS';">
        <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" style="border: 1px solid #5078B3">
            <tr>
                <td colspan="4" align="left" class="searchHeader" style="font-size: 11px;">
                    Search
                </td>
            </tr>
            <tr>
                <td style="width: 25%" class="tblLeft">
                    Bill No. :
                </td>
                <td style="width: 25%" class="tblLeft">
                    <asp:TextBox ValidationGroup="search" ID="txtBillnoSrc" Width="120px" runat="server"
                        MaxLength="8" CssClass="lblFont"></asp:TextBox>
                    <ajX:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtBillnoSrc"
                        WatermarkText="Search Bill No." WatermarkCssClass="watermark" />
                    <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtBillnoSrc"
                        FilterType="Numbers" />
                </td>
                <td style="width: 25%" class="tblLeft">
                    Trans. No. :
                </td>
                <td style="width: 25%" class="tblLeft">
                    <asp:TextBox ValidationGroup="search" ID="txtTransNo" Width="120px" runat="server"
                        MaxLength="8" CssClass="lblFont"></asp:TextBox>
                    <ajX:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="txtTransNo"
                        WatermarkText="Search Trans. No." WatermarkCssClass="watermark" />
                    <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtTransNo"
                        FilterType="Numbers" />
                </td>
            </tr>
            <tr>
                <td style="width: 25%" class="tblLeft">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="skinBtnSearch" OnClick="btnSearch_Click" />
                </td>
                <td style="width: 25%" class="tblLeft">
                </td>
                <td style="width: 25%" class="tblLeft">
                </td>
                <td style="width: 25%" class="tblLeft">
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="purchasePanel" runat="server" Visible="false">
            <table style="width: 100%;" align="center" cellpadding="3" cellspacing="5" class="SalesBg">
                <tr>
                    <td style="width: 100%">
                        <table class="tblLeft" width="100%">
                            <tr style="height: 25px">
                                <td colspan="4" class="SalesHeader" align="center">
                                    Purchase Form
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td style="width: 25%" class="tblLeft allPad">
                                    Bill No.
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <asp:TextBox ID="txtBillno" runat="server" ValidationGroup="purchaseval" Width="91%"
                                        MaxLength="8" SkinID="skinTextBox"></asp:TextBox>
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                    Bill Date *
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                    <asp:TextBox ID="txtBillDate" runat="server" CssClass="lblFont" Width="80px" MaxLength="10"
                                        ValidationGroup="purchaseval"></asp:TextBox>
                                    <script language="JavaScript">                                        new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00$cplhControlPanel$txtBillDate' });</script>
                                </td>
                            </tr>
                            <tr>
                                <td class="ControlLabel" style="width:25%;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtInvoiveNo"
                                        CssClass="lblFont" Display="Dynamic" ErrorMessage="Invoice No. is mandatory" ValidationGroup="purchaseval">*</asp:RequiredFieldValidator>
                                    Invoice No. *
                                </td>
                                <td class="ControlTextBox3" style="width:24%;">
                                    <asp:TextBox ID="txtInvoiveNo" runat="server" MaxLength="8" CssClass="cssTextBox" BackColor = "#90c9fc" Width="80%"
                                                ValidationGroup="purchaseval" BorderStyle="NotSet" Height="23px"></asp:TextBox>
                                </td>
                                <td class="ControlLabel" style="width:15%;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtInvoiveDate"
                                        CssClass="lblFont" Display="Dynamic" ErrorMessage="Invoice Date is mandatory" Text="*"
                                        ValidationGroup="purchaseval"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="valdate" runat="server" ControlToValidate="txtInvoiveDate"
                                        ErrorMessage="Purchase Invoice date cannot be future date." Text="*" Type="Date" ValidationGroup="purchaseval"></asp:RangeValidator>
                                    Invoice Date *
                                </td>
                                <td class="ControlTextBox3" style="width:24%;">
                                    <asp:TextBox ID="txtInvoiveDate" runat="server" CssClass="cssTextBox" MaxLength="10" Height="23px" BackColor = "#90c9fc" 
                                            ValidationGroup="purchaseval" Width="80%"></asp:TextBox>
                                    <script language="JavaScript">                                        new tcal({ 'formname': 'aspnetForm', 'controlname': 'ctl00$cplhControlPanel$txtBillDate' });</script>
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td class="tblLeft allPad" style="width: 25%">
                                    Supplier :
                                </td>
                                <td class="tblLeft" style="width: 25%">
                                    <asp:DropDownList ID="cmbSupplier" runat="server" CssClass="lblFont" AutoPostBack="false"
                                        Width="94%" ValidationGroup="purchaseval" AutoCompleteMode="Suggest" DataValueField="LedgerID"
                                        DataTextField="LedgerName" AppendDataBoundItems="true">
                                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                    Paymode :
                                </td>
                                <td class="tblLeft" style="width: 25%">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmdPaymode" runat="server" CssClass="lblFont" AutoPostBack="true"
                                                Width="90%" AutoCompleteMode="Suggest" ValidationGroup="purchaseval" AppendDataBoundItems="true"
                                                OnSelectedIndexChanged="cmdPaymode_SelectedIndexChanged">
                                                <%--<asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>--%>
                                                <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Bank" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Credit" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td class="tblLeft allPad">
                                    Loading / Unloading
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLU" runat="server" SkinID="skinTextBox" Text="0" ValidationGroup="product"
                                        Width="90%"></asp:TextBox>
                                    <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers"
                                        TargetControlID="txtLU" ValidChars="." />
                                </td>
                                <td style="width: 25%" class="tblLeft allPad">
                                    Freight
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtFreight" runat="server" SkinID="skinTextBox" Text="0" ValidationGroup="product"
                                        Width="90%"></asp:TextBox>
                                    <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers"
                                        TargetControlID="txtFreight" ValidChars="." />
                                </td>
                            </tr>
                            <tr style="height: 30px">
                                <td class="tblLeft allPad" style="width: 25%">
                                    Sales Return:
                                </td>
                                <td class="tblLeft" style="width: 25%">
                                    <asp:DropDownList ID="drpSalesReturn" AutoPostBack="true" runat="server" Width="94%"
                                        OnSelectedIndexChanged="drpSalesReturn_SelectedIndexChanged" SkinID="skinDdlBox">
                                        <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td style="width: 25%" class="tblLeft">
                                </td>
                            </tr>
                            <tr id="rowSalesRet" runat="server">
                                <td class="tblLeft allPad" style="width: 25%">
                                    Sales Return Reason:
                                </td>
                                <td class="tblLeft" style="width: 50%" colspan="2">
                                    <asp:TextBox CssClass="lblFont" ID="txtSRReason" runat="server" TextMode="MultiLine"
                                        Width="100%" Height="40px"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:RequiredFieldValidator ID="rvBill" runat="server" ControlToValidate="txtBillno"
                                        Display="Dynamic" EnableClientScript="False" CssClass="lblFont" ValidationGroup="purchaseval">Billno is mandatory</asp:RequiredFieldValidator>
                                </td>
                                <td align="left">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="lblFont" runat="server"
                                        ControlToValidate="txtBillDate" Display="Dynamic" EnableClientScript="False"
                                        ValidationGroup="purchaseval">BillDate is mandatory</asp:RequiredFieldValidator>
                                    <ajX:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtBillDate"
                                        WatermarkText="Bill Date" WatermarkCssClass="watermark" />
                                </td>
                                <td align="left">
                                    <asp:RequiredFieldValidator CssClass="lblFont" ID="reqSuppllier" Text="Supplier is mandatory"
                                        InitialValue="0" ControlToValidate="cmbSupplier" runat="server" ValidationGroup="purchaseval" />
                                </td>
                                <td align="left">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="lblFont" Text="Paymode is mandatory"
                                        InitialValue="0" ControlToValidate="cmdPaymode" runat="server" ValidationGroup="purchaseval" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="tblLeft">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlBank" runat="server" Visible="false">
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 25%" class="tblLeft">
                                                            <asp:Label ID="lblCheque" Text="Cheque No.:" CssClass="lblFont" runat="server"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td style="width: 25%" class="tblLeft">
                                                            <asp:Label ID="lblBankname" CssClass="lblFont" Text="Bank Name" runat="server"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td style="width: 25%" class="tblLeft">
                                                        </td>
                                                        <td style="width: 25%" class="tblLeft">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%" class="tblLeft">
                                                            <asp:TextBox ID="txtChequeNo" runat="server" Width="120px" CssClass="lblFont"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 75%" class="tblLeft">
                                                            <asp:DropDownList ID="cmbBankName" runat="server" CssClass="lblFont" AutoPostBack="False"
                                                                Width="150px" AutoCompleteMode="Suggest" DataValueField="LedgerID" DataTextField="LedgerName"
                                                                AppendDataBoundItems="true" ValidationGroup="purchaseval">
                                                                <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:RequiredFieldValidator ID="rvCheque" Text="Cheque No. is mandatory" ControlToValidate="txtChequeNo"
                                                                runat="server" CssClass="lblFont" ValidationGroup="purchaseval" Enabled="false" />
                                                        </td>
                                                        <td align="left">
                                                            <ajX:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="txtChequeNo"
                                                                WatermarkText="Cheque No." WatermarkCssClass="watermark" />
                                                            <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtChequeNo"
                                                                FilterType="Numbers" />
                                                            <asp:RequiredFieldValidator CssClass="lblFont" ID="rvbank" ErrorMessage="Bankname is mandatory"
                                                                InitialValue="0" ControlToValidate="cmbBankName" runat="server" ValidationGroup="purchaseval"
                                                                Enabled="false" />
                                                        </td>
                                                        <td style="width: 25%" class="tblLeft">
                                                        </td>
                                                        <td style="width: 25%" class="tblLeft">
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
                                </td>
                                <td colspan="3" align="left">
                                    <asp:RequiredFieldValidator ID="rqSalesReturn" runat="server" CssC0lass="lblFont"
                                        Text="Sales Return Reason is mandatory" Enabled="false" ControlToValidate="txtSRReason"
                                        ValidationGroup="purchaseval" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Label ID="err" runat="server" Style="color: Red; font-weight: bold; font-family: 'Trebuchet MS';
                            font-size: 12px;" Text=""></asp:Label>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table class="tblLeft" width="100%" style="border: 1px solid #5078B3">
                                    <tr>
                                        <td valign="top">
                                            Barcode
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssC0lass="lblFont"
                                                Text="BarCode is mandatory" ControlToValidate="txtBarcode" ValidationGroup="lookUp" />
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtBarcode" runat="server" Width="100px">
                                            </asp:TextBox>
                                        </td>
                                        <td colspan="4">
                                            <asp:Button ID="cmdBarcode" runat="server" Text="Lookup Product" OnClick="txtBarcode_Populated"
                                                ValidationGroup="lookUp" SkinID="skinBtnLookUp" />
                                        </td>
                                    </tr>
                                    <tr class="GrdHeaderbgClr">
                                        <td width="10%">
                                            Product Code
                                        </td>
                                        <td width="30%">
                                            Product Name
                                        </td>
                                        <td width="30%">
                                            Description
                                        </td>
                                        <td width="5%">
                                            Rate
                                        </td>
                                        <td width="5%">
                                            NLP
                                        </td>
                                        <td width="5%">
                                            Qty.
                                        </td>
                                        <td width="5%">
                                            Coir
                                        </td>
                                        <td width="15%">
                                            Disc(%)
                                        </td>
                                        <td width="5%">
                                            VAT(%)
                                        </td>
                                        <td width="5%">
                                            CST(%)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <ajX:ComboBox ID="cmbProdAdd" runat="server" AppendDataBoundItems="true" AutoCompleteMode="Suggest"
                                                AutoPostBack="true" CssClass="ajax__combobox_inputcontainer" DataTextField="ProductName"
                                                DataValueField="ItemCode" DropDownStyle="DropDown" OnSelectedIndexChanged="cmbProdAdd_SelectedIndexChanged"
                                                ValidationGroup="product" Width="100px">
                                                <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                            </ajX:ComboBox>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="lblProdNameAdd" runat="server"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:Label ID="lblProdDescAdd" runat="server"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtRateAdd" runat="server" SkinID="skinTxtBoxSmall" ValidationGroup="product"
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtNLPAdd" runat="server" SkinID="skinTxtBoxSmall" ValidationGroup="product"
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtQtyAdd" runat="server" SkinID="skinTxtBoxSmall" ValidationGroup="product"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="txtCoirAdd" runat="server" SkinID="skinTxtBoxSmall" ValidationGroup="product"
                                                Width="60px"></asp:TextBox>
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="lblDisAdd" runat="server" SkinID="skinTxtBoxSmall" Width="20px"></asp:TextBox>
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="lblVATAdd" runat="server" SkinID="skinTxtBoxSmall" Width="20px"></asp:TextBox>
                                            &nbsp;
                                        </td>
                                        <td valign="top">
                                            <asp:TextBox ID="lblCSTAdd" runat="server" SkinID="skinTxtBoxSmall" Text="0" ValidationGroup="product"
                                                Width="20px"></asp:TextBox>
                                            <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="lblCSTAdd" ValidChars="." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <asp:RequiredFieldValidator ID="rv1" runat="server" ControlToValidate="cmbProdAdd"
                                                InitialValue="0" Text="Product Selection is mandatory" ValidationGroup="product" />
                                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ListProducts"
                                                TypeName="BusinessLogic"></asp:ObjectDataSource>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRateAdd"
                                                Text="Rate is mandatory" ValidationGroup="product" />
                                            <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="txtRateAdd" ValidChars="." />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txtQtyAdd"
                                                Text="Qty. is mandatory" ValidationGroup="product" />
                                            <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="txtQtyAdd" ValidChars="." />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCoirAdd"
                                                Text="Coir is mandatory" ValidationGroup="product" />
                                            <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="txtCoirAdd" ValidChars="." />
                                            <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender55" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="lblVATAdd" ValidChars="." />
                                            <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="lblDisAdd" ValidChars="." />
                                            <asp:RegularExpressionValidator ID="regextxtPercentage" runat="server" ControlToValidate="lblDisAdd"
                                                Display="None" ErrorMessage="Invalid %" ValidationExpression="^\d{1,3}($|\.\d{1,2}$)"
                                                ValidationGroup="product"></asp:RegularExpressionValidator>
                                            <ajX:ValidatorCalloutExtender ID="regextxtPercentage_ValidatorCalloutExtender" runat="server"
                                                TargetControlID="regextxtPercentage" Width="50px">
                                            </ajX:ValidatorCalloutExtender>
                                            <asp:RegularExpressionValidator ID="regextxtPercentage2" runat="server" ControlToValidate="lblVATAdd"
                                                Display="None" ErrorMessage="Invalid %" ValidationExpression="^\d{1,3}($|\.\d{1,2}$)"
                                                ValidationGroup="product"></asp:RegularExpressionValidator>
                                            <ajX:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="regextxtPercentage2"
                                                Width="50px">
                                            </ajX:ValidatorCalloutExtender>
                                            &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQtyAdd"
                                                Display="Dynamic" ErrorMessage=" * - Qty. must be greater than Zero!!" Operator="GreaterThan"
                                                ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                            &nbsp;<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtCoirAdd"
                                                Display="Dynamic" ErrorMessage=" * - Coir must be greater than Zero!!" Operator="GreaterThan"
                                                ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                            &nbsp;<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtRateAdd"
                                                Display="Dynamic" ErrorMessage=" * - Rate must be greater than Zero!!" Operator="GreaterThan"
                                                ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="8">
                                            <asp:HiddenField ID="hdStock" runat="server" />
                                            <asp:Button ID="cmdSaveProduct" runat="server" OnClick="cmdSaveProduct_Click" Text="Add Product"
                                                SkinID="skinBtnAddProduct" ValidationGroup="product" />
                                        </td>
                                    </tr>
                                    </tr>
                                </table>
                                <br />
                                <asp:HiddenField ID="hdRole" runat="server" Value="N" />
                                <asp:Panel ID="pnlRole" runat="server" Visible="false">
                                    <table width="100%" cellpadding="3" cellspacing="2" style="font-size: 11px" class="left">
                                        <tr>
                                            <td colspan="3" align="center" class="accordionHeader">
                                                Enter Role Details
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" width="30%">
                                                Role Qty. :
                                            </td>
                                            <td width="20%">
                                                <asp:TextBox ID="txtRole" runat="server" CssClass="left" Width="120px"></asp:TextBox>
                                            </td>
                                            <%--  <td>Role Rate</td>
                        <td><asp:TextBox ID="txtRate" runat="server" CssClass="left" Width="120px" ></asp:TextBox></td>--%>
                                            <td width="50%" align="left">
                                                <asp:Button ID="btnCLick" runat="server" Text="Add Role" ValidationGroup="rolevalidation"
                                                    OnClick="btnCLick_Click" />&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtRole"
                                                    FilterType="Custom, Numbers" ValidChars="." />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text="Role Qty. is mandatory"
                                                    ControlToValidate="txtRole" runat="server" ValidationGroup="rolevalidation" />
                                            </td>
                                            <%-- <td colspan="2">
                     <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtRate"
                                    FilterType="Custom, Numbers" ValidChars="." /></td>--%>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3">
                                                <asp:GridView ID="GridView1" Width="50%" runat="server" OnRowEditing="GrdView1_RowEditing"
                                                    OnRowDeleting="GrdView1_RowDeleting" OnRowCancelingEdit="GrdView1_RowCancelingEdit"
                                                    OnRowUpdating="GrdView1_RowUpdating">
                                                    <Columns>
                                                        <asp:BoundField DataField="Qty" HeaderText="Qty." />
                                                        <%-- <asp:BoundField DataField="Rate" HeaderText="Rate"  /> --%>
                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="4%">
                                                            <ItemTemplate>
                                                                <ajX:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to delete this product from purchase?"
                                                                    runat="server">
                                                                </ajX:ConfirmButtonExtender>
                                                                <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <br />
                                <asp:HiddenField ID="hdPurchase" runat="server" Value="0" />
                                <asp:GridView ID="GrdViewItems" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataKeyNames="itemcode" OnPageIndexChanging="GrdViewItems_PageIndexChanging"
                                    OnRowCancelingEdit="GrdViewItems_RowCancelingEdit" OnRowCreated="GrdViewItems_RowCreated"
                                    OnRowDataBound="GrdViewItems_RowDataBound" OnRowDeleting="GrdViewItems_RowDeleting"
                                    OnRowEditing="GrdViewItems_RowEditing" OnRowUpdating="GrdViewItems_RowUpdating"
                                    OnSelectedIndexChanged="GrdViewItems_SelectedIndexChanged" SkinID="ReportGrid1"
                                    Width="100%">
                                    <EditRowStyle VerticalAlign="Middle" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="5%" HeaderStyle-Wrap="false" HeaderText="Product Code">
                                            <ItemTemplate>
                                                <%# Eval("itemcode")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblCode" runat="server" Text='<%# Eval("itemcode")%>'></asp:Label>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="15%" HeaderStyle-Wrap="false" HeaderText="Product Name">
                                            <ItemTemplate>
                                                <%# Eval("ProductName")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblProdname" runat="server" Text='<%# Eval("ProductName")%>'></asp:Label>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="30%" HeaderText="Description">
                                            <ItemTemplate>
                                                <%# Eval("ProductDesc")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("ProductDesc")%>'></asp:Label>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="30%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Qty.">
                                            <ItemTemplate>
                                                <%# Eval("Qty")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtQty" runat="server" SkinID="skinTxtBoxSmall" Text='<%# Eval("Qty") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvq" runat="server" ControlToValidate="txtQty" CssClass="lblFont"
                                                    ErrorMessage="*" ValidationGroup="editVal"></asp:RequiredFieldValidator>
                                                <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers"
                                                    TargetControlID="txtQty" ValidChars="." />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Coir">
                                            <ItemTemplate>
                                                <%# Eval("Coir")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCoir" runat="server" SkinID="skinTxtBoxSmall" Text='<%# Eval("Coir") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvC" runat="server" ControlToValidate="txtCoir" CssClass="lblFont"
                                                    ErrorMessage="*" ValidationGroup="editVal"></asp:RequiredFieldValidator>
                                                <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtenderCoir" runat="server" FilterType="Custom, Numbers"
                                                    TargetControlID="txtCoir" ValidChars="." />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="Rate">
                                            <ItemTemplate>
                                                <%# Eval("PurchaseRate")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRate" runat="server" SkinID="skinTxtBoxSmall" Text='<%# Eval("PurchaseRate") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvR" runat="server" ControlToValidate="txtRate" CssClass="lblFont"
                                                    ErrorMessage="*" ValidationGroup="editVal"></asp:RequiredFieldValidator>
                                                <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" FilterType="Custom, Numbers"
                                                    TargetControlID="txtRate" ValidChars="." />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="NLP">
                                            <ItemTemplate>
                                                <%# Eval("NLP")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtNLP" runat="server" SkinID="skinTxtBoxSmall" Text='<%# Eval("NLP") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvNLP" runat="server" ControlToValidate="txtNLP"
                                                    ErrorMessage="*" ValidationGroup="editVal"></asp:RequiredFieldValidator>
                                                <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtenderNLP" runat="server" FilterType="Custom, Numbers"
                                                    TargetControlID="txtNLP" ValidChars="." />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                            <ItemStyle VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="10%" HeaderStyle-Wrap="false" HeaderText="Discount(%)">
                                            <ItemTemplate>
                                                <%# Eval("Discount")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDiscount" runat="server" MaxLength="5" SkinID="skinTxtBoxSmall"
                                                    Text='<%# Eval("Discount") %>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="regextxtPercentage" runat="server" ControlToValidate="txtDiscount"
                                                    CssClass="lblFont" Display="None" ErrorMessage="Invalid %" ValidationExpression="^\d{1,3}($|\.\d{1,2}$)"
                                                    ValidationGroup="editVal"></asp:RegularExpressionValidator>
                                                <ajX:ValidatorCalloutExtender ID="regextxtPercentage_ValidatorCalloutExtender" runat="server"
                                                    TargetControlID="regextxtPercentage" Width="50px">
                                                </ajX:ValidatorCalloutExtender>
                                                <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender43" runat="server" FilterType="Custom, Numbers"
                                                    TargetControlID="txtDiscount" ValidChars="." />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="5%" HeaderStyle-Wrap="false" HeaderText="VAT(%)">
                                            <ItemTemplate>
                                                <%# Eval("vat") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtVat" runat="server" MaxLength="5" SkinID="skinTxtBoxSmall" Text='<%# Eval("vat") %>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="regextxtPercentage2" runat="server" ControlToValidate="txtVat"
                                                    CssClass="lblFont" Display="None" ErrorMessage="Invalid %" ValidationExpression="^\d{1,3}($|\.\d{1,2}$)"
                                                    ValidationGroup="editVal"></asp:RegularExpressionValidator>
                                                <ajX:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="regextxtPercentage2"
                                                    Width="50px">
                                                </ajX:ValidatorCalloutExtender>
                                                <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers"
                                                    TargetControlID="txtVat" ValidChars="." />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="5%" HeaderStyle-Wrap="false" HeaderText="CST(%)">
                                            <ItemTemplate>
                                                <%# Eval("CST") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCST" runat="server" SkinID="skinTxtBoxSmall" Text='<%# Eval("CST") %>'></asp:TextBox>
                                                <ajX:FilteredTextBoxExtender ID="FilteredTextBoxExtender411" runat="server" FilterType="Custom, Numbers"
                                                    TargetControlID="txtCST" ValidChars="." />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="Roles" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                    <%# Eval("Roles")  %>
                                    </ItemTemplate>
                                    </asp:TemplateField> --%>
                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderStyle-Width="12%" HeaderText="Total">
                                            <ItemTemplate>
                                                <%# GetTotal(Convert.ToDouble(Eval("Qty").ToString()), Convert.ToDouble(Eval("purchaserate").ToString()), Convert.ToDouble(Eval("discount").ToString()), Convert.ToDouble(Eval("vat").ToString()), Convert.ToDouble(Eval("CST").ToString()))%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" />
                                            <HeaderStyle Width="12%" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField>
                                <ItemTemplate>
                                 <%# Eval("Roles")  %>
                                </ItemTemplate> 
                                </asp:TemplateField> --%>
                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="Role Type">
                                            <ItemTemplate>
                                                <%# Eval("isRole")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ButtonType="Image" CancelImageUrl="~/App_Themes/DefaultTheme/Images/dialog-cancel.png"
                                            EditImageUrl="~/App_Themes/DefaultTheme/Images/edit.png" HeaderStyle-Width="4%"
                                            ShowEditButton="true" UpdateImageUrl="~/App_Themes/DefaultTheme/Images/save.png"
                                            ValidationGroup="editVal">
                                            <HeaderStyle Width="4%" />
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderStyle-Width="4%" ItemStyle-CssClass="command">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkB" runat="Server" CommandName="Delete" SkinID="delete" />
                                                <ajX:ConfirmButtonExtender ID="CnrfmDel" runat="server" ConfirmText="Are you sure to delete this product from purchase?"
                                                    TargetControlID="lnkB">
                                                </ajX:ConfirmButtonExtender>
                                            </ItemTemplate>
                                            <HeaderStyle Width="4%" />
                                            <ItemStyle CssClass="command" />
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
                                <asp:HiddenField ID="hdFilename" runat="server" Value="0" />
                                <asp:HiddenField ID="hdTotalAmt" runat="server" Value="0" />
                                <asp:HiddenField ID="hdMode" runat="server" Value="New" />
                                <asp:Panel ID="pnlSales" runat="server">
                                </asp:Panel>
                                <br />
                                <center>
                                    <table cellpadding="3" cellspacing="3">
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Panel ID="PanelCmd" runat="server" Visible="false">
                            <asp:Button ID="cmdSave" ValidationGroup="purchaseval" runat="server" Text="Save"
                                OnClick="cmdSave_Click" SkinID="skinBtnSave" />
                            <asp:Button ID="cmdUpdate" ValidationGroup="purchaseval" runat="server" Text="Update"
                                OnClick="cmdUpdate_Click" SkinID="skinBtnSave" />
                            <asp:Button ID="cmdDelete" ValidationGroup="purchaseval" runat="server" Text="Delete"
                                OnClick="cmdDelete_Click" OnClientClick="return confirm_delete()" SkinID="skinBtnDelete" />
                            <asp:Button ID="cmdPrint" ValidationGroup="purchaseval" runat="server" Text="Print"
                                Enabled="false" OnClick="cmdPrint_Click" SkinID="skinBtnPrint" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Enabled="false" Visible="true"
                                OnClick="cmdCancel_Click" SkinID="skinBtnCancel" />
                        </asp:Panel>
                        <br />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" SkinID="skinBtnAddNew">
        </asp:Button>
        <br />
        <br />
        <asp:Panel ID="PanelBill" runat="server">
            <asp:GridView ID="GrdViewPurchase" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                Width="100%" DataKeyNames="PurchaseID" AllowPaging="True" EmptyDataText="No Purchase Details found for the search criteria"
                OnPageIndexChanging="GrdViewPurchase_PageIndexChanging" OnRowCommand="GrdViewPurchase_RowCommand"
                OnRowEditing="GrdViewPurchase_RowEditing" OnSelectedIndexChanged="GrdViewPurchase_SelectedIndexChanged"
                OnRowCreated="GrdViewPurchase_RowCreated" OnRowDataBound="GrdViewPurchase_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="PurchaseID" Visible="false" />
                    <asp:BoundField DataField="TransNo" HeaderText="Trans. No." />
                    <asp:BoundField DataField="Billno" HeaderText="Bill No." />
                    <asp:BoundField DataField="BillDate" HeaderText="Date" />
                    <asp:TemplateField HeaderText="Paymode">
                        <ItemTemplate>
                            <asp:Label ID="lblPaymode" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" />
                    <asp:BoundField DataField="Chequeno" HeaderText="Chequeno" />
                    <asp:BoundField DataField="Creditor" HeaderText="Creditor" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}" />
                    <asp:BoundField DataField="SalesReturn" HeaderText="Sales Return" />
                    <asp:BoundField DataField="SalesReturnReason" HeaderText="Sales Return - Reason" />
                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="35px">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
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
                <SelectedRowStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <br />
            <input type="hidden" value="" id="hdDel" runat="server" />
            <input type="hidden" id="delFlag" value="0" runat="server" />
            <asp:HiddenField ID="hdToDelete" Value="0" runat="server" />
            <br />
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
    </div>
    <br />
    <br />
</asp:Content>
