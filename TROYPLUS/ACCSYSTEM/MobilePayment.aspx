<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobilePayment.aspx.cs" Inherits="MobilePayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Details</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <link href="App_Themes/DefaultTheme/DefaultTheme.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/calendar.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/DefaultTheme/ComboBox.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="Scripts/calendar_eu.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/JScript.js"></script>
    <script language="javascript" type="text/javascript">

function PrintItem(ID) 
{ 
  window.showModalDialog('./PrintPayment.aspx?ID=' + ID ,self,'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;') ;
}


function AdvancedTest(id) 
{ 
       
       var panel = document.getElementById('tblBank');   
       var adv = document.getElementById('hidAdvancedState') ; 
       
       var rdoArray = document.getElementsByTagName("input");  
       
       
       for(i=0;i<=rdoArray.length-1;i++)  
        {  
            //alert(rdoArray[i].type);
            if(rdoArray[i].type == 'radio')
            {
                
                if( rdoArray[i].value == 'Cash' && rdoArray[i].checked == true)
                {
                    panel.className = "hidden" ; 
                    adv.value = panel.className ; 
                }
                else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) 
                { 
                    panel.className = "AdvancedSearch" ; 
                    adv.value = panel.className ; 
                }        
                
            }
        }  

} 

function AdvancedAdd(id) 
{ 
       
       var panel = document.getElementById('tblBankAdd');   
       var adv = document.getElementById('hidAdvancedState') ; 
       
       var rdoArray = document.getElementsByTagName("input");  
       
       
       for(i=0;i<=rdoArray.length-1;i++)  
        {  
            //alert(rdoArray[i].type);
            if(rdoArray[i].type == 'radio')
            {
                
                if( rdoArray[i].value == 'Cash' && rdoArray[i].checked == true)
                {
                    panel.className = "hidden" ; 
                    adv.value = panel.className ; 
                    document.getElementById('cvBankAdd').enabled = false;
                    document.getElementById('rvChequeNoAdd').enabled = false;
                }
                else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) 
                { 
                    panel.className = "AdvancedSearch" ; 
                    adv.value = panel.className ; 
                    document.getElementById('cvBankAdd').enabled = true;
                    document.getElementById('rvChequeNoAdd').enabled = true;                    
                }        
                
            }
        }  
} 

<!-- 
function Advanced() 
{ 
        var table = document.getElementById('tblBank'); 
        var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState') ; 
        
        var tr = table.getElementsByTagName('tr') ; 
        
        for (i = 0; i < tr.length; i++) 
        { 
                if (tr[i].className == "AdvancedSearch") 
                { 
                        tr[i].className = "hidden" ; 
                        adv.value = tr[i].className ; 
                } 
                else if (tr[i].className == "hidden") 
                { 
                        tr[i].className = "AdvancedSearch" ; 
                        adv.value = tr[i].className ; 
                }                               
        } 
} 
//--> 
    </script>
