<%@ Page Title="Inventory > ProductMaster" Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="ProdMaster.aspx.cs" Inherits="ProdMaster" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="RealWorld.Grids" Namespace="RealWorld.Grids" TagPrefix="rwg" %>
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


        function pageLoad() {
            //  get the behavior associated with the tab control
            var tabContainer = $find('ctl00_cplhControlPanel_tablInsertControl');

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
                                        <td style="width: 30%; font-size: 22px; color: white;">Manage Products
                                        </td>
                                        <td style="width: 2%">
                                            
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
                                                    <asp:ListItem Value="ItemCode">Product Code</asp:ListItem>
                                                        <asp:ListItem Value="ProductName">Product Name</asp:ListItem>
                                                        <asp:ListItem Value="Model">Model</asp:ListItem>
                                                        <asp:ListItem Value="Brand">Brand</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="width: 13%; text-align: left">
                                            <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click" CausesValidation="false"
                                                CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" />
                                        </td>
                                        <td style="width: 20%" class="tblLeftNoPad">
                                            <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false"  CausesValidation="false" Text="" CssClass="ClearFilter6" />
                                        </td>
                                        <td style="width: 10%"></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <asp:ValidationSummary ID="VS" runat="server" Font-Names="'Trebuchet MS'"
                            Font-Size="12pt" HeaderText="Validation Messages" ShowMessageBox="True"
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
                                                        <table cellpadding="1" cellspacing="1" style="border: 1px solid #86b2d1; width: 100%;">
                                                            <tr>
                                                                <td colspan="5" class="headerPopUp">
                                                                    <%--Product Master--%>
                                                                    <asp:Label ID="title1" runat="server">

                                                                    </asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                    <div style="text-align: left">
                                                                        <cc1:TabContainer ID="tablInsertControl" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                                            <cc1:TabPanel ID="tabInsProdMaster"  runat="server" HeaderText="Product Details">
                                                                                <ContentTemplate>
                                                                                    
                                                                                                <table style="width: 800px; border: 0px solid #5078B3" align="center" cellpadding="3" cellspacing="1">
                                                                                                    <tr style="height: 30px" class="tblLeft">
                                                                                                        <td style="width: 20%" class="ControlLabelNew">
                                                                                                            Product ID *
                                                                                                            <asp:RequiredFieldValidator ID="rvItemCodeAdd" runat="server" ControlToValidate="txtItemCodeAdd"
                                                                                                                Text="*" Display="Dynamic" ValidationGroup="salesval" EnableClientScript="True" ErrorMessage="ProductCode is mandatory"></asp:RequiredFieldValidator>
                                                                                                            <cc1:FilteredTextBoxExtender ID="fltItemCodeAdd" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers"
                                                                                                                TargetControlID="txtItemCodeAdd" />
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtItemCodeAdd" Enabled="true" runat="server"
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 28%" class="ControlLabelNew">
                                                                                                            Quantity in Stock
                                                                                                            <cc1:FilteredTextBoxExtender ID="FTBStockAdd" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="txtStockAdd" ValidChars="." />
                                                                                                            <asp:CompareValidator ID="rvStockAdd" runat="server" ControlToValidate="txtStockAdd"
                                                                                                                Display="Dynamic" EnableClientScript="True" Type="Integer" Operator="DataTypeCheck"
                                                                                                                Text="*" ErrorMessage="Stock should be a Number"></asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtStockAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width:2%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:2px">
                                                                                    </tr>
                                                                                                    <tr style="height: 30px" class="tblLeft">
                                                                                                        <td style="width: 20%" class="ControlLabelNew">
                                                                                                            Name of Product *
                                                                                                            <asp:RequiredFieldValidator ID="rvBDateAdd" runat="server" ControlToValidate="txtItemNameAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ValidationGroup="salesval" ErrorMessage="Product Name is mandatory"></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtItemNameAdd" runat="server" Text='<%# Bind("ProductName") %>'
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 28%" class="ControlLabelNew">
                                                                                                            Unit of Measure
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                                            <asp:DropDownList ID="drpMeasureAdd" runat="server" DataTextField="Unit" DataValueField="Unit" Width="100%" style="border: 1px solid #e7e7e7" height="26px"
                                                                                                                CssClass="drpDownListMedium" BackColor = "#e7e7e7" DataSourceID="srcUnitMntAdd"
                                                                                                                AppendDataBoundItems="True">
                                                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Measure</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:2px">
                                                                                    </tr>
                                                                                                    <tr style="height: 30px" class="tblLeft">
                                                                                                        <td style="width: 20%" class="ControlLabelNew">
                                                                                                            Name of Brand *
                                                                                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtProdDescAdd"
                                                                                                                Display="Dynamic" ErrorMessage="Brand is Mandatory" ValidationGroup="salesval" Operator="GreaterThan"
                                                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                                            <%--<asp:TextBox ID="txtProdDescAdd" runat="server" Text='<%# Bind("ProductDesc") %>'
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>--%>
                                                                                                            <asp:DropDownList ID="txtProdDescAdd" runat="server" DataTextField="BrandName" Width="100%" BackColor = "#e7e7e7" CssClass="drpDownListMedium"
                                                                                                                DataValueField="BrandName" style="border: 1px solid #e7e7e7" height="26px"
                                                                                                                EnableTheming="false"
                                                                                                                AppendDataBoundItems="True">
                                                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select BrandName</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 28%" class="ControlLabelNew">
                                                                                                            VAT (%) *
                                                                                                            <asp:RequiredFieldValidator ID="rvVATAdd" runat="server" ControlToValidate="txtVATAdd" ValidationGroup="salesval"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="VAT is mandatory"></asp:RequiredFieldValidator>
                                                                                                            <asp:RangeValidator ID="cvVATAdd" runat="server" ControlToValidate="txtVATAdd" Display="Dynamic"
                                                                                                                Text="*" EnableClientScript="True" MaximumValue="100" Type="Double" MinimumValue="0" ValidationGroup="salesval"
                                                                                                                ErrorMessage="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                            
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtVATAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                            
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height: 30px; display: none" class="tblLeft">
                                                                                                        <td style="width: 20%">
                                                                                                            <asp:DropDownList ID="drpRoleTypeAdd" runat="server" Width="30%" CssClass="drpDownListMedium" BackColor = "#e7e7e7"
                                                                                                                AppendDataBoundItems="True" EnableTheming="False" style="border: 1px solid #e7e7e7" height="26px">
                                                                                                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                                                                <asp:ListItem Value="YES">YES</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtDealerUnitAdd" runat="server" Text='<%# Bind("DealerUnit") %>'
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 28%" class="ControlLabelNew">
                                                                                                            Complex
                                                                                                        </td>
                                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                                            <asp:DropDownList ID="drpComplexAdd" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7"
                                                                                                                AppendDataBoundItems="True" EnableTheming="False" style="border: 1px solid #e7e7e7" height="26px">
                                                                                                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                                                                <asp:ListItem Value="">NO</asp:ListItem>
                                                                                                                <asp:ListItem Value="YES">YES</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <%--<tr style="height:2px">
                                                                                                    </tr>--%>
                                                                                                    <tr style="height: 30px" class="tblLeft" runat="server" visible="false">
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            MRP *
                                                                                                            <cc1:FilteredTextBoxExtender ID="FTBXBRAdd" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="txtUnitRateAdd" ValidChars="." />
                                                                                                            <asp:RequiredFieldValidator ID="rvSRateAdd" runat="server" ControlToValidate="txtUnitRateAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Rate is mandatory"></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtUnitRateAdd" runat="server" Text="0" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 28%" class="ControlLabel">
                                                                                                            MRP Effective date
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator786" runat="server" ControlToValidate="txtMrpDateAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="MRP Effective date is mandatory"></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtMrpDateAdd" runat="server"
                                                                                                                CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                            <cc1:CalendarExtender ID="calExtender312" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                PopupButtonID="btnDate33" PopupPosition="BottomLeft" TargetControlID="txtMrpDateAdd">
                                                                                                            </cc1:CalendarExtender>
                                                                                                        </td>
                                                                                                        
                                                                                                        <td style="width:2%" align="left">
                                                                                                            <asp:ImageButton ID="btnDate33" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                Width="20px" runat="server" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:2px">
                                                                                                    </tr>
                                                                                                    <tr style="height: 30px" class="tblLeft">
                                                                                                        <td style="width: 20%" class="ControlLabelNew">
                                                                                                            Name of Category *
                                                                                                            <asp:CompareValidator ID="cvCatergoryAdd" runat="server" ControlToValidate="ddCategoryAdd"
                                                                                                                Display="Dynamic" ErrorMessage="Category is Mandatory" ValidationGroup="salesval" Operator="GreaterThan"
                                                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                                            <asp:DropDownList ID="ddCategoryAdd" runat="server" DataTextField="CategoryName" Width="100%" BackColor = "#e7e7e7" CssClass="drpDownListMedium"
                                                                                                                DataValueField="CategoryID" style="border: 1px solid #e7e7e7" height="26px"
                                                                                                                EnableTheming="false"
                                                                                                                AppendDataBoundItems="True">
                                                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Category</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 28%" class="ControlLabelNew">
                                                                                                            Reorder Level *
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtROLAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Stock Level is mandatory"></asp:RequiredFieldValidator>
                                                                                                            <cc1:FilteredTextBoxExtender ID="fltReorderAdd" runat="server" FilterType="Numbers"
                                                                                                                TargetControlID="txtROLAdd" />
                                                                                                            
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtROLAdd" runat="server" Text="1" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                            
                                                                                                        </td>
                                                                                                        
                                                                                                        <td style="width: 2%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:2px">
                                                                                                    </tr>
                                                                                                    <tr style="height: 30px" class="tblLeft" runat="server" id="rowDealerAdd"  visible="false">
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            Dealer Rate *
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDealerRateAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Dealer Rate is mandatory"></asp:RequiredFieldValidator>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FTDUnitRAdd" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="txtDealerRateAdd" ValidChars="." />
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtDealerRateAdd" runat="server" Text="0"
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 28%" class="ControlLabel">
                                                                                                            Dealer Rate Effective date
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtDpDateAdd" runat="server"
                                                                                                                CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                            <cc1:CalendarExtender ID="CalendarExtender123" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                PopupButtonID="ImageButton123" PopupPosition="BottomLeft" TargetControlID="txtDpDateAdd">
                                                                                                            </cc1:CalendarExtender>
                                                                                                        </td>
                                                                                                        
                                                                                                        <td style="width: 2%" align="left">
                                                                                                            <asp:ImageButton ID="ImageButton123" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                Width="20px" runat="server" />
                                                                                                        </td>
                                                                                            
                                                                                                    </tr>
                                                                                                    <%--<tr style="height:2px">
                                                                                                    </tr>--%>
                                                                                                    <tr style="height: 30px;" class="tblLeft" runat="server" visible="false">
                                                                                                        <td style="width: 20%" class="ControlLabel">
                                                                                                            
                                                                                                            NLC *
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNLCAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="NLC is mandatory"></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtNLCAdd" runat="server"  Text="0" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 28%" class="ControlLabel">
                                                                                                            NLC Effective date *
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtNLCDateAdd" runat="server"
                                                                                                                CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                            <cc1:CalendarExtender ID="CalendarExtender22" runat="server" Animated="true" Format="dd/MM/yyyy"
                                                                                                                PopupButtonID="ImageButton22" PopupPosition="BottomLeft" TargetControlID="txtNLCDateAdd">
                                                                                                            </cc1:CalendarExtender>
                                                                                                        </td>
                                                                                                        
                                                                                                        <td style="width: 2%" align="left">
                                                                                                            <asp:ImageButton ID="ImageButton22" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="false"
                                                                                                                Width="20px" runat="server" />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <%--<tr style="height:2px">
                                                                                                    </tr>--%>
                                                                                                    <tr runat="server" visible="false">
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
                                                                                                        <td style="width: 28%" class="ControlLabel">
                                                                                                            Dealer Discount (%) *
                                                                                                            <asp:RangeValidator ID="dvDisAdd" runat="server" ControlToValidate="txtDealerDiscountAdd"
                                                                                                                Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                                MinimumValue="0" Text="Discount cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtDealerDiscountAdd" runat="server" Text="0"
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                            
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <%--<tr style="height:2px">
                                                                                                    </tr>--%>
                                                                                                    <tr>
                                                                                                        <td style="width: 20%" class="ControlLabelNew">
                                                                                                            Name of Model *
                                                                                                            <asp:RequiredFieldValidator ID="rvModelAdd" runat="server" ControlToValidate="txtModelAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ValidationGroup="salesval" ErrorMessage="Model is mandatory"></asp:RequiredFieldValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%;" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtModelAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 28%" class="ControlLabelNew">
                                                                                                            Allowed Price Deviation % *
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAllowedPriceAdd"
                                                                                                                Text="*" Display="Dynamic" EnableClientScript="True" ErrorMessage="Allowed Price is mandatory"></asp:RequiredFieldValidator>
                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="txtAllowedPriceAdd" ValidChars="." />
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtAllowedPriceAdd" runat="server" Text="0"
                                                                                                                SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                            
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="height:2px">
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td style="width: 20%" class="ControlLabelNew">
                                                                                                            IsActive *
                                                                                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="drpIsActiveAdd"
                                                                                                                Display="Dynamic" ErrorMessage="IsActive is Mandatory" Operator="GreaterThan"
                                                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%;" class="ControlDrpBorder">
                                                                                                            <asp:DropDownList ID="drpIsActiveAdd" runat="server" BackColor = "#e7e7e7" CssClass="drpDownListMedium"
                                                                                                                Width="100%" style="border: 1px solid #e7e7e7" height="26px">
                                                                                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                                                <asp:ListItem Text="YES" Value="YES" Selected="True"></asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 28%" class="ControlLabelNew">
                                                                                                            Outdated? *
                                                                                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddCategoryAdd"
                                                                                                                Display="Dynamic" ErrorMessage="Outdated is Mandatory" Operator="GreaterThan"
                                                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlDrpBorder">
                                                                                                           <asp:DropDownList ID="drpOutdatedAdd" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7" SelectedValue='<%# Bind("Outdated") %>'
                                                                                                                AppendDataBoundItems="True" EnableTheming="False" style="border: 1px solid #e7e7e7" height="26px">
                                                                                                                <asp:ListItem Value="N" Selected="True">NO</asp:ListItem>
                                                                                                                <asp:ListItem Value="Y">YES</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                            
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr style="display: none">
                                                                                                        <td style="width: 20%;">
                                                                                                            <asp:DropDownList ID="drpblockadd" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor = "#e7e7e7" SelectedValue='<%# Bind("block") %>'
                                                                                                                AppendDataBoundItems="True" EnableTheming="False" Visible="false" style="border: 1px solid #e7e7e7" height="26px">
                                                                                                                <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                                                                <asp:ListItem Value="YES">YES</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                            <asp:TextBox ID="txtUnitAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td style="width: 28%">
                                                                                                            <asp:ObjectDataSource ID="srcCategoryAdd" runat="server" SelectMethod="ListCategory"
                                                                                                                TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                                                                <SelectParameters>
                                                                                                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                                    <asp:CookieParameter Name="method" CookieName="" Type="String" />
                                                                                                                </SelectParameters>
                                                                                                            </asp:ObjectDataSource>
                                                                                                        </td>
                                                                                                        <td align="right" style="width: 25%">
                                                                                                            <asp:ObjectDataSource ID="srcUnitMntAdd" runat="server" SelectMethod="ListMeasurementUnits"
                                                                                                                TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                                                                <SelectParameters>
                                                                                                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                                </SelectParameters>
                                                                                                            </asp:ObjectDataSource>
                                                                                                            <asp:ObjectDataSource ID="srcBrandAdd" runat="server" SelectMethod="ListBrandMaster"
                                                                                                                TypeName="BusinessLogic" OldValuesParameterFormatString="original_{0}">
                                                                                                                <SelectParameters>
                                                                                                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                                                                </SelectParameters>
                                                                                                            </asp:ObjectDataSource>
                                                                                                        </td>
                                                                                                        <td style="width: 2%">
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Price Details">
                                                                                <ContentTemplate>
                                                                                    <table width="800px" cellpadding="1" cellspacing="1" align="center" >
                                                                                        <tr>
                                                                                            <td style="width:100%">
                                                                                                <div id="div" runat="server" style="height:190px; overflow:scroll">
                                                                                                    <rwg:BulkEditGridView ID="GrdViewItems" AutoGenerateColumns="False"
                                                                                                        GridLines="None" runat="server" OnRowDataBound="GrdViewItems_RowDataBound" 
                                                                                                        Width="95%">
                                                                                                        <%--<RowStyle CssClass="dataRow" />
                                                                                                        <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                        <AlternatingRowStyle CssClass="altRow" />
                                                                                                        <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                        <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                        <FooterStyle CssClass="dataRow" />--%>
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="7%">
                                                                                                                <ItemTemplate>
                                                                                                                    <%--<asp:TextBox ID="txtPriceName" runat="server" Width="90%"  Text='<%# Eval("PriceName")%>' Enabled="false" Height="26px"
                                                                                                                        ></asp:TextBox>--%>
                                                                                                                    <asp:Label ID="txtSNO" runat="server" Text='<%# Eval("Row")%>' Font-Bold="true">

                                                                                                                    </asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" Visible="false" HeaderStyle-ForeColor="Black" HeaderText="ID" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtId" runat="server" Width="85%"  Text='<%# Eval("Id")%>' Enabled="false" Height="26px"
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Price Component" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <%--<asp:TextBox ID="txtPriceName" runat="server" Width="90%"  Text='<%# Eval("PriceName")%>' Enabled="false" Height="26px"
                                                                                                                        ></asp:TextBox>--%>
                                                                                                                    <asp:Label ID="txtPriceName" runat="server" Text='<%# Eval("PriceName")%>' Font-Bold="true">

                                                                                                                    </asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Price" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="fltReorderAd" runat="server" FilterType="Numbers"
                                                                                                                TargetControlID="txtPrice" />
                                                                                                                    <asp:TextBox ID="txtPrice" runat="server" Width="100%" Style="text-align: center" Text='<%# Eval("Price")%>' CssClass="cssTextBoxGrid" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <ItemStyle VerticalAlign="Middle" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" HeaderText="Date Effective From" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtEffDate" runat="server" Width="70%" Style="text-align: Center"  Text='<%# Eval("EffDate","{0:dd/MM/yyyy}")%>'  CssClass="cssTextBoxGrid1" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"
                                                                                                                        ></asp:TextBox>
                                                                                                                    <cc1:CalendarExtender ID="calBillDate" runat="server" Format="dd/MM/yyyy"
                                                                                                                            PopupButtonID="btnBillDate" TargetControlID="txtEffDate" Enabled="True">
                                                                                                                    </cc1:CalendarExtender>
                                                                                                                    <asp:ImageButton ID="btnBillDate" ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                                                                CausesValidation="False" Width="20px" runat="server" />   
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Discount %" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="FTBRadeA" runat="server" FilterType="Custom, Numbers"
                                                                                                    TargetControlID="txtDiscount1" ValidChars="." />
                                                                                                                    <asp:TextBox ID="txtDiscount1" runat="server" Width="90%" Style="text-align: Center"  Text='<%# Eval("Discount")%>'  CssClass="cssTextBoxGrid" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"
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
                                                                                    <table width="800px" cellpadding="3" cellspacing="1" align="center" >
                                                                                        
                                                                                        <tr style= "height: 30px" class="tblLeft">
                                                                                            
                                                                                            <td style="width: 25%" class="ControlLabelNew">
                                                                                                Central Sales Tax (CST) %
                                                                                                <asp:RangeValidator ID="RangeValidator2Add" runat="server" ControlToValidate="txtCSTAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                    MinimumValue="0" Text="CST cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtCSTAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabelNew">
                                                                                                Commodity Code
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtCommCodeAdd" runat="server"
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            
                                                                                            <td  style="width:5%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height: 2px">
                                                                                    </tr>
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 25%" class="ControlLabelNew">
                                                                                                Executive Commission %
                                                                                                <asp:CompareValidator ID="rvExecCommAdd" runat="server" ControlToValidate="txtExecutiveCommissionAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" Type="Double" Operator="DataTypeCheck"
                                                                                                    Text="Commission should be Numeric value"></asp:CompareValidator>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlDrpBorder">
                                                                                                <asp:TextBox ID="txtExecutiveCommissionAdd" runat="server"
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabelNew">
                                                                                                Barcode
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBarcodeAdd" runat="server" Text='<%# Bind("Barcode") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                             <td  style="width:5%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <%--<tr style="height:3px">
                                                                                    </tr>--%>
                                                                                        <tr style="height:30px;" class="tblLeft" runat="server" visible="false">
                                                                                            <td style="width: 25%" class="ControlLabel">
                                                                                                Buy Unit Rate
                                                                                                <cc1:FilteredTextBoxExtender ID="FTBRadteAdd" runat="server" FilterType="Custom, Numbers"
                                                                                                    TargetControlID="txtBuyRateAdd" ValidChars="." />
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBuyRateAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                Buy VAT (%)
                                                                                                <asp:RangeValidator ID="rvBuyVATAdd" runat="server" ControlToValidate="txtBuyVATAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                    MinimumValue="0" Text="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBuyVATAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                             <td  style="width: 5%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <%--<tr style="height:2px">
                                                                                    </tr>
                                                                                        <tr style="height: 30px" class="tblLeft">
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                
                                                                                            </td>
                                                                                            <td style="width: 20%" class="ControlLabel">
                                                                                                
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                
                                                                                            </td>
                                                                                             <td  style="width: 10%">
                                                                                            </td>
                                                                                    
                                                                                        </tr>--%>
                                                                                        <tr style="height:3px">
                                                                                    </tr>
                                                                                        
                                                                                        <tr style="height: 30px; display: none" class="tblLeft" id="rowDealerAdd1" runat="server" visible="false">
                                                                                            <td style="width: 25%" class="ControlLabel">
                                                                                                Dealer VAT (%)
                                                                                                <asp:RangeValidator ID="RangeValidator1Add" runat="server" ControlToValidate="txtDealerVATAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                    MinimumValue="0" Text="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtDealerVATAdd" runat="server"
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%">
                                                                                                Buy Discount (%)
                                                                                                <asp:RangeValidator ID="RVBusDisAdd" runat="server" ControlToValidate="txtBuyDiscountAdd"
                                                                                                    Display="Dynamic" MaximumValue="100" Type="Double" MinimumValue="0" Text="*"
                                                                                                    ErrorMessage="Discount cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:TextBox ID="txtBuyDiscountAdd" runat="server"
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                <asp:TextBox ID="txtproductlevel" runat="server" Visible="false"
                                                                                                    SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td  style="width: 5%">
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr style="height:3px">
                                                                                        </tr>
                                                                                        <tr style="display: none">
                                                                                            <td style="width: 25%">
                                                                                                
                                                                                            </td>
                                                                                            <td style="width: 25%" class="ControlTextBox3">
                                                                                                <asp:TextBox ID="txtBuyUnitAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                            </td>
                                                                                            <td style="width: 20%">
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                            </td>
                                                                                             <td  style="width: 5%">
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    
                                                                                </ContentTemplate>
                                                                            </cc1:TabPanel>
                                                                            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Price History">
                                                                                <ContentTemplate>
                                                                                    <table width="800px" cellpadding="1" cellspacing="1" align="center" >
                                                                                        <tr>
                                                                                            <td style="width:100%">
                                                                                                <div id="div1" runat="server" style="height:190px; overflow:scroll">
                                                                                                    <rwg:BulkEditGridView ID="BulkEditGridView1" AutoGenerateColumns="False"
                                                                                                        GridLines="None" runat="server"
                                                                                                        Width="100%" EmptyDataText="No history found!">
                                                                                                        <HeaderStyle Height="30px" ForeColor="Black"/>
                                <RowStyle Height="30px" />
                                                                                                        <%--<RowStyle CssClass="dataRow" />
                                                                                                        <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                        <AlternatingRowStyle CssClass="altRow" />--%>
                                                                                                        <EmptyDataRowStyle Font-Bold="true" HorizontalAlign="Center" Height="26px" BorderColor="#cccccc" BorderWidth="1px" />
                                                                                                        <%--<HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                        <FooterStyle CssClass="dataRow" />--%>
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="7%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="txtSNO" runat="server" Text='<%# Eval("Row")%>' Font-Bold="true">

                                                                                                                    </asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" Visible="false" HeaderText="ID" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtId" runat="server" Width="85%"  Text='<%# Eval("Id")%>' Enabled="false" Height="26px"
                                                                                                                        ></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Left" HeaderText="Price Component" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <%--<asp:TextBox ID="txtPriceName" runat="server" Width="90%"  Text='<%# Eval("PriceName")%>' Enabled="false" Height="26px"
                                                                                                                        ></asp:TextBox>--%>
                                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("PriceName")%>' Font-Bold="true">

                                                                                                                    </asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Price" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <%--<asp:TextBox ID="txtPrice" runat="server" Width="90%"  Text='<%# Eval("Price")%>' Height="26px" Enabled="false"
                                                                                                                        ></asp:TextBox>--%>
                                                                                                                    <asp:Label ID="txtPrice" runat="server" Text='<%# Eval("Price")%>' Font-Bold="true">

                                                                                                                    </asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Effective Date From" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <%--<asp:TextBox ID="txtEffDate" runat="server" Width="70%"  Enabled="false" Text='<%# Eval("EffDate","{0:dd/MM/yyyy}")%>'  Height="26px"
                                                                                                                        ></asp:TextBox>--%>
                                                                                                                    <asp:Label ID="txtEffDate" runat="server" Text='<%# Eval("EffDate","{0:dd/MM/yyyy}")%>' Font-Bold="true">

                                                                                                                    </asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Discount %" HeaderStyle-BorderColor="Gray" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <%--<asp:TextBox ID="txtDiscount1" runat="server" Width="90%"  Text='<%# Eval("Discount")%>' Height="26px" Enabled="false"
                                                                                                                        ></asp:TextBox>--%>
                                                                                                                    <asp:Label ID="txtDiscount1" runat="server" Text='<%# Eval("Discount")%>' Font-Bold="true">

                                                                                                                    </asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="User Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="Label123" runat="server" Text='<%# Eval("UserName")%>' Font-Bold="true">

                                                                                                                    </asp:Label>
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
                                                                                 <asp:Button ID="UpdateButton" runat="server" SkinID="skinBtnSave"
                                                                                        OnClick="UpdateButton_Click" CssClass="Updatebutton1231" ValidationGroup="salesval" EnableTheming="false"></asp:Button>
                                                                                    <asp:Button ID="AddButton" runat="server" SkinID="skinBtnSave"
                                                                                        OnClick="AddButton_Click" CssClass="savebutton1231" ValidationGroup="salesval" EnableTheming="false"></asp:Button>
                                                                            </td>
                                                                            <td style="width: 20%" align="center">
                                                                                <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                        OnClick="UpdateCancelButton_Click" SkinID="skinBtnCancel" CssClass="cancelbutton6"
                                                                                        EnableTheming="false"></asp:Button>
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
                                        <asp:GridView ID="GrdViewProduct" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                OnRowCreated="GrdViewProduct_RowCreated" Width="100.4%" CssClass="someClass" OnPageIndexChanging="GrdViewProduct_PageIndexChanging"
                                AllowPaging="True" DataKeyNames="ItemCode" EmptyDataText="No Products found." OnRowDeleting="GrdViewProduct_RowDeleting"
                                OnRowCommand="GrdViewProduct_RowCommand" OnRowDataBound="GrdViewProduct_RowDataBound" OnRowDeleted="GrdViewProduct_RowDeleted"
                                OnSelectedIndexChanged="GrdViewProduct_SelectedIndexChanged">
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px"/>
                                <RowStyle Font-Bold="True" HorizontalAlign="Center" Height="30px" ForeColor="#0567AE"  Font-Size="15px"/>
                                <Columns>
                                    <asp:BoundField DataField="Row" HeaderText="#"  ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" HeaderStyle-Width="60px"/>
                                    <asp:BoundField DataField="ItemCode" HeaderText="Product Code" ItemStyle-HorizontalAlign="Left"  ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true"  HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="Model" HeaderText="Model" ItemStyle-HorizontalAlign="Left"  ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category" ItemStyle-HorizontalAlign="Left"  ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="ProductDesc" HeaderText="Brand" ItemStyle-HorizontalAlign="Left"  ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-BorderColor="Gray"/>
                                    <asp:BoundField DataField="Stock" HeaderText="In Stock"  HeaderStyle-BorderColor="Gray" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true"/>
                                    <asp:BoundField DataField="Rate" HeaderText="MRP"  HeaderStyle-BorderColor="Gray" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true"/>
                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select"  CausesValidation="false"/>
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
                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete" CausesValidation="false"></asp:ImageButton>
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
                                                <asp:DropDownList ID="ddlPageSelector" BackColor="#e7e7e7" runat="server" AutoPostBack="true" Width="70px" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" Height="25px" style="border:1px solid Gray">
                                                </asp:DropDownList>
                                            </td>
                                            <td style=" border-color:white;Width:5px">
                                            
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnFirst" CausesValidation="false" />
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnPrevious"  CausesValidation="false"/>
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnNext"  CausesValidation="false"/>
                                            </td>
                                            <td style=" border-color:white">
                                                <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                    ID="btnLast"  CausesValidation="false"/>
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
                
            </table>
            <input type="hidden" id="hidAdvancedState" runat="server" />
            <asp:HiddenField ID="hdSMS" runat="server" Value="NO" />
            <asp:HiddenField ID="hdText" runat="server" />
            <asp:HiddenField ID="hdMobile" runat="server" />
            <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
            <asp:HiddenField ID="hdPendingCount" runat="server" Value="0" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <table style="width: 100%">
            <tr>
                <td  style="width:10%">
                    </td>
                    <td  style="width:30%">
                        <div style="text-align: right;">
                                                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                                                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CausesValidation="false"
                                                        EnableTheming="false" Text="Add New Product"></asp:Button>
                                                </asp:Panel>
                                            </div>
                    </td>
                    <td style="width:15%">
                        <asp:Button ID="BlkAdd" runat="server" CausesValidation="false"  OnClientClick="window.open('BulkAddition.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');"
                                    EnableTheming="false" Text="Import products from Excel file"></asp:Button>
                    </td>
                    <td  style="width:2%">
                        <asp:Button ID="BlkUpd" runat="server" OnClick="BlkUpd_Click" SkinID="skinButtonCol2"  Width="80%"  CausesValidation="false"
                                    Text="Bulk Updation By Screen" Visible="false"></asp:Button>
                    </td>
                    <td style="width:2%">
                    <asp:Button ID="Button4"  CausesValidation="false" runat="server" Text="Bulk Updation By Excel" OnClientClick="window.open('BulkProductUpdation.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=400,width=480,left=425,top=150, scrollbars=yes');"
                                                SkinID="skinButtonCol2"  Width="80%" Visible="false"></asp:Button>
                    </td>
                    <td  style="width:2%">
                        <asp:Button ID="cmdhistory" Visible="false" runat="server" Text="" EnableTheming="false" CausesValidation="False" cssclass="ShowHistory"  OnClientClick="window.open('ReportExcelProductsHistory.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"/>    
                    </td>
                    <td  style="width:15%">
                        <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6"  CausesValidation="false" OnClientClick="window.open('ReportExcelProducts.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                                                EnableTheming="false"></asp:Button>
                    </td>
                    <td  style="width:25%">
                        
                    </td>
                </tr>
        </table>
    </div>
</asp:Content>

