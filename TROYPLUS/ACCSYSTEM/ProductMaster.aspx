<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    CodeFile="ProductMaster.aspx.cs" Inherits="ProductMaster" Title="Inventory > Product Master" %>

<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script language="javascript" type="text/javascript">

////        function MethodonFirstPage() {
////            document.getElementById("lnkBtnAdd").click();
////            //            __doPostBack('lnkBtnAdd','');
////            //            document.myForm.lnkBtnAdd.click();
////        }
//#0567AE
////        function openPopup() {
////            window.open('productmaster.aspx', 'pop', 'width=1000,height=500,toolbar=no,scrollbars=yes');
////        }



//        function SetValue() 
//        {
//            var name = document.getElementById("ModalPopupExtender1").value;
//            window.opener.SetName(name);
//            window.close();
//        }



        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_frmViewAdd_tablInsertControl');

            if (tabContainer == null)
                tabContainer = $find('ctl00_cplhControlPanel_frmViewAdd_tabEditContol');

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


        function CheckReorderLevel() {
            var txtLevel = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tablInsertControl_tabInsProdMaster_txtROLAdd');

            if (txtLevel.value == '0') {
                var rv = confirm("ReOrder Level With 0 will consider as Obsolute Model and based on Configuration it will not appear in Purchase. Are you sure to make it as Obsolute Model?");

                if (rv == true) {
                    return true;
                }
                else {
                    return window.event.returnValue = false;
                }
            }
        }

        function CheckorderLevel() {
            var txtLevel1 = document.getElementById('ctl00_cplhControlPanel_frmViewAdd_tabEditContol_tabEditProdMaster_txtROL');

            if (txtLevel1 == null)
                txtLevel1 = document.getElementById('ctl00_cplhControlPanel_tabEditContol_tabEditProdMaster_txtROL');

            if (txtLevel1.value == '0') {
                var rv = confirm("ReOrder Level With 0 will consider as Obsolute Model and based on Configuration it will not appear in Purchase. Are you sure to make it as Obsolute Model?");

                if (rv == true) {
                    return true;
                }
                else {
                    return window.event.returnValue = false;
                }
            }
        }

    </script>

    <style id="Style1" runat="server">
        
        
        .fancy-green .ajax__tab_header
        {
	        background: url(App_Themes/NewTheme/Images/green_bg_Tab.gif) repeat-x;
	        cursor:pointer;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_outer, .fancy-green .ajax__tab_active .ajax__tab_outer
        {
	        background: url(App_Themes/NewTheme/Images/green_left_Tab.gif) no-repeat left top;
        }
        .fancy-green .ajax__tab_hover .ajax__tab_inner, .fancy-green .ajax__tab_active .ajax__tab_inner
        {
	        background: url(App_Themes/NewTheme/Images/green_right_Tab.gif) no-repeat right top;
        }
        .fancy .ajax__tab_header
        {
	        font-size: 13px;
	        font-weight: bold;
	        color: #000;
	        font-family: sans-serif;
        }
        .fancy .ajax__tab_active .ajax__tab_outer, .fancy .ajax__tab_header .ajax__tab_outer, .fancy .ajax__tab_hover .ajax__tab_outer
        {
	        height: 46px;
        }
        .fancy .ajax__tab_active .ajax__tab_inner, .fancy .ajax__tab_header .ajax__tab_inner, .fancy .ajax__tab_hover .ajax__tab_inner
        {
	        height: 46px;
	        margin-left: 16px; /* offset the width of the left image */
        }
        .fancy .ajax__tab_active .ajax__tab_tab, .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_header .ajax__tab_tab
        {
	        margin: 16px 16px 0px 0px;
        }
        .fancy .ajax__tab_hover .ajax__tab_tab, .fancy .ajax__tab_active .ajax__tab_tab
        {
	        color: #fff;
        }
        .fancy .ajax__tab_body
        {
	        font-family: Arial;
	        font-size: 10pt;
	        border-top: 0;
	        border:1px solid #999999;
	        padding: 8px;
	        background-color: #ffffff;
        }
        
    </style>

    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Always">
        <ContentTemplate>
            
            <table style="width: 100%; vertical-align: top" align="center">
                <tr style="width: 100%">
                    <td style="width: 100%">
                    
                            <%--<div class="mainConHd">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tr valign="middle">
                                        <td>
                                            <span>Inventory Products</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Inventory Products
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <div>
                                    <table cellspacing="0" cellpadding="0" border="0" width="99.8%" style="margin: 0px 4px 0px 1px;"
                                        class="searchbg">
                                        <tr >
                                            
                                            <td style="width: 24%; font-size: 22px; color: White;" >
                                                Product Master
                                            </td>
                                            <td style="width: 13%">
                                                <div style="text-align: right;">
                                                    <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                        <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                                                            EnableTheming="false" Width="80px" Text=""></asp:Button>
                                                    </asp:Panel>
                                                </div>
                                            </td>

                                            <td style="width: 7%; color: White;" align="right">
                                                Search
                                            </td>
                                           
                                            <td style="width: 19%" class="NewBox">
                                                <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch" 
                                                    ontextchanged="txtSearch_TextChanged"></asp:TextBox>
                                            </td>

                                            <td style="width: 20%;" class="NewBox">
                                                <div style="width: 160px; font-family: 'Trebuchet MS';">
                                                    <asp:DropDownList ID="ddCriteria" runat="server" Width="153px" BackColor="White" style="text-align:center;border:1px solid White "
                                                        AutoPostBack="false" 
                                                        onselectedindexchanged="ddCriteria_SelectedIndexChanged">
                                                        <asp:ListItem Value="ItemCode">Product Code</asp:ListItem>
                                                        <asp:ListItem Value="ProductName">Product Name</asp:ListItem>
                                                        <asp:ListItem Value="Model">Model</asp:ListItem>
                                                        <asp:ListItem Value="Brand">Brand</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="width: 15%" class="tblLeftNoPad">
                                                <asp:Button ID="btnSearch" runat="server" Text="" EnableTheming="false" CssClass="ButtonSearch6"
                                                    onclick="btnSearch_Click" />
                                            </td>
                                            <td style="width: 15%" class="tblLeftNoPad">
                                                <asp:Button ID="BtnClearFilter" runat="server"  OnClick="BtnClearFilter_Click"  EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                            </td>
                                            
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        
                        <input id="dummy" type="button" style="display: none" runat="server" />
                        <input id="Button1" type="button" style="display: none" runat="server" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="Button1" DynamicServicePath="" Enabled="True" PopupControlID="popUp"
                            TargetControlID="dummy">
                        </cc1:ModalPopupExtender>
                        <asp:Panel runat="server" ID="popUp" Style="width: 52%">
                            <div id="contentPopUp">
                                <table class="tblLeft" cellpadding="0" cellspacing="0" style="border: 0px solid #5078B3;
                                    background-color: #fff; color: #000;" width="100%">
                                    <tr>
                                        <td>
                                            <asp:FormView ID="frmViewAdd" runat="server" Width="100%" DataSourceID="frmSource"
                                                DataKeyNames="ItemCode" OnItemCommand="frmViewAdd_ItemCommand" DefaultMode="Edit"
                                                OnItemCreated="frmViewAdd_ItemCreated" Visible="False" OnItemInserting="frmViewAdd_ItemInserting"
                                                OnItemUpdating="frmViewAdd_ItemUpdating" EmptyDataText="No Records" OnItemInserted="frmViewAdd_ItemInserted"
                                                OnItemUpdated="frmViewAdd_ItemUpdated" OnDataBound="frmViewAdd_DataBound" OnModeChanged="frmViewAdd_ModeChanged">
                                              
                                                <EditItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="3" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="5" class="headerPopUp">
                                                                    Product Master
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <div style="text-align: left">
                                                                        <cc1:TabContainer ID="tabEditContol" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                            <cc1:TabPanel ID="tabEditProdMaster" runat="server" HeaderText="Product Details">
                                                                                <ContentTemplate>
                                                                                    <table style="width: 700px; border: 0px solid #5078B3;"
                                                                                        cellpadding="3" cellspacing="1">
                                                                                        <tr class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Product Code *
                                                                                                <asp:RequiredFieldValidator ID="rvItemCode" runat="server" ControlToValidate="txtItemCode"
                                                                                                    Text="*" Display="Dynamic" ErrorMessage="ItemCode is mandatory">*</asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtItemCode" runat="server" Text='<%# Bind("ItemCode") %>' Enabled="False"
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="ControlLabel" style="width: 20%">
                                                                                                Stock
                                                                                                <cc1:FilteredTextBoxExtender ID="FTBStock" runat="server" FilterType="Custom, Numbers"
                                                                                                    TargetControlID="txtStock" ValidChars="." />
                                                                                                <asp:CompareValidator ID="rvStock" runat="server" ControlToValidate="txtStock" Display="Dynamic"
                                                                                                    Type="Integer" Operator="DataTypeCheck" Text="*" ErrorMessage="Stock should be a Number"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtStock" runat="server" Text='<%# Bind("Stock") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        <tr class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Product Name *
                                                                                                <asp:RequiredFieldValidator ID="rvBDate" runat="server" ControlToValidate="txtItemName"
                                                                                                    Text="*" Display="Dynamic" ErrorMessage="ItemName is mandatory."></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%;" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtItemName" runat="server" Text='<%# Bind("ProductName") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Model *
                                                                                                <asp:RequiredFieldValidator ID="rvModel" runat="server" ControlToValidate="txtModel"
                                                                                                    Text="*" Display="Dynamic" ErrorMessage="Model is mandatory"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtModel" runat="server" Text='<%# Bind("Model") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        <tr class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Brand *
                                                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtProdDesc"
                                                                                                    Display="Dynamic" ErrorMessage="Brand is Mandatory" Operator="GreaterThan"
                                                                                                    Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlDrpBorder">
                                                                                                <%--<asp:TextBox ID="txtProdDesc" runat="server" Text='<%# Bind("ProductDesc") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>--%>
                                                                                                <asp:DropDownList ID="txtProdDesc" runat="server" DataTextField="BrandName" Width="100%" BackColor = "#e7e7e7" CssClass="drpDownListMedium"
                                                                                                    DataValueField="BrandID" DataSourceID="srcBrand" style="border: 1px solid #e7e7e7" height="26px"
                                                                                                    EnableTheming="false"  OnDataBound="txtProdDesc_DataBound"
                                                                                                    AppendDataBoundItems="True">
                                                                                                    <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select BrandName</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Category *
                                                                                                <asp:CompareValidator ID="cvCatergory" runat="server" ControlToValidate="ddCategory"
                                                                                                    Display="Dynamic" ErrorMessage="Category is Mandatory" Operator="GreaterThan"
                                                                                                    Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlDrpBorder">
                                                                                                <asp:DropDownList ID="ddCategory" EnableTheming="false" runat="server" 
                                                                                                    DataTextField="CategoryName" DataValueField="CategoryID" CssClass="drpDownListMedium" BackColor = "#e7e7e7"
                                                                                                    Width="100%" DataSourceID="srcCategory" style="border: 1px solid #e7e7e7" height="26px" SelectedValue='<%# Bind("CategoryID") %>'
                                                                                                    AppendDataBoundItems="True">
                                                                                                    <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Category</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        
                                                                                        <tr style="display: none">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Sell Units
                                                                                            </td>
                                                                                            <td style="width: 25%; display: none" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtUnit" runat="server" Text='<%# Bind("Unit") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%">
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        <tr class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                MRP *
                                                                                                <cc1:FilteredTextBoxExtender ID="FTBRadteAdd1" runat="server" FilterType="Custom, Numbers"
                                                                                                    TargetControlID="txtUnitRate" ValidChars="." />
                                                                                                <asp:RequiredFieldValidator ID="rvSRate" runat="server" ControlToValidate="txtUnitRate"
                                                                                                    Text="*" Display="Dynamic" ErrorMessage="UnitRate is mandatory"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtUnitRate" runat="server" Text='<%# Bind("Rate") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                MRP Effective date
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMrpDate"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="MRP Effective date is mandatory"></asp:RequiredFieldValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtMrpDate" runat="server" Text='<%# Bind("MRPEffDate","{0:dd/MM/yyyy}") %>'
                                                                                                    CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                <cc1:CalendarExtender ID="calExtender3" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                    PopupButtonID="btnDate3" PopupPosition="BottomLeft" TargetControlID="txtMrpDate">
                                                                                                </cc1:CalendarExtender>
                                                                                            </td>
                                                                                                        
                                                                                            <td style="width: 10%" align="left">
                                                                                                <asp:ImageButton ID="btnDate3" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                    Width="20px" runat="server" />
                                                                                            </td>
                                                                                            
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        <tr class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                VAT (%) *
                                                                                                <asp:RequiredFieldValidator ID="rvVAT" runat="server" ControlToValidate="txtVAT"
                                                                                                    Text="*" Display="Dynamic" ErrorMessage="VAT is mandatory"></asp:RequiredFieldValidator>
                                                                                                <asp:RangeValidator ID="cvVAT" runat="server" ControlToValidate="txtVAT" Display="Dynamic"
                                                                                                    MaximumValue="100" Type="Double" MinimumValue="0" Text="*" ErrorMessage="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtVAT" runat="server" Text='<%# Bind("VAT") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Sell Discount (%) *
                                                                                                <asp:RequiredFieldValidator ID="rvDiscDis" runat="server" ControlToValidate="txtDiscount"
                                                                                                    Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Discount is mandatory"></asp:RequiredFieldValidator>
                                                                                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtDiscount"
                                                                                                    Display="Dynamic" MaximumValue="100" Type="Double" MinimumValue="0" Text="*"
                                                                                                    ErrorMessage="Discount cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtDiscount" runat="server" Text='<%# Bind("Discount") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                        </tr>
                                                                                        <tr style="height: 30px" class="tblLeft" runat="server" id="rowDealer">
                                                                                                <td style="width: 20%" class="ControlLabel">
                                                                                                    Dealer Rate *
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDealerRate"
                                                                                                        Text="*" Display="Dynamic" ErrorMessage="Dealer Rate is mandatory"></asp:RequiredFieldValidator>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FTBDRate" runat="server" FilterType="Custom, Numbers"
                                                                                                        TargetControlID="txtDealerRate" ValidChars="." />
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtDealerRate" runat="server" Text='<%# Bind("DealerRate") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 20%" class="ControlLabel">
                                                                                                    Dealer Rate Effective date
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtDpDate" runat="server" Text='<%# Bind("DPEffDate","{0:dd/MM/yyyy}") %>'
                                                                                                        CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                        PopupButtonID="ImageButton1" PopupPosition="BottomLeft" TargetControlID="txtDpDate">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                                <td style="width: 10%" align="left">
                                                                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                        Width="20px" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height:3px">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 20%; padding-left: 56px;" class="ControlLabel">
                                                                                                     NLC *
                                                                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNLC"
                                                                                                        Text="*" Display="Dynamic" ErrorMessage="NLC Rate is mandatory"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtNLC" runat="server" Text='<%# Bind("NLC") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 20%" class="ControlLabel">
                                                                                                    NLC Effective date
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtNLCDate" runat="server" Text='<%# Bind("NLCEffDate","{0:dd/MM/yyyy}") %>'
                                                                                                        CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                        PopupButtonID="ImageButton2" PopupPosition="BottomLeft" TargetControlID="txtNLCDate">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                                <td style="width: 10%" align="left">
                                                                                                    <asp:ImageButton ID="ImageButton2" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                        Width="20px" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height:3px">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 20%" class="ControlLabel">
                                                                                                    Reorder Level *
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtROL"
                                                                                                        Text="*" Display="Dynamic" ErrorMessage="Stock Level is mandatory"></asp:RequiredFieldValidator>
                                                                                                    <cc1:FilteredTextBoxExtender ID="fltReorder" runat="server" FilterType="Numbers"
                                                                                                        TargetControlID="txtROL" />
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtROL" runat="server" Text='<%# Bind("ROL") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td id="Td2" style="width: 20%" runat="server" class="ControlLabel">
                                                                                                    Dealer Discount (%) *
                                                                                                    <asp:RangeValidator ID="rvDis" runat="server" ControlToValidate="txtDealerDiscount"
                                                                                                        Display="Dynamic" MaximumValue="100" Type="Double" MinimumValue="0" Text="*"
                                                                                                        ErrorMessage="Discount cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                </td>
                                                                                                <td id="Td3" style="width: 25%" runat="server" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtDealerDiscount" runat="server" Text='<%# Bind("DealerDiscount") %>'
                                                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height:3px">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 20%" class="ControlLabel">
                                                                                                    OutDated *
                                                                                                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="drpOutdated"
                                                                                                    Display="Dynamic" ErrorMessage="OutDated is Mandatory" Operator="GreaterThan"
                                                                                                    Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                    
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                                                    <asp:DropDownList ID="drpOutdated" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7" SelectedValue='<%# Bind("Outdated") %>'
                                                                                                        AppendDataBoundItems="True" EnableTheming="False" style="border: 1px solid #e7e7e7" height="26px">
                                                                                                        <asp:ListItem Value="N">No</asp:ListItem>
                                                                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td style="width: 20%" class="ControlLabel">
                                                                                                    Allowed Price % *
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAllowedPrice"
                                                                                                        Text="*" Display="Dynamic" ErrorMessage="Allowed Price is mandatory"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtAllowedPrice" runat="server" Text='<%# Bind("Deviation") %>'
                                                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        <tr style="height:3px">
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 20%" class="ControlLabel">
                                                                                                    IsActive *
                                                                                                    <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="drpIsActive"
                                                                                                    Display="Dynamic" ErrorMessage="IsActive is Mandatory" Operator="GreaterThan"
                                                                                                    Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                    
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                                                    <asp:DropDownList ID="drpIsActive" TabIndex="10" runat="server" BackColor = "#e7e7e7" CssClass="drpDownListMedium"
                                                                                                                Width="100%" style="border: 1px solid #e7e7e7" height="26px" SelectedValue='<%# Bind("IsActive") %>'>
                                                                                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                                                <asp:ListItem Text="YES" Value="YES" Selected="True"></asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                </td>
                                                                                                <td style="width: 20%" class="ControlLabel">
                                                                                                    
                                                                                                </td>
                                                                                                <td style="width: 25%">
                                                                                                    
                                                                                                </td>
                                                                                            </tr>
                                                                                        
                                                                                            <tr>
                                                                                                <td>
                                                                                                <asp:DropDownList ID="drpblock" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7" SelectedValue='<%# Bind("block") %>'
                                                                                                    AppendDataBoundItems="True" EnableTheming="False" style="border: 1px solid #90c9fc" height="26px" Visible="False">
                                                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                                <asp:ObjectDataSource ID="srcBrand" runat="server" SelectMethod="ListBrandMaster"
                                                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                                                    <SelectParameters>
                                                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                    </SelectParameters>
                                                                                                </asp:ObjectDataSource>
                                                                                            </td>
                                                                                            <td style="width: 5%">
                                                                                                <asp:ObjectDataSource ID="srcCategory" runat="server" SelectMethod="ListCategory"
                                                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                                                    <SelectParameters>
                                                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                    </SelectParameters>
                                                                                                </asp:ObjectDataSource>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="tabEditRates" runat="server" HeaderText="Additional Details">
                                                                                <ContentTemplate>
                                                                                    <table align="center" cellpadding="3" cellspacing="2" style="border: 0px solid #5078B3;
                                                                                        width: 700px;">
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                CST (%)
                                                                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtCST"
                                                                                                    Display="Dynamic" MaximumValue="100" Type="Double" Text="*" MinimumValue="0"
                                                                                                    ErrorMessage="CST cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtCST" runat="server" Text='<%# Bind("CST") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Commodity Code
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtCommCode" runat="server" Text='<%# Bind("CommodityCode") %>'
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            
                                                                                            <td style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Unit Measure
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlDrpBorder">
                                                                                                <asp:DropDownList EnableTheming="false" ID="drpMeasure" runat="server" Width="100%" style="border: 1px solid #e7e7e7" height="26px"
                                                                                                    DataTextField="Unit" DataValueField="Unit" CssClass="drpDownListMedium" BackColor = "#e7e7e7" DataSourceID="srcUnitMnt"
                                                                                                    AppendDataBoundItems="True" OnDataBound="drpMeasure_DataBound">
                                                                                                    <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Measure</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Barcode
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBarcode" runat="server" Text='<%# Bind("Barcode") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <%--<tr style="height:3px">
                                                                                    </tr>--%>
                                                                                        <tr style="height: 30px" class="tblLeft" runat="server" visible="false">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Buy Unit Rate
                                                                                                <cc1:FilteredTextBoxExtender ID="FTBBURate" runat="server" FilterType="Custom, Numbers"
                                                                                                    TargetControlID="txtBuyRate" ValidChars="." />
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBuyRate" runat="server" Text='<%# Bind("BuyRate") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Buy VAT (%)
                                                                                                <asp:RangeValidator ID="rvBuyVAT" runat="server" ControlToValidate="txtBuyVAT" Display="Dynamic"
                                                                                                    MaximumValue="100" Type="Double" MinimumValue="0" Text="*" ErrorMessage="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBuyVAT" runat="server" Text='<%# Bind("BuyVAT") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Buy Discount (%)
                                                                                                <asp:RangeValidator ID="RVBusDis" runat="server" ControlToValidate="txtBuyDiscount"
                                                                                                    Display="Dynamic" MaximumValue="100" Type="Double" MinimumValue="0" Text="*"
                                                                                                    ErrorMessage="Discount cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBuyDiscount" runat="server" Text='<%# Bind("BuyDiscount") %>'
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Executive Commission
                                                                                                <asp:CompareValidator ID="rvExecComm" runat="server" ControlToValidate="txtEditExecutiveCommission"
                                                                                                    Display="Dynamic" Type="Double" Operator="DataTypeCheck" Text="*" ErrorMessage="Commission should be Numeric value"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtEditExecutiveCommission" runat="server" Text='<%# Bind("ExecutiveCommission") %>'
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        
                                                                                        <tr style="height: 30px; display: none" class="tblLeft" runat="server" id="rowDealer1">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Dealer VAT (%)
                                                                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDealerVAT"
                                                                                                    Display="Dynamic" MaximumValue="100" Type="Double" Text="*" MinimumValue="0"
                                                                                                    ErrorMessage="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td id="Td1" style="width: 25%" runat="server" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtDealerVAT" runat="server" Text='<%# Bind("DealerVAT") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%">
                                                                                            </td>
                                                                                            <td style="width: 25%; display: none">
                                                                                                <asp:TextBox ID="txtProductlevel" runat="server" Visible="false" Text='<%# Bind("Productlevel") %>'
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 30px; display: none" class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Dealer Units
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtDealerUnit" runat="server" Text='<%# Bind("DealerUnit") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Complex
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:DropDownList ID="drpComplex" runat="server" Width="100%" SelectedValue='<%# Bind("Complex") %>'
                                                                                                    AppendDataBoundItems="True" EnableTheming="False" style="border: 1px solid #e7e7e7" height="26px" CssClass="drpDownListMedium" BackColor = "#e7e7e7">
                                                                                                    <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                                                    <asp:ListItem Value="">No</asp:ListItem>
                                                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        
                                                                                    
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        <tr style="height: 30px; display: none" class="tblLeft">
                                                                                            <td style="width: 20%; display: none" class="ControlLabel">
                                                                                                Buy Units
                                                                                            </td>
                                                                                            <td style="width: 25%; display: none" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBuyUnit" runat="server" Text='<%# Bind("BuyUnit") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%">
                                                                                                <asp:ObjectDataSource ID="srcUnitMnt" runat="server" SelectMethod="ListMeasurementUnits"
                                                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                                                    <SelectParameters>
                                                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                    </SelectParameters>
                                                                                                </asp:ObjectDataSource>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:DropDownList ID="drpRoleType" Visible="False" runat="server" style="border: 1px solid #e7e7e7" height="26px" Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7" SelectedValue='<%# Bind("Accept_Role") %>'
                                                                                                    AppendDataBoundItems="True" EnableTheming="False">
                                                                                                    <asp:ListItem Selected="True" style="background-color: #e7e7e7">-- Please Select --</asp:ListItem>
                                                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                                                                    <asp:ListItem Value="">No</asp:ListItem>
                                                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                            <%--<cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Rate History Details">
                                                                                <ContentTemplate>
                                                                                    <table align="center" cellpadding="3" cellspacing="2" style="border: 0px solid #5078B3;
                                                                                        width: 700px;">
                                                                                            <tr style="height:10px">
                                                                                            </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:GridView ID="GrdViewHistory" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                                                    Width="100%" DataKeyNames="Itemcode" AllowPaging="True" EmptyDataText="No History found." CssClass="someClass">
                                                                                                    <EmptyDataRowStyle CssClass="GrdContent" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="mrp"  HeaderText="MRP Rate"  HeaderStyle-BorderColor="Gray"/>
                                                                                                        <asp:BoundField DataField="MrpDate" HeaderText="Mrp Eff Date" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-BorderColor="Gray"/>
                                                                                                        <asp:BoundField DataField="NLC" HeaderText="NLC"  HeaderStyle-BorderColor="Gray"/>
                                                                                                        <asp:BoundField DataField="NLCDate" HeaderText="NLC Eff Date" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-BorderColor="Gray"/>
                                                                                                        <asp:BoundField DataField="DP" HeaderText="DP"  HeaderStyle-BorderColor="Gray"/>
                                                                                                        <asp:BoundField DataField="DPDate" HeaderText="DP Eff Date" DataFormatString="{0:dd/MM/yyyy}"  HeaderStyle-BorderColor="Gray"/>
                                                                                                        <%--<asp:BoundField DataField="Itemcode"  HeaderText="Itemcode" />--%>
                                                                                                    <%--</Columns>
                                                                                                    <PagerTemplate>
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    Goto Page
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:DropDownList ID="ddlPageSelector" runat="server" AutoPostBack="true" SkinID="skinPagerDdlBox2" style="border:1px solid blue">
                                                                                                                    </asp:DropDownList>
                                                                                                                </td>
                                                                                                                <td style="Width:5px">
                                            
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                        ID="btnFirst" />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Button Text="Previous" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                        ID="btnPrevious" />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Button Text="Next" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                        ID="btnNext" />
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <asp:Button Text="Last" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                        ID="btnLast" />
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </PagerTemplate>
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:10px">
                                                                                            </tr>
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>--%>
                                                                        </cc1:TabContainer>
                                                                    </div>
                                                                    <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                        ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                        Font-Size="12" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 40px;">
                                                                <td style="width: 100%">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 35%">
                                                                            </td>
                                                                            <td style="width: 18%" align="center">                                                                               
                                                                                 <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    Text="" CssClass="cancelbutton6" EnableTheming="false"
                                                                                    OnClick="UpdateCancelButton_Click"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 18%" align="center">
                                                                                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" OnClientClick="javascript:CheckorderLevel();"
                                                                                    Text="" CssClass="Updatebutton1231" EnableTheming="false" OnClick="UpdateButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td style="width: 5%" align="center">
                                                                                <asp:Button ID="cmdshowhistory" runat="server" Text="" EnableTheming="false" CausesValidation="False" cssclass="ShowHistory" OnClick="cmdshowhistory_click" Visible="False" />
                                                                            </td>
                                                                            <td style="width: 23%">
                                                                            </td>    
                                                                            
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <InsertItemTemplate>
                                                    <div class="divArea">
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="5" class="headerPopUp">
                                                                    Product Master
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <div style="text-align: left">
                                                                        <cc1:TabContainer ID="tablInsertControl" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                            <cc1:TabPanel ID="tabInsProdMaster"  runat="server" HeaderText="Product Details">
                                                                                <ContentTemplate>
                                                                                    
                                                                                                <table style="width: 700px; border: 0px solid #5078B3" align="center" cellpadding="3" cellspacing="1">
                                                                                                    <tr style="height: 30px" class="tblLeft">
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Product Code *
                                                                                                            <asp:RequiredFieldValidator ID="rvItemCodeAdd" runat="server" ControlToValidate="txtItemCodeAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="ItemCode is mandatory"></asp:RequiredFieldValidator>
                                                                                                            <cc1:FilteredTextBoxExtender ID="fltItemCodeAdd" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers"
                                                                                                                TargetControlID="txtItemCodeAdd" />
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtItemCodeAdd" Enabled="true" runat="server" Text='<%# Bind("ItemCode") %>'
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Stock
                                                                                                            <cc1:FilteredTextBoxExtender ID="FTBStockAdd" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="txtStockAdd" ValidChars="." />
                                                                                                            <asp:CompareValidator ID="rvStockAdd" runat="server" ControlToValidate="txtStockAdd"
                                                                                                                Display="Dynamic" EnableClientScript="True" Type="Integer" Operator="DataTypeCheck"
                                                                                                                Text="*" ErrorMessage="Stock should be a Number"></asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtStockAdd" runat="server" Text='<%# Bind("Stock") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width:10%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:3px">
                                                                                    </tr>
                                                                                                    <tr style="height: 30px" class="tblLeft">
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Product Name *
                                                                                                            <asp:RequiredFieldValidator ID="rvBDateAdd" runat="server" ControlToValidate="txtItemNameAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Item Name is mandatory."></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtItemNameAdd" runat="server" Text='<%# Bind("ProductName") %>'
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Model *
                                                                                                            <asp:RequiredFieldValidator ID="rvModelAdd" runat="server" ControlToValidate="txtModelAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Model is mandatory"></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtModelAdd" runat="server" Text='<%# Bind("Model") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 10%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:3px">
                                                                                    </tr>
                                                                                                    <tr style="height: 30px" class="tblLeft">
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Brand *
                                                                                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtProdDescAdd"
                                                                                                                Display="Dynamic" ErrorMessage="Brand is Mandatory" Operator="GreaterThan"
                                                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                                            <%--<asp:TextBox ID="txtProdDescAdd" runat="server" Text='<%# Bind("ProductDesc") %>'
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>--%>
                                                                                                            <asp:DropDownList ID="txtProdDescAdd" runat="server" DataTextField="BrandName" Width="100%" BackColor = "#e7e7e7" CssClass="drpDownListMedium"
                                                                                                                DataValueField="BrandID" DataSourceID="srcBrandAdd" style="border: 1px solid #e7e7e7" height="26px"
                                                                                                                EnableTheming="false" Text='<%# Bind("ProductDesc") %>'
                                                                                                                AppendDataBoundItems="True">
                                                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select BrandName</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Category *
                                                                                                            <asp:CompareValidator ID="cvCatergoryAdd" runat="server" ControlToValidate="ddCategoryAdd"
                                                                                                                Display="Dynamic" ErrorMessage="Category is Mandatory" Operator="GreaterThan"
                                                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                                            <asp:DropDownList ID="ddCategoryAdd" runat="server" DataTextField="CategoryName" Width="100%" BackColor = "#e7e7e7" CssClass="drpDownListMedium"
                                                                                                                DataValueField="CategoryID" DataSourceID="srcCategoryAdd" style="border: 1px solid #e7e7e7" height="26px"
                                                                                                                EnableTheming="false" SelectedValue='<%# Bind("CategoryID") %>'
                                                                                                                AppendDataBoundItems="True">
                                                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Category</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 10%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height: 30px; display: none" class="tblLeft">
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:DropDownList ID="drpRoleTypeAdd" runat="server" Width="30%" CssClass="drpDownListMedium" BackColor = "#e7e7e7" SelectedValue='<%# Bind("Accept_Role") %>'
                                                                                                                AppendDataBoundItems="True" EnableTheming="False" style="border: 1px solid #e7e7e7" height="26px">
                                                                                                                <asp:ListItem Value="N">No</asp:ListItem>
                                                                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtDealerUnitAdd" runat="server" Text='<%# Bind("DealerUnit") %>'
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Complex
                                                                                                        </td>
                                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                                            <asp:DropDownList ID="drpComplexAdd" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7" SelectedValue='<%# Bind("Complex") %>'
                                                                                                                AppendDataBoundItems="True" EnableTheming="False" style="border: 1px solid #e7e7e7" height="26px">
                                                                                                                <asp:ListItem Value="N">No</asp:ListItem>
                                                                                                                <asp:ListItem Value="">No</asp:ListItem>
                                                                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 10%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:3px">
                                                                                                    </tr>
                                                                                                    <tr style="height: 30px" class="tblLeft">
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            MRP *
                                                                                                            <cc1:FilteredTextBoxExtender ID="FTBXBRAdd" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="txtUnitRateAdd" ValidChars="." />
                                                                                                            <asp:RequiredFieldValidator ID="rvSRateAdd" runat="server" ControlToValidate="txtUnitRateAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Rate is mandatory"></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtUnitRateAdd" runat="server" Text="" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            MRP Effective date
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator786" runat="server" ControlToValidate="txtMrpDateAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="MRP Effective date is mandatory"></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtMrpDateAdd" runat="server" Text='<%# Bind("MRPEffDate","{0:dd/MM/yyyy}") %>'
                                                                                                                CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                            <cc1:CalendarExtender ID="calExtender312" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                PopupButtonID="btnDate33" PopupPosition="BottomLeft" TargetControlID="txtMrpDateAdd">
                                                                                                            </cc1:CalendarExtender>
                                                                                                        </td>
                                                                                                        
                                                                                                        <td style="width:10%" align="left">
                                                                                                            <asp:ImageButton ID="btnDate33" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                Width="20px" runat="server" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:3px">
                                                                                                    </tr>
                                                                                                    <tr style="height: 30px" class="tblLeft">
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            VAT (%) *
                                                                                                            <asp:RequiredFieldValidator ID="rvVATAdd" runat="server" ControlToValidate="txtVATAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="VAT is mandatory"></asp:RequiredFieldValidator>
                                                                                                            <asp:RangeValidator ID="cvVATAdd" runat="server" ControlToValidate="txtVATAdd" Display="Dynamic"
                                                                                                                Text="*" EnableClientScript="True" MaximumValue="100" Type="Double" MinimumValue="0"
                                                                                                                ErrorMessage="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtVATAdd" runat="server" Text='<%# Bind("VAT") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Sell Discount (%) *
                                                                                                            <asp:RequiredFieldValidator ID="rvDiscDis1" runat="server" ControlToValidate="txtDiscountAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Discount is mandatory"></asp:RequiredFieldValidator>
                                                                                                            <asp:RangeValidator ID="RangeValidator33" runat="server" ControlToValidate="txtDiscountAdd"
                                                                                                                Display="Dynamic" MaximumValue="100" Type="Double" MinimumValue="0" Text="*"
                                                                                                                ErrorMessage="Discount cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtDiscountAdd" runat="server" Text="0" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        
                                                                                                        <td style="width: 10%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:3px">
                                                                                                    </tr>
                                                                                                    <tr style="height: 30px" class="tblLeft" runat="server" id="rowDealerAdd">
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Dealer Rate *
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDealerRateAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Dealer Rate is mandatory"></asp:RequiredFieldValidator>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FTDUnitRAdd" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="txtDealerRateAdd" ValidChars="." />
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtDealerRateAdd" runat="server" Text=""
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Dealer Rate Effective date
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtDpDateAdd" runat="server" Text='<%# Bind("DPEffDate","{0:dd/MM/yyyy}") %>'
                                                                                                                CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                            <cc1:CalendarExtender ID="CalendarExtender123" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                PopupButtonID="ImageButton123" PopupPosition="BottomLeft" TargetControlID="txtDpDateAdd">
                                                                                                            </cc1:CalendarExtender>
                                                                                                        </td>
                                                                                                        
                                                                                                        <td style="width: 10%" align="left">
                                                                                                            <asp:ImageButton ID="ImageButton123" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                Width="20px" runat="server" />
                                                                                                        </td>
                                                                                            
                                                                                                    </tr>
                                                                                                    <tr style="height:3px">
                                                                                                    </tr>
                                                                                                    <tr style="height: 30px;" class="tblLeft">
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            
                                                                                                            NLC *
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNLCAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="NLC is mandatory"></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtNLCAdd" runat="server" Text="" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            NLC Effective date *
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtNLCDateAdd" runat="server" Text='<%# Bind("NLCEffDate","{0:dd/MM/yyyy}") %>'
                                                                                                                CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                            <cc1:CalendarExtender ID="CalendarExtender22" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                PopupButtonID="ImageButton22" PopupPosition="BottomLeft" TargetControlID="txtNLCDateAdd">
                                                                                                            </cc1:CalendarExtender>
                                                                                                        </td>
                                                                                                        
                                                                                                        <td style="width: 10%" align="left">
                                                                                                            <asp:ImageButton ID="ImageButton22" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                Width="20px" runat="server" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:3px">
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Reorder Level *
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtROLAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Stock Level is mandatory"></asp:RequiredFieldValidator>
                                                                                                            <cc1:FilteredTextBoxExtender ID="fltReorderAdd" runat="server" FilterType="Numbers"
                                                                                                                TargetControlID="txtROLAdd" />
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtROLAdd" runat="server" Text="1" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Dealer Discount (%) *
                                                                                                            <asp:RangeValidator ID="dvDisAdd" runat="server" ControlToValidate="txtDealerDiscountAdd"
                                                                                                                Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                                MinimumValue="0" Text="Discount cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtDealerDiscountAdd" runat="server" Text="0"
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 10%">
                                                                                                            
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:3px">
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            OutDated *
                                                                                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddCategoryAdd"
                                                                                                                Display="Dynamic" ErrorMessage="Outdated is Mandatory" Operator="GreaterThan"
                                                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%;" class="ControlDrpBorder">
                                                                                                            <asp:DropDownList ID="drpOutdatedAdd" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7" SelectedValue='<%# Bind("Outdated") %>'
                                                                                                                AppendDataBoundItems="True" EnableTheming="False" style="border: 1px solid #e7e7e7" height="26px">
                                                                                                                <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                                                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Allowed Price % *
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAllowedPriceAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Allowed Price is mandatory"></asp:RequiredFieldValidator>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="txtAllowedPriceAdd" ValidChars="." />
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtAllowedPriceAdd" runat="server" Text="0"
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 10%">
                                                                                                            
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:3px">
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            IsActive *
                                                                                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="drpIsActiveAdd"
                                                                                                                Display="Dynamic" ErrorMessage="IsActive is Mandatory" Operator="GreaterThan"
                                                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%;" class="ControlDrpBorder">
                                                                                                            <asp:DropDownList ID="drpIsActiveAdd" TabIndex="10" runat="server" BackColor = "#e7e7e7" CssClass="drpDownListMedium"
                                                                                                                Width="100%" style="border: 1px solid #e7e7e7" height="26px" SelectedValue='<%# Bind("IsActive") %>'>
                                                                                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                                                <asp:ListItem Text="YES" Value="YES" Selected="True"></asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            
                                                                                                        </td>
                                                                                                        <td style="width: 25%">
                                                                                                           
                                                                                                        </td>
                                                                                                        <td style="width: 10%">
                                                                                                            
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="display: none">
                                                                                                        <td style="width: 20%;">
                                                                                                            <asp:DropDownList ID="drpblockadd" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7" SelectedValue='<%# Bind("block") %>'
                                                                                                                AppendDataBoundItems="True" EnableTheming="False" Visible="false" style="border: 1px solid #e7e7e7" height="26px">
                                                                                                                <asp:ListItem Value="N">No</asp:ListItem>
                                                                                                                <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtUnitAdd" runat="server" Text='<%# Bind("Unit") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:ObjectDataSource ID="srcCategoryAdd" runat="server" SelectMethod="ListCategory"
                                                                                                                TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                                                                <SelectParameters>
                                                                                                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                                </SelectParameters>
                                                                                                            </asp:ObjectDataSource>
                                                                                                        </td>
                                                                                                        <td align="right" style="width: 25%">
                                                                                                            <asp:ObjectDataSource ID="srcBrandAdd" runat="server" SelectMethod="ListBrandMaster"
                                                                                                                TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                                                                <SelectParameters>
                                                                                                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                                </SelectParameters>
                                                                                                            </asp:ObjectDataSource>
                                                                                                        </td>
                                                                                                        <td style="width: 10%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Rate Details">
                                                                                <ContentTemplate>
                                                                                    <table width="700px" cellpadding="3" cellspacing="1" align="center" >
                                                                                        <tr>
                                                                                            <td style="width:100%">
                                                                                                <div id="div" runat="server" style="height:330px; overflow:scroll">
                                                                                                    <rwg:BulkEditGridView ID="GrdViewItems" AutoGenerateColumns="False" BorderWidth="1px"
                                                                                                        BorderStyle="Solid" GridLines="Both" runat="server" CssClass="someClass" OnRowDataBound="GrdViewItems_RowDataBound" 
                                                                                                        Width="100%">
                                                                                                        <RowStyle CssClass="dataRow" />
                                                                                                        <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                        <AlternatingRowStyle CssClass="altRow" />
                                                                                                        <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                        <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                        <FooterStyle CssClass="dataRow" />
                                                                                                        <Columns>
                                                                                                            <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-BorderColor="Gray"  ReadOnly="true" ApplyFormatInEditMode="false" HeaderStyle-Width="10%" />
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Price Type" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtPriceName" runat="server" Width="85%"  Text='<%# Eval("PriceName")%>'
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Price" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtPrice" runat="server" Width="85%"  Text='<%# Eval("Price")%>'
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Effective Date" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtEffDate" runat="server" Width="85%"  Text='<%# Eval("EffDate")%>'
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Discount" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtDiscount" runat="server" Width="85%"  Text='<%# Eval("Discount")%>'
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                    </rwg:BulkEditGridView>
                                                                                                </div>
                                                                                            </td>
                                                                                         </tr>    
                                                                                    </table>
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="tabInsRates" runat="server" HeaderText="Additional Details">
                                                                                <ContentTemplate>
                                                                                    <table width="700px" cellpadding="3" cellspacing="1" align="center" >
                                                                                        
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                CST (%)
                                                                                                <asp:RangeValidator ID="RangeValidator2Add" runat="server" ControlToValidate="txtCSTAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                    MinimumValue="0" Text="CST cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtCSTAdd" runat="server" Text='<%# Bind("CST") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Commodity Code
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtCommCodeAdd" runat="server" Text='<%# Bind("CommodityCode") %>'
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            
                                                                                            <td  style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Unit of Measure
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlDrpBorder">
                                                                                                <asp:DropDownList ID="drpMeasureAdd" runat="server" DataTextField="Unit" DataValueField="Unit" Width="100%" style="border: 1px solid #e7e7e7" height="26px"
                                                                                                    CssClass="drpDownListMedium" BackColor = "#e7e7e7" DataSourceID="srcUnitMntAdd" SelectedValue='<%# Bind("Measure_Unit") %>'
                                                                                                    AppendDataBoundItems="True">
                                                                                                    <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Measure</asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Barcode
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBarcodeAdd" runat="server" Text='<%# Bind("Barcode") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                             <td  style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <%--<tr style="height:3px">
                                                                                    </tr>--%>
                                                                                        <tr style="height: 30px;" class="tblLeft" runat="server" visible="false">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Buy Unit Rate
                                                                                                <cc1:FilteredTextBoxExtender ID="FTBRadteAdd" runat="server" FilterType="Custom, Numbers"
                                                                                                    TargetControlID="txtBuyRateAdd" ValidChars="." />
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBuyRateAdd" runat="server" Text='<%# Bind("BuyRate") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Buy VAT (%)
                                                                                                <asp:RangeValidator ID="rvBuyVATAdd" runat="server" ControlToValidate="txtBuyVATAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                    MinimumValue="0" Text="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBuyVATAdd" runat="server" Text='<%# Bind("BuyVAT") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                             <td  style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Buy Discount (%)
                                                                                                <asp:RangeValidator ID="RVBusDisAdd" runat="server" ControlToValidate="txtBuyDiscountAdd"
                                                                                                    Display="Dynamic" MaximumValue="100" Type="Double" MinimumValue="0" Text="*"
                                                                                                    ErrorMessage="Discount cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBuyDiscountAdd" runat="server" Text='<%# Bind("BuyDiscount") %>'
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Executive Commission
                                                                                                <asp:CompareValidator ID="rvExecCommAdd" runat="server" ControlToValidate="txtExecutiveCommissionAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" Type="Double" Operator="DataTypeCheck"
                                                                                                    Text="Commission should be Numeric value"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtExecutiveCommissionAdd" runat="server" Text='<%# Bind("ExecutiveCommission") %>'
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                             <td  style="width: 10%">
                                                                                            </td>
                                                                                    
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        
                                                                                        <tr style="height: 30px; display: none" class="tblLeft" id="rowDealerAdd1" runat="server">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Dealer VAT (%)
                                                                                                <asp:RangeValidator ID="RangeValidator1Add" runat="server" ControlToValidate="txtDealerVATAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                    MinimumValue="0" Text="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtDealerVATAdd" runat="server" Text='<%# Bind("DealerVAT") %>'
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%">
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtproductlevel" runat="server" Text='<%# Bind("Productlevel") %>' Visible="false"
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td  style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                        </tr>
                                                                                        <tr style="display: none">
                                                                                            <td style="width: 20%">
                                                                                                <asp:ObjectDataSource ID="srcUnitMntAdd" runat="server" SelectMethod="ListMeasurementUnits"
                                                                                                    TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                                                    <SelectParameters>
                                                                                                        <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                    </SelectParameters>
                                                                                                </asp:ObjectDataSource>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBuyUnitAdd" runat="server" Text='<%# Bind("BuyUnit") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%">
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                            </td>
                                                                                             <td  style="width: 10%">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                        </cc1:TabContainer>
                                                                        </div>
                                                                        <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" ShowMessageBox="true"
                                                                            ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                            Font-Size="12" runat="server" />
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 30%">
                                                                            </td>
                                                                
                                                                            <td style="width: 20%" align="center">                                                                               
                                                                                 <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" OnClientClick="javascript:CheckReorderLevel();"
                                                                                    CssClass="savebutton1231" EnableTheming="false" SkinID="skinBtnSave" OnClick="InsertButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <td style="width: 20%" align="center">
                                                                                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                    CssClass="cancelbutton6" EnableTheming="false" SkinID="skinBtnCancel" OnClick="InsertCancelButton_Click">
                                                                                </asp:Button>
                                                                            </td>
                                                                            <%--<td style="width: 25%"  align="left">
                                                                                <asp:Button ID="cmdcat" runat="server" Text="" EnableTheming="false" cssclass="NewCat" OnClick="cmdcat_click" />
                                                                            </td>--%>
                                                                
                                                                            <td style="width: 30%">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                
                                                            </tr>
                                                        </table>
                                                        
                                                    </div>
                                                </InsertItemTemplate>
                                            </asp:FormView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%--<div>
                                <table>
                                    <tr>
                                        <td align="left">
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
                                                                                        Rate History
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
                                                                        
                                                                        <div align="center">
                                                                            <asp:Button ID="CloseWindow" runat="server"
                                                                                CssClass="CloseWindow6" EnableTheming="false" OnClick="CloseWindow_Click">
                                                                            </asp:Button>
                                                                        </div>
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
                        <asp:Panel ID="pnlRole" runat="server" Visible="false">
                            <table width="27%" cellpadding="3" cellspacing="2" style="font-size: 11px" class="left">
                                <tr>
                                    <td colspan="3">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdRole" runat="server" PageSize="5" AutoGenerateColumns="False"
                                                    AllowPaging="true" OnPageIndexChanging="grdRole_PageIndexChanging">
                                                    <Columns>
                                                        <asp:BoundField DataField="Qty_Bought" HeaderText="Quantity Bought" />
                                                        <asp:BoundField DataField="Qty_Available" HeaderText="Quantity Available" />
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                           
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%; text-align: left">
                        <table width="100%" style="margin: -3px 0px 0px 0px;">
                                <tr style="width: 100%">
                                    <td>
                        <div class="mainGridHold" id="searchGrid">
                            <asp:GridView ID="GrdViewProduct" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewProduct_RowCreated" Width="100.4%" DataSourceID="GridSource" CssClass="someClass"
                                AllowPaging="True" DataKeyNames="ItemCode" EmptyDataText="No Products found." OnRowDeleting="GrdViewProduct_RowDeleting"
                                OnRowCommand="GrdViewProduct_RowCommand" OnRowDataBound="GrdViewProduct_RowDataBound"
                                OnSelectedIndexChanged="GrdViewProduct_SelectedIndexChanged">
                                <Columns>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Product Code"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="Model" HeaderText="Model"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="ProductDesc" HeaderText="Brand"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="Stock" HeaderText="In Stock"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="Rate" HeaderText="MRP"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                            <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Product?"
                                                runat="server">
                                            </cc1:ConfirmButtonExtender>
                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server">
                                            </asp:ImageButton>
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                            <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ItemCode") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    <table style=" border-color:white">
                                        <tr style=" border-color:white">
                                            <td style=" border-color:white">
                                                Goto Page
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:DropDownList ID="ddlPageSelector" BackColor="#e7e7e7" runat="server" AutoPostBack="true" Width="70px" Height="25px" style="border:1px solid Gray">
                                                </asp:DropDownList>
                                            </td>
                                            <td style=" border-color:white;Width:5px">
                                            
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
                        </td>
                        </tr>
                        </table>
                    </td>
                </tr>
                <tr style="width:100%">
                    <td style="width: 918px" align="left">
                        <asp:ObjectDataSource ID="GridSource" runat="server" SelectMethod="ListProducts"
                            TypeName="BusinessLogic" DeleteMethod="DeleteProduct" OnDeleting="GridSource_Deleting">
                            <DeleteParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ItemCode" Type="String" />
                                <asp:Parameter Name="Username" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="frmSource" runat="server" SelectMethod="GetProductForId"
                            TypeName="BusinessLogic" OnUpdating="frmSource_Updating" OnInserting="frmSource_Inserting"
                            UpdateMethod="UpdateProduct" InsertMethod="InsertProduct">
                            <UpdateParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ItemCode" Type="String" />
                                <asp:Parameter Name="ProductName" Type="String" />
                                <asp:Parameter Name="Model" Type="String" />
                                <asp:Parameter Name="CategoryID" Type="Int32" />
                                <asp:Parameter Name="ProductDesc" Type="String" />
                                <asp:Parameter Name="ROL" Type="Int32" />
                                <asp:Parameter Name="Stock" Type="Double" />
                                <asp:Parameter Name="Rate" Type="Double" />
                                <asp:Parameter Name="Unit" Type="Int32" />
                                <asp:Parameter Name="VAT" Type="Double" />
                                <asp:Parameter Name="Discount" Type="Int32" />
                                <asp:Parameter Name="BuyUnit" Type="Int32" />
                                <asp:Parameter Name="BuyRate" Type="Double" />
                                <asp:Parameter Name="BuyVAT" Type="Double" />
                                <asp:Parameter Name="CST" Type="Double" />
                                <asp:Parameter Name="BuyDiscount" Type="Int32" />
                                <asp:Parameter Name="DealerUnit" Type="Int32" />
                                <asp:Parameter Name="DealerRate" Type="Double" />
                                <asp:Parameter Name="DealerVAT" Type="Double" />
                                <asp:Parameter Name="DealerDiscount" Type="Int32" />
                                <asp:Parameter Name="Complex" Type="String" />
                                <asp:Parameter Name="Measure_Unit" Type="String" />
                                <asp:Parameter Name="Accept_Role" Type="String" />
                                <asp:Parameter Name="Barcode" Type="String" />
                                <asp:Parameter Name="CommodityCode" Type="String" />
                                <asp:Parameter Name="NLC" Type="Double" />
                                <asp:Parameter Name="block" Type="String" />
                                <asp:Parameter Name="Productlevel" Type="Int32" />
                                <asp:Parameter Name="MRPEffDate" Type="DateTime" />
                                <asp:Parameter Name="DPEffDate" Type="DateTime" />
                                <asp:Parameter Name="NLCEffDate" Type="DateTime" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Outdated" Type="String" />
                                <asp:Parameter Name="Deviation" Type="Int32" />
                                <asp:Parameter Name="IsActive" Type="String" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                <asp:Parameter Name="ItemCode" Type="String" />
                                <asp:Parameter Name="ProductName" Type="String" />
                                <asp:Parameter Name="Model" Type="String" />
                                <asp:Parameter Name="CategoryID" Type="Int32" />
                                <asp:Parameter Name="ProductDesc" Type="String" />
                                <asp:Parameter Name="ROL" Type="Int32" />
                                <asp:Parameter Name="Stock" Type="Double" />
                                <asp:Parameter Name="Rate" Type="Double" />
                                <asp:Parameter Name="Unit" Type="Int32" />
                                <asp:Parameter Name="VAT" Type="Double" />
                                <asp:Parameter Name="Discount" Type="Int32" />
                                <asp:Parameter Name="BuyUnit" Type="Int32" />
                                <asp:Parameter Name="BuyRate" Type="Double" />
                                <asp:Parameter Name="BuyVAT" Type="Double" />
                                <asp:Parameter Name="CST" Type="Double" />
                                <asp:Parameter Name="BuyDiscount" Type="Int32" />
                                <asp:Parameter Name="DealerUnit" Type="Int32" />
                                <asp:Parameter Name="DealerRate" Type="Double" />
                                <asp:Parameter Name="DealerVAT" Type="Double" />
                                <asp:Parameter Name="DealerDiscount" Type="Int32" />
                                <asp:Parameter Name="Complex" Type="String" />
                                <asp:Parameter Name="Measure_Unit" Type="String" />
                                <asp:Parameter Name="Accept_Role" Type="String" />
                                <asp:Parameter Name="Barcode" Type="String" />
                                <asp:Parameter Name="CommodityCode" Type="String" />
                                <asp:Parameter Name="NLC" Type="Double" />
                                <asp:Parameter Name="block" Type="String" />
                                <asp:Parameter Name="Productlevel" Type="Int32" />
                                <asp:Parameter Name="MRPEffDate" Type="DateTime" />
                                <asp:Parameter Name="DPEffDate" Type="DateTime" />
                                <asp:Parameter Name="NLCEffDate" Type="DateTime" />
                                <asp:Parameter Name="Username" Type="String" />
                                <asp:Parameter Name="Outdated" Type="String" />
                                <asp:Parameter Name="Deviation" Type="Int32" />
                                <asp:Parameter Name="IsActive" Type="String" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="GrdViewProduct" Name="ItemCode" PropertyName="SelectedValue"
                                    Type="String" ConvertEmptyStringToNull="False" DefaultValue="D01" />
                                <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                
            </table>
            <table align="center" style="width:100%">
                <tr>
                    <td  style="width:10%">
                        
                    </td>
                    <td style="width:15%">
                        <asp:Button ID="BlkAdd" runat="server"  OnClientClick="window.open('BulkAddition.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');" CssClass="bulkaddition"
                                    EnableTheming="false" Text=""></asp:Button>
                    </td>
                    <td  style="width:18%">
                        <asp:Button ID="BlkUpd" runat="server" OnClick="BlkUpd_Click" SkinID="skinButtonCol2"  Width="80%"
                                    Text="Bulk Updation By Screen"></asp:Button>
                    </td>
                    <td style="width:18%">
                    <asp:Button ID="Button4" runat="server" Text="Bulk Updation By Excel" OnClientClick="window.open('BulkProductUpdation.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=400,width=480,left=425,top=150, scrollbars=yes');"
                                                SkinID="skinButtonCol2"  Width="80%"></asp:Button>
                    </td>
                    <td  style="width:15%">
                        <asp:Button ID="cmdhistory" runat="server" Text="" EnableTheming="false" CausesValidation="False" cssclass="ShowHistory"  OnClientClick="window.open('ReportExcelProductsHistory.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"/>    
                    </td>
                    <td  style="width:15%">
                        <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6" OnClientClick="window.open('ReportExcelProducts.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                                EnableTheming="false"></asp:Button>
                    </td>
                    <td  style="width:10%">
                        
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
