<%@ Page Language="C#" MasterPageFile="~/PageMaster.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="Purchase.aspx.cs" Inherits="Purchase"
    Title="Purchases > Supplier Purchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cplhTab" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cplhControlPanel" runat="Server">
    <script src="Scripts/JScriptPurchase.js" type="text/javascript">

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

            <div style="">
                <table style="width: 998px;" align="center">
                    <tr style="width: 100%">
                        <td style="width: 100%" valign="middle">
                            <%--<table class="mainConHd" style="width: 994px;">
                                <tr valign="middle">
                                    <td style="font-size: 20px;">
                                        Supplier Purchases
                                    </td>
                                </tr>
                            </table>--%>
                            <div class="mainConBody">
                                <div>
                                    <table cellspacing="2px" cellpadding="3px" border="0" width="100%"
                                        class="searchbg">
                                        <tr>
                                            <td style="width: 2%"></td>
                                            <td style="width: 35%; font-size: 22px; color: White;">Supplier Purchases
                                            </td>
                                            <td style="width: 15%">
                                                <div style="text-align: right;">
                                                </div>
                                            </td>
                                            <td style="width: 12%; color: #000000;" align="right">
                                                <%--Bill No.--%>
                                                Search
                                            </td>
                                            <td style="width: 18%; text-align: center" class="NewBox">
                                                <asp:TextBox ValidationGroup="search" ID="txtBillnoSrc" runat="server" MaxLength="8"
                                                    CssClass="cssTextBox" Width="92%" Visible="False"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtBillnoSrc"
                                                    FilterType="Numbers" />
                                                <asp:TextBox ID="txtSearch" runat="server" SkinID="skinTxtBoxSearch"></asp:TextBox>
                                            </td>
                                            <td style="width: 1%" align="center">
                                                <%--Trans. No.--%>
                                            </td>
                                            <td style="width: 18%" class="NewBox">
                                                <div style="width: 150px; font-family: 'Trebuchet MS';">
                                                    <asp:DropDownList ID="ddCriteria" runat="server" BackColor="White" Width="157px" Height="23px" Style="text-align: center; border: 1px solid White">
                                                        <asp:ListItem Value="0">All</asp:ListItem>
                                                        <asp:ListItem Value="BillNo">Bill No</asp:ListItem>
                                                        <asp:ListItem Value="TransNo">Trans No</asp:ListItem>
                                                        <asp:ListItem Value="VoucherNo">Voucher No</asp:ListItem>
                                                        <asp:ListItem Value="Date">Bill Date</asp:ListItem>
                                                        <asp:ListItem Value="VoucherDate">Voucher Date</asp:ListItem>
                                                        <asp:ListItem Value="SupplierName">Supplier Name</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:TextBox ValidationGroup="search" ID="txtTransNo" runat="server" MaxLength="8"
                                                    CssClass="cssTextBox" Visible="False"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtTransNo"
                                                    FilterType="Numbers" />
                                            </td>
                                            <td style="width: 18%; text-align: left">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="ButtonSearch6" EnableTheming="false" ForeColor="White" OnClick="btnSearch_Click" />
                                            </td>
                                            <td style="width: 17%" class="tblLeftNoPad">
                                                <asp:Button ID="BtnClearFilter1" runat="server" OnClick="BtnClearFilter_Click" EnableTheming="false" Text="" CssClass="ClearFilter6" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <cc1:ModalPopupExtender ID="ModalPopupMethod" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="CancelPopUpMethod" DynamicServicePath="" Enabled="True" PopupControlID="pnlMethod"
                                TargetControlID="ShowPopUpMethod">
                            </cc1:ModalPopupExtender>
                            <input id="ShowPopUpMethod" type="button" style="display: none" runat="server" />
                            <input id="CancelPopUpMethod" runat="server" style="display: none"
                                type="button" />
                            </input>
                                            </input>
                                            <asp:ValidationSummary ID="VSContact" runat="server" Font-Names="'Trebuchet MS'" Font-Size="12pt"
                                                HeaderText="Validation Messages" ShowMessageBox="true" ShowSummary="true" ValidationGroup="contact" />
                            <asp:Panel ID="pnlMethod" runat="server" Style="width: 55%; display: none" CssClass="chzn-container">
                                <asp:UpdatePanel ID="updatePnlMethod" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlContactItems" CssClass="pnlPopUp" runat="server">
                                            <div id="Div2" class="divArea">
                                                <table cellpadding="3" cellspacing="2" style="width: 100%" align="center">
                                                    <tr style="width: 100%">
                                                        <td style="width: 100%">
                                                            <table style="text-align: left; width: 100%; border: 1px solid Blue;" cellpadding="3" cellspacing="2">
                                                                <tr>

                                                                    <td>
                                                                        <table class="headerPopUp" width="100%">
                                                                            <tr>
                                                                                <td>Select the Purchase Type
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>

                                                                </tr>

                                                                <tr>

                                                                    <td>
                                                                        <table style="width: 100%;" cellpadding="3" cellspacing="1">
                                                                            <tr style="height: 10px">
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 16%"></td>
                                                                                <td style="width: 69%;" class="ControlTextBox3">

                                                                                    <asp:RadioButtonList ID="optionmethod" runat="server" Style="font-size: 14px" align="center"
                                                                                        RepeatDirection="Horizontal" BackColor="#e7e7e7" Height="35px">
                                                                                        <asp:ListItem Selected="True" Value="Purchase">Purchase&nbsp;&nbsp;</asp:ListItem>
                                                                                        <asp:ListItem Value="InternalTransfer">Internal Transfer&nbsp;&nbsp;</asp:ListItem>
                                                                                        <asp:ListItem Value="DeliveryNote">Delivery Return&nbsp;&nbsp;</asp:ListItem>
                                                                                        <asp:ListItem Value="SalesReturn">Sales Return</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                                <td style="width: 12%"></td>

                                                                            </tr>
                                                                            <tr style="height: 7px">
                                                                            </tr>
                                                                        </table>
                                                                    </td>

                                                                </tr>
                                                                <tr>

                                                                    <td>
                                                                        <table cellpadding="1" cellspacing="2"
                                                                            width="100%">
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <table>
                                                                                        <tr>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <asp:Panel ID="Panel4" runat="server" Width="120px">
                                                                                                    <asp:Button ID="cmdMethod" runat="server" CssClass="Start6"
                                                                                                        EnableTheming="false" OnClick="cmdMethod_Click" Text=""
                                                                                                        ValidationGroup="contact" />
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Panel ID="Panel5" runat="server" Width="120px">
                                                                                                    <asp:Button ID="cmdCancelMethod" runat="server" CssClass="cancelbutton6" OnClick="cmdCancelMethod_Click" CausesValidation="false"
                                                                                                        EnableTheming="false" />
                                                                                                </asp:Panel>
                                                                                            </td>
                                                                                        </tr>

                                                                                    </table>
                                                                                </td>

                                                                            </tr>
                                                                        </table>
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <asp:ValidationSummary ID="valSum" DisplayMode="BulletList" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="product" HeaderText="Validation Messages"
                                                                Font-Names="'Trebuchet MS'" Font-Size="12" runat="server" />
                                                            <asp:ValidationSummary ID="VS" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false"
                                                                ValidationGroup="purchaseval" HeaderText="Validation Messages" Font-Names="'Trebuchet MS'"
                                                                Font-Size="12" runat="server" />
                                                            <input id="dummyPurchase" type="button" style="display: none" runat="server" />
                                                            <input id="BtnPopUpCancel1" type="button" style="display: none" runat="server" />
                                                            <cc1:ModalPopupExtender ID="ModalPopupPurchase" runat="server" BackgroundCssClass="modalBackground"
                                                                CancelControlID="BtnPopUpCancel1" DynamicServicePath="" Enabled="True" PopupControlID="purchasePanel"
                                                                RepositionMode="RepositionOnWindowResizeAndScroll" TargetControlID="dummyPurchase">
                                                            </cc1:ModalPopupExtender>
                                                            <asp:Panel ID="purchasePanel" runat="server" Style="width: 95%; display: none">
                                                                <asp:UpdatePanel ID="updatePnlPurchase" runat="server" RenderMode="Block" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div id="Div1" style="background-color: White; width: 95%">
                                                                            <table style="width: 100%;" align="center">
                                                                                <tr style="width: 100%">
                                                                                    <td style="width: 100%">
                                                                                        <table style="text-align: left;" width="100%" cellpadding="0" cellspacing="2">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <table class="headerPopUp" width="95%">
                                                                                                        <tr>
                                                                                                            <td>Purchase Details
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <cc1:TabContainer ID="tabs2" runat="server" Width="1225px" CssClass="fancy fancy-green">
                                                                                                        <cc1:TabPanel ID="tabMaster" runat="server" HeaderText="Purchase Details" Width="1210px">
                                                                                                            <HeaderTemplate>
                                                                                                                Purchase Details
                                                                                                            </HeaderTemplate>
                                                                                                            <ContentTemplate>

                                                                                                                <table width="1210px" cellpadding="0" cellspacing="1">
                                                                                                                    <tr>
                                                                                                                        <td colspan="5"></td>
                                                                                                                    </tr>
                                                                                                                     <tr>                                                                                                                        
                                                                                                                        <td style="width: 25%;" class="ControlLabelproject">                                                                                                                           
                                                                                                                          Sales Invoice No
                                                                                                                        </td>
                                                                                                                        <td class="ControlDrpBorder" style="width: 24%">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:DropDownList ID="drpSalesID" runat="server" AutoPostBack="true" AppendDataBoundItems="true" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="BillNo" DataValueField="BillNo" Height="26px" Style="border: 1px solid #e7e7e7" TabIndex="2" ValidationGroup="salesval" Width="100%" OnSelectedIndexChanged="drpSalesID_SelectedIndexChanged">
                                                                                                                                        <asp:ListItem style="background-color: #e7e7e7" Text="Select Invoice No" Value="0"></asp:ListItem>
                                                                                                                                    </asp:DropDownList>
                                                                                                                                </ContentTemplate>
                                                                                                                                <Triggers>
                                                                                                                                  
                                                                                                                                </Triggers>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                         <td style="width: 10%;"></td>
                                                                                                                         <td style="width: 10%;"></td>
                                                                                                                         <td style="width: 24%;"></td>
                                                                                                                         <td style="width: 10%;"></td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="ControlLabelproject" style="width: 25%;">Invoice No. *
                                                                                                        <asp:RequiredFieldValidator ID="rvBill" runat="server" ControlToValidate="txtBillno"
                                                                                                            CssClass="lblFont" Display="Dynamic" ErrorMessage="Bill No. is mandatory" ValidationGroup="purchaseval">*</asp:RequiredFieldValidator>
                                                                                                                        </td>
                                                                                                                        <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                            <asp:TextBox ID="txtBillno" runat="server" MaxLength="8" CssClass="cssTextBox" BackColor="#e7e7e7" Width="80%"
                                                                                                                                ValidationGroup="purchaseval" BorderStyle="NotSet" Height="23px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%;"></td>
                                                                                                                        <td class="ControlLabelproject" style="width: 10%;">Purchase Entry Date
                                                                                                                        </td>
                                                                                                                        <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                            <asp:TextBox ID="txtInvoiveDate" Enabled="false" runat="server" CssClass="cssTextBox" MaxLength="10" Height="23px" BackColor="#e7e7e7"
                                                                                                                                ValidationGroup="purchaseval" Width="80%"></asp:TextBox>

                                                                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                                                                                OnClientDateSelectionChanged="checkDate" PopupButtonID="ImageButton1"
                                                                                                                                TargetControlID="txtInvoiveDate" Enabled="True">
                                                                                                                            </cc1:CalendarExtender>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%;"></td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>

                                                                                                                    <tr>
                                                                                                                        <td class="ControlLabelproject" style="width: 25%;">
                                                                                                                            <%--<asp:RequiredFieldValidator ID="reqSuppllier" runat="server" ControlToValidate="cmbSupplier"
                                                                                                            CssClass="lblFont" ErrorMessage="Supplier is mandatory" InitialValue="0" Text="*"
                                                                                                            ValidationGroup="purchaseval"></asp:RequiredFieldValidator>--%>
                                                                                                        Supplier *
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;" class="ControlDrpBorder">
                                                                                                                            <%--<asp:Panel ID="Panel18" runat="server" Width="100px">--%>
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="tabs2$TabPanel1$drpSalesReturn" EventName="SelectedIndexChanged" />
                                                                                                                                </Triggers>
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:DropDownList ID="cmbSupplier" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor="#e7e7e7" CssClass="chzn-select" DataTextField="LedgerName" DataValueField="LedgerID" Height="26px" OnSelectedIndexChanged="cmbSupplier_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" TabIndex="2" ValidationGroup="purchaseval" Width="313px">
                                                                                                                                        <asp:ListItem style="background-color: #90c9fc; color: Black" Text="Select Supplier"
                                                                                                                                            Value="0"></asp:ListItem>
                                                                                                                                    </asp:DropDownList>
                                                                                                                                    <asp:TextBox ID="txtSupplier" runat="server" BackColor="#e7e7e7" MaxLength="200" SkinID="skinTxtBoxGrid" TabIndex="8"></asp:TextBox>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                            <%--</asp:Panel> --%>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%;">
                                                                                                                            <asp:CheckBox runat="server" ID="chk" Text="Existing Supplier" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" />
                                                                                                                        </td>
                                                                                                                        <td class="ControlLabelproject" style="width: 10%;">
                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBillDate"
                                                                                                                                CssClass="lblFont" Display="Dynamic" ErrorMessage="BillDate is mandatory" Text="*"
                                                                                                                                ValidationGroup="purchaseval"></asp:RequiredFieldValidator>
                                                                                                                            <%--<asp:RangeValidator ID="rvBillDate" runat="server" ControlToValidate="txtBillDate"
                                                                                                            ErrorMessage="Purchase date cannot be future date." Text="*" Type="Date" ValidationGroup="purchaseval"></asp:RangeValidator>--%>
                                                                                                        Bill Date *

                                                                                                        
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%;" class="ControlTextBox3">
                                                                                                                            <asp:TextBox ID="txtBillDate" Enabled="false" runat="server" CssClass="cssTextBox" MaxLength="10" Height="23px" BackColor="#e7e7e7"
                                                                                                                                ValidationGroup="purchaseval" Width="80%"></asp:TextBox>
                                                                                                                            <cc1:CalendarExtender ID="calBillDate" runat="server" Format="dd/MM/yyyy"
                                                                                                                                PopupButtonID="btnBillDate"
                                                                                                                                TargetControlID="txtBillDate" Enabled="True">
                                                                                                                            </cc1:CalendarExtender>


                                                                                                                        </td>
                                                                                                                        <td style="width: 10%;">&nbsp;
                                                                                                        <asp:ImageButton ID="btnBillDate" ImageUrl="App_Themes/NewTheme/images/cal.gif" CausesValidation="False"
                                                                                                            Width="20px" runat="server" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="ControlLabelproject" style="width: 25%;" valign="middle">Address1 </td>
                                                                                                                        <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:TextBox ID="txtAddress1" runat="server" BackColor="#e7e7e7" MaxLength="10" SkinID="skinTxtBoxGrid" TabIndex="6" Width="200px"></asp:TextBox>
                                                                                                                                </ContentTemplate>
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmbSupplier" EventName="SelectedIndexChanged" />
                                                                                                                                </Triggers>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%;"></td>
                                                                                                                        <td class="ControlLabelproject" style="width: 10%;">Address2 </td>
                                                                                                                        <td class="ControlTextBox3" style="width: 24%">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:TextBox ID="txtAddress2" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" Width="500px"></asp:TextBox>
                                                                                                                                </ContentTemplate>
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmbSupplier" EventName="SelectedIndexChanged" />
                                                                                                                                </Triggers>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%;"></td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td class="ControlLabelproject" style="width: 25%;" valign="middle">Mobile </td>
                                                                                                                        <td class="ControlTextBox3" style="width: 24%;">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:TextBox ID="txtMobile" runat="server" BackColor="#e7e7e7" MaxLength="10" SkinID="skinTxtBoxGrid" TabIndex="6" Width="200px"></asp:TextBox>
                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxEx" runat="server" FilterType="Numbers" TargetControlID="txtMobile" />
                                                                                                                                </ContentTemplate>
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmbSupplier" EventName="SelectedIndexChanged" />
                                                                                                                                </Triggers>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%;"></td>
                                                                                                                        <td class="ControlLabelproject" style="width: 10%;">Address3 </td>
                                                                                                                        <td class="ControlTextBox3" style="width: 24%">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:TextBox ID="txtAddress3" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" MaxLength="200" SkinID="skinTxtBox" Width="500px"></asp:TextBox>
                                                                                                                                </ContentTemplate>
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmbSupplier" EventName="SelectedIndexChanged" />
                                                                                                                                </Triggers>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%;"></td>
                                                                                                                    </tr>

                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 25%" class="ControlLabelproject">Payment Mode

                                                                                                                        </td>
                                                                                                                        <td style="width: 24%" class="ControlDrpBorder">
                                                                                                                            <%--<asp:Panel ID="Panel19" runat="server" Width="100px">--%>
                                                                                                                            <asp:DropDownList ID="cmdPaymode" runat="server" AppendDataBoundItems="True" AutoCompleteMode="Suggest" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                                                AutoPostBack="True" OnSelectedIndexChanged="cmdPaymode_SelectedIndexChanged" Style="border: 1px solid #e7e7e7" Height="26px"
                                                                                                                                ValidationGroup="purchaseval" Width="100%">
                                                                                                                                <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="Bank / Credit Card" Value="2"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="Credit" Value="3"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                            <%--</asp:Panel> --%>                                                                                                        
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%;"></td>
                                                                                                                        <td style="width: 10%" class="ControlLabelproject">Fixed Total *
                                                                                                                        </td>
                                                                                                                        <td style="width: 24%" class="ControlTextBox3">
                                                                                                                            <%--<asp:TextBox ID="txtroundoff" runat="server" CssClass="cssTextBox" Width="100%" Height="23px"  BackColor = "#90c9fc"></asp:TextBox>--%>
                                                                                                                            <asp:TextBox ID="txtfixedtotal" runat="server" CssClass="cssTextBox" ValidationGroup="product" Text="0" Width="100%" BackColor="#e7e7e7"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 10%"></td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>                                                                                                                   
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="6">
                                                                                                                            <asp:UpdatePanel ID="UP1" runat="server" UpdateMode="Conditional">

                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:Panel ID="pnlBank" runat="server" Visible="false">
                                                                                                                                        <table cellpadding="2" cellspacing="2" width="100%">
                                                                                                                                            <tr style="height: 2px;">
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td class="ControlLabelproject" style="width: 25%;">
                                                                                                                                                    <asp:RequiredFieldValidator ID="rvbank" runat="server" ControlToValidate="cmbBankName" CssClass="lblFont" EnableClientScript="true" Enabled="false" ErrorMessage="Bank is mandatory" InitialValue="0" Text="*" ValidationGroup="purchaseval" />
                                                                                                                                                    Bank name * </td>
                                                                                                                                                <td class="ControlDrpBorder" style="width: 24%;"><%--<asp:Panel ID="Panel21" runat="server" Width="20%">--%>
                                                                                                                                                    <asp:DropDownList ID="cmbBankName" runat="server" AppendDataBoundItems="true" AutoPostBack="true" AutoCompleteMode="Suggest" OnSelectedIndexChanged="cmbBankName_SelectedIndexChanged" BackColor="#e7e7e7" CssClass="drpDownListMedium" DataTextField="LedgerName" DataValueField="LedgerID" Height="26px" Style="border: 1px solid #e7e7e7" ValidationGroup="purchaseval" Width="100%">
                                                                                                                                                        <asp:ListItem style="background-color: #e7e7e7" Text="Select Bank" Value="0"></asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                    <%--</asp:Panel> --%></td>
                                                                                                                                                <td style="width: 7%;"></td>
                                                                                                                                                <td class="ControlLabelproject" style="width: 13%">
                                                                                                                                                    <asp:RequiredFieldValidator ID="rvCheque" runat="server" ControlToValidate="cmbChequeNo" EnableClientScript="true" Enabled="false" ErrorMessage="Cheque No. is mandatory" Text="*" ValidationGroup="purchaseval" />
                                                                                                                                                    Cheque / Credit Card No.* </td>
                                                                                                                                                <td class="ControlDrpBorder" style="width: 24%;"><%--<asp:Panel ID="Panel21" runat="server" Width="20%">--%>
                                                                                                                                                    <asp:TextBox ID="txtChequeNo" runat="server" BackColor="#e7e7e7" CssClass="cssTextBox" MaxLength="1" Width="0%" Visible="false"></asp:TextBox>
                                                                                                                                                    <asp:DropDownList ID="cmbChequeNo" runat="server" AppendDataBoundItems="True" AutoPostBack="true" BackColor="#e7e7e7" DataTextField="ChequeNo" DataValueField="ChequeNo" CssClass="drpDownListMedium" Height="26px" Style="border: 1px solid #e7e7e7" Width="100%">
                                                                                                                                                        <asp:ListItem Selected="True" style="height: 1px; background-color: #e7e7e7" Value="0">Select Cheque No</asp:ListItem>
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                    <%--</asp:Panel> --%></td>
                                                                                                                                                <td style="width: 13%;"></td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                                                                                                            TargetControlID="txtChequeNo" />
                                                                                                                                    </asp:Panel>
                                                                                                                                </ContentTemplate>
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdPaymode" EventName="SelectedIndexChanged" />
                                                                                                                                </Triggers>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                    </tr>


                                                                                                                    <tr>
                                                                                                                        <td colspan="5">
                                                                                                                            <table style="width: 100%;">
                                                                                                                                <tr>
                                                                                                                                    <td style="width: 72%;">
                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                                                                                            <ContentTemplate>
                                                                                                                                            </ContentTemplate>
                                                                                                                                            <Triggers>
                                                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />--%>
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbSupplier" EventName="SelectedIndexChanged" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmdSave" EventName="Click" />
                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmdUpdate" EventName="Click" />
                                                                                                                                                <%-- <asp:AsyncPostBackTrigger ControlID="cmdUpdateProduct" EventName="Click" />
                                                                                                                            <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />--%>
                                                                                                                                            </Triggers>
                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                    </td>
                                                                                                                                    <td style="width: 18%;"></td>
                                                                                                                                    <td style="width: 10%;"></td>
                                                                                                                                </tr>
                                                                                                                            </table>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>



                                                                                                            </ContentTemplate>
                                                                                                        </cc1:TabPanel>
                                                                                                        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Product Details">
                                                                                                            <HeaderTemplate>
                                                                                                                Product Details
                                                                                                            </HeaderTemplate>
                                                                                                            <ContentTemplate>
                                                                                                                <table width="600px" cellpadding="1" cellspacing="1" class="tblLeft">

                                                                                                                    <tr>
                                                                                                                        <td align="center" colspan="4">
                                                                                                                            <asp:HiddenField ID="hdRole" runat="server" Value="N" />
                                                                                                                            <asp:HiddenField ID="hdStock" runat="server" Value="0" />
                                                                                                                            <asp:HiddenField ID="hdPurchase" runat="server" Value="0" />
                                                                                                                            <asp:HiddenField ID="hdFilename" runat="server" Value="0" />
                                                                                                                            <asp:HiddenField ID="hdTotalAmt" runat="server" Value="0" />
                                                                                                                            <asp:HiddenField ID="hdMode" runat="server" Value="New" />
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmdUpdateProduct" EventName="Click" />
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="GrdViewItems" EventName="SelectedIndexChanged" />
                                                                                                                                    <%--<asp:AsyncPostBackTrigger ControlID="cmdUpdate" EventName="Click" />--%>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                                                                                                                </Triggers>
                                                                                                                                <ContentTemplate>
                                                                                                                                    <asp:Panel ID="pnlPurchase" runat="server">
                                                                                                                                        <table cellpadding="0" cellspacing="1" style="border: 0px solid #86b2d1;"
                                                                                                                                            width="100%">
                                                                                                                                            <tr>
                                                                                                                                                <td align="left" colspan="4">
                                                                                                                                                    <cc1:ModalPopupExtender ID="ModalPopupProduct" runat="server"
                                                                                                                                                        BackgroundCssClass="modalBackground" CancelControlID="CancelPopUp"
                                                                                                                                                        DynamicServicePath="" Enabled="True" PopupControlID="pnlItems"
                                                                                                                                                        TargetControlID="ShowPopUp">
                                                                                                                                                    </cc1:ModalPopupExtender>
                                                                                                                                                    <input id="ShowPopUp" runat="server" style="display: none" type="button" />
                                                                                                                                                    &nbsp;
                                                                                                        <input id="CancelPopUp" runat="server" style="display: none" type="button"></input>
                                                                                                                                                    <asp:Panel ID="pnlItems" runat="server" CssClass="pnlPopUp" Width="700px">
                                                                                                                                                        <asp:UpdatePanel ID="updatePnlProduct" runat="server" UpdateMode="Conditional" Visible="false">
                                                                                                                                                            <ContentTemplate>
                                                                                                                                                                <div id="contentPopUp">
                                                                                                                                                                    <table cellpadding="0" cellspacing="3" class="tblLeft" style="border: 1px solid blue;"
                                                                                                                                                                        width="100%">
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td>
                                                                                                                                                                                <table class="headerPopUp" width="100%">
                                                                                                                                                                                    <tr>
                                                                                                                                                                                        <td>Product Details
                                                                                                                                                                                        </td>
                                                                                                                                                                                    </tr>
                                                                                                                                                                                </table>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>
                                                                                                                                                                        <tr>
                                                                                                                                                                            <td>
                                                                                                                                                                                <table style="border: 0px solid #5078B3" cellpadding="3" cellspacing="2"
                                                                                                                                                                                    width="100%">
                                                                                                                                                                                    <tr>
                                                                                                                                                                                    </tr>

                                                                                                                                                                                    <tr id="rowBarcode" runat="server">
                                                                                                                                                                                        <td id="Td1" runat="server" class="ControlLabel" style="width: 17%">Barcode
                                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBarcode"
                                                                                                                                                    CssClass="lblFont" Text="BarCode is mandatory" ValidationGroup="lookUp"></asp:RequiredFieldValidator>
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td id="Td2" runat="server" class="ControlTextBox3" style="width: 22%">
                                                                                                                                                                                            <asp:TextBox ID="txtBarcode" runat="server" CssClass="cssTextBox" Width="50px"></asp:TextBox>
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td id="Td3" runat="server" style="width: 9%">
                                                                                                                                                                                            <asp:Button ID="cmdBarcode" runat="server" OnClick="txtBarcode_Populated" SkinID="skinBtnMedium"
                                                                                                                                                                                                Text="Lookup Product" ValidationGroup="lookUp" />
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 22%"></td>
                                                                                                                                                                                        <td style="width: 15%"></td>
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr style="height: 2px">
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr>

                                                                                                                                                                                        <td class="ControlLabel" style="width: 17%">Category *
                                                                                                                                                <asp:CompareValidator ID="cvCatergory" runat="server" ControlToValidate="cmbCategory"
                                                                                                                                                    Display="Dynamic" ErrorMessage="Category is Mandatory" Operator="GreaterThan"
                                                                                                                                                    Text="*" ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlDrpBorder" style="width: 28%">
                                                                                                                                                                                            <%--<asp:Panel ID="Panel1" runat="server" Width="90%">--%>
                                                                                                                                                                                            <asp:DropDownList ID="cmbCategory" runat="server" AutoPostBack="true" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                                                                                                                Width="100%" OnSelectedIndexChanged="LoadProducts" AppendDataBoundItems="True" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                                                                                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Category</asp:ListItem>
                                                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                                                            <%--</asp:Panel>--%> 
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlLabel" style="width: 16.7%">Product Code
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlDrpBorder" style="width: 28.4%">
                                                                                                                                                                                            <%--<asp:Panel ID="Panel4" runat="server"  Width="90%">--%>
                                                                                                                                                                                            <asp:DropDownList ID="cmbProdAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true" BackColor="#e7e7e7"
                                                                                                                                                                                                CssClass="drpDownListMedium" DataTextField="ProductName" DataValueField="ItemCode"
                                                                                                                                                                                                OnSelectedIndexChanged="LoadForProduct" ValidationGroup="product" Width="100%" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                                                                                                                                                <asp:ListItem style="background-color: #e7e7e7;" Text="Select Product" Value="0"></asp:ListItem>
                                                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                                                            <%--</asp:Panel>--%> 
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 15%"></td>
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr style="height: 2px">
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr>

                                                                                                                                                                                        <td class="ControlLabel" style="width: 17%">Product Name
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                                                                            <%--<asp:Panel ID="Panel5" runat="server" Width="90%">--%>
                                                                                                                                                                                            <asp:DropDownList ID="cmbProdName" runat="server" Width="100%" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                                                                                                                AutoPostBack="true" OnSelectedIndexChanged="LoadForProductName" AppendDataBoundItems="True" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                                                                                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select ItemName</asp:ListItem>
                                                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                                                            <asp:TextBox ID="lblProdNameAdd" runat="server" CssClass="cssTextBox" ReadOnly="true"
                                                                                                                                                                                                Visible="false" Width="196px" Enabled="false"></asp:TextBox>
                                                                                                                                                                                            <%--</asp:Panel> --%>
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlLabel" style="width: 9%">Brand
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                                                                            <%--<asp:Panel ID="Panel6" runat="server"  Width="90%">--%>
                                                                                                                                                                                            <asp:DropDownList ID="cmbBrand" runat="server" Width="100%" AutoPostBack="true" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                                                                                                                OnSelectedIndexChanged="LoadForBrand" AppendDataBoundItems="True" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                                                                                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Brand</asp:ListItem>
                                                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                                                            <%--</asp:Panel> --%>
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 15%" align="center"></td>
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr style="height: 2px">
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr>
                                                                                                                                                                                        <td class="ControlLabel" style="width: 17%">Model
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlDrpBorder" style="width: 22%">
                                                                                                                                                                                            <%--<asp:Panel ID="Panel7" runat="server" Width="90%">--%>
                                                                                                                                                                                            <asp:DropDownList ID="cmbModel" runat="server" OnSelectedIndexChanged="LoadForModel" CssClass="drpDownListMedium" BackColor="#e7e7e7"
                                                                                                                                                                                                AutoPostBack="true" Width="100%" AppendDataBoundItems="True" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                                                                                                                                                <asp:ListItem Selected="True" Value="0" style="background-color: #e7e7e7">Select Model</asp:ListItem>
                                                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                                                            <%--</asp:Panel> --%>
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlLabel" style="width: 9%;">Stock
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 22%" class="ControlTextBox3">
                                                                                                                                                                                            <asp:TextBox ID="txtstock" runat="server" CssClass="cssTextBox" ValidationGroup="product" Width="70px" Height="23px" BackColor="#e7e7e7"
                                                                                                                                                                                                Enabled="False"></asp:TextBox>
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 10%" align="left" valign="middle"></td>
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr style="height: 1px">

                                                                                                                                                                                        <td>
                                                                                                                                                                                            <asp:TextBox ID="lblProdDescAdd" runat="server" Enabled="false"
                                                                                                                                                                                                Visible="false"></asp:TextBox>
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td></td>
                                                                                                                                                                                        <td></td>
                                                                                                                                                                                        <td style="width: 15%"></td>
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <%--<tr style="height:1px">
                                                                                                                                        </tr>--%>
                                                                                                                                                                                </table>
                                                                                                                                                                            </td>
                                                                                                                                                                        </tr>

                                                                                                                                                                        <tr>
                                                                                                                                                                            <td>
                                                                                                                                                                                <table style="width: 100%; border: 0px solid #5078B3;" cellpadding="3" cellspacing="2">
                                                                                                                                                                                    <%--<tr style="height:1px">
                                                                                                                                        </tr>--%>
                                                                                                                                                                                    <tr>
                                                                                                                                                                                        <td style="width: 17%" class="ControlLabel">Rate
                                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRateAdd"
                                                                                                                                                    ErrorMessage="Product Rate is mandatory" Text="*" ValidationGroup="product"></asp:RequiredFieldValidator>

                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlTextBox3" style="width: 28%">
                                                                                                                                                                                            <asp:TextBox ID="txtRateAdd" runat="server" ValidationGroup="product" BackColor="#e7e7e7" Width="100%"
                                                                                                                                                                                                Height="20px" CssClass="cssTextBox"></asp:TextBox>
                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                                                                TargetControlID="txtRateAdd" ValidChars="." />
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 16.7%" class="ControlLabel">NLP
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlTextBox3" style="width: 28.4%">
                                                                                                                                                                                            <asp:TextBox ID="txtNLPAdd" runat="server" ValidationGroup="product" Width="100%" BackColor="#e7e7e7" CssClass="cssTextBox"
                                                                                                                                                                                                Height="20px"></asp:TextBox>
                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="fltNLP" runat="server" FilterType="Numbers, Custom"
                                                                                                                                                                                                TargetControlID="txtNLPAdd" ValidChars="." />
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 17.6%"></td>
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr style="height: 2px">
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr>
                                                                                                                                                                                        <td style="width: 17%" class="ControlLabel">Qty.
                                                                                                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQtyAdd"
                                                                                                                                                    Display="Dynamic" ErrorMessage="Qty. must be greater than Zero!!" Operator="GreaterThan"
                                                                                                                                                    Text="*" ValidationGroup="product" ValueToCompare="0"></asp:CompareValidator>
                                                                                                                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txtQtyAdd"
                                                                                                                                                                                                ErrorMessage="Qty. is mandatory" Text="*" ValidationGroup="product"></asp:RequiredFieldValidator>

                                                                                                                                                                                        </td>

                                                                                                                                                                                        <td class="ControlTextBox3" style="width: 26%">
                                                                                                                                                                                            <asp:TextBox ID="txtQtyAdd" runat="server" CssClass="cssTextBox" ValidationGroup="product" Width="100%" BackColor="#e7e7e7"
                                                                                                                                                                                                Height="23px"></asp:TextBox>
                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                                                                TargetControlID="txtQtyAdd" ValidChars="." />
                                                                                                                                                                                        </td>

                                                                                                                                                                                        <td style="width: 11%" class="ControlLabel">Disc (%)
                                                                                                                                                <asp:RegularExpressionValidator ID="regextxtPercentage" runat="server" ControlToValidate="lblDisAdd"
                                                                                                                                                    Display="None" ErrorMessage="Invalid % in Discount" Text="*" ValidationExpression="^\d{1,3}($|\.\d{1,2}$)"
                                                                                                                                                    ValidationGroup="product"></asp:RegularExpressionValidator>
                                                                                                                                                                                            <asp:RangeValidator ID="cvDisc" runat="server" ControlToValidate="lblDisAdd" Display="Dynamic"
                                                                                                                                                                                                ErrorMessage="Discount cannot be Greater than 100% and Less than 0%" MaximumValue="100"
                                                                                                                                                                                                MinimumValue="0" Text="*" Type="Double"></asp:RangeValidator>

                                                                                                                                                                                        </td>

                                                                                                                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                                                                                                                            <asp:TextBox ID="lblDisAdd" runat="server" CssClass="cssTextBox" Height="23px" Width="100%" BackColor="#e7e7e7"></asp:TextBox>
                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                                                                TargetControlID="lblDisAdd" ValidChars="." />
                                                                                                                                                                                            <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="cssTextBox" Width="20%" Height="23px"  BackColor = "#90c9fc" ></asp:TextBox>--%>
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 12%"></td>
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr style="height: 2px">
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr>
                                                                                                                                                                                        <td style="width: 17%" class="ControlLabel">VAT (%)
                                                                                                                                                <asp:RegularExpressionValidator ID="regextxtPercentage2" runat="server" ControlToValidate="lblVATAdd"
                                                                                                                                                    Display="None" ErrorMessage="Invalid % in VAT" Text="*" ValidationExpression="^\d{1,3}($|\.\d{1,2}$)"
                                                                                                                                                    ValidationGroup="product"></asp:RegularExpressionValidator>
                                                                                                                                                                                            <asp:RangeValidator ID="cvVAT" runat="server" ControlToValidate="lblVATAdd" Display="Dynamic"
                                                                                                                                                                                                ErrorMessage="VAT cannot be Greater than 100% and Less than 0%" MaximumValue="100"
                                                                                                                                                                                                MinimumValue="0" Text="*" Type="Double"></asp:RangeValidator>

                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlTextBox3" style="width: 26%">
                                                                                                                                                                                            <asp:TextBox ID="lblVATAdd" runat="server" CssClass="cssTextBox" Height="23px" Width="100%" BackColor="#e7e7e7"> </asp:TextBox>
                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender55" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                                                                TargetControlID="lblVATAdd" ValidChars="." />
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 11%" class="ControlLabel">CST (%)
                                                                                                                                                <asp:RangeValidator ID="cvCST" runat="server" ControlToValidate="lblCSTAdd" Display="Dynamic"
                                                                                                                                                    ErrorMessage="CST cannot be Greater than 100% and Less than 0%" MaximumValue="100"
                                                                                                                                                    MinimumValue="0" Text="*" Type="Double"></asp:RangeValidator>

                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td class="ControlTextBox3" style="width: 25%">
                                                                                                                                                                                            <asp:TextBox ID="lblCSTAdd" runat="server" CssClass="cssTextBox" Text="0" Width="100%" ValidationGroup="product" BackColor="#e7e7e7" Height="23px"></asp:TextBox>
                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                                                                TargetControlID="lblCSTAdd" ValidChars="." />
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 12%"></td>
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr style="height: 2px">
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr id="rowdiscamt" visible="true">
                                                                                                                                                                                        <td style="width: 17%" class="ControlLabel">Disc Amt
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 26%" class="ControlTextBox3">
                                                                                                                                                                                            <asp:TextBox ID="lbldiscamt" runat="server" CssClass="cssTextBox" Width="100%" Height="23px" BackColor="#e7e7e7"></asp:TextBox>
                                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers"
                                                                                                                                                                                                TargetControlID="lbldiscamt" ValidChars="." />
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 11%" class="ControlLabel">Stock Level
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 25%" class="ControlTextBox3">
                                                                                                                                                                                            <asp:TextBox ID="txtrol" runat="server" CssClass="cssTextBox" Width="70px" Height="23px" BackColor="#e7e7e7"
                                                                                                                                                                                                Enabled="False"></asp:TextBox>
                                                                                                                                                                                        </td>
                                                                                                                                                                                        <td style="width: 12%"></td>

                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr style="height: 3px">
                                                                                                                                                                                    </tr>
                                                                                                                                                                                    <tr>
                                                                                                                                                                                        <td align="center" colspan="6">
                                                                                                                                                                                            <table>
                                                                                                                                                                                                <td align="center">
                                                                                                                                                                                                    <asp:Panel ID="Panel2" runat="server" Width="120px">
                                                                                                                                                                                                        <asp:Button ID="cmdCancelProduct" runat="server" EnableTheming="false" CssClass="CloseWindow6" Text="" OnClick="cmdCancelProduct_Click" />
                                                                                                                                                                                                        <%--<asp:Label ID="Label1" runat="server" Text="Close Window" Font-Bold="True"></asp:Label>--%>
                                                                                                                                                                                                    </asp:Panel>
                                                                                                                                                                                                </td>
                                                                                                                                                                                                <td align="center">
                                                                                                                                                                                                    <asp:Panel ID="Panel3" runat="server" Width="120px">
                                                                                                                                                                                                        <asp:Button ID="cmdSaveProduct" runat="server" OnClick="cmdSaveProduct_Click" EnableTheming="false" CssClass="AddProd6"
                                                                                                                                                                                                            Text="" ValidationGroup="product" />
                                                                                                                                                                                                        <%--<asp:Label ID="Label2" runat="server" Text="Add Product" Font-Bold="True"></asp:Label>--%>
                                                                                                                                                                                                        <asp:Button ID="cmdUpdateProduct" runat="server" Enabled="false" OnClick="cmdUpdateProduct_Click" EnableTheming="false" CssClass="UpdateProd6" Width="38px" Height="37px"
                                                                                                                                                                                                            Text="" ValidationGroup="product" />
                                                                                                                                                                                                        <%--<asp:Label ID="Label3" runat="server" Enabled="false" Text="Update Product" Font-Bold="True"></asp:Label>--%>
                                                                                                                                                                                                        <asp:TextBox ID="lblUnitMrmnt" runat="server" CssClass="cssTextBox" Visible="False"></asp:TextBox>
                                                                                                                                                                                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ListProducts"
                                                                                                                                                                                                            TypeName="BusinessLogic"></asp:ObjectDataSource>
                                                                                                                                                                                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                                                                                                                                                                            <Triggers>
                                                                                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmbProdAdd" EventName="SelectedIndexChanged" />
                                                                                                                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />--%>
                                                                                                                                                                                                                <asp:AsyncPostBackTrigger ControlID="cmdSaveProduct" EventName="Click" />
                                                                                                                                                                                                            </Triggers>
                                                                                                                                                                                                            <ContentTemplate>
                                                                                                                                                                                                                <asp:HiddenField ID="hdCurrentRow" runat="server" Value="0" />
                                                                                                                                                                                                            </ContentTemplate>
                                                                                                                                                                                                        </asp:UpdatePanel>
                                                                                                                                                                                                    </asp:Panel>
                                                                                                                                                                                                </td>
                                                                                                                                                                                                <td>
                                                                                                                                                                                                    <asp:Panel ID="Panel8" runat="server" Width="120px">
                                                                                                                                                                                                        <asp:Button ID="BtnClearFilter" runat="server" EnableTheming="false" CssClass="ClearFilter666" OnClick="btnClearFilter_Click"
                                                                                                                                                                                                            Text="" />
                                                                                                                                                                                                    </asp:Panel>
                                                                                                                                                                                                </td>
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
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td style="width: 50%;">
                                                                                                                                                    <asp:GridView ID="GrdViewItems" runat="server" BorderWidth="1px" DataKeyNames="itemcode" Visible="false"
                                                                                                                                                        EmptyDataText="No Purchase Items added." OnPageIndexChanging="GrdViewItems_PageIndexChanging"
                                                                                                                                                        OnRowCancelingEdit="GrdViewItems_RowCancelingEdit" OnRowCreated="GrdViewItems_RowCreated"
                                                                                                                                                        OnRowDataBound="GrdViewItems_RowDataBound" OnRowDeleting="GrdViewItems_RowDeleting"
                                                                                                                                                        OnRowEditing="GrdViewItems_RowEditing" OnRowUpdating="GrdViewItems_RowUpdating"
                                                                                                                                                        OnSelectedIndexChanged="GrdViewItems_SelectedIndexChanged" ShowFooter="false" CssClass="someClass"
                                                                                                                                                        Width="100%">
                                                                                                                                                        <%--<HeaderStyle ForeColor="Black" />--%>
                                                                                                                                                        <EditRowStyle VerticalAlign="Middle" />
                                                                                                                                                        <RowStyle Font-Bold="false" />
                                                                                                                                                        <FooterStyle CssClass="HeadataRow" Font-Bold="true" Height="27px" />
                                                                                                                                                        <Columns>
                                                                                                                                                            <asp:BoundField DataField="itemcode" HeaderText="Product Code" HeaderStyle-BorderColor="Gray" />
                                                                                                                                                            <asp:BoundField DataField="ProductName" HeaderText="Product Name &nbsp;" HeaderStyle-BorderColor="Gray" />
                                                                                                                                                            <asp:BoundField DataField="PurchaseRate" HeaderText="Rate &nbsp;" HeaderStyle-BorderColor="Gray" />
                                                                                                                                                            <asp:BoundField DataField="NLP" HeaderText="NLP &nbsp;" HeaderStyle-BorderColor="Gray" />
                                                                                                                                                            <asp:BoundField DataField="Qty" HeaderText="Qty. &nbsp;" HeaderStyle-BorderColor="Gray" />
                                                                                                                                                            <asp:BoundField DataField="Measure_Unit" HeaderText="Unit &nbsp;" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                                                                                                                            <asp:BoundField DataField="Discount" HeaderText="Disc(%) &nbsp;" HeaderStyle-BorderColor="Gray" />
                                                                                                                                                            <asp:BoundField DataField="vat" HeaderText="VAT(%) &nbsp;" HeaderStyle-BorderColor="Gray" />
                                                                                                                                                            <asp:BoundField DataField="CST" HeaderText="CST(%) &nbsp;" HeaderStyle-BorderColor="Gray" />
                                                                                                                                                            <asp:BoundField DataField="Discountamt" HeaderText="DiscAmt &nbsp;" HeaderStyle-BorderColor="Gray" />
                                                                                                                                                            <asp:TemplateField FooterStyle-Font-Bold="True" HeaderText="Total &nbsp;" HeaderStyle-BorderColor="Gray">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <%# GetTotal(Convert.ToDouble(Eval("Qty").ToString()), Convert.ToDouble(Eval("purchaserate").ToString()), Convert.ToDouble(Eval("discount").ToString()), Convert.ToDouble(Eval("vat").ToString()), Convert.ToDouble(Eval("CST").ToString()), Convert.ToDouble(Eval("Discountamt").ToString()))%>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="Role Type" Visible="false" HeaderStyle-BorderColor="Gray">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <%# Eval("isRole")%>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                <ItemStyle VerticalAlign="Top" />
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-CssClass="command" HeaderStyle-BorderColor="Gray" HeaderText="Edit">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Select" SkinID="edit" />
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                            <asp:TemplateField HeaderStyle-Width="30px" HeaderText="Delete">
                                                                                                                                                                <ItemTemplate>
                                                                                                                                                                    <asp:ImageButton ID="lnkB" runat="Server" CommandName="Delete" SkinID="delete" HeaderStyle-BorderColor="Gray" />
                                                                                                                                                                    <cc1:ConfirmButtonExtender ID="CnrfmDel" runat="server" ConfirmText="Are you sure to delete this product from purchase?"
                                                                                                                                                                        TargetControlID="lnkB">
                                                                                                                                                                    </cc1:ConfirmButtonExtender>
                                                                                                                                                                </ItemTemplate>
                                                                                                                                                                <HeaderStyle Width="30px" />
                                                                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                                                            </asp:TemplateField>
                                                                                                                                                        </Columns>
                                                                                                                                                        <PagerTemplate>
                                                                                                                                                            Goto Page
                                                                                                                                        <asp:DropDownList ID="ddlPageSelector2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSelector2_SelectedIndexChanged"
                                                                                                                                            SkinID="skinPagerDdlBox">
                                                                                                                                        </asp:DropDownList>
                                                                                                                                                            <asp:Button ID="btnFirst" runat="server" CommandArgument="First" CommandName="Page" CssClass="NewFirst" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                Text="" />
                                                                                                                                                            <asp:Button ID="btnPrevious" runat="server" CommandArgument="Prev" CommandName="Page" CssClass="NewPrev" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                Text="" />
                                                                                                                                                            <asp:Button ID="btnNext" runat="server" CommandArgument="Next" CommandName="Page" CssClass="NewNext" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                Text="" />
                                                                                                                                                            <asp:Button ID="btnLast" runat="server" CommandArgument="Last" CommandName="Page" CssClass="NewLast" EnableTheming="false" Width="22px" Height="18px"
                                                                                                                                                                Text="" />
                                                                                                                                                        </PagerTemplate>
                                                                                                                                                    </asp:GridView>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                            <tr>
                                                                                                                                                <td colspan="4">
                                                                                                                                                    <div style="height: 250px; width: 1190px; overflow: scroll">
                                                                                                                                                        <center>
                                                                                                                                                            <asp:GridView ID="grvStudentDetails" runat="server" Width="50%" AllowPaging="True"
                                                                                                                                                                ShowFooter="True" AutoGenerateColumns="False" OnRowDeleting="grvStudentDetails_RowDeleting" OnRowDataBound="grvStudentDetails_RowDataBound"
                                                                                                                                                                CellPadding="1"
                                                                                                                                                                GridLines="None">

                                                                                                                                                                <%--<RowStyle CssClass="dataRow" />
                                                                                                                                                        <SelectedRowStyle CssClass="SelectdataRow" />
                                                                                                                                                        <AlternatingRowStyle CssClass="altRow" />
                                                                                                                                                        <EmptyDataRowStyle CssClass="HeadataRow" Font-Bold="true" />
                                                                                                                                                        <HeaderStyle CssClass="HeadataRow" Wrap="false" />
                                                                                                                                                        <FooterStyle CssClass="dataRow" />--%>

                                                                                                                                                                <Columns>
                                                                                                                                                                    <asp:BoundField DataField="RowNumber" HeaderText="#" ItemStyle-Width="45px" ItemStyle-Font-Size="15px" HeaderStyle-ForeColor="Black" />
                                                                                                                                                                    <asp:TemplateField HeaderText="Product Code-Name-Model" ItemStyle-Width="1px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:DropDownList ID="drpPrd" Width="300px" runat="server" AppendDataBoundItems="true" BackColor="White" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" CssClass="chzn-select" DataTextField="ProductName" DataValueField="ItemCode" OnSelectedIndexChanged="drpPrd_SelectedIndexChanged">
                                                                                                                                                                            </asp:DropDownList>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="65px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="Qty" runat="server" FilterType="Numbers" TargetControlID="txtQty" />
                                                                                                                                                                            <asp:TextBox ID="txtQty" Style="text-align: right" runat="server" Width="65px" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Return Quantity" ItemStyle-Width="65px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="rtQty" runat="server" FilterType="Numbers" TargetControlID="txtRtnQty" />
                                                                                                                                                                            <asp:TextBox ID="txtRtnQty" Style="text-align: right" runat="server" Width="65px" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" OnTextChanged="txtRtnQty_TextChanged"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Width="75px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                             <cc1:FilteredTextBoxExtender ID="rte" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtRate" ValidChars="." />
                                                                                                                                                                            <asp:TextBox ID="txtRate" Style="text-align: right" runat="server" Width="75px" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="NLP" ItemStyle-Width="75px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <cc1:FilteredTextBoxExtender ID="nlp" runat="server" FilterType="Numbers" TargetControlID="txtNLP" />
                                                                                                                                                                            <asp:TextBox ID="txtNLP" Style="text-align: right" runat="server" Width="75px" ForeColor="#0567AE" Font-Bold="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Disc(%)" ItemStyle-Width="50px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                             <cc1:FilteredTextBoxExtender ID="dispre" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtDisPre" ValidChars="." />
                                                                                                                                                                            <asp:TextBox ID="txtDisPre" Style="text-align: right" runat="server" Width="50px" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="VAT(%)" ItemStyle-Width="50px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                             <cc1:FilteredTextBoxExtender ID="vatpre" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtVATPre" ValidChars="." />
                                                                                                                                                                            <asp:TextBox ID="txtVATPre" Style="text-align: right" runat="server" Width="50px" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="CST(%)" ItemStyle-Width="50px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                             <cc1:FilteredTextBoxExtender ID="cstpre" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtCSTPre" ValidChars="." />
                                                                                                                                                                            <asp:TextBox ID="txtCSTPre" Style="text-align: right" runat="server" Width="50px" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Discount Amount" ItemStyle-Width="70px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                             <cc1:FilteredTextBoxExtender ID="diamt" runat="server" FilterType="Numbers" TargetControlID="txtDiscAmt" />
                                                                                                                                                                            <asp:TextBox ID="txtDiscAmt" Style="text-align: right" runat="server" Width="70px" ForeColor="#0567AE" Font-Bold="false" AutoPostBack="true" OnTextChanged="txtQty_TextChanged"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:TemplateField HeaderText="Row Total" ItemStyle-Width="100px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="Black">
                                                                                                                                                                        <ItemTemplate>
                                                                                                                                                                            <asp:TextBox ID="txtTotal" Style="text-align: right" runat="server" ReadOnly="true" Width="100px" ForeColor="#0567AE" Font-Bold="false"></asp:TextBox>
                                                                                                                                                                        </ItemTemplate>
                                                                                                                                                                        <FooterStyle HorizontalAlign="Left" />
                                                                                                                                                                        <FooterTemplate>
                                                                                                                                                                            <asp:Button ID="ButtonAdd" runat="server" AutoPostback="true" EnableTheming="false" OnClick="ButtonAdd_Click"
                                                                                                                                                                                ValidationGroup="DynRowAdd" Text="Add New" />
                                                                                                                                                                        </FooterTemplate>
                                                                                                                                                                    </asp:TemplateField>
                                                                                                                                                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                                                                                                                                                                </Columns>
                                                                                                                                                                <%-- <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                                                                                        <RowStyle BackColor="#EFF3FB" />
                                                                                                                                                        <EditRowStyle BackColor="#2461BF" />
                                                                                                                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                                                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                                                                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                                                                                                        <AlternatingRowStyle BackColor="White" />--%>
                                                                                                                                                            </asp:GridView>
                                                                                                                                                        </center>
                                                                                                                                                    </div>
                                                                                                                                                </td>
                                                                                                                                            </tr>
                                                                                                                                        </table>
                                                                                                                                    </asp:Panel>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                            <%--<br>
                                                                                                        <br>
                                                                                                        <br></br>
                                                                                                        <br></br>
                                                                                                        </br>
                                                                                                        </br>--%>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 5px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td>
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                                                                                                                                <ContentTemplate>
                                                                                                                                    <table width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <%--<td style="width:13px">
                                                                                                                        </td>--%>
                                                                                                                                            <td style="text-align: left;">
                                                                                                                                                <div>
                                                                                                                                                    <div>
                                                                                                                                                        <%--<div>--%>
                                                                                                                                                        <table border="0" cellpadding="0px" cellspacing="5px" style="margin: 0px auto;">
                                                                                                                                                            <tr style="display: none">
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <asp:Label ID="lblDispTotal" runat="server" CssClass="item3" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td width="1px"></td>
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <asp:Label ID="lblTotalSum" runat="server" CssClass="item3" Font-Bold="true" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr style="display: none">
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <asp:Label ID="lblDispDisRate" runat="server" CssClass="item3" Visible="false"></asp:Label>
                                                                                                                                                                    <asp:Label ID="lblDispTotalRate" runat="server" CssClass="item3" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td width="1px"></td>
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <asp:Label ID="lblTotalDis" runat="server" CssClass="item3" Font-Bold="true" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <asp:Label ID="lblDispIncVAT" runat="server" CssClass="item3" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td width="1px"></td>
                                                                                                                                                                <td align="right">
                                                                                                                                                                    <asp:Label ID="lblTotalVAT" runat="server" CssClass="item2" Font-Bold="true" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr style="display: none">
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <asp:Label ID="lblDispIncCST" runat="server" CssClass="item" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td width="1px"></td>
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <asp:Label ID="lblTotalCST" runat="server" CssClass="item3" Font-Bold="true" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <asp:Label ID="lblDispLoad" runat="server" CssClass="item3" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td width="1px"></td>
                                                                                                                                                                <td align="right">
                                                                                                                                                                    <asp:Label ID="lblFreight" runat="server" CssClass="item2" Font-Bold="true" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>

                                                                                                                                                            <tr>
                                                                                                                                                                <td align="left">
                                                                                                                                                                    <asp:Label ID="lblDispGrandTtl" runat="server" CssClass="item3" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                <td width="1px"></td>
                                                                                                                                                                <td align="right">
                                                                                                                                                                    <asp:Label ID="lblNet1" runat="server" CssClass="item2" Font-Bold="true" Text="0" Visible="false"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                        <%-- </div>--%>
                                                                                                                                                    </div>
                                                                                                                                                </div>
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 13px"></td>
                                                                                                                                            <td></td>
                                                                                                                                            <td style="text-align: right">
                                                                                                                                                <div style="text-align: right">
                                                                                                                                                    <asp:Panel ID="PanelCmd" runat="server">
                                                                                                                                                        <table align="right">
                                                                                                                                                            <tr>
                                                                                                                                                                <td></td>
                                                                                                                                                                <td style="padding: 1px;">
                                                                                                                                                                    <asp:Button ID="AddNewProd" runat="server" Text="" CssClass="addproductbutton6" EnableTheming="false"
                                                                                                                                                                        SkinID="skinBtnAddProduct" OnClick="lnkAddProduct_Click" Visible="false" />
                                                                                                                                                                </td>
                                                                                                                                                                <td style="padding: 1px;"></td>
                                                                                                                                                                <td style="width: 13px"></td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td></td>
                                                                                                                                                                <td style="padding: 1px;">
                                                                                                                                                                    <asp:Button ID="CmdProd" runat="server" Text=""
                                                                                                                                                                        OnClick="CmdProd_Click" EnableTheming="false" CssClass="Newproductbutton6"
                                                                                                                                                                        Width="28px" Height="27px" Visible="false" />
                                                                                                                                                                </td>
                                                                                                                                                                <td style="padding: 1px;"></td>
                                                                                                                                                                <td style="width: 13px"></td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </asp:Panel>
                                                                                                                                                </div>
                                                                                                                                            </td>
                                                                                                                                            <td style="text-align: right">
                                                                                                                                                <div style="text-align: right">
                                                                                                                                                    <asp:Panel ID="Panel7" runat="server">
                                                                                                                                                        <table style="width: 100%;">
                                                                                                                                                            <tr>
                                                                                                                                                                <td></td>
                                                                                                                                                                <td style="width: 30%;"></td>
                                                                                                                                                                <td style="width: 15%"></td>
                                                                                                                                                                <td style="width: 25%;" class="ControlLabel">Freight Charges                                                                                                                                                               
                                                                                                                                                                <td style="width: 14%;">
                                                                                                                                                                    <asp:TextBox ID="txtFreight" Style="text-align: right" runat="server" AutoPostBack="True" BorderWidth="1px" SkinID="skinTxtBox" TabIndex="6" Text="0" ValidationGroup="product" Width="200px" OnTextChanged="txtFreight_TextChanged1"></asp:TextBox>
                                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtFreight" ValidChars="." />
                                                                                                                                                                </td>
                                                                                                                                                                    <td style="width: 26%;"></td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td></td>
                                                                                                                                                                <td style="width: 30%;"></td>
                                                                                                                                                                <td style="width: 15%"></td>
                                                                                                                                                                <td style="width: 25%;" class="ControlLabel">Loading / Unloading Charges                                                                                                                                                               
                                                                                                                                                                <td style="width: 14%;">
                                                                                                                                                                    <asp:TextBox ID="txtLU" Style="text-align: right" runat="server" AutoPostBack="True" BorderWidth="1px" SkinID="skinTxtBox" TabIndex="7" Text="0" ValidationGroup="product" Width="200px" OnTextChanged="txtFreight_TextChanged1"></asp:TextBox>
                                                                                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtLU" ValidChars="." />
                                                                                                                                                                </td>
                                                                                                                                                                    <td style="width: 26%;"></td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td></td>
                                                                                                                                                                <td style="width: 30%;"></td>
                                                                                                                                                                <td style="width: 15%"></td>
                                                                                                                                                                <td style="width: 25%" class="ControlLabel">Over Disc Amount                                                                                                        
                                                                                                                                                                </td>
                                                                                                                                                                <td style="width: 14%">
                                                                                                                                                                    <asp:TextBox ID="txtdiscamt" Style="text-align: right" runat="server" AutoPostBack="True" BorderWidth="1px" SkinID="skinTxtBox" OnTextChanged="txtFreight_TextChanged1" ValidationGroup="product" Text="0" Width="100%"></asp:TextBox>
                                                                                                                                                                </td>
                                                                                                                                                                <td style="width: 26%;"></td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td></td>
                                                                                                                                                                <td style="width: 30%;"></td>
                                                                                                                                                                <td style="width: 15%"></td>
                                                                                                                                                                <td style="width: 25%" class="ControlLabel">Over Disc %
                                                                                                                                                                </td>
                                                                                                                                                                <td style="width: 14%">
                                                                                                                                                                    <asp:TextBox ID="txtdisc" Style="text-align: right" runat="server" AutoPostBack="True" BorderWidth="1px" OnTextChanged="txtFreight_TextChanged1" CssClass="cssTextBox" ValidationGroup="product" Text="0" Width="100%"></asp:TextBox>
                                                                                                                                                                </td>
                                                                                                                                                                <td style="width: 26%"></td>
                                                                                                                                                            </tr>
                                                                                                                                                            <tr>
                                                                                                                                                                <td></td>
                                                                                                                                                                <td style="width: 30%;"></td>
                                                                                                                                                                <td style="width: 15%"></td>
                                                                                                                                                                <td style="width: 25%;" class="ControlLabel">Grand Total(INR)   
                                                                                                                                                                <td style="width: 14%;">
                                                                                                                                                                    <%--<asp:TextBox ID="TextBox3" Style="text-align: right" runat="server" AutoPostBack="True" SkinID="skinTxtBox" TabIndex="7" Text="0" Width="200px"></asp:TextBox>--%>
                                                                                                                                                                    <asp:Label ID="lblNet" Style="text-align: right" runat="server" AutoPostBack="True" CssClass="ControlLabelproject" Text="0"></asp:Label>
                                                                                                                                                                </td>
                                                                                                                                                                    <td style="width: 26%;"></td>
                                                                                                                                                            </tr>
                                                                                                                                                        </table>
                                                                                                                                                    </asp:Panel>
                                                                                                                                                </div>
                                                                                                                                            </td>
                                                                                                                                        </tr>
                                                                                                                                    </table>


                                                                                                                                </ContentTemplate>

                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ContentTemplate>
                                                                                                        </cc1:TabPanel>

                                                                                                        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Additional Details">

                                                                                                            <ContentTemplate>
                                                                                                                <table class="tblLeft" width="1200px" cellpadding="0" cellspacing="1">
                                                                                                                    <tr>
                                                                                                                        <td style="width: 20%" class="ControlLabelproject">Voucher No. *
                                                                                                                        </td>
                                                                                                                        <td class="ControlTextBox3" style="width: 23%">
                                                                                                                            <asp:Label ID="txtInvoiveNo" runat="server" Height="30px" BackColor="#e7e7e7"></asp:Label>
                                                                                                                        </td>
                                                                                                                        <td class="ControlLabelproject" style="width: 20%">Narration
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%" class="ControlTextBox3">
                                                                                                                            <asp:TextBox ID="txtnarr" runat="server" SkinID="skinTxtBox" Text="0" ValidationGroup="product" BackColor="#e7e7e7" TabIndex="6" Height="24px"
                                                                                                                                Width="200px"></asp:TextBox>
                                                                                                                        </td>
                                                                                                                        <td style="width: 35%"></td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <%-- <tr>
                                                                                                                        <td style="width: 20%" class="ControlLabel">Loading / Unloading
                                                                                                                        </td>
                                                                                                                        <td class="ControlTextBox3" style="width: 23%">
                                                                                                                            <asp:TextBox ID="txtLU" runat="server" SkinID="skinTxtBox" Text="0" ValidationGroup="product" BackColor="#e7e7e7" TabIndex="6" Height="24px"
                                                                                                                                AutoPostBack="True" Width="200px" OnTextChanged="txtFreight_TextChanged"></asp:TextBox>
                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers" Enabled="True"
                                                                                                                                TargetControlID="txtLU" ValidChars="." />
                                                                                                                        </td>
                                                                                                                        <td class="ControlLabel" style="width: 20%">Freight
                                                                                                                        </td>
                                                                                                                        <td class="ControlTextBox3" style="width: 23%">
                                                                                                                            <asp:TextBox ID="txtFreight" BackColor="#e7e7e7" Width="200px" Height="23px" runat="server" SkinID="skinTxtBox" Text="0" TabIndex="7" ValidationGroup="product"></asp:TextBox>
                                                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers" Enabled="True"
                                                                                                                                TargetControlID="txtFreight" ValidChars="." />
                                                                                                                        </td>
                                                                                                                        <td style="width: 35%"></td>
                                                                                                                    </tr>--%>
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 20%" class="ControlLabelproject">Bilits
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%" class="ControlDrpBorder">
                                                                                                                            <asp:DropDownList ID="ddBilts" AutoPostBack="false" AppendDataBoundItems="true" runat="server" BackColor="#e7e7e7" Width="100%"
                                                                                                                                CssClass="drpDownListMedium" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td class="ControlLabelproject" style="width: 20%">Internal Transfer
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%" class="ControlDrpBorder">
                                                                                                                            <asp:DropDownList ID="drpIntTrans" AutoPostBack="false" runat="server" CssClass="drpDownListMedium" BackColor="#e7e7e7" Style="border: 1px solid #e7e7e7" Height="26px" Width="100%">
                                                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 35%"></td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td style="width: 20%" class="ControlLabelproject">Sales Return
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%" class="ControlDrpBorder">
                                                                                                                            <asp:DropDownList ID="drpSalesReturn" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drpSalesReturn_SelectedIndexChanged"
                                                                                                                                CssClass="drpDownListMedium" BackColor="#e7e7e7" Width="100%" Style="border: 1px solid #e7e7e7" Height="26px">
                                                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td class="ControlLabelproject" style="width: 20%">Delivery Note
                                                                                                                        </td>
                                                                                                                        <td style="width: 23%" class="ControlDrpBorder">
                                                                                                                            <asp:DropDownList ID="ddDeliveryNote" TabIndex="10" AutoPostBack="false" runat="server" BackColor="#e7e7e7" Width="100%"
                                                                                                                                CssClass="drpDownListMedium" Style="border: 1px solid #e7e7e7" Height="26px" OnSelectedIndexChanged="ddDeliveryNote_SelectedIndexChanged">
                                                                                                                                <asp:ListItem Text="NO" Value="NO" Selected="True"></asp:ListItem>
                                                                                                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                                                                                            </asp:DropDownList>
                                                                                                                        </td>
                                                                                                                        <td style="width: 35%"></td>
                                                                                                                    </tr>

                                                                                                                    <tr>
                                                                                                                        <td colspan="5">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel222" RenderMode="Inline" runat="server" UpdateMode="Conditional">
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="drpSalesReturn" EventName="SelectedIndexChanged" />
                                                                                                                                </Triggers>
                                                                                                                                <ContentTemplate>
                                                                                                                                    <table runat="server" id="rowSalesRet" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                                        <tr style="height: 2px">
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                                            <td class="ControlLabelproject" style="width: 19.7%">
                                                                                                                                                <asp:RequiredFieldValidator ID="rqSalesReturn" runat="server" ErrorMessage="Reason is mandatory"
                                                                                                                                                    Text="*" Enabled="false" ControlToValidate="txtSRReason" ValidationGroup="purchaseval" />
                                                                                                                                                Sales Return Reason *
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 23.5%" class="ControlTextBox3">
                                                                                                                                                <asp:TextBox ID="txtSRReason" runat="server" TextMode="MultiLine" CssClass="cssTextBox" BackColor="#e7e7e7" Height="23px"
                                                                                                                                                    MaxLength="200" Width="90%"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 20%"></td>
                                                                                                                                            <td style="width: 23%"></td>
                                                                                                                                            <td style="width: 35%"></td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                    </tr>
                                                                                                                    <tr style="height: 2px">
                                                                                                                    </tr>
                                                                                                                    <tr>
                                                                                                                        <td colspan="5">
                                                                                                                            <asp:UpdatePanel ID="UpdatePanel1" RenderMode="Inline" runat="server" UpdateMode="Conditional">
                                                                                                                                <Triggers>
                                                                                                                                    <asp:AsyncPostBackTrigger ControlID="ddDeliveryNote" EventName="SelectedIndexChanged" />
                                                                                                                                </Triggers>
                                                                                                                                <ContentTemplate>
                                                                                                                                    <table runat="server" id="rowdcnum" cellpadding="0" cellspacing="0" width="100%">
                                                                                                                                        <tr>
                                                                                                                                            <td style="width: 19.7%" class="ControlLabelproject">
                                                                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Dc No is mandatory"
                                                                                                                                                    Text="*" Enabled="false" ControlToValidate="txtdcbillno" ValidationGroup="purchaseval" />
                                                                                                                                                DC Bill No *
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 23.5%" class="ControlTextBox3">
                                                                                                                                                <asp:TextBox ID="txtdcbillno" runat="server" TextMode="MultiLine" CssClass="cssTextBox" BackColor="#e7e7e7" Height="23px"
                                                                                                                                                    MaxLength="200" Width="90%"></asp:TextBox>
                                                                                                                                            </td>
                                                                                                                                            <td style="width: 13%"></td>
                                                                                                                                            <td style="width: 23%"></td>
                                                                                                                                            <td style="width: 35%"></td>
                                                                                                                                        </tr>
                                                                                                                                    </table>
                                                                                                                                </ContentTemplate>
                                                                                                                            </asp:UpdatePanel>
                                                                                                                        </td>
                                                                                                                        <td></td>
                                                                                                                        <td></td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </ContentTemplate>
                                                                                                        </cc1:TabPanel>
                                                                                                    </cc1:TabContainer>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="4" style="width: 1200px">
                                                                                                    <table style="width: 100%">
                                                                                                        <tr>
                                                                                                            <td style="width: 18%"></td>
                                                                                                            <td style="width: 5%">
                                                                                                                <asp:Button ID="CmdCat" runat="server" Text=""
                                                                                                                    OnClick="cmdcat_click" EnableTheming="false" CssClass="NewSupplierbutton6"
                                                                                                                    Width="28px" Height="27px" Visible="false" />
                                                                                                            </td>
                                                                                                            <td style="width: 17%">
                                                                                                                <asp:Button ID="cmdUpdate" ValidationGroup="purchaseval" runat="server" Text="" CssClass="Updatebutton1231"
                                                                                                                    EnableTheming="false" OnClick="cmdUpdate_Click" SkinID="skinBtnSave" />
                                                                                                                <asp:Button ID="cmdSave" ValidationGroup="purchaseval" runat="server" Text="" CssClass="savebutton1231"
                                                                                                                    EnableTheming="false" OnClick="cmdSave_Click" SkinID="skinBtnSave" />
                                                                                                            </td>
                                                                                                            <td style="width: 17%">
                                                                                                                <asp:Button ID="btnCancel" runat="server" Text="" CssClass="cancelbutton6" EnableTheming="false"
                                                                                                                    Visible="true" OnClick="cmdCancel_Click" SkinID="skinBtnCancel" />
                                                                                                            </td>
                                                                                                            <td style="width: 17%">
                                                                                                                <asp:Button ID="cmdPrint" CausesValidation="false" runat="server" Text="" Enabled="false"
                                                                                                                    CssClass="printbutton6" EnableTheming="false" OnClick="cmdPrint_Click" SkinID="skinBtnPrint" />
                                                                                                            </td>
                                                                                                            <td style="width: 24%"></td>
                                                                                                        </tr>
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
                                                            <asp:ObjectDataSource ID="empSrc" runat="server" SelectMethod="ListExecutive" TypeName="BusinessLogic">
                                                                <SelectParameters>
                                                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                </SelectParameters>
                                                            </asp:ObjectDataSource>
                                                            <asp:ObjectDataSource ID="srcCreditorDebitor" runat="server" SelectMethod="ListCreditorDebitor"
                                                                TypeName="BusinessLogic"></asp:ObjectDataSource>
                                                            <asp:ObjectDataSource ID="srcBanks" runat="server" SelectMethod="ListBanks" TypeName="BusinessLogic">
                                                                <SelectParameters>
                                                                    <asp:CookieParameter Name="connection" CookieName="Company" Type="String" />
                                                                </SelectParameters>
                                                            </asp:ObjectDataSource>
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

                    <tr style="width: 100%">
                        <td style="width: 100%;">
                            <asp:Panel ID="PanelBill" Direction="LeftToRight" ClientIDMode="Static" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GrdViewPurchase" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                CssClass="someClass" Width="100%" DataKeyNames="PurchaseID" AllowPaging="True"
                                                EmptyDataText="No Purchase Details Found" OnPageIndexChanging="GrdViewPurchase_PageIndexChanging"
                                                OnRowCommand="GrdViewPurchase_RowCommand" OnRowEditing="GrdViewPurchase_RowEditing"
                                                OnSelectedIndexChanged="GrdViewPurchase_SelectedIndexChanged" OnRowCreated="GrdViewPurchase_RowCreated"
                                                OnRowDataBound="GrdViewPurchase_RowDataBound" OnRowDeleting="GrdViewPurchase_RowDeleting">
                                                <HeaderStyle Height="30px" HorizontalAlign="Center" Font-Bold="true" BackColor="#cccccc" BorderColor="Gray" Font-Size="Small" />
                                                <RowStyle Font-Bold="true" HorizontalAlign="Center" Height="30px" Font-Size="Small" ForeColor="#0567AE" />
                                                <Columns>
                                                    <asp:BoundField DataField="PurchaseID" HeaderText="Voucher No" HeaderStyle-Width="50px" HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="TransNo" HeaderText="Trans. No." HeaderStyle-Wrap="false" HeaderStyle-BorderColor="Gray"
                                                        HeaderStyle-Width="50px" />
                                                    <asp:BoundField DataField="Billno" HeaderText="Bill No." HeaderStyle-Width="60px" HeaderStyle-BorderColor="Gray"
                                                        HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="InvoiceDate" HeaderText="Voucher Date" HeaderStyle-Width="65px" HeaderStyle-BorderColor="Gray" />
                                                    <asp:TemplateField HeaderText="Payment Mode" HeaderStyle-BorderColor="Gray">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPaymode" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Supplier" HeaderStyle-Width="130px" HeaderText="Supplier" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="Chequeno" Visible="false" HeaderText="Chequeno" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="Creditor" HeaderStyle-Width="130px" HeaderText="Creditor" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Width="60px" DataFormatString="{0:F2}" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="SalesReturn" HeaderText="Sales Return" HeaderStyle-Wrap="true" HeaderStyle-BorderColor="Gray" />
                                                    <asp:BoundField DataField="SalesReturnReason" HeaderText="Sales Return - Reason" HeaderStyle-BorderColor="Gray"
                                                        Visible="false" />
                                                    <asp:BoundField DataField="SupplierID" HeaderText="Supplier ID" Visible="false" HeaderStyle-BorderColor="Gray" />
                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderStyle-Width="45px" HeaderText="Edit" HeaderStyle-BorderColor="Gray">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" SkinID="edit" CommandName="Select" />
                                                            <asp:ImageButton ID="btnEditDisabled" Enabled="false" SkinID="editDisable" runat="Server"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="45px" HeaderText="Print" HeaderStyle-BorderColor="Gray">
                                                        <ItemTemplate>
                                                            <a href='<%# DataBinder.Eval(Container, "DataItem.PurchaseID", "javascript:PrintItem({0});") %>'>
                                                                <asp:Image runat="server" ID="lnkprint" alt="Print" border="0" src="App_Themes/DefaultTheme/Images/Print1.png" />
                                                            </a>
                                                            <asp:ImageButton ID="btnViewDisabled" Enabled="false" SkinID="search" runat="Server"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="command" HeaderText="Delete" HeaderStyle-Width="45px" HeaderStyle-BorderColor="Gray">
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <cc1:ConfirmButtonExtender ID="CnrfmDel" TargetControlID="lnkB" ConfirmText="Are you sure to Delete this Purchase?"
                                                                runat="server">
                                                            </cc1:ConfirmButtonExtender>
                                                            <asp:ImageButton ID="lnkB" SkinID="delete" runat="Server" CommandName="Delete"></asp:ImageButton>
                                                            <asp:ImageButton ID="lnkBDisabled" Enabled="false" SkinID="deleteDisable" runat="Server"></asp:ImageButton>
                                                            <asp:HiddenField ID="ldgID" runat="server" Value='<%# Bind("PurchaseID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerTemplate>
                                                    <table style="border-color: white">
                                                        <tr style="border-color: white">
                                                            <td style="border-color: white">Goto Page
                                                            </td>
                                                            <td style="border-color: white">
                                                                <asp:DropDownList ID="ddlPageSelector" OnSelectedIndexChanged="ddlPageSelector_SelectedIndexChanged"
                                                                    runat="server" AutoPostBack="true" BackColor="#e7e7e7" Width="70px" Height="24px" Style="border: 1px solid blue">
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
                                                <SelectedRowStyle BackColor="#E3F6CE" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <input type="hidden" value="" id="hdDel" runat="server" />
                                <input type="hidden" id="delFlag" value="0" runat="server" />
                                <asp:HiddenField ID="hdToDelete" Value="0" runat="server" />
                                <asp:HiddenField ID="hdEmailRequired" runat="server" Value="NO" />
                                <asp:HiddenField ID="hdSMSRequired" runat="server" Value="NO" />
                            </asp:Panel>
                            <asp:Label ID="err" runat="server" Style="color: Red; font-weight: bold; font-family: 'Trebuchet MS'; font-size: 11px;"
                                Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table align="center">
        <tr>
            <td style="width: 50%">
                <asp:Panel ID="pnlSearch" runat="server" Width="100px">
                    <asp:Button ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="ButtonAdd66"
                        EnableTheming="false" Width="80px" ForeColor="White"></asp:Button>
                </asp:Panel>
            </td>
            <%--<td>
                <asp:Button ID="btnpurchase" runat="server" CssClass="NewReport6"
                     EnableTheming="false" Width="80px"  OnClientClick="window.open('ReportExlPurchase.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=550,width=790,left=290,top=60, scrollbars=yes');"></asp:Button>
            </td>--%>
            <td style="width: 50%">
                <asp:Button ID="btnpur" runat="server"
                    CssClass="exportexl6" EnableTheming="false" CausesValidation="false"
                    OnClientClick="window.open('ReportXLPurchase.aspx','CSTSummary', 'toolbar=no,status=no,menu=no,location=no,resizable=yes,height=360,width=650,left=370,top=220, scrollbars=yes');"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
