<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true" CodeFile="ReportXLPurchase.aspx.cs" Inherits="ReportXLPurchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">

    <script src="Scripts/JScriptSales.js" type="text/javascript">

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

    <asp:UpdatePanel ID="UpdatePnlMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <input id="dummyStock" type="button" style="display: none" runat="server" />
            <input id="BtnPopUpCancel" type="button" style="display: none" runat="server" />

            <cc1:ModalPopupExtender ID="ModalPopupPurchase" runat="server" BackgroundCssClass="modalBackgroundNew"
                RepositionMode="RepositionOnWindowResizeAndScroll" DynamicServicePath="" Enabled="True" PopupControlID="pnlPurchaseForm" TargetControlID="dummyStock" OkControlID="dummyStock" CancelControlID="BtnPopUpCancel">
            </cc1:ModalPopupExtender>

            <asp:Panel ID="pnlPurchaseForm" runat="server" Style="width: 660px; height: 100%; display: none">
                <asp:UpdatePanel ID="updatePnlPurchase" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="Div1" style="width: 100%; height: 100%; background-color: White;">
                            <table style="width: 100%;" align="center">
                                <tr style="width: 100%">
                                    <td style="width: 100%">
                                        <table style="text-align: left;" width="100%">
                                            <tr>
                                                <td>
                                                    <cc1:TabContainer ID="tabs2" runat="server" Width="100%" ActiveTabIndex="0" CssClass="fancy fancy-green">
                                                        <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Field">
                                                            <HeaderTemplate>
                                                                Purchase Report
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <div>
                                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table style="width: 540px; height: 100%;">
                                                                                <tr style="height: 10px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="ControlLabel2" style="width: 40%">Start Date
                                                                                    </td>
                                                                                    <td align="left" style="width: 40%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtStrtDt" runat="server" AutoPostBack="True" SkinID="skinTxtBoxGrid"
                                                                                            BackColor="#e7e7e7" Height="25px" Width="150px"></asp:TextBox>
                                                                                        <cc1:CalendarExtender ID="calStartDate" runat="server" Enabled="True"
                                                                                            Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate"
                                                                                            PopupButtonID="btnStartDate" TargetControlID="txtStrtDt">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <td align="left" style="width: 20%">
                                                                                        <asp:ImageButton ID="btnStartDate" runat="server" CausesValidation="False"
                                                                                            ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                            Width="20px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="ControlLabel2" width="40%">End Date
                                                                                    </td>
                                                                                    <td align="left" width="40%" class="ControlTextBox3">
                                                                                        <asp:TextBox ID="txtEndDt" runat="server" AutoPostBack="True" SkinID="skinTxtBoxGrid"
                                                                                            BackColor="#e7e7e7" Height="25px" Width="150px"></asp:TextBox>
                                                                                        <cc1:CalendarExtender ID="CalEndDate" runat="server" Enabled="True"
                                                                                            Format="MM/dd/yyyy" OnClientDateSelectionChanged="checkDate"
                                                                                            PopupButtonID="btnEndDate" TargetControlID="txtEndDt">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <td align="left" style="width: 20%">
                                                                                        <asp:ImageButton ID="btnEndDate" runat="server" CausesValidation="False"
                                                                                            ImageUrl="App_Themes/NewTheme/images/cal.gif"
                                                                                            Width="20px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="ControlLabel2" width="40%">Sales Return
                                                                                    </td>
                                                                                    <td align="left" style="width: 40%" class="ControlTextBox3">
                                                                                        <asp:RadioButtonList ID="rblSalesRtn" runat="server" CssClass="label"
                                                                                            RepeatDirection="Horizontal" BackColor="#e7e7e7" Width="150px" Height="25px">
                                                                                            <asp:ListItem>Yes</asp:ListItem>
                                                                                            <asp:ListItem>No</asp:ListItem>
                                                                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </td>
                                                                                    <td style="width: 20%">&nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" class="ControlLabel2" width="40%">Internal Transfer
                                                                                    </td>
                                                                                    <td align="left" style="width: 40%" class="ControlTextBox3">
                                                                                        <asp:RadioButtonList ID="rblIntrnlTrns" runat="server" CssClass="label"
                                                                                            RepeatDirection="Horizontal" BackColor="#e7e7e7" Width="150px" Height="25px">
                                                                                            <asp:ListItem>Yes</asp:ListItem>
                                                                                            <asp:ListItem>No</asp:ListItem>
                                                                                            <asp:ListItem Selected="True">All</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </td>
                                                                                    <td style="width: 20%">&nbsp;</td>
                                                                                </tr>
                                                                                <tr style="height: 10px">
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>

                                                        <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Field">
                                                            <HeaderTemplate>
                                                                Fields
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <div>
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table style="width: 540px; height: 100%; background-color: #e7e7e7">
                                                                                <tr>
                                                                                    <td>&nbsp;
                                                                                    </td>
                                                                                    <td>&nbsp;
                                                                                    </td>
                                                                                    <td style="width: 10%">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">&nbsp;
                                                                                    </td>
                                                                                    <td align="left" style="width: 30%">
                                                                                        <asp:CheckBox ID="chkboxCategory" runat="server" Style="color: Black" Text="Category" Font-Names="arial" Font-Size="11px" AutoPostBack="true" />
                                                                                    </td>
                                                                                    <td align="left" style="width: 35%">
                                                                                        <asp:CheckBox ID="chkboxSupplier" runat="server" Text="Supplier" Style="color: Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true" />
                                                                                    </td>
                                                                                    <td style="width: 10%">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">&nbsp;
                                                                                    </td>
                                                                                    <td align="left" style="width: 30%">
                                                                                        <asp:CheckBox ID="chkboxProductCode" runat="server" Style="color: Black" Text="ProductCode" Font-Names="arial" Font-Size="11px" AutoPostBack="true" />
                                                                                    </td>
                                                                                    <td align="left" style="width: 35%">
                                                                                        <asp:CheckBox ID="chkboxBrand" runat="server" Text="Brand" Style="color: Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true" />
                                                                                    </td>
                                                                                    <td style="width: 10%">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">&nbsp;
                                                                                    </td>
                                                                                    <td align="left" style="width: 30%">
                                                                                        <asp:CheckBox ID="chkboxProductName" runat="server" Style="color: Black" Text="ProductName" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td align="left" style="width: 35%">
                                                                                        <asp:CheckBox ID="chkboxBillno" runat="server" Text="Billno" Style="color: Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td style="width: 10%">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">&nbsp;
                                                                                    </td>
                                                                                    <td align="left" style="width: 30%">
                                                                                        <asp:CheckBox ID="chkboxBillDate" runat="server" Style="color: Black" Text="BillDate" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td align="left" style="width: 35%">
                                                                                        <asp:CheckBox ID="chkboxInternalTransfer" runat="server" Text="InternalTransfer" Style="color: Black" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td style="width: 10%">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">&nbsp;
                                                                                    </td>
                                                                                    <td align="left" style="width: 30%">
                                                                                        <asp:CheckBox ID="chkboxSalesReturn" runat="server" Style="color: Black" Text="SalesReturn" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td align="left" style="width: 35%">
                                                                                        <asp:CheckBox ID="chkboxModel" runat="server" Style="color: Black" Text="Model" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td style="width: 10%">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">&nbsp;
                                                                                    </td>
                                                                                    <td align="left" style="width: 30%">
                                                                                        <asp:CheckBox ID="ChkboxCustaddr" runat="server" Style="color: Black" Text="Customer Address" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td align="left" style="width: 35%">
                                                                                        <asp:CheckBox ID="ChkboxCustphone" runat="server" Style="color: Black" Text="Customer Phone" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                        <td style="width: 10%">&nbsp;
                                                                                        </td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">&nbsp;
                                                                                    </td>
                                                                                    <td align="left" style="width: 30%">
                                                                                        <asp:CheckBox ID="chkboxStock" runat="server" Style="color: Black" Text="Quantity" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td align="left" style="width: 35%">
                                                                                        <asp:CheckBox ID="chkboxDiscount" runat="server" AutoPostBack="True" Style="color: Black" Text="Discount" Font-Names="arial" Font-Size="11px"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td style="width: 10%">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">&nbsp;
                                                                                    </td>
                                                                                    <td align="left" style="width: 30%">
                                                                                        <asp:CheckBox ID="chkboxFreight" runat="server" Style="color: Black" Text="Freight" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td align="left" style="width: 35%">
                                                                                        <asp:CheckBox ID="chkboxRate" runat="server" AutoPostBack="True" Style="color: Black" Text="Rate" Font-Names="arial" Font-Size="11px"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td style="width: 10%">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">&nbsp;
                                                                                    </td>
                                                                                    <td align="left" style="width: 30%">
                                                                                        <asp:CheckBox ID="chkboxpaymode" runat="server" AutoPostBack="True" Style="color: Black" Text="Pay Mode" Font-Names="arial" Font-Size="11px"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td align="left" style="width: 35%">
                                                                                        <asp:CheckBox ID="chkboxVoucherNo" runat="server" Style="color: Black" Text="Voucher No" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td style="width: 10%">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">&nbsp;</td>
                                                                                    <td align="left" style="width: 35%">
                                                                                        <asp:CheckBox ID="chkboxTransNo" runat="server" Style="color: Black" Text="Trans No" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td align="left" style="width: 30%">

                                                                                        <asp:CheckBox ID="chkboxNarration" runat="server" AutoPostBack="True" Style="color: Black" Text="Narration" Font-Names="arial" Font-Size="11px"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td style="width: 10%">&nbsp;</td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="width: 25%">&nbsp;</td>
                                                                                    <td align="left" style="width: 30%">
                                                                                        <asp:CheckBox ID="chkPurEntyDate" runat="server" Style="color: Black" Text="Purchase Entry Date" Font-Names="arial" Font-Size="11px" AutoPostBack="true"></asp:CheckBox>
                                                                                    </td>
                                                                                    <td align="left" style="width: 30%">
                                                                                        <asp:CheckBox ID="chkboxAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkboxAll_CheckedChanged" Style="color: Black" Text="Select All" Font-Names="arial" Font-Size="11px"></asp:CheckBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 6px">
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="chkboxAll" EventName="CheckedChanged" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>

                                                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Filter">
                                                            <HeaderTemplate>
                                                                Filter
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div>
                                                                            <table style="width: 540px; height: 100%; background-color: #e7e7e7">
                                                                                <tr style="height: 10px">
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Category Name
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">

                                                                                        <asp:DropDownList ID="ddlCategory" OnSelectedIndexChanged="LoadProducts" AutoPostBack="true" Style="border: 2px medium #e7e7e7" runat="server" Height="24px" Width="98%" CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                            <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">All</asp:ListItem>
                                                                                        </asp:DropDownList>

                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Supplier Name
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlSupplier" Style="border: 2px medium #e7e7e7" AppendDataBoundItems="true" AutoPostBack="true" runat="server" Height="24px" Width="98%" CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Product Name
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlPrdctNme" AutoPostBack="true" OnSelectedIndexChanged="LoadForProductName" Style="border: 2px medium #e7e7e7" runat="server" Height="24px" Width="98%" CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                            <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">All</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Product Code
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlPrdctCode" Style="border: 2px medium #e7e7e7" OnSelectedIndexChanged="LoadForProduct" AutoPostBack="true" runat="server" Height="24px" Width="98%"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                            <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">All</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Brand
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlBrand" OnSelectedIndexChanged="LoadForBrand" AutoPostBack="true" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                            <asp:ListItem Selected="True" Value="0" style="background-color: #90c9fc">All</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">PayMode
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlPayMode" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel" width="5%"></td>
                                                                                </tr>
                                                                                <tr style="height: 10px">
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>

                                                        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Groupby">
                                                            <HeaderTemplate>
                                                                Groupby
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div>
                                                                            <table style="width: 540px; height: 100%; background-color: #e7e7e7">
                                                                                <tr style="height: 10px">
                                                                                    <td></td>
                                                                                </tr>
                                                                                <%--<tr style="height:5px">
                                                                                                            </tr>--%>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">First Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlFirstLvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Fourth Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlFourthLvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Second Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlSecondLvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Fifth Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlFifthLvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Third Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlThirdLvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Sixth Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlSixthLvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Seventh Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlSeventhLvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">
                                                                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                                                                    </td>
                                                                                    <td class="ControlLabel" width="30%"></td>
                                                                                    <td class="ControlLabel" width="5%"></td>
                                                                                </tr>
                                                                                <tr style="height: 10px">
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Orderby">
                                                            <HeaderTemplate>
                                                                Orderby
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div>
                                                                            <table style="width: 540px; height: 100%; background-color: #e7e7e7">
                                                                                <tr style="height: 10px">
                                                                                    <td></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">First Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="odlfirstlvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Fourth Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="odlfourlvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Second Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="odlsecondlvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Fifth Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="odlfifthlvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Third Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="odlthirdlvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Sixth Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="odlsixthlvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel" width="5%"></td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Seventh Level
                                                                                    </td>
                                                                                    <td width="30%" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="odlseventhlvl" runat="server" Height="24px" Width="98%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%"></td>
                                                                                    <td class="ControlLabel" width="30%"></td>
                                                                                    <td class="ControlLabel" width="5%"></td>
                                                                                </tr>
                                                                                <tr style="height: 10px">
                                                                                </tr>
                                                                            </table>
                                                                            <table>
                                                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                                                                    EnableViewState="False">
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>name</HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>chkboxCategory</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>chkboxBrand</HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>chkboxProductCode</HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>chkboxProductNme</HeaderTemplate>

                                                                                        </asp:TemplateField>


                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>chkboxCustomer</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>chkboxPurchasertn</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>chkboxInternaltransfer</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>chkboxPaymode</HeaderTemplate>


                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>chkboxall</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>dproductdesc</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>dcat</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>dprod</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>ditem</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>dpay</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>drate</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>dfirst</HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>dsecond</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>dthird</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>dfour</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>dfifth</HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>dsixth</HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>dseventh</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>odfirst</HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>odsecond</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>odthird</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>odfour</HeaderTemplate>

                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>odfifth</HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>odsixth</HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>odseventh</HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </table>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>

                                                        <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="SubTotal">
                                                            <HeaderTemplate>
                                                                SubTotal
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div>
                                                                            <table style="width: 540px; height: 100%; background-color: #e7e7e7;">
                                                                                <tr style="height: 10px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">First Level
                                                                                    </td>
                                                                                    <td width="30%" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="firstsub" runat="server" Height="24px" Width="100%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Fifth Level
                                                                                    </td>
                                                                                    <td width="30%" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="fivesub" runat="server" Height="24px" Width="100%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Second Level
                                                                                    </td>
                                                                                    <td width="30%" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="secondsub" runat="server" Height="24px" Width="100%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Sixth Level
                                                                                    </td>
                                                                                    <td width="30%" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="sixsub" runat="server" Height="24px" Width="100%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Third Level
                                                                                    </td>
                                                                                    <td width="30%" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="thirdsub" runat="server" Height="24px" Width="100%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Seventh Level
                                                                                    </td>
                                                                                    <td width="30%" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="sevensub" runat="server" Height="24px" Width="100%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel" width="5%">
                                                                                        <asp:Label ID="Label33" runat="server" Text=""></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 5px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" width="20%">Fourth Level
                                                                                    </td>
                                                                                    <td width="30%" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="foursub" runat="server" Height="24px" Width="100%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td class="ControlLabel2" width="20%">Eighth Level
                                                                                    </td>
                                                                                    <td width="30%" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="eightsub" runat="server" Height="24px" Width="100%" Style="border: 2px medium #e7e7e7"
                                                                                            CssClass="drpDownListMedium" BackColor="#e7e7e7">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 10px">
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>


                                                        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Save">
                                                            <HeaderTemplate>
                                                                FinalReport
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <div>
                                                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table style="width: 540px; height: 100%; background-color: #e7e7e7">
                                                                                <tr style="height: 10px">
                                                                                </tr>
                                                                                <tr style="height: 10px">
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" style="width: 35%;">Save Selections
                                                                                    </td>
                                                                                    <td style="width: 35%;background-color: #e7e7e7"" class="ControlTextBox">
                                                                                        <asp:TextBox ID="Savetxtbox" runat="server" Height="22px" Width="118%" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px"
                                                                                            BackColor="#e7e7e7" AutoPostBack="True"></asp:TextBox>                                                                                        

                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Savetxtbox"
                                                                                            CssClass="lblFont" Display="Dynamic" ErrorMessage="Please enter a name to save selections" Text="*"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td style="width: 30%;">
                                                                                        <asp:Label ID="Label32" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="ControlLabel2" style="width: 35%;">Retrive Selections
                                                                                    </td>
                                                                                    <td style="width: 35%;" align="left" class="ControlTextBox2">
                                                                                        <asp:DropDownList ID="ddlretrive" runat="server" AppendDataBoundItems="true" AutoPostBack="true" Style="border: 2px medium #e7e7e7"
                                                                                            Width="98%" Height="25px" CssClass="drpDownListMedium" BackColor="#e7e7e7" TabIndex="2">
                                                                                            <asp:ListItem style="background-color: #e7e7e7"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td style="width: 30%;">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 20px">
                                                                                </tr>
                                                                                <tr>

                                                                                    <table width="100%">
                                                                                        <tr style="height: 15px">
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td style="width: 25%"></td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:Button ID="savebtn" runat="server" CssClass="savebutton1231"
                                                                                                    EnableTheming="false" Text="" Enabled="true" OnClick="savebtn_Click"></asp:Button>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <asp:Button ID="retrivebtn" runat="server" EnableTheming="false"
                                                                                                    CssClass="RRetrive" Enabled="true" OnClick="retrivebtn_Click"></asp:Button>
                                                                                            </td>
                                                                                            <td style="width: 25%"></td>
                                                                                        </tr>
                                                                                    </table>

                                                                                </tr>
                                                                            </table>
                                                                            <asp:ValidationSummary ID="ValidationSummary2" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="savebtn" EventName="Click" />
                                                                            <asp:PostBackTrigger ControlID="retrivebtn" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </ContentTemplate>
                                                        </cc1:TabPanel>
                                                    </cc1:TabContainer>
                                                </td>
                                            </tr>

                                            <table>
                                                <tr style="height: 8px">
                                                </tr>
                                                <tr>
                                                    <td style="width: 15%">&nbsp;
                                                    </td>
                                                    <td align="center" style="width: 30%">
                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Button ID="btnxls" runat="server" CssClass="exportexl6"
                                                                    EnableTheming="false" OnClick="btnxls_Click" Width="153px" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="btnxls" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td align="center" style="width: 5%">
                                                        <asp:UpdatePanel ID="UpdatePanel66" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:Button ID="clearbtn" runat="server" CssClass="RClear"
                                                                    EnableTheming="False" OnClientClick="document.location.href=document.location.href;" TabIndex="5" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="clearbtn" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td align="center" style="width: 30%">
                                                        <asp:Button ID="btncancel1" runat="server" CssClass="cancelbutton6"
                                                            EnableTheming="False" OnClientClick="window.close();" TabIndex="6" />
                                                    </td>
                                                    <td style="width: 15%">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </td>
                      </tr>
                   </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



