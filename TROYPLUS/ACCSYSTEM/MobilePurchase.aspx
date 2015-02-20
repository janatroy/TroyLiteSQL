<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobilePurchase.aspx.cs" Inherits="MobilePurchase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchase</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/ComboBox.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript">

        function Advanced() {
            var panel = document.getElementById('pnlBank');

            var rdoArray = document.getElementById('cmdPaymode');

            for (i = 0; i <= rdoArray.length - 1; i++) {
                if (rdoArray[i].selected == true) {
                    if (rdoArray.options[i].value == '1') {
                        //alert(rdoArray[i].value);
                        panel.className = "hidden";
                        document.getElementById('rvBank').enabled = false;
                        document.getElementById('rvCheque').enabled = false;
                        //panel.style.visibility="visible";
                    }
                    else if (rdoArray.options[i].value == '2') {
                        //alert(rdoArray[i].value);
                        panel.className = "AdvancedSearch";
                        //panel.style.visibility="hidden";
                        document.getElementById('rvBank').enabled = true;
                        document.getElementById('rvCheque').enabled = true;
                    }
                    else if (rdoArray.options[i].value == '3') {
                        //alert(rdoArray[i].value);
                        panel.className = "hidden";
                        document.getElementById('rvBank').enabled = false;
                        document.getElementById('rvCheque').enabled = false;
                        //panel.style.visibility="visible";
                    }
                }

            }
        }
          
    </script>