</head>
<body onload="AdvancedAdd(this)">
    <form id="form1" runat="server" style="text-align: center">
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
                    Payment Details
                </td>
            </tr>
            <tr style="height: 30px; width: 100%" class="tblLeftMobile">
                <td style="width: 50%" class="tblLeftMobile">
                    Ref. No. *
                    <asp:RequiredFieldValidator ID="rvRefNoAdd" runat="server" ControlToValidate="txtRefNoAdd"
                        ErrorMessage="Ref. No. is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtRefNoAdd" runat="server" Width="99%" CssClass="cssTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 30px; width: 100%">
                <td style="width: 50%" class="tblLeftMobile">
                    Date *
                    <asp:RequiredFieldValidator ID="rvStockAdd" runat="server" ControlToValidate="txtTransDateAdd"
                        ErrorMessage="Trasaction Date is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ControlToValidate="txtTransDateAdd" Operator="DataTypeCheck"
                        Type="Date" ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDateAdd">*</asp:CompareValidator>
                    <asp:RangeValidator ID="myRangeValidatorAdd" runat="server" ControlToValidate="txtTransDateAdd"
                        ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtTransDateAdd" runat="server" Width="99%" CssClass="cssTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 30px; width: 100%" class="tblLeftMobile">
                <td style="width: 50%" class="tblLeftMobile">
                    Paid To *
                    <asp:CompareValidator ID="cvPayedToAdd" runat="server" ControlToValidate="ComboBox2Add"
                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Paid To is Mandatory"
                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                </td>
                <td style="width: 50%">
                    <asp:DropDownList ID="ComboBox2Add" runat="server" Width="100%" CssClass="cssDropDown"
                        AutoPostBack="False" DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID"
                        DataTextField="LedgerName" AppendDataBoundItems="true">
                        <asp:ListItem Text=" -- Please Select -- " Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 30px; width: 100%" class="tblLeftMobile">
                <td style="width: 50%" class="tblLeftMobile">
                    Amount *
                    <asp:RequiredFieldValidator ID="rvModelAdd" runat="server" ControlToValidate="txtAmountAdd"
                        ErrorMessage="Amount is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                    <cc1:FilteredTextBoxExtender ID="fltAmtAdd" runat="server" TargetControlID="txtAmountAdd"
                        ValidChars="." FilterType="Numbers, Custom" />
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtAmountAdd" runat="server" Width="99%" CssClass="cssTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 30px; width: 100%" class="tblLeftMobile">
                <td style="width: 50%;" class="tblLeftMobile">
                    Payment Made By *
                    <asp:RequiredFieldValidator ID="rvBDateAdd" runat="server" ControlToValidate="chkPayToAdd"
                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Item Name is mandatory.">*</asp:RequiredFieldValidator>
                </td>
                <td style="width: 50%">
                    <asp:RadioButtonList ID="chkPayToAdd" runat="server" OnDataBound="chkPayToAdd_DataBound"
                        onclick="javascript:AdvancedAdd(this.id);" AutoPostBack="false" Width="100%"
                        OnSelectedIndexChanged="chkPayToAdd_SelectedIndexChanged">
                        <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Cheque"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr style="height: 30px; width: 100%" class="tblLeftMobile">
                <td style="width: 50%" class="tblLeftMobile">
                    Narration *
                    <asp:RequiredFieldValidator ID="rvNarrationAdd" runat="server" ControlToValidate="txtNarrationAdd"
                        ErrorMessage="Narration is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                </td>
                <td style="width: 50%">
                    <asp:TextBox ID="txtNarrationAdd" runat="server" Height="30px" TextMode="MultiLine"
                        Width="99%" CssClass="cssTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr style="width: 100%">
                <td colspan="2" width="100%">
                    <asp:Panel ID="PanelBankAdd" runat="server">
                        <table width="100%" id="tblBankAdd" cellpadding="0" cellspacing="0" runat="server">
                            <tr>
                                <td style="width: 50%;" class="tblLeftMobile">
                                    Bank Name *
                                    <asp:CompareValidator ID="cvBankAdd" runat="server" ControlToValidate="ddBanksAdd"
                                        Enabled="false" Display="Dynamic" EnableClientScript="true" ErrorMessage="Bank is Mandatory"
                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                </td>
                                <td style="width: 50%;">
                                    <asp:DropDownList ID="ddBanksAdd" runat="server" Width="100%" CssClass="cssDropDown"
                                        DataSourceID="srcBanksAdd" DataTextField="LedgerName" DataValueField="LedgerID"
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Selected="True" Value="0">-- Please Select --</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%" class="tblLeftMobile">
                                    Cheque No. *
                                    <asp:RequiredFieldValidator ID="rvChequeNoAdd" runat="server" ControlToValidate="txtChequeNoAdd"
                                        Enabled="false" ErrorMessage="Cheque No. is mandatory" Display="Dynamic" EnableClientScript="true">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 50%;" class="tblLeftMobile">
                                    <asp:TextBox ID="txtChequeNoAdd" runat="server" SkinID="mobileTxtBox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr style="height: 30px" class="tblLeftMobile">
                <td align="left" style="width: 50%">
                    <br />
                    <table cellspacing="0" cellpadding="0">
                        <tr width="100%">
                            <td style="height: 30px">
                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    SkinID="skinBtnSave" Text="Save" OnClick="InsertButton_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%">
                    <asp:ObjectDataSource ID="srcCreditorDebitorAdd" runat="server" SelectMethod="ListCreditorDebitor"
                        TypeName="BusinessLogic">
                        <SelectParameters>
                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
            <tr style="height: 30px; width: 100%" class="tblLeftMobile">
                <td style="width: 100%" colspan="2">
                    <asp:ObjectDataSource ID="srcBanksAdd" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                        <SelectParameters>
                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
            ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
            Font-Size="12" runat="server" />
        <input type="hidden" id="hidAdvancedState" runat="server" />
        <asp:ObjectDataSource ID="frmSource" runat="server" TypeName="BusinessLogic" InsertMethod="InsertPayment"
            OnInserting="frmSource_Inserting" UpdateMethod="UpdatePayment" OnInserted="frmSource_Inserted">
            <UpdateParameters>
                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                <asp:Parameter Name="TransNo" Type="Int32" />
                <asp:Parameter Name="RefNo" Type="String" />
                <asp:Parameter Name="TransDate" Type="DateTime" />
                <asp:Parameter Name="DebitorID" Type="Int32" />
                <asp:Parameter Name="CreditorID" Type="Int32" />
                <asp:Parameter Name="Amount" Type="Double" />
                <asp:Parameter Name="Narration" Type="String" />
                <asp:Parameter Name="VoucherType" Type="String" />
                <asp:Parameter Name="ChequeNo" Type="String" />
                <asp:Parameter Name="Paymode" Type="String" />
                <asp:Parameter Name="Billno" Type="String" />
                <asp:Parameter Name="NewTransNo" Type="Int32" Direction="Output" />
            </UpdateParameters>
            <InsertParameters>
                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                <asp:Parameter Name="RefNo" Type="String" />
                <asp:Parameter Name="TransDate" Type="DateTime" />
                <asp:Parameter Name="DebitorID" Type="Int32" />
                <asp:Parameter Name="CreditorID" Type="Int32" />
                <asp:Parameter Name="Amount" Type="Double" />
                <asp:Parameter Name="Narration" Type="String" />
                <asp:Parameter Name="VoucherType" Type="String" />
                <asp:Parameter Name="ChequeNo" Type="String" />
                <asp:Parameter Name="PaymentMode" Type="String" />
                <asp:Parameter Name="NewTransNo" Type="Int32" Direction="Output" />
            </InsertParameters>
        </asp:ObjectDataSource>
        <input type="hidden" id="Hidden1" runat="server" />
        <asp:HiddenField ID="hdSMS" runat="server" Value="NO" />
        <asp:HiddenField ID="hdText" runat="server" />
        <asp:HiddenField ID="hdMobile" runat="server" />
        <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
    </div>
    </form>
</body>
</html>
