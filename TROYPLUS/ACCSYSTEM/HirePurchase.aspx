<%@ Page Title="Sales > HirePurchase" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="HirePurchase.aspx.cs" Inherits="HirePurchase" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
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

        function CheckLeadContact() {

        }

    </script>

    <style id="Style1" runat="server">
        .someClass td {
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


    <asp:UpdatePanel ID="UpdatePanelPage" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table style="width: 100%;" align="center">
                <tr style="width: 100%">
                    <td style="width: 100%" valign="middle">

                        <div class="mainConBody">
                            <div>
                                <table cellspacing="2px" cellpadding="3px" border="0" width="99.8%" style="margin: -2px 0px 0px 1px;"
                                    class="searchbg">
                                    <tr>
                                        <td style="width: 2%"></td>
                                        <td style="width: 20%; font-size: 22px; color: white;">Hire Purchase
                                        </td>
                                        <td style="width: 13%">
                                            <div style="text-align: right;">
                                                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66" CausesValidation="false"
                                                        EnableTheming="false" Width="80px" Text=""></asp:Button>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                        <td style="width: 15%; color: white;" align="right">Search
                                        </td>
                                        <td style="width: 17%" class="NewBox">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="cssTextBox" Width="92%"></asp:TextBox>
                                        </td>
                                        <td style="width: 5%"></td>
                                        <td style="width: 18%" class="NewBox">
                                            <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid white">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                    <asp:ListItem Value="BillNo">BillNo</asp:ListItem>
                                                    <asp:ListItem Value="BillDate">Bill Date</asp:ListItem>
                                                    <asp:ListItem Value="RefNo">Branch Ref No</asp:ListItem>
                                                    <asp:ListItem Value="dob">Date of birth</asp:ListItem>
                                                    <asp:ListItem Value="CustomerName">Customer Name</asp:ListItem>
                                                    <asp:ListItem Value="Mobile">Mobile</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 13%; text-align: left">
                                            <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click" CausesValidation="false"
                                                CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" />
                                        </td>
                                        <td style="width: 20%" class="tblLeftNoPad">
                                            <asp:Button ID="BtnClearFilter" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false"  CausesValidation="false" Text="" CssClass="ClearFilter6" />
                                        </td>
                                        <td style="width: 10%"></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <asp:ValidationSummary ID="VS" runat="server" Font-Names="'Trebuchet MS'"
                            Font-Size="12pt" HeaderText=" " ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="salesval" />
                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="popUp" Style="width: 60%; display: none">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div id="contentPopUp">
                                        <asp:Panel ID="pnlEdit" runat="server">
                                            <table class="tblLeft" cellpadding="0" cellspacing="2"
                                                width="100%">
                                                <tr>
                                                    <td>
                                                        <div class="divArea">
                                                            <table class="tblLeft" cellpadding="1" cellspacing="2" style="width: 100%;">
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <table class="headerPopUp">
                                                                            <tr>
                                                                                <td>Hire Purchase
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                            <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Hire Purchase Details">
                                                                                <HeaderTemplate>
                                                                                    <div>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td><b>Hire Purchase Details </b></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <div>
                                                                                        <table style="width: 770px;" align="center" cellpadding="1" cellspacing="2">
                                                                                            <tr>
                                                                                                <td class="ControlLabel" style="width: 20%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="salesval"
                                                                                                        ControlToValidate="txtbillnonew" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please enter Bill No. It cannot be left blank." Text="*"></asp:RequiredFieldValidator>
                                                                                                    Bill No *
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%;">
                                                                                                    <asp:TextBox ID="txtbillnonew" runat="server" BackColor="#e7e7e7"
                                                                                                        SkinID="skinTxtBoxGrid" TabIndex="1" Width="200px"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 20%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="salesval"
                                                                                                        ControlToValidate="txtbranchrefno" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please enter Branch Ref No. It cannot be left blank." Text="*"></asp:RequiredFieldValidator>
                                                                                                    Branch Ref No *
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%;">
                                                                                                    <asp:TextBox ID="txtbranchrefno" runat="server" BackColor="#e7e7e7"
                                                                                                        SkinID="skinTxtBoxGrid" TabIndex="2" Width="200px"></asp:TextBox>
                                                                                                </td>
                                                                                                <td align="left" style="width: 5%;"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="ControlLabel" style="width: 20%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                                                        ControlToValidate="txtBillDate" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please select BillDate. It cannot be left blank." Text="*"></asp:RequiredFieldValidator>
                                                                                                    <asp:RangeValidator ID="mrBillDate" runat="server"
                                                                                                        ControlToValidate="txtBillDate" ErrorMessage="Bill date cannot be future date."
                                                                                                        Text="*" Type="Date"></asp:RangeValidator>
                                                                                                    Bill Date *
                                                                                                </td>
                                                                                                <td style="width: 24%;" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtBillDate" Enabled="false" runat="server" AutoPostBack="True"
                                                                                                        BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" MaxLength="10" ValidationGroup="salesval"
                                                                                                        OnTextChanged="txtBillDate_TextChanged" TabIndex="3"></asp:TextBox>
                                                                                                    <cc1:CalendarExtender ID="calBillDate" runat="server" Enabled="True"
                                                                                                        Format="dd/MM/yyyy"
                                                                                                        PopupButtonID="btnBillDate" TargetControlID="txtBillDate">
                                                                                                    </cc1:CalendarExtender>

                                                                                                    <asp:Label ID="lblBillNo" runat="server" BackColor="#e7e7e7"
                                                                                                        Visible="False"></asp:Label>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 20%;">
                                                                                                    <asp:ImageButton ID="btnBillDate" runat="server" CausesValidation="False"
                                                                                                        ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                                    <asp:Label ID="Label1" runat="server" Width="150px" Text="Others"></asp:Label>
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%;">
                                                                                                    <asp:TextBox ID="txtothers" runat="server" BackColor="#e7e7e7"
                                                                                                        CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="4"
                                                                                                        Width="500px" ViewStateMode="Enabled"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 5%;"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr style="height: 30px">
                                                                                                <td class="ControlLabel" style="width: 25%;">Name *
                                                                                                <asp:CompareValidator ID="cvCustomer" runat="server"
                                                                                                    ControlToValidate="cmbCustomer" Display="Dynamic"
                                                                                                    ErrorMessage="Please Select Customer!!.It cannot be left blank." Operator="GreaterThan" Text="*"
                                                                                                    ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>
                                                                                                </td>
                                                                                                <td class="ControlDrpBorder" style="width: 24%;">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:DropDownList ID="cmbCustomer" runat="server" AppendDataBoundItems="true"
                                                                                                                AutoPostBack="true" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                                                                DataTextField="LedgerName" DataValueField="LedgerID" Height="26px"
                                                                                                                OnSelectedIndexChanged="cmbCustomer_SelectedIndexChanged"
                                                                                                                Style="border: 1px solid #e7e7e7" TabIndex="5" ValidationGroup="salesval"
                                                                                                                Width="100%">
                                                                                                                <asp:ListItem style="background-color: #e7e7e7" Text="Select Customer" Value="0"></asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtOtherCusName" runat="server" BackColor="#e7e7e7"
                                                                                                                Height="10px" SkinID="skinTxtBox" TabIndex="5" ValidationGroup="salesval"
                                                                                                                Visible="False"></asp:TextBox>
                                                                                                            <asp:Label ID="lblledgerCategory" runat="server" CssClass="lblFont" Style="color: royalblue; font-weight: normal; font-size: smaller"></asp:Label>
                                                                                                        </ContentTemplate>
                                                                                                        <Triggers>
                                                                                                            <asp:AsyncPostBackTrigger ControlID="cmbCustomer"
                                                                                                                EventName="SelectedIndexChanged" />
                                                                                                        </Triggers>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%;">Mobile
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
                                                                                                    TargetControlID="txtMobile" ValidChars="+" />
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:TextBox ID="txtMobile" runat="server" BackColor="#e7e7e7"
                                                                                                        CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="6"
                                                                                                        Width="500px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                                                                                        ControlToValidate="txtdob" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please select Date of birth. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    Date of birth *

                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:TextBox ID="txtdob" Enabled="false" runat="server" BackColor="#e7e7e7" AutoPostBack="True" OnTextChanged="txtdocchr_TextChanged"
                                                                                                        CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="7"
                                                                                                        Width="500px"></asp:TextBox>
                                                                                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True"
                                                                                                        Format="dd/MM/yyyy"
                                                                                                        PopupButtonID="ImageButton3" TargetControlID="txtdob">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 25%;">
                                                                                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False"
                                                                                                        ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                                                                        ControlToValidate="drpPaymode" Display="Dynamic"
                                                                                                        ErrorMessage="Please Select Payment Mode.It cannot be left blank." Operator="GreaterThan" Text="*"
                                                                                                        ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>
                                                                                                    <asp:Label ID="Label3" runat="server" Width="150px" Text="Payment Mode *"></asp:Label>
                                                                                                </td>
                                                                                                <td class="ControlDrpBorder" style="width: 24%;">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:DropDownList ID="drpPaymode" runat="server" AppendDataBoundItems="True" Style="text-align: center; border: 1px solid #e7e7e7" Height="26px" BackColor="#e7e7e7" AutoPostBack="True" Width="100%" CssClass="drpDownListMedium"
                                                                                                                OnSelectedIndexChanged="drpPaymode_SelectedIndexChanged"
                                                                                                                TabIndex="8" ValidationGroup="salesval">
                                                                                                                <asp:ListItem Text="Select Paymode" Value="0"></asp:ListItem>
                                                                                                                <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                                                                                <asp:ListItem Text="Cheque" Value="2"></asp:ListItem>
                                                                                                                <asp:ListItem Text="ECS" Value="3"></asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr style="height: 30px">
                                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                                                                        ControlToValidate="txtDueDate" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please select Due Date.It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    <%--<asp:RangeValidator ID="mrduedate" runat="server" 
                                                                                                    ControlToValidate="txtDueDate" ErrorMessage="Bill date cannot be future date." 
                                                                                                    Text="*" Type="Date" ValidationGroup="salesval"></asp:RangeValidator>--%>
                                                                                                    Start Due Date *
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%;">
                                                                                                    <asp:TextBox ID="txtduedate" Enabled="false" runat="server"
                                                                                                        BackColor="#e7e7e7" CssClass="cssTextBox" Height="23px" MaxLength="10"
                                                                                                        TabIndex="9" ValidationGroup="salesval"></asp:TextBox>
                                                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                                                                        Format="dd/MM/yyyy"
                                                                                                        PopupButtonID="ImageButton2" TargetControlID="txtDueDate">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                                <%--<td align="left" style="width: 15%;">
                                                                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                                                                    ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                            </td>--%>
                                                                                                <td class="ControlLabel" style="width: 25%;" valign="middle">
                                                                                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False"
                                                                                                        ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                                                                        ControlToValidate="txtpuramt" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please enter Purchase Amount. It cannot be left blank" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    <asp:Label ID="Label2" runat="server" Width="150px" Text="Cost *"></asp:Label>
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%;">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtpuramt" runat="server" BackColor="#e7e7e7"
                                                                                                                MaxLength="10" SkinID="skinTxtBoxGrid" TabIndex="10" Width="200px" AutoPostBack="True" OnTextChanged="txtpuramt_TextChanged"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>

                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                                                        ControlToValidate="Txtinipay" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please enter Initial Payment.It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    Initial Payment *
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtinipay" runat="server" BackColor="#e7e7e7"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" AutoPostBack="True" OnTextChanged="txtinipay_TextChanged" TabIndex="11"
                                                                                                                Width="500px"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                                                        ControlToValidate="Txtlnamt" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please enter Loan Amount. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    Loan Amount *
                                                                                                </td>

                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="Txtlnamt" runat="server" BackColor="#e7e7e7"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="12"
                                                                                                                Width="500px"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>

                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr style="height: 30px">
                                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                                                                        ControlToValidate="txtdocchr" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please enter Document Charges.It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    Document Charges *
                                                                                                </td>

                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtdocchr" runat="server" BackColor="#e7e7e7" AutoPostBack="True" OnTextChanged="txtdocchr_TextChanged"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="13"
                                                                                                                Width="500px"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                                                                        ControlToValidate="txtintamt" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please enter Interest Amount. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    Interest Amount *
                                                                                                </td>

                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtintamt" runat="server" BackColor="#e7e7e7" AutoPostBack="True" OnTextChanged="txtintamt_TextChanged"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="14"
                                                                                                                Width="500px"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>

                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr style="height: 30px">
                                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                                                        ControlToValidate="txtfinpay" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please enter Final Payment. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    Amount to collect *
                                                                                                </td>

                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtfinpay" runat="server" BackColor="#e7e7e7"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="15"
                                                                                                                Width="500px"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                                                        ControlToValidate="txtnoinst1" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please enter No of installment. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    No Of Installment *
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtnoinst1" runat="server" BackColor="#e7e7e7" AutoPostBack="True" OnTextChanged="txtnoinst1_TextChanged"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="16"
                                                                                                                Width="500px"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>

                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr style="height: 30px">
                                                                                                <td class="ControlLabel" style="width: 15%;">
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                                                                        ControlToValidate="txteach" CssClass="lblFont" Display="Dynamic"
                                                                                                        ErrorMessage="Please enter Each Month Payment. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    Each Month Payment *
                                                                                                </td>

                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="Txteach" runat="server" BackColor="#e7e7e7"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="17"
                                                                                                                Width="500px"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%;">Down Payment
                                                                                                </td>

                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtdown1" runat="server" BackColor="#e7e7e7"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="18"
                                                                                                                Width="500px"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="lllkl" visible="false" runat="server">
                                                                                                <td class="ControlLabel" style="width: 15%;"></td>

                                                                                                <td style="width: 24%">
                                                                                                    <asp:TextBox ID="txtdatepay" runat="server" BackColor="#e7e7e7"
                                                                                                        CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="19"
                                                                                                        Width="500px" Visible="False"></asp:TextBox>
                                                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                                                                                        Format="dd/MM/yyyy"
                                                                                                        PopupButtonID="ImageButton3" TargetControlID="txtdatepay">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 25%;">

                                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                                                                                    ControlToValidate="txtothers" CssClass="lblFont" Display="Dynamic" 
                                                                                                    ErrorMessage="Each Month Payment is mandatory" Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>--%>
                                                                                                
                                                                                                </td>
                                                                                                <td style="width: 24%"></td>
                                                                                            </tr>
                                                                                            <tr id="lll" visible="false" runat="server">
                                                                                            </tr>
                                                                                            <tr style="height: 30px" id="lllll" visible="false" runat="server">
                                                                                                <td class="ControlLabel" style="width: 15%;">UpFront Interest
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtupfront" runat="server" BackColor="#e7e7e7" AutoPostBack="True" OnTextChanged="txtupfront_TextChanged"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="15"
                                                                                                                Width="500px"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%;">Advance EMI %
                                                                                                </td>

                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtemiper" runat="server" BackColor="#e7e7e7" AutoPostBack="True" OnTextChanged="txtemiper_TextChanged"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="16"
                                                                                                                Width="500px"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="lllklk" visible="false" runat="server">
                                                                                                <td class="ControlLabel" style="width: 15%;">Advance EMI
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtemi" runat="server" BackColor="#e7e7e7" AutoPostBack="True" OnTextChanged="txtemi_TextChanged"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="17"
                                                                                                                Width="500px"></asp:TextBox>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </td>
                                                                                                <td class="ControlLabel" style="width: 15%;">Down Payment Sc 2
                                                                                                </td>

                                                                                                <td class="ControlTextBox3" style="width: 24%">
                                                                                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                                                                                        <ContentTemplate>
                                                                                                            <asp:TextBox ID="txtdown" runat="server" BackColor="#e7e7e7"
                                                                                                                CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="17"
                                                                                                                Width="500px"></asp:TextBox>
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
                                                                            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Installment Details">
                                                                                <HeaderTemplate>
                                                                                    <div>
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td><b>Installment Details</b> </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </HeaderTemplate>
                                                                                <ContentTemplate>
                                                                                    <table style="width: 770px;" align="center" cellpadding="1" cellspacing="2">
                                                                                        <tr style="height: 30px">
                                                                                            <td class="ControlLabel" style="width: 15%;" id="lllklkl" visible="false" runat="server">
                                                                                                <asp:CompareValidator ID="CompareValidator2" runat="server"
                                                                                                    ControlToValidate="drpBankName" Display="Dynamic"
                                                                                                    ErrorMessage="Please select Bank Name. It cannot be left blank." Operator="GreaterThan" Text="*"
                                                                                                    ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>
                                                                                                Bank Name *
                                                                                            </td>
                                                                                            <td class="ControlDrpBorder" style="width: 24%">
                                                                                                <asp:DropDownList ID="drpBankName" runat="server" AppendDataBoundItems="true" DataTextField="LedgerName" Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                    DataValueField="LedgerID" TabIndex="11" ValidationGroup="salesval">
                                                                                                    <asp:ListItem style="background-color: #e7e7e7" Text="Select Bank" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td class="ControlLabel" style="width: 25%;"></td>
                                                                                            <td style="width: 24%"></td>
                                                                                        </tr>
                                                                                        <tr style="height: 2px">
                                                                                        </tr>
                                                                                        <tr style="height: 30px" id="lllklkll" visible="false" runat="server">
                                                                                            <td class="ControlLabel" style="width: 15%;">
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                                                                                    ControlToValidate="txtAccountNumber" CssClass="lblFont" Display="Dynamic"
                                                                                                    ErrorMessage="Please enter Account Number. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                                                                                    TargetControlID="txtAccountNumber" ValidChars="+" />
                                                                                                Account Number *
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 24%">
                                                                                                <asp:TextBox ID="txtAccountNumber" runat="server" BackColor="#e7e7e7"
                                                                                                    CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="16"
                                                                                                    Width="500px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="ControlLabel" style="width: 25%;">
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                                                                                    ControlToValidate="txtBranchName" CssClass="lblFont" Display="Dynamic"
                                                                                                    ErrorMessage="Pleas enter Branch Name. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                Branch Name *
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 24%">
                                                                                                <asp:TextBox ID="txtBranchName" runat="server" BackColor="#e7e7e7"
                                                                                                    CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="16"
                                                                                                    Width="500px"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 2px">
                                                                                        </tr>
                                                                                        <tr style="height: 30px" id="lllklklll" visible="false" runat="server">
                                                                                            <td class="ControlLabel" style="width: 15%;">
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server"
                                                                                                    ControlToValidate="txtIFSCCode" CssClass="lblFont" Display="Dynamic"
                                                                                                    ErrorMessage="Please enter IFSC Code. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                IFSC Code *
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 24%">
                                                                                                <asp:TextBox ID="txtIFSCCode" runat="server" BackColor="#e7e7e7"
                                                                                                    CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="16"
                                                                                                    Width="500px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="ControlLabel" style="width: 25%;">
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server"
                                                                                                    ControlToValidate="txtpaydate" CssClass="lblFont" Display="Dynamic"
                                                                                                    ErrorMessage="Please select Day of Payment. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                Day of Payment *
                                                                                            </td>
                                                                                            <td class="ControlTextBox3" style="width: 24%">
                                                                                                <asp:TextBox ID="txtpaydate" Enabled="false" runat="server" BackColor="#e7e7e7"
                                                                                                    CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" TabIndex="16"
                                                                                                    Width="500px"></asp:TextBox>
                                                                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True"
                                                                                                    Format="dd/MM/yyyy"
                                                                                                    PopupButtonID="ImageButton12311" TargetControlID="txtpaydate">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:ImageButton ID="ImageButton12311" runat="server" CausesValidation="False"
                                                                                                    ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 3px">
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                                <table cellpadding="0" cellspacing="1"
                                                                                                    width="100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 100%">
                                                                                                            <div id="div" runat="server" style="height: 330px; overflow: scroll">
                                                                                                                <rwg:BulkEditGridView ID="GrdViewItems" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                                    BorderStyle="Solid" GridLines="Both" runat="server" CssClass="someClass"
                                                                                                                    Width="100%">
                                                                                                                    <RowStyle CssClass="dataRow" />
                                                                                                                    <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                                    <AlternatingRowStyle CssClass="altRow" />
                                                                                                                    <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                                    <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                                    <FooterStyle CssClass="dataRow" />
                                                                                                                    <Columns>
                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Cheque No" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="30px">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="txtChequeNo" runat="server" Width="95%" Height="26px" Text='<%# Bind("ChequeNo") %>'></asp:TextBox>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Due Date" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="30px">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="txtDueDate" runat="server" Width="95%" Height="26px" Text='<%# Bind("DueDate") %>'></asp:TextBox>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Amount" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="30px">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="txtAmount" runat="server" Width="95%" Height="26px" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Cancelled" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="30px">
                                                                                                                            <ItemTemplate>
                                                                                                                                <%--<asp:CheckBox ID="chkboxCancelled" runat="server" style="color: Black" Text="" Font-Names="arial" Font-Size="11px" Checked='<%# Bind("Cancelled") %>'>
                                                                                                                            </asp:CheckBox>--%>
                                                                                                                                <asp:DropDownList ID="chkboxCancelled" TabIndex="10" AutoPostBack="false" runat="server"
                                                                                                                                    Width="100%" Style="border: 1px solid #90c9fc" Height="29px" SelectedValue='<%# Bind("Cancelled") %>'>
                                                                                                                                    <asp:ListItem Text="NO" Value="N" Selected="True"></asp:ListItem>
                                                                                                                                    <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                                                                                                                                </asp:DropDownList>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Narration" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="10px">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:TextBox ID="txtRemarks" runat="server" Width="95%" Height="26px" Text='<%# Bind("Narration") %>'></asp:TextBox>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                    </Columns>
                                                                                                                </rwg:BulkEditGridView>
                                                                                                            </div>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <%--<tr style="height:5px">
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                                <asp:Panel ID="pnlPopup" BackColor="White" BorderStyle="Solid" BorderColor="White"
                                                                                                    Width="100%" runat="server">
                                                                                                    <div style="text-align: center;">
                                                                                                        <asp:GridView ID="GrdViewLeadContact" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowDeleting="GrdViewLeadContact_RowDeleting" OnSelectedIndexChanged="GrdViewLeadContact_SelectedIndexChanged"
                                                                                                            Width="100%" DataKeyNames="ContactRefID" AllowPaging="True" EmptyDataText="No Contacts found."  CssClass="someClass">
                                                                                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                                                                                            <Columns>
                                                                                                                <asp:BoundField DataField="ContactRefID" HeaderText="ContactRefID" ItemStyle-Width="20%" Visible="false"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                <asp:BoundField DataField="ContactedDate" HeaderText="Contacted Date" ItemStyle-Width="30%" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                <asp:BoundField DataField="ContactSummary" HeaderText="ContactSummary" ItemStyle-Width="60%"  HeaderStyle-BorderColor="Gray"/>
                                                                                                                <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-CssClass="command" HeaderStyle-BorderColor="Gray">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Select" SkinID="edit" CausesValidation="false" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-CssClass="command" HeaderStyle-BorderColor="Gray">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:ImageButton ID="lnkB" runat="Server" CommandName="Delete" SkinID="delete" OnClientClick="return confirm('Are you sure you want to delete this Lead Contact?');"  CausesValidation="false" />
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle Width="4%" />
                                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                                </asp:TemplateField>
                                                                                                            </Columns>
                                                                                                            <PagerTemplate>
                                                                                                                <table style=" border-color:white">
                                                                                                                    <tr style=" border-color:white; height:1px">
                                                                                                                    </tr>
                                                                                                                    <tr style=" border-color:white">
                                                                                                                        <td style=" border-color:white">
                                                                                                                            Goto Page
                                                                                                                        </td> 
                                                                                                                        <td style=" border-color:white">
                                                                                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" style="border:1px solid blue" AutoPostBack="true" SkinID="skinPagerDdlBox">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style=" border-color:white">
                                                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                ID="btnFirst" />
                                                                                                                        </td>
                                                                                                                        <td style=" border-color:white">
                                                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                ID="btnPrevious" />
                                                                                                                        </td>
                                                                                                                        <td style=" border-color:white">
                                                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                ID="btnNext" />
                                                                                                                        </td>
                                                                                                                        <td style=" border-color:white">
                                                                                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                ID="btnLast" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </PagerTemplate>
                                                                                                        </asp:GridView>
                                                                                                    </div>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="5">
                                                                                                <table  style="width: 100%">
                                                                                                    <tr>
                                                                                                        <td style="width: 80%">

                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                                                <ContentTemplate>
                                                                                                                    <asp:Button ID="lnkAddContact" Text="" CausesValidation="False" runat="server" CssClass="addContactbutton"
                                                                                                                        EnableTheming="false" SkinID="skinBtnUpload" Width="80px"  OnClick="lnkAddContact_Click"
                                                                                                                            />
                                                                                                                </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </td>
                                                                                        </tr>
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
                                                                                                                            <td>
                                                                                                                                <table class="headerPopUp" width="100%">
                                                                                                                                    <tr>
                                                                                                                                        <td>
                                                                                                                                            Hire Contact
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                        </tr>
                                                                                                                        <tr>
                                                                                                                            <td>
                                                                                                                                <table style="width: 100%; border: 1px solid #86b2d1" cellpadding="3" cellspacing="1">
                                                                                                                                    <tr style="height:5px">
                                                                                                                                        <td>
                                                                                                                                            <asp:HiddenField ID="hdCurrentRow" runat="server" Value="0" />
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 40%" class="ControlLabel">
                                                                                                                                            <asp:RequiredFieldValidator ID="rvDate" runat="server" ControlToValidate="txtContactedDate"
                                                                                                                                                ErrorMessage="Date is mandatory" Text="*" ValidationGroup="contact" />
                                                                                                                                        Contacted Date
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 20%" class="ControlTextBox3">
                                                                                                                                            <asp:TextBox ID="txtContactedDate" runat="server" CssClass="cssTextBox" ValidationGroup="contact" Width="70px" Height="23px" BackColor = "#90c9fc" ></asp:TextBox>
                                                                                                                                            <cc1:CalendarExtender ID="calContactedDate" runat="server" Format="dd/MM/yyyy"
                                                                                                                                                            PopupButtonID="ImageButton1" TargetControlID="txtContactedDate" Enabled="True">
                                                                                                                                            </cc1:CalendarExtender>
                                                                                                                                   
                                                                                                                                        </td>
                                                                                                                                        <td style="width:40%" align="left">
                                                                                                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                                                                    CausesValidation="False" Width="20px" runat="server" /> 
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>
                                                                                                                                        <td style="width: 40%" class="ControlLabel">
                                                                                                                                            Contact Summary
                                                                                                                                            <asp:RequiredFieldValidator ID="rvContactSummary" runat="server" ControlToValidate="txtContactSummary"
                                                                                                                                                ErrorMessage="Summary is mandatory" Text="*" ValidationGroup="contact" />
                                                                                                                                        </td> 
                                                                                                                                        <td style="width: 20%" class="ControlTextBox3">
                                                                                                                                            <asp:TextBox ID="txtContactSummary" runat="server" CssClass="cssTextBox" ValidationGroup="contact" Width="100%" Height="23px" BackColor = "#90c9fc" ></asp:TextBox>
                                                                                                                                        </td>
                                                                                                                                        <td style="width: 40%">
                                                                                                                                            
                                                                                                                                        </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr style="height:5px">
                                                                                                                                    </tr>
                                                                                                                                </table>
                                                                                                                            </td>
                                                                                                                            
                                                                                                                        </tr>
                                                                                                                        
                                                                                                                        <tr>
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
                                                                                                                                                            <asp:Button ID="cmdSaveContact" runat="server" CssClass="AddLeadContact" 
                                                                                                                                                                EnableTheming="false" OnClick="cmdSaveContact_Click" Text="" Height="45px" 
                                                                                                                                                                ValidationGroup="contact" />
                                                                                                                                                    
                                                                                                                                                            <asp:Button ID="cmdUpdateContact" runat="server" CssClass="UpdateLeadContact" 
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
                                                                                                                         </tr>
                                                                                                                    </table>
                                                                                                                </div>
                                                                                                            </asp:Panel>
                                                                                                        </ContentTemplate>
                                                                                                    </asp:UpdatePanel>
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>--%>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                        </cc1:TabContainer>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <table style="width: 100%" cellpadding="3" cellspacing="2">
                                                                            <tr>
                                                                                <td style="width: 32%"></td>
                                                                                <td style="width: 18%">
                                                                                    <asp:Button ID="UpdateButton" runat="server" SkinID="skinBtnSave" OnClientClick="javascript:CheckLeadContact();"
                                                                                        OnClick="UpdateButton_Click" CssClass="Updatebutton1231" ValidationGroup="salesval" EnableTheming="false"></asp:Button>
                                                                                    <asp:Button ID="AddButton" runat="server" SkinID="skinBtnSave" OnClientClick="javascript:CheckLeadContact()"
                                                                                        OnClick="AddButton_Click" CssClass="savebutton1231" ValidationGroup="salesval" EnableTheming="false"></asp:Button>
                                                                                </td>
                                                                                <td style="width: 18%">
                                                                                    <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                        OnClick="UpdateCancelButton_Click" SkinID="skinBtnCancel" CssClass="cancelbutton6"
                                                                                        EnableTheming="false"></asp:Button>
                                                                                </td>
                                                                                <td style="width: 32%" align="center"></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                                                                        <SelectParameters>
                                                                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
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
                                                                                </td>
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <table width="100%" style="margin: -4px 0px 0px 0px;">
                            <tr style="width: 100%">
                                <td>
                                    <div class="mainGridHold" id="searchGrid">
                                        <asp:GridView ID="GrdViewLead" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewLead_RowCreated" Width="100.4%"
                                            AllowPaging="True" DataKeyNames="slno" EmptyDataText="No Records found!"
                                            OnRowCommand="GrdViewLead_RowCommand" OnRowDataBound="GrdViewLead_RowDataBound" OnPageIndexChanging="GrdViewLead_PageIndexChanging"
                                            OnSelectedIndexChanged="GrdViewLead_SelectedIndexChanged" OnRowDeleting="GrdViewLead_RowDeleting"
                                            OnRowDeleted="GrdViewLead_RowDeleted" CssClass="someClass">
                                            <EmptyDataRowStyle CssClass="GrdContent" />
                                             <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="SlNo" HeaderText="SlNo" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="BranchRefNo" HeaderText="Ref No" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="BillNoNew" HeaderText="Bill No" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="BillDate" HeaderText="BillDate" HeaderStyle-BorderColor="Gray" DataFormatString="{0:dd/MM/yyyy}"/>
                                                <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="puramt" HeaderText="Cost" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="noinst" HeaderText="No of Inst" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="startdate" HeaderText="Start Due Date" HeaderStyle-BorderColor="Gray" DataFormatString="{0:dd/MM/yyyy}"/>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" SkinID="edit"
                                                            CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Hire Purchase Details?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete" CausesValidation="False"></asp:ImageButton>
                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server">
                                            </asp:ImageButton>
                                            <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("SlNo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white; height: 1px">
                                                    </tr>
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Width="65px" Style="border: 1px solid blue" Height="23px" BackColor="#e7e7e7" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="border-color: white; width: 5px"></td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CausesValidation="false" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnFirst" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CausesValidation="false" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnPrevious" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CausesValidation="false" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnNext" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CausesValidation="false" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnLast" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </PagerTemplate>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <%--<tr style="width:100%">
                    <td align="left">--%>
                <%--<asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListLeadMaster"
                            TypeName="LeadBusinessLogic">--%>
                <%--<SelectParameters>
                                <%--<asp:ControlParameter ControlID="GrdViewLead" Name="LeadId" PropertyName="SelectedValue"
                                    Type="Int32" />--%>
                <%--<asp:CookieParameter CookieName="Company" Type="String" Name="connection" />
                            </SelectParameters>--%>
                <%--   <SelectParameters>
                                <asp:CookieParameter Name="LeadID" CookieName="LeadID" Type="Int32" />
                            </SelectParameters>--%>
                <%--</asp:ObjectDataSource>
                    </td>--%>
                <%--</tr>--%>
                <tr>
                    <%--<td style="width: 100%">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="GetHireList"
                            TypeName="BusinessLogic" DeleteMethod="DeleteHirePurchase" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="Slno" Type="Int32" />
                                <asp:Parameter Name="usernam" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                    </td>--%>
                </tr>
            </table>
            <input type="hidden" id="hidAdvancedState" runat="server" />
            <asp:HiddenField ID="hdSMS" runat="server" Value="NO" />
            <asp:HiddenField ID="hdText" runat="server" />
            <asp:HiddenField ID="hdMobile" runat="server" />
            <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
            <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
            <asp:HiddenField ID="hdPendingCount" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <table style="width: 100%">
            <tr>
                <td style="width: 12%"></td>
                <td style="width: 14%">
                    <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6" OnClientClick="window.open('ReportExcelHirePurchase.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=210,width=500,left=425,top=220, scrollbars=yes');"
                        EnableTheming="false" CausesValidation="False"></asp:Button>
                </td>
                <td style="width: 14%">
                    <asp:Button ID="Button2" runat="server" CssClass="NewReport6" OnClientClick="window.open('HirePurchaseReport.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');"
                        EnableTheming="false" CausesValidation="False"></asp:Button>
                </td>
                <td style="width: 14%">
                    <asp:Button ID="Button3" runat="server" Text="Bulk Receipt ECS" OnClientClick="window.open('BulkReceiptECS.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=610,width=930,left=200,top=30, scrollbars=yes');"
                        SkinID="skinButtonCol2" Width="100%"></asp:Button>
                </td>
                <td style="width: 14%">
                    <asp:Button ID="Button4" runat="server" Text="Bulk Receipt Cheque" OnClientClick="window.open('BulkReceiptCheque.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=610,width=930,left=200,top=30, scrollbars=yes');"
                        SkinID="skinButtonCol2" Width="100%"></asp:Button>
                </td>
                <td style="width: 14%">
                    <asp:Button ID="Button5" runat="server" Text="Outstanding Report" OnClientClick="window.open('HireOutstandingReport.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=610,width=930,left=200,top=30, scrollbars=yes');"
                        SkinID="skinButtonCol2" Width="100%"></asp:Button>
                </td>
                <td style="width: 12%"></td>
            </tr>
        </table>
    </div>
</asp:Content>

