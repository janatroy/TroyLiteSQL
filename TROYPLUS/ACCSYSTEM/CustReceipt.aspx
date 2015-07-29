<%@ Page Title="Sales > Customer / Dealer Receipt" Language="C#" MasterPageFile="~/PageMaster.master"
    AutoEventWireup="true" CodeFile="CustReceipt.aspx.cs" Inherits="CustReceipt" %>

<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

        /*@cc_on@*/
        /*@if (@_win32 && @_jscript_version>=5)

        function window.confirm(str) {
            execScript('n = msgbox("' + str + '","4132")', "vbscript");
            return (n == 6);
        }

        @end@*/

       <%-- window.onload = function Showalert() {

            var txt = document.getElementById("<%= txtSearch.ClientID %>");
            var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");
            if (txt.value == "") {
                alert(btn);
                // alert(txt.value);
                btn.style.visibility = "hidden";
                // when the window is loaded, hide the button if the textbox is empty
            }

        }--%>
        <%--function Show() {
            document.getElementById("<%= BtnClearFilter.ClientID %>").style.visibility = "visible";
        }

        window.onload = function Hide() {
            var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");
            btn.style.visibility = "hidden";
            alert("hide");
            document.getElementById("<%= BtnClearFilter.ClientID %>").style.visibility = "Hidden";
        }--%>

       <%-- function EnableDisableButton(sender, target) {
            var first = document.getElementById('<%=txtSearch.ClientID %>');

            if (sender.value.length >= 1 && first.value.length >= 1) {
                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "visible";

            }

            if (sender.value.length < 1 && first.value.length < 1) {

                document.getElementById('<%=BtnClearFilter.ClientID %>').style.visibility = "Hidden";
                alert(length);
            }
        }--%>



        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_tabs2');

            //        if (tabContainer == null)
            //            tabContainer = $find('ctl00_cplhControlPanel_tabPanel2');

            if (tabContainer != null) {
                //  get all of the tabs from the container
                var tabs = tabContainer.get_tabs();

                //  loop through each of the tabs and attach a handler to
                //  the tab header's mouseover event
                for (var i = 0; i < tabs.length; i++) {
                    var tab = tabs[i];

                    $addHandler(
                tab.get_headerTab(),
                'mouseover',
                Function.createDelegate(tab, function () {
                    tabContainer.set_activeTab(this);
                }
            ));
                }
            }
        }

        function OnKeyPress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                $find("ctl00_cplhControlPanel_ModalPopupExtender2").hide();
            }
        }

        $("#ctl00_cplhControlPanel_UpdateCancelButton").live("click", function () {
            $find("ctl00_cplhControlPanel_ModalPopupExtender2").hide();
        });


        function CheckDate() {
            if (document.getElementById('ctl00_cplhControlPanel_hddate').value != '0') {
                if (document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hddatecheck').value != '0') {
                    var rv = confirm("Date in Future, Do you still want to continue?");

                    if (rv == true) {
                        return true;
                    }
                    else {
                        return window.event.returnValue = false;
                    }
                }
            }
            else {
                if (document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_hddatecheck').value != '0') {
                    alert('Cash Payment Cannot be Future');
                    return window.event.returnValue = false;
                }
            }

        }


        function CheckPendingBill() {
            if (document.getElementById('ctl00_cplhControlPanel_hdPendingCount').value != '0') {
                var rv = confirm("Unadjusted Invoice found, Do you still want to continue?");

                if (rv == true) {
                    return true;
                }
                else {
                    return window.event.returnValue = false;
                }
            }
        }

        function ConfirmSMS() {
            if (Page_IsValid) {
                var confSMS = document.getElementById('ctl00_cplhControlPanel_hdSMS').value;

                var confSMSRequired = document.getElementById('ctl00_cplhControlPanel_hdSMSRequired').value;

                var txtMobile = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_txtMobile');

                if (txtMobile == null)
                    txtMobile = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_txtMobile');

                if (txtMobile != null) {
                    if (txtMobile.value != "") {

                        if (confSMSRequired == "YES") {
                            var rv = confirm("Do you want to send SMS to Customer?");

                            if (rv == true) {
                                document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "YES";
                                return false;
                            }
                            else {
                                document.getElementById('ctl00_cplhControlPanel_hdSMS').value = "NO";
                                return false;
                            }
                        }
                    }
                }
            }
        }

        function Validate() {
            var txtAmount = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_txtAmount').value;

            if (txtAmount == "") {
                alert('Please enter the Receipt Amount before Adding BillNo');
                return true;
            }

            var e = document.getElementById("ctl00_cplhControlPanel_tabs2_tabMaster_ddReceivedFrom");

            var strUser = e.options[e.selectedIndex].value;

            if (strUser == "0") {
                alert('Please Select the Customer before Adding Bills');
                return true;
            }

        }

        function EditMobile_Validator() {
            var txtMobile = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_txtMobile').value;

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
            var txtMobile = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_txtMobile').value;

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


        function PrintItem(ID)
        {
            if (window.showModalDialog)
            {
                var dialogArguments = new Object();
                var _R = window.showModalDialog('./PrintReceipt.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
            }
            else

            {
                window.open('./PrintReceipt.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
                winHandle.focus();
            }
        }

        function ShowCreditSales() {
            //return window.open('./ShowSalesBills.aspx', self, 'dialogWidth:800px;dialogHeight:350px;scroll:on;status:no;dialogHide:yes;unadorned:no;');
            window.open("ShowSalesBills.aspx", "TROY", "toolbar=no,menubar=no,resizable=yes,status=yes,height=450px,width=700px,scrollbars=yes");
        }

        function AdvancedTest(id) {

            var panel = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_tblBank');
            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');

            var rdoArray = document.getElementsByTagName("input");


            for (i = 0; i <= rdoArray.length - 1; i++) {
                //alert(rdoArray[i].type);
                if (rdoArray[i].type == 'radio') {

                    if (rdoArray[i].value == 'Cash' && rdoArray[i].checked == true) {
                        document.getElementById('ctl00_cplhControlPanel_hddate').value = "0";
                        panel.className = "hidden";
                        adv.value = panel.className;
                    }
                    else if ((rdoArray[i].value == 'Cheque' || rdoArray[i].value == 'Card') && rdoArray[i].checked == true) {
                        document.getElementById('ctl00_cplhControlPanel_hddate').value = "1";
                        panel.className = "AdvancedSearch";
                        adv.value = panel.className;
                    }
                }
            }
        }

        function AdvancedAdd(id) {

            var panel = document.getElementById('ctl00_cplhControlPanel_tabs2_tabMaster_tblBankAdd');
            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');

            var rdoArray = document.getElementsByTagName("input");


            for (i = 0; i <= rdoArray.length - 1; i++) {
                //alert(rdoArray[i].type);
                if (rdoArray[i].type == 'radio') {

                    if (rdoArray[i].value == 'Cash' && rdoArray[i].checked == true) {
                        document.getElementById('ctl00_cplhControlPanel_hddate').value = "0";
                        panel.className = "hidden";
                        adv.value = panel.className;
                    }
                    else if (rdoArray[i].value == 'Cheque' && rdoArray[i].checked == true) {
                        document.getElementById('ctl00_cplhControlPanel_hddate').value = "1";
                        panel.className = "AdvancedSearch";
                        adv.value = panel.className;
                    }
                    else if (rdoArray[i].value == 'Card' && rdoArray[i].checked == true) {
                        document.getElementById('ctl00_cplhControlPanel_hddate').value = "1";
                        panel.className = "AdvancedSearch";
                        adv.value = panel.className;
                    }
                }
            }
        }

        function Showalert() {


            var txt = document.getElementById("<%= txtSearch.ClientID %>");
            var btn = document.getElementById("<%= BtnClearFilter.ClientID %>");
            if (txt.value == "") {
                //  alert("hide");
                // alert(txt.value);
                btn.style.visibility = "hidden";
                // when the window is loaded, hide the button if the textbox is empty
            }
            else {
                //  alert("show");
                btn.style.visibility = "visible";
            }

        }
    </script>

    <style id="Style1" runat="server">
        /*.someClass td {
            font-size: 12px;
            border: 1px solid Gray;
        }

        .fancy-green .ajax__tab_header {
            background: url(App_Themes/NewTheme/Images/green_bg_Tab.gif) repeat-x;
            cursor: pointer;
        }

        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer {
            background: url(App_Themes/NewTheme/Images/green_left_Tab.gif) no-repeat left top;
        }

        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner {
            background: url(App_Themes/NewTheme/Images/green_right_Tab.gif) no-repeat right top;
        }

        .fancy .ajax__tab_header {
            font-size: 13px;
            font-weight: bold;
            color: #000;
            font-family: sans-serif;
        }

            .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer {
                height: 46px;
            }

            .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner {
                height: 46px;
                margin-left: 16px; /* offset the width of the left image */
            /*}

            .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab {
                margin: 16px 16px 0px 0px;
            }

        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab {
            color: #fff;
        }

        .fancy .ajax__tab_body {
            font-family: Arial;
            font-size: 10pt;
            border-top: 0;
            border: 1px solid #999999;
            padding: 8px;
            background-color: #ffffff;
        }*/
    </style>

    <asp:UpdatePanel ID="UpdatePanelPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <table style="width: 100%;" align="center">
                <tr style="width: 100%">
                    <td style="width: 100%" valign="middle">

                        <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Customer Receipts</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Customer Receipts
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">

                            <table cellspacing="0" cellpadding="3px" border="0" width="99.8%"
                                class="searchbg" style="margin: -2px 0px 0px 1px;">
                                <tr>
                                    <td style="width: 2%"></td>
                                    <td style="width: 30%; font-size: 22px; color: White;">Receipts from Customer
                                    </td>
                                    <td style="width: 2%">
                                        <div style="text-align: right;">
                                           
                                        </div>
                                    </td>
                                    <td style="width: 10%; color: White;" align="right">Search
                                    </td>
                                    <td style="width: 21%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                    </td>
                                    <td style="width: 21%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid White">
                                                <%--<asp:ListItem Value="0" style="background-color: #bce1fe">All</asp:ListItem>--%>
                                                <asp:ListItem Value="TransNo">Trans. No.</asp:ListItem>
                                                <asp:ListItem Value="RefNo">Bill No.</asp:ListItem>
                                                <asp:ListItem Value="TransDate">Transaction Date</asp:ListItem>
                                                <asp:ListItem Value="LedgerName">Received From</asp:ListItem>
                                                <asp:ListItem Value="Narration">Narration</asp:ListItem>
                                                 <asp:ListItem Value="BranchCode">Branch</asp:ListItem>
                                            </asp:DropDownList>
                                    </td>
                                    <td style="width: 15%; text-align: left">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch6" onkeyup="show()"
                                            EnableTheming="false" ForeColor="White" OnClick="btnSearch_Click" />
                                    </td>
                                    <td style="width: 16%" class="tblLeftNoPad">
                                        <asp:Button ID="BtnClearFilter" onkeyup="hide()" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                    </td>
                                </tr>
                            </table>

                        </div>




                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <asp:ValidationSummary ID="VS" runat="server" Font-Names="'Trebuchet MS'"
                            Font-Size="12pt" HeaderText=" " ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="salesval" />
                        <asp:Panel runat="server" ID="popUp" Style="width: 61%; display: none">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="contentPopUp">
                                        <asp:Panel ID="pnlEdit" runat="server">
                                            <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3; background-color: #fff; color: #000;"
                                                width="100%">
                                                <tr>
                                                    <td>
                                                        <div class="divArea">
                                                            <table class="tblLeft" cellpadding="3" cellspacing="0" style="padding-left: 5px;"
                                                                width="100%">
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <table class="headerPopUp">
                                                                            <tr>
                                                                                <td>Customer Receipt
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                            <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Receipt Details">
                                                                                <HeaderTemplate>
                                                                                    <div>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td><b>Receipt Details </b></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>

                                                                                    <div style="text-align: left">
                                                                                        <table style="width: 800px; border: 0px solid #86b2d1" align="center" cellpadding="0" cellspacing="2">
                                                                                            <tr style="height: 10%">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="ControlLabel" style="width: 24%">
                                                                                                    Branch *
                                                                    <asp:CompareValidator ID="CompareValidator123" runat="server" ControlToValidate="drpBranch"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Branch. It cannot be left blank."
                                                                                                Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                                </td>
                                                                                                <td class="ControlDrpBorder" style="width: 25%">
                                                                                                   <asp:DropDownList ID="drpBranch" TabIndex="10" Enabled="false" Width="100%" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                 runat="server">
                                                                                                <asp:ListItem Text="Select Branch" Value="0"></asp:ListItem>
                                                                                             </asp:DropDownList>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 16%">
                                                                                                    
                                                                                                </td>
                                                                                                <td style="width: 25%">
                                                                                                    
                                                                                                </td>
                                                                                                <td>
                                                                                                   
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="ControlLabel" style="width: 24%">Ref. No. *
                                                                                                                <asp:RequiredFieldValidator ID="rvRefNo" runat="server" ErrorMessage="Please enter Ref. No. It cannot be left blank."
                                                                                                                    ControlToValidate="txtRefNo" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("RefNo") %>' Width="100px"
                                                                                                        CssClass="cssTextBox"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 16%">
                                                                                                    <asp:Label ID="Label1" runat="server"
                                                                                                        Width="120px" Text="Received Date *"></asp:Label>
                                                                                                    <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtTransDate"
                                                                                                        ErrorMessage="Trasaction Date is mandatory" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                                                    <asp:CompareValidator ControlToValidate="txtTransDate" Operator="DataTypeCheck" Type="Date" Display="Dynamic"
                                                                                                        ErrorMessage="Please select a valid date. It cannot be left blank." runat="server" ID="cmpValtxtDate">*</asp:CompareValidator>
                                                                                                    <%--<asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtTransDate"
                                                                                                                    ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>--%>
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtTransDate" runat="server" Enabled="false" OnTextChanged="txtTransDate_TextChanged" AutoPostBack="true" Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                                                                        CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                                <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                    PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDate">
                                                                                                                </cc1:CalendarExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                        Width="20px" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="ControlLabel" style="width: 24%">Received From *
                                                                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddReceivedFrom"
                                                                                                                    Display="Dynamic" ErrorMessage="Please select Received From. It cannot be left blank." Operator="GreaterThan"
                                                                                                                    ValueToCompare="0">*</asp:CompareValidator>
                                                                                                </td>
                                                                                                <td class="ControlDrpBorder" style="width: 25%">
                                                                                                    <asp:DropDownList ID="ddReceivedFrom" runat="server" AutoPostBack="True" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                        OnSelectedIndexChanged="ComboBox2_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        AppendDataBoundItems="True">
                                                                                                        <%--<asp:ListItem Text="Select Customer" Value="0"></asp:ListItem>--%>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 16%">Amount *
                                                                                                                <asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                                                                                                    Display="Dynamic" ErrorMessage="Please enter Amount. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                                                    <cc1:FilteredTextBoxExtender ID="fltAmt" runat="server" TargetControlID="txtAmount"
                                                                                                        ValidChars="." FilterType="Custom, Numbers" Enabled="True" />
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="ControlLabel" style="width: 24%">Payment Made By *
                                                                                                                <asp:RequiredFieldValidator ID="rvBDate" runat="server" ControlToValidate="chkPayTo"
                                                                                                                    Display="Dynamic" ErrorMessage="Please select Item Name. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:RadioButtonList ID="chkPayTo" runat="server" onclick="javascript:AdvancedTest(this.id);"
                                                                                                        Width="100%" OnSelectedIndexChanged="chkPayTo_SelectedIndexChanged">
                                                                                                        <asp:ListItem Text="Cash" Selected="True"></asp:ListItem>
                                                                                                        <asp:ListItem Text="Cheque"></asp:ListItem>
                                                                                                        <asp:ListItem Text="Card"></asp:ListItem>
                                                                                                    </asp:RadioButtonList>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 16%">Narration *
                                                                                                                <asp:RequiredFieldValidator ID="rvNarration" runat="server" ErrorMessage="Please enter Narration. It cannt be left blank. "
                                                                                                                    ControlToValidate="txtNarration" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtNarration" runat="server" Height="25px" TextMode="MultiLine"
                                                                                                        Text='<%# Bind("Narration") %>' Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <%--<tr style="height:0px">
                                                                                                        </tr>--%>
                                                                                            <tr>
                                                                                                <td class="ControlLabel" style="width: 24%">Mobile
                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxEx" runat="server" FilterType="Numbers"
                                                                                                                    TargetControlID="txtMobile" Enabled="True" />
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtMobile" runat="server" Text='<%# Bind("Mobile") %>' MaxLength="10" Height="26px"
                                                                                                        CssClass="cssTextBox" Width="98%"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 16%">Phone No
                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                                                                    TargetControlID="txtPhone" Enabled="True" />
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>' MaxLength="10" Height="26px"
                                                                                                        CssClass="cssTextBox" Width="98%"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="4" width="100%">
                                                                                                    <asp:Panel ID="PanelBank" runat="server">
                                                                                                        <table width="100%" id="tblBank" runat="server" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td class="ControlLabel" style="width: 24%">
                                                                                                                    <%--<asp:CompareValidator ID="cvBank" runat="server" ControlToValidate="ddBanks" Display="Dynamic"
                                                                                                                                    EnableClientScript="False" ErrorMessage="Bank Name is Mandatory" Operator="GreaterThan"
                                                                                                                                    ValueToCompare="0">*</asp:CompareValidator>--%>
                                                                                                                                    Bank Name *
                                                                                                                </td>
                                                                                                                <td style="width: 24.7%" class="ControlDrpBorder">
                                                                                                                    <asp:DropDownList ID="ddBanks" runat="server" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium" BackColor="#e7e7e7" Width="100%"
                                                                                                                        DataTextField="LedgerName" DataValueField="LedgerID" AppendDataBoundItems="True">
                                                                                                                        <asp:ListItem Selected="True" style="background-color: #e7e7e7" Value="0">Select Bank</asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td class="ControlLabel" style="width: 16.8%">
                                                                                                                    <%--<asp:RequiredFieldValidator ID="rvChequeNo" runat="server" ControlToValidate="txtChequeNo"
                                                                                                                                    ErrorMessage="Cheque No. is mandatory" Display="Dynamic" EnableClientScript="False">*</asp:RequiredFieldValidator>--%>
                                                                                                                                Cheque No. *
                                                                                                                </td>
                                                                                                                <td class="ControlTextBox3" style="width: 24.5%">
                                                                                                                    <asp:TextBox ID="txtChequeNo" runat="server" Text='<%# Bind("ChequeNo") %>' Height="26px" SkinID="skinTxtBoxGrid"
                                                                                                                        Width="95%" CssClass="cssTextBox"></asp:TextBox>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="4">
                                                                                                    <asp:UpdatePanel ID="UP1" runat="server" UpdateMode="Conditional">
                                                                                                        <Triggers>
                                                                                                            <asp:AsyncPostBackTrigger ControlID="txtTransDate" EventName="TextChanged" />
                                                                                                        </Triggers>
                                                                                                        <ContentTemplate>
                                                                                                            <asp:HiddenField ID="hddatecheck" runat="server" Value="0" />
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                            </tr>



                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label runat="server" ID="Error" ForeColor="Red"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>

                                                                                    </div>

                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Bill Details">
                                                                                <HeaderTemplate>
                                                                                    <div>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td><b>Bill Details </b></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table style="width: 800px" cellpadding="3" cellspacing="1">
                                                                                        <tr style="height: 3px">
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                                <asp:Panel ID="Panel1" BackColor="White" BorderStyle="Solid" BorderColor="White"
                                                                                                    Width="100%" runat="server">
                                                                                                    <div style="text-align: center;">
                                                                                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                                            <ContentTemplate>
                                                                                                                <asp:GridView ID="GrdBills" runat="server" AutoGenerateColumns="False" OnRowCreated="GrdBills_RowCreated"
                                                                                                                    Width="100%" PageSize="7" OnRowEditing="EditBill" OnRowCommand="GrdBills_RowCommand" CssClass="someClass"
                                                                                                                    OnRowDataBound="GrdBills_RowDataBound" AllowPaging="True" OnRowUpdating="GrdBills_RowUpdating"
                                                                                                                    OnRowCancelingEdit="GrdBillsCancelEdit" OnRowDeleting="GrdBills_RowDeleting"
                                                                                                                    EmptyDataText="No Bills Added." OnRowUpdated="GrdBills_RowUpdated">
                                                                                                                    <Columns>
                                                                                                                        <asp:TemplateField HeaderText="Bill No." HeaderStyle-BorderColor="Gray">
                                                                                                                            <ItemStyle Width="35%" />
                                                                                                                            <FooterStyle Width="35%" />
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblBillNo" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <EditItemTemplate>
                                                                                                                                <asp:TextBox ID="txtBillNo" runat="server" Text='<%# Bind("BillNo") %>' CssClass=""
                                                                                                                                    Width="95%"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="rvBillNo" runat="server" ControlToValidate="txtBillNo"
                                                                                                                                    ValidationGroup="bills" Display="Dynamic" EnableClientScript="False" ErrorMessage="Please enter BillNo. It cannot be left blank. ">*</asp:RequiredFieldValidator>
                                                                                                                            </EditItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:TextBox ID="txtAddBillNo" runat="server" CssClass="" Width="95%"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="rvAddBillNo" runat="server" ControlToValidate="txtAddBillNo"
                                                                                                                                    ValidationGroup="bills" Display="Dynamic" EnableClientScript="true" ErrorMessage="Please enter BillNo. It cannot be left blank. ">*</asp:RequiredFieldValidator>
                                                                                                                            </FooterTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-BorderColor="Gray">
                                                                                                                            <ItemStyle Width="35%" />
                                                                                                                            <FooterStyle Width="35%" />
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label ID="lblBillAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                            <EditItemTemplate>
                                                                                                                                <asp:TextBox ID="txtBillAmount" runat="server" Text='<%# Bind("Amount") %>' CssClass=""
                                                                                                                                    Width="93%"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="rvDescr" runat="server" ControlToValidate="txtBillAmount"
                                                                                                                                    ValidationGroup="bills" Display="Dynamic" EnableClientScript="False" ErrorMessage="Please enter Amount. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                                                                                <asp:CompareValidator ID="cvBillAmount" runat="server" ControlToValidate="txtBillAmount"
                                                                                                                                    Display="Dynamic" ErrorMessage="Amount must be greater than Zero" Operator="GreaterThan"
                                                                                                                                    Text="*" ValidationGroup="bills" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                            </EditItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:TextBox ID="txtAddBillAmount" runat="server" CssClass="" Width="93%"></asp:TextBox>
                                                                                                                                <asp:RequiredFieldValidator ID="rvAddBillAmt" runat="server" ControlToValidate="txtAddBillAmount"
                                                                                                                                    ValidationGroup="bills" Display="Dynamic" EnableClientScript="true" ErrorMessage="Please enter Amount. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                                                                                <asp:CompareValidator ID="cvAddBillAmount" runat="server" ControlToValidate="txtAddBillAmount"
                                                                                                                                    Display="Dynamic" ErrorMessage="Amount must be greater than Zero" Operator="GreaterThan"
                                                                                                                                    Text="*" ValidationGroup="bills" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                            </FooterTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                                                                                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                                                                                            <FooterStyle Width="20%" HorizontalAlign="Center" />
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CausesValidation="False"
                                                                                                                                    CommandName="Edit" />
                                                                                                                            </ItemTemplate>
                                                                                                                            <EditItemTemplate>
                                                                                                                                <asp:ImageButton ID="lbUpdate" runat="server" CausesValidation="true" ValidationGroup="bills"
                                                                                                                                    CommandName="Update" Text="Update" SkinID="GridUpdate"></asp:ImageButton>
                                                                                                                                <asp:ImageButton ID="lbCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                                                    Text="Cancel" SkinID="GridCancel"></asp:ImageButton>
                                                                                                                            </EditItemTemplate>
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:ImageButton ID="lbInsert" runat="server" CommandName="Insert" Text="Save" ValidationGroup="bills"
                                                                                                                                    SkinID="GridUpdate"></asp:ImageButton>
                                                                                                                                <asp:ImageButton ID="lblInsCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                                                                    Text="Cancel" SkinID="GridCancel"></asp:ImageButton>
                                                                                                                            </FooterTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                                                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                            <FooterStyle Width="10%" HorizontalAlign="Center" />
                                                                                                                            <ItemTemplate>
                                                                                                                                <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Bill?"
                                                                                                                                    runat="server">
                                                                                                                                </cc1:ConfirmButtonExtender>
                                                                                                                                <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete" CausesValidation="false"></asp:ImageButton>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                    </Columns>
                                                                                                                    <PagerTemplate>
                                                                                                                        <table style="border-color: white">
                                                                                                                            <tr style="border-color: white">
                                                                                                                                <td style="border-color: white">Goto Page
                                                                                                                                </td>
                                                                                                                                <td style="border-color: white">
                                                                                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox2" Style="border: 1px solid blue">
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
                                                                                                                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="True" ValidationGroup="bills"
                                                                                                                    ShowSummary="False" HeaderText=" " Font-Names="'Trebuchet MS'"
                                                                                                                    Font-Size="12pt" runat="server" />
                                                                                                            </ContentTemplate>
                                                                                                            <Triggers>
                                                                                                                <asp:AsyncPostBackTrigger ControlID="lnkAddBills" EventName="Click" />
                                                                                                            </Triggers>
                                                                                                        </asp:UpdatePanel>
                                                                                                    </div>
                                                                                                </asp:Panel>


                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                                <table style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%"></td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                                <ContentTemplate>
                                                                                                                    <asp:Button ID="lnkAddBills" Text="" CausesValidation="False" runat="server" CssClass="addbillbutton6"
                                                                                                                        EnableTheming="false" OnClick="lnkAddBills_Click" SkinID="skinBtnUpload" Width="80px"
                                                                                                                        OnClientClick="return Validate();" />
                                                                                                                </ContentTemplate>
                                                                                                                <Triggers>
                                                                                                                    <asp:AsyncPostBackTrigger ControlID="GrdBills" EventName="SelectedIndexChanged" />
                                                                                                                </Triggers>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>

                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                        </cc1:TabContainer>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 6px;">
                                                                    <td colspan="5">
                                                                        <asp:ValidationSummary ID="valSum" ShowMessageBox="True" ShowSummary="False" HeaderText=""
                                                                            Font-Names="'Trebuchet MS'" Font-Size="12pt" runat="server" />
                                                                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                                            CancelControlID="CancelPopUp" DynamicServicePath="" Enabled="True" PopupControlID="pnlPopup"
                                                                            TargetControlID="ShowPopUp">
                                                                        </cc1:ModalPopupExtender>
                                                                        <asp:Panel ID="pnlPopup" BackColor="White" BorderStyle="Solid" BorderColor="#bce1fe"
                                                                            Width="700px" runat="server">
                                                                            <div style="text-align: center; margin-top: 10px" align="center">
                                                                                <asp:GridView ID="GrdViewSales" runat="server" AllowSorting="True" AutoGenerateColumns="False" CssClass="someClass" OnRowCreated="GrdViewSales_RowCreated" OnRowDataBound="GrdViewSales_RowDataBound"
                                                                                    Width="100%" DataKeyNames="Billno" AllowPaging="True" OnPageIndexChanging="GrdViewSales_PageIndexChanging" EmptyDataText="No Credit Sales found." PageSize="6">
                                                                                    <EmptyDataRowStyle CssClass="GrdContent" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="Billno" HeaderText="Bill No." HeaderStyle-BorderColor="Gray" />
                                                                                        <asp:BoundField DataField="BillDate" HeaderText="Bill Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-BorderColor="Gray" />
                                                                                        <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" HeaderStyle-BorderColor="Gray" />
                                                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}" HeaderStyle-BorderColor="Gray" />
                                                                                    </Columns>
                                                                                    <PagerTemplate>
                                                                                        <table style="border-color: white">
                                                                                            <tr style="border-color: white">
                                                                                                <td style="border-color: white">Goto Page
                                                                                                </td>
                                                                                                <td style="border-color: white">
                                                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" SkinID="skinPagerDdlBox2" Style="border: 1px solid blue">
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td style="border-color: white; width: 5px"></td>
                                                                                                <td style="border-color: white">
                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="First" CausesValidation="false" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                                                        ID="btnFirst" />
                                                                                                </td>
                                                                                                <td style="border-color: white">
                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Prev" CausesValidation="false" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                                                        ID="btnPrevious" />
                                                                                                </td>
                                                                                                <td style="border-color: white">
                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Next" CausesValidation="false" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                                                        ID="btnNext" />
                                                                                                </td>
                                                                                                <td style="border-color: white">
                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Last" CausesValidation="false" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                                                        ID="btnLast" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </PagerTemplate>
                                                                                </asp:GridView>
                                                                                <div align="center">
                                                                                    <input id="CancelPopUp" type="button" runat="server" class="CloseWindow6" />
                                                                                </div>
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 30px; text-align: center">
                                                                    <td colspan="4">
                                                                        <table width="100%" cellspacing="0" style="table-layout: fixed">
                                                                            <tr>
                                                                                <td style="width: 23%"></td>
                                                                                <td style="width: 18%">
                                                                                    <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                        OnClick="UpdateCancelButton_Click" SkinID="skinBtnCancel" CssClass="cancelbutton6"
                                                                                        EnableTheming="false"></asp:Button>
                                                                                </td>
                                                                                <td style="width: 18%">
                                                                                    <asp:Button ID="UpdateButton" runat="server" SkinID="skinBtnSave" OnClientClick="javascript:CheckDate();CheckPendingBill();EditMobile_Validator();ConfirmSMS();"
                                                                                        OnClick="UpdateButton_Click" CssClass="Updatebutton1231" EnableTheming="false"></asp:Button>
                                                                                    <asp:Button ID="SaveButton" runat="server" SkinID="skinBtnSave" OnClientClick="javascript:CheckDate();CheckPendingBill();EditMobile_Validator();ConfirmSMS();"
                                                                                        OnClick="SaveButton_Click" CssClass="savebutton1231" EnableTheming="false"></asp:Button>
                                                                                </td>
                                                                                <td style="width: 18%">
                                                                                    <input id="ShowPopUp" type="button" class="pendingbillbutton6" style="width: 110px; vertical-align: middle"
                                                                                        runat="server" value="" />
                                                                                    <asp:Button ID="BtnSales" Text="" CausesValidation="False" Visible="False" OnClick="ShowPendingSales_Click"
                                                                                        runat="server" CssClass="Button" Width="120px" />
                                                                                </td>
                                                                                <td style="width: 23%"></td>
                                                                            </tr>
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
                                                                                    <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListSundryDebitors"
                                                                                        TypeName="BusinessLogic">
                                                                                        <SelectParameters>
                                                                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                        </SelectParameters>
                                                                                    </asp:ObjectDataSource>
                                                                                    <asp:ObjectDataSource ID="srcCreditorDebitorIsActive" runat="server" SelectMethod="ListSundryDebitorsIsActive"
                                                                                        TypeName="BusinessLogic">
                                                                                        <SelectParameters>
                                                                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                        </SelectParameters>
                                                                                    </asp:ObjectDataSource>
                                                                                </td>
                                                                                <td></td>
                                                                                <td></td>
                                                                                <td></td>
                                                                                <td></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                    <div>
                                        <table>
                                            <tr>
                                                <td colspan="5">
                                                    <input id="Button2" type="button" style="display: none" runat="server" />
                                                    <input id="Button3" type="button" style="display: none" runat="server" />
                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
                                                        CancelControlID="Button3" DynamicServicePath="" Enabled="True" PopupControlID="Panel2"
                                                        TargetControlID="Button2">
                                                    </cc1:ModalPopupExtender>

                                                    <asp:Panel runat="server" ID="Panel2" Style="width: 85%; display: none">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" RenderMode="Block">
                                                            <ContentTemplate>
                                                                <div id="conPopUpAdd">
                                                                    <asp:Panel ID="Panel3" runat="server">
                                                                        <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3; background-color: #fff; color: #000;"
                                                                            width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <div>
                                                                                        <table cellpadding="2" cellspacing="0" style="padding-left: 2px;"
                                                                                            width="100%">
                                                                                            <tr>
                                                                                                <td colspan="5">
                                                                                                    <table class="headerPopUp">
                                                                                                        <tr>
                                                                                                            <td>Receipts from Customer
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="5">
                                                                                                    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                                                        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Details of Receipt">
                                                                                                            <HeaderTemplate>
                                                                                                                <div>
                                                                                                                    <table>
                                                                                                                        <tr>
                                                                                                                            <td><b>Details of Receipt </b></td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </HeaderTemplate>
                                                                                                            <ContentTemplate>
                                                                                                                <div style="text-align: left; width:100%;">
                                                                                                                    <table style="width: 1125px; border: 0px solid #86b2d1" align="center" cellpadding="0" cellspacing="1">
                                                                                                                       <%-- <tr>
                                                                                                                            <td class="ControlLabelNew" style="width: 19%">
                                                                                                                                
                                                                                                                            </td>
                                                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                
                                                                                                                                </td>
                                                                                                                        </tr>--%>
                                                                                                                        <tr>
                                                                                                                            <td class="ControlLabelNew" style="width: 19%">Receipt Type
                                                                                                                            </td>
                                                                                                                            <td class="ControlBorderRadio" style="width: 22%">
                                                                                                                                <%--<asp:DropDownList ID="drpReceiptType" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7" AutoPostBack="true"
                                                                                                                    style="border: 1px solid #e7e7e7" height="26px"  OnSelectedIndexChanged="drpReceiptType_SelectedIndexChanged"
                                                                                                                    AppendDataBoundItems="True">
                                                                                                                    <asp:ListItem Text="Against" Value="1" Selected="True"></asp:ListItem>
                                                                                                                    <asp:ListItem Text="Advance" Value="2"></asp:ListItem>
                                                                                                                    
                                                                                                                </asp:DropDownList>--%>
                                                                                                                                <asp:RadioButtonList ID="drpReceiptType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                                                                                                    Width="100%" OnSelectedIndexChanged="drpReceiptType_SelectedIndexChanged" BackColor="#c0c0c0" Font-Bold="true">
                                                                                                                                    <asp:ListItem Text="Against Bill" Value="1" Selected="True"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="Advance" Value="2"></asp:ListItem>
                                                                                                                                </asp:RadioButtonList>
                                                                                                                            </td>
                                                                                                                            <td style="width: 13%;" align="left"></td>
                                                                                                                              <td class="ControlLabelNew" style="width: 17%">Received Date *
                                                                                                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" Display="Dynamic"
                                                                                                                                    ErrorMessage="Please select Received Date. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                                                                                <asp:CompareValidator ControlToValidate="txtDate" Operator="DataTypeCheck" Type="Date" Display="Dynamic"
                                                                                                                                    ErrorMessage="Please enter a valid date" runat="server" ID="CompareValidator2">*</asp:CompareValidator>--%>
                                                                                                                            </td>
                                                                                                                            <td class="ControlTextBox3" style="width: 22%">
                                                                                                                                <asp:TextBox ID="txtDate" runat="server" Font-Bold="true" ForeColor="Black" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"
                                                                                                                                    CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                                                
                                                                                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                                PopupButtonID="ImageButton1" PopupPosition="BottomLeft" TargetControlID="txtDate">
                                                                                                                                </cc1:CalendarExtender>
                                                                                                                            </td>
                                                                                                                            <td style="width: 15%;" align="left">
                                                                                                                                <asp:ImageButton ID="ImageButton1" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                                    Width="20px" runat="server" />
                                                                                                                            </td>
                                                                                                                            
                                                                                                                                
                                                                                                                            
                                                                                                                           
                                                                                                                            <td>
                                                                                                                                <asp:CheckBox ID="chkcash" runat="server" Text="Cash" OnCheckedChanged="chkcash_CheckedChanged" AutoPostBack="true"  Visible="false"/>
                                                                                                                                <asp:CheckBox ID="chkcheque" runat="server" Text="Cheque" OnCheckedChanged="chkcheque_CheckedChanged" AutoPostBack="true"  Visible="false" />
                                                                                                                                <asp:CheckBox ID="chkcard" runat="server" Text="Card" OnCheckedChanged="chkcard_CheckedChanged" AutoPostBack="true"  Visible="false" />
                                                                                                                            </td>

                                                                                                                        </tr>
                                                                                                                        <tr style="height: 2px">
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                             <td style="width: 19%;" class="ControlLabelNew">
                                                                                                                                        Branch *
                                                                                           <%-- <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="drpBranchAdd"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Branch. It cannot be left blank."
                                                                                                Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>--%>
                                                                                                                                    </td>
                                                                                                                                    <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                       <asp:DropDownList ID="drpBranchAdd" TabIndex="10" AutoPostBack="true" DataTextField="BranchName" DataValueField="Branchcode" OnSelectedIndexChanged="drpBranchAdd_SelectedIndexChanged" Width="260px"  CssClass="chzn-select" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                 runat="server">
                                                                                             </asp:DropDownList>
                                                                                                                                    </td>
                                                                                                                             <td  style="width: 13%">
                                                                                                                               
                                                                                                                            </td>
                                                                                                                          

                                                                                                                            <td class="ControlLabelNew" style="width: 15%;">Address Line 1</td>
                                                                                                                            <td class="ControlTextBox3" style="width: 22%;">
                                                                                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                        <asp:TextBox ID="txtAddress" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Height="26px" MaxLength="200" SkinID="skinTxtBox" TabIndex="3" Width="50%"></asp:TextBox>
                                                                                                                                    </ContentTemplate>
                                                                                                                                    <Triggers>
                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="drpLedger" EventName="SelectedIndexChanged" />
                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="drpMobile" EventName="SelectedIndexChanged" />
                                                                                                                                    </Triggers>
                                                                                                                                </asp:UpdatePanel>
                                                                                                                            </td>
                                                                                                                            <td style="width: 7%;"></td>


                                                                                                                        </tr>
                                                                                                                        <tr style="height: 2px">
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td class="ControlLabelNew" style="width: 17%;">
                                                                                                                                Name of Customer *
                                                                                                                <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="drpLedger"
                                                                                                                    ErrorMessage="Mandatory" Operator="GreaterThan" Display="Dynamic"
                                                                                                                    ValueToCompare="0">*</asp:CompareValidator>--%>
                                                                                                                                
                                                                                                                            </td>
                                                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                 <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                        <asp:DropDownList ID="drpLedger" runat="server" OnSelectedIndexChanged="drpLedger_SelectedIndexChanged" AutoPostBack="True"  CssClass="chzn-select" BackColor="#e7e7e7"
                                                                                                                                            Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                                            AppendDataBoundItems="True" Width="260px">
                                                                                                                                            <asp:ListItem Text="Select Customer" Value="0"></asp:ListItem>
                                                                                                                                        </asp:DropDownList>
                                                                                                                                        <asp:TextBox ID="txtCustomerName" runat="server" SkinID="skinTxtBoxGrid" Visible="false"></asp:TextBox>
                                                                                                                                    </ContentTemplate>
                                                                                                                                    <Triggers>
                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="chk" EventName="CheckedChanged" />
                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="drpMobile" EventName="SelectedIndexChanged" />
                                                                                                                                    </Triggers>
                                                                                                                                </asp:UpdatePanel>

                                                                                                                                

                                                                                                                                
                                                                                                                            </td>

                                                                                                                            <td style="width: 13%;" align="left">
                                                                                                                                <asp:CheckBox runat="server" ID="chk" Text="Existing Customer"  Checked="true" OnCheckedChanged="chk_CheckedChanged"  Font-Bold="true" AutoPostBack="true" />
                                                                                                                            </td>

                                                                                                                            <td class="ControlLabelNew" style="width: 15%;">Address Line 2</td>
                                                                                                                            <td class="ControlTextBox3" style="width: 22%">
                                                                                                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                        <asp:TextBox ID="txtAddress2" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" Height="26px" MaxLength="200" SkinID="skinTxtBox" TabIndex="5" Width="500px"></asp:TextBox>
                                                                                                                                    </ContentTemplate>
                                                                                                                                    <Triggers>
                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="drpLedger" EventName="SelectedIndexChanged" />
                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="drpMobile" EventName="SelectedIndexChanged" />
                                                                                                                                    </Triggers>
                                                                                                                                </asp:UpdatePanel>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr style="height: 2px">
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td class="ControlLabelNew" style="width: 17%">
                                                                                                                                Mobile
                                                                                                                            </td>
                                                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                        <asp:DropDownList Visible="false" ID="drpMobile" runat="server" AppendDataBoundItems="true" DataSourceID="srcCreditorDebitorIsActive" AutoPostBack="true" BackColor="#e7e7e7" CssClass="chzn-select" DataTextField="Mobile" DataValueField="LedgerID" Height="26px" OnSelectedIndexChanged="drpMobile_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" TabIndex="2" Width="260px">
                                                                                                                                            <asp:ListItem style="background-color: #e7e7e7" Text="Select Mobile" Value="0"></asp:ListItem>
                                                                                                                                        </asp:DropDownList>
                                                                                                                                        <asp:TextBox ID="txtCustomerId" runat="server" BackColor="#e7e7e7" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                                                    </ContentTemplate>
                                                                                                                                    <%--<Triggers>
                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="drpLedger" EventName="SelectedIndexChanged" />
                                                                                                                                    </Triggers>--%>
                                                                                                                                </asp:UpdatePanel>
                                                                                                                            </td>
                                                                                                                            <td></td>

                                                                                                                            <td style="width: 15%;" class="ControlLabelNew">Address Line 3
                                                                                                                            </td>
                                                                                                                            <td class="ControlTextBox3" style="width: 22%">
                                                                                                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                        <asp:TextBox ID="txtAddress3" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" Width="500px"></asp:TextBox>
                                                                                                                                    </ContentTemplate>
                                                                                                                                    <Triggers>
                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="drpLedger" EventName="SelectedIndexChanged" />
                                                                                                                                        <asp:AsyncPostBackTrigger ControlID="drpMobile" EventName="SelectedIndexChanged" />
                                                                                                                                    </Triggers>
                                                                                                                                </asp:UpdatePanel>
                                                                                                                            </td>
                                                                                                                            <td></td>
                                                                                                                        </tr>
                                                                                                                        <tr style="height: 2px">
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td class="ControlLabelNew" style="width: 17%">
                                                                                                                                <%--<asp:CompareValidator ID="cvCustomer" runat="server" ControlToValidate="drpCustomerCategoryAdd" Display="Dynamic" ErrorMessage="Please Select Customer Category. It cannot be left blank." Operator="GreaterThan" Text="*" ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>--%>
                                                                                                                                        Category of Customer *
                                                                                                                            </td>
                                                                                                                            <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                                <asp:DropDownList ID="drpCustomerCategoryAdd" runat="server" AppendDataBoundItems="true" BackColor="#e7e7e7" CssClass="chzn-select" DataTextField="CusCategory_Name" DataValueField="CusCategory_Value" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="2" ValidationGroup="salesval" Width="260px">
                                                                                                                                                    <asp:ListItem style="background-color: #e7e7e7" Text="Select Customer Category" Value="0"></asp:ListItem>
                                                                                                                                                </asp:DropDownList>
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpLedger" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpMobile" EventName="SelectedIndexChanged" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                            </td>
                                                                                                                             <td></td>
                                                                                                                            <td class="ControlLabelNew" style="width: 19%;">
                                                                                                                                            Outstanding to be Adjusted
                                                                                                                                        </td>
                                                                                                                                        <td class="ControlLabelNew1" align="left" >
                                                                                                                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                            <asp:label runat="server" ID="outstand" Text="">

                                                                                                                                            </asp:label>
                                                                                                                                                </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="drpLedger" EventName="SelectedIndexChanged" />
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                        </td>
                                                                                                                            
                                                                                                                        </tr>
                                                                                                                        <tr style="height: 6px">
                                                                                                                            </tr>
                                                                                                                        <tr>
                                                                                                                            <td colspan="6">
                                                                                                                                <table style="width: 100%">
                                                                                                                                    <tr>
                                                                                                                                        <td style="background-color:#cccccc;  border: 1px solid gray">
                                                                                                                                            
                                                                                                                                             <asp:Label ID="txthead1" runat="server" Text="Break-up of Receipts" Font-Bold="true" ForeColor="Black"  Font-Size="Medium" >

                                                                                                                                             </asp:Label>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td colspan="6">
                                                                                                                                <table style="width: 100%; border: 1px solid gray">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <div id="div" runat="server" style="height: 150px; overflow: scroll">
                                                                                                                                                <%--<rwg:BulkEditGridView ID="GrdViewItems" AutoGenerateColumns="False"
                                                                                                                                                    OnRowDataBound="GrdViewItems_RowDataBound" GridLines="None" runat="server"
                                                                                                                                                    Width="100%">
                                                                                                                                                   
                                                                                                                                                    <Columns>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="#006699" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:Label ID="txtType" runat="server" Text='<%# Eval("Type")%>' Font-Bold="true">

                                                                                                                                                                </asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="RefNo" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:TextBox ID="txtRefNo" runat="server" Width="90%" Height="24px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Text='<%# Bind("RefNo") %>'></asp:TextBox>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20px">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:TextBox ID="txtAmount" runat="server" Width="90%" Height="24px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                                                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server" TargetControlID="txtAmount"
                                                                                                                                                                    ValidChars="." FilterType="Numbers, Custom" />
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Bank" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="50px">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:DropDownList ID="drpBank" runat="server" BackColor="White" CssClass="drpDownListMedium" Width="100%" AutoPostBack="False"
                                                                                                                                                                    DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #cccccc" Height="25px" ForeColor="#006699"
                                                                                                                                                                    AppendDataBoundItems="true" ValidationGroup="editVal">
                                                                                                                                                                    <asp:ListItem Text="Select Creditor" style="background-color: White" Value="0"></asp:ListItem>
                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="ChequeNo" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20px">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:TextBox ID="txtChequeNo" runat="server" Width="90%" Height="24px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Text='<%# Bind("ChequeNo") %>'></asp:TextBox>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Narration" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20px">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:TextBox ID="txtNarration" runat="server" Width="90%" Height="24px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Text='<%# Bind("Narration") %>'></asp:TextBox>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                    </Columns>
                                                                                                                                                </rwg:BulkEditGridView>--%>

                                                                                                                                                <asp:GridView ID="GridView2" AutoGenerateColumns="False" ShowFooter="True"
                                                                                                                                                    OnRowDataBound="GridView2_RowDataBound" OnRowDeleting="GridView2_RowDeleting" GridLines="None" SaveButtonID="SaveButton" runat="server"
                                                                                                                                                    Width="100%">
                                                                                                                                                    <%--<RowStyle CssClass="dataRow" />
                                                                                                                                                    <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                                                                    <AlternatingRowStyle CssClass="altRow" />
                                                                                                                                                    <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                                                                    <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                                                                    <FooterStyle CssClass="dataRow" />--%>
                                                                                                                                                    <Columns>
                                                                                                                                                        <asp:BoundField DataField="RowNumber" HeaderText="#" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center" ItemStyle-ForeColor="#0567AE"  HeaderStyle-Width="6%"/>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Mode of Receipt" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="15%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:DropDownList ID="txtType" runat="server" CssClass="drpDownListMedium" Width="98%" BackColor="White" DataValueField="PaymodeID" DataTextField="Paymode"
                                                                                                                                                                    style="border:1px solid #cccccc" ForeColor="#006699" Height="22px" OnSelectedIndexChanged="txtType_SelectedIndexChanged" AutoPostBack="True"
                                                                                                                                                                    AppendDataBoundItems="true"  Font-Bold="true">
                                                                                                                                                                    <asp:ListItem Text="Select Type" Value="0"></asp:ListItem>
                                                                                                                                                                    <asp:ListItem Text="Cash"  Value="1"></asp:ListItem>
                                                                                                                                                                    <asp:ListItem Text="Cheque/Card"  Value="2"></asp:ListItem>
                                                                                                                                                                    <%--<asp:ListItem Text="Card" Value="3"></asp:ListItem>--%>
                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Bill Number" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="24%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:TextBox ID="txtRefNo" runat="server" Width="98%" Height="20px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699"  Font-Bold="true"
                                                                                                                                                                    ></asp:TextBox>

                                                                                                                                                                

                                                                                                                                                            </ItemTemplate>
                                                                                                                                                            <FooterStyle HorizontalAlign="Center" />
                                                                                                                                                                                                        <FooterTemplate>
                                                                                                                                                            <asp:Label ID="txtTotalAmountReceived" runat="server" Text="Total Amount Received" Font-Bold="true">

                                                                                                                                                                </asp:Label>
                                                                                                                                                            </FooterTemplate>

                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        
                                                                                                                                                        
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="11%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:TextBox ID="txtAmount" Font-Bold="true" runat="server" Width="96%" Height="20px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged"
                                                                                                                                                                    ></asp:TextBox>
                                                                                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender312" runat="server" TargetControlID="txtAmount"
                                                                                                                                    ValidChars="." FilterType="Numbers, Custom" />
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                            <FooterStyle HorizontalAlign="Right" />
                                                                                                                                                                                                        <FooterTemplate>
                                                                                                                                                            <asp:TextBox ID="txttot" runat="server" BackColor="#e7e7e7" Text="0" Width="96%" Height="20px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" ReadOnly="true"  Font-Bold="true" ></asp:TextBox>
                                                                                                                                                                                                            </FooterTemplate>

                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Name of Bank" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="13%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:DropDownList ID="drpBank" runat="server" BackColor="White" CssClass="drpDownListMedium" Width="98%" AutoPostBack="False"  Font-Bold="true"
                                                                                                                                                                    DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #cccccc" ForeColor="#006699"
                                                                                                                                                                    AppendDataBoundItems="true" ValidationGroup="editVal">
                                                                                                                                                                    <%--<asp:ListItem Text="Select Creditor" style="background-color: White; color:#006699" Value="0"></asp:ListItem>--%>
                                                                                                                                                                </asp:DropDownList>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                           
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                         <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Cheque/Card Number" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="12%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:TextBox ID="txtChequeNo" runat="server" Width="96%" Height="20px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" ForeColor="#006699"></asp:TextBox>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Remarks" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="19%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:TextBox ID="txtNarration" runat="server" Width="96%" Height="20px" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" Font-Bold="true" ForeColor="#006699"></asp:TextBox>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                             <FooterStyle HorizontalAlign="Right" />
                                                                                                                                                                                                        <FooterTemplate>
                                                                                                                                                                                                            <asp:Button ID="ButtonAdd1" runat="server" AutoPostback="false" EnableTheming="false"
                                                                                                                                                                                                                Text="Add New" OnClick="ButtonAdd1_Click"  Width="40%"/>
                                                                                                                                                                                                        </FooterTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="2%">
                                                                                                                                                            
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                    </Columns>
                                                                                                                                                </asp:GridView>
                                                                                                                                            </div>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        
                                                                                                                        <%--<tr>
                                                                                                                            <td colspan="6">
                                                                                                                                <table style="width: 100%">
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 12%">
                                                                                                                                            
                                                                                                                                        </td>
                                                                                                                                        <td class="ControlLabelNew" style="width: 20%">
                                                                                                                                            
                                                                                                                                
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 15%">
                                                                                                                                            
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 53%">
                                                                                                                                            
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>--%>
                                                                                                                        
                                                                                                                                    
                                                                                                                        <tr >
                                                                                                                            <td colspan="6">
                                                                                                                                <table style="width: 100%">
                                                                                                                                    <tr>
                                                                                                                                        <td style="background-color:#cccccc;  border: 1px solid gray">
                                                                                                                                             <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                            <asp:Panel  id="totalrow1" runat="server" visible="false"
                                                                                                    Width="100%" >

                                                                                                                                             <asp:Label ID="Label2" runat="server" Text="Pending Bills to be Adjusted" Font-Bold="true" ForeColor="Black" Font-Size="Medium" >

                                                                                                                                             </asp:Label>
                                                                                                                                                </asp:Panel>
                                                                                                                                        </ContentTemplate>
                                                                                                                                                 </asp:UpdatePanel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                              
                                                                                                                        <tr >
                                                                                                                            <td colspan="6">
                                                                                                                                <table style="width: 100%;  border: 1px solid gray" >
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                            <asp:Panel id="totalrow123"
                                                                                                    Width="100%" runat="server" visible="false">
                                                                                                                                            <div id="div1" runat="server" style="height: 100px; overflow: scroll">
                                                                                                                                                <asp:UpdatePanel ID="UpdatePanel112" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                                                                                                                    Width="100%" DataKeyNames="Billno" EmptyDataText="No Credit Sales found." GridLines="None">
                                                                                                                                                    <EmptyDataRowStyle CssClass="GrdContent" />
                                                                                                                                                    <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" Font-Size="Small" />
                                                                                                                                                    <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="25px" Font-Size="Small" ForeColor="#0567AE" />
                                                                                                                                                    <Columns>
                                                                                                                                                        <%--<asp:BoundField DataField="Billno" HeaderText="Bill No." HeaderStyle-BorderColor="Gray" />--%>
                                                                                                                                                        <asp:BoundField DataField="Row" HeaderText="#" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%" />
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Bill Number" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <%--<asp:TextBox ID="txtBillno" runat="server" Width="90%"  Text='<%# Eval("Billno")%>' Height="26px"
                                                                                                                                                                    ></asp:TextBox>--%>
                                                                                                                                                                <asp:Label ID="txtBillno" runat="server" Text='<%# Eval("Billno")%>' Font-Bold="true">

                                                                                                                                                                </asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Name of Customer" HeaderStyle-BorderColor="Gray" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <%--<asp:TextBox ID="txtCustomerName" runat="server" Width="90%"  Text='<%# Eval("CustomerName")%>' Height="26px"
                                                                                                                                                                    ></asp:TextBox>--%>
                                                                                                                                                                <asp:Label ID="txtCustomerName" runat="server" Text='<%# Eval("CustomerName")%>' Font-Bold="true">

                                                                                                                                                                </asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <%--<asp:BoundField DataField="BillDate" HeaderText="Bill Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-BorderColor="Gray" />--%>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Bill Date" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <%--<asp:TextBox ID="txtBillDate" runat="server" Width="90%"  Text='<%# Eval("BillDate")%>' Height="26px"
                                                                                                                                                                    ></asp:TextBox>--%>
                                                                                                                                                                <asp:Label ID="txtBillDate" runat="server" Text='<%# Eval("BillDate")%>' Font-Bold="true">

                                                                                                                                                                </asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <%--<asp:BoundField DataField="CustomerName" HeaderText="CustomerName" HeaderStyle-BorderColor="Gray" />--%>
                                                                                                                                                        
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Total Bill Amount" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <%--<asp:TextBox ID="txtAmount" runat="server" Width="90%"  Text='<%# Eval("Amount")%>' Height="26px"
                                                                                                                                                                    ></asp:TextBox>--%>
                                                                                                                                                                <asp:Label ID="txtAmount123" runat="server" Text='<%# Eval("Amount")%>' Font-Bold="true">

                                                                                                                                                                </asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                        <%--<asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}" HeaderStyle-BorderColor="Gray" />--%>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Pending Bill Amount" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="13%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <%--<asp:TextBox ID="txtAmount" runat="server" Width="90%"  Text='<%# Eval("Amount")%>' Height="26px"
                                                                                                                                                                    ></asp:TextBox>--%>
                                                                                                                                                                <asp:Label ID="txtAmount" runat="server" Text='<%# Eval("PendingAmount")%>' Font-Bold="true">

                                                                                                                                                                </asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount to be Adjusted" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="17%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:TextBox ID="txtAdjustAmount" runat="server" AutoPostBack="true" OnTextChanged="txtAdjustAmount_TextChanged" Width="92%"  Height="20px" Text='<%# Eval("AdjustAmount")%>'  BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"
                                                                                                                                                                    ></asp:TextBox>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Completed" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:Label ID="txtCompleted" runat="server" Width="90%"  Height="23px" Text='<%# Eval("Completed")%>'  BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"
                                                                                                                                                                    ></asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount1" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:Label ID="txtAmount1" runat="server" Width="90%"  Height="23px" Text='<%# Eval("Amount1")%>'  BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"
                                                                                                                                                                    ></asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
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
                                                                                                                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" SkinID="skinPagerDdlBox2" Style="border: 1px solid blue">
                                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                                </td>
                                                                                                                                                                <td style="border-color: white; width: 5px"></td>
                                                                                                                                                                <td style="border-color: white">
                                                                                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="First" CausesValidation="false" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                        ID="btnFirst" />
                                                                                                                                                                </td>
                                                                                                                                                                <td style="border-color: white">
                                                                                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Prev" CausesValidation="false" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                        ID="btnPrevious" />
                                                                                                                                                                </td>
                                                                                                                                                                <td style="border-color: white">
                                                                                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Next" CausesValidation="false" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                        ID="btnNext" />
                                                                                                                                                                </td>
                                                                                                                                                                <td style="border-color: white">
                                                                                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Last" CausesValidation="false" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                        ID="btnLast" />
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </PagerTemplate>
                                                                                                                                                </asp:GridView>
                                                                                                                                        </ContentTemplate>
                                                                                                                                    
                                                                                                                                </asp:UpdatePanel>
                                                                                                                                            </div>
                                                                                                                                                </asp:Panel>
                                                                                                                                        </ContentTemplate>
                                                                                                                                                </asp:UpdatePanel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr >
                                                                                                                            <td colspan="6">
                                                                                                                                 
                                                                                                                                <table style="width: 100%">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                        <asp:Panel  id="totalrow" runat="server" visible="false"
                                                                                                    Width="100%" >
                                                                                                                                 
                                                                                                                                <table style="width: 100%">
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 8%">
                                                                                                                                            
                                                                                                                                        </td>
                                                                                                                                        
                                                                                                                                        <td style="width: 47%">
                                                                                                                                            
                                                                                                                                        </td>
                                                                                                                                        <td class="ControlLabelNew" style="width: 20%">
                                                                                                                                            Total Amount Adjusted
                                                                                                                                
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 16%;">
                                                                                                                                            <asp:TextBox ID="TextBox1" runat="server"  BackColor="#e7e7e7" Height="20px" Width="100%" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" ReadOnly="true"  Font-Bold="true" ></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 8%">
                                                                                                                                            
                                                                                                                                        </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                            </asp:Panel>
                                                                                                                                        </ContentTemplate>
                                                                                                                                            </asp:UpdatePanel>
                                                                                                                                            </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                                     
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                            <tr  id="Tr1" runat="server" visible="false">
                                                                                                                            <td colspan="6">
                                                                                                                                <table style="width: 100%;  border: 1px solid gray" >
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            <asp:Panel ID="Panel5" 
                                                                                                    Width="100%" runat="server">
                                                                                                                                            <div id="div2" runat="server" style="height: 100px; overflow: scroll">
                                                                                                                                                <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                                                                                                    <ContentTemplate>
                                                                                                                                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False"
                                                                                                                                                    Width="100%" DataKeyNames="Billno" EmptyDataText="No Datas found." GridLines="None">
                                                                                                                                                    <EmptyDataRowStyle CssClass="GrdContent" />
                                                                                                                                                    <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" Font-Size="Small" />
                                                                                                                                                    <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="25px" Font-Size="Small" ForeColor="#0567AE" />
                                                                                                                                                    <Columns>
                                                                                                                                                        <%--<asp:BoundField DataField="Billno" HeaderText="Bill No." HeaderStyle-BorderColor="Gray" />--%>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Bill Number" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <%--<asp:TextBox ID="txtBillno" runat="server" Width="90%"  Text='<%# Eval("Billno")%>' Height="26px"
                                                                                                                                                                    ></asp:TextBox>--%>
                                                                                                                                                                <asp:Label ID="txtBillno" runat="server" Text='<%# Eval("Billno")%>' Font-Bold="true">

                                                                                                                                                                </asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <%--<asp:TextBox ID="txtAmount" runat="server" Width="90%"  Text='<%# Eval("Amount")%>' Height="26px"
                                                                                                                                                                    ></asp:TextBox>--%>
                                                                                                                                                                <asp:Label ID="txtAmount123" runat="server" Text='<%# Eval("Amount")%>' Font-Bold="true">

                                                                                                                                                                </asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>

                                                                                                                                                        <%--<asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}" HeaderStyle-BorderColor="Gray" />--%>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="ChequeNo" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="13%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <%--<asp:TextBox ID="txtAmount" runat="server" Width="90%"  Text='<%# Eval("Amount")%>' Height="26px"
                                                                                                                                                                    ></asp:TextBox>--%>
                                                                                                                                                                <asp:Label ID="txtAmount" runat="server" Text='<%# Eval("ChequeNo")%>' Font-Bold="true">

                                                                                                                                                                </asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Type" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="17%">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:TextBox ID="txtAdjustAmount" runat="server" Width="92%"  Height="20px" Text='<%# Eval("Type")%>'  BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"
                                                                                                                                                                    ></asp:TextBox>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Completed" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:Label ID="txtCompleted" runat="server" Width="90%"  Height="23px" Text='<%# Eval("Completed")%>'  BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"
                                                                                                                                                                    ></asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount1" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center">
                                                                                                                                                            <ItemTemplate>
                                                                                                                                                                <asp:Label ID="txtAmount1" runat="server" Width="90%"  Height="23px" Text='<%# Eval("Amount1")%>'  BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"
                                                                                                                                                                    ></asp:Label>
                                                                                                                                                            </ItemTemplate>
                                                                                                                                                        </asp:TemplateField>
                                                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
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
                                                                                                                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" SkinID="skinPagerDdlBox2" Style="border: 1px solid blue">
                                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                                </td>
                                                                                                                                                                <td style="border-color: white; width: 5px"></td>
                                                                                                                                                                <td style="border-color: white">
                                                                                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="First" CausesValidation="false" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                        ID="btnFirst" />
                                                                                                                                                                </td>
                                                                                                                                                                <td style="border-color: white">
                                                                                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Prev" CausesValidation="false" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                        ID="btnPrevious" />
                                                                                                                                                                </td>
                                                                                                                                                                <td style="border-color: white">
                                                                                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Next" CausesValidation="false" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                        ID="btnNext" />
                                                                                                                                                                </td>
                                                                                                                                                                <td style="border-color: white">
                                                                                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="Last" CausesValidation="false" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                        ID="btnLast" />
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </PagerTemplate>
                                                                                                                                                </asp:GridView>
                                                                                                                                        </ContentTemplate>
                                                                                                                                    
                                                                                                                                </asp:UpdatePanel>
                                                                                                                                            </div>
                                                                                                                                                </asp:Panel>
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                            
                                                                                                                        <tr>
                                                                                                                            <td colspan="5">
                                                                                                                                <table style="width: 100%;">
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 40%;"></td>
                                                                                                                                        <td style="width: 15%;">
                                                                                                                                            <asp:Button ID="UpdButton" runat="server" CausesValidation="true"
                                                                                                                                                CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                                                                                OnClick="UpdButton_Click"></asp:Button>
                                                                                                                                            <asp:Button ID="Button4" runat="server" CausesValidation="true"
                                                                                                                                                CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                                                                                OnClick="Button4_Click"></asp:Button>
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 15%;">

                                                                                                                                            <asp:Button ID="UpdCancelButton" runat="server" CausesValidation="False"
                                                                                                                                                CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdCancelButton_Click"></asp:Button>
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 40%;"></td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </ContentTemplate>
                                                                                                        </cc1:TabPanel>
                                                                                                    </cc1:TabContainer>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                    <div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%; margin: -4px 0px 0px 0px;">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -4px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewReceipt" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewReceipt_RowCreated" Width="100.4%" DataSourceID="GridSource"
                                            AllowPaging="True" DataKeyNames="TransNo" EmptyDataText="No Receipts found!"
                                            OnRowCommand="GrdViewReceipt_RowCommand" OnRowDataBound="GrdViewReceipt_RowDataBound"
                                            OnSelectedIndexChanged="GrdViewReceipt_SelectedIndexChanged" OnRowDeleting="GrdViewReceipt_RowDeleting"
                                            OnRowDeleted="GrdViewReceipt_RowDeleted" CssClass="someClass">
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="TransNo" HeaderText="Trans. No." HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="RefNo" HeaderText="Bill No." HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="TransDate" HeaderText="Transaction Date" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="LedgerName" HeaderText="Received From" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Debi" HeaderText="Bank Name / Cash" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Narration" HeaderText="Narration" Visible="false" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Branchcode" HeaderText="Branch Code" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" CausesValidation="false" runat="server" SkinID="edit"
                                                            CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                       <%-- <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Receipt?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>--%>
                                                       <%-- <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>--%>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Print" HeaderStyle-BorderColor="Gray">
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
                                                            <asp:DropDownList ID="ddlPageSelector" Style="border: 1px solid Gray" Width="75px" Height="23px" BackColor="#e7e7e7" runat="server" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 5px; border-color: white"></td>
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
                    <td align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListReceiptsCustomers"
                            TypeName="BusinessLogic" DeleteMethod="DeleteCustReceipt" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="TransNo" Type="Int32" />
                                <asp:Parameter Name="requireValidation" Type="Boolean" DefaultValue="true" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
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
            <asp:HiddenField ID="hddate" runat="server" Value="0" />
            <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
            <asp:HiddenField ID="hd" runat="server" Value="0" />
            <asp:HiddenField ID="hd1" runat="server" Value="0" />
            <asp:HiddenField ID="hdPendingCount" runat="server" Value="0" />
            <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table align="center">
        <tr>
            <td style="width:50%">
                 <asp:Panel ID="pnlSearch" runat="server" Width="60px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CausesValidation="false" CssClass="ButtonAdd66" ForeColor="White" EnableTheming="false"
                                                    Width="60px"></asp:Button>
                                            </asp:Panel>
            </td>
            <td style="width:50%">
                <asp:Button ID="btnrec" runat="server"
                    CssClass="exportexl6" EnableTheming="false" CausesValidation="false"
                    OnClientClick="window.open('ReportExcelReceipts.aspx?ID=CustRec','Summary', 'toolbar=no,status=no,menu=no,location=no,height=280,width=650,left=405,top=220 ,resizable=yes, scrollbars=yes');"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
