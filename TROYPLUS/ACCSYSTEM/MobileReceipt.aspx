<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileReceipt.aspx.cs" Inherits="MobileReceipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Receipt Details</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/ComboBox.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        /*@cc_on@*/
        /*@if (@_win32 && @_jscript_version>=5)

        function window.confirm(str) {
            execScript('n = msgbox("' + str + '","4132")', "vbscript");
            return (n == 6);
        }

        @end@*/


        var panel = document.getElementById('tblBank');
        var adv = document.getElementById('hidAdvancedState');

        var rdoArray = document.getElementsByTagName("input");


        for (i = 0; i <= rdoArray.length - 1; i++) {
            //alert(rdoArray[i].type);
            if (rdoArray[i].type == 'radio') {

                if (rdoArray[i].value == 'Cash' && rdoArray[i].checked == true) {
                    panel.className = "hidden";
                    adv.value = panel.className;
                }
                else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) {
                    panel.className = "AdvancedSearch";
                    adv.value = panel.className;
                }

            }
        }

        function Mobile_Validator() {
            var txtMobile = document.getElementById('txtMobile').value;

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

        function PrintItem(ID) {
            window.showModalDialog('./PrintReceipt.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
        }


        function AdvancedTest(id) {

            var panel = document.getElementById('tblBank');
            var adv = document.getElementById('hidAdvancedState');

            var rdoArray = document.getElementsByTagName("input");


            for (i = 0; i <= rdoArray.length - 1; i++) {
                //alert(rdoArray[i].type);
                if (rdoArray[i].type == 'radio') {

                    if (rdoArray[i].value == 'Cash' && rdoArray[i].checked == true) {
                        panel.className = "hidden";
                        adv.value = panel.className;
                        document.getElementById('cvBank').enabled = false;
                        document.getElementById('rvChequeNo').enabled = false;
                    }
                    else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) {
                        panel.className = "AdvancedSearch";
                        adv.value = panel.className;
                        document.getElementById('cvBank').enabled = true;
                        document.getElementById('rvChequeNo').enabled = true;
                    }

                }
            }
        }

        function AdvancedAdd(id) {

            var panel = document.getElementById('tblBank');
            var adv = document.getElementById('hidAdvancedState');

            var rdoArray = document.getElementsByTagName("input");


            for (i = 0; i <= rdoArray.length - 1; i++) {
                //alert(rdoArray[i].type);
                if (rdoArray[i].type == 'radio') {

                    if (rdoArray[i].value == 'Cash' && rdoArray[i].checked == true) {
                        panel.className = "hidden";
                        adv.value = panel.className;
                    }
                    else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) {
                        panel.className = "AdvancedSearch";
                        adv.value = panel.className;
                    }

                }
            }
        }
    </script>
