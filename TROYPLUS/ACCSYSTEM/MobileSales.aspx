<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileSales.aspx.cs" Inherits="MobileSales" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/ComboBox.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript">

        /*@cc_on@*/
        /*@if (@_win32 && @_jscript_version>=5)

        function window.confirm(str) {
            execScript('n = msgbox("' + str + '","4132")', "vbscript");
            return (n == 6);
        }

        @end@*/

        function ConfirmSMS() {

            if (Page_IsValid) {
                var confSMS = document.getElementById('hdSMS').value;

                var confSMSRequired = document.getElementById('hdSMSRequired').value;

                if (confSMSRequired == "YES") {
                    var rv = confirm("Do you want to send SMS to Customer?");

                    if (rv == true) {
                        document.getElementById('hdSMS').value = "YES";
                        return true;
                    }
                    else {
                        document.getElementById('hdSMS').value = "NO";
                        return true;
                    }
                }
            }
        }

        function Mobile_Validator() {
            var txtMobile = document.getElementById('txtCustPh').value;

            if (txtMobile.length > 0) {
                if (txtMobile.length != 10) {
                    alert("Customer Mobile Number should be minimum of 10 digits.");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }

                if (txtMobile.charAt(0) == "0") {
                    alert("Customer Mobile should not start with Zero. Please remove Zero and try again.");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
            else {
                Page_IsValid = true;
            }
        }

        function Advanced() {
            var panel = document.getElementById('pnlBank');

            var rdoArray = document.getElementById('drpPaymode');

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
    <div style="text-align: left; width: 97%">
        <br />
        <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" SkinID="skinBtnBack"
            Text="Back" OnClick="UpdateCancelButton_Click"></asp:Button>
        <br />
        <br />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="height: 30px; width: 99%; font-weight: bold; font-size: small; padding-left: 5px"
                class="GrdHeaderbgClr">
                <td colspan="2">
                    Sales Details
                </td>
            </tr>
            <tr style="height: 25px">
                <td style="width: 50%" class="tblLeft">
                    Bill Date *
                    <asp:RequiredFieldValidator CssClass="lblFont" ID="RequiredFieldValidator5"
                        runat="server" ControlToValidate="txtBillDate" Display="Dynamic" Text="*" ErrorMessage="BillDate is mandatory" />
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtBillDate" CssClass="cssTextBox" Width="98%" runat="server" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Customer Name *
                    <asp:CompareValidator ID="cvCustomer" runat="server" ControlToValidate="cmbCustomer"
                        Display="Dynamic" ErrorMessage="Please Select Customer!!" Operator="GreaterThan"
                        Text="*" ValueToCompare="0"></asp:CompareValidator>
                </td>
                <td style="width: 50%">
                    <asp:DropDownList TabIndex="1" ID="cmbCustomer" AppendDataBoundItems="true" CssClass="cssDropDown"
                        Width="99%" runat="server" AutoPostBack="true" DataValueField="LedgerID" DataTextField="LedgerName"
                        OnSelectedIndexChanged="cmbCustomer_SelectedIndexChanged">
                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblledgerCategory" runat="server" CssClass="lblFont" Style="color: royalblue;
                        font-weight: normal; font-size: smaller"></asp:Label>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Customer Mobile :
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtCustPh" MaxLength="10" runat="server" CssClass="cssTextBox" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Payment Mode :
                </td>
                <td style="width: 50%">
                    <asp:DropDownList ID="drpPaymode" TabIndex="8" AppendDataBoundItems="True" CssClass="cssDropDown"
                        onchange="Javascript:Advanced();" Width="99%" runat="server" AutoPostBack="false">
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
                                    <asp:RequiredFieldValidator ID="rvCheque" runat="server" ErrorMessage="Cheque\Card number is mandatory"
                                        Text="*" ControlToValidate="txtCreditCardNo" Enabled="false"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 50%">
                                    <asp:TextBox ID="txtCreditCardNo" runat="server" MaxLength="20" Width="98%" CssClass="cssTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tblLeft" style="width: 50%">
                                    Bank Name *
                                    <asp:RequiredFieldValidator ID="rvBank" runat="server" ErrorMessage="Bankname is mandatory"
                                        Text="*" ControlToValidate="drpBankName" Enabled="false" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 50%">
                                    <asp:DropDownList ID="drpBankName" runat="server" DataTextField="LedgerName" DataValueField="LedgerID"
                                        CssClass="cssDropDown" Width="99%" AppendDataBoundItems="true">
                                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Product<asp:RequiredFieldValidator ID="rv1" runat="server" ControlToValidate="cmbProdAdd"
                        Display="Dynamic" ErrorMessage="Product Selection is Required" InitialValue="0"
                        Text="*" />
                </td>
                <td style="width: 50%">
                    <asp:DropDownList ID="cmbProdAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                        CssClass="cssDropDown" Width="99%" DataTextField="ProductName" DataValueField="ItemCode"
                        OnSelectedIndexChanged="cmbProdAdd_SelectedIndexChanged">
                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Description<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="txtQtyAdd" ErrorMessage="Prodcut Qty. is Required" Text="*" />
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="lblProdDescAdd" runat="server" CssClass="cssTextBox" ReadOnly="true"
                        Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Rate
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRateAdd"
                        ErrorMessage="Product Rate is mandatory" Text="*" />
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtRateAdd" runat="server" CssClass="cssTextBox" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Qty.<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQtyAdd"
                        Display="Dynamic" ErrorMessage="Product Qty. must be greater than Zero" Operator="GreaterThan"
                        Text="*" ValueToCompare="0"></asp:CompareValidator>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtQtyAdd" runat="server" CssClass="cssTextBox" Text="0" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Disc(%)<asp:RangeValidator ID="cvDisc" runat="server" ControlToValidate="lblDisAdd"
                        Display="Dynamic" MaximumValue="100" Type="Double" MinimumValue="0" Text="*"
                        ErrorMessage="Discount cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="lblDisAdd" runat="server" CssClass="cssTextBox" Text="0" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    VAT(%)<asp:RangeValidator ID="cvVAT" runat="server" ControlToValidate="lblVATAdd"
                        Display="Dynamic" MaximumValue="100" Type="Double" MinimumValue="0" Text="*"
                        ErrorMessage="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="lblVATAdd" runat="server" CssClass="cssTextBox" Text="0" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Freight :
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtFreight" CssClass="cssTextBox" Width="98%" TabIndex="6" runat="server"
                        Text="0" AutoPostBack="True"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Loading / Unloading :
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtLU" CssClass="cssTextBox" Width="98%" TabIndex="5" runat="server"
                        Text="0" AutoPostBack="True"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    <asp:Button ID="cmdSave" runat="server" OnClick="cmdSave_Click" SkinID="skinBtnSave"
                        Text="Save" OnClientClick="javascript:Mobile_Validator();ConfirmSMS();" />&nbsp;
                    <asp:Button ID="cmdShowSummary" runat="server" SkinID="skinBtn" Text="Show Total"
                        OnClick="cmdTotal_Click" />&nbsp;
                </td>
                <td style="width: 50%" class="tblLeft">
                </td>
            </tr>
        </table>
        <br>
        <div style="width: 300px">
            <div class="mainSummary">
                <div class="shadowSummary">
                </div>
                <div>
                    <table border="0" cellpadding="0" cellspacing="5" style="margin: 0px auto">
                        <tr style="display: none">
                            <td align="left">
                                <asp:Label ID="lblDispTotal" runat="server" CssClass="item"></asp:Label>
                            </td>
                            <td width="1">
                                :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTotalSum" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                                <br>
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
                                <asp:Label ID="lblTotalDis" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                                <br>
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
                                <asp:Label ID="lblNet" runat="server" CssClass="item" Font-Bold="true" Text="0"></asp:Label>
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
            <asp:HiddenField ID="hdSMS" runat="server" Value="NO" />
            <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
            <asp:HiddenField ID="hdStock" runat="server" Value="0" />
            <asp:HiddenField ID="hdTotalAmt" runat="server" Value="0" />
            <asp:HiddenField ID="hdContact" runat="server" />
        </div>
    </div>
    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
        ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
        Font-Size="12" runat="server" />
    <asp:TextBox ID="txtAddress" TabIndex="5" TextMode="MultiLine" Height="30px" Visible="false"
        runat="server" MaxLength="200"></asp:TextBox>
    <asp:TextBox ID="txtAddress2" TabIndex="6" TextMode="MultiLine" Height="30px" Visible="false"
        runat="server" MaxLength="200"></asp:TextBox>
    <asp:TextBox ID="txtAddress3" TabIndex="7" TextMode="MultiLine" Height="30px" Visible="false"
        runat="server" MaxLength="200"></asp:TextBox>
    </form>
</body>
</html>
