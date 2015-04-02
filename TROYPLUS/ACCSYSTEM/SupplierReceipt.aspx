<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="SupplierReceipt.aspx.cs" Inherits="SupplierReceipt" Title="Purchases > Supplier Receipt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">


        window.onload = function Showalert() {

            var txt = document.getElementById("<%= txtSearch.ClientID %>");
            var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");
            if (txt.value == "") {
                // alert(txt.value);
                btn.style.visibility = "hidden";
                // when the window is loaded, hide the button if the textbox is empty
            }

        }

        function EnableDisableButton(sender, target) {
            var first = document.getElementById('<%=txtSearch.ClientID %>');

            if (sender.value.length >= 1 && first.value.length >= 1) {
                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "visible";

            }

            if (sender.value.length < 1 && first.value.length < 1) {

                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "Hidden";
            }
        }
        /*@cc_on@*/
        /*@if (@_win32 && @_jscript_version>=5)

        function window.confirm(str) {
            execScript('n = msgbox("' + str + '","4132")', "vbscript");
            return (n == 6);
        }

        @end@*/

        function ConfirmSMS() {
            if (Page_IsValid) {
                var confSMS = document.getElementById('ctl00_cplhControlPanel_hdSMS').value;

                var confSMSRequired = document.getElementById('ctl00_cplhControlPanel_hdSMSRequired').value;

                var txtMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtMobileAdd');

                if (txtMobile == null)
                    txtMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tabEdit_tabEditMain_txtMobile');

                if (txtMobile != null) {
                    if (txtMobile.value != "") {
                        if (confSMSRequired == "YES") {
                            var rv = confirm("Do you want to send SMS to Customer?");

                            if (rv == true) {
                                document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "YES";
                                return true;
                            }
                            else {
                                document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "NO";
                                return true;
                            }
                        }
                    }
                }
            }
        }

        function EditMobile_Validator() {
            var txtMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtMobile').value;

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

        function AddMobile_Validator() {
            var txtMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_txtMobileAdd').value;

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


        function PrintItem(ID) {
            window.showModalDialog('./PrintReceipt.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
        }


        function AdvancedTest(id) {

            var panel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tblBank');
            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');

            var grd = document.getElementById("<%= frmViewAdd.ClientID %>");

            var rdoArray = grd.getElementsByTagName("input");


            for (i = 0; i <= rdoArray.length - 1; i++) {
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

        function AdvancedAdd(id) {

            var panel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tblBankAdd');
            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');

            var grd = document.getElementById("<%= frmViewAdd.ClientID %>");

            var rdoArray = grd.getElementsByTagName("input");


            for (i = 0; i <= rdoArray.length - 1; i++) {
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
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Supplier Receipts</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Supplier Receipts
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="0" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%"></td>
                                    <td style="width: 30%; font-size: 22px; color: White;">Suppliers Receipts
                                    </td>
                                    <td style="width: 10%">
                                        <div style="text-align: right;">
                                        
                                        </div>
                                    </td>
                                    <td style="width: 13%; color: White;" align="right">Search
                                            <asp:RequiredFieldValidator ID="rvSearch" runat="server" ControlToValidate="txtSearch"
                                                Display="Dynamic" EnableClientScript="False" Enabled="false">Search is mandatory</asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                         <cc1:CalendarExtender ID="txtdate" runat="server"
                                            Enabled="false" TargetControlID="txtSearch" Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" OnSelectedIndexChanged="ddCriteria_SelectedIndexChanged" AutoPostBack="true"  Width="154px" Height="23px" BackColor="White" Style="text-align: center; border: 1px solid White">
                                                <asp:ListItem Value="TransNo">Trans. No.</asp:ListItem>
                                                <asp:ListItem Value="RefNo">Ref. No.</asp:ListItem>
                                                <asp:ListItem Value="TransDate">Transaction Date</asp:ListItem>
                                                <asp:ListItem Value="LedgerName">Supplier Name</asp:ListItem>
                                                <asp:ListItem Value="AutoLedgerID">Supplier ID</asp:ListItem>
                                                <asp:ListItem Value="Narration">Narration</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 21%; text-align: left">
                                        <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click"
                                            CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" />
                                    </td>
                                    <td style="width: 20%" class="tblLeftNoPad">
                                        <asp:Button ID="BtnClearFilter" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="popUp" Style="width: 60%">
                            <div id="contentPopUp">
                                <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3; background-color: #fff; color: #000;"
                                    width="100%">
                                    <tr>
                                        <td>
                                            <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource" OnDataBound="frmViewAdd_DataBound" OnModeChanged="frmViewAdd_ModeChanged"
                                                OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit" DataKeyNames="TransNo"
                                                OnItemUpdated="frmViewAdd_ItemUpdated" OnItemCreated="frmViewAdd_ItemCreated"
                                                Visible="False" OnItemInserting="frmViewAdd_ItemInserting" EmptyDataText="No Records"
                                                OnItemInserted="frmViewAdd_ItemInserted">
                                                <RowStyle HorizontalAlign="left" CssClass="GrdContent allPad" VerticalAlign="Middle"
                                                    BorderColor="#cccccc" Height="20px" />
                                                <EditRowStyle HorizontalAlign="left" CssClass="GrdAlternateColor allPad" BorderColor="#cccccc"
                                                    VerticalAlign="middle" Height="20px" />
                                                <HeaderStyle HorizontalAlign="left" CssClass="GrdHeaderbgClr GrdHdrContent allPad"
                                                    Height="25px" BorderColor="#cccccc" VerticalAlign="Middle" />
                                                <EditItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="3" style="border: 1px solid #86b2d1; width: 800px;">
                                                            <tr>
                                                                <td colspan="5" class="headerPopUp">Supplier Receipt
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 24%">
                                                                    Branch *
                                                                    <asp:CompareValidator ID="CompareValidator123" runat="server" ControlToValidate="drpBranch"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Branch. It cannot be left blank."
                                                                                                Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 25%">
                                                                    <asp:DropDownList ID="drpBranch" TabIndex="10" SelectedValue='<%# Bind("BranchCode") %>' DataSourceID="srcBranch" OnDataBound="drpBranch_DataBound" DataTextField="BranchName" DataValueField="BranchCode" Enabled="false" Width="100%" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                 runat="server">
                                                                                                <asp:ListItem Text="Select Branch" Value="0"></asp:ListItem>
                                                                                             </asp:DropDownList>
                                                                </td>
                                                                <td class="ControlLabel" style="width: 15%">
                                                                    <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtTransDate"
                                                                        ErrorMessage="Trasaction Date is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator ControlToValidate="txtTransDate" Operator="DataTypeCheck" Type="Date"
                                                                        ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate">*</asp:CompareValidator>
                                                                    <asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtTransDate"
                                                                        ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                    Received Date *
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtTransDate" Enabled="false" runat="server" Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                                        CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calExt3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                        PopupButtonID="btnD3" PopupPosition="BottomLeft" TargetControlID="txtTransDate">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td align="left" style="width: 10%">
                                                                    <asp:ImageButton ID="btnD3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 24%">Ref. No. *
                                                                    <asp:RequiredFieldValidator ID="rvRefNo" runat="server" ErrorMessage="Please enter Ref. No. It cannot be left blank."
                                                                        ControlToValidate="txtRefNo" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("RefNo") %>' Width="100%"
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                 <td class="ControlLabel" style="width: 15%">
                                                                    <asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                                                        ErrorMessage="Please enter Amount. It cannot be left blank." Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="fltAmt" runat="server" TargetControlID="txtAmount"
                                                                        ValidChars="." FilterType="Numbers, Custom" />
                                                                    Amount *
                                                                </td>
                                                                <td class="ControlNumberBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="100%"
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                                
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 24%">

                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ComboBox2"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Received From. It cannot be left blank."
                                                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                    Received From *
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 25%">
                                                                    <asp:DropDownList ID="ComboBox2" runat="server" AutoPostBack="True" Width="100%" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium"
                                                                        DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" OnSelectedIndexChanged="ComboBox2_SelectedIndexChanged"
                                                                        DataTextField="LedgerName" AppendDataBoundItems="true" OnDataBound="ComboBox2_DataBound">
                                                                        <asp:ListItem style="background-color: #e7e7e7" Text="Select Supplier" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                  <td class="ControlLabel" style="width: 15%">Narration *
                                                                    <asp:RequiredFieldValidator ID="rvNarration" runat="server" ErrorMessage="Please enter Narration. It cannot be left blank."
                                                                        ControlToValidate="txtNarration" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtNarration" runat="server" Height="30px" TextMode="MultiLine"
                                                                        Text='<%# Bind("Narration") %>' Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                               
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 24%">
                                                                    <asp:RequiredFieldValidator ID="rvBDate" runat="server" ControlToValidate="chkPayTo"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Item Name is mandatory.">*</asp:RequiredFieldValidator>
                                                                    Payment Made By *
                                                                </td>
                                                                <td class="ControlTextBox3" align="left" style="width: 25%">
                                                                    <asp:RadioButtonList ID="chkPayTo" runat="server" onclick="javascript:AdvancedTest(this.id);"
                                                                        AutoPostBack="False" Width="100%" OnDataBound="chkPayTo_DataBound" OnSelectedIndexChanged="chkPayTo_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                                                                        <asp:ListItem Text="Cheque"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td class="ControlLabel" style="width: 15%">Phone No
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtPhone" />
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtPhone" runat="server" Width="100%"
                                                                        MaxLength="10" CssClass="cssTextBox"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                              
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 24%">Mobile
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxEx" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtMobile" />
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtMobile" runat="server" Text='<%# Bind("Mobile") %>' Width="100%"
                                                                        MaxLength="10" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td class="ControlLabel" style="width: 15%">
                                                                    
                                                                </td>
                                                                <td style="width: 25%">
                                                                    
                                                                </td>
                                                                <td align="left" style="width: 10%">
                                                                
                                                            </tr>
                                                            <tr style="height: 2px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="PanelBank" runat="server">
                                                                        <table width="100%" id="tblBank" cellpadding="1" cellspacing="1" class="" runat="server">
                                                                            <tr>
                                                                                <td style="width: 24%;" class="ControlLabel">

                                                                                    <asp:CompareValidator ID="cvBank" runat="server" ControlToValidate="ddBanks" Display="Dynamic"
                                                                                        EnableClientScript="false" ErrorMessage="Bank Name is Mandatory" Operator="GreaterThan"
                                                                                        ValueToCompare="0">*</asp:CompareValidator>
                                                                                    Bank Name *
                                                                                </td>
                                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                                    <asp:DropDownList ID="ddBanks" Enabled="true" runat="server" OnDataBound="ddBanks_DataBound" Width="100%" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium"
                                                                                        DataSourceID="srcBanks" DataTextField="LedgerName" DataValueField="LedgerID"
                                                                                        AppendDataBoundItems="True">
                                                                                        <asp:ListItem Selected="True" style="background-color: #e7e7e7" Value="0">Select Bank</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td style="width: 15%" class="ControlLabel">
                                                                                    <asp:RequiredFieldValidator ID="rvChequeNo" runat="server" ControlToValidate="txtChequeNo"
                                                                                        ErrorMessage="Cheque No. is mandatory" Display="Dynamic" EnableClientScript="False">*</asp:RequiredFieldValidator>
                                                                                    Cheque No. *
                                                                                </td>
                                                                                <td style="width: 25%;" class="ControlTextBox3">
                                                                                    <asp:TextBox ID="txtChequeNo" runat="server" Text='<%# Bind("ChequeNo") %>' Width="96.5%"
                                                                                        CssClass="cssTextBox"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 10%"></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                                <td colspan="4"></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 36%;"></td>
                                                                            <td align="right" style="width: 17%;">
                                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClientClick="javascript:EditMobile_Validator();ConfirmSMS();"
                                                                                    OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="right" style="width: 17%;">
                                                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="right" style="width: 30%;"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                                <asp:ObjectDataSource ID="srcBranch" runat="server" SelectMethod="ListBranch"
                                                                    TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                        <asp:CookieParameter Name="User" CookieName="LoggedUserName" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListSundryCreditors"
                                                                    TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                    ShowSummary="false" HeaderText="" Font-Names="'Trebuchet MS'"
                                                                    Font-Size="12" runat="server" />
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <InsertItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid Gray; width: 800px;">
                                                            <tr>
                                                                <td colspan="5" class="headerPopUp">Supplier Receipt
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 24%">
                                                                    Branch *
                                                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpBranchAdd"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Branch. It cannot be left blank."
                                                                                                Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:DropDownList ID="drpBranchAdd" TabIndex="10" Width="100%" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                 runat="server">
                                                                                             </asp:DropDownList>
                                                                </td>
 <td class="ControlLabel" style="width: 15%">
                                                                    <%--<asp:Label ID="Label1" runat="server"
                                                                                                        Width="120px" Text="Received Date *" ></asp:Label>--%>
                                                                    <asp:RequiredFieldValidator ID="rvStockAdd" runat="server" ControlToValidate="txtTransDateAdd"
                                                                        ErrorMessage="Trans. Date is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator ControlToValidate="txtTransDateAdd" Operator="DataTypeCheck"
                                                                        Type="Date" ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDateAdd">*</asp:CompareValidator>
                                                                    <asp:RangeValidator ID="myRangeValidatorAdd" runat="server" ControlToValidate="txtTransDateAdd"
                                                                        ErrorMessage="Receipt date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                    Received Date *
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtTransDateAdd" Enabled="false" runat="server" Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                                        CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                        PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td style="width: 10%" align="left">
                                                                    <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                        Width="20px" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 24%">Ref. No. *
                                                                    <asp:RequiredFieldValidator ID="rvRefNoAdd" runat="server" ErrorMessage="Please enter Ref. No. It cannot be left blank."
                                                                        ControlToValidate="txtRefNoAdd" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtRefNoAdd" runat="server" Text='<%# Bind("RefNo") %>' Width="100%"
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                  <td class="ControlLabel" style="width: 15%">Amount *
                                                                    <asp:RequiredFieldValidator ID="rvModelAdd" runat="server" ControlToValidate="txtAmountAdd"
                                                                        ErrorMessage="Please enter Amount. It cannot be left blank." Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                    <cc1:FilteredTextBoxExtender ID="fltAmtAdd" runat="server" TargetControlID="txtAmountAdd"
                                                                        ValidChars="." FilterType="Numbers, Custom" />
                                                                </td>
                                                                <td class="ControlNumberBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtAmountAdd" runat="server" Text='<%# Bind("Amount") %>' Width="100%"
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                               
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 24%">Received From *
                                                                    <asp:CompareValidator ID="CV1Add" runat="server" ControlToValidate="ComboBox2Add"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Received From. It cannot be left blank."
                                                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                </td>
                                                                <td class="ControlDrpBorder" style="width: 25%">
                                                                    <asp:DropDownList ID="ComboBox2Add" runat="server" Width="100%" BackColor="#e7e7e7" CssClass="drpDownListMedium" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                        AutoPostBack="True" OnSelectedIndexChanged="ComboBox2Add_SelectedIndexChanged"
                                                                        DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName"
                                                                        AppendDataBoundItems="true">
                                                                        <asp:ListItem style="background-color: #e7e7e7" Text="Select Supplier" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                 <td class="ControlLabel" style="width: 15%">Narration *
                                                                    <asp:RequiredFieldValidator ID="rvNarrationAdd" runat="server" ErrorMessage="Please enter Narration. It cannot be left blank. "
                                                                        ControlToValidate="txtNarrationAdd" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                    <asp:TextBox ID="txtNarrationAdd" runat="server" Height="29px" TextMode="MultiLine" Width="95%"
                                                                        Text='<%# Bind("Narration") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                              
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 24%">Payment Made By *
                                                                    <asp:RequiredFieldValidator ID="rvBDateAdd" runat="server" ControlToValidate="chkPayToAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Item Name is mandatory."></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td class="ControlTextBox3" align="left" style="width: 25%">
                                                                    <asp:RadioButtonList ID="chkPayToAdd" runat="server" OnDataBound="chkPayToAdd_DataBound"
                                                                        onclick="javascript:AdvancedAdd(this.id);" AutoPostBack="false" Width="100%"
                                                                        OnSelectedIndexChanged="chkPayToAdd_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                                                                        <asp:ListItem Text="Cheque"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                 <td style="width: 15%" class="ControlLabel">Phone No
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtPhoneAdd" />
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                    <asp:TextBox ID="txtPhoneAdd" runat="server" Width="100%"
                                                                        MaxLength="10" CssClass="cssTextBox"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 10%"></td>
                                                               
                                                            </tr>
                                                            <tr style="height: 3px">
                                                            </tr>
                                                            <tr>
                                                                <td class="ControlLabel" style="width: 24%">Mobile
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txtMobileAdd" />
                                                                </td>
                                                                <td class="ControlTextBox3" style="width: 24.8%">
                                                                    <asp:TextBox ID="txtMobileAdd" runat="server" Text='<%# Bind("Mobile") %>' Width="100%"
                                                                        MaxLength="10" CssClass="cssTextBox"></asp:TextBox>
                                                                </td>
                                                                <td class="ControlLabel" style="width: 15%">
                                                                    
                                                                </td>
                                                                <td style="width: 25%">
                                                                    
                                                                </td>
                                                                <td align="left" style="width: 10%">
                                                               
                                                            </tr>
                                                            <tr style="height: 2px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <asp:Panel ID="PanelBankAdd" runat="server" Width="100%">
                                                                        <table style="width: 100%" id="tblBankAdd" cellpadding="0" cellspacing="1" runat="server">
                                                                            <tr>
                                                                                <td style="width: 24%;" class="ControlLabel">
                                                                                    <asp:CompareValidator ID="cvBankAdd" runat="server" ControlToValidate="ddBanksAdd"
                                                                                        Display="Dynamic" EnableClientScript="False" ErrorMessage="Bank Name is Mandatory"
                                                                                        Text="*" Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                    Bank Name *
                                                                                </td>
                                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                                    <asp:DropDownList ID="ddBanksAdd" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium" Style="border: 0px solid #e7e7e7" Height="26px" Width="100%" SelectedValue='<%# Bind("CreditorID") %>'
                                                                                        DataSourceID="srcBanksAdd" DataTextField="LedgerName" DataValueField="LedgerID"
                                                                                        AppendDataBoundItems="True">
                                                                                        <asp:ListItem Selected="True" Value="0">Select Bank</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td style="width: 15%" class="ControlLabel">
                                                                                    <asp:RequiredFieldValidator ID="rvChequeNoAdd" runat="server" ControlToValidate="txtChequeNoAdd"
                                                                                        ErrorMessage="Cheque No. is mandatory" Display="Dynamic" EnableClientScript="false">*</asp:RequiredFieldValidator>
                                                                                    Cheque No. *
                                                                                </td>
                                                                                <td style="width: 25%;" class="ControlTextBox3">
                                                                                    <asp:TextBox ID="txtChequeNoAdd" runat="server" Text='<%# Bind("ChequeNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                </td>
                                                                                <td style="width: 10%"></td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td style="width: 36%;"></td>
                                                                            <td align="right" style="width: 17%;">
                                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClientClick="javascript:AddMobile_Validator();ConfirmSMS();"
                                                                                    OnClick="InsertButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="right" style="width: 17%;">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 30%;"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcBanksAdd" runat="server" SelectMethod="ListBanksIsActive" TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcCreditorDebitorAdd" runat="server" SelectMethod="ListSundryCreditorsIsActive"
                                                                    TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:ValidationSummary ID="valSumAdd" DisplayMode="BulletList" ShowMessageBox="true"
                                                                    ShowSummary="false" HeaderText="" Font-Names="'Trebuchet MS'"
                                                                    Font-Size="12" runat="server" />
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewReceipt" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewReceipt_RowCreated" Width="100.4%" DataSourceID="GridSource"
                                            AllowPaging="True" DataKeyNames="TransNo" EmptyDataText="No Supplier Receipts found!"
                                            OnRowCommand="GrdViewReceipt_RowCommand" OnRowDataBound="GrdViewReceipt_RowDataBound"
                                            OnSelectedIndexChanged="GrdViewReceipt_SelectedIndexChanged" OnRowDeleting="GrdViewReceipt_RowDeleting"
                                            OnRowDeleted="GrdViewReceipt_RowDeleted" CssClass="someClass">
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="TransNo" HeaderText="Trans. No." HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="RefNo" HeaderText="Ref. No." HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="LedgerName" HeaderText="Received From" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Debi" HeaderText="Bank Name / Cash" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Narration" HeaderText="Narration" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="BranchCode" HeaderText="Branch Code" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Receipt?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Print" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <a href='<%# DataBinder.Eval(Container, "DataItem.TransNo", "javascript:PrintItem({0});") %>'>
                                                            <asp:Image runat="server" ID="lnkprint" alt="Print" border="0" src="App_Themes/DefaultTheme/Images/Print1.png" />
                                                        </a>
                                                        <asp:ImageButton ID="btnViewDisabled" Enabled="false" SkinID="search" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" BackColor="#e7e7e7" Style="border: 1px solid blue" Width="65px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="border-color: white; width: 5px"></td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnFirst" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnPrevious" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnNext" />
                                                        </td>
                                                        <td style="border-color: white">
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
                <tr style="width: 100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListReceiptsSuppliers"
                            TypeName="BusinessLogic" DeleteMethod="DeleteReceipt" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="TransNo" Type="Int32" />
                                <asp:Parameter Name="requireValidation" Type="Boolean" DefaultValue="true" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetReceiptForId"
                            TypeName="BusinessLogic" InsertMethod="InsertSupplierReceipt" OnUpdating="frmSource_Updating"
                            OnInserting="frmSource_Inserting" UpdateMethod="UpdateSupplierReceipt" OnInserted="frmSource_Inserted"
                            OnUpdated="frmSource_Updated">
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
                                <asp:Parameter Name="NewTransNo" Type="Int32" Direction="Output" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="BranchCode" Type="String" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewReceipt" Name="TransNo" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
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
                                <asp:Parameter Name="Paymode" Type="String" />
                                <asp:Parameter Name="NewTransNo" Type="Int32" Direction="Output" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="BranchCode" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%"></td>
                </tr>
            </table>
            <input type="hidden" id="hidAdvancedState" runat="server" />
            <asp:HiddenField ID="hdSMS" runat="server" Value="NO" />
            <asp:HiddenField ID="hdText" runat="server" />
            <asp:HiddenField ID="hdMobile" runat="server" />
            <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
            <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table align="center">
        <tr>
            <td style="width:50%">
                    <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                                            </asp:Panel>
            </td>
            <td style="width:50%">
                <asp:Button ID="btnrec" runat="server"
                    CssClass="exportexl6" EnableTheming="false" CausesValidation="false"
                    OnClientClick="window.open('ReportExcelReceipts.aspx?ID=SupRec','Summary', 'toolbar=no,status=no,menu=no,location=no,height=280,width=650,left=405,top=220 ,resizable=yes, scrollbars=yes');"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
