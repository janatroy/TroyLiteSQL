<%@ Page Title="Financials > Credit/Debit Notes" Language="C#" MasterPageFile="~/PageMaster.master"
    AutoEventWireup="true" CodeFile="CreditDebitNote.aspx.cs" Inherits="CreditDebitNote" %>

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
            window.showModalDialog('./PrintNote.aspx?ID=' + ID, self, 'dialogWidth:700px;dialogHeight:430px;status:no;dialogHide:yes;unadorned:yes;');
        }

        function ChangeHeader() {

            var rdoArray = document.getElementsByTagName("input");

            for (i = 0; i <= rdoArray.length - 1; i++) {

                if (rdoArray[i].type == 'radio' && rdoArray[i].checked) {

                    if (rdoArray[i].value == 'Credit' && rdoArray[i].checked == true) {
                        document.getElementById('ctl00_cplhControlPanel_frmViewAdd_pnlHeader').innerHTML = "Credit Note";
                    }
                    else {
                        document.getElementById('ctl00_cplhControlPanel_frmViewAdd_pnlHeader').innerHTML = "Debit Note";
                    }

                }

            }
        }

        function CheckNoteBill() {
            if (document.getElementById('ctl00_cplhControlPanel_HiddenField1').value == '1') {
                var rv = confirm("Want to Create a Ledger with LedgerName - CreditDebitNoteId, Do you want to create the ledger as default?");

                if (rv == true) {
                    document.getElementById('ctl00_cplhControlPanel_HiddenField2').value = "1";
                    return true;
                }
                else {
                    document.getElementById('ctl00_cplhControlPanel_HiddenField2').value = "0";
                    return window.event.returnValue = false;
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


    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>

            <table style="width: 100%">
                <tr style="width: 100%">
                    <td style="width: 100%">

                        <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Credit / Debit Notes</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                         Credit / Debit Notes
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -1px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 1%"></td>
                                    <td style="width: 38%; font-size: 22px; color: white;">Credit / Debit Notes
                                    </td>
                                    <td style="width: 12%">
                                        <div style="text-align: right;">
                                           
                                        </div>
                                    </td>
                                    <td style="width: 10%; color: white;" align="right">Search
                                            <asp:RequiredFieldValidator ID="rvSearch" runat="server" ControlToValidate="txtSearch"
                                                Display="Dynamic" EnableClientScript="False" Enabled="false">Search is mandatory</asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <div style="width: 150px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="156px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid white">
                                                <asp:ListItem Value="NoteID">Note ID</asp:ListItem>
                                                <asp:ListItem Value="RefNo">Ref. No.</asp:ListItem>
                                                <asp:ListItem Value="Ledger">Creditor / Debitor</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 15%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" Text="" EnableTheming="false" CssClass="ButtonSearch6" />
                                    </td>
                                    <td style="width: 16%" class="tblLeftNoPad">
                                        <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <asp:Panel runat="server" ID="popUp" Style="width: 55%">
                            <div id="contentPopUp">
                                <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3; background-color: #fff; color: #000;"
                                    width="100%">
                                    <tr>
                                        <td>
                                            <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
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
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="5" class="headerPopUp">
                                                                    <asp:Label ID="pnlHeader" runat="server" Text="Credit Debit Note"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="left">
                                                                    <cc1:TabContainer ID="tabEdit" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabEditMain" runat="server" HeaderText="Note Details">
                                                                            <ContentTemplate>
                                                                                <table style="width: 800px; vertical-align: text-top; border: 0px solid #86b2d1;"
                                                                                    align="center" cellspacing="2" cellpadding="3">
                                                                                    <tr>
                                                                                        <td colspan="5">
                                                                                            <asp:HiddenField ID="hdNoteID" runat="server" Value='<%# Bind("NoteID") %>' />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 5px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Type
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                            <asp:RadioButtonList runat="server" ID="rdoCDType" Width="100%" AutoPostBack="false"
                                                                                                onclick="javascript:ChangeHeader();" CssClass="cssTextBox" RepeatDirection="Horizontal"
                                                                                                OnDataBound="rdoCDType_DataBound">
                                                                                                <asp:ListItem Text="Credit" Value="Credit"></asp:ListItem>
                                                                                                <asp:ListItem Text="Debit" Value="Debit" Selected="True"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 15%">Ledger *
                                                                    <asp:CompareValidator ID="cvPayedTo" runat="server" ControlToValidate="ComboBox2"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Ledger. It cannot be left blank"
                                                                        Operator="GreaterThan" ValueToCompare="0"></asp:CompareValidator>
                                                                                        </td>
                                                                                        <td align="left" style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="ComboBox2" runat="server" AutoPostBack="False" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                DataSourceID="srcCreditorDebitor" DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                AppendDataBoundItems="true" OnDataBound="ComboBox2_DataBound">
                                                                                                <asp:ListItem Text="Select Ledger" style="background-color: #e7e7e7" Value="0"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Ref. No. *
                                                                    <asp:RequiredFieldValidator ID="rvRefNo" runat="server" ControlToValidate="txtRefNo"
                                                                        Text="*" ErrorMessage="Please enter Ref.No. It cannot be left blank" CssClass="rfv" Display="Dynamic"
                                                                        EnableClientScript="True"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtRefNo" runat="server" Text='<%# Bind("RefNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 15%">
                                                                                            <asp:RequiredFieldValidator ID="rvStock" runat="server" ControlToValidate="txtTransDate"
                                                                                                Text="*" ErrorMessage="Please select Date. It cannot be left blank" Display="Dynamic" EnableClientScript="True"></asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ControlToValidate="txtTransDate" Operator="DataTypeCheck" Type="Date"
                                                                                                Text="*" ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDate"></asp:CompareValidator>
                                                                                            <%--<asp:RangeValidator ID="myRangeValidator" runat="server" ControlToValidate="txtTransDate"
                                                                        ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>--%>
                                                                    Date *
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtTransDate" Enabled="false" runat="server" Text='<%# Bind("NoteDate","{0:dd/MM/yyyy}") %>'
                                                                                                CssClass="cssTextBox"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtTransDate">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                Width="20px" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Amount *
                                                                    <asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtAmount"
                                                                        ErrorMessage="Please enter Amount. It cannot be left blank" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            <cc1:FilteredTextBoxExtender ID="fltAmt" runat="server" TargetControlID="txtAmount"
                                                                                                ValidChars="." FilterType="Numbers, Custom" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="100%"
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 15%">Note *
                                                                    <asp:RequiredFieldValidator ID="rvNarration" runat="server" ControlToValidate="txtNarration"
                                                                        ErrorMessage="Please enter Note. It cannot be left blank" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtNarration" runat="server" Height="30px" TextMode="MultiLine"
                                                                                                Text='<%# Bind("Note") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                    <tr style="height: 5px">
                                                                                        <td colspan="5"></td>
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
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                                <td colspan="5"></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 30%"></td>
                                                                            <td align="center" style="width: 18%">
                                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="left" style="width: 18%">
                                                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 30%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>

                                                            </tr>
                                                        </table>

                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitorSuppliers"
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
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <InsertItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="5" class="headerPopUp">
                                                                    <asp:Label ID="pnlHeader" runat="server" Text="Debit Note"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="left">
                                                                    <cc1:TabContainer ID="tablInsert" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabInsMain" runat="server" HeaderText="Note Details">
                                                                            <ContentTemplate>
                                                                                <table style="width: 800px; vertical-align: text-top; border: 0px solid #86b2d1;"
                                                                                    align="center" cellspacing="2" cellpadding="3">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Type
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:RadioButtonList runat="server" ID="rdoCDTypeAdd" Width="100%" AutoPostBack="false"
                                                                                                onclick="javascript:ChangeHeader();" CssClass="cssTextBox" RepeatDirection="Horizontal">
                                                                                                <asp:ListItem Text="Credit" Value="Credit"></asp:ListItem>
                                                                                                <asp:ListItem Text="Debit" Value="Debit" Selected="True"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 15%">Ledger *
                                                                    <asp:CompareValidator ID="cvPayedToAdd" runat="server" ControlToValidate="ComboBox2Add"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Please select Ledger. It cannot be left blank"
                                                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                        </td>
                                                                                        <td align="left" style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="ComboBox2Add" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7" AutoPostBack="False"
                                                                                                DataSourceID="srcCreditorDebitorAdd" DataValueField="LedgerID" DataTextField="LedgerName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                AppendDataBoundItems="true">
                                                                                                <asp:ListItem style="background-color: #e7e7e7" Text="Select Ledger" Value="0"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Ref. No. *
                                                                    <asp:RequiredFieldValidator ID="rvRefNoAdd" runat="server" ControlToValidate="txtRefNoAdd"
                                                                        ErrorMessage="Please enter Ref.No. It cannot be left blank" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtRefNoAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 15%">
                                                                                            <asp:RequiredFieldValidator ID="rvStockAdd" runat="server" ControlToValidate="txtTransDateAdd"
                                                                                                ErrorMessage="Please select Date. It cannot be left blank." Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            <asp:CompareValidator ControlToValidate="txtTransDateAdd" Operator="DataTypeCheck"
                                                                                                Type="Date" ErrorMessage="Please enter a valid date" runat="server" ID="cmpValtxtDateAdd">*</asp:CompareValidator>
                                                                                            <%--<asp:RangeValidator ID="myRangeValidatorAdd" runat="server" ControlToValidate="txtTransDateAdd"
                                                                        ErrorMessage="Payment date cannot be future date." Text="*" Type="Date"></asp:RangeValidator>--%>
                                                                    Date *
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtTransDateAdd" Enabled="false" runat="server" CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calExtender312" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                PopupButtonID="btnDate312" PopupPosition="BottomLeft" TargetControlID="txtTransDateAdd">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:ImageButton ID="btnDate312" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                Width="20px" runat="server" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Amount *
                                                                    <asp:RequiredFieldValidator ID="rvModelAdd" runat="server" ControlToValidate="txtAmountAdd"
                                                                        ErrorMessage="Please enter Amount. It cannot be left blank" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                            <cc1:FilteredTextBoxExtender ID="fltAmtAdd" runat="server" TargetControlID="txtAmountAdd"
                                                                                                ValidChars="." FilterType="Numbers, Custom" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtAmountAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 15%">Note *
                                                                    <asp:RequiredFieldValidator ID="rvNarrationAdd" runat="server" ControlToValidate="txtNarrationAdd"
                                                                        ErrorMessage="Please enter Note. It cannot be left blank" Display="Dynamic" EnableClientScript="True">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtNarrationAdd" runat="server" Height="30px" TextMode="MultiLine"
                                                                                                Width="100%" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                    <tr style="height: 10px">
                                                                                        <td colspan="5"></td>
                                                                                    </tr>

                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                        <cc1:TabPanel ID="tabInsAddTab" runat="server" HeaderText="Additional Details">
                                                                            <ContentTemplate>
                                                                                <table align="center" cellpadding="3" cellspacing="1" style="border: 0px solid #5078B3; width: 800px;">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 35%">Against Bill No.
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                            <asp:TextBox ID="txtBillAdd" runat="server" Text='<%# Bind("BillNo") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 15%"></td>
                                                                                        <td style="width: 10%"></td>
                                                                                        <td></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                    </cc1:TabContainer>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 10px">
                                                                <td colspan="5"></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 30%"></td>
                                                                            <td align="center" style="width: 18%">
                                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="InsertButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td align="center" style="width: 18%">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                            </td>

                                                                            <td style="width: 30%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcCreditorDebitorAdd" runat="server" SelectMethod="ListCreditorDebitorSuppliersForBranch"
                                                                    TypeName="BusinessLogic">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                         <asp:CookieParameter Name="Username" CookieName="LoggedUserName" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
                                                            <td>
                                                                <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                    ShowSummary="false" HeaderText="" Font-Names="'Trebuchet MS'"
                                                                    Font-Size="12" runat="server" />
                                                            </td>
                                                            <td></td>
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
                        <table width="100%" style="margin: -4px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewNote" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewNote_RowCreated" Width="100.4%" DataSourceID="GridSource"
                                            AllowPaging="True" DataKeyNames="TransNo" EmptyDataText="No Notes found!" OnRowCommand="GrdViewNote_RowCommand"
                                            OnRowDataBound="GrdViewNote_RowDataBound" OnSelectedIndexChanged="GrdViewNote_SelectedIndexChanged"
                                            OnRowDeleting="GrdViewNote_RowDeleting" OnRowDeleted="GrdViewNote_RowDeleted" CssClass="someClass">
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="NoteID" HeaderStyle-Wrap="false" HeaderText="Note ID" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="RefNo" HeaderStyle-Wrap="false" HeaderText="Ref. No." HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="NoteDate" HeaderStyle-Wrap="false" HeaderText="Note Date" HeaderStyle-BorderColor="Gray"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="CDType" HeaderText="Type" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="LedgerName" HeaderText="Ledger" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Note?"
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
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" Style="border: 1px solid blue" BackColor="#e7e7e7">
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
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListNotes" TypeName="BusinessLogic"
                            DeleteMethod="DeleteCreditDebitNote" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="TransNo" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetCreditDebitNoteForId"
                            TypeName="BusinessLogic" InsertMethod="InsertCreditDebitNote" OnUpdating="frmSource_Updating"
                            OnInserting="frmSource_Inserting" UpdateMethod="UpdateCreditDebitNote" OnInserted="frmSource_Inserted"
                            OnUpdated="frmSource_Updated">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="TransNo" Type="Int32" />
                                <asp:Parameter Name="NoteID" Type="Int32" />
                                <asp:Parameter Name="RefNo" Type="String" />
                                <asp:Parameter Name="CDType" Type="String" />
                                <asp:Parameter Name="NoteDate" Type="DateTime" />
                                <asp:Parameter Name="LedgerID" Type="Int32" />
                                <asp:Parameter Name="Amount" Type="Double" />
                                <asp:Parameter Name="Note" Type="String" />
                                <asp:Parameter Name="BillNo" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />

                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewNote" Name="TransNo" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:CookieParameter CookieName="Company" Type="String" Name="connection" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="RefNo" Type="String" />
                                <asp:Parameter Name="NoteDate" Type="DateTime" />
                                <asp:Parameter Name="CDType" Type="String" />
                                <asp:Parameter Name="LedgerID" Type="Int32" />
                                <asp:Parameter Name="Amount" Type="Double" />
                                <asp:Parameter Name="Note" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:HiddenField ID="hdPayment" runat="server" />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                        <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
            <input type="hidden" id="hidAdvancedState" runat="server" />
            <input id="dummy" type="button" style="display: none" runat="server" />
            <input id="Button1" type="button" style="display: none" runat="server" />
            <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
            <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                TargetControlID="dummy">
            </cc1:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table align="center">
        <tr>
            <td style="width:50%">
                 <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66" OnClientClick="javascript:CheckNoteBill();"
                                                    EnableTheming="false" Text=""></asp:Button>
                                            </asp:Panel>
            </td>
            <td style="width:50%">
                <asp:Button ID="Creditnote" runat="server"
                    CssClass="exportexl6" EnableTheming="false" CausesValidation="false"
                    OnClientClick="window.open('ReportExcelCreditnote.aspx ','Summary', 'toolbar=no,status=no,menu=no,location=no,height=310,width=500,left=425,top=220 ,resizable=yes, scrollbars=yes');"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