</head>
<body onload="Javascript:Advanced();">
    <form id="form1" runat="server">
    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
        ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
        Font-Size="12" runat="server" />
    <div style="width: 98%; text-align: left">
        <div>
            <br />
            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" SkinID="skinBtnBack"
                Text="Back" OnClick="UpdateCancelButton_Click"></asp:Button>
            <br />
            <br />
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr style="height: 30px; width: 100%; font-weight: bold; font-size: small" class="GrdHeaderbgClr">
                    <td colspan="2">
                        Purchase Details
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        Bill No. *
                        <asp:RequiredFieldValidator ID="rvBill" runat="server" ControlToValidate="txtBillno"
                            ErrorMessage="Bill No. is mandatory" Display="Dynamic" EnableClientScript="True"
                            CssClass="lblFont">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtBillno" runat="server" Width="98%" MaxLength="8" CssClass="cssTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        Bill Date *
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="lblFont" runat="server"
                            ErrorMessage="BillDate is mandatory" Text="*" ControlToValidate="txtBillDate"
                            Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rvBillDate" runat="server" ControlToValidate="txtBillDate"
                            ErrorMessage="Purchase date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtBillDate" runat="server" CssClass="cssTextBox" Width="98%" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ControlLabel" style="width:25%;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInvoiveNo"
                            CssClass="lblFont" Display="Dynamic" ErrorMessage="Invoice No. is mandatory" ValidationGroup="purchaseval">*</asp:RequiredFieldValidator>
                        Invoice No. *
                    </td>
                    <td class="ControlTextBox3" style="width:24%;">
                        <asp:TextBox ID="txtInvoiveNo" runat="server" MaxLength="8" CssClass="cssTextBox" BackColor = "#90c9fc" Width="80%"
                                    ValidationGroup="purchaseval" BorderStyle="NotSet" Height="23px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="ControlLabel" style="width:15%;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtInvoiveDate"
                            CssClass="lblFont" Display="Dynamic" ErrorMessage="Invoice Date is mandatory" Text="*"
                            ValidationGroup="purchaseval"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="valdate" runat="server" ControlToValidate="txtInvoiveDate"
                            ErrorMessage="Purchase Invoice date cannot be future date." Text="*" Type="Date" ValidationGroup="purchaseval"></asp:RangeValidator>
                        Invoice Date *
                    </td>
                    <td class="ControlTextBox3" style="width:24%;">
                        <asp:TextBox ID="txtInvoiveDate" runat="server" CssClass="cssTextBox" MaxLength="10" Height="23px" BackColor = "#90c9fc" 
                                ValidationGroup="purchaseval" Width="80%"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        Supplier *
                        <asp:RequiredFieldValidator CssClass="lblFont" ID="reqSuppllier" ErrorMessage="Supplier is mandatory"
                            EnableClientScript="True" Text="*" InitialValue="0" ControlToValidate="cmbSupplier"
                            runat="server" />
                    </td>
                    <td style="width: 50%">
                        <asp:DropDownList ID="cmbSupplier" runat="server" CssClass="cssDropDown" Width="99%"
                            AutoPostBack="false" AutoCompleteMode="Suggest" DataValueField="LedgerID" DataTextField="LedgerName"
                            AppendDataBoundItems="true">
                            <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        Payment Mode :
                    </td>
                    <td style="width: 50%">
                        <asp:DropDownList ID="cmdPaymode" runat="server" CssClass="cssDropDown" Width="99%"
                            AutoPostBack="false" onchange="Javascript:Advanced();" AutoCompleteMode="Suggest"
                            AppendDataBoundItems="true" OnSelectedIndexChanged="cmdPaymode_SelectedIndexChanged">
                            <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Bank / Credit Card" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Credit" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%" colspan="2">
                        <asp:Panel ID="pnlBank" runat="server">
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td class="tblLeft" style="width: 50%">
                                        Cheque / Credit Card No. *
                                        <asp:RequiredFieldValidator ID="rvCheque" runat="server" EnableClientScript="true"
                                            ErrorMessage="Cheque No. is mandatory" ControlToValidate="txtChequeNo" Text="*"
                                            Enabled="false"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="txtChequeNo" runat="server" CssClass="cssTextBox" Width="98%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tblLeft" style="width: 50%">
                                        Bank name *
                                        <asp:RequiredFieldValidator ID="rvbank" runat="server" CssClass="lblFont" EnableClientScript="true"
                                            ErrorMessage="Bankname is mandatory" ControlToValidate="cmbBankName" Text="*"
                                            InitialValue="0" Enabled="false"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 50%">
                                        <asp:DropDownList ID="cmbBankName" runat="server" CssClass="cssDropDown" Width="99%"
                                            AppendDataBoundItems="true" DataTextField="LedgerName" DataValueField="LedgerID"
                                            AutoCompleteMode="Suggest" AutoPostBack="False">
                                            <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        Product Code
                        <asp:RequiredFieldValidator ID="rv1" runat="server" ControlToValidate="cmbProdAdd"
                            InitialValue="0" Text="*" ErrorMessage="Product Selection is mandatory" />
                    </td>
                    <td style="width: 50%">
                        <asp:DropDownList ID="cmbProdAdd" AppendDataBoundItems="true" CssClass="cssDropDown"
                            Width="99%" runat="server" AutoPostBack="false" DataTextField="ProductName" DataValueField="ItemCode"
                            OnSelectedIndexChanged="cmbProdAdd_SelectedIndexChanged">
                            <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        Product Name
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="lblProdNameAdd" CssClass="cssTextBox" Width="98%" Enabled="false"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                    </td>
                    <td style="width: 50%">
                        <asp:Button ID="refresh" runat="server" Text="GetDetails" Width="98%" CausesValidation="false"
                            OnClick="refresh_Click" />
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        Rate
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRateAdd"
                            Text="*" ErrorMessage="Product Rate is mandatory" />
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtRateAdd" runat="server" CssClass="cssTextBox" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        Qty.<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQtyAdd"
                            Text="*" Display="Dynamic" ErrorMessage="Qty. must be greater than Zero!!" Operator="GreaterThan"
                            ValueToCompare="0"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txtQtyAdd"
                            Text="*" ErrorMessage="Qty. is mandatory" />
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtQtyAdd" runat="server" CssClass="cssTextBox" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        Disc(%)<asp:RegularExpressionValidator ID="regextxtPercentage" runat="server" ControlToValidate="lblDisAdd"
                            Display="None" ErrorMessage="Invalid % in Discount" ValidationExpression="^\d{1,3}($|\.\d{1,2}$)"
                            Text="*"></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="cvDisc" runat="server" ControlToValidate="lblDisAdd" Display="Dynamic"
                            MaximumValue="100" Type="Double" MinimumValue="0" Text="*" ErrorMessage="Discount cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lblDisAdd"
                            EnableClientScript="true" Text="*" ErrorMessage="Discount is mandatory" />
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="lblDisAdd" runat="server" CssClass="cssTextBox" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        VAT(%)<asp:RegularExpressionValidator ID="regextxtPercentage2" runat="server" ControlToValidate="lblVATAdd"
                            Display="None" ErrorMessage="Invalid % in VAT" ValidationExpression="^\d{1,3}($|\.\d{1,2}$)"
                            Text="*"></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="cvVAT" runat="server" ControlToValidate="lblVATAdd" Display="Dynamic"
                            MaximumValue="100" Type="Double" MinimumValue="0" Text="*" ErrorMessage="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="lblVATAdd" runat="server" CssClass="cssTextBox" Width="98%"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        Loading / Unloading
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtLU" runat="server" CssClass="cssTextBox" Width="98%" Text="0"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        Freight
                    </td>
                    <td style="width: 50%">
                        <asp:TextBox ID="txtFreight" runat="server" CssClass="cssTextBox" Width="98%" Text="0"></asp:TextBox>
                    </td>
                </tr>
                <tr style="width: 100%; height: 30px">
                    <td style="width: 50%" class="tblLeft ">
                        <asp:Button ID="cmdSave" runat="server" Text="Save" OnClick="cmdSave_Click" SkinID="skinBtnSave" />&nbsp;
                        <asp:Button ID="cmdShowSummary" runat="server" SkinID="skinBtn" Text="Show Total"
                            OnClick="cmdTotal_Click" />&nbsp;
                    </td>
                    <td style="width: 50%" class="tblLeft ">
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div style="width: 300px">
            <div class="mainSummary">
                <div class="shadowSummary">
                </div>
                <div>
                    <table cellspacing="5" cellpadding="0" border="0" style="margin: 0px auto">
                        <tr style="display: none">
                            <td align="left">
                                <asp:Label ID="lblDispTotal" runat="server" CssClass="item"></asp:Label>
                            </td>
                            <td width="1">
                                :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTotalSum" runat="server" CssClass="item" Font-Bold="true"></asp:Label><br>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td align="left">
                                <asp:Label ID="lblDispDisRate" runat="server" CssClass="item"></asp:Label>
                                <asp:Label ID="lblDispTotalRate" runat="server" CssClass="item"></asp:Label>
                            </td>
                            <td width="1">
                                :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTotalDis" runat="server" CssClass="item" Font-Bold="true"></asp:Label><br>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td align="left">
                                <asp:Label ID="lblDispIncVAT" runat="server" CssClass="item"></asp:Label>
                            </td>
                            <td width="1">
                                :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTotalVAT" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td align="left">
                                <asp:Label ID="lblDispIncCST" runat="server" CssClass="item"></asp:Label>
                            </td>
                            <td width="1">
                                :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTotalCST" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td align="left">
                                <asp:Label ID="lblDispLoad" runat="server" CssClass="item"></asp:Label>
                            </td>
                            <td width="1">
                                :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblFreight" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblDispGrandTtl" runat="server" CssClass="item"></asp:Label>
                            </td>
                            <td width="1">
                                :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblNet" Text="0" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="mainConFoot">
                <div class="leftTbl">
                </div>
                <div class="midTblSummary">
                </div>
                <div class="rightTbl">
                </div>
            </div>
        </div>
        <br />
        <br />
        <asp:HiddenField ID="hdStock" runat="server" />
    </div>
    </form>
</body>
</html>
