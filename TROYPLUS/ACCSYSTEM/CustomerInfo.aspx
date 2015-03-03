<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="CustomerInfo.aspx.cs" Inherits="CustomerInfo" Title="sales > Customer Information" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
    <%--<link href="App_Themes/NewTheme/Selectbox.css" rel="stylesheet" type="text/css" />
	<script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js" 
            type="text/javascript" charset="utf-8"></script>
	<script type="text/javascript" src="Scripts/jquery.selectbox-0.5.js"></script>--%>
    <%--<script type="text/javascript">
	    	    $(document).ready(function () {
	    $('#drpLedgerCat').selectbox();
	    	    });
	</script>--%>

    <%--    <script type="text/javascript">
        $(document).ready(function () {
        	 $('#drpLedgerCat').selectbox();
        });
    </script>--%>
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


        //        protected void Page_Load(object sender, System.EventArgs e)
        //        {
        //            drpLedgerCatAdd.BorderWidth = 2;
        //        }
        //        protected void lnkBtnAdd_Click(object sender, System.EventArgs e)
        //        {
        //            drpLedgerCatAdd.BorderColor = System.Drawing.Color.SteelBlue;
        //            drpLedgerCatAdd.BackColor = System.Drawing.Color.SteelBlue;
        //        }

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
            <table style="width: 100%;">
                <tr style="width: 100%">
                    <td style="width: 100%">

                        <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Customers</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Customers
                                    </td>
                                </tr>
                            </table>--%>
                        <div class="mainConBody">
                            <table style="width: 99.8%; margin: -1px 0px 0px 1px;" cellpadding="0" cellspacing="2px" class="searchbg">
                                <tr>
                                    <td style="width: 3%"></td>
                                    <td style="width: 15%; font-size: 22px; color: White;">Customers
                                    </td>
                                    <td style="width: 14%">
                                        
                                    </td>
                                    <td style="width: 10%; color: White;" align="right">Search
                                    </td>
                                    <td style="width: 19%" class="NewBox">
                                        <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                    </td>
                                    <td style="width: 18%" class="NewBox">
                                        <div style="width: 150px; font-family: 'Trebuchet MS';">
                                            <asp:DropDownList ID="ddCriteria" runat="server" Width="157px" BackColor="White" Height="23px" Style="text-align: center; border: 1px solid White">
                                                <asp:ListItem Value="0">All</asp:ListItem>
                                                <asp:ListItem Value="LedgerName">Customer Name</asp:ListItem>
                                                <asp:ListItem Value="AliasName">Alias Name</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 14%" class="tblLeftNoPad">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch6"
                                            EnableTheming="false" ForeColor="White" />
                                    </td>
                                    <td style="width: 17%" class="tblLeftNoPad">

                                        <%--<asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />--%>

                                        <asp:Button ID="BtnClearFilter" runat="server" onkeyup="EnableDisableButton(this,'BtnClearFilter')" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />

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
                        <asp:Panel runat="server" ID="popUp" Style="width: 57%;">
                            <%--<asp:UpdatePanel ID="updatePnlSales" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                            <div id="contentPopUp">
                                <table align="center" style="width: 100%" class="tblLeft">
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
                                                                <td colspan="5">
                                                                    <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                        <tr>
                                                                            <td>Customer Information
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" align="left">
                                                                    <cc1:TabContainer ID="tabEdit" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabEditMain" runat="server" HeaderText="Customer Details">
                                                                            <ContentTemplate>
                                                                                <table style="width: 770px; vertical-align: text-top; border: 0px solid #86b2d1;"
                                                                                    align="center" cellspacing="2" cellpadding="3">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%;">Customer / Dealer Name *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrName" runat="server" ControlToValidate="txtLdgrName"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="CustomerName is mandatory">*</asp:RequiredFieldValidator>
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
                                                                                            <asp:TextBox ID="txtAliasName" TabIndex="2" runat="server" Text='<%# Bind("AliasName") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Contact Person Name
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtContName" TabIndex="3" runat="server" Text='<%# Bind("ContactName") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
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
                                                                                            <asp:TextBox ID="txtOpenBal" TabIndex="4" runat="server" Text='<%# Bind("OpenBalance") %>'
                                                                                                CssClass="cssTextBox" Width="100%"></asp:TextBox>
                                                                                            <asp:DropDownList ID="ddCRDR" EnableTheming="false" TabIndex="5" runat="server" Style="border: 1px solid Gray" Height="26px" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                Width="68px" SelectedValue='<%# Bind("DRORCR") %>'>
                                                                                                <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                                                                                                <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td align="left" style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 1
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd1" TabIndex="6" runat="server" Text='<%# Bind("Add1") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Phone No.
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
                                                                        TargetControlID="txtPhone" ValidChars="+" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtPhone" TabIndex="9" runat="server" Text='<%# Bind("Phone") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 2
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd2" TabIndex="7" runat="server" Text='<%# Bind("Add2") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Category *
                                                                    <asp:CompareValidator ID="cmpLedCat" runat="server" ControlToValidate="drpLedgerCat"
                                                                        Display="Dynamic" EnableClientScript="True" ErrorMessage="Category is Mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0">*</asp:CompareValidator>
                                                                                        </td>
                                                                                        <td style="width: 28%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpLedgerCat" ClientIDMode="Static" TabIndex="10" Width="100%" DataTextField="CusCategory_Name" DataValueField="CusCategory_Value" DataSourceID="srccuscat" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                runat="server" OnDataBound="drpLedgerCat_DataBound">
                                                                                                <asp:ListItem Text="Select Category" Value="0" style="background-color: #e7e7e7"></asp:ListItem>
                                                                                                <%--<asp:ListItem Text="Dealer" Value="Dealer"></asp:ListItem>
                                                                        <asp:ListItem Text="Customer" Value="Customer"></asp:ListItem>
                                                                        <asp:ListItem Text="Others" Value="Others"></asp:ListItem>--%>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 3
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd3" TabIndex="8" runat="server" Text='<%# Bind("Add3") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Mode of Contact *
                                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="drpModeofContact"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Mode of Contact is mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0" />
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 28%">
                                                                                            <asp:DropDownList ID="drpModeofContact" TabIndex="10" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                runat="server" OnDataBound="drpModeofContact_DataBound" AutoPostBack="false" SelectedValue='<%# Bind("ModeofContact") %>'>
                                                                                                <asp:ListItem Text="Select Mode of Contact" Value="0" Selected="True"></asp:ListItem>
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
                                                                                        <td class="ControlLabel" style="width: 20%">Email Id
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtEmailId" TabIndex="8" runat="server" Text='<%# Bind("EmailId") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Mobile
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                                                        TargetControlID="txtMobile" ValidChars="+" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtMobile" MaxLength="10" TabIndex="13" runat="server" Text='<%# Bind("Mobile") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">OB Due Date    
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtdueDate" MaxLength="10" TabIndex="13" Enabled="false" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("OpDueDate") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calBillDate1" runat="server" Format="dd/MM/yyyy" PopupButtonID="btnBillDate1" TargetControlID="txtdueDate">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>
                                                                                        <td style="width: 14%;" align="left">
                                                                                            <asp:ImageButton ID="btnBillDate1" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                        </td>

                                                                                        <td style="width: 28%">
                                                                                            <asp:UpdatePanel ID="UpdatePanel123456" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
                                                                                            <asp:DropDownList ID="drpBranch" TabIndex="10" Width="100%" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                runat="server">
                                                                                                <%--<asp:ListItem style="background-color: #e7e7e7" Text="Select Branch" Value="0"></asp:ListItem>--%>
                                                                                           
                                                                                            </asp:DropDownList>
            </ContentTemplate>
            </asp:UpdatePanel>
                                                                                        </td>

                                                                                        <td style="width: 28%"></td>

                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td></td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddAccGroup" DataSourceID="srcGroupInfo" Height="30px" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                runat="server" SelectedValue='<%# Bind("GroupID") %>' DataTextField="GroupName"
                                                                                                DataValueField="GroupID" Width="98%" AppendDataBoundItems="True" Visible="False">
                                                                                                <%--<asp:ListItem style="background-color: #90c9fc" Selected="True" Value="0">Select Account Group</asp:ListItem>--%>
                                                                                            </asp:DropDownList>
                                                                                    </tr>

                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                        <cc1:TabPanel ID="tabEditAddTab" runat="server" HeaderText="Additional Details">
                                                                            <ContentTemplate>
                                                                                <table align="center" cellpadding="3" cellspacing="1" style="border: 0px solid #5078B3; width: 770px;">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">TIN No.
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtTin" TabIndex="12" runat="server" Text='<%# Bind("TINnumber") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Executive Incharge
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 28%">
                                                                                            <asp:DropDownList ID="drpIncharge" Width="100%" TabIndex="11" EnableTheming="false" Style="border: 1px solid #e7e7e7" Height="26px" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                AppendDataBoundItems="true" runat="server" DataSourceID="empSrc"
                                                                                                DataTextField="empFirstName" DataValueField="empno" OnDataBound="drpIncharge_DataBound">
                                                                                                <asp:ListItem Text="Select Executive" Value="0"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>

                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Credit Limit
                                                                                <cc1:FilteredTextBoxExtender ID="FTextBoxExtenAdd1" runat="server" FilterType="Custom,Numbers"
                                                                                    ValidChars="." TargetControlID="txtCreditLimit" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtCreditLimit" TabIndex="14" runat="server" Text='<%# Bind("CreditLimit") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Credit Limit Days
                                                                                <cc1:FilteredTextBoxExtender ID="FTextBoxExtender1" runat="server" FilterType="Custom,Numbers"
                                                                                    ValidChars="." TargetControlID="txtCredtLimitDays" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtCredtLimitDays" TabIndex="15" runat="server" Text='<%# Bind("CreditDays") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Internal Transfer
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpIntTrans" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpIntTrans_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("Inttrans") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Payment Source
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 28%">
                                                                                            <asp:DropDownList ID="drpPaymentmade" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpPaymentmade_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("Paymentmade") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
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
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("dc") %>'>
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
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">
                                                                                            <%--Un Use--%>
                                                                                Is Active
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpunuse" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpunuse_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("UnUse") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES" Selected="True"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
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
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td style="width: 35%"></td>
                                                                            <td style="width: 18%">
                                                                                <asp:Button ID="UpdateButton" runat="server" TabIndex="17" CausesValidation="True"
                                                                                    CommandName="Update" CssClass="Updatebutton1231" EnableTheming="false" SkinID="skinBtnSave"
                                                                                    OnClientClick="Mobile_Validator();Check();" OnClick="UpdateButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 18%">
                                                                                <asp:Button ID="UpdateCancelButton" TabIndex="16" runat="server" CausesValidation="False"
                                                                                    CommandName="Cancel" CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel"
                                                                                    OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 25%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </div>
                                                    <table cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:ObjectDataSource ID="srcGroupInfo" runat="server" SelectMethod="ListGroupInfoCust"
                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                    <SelectParameters>
                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                    </SelectParameters>
                                                                </asp:ObjectDataSource>
                                                                <td>
                                                                    <asp:ObjectDataSource ID="srccuscat" runat="server" SelectMethod="ListCusCategory"
                                                                        TypeName="BusinessLogic">
                                                                        <SelectParameters>
                                                                            <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                        </SelectParameters>
                                                                    </asp:ObjectDataSource>
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
                                                                <td colspan="5">
                                                                    <table class="headerPopUp" style="border: 1px solid #86b2d1" width="100%">
                                                                        <tr>
                                                                            <td>Customer Information
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 5px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" align="left">
                                                                    <cc1:TabContainer ID="tablInsert" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                        <cc1:TabPanel ID="tabInsMain" runat="server" HeaderText="Customer Details">
                                                                            <ContentTemplate>
                                                                                <table style="width: 770px; border: 0px solid #86b2d1; vertical-align: text-top" align="center" cellpadding="3"
                                                                                    cellspacing="1">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Customer / Dealer Name *
                                                                    <asp:RequiredFieldValidator ID="rvLdgrNameAdd" runat="server" ControlToValidate="txtLdgrNameAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Customer / Dealer Name is mandatory" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtLdgrNameAdd" TabIndex="1" runat="server" BackColor="#e7e7e7" Text='<%# Bind("LedgerName") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">
                                                                                            <%--<asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="CheckBox13" runat="server" OnCheckedChanged="CheckBox13_CheckedChanged"  Font-Size="12px" AutoPostBack="true"/>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="CheckBox13" EventName="CheckedChanged" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>--%>
                                                                    Alias Name 
                                                                    <%--<asp:RequiredFieldValidator ID="rvAliasNameAdd" runat="server" ControlToValidate="txtAliasNameAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Alias Name is mandatory" />--%>
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAliasNameAdd" TabIndex="2" runat="server" BackColor="#e7e7e7" Text='<%# Bind("AliasName") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Contact Person Name
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtContNameAdd" TabIndex="3" runat="server" BackColor="#e7e7e7" Text='<%# Bind("ContactName") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Opening Balance *
                                                                    <asp:CompareValidator ID="cvOpenBalAdd" runat="server" ControlToValidate="txtOpenBalAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Opening Balance should be numeric"
                                                                        Operator="DataTypeCheck" Type="Double" />
                                                                                            <asp:RequiredFieldValidator ID="rvOpenBalAdd" runat="server" ControlToValidate="txtOpenBalAdd"
                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Opening Balance is mandatory" />
                                                                                            <cc1:FilteredTextBoxExtender ID="OBvalidAdd" runat="server" FilterType="Numbers"
                                                                                                TargetControlID="txtOpenBalAdd" />
                                                                                        </td>
                                                                                        <td class="ControlNumberBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtOpenBalAdd" TabIndex="4" runat="server" BackColor="#e7e7e7" Text="0"
                                                                                                CssClass="cssTextBox" Width="100%"></asp:TextBox>
                                                                                            <asp:DropDownList ID="ddCRDRAdd" EnableTheming="false" TabIndex="5" runat="server" Style="border: 1px solid Gray" Height="26px" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                Width="68px" SelectedValue='<%# Bind("DRORCR") %>'>
                                                                                                <asp:ListItem Text="CR" Value="CR"></asp:ListItem>
                                                                                                <asp:ListItem Text="DR" Value="DR"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td align="left" style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 1
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd1Add" TabIndex="6" runat="server" BackColor="#e7e7e7" Text='<%# Bind("Add1") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Phone No.
                                                                    <cc1:FilteredTextBoxExtender ID="FTBoxE5Add" runat="server" FilterType="Custom,Numbers"
                                                                        TargetControlID="txtPhoneAdd" ValidChars="+" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtPhoneAdd" runat="server" TabIndex="9" BackColor="#e7e7e7" Text='<%# Bind("Phone") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 2
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd2Add" TabIndex="7" runat="server" BackColor="#e7e7e7" Text='<%# Bind("Add2") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Category *
                                                                    <asp:CompareValidator ID="cvLedgerAdd" runat="server" ControlToValidate="drpLedgerCatAdd"
                                                                        Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Category is mandatory"
                                                                        Operator="GreaterThan" ValueToCompare="0" />
                                                                                        </td>
                                                                                        <td style="width: 28%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpLedgerCatAdd" TabIndex="10" Width="100%" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" DataTextField="CusCategory_Name" DataValueField="CusCategory_Value" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                runat="server" OnDataBound="drpLedgerCatAdd_DataBound" DataSourceID="srccuscatadd">
                                                                                                <asp:ListItem style="background-color: #e7e7e7" Text="Select Category" Value="0"></asp:ListItem>
                                                                                                <%--<asp:ListItem Text="Customer" Value="Customer" Selected="True"></asp:ListItem>--%>
                                                                                                <%--<asp:ListItem Text="Select Category" Value="0"></asp:ListItem>--%>
                                                                                                <%--<asp:ListItem Text="Dealer" Value="Dealer"></asp:ListItem>--%>

                                                                                                <%--<asp:ListItem Text="Others" Value="Others"></asp:ListItem>--%>
                                                                                            </asp:DropDownList>
                                                                                            <%--<cc1:ListSearchExtender id="LSE2" runat="server" TargetControlID="drpLedgerCatAdd" PromptCssClass="ListSearchExtenderPrompt" PromptPosition="Bottom"></cc1:ListSearchExtender>--%>
                                                                                        
                                                                                        </td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Address 3
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtAdd3Add" TabIndex="8" runat="server" Width="150%" BackColor="#e7e7e7" Text='<%# Bind("Add3") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
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

                                                                                        <td style="width: 10%;"></td>
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
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender123" runat="server" FilterType="Custom, Numbers"
                                                                        TargetControlID="txtMobileAdd" ValidChars="+" />
                                                                                        </td>
                                                                                        <td style="width: 28%" class="ControlDrpBorder">
                                                                                            <asp:TextBox ID="txtMobileAdd" MaxLength="10" TabIndex="13" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("Mobile") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">OB Due Date    
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtdueDateadd" MaxLength="10" TabIndex="13" Enabled="false" Width="150%" BackColor="#e7e7e7" runat="server" Text='<%# Bind("OpDueDate") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="calBillDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="btnBillDate" TargetControlID="txtdueDateadd">
                                                                                            </cc1:CalendarExtender>
                                                                                        </td>

                                                                                        <td style="width: 14%;" class="ControlLabel" align="left">
                                                                                            <asp:ImageButton ID="btnBillDate" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                            Branch
                                                                                        </td>
                                                                                        <td style="width: 28%" class="ControlDrpBorder">
                                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
                                                                                            <asp:DropDownList ID="drpBranchAdd" TabIndex="10" Width="100%" CssClass="drpDownListMedium" AppendDataBoundItems="true" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                runat="server">
                                                                                                <%--<asp:ListItem style="background-color: #e7e7e7" Text="Select Branch" Value="0"></asp:ListItem>--%>
                                                                                           
                                                                                            </asp:DropDownList>
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>

                                                                                        <td style="width: 14%;" align="left">
                                                                                            <%--<asp:ImageButton ID="btnBillDate" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />--%>
                                                                                        </td>
                                                                                        <td style="width: 28%"></td>

                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <%--<tr>
                                                                <td class="ControlLabel" style="width:20%">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDueDate"
                                                                                                    Display="Dynamic" ErrorMessage="Opening Due Date is mandatory.">*</asp:RequiredFieldValidator>
                                                                    Opening Due Date *
                                                                </td>
                                                                <td class="ControlTextBox3" style="width:28%">
                                                                    <asp:TextBox ID="txtDueDate" MaxLength="10" TabIndex="13" Width="150%" BackColor = "#e7e7e7" runat="server" Text='<%# Bind("DueDate") %>'
                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                </td>
                                                                <td class="ControlLabel" style="width:14%">
                                                                    
                                                                </td>
                                                                <td style="width:28%" class="ControlDrpBorder">
                                                                   
                                                                </td>
                                                                <td style="width: 10%;">
                                                                </td>
                                                            </tr>
                                                            <tr style="height:3px">
                                                            </tr>--%>
                                                                                    <tr>
                                                                                        <td></td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddAccGroupAdd" DataSourceID="srcGroupInfoAdd" Height="30px" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                runat="server" SelectedValue='<%# Bind("GroupID") %>' DataTextField="GroupName"
                                                                                                DataValueField="GroupID" Width="98%" AppendDataBoundItems="True" Visible="False">
                                                                                                <%--<asp:ListItem style="background-color: #90c9fc" Selected="True" Value="0">Select Account Group</asp:ListItem>--%>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>

                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                        <cc1:TabPanel ID="tabInsAddTab" runat="server" HeaderText="Additional Details">
                                                                            <ContentTemplate>
                                                                                <table align="center" cellpadding="3" cellspacing="5" style="border: 0px solid #5078B3; width: 770px;">
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">TIN No.
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtTinAdd" TabIndex="12" runat="server" Width="150%" BackColor="#e7e7e7" Text='<%# Bind("TINnumber") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Executive Incharge
                                                                                        </td>
                                                                                        <td class="ControlDrpBorder" style="width: 28%">
                                                                                            <asp:DropDownList ID="drpInchargeAdd" Width="100%" EnableTheming="false" TabIndex="11" BackColor="#e7e7e7"
                                                                                                AppendDataBoundItems="true" runat="server" CssClass="drpDownListMedium" DataSourceID="empSrcAdd" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                DataTextField="empFirstName" DataValueField="empno" SelectedValue='<%# Bind("ExecutiveIncharge") %>'>
                                                                                                <asp:ListItem Text="Select Executive" Value="0" Selected="True"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Credit Limit
                                                                                <cc1:FilteredTextBoxExtender ID="flCreditDaysAdd" runat="server" FilterType="Numbers"
                                                                                    TargetControlID="txtCredtLimitDaysAdd" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtCreditLimitAdd" TabIndex="14" runat="server" Width="150%" BackColor="#e7e7e7" Text='<%# Bind("CreditLimit") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Credit Limit Days
                                                                                <cc1:FilteredTextBoxExtender ID="FtTextBoxExtender1" runat="server" FilterType="Custom,Numbers"
                                                                                    ValidChars="." TargetControlID="txtCreditLimitAdd" />
                                                                                        </td>
                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                            <asp:TextBox ID="txtCredtLimitDaysAdd" TabIndex="15" runat="server" Width="150%" BackColor="#e7e7e7" Text='<%# Bind("CreditDays") %>'
                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                        </td>
                                                                                        <td style="width: 10%;"></td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">Internal Transfer
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpIntTransAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpIntTransAdd_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("Inttrans") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%">Payment Source
                                                                                        </td>
                                                                                        <td style="width: 28%" class="ControlDrpBorder">

                                                                                            <asp:UpdatePanel ID="UpdatePanel123" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
                                                                                            <asp:DropDownList ID="drpPaymentmadeAdd" TabIndex="10" AutoPostBack="true" runat="server" BackColor="#e7e7e7" OnSelectedIndexChanged="drpPaymentmadeAdd_SelectedIndexChanged" OnDataBound="drpPaymentmadeAdd_DataBound" CssClass="drpDownListMedium"

                                                                                            

                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("Paymentmade") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>

            </ContentTemplate>
                                                                                                </asp:UpdatePanel>


                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr style="height: 3px">
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">DC
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder">

                                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
                                                                                            <asp:DropDownList ID="drpdcAdd" TabIndex="10" AutoPostBack="true" runat="server" BackColor="#e7e7e7" OnDataBound="drpdcAdd_DataBound" CssClass="drpDownListMedium" OnSelectedIndexChanged="drpdcAdd_SelectedIndexChanged" 

                                                                                            
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("dc") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                            </asp:DropDownList>

            </ContentTemplate>
                                                                                                </asp:UpdatePanel>

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
                                                                                    <tr>
                                                                                        <td class="ControlLabel" style="width: 20%">
                                                                                            <%--Un Use--%>
                                                                                Is Active  
                                                                                        </td>
                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                            <asp:DropDownList ID="drpunuseAdd" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" OnDataBound="drpunuseAdd_DataBound" CssClass="drpDownListMedium"
                                                                                                Width="100%" Style="border: 1px solid #e7e7e7" Height="26px" SelectedValue='<%# Bind("Unuse") %>'>
                                                                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                                <asp:ListItem Text="YES" Value="YES" Selected="True"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td class="ControlLabel" style="width: 14%"></td>
                                                                                        <td style="width: 28%"></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </ContentTemplate>
                                                                        </cc1:TabPanel>
                                                                    </cc1:TabContainer>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 8px">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td style="width: 35%"></td>
                                                                            <td style="width: 18%">
                                                                                <asp:Button ID="InsertButton" TabIndex="17" CssClass="savebutton1231" EnableTheming="false"
                                                                                    runat="server" CausesValidation="True" CommandName="Insert" SkinID="skinBtnSave"
                                                                                    OnClientClick="Mobile_Validator();CheckMode();" OnClick="InsertButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 18%">
                                                                                <asp:Button ID="InsertCancelButton" CssClass="cancelbutton6" EnableTheming="false"
                                                                                    TabIndex="16" runat="server" CausesValidation="False" CommandName="" SkinID="skinBtnCancel"
                                                                                    OnClick="InsertCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 25%"></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>


                                                                <tr style="display: none">
                                                                    <td>
                                                                        <asp:ValidationSummary ID="valSumAdd" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                            Font-Size="12" runat="server" />
                                                                        <asp:ObjectDataSource ID="srccuscatadd" runat="server" SelectMethod="ListCusCategory"
                                                                            TypeName="BusinessLogic">
                                                                            <SelectParameters>
                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ObjectDataSource ID="srcGroupInfoAdd" runat="server" SelectMethod="ListGroupInfoCust"
                                                                            TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                            <SelectParameters>
                                                                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                            </SelectParameters>
                                                                        </asp:ObjectDataSource>
                                                                    </td>
                                                                </tr>
                                                            </tr>
                                                        </table>

                                                    </div>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
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
                                            AllowPaging="True" DataKeyNames="LedgerID" EmptyDataText="No Customer Data Found."
                                            OnRowCommand="GrdViewLedger_RowCommand" OnRowDataBound="GrdViewLedger_RowDataBound" OnRowDeleting="GrdViewLedger_RowDeleting"
                                            OnRowDeleted="GrdViewLedger_RowDeleted" CssClass="someClass">
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                            <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                            <Columns>
                                                <asp:BoundField DataField="LedgerName" HeaderText="Customer / Dealer Name" HeaderStyle-BorderColor="Gray" />
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
                                                <asp:BoundField DataField="LedgerCategory" HeaderText="Category" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="CreditLimit" HeaderText="Credit Limit" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="CreditDays" HeaderText="Credit Days" HeaderStyle-BorderColor="Gray" />
                                                <asp:TemplateField ItemStyle-CssClass="command" ItemStyle-Width="50px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="50px" HeaderText="Delete" HeaderStyle-BorderColor="Gray">
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Customer Details?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("LedgerID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="height: 1px">
                                                    </tr>
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" runat="server" Style="border: 1px solid blue" AutoPostBack="true" Width="75px" Height="23px" BackColor="#e7e7e7">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="border-color: white; width: 5px"></td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false"
                                                                ID="btnFirst" Width="22px" Height="18px" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false"
                                                                ID="btnPrevious" Width="22px" Height="18px" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false"
                                                                ID="btnNext" Width="22px" Height="18px" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false"
                                                                ID="btnLast" Width="22px" Height="18px" />
                                                        </td>

                                                    </tr>
                                                    <tr style="height: 2px">
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
                <tr>
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListCustomerInfo"
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
                            InsertMethod="InsertCustomerInfo" UpdateMethod="UpdateCustomerInfo">
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
                                <asp:Parameter Name="CreditLimit" Type="Double" />
                                <asp:Parameter Name="CreditDays" Type="Int32" />
                                <asp:Parameter Name="Inttrans" Type="String" />
                                <asp:Parameter Name="Paymentmade" Type="String" />
                                <asp:Parameter Name="dc" Type="String" />
                                <asp:Parameter Name="ChequeName" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="unuse" Type="String" />
                                <asp:Parameter Name="Email" Type="String" />
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
                                <asp:Parameter Name="CreditLimit" Type="Double" />
                                <asp:Parameter Name="CreditDays" Type="Int32" />
                                <asp:Parameter Name="Inttrans" Type="String" />
                                <asp:Parameter Name="Paymentmade" Type="String" />
                                <asp:Parameter Name="dc" Type="String" />
                                <asp:Parameter Name="ChequeName" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="unuse" Type="String" />
                                <asp:Parameter Name="Email" Type="String" />
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
                                <td style="width:20%">

                                </td>
                                <td style="width: 15%" align="right">
                                   
                                            <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66" ForeColor="White" EnableTheming="false"
                                                    Width="80px"></asp:Button>
                                            </asp:Panel>
                                       
                                </td>
                                <td style="width: 15%">
                                    <asp:Button ID="BlkAdd" runat="server" OnClientClick="window.open('BulkAdditionLedger.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');" CssClass="bulkaddition"
                                        EnableTheming="false" Text=""></asp:Button>
                                </td>
                                <td style="width: 15%">
                                    <asp:Button ID="Button2" runat="server" CssClass="exportexl6" OnClientClick="window.open('ReportExcelCustomers.aspx ','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=220,width=500,left=425,top=240, scrollbars=yes');"
                                        EnableTheming="false"></asp:Button>
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
