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
                    'click',
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

    </script>

    <style id="Style1" runat="server">

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
                                        <td style="width: 2%"></td>
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
                                            <asp:Button ID="btnSearch" onkeyup="EnableDisableButton(this,'BtnClearFilter')" runat="server" Text="" OnClick="btnSearch_Click" CausesValidation="false"
                                                CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" />
                                        </td>
                                        <td style="width: 20%" class="tblLeftNoPad">
                                            <asp:Button ID="BtnClearFilter" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" CausesValidation="false" Text="" CssClass="ClearFilter6" />
                                        </td>
                                        <td style="width: 10%"></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <asp:ValidationSummary ID="VS" runat="server" Font-Names="'Trebuchet MS'"
                            Font-Size="12pt" HeaderText="" ShowMessageBox="True"
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
                                                                                <cc1:TabPanel ID="tabInsProdMaster" runat="server" HeaderText="Product Details">
                                                                                    <ContentTemplate>

                                                                                        <table style="width: 800px; border: 0px solid #5078B3" align="center" cellpadding="3" cellspacing="1">
                                                                                            <tr style="height: 30px" class="tblLeft">
                                                                                                <td style="width: 20%" class="ControlLabelNew">Product ID *
                                                                                                            <asp:RequiredFieldValidator ID="rvItemCodeAdd" runat="server" ControlToValidate="txtItemCodeAdd"
                                                                                                                Text="*" Display="Dynamic" ValidationGroup="salesval" ErrorMessage="Please enter Product ID. It cannot be left blank."></asp:RequiredFieldValidator>
                                                                                                    <cc1:FilteredTextBoxExtender ID="fltItemCodeAdd" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters"
                                                                                                        TargetControlID="txtItemCodeAdd" Enabled="True" />
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtItemCodeAdd" runat="server"
                                                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 28%" class="ControlLabelNew">Outdated? *
                                                                                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="drpOutdatedAdd"
                                                                                                                Display="Dynamic" ErrorMessage="Outdated is Mandatory" Operator="GreaterThan"
                                                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                                                    <asp:DropDownList ID="drpOutdatedAdd" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7" SelectedValue='<%# Bind("Outdated") %>'
                                                                                                        AppendDataBoundItems="True" EnableTheming="False" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                                                        <asp:ListItem Value="N" Selected="True">NO</asp:ListItem>
                                                                                                        <asp:ListItem Value="Y">YES</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                
                                                                                                <td style="width: 2%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 30px" class="tblLeft">
                                                                                                <td style="width: 20%" class="ControlLabelNew">Name of Product *
                                                                                                            <asp:RequiredFieldValidator ID="rvBDateAdd" runat="server" ControlToValidate="txtItemNameAdd"
                                                                                                                Text="*" Display="Dynamic" ValidationGroup="salesval" ErrorMessage="Please enter Product Name. It cannot be left blank."></asp:RequiredFieldValidator>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers,Custom" ValidChars=" " TargetControlID="txtItemNameAdd" />
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtItemNameAdd" runat="server" Text='<%# Bind("ProductName") %>'
                                                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 28%" class="ControlLabelNew">Unit of Measure
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                                                    <asp:DropDownList ID="drpMeasureAdd" runat="server" DataTextField="Unit" DataValueField="Unit" Width="100%" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        CssClass="drpDownListMedium" BackColor="#E7E7E7" DataSourceID="srcUnitMntAdd"
                                                                                                        AppendDataBoundItems="True">
                                                                                                        <asp:ListItem Selected="True" Value="0">Select Measure</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td style="width: 2%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 30px" class="tblLeft">
                                                                                                <td style="width: 20%" class="ControlLabelNew">Name of Brand *
                                                                                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtProdDescAdd"
                                                                                                                Display="Dynamic" ErrorMessage="Please Select Name of Brand. It cannot be left blank. " ValidationGroup="salesval" Operator="GreaterThan"
                                                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                                                    <asp:DropDownList ID="txtProdDescAdd" runat="server" DataTextField="BrandName" Width="100%" BackColor="#E7E7E7" CssClass="drpDownListMedium"
                                                                                                        DataValueField="BrandName" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                        EnableTheming="False"
                                                                                                        AppendDataBoundItems="True">
                                                                                                        <asp:ListItem Selected="True" Value="0">Select Brand Name</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td style="width: 28%" class="ControlLabelNew">VAT (%) *
                                                                                                            <asp:RequiredFieldValidator ID="rvVATAdd" runat="server" ControlToValidate="txtVATAdd" ValidationGroup="salesval"
                                                                                                                Text="*" Display="Dynamic" ErrorMessage="Please enter VAT(%). It cannot be left blank."></asp:RequiredFieldValidator>
                                                                                                    <asp:RangeValidator ID="cvVATAdd" runat="server" ControlToValidate="txtVATAdd" Display="Dynamic"
                                                                                                        Text="*" MaximumValue="100" Type="Double" MinimumValue="0" ValidationGroup="salesval"
                                                                                                        ErrorMessage="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                    <cc1:FilteredTextBoxExtender ID="fltReorderAd" runat="server" FilterType="Custom,Numbers" ValidChars="."
                                                                                                        TargetControlID="txtVATAdd" />

                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtVATAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>

                                                                                                </td>
                                                                                                <td style="width: 2%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 30px; display: none;" class="tblLeft">
                                                                                                <td style="width: 20%">
                                                                                                    <asp:DropDownList ID="drpRoleTypeAdd" runat="server" AppendDataBoundItems="True" BackColor="#E7E7E7" CssClass="drpDownListMedium" EnableTheming="False" Height="26px" Style="border: 1px solid #e7e7e7" Width="30%">
                                                                                                        <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                                                        <asp:ListItem Value="YES">YES</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtDealerUnitAdd" runat="server" SkinID="skinTxtBoxGrid" Text='<%# Bind("DealerUnit") %>'></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabelNew" style="width: 28%">Complex </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:DropDownList ID="drpComplexAdd" runat="server" AppendDataBoundItems="True" BackColor="#E7E7E7" CssClass="drpDownListMedium" EnableTheming="False" Height="26px" Style="border: 1px solid #e7e7e7" Width="100%">
                                                                                                        <asp:ListItem Value="NO">NO</asp:ListItem>
                                                                                                        <asp:ListItem>NO</asp:ListItem>
                                                                                                        <asp:ListItem Value="YES">YES</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td style="width: 2%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 30px" class="tblLeft" runat="server" visible="False">
                                                                                                <td style="width: 20%" class="ControlLabel" runat="server">MRP *
                                                                                                            Name of Category * Dealer Rate * MRP *
                                                                                                            <cc1:FilteredTextBoxExtender ID="FTBXBRAdd" runat="server" FilterType="Custom, Numbers"
                                                                                                                TargetControlID="txtUnitRateAdd" ValidChars="." Enabled="True" />
                                                                                                    <asp:RequiredFieldValidator ID="rvSRateAdd" runat="server" ControlToValidate="txtUnitRateAdd"
                                                                                                        Text="*" Display="Dynamic" ErrorMessage="Please enter MRP. It cannot be left blank."></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3" runat="server">
                                                                                                    <asp:TextBox ID="txtUnitRateAdd" runat="server" Text="0" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 28%" class="ControlLabel" runat="server">Reorder Level * &nbsp;MRP Effective date
                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator786" runat="server" ControlToValidate="txtMrpDateAdd"
                                                                                                                Text="*" Display="Dynamic" ErrorMessage="Please select MRP Effective date. It cannot be left blank."></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3" runat="server">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                    <asp:TextBox ID="txtMrpDateAdd" runat="server"
                                                                                                        CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                            <cc1:CalendarExtender ID="calExtender312" runat="server" Format="dd/MM/yyyy"
                                                                                                                PopupButtonID="btnDate33" TargetControlID="txtMrpDateAdd" Enabled="True">
                                                                                                            </cc1:CalendarExtender>
                                                                                                </td>

                                                                                                <td style="width: 2%" align="left" runat="server">
                                                                                                    <asp:ImageButton ID="btnDate33" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="False"
                                                                                                        Width="20px" runat="server" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 30px;" class="tblLeft">
                                                                                                <td style="width: 20%" class="ControlLabelNew">Name of Category *
                                                                                                    <asp:CompareValidator ID="cvCatergoryAdd" runat="server" ControlToValidate="ddCategoryAdd" Display="Dynamic" ErrorMessage="Please Select Name of Category. It cannot be left blank." Operator="GreaterThan" Text="*" ValidationGroup="salesval" ValueToCompare="0"></asp:CompareValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlDrpBorder">
                                                                                                    <asp:DropDownList ID="ddCategoryAdd" runat="server" AppendDataBoundItems="True" BackColor="#E7E7E7" CssClass="drpDownListMedium" DataTextField="CategoryName" DataValueField="CategoryID" EnableTheming="False" Height="26px" Style="border: 1px solid #e7e7e7" Width="100%">
                                                                                                        <asp:ListItem Selected="True" Value="0">Select Category</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td style="width: 28%" class="ControlLabelNew">Reorder Level *
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtROLAdd" Display="Dynamic" ErrorMessage="Please enter Reorder Level. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    <cc1:FilteredTextBoxExtender ID="fltReorderAdd" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtROLAdd">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtROLAdd" runat="server" SkinID="skinTxtBoxGrid" Text="1"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 2%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 30px" class="tblLeft" id="rowDealerAdd" runat="server" visible="False">
                                                                                                <td style="width: 20%" class="ControlLabel" runat="server">Name of Category *
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDealerRateAdd" Display="Dynamic" ErrorMessage="Please enter Dealer Rate. It cannot be left blank." Text="*"></asp:RequiredFieldValidator>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FTDUnitRAdd" runat="server" FilterType="Custom, Numbers"
                                                                                                        TargetControlID="txtDealerRateAdd" ValidChars="." Enabled="True" />
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3" runat="server">
                                                                                                    <asp:TextBox ID="txtDealerRateAdd" runat="server" Text="0" SkinID="skinTxtBoxGrid"></asp:TextBox>

                                                                                                </td>

                                                                                                <td style="width: 28%" class="ControlLabel" runat="server">Reorder Level *
                                                                                                            
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3" runat="server">
                                                                                                    <asp:TextBox ID="txtDpDateAdd" runat="server"
                                                                                                        CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                            <cc1:CalendarExtender ID="CalendarExtender123" runat="server" Format="dd/MM/yyyy"
                                                                                                                PopupButtonID="ImageButton123" TargetControlID="txtDpDateAdd" Enabled="True">
                                                                                                            </cc1:CalendarExtender>
                                                                                                </td>

                                                                                                <td style="width: 2%" align="left" runat="server">
                                                                                                    <asp:ImageButton ID="ImageButton123" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="False"
                                                                                                        Width="20px" runat="server" />
                                                                                                </td>
                                                                                                <td runat="server" class="ControlLabel" style="width: 20%">Sell Discount (%) *
                                                                                                    <asp:RequiredFieldValidator ID="rvDiscDis1" runat="server" ControlToValidate="txtDiscountAdd" Display="Dynamic" ErrorMessage="Please enter Sell Discount (%). It cannot be left blank." Text="*"></asp:RequiredFieldValidator>
                                                                                                    <asp:RangeValidator ID="RangeValidator33" runat="server" ControlToValidate="txtDiscountAdd" Display="Dynamic" ErrorMessage="Discount cannot be Greater than 100% and Less than 0%" MaximumValue="100" MinimumValue="0" Text="*" Type="Double"></asp:RangeValidator>
                                                                                                </td>
                                                                                                <td runat="server" class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtDiscountAdd" runat="server" SkinID="skinTxtBoxGrid" Text="0"></asp:TextBox>
                                                                                                </td>
                                                                                                <td runat="server" class="ControlLabel" style="width: 28%">Dealer Discount (%) *
                                                                                                    <asp:RangeValidator ID="dvDisAdd" runat="server" ControlToValidate="txtDealerDiscountAdd" Display="Dynamic" MaximumValue="100" MinimumValue="0" Text="Discount cannot be Greater than 100% and Less than 0%" Type="Double"></asp:RangeValidator>
                                                                                                </td>
                                                                                                <td runat="server" class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtDealerDiscountAdd" runat="server" SkinID="skinTxtBoxGrid" Text="0"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3" runat="server"></td>

                                                                                                <td style="width: 25%" align="right" runat="server">
                                                                                                    <asp:ObjectDataSource ID="srcUnitMntAdd" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListMeasurementUnits" TypeName="BusinessLogic">
                                                                                                        <SelectParameters>
                                                                                                            <asp:CookieParameter CookieName="Company" Name="connection" Type="String" />
                                                                                                        </SelectParameters>
                                                                                                    </asp:ObjectDataSource>
                                                                                                    <asp:ObjectDataSource ID="srcBrandAdd" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListBrandMaster" TypeName="BusinessLogic">
                                                                                                        <SelectParameters>
                                                                                                            <asp:CookieParameter CookieName="Company" Name="connection" Type="String" />
                                                                                                        </SelectParameters>
                                                                                                    </asp:ObjectDataSource>
                                                                                                </td>

                                                                                               <td style="width: 20%" runat="server">IsActive *
                                                                                                    <asp:DropDownList ID="drpblockadd" runat="server" AppendDataBoundItems="True" BackColor="#E7E7E7" CssClass="drpDownListMedium" EnableTheming="False" Height="26px" SelectedValue='<%# Bind("block") %>' Style="border: 1px solid #e7e7e7" Visible="true" Width="100%">
                                                                                                        <asp:ListItem Selected="True" Value="NO">NO</asp:ListItem>
                                                                                                        <asp:ListItem Value="YES">YES</asp:ListItem>
                                                                                                    </asp:DropDownList>

                                                                                                </td>
                                                                                               

                                                                                                <td style="width: 2%" runat="server"></td>
                                                                                                <td style="width: 2%" runat="server"></td>
                                                                                                <td style="width: 20%" class="ControlLabelNew" runat="server">
                                                                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="drpIsActiveAdd" Display="Dynamic" ErrorMessage="IsActive is Mandatory" Operator="GreaterThan" Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlDrpBorder" runat="server">
                                                                                                    <asp:DropDownList ID="drpIsActiveAdd1" runat="server" BackColor="#E7E7E7" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid #e7e7e7" Width="100%">
                                                                                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                                        <asp:ListItem Selected="True" Text="YES" Value="YES"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                <td style="width: 28%" class="ControlLabelNew" runat="server">
                                                                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddCategoryAdd" Display="Dynamic" ErrorMessage="Outdated is Mandatory" Operator="GreaterThan" Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlDrpBorder" runat="server">
                                                                                                    <asp:TextBox ID="txtUnitAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                    <asp:DropDownList ID="drpOutdatedAdd1" runat="server" AppendDataBoundItems="True" BackColor="#E7E7E7" CssClass="drpDownListMedium" EnableTheming="False" Height="26px" SelectedValue='<%# Bind("Outdated") %>' Style="border: 1px solid #e7e7e7" Width="100%">
                                                                                                        <asp:ListItem Selected="True" Value="N">NO</asp:ListItem>
                                                                                                        <asp:ListItem Value="Y">YES</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr style="height: 30px" runat="server" class="tblLeft" visible="False">
                                                                                                <td runat="server" class="ControlLabel" style="width: 20%">NLC *
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNLCAdd" Display="Dynamic" ErrorMessage="Please enter NLC. It cannot be left blank." Text="*"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                                <td runat="server" class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtNLCAdd" runat="server" SkinID="skinTxtBoxGrid" Text="0"></asp:TextBox>
                                                                                                </td>
                                                                                                <td runat="server" class="ControlLabel" style="width: 28%">NLC Effective date * </td>
                                                                                                <td runat="server" class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtNLCDateAdd" runat="server" CssClass="cssTextBox" Width="100px"></asp:TextBox>
                                                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                                                    <cc1:CalendarExtender ID="CalendarExtender22" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImageButton22" TargetControlID="txtNLCDateAdd">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                                <td runat="server" align="left" style="width: 2%">
                                                                                                    <asp:ImageButton ID="ImageButton22" runat="server" CausesValidation="False" ImageUrl="App_Themes/NewTheme/images/cal.gif" Width="20px" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr runat="server" visible="False">
                                                                                                <td style="width: 2%" runat="server"></td>

                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="ControlLabelNew" style="width: 20%">Name of Model *
                                                                                                    <asp:RequiredFieldValidator ID="rvModelAdd" runat="server" ControlToValidate="txtModelAdd" Display="Dynamic" ErrorMessage="Please enter Name of Model. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="LowercaseLetters, UppercaseLetters,Numbers" TargetControlID="txtModelAdd" />
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%;">
                                                                                                    <asp:TextBox ID="txtModelAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td class="ControlLabelNew" style="width: 28%">Allowed Price Deviation % *
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAllowedPriceAdd" Display="Dynamic" ErrorMessage="Please enter Allowed Price Deviation %. It cannot be left blank." Text="*" ValidationGroup="salesval"></asp:RequiredFieldValidator>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtAllowedPriceAdd" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                    <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtAllowedPriceAdd" Display="Dynamic"
                                                                                                        Text="*" MaximumValue="200" Type="Double" MinimumValue="0" ValidationGroup="salesval"
                                                                                                        ErrorMessage="Price deviation cannot be Greater than 200% and Less than 0%"></asp:RangeValidator>
                                                                                                </td>
                                                                                                <td class="ControlTextBox3" style="width: 25%">
                                                                                                    <asp:TextBox ID="txtAllowedPriceAdd" runat="server" SkinID="skinTxtBoxGrid" Text="0"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 2%"></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="width: 20%" class="ControlLabelNew">IsActive *
                                                                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="drpIsActiveAdd"
                                                                                                                Display="Dynamic" ErrorMessage="IsActive is Mandatory" Operator="GreaterThan"
                                                                                                                Text="*" ValueToCompare="0"></asp:CompareValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%;" class="ControlDrpBorder">
                                                                                                    <asp:DropDownList ID="drpIsActiveAdd" runat="server" BackColor="#e7e7e7" CssClass="drpDownListMedium"
                                                                                                        Width="100%" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                                                        <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                                                                                        <asp:ListItem Text="YES" Value="YES" Selected="True"></asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                                
                                                                                                <td style="width: 2%"></td>
                                                                                            </tr>
                                                                                        </table>

                                                                                    </ContentTemplate>
                                                                                </cc1:TabPanel>
                                                                                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Price Details">
                                                                                    <ContentTemplate>
                                                                                        <table width="800px" cellpadding="1" cellspacing="1" align="center">
                                                                                            <tr>
                                                                                                <td style="width: 100%">
                                                                                                    <div id="div" runat="server" style="height: 190px; overflow: scroll">
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
                                                                                                                        <asp:TextBox ID="txtId" runat="server" Width="85%" Text='<%# Eval("Id")%>' Enabled="false" Height="26px"></asp:TextBox>
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
                                                                                                                        <asp:TextBox ID="txtPrice" runat="server" Width="100%" Style="text-align: center" Text='<%# Eval("Price")%>' CssClass="cssTextBoxGrid" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"></asp:TextBox>
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle VerticalAlign="Middle" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" HeaderText="Date Effective From" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:TextBox ID="txtEffDate" runat="server" Enabled="false" Width="70%" Style="text-align: Center" Text='<%# Eval("EffDate","{0:dd/MM/yyyy}")%>' CssClass="cssTextBoxGrid1" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"></asp:TextBox>
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
                                                                                                                        <%--  <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtDiscount1" Display="Dynamic"
                                                                                                        Text="*" MaximumValue="100" Type="Double" MinimumValue="0" ValidationGroup="salesval"
                                                                                                        ErrorMessage="Discount cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>--%>
                                                                                                                        <asp:TextBox ID="txtDiscount1" runat="server" Width="90%" Style="text-align: Center" Text='<%# Eval("Discount")%>' CssClass="cssTextBoxGrid" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006699" Font-Bold="true"></asp:TextBox>
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
                                                                                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Stock Details">
                                                                                    <ContentTemplate>
                                                                                        <table width="800px" cellpadding="1" cellspacing="1" align="center">
                                                                                            <tr>
                                                                                                <td style="width: 100%">
                                                                                                    <div id="div2" runat="server" style="height: 190px; overflow: scroll">
                                                                                                        <rwg:BulkEditGridView ID="BulkEditGridView2" AutoGenerateColumns="False"
                                                                                                            GridLines="None" runat="server" OnRowDataBound="BulkEditGridView2_RowDataBound"
                                                                                                            Width="95%">
                                                                                                            <%--<RowStyle CssClass="dataRow" />
                                                                                                        <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                        <AlternatingRowStyle CssClass="altRow" />
                                                                                                        <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                        <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                        <FooterStyle CssClass="dataRow" />--%>
                                                                                                            <RowStyle Font-Bold="True" HorizontalAlign="Center" Height="30px" ForeColor="#0567AE" Font-Size="15px" />
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="7%">
                                                                                                                    <ItemTemplate>
                                                                                                                        <%--<asp:TextBox ID="txtPriceName" runat="server" Width="90%"  Text='<%# Eval("PriceName")%>' Enabled="false" Height="26px"
                                                                                                                        ></asp:TextBox>--%>
                                                                                                                        <asp:Label ID="txtSNO" runat="server" Text='<%# Eval("Row")%>' Font-Bold="true">

                                                                                                                        </asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Branch Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" HeaderStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                    <ItemTemplate>
                                                                                                                        <%--<asp:TextBox ID="txtPriceName" runat="server" Width="90%"  Text='<%# Eval("PriceName")%>' Enabled="false" Height="26px"
                                                                                                                        ></asp:TextBox>--%>
                                                                                                                        <asp:Label ID="txtBranchName" runat="server" Text='<%# Eval("BranchName")%>' Font-Bold="true">

                                                                                                                        </asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Branch Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="txtBranchcode" runat="server" Text='<%# Eval("Branchcode")%>' Font-Bold="true">

                                                                                                                        </asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle VerticalAlign="Middle" />
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Stock" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="Black" HeaderStyle-BorderColor="Gray" HeaderStyle-Width="20%">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="txtStock" runat="server" Text='<%# Eval("Stock")%>' Font-Bold="true">

                                                                                                                        </asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                    <ItemStyle VerticalAlign="Middle" />
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
                                                                                        <table width="800px" cellpadding="3" cellspacing="1" align="center">

                                                                                            <tr style="height: 30px" class="tblLeft">

                                                                                                <td style="width: 25%" class="ControlLabelNew">Central Sales Tax (CST) %                                                                                            
                                                                                                    
                                                                                                        <asp:RangeValidator ID="RangeValidator44" runat="server" ControlToValidate="txtCSTAdd" Display="Dynamic"
                                                                                                            Text="*" MaximumValue="100" Type="Double" MinimumValue="0" ValidationGroup="salesval"
                                                                                                            ErrorMessage="CST cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>


                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                                                                                        TargetControlID="txtCSTAdd" />
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtCSTAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 20%" class="ControlLabelNew">Commodity Code
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtCommCodeAdd" runat="server"
                                                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>

                                                                                                <td style="width: 5%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr style="height: 30px" class="tblLeft">
                                                                                                <td style="width: 25%" class="ControlLabelNew">Executive Commission %
                                                                                              <%--  <asp:CompareValidator ID="rvExecCommAdd" runat="server" ControlToValidate="txtExecutiveCommissionAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" Type="Double" Operator="DataTypeCheck"
                                                                                                    Text="Commission should be Numeric value"></asp:CompareValidator>--%>

                                                                                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtExecutiveCommissionAdd" Display="Dynamic"
                                                                                                        Text="*" MaximumValue="100" Type="Double" MinimumValue="0" ValidationGroup="salesval"
                                                                                                        ErrorMessage="Executive commission cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>

                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers"
                                                                                                        TargetControlID="txtExecutiveCommissionAdd" />
                                                                                                </td>
                                                                                                <td style="width: 20%" class="ControlDrpBorder">
                                                                                                    <asp:TextBox ID="txtExecutiveCommissionAdd" runat="server"
                                                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 20%" class="ControlLabelNew">Barcode
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtBarcodeAdd" runat="server" Text='<%# Bind("Barcode") %>' SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 5%"></td>
                                                                                            </tr>
                                                                                            <%--<tr style="height:3px">
                                                                                    </tr>--%>
                                                                                            <tr style="height: 30px;" class="tblLeft" runat="server" visible="false">
                                                                                                <td style="width: 25%" class="ControlLabel">Buy Unit Rate
                                                                                                <cc1:FilteredTextBoxExtender ID="FTBRadteAdd" runat="server" FilterType="Custom, Numbers"
                                                                                                    TargetControlID="txtBuyRateAdd" ValidChars="." />
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtBuyRateAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 20%" class="ControlLabel">Buy VAT (%)
                                                                                                <asp:RangeValidator ID="rvBuyVATAdd" runat="server" ControlToValidate="txtBuyVATAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                    MinimumValue="0" Text="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtBuyVATAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 5%"></td>
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
                                                                                            <tr style="height: 3px">
                                                                                            </tr>

                                                                                            <tr style="height: 30px; display: none" class="tblLeft" id="rowDealerAdd1" runat="server" visible="false">
                                                                                                <td style="width: 25%" class="ControlLabel">Dealer VAT (%)
                                                                                                <asp:RangeValidator ID="RangeValidator1Add" runat="server" ControlToValidate="txtDealerVATAdd"
                                                                                                    Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                    MinimumValue="0" Text="VAT cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtDealerVATAdd" runat="server"
                                                                                                        SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 20%">Buy Discount (%)
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
                                                                                                <td style="width: 5%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 3px">
                                                                                            </tr>
                                                                                            <tr style="display: none">
                                                                                                <td style="width: 25%"></td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtBuyUnitAdd" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 20%"></td>
                                                                                                <td style="width: 25%"></td>
                                                                                                <td style="width: 5%"></td>
                                                                                            </tr>
                                                                                        </table>

                                                                                    </ContentTemplate>
                                                                                </cc1:TabPanel>
                                                                                <cc1:TabPanel ID="tabSalesIncentive" Visible="false" runat="server" HeaderText="Sales Incentive">
                                                                                    <ContentTemplate>
                                                                                        <table width="800px" cellpadding="3" cellspacing="1" align="center">
                                                                                            <tr style="height: 30px" class="tblLeft">
                                                                                                <td style="width: 50%; align-content: center;" align="center" class="ControlLabelNew">Profit Margin
				                                                                                    <asp:HiddenField ID="hdnfSalesIncentiveId" Value="" runat="server" />
                                                                                                    <asp:HiddenField ID="hdnfItemCode" Value="" runat="server" />
                                                                                                </td>
                                                                                                <td style="width: 25%" align="center" class="ControlLabelNew">Incentive Amount (%)
                                                                                                </td>
                                                                                                <td style="width: 25%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                            <tr style="height: 30px" class="tblLeft">
                                                                                                <td style="width: 50%" class="ControlText3, alignCenter">Slab #1 ( 1% - 10% )
				                                                                                    <asp:RangeValidator ID="RangeValidatorSlab1" runat="server" ControlToValidate="txtSlab1"
                                                                                                        Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                        MinimumValue="1" Text="Incentive amount percentage cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtSlab1" Text="0" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 25%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 30px" class="tblLeft">
                                                                                                <td style="width: 50%" class="ControlText3, alignCenter">Slab #2 ( 11% - 25% )
				                                                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtSlab2"
                                                                                                        Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                        MinimumValue="1" Text="Incentive amount percentage cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtSlab2"  Text="0" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 25%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 30px" class="tblLeft">
                                                                                                <td style="width: 50%" class="ControlText3, alignCenter">Slab #3 ( 26% - 60% )
				                                                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtSlab3"
                                                                                                        Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                        MinimumValue="1" Text="Incentive amount percentage cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtSlab3"  Text="0" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 25%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 30px" class="tblLeft">
                                                                                                <td style="width: 50%" class="ControlText3, alignCenter">Slab #4 ( 61% - 90% )
				                                                                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtSlab4"
                                                                                                        Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                        MinimumValue="1" Text="Incentive amount percentage cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtSlab4"  Text="0" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 25%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 30px" class="tblLeft">
                                                                                                <td style="width: 50%" class="ControlText3, alignCenter">Slab #5 ( 91% - 100% )
				                                                                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtSlab5"
                                                                                                        Display="Dynamic" EnableClientScript="True" MaximumValue="100" Type="Double"
                                                                                                        MinimumValue="1" Text="Incentive amount percentage cannot be Greater than 100% and Less than 0%"></asp:RangeValidator>
                                                                                                </td>
                                                                                                <td style="width: 25%" class="ControlTextBox3">
                                                                                                    <asp:TextBox ID="txtSlab5"  Text="0" runat="server" SkinID="skinTxtBoxGrid"></asp:TextBox>
                                                                                                </td>
                                                                                                <td style="width: 25%"></td>
                                                                                            </tr>
                                                                                            <tr style="height: 2px">
                                                                                            </tr>
                                                                                        </table>

                                                                                    </ContentTemplate>
                                                                                </cc1:TabPanel>
                                                                                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Price History">
                                                                                    <ContentTemplate>
                                                                                        <table width="800px" cellpadding="1" cellspacing="1" align="center">
                                                                                            <tr>
                                                                                                <td style="width: 100%">
                                                                                                    <div id="div1" runat="server" style="height: 190px; overflow: scroll">
                                                                                                        <rwg:BulkEditGridView ID="BulkEditGridView1" AutoGenerateColumns="False"
                                                                                                            GridLines="None" runat="server"
                                                                                                            Width="100%" EmptyDataText="No history found!">
                                                                                                            <HeaderStyle Height="30px" ForeColor="Black" />
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
                                                                                                                        <asp:TextBox ID="txtId" runat="server" Width="85%" Text='<%# Eval("Id")%>' Enabled="false" Height="26px"></asp:TextBox>
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
                                                                            ShowSummary="false" HeaderText="" Font-Names="'Trebuchet MS'"
                                                                            Font-Size="12" runat="server" />

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td style="width: 30%"></td>

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

                                                                                <td style="width: 30%"></td>
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
                                            <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="15px" />
                                            <RowStyle Font-Bold="True" HorizontalAlign="Center" Height="30px" ForeColor="#0567AE" Font-Size="15px" />
                                            <Columns>
                                                <asp:BoundField DataField="Row" HeaderText="#" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" HeaderStyle-Width="60px" />
                                                <asp:BoundField DataField="ItemCode" HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="Model" HeaderText="Model" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="CategoryName" HeaderText="Category" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-BorderColor="Gray" />
                                                <asp:BoundField DataField="ProductDesc" HeaderText="Brand" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" HeaderStyle-BorderColor="Gray" />
                                                <%--<asp:BoundField DataField="Stock" HeaderText="In Stock"  HeaderStyle-BorderColor="Gray" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true"/>--%>
                                                <asp:BoundField DataField="Rate" HeaderText="MRP" HeaderStyle-BorderColor="Gray" Visible="false" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="#0567AE" HeaderStyle-Font-Bold="true" />
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Edit" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" CausesValidation="false" />
                                                        <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="50px" HeaderStyle-BorderColor="Gray">
                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Product?"
                                                            runat="server">
                                                        </cc1:ConfirmButtonExtender>
                                                        <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                        <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete" CausesValidation="false"></asp:ImageButton>
                                                        <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("ItemCode") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerTemplate>
                                                <table style="border-color: white">
                                                    <tr style="border-color: white">
                                                        <td style="border-color: white">Goto Page
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:DropDownList ID="ddlPageSelector" BackColor="#e7e7e7" runat="server" AutoPostBack="true" Width="70px" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged" Height="25px" Style="border: 1px solid Gray">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="border-color: white; width: 5px"></td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="First" runat="server" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnFirst" CausesValidation="false" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Prev" runat="server" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnPrevious" CausesValidation="false" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Next" runat="server" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnNext" CausesValidation="false" />
                                                        </td>
                                                        <td style="border-color: white">
                                                            <asp:Button Text="" CommandName="Page" CommandArgument="Last" runat="server" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                ID="btnLast" CausesValidation="false" />
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
                <td style="width: 10%"></td>
                <td style="width: 30%">
                    <div style="text-align: right;">
                        <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                            <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CausesValidation="false" ForeColor="White" CssClass="ButtonAdd66"
                                EnableTheming="false"></asp:Button>
                        </asp:Panel>
                    </div>
                </td>
                <td style="width: 15%">
                    <asp:Button ID="BlkAdd" runat="server" CausesValidation="false" OnClientClick="window.open('BulkAddition.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=250,width=500,left=425,top=220, scrollbars=yes');"
                        EnableTheming="false" CssClass="bulkaddition"></asp:Button>
                </td>
                <td style="width: 2%">
                    <asp:Button ID="BlkUpd" runat="server" OnClick="BlkUpd_Click" SkinID="skinButtonCol2" Width="80%" CausesValidation="false"
                        Text="Bulk Updation By Screen" Visible="false"></asp:Button>
                </td>
                <td style="width: 2%">
                    <asp:Button ID="Button4" CausesValidation="false" runat="server" Text="Bulk Updation By Excel" OnClientClick="window.open('BulkProductUpdation.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=400,width=480,left=425,top=150, scrollbars=yes');"
                        SkinID="skinButtonCol2" Width="80%" Visible="false"></asp:Button>
                </td>
                <td style="width: 2%">
                    <asp:Button ID="cmdhistory" Visible="false" runat="server" Text="" EnableTheming="false" CausesValidation="False" CssClass="ShowHistory" OnClientClick="window.open('ReportExcelProductsHistory.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');" />
                </td>
                <td style="width: 15%">
                    <asp:Button ID="btnExportToExcel" runat="server" CssClass="exportexl6" CausesValidation="false" OnClientClick="window.open('ReportExcelProducts.aspx','billSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=310,width=500,left=425,top=220, scrollbars=yes');"
                        EnableTheming="false"></asp:Button>
                </td>
                <td style="width: 25%"></td>
            </tr>
        </table>
    </div>
</asp:Content>

