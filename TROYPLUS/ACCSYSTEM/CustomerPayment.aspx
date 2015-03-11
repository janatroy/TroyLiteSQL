<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="CustomerPayment.aspx.cs" Inherits="CustomerPayment" Title="Sales > Customer Payment" %>

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


        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_frmViewAdd_tablInsert');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_frmViewAdd_tabEdit');

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

        function PrintItem(ID) {
            window.showModalDialog('./PrintPayment.aspx?ID=' + ID, self, 'dialogWidth:800px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
        }

        function OnKeyPress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                $find("ctl00_cplhControlPanel_ModalPopupExtender1").hide();
            }
        }

        $("#ctl00_cplhControlPanel_frmViewAdd_tablInsert_tabInsMain_InsertCancelButton").live("click", function () {
            $find("ctl00_cplhControlPanel_ModalPopupExtender1").hide();
        });

        $("#ctl00_cplhControlPanel_frmViewAdd_tabEdit_tabEditMain_UpdateCancelButton").live("click", function () {
            $find("ctl00_cplhControlPanel_ModalPopupExtender1").hide();
        });

        function AdvancedTest(id) {

            var panel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tabEdit_tabEditMain_tblBank');
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
            alert(id);
            var panel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tablInsert_tabInsMain_tblBankAdd');
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

        function Advanced() {
            var table = document.getElementById('tblBank');
            var adv = document.getElementById('ctl00_cplhControlPanel_hidAdvancedState');

            var tr = table.getElementsByTagName('tr');

            for (i = 0; i < tr.length; i++) {
                if (tr[i].className == "AdvancedSearch") {
                    tr[i].className = "hidden";
                    adv.value = tr[i].className;
                }
                else if (tr[i].className == "hidden") {
                    tr[i].className = "AdvancedSearch";
                    adv.value = tr[i].className;
                }
            }
        }
    </script>

    <style id="Style1" runat="server">
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
            }

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
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">

                        <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Customer Payments</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Customer Payments
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 1%"></td>
                                    <td style="width: 30%; font-size: 22px; color: White;">Customer Payments
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
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <div style="width: 150px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="156px" BackColor="White" Height="21px" Style="text-align: center; border: 1px solid White">
                                                <asp:ListItem Value="TransNo">Trans. No.</asp:ListItem>
                                                <asp:ListItem Value="RefNo">Ref. No.</asp:ListItem>
                                                <asp:ListItem Value="TransDate">Transaction Date</asp:ListItem>
                                                <asp:ListItem Value="LedgerName">Paid To</asp:ListItem>
                                                <asp:ListItem Value="Narration">Narration</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 21%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" />

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
                                                        <table style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="4" class="headerPopUp">Customer Payment
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="left">
                                                                    <cc1:TabContainer ID="tabEdit" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabEditMain" runat="server" HeaderText="Payment Details">
                                                                            <ContentTemplate>
                                                                                <table style="width: 800px; vertical-align: text-top; border: 0px solid #86b2d1;"
                                                                                    align="center" cellspacing="2" cellpadding="3">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 25%">Branch *
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
                                                                                        <td class="ControlLabel" style="width: 15%"></td>
                                                                                        <td style="width: 25%"></td>

                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 25%">Ref. No. *
                                                                                            <asp:RequiredFieldValidator ID="rvRefNo" runat="server" ControlToValidate="txtRefNo"
                                                                                                Text="*" ErrorMessage="Please enter Ref. No. It cannot be left blank." CssClass="rfv" Display="Dynamic"
                                                                                                EnableClientScript="True"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("RefNo") %>' SkinID="skinTxtBoxGrid">
                                                                                            </asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 15%">

                                                                                            <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtTransDate"
                                                                                                Text="*" ErrorMessage="Ref Date is mandatory" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ControlToValidate="txtTransDate" Operator="DataTypeCheck" Type="Date"
                                                                                                Text="*" ErrorMessage="Please select a valid date. It cannot be left blank." runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
                                                                                            <asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtTransDate"
                                                                                                ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                                            Date *
                                                                                        </td>
                                                                                        <td class="ControlNumberBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtTransDate" Enabled="false" runat="server" Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                                                                CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDate">
                                                                                            </cc1:CalendarExtender>
                                                                                            <td>
                                                                                                <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                    Width="20px" runat="server" />
                                                                                            </td>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 25%">Paid To *
                                                                                            <asp:CompareValidator ID="cvPayedTo" runat="server" ControlToValidate="ComboBox2"
                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Paid To. It cannot be left blank."
                                                                                                Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 25%">
                                                                                            <asp:DropDownList ID="ComboBox2" runat="server" CssClass="drpDownListMedium" Style="border: 1px solid #e7e7e7" Height="26px" BackColor="#e7e7e7" Width="100%" AutoPostBack="False"
                                                                                                
                                                                                                AppendDataBoundItems="true" OnDataBound="ComboBox2_DataBound">
                                                                                                <asp:ListItem style="background-color: #e7e7e7" Text="Select Customer" Value="0"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 15%">Amount *
                                                                                            <asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                                                                                ErrorMessage="Please enter Amount. It cannot be left blank." Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            <cc1:FilteredTextBoxExtender ID="fltAmt" runat="server" TargetControlID="txtAmount"
                                                                                                ValidChars="." FilterType="Numbers, Custom" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 15%">
                                                                                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="100%"
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 25%">Payment Made By *
                                                                                            <asp:RequiredFieldValidator ID="rvBDate" runat="server" ControlToValidate="chkPayTo"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Please Select Item Name. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                            <asp:RadioButtonList ID="chkPayTo" onclick="javascript:AdvancedTest(this.id);" runat="server"
                                                                                                AutoPostBack="false" Width="100%" OnDataBound="chkPayTo_DataBound" OnSelectedIndexChanged="chkPayTo_SelectedIndexChanged">
                                                                                                <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                                                                                                <asp:ListItem Text="Cheque"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 15%">Narration *
                                                                                            <asp:RequiredFieldValidator ID="rvNarration" runat="server" ControlToValidate="txtNarration"
                                                                                                ErrorMessage="Please enter Narration. It cannot be left blank." Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtNarration" runat="server" Height="30px" TextMode="MultiLine"
                                                                                                Text='<%# Bind("Narration") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4" width="100%">
                                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">

                                                                                                <ContentTemplate>
                                                                                                    <asp:Panel ID="PanelBank" runat="server">
                                                                                                        <table width="100%" id="tblBank" runat="server" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="ControlLabel" style="width: 25%">
                                                                                                                    <asp:CompareValidator ID="cvBank" runat="server" ControlToValidate="ddBanks" Display="Dynamic"
                                                                                                                        EnableClientScript="false" ErrorMessage="Phase is Mandatory" Operator="GreaterThan"
                                                                                                                        ValueToCompare="0">*</asp:CompareValidator>
                                                                                                                    Bank Name *
                                                                                                                </td>
                                                                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                                                                    <asp:DropDownList ID="ddBanks" runat="server" OnDataBound="ddBanks_DataBound" DataSourceID="srcBanks" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                        DataTextField="LedgerName" DataValueField="LedgerID" AppendDataBoundItems="True"
                                                                                                                        CssClass="drpDownListMedium" BackColor="#e7e7e7" Width="100%">
                                                                                                                        <asp:ListItem Selected="True" style="background-color: #e7e7e7" Value="0">Select Bank</asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td class="ControlLabel" style="width: 15.3%">
                                                                                                                    <asp:RequiredFieldValidator ID="rvChequeNo" runat="server" ControlToValidate="txtChequeNo"
                                                                                                                        ErrorMessage="Please enter Cheque No.It cannot be left blank." Display="Dynamic" EnableClientScript="false">*</asp:RequiredFieldValidator>
                                                                                                                    Cheque No. *
                                                                                                                </td>
                                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                                    <asp:TextBox ID="txtChequeNo" runat="server" Text='<%# Bind("ChequeNo") %>' SkinID="skinTxtBoxGridBank" MaxLength="10" Visible="false"></asp:TextBox>
                                                                                                                    <asp:DropDownList ID="cmbChequeNo" runat="server" AppendDataBoundItems="True" AutoPostBack="true" BackColor="#e7e7e7" DataTextField="ChequeNo" DataValueField="ChequeNo" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid #e7e7e7" Width="100%">
                                                                                                                        <asp:ListItem Selected="True" style="height: 1px; background-color: #e7e7e7" Value="0">Select Cheque No</asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <%--test<td>
                                                                                                        </td>--%>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                        <cc1:TabPanel ID="tabEditAddTab" runat="server" HeaderText="Additional Details">
                                                                            <ContentTemplate>
                                                                                <table align="center" cellpadding="3" cellspacing="1" style="border: 0px solid #5078B3; width: 800px;">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 35%">Against Bill No.
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtBill" runat="server" Text='<%# Bind("BillNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 15%"></td>
                                                                                        <td style="width: 10%"></td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                    </cc1:TabContainer>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ShowSummary="false" HeaderText=" " Font-Names="'Trebuchet MS'"
                                                                        Font-Size="12" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                                <td colspan="4"></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" width="100%">
                                                                    <table width="100%" id="Table1" runat="server" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td style="width: 30%;"></td>
                                                                            <td align="center" style="width: 18%;">
                                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="center" style="width: 18%;">
                                                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
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
                                                                <td>
                                                                    <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListSundryDebitors"
                                                                        TypeName="BusinessLogic">
                                                                        <SelectParameters>
                                                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                        </SelectParameters>
                                                                    </asp:ObjectDataSource>
                                                                </td>
                                                                <td></td>
                                                                <td></td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <InsertItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="2" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="4" class="headerPopUp">Customer Payment
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="left">
                                                                    <cc1:TabContainer ID="tablInsert" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabInsMain" runat="server" HeaderText="Payment Details">
                                                                            <ContentTemplate>
                                                                                <table style="width: 800px; vertical-align: text-top" align="center" cellpadding="0"
                                                                                    cellspacing="1">
                                                                                    <%--<tr>
                                                                                        <td style="width:25%" class="ControlLabel">
                                                                                        </td>
                                                                                        <td style="width:25.2%">
                                                                                        </td>
                                                                                        <td style="width:15%" class="ControlLabel">
                                                                                        </td>
                                                                                        <td style="width:25%" class="ControlLabel">
                                                                                            
                                                                                        </td>
                                                                                    </tr>--%>
                                                                                    <%--<tr>
                                                                                        <td class="ControlLabel" style="width:25%">
                                                                                            
                                                                                        </td>
                                                                                        <td style="width:25%">
                                                                                            <asp:CheckBox ID="chkboxAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkboxAll_CheckedChanged" Text="Copy from Old Entry" Font-Size="12px">
                                                                                            </asp:CheckBox>
                                                                                        </td>
                                                                                        <td colspan="2">
                                                                                            <table style="width:100%">
                                                                                                <asp:Panel ID="Panelcopy" runat="server" Visible="False">
                                                                                                    <tr>
                                                                                                        <td class="ControlLabel" style="width:15%">
                                                                                                            Old Ref No
                                                                                                        </td>
                                                                                                        <td class="ControlTextBox3" style="width:25%">--%>
                                                                                    <%--<asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                    <asp:DropDownList ID="combooldrefno" runat="server"  style="border: 1px solid #90c9fc" height="26px"  CssClass="drpDownListMedium" BackColor = "#90c9fc" AutoPostBack="true"
                                                                                                                        DataValueField="TransNo" DataTextField="RefNo" Width="100%" DataSourceID="srcOldRefno"
                                                                                                                        AppendDataBoundItems="true" OnSelectedIndexChanged="combooldrefno_SelectedIndexChanged">
                                                                                                                        <asp:ListItem style="background-color: #90c9fc" Text="Select RefNo " Value="0"></asp:ListItem>
                                                                                                                    </asp:DropDownList>
                                                                                                                    </ContentTemplate>
                                                                                                                <Triggers>
                                                                                                                    <asp:AsyncPostBackTrigger ControlID="combooldrefno" EventName="SelectedIndexChanged" />
                                                                                                                </Triggers>
                                                                                                            </asp:UpdatePanel>--%>
                                                                                    <%--<asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                                                                <ContentTemplate>
                                                                                                                    <asp:TextBox ID="txtoldref" runat="server" SkinID="skinTxtBoxGrid" OnTextChanged="TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                                                            </ContentTemplate>
                                                                                                                <Triggers>
                                                                                                                    <asp:AsyncPostBackTrigger ControlID="txtoldref" EventName="TextChanged" />
                                                                                                                </Triggers>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </asp:Panel>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="<%--height:3px">
                                                                                    </tr>--%>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 25%">Branch *
                                                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpBranchAdd"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Branch. It cannot be left blank."
                                                                                                Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 25.2%">
                                                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="drpBranchAdd" TabIndex="10" OnSelectedIndexChanged="drpBranchAdd_SelectedIndexChanged" AutoPostBack="true" Width="100%" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        runat="server">
                                                                                                    </asp:DropDownList>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <td style="width: 15%" class="ControlLabel"></td>
                                                                                        <td style="width: 25%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 25%" class="ControlLabel">Ref. No. *
                                                                                            <asp:RequiredFieldValidator ID="rvRefNoAdd" runat="server" ControlToValidate="txtRefNoAdd"
                                                                                                ErrorMessage="Please enter Ref. No. It cannot be left blank." Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td style="width: 25.2%" class="ControlTextBox3">
                                                                                            <asp:TextBox ID="txtRefNoAdd" runat="server" Text='<%# Bind("RefNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 15%" class="ControlLabel">
                                                                                            <%--<asp:Label ID="Label1" runat="server"
                                                                                                        Width="120px" Text="" ></asp:Label>--%>

                                                                                            <asp:RequiredFieldValidator ID="rvStockAdd" runat="server" ControlToValidate="txtTransDateAdd"
                                                                                                ErrorMessage="Trasaction Date is mandatory" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ControlToValidate="txtTransDateAdd" Operator="DataTypeCheck"
                                                                                                Type="Date" ErrorMessage="Please select a valid date" runat="server" ID="cmpValtxtDateAdd">*</asp:CompareValidator>
                                                                                            <asp:RangeValidator ID="myRangeValidatorAdd" runat="server" ControlToValidate="txtTransDateAdd"
                                                                                                ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>
                                                                                            Date *
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                            <asp:TextBox ID="txtTransDateAdd" Enabled="false" runat="server" Text='<%# Bind("TransDate","{0:dd/MM/yyyy}") %>'
                                                                                                CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calExt3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                PopupButtonID="btnDateAdd" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd">
                                                                                            </cc1:CalendarExtender>
                                                                                            <td>
                                                                                                <asp:ImageButton ID="btnDateAdd" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                    Width="20px" runat="server" />
                                                                                            </td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 25%">Paid To *
                                                                                            <asp:CompareValidator ID="cvPayedToAdd" runat="server" ControlToValidate="ComboBox2Add"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Paid To. It cannot be left blank."
                                                                                                Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:DropDownList ID="ComboBox2Add" runat="server" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium" BackColor="#e7e7e7" AutoPostBack="false"
                                                                                                        Width="100%"
                                                                                                        AppendDataBoundItems="true">
                                                                                                        <%--<asp:ListItem Text="Select Customer" Value="0"></asp:ListItem>--%>
                                                                                                    </asp:DropDownList>
                                                                                                </ContentTemplate>
                                                                                                <Triggers>
                                                                                                    <asp:AsyncPostBackTrigger ControlID="drpBranchAdd" EventName="SelectedIndexChanged" />
                                                                                                </Triggers>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 15%">Amount *
                                                                                            <asp:RequiredFieldValidator ID="rvModelAdd" runat="server" ControlToValidate="txtAmountAdd"
                                                                                                ErrorMessage="Please enter Amount. It cannot be left blank." Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            <cc1:FilteredTextBoxExtender ID="fltAmtAdd" runat="server" TargetControlID="txtAmountAdd"
                                                                                                ValidChars="." FilterType="Numbers, Custom" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtAmountAdd" runat="server" Text='<%# Bind("Amount") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 25%">Payment Made By *
                                                                                            <asp:RequiredFieldValidator ID="rvBDateAdd" runat="server" ControlToValidate="chkPayToAdd"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Payment Made By. It cannot be left blank.">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:RadioButtonList ID="chkPayToAdd" runat="server" OnDataBound="chkPayToAdd_DataBound"
                                                                                                onclick="javascript:AdvancedAdd(this.id);" AutoPostBack="false" Width="100%"
                                                                                                OnSelectedIndexChanged="chkPayToAdd_SelectedIndexChanged">
                                                                                                <asp:ListItem Text="Cash" Selected="true"></asp:ListItem>
                                                                                                <asp:ListItem Text="Cheque"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 15%">Narration *
                                                                                            <asp:RequiredFieldValidator ID="rvNarrationAdd" runat="server" ControlToValidate="txtNarrationAdd"
                                                                                                ErrorMessage="Please enter Narration. It cannot be left blank." Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtNarrationAdd" runat="server" Height="30px" TextMode="MultiLine"
                                                                                                Text='<%# Bind("Narration") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 2px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4" align="center">
                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Panel ID="PanelBankAdd" runat="server">
                                                                                                        <table width="100%" id="tblBankAdd" cellpadding="0" cellspacing="0" runat="server">
                                                                                                            <tr>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="ControlLabel" style="width: 25%">Bank Name *
                                                                                                                </td>
                                                                                                                <td class="ControlDrpBorder" style="width: 25%">
                                                                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                                        <ContentTemplate>
                                                                                                                            <asp:DropDownList ID="ddBanksAdd" runat="server" AutoPostBack="true" CssClass="drpDownListMedium" OnSelectedIndexChanged="ddBanksAdd_SelectedIndexChanged" BackColor="#e7e7e7" SelectedValue='<%# Bind("CreditorID") %>'
                                                                                                                                DataSourceID="srcBanksAdd" DataTextField="LedgerName" Width="100%" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                                DataValueField="LedgerID" AppendDataBoundItems="True">
                                                                                                                                <asp:ListItem Selected="True" style="background-color: #e7e7e7" Value="0">Select Bank</asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </ContentTemplate>
                                                                                                                    </asp:UpdatePanel>
                                                                                                                </td>
                                                                                                                <td class="ControlLabel" style="width: 15%">Cheque No. *
                                                                                                                </td>
                                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                                                        <ContentTemplate>
                                                                                                                            <asp:TextBox ID="txtChequeNoAdd" runat="server" Text='<%# Bind("ChequeNo") %>' SkinID="skinTxtBoxGridBank" MaxLength="10"></asp:TextBox>
                                                                                                                            <asp:DropDownList ID="cmbChequeNo1" runat="server" AppendDataBoundItems="True" AutoPostBack="true" BackColor="#e7e7e7" DataTextField="ChequeNo" DataValueField="ChequeNo" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid #e7e7e7" Width="100%">
                                                                                                                                <asp:ListItem Selected="True" style="height: 1px; background-color: #e7e7e7" Value="0">Select Cheque No</asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </ContentTemplate>
                                                                                                                    </asp:UpdatePanel>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                        <cc1:TabPanel ID="tabInsAddTab" runat="server" HeaderText="Additional Details">
                                                                            <ContentTemplate>
                                                                                <table align="center" cellpadding="3" cellspacing="5" style="border: 0px solid #5078B3; width: 800px;">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 35%;">Against Bill No.
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%;">
                                                                                            <asp:TextBox ID="txtBillAdd" runat="server" Text='<%# Bind("BillNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%;"></td>
                                                                                        <td style="width: 25%;"></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                    </cc1:TabContainer>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ShowSummary="false" HeaderText=" " Font-Names="'Trebuchet MS'"
                                                                        Font-Size="12" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 8px">
                                                                <td colspan="4"></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 25%;"></td>
                                                                            <td align="right" style="width: 25%;">
                                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="InsertButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="center" style="width: 20%;">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName=""
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="left" style="width: 30%;">
                                                                                <%--<asp:Button ID="GetRefNo" runat="server" OnClick="GetRefNo_Click" CssClass="AddGetRefNo6" CausesValidation="False"
                                                                                    EnableTheming="false" Text=""></asp:Button>--%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcCreditorDebitorAdd" runat="server" SelectMethod="ListSundryDebitorsIsActive"
                                                                    TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcBanksAdd" runat="server" SelectMethod="ListBanksIsActive" TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcOldRefno" runat="server" SelectMethod="ListCustomerPaymentsRef"
                                                                    TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
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
                            <%--<div>
                                <table>
                                    <tr>
                                        <td align="left" colspan="5">
                                            <cc1:ModalPopupExtender ID="ModalPopupContact" runat="server" BackgroundCssClass="modalBackground"
                                                CancelControlID="CancelPopUpContact" DynamicServicePath="" Enabled="True" PopupControlID="pnlContact"
                                                TargetControlID="ShowPopUpContact">
                                            </cc1:ModalPopupExtender>
                                            <input id="ShowPopUpContact" type="button" style="display: none" runat="server" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<input ID="CancelPopUpContact" runat="server" style="display: none" 
                                                type="button" /> </input>
                                            </input>
                                            &nbsp;<asp:ValidationSummary ID="VSContact" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                HeaderText="Validation Messages" ShowMessageBox="true" ShowSummary="true" ValidationGroup="contact" />
                                            <asp:Panel ID="pnlContact" runat="server" Width="700px" CssClass="modalPopup">
                                                <asp:UpdatePanel ID="updatePnlContact" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="pnlContactItems" CssClass="pnlPopUp" runat="server">
                                                            <div id="Div1">
                                                                <table class="tblLeft" cellpadding="3" cellspacing="2" width="100%">
                                                                    <tr>
                                                                        <td style="width:5px;">
                                                                        </td>
                                                                        <td>
                                                                            <table class="headerPopUp" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        Get RefNo
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width:5px;">
                                                                        </td>
                                                                    </tr>
                                                                                                                
                                                                    <tr>
                                                                        <td style="width:5px;">
                                                                            <asp:HiddenField ID="hdCurrentRow" runat="server" Value="0" />
                                                                        </td>
                                                                        <td>
                                                                            <table style="width: 100%; border: 1px solid #86b2d1" cellpadding="3" cellspacing="1">
                                                                                <tr style="height:1px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 32%" class="ControlLabel">
                                                                                        <asp:RequiredFieldValidator ID="rvDate" runat="server" ControlToValidate="txtContactedDate"
                                                                                                                                        ErrorMessage="RefNo is mandatory" Text="*" ValidationGroup="contact" />
                                                                                        Old RefNo
                                                                                    </td>
                                                                                    <td style="width: 27%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtContactedDate" runat="server" CssClass="cssTextBox" ValidationGroup="contact" Width="70px" Height="23px" BackColor = "#90c9fc" ></asp:TextBox>
                                                                                    </td>
                                                                                    <td style="width: 35%" class="ControlLabel">
                                                                                        
                                                                                    </td>
                                                                                    
                                                                                </tr>
                                                                                                                            
                                                                            </table>
                                                                        </td>
                                                                        <td style="width:5px;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:5px;">
                                                                        </td>
                                                                        <td>
                                                                            <table class="tblLeft" cellpadding="1" cellspacing="2"
                                                                                width="100%">
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <table style="border: 1px solid #86b2d1">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Panel ID="Panel2" runat="server" Width="120px" Height="32px">
                                                                                                        <asp:Button ID="cmdCancelContact" runat="server" CssClass="CloseWindow6" Height="45px" OnClick="cmdCancelContact_Click" CausesValidation="false"
                                                                                                            EnableTheming="false"/>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Panel ID="Panel3" runat="server" Width="120px" Height="32px">
                                                                                                        <asp:Button ID="cmdSaveContact" runat="server" CssClass="AddGetRefNos6" 
                                                                                                            EnableTheming="false" OnClick="cmdSaveContact_Click" Text="" Height="45px" 
                                                                                                            ValidationGroup="contact" />
                                                                                                                                                    
                                                                                                        <asp:Button ID="cmdUpdateContact" runat="server" CssClass="UpGetRefNos" 
                                                                                                            EnableTheming="false" Height="45px" 
                                                                                                            OnClick="cmdUpdateContact_Click" Text="" ValidationGroup="contact" 
                                                                                                            Width="45px" />
                                                                                                                                                    
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                                                                       
                                                                                        </table>
                                                                                    </td>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width:5px;">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                     </tr>
                                </table>
                            </div>--%>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewPayment" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewPayment_RowCreated" Width="100.4%" DataSourceID="GridSource"
                                            AllowPaging="True" DataKeyNames="TransNo" EmptyDataText="No Customer / Dealer Payments found!"
                                            OnRowCommand="GrdViewPayment_RowCommand" OnRowDataBound="GrdViewPayment_RowDataBound"
                                            OnSelectedIndexChanged="GrdViewPayment_SelectedIndexChanged" OnRowDeleting="GrdViewPayment_RowDeleting"
                                            OnRowDeleted="GrdViewPayment_RowDeleted" CssClass="someClass">
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="TransNo" HeaderStyle-Wrap="false" HeaderText="Trans. No." HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="RefNo" HeaderStyle-Wrap="false" HeaderText="Ref. No." HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="TransDate" HeaderStyle-Wrap="false" HeaderText="Trans. Date" HeaderStyle-BorderColor="Gray"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="Debi" HeaderText="Paid To" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="LedgerName" HeaderText="Bank Name / Cash" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Narration" HeaderText="Narration" Visible="false" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Branchcode" HeaderText="Branch Code" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Payment?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="50px" HeaderText="Print" HeaderStyle-BorderColor="Gray">
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
                                                <table>
                                                    <tr>
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" BackColor="#e7e7e7" Style="border: 1px solid Gray" AutoPostBack="true" Width="70px">
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
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListCustomerPayments"
                            TypeName="BusinessLogic" DeleteMethod="DeletePayment" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="TransNo" Type="Int32" />
                                <asp:Parameter Name="requireValidation" Type="Boolean" DefaultValue="true" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetPaymentForId"
                            TypeName="BusinessLogic" InsertMethod="InsertPayment" OnUpdating="frmSource_Updating"
                            OnInserting="frmSource_Inserting" UpdateMethod="UpdateCustPayment" OnInserted="frmSource_Inserted"
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
                                <asp:Parameter Name="BillNo" Type="String" />
                                <asp:Parameter Name="NewTransNo" Type="Int32" Direction="Output" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="BranchCode" Type="String" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewPayment" Name="TransNo" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:CookieParameter CookieName="Company" Type="String" Name="connection" />
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
                                <asp:Parameter Name="PaymentMode" Type="String" />
                                <asp:Parameter Name="NewTransNo" Type="Int32" Direction="Output" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="BranchCode" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:HiddenField ID="hdPayment" runat="server" />
                        <asp:HiddenField ID="hddate" runat="server" Value="0" />
                        <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                        <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                    </td>
                </tr>
            </table>
            <input type="hidden" id="hidAdvancedState" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>
    <table align="center">
        <tr>
            <td style="width: 50%">
                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                        EnableTheming="false" Width="80px" Text=""></asp:Button>
                </asp:Panel>
            </td>
            <td style="width: 50%">
                <asp:Button ID="btnpay" runat="server"
                    CssClass="exportexl6" EnableTheming="false" CausesValidation="false"
                    OnClientClick="window.open('ReportExcelPayments.aspx?ID=CustPay','Summary', 'toolbar=no,status=no,menu=no,location=no,height=280,width=650,left=405,top=220 ,resizable=yes, scrollbars=yes');"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
