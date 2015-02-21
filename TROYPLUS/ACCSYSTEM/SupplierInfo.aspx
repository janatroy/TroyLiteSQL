<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="SupplierInfo.aspx.cs" Inherits="SupplierInfo" Title="Purchases > Supplier Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

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

        function Mobile_Validator() {
            var ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tabEdit_tabEditMain_txtMobile');

            if (ctrMobile == null)
                ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tablInsert_tabInsMain_txtMobileAdd');

            var txtMobile = ctrMobile.value;

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


        function Check() {
            var ctr = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tabEdit_tabEditMain_drpModeofContact');

            var ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tabEdit_tabEditMain_txtMobile');
            var ctrEmail = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tabEdit_tabEditMain_txtEmailId');
            var txt = ctr.value;
            var Mobile = ctrMobile.value;
            var Email = ctrEmail.value;

            if (txt == "1") {
                if (Mobile == "") {
                    alert("Enter Mobile Number");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
            else if (txt == "2") {
                if (Email == "") {
                    alert("Enter Email Id");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
            else {
                Page_IsValid = true;
            }
        }




        function CheckMode() {
            var ctr = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tablInsert_tabInsMain_drpModeofContactAdd');

            var ctrMobile = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tablInsert_tabInsMain_txtMobileAdd');
            var ctrEmail = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tablInsert_tabInsMain_txtEmailIdAdd');
            var txt = ctr.value;
            var Mobile = ctrMobile.value;
            var Email = ctrEmail.value;

            if (txt == "1") {
                if (Mobile == "") {
                    alert("Enter Mobile Number");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
            else if (txt == "2") {
                if (Email == "") {
                    alert("Enter Email Id");
                    Page_IsValid = false;
                    return window.event.returnValue = false;
                }
            }
            else {
                Page_IsValid = true;
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
                                            <span>Suppliers</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Suppliers
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -2px 0px 0px 1px;" cellpadding="3" cellspacing="2" class="searchbg">
                                <tr style="height: 25px; vertical-align: middle">
                                    <td style="width: 2%"></td>
                                    <td style="width: 16%; font-size: 22px; color: White;">Suppliers
                                    </td>
                                    <td style="width: 14%">
                                        <div style="text-align: right;">
                                       
                                        </div>
                                    </td>
                                    <td style="width: 13%; color: White;" align="right">Search
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%" class="NewBox">
                                        <div style="width: 160px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="154px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid White">
                                                <asp:ListItem Value="All">All</asp:ListItem>
                                                <asp:ListItem Value="LedgerName">Supplier Name</asp:ListItem>
                                                <asp:ListItem Value="AliasName">Alias Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 17%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" runat="server" Text="" CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" />
                                    </td>

                                    <td style="width: 20%" class="tblLeftNoPad">
                                        <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
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
                        <asp:Panel runat="server" ID="popUp" Style="width: 57%">
                            <div id="contentPopUp">
                                <table style="width: 100%;" align="center">
                                    <tr style="width: 100%">
                                        <td style="width: 100%">
                                            <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource" OnModeChanged="frmViewAdd_ModeChanged" OnDataBound="frmViewAdd_DataBound"
                                                DataKeyNames="LedgerID" OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit"
                                                OnItemCreated="frmViewAdd_ItemCreated" Visible="False" OnItemInserting="frmViewAdd_ItemInserting"
                                                OnItemUpdating="frmViewAdd_ItemUpdating" EmptyDataText="No Records" OnItemInserted="frmViewAdd_ItemInserted"
                                                OnItemUpdated="frmViewAdd_ItemUpdated">
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
                                                                <td colspan="5" class="headerPopUp">Supplier Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" align="left">
                                                                    <cc1:TabContainer ID="tabEdit" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabEditMain" runat="server" HeaderText="Supplier Details">
                                                                            <ContentTemplate>
                                                                                <table style="width: 770px; vertical-align: text-top; border: 0px solid #86b2d1;"
                                                                                    align="center" cellspacing="2" cellpadding="3">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Supplier Name *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrName" runat="server" ControlToValidate="txtLdgrName"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="LedgerName is mandatory">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtLdgrName" runat="server" Text='<%# Bind("LedgerName") %>' SkinID="skinTxtBoxGrid"
                                                                                                TabIndex="1"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Alias Name
                                                                    <%--<asp:RequiredFieldValidator ID="rvAliasName" runat="server" ControlToValidate="txtAliasName"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Alias is mandatory">*</asp:RequiredFieldValidator>--%>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAliasName" runat="server" Text='<%# Bind("AliasName") %>' SkinID="skinTxtBoxGrid"
                                                                                                TabIndex="2"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Contact Person Name
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtContName" runat="server" Text='<%# Bind("ContactName") %>' SkinID="skinTxtBoxGrid"
                                                                                                TabIndex="3"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Opening Balance *
                                                                    <asp:CompareValidator ID="cvOpenBal" runat="server" ControlToValidate="txtOpenBal"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Open Balance should be numeric value"
                                                                        Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator>
                                                                                            <asp:RequiredFieldValidator ID="rvOpenBal" runat="server" ControlToValidate="txtOpenBal"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Open Balance is mandatory">*</asp:RequiredFieldValidator>
                                                                                            <cc1:FilteredTextBoxExtender ID="OBvalid" runat="server" FilterType="Numbers" TargetControlID="txtOpenBal" />
                                                                                        </td>
                                                                                        <td class="ControlNumberBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtOpenBal" runat="server" Text='<%# Bind("OpenBalance") %>' CssClass="cssTextBox"
                                                                                                Width="92%" TabIndex="5"></asp:TextBox>
                                                                                            <asp:DropDownList ID="ddCRDR" runat="server" Width="68px" SelectedValue='<%# Bind("DRORCR") %>' CssClass="drpDownListMedium" Style="border: 1px solid Gray" Height="26px" BackColor="#e7e7e7"
                                                                                                TabIndex="5">
                                                                                                <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                                                                                                <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 1
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd1" runat="server" Text='<%# Bind("Add1") %>' SkinID="skinTxtBoxGrid"
                                                                                                TabIndex="6"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Phone No.
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
                                                                        TargetControlID="txtPhone" ValidChars="+" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>' SkinID="skinTxtBoxGrid"
                                                                                                TabIndex="9"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 2
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd2" runat="server" Text='<%# Bind("Add2") %>' SkinID="skinTxtBoxGrid"
                                                                                                TabIndex="7"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTin"
                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="TIN No is mandatory"></asp:RequiredFieldValidator>
                                                                                            TIN No. *
                                                                    
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtTin" runat="server" Text='<%# Bind("TINnumber") %>' SkinID="skinTxtBoxGrid"
                                                                                                TabIndex="11"></asp:TextBox>

                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 3
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd3" runat="server" Text='<%# Bind("Add3") %>' SkinID="skinTxtBoxGrid"
                                                                                                TabIndex="8"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Mode of Contact *
                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="drpModeofContact"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Mode of Contact is mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0" />
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 28%">
                                                                                            <asp:DropDownList ID="drpModeofContact" TabIndex="10" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                runat="server" OnDataBound="drpModeofContact_DataBound" AutoPostBack="false" SelectedValue='<%# Bind("ModeofContact") %>'>
                                                                                                <asp:ListItem Text="Select Mode of Contact" Value="0"></asp:ListItem>
                                                                                                <asp:ListItem Text="SMS" Value="1"></asp:ListItem>
                                                                                                <asp:ListItem Text="Email" Value="2"></asp:ListItem>
                                                                                                <asp:ListItem Text="Paper" Value="3"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">EmailId
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtEmailId" TabIndex="8" runat="server" Text='<%# Bind("EmailId") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Mobile
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender123" runat="server" FilterType="Custom, Numbers"
                                                                        TargetControlID="txtMobile" ValidChars="+" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtMobile" MaxLength="10" runat="server" Text='<%# Bind("Mobile") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="10"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">OB Due Date    
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtdueDate" Enabled="false" MaxLength="10" TabIndex="13" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("OpDueDate") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calBillDate1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnBillDate1" TargetControlID="txtdueDate">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                        <td style="width: 14%;" align="left">
                                                                                            <asp:ImageButton ID="btnBillDate1" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                        </td>
                                                                                        <td style="width: 28%"></td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddAccGroup" DataSourceID="srcGroupInfo" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                runat="server" SelectedValue='<%# Bind("GroupID") %>' DataTextField="GroupName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                DataValueField="GroupID" Width="98%" AppendDataBoundItems="True" Visible="False">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                        <cc1:TabPanel ID="tabEditAddTab" runat="server" HeaderText="Additional Details">
                                                                            <ContentTemplate>
                                                                                <table align="center" cellpadding="3" cellspacing="1" style="border: 0px solid #5078B3; width: 770px;">
                                                                                    <tr style="height: 5px">
                                                                                        <td class="ControlLabel" style="width: 20%">Internal Transfer
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpIntTrans" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpIntTrans_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("Inttrans") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">
                                                                                            <%--Payment Source--%>
                                                                                Is Active
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 28%">
                                                                                            <asp:DropDownList ID="drpPaymentmade" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpPaymentmade_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" Visible="false" SelectedValue='<%# Bind("Paymentmade") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <asp:DropDownList ID="drpunuse" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpunuse_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("UnUse") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">DC
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpdc" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpdc_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("Inttrans") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Name on Cheque
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtChequeName" TabIndex="15" runat="server" Width="150%" BackColor="#e7e7e7" Text='<%# Bind("ChequeName") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr runat="server" visible="false">
                                                                                        <td class="ControlLabel" style="width: 20%">
                                                                                            <%--Un Use--%>
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder"></td>
                                                                                        <td class="ControlLabel" style="width: 14%"></td>
                                                                                        <td style="width: 28%"></td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>

                                                                    </cc1:TabContainer>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 6px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 32%;"></td>
                                                                            <td style="width: 19%;">
                                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                    CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClientClick="Mobile_Validator();Check();"
                                                                                    OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 19%;">
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
                                                                <asp:ObjectDataSource ID="srcGroupInfo" runat="server" SelectMethod="ListGroupInfoSupp"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                                <td>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ShowSummary="false" HeaderText="Validation Messages" Font-Names="Trebuchet MS"
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
                                                                <td colspan="5" class="headerPopUp">Supplier Information
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" align="left">
                                                                    <cc1:TabContainer ID="tablInsert" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabInsMain" runat="server" HeaderText="Suppier Details">
                                                                            <ContentTemplate>
                                                                                <table style="width: 770px; border: 0px solid #86b2d1; vertical-align: text-top" align="center" cellpadding="3"
                                                                                    cellspacing="1">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Supplier Name *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrNameAdd" runat="server" ControlToValidate="txtLdgrNameAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Name is mandatory"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtLdgrNameAdd" runat="server" Text='<%# Bind("LedgerName") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="1"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Alias Name
                                                                    <%--<asp:RequiredFieldValidator ID="rvAliasNameAdd" runat="server" ControlToValidate="txtAliasNameAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Alias is mandatory">*</asp:RequiredFieldValidator>--%>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAliasNameAdd" runat="server" Text='<%# Bind("AliasName") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="2"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Contact Person Name
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtContNameAdd" runat="server" Text='<%# Bind("ContactName") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="3"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Opening Balance *
                                                                    <asp:CompareValidator ID="cvOpenBalAdd" runat="server" ControlToValidate="txtOpenBalAdd"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Open Balance should be numeric value"
                                                                        Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator>
                                                                                            <asp:RequiredFieldValidator ID="rvOpenBalAdd" runat="server" ControlToValidate="txtOpenBalAdd"
                                                                                                Display="Dynamic" EnableClientScript="True" ErrorMessage="Open Balance is mandatory">*</asp:RequiredFieldValidator>
                                                                                            <cc1:FilteredTextBoxExtender ID="OBvalidAdd" runat="server" FilterType="Numbers"
                                                                                                TargetControlID="txtOpenBalAdd" />
                                                                                        </td>
                                                                                        <td class="ControlNumberBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtOpenBalAdd" runat="server" Text="0"
                                                                                                CssClass="cssTextBox" Width="92%" TabIndex="4"></asp:TextBox>
                                                                                            <asp:DropDownList ID="ddCRDRAdd" runat="server" Width="68px" SelectedValue='<%# Bind("DRORCR") %>' Style="border: 1px solid Gray" Height="26px" CssClass="drpDownListMedium" BackColor="#e7e7e7" TabIndex="5">
                                                                                                <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                                                                                                <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 1
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd1Add" runat="server" Text='<%# Bind("Add1") %>' SkinID="skinTxtBoxGrid" TabIndex="6"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Phone No.
                                                                    <cc1:FilteredTextBoxExtender ID="FTBoxE5Add" runat="server" FilterType="Custom,Numbers"
                                                                        TargetControlID="txtPhoneAdd" ValidChars="+" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtPhoneAdd" runat="server" Text='<%# Bind("Phone") %>' SkinID="skinTxtBoxGrid" TabIndex="9"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 2
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd2Add" runat="server" Text='<%# Bind("Add2") %>' SkinID="skinTxtBoxGrid" TabIndex="7"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">
                                                                                            <asp:RequiredFieldValidator ID="hRequiredFieldValidator1" runat="server" ControlToValidate="txtTinAdd"
                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="TIN No is mandatory"></asp:RequiredFieldValidator>
                                                                                            TIN No.
                                                                    
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtTinAdd" runat="server" Text='<%# Bind("TINnumber") %>' SkinID="skinTxtBoxGrid" TabIndex="11"></asp:TextBox>

                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 3
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd3Add" runat="server" Text='<%# Bind("Add3") %>' SkinID="skinTxtBoxGrid" TabIndex="8"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Mode of Contact *
                                                                    <asp:CompareValidator ID="CompareValidator213" runat="server" ControlToValidate="drpModeofContactAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Mode of Contact is mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0" />
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 28%">
                                                                                            <asp:DropDownList ID="drpModeofContactAdd" TabIndex="10" Width="100%" AutoPostBack="false" OnDataBound="drpModeofContactAdd_DataBound" CssClass="drpDownListMedium" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                runat="server" SelectedValue='<%# Bind("ModeofContact") %>'>
                                                                                                <%--<asp:ListItem Text="Select Mode of Contact" Value="0"></asp:ListItem>--%>
                                                                                                <asp:ListItem Text="SMS" Value="1"></asp:ListItem>
                                                                                                <asp:ListItem Text="Email" Value="2"></asp:ListItem>
                                                                                                <asp:ListItem Text="Paper" Value="3" Selected="True"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Email Id
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtEmailIdAdd" MaxLength="10" TabIndex="13" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("EmailId") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Mobile
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                                                        TargetControlID="txtMobileAdd" ValidChars="+" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtMobileAdd" MaxLength="10" runat="server" Text='<%# Bind("Mobile") %>'
                                                                                                SkinID="skinTxtBoxGrid" TabIndex="10"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>


                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">OB Due Date    
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtdueDateadd" Enabled="false" MaxLength="10" TabIndex="13" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("OpDueDate") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calBillDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnBillDate" TargetControlID="txtdueDateadd">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                        <td style="width: 14%;" align="left">
                                                                                            <asp:ImageButton ID="btnBillDate" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                        </td>
                                                                                        <td style="width: 28%"></td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddAccGroupAdd" DataSourceID="srcGroupInfoAdd" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                runat="server" SelectedValue='<%# Bind("GroupID") %>' DataTextField="GroupName"
                                                                                                DataValueField="GroupID" Width="98%" AppendDataBoundItems="True" Visible="False">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                        <cc1:TabPanel ID="tabInsAddTab" runat="server" HeaderText="Additional Details">
                                                                            <ContentTemplate>
                                                                                <table align="center" cellpadding="3" cellspacing="5" style="border: 0px solid #5078B3; width: 770px;">
                                                                                    <tr style="height: 5px">
                                                                                        <td class="ControlLabel" style="width: 20%">Internal Transfer
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpIntTransAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpIntTransAdd_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("Inttrans") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">
                                                                                            <%--Payment Source--%>
                                                                                Is Active
                                                                                        </td>
                                                                                        <td style="width: 28%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpPaymentmadeAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpPaymentmadeAdd_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" Visible="false" SelectedValue='<%# Bind("Paymentmade") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            <asp:DropDownList ID="drpunuseAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpunuseAdd_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("unuse") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES" Selected="True"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>

                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">DC
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpdcAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpdcAdd_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("dc") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Name on Cheque 
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtChequeNameAdd" TabIndex="15" runat="server" Width="150%" BackColor="#e7e7e7" Text='<%# Bind("ChequeName") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr runat="server" visible="false">
                                                                                        <td class="ControlLabel" style="width: 20%">
                                                                                            <%--Un Use--%>
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder"></td>
                                                                                        <td class="ControlLabel" style="width: 14%"></td>
                                                                                        <td style="width: 28%"></td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                    </cc1:TabContainer>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 6px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="right" style="width: 33%;"></td>
                                                                            <td style="width: 19%;">
                                                                                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClientClick="Mobile_Validator();CheckMode();"
                                                                                    OnClick="InsertButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 19%;">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 29%;"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <%--<tr style="height:2px">
                                                            </tr>--%>
                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td style="width: 50%" colspan="2">
                                                                <asp:ValidationSummary ID="valSumAdd" DisplayMode="BulletList" ShowMessageBox="true"
                                                                    ShowSummary="false" HeaderText="Validation Messages" Font-Names="Trebuchet MS"
                                                                    Font-Size="12" runat="server" />
                                                            </td>
                                                            <td style="width: 50%" colspan="2">
                                                                <asp:ObjectDataSource ID="srcGroupInfoAdd" runat="server" SelectMethod="ListGroupInfoSupp"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                            </td>
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
                                        <asp:GridView ID="GrdViewLedger" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            OnRowCreated="GrdViewLedger_RowCreated" Width="100.4%" DataSourceID="GridSource"
                                            AllowPaging="True" DataKeyNames="LedgerID" EmptyDataText="No Supplier Data Found."
                                            OnRowCommand="GrdViewLedger_RowCommand" OnRowDataBound="GrdViewLedger_RowDataBound" OnRowDeleting="GrdViewLedger_RowDeleting"
                                            OnRowDeleted="GrdViewLedger_RowDeleted" Font-Names="Trebuchet MS" CssClass="someClass">
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="LedgerName" HeaderText="Supplier" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="AliasName" HeaderText="Alias Name" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="GroupName" HeaderText="Group Name" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField HeaderText="Opening Balance" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                                        <asp:HiddenField ID="DRORCR" runat="server" Value='<%# Bind("DRORCR") %>' />
                                                        <asp:HiddenField ID="OpenBalance" runat="server" Value='<%# Bind("OpenBalance") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Phone" HeaderText="Phone No." HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="LedgerCategory" HeaderText="Category" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="TinNumber" HeaderText="TIN#" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="command" Width="50px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray"
                                                    HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Supplier Details?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("LedgerID") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px"></HeaderStyle>
                                                    <ItemStyle CssClass="command"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" Style="border: 1px solid blue" BackColor="#e7e7e7" Width="75px" Height="25px">
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
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListSupplierInfo"
                            TypeName="BusinessLogic" DeleteMethod="DeleteLedger" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="LedgerID" Type="Int32" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="empSrc" runat="server" SelectMethod="ListExecutive" TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="empSrcAdd" runat="server" SelectMethod="ListExecutive"
                            TypeName="BusinessLogic">
                            <SelectParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetLedgerInfoForId"
                            TypeName="BusinessLogic" OnUpdating="frmSource_Updating" OnInserting="frmSource_Inserting"
                            InsertMethod="InsertLedgerInfo" UpdateMethod="UpdateLedgerInfo">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="LedgerID" Type="Int32" />
                                <asp:Parameter Name="LedgerName" Type="String" />
                                <asp:Parameter Name="AliasName" Type="String" />
                                <asp:Parameter Name="GroupID" Type="Int32" />
                                <asp:Parameter Name="OpenBalanceDR" Type="Double" />
                                <asp:Parameter Name="OpenBalanceCR" Type="Double" />
                                <asp:Parameter Name="OpenBalance" Type="Double" />
                                <asp:Parameter Name="ContactName" Type="String" />
                                <asp:Parameter Name="DRORCR" Type="String" />
                                <asp:Parameter Name="Add1" Type="String" />
                                <asp:Parameter Name="Add2" Type="String" />
                                <asp:Parameter Name="Add3" Type="String" />
                                <asp:Parameter Name="Phone" Type="String" />
                                <asp:Parameter Name="LedgerCategory" Type="String" />
                                <asp:Parameter Name="ExecutiveIncharge" Type="Int32" />
                                <asp:Parameter Name="TinNumber" Type="String" />
                                <asp:Parameter Name="Mobile" Type="String" />
                                <asp:Parameter Name="Inttrans" Type="String" />
                                <asp:Parameter Name="Paymentmade" Type="String" />
                                <asp:Parameter Name="dc" Type="String" />
                                <asp:Parameter Name="ChequeName" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="unuse" Type="String" />
                                <asp:Parameter Name="EmailId" Type="String" />
                                <asp:Parameter Name="ModeofContact" Type="Int32" />
                                <asp:Parameter Name="OpDueDate" Type="string" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewLedger" Name="LedgerID" PropertyName="SelectedValue"
                                    Type="String" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="LedgerName" Type="String" />
                                <asp:Parameter Name="AliasName" Type="String" />
                                <asp:Parameter Name="GroupID" Type="Int32" />
                                <asp:Parameter Name="OpenBalanceDR" Type="Double" />
                                <asp:Parameter Name="OpenBalanceCR" Type="Double" />
                                <asp:Parameter Name="OpenBalance" Type="Double" />
                                <asp:Parameter Name="ContactName" Type="String" />
                                <asp:Parameter Name="DRORCR" Type="String" />
                                <asp:Parameter Name="Add1" Type="String" />
                                <asp:Parameter Name="Add2" Type="String" />
                                <asp:Parameter Name="Add3" Type="String" />
                                <asp:Parameter Name="Phone" Type="String" />
                                <asp:Parameter Name="LedgerCategory" Type="String" />
                                <asp:Parameter Name="ExecutiveIncharge" Type="Int32" />
                                <asp:Parameter Name="TinNumber" Type="String" />
                                <asp:Parameter Name="Mobile" Type="String" />
                                <asp:Parameter Name="Inttrans" Type="String" />
                                <asp:Parameter Name="Paymentmade" Type="String" />
                                <asp:Parameter Name="dc" Type="String" />
                                <asp:Parameter Name="ChequeName" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="unuse" Type="String" />
                                <asp:Parameter Name="EmailId" Type="String" />
                                <asp:Parameter Name="ModeofContact" Type="Int32" />
                                <asp:Parameter Name="OpDueDate" Type="string" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 20%"></td>
                                <td style="width: 15%">
                                         <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                    EnableTheming="false" Width="80px" Text=""></asp:Button>
                                            </asp:Panel>
                                </td>
                                <td style="width: 15%">
                                    <asp:Button ID="BlkAdd" runat="server" OnClientClick="window.open('BulkAdditionSupplier.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');" CssClass="bulkaddition"
                                        EnableTheming="false" Text=""></asp:Button>
                                </td>
                                <td style="width: 15%">
                                    <asp:Button ID="Button2" runat="server" CssClass="exportexl6" OnClientClick="window.open('ReportExcelSuppliers.aspx ','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=230,width=500,left=425,top=220, scrollbars=yes');"
                                        EnableTheming="false" Width="120px"></asp:Button>
                                </td>
                                <td style="width: 35%"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