</head>
<body onload="AdvancedTest(this);">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="6000">
    </asp:ScriptManager>
    <div style="width: 98%; text-align: left">
        <br />
        <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
            SkinID="skinBtnBack" Text="Back" OnClick="UpdateCancelButton_Click"></asp:Button>
        <br />
        <br />
        <table style="width: 100%; vertical-align: text-top; text-align: center" align="center"
            cellpadding="2" cellspacing="2">
            <tr style="height: 30px; width: 100%; font-weight: bold; font-size: small" class="GrdHeaderbgClr">
                <td colspan="2">
                    Receipt Details
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Ref. No. *
                    <asp:RequiredFieldValidator ID="rvRefNo" runat="server" ErrorMessage="Ref. No. is mandatory"
                        ControlToValidate="txtRefNo" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtRefNo" runat="server" Width="100%" SkinID="mobileTxtBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Received Date *
                    <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtTransDate"
                        ErrorMessage="Trasaction Date is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ControlToValidate="txtTransDate" Operator="DataTypeCheck" Type="Date"
                        ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate">*</asp:CompareValidator>
                    <asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtTransDate"
                        ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtTransDate" runat="server" CssClass="cssTextBox" Width="100%">
                    </asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Received From *
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ComboBox2"
                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Received From is Mandatory"
                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                </td>
                <td style="width: 50%">
                    <asp:DropDownList ID="ComboBox2" runat="server" CssClass="cssDropDown" AutoPostBack="True"
                        Width="100%" DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" OnSelectedIndexChanged="ComboBox2_SelectedIndexChanged"
                        DataTextField="LedgerName" AppendDataBoundItems="true">
                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Amount *
                    <asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                        ErrorMessage="Amount is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                    <cc1:FilteredTextBoxExtender ID="fltAmt" runat="server" TargetControlID="txtAmount"
                        ValidChars="." FilterType="Numbers, Custom" />
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtAmount" runat="server" Width="100%" SkinID="mobileTxtBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 50%; height: 34px" class="tblLeft">
                    Payment Made By *
                    <asp:RequiredFieldValidator ID="rvBDate" runat="server" ControlToValidate="chkPayTo"
                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Payment made by is mandatory.">*</asp:RequiredFieldValidator>
                </td>
                <td style="width: 50%; height: 34px" class="tblRight">
                    <asp:RadioButtonList ID="chkPayTo" runat="server" onclick="javascript:AdvancedTest(this.id);"
                        AutoPostBack="False" Width="100%" OnSelectedIndexChanged="chkPayTo_SelectedIndexChanged">
                        <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Cheque"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Narration *
                    <asp:RequiredFieldValidator ID="rvNarration" runat="server" ErrorMessage="Narration is mandatory"
                        ControlToValidate="txtNarration" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtNarration" runat="server" Height="30px" TextMode="MultiLine"
                        Width="100%" SkinID="mobileTxtBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%">
                <td colspan="2" width="100%">
                    <asp:Panel ID="PanelBank" runat="server">
                        <table class="" id="tblBank" width="100%" cellpadding="0" cellspacing="0" runat="server">
                            <tr style="width: 100%; height: 30px">
                                <td class="tblLeft" style="width: 50%">
                                    Bank Name *
                                    <asp:CompareValidator ID="cvBank" runat="server" EnableClientScript="true" Display="Dynamic"
                                        ControlToValidate="ddBanks" ErrorMessage="Bank Name is Mandatory" Operator="GreaterThan"
                                        ValueToCompare="0">*</asp:CompareValidator>
                                </td>
                                <td class="tblLeftMobile" style="width: 50%">
                                    <asp:DropDownList ID="ddBanks" runat="server" CssClass="cssDropDown" AppendDataBoundItems="True"
                                        DataTextField="LedgerName" DataValueField="LedgerID" DataSourceID="srcBanks"
                                        Width="100%" Enabled="true">
                                        <asp:ListItem Selected="True" Value="0">-- Please Select --</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="width: 100%; height: 30px">
                                <td class="tblLeft" style="width: 50%">
                                    Cheque No. *
                                    <asp:RequiredFieldValidator ID="rvChequeNo" runat="server" EnableClientScript="true"
                                        Display="Dynamic" ControlToValidate="txtChequeNo" ErrorMessage="Cheque No. is mandatory">*</asp:RequiredFieldValidator>
                                </td>
                                <td class="tblLeftMobile" style="width: 50%; text-align: left">
                                    <asp:TextBox ID="txtChequeNo" runat="server" SkinID="mobileTxtBox" Width="100%">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 50%" class="tblLeft">
                    Mobile
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxEx" runat="server" FilterType="Numbers"
                        TargetControlID="txtMobile" />
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtMobile" runat="server" Width="100%" MaxLength="10" SkinID="mobileTxtBox">
                    </asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%; height: 30px">
                <td style="width: 100%; text-align: left" colspan="2">
                    <br />
                    <br />
                    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                        OnClick="InsertButton_Click" SkinID="skinBtnSave" OnClientClick="javascript:Mobile_Validator();ConfirmSMS();"
                        Text="Update"></asp:Button>
                </td>
            </tr>
            <tr style="width: 100%">
                <td style="width: 50%">
                    <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                        <SelectParameters>
                            <asp:CookieParameter Type="String" CookieName="Company" Name="connection"></asp:CookieParameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td style="width: 50%">
                    <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitor"
                        TypeName="BusinessLogic">
                        <SelectParameters>
                            <asp:CookieParameter Type="String" CookieName="Company" Name="connection"></asp:CookieParameter>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="hidAdvancedState" runat="server" />
    <asp:HiddenField ID="hdSMS" runat="server" Value="NO" />
    <asp:HiddenField ID="hdText" runat="server" />
    <asp:HiddenField ID="hdMobile" runat="server" />
    <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
        ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
        Font-Size="12" runat="server" />
    </form>
</body>
</html>
